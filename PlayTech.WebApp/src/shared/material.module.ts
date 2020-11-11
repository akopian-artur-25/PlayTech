import {MatButtonModule} from '@angular/material/button';
import {MatDialogModule} from '@angular/material/dialog';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {CommonModule} from '@angular/common';
import {MatPaginatorModule} from '@angular/material/paginator';
import {NgModule} from '@angular/core';
import {MatTableModule} from '@angular/material/table';

const modules = [
  MatButtonModule,
  MatDialogModule,
  MatSnackBarModule,
  MatFormFieldModule,
  MatInputModule,
  MatIconModule,
  MatSelectModule,
  MatProgressSpinnerModule,
  MatSlideToggleModule,
  MatPaginatorModule,
  MatTableModule,
  MatAutocompleteModule
];

@NgModule({
  imports: [
    CommonModule,
    ...modules
  ],
  declarations: [
  ],
  exports: [
    ...modules
  ],
  entryComponents: [
  ]
})
export class MaterialModule {
}
