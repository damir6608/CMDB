<template>
  <div>
    <h2 class="content-block">Performance</h2>

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
        data-field="operationsystem"
        caption="Операционная система"
        :width="190"
        :hiding-priority="3"
      />

      <dx-column
        data-field="processorarchitecture"
        caption="Архитектура процессора"
        :hiding-priority="6"
      />

      <dx-column
        data-field="processormodel"
        caption="Модель процессора"
        :hiding-priority="5"
      />

      <dx-column
        data-field="processorlevel"
        caption="Уровень процессора"
        :hiding-priority="7"
        alignment='center'
      />

      <dx-column
        data-field="systemdirectory"
        caption="Системная директория"
        alignment='center'
        :hiding-priority="3"
      />

      <dx-column
        data-field="username"
        caption="Имя учетной записи"
        :hiding-priority="4"
        alignment='center'
      />

      <dx-column
        data-field="machinename"
        caption="Имя устройства"
        alignment='center'
        :hiding-priority="1"
      />

      <dx-column
          data-field="logicalDrives.length"
          caption="Кол-во жестких дисков"
          name="logicalDrives"
          alignment='center'
          :hiding-priority="1"
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

const priorities = [
  { name: "High", value: 4 },
  { name: "Urgent", value: 3 },
  { name: "Normal", value: 2 },
  { name: "Low", value: 1 }
];

export default {
  setup() {
    const dataSourceConfig = {
      store: {
         type: "odata",
         key: "id",
        url: "https://localhost:7221/api/PerformanceSystemUI/GetAll"
      },
      // expand: "PerformanceSystem",
      // select: [
      //   "id",
      //   "operationsystem",
      //   "processorarchitecture",
      //   "processormodel",
      //   "processorlevel",
      //   "systemdirectory",
      //   "username",
      //   "machinename",
      //   "logicalDrives",
      //   "server.baseaddress"
      // ]
    };
    return {
      dataSourceConfig,
      priorities
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
