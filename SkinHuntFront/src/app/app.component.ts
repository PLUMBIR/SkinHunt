import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MdbCheckboxModule} from 'mdb-angular-ui-kit/checkbox';
import { HeaderComponent } from './header/header.component';
import { BodyComponent } from './body/body/body.component';
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MdbCheckboxModule,
    HeaderComponent,
    BodyComponent,
    FooterComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'SkinHunt';
}
