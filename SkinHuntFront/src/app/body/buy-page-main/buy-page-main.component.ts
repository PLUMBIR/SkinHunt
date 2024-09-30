import {ChangeDetectionStrategy, Component, computed, effect, inject, signal} from '@angular/core';
import {MdbRippleModule} from 'mdb-angular-ui-kit/ripple';
import { MdbDropdownModule} from 'mdb-angular-ui-kit/dropdown';
import {SkinItemCardComponent} from "../skin-item-card/skin-item-card.component";
import {FormsModule} from "@angular/forms";
import {FiltersSubmenuItems} from "../../models/filters-submenu-items";
import { SortItems } from '../../models/sort';
import { MdbCheckboxModule } from 'mdb-angular-ui-kit/checkbox';
import { HttpClient } from '@angular/common/http';
import { SkinModel } from '../../models/skinModel';
import { SkinService } from '../../services/skin.service';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-buy-page-main',
  standalone: true,
  imports: [
    MdbDropdownModule,
    MdbRippleModule,
    MdbCheckboxModule,
    SkinItemCardComponent,
    FormsModule,
  ],
  templateUrl: './buy-page-main.component.html',
  styleUrl: './buy-page-main.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class BuyPageMainComponent {

  router: Router = inject(Router);

  skinService = inject(SkinService);
  authService = inject(AuthService);

  cards$ = signal<SkinModel[]>([]);
  sortBy$ = signal<SortItems>('default');
  priceAbove$ = signal<number>(0);
  priceLess$ = signal<number>(0);
  types$ = signal<number[]>([]);
  rarity$ = signal<string[]>([]);
  floatAbove$ = signal<number>(0);
  floatLess$ = signal<number>(0);

  typehead$ = signal<string>('');
  showSubmenuList$ = signal<FiltersSubmenuItems[]>([]);

  constructor(private readonly http: HttpClient) {
    effect(() => {
      this.getCards();
    });
  }

  cardsList$ = computed(() => {
    if (this.typehead$()) {
      return this.cards$().filter((o) => o.name.toLowerCase().includes(this.typehead$().toLowerCase().trim()));
    }

    return this.cards$();
  });

  onChangedSubmenuList(submenu: FiltersSubmenuItems) {
    if (this.showSubmenuList$().includes(submenu)) {
      this.showSubmenuList$.set(this.showSubmenuList$().filter((item) => item !== submenu));
    } else {
      this.showSubmenuList$.update((items) => [...items, submenu]);
    }
  }

  setSortBy(sortItem: SortItems) {
    this.sortBy$.set(sortItem);
  }

  onChangePriceAbove($event: any) {
    this.priceAbove$.set($event.target.value);
  }

  onChangePriceLess($event: any) {
    this.priceLess$.set($event.target.value);
  }

  onChangeType(numberCheckBox: number) {
    if (this.types$().includes(numberCheckBox)) {
      this.types$.set(this.types$().filter((item) => item !== numberCheckBox));
    } else {
      this.types$.update((items) => [...items, numberCheckBox]);
    }
  }

  onChangeRarity(rarityCheckBox: string) {
    if (this.rarity$().includes(rarityCheckBox)) {
      this.rarity$.set(this.rarity$().filter((item) => item !== rarityCheckBox));
    } else {
      this.rarity$.update((items) => [...items, rarityCheckBox]);
    }
  }

  onChangeFloatAbove($event: any) {
    this.floatAbove$.set($event.target.value);
  }

  onChangeFloatLess($event: any) {
    this.floatLess$.set($event.target.value);
  }

  getCards() {
    const filters = {
      sortBy: this.sortBy$(),
      priceAbove: this.priceAbove$(),
      priceLess: this.priceLess$(),
      types: this.types$(),
      rarity: this.rarity$(),
      floatAbove: this.floatAbove$(),
      floatLess: this.floatLess$()
    }

    this.skinService.getSortedCards(filters)
      .subscribe((skins) => {
        this.cards$.set(skins);
      });
  }

  addSkinToBasket(id: string) {
    const userId = this.authService.user$()?.id;

    if (userId) {
      const command = {
        userId: userId,
        skinId: id,
      };
  
      this.skinService.addSkinToBasket(command)
      .subscribe(() => {
        this.cards$.set(this.cards$().filter((o) => o.id !== id))
      });
    }
  }

  openBasket() {
    this.router.navigate(['/basket']);
  }
}
