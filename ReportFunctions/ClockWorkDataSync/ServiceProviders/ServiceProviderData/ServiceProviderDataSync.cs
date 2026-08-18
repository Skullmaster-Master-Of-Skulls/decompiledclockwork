using System;
using System.Collections.Generic;
using System.Data;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderData
{
	// Token: 0x0200005B RID: 91
	public class ServiceProviderDataSync
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00053980 File Offset: 0x00052980
		public List<ServiceProviderDataSyncDataItemAction> DataSyncServiceProviderData(DataTable t)
		{
			if (t.Rows.Count > 0)
			{
				string text = t.Rows[0]["student_no"].ToString().Trim().ToUpper();
				if (text.Length > 0)
				{
					UnivDataAdapter da = ClientCache.CurrentInstance.da;
					TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
					string commandText = "SELECT serviceproviderid FROM serviceproviders WHERE student_no=@sne";
					DataTable dataTable = new DataTable();
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@sne", tripleDES.Encrypt(text));
					da.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						int spid = (int)dataTable.Rows[0][0];
						return this.DataSyncServiceProviderData(spid, t);
					}
				}
			}
			return new List<ServiceProviderDataSyncDataItemAction>();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00053AC4 File Offset: 0x00052AC4
		public List<ServiceProviderDataSyncDataItemAction> DataSyncServiceProviderData(int spid, DataTable t)
		{
			List<ServiceProviderDataSyncDataItemAction> list = new List<ServiceProviderDataSyncDataItemAction>();
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			if (t.Rows.Count > 0)
			{
				string value = t.Rows[0]["student_no"].ToString().Trim().ToUpper();
				List<ServiceProviderDataSyncDataItem> list2 = new List<ServiceProviderDataSyncDataItem>();
				foreach (object obj in t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow["student_no"].ToString().Trim().ToUpper();
					if (text.Equals(value) && text.Length > 0)
					{
						foreach (object obj2 in t.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj2;
							eServiceProviderDataItemType dataItemType = ServiceProviderDataSync.ParseType(dataColumn.ColumnName);
							if (dataItemType != eServiceProviderDataItemType.Unknown)
							{
								string text2 = dataRow[dataColumn].ToString().Trim();
								if (!string.IsNullOrEmpty(text2))
								{
									ServiceProviderDataSyncDataItem serviceProviderDataSyncDataItem = new ServiceProviderDataSyncDataItem(dataItemType);
									serviceProviderDataSyncDataItem.DataItemExternalValue = text2;
									ServiceProviderDataSyncDataItem serviceProviderDataSyncDataItem2 = list2.Find((ServiceProviderDataSyncDataItem di) => di.DataItemType == dataItemType);
									if (serviceProviderDataSyncDataItem2 == null)
									{
										list2.Add(serviceProviderDataSyncDataItem);
									}
									else if (string.IsNullOrEmpty(serviceProviderDataSyncDataItem2.DataItemExternalValue))
									{
										serviceProviderDataSyncDataItem2.DataItemExternalValue = serviceProviderDataSyncDataItem.DataItemExternalValue;
									}
								}
							}
						}
					}
				}
				foreach (ServiceProviderDataSyncDataItem externalDataItem in list2)
				{
					list.Add(new ServiceProviderDataSyncDataItemAction
					{
						ExternalDataItem = externalDataItem,
						ActionType = DataSyncActionType.ServiceProviderDataItem_AddUpdateClockWork,
						Pid = spid
					});
				}
			}
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string text9 = "";
			string text10 = "";
			string text11 = "";
			string text12 = "";
			string text13 = "";
			string text14 = "";
			foreach (ServiceProviderDataSyncDataItemAction serviceProviderDataSyncDataItemAction in list)
			{
				switch (serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemType)
				{
				case eServiceProviderDataItemType.FirstName:
					text3 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.MiddleName:
					text4 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.LastName:
					text5 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Student_no:
					text6 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Email:
					text8 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.AltEmail:
					text9 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.AltId:
					text7 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Phone1:
					text12 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Phone2:
					text13 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Address1:
					text10 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.Address2:
					text11 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				case eServiceProviderDataItemType.PhoneNote:
					text14 = serviceProviderDataSyncDataItemAction.ExternalDataItem.DataItemExternalValue;
					break;
				}
			}
			string text15 = "UPDATE serviceproviders SET firstname=@firstname,middlename=@middlename,lastname=@lastname,student_no=@student_no\r\n,altid=@altid,email=@email1,email2=@email2,phone1=@phone1,phone2=@phone2,address=@address1,address2=@address2,phonenote=@phonenote\r\nWHERE serviceproviderid=@id";
			int num = 0;
			if (string.IsNullOrEmpty(text3.Trim()))
			{
				text15 = text15.Replace("@firstname", "firstname");
				num++;
			}
			if (string.IsNullOrEmpty(text5.Trim()))
			{
				text15 = text15.Replace("@lastname", "lastname");
				num++;
			}
			if (string.IsNullOrEmpty(text4.Trim()))
			{
				text15 = text15.Replace("@middlename", "middlename");
				num++;
			}
			if (string.IsNullOrEmpty(text6.Trim()))
			{
				text15 = text15.Replace("@student_no", "student_no");
				num++;
			}
			if (string.IsNullOrEmpty(text7.Trim()))
			{
				text15 = text15.Replace("@altid", "altid");
				num++;
			}
			if (string.IsNullOrEmpty(text8.Trim()))
			{
				text15 = text15.Replace("@email1", "email1");
				num++;
			}
			if (string.IsNullOrEmpty(text9.Trim()))
			{
				text15 = text15.Replace("@email2", "email2");
				num++;
			}
			if (string.IsNullOrEmpty(text12.Trim()))
			{
				text15 = text15.Replace("@phone1", "phone1");
				num++;
			}
			if (string.IsNullOrEmpty(text13.Trim()))
			{
				text15 = text15.Replace("@phone2", "phone2");
				num++;
			}
			if (string.IsNullOrEmpty(text10.Trim()))
			{
				text15 = text15.Replace("@address1", "address1");
				num++;
			}
			if (string.IsNullOrEmpty(text11.Trim()))
			{
				text15 = text15.Replace("@address2", "address2");
				num++;
			}
			if (string.IsNullOrEmpty(text14.Trim()))
			{
				text15 = text15.Replace("@phonenote", "phonenote");
				num++;
			}
			if (num >= 12)
			{
				foreach (ServiceProviderDataSyncDataItemAction serviceProviderDataSyncDataItemAction in list)
				{
					serviceProviderDataSyncDataItemAction.ActionResult = DataSyncActionResult.NoActionTaken;
				}
			}
			else
			{
				da.SelectCommand.CommandText = text15;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@firstname", tripleDES.Encrypt(text3));
				da.SelectCommand.Parameters.Add("@middlename", tripleDES.Encrypt(text4));
				da.SelectCommand.Parameters.Add("@lastname", tripleDES.Encrypt(text5));
				da.SelectCommand.Parameters.Add("@student_no", tripleDES.Encrypt(text6));
				da.SelectCommand.Parameters.Add("@altid", tripleDES.Encrypt(text7));
				da.SelectCommand.Parameters.Add("@email1", tripleDES.Encrypt(text8));
				da.SelectCommand.Parameters.Add("@email2", tripleDES.Encrypt(text9));
				da.SelectCommand.Parameters.Add("@phone1", tripleDES.Encrypt(text12));
				da.SelectCommand.Parameters.Add("@phone2", tripleDES.Encrypt(text13));
				da.SelectCommand.Parameters.Add("@address1", tripleDES.Encrypt(text10));
				da.SelectCommand.Parameters.Add("@address2", tripleDES.Encrypt(text11));
				da.SelectCommand.Parameters.Add("@phonenote", tripleDES.Encrypt(text14));
				da.SelectCommand.Parameters.Add("@id", spid);
				da.Fill(new DataTable());
				foreach (ServiceProviderDataSyncDataItemAction serviceProviderDataSyncDataItemAction in list)
				{
					serviceProviderDataSyncDataItemAction.ActionResult = DataSyncActionResult.Success;
				}
			}
			return list;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00054420 File Offset: 0x00053420
		public static eServiceProviderDataItemType ParseType(string columnName)
		{
			string text = columnName.ToLower().Trim();
			string text2 = text;
			switch (text2)
			{
			case "firstname":
				return eServiceProviderDataItemType.FirstName;
			case "middlename":
				return eServiceProviderDataItemType.MiddleName;
			case "lastname":
				return eServiceProviderDataItemType.LastName;
			case "student_no":
				return eServiceProviderDataItemType.Student_no;
			case "email":
			case "serviceprovideremail":
				return eServiceProviderDataItemType.Email;
			case "altemail":
			case "email2":
				return eServiceProviderDataItemType.AltEmail;
			case "altid":
			case "username":
				return eServiceProviderDataItemType.AltId;
			case "phone1":
			case "phone":
				return eServiceProviderDataItemType.Phone1;
			case "phone2":
			case "cellphone":
				return eServiceProviderDataItemType.Phone2;
			case "address":
			case "address1":
			case "laddress":
				return eServiceProviderDataItemType.Address1;
			case "address2":
			case "paddress":
				return eServiceProviderDataItemType.Address2;
			case "phonenote":
				return eServiceProviderDataItemType.PhoneNote;
			}
			return eServiceProviderDataItemType.Unknown;
		}
	}
}
