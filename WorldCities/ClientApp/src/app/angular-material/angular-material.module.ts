import { NgModule } from "@angular/core";
import { MatPaginatorModule } from "@angular/material/paginator";

import { MatTableModule } from "@angular/material/table";

const items = [MatTableModule, MatPaginatorModule];

@NgModule({
  declarations: [],
  imports: items,
  exports: items,
})
export class AngularMaterialModule {}
