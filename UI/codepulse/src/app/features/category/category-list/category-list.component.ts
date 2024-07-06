import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit{

  //categories?: Category[];      -->Used as a variable to store the response of array from the categories for observable

  //for async pipe variable
  categories$?: Observable<Category[]>;


  constructor(private categoryService: CategoryService){

  }


  //example with observable and subscribing
  /* ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe({
      next:(response)=>{
        this.categories=response;
      }
    });
  } */

    //with async pipe
    ngOnInit(): void {
      this.categories$=this.categoryService.getAllCategories();
    }
  

}
