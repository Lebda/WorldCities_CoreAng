import { NgModule } from "@angular/core";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import { MatInputModule } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";

const items = [
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatInputModule,
  ReactiveFormsModule,
];

@NgModule({
  declarations: [],
  imports: items,
  exports: items,
})
export class AngularMaterialModule {}
