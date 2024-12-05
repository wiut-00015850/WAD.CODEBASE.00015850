import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producer } from '../models/producer.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProducerService {
  private apiUrl = 'https://localhost:7288/Api/Producers'; // Update with your API URL

  constructor(private http: HttpClient) { }

  getAllProducers(): Observable<Producer[]> {
    return this.http.get<Producer[]>(this.apiUrl).pipe(
      map((producers: Producer[]) => {
        return producers.map(producer => {
          producer.foundedDate = new Date(producer.foundedDate);
          return producer;
        });
      })
    );
  }

  getProducerById(id: string): Observable<Producer> {
    return this.http.get<Producer>(`${this.apiUrl}/${id}`).pipe(
      map((producer: Producer) => {
        producer.foundedDate = new Date(producer.foundedDate);
        return producer;
      })
    );
  }

  addProducer(producer: Producer): Observable<Producer> {
    producer.foundedDate = new Date(producer.foundedDate);
    return this.http.post<Producer>(this.apiUrl, producer);
  }

  updateProducer(id: number, producer: Producer): Observable<Producer> {
    producer.foundedDate = new Date(producer.foundedDate);
    return this.http.put<Producer>(`${this.apiUrl}/${id}`, producer);
  }

  deleteProducer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
