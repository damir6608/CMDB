<template>
  <div>
    <h2 class="content-block">Service</h2>
    <div class="content-block">
      <div class="dx-card responsive-paddings">
        <form
            action="your-action"
            @submit="handleSubmit"
        >
          <DxForm
              v-model:form-data="model"
              :read-only="false"
              :show-colon-after-label="true"
              :show-validation-summary="true"
              validation-group="customerData"
          >
            <DxGroupItem caption="Device management">
              <DxSimpleItem
                  data-field="command"
                  help-text="Enter the command which you want to run in all active devices"
              >
              </DxSimpleItem>
            </DxGroupItem>
            <DxButtonItem
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
        text: 'Send',
        type: 'success',
        useSubmitBehavior: true,
      }
    };
  },
  methods: {
    handleSubmit(e) {
      notify({
        message: 'You have send the command',
        position: {
          my: 'center top',
          at: 'center top',
        },
      }, 'success', 3000);
      e.preventDefault(); //TODO sending command
    },
  },
};
</script>
