import { useState } from "react";
import { Typography, Divider } from "@mui/material";
import { ITransferData } from "../../Others/ClientInterfaces";
import { GetNonReadNotificationsCount, TransferFundAPI } from "../../../features/Slices/Client_Infos_Slice";
import { useAppDispatch } from "../../../features/Slices/hooks";
export function TransferFund() {
  const dispatch=useAppDispatch();
  const [transferData, setTransferData] = useState<ITransferData>({
    recipientAccount: "",
    amount: "",
    description: "",
  });

  const [errors, setErrors] = useState<Partial<ITransferData>>({});
  const [submitted, setSubmitted] = useState(false);
  const [showSummary, setShowSummary] = useState(false);
  const [showConfirmCard, setShowConfirmCard] = useState(false); // 🟢 New state
  const [notificationVisible, setNotificationVisible] = useState(false);
  const[Status,SetStatus]=useState<string>("");
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;

    if (name === "description" && value.length > 60) return;

    setTransferData({ ...transferData, [name]: value });
    setErrors({ ...errors, [name]: "" });
  };

  const validate = () => {
    const newErrors: Partial<ITransferData> = {};
    
    
  
    if (!transferData.recipientAccount.trim()) {
      newErrors.recipientAccount = "Recipient account is required";
    }
  
    const amount = Number(transferData.amount);
    if (
      !String(transferData.amount).trim() ||
      isNaN(amount) ||
      amount < 1000
    ) {
      newErrors.amount = "Amount must be greater than 1000 DA";
    }
  
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };
  

  const  handleSubmit =(e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validate()) return;
    setShowConfirmCard(true); // 🟡 Show confirmation card instead of submitting immediately
  };
  const handleConfirmTransfer =async  () => {
    const payload = {
      recipientAccount: transferData.recipientAccount.trim(),
      amount: Number(transferData.amount.toString().trim()),
      description: transferData.description,
    };
    

    setSubmitted(true);
    setShowConfirmCard(false);
    setTimeout(() => setNotificationVisible(false), 4000); 

    const result=await TransferFundAPI(payload);
     dispatch(GetNonReadNotificationsCount());
     if(result===false)
      SetStatus("Failed");
    else
      SetStatus("Succeeded");
   
     setNotificationVisible(true);
     setTimeout(() => setNotificationVisible(false), 4000);
    
  };
  
  const handleEditForm = () => setShowSummary(false);

  return (
    <div className="flex flex-1 dark:bg-gray-900 flex-col items-center justify-center h-full">
      <div className="bg-white dark:bg-gray-800 dark:text-gray-100 flex items-center justify-center flex-col p-6 w-full shadow-lg rounded-lg">
        <div className="w-[95%] sm:w-[60%]">
          <Typography variant="h5" sx={{ mb: 2, fontWeight: "bold", textAlign: "center" }}>
            Transfer Funds
          </Typography>
          <Typography variant="body1" sx={{ mb: 3, textAlign: "center" }}>
            Use this form to securely transfer funds to another account. Please review the details before submitting your transfer.
          </Typography>
          <Divider sx={{ mb: 3 }} />

          {submitted && (
            <Typography color="success.main" sx={{ mb: 2, textAlign: "center" }}>
              Your transfer has been submitted!
            </Typography>
          )}
        </div>

        <form onSubmit={handleSubmit} className="flex w-[95%] sm:w-[60%] flex-col gap-3">
          {!showSummary ? (
            <>
              

              <Typography variant="h6" sx={{ mb: 1 }}>Recipient's Account Number</Typography>
              <Typography variant="body2" sx={{ mb: 2 }}>
                Enter the account number of the recipient to whom you are sending the funds.
              </Typography>
              <input
                className="border p-2 rounded w-full"
                name="recipientAccount"
                placeholder="Recipient Account Number"
                value={transferData.recipientAccount}
                onChange={handleChange}
              />
              {errors.recipientAccount && <p className="text-red-500 text-sm">{errors.recipientAccount}</p>}

              <Typography variant="h6" sx={{ mb: 1 }}>Transfer Amount</Typography>
              <Typography variant="body2" sx={{ mb: 2 }}>
                Specify the amount you would like to transfer. Make sure it's a positive number and formatted correctly.
              </Typography>
              <input
                className="border p-2 rounded w-full"
                name="amount"
                maxLength={6}
                minLength={4}
                placeholder="Amount (DA)"
                value={transferData.amount}
                onChange={handleChange}
              />
              {errors.amount && <p className="text-red-500 text-sm">{errors.amount}</p>}

              <Typography variant="h6" sx={{ mb: 1 }}>Description (Optional)</Typography>
              <Typography variant="body2" sx={{ mb: 2 }}>
                Add a description (e.g., "Payment for services"). This is optional but helpful.
              </Typography>
              <textarea
                className="border p-2 rounded w-full"
                name="description"
                placeholder="Description"
                value={transferData.description}
                onChange={handleChange}
                maxLength={60}
              />
              <p className="text-sm text-gray-500">
                {60 - transferData.description.length} characters remaining
              </p>
              {errors.description && <p className="text-red-500 text-sm">{errors.description}</p>}

              <div className="w-full flex gap-4 flex-row justify-center">
                <button
                  type="submit"
                  className="px-4 py-2 text-white font-semibold cursor-pointer hover:bg-teal-700 bg-teal-600 rounded-lg"
                >
                  Transfer
                </button>
              
              </div>
            </>
          ) : (
            <>
              
              <Typography variant="h6" sx={{ mb: 2 }}>
                1. Recipient's Account: {transferData.recipientAccount}
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                2. Amount: {transferData.amount} DA
              </Typography>
              <Typography variant="h6" sx={{ mb: 2 }}>
                3. Description: {transferData.description || "No description provided."}
              </Typography>

              <div className="flex items-center justify-center">
                <button
                  type="button"
                  className="px-4 py-2 text-white font-semibold cursor-pointer hover:bg-cyan-700 bg-cyan-600 rounded-lg"
                  onClick={handleEditForm}
                >
                  Understood
                </button>
              </div>
            </>
          )}
        </form>
      </div>

    
      {showConfirmCard && (
        <div className="fixed top-0 left-0 w-full h-full bg-black/50   flex items-center justify-center z-50">
          <div className="bg-white dark:bg-gray-800 dark:text-white p-6 rounded-lg shadow-lg w-[90%] sm:w-[400px]">
            <Typography variant="h6" sx={{ mb: 2, textAlign: "center" }}>
              Confirm Your Transfer
            </Typography>
            <Divider sx={{ mb: 2 }} />
         
            <p><strong>Recipient:</strong> {transferData.recipientAccount}</p>
            <p><strong>Amount:</strong> {transferData.amount} DA</p>
            <p><strong>Description:</strong> {transferData.description || "No description"}</p>

            <div className="flex justify-end gap-4 mt-4">
              <button
                onClick={() => setShowConfirmCard(false)}
                className="px-4 py-2 rounded bg-gray-500 text-white hover:bg-gray-600"
              >
                Cancel
              </button>
              <button
                onClick={handleConfirmTransfer}
                className="px-4 py-2 rounded bg-teal-600 text-white hover:bg-teal-700"
              >
                Confirm
              </button>
            </div>
          </div>
        </div>
      )}
   {notificationVisible && (
  <div className={`fixed top-5 left-1/2 transform 
    -translate-x-1/2 ${Status==="Succeeded"?"bg-teal-600":"bg-red-600"} text-white px-6
     py-4 rounded-xl shadow-2xl z-[9999] 
     transition-all  ease-in-out
      animate-slide-down flex items-start 
      gap-3 max-w-[90%] sm:max-w-md`}>
  
 
  <div>
    <p className="font-semibold">{Status==="Succeeded"?"Transfer process went successfully!":"Your request failed"}</p>
    <p className="text-sm text-white/90">Check notifications for more information.</p>
  </div>
</div>

)}

    </div>
  );
}
