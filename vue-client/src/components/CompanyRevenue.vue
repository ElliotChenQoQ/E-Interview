<template>
  <div>
    <el-card class="mb-4">
      <h1>公司營業收入查詢系統</h1>
      
      <div class="mb-4 d-flex">
        <el-button type="primary" icon="Refresh" @click="syncData" class="mr-3">同步政府資料</el-button>
        <el-form :inline="true" class="mr-3">
          <el-form-item label="公司代號查詢">
            <el-input v-model="companyCode" placeholder="輸入公司代號" />
          </el-form-item>
        </el-form>
        <el-button type="primary" @click="fetchCompanyData">查詢</el-button>
      </div>
    </el-card>

    <el-card v-if="companyData" class="result-card">
      <h2>營業收入</h2>
      <el-table :data="paginatedData" class="mt-4" stripe style="overflow-x: auto;">
        <el-table-column prop="reportDate" label="出表日期" :width="110" sortable />
        <el-table-column prop="yearMonth" label="資料年月" :width="110" sortable />
        <el-table-column prop="companyID" label="公司代號" :width="110" sortable />
        <el-table-column prop="companyName" label="公司名稱" />
        <el-table-column prop="industry" label="產業別" />
        <el-table-column prop="currentMonthRevenue" label="當月營業收入" :width="100" sortable />
        <el-table-column prop="lastMonthRevenue" label="上月營業收入" :width="120" sortable />
        <el-table-column prop="lastYearRevenue" label="去年當月營收" sortable />
        <el-table-column prop="monthGrowth" label="上月比較增減 (%)" :width="120" sortable />
        <el-table-column prop="yearGrowth" label="去年同月增減 (%)" :width="120" sortable />
        <el-table-column prop="cumulativeMonthRevenue" label="累計本月營業收入" :width="100" sortable />
        <el-table-column prop="cumulativeLastYearRevenue" label="累計去年同期營業收入" :width="120" sortable />
        <el-table-column prop="cumulativeGrowth" label="累計成長率 (%)" :width="120" sortable />
        <el-table-column prop="remarks" label="備註" :width="200" show-overflow-tooltip />
      </el-table>
      <el-pagination
        v-if="companyData.records.length > pageSize"
        class="mt-4"
        background
        layout="prev, pager, next"
        :total="companyData.records.length"
        :page-size="pageSize"
        v-model:currentPage="currentPage"
        @current-change="handlePageChange"
        />
      <p class="total-records">總資料筆數: {{ totalRecords }}</p>
    </el-card>
  </div>
</template>

<script>
import { ref, computed, onMounted } from 'vue';
import { ElButton, ElInput, ElForm, ElFormItem, ElTable, ElTableColumn, ElCard, ElPagination, ElMessage } from 'element-plus';
import 'element-plus/dist/index.css';
import apiService from '../services/apiService.ts';

export default {
  components: {
    ElButton,
    ElInput,
    ElForm,
    ElFormItem,
    ElTable,
    ElTableColumn,
    ElCard,
    ElPagination,
  },
  setup() {
    const companyCode = ref('');
    const companyData = ref(null);
    const loading = ref(false);
    const currentPage = ref(1);
    const pageSize = 10;

    const totalRecords = computed(() => companyData.value?.records.length || 0);
    
    const paginatedData = computed(() => {
      const start = (currentPage.value - 1) * pageSize;
      return companyData.value ? companyData.value.records.slice(start, start + pageSize) : [];
    });

    const syncData = async () => {
      loading.value = true;
      try {
        await apiService.syncGovernmentData();
        ElMessage.success('資料同步成功完成。');
      } catch (error) {
        ElMessage.error('同步失敗: ' + error.message);
      } finally {
        loading.value = false;
      }
    };

    const fetchCompanyData = async () => {
      loading.value = true;
      try {
        const response = companyCode.value
          ? await apiService.getCompanyRevenue(companyCode.value)
          : await apiService.getAllCompanyRevenue();
        companyData.value = { records: response };
      } catch (error) {
        ElMessage.error('查詢失敗: ' + error.message);
      } finally {
        loading.value = false;
      }
    };

    const handlePageChange = (page) => (currentPage.value = page);

    onMounted(() => fetchCompanyData());

    return {
      companyCode,
      companyData,
      totalRecords,
      syncData,
      fetchCompanyData,
      loading,
      currentPage,
      pageSize,
      handlePageChange,
      paginatedData,
    };
  },
};
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Noto+Sans+TC:wght@400;700&display=swap');

body {
  font-family: 'Noto Sans TC', sans-serif;
}

.mb-4 {
  margin-bottom: 16px;
}
.mt-4 {
  margin-top: 16px;
}
.d-flex {
  display: flex;
}
.align-items-center {
  align-items: center;
}
.justify-content-center {
  justify-content: center;
}
.mr-3 {
  margin-right: 12px;
}
.result-card {
  padding: 20px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
}
.total-records {
  margin-top: 16px;
  font-weight: bold;
}
</style>
