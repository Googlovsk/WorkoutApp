import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { tap } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService)
  const router = inject(Router)
  const toastr = inject(ToastrService)

  if (authService.isLoggedIn()) {
    const clonedReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + authService.getToken())
    })
    return next(clonedReq).pipe(
      tap({
        error: (err: any) => {
          if (err.status == 401) {//don't have a valid token
            authService.deleteToken()
            setTimeout(() => {
              toastr.info('Пожалуйста, войдите снова', 'Сессия прервана!')
            }, 1500);
            router.navigateByUrl('/signin')
          }
          else if (err.status == 403)
            toastr.error("Упс! Похоже, у вас нет прав для выполнения этого действия.")
        }
      })
    );
  }
  else
    return next(req);
};
