import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PhoneBooksResolverService } from './resolvers/phone-books';

const routes: Routes = [
  { path: '', component: HomeComponent, resolve: { products: PhoneBooksResolverService } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
