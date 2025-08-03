import { CircularProgress } from "@mui/material";
import { useAppSelector } from "../../../features/Slices/hooks";


export function ProfileCards() {
  const Accounts = useAppSelector((state) => state.ClientInfos.Accounts);
  const ClientName= useAppSelector((state) => state.ClientInfos.client_informations?.firstName+" "+
                                       state.ClientInfos.client_informations?.lastName);
if (!Accounts) {
    return <div className="w-full h-full flex items-center justify-center dark:bg-gray-900"> <CircularProgress /></div>;
  }
 

  return (
    <div className="w-full dark:bg-gray-900 min-h-screen grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 gap-6 px-1 py-2 sm:px-4 sm:py-4">
      {Accounts.map((account) => (
        <div
  key={account.id}
  className="relative w-full max-w-[350px] h-[200px] bg-gradient-to-r from-blue-600 to-cyan-500 rounded-2xl text-white shadow-xl p-5 flex flex-col justify-between mb-4"
>
  {/* Top Row: Chip + Balance */}
  <div className="flex justify-between items-center">
    {/* Simulated Chip */}
    <div className="w-10 h-7 bg-yellow-300 rounded-sm opacity-90 shadow-md" />
    <div className="text-right">
      <p className="text-sm uppercase tracking-wide text-white/70">Balance</p>
      <p className="text-lg font-semibold">{account.balance.toLocaleString()} DA</p>
    </div>
  </div>

  {/* Card Number */}
  <div className="text-center tracking-widest text-xl font-semibold mt-4">
    **** **** **** {account.card.cardNumber.toString().slice(-4)}
  </div>

  {/* Bottom Row: Name + Expiry + Status */}
  <div className="flex justify-between items-end text-sm mt-4">
    <div>
      <p className="uppercase text-white/70 text-xs">Card Holder</p>
      <p className="font-semibold">{ClientName}</p>
    </div>
    <div>
      <p className="uppercase text-white/70 text-xs">Expires</p>
      <p className="font-semibold">{account.card.expirationDate}</p>
    </div>
    <div className="text-right">
      <p className="uppercase text-white/70 text-xs">Status</p>
      <p
        className={`font-semibold ${
          account.isFrozen ? "text-red-200" : "text-green-200"
        }`}
      >
        {account.isFrozen ? "Frozen" : "Active"}
      </p>
    </div>
  </div>
</div>
      ))}
    </div>
  );
}
