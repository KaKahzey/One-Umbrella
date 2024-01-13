import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarouselModule } from 'primeng/carousel';
import { onlyBlob } from '../../../shared/models/shared/onlyBlob';
import { Restaurant } from '../../../shared/models/restaurant/restaurant';
import { ApiService } from '../../../shared/services/api.service';
import { TabViewModule } from 'primeng/tabview';
import { Grid } from '../../../shared/models/restaurant/grid';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { SelectButtonModule } from 'primeng/selectbutton';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [CarouselModule, TabViewModule, DialogModule, InputTextModule, ReactiveFormsModule, FormsModule, ButtonModule, SelectButtonModule],
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.scss'
})
export class RestaurantComponent {
  restaurantId: number = 0;
  restaurant : Restaurant = {restaurantId : this.restaurantId, restaurantName : "", ownerId : 0, restaurantStreet : "", restaurantCity : "", restaurantPostCode : "", restaurantRating : 0, restaurantDescription : "", images : []}
  responsiveOptions: any[] | undefined
  hasMenu : boolean = false
  showNewGridForm : boolean = false

  grids : Grid[] = []
  newGrid : Grid = {gridId : 0, gridName : "", restaurantId : 0, gridRows : 0, gridColumns : 0, gridElements : [], gridTables : []}
  

  visible : boolean = false
  loading: boolean = false

  sizeForm : FormGroup
  nameForm : FormGroup
  newArrayRows : Array<any> = []
  newArrayColumns : Array<any> = []

  newArrayColumnsSubject = new BehaviorSubject<Array<number>>([]);
  newArrayRowsSubject = new BehaviorSubject<Array<number>>([]);

  constructor(private _route: ActivatedRoute, private _apiService : ApiService, private _fb : FormBuilder) {
    this.sizeForm = this._fb.group({
      rows :[null, [Validators.required]],
      columns :[null, [Validators.required]]
    })
    this.nameForm = this._fb.group({
      name :[null, [Validators.required]]
    })
  }

  ngOnInit(): void {
    this._route.params.subscribe(data => {
      this.restaurantId = parseInt(data['id'])
    })

    this._apiService.getRestaurantById(this.restaurantId).subscribe({
      next : resp => this.restaurant = resp,
      error : error => console.log(error)
    })

    this.responsiveOptions = [
      {
          breakpoint: '1199px',
          numVisible: 1,
          numScroll: 1
      },
      {
          breakpoint: '1199px',
          numVisible: 2,
          numScroll: 1
      }
    ]
  }
  verifyMenu(): boolean | undefined {
    const menuExists = this.restaurant?.images;
    return menuExists?.some(i => i.isMenu === true);
  }

  setNewGridSize() : void {
    const newRows = parseInt(this.sizeForm.get("rows")?.value)
    const newColumns = parseInt(this.sizeForm.get("columns")?.value)
    if(newRows && newColumns){
      this.newGrid.gridRows = newRows
      this.newGrid.gridColumns = newColumns
      this.newGrid.restaurantId = this.restaurantId
      this.load()
      this.newArrayRows = Array(newRows).fill(0).map((_, index) => index + 1)
      this.newArrayColumns = Array(newColumns).fill(0).map((_, index) => index + 1)
      this.newArrayRowsSubject.next(this.newArrayRows);
    this.newArrayColumnsSubject.next(this.newArrayColumns);
    }
  }
  

  load() {
    this.loading = true;

    setTimeout(() => {
        this.loading = false
    }, 2000);
  }

}
