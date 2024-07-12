# FrontEnd

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 14.2.5.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

```
Angular_VCProject
├─ .browserslistrc
├─ .editorconfig
├─ .git
│  ├─ config
│  ├─ description
│  ├─ HEAD
│  ├─ hooks
│  │  ├─ applypatch-msg.sample
│  │  ├─ commit-msg.sample
│  │  ├─ fsmonitor-watchman.sample
│  │  ├─ post-update.sample
│  │  ├─ pre-applypatch.sample
│  │  ├─ pre-commit.sample
│  │  ├─ pre-merge-commit.sample
│  │  ├─ pre-push.sample
│  │  ├─ pre-rebase.sample
│  │  ├─ pre-receive.sample
│  │  ├─ prepare-commit-msg.sample
│  │  ├─ push-to-checkout.sample
│  │  ├─ sendemail-validate.sample
│  │  └─ update.sample
│  ├─ index
│  ├─ info
│  │  └─ exclude
│  ├─ logs
│  │  ├─ HEAD
│  │  └─ refs
│  │     ├─ heads
│  │     │  └─ main
│  │     └─ remotes
│  │        └─ origin
│  │           └─ HEAD
│  ├─ objects
│  │  ├─ info
│  │  └─ pack
│  │     ├─ pack-c5354da48c3e1e84c62c57b48ad53b0d0527f959.idx
│  │     ├─ pack-c5354da48c3e1e84c62c57b48ad53b0d0527f959.pack
│  │     └─ pack-c5354da48c3e1e84c62c57b48ad53b0d0527f959.rev
│  ├─ packed-refs
│  └─ refs
│     ├─ heads
│     │  └─ main
│     ├─ remotes
│     │  └─ origin
│     │     └─ HEAD
│     └─ tags
├─ .gitattributes
├─ .gitignore
├─ angular.json
├─ karma.conf.js
├─ kendo-ui-license.txt
├─ package-lock.json
├─ package.json
├─ README.md
├─ src
│  ├─ app
│  │  ├─ admin-side
│  │  │  ├─ admin-side-routing.module.ts
│  │  │  ├─ admin-side.module.ts
│  │  │  ├─ CustomPipe
│  │  │  │  └─ filter.pipe.ts
│  │  │  ├─ dashboard
│  │  │  │  ├─ dashboard.component.css
│  │  │  │  ├─ dashboard.component.html
│  │  │  │  ├─ dashboard.component.spec.ts
│  │  │  │  └─ dashboard.component.ts
│  │  │  ├─ header
│  │  │  │  ├─ header.component.css
│  │  │  │  ├─ header.component.html
│  │  │  │  ├─ header.component.spec.ts
│  │  │  │  └─ header.component.ts
│  │  │  ├─ mission
│  │  │  │  ├─ add-mission
│  │  │  │  │  ├─ add-mission.component.css
│  │  │  │  │  ├─ add-mission.component.html
│  │  │  │  │  ├─ add-mission.component.spec.ts
│  │  │  │  │  └─ add-mission.component.ts
│  │  │  │  ├─ mission.component.css
│  │  │  │  ├─ mission.component.html
│  │  │  │  ├─ mission.component.spec.ts
│  │  │  │  ├─ mission.component.ts
│  │  │  │  └─ update-mission
│  │  │  │     ├─ update-mission.component.css
│  │  │  │     ├─ update-mission.component.html
│  │  │  │     ├─ update-mission.component.spec.ts
│  │  │  │     └─ update-mission.component.ts
│  │  │  ├─ mission-application
│  │  │  │  ├─ mission-application.component.css
│  │  │  │  ├─ mission-application.component.html
│  │  │  │  ├─ mission-application.component.spec.ts
│  │  │  │  └─ mission-application.component.ts
│  │  │  ├─ missionskill
│  │  │  │  ├─ add-mission-skill
│  │  │  │  │  ├─ add-mission-skill.component.css
│  │  │  │  │  ├─ add-mission-skill.component.html
│  │  │  │  │  ├─ add-mission-skill.component.spec.ts
│  │  │  │  │  └─ add-mission-skill.component.ts
│  │  │  │  ├─ missionskill.component.css
│  │  │  │  ├─ missionskill.component.html
│  │  │  │  ├─ missionskill.component.spec.ts
│  │  │  │  └─ missionskill.component.ts
│  │  │  ├─ missiontheme
│  │  │  │  ├─ add-mission-theme
│  │  │  │  │  ├─ add-mission-theme.component.css
│  │  │  │  │  ├─ add-mission-theme.component.html
│  │  │  │  │  ├─ add-mission-theme.component.spec.ts
│  │  │  │  │  └─ add-mission-theme.component.ts
│  │  │  │  ├─ missiontheme.component.css
│  │  │  │  ├─ missiontheme.component.html
│  │  │  │  ├─ missiontheme.component.spec.ts
│  │  │  │  └─ missiontheme.component.ts
│  │  │  ├─ sidebar
│  │  │  │  ├─ sidebar.component.css
│  │  │  │  ├─ sidebar.component.html
│  │  │  │  ├─ sidebar.component.spec.ts
│  │  │  │  └─ sidebar.component.ts
│  │  │  └─ user
│  │  │     ├─ add-user
│  │  │     │  ├─ add-user.component.css
│  │  │     │  ├─ add-user.component.html
│  │  │     │  ├─ add-user.component.spec.ts
│  │  │     │  └─ add-user.component.ts
│  │  │     ├─ update-user
│  │  │     │  ├─ update-user.component.css
│  │  │     │  ├─ update-user.component.html
│  │  │     │  ├─ update-user.component.spec.ts
│  │  │     │  └─ update-user.component.ts
│  │  │     ├─ user.component.css
│  │  │     ├─ user.component.html
│  │  │     ├─ user.component.spec.ts
│  │  │     └─ user.component.ts
│  │  ├─ app-routing.module.ts
│  │  ├─ app.component.css
│  │  ├─ app.component.html
│  │  ├─ app.component.spec.ts
│  │  ├─ app.component.ts
│  │  ├─ app.module.ts
│  │  ├─ Helper
│  │  │  └─ ValidateForm.ts
│  │  ├─ Interceptors
│  │  │  ├─ token.interceptor.spec.ts
│  │  │  └─ token.interceptor.ts
│  │  ├─ LoginRegister
│  │  │  ├─ forgot-password
│  │  │  │  ├─ forgot-password.component.css
│  │  │  │  ├─ forgot-password.component.html
│  │  │  │  ├─ forgot-password.component.spec.ts
│  │  │  │  └─ forgot-password.component.ts
│  │  │  ├─ login
│  │  │  │  ├─ login.component.css
│  │  │  │  ├─ login.component.html
│  │  │  │  ├─ login.component.spec.ts
│  │  │  │  └─ login.component.ts
│  │  │  ├─ register
│  │  │  │  ├─ register.component.css
│  │  │  │  ├─ register.component.html
│  │  │  │  ├─ register.component.spec.ts
│  │  │  │  └─ register.component.ts
│  │  │  └─ reset-password
│  │  │     ├─ reset-password.component.css
│  │  │     ├─ reset-password.component.html
│  │  │     ├─ reset-password.component.spec.ts
│  │  │     └─ reset-password.component.ts
│  │  ├─ model
│  │  │  ├─ cms.model.ts
│  │  │  ├─ missionApplication.model.ts
│  │  │  ├─ missionSkill.model.ts
│  │  │  ├─ missionTheme.model.ts
│  │  │  ├─ user.model.ts
│  │  │  └─ volunteering.model.ts
│  │  ├─ NavBar
│  │  │  ├─ footer
│  │  │  │  ├─ footer.component.css
│  │  │  │  ├─ footer.component.html
│  │  │  │  ├─ footer.component.spec.ts
│  │  │  │  └─ footer.component.ts
│  │  │  ├─ home
│  │  │  │  ├─ home.component.css
│  │  │  │  ├─ home.component.html
│  │  │  │  ├─ home.component.spec.ts
│  │  │  │  └─ home.component.ts
│  │  │  ├─ navbar
│  │  │  │  ├─ navbar.component.css
│  │  │  │  ├─ navbar.component.html
│  │  │  │  ├─ navbar.component.spec.ts
│  │  │  │  └─ navbar.component.ts
│  │  │  └─ searchin-sorting
│  │  │     ├─ searchin-sorting.component.css
│  │  │     ├─ searchin-sorting.component.html
│  │  │     ├─ searchin-sorting.component.spec.ts
│  │  │     └─ searchin-sorting.component.ts
│  │  ├─ new-mission
│  │  │  ├─ new-mission.component.css
│  │  │  ├─ new-mission.component.html
│  │  │  ├─ new-mission.component.spec.ts
│  │  │  └─ new-mission.component.ts
│  │  ├─ Pipe
│  │  │  ├─ search.pipe.spec.ts
│  │  │  └─ search.pipe.ts
│  │  ├─ privacy-policy
│  │  │  ├─ privacy-policy.component.css
│  │  │  ├─ privacy-policy.component.html
│  │  │  ├─ privacy-policy.component.spec.ts
│  │  │  └─ privacy-policy.component.ts
│  │  ├─ service
│  │  │  ├─ adminlogin.service.ts
│  │  │  ├─ adminside-service.service.ts
│  │  │  ├─ auth.guard.ts
│  │  │  ├─ client.service.ts
│  │  │  ├─ common.service.ts
│  │  │  └─ user-type.guard.ts
│  │  ├─ usereditprofile
│  │  │  ├─ usereditprofile.component.css
│  │  │  ├─ usereditprofile.component.html
│  │  │  ├─ usereditprofile.component.spec.ts
│  │  │  └─ usereditprofile.component.ts
│  │  ├─ volun-volunteering-mission
│  │  │  ├─ volun-volunteering-mission.component.css
│  │  │  ├─ volun-volunteering-mission.component.html
│  │  │  ├─ volun-volunteering-mission.component.spec.ts
│  │  │  └─ volun-volunteering-mission.component.ts
│  │  └─ volunteering-timesheet
│  │     ├─ volunteering-timesheet.component.css
│  │     ├─ volunteering-timesheet.component.html
│  │     ├─ volunteering-timesheet.component.spec.ts
│  │     └─ volunteering-timesheet.component.ts
│  ├─ assets
│  │  ├─ .gitkeep
│  │  ├─ Images
│  │  │  ├─ 1.png
│  │  │  ├─ 2.png
│  │  │  ├─ 3.png
│  │  │  ├─ 4.png
│  │  │  ├─ 5.png
│  │  │  ├─ 6.png
│  │  │  ├─ 7.png
│  │  │  ├─ image.png
│  │  │  └─ user.png
│  │  ├─ Images1
│  │  │  ├─ 1.png
│  │  │  ├─ 11.png
│  │  │  ├─ 12.png
│  │  │  ├─ 2.png
│  │  │  ├─ 21.png
│  │  │  ├─ 22.png
│  │  │  ├─ 3.png
│  │  │  ├─ 31.png
│  │  │  ├─ 32.png
│  │  │  ├─ 33.png
│  │  │  ├─ 34.png
│  │  │  ├─ 41.png
│  │  │  ├─ video.mov
│  │  │  ├─ volunteer1.png
│  │  │  ├─ volunteer2.png
│  │  │  ├─ volunteer3.png
│  │  │  ├─ volunteer4.png
│  │  │  ├─ volunteer5.png
│  │  │  ├─ volunteer6.png
│  │  │  ├─ volunteer7.png
│  │  │  ├─ volunteer8.png
│  │  │  └─ volunteer9.png
│  │  ├─ Img
│  │  │  ├─ 404-Page-image.png
│  │  │  ├─ achieved.png
│  │  │  ├─ add.png
│  │  │  ├─ add1.png
│  │  │  ├─ Already-volunteered.png
│  │  │  ├─ Animal-welfare-_-save-birds-campaign-1.png
│  │  │  ├─ animal.png
│  │  │  ├─ arrow.png
│  │  │  ├─ Assets.zip
│  │  │  ├─ bell-big.png
│  │  │  ├─ bell.png
│  │  │  ├─ bin.png
│  │  │  ├─ calender.png
│  │  │  ├─ cancel.png
│  │  │  ├─ cancel1.png
│  │  │  ├─ checked.png
│  │  │  ├─ cross.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-1.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-2.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-3.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-5.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity-6.png
│  │  │  ├─ CSR-initiative-stands-for-Coffee--and-Farmer-Equity.png
│  │  │  ├─ deadline.png
│  │  │  ├─ doc.png
│  │  │  ├─ down.png
│  │  │  ├─ drag-and-drop.png
│  │  │  ├─ drop-down.png
│  │  │  ├─ Education-Supplies-for-Every--Pair-of-Shoes-Sold-1.png
│  │  │  ├─ Education-Supplies-for-Every--Pair-of-Shoes-Sold-2.png
│  │  │  ├─ Education-Supplies-for-Every--Pair-of-Shoes-Sold.png
│  │  │  ├─ eye.png
│  │  │  ├─ facebook.png
│  │  │  ├─ filter.png
│  │  │  ├─ gift.png
│  │  │  ├─ graph.png
│  │  │  ├─ grid.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-1.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-2.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-3.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-4.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-5.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability-login.png
│  │  │  ├─ Grow-Trees-On-the-path-to-environment-sustainability.png
│  │  │  ├─ heart.png
│  │  │  ├─ heart1.png
│  │  │  ├─ heart11.png
│  │  │  ├─ hours.png
│  │  │  ├─ image.png
│  │  │  ├─ image1.png
│  │  │  ├─ image2.png
│  │  │  ├─ img.png
│  │  │  ├─ img1.png
│  │  │  ├─ img11.png
│  │  │  ├─ img2.png
│  │  │  ├─ img22.png
│  │  │  ├─ img3.png
│  │  │  ├─ img33.png
│  │  │  ├─ info.png
│  │  │  ├─ left.png
│  │  │  ├─ left1.png
│  │  │  ├─ linkedin.png
│  │  │  ├─ list.png
│  │  │  ├─ logo.png
│  │  │  ├─ mission.png
│  │  │  ├─ my-story.png
│  │  │  ├─ new-message.png
│  │  │  ├─ next.png
│  │  │  ├─ Nourish-the-Children-in--African-country-1.png
│  │  │  ├─ Nourish-the-Children-in--African-country.png
│  │  │  ├─ Open-Volunteering-Requests.png
│  │  │  ├─ organization.png
│  │  │  ├─ pdf.png
│  │  │  ├─ pin.png
│  │  │  ├─ pin1.png
│  │  │  ├─ Plantation-and-Afforestation-programme-1.png
│  │  │  ├─ Plantation.png
│  │  │  ├─ previous.png
│  │  │  ├─ right-arrow.png
│  │  │  ├─ right-arrow1.png
│  │  │  ├─ right-arrow2.png
│  │  │  ├─ right-white.png
│  │  │  ├─ right.png
│  │  │  ├─ search.png
│  │  │  ├─ Seats-left.png
│  │  │  ├─ selected-star.png
│  │  │  ├─ settings.png
│  │  │  ├─ skill.png
│  │  │  ├─ star-empty.png
│  │  │  ├─ star.png
│  │  │  ├─ twitter.png
│  │  │  ├─ up.png
│  │  │  ├─ user-img-large.png
│  │  │  ├─ user-img.png
│  │  │  ├─ user.png
│  │  │  ├─ user1.png
│  │  │  ├─ volunteer1.png
│  │  │  ├─ volunteer2.png
│  │  │  ├─ volunteer3.png
│  │  │  ├─ volunteer4.png
│  │  │  ├─ volunteer5.png
│  │  │  ├─ volunteer6.png
│  │  │  ├─ volunteer7.png
│  │  │  ├─ volunteer8.png
│  │  │  ├─ volunteer9.png
│  │  │  ├─ Volunteering-Rank.png
│  │  │  ├─ Voted-Missions.png
│  │  │  ├─ web.png
│  │  │  └─ xlsx.png
│  │  ├─ img1.png
│  │  ├─ NewImages
│  │  │  ├─ 1.jpeg
│  │  │  ├─ 10.jpeg
│  │  │  ├─ 2.jpeg
│  │  │  ├─ 3.jpeg
│  │  │  ├─ 4.jpeg
│  │  │  ├─ 5.jpeg
│  │  │  ├─ 6.jpeg
│  │  │  ├─ 7.jpeg
│  │  │  ├─ 8.jpeg
│  │  │  └─ 9.jpeg
│  │  ├─ NoImg.png
│  │  ├─ NoUser.png
│  │  └─ tabs.css
│  ├─ environments
│  │  ├─ environment.prod.ts
│  │  └─ environment.ts
│  ├─ favicon.ico
│  ├─ index.html
│  ├─ main.ts
│  ├─ polyfills.ts
│  ├─ styles.css
│  └─ test.ts
├─ tsconfig.app.json
├─ tsconfig.json
└─ tsconfig.spec.json

```