import { useState } from "react";
import { TransactionsTable } from "../Others/Table";
import { TransactionsChart } from "./Transactionschart";
import { useAppSelector } from "../../../features/Slices/hooks";
import AccountBalancePieChart from "./AnimatedProgressProvider";
import { LoadingCircle } from "../../../SharedComponents/LoadingCircle";

export function Dashboard() {

  const clientInfo = useAppSelector((state) => state.ClientInfos.client_informations);
  const Accounts = useAppSelector((state) => state.ClientInfos.Accounts);

  const clientName = `${clientInfo?.firstName ?? ""} ${clientInfo?.lastName ?? ""}`.trim();

  const [currentPage, setCurrentPage] = useState(1);
  const accountsPerPage = 1;

  if (!clientInfo || !Accounts) {
    return <LoadingCircle />;
  }

  const totalPages = Math.ceil(Accounts.length / accountsPerPage);
  const indexOfLastAccount = currentPage * accountsPerPage;
  const indexOfFirstAccount = indexOfLastAccount - accountsPerPage;
  const currentAccounts = Accounts.slice(indexOfFirstAccount, indexOfLastAccount);
  let TotalBalance=0;
   Accounts.forEach(acc=>TotalBalance+=acc.balance);

return(
    <div className="dark:bg-gray-900 gap-4 dark:text-gray-100 flex flex-col justify-start items-center w-full h-full">
      {/* Welcome */}
      <div className="p-4 dark:bg-gray-800 rounded-lg bg-white w-full shadow-md">
        <h1 className="text-2xl mb-1">
          Welcome <span className="text-blue-600">{clientName}</span>
        </h1>
        <p>Access and manage your accounts, cards efficiently.</p>
      </div>

      {/* Charts & Cards */}
      <div className="w-full min-h-60 h-auto flex flex-col md:flex-row gap-4">
        {/* Balance Pie Chart */}
        <div className="flex dark:bg-gray-800 flex-col rounded-xl bg-white w-full md:w-[60%] shadow-lg p-4">
          <h2 className="text-xl text-center mb-4 font-semibold">Your Total Current Balance : {TotalBalance} DA</h2>
          <div className="flex justify-center">
            <AccountBalancePieChart />
          </div>
        </div>

        {/* Cards Section */}
        <div className="flex-1 bg-white dark:bg-gray-800 py-4 px-6 text-center flex flex-col items-center min-h-[200px] rounded-xl shadow-lg">
          <div className="w-full flex justify-between items-center mb-2">
            <h4 className="text-xl">Your Cards</h4>
            <p className="text-xl font-bold">Total: {Accounts.length}</p>
          </div>

          {currentAccounts.map((account) => (
           <div
  key={account.id}
  className="relative w-full max-w-[350px] min-w-[250px] h-[200px] bg-gradient-to-r from-blue-600 to-cyan-500 rounded-2xl text-white shadow-xl p-5 flex flex-col justify-between mb-4"
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
      <p className="font-semibold">{clientName}</p>
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

          {/* Pagination Controls */}
          <div className="flex justify-center gap-4 mt-4">
            <button
              onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
              disabled={currentPage === 1}
              className="px-3 py-1 bg-gray-300 dark:bg-gray-700 text-black dark:text-white rounded disabled:opacity-50"
            >
              Previous
            </button>
            <span className="text-sm text-gray-700 dark:text-gray-300">
              Page {currentPage} of {totalPages}
            </span>
            <button
              onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))}
              disabled={currentPage === totalPages}
              className="px-3 py-1 bg-gray-300 dark:bg-gray-700 text-black dark:text-white rounded disabled:opacity-50"
            >
              Next
            </button>
          </div>
        </div>
      </div>

      {/* Table & Chart */}
      <TransactionsTable />
      <TransactionsChart />
    </div>
  );
}
