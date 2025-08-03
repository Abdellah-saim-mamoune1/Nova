import { useState, useEffect, useRef } from "react";
import { Menu, Bell, User, LogOut } from "lucide-react";
import { IoPersonCircle, IoSettings } from "react-icons/io5";
import { useAppSelector, useAppDispatch } from "../../../features/Slices/hooks";
import { setLeftNav } from "../../../features/Slices/TopNavSlice";
import { useNavigate } from "react-router-dom";
import { ToggleDarkMode } from "../Others/ToggleDarkMode";
import { GetNonReadNotificationsCount, SetIsLoggedIn } from "../../../features/Slices/Client_Infos_Slice";
import axios from "axios";

export default function TopNav() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const Left_Nav_State = useAppSelector((state) => state.TopNav.Left_Nav_State);
  const NonReadNotificationsCount=useAppSelector(s=>s.ClientInfos.NonReadNotificationsCount);
  const [openDropdown, setOpenDropdown] = useState<"profile" | "settings" | null>(null);
  const dropdownRef = useRef<HTMLDivElement>(null);

  function toggleLeftNav() {
    const state = Left_Nav_State === "open" ? "closed" : "open";
    dispatch(setLeftNav(state));
  }

  function toggleDropdown(type:  "profile" | "settings") {
    setOpenDropdown((prev) => (prev === type ? null : type));
  }

 async function handleLogout() {
    dispatch(SetIsLoggedIn(false));
     await axios.delete("http://localhost:5157/api/authentication/DeleteCookies",{withCredentials:true})
  }

  useEffect(() => {
    async function Get(){
         await dispatch(GetNonReadNotificationsCount());
      }
    Get();

    function handleClickOutside(event: MouseEvent) {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setOpenDropdown(null);
      }
    
    }

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
 
  }, []);

  

  return (
    <div className="dark:bg-gray-800 dark:border-gray-700 flex-shrink-0 h-[63px] bg-white dark:text-gray-100 sticky border-b-1 border-gray-200 top-0 flex items-center justify-between z-20 px-3">
      <button onClick={toggleLeftNav} className="text-2xl cursor-pointer hover:text-cyan-600 text-black dark:text-white rounded">
        <Menu size={35} />
      </button>

      <div className="flex flex-1 justify-end gap-3 items-center h-full relative" ref={dropdownRef}>
        {/* Notifications */}
        <div className="relative">
          <button
            onClick={() =>navigate("Notifications") }
            className="p-2 hover:text-cyan-600 rounded-full cursor-pointer relative"
          >
            <Bell size={24} />
           {typeof NonReadNotificationsCount === 'number' && NonReadNotificationsCount > 0 && (
  <div className="absolute text-[11px] text-center rounded-full bg-red-500 top-0 right-[2px] w-5 h-5 flex items-center justify-center">
    <p className="text-white">{NonReadNotificationsCount > 99 ? "99+" : NonReadNotificationsCount}</p>
  </div>
)}

           </button>

          
        </div>

        {/* Profile */}
        <div className="relative">
          <button
            onClick={() => toggleDropdown("profile")}
            className="hover:text-cyan-600 cursor-pointer rounded-full p-1 flex items-center"
          >
            <IoPersonCircle size={34} />
          </button>

          {openDropdown === "profile" && (
            <div className="absolute p-1 border dark:border-gray-600 border-gray-400 right-0 top-[124%] bg-white dark:bg-gray-800 shadow-lg rounded-lg w-40">
              <button
                onClick={() => navigate('Account')}
                className="flex items-center cursor-pointer w-full p-2 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700"
              >
                <User size={20} className="mr-2" />
                Profile
              </button>
              <button
                onClick={handleLogout}
                className="flex items-center cursor-pointer w-full p-2 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700 text-red-500"
              >
                <LogOut size={20} className="mr-2" />
                Log Out
              </button>
            </div>
          )}
        </div>

        {/* Settings */}
        <div className="relative">
          <button
            onClick={() => toggleDropdown("settings")}
            className="hover:text-cyan-600 cursor-pointer rounded-full p-1 flex items-center"
          >
            <IoSettings size={29} />
          </button>

          {openDropdown === "settings" && (
            <div className="absolute p-1 border dark:border-gray-600 border-gray-400 right-0 top-[134%] bg-white dark:bg-gray-800 shadow-lg rounded-lg w-40">
              <ToggleDarkMode />
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
