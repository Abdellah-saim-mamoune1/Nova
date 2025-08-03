import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";
import { useAppSelector } from "../../../features/Slices/hooks";

const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042", "#AA66CC", "#FF66A1"];

export default function AccountBalancePieChart() {
  
   const Accounts = useAppSelector((state) =>state.ClientInfos.Accounts);
  if(!Accounts||Accounts.length===0)
   return null;
 
 const accountBalances = Accounts.map((email) => email.balance)
  console.log(accountBalances);
  
  const totalBalance = accountBalances.reduce((acc: any, val: any) => acc + val, 0);

  const data = Accounts.map((account) => ({
    name: account.account,
    value: account.balance,
  }));

  return (
    <div className="w-full flex justify-center">
      <div className="w-full 2xl:text-xl text-sm sm:w-[334px] h-[240px] sm:h-[279px]">
        <ResponsiveContainer width="100%" height="100%">
          <PieChart>
            <Pie
              data={data}
              cx="50%"
              cy="50%"
              outerRadius="65%"
              dataKey="value"
              labelLine={false}
              label={({ value }) => {
                const percent = totalBalance ? ((value / totalBalance) * 100).toFixed(1) : "0";
                return `${percent}%`;
              }}
            >
              {data.map((_: any, index: number) => (
                <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
              ))}
            </Pie>
            <Tooltip formatter={(value: number) => `${value.toLocaleString()} DA`} />
            <Legend />
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}
