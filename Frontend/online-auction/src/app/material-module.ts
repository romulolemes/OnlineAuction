import { NgModule } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatTreeModule } from '@angular/material/tree';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatNativeDateModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';

const materialModules = [
  FormsModule,
  MatMenuModule,
  MatTabsModule,
  MatInputModule,
  MatAutocompleteModule,
  MatButtonToggleModule,
  MatTreeModule,
  MatSlideToggleModule,
  MatRadioModule,
  MatButtonModule,
  MatDividerModule,
  MatCardModule,
  MatSelectModule,
  MatIconModule,
  MatChipsModule,
  MatProgressBarModule,
  MatDialogModule,
  MatToolbarModule,
  MatSidenavModule,
  MatFormFieldModule,
  MatTableModule,
  MatCheckboxModule,
  ReactiveFormsModule,
  MatDatepickerModule,
  MatPaginatorModule,
  MatSortModule,
  MatNativeDateModule,
  MatExpansionModule,
  MatTooltipModule,
];

@NgModule({
  imports: materialModules,
  exports: materialModules,
})
export class MaterialModule {}
