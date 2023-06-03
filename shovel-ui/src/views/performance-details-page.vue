<template>
  <div class="performance-content">
    <h2 class="content-block">Детали производительности</h2>

    <div class="content-block dx-card responsive-paddings">
      <div class="form">
        <div class="dx-fieldset">

          <div class="dx-field">
            <div class="dx-field-label">Операционная система устройства:</div>
            <div class="dx-field-value">
              {{this.resultData.OperationSystem}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Уровень процессора:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessorLevel}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Имя пользователя системы:</div>
            <div class="dx-field-value">
              {{this.resultData.UserName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Количество байтов на странице памяти операционной системы:</div>
            <div class="dx-field-value">
              {{this.resultData.SystemPageSize}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Является ли 64-х битовой ОС:</div>
            <div class="dx-field-value">
              {{this.resultData.Is64BitOperatingSystem === 'true' ? 'Да' : 'Нет'}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Дата сохранения данных:</div>
            <div class="dx-field-value">
              {{new Date(this.resultData.InsertDate).toDateString() + ' ' + new Date(this.resultData.InsertDate).toTimeString()}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Системная директория устройства:</div>
            <div class="dx-field-value">
              {{this.resultData.SystemDirectory}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Имя устройства:</div>
            <div class="dx-field-value">
              {{this.resultData.MachineName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Сколько устройство работает с момента включения:</div>
            <div class="dx-field-value">
              {{this.resultData.TickCount64 / 60_000}} минут
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Является ли 64 битовым процессом:</div>
            <div class="dx-field-value">
              {{this.resultData.Is64BitProcess === 'true' ? 'Да' : 'Нет'}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Дата запущенной синхронизации:</div>
            <div class="dx-field-value">
              {{new Date(this.resultData.SyncTime).toDateString() + ' ' + new Date(this.resultData.SyncTime).toTimeString()}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Архитектура процессора:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessorArchitecture}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Количество ядер процессора:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessorCount}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Индентификатор процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessId}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Запущен ли данный процес в режиме interactive:</div>
            <div class="dx-field-value">
              {{this.resultData.UserInteractive === 'true' ? 'Да' : 'Нет'}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Размер доступной оперативной памяти:</div>
            <div class="dx-field-value">
              {{this.resultData.RamAvailable}} ГБ.
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Модель процессора:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessorModel}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Имя сетевого домена, связанное с текущим пользователем:</div>
            <div class="dx-field-value">
              {{this.resultData.UserDomainName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Путь процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessPath}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Количество физ. памяти, выделеной под процесс:</div>
            <div class="dx-field-value">
              {{this.resultData.WorkingSet}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Версия ПО собирающее данные:</div>
            <div class="dx-field-value">
              {{this.resultData.Version}}
            </div>
          </div>

        </div>
      </div>
    </div>


    <h2 class="content-block">Жесткие диски на устройстве</h2>

    <div class="content-block dx-card responsive-paddings">
        <DxDataGrid
            :data-source="logicalDrives"
        >
          <DxColumn
              :width="80"
              data-field="Drive"
              caption="Диск"/>
          <DxColumn
              :width="120"
              data-field="VolumeLabel"
                    caption="Метка тома"/>
          <DxColumn
              :width="80"
              data-field="DriveType"
                    caption="Тип"/>
          <DxColumn
              :width="140"
              data-field="DriveFormat"
                    caption="Формат диска"/>
          <DxColumn
              :width="200"
              data-field="TotalSize"
                    caption="Общий размер диска"/>
          <DxColumn
              data-field="AvailableFreeSpace"
              caption="Доступное свободное место"
          />
        </DxDataGrid>
    </div>

    <h2 class="content-block">Информация о сервере</h2>
    <div class="content-block dx-card responsive-paddings">
      <div class="form">
        <div class="dx-fieldset">

          <div class="dx-field">
            <div class="dx-field-label">Адрес компьютера с которого собиралась информация</div>
            <div class="dx-field-value">
              <DxTextBox
                  :disabled="true">{{this.server.BaseAddress}}</DxTextBox>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { DxDataGrid, DxColumn } from 'devextreme-vue/data-grid';
import axios from "axios";

export default {
  components: {
    DxDataGrid,
    DxColumn,
  },
  created() {
    this.getById()
  },
  data() {
    return {
      resultData : {
        OperationSystem: '',
        ProcessorLevel: '',
        UserName: '',
        SystemPageSize: '',
        Is64BitOperatingSystem: '',
        InsertDate: '',
        SystemDirectory: '',
        MachineName: '',
        TickCount64: '',
        Is64BitProcess: '',
        SyncTime: '',
        ProcessorArchitecture: '',
        ProcessorCount: '',
        ProcessId: '',
        UserInteractive: '',
        RamAvailable: '',
        ServerId: '',
        ProcessorModel: '',
        UserDomainName: '',
        ProcessPath: '',
        WorkingSet: '',
        Version: ''
      },
      server : {BaseAddress: ''},
      logicalDrives : [],
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

      this.resultData = {
        OperationSystem: res.data.operationsystem,
        ProcessorLevel: res.data.processorlevel,
        UserName: res.data.username,
        SystemPageSize: res.data.systempagesize,
        Is64BitOperatingSystem: res.data.is64bitoperatingsystem,
        InsertDate: res.data.insertdate,
        SystemDirectory: res.data.systemdirectory,
        MachineName: res.data.machinename,
        TickCount64: res.data.tickcount64,
        Is64BitProcess: res.data.is64bitprocess,
        SyncTime: res.data.synctime,
        ProcessorArchitecture: res.data.processorarchitecture,
        ProcessorCount: res.data.processorcount,
        ProcessId: res.data.processid,
        UserInteractive: res.data.userinteractive,
        RamAvailable: res.data.ramavailable,
        ServerId: res.data.serverid,
        ProcessorModel: res.data.processormodel,
        UserDomainName: res.data.userdomainname,
        ProcessPath: res.data.processpath,
        WorkingSet: res.data.workingset,
        Version: res.data.version
      }

      this.server = {BaseAddress: res.data.server.baseaddress};

      for (const logicalDrive of res.data.logicalDrives) {
        this.logicalDrives.push({
          AvailableFreeSpace: (logicalDrive.availablefreespace / 1024 / 1024 / 1024).toString() + ' Гб' ,
          Drive: logicalDrive.drive,
          DriveFormat: logicalDrive.driveformat,
          DriveType: logicalDrive.drivetype,
          PerformanceSystemId: logicalDrive.performancesystemid,
          TotalSize: (logicalDrive.totalsize / 1024 / 1024 / 1024).toString() + ' Гб' ,
          VolumeLabel: logicalDrive.volumelabel,
        })
      }

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
