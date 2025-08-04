import { useEffect, useState } from "react";
import { Line } from "react-chartjs-2";
import { ITransactionsHistory } from "../../Others/ClientInterfaces";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { LastMonthTransactionsHistoryGet } from "../../../features/Slices/Client_Infos_Slice";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

export function TransactionsChart() {
  const [transactions, setTransactions] = useState<ITransactionsHistory[]>([]);
  const [isDarkMode, setIsDarkMode] = useState<boolean>(false);

  useEffect(() => {
    const fetchTransactions = async () => {
      const data = await LastMonthTransactionsHistoryGet();
      if (data !== false) setTransactions(data);
    };

    fetchTransactions();
  }, []);

  useEffect(() => {
    const updateDarkMode = () => {
      setIsDarkMode(document.documentElement.classList.contains("dark"));
    };

    updateDarkMode(); // Run on mount

    const observer = new MutationObserver(updateDarkMode);
    observer.observe(document.documentElement, {
      attributes: true,
      attributeFilter: ["class"],
    });

    const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");
    mediaQuery.addEventListener("change", updateDarkMode);

    return () => {
      observer.disconnect();
      mediaQuery.removeEventListener("change", updateDarkMode);
    };
  }, []);

  if (transactions.length === 0) return null;

  const textColor = isDarkMode ? "white" : "black";
  const gridColor = isDarkMode ? "#ffffff33" : "#00000022";
  const tooltipBackgroundColor = isDarkMode ? "black" : "#F3F4F6";
  const tooltipTitleColor = isDarkMode ? "white" : "black";
  const tooltipBodyColor = isDarkMode ? "white" : "black";

  const depositData = transactions.filter((t) => t.type === "Deposit");
  const withdrawData = transactions.filter((t) => t.type === "Withdraw");

  const sortedTransactions = [...transactions].sort(
    (a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
  );

  const formattedLabels = Array.from(
    new Set(
      sortedTransactions.map((t) =>
        new Date(t.createdAt).toISOString().split("T")[0]
      )
    )
  );

  const depositAmounts = formattedLabels.map((date) => {
    return depositData
      .filter((t) => new Date(t.createdAt).toISOString().split("T")[0] === date)
      .reduce((sum, t) => sum + t.amount, 0);
  });

  const withdrawAmounts = formattedLabels.map((date) => {
    return withdrawData
      .filter((t) => new Date(t.createdAt).toISOString().split("T")[0] === date)
      .reduce((sum, t) => sum + t.amount, 0);
  });

  const maxAmount = Math.max(
    Math.max(...depositAmounts),
    Math.max(...withdrawAmounts)
  );

  const data = {
    labels: formattedLabels,
    datasets: [
      {
        label: "Deposits",
        data: depositAmounts,
        borderColor: "green",
        backgroundColor: "rgba(0, 255, 0, 0.2)",
        tension: 0.3,
      },
      {
        label: "Withdrawals",
        data: withdrawAmounts,
        borderColor: "red",
        backgroundColor: "rgba(255, 0, 0, 0.2)",
        tension: 0.3,
      },
    ],
  };

  const options = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        labels: {
          color: textColor,
        },
      },
      tooltip: {
        backgroundColor: tooltipBackgroundColor,
        titleColor: tooltipTitleColor,
        bodyColor: tooltipBodyColor,
      },
    },
    scales: {
      y: {
        beginAtZero: true,
        max: maxAmount + 100,
        ticks: {
          stepSize: Math.ceil(maxAmount / 10),
          color: textColor,
        },
        grid: {
          color: gridColor,
        },
      },
      x: {
        ticks: {
          maxTicksLimit: 10,
          color: textColor,
        },
        grid: {
          color: gridColor,
        },
      },
    },
  };

  return (
    <div className="w-full dark:bg-gray-800 p-5 bg-white rounded-lg max-w-full">
      <h2 className={`text-xl font-bold mb-3 ${isDarkMode ? "text-white" : "text-black"}`}>
        Deposit & Withdraw Trends
      </h2>
      <div className="overflow-x-auto">
        <div className="w-full h-[300px] sm:h-[400px] md:h-[450px] lg:h-[500px]">
          <Line data={data} options={options} />
        </div>
      </div>
    </div>
  );
}
