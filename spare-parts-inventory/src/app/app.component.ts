import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ProducerListComponent } from './components/producer-list/producer-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    ProducerListComponent
  ]
})
export class AppComponent {
  title = 'spare-parts-inventory';
}

