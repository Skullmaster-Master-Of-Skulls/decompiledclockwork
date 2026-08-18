using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl.Adapters;
using TechnoPro.Common.DAO.Exchange.Impl.Mappers;
using TechnoPro.Common.DAO.Impl.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.DAO.Exchange.Impl
{
	// Token: 0x02000002 RID: 2
	public class ExchangeAppointmentDAO : IExternalAppointmentDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public int PagingSize { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		internal ExchangeService ExchangeService { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000208C File Offset: 0x0000028C
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

		// Token: 0x06000007 RID: 7 RVA: 0x000020B8 File Offset: 0x000002B8
		public ExchangeAppointmentDAO(SyncOperationContext operationContext)
		{
			this.OpContext = operationContext;
			this.PagingSize = 25;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateClockWorkAppId(string uniqueId, int cwappid)
		{
			Appointment appointment = Appointment.Bind(this.ExchangeService, new ItemId(uniqueId));
			appointment.SetExtendedProperty(this.ExtendedPropertyDef, cwappid);
			appointment.SetClockWorkAppointmentId(cwappid, this.ExtendedPropertyDef);
			appointment.Update(ConflictResolutionMode.AutoResolve, SendInvitationsOrCancellationsMode.SendToNone);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002548 File Offset: 0x00000748
		public ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex)
		{
			bool flag = ocurrenceIndex <= 0;
			ExternalAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					Appointment appointment = Appointment.BindToRecurringMaster(this.ExchangeService, new ItemId(uniqueId));
					Appointment appointment2 = Appointment.BindToOccurrence(this.ExchangeService, new ItemId(appointment.Id.UniqueId), ocurrenceIndex);
					this.LoadPropertiesForItems(new Appointment[]
					{
						appointment2
					}, null);
					result = ((appointment2 == null) ? null : appointment2.ToDTO(this.OpContext));
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadRecurrenceOcurrence:Error={0}", ex.ToString());
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000025EC File Offset: 0x000007EC
		public string ResetSyncState(string username)
		{
			bool flag = false;
			string text = null;
			do
			{
				ChangeCollection<ItemChange> changeCollection = this.ExchangeService.SyncFolderItems(new FolderId(WellKnownFolderName.Calendar, username), PropertySet.IdOnly, null, 512, SyncFolderItemsScope.NormalItems, text);
				text = changeCollection.SyncState;
				bool flag2 = !changeCollection.MoreChangesAvailable;
				if (flag2)
				{
					flag = true;
				}
			}
			while (!flag);
			return text;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002650 File Offset: 0x00000850
		public ExternalSyncAppointmentChangesResponse LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request)
		{
			List<ExternalSyncAppointmentChange> list = new List<ExternalSyncAppointmentChange>();
			bool flag = false;
			string syncState = request.SyncState;
			try
			{
				do
				{
					ChangeCollection<ItemChange> changeCollection = this.ExchangeService.SyncFolderItems(new FolderId(WellKnownFolderName.Calendar, request.Username), PropertySet.FirstClassProperties, null, 512, SyncFolderItemsScope.NormalItems, syncState);
					bool flag2 = changeCollection.Count > 0;
					if (flag2)
					{
						IAppointmentSyncMappingDAO appointmentSyncMappingDAO = new AppointmentSyncMappingDAO(this.OpContext);
						foreach (ItemChange itemChange in changeCollection)
						{
							try
							{
								bool flag3 = itemChange.ChangeType == ChangeType.Create;
								if (flag3)
								{
									Appointment appointment = itemChange.Item as Appointment;
									bool flag4 = appointment != null;
									if (flag4)
									{
										appointment.LoadPropertiesForAppointment(new PropertyDefinitionBase[]
										{
											ItemSchema.Id,
											AppointmentSchema.ICalUid,
											ItemSchema.LastModifiedTime,
											AppointmentSchema.IsAllDayEvent,
											ItemSchema.Sensitivity,
											ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
										});
										ExternalAppointmentId externalAppointmentId = appointment.GetExternalAppointmentId(true);
										ClockWorkExternalAppMapping mapping = appointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId2(externalAppointmentId.UniqueId2);
										bool isPrivate = false;
										try
										{
											isPrivate = (appointment.Sensitivity == Sensitivity.Private);
										}
										catch (ServiceObjectPropertyException)
										{
											isPrivate = false;
										}
										bool isAllDayEvent = false;
										try
										{
											isAllDayEvent = appointment.IsAllDayEvent;
										}
										catch (ServiceObjectPropertyException)
										{
											isAllDayEvent = false;
										}
										ExternalSyncAppointmentChange item = new ExternalSyncAppointmentChange
										{
											AppointmentSyncChangeType = eAppointmentSyncChangeType.Added,
											ExternalAppointmentID = externalAppointmentId,
											Mapping = mapping,
											LastModifiedDate = appointment.LastModifiedTime,
											IsPrivate = isPrivate,
											IsAllDayEvent = isAllDayEvent
										};
										list.Add(item);
									}
								}
								else
								{
									bool flag5 = itemChange.ChangeType == ChangeType.Update;
									if (flag5)
									{
										Appointment appointment2 = itemChange.Item as Appointment;
										bool flag6 = appointment2 != null;
										if (flag6)
										{
											appointment2.LoadPropertiesForAppointment(new PropertyDefinitionBase[]
											{
												ItemSchema.Id,
												AppointmentSchema.ICalUid,
												ItemSchema.LastModifiedTime,
												AppointmentSchema.IsAllDayEvent,
												ItemSchema.Sensitivity,
												ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
											});
											bool isPrivate2 = false;
											try
											{
												isPrivate2 = (appointment2.Sensitivity == Sensitivity.Private);
											}
											catch (ServiceObjectPropertyException)
											{
												isPrivate2 = false;
											}
											bool isAllDayEvent2 = false;
											try
											{
												isAllDayEvent2 = appointment2.IsAllDayEvent;
											}
											catch (ServiceObjectPropertyException)
											{
												isAllDayEvent2 = false;
											}
											ExternalAppointmentId externalAppointmentId2 = appointment2.GetExternalAppointmentId(true);
											ClockWorkExternalAppMapping mapping2 = appointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId2(externalAppointmentId2.UniqueId2);
											ExternalSyncAppointmentChange item2 = new ExternalSyncAppointmentChange
											{
												AppointmentSyncChangeType = eAppointmentSyncChangeType.Modified,
												ExternalAppointmentID = externalAppointmentId2,
												Mapping = mapping2,
												LastModifiedDate = appointment2.LastModifiedTime,
												IsPrivate = isPrivate2,
												IsAllDayEvent = isAllDayEvent2
											};
											list.Add(item2);
										}
									}
									else
									{
										bool flag7 = itemChange.ChangeType == ChangeType.Delete;
										if (flag7)
										{
											ItemId itemId = itemChange.ItemId;
											bool flag8 = itemId != null;
											if (flag8)
											{
												ClockWorkExternalAppMapping mapping3 = appointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId(itemId.UniqueId);
												ExternalSyncAppointmentChange item3 = new ExternalSyncAppointmentChange
												{
													AppointmentSyncChangeType = eAppointmentSyncChangeType.Deleted,
													ExternalAppointmentID = new ExternalAppointmentId
													{
														UniqueId = itemId.UniqueId
													},
													Mapping = mapping3,
													LastModifiedDate = DateTime.Now.AddDays(1.0)
												};
												list.Add(item3);
											}
										}
										else
										{
											bool flag9 = itemChange.ChangeType == ChangeType.ReadFlagChange;
											if (flag9)
											{
											}
										}
									}
								}
							}
							catch (Exception ex)
							{
								CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadAppointmentChanges::User={0}, synstate={2}\n{1}", request.Username, ex.ToString(), request.SyncState ?? "NULL"), ex);
							}
						}
					}
					syncState = changeCollection.SyncState;
					bool flag10 = !changeCollection.MoreChangesAvailable;
					if (flag10)
					{
						flag = true;
					}
				}
				while (!flag);
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadAppointmentChanges::User={0},\n{1}", request.Username, ex2.ToString()), ex2);
			}
			return new ExternalSyncAppointmentChangesResponse
			{
				AppointmentChanges = list,
				SyncState = syncState
			};
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002B0C File Offset: 0x00000D0C
		public string LoadNativeAppointmentInfo(string appId)
		{
			Appointment appointment = Appointment.Bind(this.ExchangeService, new ItemId(appId));
			bool flag = appointment != null;
			if (flag)
			{
				this.LoadPropertiesForItems(new Appointment[]
				{
					appointment
				}, this._nativePropertySet_2010);
			}
			return appointment.ToDisplayString();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002B58 File Offset: 0x00000D58
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate)
		{
			return this.LoadAppointmentsByPage(user, startdate, endDate, this.PagingSize);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002B7C File Offset: 0x00000D7C
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate, bool sortedByDate = true)
		{
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			try
			{
				ItemView itemView = new ItemView(this.PagingSize)
				{
					PropertySet = new PropertySet(BasePropertySet.IdOnly),
					Offset = 0,
					OffsetBasePoint = OffsetBasePoint.Beginning
				};
				if (sortedByDate)
				{
					itemView.OrderBy.Add(AppointmentSchema.Start, SortDirection.Ascending);
				}
				FindItemsResults<Item> findItemsResults;
				do
				{
					findItemsResults = this.ExchangeService.FindItems(new FolderId(WellKnownFolderName.Calendar, user.Username), new SearchFilter.SearchFilterCollection(LogicalOperator.And, new SearchFilter[]
					{
						new SearchFilter.IsLessThan(AppointmentSchema.Start, endDate),
						new SearchFilter.IsGreaterThan(AppointmentSchema.End, startdate),
						new SearchFilter.IsEqualTo(AppointmentSchema.IsCancelled, false)
					}), itemView);
					bool flag = findItemsResults != null && findItemsResults.Items.Count > 0;
					if (flag)
					{
						this.LoadPropertiesForItems(findItemsResults.ToList<Item>(), null);
						list.AddRange(from item in findItemsResults.OfType<Appointment>()
						select item.ToDTO(this.OpContext));
						bool flag2 = findItemsResults.NextPageOffset != null;
						if (flag2)
						{
							itemView.Offset = findItemsResults.NextPageOffset.Value;
						}
					}
				}
				while (findItemsResults != null && findItemsResults.MoreAvailable);
			}
			catch (ServiceResponseException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadAppointmens::User={0},\n{1}", user.Username, ex.ToString()), ex);
				throw;
			}
			return list;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002D10 File Offset: 0x00000F10
		public IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate)
		{
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			try
			{
				ItemView itemView = new ItemView(this.PagingSize)
				{
					PropertySet = new PropertySet(BasePropertySet.IdOnly),
					Offset = 0,
					OffsetBasePoint = OffsetBasePoint.Beginning
				};
				if (sortedByDate)
				{
					itemView.OrderBy.Add(AppointmentSchema.Start, SortDirection.Ascending);
				}
				FindItemsResults<Item> findItemsResults;
				do
				{
					findItemsResults = this.ExchangeService.FindItems(new FolderId(WellKnownFolderName.Calendar, user.Username), new SearchFilter.SearchFilterCollection(LogicalOperator.And, new SearchFilter[]
					{
						new SearchFilter.IsGreaterThanOrEqualTo(AppointmentSchema.Start, startdate),
						new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.LastModifiedTime, thresholdTime)
					}), itemView);
					bool flag = findItemsResults != null && findItemsResults.Items.Count > 0;
					if (flag)
					{
						this.LoadPropertiesForItems(findItemsResults.ToList<Item>(), null);
						list.AddRange(from item in findItemsResults.OfType<Appointment>()
						select item.ToDTO(this.OpContext));
						bool flag2 = findItemsResults.NextPageOffset != null;
						if (flag2)
						{
							itemView.Offset = findItemsResults.NextPageOffset.Value;
						}
					}
				}
				while (findItemsResults != null && findItemsResults.MoreAvailable);
			}
			catch (ServiceResponseException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadModifiedAppointments::User={0},\n{1}", user.Username, ex.ToString()), ex);
				throw;
			}
			return list;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002E90 File Offset: 0x00001090
		public IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false)
		{
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			ItemId itemId = new ItemId(masterAppUid);
			Appointment appointment;
			try
			{
				appointment = Appointment.Bind(this.ExchangeService, itemId, this._recPropertySetEx_2010);
			}
			catch
			{
				appointment = Appointment.Bind(this.ExchangeService, itemId, this._recPropertySetEx_2007);
			}
			DateTime? dateTime = (appointment.LastOccurrence != null) ? new DateTime?(appointment.LastOccurrence.Start) : null;
			bool flag = startDatetime != null && dateTime != null && dateTime < startDatetime;
			IList<ExternalAppointment> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IAppointmentSyncMappingDAO appointmentSyncMappingDAO = new AppointmentSyncMappingDAO(this.OpContext);
				int num = 1;
				while (list.Count < count)
				{
					Appointment appointment2 = null;
					try
					{
						appointment2 = Appointment.BindToOccurrence(this.ExchangeService, itemId, num++);
					}
					catch (ServiceResponseException ex)
					{
						bool flag2 = ex.ErrorCode == ServiceError.ErrorCalendarOccurrenceIsDeletedFromRecurrence;
						if (flag2)
						{
							IList<ClockWorkExternalAppMapping> list2 = appointmentSyncMappingDAO.LoadMappingByExternalMasterRecurrenceAppointmentId(masterAppUid);
							bool flag3 = list2 != null && list2.Count > 0;
							if (flag3)
							{
								using (IEnumerator<ClockWorkExternalAppMapping> enumerator = list2.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										ClockWorkExternalAppMapping mapping = enumerator.Current;
										ExternalAppointment externalAppointment = list.FirstOrDefault((ExternalAppointment a) => a.Mapping != null && a.Mapping.ClockWorkAppointmentId > 0 && a.Mapping.ClockWorkAppointmentId == mapping.ClockWorkAppointmentId);
										bool flag4 = externalAppointment == null;
										if (flag4)
										{
											ExternalAppointment externalAppointment2 = this.LoadAppointment(mapping.ExternalApplicationUniqueAppointmentId) ?? this.LoadAppointmentByUniqueId2(mapping.ExternalApplicationUniqueAppointmentId2);
											bool flag5 = externalAppointment2 == null;
											if (flag5)
											{
												externalAppointment2 = new ExternalAppointment
												{
													Mapping = mapping,
													UniqueId = mapping.ExternalApplicationUniqueAppointmentId,
													UniqueId2 = mapping.ExternalApplicationUniqueAppointmentId2,
													LegacyGlobalAppointmentId = mapping.ExternalApplicationGlobalAppointmentId,
													IsCancelled = true
												};
												list.Add(externalAppointment2);
											}
										}
									}
								}
							}
							continue;
						}
						bool flag6 = ex.ErrorCode == ServiceError.ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange;
						if (flag6)
						{
							break;
						}
					}
					bool flag7 = appointment2 == null;
					if (!flag7)
					{
						bool flag8 = startDatetime != null;
						if (flag8)
						{
							bool flag9 = appointment2.Start >= startDatetime.Value;
							if (!flag9)
							{
								continue;
							}
							try
							{
								appointment2.LoadPropertiesForAppointment(this._fullPropertySet_2010);
							}
							catch (Exception)
							{
								appointment2.LoadPropertiesForAppointment(this._fullPropertySet_2007);
							}
							ExternalAppointment externalAppointment3 = appointment2.ToDTO(this.OpContext);
							if (loadMapping)
							{
								externalAppointment3.Mapping = appointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId2(externalAppointment3.UniqueId2);
							}
							list.Add(externalAppointment3);
						}
						else
						{
							ExternalAppointment externalAppointment4 = appointment2.ToDTO(this.OpContext);
							if (loadMapping)
							{
								externalAppointment4.Mapping = appointmentSyncMappingDAO.LoadMappingByExternalUniqueAppointmentId2(externalAppointment4.UniqueId2);
							}
							list.Add(externalAppointment4);
						}
						bool flag10 = dateTime != null && dateTime.Value == appointment2.Start;
						if (flag10)
						{
							break;
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003258 File Offset: 0x00001458
		public IList<ExternalAppointment> LoadAppointmentsWithoutPaging(ExternalAttendee user, DateTime startdate, DateTime endDate)
		{
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			try
			{
				FindItemsResults<Appointment> findItemsResults = this.ExchangeService.FindAppointments(new FolderId(WellKnownFolderName.Calendar, user.Username), new CalendarView(startdate, endDate));
				bool flag = findItemsResults != null && findItemsResults.Items.Count > 0;
				if (flag)
				{
					this.LoadPropertiesForItems(findItemsResults, null);
					list.AddRange(from app in findItemsResults
					select app.ToDTO(this.OpContext));
				}
			}
			catch (ServiceResponseException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadAppointmensWithoutPaging::User={0},\n{1}", user.Username, ex.ToString()), ex);
				throw;
			}
			return list;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003310 File Offset: 0x00001510
		public ExternalAppointment LoadAppointmentByICalUid(string ICalUid, bool base64 = true)
		{
			ExternalAppointment result;
			try
			{
				string icaluid = base64 ? ICalUid : AppointmentAdapter.GetObjectIdStringFromUid(ICalUid);
				Appointment appointment = ExchangeAppointmentDAO.FindRelatedAppointment(this.ExchangeService, icaluid);
				bool flag = appointment != null;
				if (flag)
				{
					this.LoadPropertiesForItems(new Appointment[]
					{
						appointment
					}, null);
				}
				result = ((appointment == null) ? null : appointment.ToDTO(this.OpContext));
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointmentByICalUid:ICalUid={1}, Error={0}", ex.ToString(), ICalUid ?? "NULL");
				result = null;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000033A4 File Offset: 0x000015A4
		public ExternalAppointment LoadAppointment(string appUid)
		{
			ExternalAppointment result;
			try
			{
				bool flag = string.IsNullOrEmpty(appUid);
				if (flag)
				{
					CWLogger.Logger.Warn("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointment:appUid is null or empty");
					result = null;
				}
				else
				{
					Appointment appointment = Appointment.Bind(this.ExchangeService, new ItemId(appUid));
					bool flag2 = appointment != null;
					if (flag2)
					{
						this.LoadPropertiesForItems(new Appointment[]
						{
							appointment
						}, null);
					}
					result = ((appointment == null) ? null : appointment.ToDTO(this.OpContext));
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointment:appUId={1}, Error={0}", ex.ToString(), appUid ?? "NULL");
				result = null;
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000344C File Offset: 0x0000164C
		public IList<ExternalAppointment> LoadAppointments(IList<string> appUidList)
		{
			IList<ExternalAppointment> result;
			try
			{
				bool flag = appUidList == null || appUidList.Count == 0;
				if (flag)
				{
					CWLogger.Logger.Warn("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointment:appUidList is null or empty");
					result = null;
				}
				else
				{
					ServiceResponseCollection<GetItemResponse> serviceResponseCollection;
					try
					{
						serviceResponseCollection = this.ExchangeService.BindToItems(from id in appUidList
						select new ItemId(id), this._fullPropertySet_2010);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::LoadAppointments:: {0}", ex.ToString()), ex);
						try
						{
							serviceResponseCollection = this.ExchangeService.BindToItems(from id in appUidList
							select new ItemId(id), this._fullPropertySet_2007);
						}
						catch (Exception ex2)
						{
							CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::LoadAppointments for Exchange 2007:: {0}", ex2.ToString()), ex2);
							serviceResponseCollection = null;
						}
					}
					bool flag2 = serviceResponseCollection == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						List<ExternalAppointment> list = new List<ExternalAppointment>();
						foreach (Appointment appointment in from i in serviceResponseCollection
						where i != null && i.Result != ServiceResult.Error && i.Item is Appointment
						select i.Item as Appointment)
						{
							try
							{
								ExternalAppointment externalAppointment = appointment.ToDTO(this.OpContext);
								bool flag3 = externalAppointment != null;
								if (flag3)
								{
									list.Add(externalAppointment);
								}
							}
							catch (Exception ex3)
							{
								CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::LoadAppointments failed to convert exchange app into external app:: AppId={0}, Error={1}", appointment.Id.UniqueId ?? "NULL", ex3.ToString()), ex3);
							}
						}
						result = list;
					}
				}
			}
			catch (Exception ex4)
			{
				CWLogger.Logger.ErrorException(string.Format("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointments: Error={0}", ex4.ToString()), ex4);
				result = null;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000036E0 File Offset: 0x000018E0
		public ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex)
		{
			ExternalAppointment result;
			try
			{
				bool flag = string.IsNullOrEmpty(masterAppUid);
				if (flag)
				{
					CWLogger.Logger.Warn("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadOccurrenceOfRecurringSerieByMasterId:masterAppUid is null or empty");
					result = null;
				}
				else
				{
					Appointment appointment = Appointment.BindToOccurrence(this.ExchangeService, new ItemId(masterAppUid), occurenceIndex);
					bool flag2 = appointment != null;
					if (flag2)
					{
						this.LoadPropertiesForItems(new Appointment[]
						{
							appointment
						}, null);
					}
					result = ((appointment == null) ? null : appointment.ToDTO(this.OpContext));
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadOccurrenceOfRecurringSerieByMasterId:masterAppUid={1}, Error={0}", ex.ToString(), masterAppUid ?? "NULL");
				result = null;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000378C File Offset: 0x0000198C
		public ExternalAppointment LoadAppointmentByUniqueId2(string uniqueid2)
		{
			ExternalAppointment result;
			try
			{
				bool flag = string.IsNullOrEmpty(uniqueid2);
				if (flag)
				{
					result = null;
				}
				else
				{
					SearchFilter searchFilter = new SearchFilter.IsEqualTo(ExchangeAppointmentDAO.uniqueId2Def, uniqueid2);
					Appointment appointment = (Appointment)this.ExchangeService.FindItems(WellKnownFolderName.Calendar, searchFilter, new ItemView(1)).FirstOrDefault<Item>();
					bool flag2 = appointment != null;
					if (flag2)
					{
						this.LoadPropertiesForItems(new Appointment[]
						{
							appointment
						}, null);
						result = appointment.ToDTO(this.OpContext);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointmentByUniqueId2:UniqueId2={0}:Error={1}", uniqueid2 ?? "", ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003840 File Offset: 0x00001A40
		public ExternalAppointment LoadAppointmentByUniqueId2(string username, string uniqueid2)
		{
			ExternalAppointment result;
			try
			{
				bool flag = string.IsNullOrEmpty(uniqueid2);
				if (flag)
				{
					result = null;
				}
				else
				{
					SearchFilter searchFilter = new SearchFilter.IsEqualTo(ExchangeAppointmentDAO.uniqueId2Def, uniqueid2);
					Appointment appointment = (Appointment)this.ExchangeService.FindItems(new FolderId(WellKnownFolderName.Calendar, username), searchFilter, new ItemView(1)).FirstOrDefault<Item>();
					bool flag2 = appointment != null;
					if (flag2)
					{
						this.LoadPropertiesForItems(new Appointment[]
						{
							appointment
						}, null);
						result = appointment.ToDTO(this.OpContext);
					}
					else
					{
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointmentByUniqueId2:Username={0}:uniqueid2={1}:Error={2}", username ?? "NULL", uniqueid2 ?? "NULL", ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003908 File Offset: 0x00001B08
		public ExternalAppointment LoadAppointmentByClockWorkAppointmentId(int cwappid)
		{
			ExternalAppointment result;
			try
			{
				SearchFilter searchFilter = new SearchFilter.IsEqualTo(this.ExtendedPropertyDef, cwappid);
				Appointment appointment = (Appointment)this.ExchangeService.FindItems(WellKnownFolderName.Calendar, searchFilter, new ItemView(1)).FirstOrDefault<Item>();
				bool flag = appointment == null;
				if (flag)
				{
					searchFilter = new SearchFilter.ContainsSubstring(AppointmentSchema.MeetingWorkspaceUrl, string.Format("cwappid={0}", cwappid.ToString()));
					appointment = (Appointment)this.ExchangeService.FindItems(WellKnownFolderName.Calendar, searchFilter, new ItemView(1)).FirstOrDefault<Item>();
				}
				bool flag2 = appointment != null;
				if (flag2)
				{
					this.LoadPropertiesForItems(new Appointment[]
					{
						appointment
					}, null);
					result = appointment.ToDTO(this.OpContext);
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.Exchange.Impl.ExchangeAppointmentDAO.LoadAppointmentByClockWorkAppointmentId:Error={0}", ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000039EC File Offset: 0x00001BEC
		public ExternalAppointment CreateAppointment(ExternalAppointment appointment)
		{
			try
			{
				Appointment appointment2 = appointment.ToDomainObject(this);
				string text = null;
				bool flag = appointment.Organizer == null || this.OpContext.SyncSettings.SyncUsers.All((ClockWorkExternalApplicationSyncUser u) => !u.ExternalApplicationUsername.Equals(appointment.Organizer.Username, StringComparison.OrdinalIgnoreCase));
				if (flag)
				{
					ExternalAttendee externalAttendee = appointment.Attendees.FirstOrDefault((ExternalAttendee a) => this.OpContext.SyncSettings.SyncUsers.Find((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(a.Username, StringComparison.OrdinalIgnoreCase)) != null);
					bool flag2 = externalAttendee != null;
					if (flag2)
					{
						text = externalAttendee.Username;
					}
				}
				bool flag3 = text == null;
				if (flag3)
				{
					text = ((appointment.Organizer != null) ? appointment.Organizer.Username : appointment.Attendees[0].Username);
				}
				appointment2.Save(new FolderId(WellKnownFolderName.Calendar, text), SendInvitationsMode.SendOnlyToAll);
				this.LoadPropertiesForItems(new Appointment[]
				{
					appointment2
				}, null);
				ExternalAppointment externalAppointment = appointment2.ToDTO(this.OpContext);
				externalAppointment.UniqueId = (appointment.UniqueId = appointment2.Id.UniqueId);
				externalAppointment.UniqueId2 = (appointment.UniqueId2 = appointment2.GetUniqueAppointmentId(true));
				externalAppointment.LegacyGlobalAppointmentId = (appointment.LegacyGlobalAppointmentId = appointment2.GetGlobalAppointmentId(true));
				CWLogger.Logger.Debug("ExchangeAppointmentDAO::CreateAppointment:: Outlook appointment was created: OutlookAppointmentId={0}", appointment2.Id.UniqueId);
				return externalAppointment;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::CreateAppointment: Failed with error {0}", ex.ToString()), ex);
			}
			return null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public void UpdateAppointment(ExternalAppointment appointment)
		{
			try
			{
				CWLogger.Logger.Debug("ExchangeAppointmentDAO::UpdatingAppointment:: Updating Outlook appointment: OutlookAppointmentId={0}", appointment.UniqueId);
				Appointment appointment2 = Appointment.Bind(this.ExchangeService, new ItemId(appointment.UniqueId));
				this.LoadPropertiesForItems(new Appointment[]
				{
					appointment2
				}, null);
				appointment2 = appointment.ToDomainObject(appointment2, this);
				appointment2.Update(ConflictResolutionMode.AutoResolve, SendInvitationsOrCancellationsMode.SendToAllAndSaveCopy);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::UpdatingAppointment:: Updating Outlook appointment: OutlookAppointmentId={0} failed, {1}", appointment.UniqueId, ex.ToString()), ex);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003C60 File Offset: 0x00001E60
		public void DeleteAppointment(ExternalAppointment exApp)
		{
			try
			{
				Appointment app = Appointment.Bind(this.ExchangeService, new ItemId(exApp.UniqueId), this._deletePropertySet);
				ExchangeAppointmentDAO.DeleteAppointment(app);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExchangeAppointmentDAO::DeleteAppointment: Failed qith error {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003CC8 File Offset: 0x00001EC8
		private static Appointment FindRelatedAppointment(ExchangeService service, string icaluid)
		{
			SearchFilter.IsEqualTo searchFilter = new SearchFilter.IsEqualTo
			{
				PropertyDefinition = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 3, MapiPropertyType.Binary),
				Value = icaluid
			};
			ItemView view = new ItemView(1)
			{
				PropertySet = new PropertySet(BasePropertySet.FirstClassProperties)
			};
			Collection<Item> items = service.FindItems(WellKnownFolderName.Calendar, searchFilter, view).Items;
			return (items != null && items.Count > 0) ? (items[0] as Appointment) : null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003D38 File Offset: 0x00001F38
		private IList<ExternalAppointment> LoadAppointmentsByPage(ExternalAttendee user, DateTime startdate, DateTime endDate, int pageSize)
		{
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			try
			{
				FindItemsResults<Appointment> findItemsResults = this.ExchangeService.FindAppointments(new FolderId(WellKnownFolderName.Calendar, user.Username), new CalendarView(startdate, endDate, pageSize));
				bool flag = findItemsResults != null && findItemsResults.TotalCount > pageSize;
				if (flag)
				{
					TimeSpan timeSpan = endDate - startdate;
					bool flag2 = timeSpan.TotalDays <= 1.0;
					if (!flag2)
					{
						DateTime startdate2 = startdate;
						DateTime dateTime = startdate.AddDays(timeSpan.TotalDays / 2.0);
						DateTime startdate3 = dateTime;
						DateTime endDate2 = endDate;
						IList<ExternalAppointment> collection = this.LoadAppointmentsByPage(user, startdate2, dateTime, pageSize);
						IList<ExternalAppointment> collection2 = this.LoadAppointmentsByPage(user, startdate3, endDate2, pageSize);
						list.AddRange(collection);
						list.AddRange(collection2);
						return list;
					}
					int maxItemsReturned = findItemsResults.TotalCount + 1;
					findItemsResults = this.ExchangeService.FindAppointments(new FolderId(WellKnownFolderName.Calendar, user.Username), new CalendarView(startdate, endDate, maxItemsReturned));
				}
				bool flag3 = findItemsResults != null && findItemsResults.Items.Count > 0;
				if (flag3)
				{
					this.LoadPropertiesForItems(findItemsResults.ToList<Appointment>(), this._cancelledPropertySet);
					List<Appointment> list2 = (from a in findItemsResults
					where !a.IsCancelled
					select a).ToList<Appointment>();
					CWLogger.Logger.Debug("OutlookAppointmentDAO::LoadAppointmensByPage::User={0},TotalApps={1},NoCancelledApps={2},startdate={3},enddate{4}", new object[]
					{
						user.Username,
						findItemsResults.Items.Count,
						list2.Count,
						startdate.ToString("yyyy-MM-dd"),
						endDate.ToString("yyyy-MM-dd")
					});
					this.LoadPropertiesForItems(list2, null);
					list.AddRange(from app in list2
					select app.ToDTO(this.OpContext));
				}
			}
			catch (ServiceResponseException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadAppointmensByPage::User={0},\n{1}", user.Username, ex.ToString()), ex);
				throw;
			}
			return list;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003F70 File Offset: 0x00002170
		private void LoadPropertiesForItems(IList<Item> items, PropertySet propertySet = null)
		{
			bool flag = items == null || items.Count == 0;
			if (!flag)
			{
				try
				{
					ServiceResponseCollection<ServiceResponse> source = this.ExchangeService.LoadPropertiesForItems(from i in items
					where i != null && i.Id != null
					select i, propertySet ?? this._fullPropertySet_2010);
					try
					{
						IEnumerable<ServiceResponse> enumerable = from sr in source
						where sr.Result == ServiceResult.Error
						select sr;
						foreach (ServiceResponse serviceResponse in enumerable)
						{
							CWLogger.Logger.Error("OutlookAppointmentDAO::LoadPropertiesForItems:: {0}: {1}", serviceResponse.ErrorCode, serviceResponse.ErrorMessage);
						}
					}
					catch
					{
					}
				}
				catch
				{
					try
					{
						ServiceResponseCollection<ServiceResponse> source2 = this.ExchangeService.LoadPropertiesForItems(from i in items
						where i != null && i.Id != null
						select i, propertySet ?? this._fullPropertySet_2007);
						try
						{
							IEnumerable<ServiceResponse> enumerable2 = from sr in source2
							where sr.Result == ServiceResult.Error
							select sr;
							foreach (ServiceResponse serviceResponse2 in enumerable2)
							{
								CWLogger.Logger.Error("OutlookAppointmentDAO::LoadPropertiesForItems:: {0}: {1}", serviceResponse2.ErrorCode, serviceResponse2.ErrorMessage);
							}
						}
						catch
						{
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::LoadPropertiesForItems:: {0}", ex.ToString()), ex);
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004190 File Offset: 0x00002390
		private void LoadPropertiesForItems(IEnumerable<Appointment> apps, PropertySet propertySet = null)
		{
			this.LoadPropertiesForItems(apps.Cast<Item>().ToList<Item>(), propertySet);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000041A8 File Offset: 0x000023A8
		private static void DeleteAppointment(Appointment app)
		{
			bool flag = app != null;
			if (flag)
			{
				bool isMeeting = app.IsMeeting;
				if (isMeeting)
				{
					try
					{
						CWLogger.Logger.Debug("ExchangeAppointmentDAO::DeleteAppointment:: Deleting Outlook appointment: OutlookAppointmentId={0}", app.Id.UniqueId);
						app.CancelMeeting();
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::DeleteAppointment:: 1. Cancel Meeting failed: OutlookAppointmentId={0} /n{1}", app.Id.UniqueId, ex.ToString()), ex);
						try
						{
							CWLogger.Logger.Trace("OutlookAppointmentDAO::DeleteAppointment:: 3. Trying to Delete a Meeting after declining it failed: OutlookAppointmentId={0}", app.Id.UniqueId);
							app.Delete(DeleteMode.MoveToDeletedItems, SendCancellationsMode.SendOnlyToAll);
							CWLogger.Logger.Trace("OutlookAppointmentDAO::DeleteAppointment:: 3. Delete a Meeting worked: OutlookAppointmentId={0}", app.Id.UniqueId);
						}
						catch (Exception ex2)
						{
							CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::DeleteAppointment:: 3. Delete a Meeting failed: OutlookAppointmentId={0} /n{1}", app.Id.UniqueId, ex2.ToString()), ex2);
							CWLogger.Logger.Trace("OutlookAppointmentDAO::DeleteAppointment:: 4. Trying to Hard Delete a Meeting after deleting it failed: OutlookAppointmentId={0}", app.Id.UniqueId);
							app.Delete(DeleteMode.HardDelete, SendCancellationsMode.SendOnlyToAll);
							CWLogger.Logger.Trace("OutlookAppointmentDAO::DeleteAppointment:: 4. Hard Delete a Meeting worked: OutlookAppointmentId={0}", app.Id.UniqueId);
						}
					}
				}
				else
				{
					try
					{
						app.Delete(DeleteMode.MoveToDeletedItems, SendCancellationsMode.SendOnlyToAll);
					}
					catch (Exception ex3)
					{
						CWLogger.Logger.ErrorException(string.Format("OutlookAppointmentDAO::DeleteAppointment:: Delete an Appointment failed: OutlookAppointmentId={0} /n{1}", app.Id.UniqueId, ex3.ToString()), ex3);
						app.Delete(DeleteMode.HardDelete, SendCancellationsMode.SendOnlyToAll);
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		public readonly ExtendedPropertyDefinition ExtendedPropertyDef = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ClockWorkAppId", MapiPropertyType.Integer);

		// Token: 0x04000004 RID: 4
		private SyncOperationContext _opContext;

		// Token: 0x04000005 RID: 5
		private static PropertyDefinitionBase PROP_DEF_PidLidGlobalObjectId = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 3, MapiPropertyType.Binary);

		// Token: 0x04000006 RID: 6
		private static PropertyDefinitionBase uniqueId2Def = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 3, MapiPropertyType.Binary);

		// Token: 0x04000007 RID: 7
		private PropertySet _fullPropertySet_2007 = new PropertySet(new PropertyDefinitionBase[]
		{
			ItemSchema.Id,
			AppointmentSchema.AppointmentType,
			AppointmentSchema.Start,
			AppointmentSchema.End,
			ItemSchema.Body,
			AppointmentSchema.RequiredAttendees,
			AppointmentSchema.OptionalAttendees,
			AppointmentSchema.Resources,
			AppointmentSchema.Organizer,
			AppointmentSchema.IsCancelled,
			ItemSchema.Sensitivity,
			AppointmentSchema.Location,
			ItemSchema.Subject,
			ItemSchema.LastModifiedTime,
			AppointmentSchema.IsAllDayEvent,
			AppointmentSchema.IsRecurring,
			AppointmentSchema.ICalUid,
			AppointmentSchema.Recurrence,
			AppointmentSchema.FirstOccurrence,
			AppointmentSchema.LastOccurrence,
			AppointmentSchema.IsMeeting,
			ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
		});

		// Token: 0x04000008 RID: 8
		private PropertySet _fullPropertySet_2010 = new PropertySet(new PropertyDefinitionBase[]
		{
			ItemSchema.Id,
			AppointmentSchema.AppointmentType,
			AppointmentSchema.StartTimeZone,
			AppointmentSchema.Start,
			AppointmentSchema.End,
			ItemSchema.Body,
			AppointmentSchema.RequiredAttendees,
			AppointmentSchema.OptionalAttendees,
			AppointmentSchema.Resources,
			AppointmentSchema.Organizer,
			AppointmentSchema.IsCancelled,
			ItemSchema.Sensitivity,
			AppointmentSchema.Location,
			ItemSchema.Subject,
			ItemSchema.LastModifiedTime,
			AppointmentSchema.IsAllDayEvent,
			AppointmentSchema.IsRecurring,
			AppointmentSchema.ICalUid,
			AppointmentSchema.Recurrence,
			AppointmentSchema.FirstOccurrence,
			AppointmentSchema.LastOccurrence,
			AppointmentSchema.IsMeeting,
			ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
		});

		// Token: 0x04000009 RID: 9
		private PropertySet _nativePropertySet_2010 = new PropertySet(new PropertyDefinitionBase[]
		{
			ItemSchema.Id,
			AppointmentSchema.AppointmentType,
			AppointmentSchema.StartTimeZone,
			AppointmentSchema.Start,
			AppointmentSchema.End,
			ItemSchema.Body,
			AppointmentSchema.RequiredAttendees,
			AppointmentSchema.OptionalAttendees,
			AppointmentSchema.Resources,
			AppointmentSchema.Organizer,
			AppointmentSchema.IsCancelled,
			ItemSchema.Sensitivity,
			AppointmentSchema.Location,
			ItemSchema.Subject,
			ItemSchema.LastModifiedTime,
			ItemSchema.LastModifiedName,
			AppointmentSchema.IsAllDayEvent,
			AppointmentSchema.IsRecurring,
			AppointmentSchema.ICalUid,
			AppointmentSchema.Recurrence,
			AppointmentSchema.FirstOccurrence,
			AppointmentSchema.LastOccurrence,
			AppointmentSchema.IsMeeting,
			ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
		});

		// Token: 0x0400000A RID: 10
		private PropertySet _recPropertySetEx_2007 = new PropertySet(new PropertyDefinitionBase[]
		{
			ItemSchema.Id,
			AppointmentSchema.AppointmentType,
			AppointmentSchema.Start,
			AppointmentSchema.End,
			AppointmentSchema.Organizer,
			AppointmentSchema.IsCancelled,
			ItemSchema.Sensitivity,
			ItemSchema.Subject,
			ItemSchema.LastModifiedTime,
			AppointmentSchema.IsRecurring,
			AppointmentSchema.ICalUid,
			AppointmentSchema.Recurrence,
			AppointmentSchema.FirstOccurrence,
			AppointmentSchema.LastOccurrence,
			AppointmentSchema.ModifiedOccurrences,
			AppointmentSchema.DeletedOccurrences,
			ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
		});

		// Token: 0x0400000B RID: 11
		private PropertySet _recPropertySetEx_2010 = new PropertySet(new PropertyDefinitionBase[]
		{
			ItemSchema.Id,
			AppointmentSchema.AppointmentType,
			AppointmentSchema.StartTimeZone,
			AppointmentSchema.Start,
			AppointmentSchema.End,
			AppointmentSchema.Organizer,
			AppointmentSchema.IsCancelled,
			ItemSchema.Sensitivity,
			ItemSchema.Subject,
			ItemSchema.LastModifiedTime,
			AppointmentSchema.IsRecurring,
			AppointmentSchema.ICalUid,
			AppointmentSchema.Recurrence,
			AppointmentSchema.FirstOccurrence,
			AppointmentSchema.LastOccurrence,
			AppointmentSchema.ModifiedOccurrences,
			AppointmentSchema.DeletedOccurrences,
			ExchangeAppointmentDAO.PROP_DEF_PidLidGlobalObjectId
		});

		// Token: 0x0400000C RID: 12
		private PropertySet _cancelledPropertySet = new PropertySet(BasePropertySet.FirstClassProperties, new PropertyDefinitionBase[]
		{
			AppointmentSchema.IsCancelled
		});

		// Token: 0x0400000D RID: 13
		private PropertySet _deletePropertySet = new PropertySet(BasePropertySet.FirstClassProperties, new PropertyDefinitionBase[]
		{
			AppointmentSchema.IsMeeting
		});
	}
}
