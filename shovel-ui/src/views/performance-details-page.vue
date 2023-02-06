<template>
  <div class="performance-content">
    <h2 class="content-block">Performance details</h2>

    <div class="content-block dx-card responsive-paddings">
        <DxForm
            id="form"
            :form-data="resultData"
            :col-count-by-screen="colCountByScreen"
            :screen-by-width="screenByWidth"
            :min-col-width="233"
            label-location="top"
            col-count="auto"
        />
    </div>
        <h2 class="content-block">Logical drives</h2>

    <div class="content-block dx-card responsive-paddings">
        <DxDataGrid
            :data-source="logicalDrives"
            key-expr="id"
        >
          <DxColumn
              :width="80"
              data-field="drive"
              caption="Drive"
          />
          <DxColumn data-field="volumelabel"/>
          <DxColumn data-field="drivetype"/>
          <DxColumn data-field="driveformat"/>
          <DxColumn data-field="totalsize"/>
          <DxColumn
              data-field="availablefreespace"
          />
        </DxDataGrid>
    </div>

    <h2 class="content-block">Server information</h2>
    <div class="content-block dx-card responsive-paddings">
        <DxForm
            id="server_form"
            :form-data="server"
            :col-count-by-screen="colCountByScreen"
            :screen-by-width="screenByWidth"
            :min-col-width="233"
            label-location="top"
            col-count="auto"
        />
    </div>
  </div>
</template>
<script>
import DxForm from 'devextreme-vue/form';
import { DxDataGrid, DxColumn } from 'devextreme-vue/data-grid';
import axios from "axios";

export default {
  components: {
    DxForm,
    DxDataGrid,
    DxColumn,
  },
  created() {
    this.getById()
  },
  data() {
    return {
      resultData : null,
      server : null,
      logicalDrives : null,
      calculateColCountAutomatically: false,
    };
  },
  computed: {
    colCountByScreen() {
      return this.calculateColCountAutomatically
          ? null
          : {
            sm: 2,
            md: 4,
          };
    },
  },
  methods: {
    screenByWidth(width) {
      return width < 720 ? 'sm' : 'md';
    },
    async getById(){
      const res = await axios.get("https://localhost:7221/api/PerformanceSystemUI/GetPerformanceById/"+ this.$route.query.id);
      this.resultData = res.data;
      this.server = res.data.server;
      this.logicalDrives = res.data.logicalDrives;
      console.log(this.logicalDrives)
      delete this.resultData['server'];
      delete this.resultData['logicalDrives'];
    },
  },
};
</script>
<style scoped>
#form {
  padding: 10px 10px 20px;
}
#server_form {
  padding: 10px 10px 20px;
}

.options {
  padding: 20px;
  background-color: rgba(191, 191, 191, 0.15);
  left: 0;
  position: absolute;
  bottom: 0;
  right: 0;
}

.caption {
  font-size: 18px;
  font-weight: 500;
}

.option {
  margin-top: 10px;
}

.performance-content {

}
</style>