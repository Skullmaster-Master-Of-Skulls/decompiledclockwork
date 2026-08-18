using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.ApplicationSyncFactories;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.AppointmentSync
{
	// Token: 0x02000133 RID: 307
	public class CalendarSyncManager : ICalendarSyncManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0005B11B File Offset: 0x0005931B
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0005B123 File Offset: 0x00059323
		private IAppointmentSyncMappingManager AppointmentSyncMappingManager { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0005B12C File Offset: 0x0005932C
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0005B134 File Offset: 0x00059334
		private IExternalAppointmentManager ExternalAppointmentManager { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0005B13D File Offset: 0x0005933D
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0005B145 File Offset: 0x00059345
		private IClockWorkSyncAppointmentManager ClockWorkSyncAppointmentManager { get; set; }

		// Token: 0x06000D2D RID: 3373 RVA: 0x0005B150 File Offset: 0x00059350
		public CalendarSyncManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.AppointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			this.ExternalAppointmentManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateExternalAppointmentManager();
			this.ClockWorkSyncAppointmentManager = new ClockWorkSyncAppointmentManager(this.OpContext);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0005B1A8 File Offset: 0x000593A8
		private IMailMergingEmailManager GetMailMergingEmailManager()
		{
			return new MailMergingEmailManager(this.OpContext);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0005B1C8 File Offset: 0x000593C8
		public void DoSlowSync(DateTime syncStart, DateTime syncEnd)
		{
			bool flag = !this.OpContext.SyncSettings.SyncIsActive;
			if (flag)
			{
				CWLogger.Logger.Info("Sync2:Sync aborted because sync is not marked as active in External Calendar Sync settings");
			}
			else
			{
				IApplicationSyncAdministrationManager applicationSyncAdministrationManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
				DateTime? dateTime;
				ProductLicenseState calendarSyncLicenseStatus = applicationSyncAdministrationManager.GetCalendarSyncLicenseStatus(out dateTime);
				bool flag2 = calendarSyncLicenseStatus != ProductLicenseState.Licensed;
				if (flag2)
				{
					CWLogger.Logger.Warn("********* NO VALID LICENCE KEY FOUND. SYNC IS NOT RUNNING ***********");
					try
					{
						MiscSafeManager miscSafeManager = new MiscSafeManager();
						string key = "Not valid license was found for calendar sync email date sent";
						string value = miscSafeManager.GetValue(key);
						DateTime dateTime2;
						bool flag3 = string.IsNullOrEmpty(value) || !DateTime.TryParse(value, out dateTime2) || dateTime2.Date <= DateTime.Today.AddDays(-1.0);
						if (flag3)
						{
							MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
							{
								Context = new MailMergeContext()
							};
							IMailMergingEmailManager mailMergingEmailManager = this.GetMailMergingEmailManager();
							TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.CLOCKWORKAPPOINTMENTSYNC_InvalidLicenseEmail);
							IEmailManager emailManager = new EmailManager(this.OpContext);
							TPMailResult tpmailResult = emailManager.SendEmail(message);
							miscSafeManager.Save(key, DateTime.Today.ToString());
						}
					}
					catch (Exception ex)
					{
					}
				}
				else
				{
					IApplicationSyncAdministrationManager applicationSyncAdministrationManager2 = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
					applicationSyncAdministrationManager2.FillUniqueId2FieldInDatabase();
					this.ValidateSyncUsers();
					this._DoSlowSync(syncStart, syncEnd);
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0005B33C File Offset: 0x0005953C
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0005B344 File Offset: 0x00059544
		public bool IsFastSyncRunning { get; private set; }

		// Token: 0x06000D32 RID: 3378 RVA: 0x0005B350 File Offset: 0x00059550
		public void DoFastSync()
		{
			try
			{
				this.IsFastSyncRunning = true;
				bool flag = !this.ExternalAppointmentManager.SupportsFastSync();
				if (flag)
				{
					CWLogger.Logger.Info("Sync:Fast sync is not supported in this version");
				}
				else
				{
					bool flag2 = !this.OpContext.SyncSettings.FastSyncIsActive;
					if (flag2)
					{
						CWLogger.Logger.Info("Sync:Sync aborted because sync is not marked as active in External Calendar Sync settings");
					}
					else
					{
						IApplicationSyncAdministrationManager applicationSyncAdministrationManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
						DateTime? dateTime;
						ProductLicenseState calendarSyncLicenseStatus = applicationSyncAdministrationManager.GetCalendarSyncLicenseStatus(out dateTime);
						bool flag3 = calendarSyncLicenseStatus != ProductLicenseState.Licensed;
						if (flag3)
						{
							try
							{
								MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
								{
									Context = new MailMergeContext()
								};
								IMailMergingEmailManager mailMergingEmailManager = this.GetMailMergingEmailManager();
								TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.CLOCKWORKAPPOINTMENTSYNC_InvalidLicenseEmail);
								IEmailManager emailManager = new EmailManager(this.OpContext);
								TPMailResult tpmailResult = emailManager.SendEmail(message);
								CWLogger.Logger.Warn("********* NO VALID LICENCE KEY FOUND. SYNC IS NOT RUNNING ***********");
							}
							catch (Exception ex)
							{
							}
						}
						else
						{
							CWLogger.Logger.Info("\r\n\r\n***************************\r\n** BEGIN FAST SYNC AT {0}\r\n***************************\r\n\r\n", DateTime.Now);
							this.ValidateSyncUsers();
							this._DoFastSync();
							CWLogger.Logger.Info("\r\n\r\n***************************\r\n** END FAST SYNC AT {0}\r\n***************************\r\n\r\n", DateTime.Now);
						}
					}
				}
			}
			finally
			{
				this.IsFastSyncRunning = false;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0005B4C8 File Offset: 0x000596C8
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0005B4D0 File Offset: 0x000596D0
		public bool IsSlowSyncRunning { get; private set; }

		// Token: 0x06000D35 RID: 3381 RVA: 0x0005B4DC File Offset: 0x000596DC
		public void DoSlowSync()
		{
			try
			{
				this.IsSlowSyncRunning = true;
				bool flag = !this.OpContext.SyncSettings.SyncIsActive;
				if (flag)
				{
					CWLogger.Logger.Info("Sync:Sync aborted because sync is not marked as active in External Calendar Sync settings");
				}
				else
				{
					IApplicationSyncAdministrationManager applicationSyncAdministrationManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
					DateTime? dateTime;
					ProductLicenseState calendarSyncLicenseStatus = applicationSyncAdministrationManager.GetCalendarSyncLicenseStatus(out dateTime);
					bool flag2 = calendarSyncLicenseStatus != ProductLicenseState.Licensed;
					if (flag2)
					{
						try
						{
							MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
							{
								Context = new MailMergeContext()
							};
							IMailMergingEmailManager mailMergingEmailManager = this.GetMailMergingEmailManager();
							TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.CLOCKWORKAPPOINTMENTSYNC_InvalidLicenseEmail);
							IEmailManager emailManager = new EmailManager(this.OpContext);
							TPMailResult tpmailResult = emailManager.SendEmail(message);
							CWLogger.Logger.Warn("********* NO VALID LICENCE KEY FOUND. SYNC IS NOT RUNNING ***********");
						}
						catch (Exception ex)
						{
						}
					}
					else
					{
						IApplicationSyncAdministrationManager applicationSyncAdministrationManager2 = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
						applicationSyncAdministrationManager2.FillUniqueId2FieldInDatabase();
						this.ValidateSyncUsers();
						DateTime date = DateTime.Now.Date;
						DateTime dateTime2 = date.AddDays((double)(this.OpContext.SyncSettings.SyncIntervalInDays * this.OpContext.SyncSettings.SyncIntervalCount));
						CWLogger.Logger.Info("\r\n\r\n\r\n\r\n\r\n*****************************\r\n*****************************\r\n**\r\n** Slow Sync Start at '{0}' for apps between '{1}' and '{2}'\r\n**\r\n*****************************\r\n*****************************\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd H:mm"), date.ToString("yyyy-MM-dd"), dateTime2.ToString("yyyy-MM-dd"));
						int syncIntervalInDays = this.OpContext.SyncSettings.SyncIntervalInDays;
						int syncIntervalCount = this.OpContext.SyncSettings.SyncIntervalCount;
						DateTime syncStart = date;
						DateTime syncEnd = syncStart.AddDays((double)syncIntervalInDays);
						for (int i = 0; i < syncIntervalCount; i++)
						{
							try
							{
								this._DoSlowSync(syncStart, syncEnd);
								syncStart = syncStart.AddDays((double)syncIntervalInDays);
								syncEnd = syncStart.AddDays((double)syncIntervalInDays);
							}
							catch (Exception exception)
							{
								CWLogger.Logger.ErrorException(string.Format("DoSlowSync: startDate = {0} endDate = {1}", syncStart.ToString("yyyy-MM-dd H:mm"), syncEnd.ToString("yyyy-MM-dd H:mm")), exception);
							}
						}
						CWLogger.Logger.Info("\r\n\r\n***************************\r\n** Slow Sync Ended at '{0}' for apps between '{1}' and '{2}'\r\n***************************\r\n\r\n", DateTime.Now, date.ToString("yyyy-MM-dd"), dateTime2.ToString("yyyy-MM-dd"));
					}
				}
			}
			finally
			{
				this.IsSlowSyncRunning = false;
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0005B778 File Offset: 0x00059978
		public IList<DuplicateAppointmentSyncMappingAction> MergeDuplicateMappingsOneClockWorkMultipleExternal(IList<DuplicateAppointmentSyncMapping> duplicateSets, bool doAction)
		{
			List<DuplicateAppointmentSyncMappingAction> list = new List<DuplicateAppointmentSyncMappingAction>();
			AppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			foreach (DuplicateAppointmentSyncMapping duplicateAppointmentSyncMapping in duplicateSets)
			{
				ClockWorkSyncAppointment clockWorkSyncAppointment = duplicateAppointmentSyncMapping.ClockWorkAppointments[0];
				bool flag = clockWorkSyncAppointment == null;
				if (!flag)
				{
					for (int i = 1; i < duplicateAppointmentSyncMapping.ExternalAppointments.Count; i++)
					{
						ExternalAppointment externalAppointment = duplicateAppointmentSyncMapping.ExternalAppointments[i];
						bool flag2 = externalAppointment == null;
						if (!flag2)
						{
							DuplicateAppointmentSyncMappingAction duplicateAppointmentSyncMappingAction = new DuplicateAppointmentSyncMappingAction
							{
								Action = eDuplicateAppointmentSyncMappingAction.DeletedExternalAppointment,
								UniqueId2 = externalAppointment.UniqueId2,
								UniqueId = externalAppointment.UniqueId,
								GlobalId = externalAppointment.LegacyGlobalAppointmentId,
								ClockWorkAppointmentId = clockWorkSyncAppointment.AppointmentId
							};
							DuplicateAppointmentSyncMappingAction duplicateAppointmentSyncMappingAction2 = new DuplicateAppointmentSyncMappingAction
							{
								Action = eDuplicateAppointmentSyncMappingAction.DeletedMapping,
								UniqueId = externalAppointment.UniqueId,
								UniqueId2 = externalAppointment.UniqueId2,
								GlobalId = externalAppointment.LegacyGlobalAppointmentId,
								ClockWorkAppointmentId = clockWorkSyncAppointment.AppointmentId
							};
							if (doAction)
							{
								try
								{
									bool flag3 = !string.IsNullOrEmpty(duplicateAppointmentSyncMappingAction.UniqueId2);
									if (flag3)
									{
										this.ExternalAppointmentManager.DeleteAppointment(new ExternalAppointmentId
										{
											UniqueId2 = duplicateAppointmentSyncMappingAction.UniqueId2,
											UniqueId = duplicateAppointmentSyncMappingAction.UniqueId,
											GlobalAppId = duplicateAppointmentSyncMappingAction.GlobalId
										});
										duplicateAppointmentSyncMappingAction.Completed = true;
										bool flag4 = externalAppointment != null && externalAppointment.Mapping != null;
										if (flag4)
										{
											appointmentSyncMappingManager.DeleteMapping(externalAppointment.Mapping);
										}
										duplicateAppointmentSyncMappingAction2.Completed = true;
									}
									else
									{
										duplicateAppointmentSyncMappingAction.ErrorMessage = "Empty action1.Uniqueid2";
									}
								}
								catch (Exception ex)
								{
									CWLogger.Logger.Error("CalendarSyncManager:MergeDuplicateMappingsOneClockWorkMultipleExternal:DoAction:{0}", ex.ToString());
									duplicateAppointmentSyncMappingAction.ErrorMessage = ex.ToString();
									duplicateAppointmentSyncMappingAction2.ErrorMessage = ex.ToString();
								}
							}
							list.Add(duplicateAppointmentSyncMappingAction);
							list.Add(duplicateAppointmentSyncMappingAction2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0005B9EC File Offset: 0x00059BEC
		public IList<DuplicateAppointmentSyncMapping> FindDuplicateMappingsOneClockWorkMultipleExternal(DateTime StartDate, DateTime EndDate)
		{
			AppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			ClockWorkSyncAppointmentManager clockWorkSyncAppointmentManager = new ClockWorkSyncAppointmentManager(this.OpContext);
			List<ClockWorkExternalAppMapping> list = (from m in appointmentSyncMappingManager.FindDuplicateMappingsOneClockWorkMultipleExternal(StartDate, EndDate)
			where m != null
			select m).ToList<ClockWorkExternalAppMapping>();
			CWLogger.Logger.Trace("CalendarSyncManager:FindDuplicateMappingsOneClockWorkMultipleExternal:Start={0}:End={1}:Count={2}", StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), list.Count.ToString());
			bool flag = list.Count < 1;
			IList<DuplicateAppointmentSyncMapping> result;
			if (flag)
			{
				result = new List<DuplicateAppointmentSyncMapping>();
			}
			else
			{
				foreach (ClockWorkExternalAppMapping clockWorkExternalAppMapping in list)
				{
					CWLogger.Logger.Debug("CalendarSyncManager:FindDuplicateMappingsOneClockWorkMultipleExternal:cwAppId={0}:uniqueid2={1}:uniqueid={2}:globalid={3}", new object[]
					{
						clockWorkExternalAppMapping.ClockWorkAppointmentId.ToString(),
						clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 ?? "NULL",
						clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId ?? "NULL",
						clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId ?? "NULL"
					});
				}
				List<DuplicateAppointmentSyncMapping> list2 = new List<DuplicateAppointmentSyncMapping>();
				int i = 0;
				while (i < list.Count)
				{
					try
					{
						CWLogger.Logger.Trace("i=" + i.ToString());
						ClockWorkExternalAppMapping clockWorkExternalAppMapping2 = list[i];
						int clockWorkAppointmentId = clockWorkExternalAppMapping2.ClockWorkAppointmentId;
						bool flag2 = clockWorkAppointmentId > 0;
						if (flag2)
						{
							int j;
							for (j = i + 1; j < list.Count; j++)
							{
								int clockWorkAppointmentId2 = list[j].ClockWorkAppointmentId;
								bool flag3 = clockWorkAppointmentId2 != clockWorkAppointmentId;
								if (flag3)
								{
									break;
								}
							}
							List<ExternalAppointment> list3 = new List<ExternalAppointment>();
							for (int k = i; k < j; k++)
							{
								string uniqueId2 = list[k].ExternalApplicationUniqueAppointmentId2;
								bool flag4 = !string.IsNullOrEmpty(uniqueId2) && list3.FirstOrDefault((ExternalAppointment g) => g.UniqueId2 != null && g.UniqueId2 == uniqueId2) == null;
								if (flag4)
								{
									ExternalAppointment externalAppointment = this.ExternalAppointmentManager.LoadAppointment(new ExternalAppointmentId
									{
										UniqueId2 = uniqueId2,
										UniqueId = list[k].ExternalApplicationUniqueAppointmentId,
										GlobalAppId = list[k].ExternalApplicationGlobalAppointmentId
									});
									bool flag5 = externalAppointment != null;
									if (flag5)
									{
										externalAppointment.Mapping = list[k];
										list3.Add(externalAppointment);
									}
									else
									{
										CWLogger.Logger.Trace("Unable to load external appointment:UniqueId2={0}:UniqueId={1}:GlobalId={2}", uniqueId2 ?? "NULL", list[k].ExternalApplicationUniqueAppointmentId ?? "NULL", list[k].ExternalApplicationGlobalAppointmentId ?? "NULL");
									}
								}
							}
							bool flag6 = list3.Count > 1;
							if (flag6)
							{
								ClockWorkSyncAppointment clockWorkSyncAppointment = clockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(clockWorkAppointmentId);
								bool flag7 = clockWorkSyncAppointment != null;
								if (flag7)
								{
									ExternalAppointment firstExternalApp = list3[0];
									IEnumerable<ExternalAppointment> duplicateExternalAppointmentsToDiscard = from extApp in list3
									where extApp != null && (firstExternalApp.StartDate != extApp.StartDate || firstExternalApp.EndDate != extApp.EndDate)
									select extApp;
									list2.Add(new DuplicateAppointmentSyncMapping
									{
										ClockWorkAppointments = new List<ClockWorkSyncAppointment>
										{
											clockWorkSyncAppointment
										},
										ExternalAppointments = (from g in list3
										where g != null && duplicateExternalAppointmentsToDiscard.FirstOrDefault((ExternalAppointment h) => h != null && h.UniqueId2 == g.UniqueId2) == null
										select g).ToList<ExternalAppointment>()
									});
								}
								else
								{
									CWLogger.Logger.Trace("Unable to load ClockWork Appointment:cwappid={0}", clockWorkAppointmentId);
								}
							}
							i = j;
						}
						else
						{
							i++;
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::FindDuplicateMappingsOneClockWorkMultipleExternal:: {0}", ex.ToString()), ex);
					}
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0005BE1C File Offset: 0x0005A01C
		public IList<DuplicateAppointmentSyncMapping> FindDuplicateMappingsOneExternalMultipleClockWork(DateTime StartDate, DateTime EndDate)
		{
			AppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			IList<ClockWorkExternalAppMapping> list = appointmentSyncMappingManager.FindDuplicateMappingsOneExternalMultipleClockWork(StartDate, EndDate);
			bool flag = list.Count < 1;
			IList<DuplicateAppointmentSyncMapping> result;
			if (flag)
			{
				CWLogger.Logger.Trace("CalendarSyncManager:FindDuplicateMappingsOneExternalMultipleClockWork:No duplicate mappings were found for {0} to {1}", StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"));
				result = new List<DuplicateAppointmentSyncMapping>();
			}
			else
			{
				List<DuplicateAppointmentSyncMapping> list2 = new List<DuplicateAppointmentSyncMapping>();
				int i = 0;
				ClockWorkSyncAppointmentManager clockWorkSyncAppointmentManager = new ClockWorkSyncAppointmentManager(this.OpContext);
				while (i < list.Count)
				{
					ClockWorkExternalAppMapping clockWorkExternalAppMapping = list[i];
					string externalApplicationUniqueAppointmentId = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2;
					bool flag2 = !string.IsNullOrEmpty(externalApplicationUniqueAppointmentId);
					if (flag2)
					{
						int j;
						for (j = i + 1; j < list.Count; j++)
						{
							string externalApplicationUniqueAppointmentId2 = list[j].ExternalApplicationUniqueAppointmentId2;
							bool flag3 = string.IsNullOrEmpty(externalApplicationUniqueAppointmentId2) || externalApplicationUniqueAppointmentId != externalApplicationUniqueAppointmentId2;
							if (flag3)
							{
								break;
							}
						}
						List<ClockWorkSyncAppointment> list3 = new List<ClockWorkSyncAppointment>();
						for (int k = i; k < j; k++)
						{
							int appId = list[k].ClockWorkAppointmentId;
							bool flag4 = appId > 0 && list3.FirstOrDefault((ClockWorkSyncAppointment g) => g.AppointmentId == appId) == null;
							if (flag4)
							{
								ClockWorkSyncAppointment clockWorkSyncAppointment = clockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(appId);
								bool flag5 = clockWorkSyncAppointment != null;
								if (flag5)
								{
									clockWorkSyncAppointment.Mapping = list[k];
									list3.Add(clockWorkSyncAppointment);
								}
							}
						}
						bool flag6 = list3.Count > 1;
						if (flag6)
						{
							ExternalAppointment extApp = this.ExternalAppointmentManager.LoadAppointment(new ExternalAppointmentId
							{
								UniqueId2 = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2,
								UniqueId = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId,
								GlobalAppId = clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId
							});
							bool flag7 = extApp != null;
							if (flag7)
							{
								IEnumerable<ClockWorkSyncAppointment> duplicateClockWorkAppointmentsToDiscard = from cwApp in list3
								where cwApp.StartDateTime != extApp.StartDate || cwApp.EndDateTime != extApp.EndDate
								select cwApp;
								list2.Add(new DuplicateAppointmentSyncMapping
								{
									ExternalAppointments = new List<ExternalAppointment>
									{
										extApp
									},
									ClockWorkAppointments = (from g in list3
									where duplicateClockWorkAppointmentsToDiscard.FirstOrDefault((ClockWorkSyncAppointment h) => h.AppointmentId == g.AppointmentId) == null
									select g).ToList<ClockWorkSyncAppointment>()
								});
							}
						}
						i = j;
					}
					else
					{
						i++;
					}
				}
				result = (from g in list2
				where g.ClockWorkAppointments.Count > 1 && g.ExternalAppointments.Count == 1
				select g).ToList<DuplicateAppointmentSyncMapping>();
			}
			return result;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0005C0D8 File Offset: 0x0005A2D8
		public IList<DuplicateAppointmentSyncMappingAction> MergeDuplicateMappingsOneExternalMultipleClockWork(IList<DuplicateAppointmentSyncMapping> duplicateSets, bool doAction)
		{
			List<DuplicateAppointmentSyncMappingAction> list = new List<DuplicateAppointmentSyncMappingAction>();
			ClockWorkSyncAppointmentManager clockWorkSyncAppointmentManager = new ClockWorkSyncAppointmentManager(this.OpContext);
			AppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			foreach (DuplicateAppointmentSyncMapping duplicateAppointmentSyncMapping in duplicateSets)
			{
				ExternalAppointment externalAppointment = duplicateAppointmentSyncMapping.ExternalAppointments[0];
				bool flag = externalAppointment == null;
				if (!flag)
				{
					DuplicateAppointmentSyncMappingAction duplicateAppointmentSyncMappingAction = new DuplicateAppointmentSyncMappingAction
					{
						Action = eDuplicateAppointmentSyncMappingAction.DeletedMapping,
						UniqueId = externalAppointment.UniqueId,
						UniqueId2 = externalAppointment.UniqueId2,
						GlobalId = externalAppointment.LegacyGlobalAppointmentId
					};
					for (int i = 1; i < duplicateAppointmentSyncMapping.ClockWorkAppointments.Count; i++)
					{
						ClockWorkSyncAppointment clockWorkSyncAppointment = duplicateAppointmentSyncMapping.ClockWorkAppointments[i];
						bool flag2 = clockWorkSyncAppointment == null;
						if (!flag2)
						{
							DuplicateAppointmentSyncMappingAction duplicateAppointmentSyncMappingAction2 = new DuplicateAppointmentSyncMappingAction
							{
								Action = eDuplicateAppointmentSyncMappingAction.CancelledClockWorkAppointment,
								ClockWorkAppointmentId = clockWorkSyncAppointment.AppointmentId
							};
							if (doAction)
							{
								try
								{
									bool flag3 = duplicateAppointmentSyncMappingAction2.ClockWorkAppointmentId > 0;
									if (flag3)
									{
										clockWorkSyncAppointmentManager.CancelClockWorkSyncAppointment(false, duplicateAppointmentSyncMappingAction2.ClockWorkAppointmentId);
										duplicateAppointmentSyncMappingAction2.Completed = true;
										bool flag4 = clockWorkSyncAppointment != null && clockWorkSyncAppointment.Mapping != null;
										if (flag4)
										{
											appointmentSyncMappingManager.DeleteMapping(clockWorkSyncAppointment.Mapping);
										}
										duplicateAppointmentSyncMappingAction.Completed = true;
									}
									else
									{
										duplicateAppointmentSyncMappingAction2.ErrorMessage = "ClockWorkAppointmentId <= 0";
									}
								}
								catch (Exception ex)
								{
									CWLogger.Logger.Error("CalendarSyncManager:MergeDuplicateMappingsOneExternalMultipleClockWork:DoAction:{0}", ex.ToString());
									duplicateAppointmentSyncMappingAction2.ErrorMessage = ex.ToString();
									duplicateAppointmentSyncMappingAction.ErrorMessage = ex.ToString();
								}
							}
							list.Add(duplicateAppointmentSyncMappingAction2);
							list.Add(duplicateAppointmentSyncMappingAction);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0005C2F0 File Offset: 0x0005A4F0
		private void _DoFastSync()
		{
			IMiscSafeManager miscSafeManager = new MiscSafeManager();
			int num = 0;
			foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser in this.OpContext.SyncSettings.SyncUsers)
			{
				try
				{
					string key = string.Format("ExSyncState:{0}", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					string key2 = string.Format("CWSyncState:{0}", clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					string value = miscSafeManager.GetValue(key);
					string value2 = miscSafeManager.GetValue(key2);
					DateTime? dateTime = null;
					DateTime value3;
					bool flag = DateTime.TryParse(value2, out value3);
					if (flag)
					{
						dateTime = new DateTime?(value3);
					}
					CWLogger.Logger.Info("**** Begin Fast Sync for User={0}({1}) ****", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername ?? string.Empty, clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					CWLogger.Logger.Info("FastSync({0}): Loading External Calendar app changes ...", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					ExternalSyncAppointmentChangesResponse exSyncAppointmentChangesResp = this.ExternalAppointmentManager.LoadAppointmentChanges(new ExternalSyncAppointmentChangesRequest
					{
						Username = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername,
						SyncState = value,
						LastSyncDateTime = dateTime
					});
					bool skipPrivateAppointments = this.OpContext.SyncSettings.SkipPrivateAppointments;
					if (skipPrivateAppointments)
					{
						exSyncAppointmentChangesResp.AppointmentChanges = (from g in exSyncAppointmentChangesResp.AppointmentChanges
						where !g.IsPrivate
						select g).ToList<ExternalSyncAppointmentChange>();
					}
					bool skipAllDayAppointments = this.OpContext.SyncSettings.SkipAllDayAppointments;
					if (skipAllDayAppointments)
					{
						exSyncAppointmentChangesResp.AppointmentChanges = (from g in exSyncAppointmentChangesResp.AppointmentChanges
						where !g.IsAllDayEvent
						select g).ToList<ExternalSyncAppointmentChange>();
					}
					CWLogger.Logger.Info("FastSync({1}): {0} External Calendar app changes.", exSyncAppointmentChangesResp.AppointmentChanges.Count, clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					CWLogger.Logger.Debug("FastSync({1}): {0} External Calendar app changes.", exSyncAppointmentChangesResp.AppointmentChanges.Count, clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					foreach (ExternalSyncAppointmentChange externalSyncAppointmentChange in exSyncAppointmentChangesResp.AppointmentChanges)
					{
						CWLogger logger = CWLogger.Logger;
						string format = "FastSync({0}): External app changed: change={1}, exappuniqueid={2}, exappuniqueid2={3}, lastmodifieddate={4}, cwappid={5}";
						object[] array = new object[6];
						array[0] = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername;
						array[1] = externalSyncAppointmentChange.AppointmentSyncChangeType;
						array[2] = externalSyncAppointmentChange.ExternalAppointmentID.UniqueId;
						array[3] = externalSyncAppointmentChange.ExternalAppointmentID.UniqueId2;
						array[4] = externalSyncAppointmentChange.LastModifiedDate;
						int num2 = 5;
						ClockWorkExternalAppMapping mapping = externalSyncAppointmentChange.Mapping;
						array[num2] = ((mapping != null) ? mapping.ClockWorkAppointmentId : 0);
						logger.Debug(string.Format(format, array));
					}
					CWLogger.Logger.Info("FastSync({0}): Loading ClockWork Calendar app changes ...", clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					ClockWorkSyncAppointmentChangeResponse clockWorkSyncAppointmentChangeResponse = this.ClockWorkSyncAppointmentManager.LoadAppointmentChanges(new ClockWorkSyncAppointmentChangeRequest
					{
						ClockWorkPersonId = clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId,
						ClockWorkSyncState = dateTime
					});
					bool skipPrivateAppointments2 = this.OpContext.SyncSettings.SkipPrivateAppointments;
					if (skipPrivateAppointments2)
					{
						clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges = (from g in clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges
						where !g.IsPrivate
						select g).ToList<ClockWorkSyncAppointmentChange>();
					}
					bool skipAllDayAppointments2 = this.OpContext.SyncSettings.SkipAllDayAppointments;
					if (skipAllDayAppointments2)
					{
						clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges = (from g in clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges
						where !g.IsAllDayEvent
						select g).ToList<ClockWorkSyncAppointmentChange>();
					}
					CWLogger.Logger.Info("FastSync({1}): {0} ClockWork Calendar app changes.", clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges.Count, clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					CWLogger.Logger.Debug("FastSync({1}): {0} ClockWork Calendar app changes.", clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges.Count, clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					foreach (ClockWorkSyncAppointmentChange clockWorkSyncAppointmentChange in clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges)
					{
						CWLogger logger2 = CWLogger.Logger;
						string format2 = "FastSync({0}): ClockWork app changed: change={1}, appid={2}, lastmodifieddate={3}, exappuniqueid={4}, exappuniqueid2 ={5}";
						object[] array2 = new object[6];
						array2[0] = clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId;
						array2[1] = clockWorkSyncAppointmentChange.AppointmentSyncChangeType;
						array2[2] = clockWorkSyncAppointmentChange.ClockWorkAppointmentID;
						array2[3] = clockWorkSyncAppointmentChange.LastModifiedDate;
						int num3 = 4;
						ClockWorkExternalAppMapping mapping2 = clockWorkSyncAppointmentChange.Mapping;
						array2[num3] = (((mapping2 != null) ? mapping2.ExternalApplicationUniqueAppointmentId : null) ?? "NULL");
						int num4 = 5;
						ClockWorkExternalAppMapping mapping3 = clockWorkSyncAppointmentChange.Mapping;
						array2[num4] = (((mapping3 != null) ? mapping3.ExternalApplicationUniqueAppointmentId2 : null) ?? "NULL");
						logger2.Debug(string.Format(format2, array2));
					}
					CWLogger.Logger.Info("FastSync({0}): Figure out Sync actions ...", clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId);
					List<CalendarSyncManager.SyncAppointmentChangePair> list = (from c in clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges
					select new CalendarSyncManager.SyncAppointmentChangePair
					{
						ClockWorkSyncAppointmentChange = c,
						ExternalSyncAppointmentChange = exSyncAppointmentChangesResp.AppointmentChanges.FirstOrDefault((ExternalSyncAppointmentChange e) => e.Mapping != null && e.Mapping.ClockWorkAppointmentId == c.ClockWorkAppointmentID)
					} into c
					where c.ExternalSyncAppointmentChange != null
					select c).ToList<CalendarSyncManager.SyncAppointmentChangePair>();
					List<int> commonChangesCwAppIds = (from c in list
					select c.ClockWorkSyncAppointmentChange.ClockWorkAppointmentID).ToList<int>();
					List<ClockWorkSyncAppointmentChange> clockWorkSyncAppointments = (from c in clockWorkSyncAppointmentChangeResponse.ClockWorkAppointmentChanges
					where !commonChangesCwAppIds.Contains(c.ClockWorkAppointmentID)
					select c).ToList<ClockWorkSyncAppointmentChange>();
					List<ExternalSyncAppointmentChange> externalSyncAppointments = (from e in exSyncAppointmentChangesResp.AppointmentChanges
					where e.Mapping == null || !commonChangesCwAppIds.Contains(e.Mapping.ClockWorkAppointmentId)
					select e).ToList<ExternalSyncAppointmentChange>();
					List<ClockWorkExternalApplicationSyncAction> list2 = new List<ClockWorkExternalApplicationSyncAction>();
					this.PreProcessChangesList(list, ref clockWorkSyncAppointments, ref externalSyncAppointments);
					this.FigureOutFastSyncActionsForExternalCalendar(clockWorkExternalApplicationSyncUser, ref list2, clockWorkSyncAppointments, externalSyncAppointments);
					this.FigureOutFastSyncActionsForClockWorkCalendar(ref list2, clockWorkSyncAppointments, externalSyncAppointments);
					bool flag2 = list2.Count > 0;
					if (flag2)
					{
						CWLogger.Logger.Info("*-*-*-* Begin Execute Sync Actions for user {0}: actions={1} *-*-*-*", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername ?? string.Empty, list2.Count.ToString());
						this.ExecuteSyncActions(list2);
						CWLogger.Logger.Info("*-*-*-* End Execute Sync Actions for User={0}: actions={1} *-*-*-*", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername ?? string.Empty, list2.Count.ToString());
					}
					CWLogger.Logger.Info("**** End Fast Sync for User={0}({1}): actions={2} ****", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername ?? string.Empty, clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId, list2.Count.ToString());
					CWLogger.Logger.Info("FastSync: {0} out of {1} users completed", ++num, this.OpContext.SyncSettings.SyncUsers.Count);
					miscSafeManager.Save(key2, clockWorkSyncAppointmentChangeResponse.ClockWorkSyncState.ToString("yyyy-MM-dd H:mm:ss tt"));
					miscSafeManager.Save(key, exSyncAppointmentChangesResp.SyncState);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0005CA9C File Offset: 0x0005AC9C
		private void _DoSlowSync(DateTime syncStart, DateTime syncEnd)
		{
			CWLogger.Logger.Info("*** Slow Sync apps between '{0}' and '{1}' ***", syncStart.ToString("yyyy-MM-dd"), syncEnd.ToString("yyyy-MM-dd"));
			List<ClockWorkExternalApplicationSyncUser> source;
			List<ExternalAppointment> list = this.LoadExternalAppointments(syncStart, syncEnd, out source);
			CWLogger.Logger.Info("Sync:Loaded {0} External Calendar appointment(s).", list.Count.ToString());
			bool skipPrivateAppointments = this.OpContext.SyncSettings.SkipPrivateAppointments;
			if (skipPrivateAppointments)
			{
				list = (from g in list
				where !g.IsPrivate
				select g).ToList<ExternalAppointment>();
			}
			List<ClockWorkSyncAppointment> list2;
			try
			{
				list2 = this.LoadClockWorkAppointments((from su in source
				where su != null && su.ClockWorkUser != null
				select su).ToList<ClockWorkExternalApplicationSyncUser>().ConvertAll<int>((ClockWorkExternalApplicationSyncUser su) => su.ClockWorkUser.PersonId), syncStart, syncEnd, false);
				CWLogger.Logger.Info("Sync:Loaded {0} ClockWork appointment(s).", list2.Count.ToString());
				bool skipPrivateAppointments2 = this.OpContext.SyncSettings.SkipPrivateAppointments;
				if (skipPrivateAppointments2)
				{
					list2 = (from g in list2
					where !g.IsPrivate
					select g).ToList<ClockWorkSyncAppointment>();
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::_DoSlowSync:: Loading ClockWork Appointments failed from '{0}' to '{1}'.\n{2}", syncStart.ToString("MMM dd, yyyy hh:mm:ss tt"), syncEnd.ToString("MMM dd, yyyy hh:mm:ss tt"), ex.ToString()), ex);
				return;
			}
			bool skipAllDayAppointments = this.OpContext.SyncSettings.SkipAllDayAppointments;
			if (skipAllDayAppointments)
			{
				try
				{
					list = list.FindAll((ExternalAppointment f) => !f.IsAllDayEvent);
				}
				catch (Exception ex2)
				{
					CWLogger.Logger.Error("Trying to apply all day app skip:{0}", ex2.ToString());
				}
			}
			using (List<ClockWorkSyncAppointment>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClockWorkSyncAppointment app = enumerator.Current;
					bool flag = app.Mapping == null || !app.Mapping.GetIsMappingAvailable();
					if (flag)
					{
						app.Mapping = this.AppointmentSyncMappingManager.LoadMappingByClockWorkAppointmentId(app.AppointmentId);
					}
					ExternalAppointment externalAppointment = list.FirstOrDefault((ExternalAppointment exapp) => exapp.Mapping != null && exapp.Mapping.ClockWorkAppointmentId == app.AppointmentId);
					bool flag2 = externalAppointment != null;
					if (flag2)
					{
						bool flag3 = app.Mapping == null;
						if (flag3)
						{
							app.Mapping = new ClockWorkExternalAppMapping
							{
								ClockWorkAppointmentId = app.AppointmentId,
								ClockWorkLastUpdatedDate = new DateTime?(app.LastModifiedTime)
							};
						}
						app.Mapping.ExternalApplicationUniqueAppointmentId = externalAppointment.Mapping.ExternalApplicationUniqueAppointmentId;
						app.Mapping.ExternalApplicationUniqueAppointmentId2 = externalAppointment.Mapping.ExternalApplicationUniqueAppointmentId2;
						app.Mapping.ExternalApplicationGlobalAppointmentId = externalAppointment.Mapping.ExternalApplicationGlobalAppointmentId;
					}
				}
			}
			using (List<ExternalAppointment>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ExternalAppointment app = enumerator2.Current;
					ClockWorkSyncAppointment clockWorkSyncAppointment = list2.FirstOrDefault((ClockWorkSyncAppointment cwapp) => this.ExternalAppointmentManager.AppointmentsAreEqual(cwapp, app));
					bool flag4 = clockWorkSyncAppointment != null;
					if (flag4)
					{
						app.Mapping = clockWorkSyncAppointment.Mapping;
					}
					else
					{
						app.Mapping = this.AppointmentSyncMappingManager.LoadMappingByExternalId(app);
					}
					bool flag5 = (app.Mapping == null || app.Mapping.ClockWorkAppointmentId < 1) && !string.IsNullOrEmpty(app.LegacyGlobalAppointmentId);
					if (flag5)
					{
						string oldUniqueId = this.AppointmentSyncMappingManager.LoadUniqueIdByGlobalAppointmentId(app);
						bool flag6 = !string.IsNullOrEmpty(oldUniqueId);
						if (flag6)
						{
							List<ClockWorkSyncAppointment> list3 = list2.FindAll((ClockWorkSyncAppointment cwapp) => cwapp.Mapping != null && cwapp.Mapping.ExternalApplicationUniqueAppointmentId.Equals(oldUniqueId, StringComparison.OrdinalIgnoreCase));
							foreach (ClockWorkSyncAppointment clockWorkSyncAppointment2 in list3)
							{
								clockWorkSyncAppointment2.Mapping.ExternalApplicationUniqueAppointmentId = app.UniqueId;
							}
							List<ExternalAppointment> list4 = list.FindAll((ExternalAppointment oa) => oa.UniqueId.Equals(oldUniqueId, StringComparison.OrdinalIgnoreCase));
							foreach (ExternalAppointment externalAppointment2 in list4)
							{
								externalAppointment2.UniqueId = app.UniqueId;
							}
							list4 = list.FindAll((ExternalAppointment oa) => oa.Mapping != null && oa.Mapping.ExternalApplicationUniqueAppointmentId.Equals(oldUniqueId, StringComparison.OrdinalIgnoreCase));
							foreach (ExternalAppointment externalAppointment3 in list4)
							{
								externalAppointment3.Mapping.ExternalApplicationUniqueAppointmentId = app.UniqueId;
							}
							this.AppointmentSyncMappingManager.UpdateMappingsLookupTable(oldUniqueId, app.UniqueId);
							this.AppointmentSyncMappingManager.UpdateMappingsTable(oldUniqueId, app.UniqueId);
							CWLogger.Logger.Debug("CalendarSyncManager::_DoSlowSync:Figure it out external calendar mappings:UpdatedUniqueidInMapping:olduniqueid={0}:newuniqueid={1}", (oldUniqueId == null) ? "NULL" : oldUniqueId, (app == null) ? "NULL APP" : app.UniqueId.ToString());
						}
					}
				}
			}
			string arg = string.Join("\r\n* ", list2.ConvertAll<string>(delegate(ClockWorkSyncAppointment cwa)
			{
				string format = "{0}-{1}-{2}-attendees={3}";
				object[] array = new object[4];
				array[0] = cwa.StartDateTime.ToString("yyyy-MM-dd H:mm");
				array[1] = cwa.GetTitleAndSubTitle();
				array[2] = cwa.AppointmentId.ToString();
				int num = 3;
				object obj;
				if (cwa != null && cwa.Attendees != null)
				{
					obj = string.Join(", ", cwa.Attendees.ConvertAll<string>((ClockWorkSyncAttendee catt) => string.Format("{0}[{1}]", catt.Attendee.FirstName, catt.Attendee.PersonId.ToString())).ToArray());
				}
				else
				{
					obj = "NULL";
				}
				array[num] = obj;
				return string.Format(format, array);
			}).ToArray());
			string arg2 = string.Join("\r\n* ", list.ConvertAll<string>(delegate(ExternalAppointment owa)
			{
				string format = "{0}-{1}-{2}-{3}-attendees={4}";
				object[] array = new object[5];
				array[0] = owa.StartDate.ToString("yyyy-MM-dd H:mm");
				array[1] = (owa.Subject ?? "NULL");
				array[2] = (owa.UniqueId ?? "NULL");
				array[3] = (owa.LegacyGlobalAppointmentId ?? "NULL");
				int num = 4;
				object obj;
				if (owa != null && owa.Attendees != null)
				{
					obj = string.Join(", ", owa.Attendees.ToList<ExternalAttendee>().ConvertAll<string>((ExternalAttendee oatt) => string.Format("{0}[{1}]", oatt.Name ?? "NULL", oatt.Username ?? "NULL")).ToArray());
				}
				else
				{
					obj = "NULL";
				}
				array[num] = obj;
				return string.Format(format, array);
			}).ToArray());
			CWLogger.Logger.Trace("***\r\nDoSlowSync:PreFigureOutSyncActions\r\nClockWork Appointments\r\n======================\r\n{0}\r\n\r\nExternal Calendar Appointments\r\n====================\r\n{1}", arg, arg2);
			List<ClockWorkExternalApplicationSyncAction> list5 = new List<ClockWorkExternalApplicationSyncAction>();
			this.FigureOutSyncActionsForClockWork(ref list5, list2, list);
			this.FigureOutSyncActionsForExternalCalendar(ref list5, list2, list);
			CWLogger.Logger.Debug("CalendarSyncManager::_DoSlowSync::syncActions::count={0}", list5.Count);
			foreach (ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction in list5)
			{
				CWLogger.Logger.Debug("CalendarSyncManager::_DoSlowSync::syncActions::syncAction={0}, cwappId={1}, outlookAppId={2}", Enum.GetName(typeof(eClockWorkExternalApplicationSyncActionType), clockWorkExternalApplicationSyncAction.SyncAction), (clockWorkExternalApplicationSyncAction.ClockWorkAppointment != null) ? clockWorkExternalApplicationSyncAction.ClockWorkAppointment.AppointmentId.ToString() : "NULL", (clockWorkExternalApplicationSyncAction.ExternalAppointment != null) ? clockWorkExternalApplicationSyncAction.ExternalAppointment.UniqueId : "NULL");
			}
			this.ExecuteSyncActions(list5);
			CWLogger.Logger.Info("*** Slow Sync completed for apps between '{0}' and '{1}': actions={2} ***", syncStart.ToString("yyyy-MM-dd"), syncEnd.ToString("yyyy-MM-dd"), list5.Count.ToString());
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0005D304 File Offset: 0x0005B504
		private void ValidateSyncUsers()
		{
			List<ClockWorkExternalApplicationSyncUser> syncUsers = this.OpContext.SyncSettings.SyncUsers;
			List<ClockWorkExternalApplicationSyncUser> disabledSyncUsers = this.OpContext.SyncSettings.DisabledSyncUsers;
			IApplicationSyncAdministrationManager syncAdministrationManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateApplicationSyncAdministrationManager();
			List<ClockWorkExternalApplicationSyncUser> list = (from u in syncUsers
			where !u.ExternalApplicationUsername.Equals(this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username, StringComparison.OrdinalIgnoreCase) && (syncAdministrationManager.GetDelegatePermissionLevel(u.ExternalApplicationUsername) & (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write)) != (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write)
			select u).ToList<ClockWorkExternalApplicationSyncUser>();
			bool flag = list.Count > 0;
			if (flag)
			{
				disabledSyncUsers.AddRange(list);
				foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser in list)
				{
					syncUsers.Remove(clockWorkExternalApplicationSyncUser);
					CWLogger.Logger.Info("Removing user '{0}' from sync users list because it does not have read|write delegate permissions", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
				}
			}
			list.Clear();
			LicenseKeyInfo calendarSyncProductKey = syncAdministrationManager.GetCalendarSyncProductKey();
			bool flag2 = syncUsers.Count > calendarSyncProductKey.NLicenses;
			if (flag2)
			{
				for (int i = calendarSyncProductKey.NLicenses; i < syncUsers.Count; i++)
				{
					ClockWorkExternalApplicationSyncUser item = syncUsers[i];
					list.Add(item);
				}
			}
			bool flag3 = list.Count > 0;
			if (flag3)
			{
				int count = syncUsers.Count;
				CWLogger.Logger.Warn("********** Removing '{0}' users from sync users list because exceeding total number of sync licences **********", list.Count);
				MiscSafeManager miscSafeManager = new MiscSafeManager();
				disabledSyncUsers.AddRange(list);
				foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser2 in list)
				{
					syncUsers.Remove(clockWorkExternalApplicationSyncUser2);
					CWLogger.Logger.Info("Removing user '{0}' from sync users list because exceeding total number of sync licences", clockWorkExternalApplicationSyncUser2.ExternalApplicationUsername);
					CWLogger.Logger.Warn("Removing user '{0}'", clockWorkExternalApplicationSyncUser2.ExternalApplicationUsername);
					string key = string.Format("Exceeded number of licence email date sent for user {0}", clockWorkExternalApplicationSyncUser2.ClockWorkUser.PersonId);
					string value = miscSafeManager.GetValue(key);
					DateTime dateTime;
					bool flag4 = string.IsNullOrEmpty(value) || !DateTime.TryParse(value, out dateTime) || dateTime.Date <= DateTime.Today.AddDays(-7.0);
					if (flag4)
					{
						try
						{
							MailMergeContextWithCustomDictionary contextWithCustomDictionary = new MailMergeContextWithCustomDictionary
							{
								Context = new MailMergeContext
								{
									PersonId = clockWorkExternalApplicationSyncUser2.ClockWorkUser.PersonId
								},
								CustomDictionary = new MailMergeCustomDictionary
								{
									Args = new Dictionary<string, string>
									{
										{
											"syncuseremail",
											clockWorkExternalApplicationSyncUser2.ExternalApplicationUsername.Trim().ToLower()
										},
										{
											"currentsyncusers",
											count.ToString()
										},
										{
											"totalsyncuserlicences",
											calendarSyncProductKey.NLicenses.ToString()
										}
									}
								}
							};
							IMailMergingEmailManager mailMergingEmailManager = this.GetMailMergingEmailManager();
							TPMailMessage message = mailMergingEmailManager.MailMerge(contextWithCustomDictionary, Setting.CLOCKWORKAPPOINTMENTSYNC_ExceededTotalSyncUserLicencesEmail);
							IEmailManager emailManager = new EmailManager(this.OpContext);
							TPMailResult tpmailResult = emailManager.SendEmail(message);
							miscSafeManager.Save(key, DateTime.Today.ToString());
						}
						catch (Exception ex)
						{
						}
					}
				}
				CWLogger.Logger.Warn("********** End of Removing users from sync users list because exceeding total sync licences **********");
			}
			this.OpContext.SyncSettings.SyncUsers = syncUsers;
			this.OpContext.SyncSettings.DisabledSyncUsers = disabledSyncUsers;
			CWLogger.Logger.Info("************ Sync Users list **************");
			foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser3 in syncUsers)
			{
				CWLogger.Logger.Info("    - External Username={0}, ClockWork Username={1}({2}), Is Enable to Sync=True", new object[]
				{
					clockWorkExternalApplicationSyncUser3.ExternalApplicationUsername ?? string.Empty,
					(clockWorkExternalApplicationSyncUser3.ClockWorkUser != null) ? (clockWorkExternalApplicationSyncUser3.ClockWorkUser.Student_no ?? string.Empty) : string.Empty,
					(clockWorkExternalApplicationSyncUser3.ClockWorkUser != null) ? clockWorkExternalApplicationSyncUser3.ClockWorkUser.PersonId : 0,
					clockWorkExternalApplicationSyncUser3.SyncIsEnabled.ToString()
				});
			}
			foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser4 in disabledSyncUsers)
			{
				CWLogger.Logger.Info("    - External Username={0}, ClockWork Username={1}({2}), Is Enable to Sync=False", new object[]
				{
					clockWorkExternalApplicationSyncUser4.ExternalApplicationUsername ?? string.Empty,
					(clockWorkExternalApplicationSyncUser4.ClockWorkUser != null) ? (clockWorkExternalApplicationSyncUser4.ClockWorkUser.Student_no ?? string.Empty) : string.Empty,
					(clockWorkExternalApplicationSyncUser4.ClockWorkUser != null) ? clockWorkExternalApplicationSyncUser4.ClockWorkUser.PersonId : 0,
					clockWorkExternalApplicationSyncUser4.SyncIsEnabled.ToString()
				});
			}
			CWLogger.Logger.Info("************ End of Sync Users list **************");
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0005D874 File Offset: 0x0005BA74
		private int GetClockWorkAppointmentIdFromSyncAction(ClockWorkExternalApplicationSyncAction syncAction)
		{
			bool flag = syncAction.ClockWorkAppointment != null && syncAction.ClockWorkAppointment.AppointmentId > 0;
			int result;
			if (flag)
			{
				result = syncAction.ClockWorkAppointment.AppointmentId;
			}
			else
			{
				bool flag2 = syncAction.ExternalAppointment != null && syncAction.ExternalAppointment.Mapping != null;
				if (flag2)
				{
					result = syncAction.ExternalAppointment.Mapping.ClockWorkAppointmentId;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0005D8E4 File Offset: 0x0005BAE4
		private bool AddClockWorkSyncAction(ref List<ClockWorkExternalApplicationSyncAction> syncActions, ClockWorkExternalApplicationSyncAction newAction)
		{
			int cwAppId = this.GetClockWorkAppointmentIdFromSyncAction(newAction);
			ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == newAction.SyncAction && cwAppId > 0 && this.GetClockWorkAppointmentIdFromSyncAction(g) == cwAppId);
			bool flag = clockWorkExternalApplicationSyncAction != null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				syncActions.Add(newAction);
				CWLogger.Logger.Debug("AddedClockWorkSyncAction:SynAction={0}:CW={1}:Ext={2}:TotCount={3}", new object[]
				{
					newAction.SyncAction.ToString(),
					(newAction.ClockWorkAppointment == null) ? "NULL" : cwAppId.ToString(),
					(newAction.ExternalAppointment == null) ? "NULL" : string.Concat(new string[]
					{
						"uniqueid=",
						newAction.ExternalAppointment.UniqueId ?? "null",
						";uniqueid2=",
						newAction.ExternalAppointment.UniqueId2 ?? "null",
						";global=",
						newAction.ExternalAppointment.LegacyGlobalAppointmentId ?? "null"
					}),
					syncActions.Count.ToString()
				});
				result = true;
			}
			return result;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0005DA44 File Offset: 0x0005BC44
		private bool AddExternalSyncAction(ref List<ClockWorkExternalApplicationSyncAction> syncActions, ClockWorkExternalApplicationSyncAction newAction)
		{
			ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == newAction.SyncAction && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, newAction.ExternalAppointment));
			bool flag = clockWorkExternalApplicationSyncAction != null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				syncActions.Add(newAction);
				CWLogger.Logger.Debug("AddedClockWorkSyncAction:SynAction={0}:CW={1}:Ext={2}:TotCount={3}", new object[]
				{
					newAction.SyncAction.ToString(),
					(newAction.ClockWorkAppointment == null) ? "NULL" : newAction.ClockWorkAppointment.AppointmentId.ToString(),
					(newAction.ExternalAppointment == null) ? "NULL" : string.Concat(new string[]
					{
						"uniqueid=",
						newAction.ExternalAppointment.UniqueId ?? "null",
						";uniqueid2=",
						newAction.ExternalAppointment.UniqueId2 ?? "null",
						";global=",
						newAction.ExternalAppointment.LegacyGlobalAppointmentId ?? "null"
					}),
					syncActions.Count.ToString()
				});
				result = true;
			}
			return result;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0005DBA0 File Offset: 0x0005BDA0
		private void FigureOutSyncActionsForExternalCalendar(ref List<ClockWorkExternalApplicationSyncAction> syncActions, List<ClockWorkSyncAppointment> clockWorkApps, List<ExternalAppointment> externalCalendarApps)
		{
			using (List<ExternalAppointment>.Enumerator enumerator = externalCalendarApps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExternalAppointment app = enumerator.Current;
					try
					{
						bool flag = app.Mapping != null && app.Mapping.GetIsMappingAvailable();
						if (flag)
						{
							ClockWorkSyncAppointment matchingClockWorkAppointment = clockWorkApps.Find((ClockWorkSyncAppointment ca) => ca.AppointmentId == app.Mapping.ClockWorkAppointmentId);
							bool flag2 = matchingClockWorkAppointment == null;
							if (flag2)
							{
								matchingClockWorkAppointment = this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(app.Mapping.ClockWorkAppointmentId);
							}
							bool flag3 = matchingClockWorkAppointment == null;
							if (flag3)
							{
								bool flag4 = !app.IsCancelled;
								if (flag4)
								{
									ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction sa) => sa.SyncAction == eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment && sa.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(sa.ExternalAppointment, app));
									bool flag5 = clockWorkExternalApplicationSyncAction == null;
									if (flag5)
									{
										this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment,
											ExternalAppointment = app,
											ClockWorkAppointment = null
										});
									}
								}
							}
							else
							{
								ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction2 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction sa) => sa.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(sa.ExternalAppointment, app));
								bool flag6 = clockWorkExternalApplicationSyncAction2 == null;
								if (flag6)
								{
									clockWorkExternalApplicationSyncAction2 = syncActions.Find((ClockWorkExternalApplicationSyncAction sa) => sa.ClockWorkAppointment != null && sa.ClockWorkAppointment.AppointmentId == matchingClockWorkAppointment.AppointmentId);
								}
								bool flag7 = clockWorkExternalApplicationSyncAction2 == null;
								if (flag7)
								{
									eClockWorkExternalApplicationAppointmentCompareResult eClockWorkExternalApplicationAppointmentCompareResult = this.ClockWorkSyncAppointmentManager.CheckAppointmentDiff(app, matchingClockWorkAppointment);
									CWLogger.Logger.Debug("CalendarSyncManager::FigureOutSyncActionsForOutlook::AppDiff = {0}, cwAppId = {1}, outlookAppId = {2}", Enum.GetName(typeof(eClockWorkExternalApplicationAppointmentCompareResult), eClockWorkExternalApplicationAppointmentCompareResult), matchingClockWorkAppointment.AppointmentId, app.UniqueId);
									bool flag8 = eClockWorkExternalApplicationAppointmentCompareResult == eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast;
									if (flag8)
									{
										bool isCancelled = matchingClockWorkAppointment.IsCancelled;
										if (isCancelled)
										{
											this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
											{
												SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment,
												ExternalAppointment = app,
												ClockWorkAppointment = null
											});
										}
										else
										{
											this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
											{
												SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateExternalAppointment,
												ExternalAppointment = app,
												ClockWorkAppointment = matchingClockWorkAppointment
											});
										}
									}
									else
									{
										bool flag9 = eClockWorkExternalApplicationAppointmentCompareResult == eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast;
										if (flag9)
										{
											bool isCancelled2 = app.IsCancelled;
											if (isCancelled2)
											{
												this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
												{
													SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
													ExternalAppointment = app,
													ClockWorkAppointment = matchingClockWorkAppointment
												});
											}
											else
											{
												this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
												{
													SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateClockWorkAppointment,
													ExternalAppointment = app,
													ClockWorkAppointment = matchingClockWorkAppointment
												});
											}
										}
									}
								}
							}
						}
						else
						{
							ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction3 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, app));
							bool flag10 = clockWorkExternalApplicationSyncAction3 == null;
							if (flag10)
							{
								ClockWorkExternalAppMapping clockWorkExternalAppMapping = this.AppointmentSyncMappingManager.LoadMappingByExternalId(app);
								bool flag11 = clockWorkExternalAppMapping == null;
								if (flag11)
								{
									this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
									{
										SyncAction = eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment,
										ExternalAppointment = app,
										ClockWorkAppointment = null
									});
								}
								else
								{
									CWLogger.Logger.Warn("DoSlowSync:FigureOutSyncActionsForExternalCalendar:SkippedCreatingNewClockWorkAppointmentBecauseAnExistingMappingWasFoundInTheClockWorkDatabaseForThisCwAppId:UniqueId2={0}", app.UniqueId2 ?? "NULL");
								}
							}
							else
							{
								CWLogger.Logger.Warn("DoSlowSync:FigureOutSyncActionsForExternalCalendar:SkippedCreatingNewClockWorkAppointmentBecauseThereIsAlreadyASyncActionForThis:UniqueId2={0}", app.UniqueId2 ?? "NULL");
							}
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::FigureOutSyncActionsForExternalCalendar: {0}", ex), ex);
					}
				}
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0005DFA8 File Offset: 0x0005C1A8
		private void FigureOutSyncActionsForClockWork(ref List<ClockWorkExternalApplicationSyncAction> syncActions, List<ClockWorkSyncAppointment> clockWorkApps, List<ExternalAppointment> externalCalendarApps)
		{
			using (List<ClockWorkSyncAppointment>.Enumerator enumerator = clockWorkApps.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClockWorkSyncAppointment app = enumerator.Current;
					try
					{
						bool flag = app.Mapping != null;
						if (flag)
						{
							ExternalAppointment externalAppointment = externalCalendarApps.FirstOrDefault((ExternalAppointment exapp) => this.ExternalAppointmentManager.AppointmentsAreEqual(app, exapp));
							bool flag2 = externalAppointment == null;
							if (flag2)
							{
								string text = app.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
								bool flag3 = string.IsNullOrEmpty(text);
								if (flag3)
								{
									CWLogger logger = CWLogger.Logger;
									string format = "CalendarSyncManager::FigureOutSyncActionsForClockWork:: FirstClockWorkSyncAttendee is null for appid={0}, app atts={1}";
									ClockWorkSyncAppointment app4 = app;
									object arg = (app4 != null) ? app4.AppointmentId : 0;
									ClockWorkSyncAppointment app2 = app;
									object arg2;
									if (app2 == null)
									{
										arg2 = null;
									}
									else
									{
										List<ClockWorkSyncAttendee> attendees = app2.Attendees;
										if (attendees == null)
										{
											arg2 = null;
										}
										else
										{
											arg2 = attendees.Select(delegate(ClockWorkSyncAttendee a)
											{
												ClockWorkSyncPersonBase attendee = a.Attendee;
												return (attendee != null) ? attendee.PersonId : 0;
											}).ToList<int>().CommaSeparatedValues<int>();
										}
									}
									logger.Debug(string.Format(format, arg, arg2));
									CWLogger logger2 = CWLogger.Logger;
									string format2 = "appid={0}, sdate={1}, edate={2}, catts={3}";
									object[] array = new object[4];
									array[0] = app.AppointmentId;
									array[1] = app.StartDateTime.ToString("u");
									array[2] = app.EndDateTime.ToString("u");
									int num = 3;
									List<ClockWorkSyncAttendee> attendees2 = app.Attendees;
									array[num] = ((attendees2 != null) ? attendees2.Count : 0);
									logger2.Debug(string.Format(format2, array));
								}
								externalAppointment = this.ExternalAppointmentManager.LoadAppointment(new ExternalAppointmentId
								{
									ClockWorkAppId = app.AppointmentId,
									UniqueId = app.Mapping.ExternalApplicationUniqueAppointmentId,
									UniqueId2 = app.Mapping.ExternalApplicationUniqueAppointmentId2,
									GlobalAppId = app.Mapping.ExternalApplicationGlobalAppointmentId
								}, text);
							}
							bool flag4 = externalAppointment == null;
							if (flag4)
							{
								this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
								{
									SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
									ExternalAppointment = null,
									ClockWorkAppointment = app
								});
							}
							else
							{
								eClockWorkExternalApplicationAppointmentCompareResult eClockWorkExternalApplicationAppointmentCompareResult = this.ClockWorkSyncAppointmentManager.CheckAppointmentDiff(externalAppointment, app);
								CWLogger.Logger.Debug("CalendarSyncManager::FigureOutSyncActionsForOutlook::AppDiff = {0}, cwAppId = {1}, outlookAppId = {2}", Enum.GetName(typeof(eClockWorkExternalApplicationAppointmentCompareResult), eClockWorkExternalApplicationAppointmentCompareResult), app.AppointmentId, externalAppointment.UniqueId);
								bool flag5 = eClockWorkExternalApplicationAppointmentCompareResult == eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast;
								if (flag5)
								{
									bool isCancelled = app.IsCancelled;
									if (isCancelled)
									{
										this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment,
											ExternalAppointment = externalAppointment,
											ClockWorkAppointment = app
										});
									}
									else
									{
										this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateExternalAppointment,
											ExternalAppointment = externalAppointment,
											ClockWorkAppointment = app
										});
									}
								}
								else
								{
									bool flag6 = eClockWorkExternalApplicationAppointmentCompareResult == eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast;
									if (flag6)
									{
										bool isCancelled2 = externalAppointment.IsCancelled;
										if (isCancelled2)
										{
											this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
											{
												SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
												ExternalAppointment = externalAppointment,
												ClockWorkAppointment = app
											});
										}
										else
										{
											this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
											{
												SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateClockWorkAppointment,
												ExternalAppointment = externalAppointment,
												ClockWorkAppointment = app
											});
										}
									}
								}
							}
						}
						else
						{
							ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment && g.ClockWorkAppointment != null && g.ClockWorkAppointment.AppointmentId == app.AppointmentId);
							bool flag7 = clockWorkExternalApplicationSyncAction == null;
							if (flag7)
							{
								ClockWorkExternalAppMapping clockWorkExternalAppMapping = this.AppointmentSyncMappingManager.LoadMappingByClockWorkAppointmentId(app.AppointmentId);
								bool flag8 = clockWorkExternalAppMapping == null;
								if (flag8)
								{
									this.AddExternalSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
									{
										SyncAction = eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment,
										ExternalAppointment = null,
										ClockWorkAppointment = app
									});
								}
								else
								{
									CWLogger.Logger.Warn("DoSlowSync:FigureOutSyncActionsForClockWork:SkippedCreatingNewExternalAppointmentBecauseAnExistingMappingWasFoundInTheClockWorkDatabaseForThisCwAppId:CwAppId={0}", app.AppointmentId.ToString());
								}
							}
							else
							{
								CWLogger.Logger.Warn("DoSlowSync:FigureOutSyncActionsForClockWork:SkippedCreatingNewExternalAppointmentBecauseThereIsAlreadyASyncActionForThis:CWAPPID={0}", app.AppointmentId.ToString());
							}
						}
					}
					catch (Exception ex)
					{
						CWLogger logger3 = CWLogger.Logger;
						string format3 = "CalendarSyncManager::FigureOutSyncActionsForClockWork:appid={0}, {1}";
						ClockWorkSyncAppointment app3 = app;
						logger3.ErrorException(string.Format(format3, (app3 != null) ? app3.AppointmentId : 0, ex), ex);
					}
				}
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0005E474 File Offset: 0x0005C674
		private void PreProcessChangesList(IList<CalendarSyncManager.SyncAppointmentChangePair> commonChanges, ref List<ClockWorkSyncAppointmentChange> cwAppChanges, ref List<ExternalSyncAppointmentChange> exAppChanges)
		{
			foreach (CalendarSyncManager.SyncAppointmentChangePair syncAppointmentChangePair in commonChanges)
			{
				bool flag = syncAppointmentChangePair.ClockWorkSyncAppointmentChange.LastModifiedDate >= syncAppointmentChangePair.ExternalSyncAppointmentChange.LastModifiedDate;
				bool flag2 = flag;
				if (flag2)
				{
					cwAppChanges.Add(syncAppointmentChangePair.ClockWorkSyncAppointmentChange);
				}
				else
				{
					exAppChanges.Add(syncAppointmentChangePair.ExternalSyncAppointmentChange);
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0005E4FC File Offset: 0x0005C6FC
		private static void EnsureSyncUserIsInAttendeesList(ClockWorkExternalApplicationSyncUser syncUser, ref ExternalAppointment externalApp)
		{
			bool flag = externalApp == null;
			if (!flag)
			{
				bool flag2 = externalApp.Attendees == null;
				if (flag2)
				{
					externalApp.Attendees = new List<ExternalAttendee>();
				}
				bool flag3 = externalApp.Attendees.Any((ExternalAttendee g) => g.Username.Equals(syncUser.ExternalApplicationUsername, StringComparison.OrdinalIgnoreCase));
				if (!flag3)
				{
					externalApp.Attendees.Add(new ExternalAttendee
					{
						Username = syncUser.ExternalApplicationUsername,
						Name = syncUser.ExternalApplicationUsername
					});
				}
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0005E594 File Offset: 0x0005C794
		private void FigureOutFastSyncActionsForExternalCalendar(ClockWorkExternalApplicationSyncUser syncUser, ref List<ClockWorkExternalApplicationSyncAction> syncActions, IList<ClockWorkSyncAppointmentChange> clockWorkSyncAppointments, IList<ExternalSyncAppointmentChange> externalSyncAppointments)
		{
			bool flag = externalSyncAppointments != null;
			if (flag)
			{
				List<ExternalAppointmentId> list = (from g in externalSyncAppointments
				where g.ExternalAppointmentID != null && (g.AppointmentSyncChangeType == eAppointmentSyncChangeType.Added || g.AppointmentSyncChangeType == eAppointmentSyncChangeType.Modified)
				select g into h
				select h.ExternalAppointmentID).ToList<ExternalAppointmentId>();
				CWLogger.Logger.Debug("CalendarSyncManager:BEGIN FigureOutFastSyncActionsForExternalCalendar:LoadingMultipleExternalAppointments:UniqueIds={0}", string.Join(",", (from g in list
				select g.UniqueId ?? "").ToArray<string>()));
				IList<ExternalAppointment> list3;
				if (list.Count <= 0)
				{
					IList<ExternalAppointment> list2 = new List<ExternalAppointment>();
					list3 = list2;
				}
				else
				{
					list3 = (this.ExternalAppointmentManager.TryToLoadAppointments(list) ?? new List<ExternalAppointment>());
				}
				IList<ExternalAppointment> list4 = list3;
				CWLogger.Logger.Debug("CalendarSyncManager:END FigureOutFastSyncActionsForExternalCalendar:LoadingMultipleExternalAppointments:CacheCount={0}", list4.Count.ToString());
				foreach (ExternalAppointment externalAppointment in list4)
				{
					CWLogger.Logger.Debug("CalendarSyncManager:FigureOutFastSyncActionsForExternalCalendar:External appointment cache exappid='{0}'", externalAppointment.UniqueId ?? "NULL");
				}
				using (IEnumerator<ExternalSyncAppointmentChange> enumerator2 = externalSyncAppointments.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ExternalSyncAppointmentChange exAppChange = enumerator2.Current;
						try
						{
							bool flag2 = exAppChange.ExternalAppointmentID == null;
							if (!flag2)
							{
								ExternalAppointment exApp = null;
								switch (exAppChange.AppointmentSyncChangeType)
								{
								case eAppointmentSyncChangeType.Added:
								{
									exApp = (list4.FirstOrDefault((ExternalAppointment g) => !string.IsNullOrEmpty(g.UniqueId) && g.UniqueId == exAppChange.ExternalAppointmentID.UniqueId) ?? this.ExternalAppointmentManager.LoadAppointment(exAppChange.ExternalAppointmentID));
									bool flag3 = exApp == null;
									if (!flag3)
									{
										exApp.Mapping = exAppChange.Mapping;
										bool flag4 = exApp.AppointmentType == ExternalAppointmentType.RecurringMaster;
										if (flag4)
										{
											bool skipRecurringAppointmentsInFastSync = this.OpContext.SyncSettings.SkipRecurringAppointmentsInFastSync;
											if (skipRecurringAppointmentsInFastSync)
											{
												CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Added: Recurring app found, skipping it ..., exappid='{0}'", exApp.UniqueId ?? "NULL");
											}
											else
											{
												CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Added: Recurring app found, loading set of recurring occurrences ..., exappid='{0}'", exApp.UniqueId ?? "NULL");
												IList<ExternalAppointment> list5 = this.ExternalAppointmentManager.LoadOccurrenceAppointmentsOfRecurrenceSerie(exApp.UniqueId, new DateTime?(DateTime.Today), 30, true);
												bool flag5 = list5 != null && list5.Count > 0;
												if (flag5)
												{
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Added: Begin adding sync actions for recurring app, nOcurrencesLoaded='{1}', exappid='{0}'", exApp.UniqueId ?? "NULL", list5.Count);
													foreach (ExternalAppointment recApp3 in list5)
													{
														ExternalAppointment recApp = recApp3;
														CalendarSyncManager.EnsureSyncUserIsInAttendeesList(syncUser, ref recApp);
														ClockWorkSyncAppointment clockWorkSyncAppointment = (recApp.Mapping != null && recApp.Mapping.ClockWorkAppointmentId > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(recApp.Mapping.ClockWorkAppointmentId) : null;
														bool flag6 = clockWorkSyncAppointment == null;
														if (flag6)
														{
															ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, recApp));
															bool flag7 = clockWorkExternalApplicationSyncAction == null;
															if (flag7)
															{
																recApp.Mapping = new ClockWorkExternalAppMapping
																{
																	ExternalApplicationUniqueAppointmentId = recApp.UniqueId,
																	ExternalApplicationUniqueAppointmentId2 = recApp.UniqueId2,
																	ExternalApplicationMasterRecurrenceAppointmentId = exApp.UniqueId,
																	ExternalApplicationGlobalAppointmentId = recApp.LegacyGlobalAppointmentId
																};
																this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
																{
																	SyncAction = eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment,
																	ExternalAppointment = recApp,
																	ClockWorkAppointment = null
																});
															}
														}
													}
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Added: End adding sync actions for recurring app, exappid='{0}'", exApp.UniqueId ?? "NULL");
												}
											}
										}
										else
										{
											CalendarSyncManager.EnsureSyncUserIsInAttendeesList(syncUser, ref exApp);
											ClockWorkSyncAppointment clockWorkSyncAppointment2 = (exAppChange.Mapping != null && exAppChange.Mapping.ClockWorkAppointmentId > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(exAppChange.Mapping.ClockWorkAppointmentId) : null;
											bool flag8 = clockWorkSyncAppointment2 == null;
											if (flag8)
											{
												ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction2 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, exApp));
												bool flag9 = clockWorkExternalApplicationSyncAction2 == null;
												if (flag9)
												{
													this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
													{
														SyncAction = eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment,
														ExternalAppointment = exApp,
														ClockWorkAppointment = null
													});
												}
												else
												{
													CWLogger.Logger.Warn("DoFastSync:FigureOutFastSyncActionsForExternalCalendar:SkippedCreatingNewClockWorkAppointmentBecauseThereIsAlreadyASyncActionForThis:UniqueId2={0}", exApp.UniqueId2 ?? "NULL");
												}
											}
										}
									}
									break;
								}
								case eAppointmentSyncChangeType.Modified:
								{
									exApp = list4.FirstOrDefault((ExternalAppointment g) => !string.IsNullOrEmpty(g.UniqueId) && g.UniqueId == exAppChange.ExternalAppointmentID.UniqueId);
									bool flag10 = exApp == null;
									if (flag10)
									{
										CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar: external appointment was not found in the cache, exappid='{0}'", exAppChange.ExternalAppointmentID.UniqueId ?? "NULL");
										exApp = this.ExternalAppointmentManager.LoadAppointment(exAppChange.ExternalAppointmentID);
									}
									bool flag11 = exApp == null;
									if (!flag11)
									{
										exApp.Mapping = exAppChange.Mapping;
										bool flag12 = exApp.AppointmentType == ExternalAppointmentType.RecurringMaster;
										if (flag12)
										{
											bool skipRecurringAppointmentsInFastSync2 = this.OpContext.SyncSettings.SkipRecurringAppointmentsInFastSync;
											if (skipRecurringAppointmentsInFastSync2)
											{
												CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Modified: Recurring app found, skipping it ..., exappid='{0}'", exApp.UniqueId ?? "NULL");
											}
											else
											{
												CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Modified: Recurring app found, loading set of recurring occurrences ..., exappid='{0}'", exApp.UniqueId ?? "NULL");
												IList<ExternalAppointment> list6 = this.ExternalAppointmentManager.LoadOccurrenceAppointmentsOfRecurrenceSerie(exApp.UniqueId, new DateTime?(DateTime.Today), 30, true);
												bool flag13 = list6 != null && list6.Count > 0;
												if (flag13)
												{
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Modified: Begin adding sync actions for recurring app, nOcurrencesLoaded='{1}', exappid='{0}'", exApp.UniqueId ?? "NULL", list6.Count);
													foreach (ExternalAppointment recApp2 in list6)
													{
														ExternalAppointment recApp = recApp2;
														CalendarSyncManager.EnsureSyncUserIsInAttendeesList(syncUser, ref recApp);
														ClockWorkSyncAppointment clockWorkSyncAppointment3 = (recApp.Mapping != null && recApp.Mapping.ClockWorkAppointmentId > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(recApp.Mapping.ClockWorkAppointmentId) : null;
														bool isCancelled = recApp.IsCancelled;
														if (isCancelled)
														{
															this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
															{
																SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
																ExternalAppointment = exApp,
																ClockWorkAppointment = clockWorkSyncAppointment3
															});
														}
														else
														{
															bool flag14 = clockWorkSyncAppointment3 == null;
															if (flag14)
															{
																ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction3 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, recApp));
																bool flag15 = clockWorkExternalApplicationSyncAction3 == null;
																if (flag15)
																{
																	recApp.Mapping = new ClockWorkExternalAppMapping
																	{
																		ExternalApplicationUniqueAppointmentId = recApp.UniqueId,
																		ExternalApplicationUniqueAppointmentId2 = recApp.UniqueId2,
																		ExternalApplicationMasterRecurrenceAppointmentId = exApp.UniqueId,
																		ExternalApplicationGlobalAppointmentId = recApp.LegacyGlobalAppointmentId
																	};
																	this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
																	{
																		SyncAction = eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment,
																		ExternalAppointment = recApp,
																		ClockWorkAppointment = null
																	});
																}
															}
															else
															{
																this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
																{
																	SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateClockWorkAppointment,
																	ExternalAppointment = recApp,
																	ClockWorkAppointment = clockWorkSyncAppointment3
																});
															}
														}
													}
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Modified: End adding sync actions for recurring app, exappid='{0}'", exApp.UniqueId ?? "NULL");
												}
											}
										}
										else
										{
											CalendarSyncManager.EnsureSyncUserIsInAttendeesList(syncUser, ref exApp);
											TimeSpan? timeSpan = (exAppChange.Mapping != null) ? (exAppChange.Mapping.ExternalApplicationLastUpdatedDate - exApp.LastModifiedTime) : null;
											ClockWorkSyncAppointment clockWorkSyncAppointment4 = (exAppChange.Mapping != null && exAppChange.Mapping.ClockWorkAppointmentId > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(exAppChange.Mapping.ClockWorkAppointmentId) : null;
											bool flag16 = clockWorkSyncAppointment4 == null;
											if (flag16)
											{
												ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction4 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment && g.ExternalAppointment != null && this.ExternalAppointmentManager.AppointmentsAreEqual(g.ExternalAppointment, exApp));
												bool flag17 = clockWorkExternalApplicationSyncAction4 == null;
												if (flag17)
												{
													this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
													{
														SyncAction = eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment,
														ExternalAppointment = exApp,
														ClockWorkAppointment = null
													});
												}
												else
												{
													CWLogger.Logger.Warn("DoFastSync:FigureOutFastSyncActionsForExternalCalendar:SkippedCreatingNewClockWorkAppointmentBecauseThereIsAlreadyASyncActionForThis:UniqueId2={0}", exApp.UniqueId2 ?? "NULL");
												}
											}
											else
											{
												bool flag18 = timeSpan != null && Math.Abs(timeSpan.Value.TotalSeconds) > 1.0;
												if (flag18)
												{
													this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
													{
														SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateClockWorkAppointment,
														ExternalAppointment = exApp,
														ClockWorkAppointment = clockWorkSyncAppointment4
													});
												}
											}
										}
									}
									break;
								}
								case eAppointmentSyncChangeType.Deleted:
								{
									ClockWorkSyncAppointment clockWorkSyncAppointment5 = (exAppChange.Mapping != null && exAppChange.Mapping.ClockWorkAppointmentId > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(exAppChange.Mapping.ClockWorkAppointmentId) : null;
									bool flag19 = exAppChange.Mapping != null && clockWorkSyncAppointment5 != null;
									if (flag19)
									{
										this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
											ExternalAppointment = new ExternalAppointment
											{
												Mapping = exAppChange.Mapping,
												UniqueId2 = exAppChange.ExternalAppointmentID.UniqueId2,
												UniqueId = exAppChange.ExternalAppointmentID.UniqueId,
												LegacyGlobalAppointmentId = exAppChange.ExternalAppointmentID.GlobalAppId,
												LastModifiedTime = exAppChange.LastModifiedDate
											},
											ClockWorkAppointment = clockWorkSyncAppointment5
										});
									}
									else
									{
										bool flag20 = exAppChange.Mapping == null;
										if (flag20)
										{
											IList<ClockWorkExternalAppMapping> list7 = this.AppointmentSyncMappingManager.LoadMappingByExternalMasterRecurrenceAppointmentId(exAppChange.ExternalAppointmentID.UniqueId);
											bool flag21 = list7 != null && list7.Count > 0;
											if (flag21)
											{
												bool skipRecurringAppointmentsInFastSync3 = this.OpContext.SyncSettings.SkipRecurringAppointmentsInFastSync;
												if (skipRecurringAppointmentsInFastSync3)
												{
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Deleted: Recurring app found, Skip deleting recurring apps, nOcurrences='{1}', exappid='{0}'", exAppChange.ExternalAppointmentID.UniqueId ?? "NULL", list7.Count);
												}
												else
												{
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Deleted: Begin deleting sync actions for recurring apps, nOcurrences='{1}', exappid='{0}'", exAppChange.ExternalAppointmentID.UniqueId ?? "NULL", list7.Count);
													foreach (ClockWorkExternalAppMapping clockWorkExternalAppMapping in list7)
													{
														bool flag22 = clockWorkExternalAppMapping != null && clockWorkExternalAppMapping.ClockWorkAppointmentId > 0;
														if (flag22)
														{
															ClockWorkSyncAppointment clockWorkSyncAppointment6 = this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(clockWorkExternalAppMapping.ClockWorkAppointmentId);
															bool flag23 = clockWorkSyncAppointment6 != null && clockWorkSyncAppointment6.StartDateTime >= DateTime.Today;
															if (flag23)
															{
																this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
																{
																	SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment,
																	ExternalAppointment = new ExternalAppointment
																	{
																		Mapping = clockWorkExternalAppMapping,
																		UniqueId2 = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2,
																		UniqueId = exAppChange.ExternalAppointmentID.UniqueId,
																		LegacyGlobalAppointmentId = clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId,
																		LastModifiedTime = exAppChange.LastModifiedDate
																	},
																	ClockWorkAppointment = clockWorkSyncAppointment6
																});
															}
														}
													}
													CWLogger.Logger.Debug("CalendarSyncManager: FigureOutFastSyncActionsForExternalCalendar:Deleted: End deleting sync actions for recurring apps, nOcurrences='{1}', exappid='{0}'", exAppChange.ExternalAppointmentID.UniqueId ?? "NULL", list7.Count);
												}
											}
										}
									}
									break;
								}
								}
							}
						}
						catch (Exception ex)
						{
							CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::FigureOutFastSyncActionsForExternalCalendar: {0}", ex), ex);
						}
					}
				}
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0005F500 File Offset: 0x0005D700
		private void FigureOutFastSyncActionsForClockWorkCalendar(ref List<ClockWorkExternalApplicationSyncAction> syncActions, IList<ClockWorkSyncAppointmentChange> clockWorkSyncAppointments, IList<ExternalSyncAppointmentChange> externalSyncAppointments)
		{
			bool flag = clockWorkSyncAppointments != null;
			if (flag)
			{
				foreach (ClockWorkSyncAppointmentChange clockWorkSyncAppointmentChange in clockWorkSyncAppointments)
				{
					try
					{
						ExternalAppointmentId externalAppointmentId;
						if (clockWorkSyncAppointmentChange.Mapping == null)
						{
							externalAppointmentId = null;
						}
						else
						{
							ExternalAppointmentId externalAppointmentId2 = new ExternalAppointmentId();
							externalAppointmentId2.UniqueId2 = clockWorkSyncAppointmentChange.Mapping.ExternalApplicationUniqueAppointmentId2;
							externalAppointmentId2.UniqueId = clockWorkSyncAppointmentChange.Mapping.ExternalApplicationUniqueAppointmentId;
							externalAppointmentId2.GlobalAppId = clockWorkSyncAppointmentChange.Mapping.ExternalApplicationGlobalAppointmentId;
							externalAppointmentId = externalAppointmentId2;
							externalAppointmentId2.ClockWorkAppId = clockWorkSyncAppointmentChange.ClockWorkAppointmentID;
						}
						ExternalAppointmentId externalAppointmentId3 = externalAppointmentId;
						switch (clockWorkSyncAppointmentChange.AppointmentSyncChangeType)
						{
						case eAppointmentSyncChangeType.Added:
						{
							ClockWorkSyncAppointment cwApp = (clockWorkSyncAppointmentChange.ClockWorkAppointmentID > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(clockWorkSyncAppointmentChange.ClockWorkAppointmentID) : null;
							bool flag2 = cwApp == null;
							if (!flag2)
							{
								cwApp.Mapping = clockWorkSyncAppointmentChange.Mapping;
								bool flag3 = clockWorkSyncAppointmentChange.Mapping == null;
								if (flag3)
								{
									ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment && g.ClockWorkAppointment != null && g.ClockWorkAppointment.AppointmentId == cwApp.AppointmentId);
									bool flag4 = clockWorkExternalApplicationSyncAction == null;
									if (flag4)
									{
										this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment,
											ExternalAppointment = null,
											ClockWorkAppointment = cwApp
										});
									}
									else
									{
										CWLogger.Logger.Warn("DoFastSync:FigureOutFastSyncActionsForClockWorkCalendar:SkippedCreatingNewExternalAppointmentBecauseThereIsAlreadyASyncActionForThis:CwAppId={0}", cwApp.AppointmentId);
									}
								}
							}
							break;
						}
						case eAppointmentSyncChangeType.Modified:
						{
							ClockWorkSyncAppointment cwApp2 = (clockWorkSyncAppointmentChange.ClockWorkAppointmentID > 0) ? this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointmentById(clockWorkSyncAppointmentChange.ClockWorkAppointmentID) : null;
							bool flag5 = cwApp2 == null;
							if (!flag5)
							{
								cwApp2.Mapping = clockWorkSyncAppointmentChange.Mapping;
								TimeSpan? timeSpan = (cwApp2.Mapping != null) ? (cwApp2.Mapping.ClockWorkLastUpdatedDate - cwApp2.LastModifiedTime) : null;
								bool flag6 = clockWorkSyncAppointmentChange.Mapping == null;
								if (flag6)
								{
									ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction2 = syncActions.FirstOrDefault((ClockWorkExternalApplicationSyncAction g) => g.SyncAction == eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment && g.ClockWorkAppointment != null && g.ClockWorkAppointment.AppointmentId == cwApp2.AppointmentId);
									bool flag7 = clockWorkExternalApplicationSyncAction2 == null;
									if (flag7)
									{
										this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
										{
											SyncAction = eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment,
											ExternalAppointment = null,
											ClockWorkAppointment = cwApp2
										});
									}
									else
									{
										CWLogger.Logger.Warn("DoFastSync:FigureOutFastSyncActionsForClockWorkCalendar:SkippedCreatingNewExternalAppointmentBecauseThereIsAlreadyASyncActionForThis:CwAppId={0}", cwApp2.AppointmentId);
									}
								}
								else
								{
									bool flag8 = timeSpan != null && Math.Abs(timeSpan.Value.TotalSeconds) > 1.0;
									if (flag8)
									{
										ExternalAppointment externalAppointment = (externalAppointmentId3 != null) ? this.ExternalAppointmentManager.LoadAppointment(externalAppointmentId3) : null;
										bool flag9 = externalAppointment != null;
										if (flag9)
										{
											this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
											{
												SyncAction = eClockWorkExternalApplicationSyncActionType.UpdateExternalAppointment,
												ExternalAppointment = externalAppointment,
												ClockWorkAppointment = cwApp2
											});
										}
									}
								}
							}
							break;
						}
						case eAppointmentSyncChangeType.Deleted:
						{
							ExternalAppointment externalAppointment2 = (externalAppointmentId3 != null) ? this.ExternalAppointmentManager.LoadAppointment(externalAppointmentId3) : null;
							bool flag10 = clockWorkSyncAppointmentChange.Mapping != null && externalAppointment2 != null;
							if (flag10)
							{
								this.AddClockWorkSyncAction(ref syncActions, new ClockWorkExternalApplicationSyncAction
								{
									SyncAction = eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment,
									ExternalAppointment = externalAppointment2,
									ClockWorkAppointment = null
								});
							}
							break;
						}
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::FigureOutFastSyncActionsForClockWorkCalendar: {0}", ex), ex);
					}
				}
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0005F900 File Offset: 0x0005DB00
		private string SyncActionToStringForLogging(ClockWorkExternalApplicationSyncAction syncAction)
		{
			return string.Format("ClockWorkApp={0}:Mapping.CwAppId={1}:Mapping.UniqueId2={2}:Mapping.UniqueId={3}::ExternalAppUniqueId2={4}:ExternalAppUniqueId={5}:Mapping.CwAppId={6}:Mapping.UniqueId2={7}:Mapping.UniqueId={8}", new object[]
			{
				(syncAction.ClockWorkAppointment == null) ? "NULL" : syncAction.ClockWorkAppointment.AppointmentId.ToString(),
				(syncAction.ClockWorkAppointment == null || syncAction.ClockWorkAppointment.Mapping == null) ? "NULL" : syncAction.ClockWorkAppointment.Mapping.ClockWorkAppointmentId.ToString(),
				(syncAction.ClockWorkAppointment == null || syncAction.ClockWorkAppointment.Mapping == null) ? "NULL" : (syncAction.ClockWorkAppointment.Mapping.ExternalApplicationUniqueAppointmentId2 ?? "null"),
				(syncAction.ClockWorkAppointment == null || syncAction.ClockWorkAppointment.Mapping == null) ? "NULL" : (syncAction.ClockWorkAppointment.Mapping.ExternalApplicationUniqueAppointmentId ?? "null"),
				(syncAction.ExternalAppointment == null) ? "NULL" : (syncAction.ExternalAppointment.UniqueId2 ?? "null"),
				(syncAction.ExternalAppointment == null) ? "NULL" : (syncAction.ExternalAppointment.UniqueId ?? "null"),
				(syncAction.ExternalAppointment == null || syncAction.ExternalAppointment.Mapping == null) ? "NULL" : syncAction.ExternalAppointment.Mapping.ClockWorkAppointmentId.ToString(),
				(syncAction.ExternalAppointment == null || syncAction.ExternalAppointment.Mapping == null) ? "NULL" : (syncAction.ExternalAppointment.Mapping.ExternalApplicationUniqueAppointmentId2 ?? "null"),
				(syncAction.ExternalAppointment == null || syncAction.ExternalAppointment.Mapping == null) ? "NULL" : (syncAction.ExternalAppointment.Mapping.ExternalApplicationUniqueAppointmentId ?? "null")
			});
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0005FAE4 File Offset: 0x0005DCE4
		private void ExecuteSyncActions(List<ClockWorkExternalApplicationSyncAction> syncActions)
		{
			foreach (ClockWorkExternalApplicationSyncAction clockWorkExternalApplicationSyncAction in syncActions)
			{
				bool flag = clockWorkExternalApplicationSyncAction.SyncAction != eClockWorkExternalApplicationSyncActionType.DoNothing;
				if (flag)
				{
					ClockWorkSyncAppointment clockWorkSyncAppointment = null;
					ExternalAppointment externalAppointment = null;
					try
					{
						bool flag2 = clockWorkExternalApplicationSyncAction.ClockWorkAppointment != null;
						if (flag2)
						{
							clockWorkSyncAppointment = clockWorkExternalApplicationSyncAction.ClockWorkAppointment;
						}
						else
						{
							bool flag3 = clockWorkExternalApplicationSyncAction.ExternalAppointment != null;
							if (flag3)
							{
								clockWorkSyncAppointment = clockWorkExternalApplicationSyncAction.ExternalAppointment.ToClockWorkSyncAppointment(this.OpContext.SyncSettings);
							}
						}
						bool flag4 = clockWorkExternalApplicationSyncAction.ExternalAppointment != null;
						if (flag4)
						{
							externalAppointment = clockWorkExternalApplicationSyncAction.ExternalAppointment;
						}
						else
						{
							bool flag5 = clockWorkExternalApplicationSyncAction.ClockWorkAppointment != null;
							if (flag5)
							{
								externalAppointment = clockWorkExternalApplicationSyncAction.ClockWorkAppointment.ToExternalCalendarAppointment(this.OpContext.SyncSettings);
							}
						}
						this.OpContext.CalendarUsername = externalAppointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
						switch (clockWorkExternalApplicationSyncAction.SyncAction)
						{
						case eClockWorkExternalApplicationSyncActionType.CreateClockWorkAppointment:
						{
							bool isCancelled = clockWorkSyncAppointment.IsCancelled;
							if (isCancelled)
							{
								CWLogger.Logger.Debug("ExecuteSyncAction:CreateClockWorkAppointment:DontCreateAppointmentInClockWorkBecauseOutlookAppIsCancelled:appid={0}:sd={1}", (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(), (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
							}
							else
							{
								bool flag6 = clockWorkSyncAppointment.AppointmentId == 0;
								if (flag6)
								{
									ClockWorkExternalAppMapping clockWorkExternalAppMapping = new ClockWorkExternalAppMapping
									{
										ExternalApplicationUniqueAppointmentId = externalAppointment.UniqueId,
										ExternalApplicationUniqueAppointmentId2 = externalAppointment.UniqueId2,
										ExternalApplicationGlobalAppointmentId = externalAppointment.LegacyGlobalAppointmentId,
										ExternalApplicationLastUpdatedDate = new DateTime?(externalAppointment.LastModifiedTime),
										ExternalApplicationMasterRecurrenceAppointmentId = ((externalAppointment.Mapping != null) ? externalAppointment.Mapping.ExternalApplicationMasterRecurrenceAppointmentId : null)
									};
									clockWorkSyncAppointment.Mapping = clockWorkExternalAppMapping;
									bool flag7;
									try
									{
										flag7 = this.ClockWorkSyncAppointmentManager.CreateClockWorkSyncAppointment(false, clockWorkSyncAppointment, externalAppointment);
									}
									finally
									{
										clockWorkSyncAppointment.Mapping = null;
									}
									bool flag8 = flag7;
									if (flag8)
									{
										CWLogger.Logger.Debug("ExecuteSyncAction:CreateClockWorkAppointment:appid={0}:sd={1}", (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(), (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
										bool flag9 = clockWorkSyncAppointment.AppointmentId > 0;
										if (flag9)
										{
											clockWorkExternalAppMapping.ClockWorkAppointmentId = clockWorkSyncAppointment.AppointmentId;
											bool flag10 = string.IsNullOrEmpty(clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2);
											if (flag10)
											{
												ExternalAppointmentId appId = new ExternalAppointmentId
												{
													UniqueId = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId,
													UniqueId2 = clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2,
													GlobalAppId = clockWorkExternalAppMapping.ExternalApplicationGlobalAppointmentId,
													ClockWorkAppId = clockWorkExternalAppMapping.ClockWorkAppointmentId
												};
												CWLogger.Logger.Warn("CalendarSyncManager:: ExecuteSyncActions: UniqueId2 is empty cwappid={0}, externalappid={1}", clockWorkSyncAppointment.AppointmentId, externalAppointment.UniqueId ?? string.Empty);
												ExternalAppointment externalAppointment2 = this.ExternalAppointmentManager.LoadAppointment(appId, externalAppointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings));
												bool flag11 = externalAppointment2 != null && !string.IsNullOrEmpty(externalAppointment2.UniqueId2);
												if (flag11)
												{
													clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 = externalAppointment2.UniqueId2;
												}
												else
												{
													CWLogger.Logger.Warn("CalendarSyncManager:: ExecuteSyncActions: Load external appointment failed cwappid={0}, externalappid={1}", clockWorkSyncAppointment.AppointmentId, externalAppointment.UniqueId ?? string.Empty);
													this.ExternalAppointmentManager.UpdateAppointment(externalAppointment);
													externalAppointment2 = this.ExternalAppointmentManager.LoadAppointment(appId, externalAppointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings));
													bool flag12 = externalAppointment2 != null && !string.IsNullOrEmpty(externalAppointment2.UniqueId2);
													if (flag12)
													{
														clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2 = externalAppointment2.UniqueId2;
													}
												}
												CWLogger.Logger.Warn("CalendarSyncManager:: ExecuteSyncActions: UniqueId2 is empty completed cwappid={0}, externalappid={1}", clockWorkSyncAppointment.AppointmentId, externalAppointment.UniqueId ?? string.Empty);
											}
											bool flag13 = string.IsNullOrEmpty(clockWorkExternalAppMapping.ExternalApplicationUniqueAppointmentId2);
											if (flag13)
											{
												CWLogger.Logger.Warn("CalendarSyncManager:: ExecuteSyncActions: UniqueId2 is still empty after loading external appointment cwappid={0}, externalappid={1}", clockWorkSyncAppointment.AppointmentId, externalAppointment.UniqueId ?? string.Empty);
											}
											bool flag14 = this.IsValidExternalMapping(clockWorkExternalAppMapping);
											if (flag14)
											{
												this.AppointmentSyncMappingManager.CreateMapping(clockWorkExternalAppMapping);
												externalAppointment.Mapping = clockWorkExternalAppMapping;
												clockWorkSyncAppointment.Mapping = clockWorkExternalAppMapping;
												clockWorkExternalAppMapping.ClockWorkLastUpdatedDate = new DateTime?(this.ClockWorkSyncAppointmentManager.GetClockWorkAppointmentLastModifiedDateTime(clockWorkSyncAppointment.AppointmentId));
												this.AppointmentSyncMappingManager.UpdateMappingClockWorkChange(clockWorkExternalAppMapping.ClockWorkAppointmentId, clockWorkExternalAppMapping.ClockWorkLastUpdatedDate.Value);
											}
											else
											{
												CWLogger.Logger.Warn(string.Format("CalendarSyncManager::ExecuteSyncActions: External mapping not valid. cwappid={0}", clockWorkExternalAppMapping.ClockWorkAppointmentId));
											}
											CWLogger.Logger.Info("Sync:CreatedClockWorkAppointment:appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
											bool flag15 = !this.ExternalAppointmentManager.IsAppointmentEditable(new ExternalAppointmentId
											{
												ClockWorkAppId = clockWorkSyncAppointment.AppointmentId,
												UniqueId = externalAppointment.UniqueId,
												GlobalAppId = externalAppointment.LegacyGlobalAppointmentId,
												UniqueId2 = externalAppointment.UniqueId2
											});
											if (flag15)
											{
												this.ClockWorkSyncAppointmentManager.UpdateClockWorkSyncAppointmentReadOnlyStatus(false, clockWorkSyncAppointment.AppointmentId, true);
											}
										}
										else
										{
											CWLogger.Logger.Warn("Sync:CreatedClockWorkAppointment: Create Clockwork App failed appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
										}
									}
									else
									{
										CWLogger.Logger.Warn("Sync:CreatedClockWorkAppointment: Create Clockwork App failed because it was already in the system appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
									}
								}
							}
							break;
						}
						case eClockWorkExternalApplicationSyncActionType.CreateExternalAppointment:
						{
							bool isCancelled2 = externalAppointment.IsCancelled;
							if (isCancelled2)
							{
								CWLogger.Logger.Debug("ExecuteSyncAction:CreateExternalCalendarApp:DontCreateAppInOutlookBecauseClockWorkAppIsCancelled:uniqueid={0}:globalappid={1}:sd={2}", (externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.LegacyGlobalAppointmentId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.StartDate.ToString("yyyy-MM-dd H:mm"));
							}
							else
							{
								bool flag16 = externalAppointment.Attendees.Count > 0 || externalAppointment.Mapping != null;
								if (flag16)
								{
									ClockWorkExternalAppMapping clockWorkExternalAppMapping2 = new ClockWorkExternalAppMapping
									{
										ClockWorkAppointmentId = clockWorkSyncAppointment.AppointmentId,
										ExternalApplicationUniqueAppointmentId = string.Empty,
										ExternalApplicationUniqueAppointmentId2 = string.Empty,
										ExternalApplicationGlobalAppointmentId = string.Empty,
										ClockWorkLastUpdatedDate = new DateTime?(clockWorkSyncAppointment.LastModifiedTime),
										ExternalApplicationLastUpdatedDate = null
									};
									externalAppointment.Mapping = clockWorkExternalAppMapping2;
									try
									{
										this.ExternalAppointmentManager.CreateAppointment(externalAppointment);
									}
									finally
									{
										externalAppointment.Mapping = null;
									}
									CWLogger.Logger.Debug("ExecuteSyncAction:CreateExternalCalendarApp:uniqueid={0}:globalappid={1}:sd={2}", (externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.LegacyGlobalAppointmentId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.StartDate.ToString("yyyy-MM-dd H:mm"));
									clockWorkExternalAppMapping2.ExternalApplicationUniqueAppointmentId = externalAppointment.UniqueId;
									clockWorkExternalAppMapping2.ExternalApplicationGlobalAppointmentId = externalAppointment.LegacyGlobalAppointmentId;
									clockWorkExternalAppMapping2.ExternalApplicationUniqueAppointmentId2 = externalAppointment.UniqueId2;
									bool flag17 = this.IsValidExternalMapping(clockWorkExternalAppMapping2);
									if (flag17)
									{
										this.AppointmentSyncMappingManager.CreateMapping(clockWorkExternalAppMapping2);
										externalAppointment.Mapping = clockWorkExternalAppMapping2;
										clockWorkSyncAppointment.Mapping = clockWorkExternalAppMapping2;
										this.ExternalAppointmentManager.ReloadExternalCalendarLastDateTimeModified(externalAppointment);
										this.AppointmentSyncMappingManager.UpdateMappingExternalChange(new ExternalAppointmentId
										{
											ClockWorkAppId = clockWorkSyncAppointment.AppointmentId,
											UniqueId = externalAppointment.UniqueId,
											UniqueId2 = externalAppointment.UniqueId2,
											GlobalAppId = externalAppointment.LegacyGlobalAppointmentId
										}, externalAppointment.LastModifiedTime);
									}
									else
									{
										CWLogger.Logger.Warn(string.Format("CalendarSyncManager::ExecuteSyncActions: External mapping not valid. cwappid={0}", clockWorkExternalAppMapping2.ClockWorkAppointmentId));
									}
									CWLogger.Logger.Info("Sync:CreatedExternalCalendarAppointment:appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
								}
							}
							break;
						}
						case eClockWorkExternalApplicationSyncActionType.UpdateClockWorkAppointment:
						{
							clockWorkSyncAppointment = externalAppointment.ToClockWorkSyncAppointment(this.OpContext.SyncSettings);
							bool flag18 = clockWorkSyncAppointment.AppointmentId > 0;
							if (flag18)
							{
								this.ClockWorkSyncAppointmentManager.UpdateClockWorkSyncAppointment(false, clockWorkSyncAppointment);
								CWLogger.Logger.Debug("ExecuteSyncAction:UpdateClockWorkApp:appid={0}:sd={1}", (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(), (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
								clockWorkSyncAppointment.Mapping.ClockWorkLastUpdatedDate = new DateTime?(this.ClockWorkSyncAppointmentManager.GetClockWorkAppointmentLastModifiedDateTime(clockWorkSyncAppointment.AppointmentId));
								clockWorkSyncAppointment.Mapping.ExternalApplicationLastUpdatedDate = new DateTime?(externalAppointment.LastModifiedTime);
								CWLogger.Logger.Debug(string.Format("CalendarSyncManager::ExecuteSyncActions: Updating clockwork app mapping cwappid={0}, cwlastmodifieddate={1}", clockWorkSyncAppointment.AppointmentId, clockWorkSyncAppointment.Mapping.ClockWorkLastUpdatedDate.Value));
								this.AppointmentSyncMappingManager.UpdateMappingClockWorkChange(clockWorkSyncAppointment.AppointmentId, clockWorkSyncAppointment.Mapping.ClockWorkLastUpdatedDate.Value);
								CWLogger.Logger.Debug(string.Format("CalendarSyncManager::ExecuteSyncActions: Updating external app mapping cwappid={0}, exlastmodifieddate={1}", clockWorkSyncAppointment.AppointmentId, externalAppointment.LastModifiedTime));
								this.AppointmentSyncMappingManager.UpdateMappingExternalChange(new ExternalAppointmentId
								{
									ClockWorkAppId = clockWorkSyncAppointment.AppointmentId,
									UniqueId = externalAppointment.UniqueId,
									UniqueId2 = externalAppointment.UniqueId2,
									GlobalAppId = externalAppointment.LegacyGlobalAppointmentId
								}, externalAppointment.LastModifiedTime);
								CWLogger.Logger.Info("Sync:UpdatedClockWorkAppointment:appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
							}
							else
							{
								CWLogger.Logger.Warn("ExecuteSyncAction:UpdateClockWorkApp: Update ClockWork App failed appid={0}:sd={1}exappuniqueid2={2}", (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(), (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"), externalAppointment.UniqueId2 ?? "NULL");
							}
							break;
						}
						case eClockWorkExternalApplicationSyncActionType.UpdateExternalAppointment:
						{
							ExternalAppointment externalAppointment3 = clockWorkSyncAppointment.ToExternalCalendarAppointment(this.OpContext.SyncSettings);
							bool flag19 = externalAppointment != null && externalAppointment.Organizer != null;
							if (flag19)
							{
								externalAppointment3.Organizer = externalAppointment.Organizer;
							}
							externalAppointment = externalAppointment3;
							this.ExternalAppointmentManager.UpdateAppointment(externalAppointment);
							CWLogger.Logger.Info("ExecuteSyncAction:UpdateExternalCalendarApp:uniqueid2={3}:uniqueid={0}:globalappid={1}:cwappid={4}:sd={2}", new object[]
							{
								(externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString(),
								(externalAppointment == null || externalAppointment.LegacyGlobalAppointmentId == null) ? "NULL" : externalAppointment.LegacyGlobalAppointmentId.ToString(),
								(externalAppointment == null) ? "NULL" : externalAppointment.StartDate.ToString("yyyy-MM-dd H:mm"),
								(externalAppointment == null) ? "NULL" : externalAppointment.UniqueId2,
								(externalAppointment == null || externalAppointment.Mapping == null) ? "NULL" : externalAppointment.Mapping.ClockWorkAppointmentId.ToString()
							});
							this.ExternalAppointmentManager.ReloadExternalCalendarLastDateTimeModified(externalAppointment);
							externalAppointment.Mapping.ClockWorkLastUpdatedDate = new DateTime?(clockWorkSyncAppointment.LastModifiedTime);
							this.AppointmentSyncMappingManager.UpdateMappingClockWorkChange(clockWorkSyncAppointment.AppointmentId, clockWorkSyncAppointment.Mapping.ClockWorkLastUpdatedDate.Value);
							this.AppointmentSyncMappingManager.UpdateMappingExternalChange(new ExternalAppointmentId
							{
								ClockWorkAppId = clockWorkSyncAppointment.AppointmentId,
								UniqueId = externalAppointment.UniqueId,
								UniqueId2 = externalAppointment.UniqueId2,
								GlobalAppId = externalAppointment.LegacyGlobalAppointmentId
							}, externalAppointment.LastModifiedTime);
							break;
						}
						case eClockWorkExternalApplicationSyncActionType.DeleteClockWorkAppointment:
						{
							bool flag20 = clockWorkSyncAppointment != null && clockWorkSyncAppointment.AppointmentId > 0;
							if (flag20)
							{
								this.ClockWorkSyncAppointmentManager.CancelClockWorkSyncAppointment(false, clockWorkSyncAppointment.AppointmentId);
							}
							CWLogger.Logger.Debug("ExecuteSyncAction:DeleteClockWorkApp:appid={0}:sd={1}", (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(), (clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
							bool flag21 = clockWorkSyncAppointment != null && clockWorkSyncAppointment.Mapping != null;
							if (flag21)
							{
								this.AppointmentSyncMappingManager.DeleteMapping(clockWorkSyncAppointment.Mapping);
							}
							CWLogger.Logger.Info("Sync:DeletedClockWorkAppointment:appid={0}:globalappid={1}:startdate={2}", clockWorkSyncAppointment.AppointmentId.ToString(), externalAppointment.UniqueId, clockWorkSyncAppointment.StartDateTime.ToString("yyyy-MM-dd H:mm"));
							break;
						}
						case eClockWorkExternalApplicationSyncActionType.DeleteExternalAppointment:
						{
							this.ExternalAppointmentManager.DeleteAppointment(new ExternalAppointmentId
							{
								ClockWorkAppId = ((externalAppointment.Mapping != null) ? externalAppointment.Mapping.ClockWorkAppointmentId : 0),
								UniqueId = externalAppointment.UniqueId,
								GlobalAppId = externalAppointment.LegacyGlobalAppointmentId,
								UniqueId2 = externalAppointment.UniqueId2
							});
							CWLogger.Logger.Info("ExecuteSyncAction:DeleteExternalCalendarApp:uniqueid={0}:globalappid={1}:sd={2}", (externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.LegacyGlobalAppointmentId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.StartDate.ToString("yyyy-MM-dd H:mm"));
							bool flag22 = externalAppointment != null && externalAppointment.Mapping != null;
							if (flag22)
							{
								this.AppointmentSyncMappingManager.DeleteMapping(externalAppointment.Mapping);
							}
							break;
						}
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("Sync:Error:action={0}:cwapp={1}:externalcalendarapp={2}.\n{3}", new object[]
						{
							(clockWorkExternalApplicationSyncAction == null) ? "NULL" : Enum.GetName(typeof(eClockWorkExternalApplicationSyncActionType), clockWorkExternalApplicationSyncAction.SyncAction),
							(clockWorkSyncAppointment == null) ? "NULL" : clockWorkSyncAppointment.AppointmentId.ToString(),
							(externalAppointment == null) ? "NULL" : externalAppointment.UniqueId,
							ex.ToString()
						}), ex);
					}
				}
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00060A0C File Offset: 0x0005EC0C
		private bool IsValidExternalMapping(ClockWorkExternalAppMapping mapping)
		{
			return !string.IsNullOrEmpty(mapping.ExternalApplicationGlobalAppointmentId) || !string.IsNullOrEmpty(mapping.ExternalApplicationUniqueAppointmentId) || !string.IsNullOrEmpty(mapping.ExternalApplicationUniqueAppointmentId2);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00060A4C File Offset: 0x0005EC4C
		private bool CompareAttendees(IList<ExternalAttendee> attendees1, IList<ExternalAttendee> attendees2)
		{
			List<ExternalAttendee> list = attendees2.ToList<ExternalAttendee>();
			List<ExternalAttendee> list2 = attendees1.ToList<ExternalAttendee>();
			using (List<ExternalAttendee>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExternalAttendee att1 = enumerator.Current;
					ExternalAttendee externalAttendee = list.Find((ExternalAttendee att2) => att2.Username.Equals(att1.Username, StringComparison.OrdinalIgnoreCase));
					bool flag = externalAttendee == null;
					if (flag)
					{
						return false;
					}
				}
			}
			using (List<ExternalAttendee>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ExternalAttendee att2 = enumerator2.Current;
					ExternalAttendee externalAttendee2 = list2.Find((ExternalAttendee att1) => att1.Username.Equals(att2.Username, StringComparison.OrdinalIgnoreCase));
					bool flag2 = externalAttendee2 == null;
					if (flag2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00060B4C File Offset: 0x0005ED4C
		protected virtual List<ClockWorkSyncAppointment> LoadClockWorkAppointments(List<int> PersonIds, DateTime StartDate, DateTime EndDate, bool IncludeCancelled)
		{
			return this.ClockWorkSyncAppointmentManager.LoadClockWorkAppointments(PersonIds, StartDate, EndDate, IncludeCancelled);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00060B70 File Offset: 0x0005ED70
		private List<ExternalAppointment> LoadExternalAppointments(DateTime syncStart, DateTime syncEnd, out List<ClockWorkExternalApplicationSyncUser> userSynced)
		{
			userSynced = new List<ClockWorkExternalApplicationSyncUser>();
			List<ExternalAppointment> list = new List<ExternalAppointment>();
			foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser in this.OpContext.SyncSettings.SyncUsers)
			{
				this.OpContext.CalendarUsername = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername;
				IList<ExternalAppointment> list2 = null;
				try
				{
					CWLogger.Logger.Trace("CalendarSyncManager::LoadExternalAppointments:: Loading external appointments for user '{0}'", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername ?? "NULL");
					list2 = this.ExternalAppointmentManager.LoadAppointments(new ExternalAttendee
					{
						Username = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername,
						Name = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername
					}, syncStart, syncEnd);
					userSynced.Add(clockWorkExternalApplicationSyncUser);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::LoadExternalAppointments:: Call to LoadAppointments for '{0}' failed: {1}", clockWorkExternalApplicationSyncUser.ExternalApplicationUsername, ex.ToString()), ex);
					continue;
				}
				List<ExternalAppointment> list3 = new List<ExternalAppointment>();
				List<ExternalAppointment> list4 = new List<ExternalAppointment>();
				bool flag = list2 != null;
				if (flag)
				{
					using (IEnumerator<ExternalAppointment> enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ExternalAppointment app = enumerator2.Current;
							try
							{
								ExternalAppointment externalAppointment = list.FirstOrDefault((ExternalAppointment oa) => this.ExternalAppointmentManager.AppointmentsAreEqual(oa, app));
								bool flag2 = externalAppointment != null;
								if (flag2)
								{
									bool flag3 = app.Attendees.Count == 0 || app.Attendees[0].Username.Equals(clockWorkExternalApplicationSyncUser.ExternalApplicationUsername, StringComparison.OrdinalIgnoreCase);
									if (flag3)
									{
										externalAppointment.UniqueId = app.UniqueId;
									}
								}
								else
								{
									list4.Add(app);
								}
							}
							catch (Exception ex2)
							{
								CWLogger.Logger.ErrorException(string.Format("CalendarSyncManager::LoadOutlookAppointments:: {0}", ex2.ToString()), ex2);
							}
						}
					}
				}
				list.AddRange(list4.ToArray());
				foreach (ExternalAppointment externalAppointment2 in list3)
				{
					string uniqueId = externalAppointment2.UniqueId;
					for (int i = 0; i < list.Count; i++)
					{
						ExternalAppointment externalAppointment3 = list[i];
						bool flag4 = externalAppointment3.UniqueId.Equals(uniqueId);
						if (flag4)
						{
							list.RemoveAt(i);
							break;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00060E90 File Offset: 0x0005F090
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00060E98 File Offset: 0x0005F098
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x020003D2 RID: 978
		private class SyncAppointmentChangePair
		{
			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x060018BD RID: 6333 RVA: 0x0008F315 File Offset: 0x0008D515
			// (set) Token: 0x060018BE RID: 6334 RVA: 0x0008F31D File Offset: 0x0008D51D
			public ClockWorkSyncAppointmentChange ClockWorkSyncAppointmentChange { get; set; }

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x060018BF RID: 6335 RVA: 0x0008F326 File Offset: 0x0008D526
			// (set) Token: 0x060018C0 RID: 6336 RVA: 0x0008F32E File Offset: 0x0008D52E
			public ExternalSyncAppointmentChange ExternalSyncAppointmentChange { get; set; }
		}
	}
}
