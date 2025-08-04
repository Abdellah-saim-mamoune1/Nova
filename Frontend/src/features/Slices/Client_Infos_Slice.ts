import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { IAccount, ITransferData } from '../../ClientDashBoardComponents/Others/ClientInterfaces';
import { InfosState } from '../../ClientDashBoardComponents/Others/ClientInterfaces';
import { ClientInfo } from '../../ClientDashBoardComponents/Others/ClientInterfaces';
export const fetchClientInfo = createAsyncThunk(
  'ClientInfos/fetchClientInfo',
  async (_,thunkAPI) => {
   
    try {
      const response = await axios.get('https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/manage/client-info',{withCredentials:true});
   
    thunkAPI.dispatch(SetIsLoggedIn(true));
    thunkAPI.dispatch(fetchClientAccounts());
      return response.data; 

    } catch (error) {
       thunkAPI.dispatch(SetIsLoggedIn(false));
      return null; 
    }
  }
);

export const fetchClientAccounts = createAsyncThunk(
  'ClientInfos/fetchClientAccounts',
  async () => {
    try {
      const response = await axios.get('https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/manage/accounts',{withCredentials:true});
    
      return response.data; 
    } catch (error) {     
      return null; 
    }
  }
);


export async function GetNotificationsPaginated(PageNumber:number,PageSize:number){
 try {
      const response = await axios.get(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/notifications/${PageNumber},${PageSize}`,{withCredentials:true});  
      return response.data; 
    } catch (error) {
     
      return false; 
    }
 }




export const GetNonReadNotificationsCount = createAsyncThunk(
  'ClientInfos/GetNonReadNotificationsCount',
  async () => {
   
   try {
      const response = await axios.get(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/notifications/non-read-notifications-count`,{withCredentials:true});  
      return response.data;
    } catch (error) {
      return null; 
    }
  }
);

export async function TransactionsHistoryPaginatedGet(PageNumber:number,PageSize:number){
  try{
 const response=await axios.get(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/transactions/transactions-history/${PageNumber},${PageSize}`
  ,{withCredentials:true}
 );

 return response.data;
  }
  catch(err){
    return false;
  }
}

export async function TransfersHistoryPaginatedGet(PageNumber:number,PageSize:number){
  try{
 const response=await axios.get(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/transactions/transfers-history/${PageNumber},${PageSize}`
  ,{withCredentials:true}
 );

 return response.data;
  }
  catch(err){
    return false;
  }
}

export function getCookie(name: string) {
  const match = document.cookie.match(new RegExp("(^| )" + name + "=([^;]+)"));
  return match ? match[2] : null;
}


export async function TransferFundAPI(data:ITransferData){
  try{
  const response=await axios.put(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/transactions/transfer-fund/`,data
  ,{
    headers:{
      CSRF:getCookie("CSRF")
    },
    withCredentials:true}
 );

 return response.data;
  }
  catch(err){
    console.log(err);
    return false;
  }
}



export async function LastMonthTransactionsHistoryGet(){
  try{
 const response=await axios.get(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/transactions/last-month-transactions-history`
  ,{withCredentials:true}
 );

 return response.data;
  }
  catch(err){
    return false;
  }
}


const initialState: InfosState = {
  client_informations: null,
  Accounts:null,
  IsLoggedIn:null,
  hasFetched:false, 
  NonReadNotificationsCount:null,
  Account: null,
};


const usersSlice = createSlice({
  name: 'ClientInfos',
  initialState,
  reducers: {
    SetIsLoggedIn: (state, action: PayloadAction<boolean>) => {
      state.IsLoggedIn = action.payload;
    },
    SetAccount: (state, action: PayloadAction<string>) => {
      state.Account = action.payload;
    },
   
  },
  extraReducers: (builder) => {
    builder.addCase(fetchClientInfo.fulfilled, (state, action: PayloadAction<ClientInfo | null>) => {
      state.client_informations = action.payload; 
   
      state.hasFetched = true;
    })
    .addCase(fetchClientAccounts.fulfilled, (state, action: PayloadAction<IAccount[] | null>) => {
      state.Accounts= action.payload;  
      state.hasFetched = true;
    })

    .addCase(GetNonReadNotificationsCount.fulfilled, (state, action: PayloadAction<number | null>) => {
      state.NonReadNotificationsCount= action.payload;  
      state.hasFetched = true;
    })
    
    ;
  },

  
  

});


export const { SetIsLoggedIn, SetAccount} = usersSlice.actions;

export default usersSlice.reducer;
