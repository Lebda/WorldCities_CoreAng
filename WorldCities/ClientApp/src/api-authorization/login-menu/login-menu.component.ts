import { Component, OnInit } from "@angular/core";
import { Observable, of } from "rxjs";
import { map } from "rxjs/operators";

import { AuthorizeService } from "../authorize.service";

@Component({
  selector: "app-login-menu",
  templateUrl: "./login-menu.component.html",
  styleUrls: ["./login-menu.component.css"],
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  public constructor(private authorizeService: AuthorizeService) {
    this.isAuthenticated = of(false);
    this.userName = of("");
  }

  public ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(
      map((u) => {
        if (!u?.name) {
          return "";
        }
        return u.name;
      })
    );
  }
}
