import { Component, input, output } from '@angular/core';
import { BasketValue } from '../../models/basket-value';

@Component({
  selector: 'skin-item-card-basket',
  standalone: true,
  imports: [],
  templateUrl: './skin-item-card-basket.component.html',
  styleUrl: './skin-item-card-basket.component.scss'
})
export class SkinItemCardBasketComponent {
  basketValue = input.required<BasketValue>();

  buySkinFromBasket = output<{ id: string; skinId: string }>();
  removeFromBasket = output<string>();
}
