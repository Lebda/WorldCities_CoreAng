import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { MatInputModule } from "@angular/material/input";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSelectModule } from "@angular/material/select";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";

const items = [
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatInputModule,
  ReactiveFormsModule,
  MatSelectModule,
];

@NgModule({
  declarations: [],
  imports: items,
  exports: items,
})
export class AngularMaterialModule {}
