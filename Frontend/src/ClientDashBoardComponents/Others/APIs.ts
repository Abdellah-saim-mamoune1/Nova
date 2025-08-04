
import { AddGetHelpRequist } from "./ClientInterfaces";
import axios from "axios";
export async function  AddNewGetHelpRequist(v: AddGetHelpRequist){
  try{
 await axios.post("https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/manage/add-get-help-request",v,{
    withCredentials:true});
   return true;
  } catch(error){
   return false;
  }
}



export async function UpdateIsClientNotificationViewed(Id:number){
 try{
 const res=await axios.put(`https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/notifications/mark-as-viewed/${Id}`,{},{withCredentials:true});
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
    await axios.put("https://novaservice-ahh3dnhqcecyetds.spaincentral-01.azurewebsites.net/api/client/manage",v,{withCredentials:true})
    return true;
}
    catch(err){
     
    return false;
    }
   }
   
