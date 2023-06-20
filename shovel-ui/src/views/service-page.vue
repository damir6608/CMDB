<template>
  <div>
    <h2 class="content-block">Конфигурация</h2>
    <div class="content-block">
      <div class="dx-card responsive-paddings">
        <form
            action="https://192.168.43.187:7221/api/ConfigurationUI/RunCommand"
            method="post"
            @submit="handleSubmit"
        >
          <DxForm
              v-model:form-data="model"
              :read-only="false"
              :show-colon-after-label="true"
              :show-validation-summary="true"
              validation-group="customerData"
          >
            <DxGroupItem caption="Конфигурация устройств">
              <DxSimpleItem
                  data-field="command"
                  v-model="model.command"
                  help-text="Введите команду, которую вы хотите выполнить на всех активных устройствах"

              >

              </DxSimpleItem>
            </DxGroupItem>
            <DxButtonItem
                id="sendButton"
                :button-options="buttonOptions"
                horizontal-alignment="left"
            />
          </DxForm>
        </form>
      </div>
    </div>
  </div>
</template>

<style lang="scss">
</style>

<script>
import DxForm, {
  DxGroupItem,
  DxSimpleItem,
  DxButtonItem,
} from 'devextreme-vue/form';

import notify from 'devextreme/ui/notify';
import axios from "axios";
import auth from "@/auth";

export default {
  components: {
    DxGroupItem,
    DxSimpleItem,
    DxButtonItem,
    DxForm,
  },
  data() {
    return {
      model: { command: 'calc' },
      buttonOptions: {
        text: 'Отправить',
        type: 'success',
        disabled: false,
        useSubmitBehavior: true,
      },
    };
  },
  methods: {
    handleSubmit(e) {
        this.submitted = true
        notify({
          message: 'Вы отправили команду',
          position: {
            my: 'center top',
            at: 'center top',
          },
        }, 'success', 3000);
        e.preventDefault();
        axios.post("https://192.168.43.187:7221/api/ConfigurationUI/RunCommand", e.target);
      console.log(e)//TODO sending command
    },
    async getUser(){
      return auth.getUser();
    },
  },
};
</script>
