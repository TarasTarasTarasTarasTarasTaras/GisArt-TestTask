import { Component } from '@angular/core';
import { DriverSort } from '../../models/driver-sort';
import { Driver } from '../../models/driver';
import { Store } from '@ngxs/store';
import { GetAllDrivers } from '../../actions/get-all-drivers';
import { Router } from '@angular/router';
import { DriversState } from 'src/app/modules/states/drivers-state-model';

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html',
  styleUrls: ['./drivers.component.css']
})
export class DriversComponent {
  formData = {
    driverId: ''
  };

  public allDrivers: Driver[] = [];

  public driverSortValues: any = [];
  public sortValue = DriverSort[DriverSort.DriverId];

  constructor(private store: Store, private router: Router) { }

  ngOnInit() {
    this.store.select(state => state.drivers.drivers).subscribe(drivers => {
      this.allDrivers = drivers;
    });

    this.getAllDrivers();

    this.driverSortValues = Object.keys(DriverSort)
      .filter(key => !isNaN(Number(DriverSort[key as keyof typeof DriverSort])));
  }

  getAllDrivers(queryParams: any = null) {
    this.store.dispatch(new GetAllDrivers(queryParams));
  }

  calculatePayableTime() {
    this.getAllDrivers({ driverId: this.formData.driverId, calculatePayableTime: true });
  }
  
  onlyNumbers($event: any) {
    const input = $event.target.value;
    $event.target.value = input.replace(/[^0-9]/g, '');
  }

  search() {
    this.getAllDrivers(this.formData);
  }

  sort() {
    if (this.sortValue == DriverSort[DriverSort.DriverId]) {
      this.allDrivers = this.allDrivers.slice().sort((a, b) => a.driverId - b.driverId);
    }
    else if (this.sortValue == DriverSort[DriverSort.PayableTime]) {
      const dates: { driver: Driver; date: Date }[] = this.allDrivers.map(driver => {
        const [hours, minutes, seconds] = driver.payableTime.split(":");
        const date = new Date();
        date.setHours(Number(hours), Number(minutes), Number(seconds));
        return { driver, date };
      });
  
      dates.sort((a, b) => a.date.getTime() - b.date.getTime());
      this.allDrivers = dates.map(item => item.driver);
    }
  }

  navigateToTrips() {
    this.router.navigate(['expedition/trips']);
  }
}
