import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { AppConfig } from './app/config/config';

export function getBaseUrl() {
  return this.config.setting['PathAPI'];
}

const providers = [
  { provide: 'BASE_URL', useValue: "https://localhost:44335/api/"}
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
