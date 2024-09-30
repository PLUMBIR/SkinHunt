import { Component } from '@angular/core';
import { SideNavComponent } from '../side-nav/side-nav.component';
import { BuyPageMainComponent } from '../buy-page-main/buy-page-main.component';

@Component({
  selector: 'app-buy-page',
  standalone: true,
  imports: [
    BuyPageMainComponent,
    SideNavComponent,
  ],
  templateUrl: './buy-page.component.html',
  styleUrl: './buy-page.component.scss'
})
export class BuyPageComponent {

}
