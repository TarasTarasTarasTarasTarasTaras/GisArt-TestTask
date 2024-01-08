import { Component, OnInit } from '@angular/core';
import { Trip } from '../../models/trip';
import { ExpeditionService } from '../../services/expedition.service';
import { TripSort } from '../../models/trip-sort';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trips',
  templateUrl: './trips.component.html',
  styleUrls: ['./trips.component.css']
})
export class TripsComponent implements OnInit {
  formData = {
    id: '',
    driverId: '',
    pickUp: '',
    dropOff: '',
    ordering: TripSort.Id
  };

  public allTrips: Trip[] = [];

  public tripSortValues: any = [];

  constructor(private expeditionService: ExpeditionService, private router: Router) { }

  ngOnInit() {
    this.getAllTrips();

    this.tripSortValues = Object.keys(TripSort)
      .filter(key => !isNaN(Number(TripSort[key as keyof typeof TripSort])))
      .map(key => ({ name: key, value: Number(TripSort[key as keyof typeof TripSort])}));
  }

  getAllTrips(queryParams: any = null) {
    this.expeditionService.getAllTrips(queryParams).subscribe(expeditions => {
      this.allTrips = expeditions;
    });
  }

  onlyNumbers($event: any) {
    const input = $event.target.value;
    $event.target.value = input.replace(/[^0-9]/g, '');
  }

  search() {
    this.getAllTrips(this.formData);
  }

  navigateToDrivers() {
    this.router.navigate(['expedition/drivers']);
  }
}
