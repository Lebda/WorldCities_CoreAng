import { NgModule } from "@angular/core";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import { MatInputModule } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";

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
