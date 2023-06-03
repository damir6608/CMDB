<template>
  <div>
    <h2 class="content-block">Домашняя страница</h2>
    <div class="content-block">
      <div class="dx-card responsive-paddings">
        <p>
          Это пользовательский интерфейс проекта агрегации данных, который позволяет просматривать характеристики компьютеров и запущенных приложений, а также отправлять различные команды.
        </p>

      </div>
    </div>
  </div>
</template>

<style lang="scss">
.logos-container {
  margin: 20px 0 40px 0;
  text-align: center;
  svg {
    display: inline-block;
  }
}

.devextreme-logo {
  width: 200px;
  height: 34px;
  margin-bottom: 17px;
}

.vue-logo {
  width: 180px;
  height: 62px;
}

.plus {
  margin: 20px 10px;
  width: 22px;
  height: 22px;
}

.screen-x-small .logos-container {
  svg {
    width: 100%;
    display: block;
    &.plus {
      margin: 0;
    }
  }
}
</style>
