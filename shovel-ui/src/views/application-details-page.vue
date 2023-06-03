<template>
  <h2 class="content-block">Детали приложения</h2>
    <div class="content-block dx-card responsive-paddings">
      <div class="form">
        <div class="dx-fieldset">

          <div class="dx-field">
            <div class="dx-field-label">Имя устройства, на котором запущено приложение:</div>
            <div class="dx-field-value">
              {{this.resultData.MachineName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Объем выгружаемой системной памяти в байтах, выделенной для связанного процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.PagedMemorySize64}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Значение, указывающее, следует ли операционной системе временно повысить <br>приоритет соответствующего процесса, когда главное окно находится в фокусе:</div>
            <div class="dx-field-value">
              {{this.resultData.PriorityBoostEnabled === 'true' ? 'Да' : 'Нет'}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Время работы с момента запуска:</div>
            <div class="dx-field-value">
              {{this.resultData.WorkingSet64 / 60_000}} минут
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Базовый приоритет связанного процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.BasePriority}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Максимально допустимый рабочий набор для связанного процесса.<br>В macOS и FreeBSD установка значения работает только для текущего процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.MaxWorkingset}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Объем выгружаемой системной памяти в байтах, выделенной для связанного процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.PagedSystemMemorySize64}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Понятное название процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.ProcessName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Заголовок запущенного приложения:</div>
            <div class="dx-field-value">
              {{this.resultData.MainWindowTitle}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Значение, указывающее, был ли завершен связанный процесс:</div>
            <div class="dx-field-value">
              {{this.resultData.HasExited === 'true' ? 'Да' : 'Нет'}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Минимально допустимый рабочий набор для связанного процесса.<br>В macOS и FreeBSD установка значения работает только для текущего процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.MinWorkingSet}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Максимальный объем физической памяти (в байтах), используемой связанным процессом:</div>
            <div class="dx-field-value">
              {{this.resultData.PeakWorkingSet64}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Имя файла:</div>
            <div class="dx-field-value">
              {{this.resultData.StartInfoFileName}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Дата синхронизации:</div>
            <div class="dx-field-value">
              {{new Date(this.resultData.SyncTime).toDateString() + ' ' + new Date(this.resultData.SyncTime).toTimeString()}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Дата запуска приложения:</div>
            <div class="dx-field-value">
              {{new Date(this.resultData.StartTime).toDateString() + ' ' + new Date(this.resultData.StartTime).toTimeString()}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Объем невыгружаемой системной памяти в байтах, выделенной для связанного процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.NonPagedSystemMemorySize64}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Максимальный объем виртуальной памяти (в байтах), используемой связанным процессом:</div>
            <div class="dx-field-value">
              {{this.resultData.PeakVirtualMemorySize64}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Количество запущенных потоков для обслуживания данного процесса:</div>
            <div class="dx-field-value">
              {{this.resultData.ThreadsCount}}
            </div>
          </div>

          <div class="dx-field">
            <div class="dx-field-label">Дата, когда приложения было считано на устройстве:</div>
            <div class="dx-field-value">
              {{new Date(this.resultData.InsertDate).toDateString() + ' ' + new Date(this.resultData.InsertDate).toTimeString()}}
            </div>
          </div>

        </div>
      </div>
    </div>
        <h2 class="content-block">Информация о сервере</h2>
        <div class="content-block dx-card responsive-paddings">


          <div class="dx-field">
            <div class="dx-field-label">Адрес компьютера с которого собиралась информация:</div>
            <div class="dx-field-value">
              <DxTextBox
                  :disabled="true">{{this.server.BaseAddress}}</DxTextBox>
            </div>
          </div>
    </div>
</template>
<script>
import axios from "axios";

export default {
  components: {
  },
  created() {
    this.getById()
  },
  data() {
    return {
      resultData : {
        MachineName: '',
        PagedMemorySize64: '',
        PriorityBoostEnabled: '',
        WorkingSet64: '',
        BasePriority: '',
        MaxWorkingset: '',
        PagedSystemMemorySize64: '',
        ProcessName: '',
        MainWindowTitle: '',
        HasExited: '',
        MinWorkingSet: '',
        PeakWorkingSet64: '',
        StartInfoFileName: '',
        SyncTime: '',
        StartTime: '',
        NonPagedSystemMemorySize64: '',
        PeakVirtualMemorySize64: '',
        ThreadsCount: '',
        InsertDate: '',
      },
      server : {BaseAddress: ''},
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
      const res = await axios.get("https://localhost:7221/api/ApplicationSystemUI/GetApplicationById/"+ this.$route.query.id);
      this.resultData = {
        MachineName: res.data.machinename,
        PagedMemorySize64: res.data.pagedmemorysize64,
        PriorityBoostEnabled: res.data.priorityboostenabled,
        WorkingSet64: res.data.workingset64,
        BasePriority: res.data.basepriority,
        MaxWorkingset: res.data.maxworkingset,
        PagedSystemMemorySize64: res.data.pagedsystemmemorysize64,
        ProcessName: res.data.processname,
        MainWindowTitle: res.data.mainwindowtitle,
        HasExited: res.data.hasexited,
        MinWorkingSet: res.data.minworkingset,
        PeakWorkingSet64: res.data.peakworkingset64,
        StartInfoFileName: res.data.startinfofilename,
        SyncTime: res.data.synctime,
        StartTime: res.data.starttime,
        NonPagedSystemMemorySize64: res.data.nonpagedsystemmemorysize64,
        PeakVirtualMemorySize64: res.data.peakvirtualmemorysize64,
        ThreadsCount: res.data.threadscount,
        InsertDate: res.data.insertDate
      }

      this.server = {BaseAddress: res.data.server.baseaddress};
      delete this.resultData['server'];
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

.application-content {

}
</style>
