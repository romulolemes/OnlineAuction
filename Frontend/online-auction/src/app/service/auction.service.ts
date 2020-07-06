import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { AuctionViewModel } from '../model/auction-view-model';
import { AuctionInputModel } from '../model/auction-input-model';

@Injectable({
  providedIn: 'root',
})
export class AuctionService {
  url = 'http://localhost:5000/api/auction';
  user;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {
    authService.getUser().then((user) => {
      this.user = user;
    });
  }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  getAuctions(): Observable<AuctionViewModel[]> {
    return this.httpClient
      .get<AuctionViewModel[]>(this.url)
      .pipe(catchError(this.handleError));
  }

  getAuctionById(Id: number): Observable<AuctionViewModel> {
    return this.httpClient
      .get<AuctionViewModel>(this.url + '/' + Id)
      .pipe(catchError(this.handleError));
  }

  saveAuction(auction: AuctionInputModel): Observable<AuctionViewModel> {
    return this.httpClient
      .post<AuctionViewModel>(
        this.url,
        JSON.stringify(auction),
        this.httpOptions
      )
      .pipe(retry(2), catchError(this.handleError));
  }

  updateAuction(auction: AuctionInputModel): Observable<AuctionViewModel> {
    return this.httpClient
      .put<AuctionViewModel>(
        this.url + '/' + auction.Id,
        JSON.stringify(auction),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  deleteAuction(auction: AuctionViewModel) {
    return this.httpClient
      .delete<AuctionViewModel>(this.url + '/' + auction.Id, this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage =
        `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
