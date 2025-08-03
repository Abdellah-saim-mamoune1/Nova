
export interface IAccount {
  id: number;
  account:string;
  balance: number;
  isFrozen: boolean;
  createdAt:Date;
  card:Card;
}



export interface Card {
  cardNumber:string;
  expirationDate: string;
 
}

export interface AddNotification{
  title: string,
  body:string|null,
  type: number,
  AccountId: string

}

export interface ClientInfo {
  id: number;
  firstName: string;
  lastName: string;
  personalEmail: string;
  birthDate: string;
  address: string;
  phoneNumber: string;
  gender:string;
  isActive: boolean;
}



export interface IClientNotificationsGet{
  id:number,
  notification:string,
  title: string,
  type:string,
  date: string,
  isViewed: boolean

}

export interface IPaginatedNotifications{
  notifications:IClientNotificationsGet[];
  totalPages:number;
}

export interface AddGetHelpRequist{
account: string,
title: string,
body: string,
type:number,



}

export interface SetNotificationViewed{
  account:string|undefined,
  id:number|null
}

 export interface ITransferData {
  recipientAccount: string;
  amount: number|string;
  description: string;
}

export interface ITransfersHistoryPaginated{
  transfers:ITransferstionsHistory[];
  totalPages:number;
}

export interface ITransferstionsHistory{
 
id:number;
senderAccount:string;
recipientAccount:string;
amount:number;
createdAt:Date;
}


export interface ITransactionsHistoryPaginated{
  transactions:ITransactionsHistory[];
  totalPages:number;
}

export interface ITransactionsHistory{
  id:number
  type:string,
  amount: number,
  createdAt: Date
}


export interface InfosState {
  client_informations: ClientInfo | null;
  Accounts:IAccount[]|null;
  IsLoggedIn: boolean|null;
  hasFetched:boolean;
  NonReadNotificationsCount:number|null;
  Account: string | null;
}
