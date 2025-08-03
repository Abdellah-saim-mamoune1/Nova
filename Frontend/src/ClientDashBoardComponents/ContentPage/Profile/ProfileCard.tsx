import {useState } from "react";
import {
  Email,
  Phone,
  DateRange,
  LocationOn,
  Male,
  Female
} from "@mui/icons-material";
import { Edit } from "lucide-react";
import { useAppSelector } from "../../../features/Slices/hooks";
import { EditClientInfos } from "./EditClientInfos";

export function ProfileCard() {
  const [showEditInfo, setShowEditInfo] = useState(false);
  const [showErrorCard, setShowErrorCard] = useState(false);
  const [notificationVisible, setNotificationVisible] = useState(false);

  const info = useAppSelector((state) => state.ClientInfos.client_informations);
  const card=useAppSelector((state) => state.ClientInfos.Accounts?.find(a=>a.account===state.ClientInfos.Account));

  if(!info)
    return;

  const handleSave = (updatedUser: boolean) => {
    setShowEditInfo(false);
    if (updatedUser) {
      setNotificationVisible(true);
      setTimeout(() => setNotificationVisible(false), 4000);
    } else {
      setShowErrorCard(true);
      setTimeout(() => setShowErrorCard(false), 4000);
    }
  };

  if (showEditInfo) {
    return (
        <EditClientInfos
          userData={info}
          onClose={() => setShowEditInfo(false)}
          onSave={handleSave}
          title={"Account/Edit infos"}
          from="Client"
       />
    )
  }

  return (
    <div className="w-full text-gray-900  ">
      <h1 className="text-2xl dark:text-gray-200 mb-2">Account</h1>
      <div className="p-6 shadow-lg dark:text-gray-100  dark:bg-gray-800 bg-white rounded-lg w-full flex flex-col justify-center">
        <div className="flex flex-col sm:flex-row items-center gap-6 mb-10">
          
          <div className="flex-1 text-center sm:text-left">
            <h2 className="text-2xl sm:text-3xl font-bold mb-2">
              {info.firstName + " " + info.lastName}
            </h2>
            <div className="flex flex-col gap-1 text-sm">
              <div className="flex items-center gap-2">
                {info.gender==="Male"?
                <Male fontSize="small" />:<Female fontSize="small" />}
                <span>{info.gender}</span>
              </div>
              </div>
            <div className="flex flex-col gap-1 text-sm">
              <div className="flex items-center gap-2">
                <Email fontSize="small" />
                <span>{info.personalEmail}</span>
              </div>
              <div className="flex items-center gap-2">
                <Phone fontSize="small" />
                <span>{info.phoneNumber}</span>
              </div>
              <div className="flex items-center gap-2">
                <DateRange fontSize="small" />
                <span>{info.birthDate}</span>
              </div>
              <div className="flex items-center gap-2">
                <LocationOn fontSize="small" />
                <span>{info.address}</span>
              </div>
            </div>
            <button
              onClick={() => setShowEditInfo(true)}
              className="mt-4 px-4 py-2 bg-teal-500 text-white rounded hover:bg-teal-600 flex items-center gap-2 mx-auto sm:mx-0"
            >
              <Edit size={16} /> Edit Info
            </button>
          </div>
        </div>

        <hr className="mb-8 border-gray-300 dark:border-gray-700" />

       <div
  className="relative w-full max-w-[350px] h-[200px] bg-gradient-to-r from-blue-600 to-cyan-500 rounded-2xl text-white shadow-xl p-5 flex flex-col justify-between mb-4"
>
  {/* Top Row: Chip + Balance */}
  <div className="flex justify-between items-center">
    {/* Simulated Chip */}
    <div className="w-10 h-7 bg-yellow-300 rounded-sm opacity-90 shadow-md" />
    <div className="text-right">
      <p className="text-sm uppercase tracking-wide text-white/70">Balance</p>
      <p className="text-lg font-semibold">{card?.balance.toLocaleString()} DA</p>
    </div>
  </div>

  {/* Card Number */}
  <div className="text-center tracking-widest text-xl font-semibold mt-4">
    **** **** **** {card?.card.cardNumber.toString().slice(-4)}
  </div>

  {/* Bottom Row: Name + Expiry + Status */}
  <div className="flex justify-between items-end text-sm mt-4">
    <div>
      <p className="uppercase text-white/70 text-xs">Card Holder</p>
      <p className="font-semibold">{info.firstName+" "+info.lastName}</p>
    </div>
    <div>
      <p className="uppercase text-white/70 text-xs">Expires</p>
      <p className="font-semibold">{card?.card.expirationDate}</p>
    </div>
    <div className="text-right">
      <p className="uppercase text-white/70 text-xs">Status</p>
      <p
        className={`font-semibold ${
           card?.isFrozen ? "text-red-200" : "text-green-200"
        }`}
      >
        {card?.isFrozen ? "Frozen" : "Active"}
      </p>
    </div>
  </div>
</div>

      {/* Notifications */}
      {notificationVisible && (
        <div className="fixed top-[-5px] left-1/2 transform -translate-x-1/2 bg-teal-600 text-white px-6 py-4 rounded-xl shadow-2xl z-[9999] animate-slide-down max-w-[90%] sm:max-w-md">
          <p className="font-semibold">Changing informations process went successfully!</p>
        </div>
      )}
      {showErrorCard && (
        <div className="fixed top-[-5px] left-1/2 transform -translate-x-1/2 bg-red-600 text-white px-6 py-4 rounded-xl shadow-2xl z-[9999] animate-slide-down max-w-[90%] sm:max-w-md">
          <p className="font-semibold">Changing informations process failed!</p>
        </div>
      )}
    </div>
    </div>
  );
  
}
