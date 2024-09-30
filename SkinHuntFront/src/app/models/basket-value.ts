import { User } from "./User";
import { SkinModel } from "./skinModel";

export interface BasketValue {
    id: string;
    user: User;
    skin: SkinModel;
    createdDate: Date;
}
