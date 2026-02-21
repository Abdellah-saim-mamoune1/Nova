
import { AddGetHelpRequist } from "./ClientInterfaces";
import axios from "axios";
export async function  AddNewGetHelpRequist(v: AddGetHelpRequist){
  try{
 await axios.post("http://localhost:8101/api/client/manage/add-get-help-request",v,{
    withCredentials:true});
   return true;
  } catch(error){
   return false;
  }
}



export async function UpdateIsClientNotificationViewed(Id:number){
 try{
 const res=await axios.put(`http://localhost:8101/api/client/notifications/mark-as-viewed/${Id}`,{},{withCredentials:true});
 if(res.status===200)
  return true;
return false;
  }
  catch(err){
    return false;
  }
}




export async function  UpdateClientPersonalInfos(v:any){
  try{
    await axios.put("http://localhost:8101/api/client/manage",v,{withCredentials:true})
    return true;
}
    catch(err){
     
    return false;
    }
   }
   
