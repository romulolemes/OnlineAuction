import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuctionInputModel } from 'src/app/model/auction-input-model';
import { AuctionService } from 'src/app/service/auction.service';
import { Router } from '@angular/router';

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
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit(): void {
    let auctionForm = this.auctionForm.getRawValue();
    let auctionInput: AuctionInputModel = {
      Id: auctionForm.id ? auctionForm.id : 0,
      Name: auctionForm.name,
      InitialValue: auctionForm.initialValue,
      IsUsed: auctionForm.isUsed,
      InitialDate: auctionForm.initialDate,
      EndDate: auctionForm.endDate,
      User: auctionForm.user,
    };

    const observer = {
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
    } else {
      this.auctionService.saveAuction(auctionInput).subscribe(observer);
    }
  }
}
