import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProducerService } from '../../services/producer.service';
import { Producer } from '../../models/producer.model';
import { FormsModule } from '@angular/forms'; // Import FormsModule for ngModel to work

@Component({
  selector: 'app-producer-edit',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // Add FormsModule to imports
  templateUrl: './producer-edit.component.html',
  styleUrls: ['./producer-edit.component.css']
})
export class ProducerEditComponent implements OnInit {
  producer: Producer | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private producerService: ProducerService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadProducer(id);
    }
  }

  loadProducer(id: string): void {
    this.producerService.getProducerById(id).subscribe(
      (data) => {
        this.producer = data;
      },
      (error) => {
        console.error('Error loading producer', error);
      }
    );
  }

  saveProducer(): void {
    if (this.producer) {
      this.producerService.updateProducer(this.producer.id, this.producer).subscribe(
        () => {
          this.router.navigate(['/producers']);
        },
        (error) => {
          console.error('Error saving producer', error);
        }
      );
    }
  }
}
