import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AuctionService } from 'src/app/service/auction.service';
import { AuctionViewModel } from 'src/app/model/auction-view-model';

@Component({
  selector: 'app-auction-list',
  templateUrl: './auction-list.component.html',
  styleUrls: ['./auction-list.component.scss'],
})
export class AuctionListComponent implements OnInit {
  constructor(public auctionService: AuctionService) {}

  displayedColumns: string[] = [
    'Name',
    'InitialValue',
    'IsUsed',
    'InitialDate',
    'EndDate',
    'User',
    'Edit',
    'Delete',
  ];
  dataSource = new MatTableDataSource<AuctionViewModel>([]);

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAuctions();
  }

  getAuctions(): void {
    this.auctionService
      .getAuctions()
      .subscribe((auctions: AuctionViewModel[]) => {
        this.dataSource.data = auctions;
      });
  }

  delete(auction: AuctionViewModel): void {
    this.auctionService.deleteAuction(auction).subscribe((auctionDelete) => {
      this.getAuctions();
    });
  }
}
