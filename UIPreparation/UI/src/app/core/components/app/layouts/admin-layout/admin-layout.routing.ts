import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { AnasayfaComponent } from 'app/core/components/anasayfa/anasayfa.component';
import { BlogComponent } from 'app/core/components/blog/blog.component';
import { BlogDetailComponent } from 'app/core/components/blogDetail/blogDetail.component';
import { DurumComponent } from 'app/core/components/durum/durum.component';
import { HakkimizdaComponent } from 'app/core/components/hakkimizda/hakkimizda.component';
import { HekimComponent } from 'app/core/components/hekim/hekim.component';
import { HekimDetailComponent } from 'app/core/components/hekimDetail/hekimDetail.component';
import { HizmetComponent } from 'app/core/components/hizmet/hizmet.component';
import { HizmetDetailComponent } from 'app/core/components/hizmetDetail/hizmetDetail.component';
import { RandevuComponent } from 'app/core/components/randevu/randevu.component';
import { HekimPagesComponent } from 'app/core/components/hekimPages/hekimPages.component';
import { HizmetPagesComponent } from 'app/core/components/hizmetPages/hizmetPages.component';
import { BlogPagesComponent } from 'app/core/components/blogPages/blogPages.component';
import { AnasayfaPhotoUrlComponent } from 'app/core/components/anasayfaPhotoUrl/anasayfaPhotoUrl.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},

    { path: 'anasayfa',       component: AnasayfaComponent,canActivate:[LoginGuard]},
    { path: 'anasayfa-photo-url',       component: AnasayfaPhotoUrlComponent,canActivate:[LoginGuard]},
    { path: 'blog',       component: BlogComponent,canActivate:[LoginGuard]},
    { path: 'blogdetay',       component: BlogDetailComponent,canActivate:[LoginGuard]},
    { path: 'blogpages',            component: BlogPagesComponent,canActivate:[LoginGuard]},
    { path: 'blogpages/:blogId', component: BlogPagesComponent ,canActivate:[LoginGuard] },
    { path: 'durum',       component: DurumComponent,canActivate:[LoginGuard]},
    { path: 'hakkimizda',       component: HakkimizdaComponent,canActivate:[LoginGuard]},
    { path: 'hekimlerimiz',       component: HekimComponent,canActivate:[LoginGuard]},
    { path: 'hekimlerimizdetay',       component: HekimDetailComponent,canActivate:[LoginGuard]},
    { path: 'hekimpages',            component: HekimPagesComponent,canActivate:[LoginGuard]},
    { path: 'hekimpages/:hekimId', component: HekimPagesComponent ,canActivate:[LoginGuard] },
    { path: 'hizmetlerimiz',       component: HizmetComponent,canActivate:[LoginGuard]},
    { path: 'hizmetlerimizdetay',       component: HizmetDetailComponent,canActivate:[LoginGuard]},
    { path: 'hizmetpages',            component: HizmetPagesComponent,canActivate:[LoginGuard]},
    { path: 'hizmetpages/:hizmetId', component: HizmetPagesComponent ,canActivate:[LoginGuard] },
    { path: 'randevu',       component: RandevuComponent,canActivate:[LoginGuard]},
    
];
