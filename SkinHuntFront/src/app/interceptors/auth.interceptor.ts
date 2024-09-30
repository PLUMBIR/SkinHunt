import { HttpInterceptorFn } from '@angular/common/http';

export const customInterceptor: HttpInterceptorFn = (req, next) => {

  if (req.url.includes('/signIn')) {
    return next(req);
  }

  if (req.url.includes('/signUp')) {
    return next(req);
  }

  const user = localStorage.getItem('user');

  if (user) {
    const userObject = JSON.parse(user);
    const cloneRequest = req.clone({
      setHeaders:{
        Authorization: `Bearer ${userObject.token}`
        }
      });

    return next(cloneRequest);
  }

  return next(req);
};
