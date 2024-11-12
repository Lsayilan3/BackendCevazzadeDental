import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslationService } from 'app/core/services/translation.service';
import { LanguageComponent } from '../components/admin/language/language.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../components/admin/log/logDto.component';
import { MatSortModule } from '@angular/material/sort';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AnasayfaComponent } from '../components/anasayfa/anasayfa.component';
import { BlogComponent } from '../components/blog/blog.component';
import { DurumComponent } from '../components/durum/durum.component';
import { HakkimizdaComponent } from '../components/hakkimizda/hakkimizda.component';
import { HekimComponent } from '../components/hekim/hekim.component';
import { HekimDetailComponent } from '../components/hekimDetail/hekimDetail.component';
import { HizmetComponent } from '../components/hizmet/hizmet.component';
import { HizmetDetailComponent } from '../components/hizmetDetail/hizmetDetail.component';
import { RandevuComponent } from '../components/randevu/randevu.component';
import { BlogDetailComponent } from '../components/blogDetail/blogDetail.component';
import { HekimPagesComponent } from '../components/hekimPages/hekimPages.component';
import { HizmetPagesComponent } from '../components/hizmetPages/hizmetPages.component';
import { BlogPagesComponent } from '../components/blogPages/blogPages.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { AnasayfaPhotoUrlComponent } from '../components/anasayfaPhotoUrl/anasayfaPhotoUrl.component';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        AngularEditorModule,   //Ng kalkov editör
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        DashboardComponent,
        UserComponent,
        LoginComponent,
        GroupComponent,
        LanguageComponent,
        TranslateComponent,
        OperationClaimComponent,
        LogDtoComponent,
        AnasayfaComponent,
        BlogComponent,
        BlogDetailComponent,
        DurumComponent,
        HakkimizdaComponent,
        HekimComponent,
        HekimDetailComponent,
        HizmetComponent,
        HizmetDetailComponent,
        RandevuComponent,
        HekimPagesComponent,
        HizmetPagesComponent,
        BlogPagesComponent,
        AnasayfaPhotoUrlComponent


    ]
})

export class AdminLayoutModule { }
