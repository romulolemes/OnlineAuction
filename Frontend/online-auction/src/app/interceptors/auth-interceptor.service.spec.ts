import { TestBed } from "@angular/core/testing";

import { AuthInterceptorService } from "./auth-interceptor.service";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { MaterialModule } from "src/app/material.module";
import { Location } from "@angular/common";
import { HttpClientTestingModule } from "@angular/common/http/testing";

describe("AuthInterceptorService", () => {
  beforeEach(() =>
    TestBed.configureTestingModule({
      imports: [MaterialModule, HttpClientTestingModule],
      providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: [] },
        { provide: Location, useValue: {} }
      ]
    })
  );

  it("should be created", () => {
    const service: AuthInterceptorService = TestBed.get(AuthInterceptorService);
    expect(service).toBeTruthy();
  });
});
