import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TripsComponent } from './components/trips/trips.component';
import { DriversComponent } from './components/drivers/drivers.component';

const routes: Routes = [
    { path: 'trips', component: TripsComponent },
    { path: 'drivers', component: DriversComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExpeditionRoutingModule { }
