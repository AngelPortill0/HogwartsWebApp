import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroPageComponent } from './hero-page/hero-page.component';
import { SignupPageComponent } from './signup-page/signup-page.component';
import { HistoryPageComponent } from './history-page/history-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'Home', pathMatch: 'full' },
  { path: 'Home', component: HeroPageComponent },
  { path: 'SignUp', component: SignupPageComponent },
  { path: 'History', component: HistoryPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
