import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberDetailResolver } from './_resolver/member-detail.resolver';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { Routes, Resolve } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';



export const appRoutes: Routes = [
    { path: '' , component: HomeComponent },
    {
        path: '', // localhost://dummymember;
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members' , component: MemberListComponent, resolve: { users : MemberListResolver} },
            { path: 'members/:id' , component: MemberDetailComponent, resolve: { user : MemberDetailResolver}},
            { path: 'member/edit', component: MemberEditComponent ,
             resolve: {user: MemberEditResolver} , canDeactivate: [ PreventUnsavedChanges] },
            { path: 'messages' , component: MessagesComponent },
            { path: 'lists' , component: ListsComponent },
        ]
    },
    { path: '**' , redirectTo: '', pathMatch: 'full' }
];
