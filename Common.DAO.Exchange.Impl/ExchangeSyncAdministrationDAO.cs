using System;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl
{
	// Token: 0x02000003 RID: 3
	public class ExchangeSyncAdministrationDAO : IApplicationSyncAdministrationDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000436E File Offset: 0x0000256E
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00004376 File Offset: 0x00002576
		internal ExchangeService ExchangeService { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000437F File Offset: 0x0000257F
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00004387 File Offset: 0x00002587
		private ISyncContactsDAO ContactsDAO { get; set; }

		// Token: 0x0600002A RID: 42 RVA: 0x00004390 File Offset: 0x00002590
		public ExchangeSyncAdministrationDAO(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ContactsDAO = new ExchangeContactsDAO(this.OpContext);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000043B4 File Offset: 0x000025B4
		public DelegatePermissionLevel GetDelegatePermissionLevel(string userEmailAddress)
		{
			string text = string.Empty;
			DelegatePermissionLevel result;
			try
			{
				bool flag = string.IsNullOrEmpty(userEmailAddress);
				if (flag)
				{
					result = DelegatePermissionLevel.None;
				}
				else
				{
					CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: User email='{0}'", userEmailAddress ?? "NULL");
					text = this.DelegateEmailAddress;
					int num = userEmailAddress.IndexOf("@");
					string suffix = userEmailAddress.Substring(num, userEmailAddress.Length - num);
					text = this.CheckDelegateEmailAddress(text, suffix);
					CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}'", text ?? "NULL");
					bool flag2 = userEmailAddress != null && userEmailAddress.Equals(text, StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						result = (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write);
					}
					else
					{
						bool flag3 = this.ExchangeService != null;
						if (flag3)
						{
							DelegateInformation delegates = this.ExchangeService.GetDelegates(new Mailbox(userEmailAddress), true, new UserId[]
							{
								text
							});
							bool flag4 = delegates == null || delegates.DelegateUserResponses == null || delegates.DelegateUserResponses.Count == 0;
							if (flag4)
							{
								CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None", text ?? "NULL", userEmailAddress ?? "NULL");
								result = DelegatePermissionLevel.None;
							}
							else
							{
								bool? flag5;
								if (delegates == null)
								{
									flag5 = null;
								}
								else
								{
									DelegateUserResponse delegateUserResponse = delegates.DelegateUserResponses[0];
									if (delegateUserResponse == null)
									{
										flag5 = null;
									}
									else
									{
										DelegateUser delegateUser = delegateUserResponse.DelegateUser;
										flag5 = ((delegateUser != null) ? new bool?(delegateUser.ViewPrivateItems) : null);
									}
								}
								bool? flag6 = flag5;
								bool valueOrDefault = flag6.GetValueOrDefault();
								DelegateUserResponse delegateUserResponse2 = delegates.DelegateUserResponses[0];
								bool flag7 = delegateUserResponse2 == null;
								if (flag7)
								{
									CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None", text ?? "NULL", userEmailAddress ?? "NULL");
									CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate user email '{0}' response is NULL", text);
									result = DelegatePermissionLevel.None;
								}
								else
								{
									bool flag8 = delegateUserResponse2.DelegateUser == null;
									if (flag8)
									{
										CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate user email '{0}' is NULL", text);
										try
										{
											CWLogger.Logger.Trace("OutlookDAO::GetDelegatePermissionLevel:: Trying to read appointment from user calendar '{0}'.", userEmailAddress);
											bool flag9 = this.ExchangeService != null;
											if (flag9)
											{
												this.ExchangeService.FindAppointments(new FolderId(WellKnownFolderName.Calendar, userEmailAddress), new CalendarView(DateTime.Now.Date, DateTime.Now.AddDays(1.0).Date));
												CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = 'Read'. Delegate permissions is not set but we were able to read appointments from its calendar", text ?? "NULL", userEmailAddress ?? "NULL");
												CWLogger.Logger.Trace("OutlookDAO::GetDelegatePermissionLevel:: Read appointments from user calendar '{0}' successfull.", userEmailAddress);
												return DelegatePermissionLevel.Read | DelegatePermissionLevel.Write;
											}
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None", text ?? "NULL", userEmailAddress ?? "NULL");
											return DelegatePermissionLevel.None;
										}
										catch (Exception ex)
										{
											CWLogger.Logger.ErrorException(string.Format("OutlookDAO::GetDelegatePermissionLevel:: Delegate {0} does not have access to {1} calendar: {2}", text, userEmailAddress, ex.ToString()), ex);
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Error occurs", text ?? "NULL", userEmailAddress ?? "NULL");
											return DelegatePermissionLevel.None;
										}
									}
									bool flag10 = delegateUserResponse2.DelegateUser.Permissions == null;
									if (flag10)
									{
										CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. No permissions were set", text ?? "NULL", userEmailAddress ?? "NULL");
										CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate user '{0}' permissions is NULL", text);
										result = DelegatePermissionLevel.None;
									}
									else
									{
										switch (delegateUserResponse2.DelegateUser.Permissions.CalendarFolderPermissionLevel)
										{
										case DelegateFolderPermissionLevel.None:
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Permission is set to None", text ?? "NULL", userEmailAddress ?? "NULL");
											result = DelegatePermissionLevel.None;
											break;
										case DelegateFolderPermissionLevel.Editor:
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = Read | Write. Permission is set Editor", text ?? "NULL", userEmailAddress ?? "NULL");
											result = (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write);
											break;
										case DelegateFolderPermissionLevel.Reviewer:
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = Read. Permission is set Reviewer", text ?? "NULL", userEmailAddress ?? "NULL");
											result = DelegatePermissionLevel.Read;
											break;
										case DelegateFolderPermissionLevel.Author:
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = Read. Permission is set Author", text ?? "NULL", userEmailAddress ?? "NULL");
											result = DelegatePermissionLevel.Read;
											break;
										default:
											CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Permission is unknown", text ?? "NULL", userEmailAddress ?? "NULL");
											result = DelegatePermissionLevel.None;
											break;
										}
									}
								}
							}
						}
						else
						{
							CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. ExchangeService is NULL", text ?? "NULL", userEmailAddress ?? "NULL");
							CWLogger.Logger.Trace("SyncOperationContextAdapter::GetDelegatePermissionLevel: ExchangeService is NULL");
							result = DelegatePermissionLevel.None;
						}
					}
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.WarnException(string.Format("OutlookDAO::GetDelegatePermissionLevel:: FullAccess to the mailbox '{1}' by delegate '{2}' is not setup. {0}", ex2.ToString(), userEmailAddress, text), ex2);
				try
				{
					bool flag11 = this.ExchangeService != null;
					if (flag11)
					{
						FindItemsResults<Appointment> findItemsResults = this.ExchangeService.FindAppointments(new FolderId(WellKnownFolderName.Calendar, userEmailAddress), new CalendarView(DateTime.Now.Date, DateTime.Now.AddDays(1.0).Date));
						CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = Read. Error occurs, but I was able to read appointments on the calendar", text ?? "NULL", userEmailAddress ?? "NULL");
						result = (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write);
					}
					else
					{
						CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Errors occur, not even able to read appointments from this calendar", text ?? "NULL", userEmailAddress ?? "NULL");
						result = DelegatePermissionLevel.None;
					}
				}
				catch (Exception ex3)
				{
					CWLogger.Logger.Info("ExchangeSyncAdministrationDAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Errors occur, not even able to read appointments from this calendar", text ?? "NULL", userEmailAddress ?? "NULL");
					CWLogger.Logger.WarnException(string.Format("OutlookDAO::GetDelegatePermissionLevel:: Delegate {0} does not have access to {1} calendar: {2}", text, userEmailAddress, ex3.ToString()), ex3);
					result = DelegatePermissionLevel.None;
				}
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000049F0 File Offset: 0x00002BF0
		private string CheckDelegateEmailAddress(string delegateEmailAddress, string suffix)
		{
			CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::CheckDelegateEmailAddress: inDelegateEmail='{0}'", delegateEmailAddress);
			string text = delegateEmailAddress.Contains("@") ? delegateEmailAddress : (delegateEmailAddress + suffix);
			CWLogger.Logger.Trace("ExchangeSyncAdministrationDAO::CheckDelegateEmailAddress: outDelegateEmail='{0}'", text);
			return text;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00004A40 File Offset: 0x00002C40
		private string DelegateEmailAddress
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this._delegateEmailAddress);
				if (flag)
				{
					this._delegateEmailAddress = this.ContactsDAO.ResolveEmailAddress(this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username);
				}
				return this._delegateEmailAddress;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00004A94 File Offset: 0x00002C94
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00004AAC File Offset: 0x00002CAC
		public SyncOperationContext OpContext
		{
			get
			{
				return this._opContext;
			}
			set
			{
				this._opContext = value;
				bool flag = value != null;
				if (flag)
				{
					this.ExchangeService = value.GetExchangeService();
				}
			}
		}

		// Token: 0x04000010 RID: 16
		private string _delegateEmailAddress;

		// Token: 0x04000011 RID: 17
		private SyncOperationContext _opContext;
	}
}
