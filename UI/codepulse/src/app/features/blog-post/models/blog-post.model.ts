import { Category } from "../../category/models/category.model";

export interface BlogPost{
    id:string;
    title:string;
    shortDescription:string;
    content:string;
    fetauredImageUrl:string;
    urlHandle:string;
    author:string;
    PublishedDate:Date;
    isVisible:boolean;
    categories: Category[];
}