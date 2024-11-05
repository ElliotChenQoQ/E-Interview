import axios from 'axios';

const apiService = {
  async syncGovernmentData() {
    try {
      const response = await axios.post(`${import.meta.env.VITE_APP_API_BASE_URL}/companies/revenues/sync`);
      return response.data;
    } catch (error) {
      throw new Error('同步政府資料失敗');
    }
  },

  async getCompanyRevenue(companyCode: any) {
    try {
      const response = await axios.get(`${import.meta.env.VITE_APP_API_BASE_URL}/companies/revenues/${companyCode}`);
      return response.data;
    } catch (error) {
      throw new Error('查詢公司營業收入失敗');
    }
  },

  async getAllCompanyRevenue() {
    try {
      const response = await axios.get(`${import.meta.env.VITE_APP_API_BASE_URL}/companies/revenues`);
      return response.data;
    } catch (error) {
      throw new Error('查詢公司營業收入失敗');
    }
  },
};

export default apiService;
