import { Action, Selector, State, StateContext } from "@ngxs/store";
import { Driver } from "../expedition/models/driver";
import { Injectable } from "@angular/core";
import { ExpeditionService } from "../expedition/services/expedition.service";
import { GetAllDrivers } from "../expedition/actions/get-all-drivers";
import { tap } from "rxjs";

export interface DriversStateModel {
    drivers: Driver[];
  }
  
  @State<DriversStateModel>({
    name: 'drivers',
    defaults: {
      drivers: []
    }
  })
  @Injectable()
  export class DriversState {
    constructor(private expeditionService: ExpeditionService) {}
  
    @Action(GetAllDrivers)
    public getAllDrivers({ patchState }: StateContext<DriversStateModel>, { queryParams }: GetAllDrivers) {
      return this.expeditionService.getAllDrivers(queryParams).pipe(
        tap(drivers => {
          patchState({ drivers });
        })
      );
    }

    @Selector([DriversState])
    static getAllDrivers(state: DriversStateModel) {
        return state.drivers;
    }
  }