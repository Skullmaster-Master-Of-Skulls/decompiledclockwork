using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsList;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsList;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsList;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.AppointmentsList
{
	// Token: 0x02000147 RID: 327
	public class ListAppointmentManager : IListAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0006C794 File Offset: 0x0006A994
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x0006C79C File Offset: 0x0006A99C
		public IListAppointmentDAO dao { get; set; }

		// Token: 0x06000E97 RID: 3735 RVA: 0x0006C7A5 File Offset: 0x0006A9A5
		public ListAppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ListAppointmentDAO(opContext);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0006C7C4 File Offset: 0x0006A9C4
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0006C7CC File Offset: 0x0006A9CC
		public OperationContext OpContext { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0006C7D8 File Offset: 0x0006A9D8
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				bool flag = this._appLogDao == null;
				if (flag)
				{
					this._appLogDao = new AppointmentLogDAO(this.OpContext);
				}
				return this._appLogDao;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0006C810 File Offset: 0x0006AA10
		private IList<DocumentPrintItem> GeneratePrintCode(IList<IList<ListAppointmentOrAvailability>> allWrappers, List<PersonBase> staff, List<DynamicDataSet> data, int MedicareCid, int BirthDateCid, int PhoneCid, int ImportantInformationCid, IList<ClosedDay> closedDays)
		{
			List<DocumentPrintItem> list = new List<DocumentPrintItem>();
			bool flag = true;
			Predicate<DynamicData> <>9__10;
			Predicate<DynamicData> <>9__11;
			Predicate<DynamicData> <>9__12;
			Predicate<DynamicData> <>9__13;
			foreach (IList<ListAppointmentOrAvailability> list2 in allWrappers)
			{
				try
				{
					bool flag2 = list2.Count > 0;
					if (flag2)
					{
						CWLogger.Logger.Trace("ListAppointmentManager:GeneratePrintCode:wrappers.Count > 0");
						ListAppointmentOrAvailability listAppointmentOrAvailability = list2[0];
						ListAppointment appointment = listAppointmentOrAvailability.Appointment;
						Availability2Item av0 = listAppointmentOrAvailability.Availability;
						bool flag3 = appointment != null;
						string arg;
						if (flag3)
						{
							arg = ((appointment.Staff == null) ? "-" : appointment.Staff.GetName());
						}
						else
						{
							bool flag4 = av0 != null;
							if (flag4)
							{
								PersonBase personBase = staff.Find((PersonBase p) => p.PersonId == av0.PersonId);
								bool flag5 = personBase != null;
								if (flag5)
								{
									arg = personBase.GetName();
								}
								else
								{
									arg = "??";
								}
							}
							else
							{
								arg = "?";
							}
						}
						bool flag6 = flag;
						if (flag6)
						{
							flag = false;
							list.Add(new DocumentPrintItem(eDocumentPrintItemType.DocumentStart));
						}
						else
						{
							list.Add(new DocumentPrintItem(eDocumentPrintItemType.PageBreak));
						}
						list.Add(new DocumentPrintItem(eDocumentPrintItemType.PageTitle, new string[]
						{
							string.Format("From book: {0}", arg)
						}));
						DateTime? dateTime = null;
						List<ListAppointmentOrAvailability> list3 = new List<ListAppointmentOrAvailability>();
						DateTime? dateTime2 = null;
						foreach (ListAppointmentOrAvailability listAppointmentOrAvailability2 in list2)
						{
							ListAppointment app = listAppointmentOrAvailability2.Appointment;
							Availability2Item availability = listAppointmentOrAvailability2.Availability;
							bool flag7 = app != null;
							if (flag7)
							{
								dateTime2 = new DateTime?(app.StartDateTime.Date);
							}
							else
							{
								bool flag8 = availability != null;
								if (flag8)
								{
									dateTime2 = new DateTime?(availability.StartDateTime.Date);
								}
								else
								{
									dateTime2 = null;
								}
							}
							bool flag9 = false;
							bool flag10 = !flag9;
							if (flag10)
							{
								bool flag11 = dateTime2 != null;
								if (flag11)
								{
									bool flag12 = dateTime == null || dateTime2 != dateTime;
									if (flag12)
									{
										bool flag13 = dateTime != null;
										if (flag13)
										{
											list.Add(new DocumentPrintItem(eDocumentPrintItemType.TableEnd));
											List<DocumentPrintItem> list4 = list;
											eDocumentPrintItemType itemType = eDocumentPrintItemType.TableFooter;
											string[] array = new string[1];
											int num = 0;
											string format = "Total for date: {0} • Appointments: {1} • In office: {2} • No-show: {3} • Confirmed: {4}";
											object[] array2 = new object[5];
											array2[0] = dateTime2.Value.ToString("yyyy.MM.dd");
											array2[1] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.AppointmentId > 0).Count.ToString();
											array2[2] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsIn).Count.ToString();
											array2[3] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsNoShow).Count.ToString();
											array2[4] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsConfirmed).Count.ToString();
											array[num] = string.Format(format, array2);
											list4.Add(new DocumentPrintItem(itemType, array));
											list.Add(new DocumentPrintItem(eDocumentPrintItemType.NewLine));
											list.Add(new DocumentPrintItem(eDocumentPrintItemType.NewLine));
											list3 = new List<ListAppointmentOrAvailability>();
										}
										dateTime = dateTime2;
										list.Add(new DocumentPrintItem(eDocumentPrintItemType.TableHeader, new string[]
										{
											string.Format("For date: {0}", dateTime2.Value.ToString("dddd MMMM d, yyyy"))
										}));
										list.Add(new DocumentPrintItem(eDocumentPrintItemType.TableStart));
									}
									list3.Add(listAppointmentOrAvailability2);
									bool flag14 = app != null;
									if (flag14)
									{
										bool flag15 = app.Student != null;
										DynamicDataSet dynamicDataSet;
										if (flag15)
										{
											dynamicDataSet = data.Find((DynamicDataSet f) => ((f.Context == null) ? 0 : f.Context.PrimaryId) == app.Student.PersonId);
											bool flag16 = dynamicDataSet == null;
											if (flag16)
											{
												dynamicDataSet = new DynamicDataSet
												{
													Context = new DynamicDataContext
													{
														PrimaryId = 0
													},
													Data = new List<DynamicData>()
												};
											}
										}
										else
										{
											dynamicDataSet = new DynamicDataSet
											{
												Context = new DynamicDataContext
												{
													PrimaryId = 0
												},
												Data = new List<DynamicData>()
											};
										}
										bool flag17 = dynamicDataSet.Data == null;
										if (flag17)
										{
											dynamicDataSet.Data = new List<DynamicData>();
										}
										List<DynamicData> data2 = dynamicDataSet.Data;
										Predicate<DynamicData> match;
										if ((match = <>9__10) == null)
										{
											match = (<>9__10 = ((DynamicData f) => f.Field != null && f.Field.ControlId.Equals(MedicareCid)));
										}
										DynamicData dynamicData = data2.Find(match);
										List<DynamicData> data3 = dynamicDataSet.Data;
										Predicate<DynamicData> match2;
										if ((match2 = <>9__11) == null)
										{
											match2 = (<>9__11 = ((DynamicData f) => f.Field != null && f.Field.ControlId.Equals(BirthDateCid)));
										}
										DynamicData dynamicData2 = data3.Find(match2);
										List<DynamicData> data4 = dynamicDataSet.Data;
										Predicate<DynamicData> match3;
										if ((match3 = <>9__12) == null)
										{
											match3 = (<>9__12 = ((DynamicData f) => f.Field != null && f.Field.ControlId.Equals(PhoneCid)));
										}
										DynamicData dynamicData3 = data4.Find(match3);
										List<DynamicData> data5 = dynamicDataSet.Data;
										Predicate<DynamicData> match4;
										if ((match4 = <>9__13) == null)
										{
											match4 = (<>9__13 = ((DynamicData f) => f.Field != null && f.Field.ControlId.Equals(ImportantInformationCid)));
										}
										DynamicData dynamicData4 = data5.Find(match4);
										bool flag18 = dynamicData4 == null;
										string text;
										if (flag18)
										{
											text = null;
										}
										else
										{
											text = this.GetValueDisplay(dynamicData4);
										}
										string text2 = (app.Student == null) ? "" : string.Format("{0}, {1}", app.Student.LastName ?? "", app.Student.FirstName ?? "").ToUpper();
										List<string> list5 = new List<string>();
										bool isNoShow = app.IsNoShow;
										if (isNoShow)
										{
											list5.Add("NS");
										}
										bool isIn = app.IsIn;
										if (isIn)
										{
											list5.Add("IN");
										}
										bool isTentative = app.IsTentative;
										if (isTentative)
										{
											list5.Add("TE");
										}
										bool isCancelled = app.IsCancelled;
										if (isCancelled)
										{
											list5.Add("CA");
										}
										bool isConfirmed = app.IsConfirmed;
										if (isConfirmed)
										{
											list5.Add("CO");
										}
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append(string.IsNullOrEmpty(text) ? text2 : string.Format("• {0}\r\n", text2, text));
										bool flag19 = string.IsNullOrEmpty(app.Memo);
										string text3;
										if (flag19)
										{
											text3 = "";
										}
										else
										{
											bool flag20 = app.Memo.IndexOf("rtf", StringComparison.OrdinalIgnoreCase) >= 0;
											if (flag20)
											{
												text3 = app.Memo.ConvertRtfToPlainText();
											}
											else
											{
												text3 = app.Memo;
											}
										}
										bool isStudentsFirstApp = app.IsStudentsFirstApp;
										if (isStudentsFirstApp)
										{
											stringBuilder.Append(" [NP]");
										}
										bool flag21 = !string.IsNullOrEmpty(text3.Trim());
										if (flag21)
										{
											stringBuilder.Append(Environment.NewLine);
											stringBuilder.Append("A NOTES: ");
											stringBuilder.Append(text3);
										}
										list.Add(new DocumentPrintItem
										{
											ColumnText = new string[]
											{
												string.Format("{0} {1}", app.StartDateTime.ToString("HH:mm"), string.Join(",", list5.ToArray())),
												stringBuilder.ToString(),
												(app.Student == null) ? "" : app.Student.Student_no.ToUpper(),
												(dynamicData == null) ? "" : this.GetValueDisplay(dynamicData).ToUpper(),
												(dynamicData2 == null) ? "" : this.GetValueDisplay(dynamicData2).ToUpper(),
												(dynamicData3 == null) ? "" : this.GetValueDisplay(dynamicData3).ToUpper()
											}
										});
									}
									else
									{
										bool flag22 = availability != null;
										if (flag22)
										{
											string text4 = (availability.AvailabilityNote == null || availability.AvailabilityNote.Text == null) ? "" : availability.AvailabilityNote.Text;
											list.Add(new DocumentPrintItem(new string[]
											{
												availability.StartDateTime.ToString("HH:mm"),
												text4,
												"",
												"",
												"",
												""
											}));
										}
									}
								}
							}
						}
						list.Add(new DocumentPrintItem(eDocumentPrintItemType.TableEnd));
						List<DocumentPrintItem> list6 = list;
						eDocumentPrintItemType itemType2 = eDocumentPrintItemType.TableFooter;
						string[] array3 = new string[1];
						int num2 = 0;
						string format2 = "Total for date: {0} • Appointments: {1} • In office: {2} • No-show: {3} • Confirmed: {4}";
						object[] array4 = new object[5];
						array4[0] = ((dateTime2 != null) ? dateTime2.Value.ToString("yyyy.MM.dd") : "");
						array4[1] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.AppointmentId > 0).Count.ToString();
						array4[2] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsIn).Count.ToString();
						array4[3] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsNoShow).Count.ToString();
						array4[4] = list3.FindAll((ListAppointmentOrAvailability f) => f.Appointment != null && f.Appointment.IsConfirmed).Count.ToString();
						array3[num2] = string.Format(format2, array4);
						list6.Add(new DocumentPrintItem(itemType2, array3));
						list3 = new List<ListAppointmentOrAvailability>();
					}
					else
					{
						CWLogger.Logger.Trace("ListAppointmentManager:GeneratePrintCode:wrappers.Count <= 0");
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("ListAppointmentManager:GeneratePrintCode:ErrorInLoop:{0}", ex.ToString());
				}
			}
			CWLogger.Logger.Trace("ListAppointmentManager:GeneratePrintCode:printCodes.Count={0}", (list == null) ? "NULL" : list.Count.ToString());
			return list;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0006D344 File Offset: 0x0006B544
		private string GetValueDisplay(DynamicData DynamicData)
		{
			bool flag = DynamicData == null || DynamicData.Value == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				object value = DynamicData.Value;
				bool flag2 = value is DateTime;
				if (flag2)
				{
					result = ((DateTime)value).ToString("yyyy-MM-dd");
				}
				else
				{
					bool flag3 = value is string;
					if (flag3)
					{
						bool flag4 = DynamicData.Field.ControlCode == eControlCode.RtfTextBox;
						if (flag4)
						{
							string rtf = (string)value;
							result = rtf.ConvertRtfToPlainText();
						}
						else
						{
							string text = (string)value;
							bool flag5 = text.StartsWith("{rtf", StringComparison.OrdinalIgnoreCase);
							if (flag5)
							{
								result = text.ConvertRtfToPlainText();
							}
							else
							{
								result = text;
							}
						}
					}
					else
					{
						bool flag6 = value is int;
						if (flag6)
						{
							result = ((int)value).ToString();
						}
						else
						{
							bool flag7 = value is DateTime;
							if (flag7)
							{
								result = ((DateTime)value).ToString("MMMM d, yyyy");
							}
							else
							{
								result = value.ToString();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0006D458 File Offset: 0x0006B658
		public List<Availability2Item> LoadOverlappingAvailabilities(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			return this.dao.LoadOverlappingAvailabilities(PersonId, StartDateTime, EndDateTime);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0006D478 File Offset: 0x0006B678
		public void CreateListAppointment(bool runInTransaction, ListAppointment Appointment)
		{
			this.dao.CreateListAppointment(Appointment);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0006D488 File Offset: 0x0006B688
		public void CancelListAppointment(bool runInTransaction, int AppointmentId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.UpdateAppointmentCancelledValue(true, AppointmentId, true, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0006D4FC File Offset: 0x0006B6FC
		public void UnCancelListAppointment(bool runInTransaction, int AppointmentId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.UpdateAppointmentCancelledValue(true, AppointmentId, false, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0006D570 File Offset: 0x0006B770
		public void MarkListAppointmentAsTentative(bool runInTransaction, int Appointmentid)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointmentid);
			}
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.UpdateAppointmentAppCodeValue(true, Appointmentid, -1);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(Appointmentid, eAppointmentModifiedItemType.ShowTimeAs);
				});
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0006D5E4 File Offset: 0x0006B7E4
		public void UnMarkListAppointmentAsTentative(bool runInTransaction, int Appointmentid)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointmentid);
			}
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.UpdateAppointmentAppCodeValue(true, Appointmentid, 0);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(Appointmentid, eAppointmentModifiedItemType.ShowTimeAs);
				});
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0006D658 File Offset: 0x0006B858
		public void DeleteListAppointment(bool runInTransaction, int AppointmentId)
		{
			ListAppointment app = this.LoadAppointmentById(AppointmentId, false);
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(true, AppointmentId);
			IList<Availability2Item> list = this.dao.LoadOverlappingAvailabilitiesWithAppointment(app);
			foreach (Availability2Item availability2Item in list)
			{
				this.dao.MarkAvailabilityWithAppointment(availability2Item.Availability2ItemId, 0);
			}
			bool flag = !runInTransaction;
			if (flag)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0006D720 File Offset: 0x0006B920
		public void UpdateListAppointment(bool runInTransaction, ListAppointment Appointment)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointment.AppointmentId);
			}
			this.dao.UpdateListAppointment(Appointment);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(Appointment.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0006D790 File Offset: 0x0006B990
		public List<Availability2Item> FreeTimeSearch(List<int> PersonIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			return this.dao.FreeTimeSearch(PersonIds, StartDateTime, EndDateTime);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0006D7B0 File Offset: 0x0006B9B0
		public IList<ClosedDay> LoadClosedDays(IList<int> PersonIds, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadClosedDays(PersonIds, StartDate, EndDate);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0006D7D0 File Offset: 0x0006B9D0
		public ClosedDay IsDayClosed(int PersonId, DateTime Date)
		{
			IList<ClosedDay> list = this.LoadClosedDays(new List<int>
			{
				PersonId
			}, Date, Date);
			return (list.Count > 0) ? list[0] : null;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0006D80D File Offset: 0x0006BA0D
		public void CreateClosedDay(IList<ClosedDay> ClosedDays)
		{
			this.dao.CreateClosedDay(ClosedDays);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0006D81D File Offset: 0x0006BA1D
		public void DeleteClosedDay(int PersonId, DateTime Date)
		{
			this.dao.DeleteClosedDay(PersonId, Date);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0006D82E File Offset: 0x0006BA2E
		public void CreateAvailabilities(List<Availability2Item> Availabilities)
		{
			this.dao.CreateAvailabilities(Availabilities);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0006D83E File Offset: 0x0006BA3E
		public void DeleteAvailability(List<int> AvailabilityIds)
		{
			this.dao.DeleteAvailability(AvailabilityIds);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0006D84E File Offset: 0x0006BA4E
		public void UpdateAvailability(List<Availability2Item> Availabilities)
		{
			this.dao.UpdateAvailability(Availabilities);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0006D860 File Offset: 0x0006BA60
		private IList<ListAppointmentOrAvailability> LoadAppointmentsWithAvailabilities(List<ListAppointment> apps, List<Availability2Item> availabilities)
		{
			apps.Sort((ListAppointment a1, ListAppointment a2) => a1.StartDateTime.CompareTo(a2.StartDateTime));
			List<ListAppointmentOrAvailability> list = new List<ListAppointmentOrAvailability>();
			List<int> addedAppIds = new List<int>();
			using (List<Availability2Item>.Enumerator enumerator = availabilities.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Availability2Item availability = enumerator.Current;
					ListAppointment listAppointment = apps.FirstOrDefault((ListAppointment a) => !(a.EndDateTime <= availability.StartDateTime) && !(a.StartDateTime >= availability.EndDateTime));
					bool flag = listAppointment == null;
					if (flag)
					{
						list.Add(new ListAppointmentOrAvailability
						{
							Availability = availability
						});
					}
					else
					{
						bool flag2 = !addedAppIds.Contains(listAppointment.AppointmentId);
						if (flag2)
						{
							list.Add(new ListAppointmentOrAvailability
							{
								Availability = availability,
								Appointment = listAppointment
							});
							addedAppIds.Add(listAppointment.AppointmentId);
						}
					}
				}
			}
			IEnumerable<ListAppointment> enumerable = from g in apps
			where !addedAppIds.Contains(g.AppointmentId)
			select g;
			foreach (ListAppointment appointment in enumerable)
			{
				list.Add(new ListAppointmentOrAvailability
				{
					Appointment = appointment
				});
			}
			list.Sort((ListAppointmentOrAvailability w1, ListAppointmentOrAvailability w2) => ((w1.Appointment != null) ? w1.Appointment.StartDateTime : ((w1.Availability != null) ? w1.Availability.StartDateTime : DateTime.MinValue)).CompareTo((w2.Appointment != null) ? w2.Appointment.StartDateTime : ((w2.Availability != null) ? w2.Availability.StartDateTime : DateTime.MinValue)));
			List<ListAppointmentOrAvailability> list2 = list.FindAll((ListAppointmentOrAvailability g) => g.Appointment != null);
			for (int i = 1; i < list2.Count; i++)
			{
				int index = i - 1;
				ListAppointment appointment2 = list2[i].Appointment;
				ListAppointment appointment3 = list2[index].Appointment;
				bool flag3 = !(appointment2.EndDateTime <= appointment3.StartDateTime) && !(appointment2.StartDateTime >= appointment3.EndDateTime);
				if (flag3)
				{
					int backgroundColorArgB = -16181;
					list2[i].BackgroundColorArgB = backgroundColorArgB;
					list2[index].BackgroundColorArgB = backgroundColorArgB;
				}
			}
			return list;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0006DAE8 File Offset: 0x0006BCE8
		public IList<DocumentPrintItem> GenerateMedicalCalendarDocumentPrintItems(DateTime StartDate, int NumDays, IList<PersonBase> Staff, bool HideCancelled)
		{
			IList<ClosedDay> closedDays = this.LoadClosedDays(Staff.ToList<PersonBase>().ConvertAll<int>((PersonBase g) => g.PersonId), StartDate, StartDate.AddDays((double)NumDays));
			List<IList<ListAppointmentOrAvailability>> list = new List<IList<ListAppointmentOrAvailability>>();
			foreach (PersonBase personBase in Staff)
			{
				List<ListAppointmentOrAvailability> item = this.LoadAppointmentsWithAvailability(new List<int>
				{
					personBase.PersonId
				}, StartDate, NumDays, true, HideCancelled).ToList<ListAppointmentOrAvailability>();
				list.Add(item);
			}
			CWLogger.Logger.Trace("ListAppointmentManager:GenerateMedicalCalendarDocumentPrintItems:allWrappers.Count={0}", (list == null) ? "NULL" : list.Count.ToString());
			List<int> list2 = new List<int>();
			foreach (IList<ListAppointmentOrAvailability> list3 in list)
			{
				foreach (ListAppointmentOrAvailability listAppointmentOrAvailability in list3)
				{
					ListAppointment appointment = listAppointmentOrAvailability.Appointment;
					bool flag = appointment != null && appointment.Student != null && appointment.Student.PersonId > 0 && !list2.Contains(appointment.Student.PersonId);
					if (flag)
					{
						list2.Add(appointment.Student.PersonId);
					}
				}
			}
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_MedicalScheduler_MedicareControlId);
			int settingValue_Int2 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_MedicalScheduler_BirthDateControlId);
			int settingValue_Int3 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_MedicalScheduler_PhoneControlId);
			int settingValue_Int4 = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_MedicalScheduler_ImportantInformationCid);
			List<int> list4 = new List<int>();
			list4.Add(settingValue_Int);
			list4.Add(settingValue_Int2);
			list4.Add(settingValue_Int3);
			list4.Add(settingValue_Int4);
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			List<DynamicDataSet> data = dynamicDataManager.LoadPerStudentDataForMultipleStudents(list2, list4);
			return this.GeneratePrintCode(list, Staff.ToList<PersonBase>(), data, settingValue_Int, settingValue_Int2, settingValue_Int3, settingValue_Int4, closedDays);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0006DD7C File Offset: 0x0006BF7C
		public IList<Availability2Item> LoadAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays)
		{
			return this.dao.LoadAvailability(PersonIds, StartDate, NumDays);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0006DD9C File Offset: 0x0006BF9C
		public IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment)
		{
			return this.LoadAppointments(PersonIds, StartDate, NumDays, LoadIsStudentsFirstAppointment, false);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0006DDBC File Offset: 0x0006BFBC
		private IList<ListAppointment> LoadAppointments(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments)
		{
			return this.dao.LoadAppointments(PersonIds, StartDate, NumDays, LoadIsStudentsFirstAppointment, HideCancelledAppointments);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0006DDE0 File Offset: 0x0006BFE0
		public IList<ListAppointmentOrAvailability> LoadAppointmentsWithAvailability(IList<int> PersonIds, DateTime StartDate, int NumDays, bool LoadIsStudentsFirstAppointment, bool HideCancelledAppointments)
		{
			IList<ListAppointment> source = this.LoadAppointments(PersonIds, StartDate, NumDays, LoadIsStudentsFirstAppointment, HideCancelledAppointments);
			IList<Availability2Item> source2 = this.LoadAvailability(PersonIds, StartDate, NumDays);
			return this.LoadAppointmentsWithAvailabilities(source.ToList<ListAppointment>(), source2.ToList<Availability2Item>());
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0006DE1C File Offset: 0x0006C01C
		public ListAppointment LoadAppointmentById(int AppointmentId, bool LoadIsStudentsFirstAppointment)
		{
			return this.dao.LoadAppointmentById(AppointmentId, LoadIsStudentsFirstAppointment);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0006DE3C File Offset: 0x0006C03C
		public void MarkIn(bool runInTransaction, int AppointmentId, bool newIn)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			IList<Attendee> source = appointmentAttendeeManager.LoadAttendeesByAppointmentId(AppointmentId);
			Attendee attendee = source.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Students);
			bool flag = attendee != null;
			if (flag)
			{
				bool flag2 = !runInTransaction;
				if (flag2)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
				}
				appointmentAttendeeManager.UpdateMiscCodeValue(true, attendee.AttendeeId, newIn ? 2 : -1);
				bool flag3 = !runInTransaction;
				if (flag3)
				{
					Task.Run(delegate()
					{
						IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
						appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.None);
					});
				}
			}
			else
			{
				CWLogger.Logger.Warn("ListAppointmentManager:MarkIn:Can't mark in because no student found in appointment (appid={0})", AppointmentId.ToString());
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0006DF18 File Offset: 0x0006C118
		public void MarkNoShow(bool runInTransaction, int AppointmentId, bool newNoShow)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			IList<Attendee> source = appointmentAttendeeManager.LoadAttendeesByAppointmentId(AppointmentId);
			Attendee attendee = source.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Students);
			bool flag = attendee != null;
			if (flag)
			{
				bool flag2 = !runInTransaction;
				if (flag2)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
				}
				appointmentAttendeeManager.UpdateNoShowValue(true, attendee.AttendeeId, newNoShow);
				bool flag3 = !runInTransaction;
				if (flag3)
				{
					Task.Run(delegate()
					{
						IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
						appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.NoShow);
					});
				}
			}
			else
			{
				CWLogger.Logger.Warn("ListAppointmentManager:MarkNoShow:Can't mark noshow because no student found in appointment (appid={0})", AppointmentId.ToString());
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
		public void MarkConfirmed(bool runInTransaction, int AppointmentId, bool newConfirmed)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			IList<Attendee> source = appointmentAttendeeManager.LoadAttendeesByAppointmentId(AppointmentId);
			Attendee attendee = source.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Students);
			bool flag = attendee != null;
			if (flag)
			{
				bool flag2 = !runInTransaction;
				if (flag2)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
				}
				appointmentAttendeeManager.UpdateMiscCodeValue(true, attendee.AttendeeId, newConfirmed ? 4 : 0);
				bool flag3 = !runInTransaction;
				if (flag3)
				{
					Task.Run(delegate()
					{
						IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
						appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.None);
					});
				}
			}
			else
			{
				CWLogger.Logger.Warn("ListAppointmentManager:MarkConfirmed:Can't mark confirmed because no student found in appointment (appid={0})", AppointmentId.ToString());
			}
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0006E0CC File Offset: 0x0006C2CC
		public Dictionary<DateTime, eAvailabilityCode> LoadSingleDayAvailabilityStatusesByUser(int PersonId, DateTime StartDate, int NumDays)
		{
			Dictionary<DateTime, eAvailabilityCode> dictionary = this.dao.LoadSingleDayAvailabilityStatusesByUser(PersonId, StartDate, NumDays);
			bool flag = dictionary.Count < 1;
			Dictionary<DateTime, eAvailabilityCode> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				DateTime date = StartDate.Date;
				Dictionary<DateTime, eAvailabilityCode>.KeyCollection keys = dictionary.Keys;
				for (int i = 0; i < NumDays; i++)
				{
					DateTime dateTime = date.AddDays((double)i);
					bool flag2 = !keys.Contains(dateTime);
					if (flag2)
					{
						dictionary.Add(dateTime, eAvailabilityCode.Empty);
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0006E150 File Offset: 0x0006C350
		public Availability2Item LoadAvailabilityById(int Availability2ItemId)
		{
			return this.dao.LoadAvailabilityById(Availability2ItemId);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0006E170 File Offset: 0x0006C370
		public void FixAvailabilityAppointmentMappings(DateTime StartDate, DateTime EndDate)
		{
			int numDays = Convert.ToInt32((EndDate.Date - StartDate.Date).TotalDays) + 1;
			IList<Availability2ItemWithAppointmentId> list = this.dao.LoadUniqueAvailabilitiesForAllPeopleWithAppointmentIds(StartDate, EndDate);
			bool flag = list == null || list.Count < 1;
			if (!flag)
			{
				List<int> list2 = new List<int>();
				foreach (Availability2ItemWithAppointmentId availability2ItemWithAppointmentId in list)
				{
					bool flag2 = !list2.Contains(availability2ItemWithAppointmentId.PersonId);
					if (flag2)
					{
						list2.Add(availability2ItemWithAppointmentId.PersonId);
					}
				}
				IList<ListAppointment> source = this.LoadAppointments(list2, StartDate, numDays, false);
				using (IEnumerator<Availability2ItemWithAppointmentId> enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Availability2ItemWithAppointmentId availability = enumerator2.Current;
						int appId = availability.AppointmentId;
						bool flag3 = true;
						bool flag4 = appId > 0;
						if (flag4)
						{
							ListAppointment listAppointment = source.FirstOrDefault((ListAppointment g) => g.AppointmentId == appId);
							bool flag5 = listAppointment != null;
							if (flag5)
							{
								flag3 = false;
							}
						}
						bool flag6 = flag3;
						if (flag6)
						{
							ListAppointment listAppointment2 = source.FirstOrDefault((ListAppointment g) => !(availability.EndDateTime <= g.StartDateTime) && !(availability.StartDateTime >= g.EndDateTime));
							int num = (listAppointment2 == null) ? 0 : listAppointment2.AppointmentId;
							bool flag7 = num != availability.AppointmentId;
							if (flag7)
							{
								this.dao.MarkAvailabilityWithAppointment(availability.Availability2ItemId, num);
								availability.AppointmentId = num;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0006E350 File Offset: 0x0006C550
		public IList<ListAppointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1)
		{
			return this.dao.LoadAllAppointments(DayToLoadAppointmentsFor.Date, NumDaysToLoadAppointmentsFor, ShowCancelled);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0006E378 File Offset: 0x0006C578
		public IList<Availability2Marker> LoadAvailability2Markers()
		{
			return this.dao.LoadAvailability2Markers();
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0006E398 File Offset: 0x0006C598
		public int CreateAvailability2Marker(Availability2Marker Marker)
		{
			return this.dao.CreateAvailability2Marker(Marker);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0006E3B6 File Offset: 0x0006C5B6
		public void DeleteAvailability2Marker(int Availability2MarkerId)
		{
			this.dao.DeleteAvailability2Marker(Availability2MarkerId);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0006E3C6 File Offset: 0x0006C5C6
		public void UpdateAvailability2Marker(Availability2Marker Marker)
		{
			this.dao.UpdateAvailability2Marker(Marker);
		}

		// Token: 0x040002B0 RID: 688
		private IAppointmentLogDAO _appLogDao;
	}
}
