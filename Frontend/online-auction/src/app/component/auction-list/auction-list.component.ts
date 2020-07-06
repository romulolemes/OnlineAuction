import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AuctionService } from 'src/app/service/auction.service';
import { AuctionViewModel } from 'src/app/model/auction-view-model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-auction-list',
  templateUrl: './auction-list.component.html',
  styleUrls: ['./auction-list.component.scss'],
})
export class AuctionListComponent implements OnInit {
  constructor(public auctionService: AuctionService) {}

  displayedColumns: string[] = [
    'Id',
    'Name',
    'User',
    'IsUsed',
    'InitialValue',
    'InitialDate',
    'EndDate',
  ];
  dataSource = new MatTableDataSource<AuctionViewModel>([]);

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAuctions();
  }

  getAuctions() {
    this.auctionService
      .getAuctions()
      .subscribe((auctions: AuctionViewModel[]) => {
        this.dataSource.data = auctions;
        console.log(auctions);
      });
  }
}

const ELEMENT_DATA: AuctionViewModel[] = [
  {
    Id: 1,
    Name: 'Teste',
    User: 'Rômulo',
    IsUsed: false,
    InitialValue: 1000,
    InitialDate: new Date(),
    EndDate: new Date(),
  },
];
