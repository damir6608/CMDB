<template>
  <div>
    <h2 class="content-block">Profile</h2>

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

    <h2 class="content-block">Permissions</h2>
    <div class="content-block dx-card responsive-paddings">
      <DxDataGrid
          :data-source="userRoles"
          key-expr="id"
      >
        <DxColumn
            data-field="role"
            caption="Role name"
        />
        <DxColumn
            data-field="permType"
            caption="Permission type"
        />
        <DxColumn
            data-field="permName"
            caption="Permission name"
        />
      </DxDataGrid>
    </div>
  </div>
</template>

<script>
import DxForm from "devextreme-vue/form";
import { DxDataGrid, DxColumn } from 'devextreme-vue/data-grid';
import auth from "@/auth";
export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxForm,
  },
  created() {
    this.getUser().then((data) => {
      this.resultData =  this.getProfile(data);
      this.userRoles = this.getUserRoles(data);
      console.log(this.userRoles)
    })
  },
  data() {
    return {
      resultData : null,
      server : null,
      userRoles :  null,
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
    getUserRoles(data){
      this.userRoles = [];
      const roles = data.userRoles;
      roles.forEach(role => {
        console.log(role)
        let id = 0;
        role.role.rolePermissions.forEach(perm => {
          console.log(perm)
          id++;
          this.userRoles.push({ id: id, role: role.role.name, permName: perm.permission.name, permType: perm.permission.type })
        })
      });
      console.log(this.userRoles)
      return this.userRoles;
    },
    getProfile(data){
      return {
        name: data.name,
        email: data.email,
        phone: data.phone
      };
    },
    async getUser(){
      return auth.getUser();
    },
  },
};
</script>

<style lang="scss">
.form-avatar {
  float: left;
  height: 120px;
  width: 120px;
  margin-right: 20px;
  border: 1px solid rgba(0, 0, 0, 0.1);
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
  background-color: #fff;
  overflow: hidden;

  img {
    height: 120px;
    display: block;
    margin: 0 auto;
  }
}
</style>
