export interface SkinModel {
    id: string;
    name: string;
    type: {
        category: number;
        id: string;
        subcategory: number;
    };
    color: string;
    float: number;
    price: number;
    isDiscount: boolean;
    priceWithDiscount: number;
    rarity: string;
    releaseDate: string;
    photo: string;
  }
  