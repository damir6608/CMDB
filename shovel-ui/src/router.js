import ServicePage from './views/service-page';
import PerformanceDetailsPage from './views/performance-details-page';
import ApplicationDetailsPage from './views/application-details-page';
import ApplicationPage from './views/application-page';
import auth from "./auth";
import { createRouter, createWebHashHistory } from "vue-router";

import Home from "./views/home-page";
import Profile from "./views/profile-page";
import Performance from "./views/performance-page";
import defaultLayout from "./layouts/side-nav-outer-toolbar";
import simpleLayout from "./layouts/single-card";

function loadView(view) {
  return () => import (/* webpackChunkName: "login" */ `./views/${view}.vue`)
}

const router = new createRouter({
  routes: [
    {
      path: "/home",
      name: "home",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: Home
    },
    {
      path: "/profile",
      name: "profile",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: Profile
    },
    {
      path: "/performance",
      name: "performance",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: Performance
    },
    {
      path: "/login-form",
      name: "login-form",
      meta: {
        requiresAuth: false,
        layout: simpleLayout,
        title: "Sign In"
      },
      component: loadView("login-form")
    },
    {
      path: "/reset-password",
      name: "reset-password",
      meta: {
        requiresAuth: false,
        layout: simpleLayout,
        title: "Reset Password",
        description: "Please enter the email address that you used to register, and we will send you a link to reset your password via Email."
      },
      component: loadView("reset-password-form")
    },
    {
      path: "/create-account",
      name: "create-account",
      meta: {
        requiresAuth: false,
        layout: simpleLayout,
        title: "Sign Up"
      },
      component: loadView("create-account-form"),
    },
    {
      path: "/change-password/:recoveryCode",
      name: "change-password",
      meta: {
        requiresAuth: false,
        layout: simpleLayout,
        title: "Change Password"
      },
      component: loadView("change-password-form")
    },
    {
      path: "/",
      redirect: "/home"
    },
    {
      path: "/recovery",
      redirect: "/home"
    },
    {
      path: "/:pathMatch(.*)*",
      redirect: "/home"
    }, 
    {
      path: "/application-page",
      name: "application-page",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: ApplicationPage
    }, 
    {
      path: "/application-details-page",
      name: "application-details-page",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: ApplicationDetailsPage
    }, 
    {
      path: "/performance-details-page",
      name: "performance-details-page",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: PerformanceDetailsPage
    }, 
    {
      path: "/service-page",
      name: "service-page",
      meta: {
        requiresAuth: true,
        layout: defaultLayout
      },
      component: ServicePage
    }
  ],
  history: createWebHashHistory()
});

router.beforeEach((to, from, next) => {

  if (to.name === "login-form" && auth.loggedIn()) {
    next({ name: "home" });
  }

  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!auth.loggedIn()) {
      next({
        name: "login-form",
        query: { redirect: to.fullPath }
      });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router;
