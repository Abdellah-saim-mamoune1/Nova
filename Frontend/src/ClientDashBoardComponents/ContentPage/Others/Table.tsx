import { useEffect, useState } from "react";
import { ITransactionsHistoryPaginated } from "../../Others/ClientInterfaces";
import { TransactionsHistoryPaginatedGet } from "../../../features/Slices/Client_Infos_Slice";

export function TransactionsTable( ) {
  const [transactions,SetTransactions] = useState<ITransactionsHistoryPaginated|null>(null);
  
useEffect(()=>{

  async function Get(){
const data=await TransactionsHistoryPaginatedGet(1,5);
if(data!=false)
  SetTransactions(data);

}
Get();

},[])


if(transactions===null||transactions.transactions.length===0)
  return;
  
  return (
   
    <div className="w-full dark:bg-gray-800  mx-auto bg-white p-4 px-7 rounded-lg shadow-md">
      <h2 className="text-xl font-semibold mb-4">Recent Transactions</h2>
     
      <div className="overflow-x-auto">
        <table className="min-w-full border-1 dark:border-gray-700 border-gray-300 table-auto border-collapse">
          <thead>
            <tr className="bg-gray-200 border-b-1 border-b-gray-300 text-gray-700 dark:border-gray-700 dark:bg-gray-900 dark:text-blue-400 text-left">
              <th className="p-3 font-bold">N</th>
              <th className="p-3 font-bold">Amount</th>
              <th className="p-3 font-bold">Type</th>
              <th className="p-3 font-bold">Date</th>
            </tr>
          </thead>
          <tbody >
            {transactions&&transactions.transactions.map((transaction) => (
              <tr
                key={transaction.id}
                className="hover:bg-gray-100 dark:hover:bg-gray-700 transition-all"
              >
                <td className="p-3">{transaction.id}</td>
                <td className="p-3 ">{transaction.amount}</td>
                <td className="p-3">{transaction.type}</td>
                <td className="p-3">{transaction.createdAt.toString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
