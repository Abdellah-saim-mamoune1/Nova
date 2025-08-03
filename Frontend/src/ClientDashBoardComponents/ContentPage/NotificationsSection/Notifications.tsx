import { useEffect, useState } from "react";
import { useAppDispatch } from "../../../features/Slices/hooks";
import {
  GetNonReadNotificationsCount,
  GetNotificationsPaginated,
} from "../../../features/Slices/Client_Infos_Slice";

import {
  ArrowDownCircle,
  ArrowUpCircle,
  Send,
  Mail,
  MessageCircleWarning,
} from "lucide-react";
import {
  IClientNotificationsGet,
  IPaginatedNotifications,
} from "../../Others/ClientInterfaces";
import { UpdateIsClientNotificationViewed } from "../../Others/APIs";

export function Notifications() {
 
  const dispatch=useAppDispatch();
  const [notifications, setnotifications] = useState<IPaginatedNotifications | null>(null);
  const [unreadcount, setunreadcount] = useState<number>(0);
  const [PageNumber, SetPageNumber] = useState<number>(1);
  const PageSize = 5;
  const [selected, setSelected] = useState<IClientNotificationsGet | null>(null);

  // Fetch notifications
  useEffect(() => {
    async function Get() {
      const response = await GetNotificationsPaginated(PageNumber, PageSize);
      if (response !== false) setnotifications(response);
    }
    Get();
  }, [PageNumber]);

  
  useEffect(() => {
    if (notifications) {
      const count = notifications.notifications.filter((msg) => !msg.isViewed).length;
      setunreadcount(count);
    }
  }, [notifications]);

  
  const markAsRead = async (id: number) => {
   
    await UpdateIsClientNotificationViewed(id);
    await dispatch(GetNonReadNotificationsCount());
    

    setnotifications((prev) => {
      if (!prev) return prev;
      return {
        ...prev,
        notifications: prev.notifications.map((msg) =>
          msg.id === id ? { ...msg, isViewed: true } : msg
        ),
      };
    });
  };

  if (!notifications) {
    return <div className="h-full flex-1 bg-white dark:bg-gray-900"></div>;
  }


  
  return (
    <div className="flex-1 dark:bg-gray-900 flex flex-col h-full">
      <div className="h-full shadow-lg w-full rounded-xl overflow-hidden flex flex-col">
        {/* Header */}
        <div className="text-center dark:bg-gray-800 dark:text-white font-semibold sm:text-xl py-4 bg-white border-b dark:border-gray-500 border-gray-600 w-full rounded-t-xl">
          <h4>
            Bank Notifications{" "}
            {unreadcount > 0 && (
              <span className="text-red-500">({unreadcount} unread)</span>
            )}
          </h4>
        </div>

        {/* Main Content */}
        <div className="flex flex-1 bg-white dark:bg-gray-800 text-gray-800 dark:text-white overflow-hidden">
          {/* Sidebar - Message List */}
          <aside
            className={`
              ${selected ? "hidden" : "block"} sm:block flex-1 
              border-r border-gray-600 dark:border-gray-500
            `}
          
          >
            <div className="px-6 py-4 border-b dark:border-gray-500 border-gray-600 text-lg font-bold sticky top-0 bg-white dark:bg-gray-800 z-10">
              📩 Inbox
            </div>

            {notifications.notifications.length === 0 ? (
              <div className="p-6 dark:text-gray-100 text-center text-gray-400">
                No messages
              </div>
            ) : (
              <>
              
              
                <div className="flex flex-col overflow-y-auto w-full max-h-[600px] custom-scrollbar">
               {notifications.notifications.map((msg) => (
                
                <div
        key={msg.id}
        onClick={() => {
          setSelected(msg);
          if (!msg.isViewed) markAsRead(msg.id);
        }}
        className={`
          px-6 py-4 cursor-pointer border-b dark:border-gray-500 border-gray-600
          hover:bg-gray-100 dark:hover:bg-gray-700 transition-all duration-200
          ${
            selected?.id === msg.id
              ? "bg-cyan-100 dark:bg-cyan-900"
              : !msg.isViewed
              ? "bg-gray-100 dark:bg-gray-700 font-semibold"
              : "bg-white dark:bg-gray-800 text-gray-500 dark:text-gray-300"
          }
        `}
      >
        <div className="flex items-center gap-2">
          {msg.type === "Withdraw" && (
            <div className="text-red-600">
              <ArrowDownCircle size={22} />
            </div>
          )}
          {msg.type === "Deposite" && (
            <div className="text-green-600">
              <ArrowUpCircle size={22} />
            </div>
          )}
          {msg.type === "Transfer Fund" && (
            <div className="text-cyan-600">
              <Send size={22} />
            </div>
          )}
          {msg.type === "Message" && (
            <div className="text-gray-400">
              <Mail size={22} />
            </div>
          )}
          {msg.type === "Warning" && (
            <div>
              <MessageCircleWarning size={22} />
            </div>
          )}
          <div className="text-sm truncate">{msg.title}</div>
        </div>
        <div className="text-xs text-right mt-1">{msg.date}</div>
      </div>
              
                 ))}
                 </div>


                {/* Pagination Controls */}
                <div className="flex justify-between items-center px-4 py-2 border-t dark:border-gray-500 border-gray-600">
                  <button
                    className="text-sm text-teal-600 disabled:opacity-50"
                    disabled={PageNumber === 1}
                    onClick={() => SetPageNumber((p) => p - 1)}
                  >
                    Previous
                  </button>
                  <span className="text-sm text-gray-600 dark:text-gray-300">
                    Page {PageNumber}
                  </span>
                  <button
                    className="text-sm text-teal-600 disabled:opacity-50"
                    disabled={notifications.notifications.length < PageSize}
                    onClick={() => SetPageNumber((p) => p + 1)}
                  >
                    Next
                  </button>
                </div>
              </>
            )}
          </aside>

          {/* Message Detail */}
         
          <main
            className={`
              ${selected ? "block" : "hidden"} sm:flex
              flex-1 flex-col
              bg-gray-50 dark:bg-gray-800 p-8 overflow-y-auto
            `}
          >
            {selected && (
              <button
                onClick={() => setSelected(null)}
                className="sm:hidden mb-4 text-teal-600"
              >
                ← Back to Inbox
              </button>
            )}

            {selected ? (
              <div className="space-y-4">
                <div className="text-xl font-bold text-teal-700 dark:text-cyan-300">
                  {selected.title}
                </div>
                <div className="text-sm text-gray-500 dark:text-gray-400">
                  {selected.date}
                </div>
                <pre className="whitespace-pre-wrap text-base text-gray-700 dark:text-gray-200">
                  {selected.notification}
                </pre>
              </div>
            ) : (
              <div className="m-auto text-center text-gray-400 dark:text-gray-300 text-sm">
                Select a message to view its content
              </div>
            )}
          </main>
        </div>
      </div>
    </div>
  );
}
