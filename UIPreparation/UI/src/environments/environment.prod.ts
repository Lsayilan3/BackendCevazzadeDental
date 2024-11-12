export const environment = {
  production: true,
  getApiUrl: "https://api.cevatzadedental.com/api",
  getApiUrlPhoto: "https://api.cevatzadedental.com",
  // getApiUrl: "https://localhost:44375/WebAPI/swagger",
  // getApiUrlPhoto: "https://api.cevatzadedental.com",
  getDropDownSetting: {
    singleSelection: false,
    idField: 'id',
    textField: 'label',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowSearchFilter: true
  },
  getDatatableSettings:  {
    pagingType: 'full_numbers',
    pageLength: 2
  }
};
