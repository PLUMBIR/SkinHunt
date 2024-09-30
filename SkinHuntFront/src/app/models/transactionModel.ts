import { User } from "./User";
import { SkinModel } from "./skinModel";

export interface TransactionModel {
    user: User,
    skin: SkinModel,
    createdDate: Date,
}