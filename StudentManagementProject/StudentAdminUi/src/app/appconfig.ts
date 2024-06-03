import {  provideAnimations } from '@angular/platform-browser/animations';
import { ApplicationConfig, importProvidersFrom } from '@angular/core';

import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { tokenHttpInterceptor } from './token-htpp-interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    importProvidersFrom(HttpClientModule),
    importProvidersFrom(ReactiveFormsModule),
    importProvidersFrom(MatInputModule, MatCardModule),
    provideHttpClient(withInterceptors([tokenHttpInterceptor])),
    // Diğer gerekli sağlayıcıları ekleyin
  ]
};
