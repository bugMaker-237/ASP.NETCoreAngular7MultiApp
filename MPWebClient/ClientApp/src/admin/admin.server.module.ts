import { NgModule } from '@angular/core';
import { ServerModule, ServerTransferStateModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AdminModule } from './admin.module';

import { AdminComponent } from './admin.component';

@NgModule({
    imports: [
        AdminModule,
        ServerModule,
        ModuleMapLoaderModule,
        ServerTransferStateModule,
        NoopAnimationsModule
    ],
    bootstrap: [AdminComponent]
})
export class AppServerModule { }
