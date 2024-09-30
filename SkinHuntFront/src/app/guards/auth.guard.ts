import { inject } from '@angular/core';
import { AuthService } from './../services/auth.service';
import { CanActivateFn, Router } from "@angular/router";

export function authGuard() : CanActivateFn {
    return () => {
        const authService: AuthService = inject(AuthService);
        const router: Router = inject(Router);

        if(authService.user$()){
            return true;
        }

        router.navigate(['']);
        return false;
    }
}
