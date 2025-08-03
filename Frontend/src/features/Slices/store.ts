import { configureStore } from '@reduxjs/toolkit';
import ClientInfosReducer from './Client_Infos_Slice';
import LeftNavReducer from './LeftNavSlice';
import TopNavReducer from './TopNavSlice';
import ManageClientsReducer from './ManageCientsSlice';
const store = configureStore({
  reducer: {
   
    ClientInfos: ClientInfosReducer,
    LeftNav: LeftNavReducer,
    TopNav: TopNavReducer,
    ManageClients: ManageClientsReducer,
  }
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;