import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { API_URL } from "../constants/URL";
import { SkinModel } from "../models/skinModel";
import { SortItems } from "../models/sort";
import { httpParamsFromRequest } from "./helper";
import { BasketModel } from "../models/basketModel";
import { BasketValue } from "../models/basket-value";
import { SoldModel } from "../models/SoldModel";

export interface SkinFilterModel {
    sortBy: SortItems;
    priceAbove?: number;
    priceLess?: number;
    types?: number[];
    rarity?: string[];
    color?: number[];
    floatAbove?: number;
    floatLess?: number;
}

@Injectable({
    providedIn: 'root',
  })
export class SkinService {
    constructor(private readonly http: HttpClient) {}
  
    getSortedCards(filter: SkinFilterModel) {
        return this.http.get<SkinModel[]>(`${API_URL}/skins`, {
            params: httpParamsFromRequest(filter),
        });
    }

    addSkinToBasket(basketModel: BasketModel) {
        return this.http.post(`${API_URL}/basket`, basketModel);
    }

    getUserBasketValue(userId: string) {
        return this.http.get<BasketValue[]>(`${API_URL}/basket`, {
            params: httpParamsFromRequest({ id: userId }),
        });
    }

    removeSkinFormBasket(id: string) {
        return this.http.delete(`${API_URL}/basket/${id}`);
    }

    buySkinFromBasket(soldModel: SoldModel) {
        return this.http.post<boolean>(`${API_URL}/users/buy-skin`, soldModel);
    }

    getUserSkinsFromBasketCount(userId: string) {
        return this.http.get<number>(`${API_URL}/basket/${userId}`);
    }

    getSkinsFromSoldsCount(userId: string) {
        return this.http.get<number>(`${API_URL}/basket/solds/${userId}`);
    }
}

  