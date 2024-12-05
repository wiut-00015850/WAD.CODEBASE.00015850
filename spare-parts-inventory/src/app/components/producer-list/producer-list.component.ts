import { Component, OnInit } from '@angular/core';
import { ProducerService } from '../../services/producer.service';
import { Producer } from '../../models/producer.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
@Component({
  selector: 'app-producer-list',
  templateUrl: './producer-list.component.html',
  styleUrls: ['./producer-list.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule, MatIconModule, MatPaginatorModule, MatFormFieldModule],
})
export class ProducerListComponent implements OnInit {
  producers: Producer[] = [];

  constructor(private producerService: ProducerService) {}

  ngOnInit() {
    this.loadProducers();
  }

  loadProducers() {
    this.producerService.getAllProducers().subscribe({
      next: (data) => {
        this.producers = data;
      },
      error: (err) => {
        console.error('Error fetching producers:', err);
      }
    });
  }

  deleteProducer(id: number) {
    this.producerService.deleteProducer(id).subscribe({
      next: () => {
        this.loadProducers(); // Refresh the list after deletion
      },
      error: (err) => {
        console.error('Error deleting producer:', err);
      }
    });
  }
}


