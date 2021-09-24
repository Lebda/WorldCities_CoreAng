import { NgModule } from "@angular/core";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";

const items = [MatTableModule, MatPaginatorModule, MatSortModule];

@NgModule({
  declarations: [],
  imports: items,
  exports: items,
})
export class AngularMaterialModule {}
