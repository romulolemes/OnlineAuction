import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuctionInputModel } from 'src/app/model/auction-input-model';
import { AuctionService } from 'src/app/service/auction.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { AuctionViewModel } from 'src/app/model/auction-view-model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-auction-add-edit',
  templateUrl: './auction-add-edit.component.html',
  styleUrls: ['./auction-add-edit.component.scss'],
})
export class AuctionAddEditComponent implements OnInit {
  auctionForm = this.fb.group({
    id: [null],
    name: [null, Validators.required],
    user: [null, Validators.required],
    isUsed: [null, Validators.required],
    initialValue: [null, Validators.required],
    initialDate: [null, Validators.required],
    endDate: [null, Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private auctionService: AuctionService,
    private router: Router,
    private route: ActivatedRoute,
    public datepipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      console.log(params.get('id'));
    });

    this.route.paramMap.subscribe((params) => {
      if (params.has('id')) {
        this.auctionService
          .getAuctionById(parseInt(params.get('id')))
          .subscribe((auction) => {
            this.fillAuction(auction);
          });
      }
    });
  }

  fillAuction(auction: AuctionViewModel): void {
    this.auctionForm.setValue({
      id: auction.Id,
      name: auction.Name,
      user: auction.User,
      isUsed: String(auction.IsUsed),
      initialValue: auction.InitialValue,
      initialDate: this.datepipe.transform(auction.InitialDate, 'yyyy-MM-dd'),
      endDate: this.datepipe.transform(auction.EndDate, 'yyyy-MM-dd'),
    });
  }

  cancel(): void {
    this.router.navigate(['/list']);
  }

  onSubmit(): void {
    let auctionForm = this.auctionForm.getRawValue();
    let auctionInput: AuctionInputModel = {
      Id: auctionForm.id ? auctionForm.id : 0,
      Name: auctionForm.name,
      InitialValue: auctionForm.initialValue,
      IsUsed: auctionForm.isUsed == 'true',
      InitialDate: auctionForm.initialDate,
      EndDate: auctionForm.endDate,
      User: auctionForm.user,
    };

    const observerCallback = {
      next: () => {
        // this.dialog.success("Your item was successfully saved!", false)();
        // this.dialogRef.close();
        this.router.navigate(['/list']);
      },
      error: (error: any) => {
        console.log(error);
      },
    };

    if (auctionInput.Id !== 0) {
      this.auctionService
        .updateAuction(auctionInput)
        .subscribe(observerCallback);
    } else {
      this.auctionService.saveAuction(auctionInput).subscribe(observerCallback);
    }
  }
}
