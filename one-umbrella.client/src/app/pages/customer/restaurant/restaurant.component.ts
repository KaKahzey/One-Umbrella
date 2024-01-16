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
import { ReservedTable } from '../../../shared/models/reservations/reservedTable';
import { AuthService } from '../../../shared/services/auth.service';
import { SliderModule } from 'primeng/slider';
import { CalendarModule } from 'primeng/calendar';
import { WholeReservation } from '../../../shared/models/reservations/wholeReservation';
import { Reservation } from '../../../shared/models/reservations/reservation';
import { FileUploadModule } from 'primeng/fileupload';
import { CheckboxModule } from 'primeng/checkbox';
import { onlyBlobData } from '../../../shared/models/shared/onlyBlobData';
import { RatingModule } from 'primeng/rating';
import { Rating } from '../../../shared/models/rating/rating';
@Component({
  selector: 'app-restaurant',
  standalone: true,
  imports: [CarouselModule, TabViewModule, DialogModule, InputTextModule, ReactiveFormsModule, FormsModule, ButtonModule, SelectButtonModule, SliderModule, CalendarModule, FileUploadModule, CheckboxModule, RatingModule],
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
  newImage : onlyBlobData = {restaurantId : this.restaurantId, imageData : "", isFront : false, isMenu : false}
  rating : number = 0

  // Reservations
  wholeReservations : WholeReservation = {reservations : [], reservedTables : []}
  newReservation : ReservationData = {restaurantId : 0, humanId : 0, reservationTimeStart : new Date(), reservationTimeEnd : new Date(), tableId : 0}

  grids : Grid[] = []
  newGrid : GridData = {gridName : "", restaurantId : 0, gridRows : 0, gridColumns : 0, gridElements : [], gridTables : []}
  arrayRows : Array<any> = []
  arrayColumns : Array<any> = []

  visibleFirst : boolean = false
  visibleSecond : boolean = false
  visibleThird : boolean = false
  visibleCreate : boolean = false
  visiblePicture : boolean = false
  loading: boolean = false

  reservationForm : FormGroup
  sizeForm : FormGroup

  sliderValue : number = 0

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
    this.reservationForm = this._fb.group({
    calendar : [new Date(), [Validators.required]],
    time :[0],
    timeDisplayed : ["00:00"]
    })
    this.sizeForm = this._fb.group({
      rows :[null, [Validators.required]],
      columns :[null, [Validators.required]],
      name :[null, [Validators.required]]
    })

    
  }

  ngOnInit(): void {
    this._route.params.subscribe(data => {
      this.restaurantId = parseInt(data['id'])
    })

    this.reservationForm.get('time')!.valueChanges.subscribe(value => {
      this.updateTime(value)
      this.sliderValue = value
    })
    this._apiService.getRestaurantById(this.restaurantId).subscribe({
      next : (resp) => {
        this.restaurant = resp
        this._apiService.getAllGridsForOneRestaurant(this.restaurantId).subscribe({
          next : resp => {
            this.grids = resp
            
          },
          error : error => console.log(error)
          
        })
        this._apiService.getAllImagesForOneRestaurant(this.restaurantId).subscribe({
          next : pictures => {            
            this.restaurant.images = pictures
            this.verifyMenu()
          },
          error : error => console.error(error)
        })
      },
      error : error => console.log(error)
    })
    this.getReservationsByDay(new Date())
    this.responsiveOptions = [
      {
      breakpoint: '1199px',
      numVisible: 1,
      numScroll: 1
    },
    {
      breakpoint: '979px',
      numVisible: 1,
      numScroll: 1
    }
    ]
    
  }

  // Envoie la note du restaurant, si déjà noté => update
  sendRating(event: any): void {
    const rating: Rating = {
      humanId: this._authService.getUser()!,
      restaurantId: this.restaurantId,
      score: event.value,
      comment: ""
    }  
    this._apiService.getAllRatingsForOneHuman(rating.humanId).subscribe({
      next: (resp) => {
        const alreadyRated = resp ? resp.some((r: any) => r.humanId === rating.humanId && r.restaurantId === rating.restaurantId) : false
  
        if (alreadyRated) {
          this._apiService.updateRating(rating).subscribe({
            next: (resp) => console.log("changed"),
            error: (error) => console.error(error)
          })
        } else {
          this._apiService.createRating(rating).subscribe({
            next: (resp) => console.log("changed"),
            error: (error) => console.error(error)
          })
        }
      },
      error: (error) => console.error(error)
    })
  }

  // Vérifie si il y a des images du menu
  verifyMenu(): void {
    const menuExists = this.restaurant.images
    this.hasMenu = menuExists?.some(i => i.isMenu === true)
  }

  // Pour les carrousels, images du menu ou pas
  getMenuImages(wantMenu : boolean) : onlyBlob[] {
    return this.restaurant.images.filter(image => image.isMenu === wantMenu)
}

  // Affichage et création des réservations

  // Get all reservations for the day
  getReservationsByDay(date : Date) : void {
    const year = date.getFullYear()
    const month = date.getMonth()
    const day = date.getDate()
    const formattedDate = `${year}-${month + 1}-${day}`
    this._apiService.getReservationsByRestaurantByDay(this.restaurantId, formattedDate).subscribe({
      next : resp => {
        this.wholeReservations = resp
        
      },
      error : error => console.log(error)
    })
  }

  // Change la date du jour par rapport au calendrier
  switchDay() : void {
    this.getReservationsByDay(this.reservationForm.get("calendar")!.value)
  }

  // Affiche les réservations par rapport à la valeur du slider et du 
  // calendrier
  displayReservations(index: number, row: number, column: number, sliderValue: number): boolean {
    for (let reservedTable of this.wholeReservations.reservedTables) {
      for (let table of this.grids[index].gridTables) {
        if (
          table.tableId === reservedTable.tableId &&
          table.rowIndex === row &&
          table.columnIndex === column
        ) {
          const reservation = this.wholeReservations.reservations.find(
            (r) => r.reservationId === reservedTable.reservationId
          )
          if (reservation) {
            const startTime = this.getMinutesFromDate(reservation.reservationTimeStart)
            const endTime = this.getMinutesFromDate(reservation.reservationTimeEnd)
  
            return sliderValue >= startTime && sliderValue <= endTime
          }
        }
      }
    }
    return false
  }
  
  // Il y a un problème de formattage quelque part,
  // en attendant, on extrait le nombre de minutes de la date entrée
  getMinutesFromDate(date: Date | string): number {
    const formattedDate = typeof date === 'string' ? new Date(date) : date
    return formattedDate.getHours() * 60 + formattedDate.getMinutes()
  }

  // Modifie le temps affiché par rapport au slider
  updateTime(timeInMinutes : number) : void {
    const hours = Math.floor(timeInMinutes / 60)
    const remainingMinutes = timeInMinutes % 60

    const convertedTime = `${this.convertNumberToDisplay(hours)}:${this.convertNumberToDisplay(remainingMinutes)}`
    this.reservationForm.get("timeDisplayed")!.setValue(convertedTime, { emitEvent: false })
  }

  // Ajoute un zéro si l'heure ou les minutes n'ont qu'un nombre
  private convertNumberToDisplay(number: number): string {
    return number < 10 ? `0${number}` : `${number}`
  }

  // Modifie date + heure de la nouvelle réservation
  updateDateTime(): Date {
    const calendarValue: Date = this.reservationForm.get("calendar")!.value
    const timeValue : number = this.reservationForm.get("time")!.value
    const year = calendarValue.getFullYear()
    const month = calendarValue.getMonth()
    const day = calendarValue.getDate()
    const updatedDate = new Date(Date.UTC(year, month, day, Math.floor(timeValue / 60), timeValue % 60))
    this.newReservation.reservationTimeStart = updatedDate
    return updatedDate
  }

  // Set une réservation et lui accorde une table si la cellule
  // visée est valide (une table et valide)
  setReservation(index : number, rowIndex : number, columnIndex : number) : void {
    const foundTable = this.grids[index].gridTables.find(
      (table: Table) =>
      table.rowIndex == rowIndex && table.columnIndex == columnIndex
    )
    if(foundTable){
      this.newReservation.tableId = foundTable.tableId
      this.newReservation.restaurantId = this.restaurantId
      this.newReservation.humanId = this._authService.getUser()
    }
  }

  // envoie la nouvelle réservation et table réservée au back
  uploadReservation() : void {
    const timeEnd = new Date(this.updateDateTime())
    timeEnd.setHours(timeEnd.getHours() + 2)
    this.newReservation.reservationTimeEnd = timeEnd
    this._apiService.createReservation(this.newReservation).subscribe({
      next : resp => {
          this.ngOnInit()
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
      case "visiblePicture":
        this.visiblePicture = !this.visiblePicture
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
    else if(element != "remove") {
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
        this.ngOnInit()
    },
      error : error => console.log(error)     
    })
  }

  checkOwnerId() : number | null {
    return this._authService.getUser()
  }

  storeImage(event: any): void {
    const uploadedFile: Blob = event.files[0]
    const reader = new FileReader()

    reader.onload = () => {
        const base64Image = btoa(reader.result as string)
        this.newImage.imageData = base64Image
    }
    reader.readAsBinaryString(uploadedFile)
  } 

  sendImage(): void {
    this.newImage.restaurantId = this.restaurantId
    if (this.newImage.imageData) {
      this._apiService.createImage(this.newImage).subscribe({
        next: resp =>{
          this.ngOnInit()
        },
        error: error => console.log(error)
      })
    }
  }

  
}
