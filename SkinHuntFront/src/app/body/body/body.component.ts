import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { MdbCheckboxModule } from "mdb-angular-ui-kit/checkbox";
import { HeaderComponent } from "../../header/header.component";
import { FooterComponent } from "../../footer/footer.component";

@Component({
    selector: 'app-body',
    standalone: true,
    template: `
        <div>
            <app-header></app-header>
            <router-outlet></router-outlet>
            <app-footer></app-footer>
        </div>
    `,
    styles: [
        `
        `,
    ],
    imports: [
        RouterOutlet,
        MdbCheckboxModule,
        HeaderComponent,
        FooterComponent
    ]
})
  export class BodyComponent {
  }
  