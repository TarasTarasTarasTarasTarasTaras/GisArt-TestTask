import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpeditionRoutingModule } from './expedition-routing.module';
import { TripsComponent } from './components/trips/trips.component';
import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { DriversComponent } from './components/drivers/drivers.component';

@NgModule({
  imports: [
    CommonModule,
    ExpeditionRoutingModule,
    GridModule,
    InputsModule,
    FormsModule,
    ButtonsModule,
    DropDownsModule
  ],
  declarations: [
    TripsComponent,
    DriversComponent
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class ExpeditionModule { }
