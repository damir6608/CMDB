<template>
  <div>
    <h2 class="content-block">Производительность</h2>

    <div  title="Экспортировать данные в Excel" class="dx-item dx-list-item" style="alignment: right; width: auto">
      <div class="dx-item-content dx-list-item-content">
        <div class="dx-list-item-icon-container">
          <i style="alignment: right; width: auto" @click="onClick()" class="dx-icon dx-icon-xlsxfile dx-list-item-icon"></i>
        </div>
        Экспортировать данные в Excel
      </div>
    </div>

    <dx-data-grid
      class="dx-card wide-card"
      :data-source="dataSourceConfig"
      :focused-row-index="0"
      :show-borders="false"
      :focused-row-enabled="true"
      :column-auto-width="true"
      :column-hiding-enabled="true"
      :filter-operations="allowedOperations"
      v-model:selected-filter-operation="selectedOperation"
      :allow-column-resizing="true"
      column-resizing-mode="widget"
      @selection-changed="onSelectionChanged"
      :selection="{ mode: 'single' }"
    >
      <dx-paging :page-size="10" />
      <dx-pager :show-page-size-selector="true" :show-info="true" />
      <dx-filter-row :visible="true" />


      <dx-column
        data-field="operationsystem"
        caption="Операционная система"
        :width="190"
        :hiding-priority="3"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="processorarchitecture"
        caption="Архитектура процессора"
        :hiding-priority="6"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="processormodel"
        caption="Модель процессора"
        :hiding-priority="5"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="processorlevel"
        caption="Уровень процессора"
        :hiding-priority="7"
        alignment='center'
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="systemdirectory"
        caption="Системная директория"
        alignment='center'
        :hiding-priority="3"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="username"
        caption="Имя учетной записи"
        :hiding-priority="4"
        alignment='center'
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="machinename"
        caption="Имя устройства"
        alignment='center'
        :hiding-priority="1"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
          data-field="logicalDrives.length"
          caption="Кол-во жестких дисков"
          name="logicalDrives"
          alignment='center'
          :hiding-priority="1"
          :filter-operations="allowedOperations"
          v-model:selected-filter-operation="selectedOperation"
      />

      <dx-column
        data-field="server.baseaddress"
        caption="Адрес устройства"
        :hiding-priority="0"
        :filter-operations="allowedOperations"
        v-model:selected-filter-operation="selectedOperation"
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
import router from "@/router";
import axios from "axios";

const priorities = [
  { name: "High", value: 4 },
  { name: "Urgent", value: 3 },
  { name: "Normal", value: 2 },
  { name: "Low", value: 1 }
];

export default {
  methods: {
    onSelectionChanged({ selectedRowsData }) {
      const datas = selectedRowsData[0];
      console.log(datas.id);
      let idRow = datas.id;
      router.push({ path: '/performance-details-page',  query: {id : idRow}})
    },
    onClick() {
      axios({
        url: 'https://localhost:7221/api/PerformanceSystemUI/GetReport',
        method: 'GET',
        responseType: 'blob',
      }).then((response) => {
        const fileURL = window.URL.createObjectURL(new Blob([response.data]));
        const fileLink = document.createElement('a');

        const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0].replaceAll('"', '');

        fileLink.href = fileURL;
        fileLink.setAttribute('download',fileName
        );
        document.body.appendChild(fileLink);

        fileLink.click();
      });
    }
  },
  setup() {
    const dataSourceConfig = {
      store: {
         type: "odata",
         key: "id",
        url: "https://localhost:7221/api/PerformanceSystemUI/GetAll"
      },
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
  },
  data() {
    return {
      allowedOperations: ['contains'],
      selectedOperation: 'contains'
    }
  },
};
</script>
