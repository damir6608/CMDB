<template>
  <div>
    <h2 class="content-block">Application</h2>

    <dx-data-grid
        class="dx-card wide-card"
        :data-source="dataSourceConfig"
        :focused-row-index="0"
        :show-borders="false"
        :focused-row-enabled="true"
        :column-auto-width="true"
        :column-hiding-enabled="true"
        :allow-column-resizing="true"
        column-resizing-mode="widget"
    >
      <dx-paging :page-size="10" />
      <dx-pager :show-page-size-selector="true" :show-info="true" />
      <dx-filter-row :visible="true" />


      <dx-column
          data-field="mainwindowtitle"
          caption="Название приложения"
          alignment='center'
          :width="190"
          :hiding-priority="3"
      />

      <dx-column
          data-field="processname"
          caption="Имя процесса"
          alignment='center'
          :hiding-priority="6"
      />

      <dx-column
          data-field="basepriority"
          caption="Приоритет"
          alignment='center'
          :hiding-priority="6"
      />

      <dx-column
          data-field="pagedmemorysize64"
          caption="Память"
          alignment='center'
          :hiding-priority="5"
      />

      <dx-column
          data-field="peakworkingset64"
          caption="Макс. нагрузка"
          :hiding-priority="7"
          alignment='center'
      />

      <dx-column
          data-field="threadscount"
          caption="Кол-во потоков"
          alignment='center'
          :hiding-priority="3"
      />

      <dx-column
          data-field="server.baseaddress"
          caption="Адрес устройства"
          :hiding-priority="0"
      />
    </dx-data-grid>
  </div>
</template>

<script>
import "devextreme/data/odata/store";
import DxDataGrid, {
  DxColumn,
  DxFilterRow,
  DxPager,
  DxPaging
} from "devextreme-vue/data-grid";

export default {
  setup() {
    const dataSourceConfig = {
      store: {
        type: "odata",
        key: "id",
        url: "https://localhost:7221/api/ApplicationSystemUI/GetAll"
      },
      // expand: "ApplicationSystem"
    };
    return {
      dataSourceConfig
    };
  },
  components: {
    DxDataGrid,
    DxColumn,
    DxFilterRow,
    DxPager,
    DxPaging
  }
};
</script>
