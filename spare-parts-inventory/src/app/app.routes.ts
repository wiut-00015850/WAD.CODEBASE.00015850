import { Routes } from '@angular/router';
import { ProducerListComponent } from './components/producer-list/producer-list.component';
import { ProducerEditComponent } from './components/producer-edit/producer-edit.component';
import { ProducerAddComponent } from './components/producer-add/producer-add.component';

export const routes: Routes = [
    { path: '', component: ProducerListComponent },
    { path: 'producer-list', component: ProducerListComponent },
    { path: 'producers/edit/:id', component: ProducerEditComponent },
    { path: 'producers/add', component: ProducerAddComponent }
];
