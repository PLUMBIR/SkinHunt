import { Component, OnInit, inject, signal } from '@angular/core';
import { SideNavComponent } from '../side-nav/side-nav.component';
import { SkinService } from '../../services/skin.service';
import { SkinItemCardBasketComponent } from '../skin-item-card-basket/skin-item-card-basket.component';
import { AuthService } from '../../services/auth.service';
import { SkinItemCardComponent } from "../skin-item-card/skin-item-card.component";
import { BasketValue } from '../../models/basket-value';
import { BuySkinModel } from '../../models/buy-skin-model';

@Component({
    selector: 'app-basket',
    standalone: true,
    templateUrl: './basket.component.html',
    styleUrl: './basket.component.scss',
    imports: [
        SideNavComponent,
        SkinItemCardBasketComponent,
        SkinItemCardComponent
    ]
})
export class BasketComponent implements OnInit {
  skinService = inject(SkinService);
  authService = inject(AuthService);
  
  basketValueList$ = signal<BasketValue[]>([]);

  ngOnInit() {
    const userId = this.authService.user$()?.id;

    if (userId) {
      this.skinService.getUserBasketValue(userId)
        .subscribe((res) => {
          this.basketValueList$.set(res);
        });
    }
  }

  removeSkinFromBasket(id: string) {  
    this.skinService.removeSkinFormBasket(id)
    .subscribe(() => {
      this.basketValueList$.set(this.basketValueList$().filter((o) => o.id !== id))
    });
  }

  buySkinFromBasket(model: BuySkinModel) {
    const userId = this.authService.user$()?.id;

    if (userId) {
      const command = {
        userId: userId,
        skinId: model.skinId,
      }

      this.skinService.buySkinFromBasket(command).subscribe({
        next: (res) => {
          if (res) {
            this.basketValueList$.set(this.basketValueList$().filter((o) => o.id !== model.id))
            this.skinService.removeSkinFormBasket(model.id).subscribe();
            this.authService.updateBalance().subscribe();
          };
        },
        error: ()=> {
          alert('Недостаточно средств на балансе!');
        },
      });
    }
  }
}
