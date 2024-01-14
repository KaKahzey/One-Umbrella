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
import { StructuralElement } from '../../../shared/models/restaurant/structuralElement';
import { Table } from '../../../shared/models/restaurant/table';
import { GridData } from '../../../shared/models/restaurant/gridData';
import { StructuralElementData } from '../../../shared/models/restaurant/structuralElementData';
import { TableData } from '../../../shared/models/restaurant/tableData';
import { ReservationData } from '../../../shared/models/reservations/reservationData';
import { Reservation } from '../../../shared/models/reservations/reservation';
import { ReservedTable } from '../../../shared/models/reservations/reservedTable';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [CarouselModule, TabViewModule, DialogModule, InputTextModule, ReactiveFormsModule, FormsModule, ButtonModule, SelectButtonModule],
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.scss'
})
export class RestaurantComponent {
  restaurantId: number = 0
  userId : number = 0
  restaurant : Restaurant = {restaurantId : this.restaurantId, restaurantName : "", ownerId : 0, restaurantStreet : "", restaurantCity : "", restaurantPostCode : "", restaurantRating : 0, restaurantDescription : "", images : []}
  responsiveOptions: any[] | undefined
  hasMenu : boolean = false
  showNewGridForm : boolean = false

  // Reservations
  reservations : Reservation[] = []
  reservedTables : ReservedTable[] = []
  newReservation : ReservationData = {restaurantId : 0, humanId : 0, reservationTimeStart : new Date(), reservationTimeEnd : new Date(), tableId : 0}
  selectedTime : Date = new Date()

  grids : Grid[] = []
  newGrid : GridData = {gridName : "", restaurantId : 0, gridRows : 0, gridColumns : 0, gridElements : [], gridTables : []}
  
  visibleFirst : boolean = false
  visibleSecond : boolean = false
  visibleThird : boolean = false
  visibleCreate : boolean = false
  loading: boolean = false

  sizeForm : FormGroup
  nameForm : FormGroup
  arrayRows : Array<any> = []
  arrayColumns : Array<any> = []

  elementToPlace : string = ""

  // Liste des logos
  tablePath : string = "/assets/img/grid/table.png"
  wallPath : string = "/assets/img/grid/wall.png"
  windowPath : string = "/assets/img/grid/window.png"
  stairPath : string = "/assets/img/grid/stair.png"
  doorPath : string = "/assets/img/grid/door.png"
  barPath : string = "/assets/img/grid/bar.png"
  tvPath : string = "/assets/img/grid/tv.png"
  //

  constructor(private _route: ActivatedRoute, private _apiService : ApiService, private _authService : AuthService, private _fb : FormBuilder) {
    this.sizeForm = this._fb.group({
      rows :[null, [Validators.required]],
      columns :[null, [Validators.required]],
      name :[null, [Validators.required]]
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
      next : (resp) => {
        this.restaurant = resp
        this._apiService.getAllGridsForOneRestaurant(this.restaurantId).subscribe({
          next : resp => {
            this.grids = resp
            console.log(this.grids);
            
          },
          error : error => console.log(error)
          
        })
      },
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

  // Vérifie si il y a des images du menu
  verifyMenu(): boolean | undefined {
    const menuExists = this.restaurant?.images;
    return menuExists?.some(i => i.isMenu === true);
  }

  // Affichage et création des réservations

  // Set une réservation et lui accorde une table si la cellule
  // visée est valide (une table et valide)
  setReservation(index : number, rowIndex : number, columnIndex : number) : void {
    const foundTable = this.grids[0].gridTables.find(
      (table: Table) =>
      table.rowIndex == rowIndex && table.columnIndex == columnIndex
    )

    if(foundTable){
      this.newReservation.reservationTimeStart = this.selectedTime
      this.newReservation.reservationTimeEnd = this.selectedTime
      const timeEnd = new Date(this.selectedTime)
      timeEnd.setHours(timeEnd.getHours() + 2)
      this.newReservation.reservationTimeEnd = new Date()
      this.newReservation.tableId = foundTable.tableId
      this.newReservation.restaurantId = this.restaurantId
      this.newReservation.humanId = this._authService.getUser()
      console.log(this.newReservation);
      
    }
  }

  // envoie la nouvelle réservation et table réservée au back
  uploadReservation() : void {
    this._apiService.createReservation(this.newReservation).subscribe({
      next : resp => {
          console.log(resp)
      },
      error : error => console.log(error)
    })
  }

  // Affichage et modification des grilles

  // Initialise les données de la nouvelle grille
  setNewGrid() : void {
    const newRows = parseInt(this.sizeForm.get("rows")?.value)
    const newColumns = parseInt(this.sizeForm.get("columns")?.value)
    const gridName = this.sizeForm.get("name")?.value
    if(newRows && newColumns && gridName){
      this.newGrid.gridRows = newRows
      this.newGrid.gridColumns = newColumns
      this.newGrid.gridName = gridName
      this.newGrid.restaurantId = this.restaurantId
      this.setGridSize(newRows, newColumns)
      this.load()
    }
  }

  // Crée des tableaux pour que les @for puissent les utiliser pour 
  // créer les grilles
  setGridSize(rows : number, columns : number) : void {
    this.arrayRows = Array(rows).fill(0).map((_, index) => index + 1)
    this.arrayColumns = Array(columns).fill(0).map((_, index) => index + 1)
  }
  
  //affiche ou non les modales spécifiques
  updateDialog(choice : string) :void {
    switch(choice){
      case "visibleCreate":
        this.visibleCreate = !this.visibleCreate
        break
      case "visibleFirst":
        this.visibleFirst = !this.visibleFirst
        break
      case "visibleSecond":
        this.visibleSecond = !this.visibleSecond
        break
      case "visibleThird":
        this.visibleThird = !this.visibleThird
        break
    }
  }

  // Fait tourner le boutton du formulaire pendant un temps donné
  load() {
    this.loading = true;

    setTimeout(() => {
        this.loading = false
    }, 2000);
  }

  // Modifie l'élément qui sera envoyé dans la grille
  setElementToPlace(element : string) : void {
    this.elementToPlace = element
  }

  // Met un élément dans la nouvelle grille, vérifie d'abord si
  // les coordonnées sont déjà utilisée par une entité et si oui 
  // la supprime
  setElement(element : string, rowIndex : number, columnIndex : number) : void {
    const elementToDelete = this.newGrid.gridElements.findIndex(
      (element: StructuralElementData) =>
        element.rowIndex == rowIndex && element.columnIndex == columnIndex
    )
    const tableToDelete = this.newGrid.gridTables.findIndex(
      (table: TableData) =>
      table.rowIndex == rowIndex && table.columnIndex == columnIndex
    )
    if(elementToDelete !== -1) {
      this.newGrid.gridElements.splice(elementToDelete, 1)
    }
    else if(tableToDelete !== -1) {
      this.newGrid.gridTables.splice(tableToDelete, 1)
    }
    const entity = this.createElement(element, rowIndex, columnIndex)
    if(element == "table") {
      this.newGrid.gridTables.push(entity)
    }
    else{
      this.newGrid.gridElements.push(entity)
    }
  }

  // Crée un objet soit TableData soit StructuralElementData
  createElement(element : string, rowIndex : number, columnIndex : number) : any {
    if(element == "table") {
      const tab : TableData = {
        rowIndex : rowIndex,
        columnIndex : columnIndex,
        endRowIndex : rowIndex,
        endColumnIndex : columnIndex,
        seatCapability : 4,
        tableType : 1
      }
      return tab
    }
    else {
      const entityEquivalent = this.getElementType(element)
      
      const entity : StructuralElementData = {
        rowIndex : rowIndex,
        columnIndex : columnIndex,
        elementType : entityEquivalent
      }
      return entity
    }
  }

  // Donne l'équivalent du nom en nombre
  getElementType(element : string) : number {
    switch(element){
      case "wall" : 
        return 1
      case "window" :
        return 2
      case "stair" :
        return 3
      case "door" :
        return 4
      case "bar" :
        return 7
      case "tv" :
        return 8
      default : 
      return 0
    }
  }

  // Chaque fois que la fonction est appelée, elle va voir si un élément
  // a ces coordonnées, si oui elle retourne le chemin pour l'image
  // correspondante
  getIcon(index : number, rowIndex : number, columnIndex : number) : string {
    const foundElement = this.grids[index].gridElements.find(
      (element: StructuralElement) =>
        element.rowIndex == rowIndex && element.columnIndex == columnIndex
    )
    const foundTable = this.grids[index].gridTables.find(
      (table: Table) =>
      table.rowIndex == rowIndex && table.columnIndex == columnIndex
    )
    if(foundElement){
      switch(foundElement.elementType){
        case 1 :
          return this.wallPath
        case 2 :
          return this.windowPath
        case 3 :
          return this.stairPath
        case 4 : 
          return this.doorPath
        case 7 :
          return this.barPath
        case 8 :
          return this.tvPath
        default :
          return ""
      }
    }
    else if(foundTable){
      return this.tablePath
    }
    return ""
  }

  // Pareil que le fonction précédente mais seulement pour la nouvelle 
  // grille
  getIconNewGrid(rowIndex : number, columnIndex : number) : string {
    const foundElement = this.newGrid.gridElements.find(
      (element: StructuralElementData) =>
        element.rowIndex == rowIndex && element.columnIndex == columnIndex
    )
    const foundTable = this.newGrid.gridTables.find(
      (table: TableData) =>
      table.rowIndex == rowIndex && table.columnIndex == columnIndex
    )
    if(foundElement){
      switch(foundElement.elementType){
        case 1 :
          return this.wallPath
        case 2 :
          return this.windowPath
        case 3 :
          return this.stairPath
        case 4 : 
          return this.doorPath
        case 7 :
          return this.barPath
        case 8 :
          return this.tvPath
        default :
          return ""
      }
    }
    else if(foundTable){
      return this.tablePath
    }
    return ""
  }
  
  // Supprime tous les éléments de la nouvelle grille
  emptyGrid() : void {
    this.newGrid.gridElements.splice(0, this.newGrid.gridElements.length)
    this.newGrid.gridTables.splice(0, this.newGrid.gridTables.length)
  }

  // envoie la nouvelle grille au back
  uploadGrid() : void {
    this._apiService.createGrid(this.newGrid).subscribe({
      next : (resp) =>{
        console.log(resp)
        this.updateDialog('visibleCreate')
    },
      error : error => console.log(error)     
    })
  }

  checkOwnerId() : number | null {
    return this._authService.getUser()
  }
}
