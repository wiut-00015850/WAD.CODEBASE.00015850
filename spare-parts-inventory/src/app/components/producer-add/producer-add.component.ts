import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Producer } from '../../models/producer.model';
import { ProducerService } from '../../services/producer.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms'; // Added to fix the 'formGroup' binding issue

@Component({
  selector: 'app-producer-add',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule], // Added ReactiveFormsModule to imports
  templateUrl: './producer-add.component.html',
  styleUrl: './producer-add.component.css'
})
export class ProducerAddComponent {
  producerForm: FormGroup;
  producer: Producer;

  constructor(private producerService: ProducerService, private router: Router) {
    this.producer = {
      id: 0,
      name: '',
      country: '',
      foundedDate: new Date()
    }

    this.producerForm = new FormGroup({
      name: new FormControl('', Validators.required),
      country: new FormControl('', Validators.required),
      foundedDate: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.producer = this.producerForm.value;
    console.log(this.producer);
    this.producerService.addProducer(this.producer).subscribe(
      data => console.log('Success!', data),
      error => console.error('Error!', error)
    );

    this.router.navigate(['/']);
  }
}