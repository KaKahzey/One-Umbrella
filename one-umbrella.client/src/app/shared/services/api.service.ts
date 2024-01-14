import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { LoginData } from '../models/account/loginData';
import { Observable } from 'rxjs';
import { RegisterData } from '../models/account/registerData';
import { UserInfoData } from '../models/account/userInfoData';
import { Restaurant } from '../models/restaurant/restaurant';
import { Grid } from '../models/restaurant/grid';
import { Rating } from '../models/rating/rating';
import { RestaurantData } from '../models/restaurant/restaurantData';
import { GridData } from '../models/restaurant/gridData';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  header : any = {
    headers: new HttpHeaders()
      .set('Authorization',  `${this._authService.getToken()!}`)
  }

  // Liste des endpoints

  //#region Account
  // Login : LoginData
  private _urlLogin : string = "https://localhost:7159/api/Auth/Login"
  // Register : RegisterData
  private _urlRegister : string = "https://localhost:7159/api/Auth/Register"
  // Infos user : userInfo
  private _urlUserInfo : string = "https://localhost:7159/api/ConfigurationProfile/"
  // Post infos user : userInfoData
  private _urlUserInfoData : string = "https://localhost:7159/api/ConfigurationProfile/"
  //#endregion

  //#region Reservations
  // Liste de toutes les réservations pour un restaurant pour un jour : reservation + reservedTable
  private _urlListReservationsRestaurantByDay : string = "https://localhost:7159/api/Reservation/GetAllForOneRestaurant/"
  // Liste des réservations pour un user : reservation
  private _urlListUserReservations : string = "https://localhost:7159/api/Reservation/GetAllForOneUser/"
  // Liste des réservation pour un restaurant par statut : reservation
  private _urlListReservationsByStatus : string = "https://localhost:7159/api/Reservation/getAllByStatus/"
  // Modification du statut d'une réservation
  private _urlReservationUpdateStatus : string = "https://localhost:7159/api/Reservation/getAllByStatus/"
  // Delete réservation
  private _urlReservationDelete : string = "https://localhost:7159/api/Reservation/"
  // Créer réservation
  private _urlCreateReservation : string = "https://localhost:7159/api/Reservation?tableId="
  //#endregion

  //#region Restaurants
  private _urlGetAllRestaurantsByPagination : string = "https://localhost:7159/api/ListRestaurant?page="
  private _urlGetAllRestaurantsForOneOwner : string = "https://localhost:7159/api/ListRestaurant/"
  private _urlGetRestaurantsByIdentifier : string = "https://localhost:7159/api/ListRestaurant/Identifier?name="
  private _urlGetRestaurantById : string = "https://localhost:7159/api/Restaurant/"
  
  //#endregion

  //#region Favorites
  private _urlFavoriteGetAllForOne : string = "https://localhost:7159/api/Favorite/"
  private _urlFavoriteModification : string = "https://localhost:7159/api/Favorite?humanId="
  //#endregion

  //#region Grids
  private _urlGetAllGridsForOneRestaurant : string = "https://localhost:7159/api/GridCrontroller/GetAllForOneRestaurant/"
  private _urlCreateGrid : string = "https://localhost:7159/api/GridCrontroller"
  private _urlGridUpdate : string = "https://localhost:7159/api/GridCrontroller/"
  //#endregion

  //#region Ratings
  private _urlGetAllRatingsForOneRestaurant : string = "https://localhost:7159/api/Rating/GetAllForOneRestaurant/"
  private _urlGetAllRatingsForOneHuman : string = "https://localhost:7159/api/Rating/GetAllForOneUser/"
  private _urlRatingPost : string = "https://localhost:7159/api/Rating"
  private _urlRatingDelete : string = "https://localhost:7159/api/Rating?humanId="
  private _urlRatingUpdate : string = "https://localhost:7159/api/Rating/"
  constructor(private _httpClient : HttpClient, private _authService : AuthService ) { }
  
  //#region Account
  login(user : LoginData) : Observable<any> {
    return this._httpClient.post(this._urlLogin, user)
  }

  register(user : RegisterData) : Observable<any> {
    return this._httpClient.post(this._urlRegister, user)
  }

  getUser(id : number) : Observable<any> {
    return this._httpClient.get(this._urlUserInfo, this.header)
  }

  updateUser(id : number, user : UserInfoData) : Observable<any> {
    return this._httpClient.put(this._urlUserInfoData + id, user, this.header)
  }
  //#endregion

  //#region Reservations
  getReservationsByRestaurantByDay(id : number, date : Date) : Observable<any> {
    return this._httpClient.get(this._urlListReservationsRestaurantByDay +id + "?date=" + date, this.header)
  }

  getReservationsByUser(id : number) : Observable<any> {
    return this._httpClient.get(this._urlListUserReservations + id, this.header)
  }

  getReservationsByRestaurantByStatus(id : number, status : number) : Observable<any> {
    return this._httpClient.get(this._urlListReservationsByStatus + id + "?status=" + status, this.header)
  }

  setReservationStatus(id : number, status : number) : Observable<any> {
    return this._httpClient.put(this._urlReservationUpdateStatus + id + "?status=" + status, this.header)
  }

  deleteReservation(id : number) : Observable<any> {
    return this._httpClient.delete(this._urlReservationDelete + id, this.header)
  }

  createReservation(id : number) : Observable<any> {
    return this._httpClient.post(this._urlCreateReservation + id, this.header)
  }
  //#endregion

  //#region Restaurants
  getAllRestaurantsByPagination(page : number, pageSize : number, sortBy : string, isDescending : boolean, id? : number, city? : string) : Observable<any> {
    const idResult = id !== undefined && id !== null ? "&humanId=" + id : "";
    const cityResult = city ? "&city=" + city : "";

    const url = this._urlGetAllRestaurantsByPagination + page + "&pageSize=" + pageSize + "&sortBy=" + sortBy + "&isDescending=" + isDescending + idResult + cityResult;

    return this._httpClient.get(url);
  }

  getRestaurantsByIdentifier(name : string) : Observable<any> {
    return this._httpClient.get(this._urlGetRestaurantsByIdentifier + name)
  }

  getAllForOneOwner(ownerId : number) : Observable<any> {
    return this._httpClient.get(this._urlGetAllRestaurantsForOneOwner + ownerId, this.header)
  }

  getRestaurantById(id : number) : Observable<any> {
    return this._httpClient.get(this._urlGetRestaurantById + id)
  }

  updateRestaurant(id : number, restaurant : Restaurant) : Observable<any> {
    return this._httpClient.put(this._urlGetRestaurantById + id, restaurant, this.header)
  }

  deleteRestaurant(id : number) : Observable<any> {
    return this._httpClient.get(this._urlGetRestaurantById + id, this.header)
  }

  createRestaurant(restaurant : RestaurantData) : Observable<any> {
    return this._httpClient.post(this._urlGetRestaurantById, restaurant, this.header)
  }
  //#endregion

  //#region Favorites
  getFavorites(humanId : number, restaurantId : number) : Observable<any> {
    return this._httpClient.post(this._urlFavoriteGetAllForOne + humanId, restaurantId, this.header)
  }

  createFavorite(humanId : number, restaurantId : number) : Observable<any> {
    return this._httpClient.post(this._urlFavoriteModification + humanId + "&restaurantId=" + restaurantId, this.header)
  }

  deleteFavorite(humanId : number, restaurantId : number) : Observable<any> {
    return this._httpClient.delete(this._urlFavoriteModification + humanId + "&restaurantId=" + restaurantId, this.header)
  }
  //#endregion

  //#region Grids
  getAllGridsForOneRestaurant(id : number) : Observable<any> {
    return this._httpClient.get(this._urlGetAllGridsForOneRestaurant + id, this.header)
  }

  createGrid(grid : GridData) : Observable<any> {
    return this._httpClient.post(this._urlCreateGrid, grid)
  }

  updateGrid(id : number, grid : Grid) : Observable<any> {
    return this._httpClient.put(this._urlGridUpdate + id, grid, this.header)
  }
  
  deleteGrid(id : number) : Observable<any> {
    return this._httpClient.delete(this._urlGridUpdate + id, this.header)
  }
  //#endregion

  //#region Ratings
  getAllRatingsForOneRestaurant(id : number) : Observable<any> {
    return this._httpClient.get(this._urlGetAllRatingsForOneRestaurant + id, this.header)
  }

  getAllRatingsForOneHuman(id : number) : Observable<any> {
    return this._httpClient.get(this._urlGetAllRatingsForOneHuman + id, this.header)
  }

  createRating(rating : Rating) : Observable<any> {
    return this._httpClient.post(this._urlRatingPost, rating, this.header)
  }

  deleteRating(humanId : number, restaurantId : number) : Observable<any> {
    return this._httpClient.delete(this._urlRatingDelete + humanId + "&restaurantId=" + restaurantId, this.header)
  }

  updateRating(humanId : number, rating : Rating) : Observable<any> {
    return this._httpClient.put(this._urlRatingUpdate + humanId, rating, this.header)
  }
  //#endregion
}
