import { useEffect, useState } from "react";
import { ITransactionsHistoryPaginated} from "../../Others/ClientInterfaces";
import { TransactionsHistoryPaginatedGet } from "../../../features/Slices/Client_Infos_Slice";
export function TransactionsTable_Pagination() {

const [Transactions,SetTransactions]=useState<ITransactionsHistoryPaginated|null>(null);
const[PageNumber,SetPageNumber]=useState<number>(1);
const PageSize=6

useEffect(()=>{

  async function Set(){
   const data=await TransactionsHistoryPaginatedGet(PageNumber,PageSize);
   if(data!==false)
   SetTransactions(data);

  }

  Set();

},[PageNumber])


  if(Transactions===null||Transactions.transactions.length===0){
    return( <div className="w-full dark:text-gray-100 dark:bg-gray-800 mx-auto bg-white p-4 px-7 rounded-lg shadow-md">
      <h2 className="text-xl font-semibold mb-4">Transactions History</h2>
      
      <div className="overflow-x-auto">
        <table className="min-w-full dark:border-gray-600 table-auto border-collapse border border-gray-300">
          <thead>
            <tr className="dark:bg-gray-900 dark:text-blue-400 dark:border-b-gray-600  text-gray-700 border-b border-gray-300 bg-gray-200">
              <th className=" px-4 py-2 text-left">Id</th>
              <th className=" px-4 py-2 text-left">Amount</th>
              <th className=" px-4 py-2 text-left">Type</th>          
              <th className=" px-4 py-2 text-left">Date</th>
           
            </tr>
          </thead>
        </table>
      </div>
    </div>);
  }


 
  const handleChangePage = (direction: "prev" | "next") => {
    if (direction === "prev" && PageNumber > 0) SetPageNumber(PageNumber-1);
    if (direction === "next" && (PageNumber + 1) <=Transactions.totalPages) SetPageNumber(PageNumber + 1);


  };

  const paginatedData = Transactions.transactions;

  return (
    <div className="w-full dark:text-gray-100 dark:bg-gray-800 mx-auto bg-white p-4 px-7 rounded-lg shadow-md">
      <h2 className="text-xl font-semibold mb-4">Transactions History</h2>
      
      <div className="overflow-x-auto">
        <table className="min-w-full dark:border-gray-600 table-auto border-collapse border border-gray-300">
          <thead>
            <tr className="dark:bg-gray-900 dark:text-blue-400 dark:border-b-gray-600  text-gray-700 border-b border-gray-300 bg-gray-200">
              <th className=" px-4 py-2 text-left">N</th>
              <th className=" px-4 py-2 text-left">Amount</th>
              <th className=" px-4 py-2 text-left">Type</th>
              <th className=" px-4 py-2 text-left">Date</th>
           
            </tr>
          </thead>
          <tbody>
            {Transactions&&paginatedData.map((transaction) => (
              <tr key={transaction.id} className="hover:bg-gray-100 dark:hover:bg-gray-700 transition-all">
                  <td className="px-4 py-2">{transaction.id}</td>
                <td className="px-4 py-2">{transaction.amount}DA</td>
                <td className="p-3">{transaction.type}</td>
                <td className="p-3">{transaction.createdAt.toString()}</td>
              
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Simple Pagination Buttons */}
      {Transactions.transactions.length!==0&&<div className="flex justify-end mt-4 space-x-2">
        <button
          onClick={() => handleChangePage("prev")}
          disabled={PageNumber === 1}
          className="px-4 py-1 cursor-pointer dark:text-black bg-gray-300 rounded disabled:opacity-50"
        >
          Previous
        </button>
        <p className="px-2 text-lg">{PageNumber}/{Transactions.totalPages}</p>
        <button
          onClick={() => handleChangePage("next")}
          disabled={(PageNumber) >= Transactions.totalPages}
          className="px-4 py-1 cursor-pointer dark:text-black bg-gray-300 rounded disabled:opacity-50"
        >
          Next
        </button>
      </div>}
    </div>
  );
}
