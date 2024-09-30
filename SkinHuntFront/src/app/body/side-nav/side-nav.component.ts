import {Component, computed, inject, signal} from '@angular/core';
import {SidebarTabs} from "../../models/sidebar-tabs";
import {NgClass} from "@angular/common";
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-side-nav',
  standalone: true,
  imports: [
    NgClass,
    RouterModule,
  ],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent {
  router: Router = inject(Router);

  selectedTab$ = computed(() => {
    const currentUrl = this.router.url;

    if (currentUrl === '/basket') {
      return 'basket';
    }

    if (currentUrl === '/history') {
      return 'history';
    }

    return 'buy';
  });
}
