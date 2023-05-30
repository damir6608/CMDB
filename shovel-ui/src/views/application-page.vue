<template>
  <h2 class="content-block">Application</h2>
  <button @click="onClick()">Report</button>
  <div>
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
        @selection-changed="onSelectionChanged"
        :selection="{ mode: 'single' }"
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
import router from "@/router";
import axios from "axios";

export default {
  methods: {
    onSelectionChanged({ selectedRowsData }) {
      const datas = selectedRowsData[0];
      console.log(datas.id);
      const idRow = datas.id;
      router.push({ path: '/application-details-page',  query: {id : idRow}})
    },
    onClick() {
      axios({
        url: 'https://localhost:7221/api/ApplicationSystemUI/GetReport',
        method: 'GET',
        responseType: 'blob',
      }).then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement('a');

        fileLink.href = fileURL;
        fileLink.setAttribute('download', 'HelloWorld.xlsx');
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
        url: "https://localhost:7221/api/ApplicationSystemUI/GetAll"
      },
      // expand: "ApplicationSystem"
    };
    return {
      dataSourceConfig,
    };
  },
  components: {
    DxDataGrid,
    DxColumn,
    DxFilterRow,
    DxPager,
    DxPaging,
  }
};
</script>
