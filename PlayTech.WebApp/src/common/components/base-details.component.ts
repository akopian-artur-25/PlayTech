import { ChangeDetectorRef, OnDestroy, OnInit, Directive } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import {ActivatedRoute, Router} from '@angular/router';
import {LocalizeRouterService} from '@gilsdav/ngx-translate-router';

@Directive()
@UntilDestroy()
export abstract class BaseDetailsComponent<TDetails> implements OnInit, OnDestroy{

  public readonly isLoading$ = new BehaviorSubject<boolean>(false);

  model?: TDetails;

  public id?: number;
  public slug?: string;

  protected constructor(protected readonly route: ActivatedRoute,
                        protected readonly router: Router,
                        protected readonly localizeRouterService: LocalizeRouterService,
                        protected readonly cd: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.id = this.getIdentifier();
    this.slug = this.getSlug();
    this.refreshModel();
  }

  public getIdentifier() {
    let numberStr = this.route.snapshot.params['id'];
    if(!numberStr) return undefined;
    let number = Number(numberStr);
    if(isNaN(number) || number <= 0) {
      this.router.navigate([this.localizeRouterService.translateRoute('/404')]);
      return;
    }
    return number;
  }

  public getSlug() {
    let slug = this.route.snapshot.params['slug'];
    if(!slug) return undefined;
    return slug;
  }

  protected refreshModel() {
    this.isLoading$.next(true);
    this.refreshObservable().pipe(untilDestroyed(this))
      .subscribe((model: TDetails) => {
        this.model = model;
        this.cd.detectChanges();
        this.modelLoaded();
        this.isLoading$.next(false);
      }, () => {
        this.isLoading$.next(false);
        this.router.navigate([this.localizeRouterService.translateRoute('/404')]);
      });
  }

  protected abstract refreshObservable();

  protected modelLoaded() {
  }

  ngOnDestroy(): void {
  }
}
