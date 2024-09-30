import {Component, OnInit, input, output} from "@angular/core";
import {RouterOutlet} from "@angular/router";
import {MdbCheckboxModule} from "mdb-angular-ui-kit/checkbox";
import {HeaderComponent} from "../../header/header.component";
import { SkinModel } from "../../models/skinModel";

@Component({
  selector: 'skin-item-card',
  standalone: true,
  imports: [
    RouterOutlet,
    MdbCheckboxModule,
    HeaderComponent,
  ],
  template: `
    <div class="card-container">
      <img class="card-image" [src]="card().photo" loading="lazy">
      <div class="baseCard-lower-part">
        @if (card().isDiscount) {
          <div class="baseCard-tag">
            <span class="badge bagde-color">-{{(((card().price - card().priceWithDiscount) / card().price) * 100).toFixed(2)}}%</span>
          </div>
        }
        <div class="text-white">
        {{card().name}}
        </div>
        <div class="baseCard-description">
            <span class="text-description">
                {{card().rarity}} / {{card().float}}
            </span>
        </div>
        <div class="baseCard-price">
            <div class="price">
                <span class="styles-price">
                    <span>
                      $ {{card().priceWithDiscount}}
                    </span>
                </span>
            </div>
        </div>
        <div class="baseCard-action-button">
            <button type="button" class="btn-basket" (click)="addToBasket.emit(card().id)">
              <i class="fas fa-shopping-cart"></i>
            </button>
        </div>
      </div>
    </div>
    `,
  styles: [
    `
      .card-container {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        width: 200px;
        height: 300px;
        background-color: #312f3d;
        border-radius: 4px;
        padding: 8px 0 8px 0;
      }

      .card-container:hover {
        background-color: #6c697c;
      }

      .card-container:hover button {
        background-color: #8678d7;
      }

      .card-container:hover i {
        opacity: 80%;
      }

      .card-image {
        width: 100%;
        height: auto;
      }

      .baseCard-lower-part {
        margin-top: auto;
        width: calc(100% - 16px);
      }

      .bagde-color {
        background-color: color-mix(in srgb, #5bc27a 20%, rgba(28,26,36,0.5));
        color: #5bc27a;
      }

      .text-description {
        color: #bbb9c7;
        font-size: 11px;
        font-weight: 400;
        line-height: 16px;
        letter-spacing: 0.275px;
      }

      .styles-price {
        color: #fff;
        font-family: Inter, system-ui;
        font-size: 14px;
        font-style: normal;
        font-weight: 500;
        line-height: 20px;
        letter-spacing: .02em;
      }

      .baseCard-action-button {
        display: flex;
        gap: 4px;
        height: 28px;
      }

      .btn-basket {
        width: 100%;
        height: 28px;
        cursor: pointer;
        color: #fff;
        background-color: #403d4d;
        border: 0;
        border-radius: 4px;
      }

      .btn-basket i {
        opacity: 50%;
      }

      .btn-basket:hover {
        background-color: #a293e5 !important;
      }

      .btn-basket:hover i{
        opacity: 100%;
      }
    `,
  ]
})
export class SkinItemCardComponent {
  card = input.required<SkinModel>();

  addToBasket = output<string>();
}
