export interface UpdateBlogPost{
    title:string;
    shortDescription:string;
    urlHandle:string;
    content:string;
    featuredImageUrl:string;    
    author:string;
    publishedDate:Date;
    isVisible:boolean;
    categories:string[];
}