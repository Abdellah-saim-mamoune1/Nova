import TopNav from '../ClientDashBoardComponents/ContentPage/DashboardSection/TopNav';
import { Routes, Route } from 'react-router-dom';
import { Dashboard } from "../ClientDashBoardComponents/ContentPage/DashboardSection/DashBoard";
import { TransferFund } from "../ClientDashBoardComponents/ContentPage/Others/TransferFund";
import { TransactionHistory } from "../ClientDashBoardComponents/ContentPage/Others/TransactionHistory";
import { Account } from "../ClientDashBoardComponents/ContentPage/Profile/Account";
import { ProfileCards } from "../ClientDashBoardComponents/ContentPage/Others/ProfileCards";
import { GetHelp } from "../ClientDashBoardComponents/ContentPage/Others/GetHelp";
import { useAppSelector } from "../features/Slices/hooks";
import { useAppDispatch } from "../features/Slices/hooks";
import { useEffect} from "react";
import { fetchClientInfo } from "../features/Slices/Client_Infos_Slice";
import { MainBankRoutes } from './MainBankRoutes';
import { Navigate } from "react-router-dom";
import { Notifications } from "../ClientDashBoardComponents/ContentPage/NotificationsSection/Notifications";
import A from "../ClientDashBoardComponents/Others/LeftNav";

import { TransfersHistoryByClient } from "../ClientDashBoardComponents/ContentPage/Others/TransfersHistory";
import { CircularProgress } from "@mui/material";
export function Container() {
  const dispatch = useAppDispatch();
  const IsLoggedIn =useAppSelector((state) => state.ClientInfos.IsLoggedIn);
 
  useEffect(() => {
    dispatch(fetchClientInfo());    
  }, []);

   if(IsLoggedIn===null){
     return <div className="w-full h-full flex items-center justify-center"><CircularProgress/></div>
   }

  if (IsLoggedIn===false)
    return <MainBankRoutes />
  
  return (
    <div className="w-full h-full flex flex-row">
     <A/>
      <div className="flex-1 h-full overflow-y-auto flex flex-col">
      <TopNav />
        <div className="flex-1 bg-gray-100 dark:bg-gray-900 p-3">
        
        
           <Routes>
         
           <Route path="/dashboard" element={<Dashboard />} />
           <Route path="/TransfersHistoryByClient" element={ <TransfersHistoryByClient/>} />
           <Route path="/TransactionHistory" element={<TransactionHistory />} />
           <Route path="/transferFund" element={<TransferFund />} />
           <Route path="/Account" element={<Account />} />
           <Route path="/ProfileCards" element={<ProfileCards />} />
           <Route path="/GetHelp" element={<GetHelp />} />
           <Route path="/Notifications" element={<Notifications />} />
           <Route path="*" element={<Navigate to="/dashboard" />} />  
         </Routes>
          
        </div>
      </div>
    </div>
  );
}
