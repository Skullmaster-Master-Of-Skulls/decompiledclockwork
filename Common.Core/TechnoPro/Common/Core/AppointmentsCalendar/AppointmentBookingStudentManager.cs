using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Core.AvailabilitySchedule;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.DAO.Impl.AppointmentsCalendar;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.ICore.AvailabilitySchedule;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentsCalendar
{
	// Token: 0x02000148 RID: 328
	public class AppointmentBookingStudentManager : IAppointmentBookingStudentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0006E3D6 File Offset: 0x0006C5D6
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0006E3DE File Offset: 0x0006C5DE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0006E3E7 File Offset: 0x0006C5E7
		public AppointmentBookingStudentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0006E3FC File Offset: 0x0006C5FC
		public IList<ChannelCalendarWithAvailability> LoadAvailabilityForChannel(int studentPersonId, string channelId, string optionalCalendarName, DateTime startDate, int numDays)
		{
			Channel channel;
			return this.LoadAvailabilityForChannel(studentPersonId, channelId, optionalCalendarName, startDate, numDays, out channel);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0006E420 File Offset: 0x0006C620
		private IList<ChannelCalendarWithAvailability> LoadAvailabilityForChannel(int studentPersonId, string channelId, string optionalCalendarName, DateTime startDate, int numDays, out Channel foundChannel)
		{
			AppointmentBookingStudentManager.<>c__DisplayClass6_0 CS$<>8__locals1 = new AppointmentBookingStudentManager.<>c__DisplayClass6_0();
			CS$<>8__locals1.channelId = channelId;
			CS$<>8__locals1.optionalCalendarName = optionalCalendarName;
			WebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			CutoffTime cutoffTime = webSettingManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_CutoffForBooking).CutoffTimeFromXml();
			CS$<>8__locals1.cutoffTimeMinDateTime = (((cutoffTime != null && cutoffTime.Enabled) ? cutoffTime.GetMinimumDateForBeforeTypeCutoff() : new DateTime?(DateTime.Now)) ?? DateTime.Now);
			IList<Channel> activeChannelsForStudent = this.GetActiveChannelsForStudent(studentPersonId);
			foundChannel = activeChannelsForStudent.FirstOrDefault((Channel g) => g.Id != null && g.Id.Equals(CS$<>8__locals1.channelId ?? "", StringComparison.OrdinalIgnoreCase));
			bool flag = foundChannel == null;
			IList<ChannelCalendarWithAvailability> result;
			if (flag)
			{
				result = new List<ChannelCalendarWithAvailability>();
			}
			else
			{
				bool flag2 = !string.IsNullOrWhiteSpace(CS$<>8__locals1.optionalCalendarName);
				IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(this.OpContext);
				List<ChannelCalendarWithAvailability> list = new List<ChannelCalendarWithAvailability>();
				using (IEnumerator<ChannelAvailability> enumerator = foundChannel.Availabilities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AppointmentBookingStudentManager.<>c__DisplayClass6_1 CS$<>8__locals2 = new AppointmentBookingStudentManager.<>c__DisplayClass6_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.availability = enumerator.Current;
						IList<ChannelPersonCollection> list2;
						if (!flag2)
						{
							list2 = CS$<>8__locals2.availability.PersonCollection;
						}
						else
						{
							IEnumerable<ChannelPersonCollection> personCollection = CS$<>8__locals2.availability.PersonCollection;
							Func<ChannelPersonCollection, bool> predicate;
							if ((predicate = CS$<>8__locals2.CS$<>8__locals1.<>9__1) == null)
							{
								predicate = (CS$<>8__locals2.CS$<>8__locals1.<>9__1 = ((ChannelPersonCollection g) => g.Title.Equals(CS$<>8__locals2.CS$<>8__locals1.optionalCalendarName, StringComparison.OrdinalIgnoreCase)));
							}
							IList<ChannelPersonCollection> list3 = personCollection.Where(predicate).ToList<ChannelPersonCollection>();
							list2 = list3;
						}
						IList<ChannelPersonCollection> list4 = list2;
						List<int> list5 = (from q in (from m in list4.SelectMany((ChannelPersonCollection g) => g.UnderlyingPeople)
						select m.PersonId).Distinct<int>()
						where q > 0
						select q).ToList<int>();
						bool flag3 = list5.Count < 1;
						if (!flag3)
						{
							List<AvailabilityScheduleContext> contexts = (from g in list5
							select new AvailabilityScheduleContext
							{
								PersonId = g,
								AvailabilityGroupId = CS$<>8__locals2.availability.AvailabilityGroupId
							}).ToList<AvailabilityScheduleContext>();
							IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(this.OpContext);
							IList<Holiday> holidays = appointmentHolidayManager.LoadHolidays(startDate.Date, startDate.Date.AddDays((double)numDays));
							IList<AvailabilityScheduleItemsForContext> list6 = availabilityScheduleManager.LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(contexts, startDate, numDays);
							Func<AvailabilityScheduleItemInfo, bool> <>9__6;
							foreach (AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext in list6)
							{
								AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext2 = availabilityScheduleItemsForContext;
								IEnumerable<AvailabilityScheduleItemInfo> availabilityScheduleItems = availabilityScheduleItemsForContext.AvailabilityScheduleItems;
								Func<AvailabilityScheduleItemInfo, bool> predicate2;
								if ((predicate2 = <>9__6) == null)
								{
									predicate2 = (<>9__6 = delegate(AvailabilityScheduleItemInfo g)
									{
										DateTime dt = g.DayAndTime.Date.Date;
										bool flag7 = holidays.Any((Holiday h) => h.Date.Date == dt);
										bool result2;
										if (flag7)
										{
											result2 = false;
										}
										else
										{
											bool flag8 = dt < CS$<>8__locals2.CS$<>8__locals1.cutoffTimeMinDateTime.Date;
											if (flag8)
											{
												result2 = false;
											}
											else
											{
												DateTime dateTime = dt.Add(g.DayAndTime.Time.StartTime);
												bool flag9 = dateTime < CS$<>8__locals2.CS$<>8__locals1.cutoffTimeMinDateTime;
												if (flag9)
												{
													DateTime t = dt.Add(g.DayAndTime.Time.EndTime);
													DateTime dateTime2 = dateTime.AddMinutes((double)((Convert.ToInt32((CS$<>8__locals2.CS$<>8__locals1.cutoffTimeMinDateTime - dateTime).TotalMinutes / (double)CS$<>8__locals2.availability.SlotSizeInMinutes) + 1) * CS$<>8__locals2.availability.SlotSizeInMinutes));
													bool flag10 = dateTime2.AddMinutes((double)CS$<>8__locals2.availability.SlotSizeInMinutes) > t;
													if (flag10)
													{
														result2 = false;
													}
													else
													{
														g.DayAndTime.Time.StartTime = dateTime2.TimeOfDay;
														result2 = true;
													}
												}
												else
												{
													result2 = true;
												}
											}
										}
										return result2;
									});
								}
								availabilityScheduleItemsForContext2.AvailabilityScheduleItems = availabilityScheduleItems.Where(predicate2).ToList<AvailabilityScheduleItemInfo>();
							}
							int slotSizeInMinutes = CS$<>8__locals2.availability.SlotSizeInMinutes;
							using (IEnumerator<ChannelPersonCollection> enumerator3 = list4.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									ChannelPersonCollection calendar = enumerator3.Current;
									ChannelCalendarWithAvailability channelCalendarWithAvailability = list.FirstOrDefault((ChannelCalendarWithAvailability g) => g.CalendarTitle.Equals(calendar.Title, StringComparison.OrdinalIgnoreCase));
									bool flag4 = channelCalendarWithAvailability == null;
									if (flag4)
									{
										channelCalendarWithAvailability = new ChannelCalendarWithAvailability
										{
											CalendarTitle = calendar.Title,
											Availabilities = new List<AvailabilityForChannelCalendar>()
										};
										list.Add(channelCalendarWithAvailability);
									}
									List<AvailabilityScheduleItemsForContext> list7 = (from g in list6
									where calendar.UnderlyingPeople.Any((ChannelUnderlyingPerson m) => m.PersonId == g.Context.PersonId)
									select g).ToList<AvailabilityScheduleItemsForContext>();
									using (List<AvailabilityScheduleItemsForContext>.Enumerator enumerator4 = list7.GetEnumerator())
									{
										while (enumerator4.MoveNext())
										{
											AvailabilityScheduleItemsForContext availabilityItems = enumerator4.Current;
											List<AvailabilityScheduleItemInfo> list8 = availabilityItems.AvailabilityScheduleItems.ToList<AvailabilityScheduleItemInfo>();
											list8.Sort((AvailabilityScheduleItemInfo g1, AvailabilityScheduleItemInfo g2) => g1.DayAndTime.Date.Add(g1.DayAndTime.Time.StartTime).CompareTo(g2.DayAndTime.Date.Add(g2.DayAndTime.Time.StartTime)));
											foreach (AvailabilityScheduleItemInfo availabilityScheduleItemInfo in list8)
											{
												DateTime date = availabilityScheduleItemInfo.DayAndTime.Date.Date;
												TimeSpan endTime = availabilityScheduleItemInfo.DayAndTime.Time.EndTime;
												TimeSpan value = availabilityScheduleItemInfo.DayAndTime.Time.StartTime;
												TimeSpan timeSpan = value.Add(TimeSpan.FromMinutes((double)slotSizeInMinutes));
												while (timeSpan <= endTime)
												{
													DateTime sdt = date.Add(value);
													DateTime edt = date.Add(timeSpan);
													AvailabilityForChannelCalendar availabilityForChannelCalendar = channelCalendarWithAvailability.Availabilities.FirstOrDefault((AvailabilityForChannelCalendar g) => g.StartDateTime == sdt && g.EndDateTime == edt && g.AvailabilityGroupId == availabilityItems.Context.AvailabilityGroupId);
													bool flag5 = availabilityForChannelCalendar == null;
													if (flag5)
													{
														channelCalendarWithAvailability.Availabilities.Add(new AvailabilityForChannelCalendar
														{
															PersonIds = new int[]
															{
																availabilityItems.Context.PersonId
															}.ToList<int>(),
															AvailabilityGroupId = availabilityItems.Context.AvailabilityGroupId,
															AvailabilityTitle = (CS$<>8__locals2.availability.Title ?? ("? " + CS$<>8__locals2.availability.AvailabilityGroupId.ToString())),
															StartDateTime = date.Add(value),
															EndDateTime = date.Add(timeSpan)
														});
													}
													else
													{
														bool flag6 = !availabilityForChannelCalendar.PersonIds.Contains(availabilityItems.Context.PersonId);
														if (flag6)
														{
															availabilityForChannelCalendar.PersonIds.Add(availabilityItems.Context.PersonId);
														}
													}
													value = timeSpan;
													timeSpan = value.Add(TimeSpan.FromMinutes((double)slotSizeInMinutes));
												}
											}
										}
									}
								}
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0006EB18 File Offset: 0x0006CD18
		private IList<Channel> GetAllActiveChannels()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<Channel> list = cacheStorageManager["allActiveChannels"] as IList<Channel>;
			bool flag = list != null;
			IList<Channel> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				WebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
				string settingValue = webSettingManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_availabilitygroupidsdurations);
				string settingValue2 = webSettingManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_Channels);
				IAppointmentBookingStudentDAO appointmentBookingStudentDAO = new AppointmentBookingStudentDAO(this.OpContext);
				IList<Channel> allChannels = appointmentBookingStudentDAO.GetAllChannels(settingValue, settingValue2);
				list = (from g in allChannels
				where g.IsActive
				select g).ToList<Channel>();
				foreach (Channel channel in list)
				{
					Channel channel2 = channel;
					IList<ChannelAvailability> availabilities = channel.Availabilities;
					IList<ChannelAvailability> list2;
					if (availabilities == null)
					{
						list2 = null;
					}
					else
					{
						list2 = (from g in availabilities
						where g.IsActive
						select g).ToList<ChannelAvailability>();
					}
					channel2.Availabilities = (list2 ?? new List<ChannelAvailability>());
					foreach (ChannelAvailability channelAvailability in channel.Availabilities)
					{
						ChannelAvailability channelAvailability2 = channelAvailability;
						IList<ChannelPersonCollection> personCollection = channelAvailability.PersonCollection;
						IList<ChannelPersonCollection> list3;
						if (personCollection == null)
						{
							list3 = null;
						}
						else
						{
							list3 = (from g in personCollection
							where g.IsActive
							select g).ToList<ChannelPersonCollection>();
						}
						channelAvailability2.PersonCollection = (list3 ?? new List<ChannelPersonCollection>());
					}
				}
				cacheStorageManager.Insert("allActiveChannels", list, TimeSpan.FromHours(8.0));
				result = list;
			}
			return result;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
		public IList<Channel> GetActiveChannelsForStudent(int studentPersonId)
		{
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager(this.OpContext.TenantId);
			IList<Channel> list = userDatabaseCacheStorageManager[studentPersonId, "studentsActiveChannels"] as IList<Channel>;
			bool flag = list != null;
			IList<Channel> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IList<Channel> allActiveChannels = this.GetAllActiveChannels();
				bool flag2 = allActiveChannels.Any((Channel activeChannel) => activeChannel.Availabilities.Any((ChannelAvailability availability) => availability.IsActive && availability.UseAssignedAdvisorInsteadOfPersonCollection));
				bool flag3 = !flag2;
				if (flag3)
				{
					result = allActiveChannels;
				}
				else
				{
					int? num = null;
					foreach (Channel channel in allActiveChannels)
					{
						foreach (ChannelAvailability channelAvailability in channel.Availabilities)
						{
							bool flag4 = !channelAvailability.UseAssignedAdvisorInsteadOfPersonCollection;
							if (!flag4)
							{
								int[] useAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids = channelAvailability.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids;
								bool flag5 = useAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids != null && useAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids.Length != 0;
								int[] array;
								if (flag5)
								{
									array = useAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids;
								}
								else
								{
									bool flag6 = num == null;
									if (flag6)
									{
										IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
										num = new int?(oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AssignedCounsellorCid));
									}
									int[] array2;
									if (num.Value <= 0)
									{
										array2 = new int[0];
									}
									else
									{
										(array2 = new int[1])[0] = num.Value;
									}
									array = array2;
								}
								bool flag7 = array.Length != 0;
								if (flag7)
								{
									IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
									List<DynamicData> source = dynamicDataManager.LoadDataByFields(new DynamicDataContext
									{
										PrimaryId = studentPersonId
									}, array.Distinct<int>().ToList<int>(), eDynamicFormType.PerStudent);
									List<int> personIds = (from g in source
									select g.ValueId).Distinct<int>().ToList<int>();
									IPeopleManager peopleManager = new PeopleManager(this.OpContext);
									IList<PersonBase> source2 = peopleManager.LoadPersonsByIds(personIds) ?? new List<PersonBase>();
									channelAvailability.PersonCollection = (from g in source2
									select new ChannelPersonCollection
									{
										Title = g.GetName(),
										IsActive = true,
										UnderlyingPeople = new ChannelUnderlyingPerson[]
										{
											new ChannelUnderlyingPerson
											{
												PersonId = g.PersonId
											}
										}.ToList<ChannelUnderlyingPerson>()
									}).ToList<ChannelPersonCollection>();
								}
							}
						}
					}
					userDatabaseCacheStorageManager.Insert(studentPersonId, "studentsActiveChannels", TimeSpan.FromMinutes(30.0));
					result = allActiveChannels;
				}
			}
			return result;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0006EFA8 File Offset: 0x0006D1A8
		private AppointmentBookingFilterParameters GetOnlineAppointmentBookingFilterParameters()
		{
			WebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			CutoffTime cutoffTime = webSettingManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_CutoffForBooking).CutoffTimeFromXml();
			return new AppointmentBookingFilterParameters
			{
				AllowDoubleBookingStaff = false,
				AllowDoubleBookingStudent = !webSettingManager.GetSettingValue<bool>(Setting.APPOINTMENTBOOKING_NoConsecutiveOrOverlapping),
				MaxNumberOfAppointmentsInFuture = webSettingManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_maxNumApptsInFuture),
				MaxNumberOfAppointmentsPerWeek = webSettingManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_MaxNumAppsPerWeek),
				MaxNumberOfAppointmentsPerDay = webSettingManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_MaxNumAppsPerDay),
				CutoffTime = ((cutoffTime == null) ? null : cutoffTime),
				BannedExpiryDateCid = webSettingManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid)
			};
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0006F054 File Offset: 0x0006D254
		public AppointmentBookingRes TryToBookStudentAppointment(int studentPersonId, string channelId, int availabilityGroupId, string calendarTitle, DateTime start, DateTime end)
		{
			IList<IStudentAppointmentBookingRuleManager> allStudentRuleManagers = StudentAppointmentBookingRuleFactory.GetAllStudentRuleManagers(this.OpContext);
			AppointmentBookingReq appointmentBookingReq = new AppointmentBookingReq
			{
				StudentPersonId = studentPersonId,
				StartDateTime = start,
				EndDateTime = end
			};
			AppointmentBookingFilterParameters onlineAppointmentBookingFilterParameters = this.GetOnlineAppointmentBookingFilterParameters();
			AppointmentBookingRes appointmentBookingRes = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStudent, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
			bool flag = !appointmentBookingRes.PassedChecks;
			AppointmentBookingRes result;
			if (flag)
			{
				result = appointmentBookingRes;
			}
			else
			{
				AppointmentBookingRes appointmentBookingRes2 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
				bool flag2 = !appointmentBookingRes2.PassedChecks;
				if (flag2)
				{
					result = appointmentBookingRes2;
				}
				else
				{
					AppointmentBookingRes appointmentBookingRes3 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
					bool flag3 = !appointmentBookingRes3.PassedChecks;
					if (flag3)
					{
						result = appointmentBookingRes3;
					}
					else
					{
						Channel channel;
						IList<ChannelCalendarWithAvailability> list = this.LoadAvailabilityForChannel(studentPersonId, channelId, calendarTitle, start.Date, 1, out channel);
						ChannelAvailability channelAvailability = (channel != null) ? channel.Availabilities.FirstOrDefault((ChannelAvailability g) => g.AvailabilityGroupId == availabilityGroupId) : null;
						List<AvailabilityForChannelCalendar> list2 = new List<AvailabilityForChannelCalendar>();
						IEnumerable<ChannelCalendarWithAvailability> source = list;
						Func<ChannelCalendarWithAvailability, bool> <>9__1;
						Func<ChannelCalendarWithAvailability, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((ChannelCalendarWithAvailability g) => !string.IsNullOrWhiteSpace(g.CalendarTitle) && g.CalendarTitle.Equals(calendarTitle, StringComparison.OrdinalIgnoreCase)));
						}
						Func<AvailabilityForChannelCalendar, bool> <>9__2;
						foreach (ChannelCalendarWithAvailability channelCalendarWithAvailability in source.Where(predicate))
						{
							List<AvailabilityForChannelCalendar> list3 = list2;
							IEnumerable<AvailabilityForChannelCalendar> availabilities = channelCalendarWithAvailability.Availabilities;
							Func<AvailabilityForChannelCalendar, bool> predicate2;
							if ((predicate2 = <>9__2) == null)
							{
								predicate2 = (<>9__2 = ((AvailabilityForChannelCalendar g) => g.AvailabilityGroupId == availabilityGroupId && g.StartDateTime.Hour == start.Hour && g.StartDateTime.Minute == start.Minute && g.EndDateTime.Hour == end.Hour && g.EndDateTime.Minute == end.Minute));
							}
							list3.AddRange(availabilities.Where(predicate2).ToList<AvailabilityForChannelCalendar>());
						}
						bool flag4 = list2.Count < 1;
						if (flag4)
						{
							result = new AppointmentBookingRes
							{
								PassedChecks = false,
								PublicMessage = "The time slot you selected is no longer available.",
								PrivateMessage = "Invalid request - couldn't find availability"
							};
						}
						else
						{
							AppointmentBookingRes appointmentBookingRes4 = null;
							foreach (AvailabilityForChannelCalendar availabilityForChannelCalendar in list2)
							{
								IList<int> personIds = availabilityForChannelCalendar.PersonIds;
								foreach (int num in personIds)
								{
									appointmentBookingReq.StaffPersonId = num;
									appointmentBookingRes4 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStaffToBookWith, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
									bool flag5 = !appointmentBookingRes4.PassedChecks;
									if (!flag5)
									{
										PersonBase personBase = new PersonBase
										{
											PersonId = studentPersonId
										};
										Appointment appointment = new Appointment
										{
											Attendees = new List<Attendee>
											{
												new Attendee
												{
													Person = personBase
												},
												new Attendee
												{
													Person = new PersonBase
													{
														PersonId = num
													}
												}
											},
											StartDateTime = appointmentBookingReq.StartDateTime,
											EndDateTime = appointmentBookingReq.EndDateTime,
											AppType = new AppType
											{
												AppTypeId = ((channelAvailability != null) ? channelAvailability.AppTypeIdToBookWith : -1)
											},
											WhoBooked = personBase
										};
										IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
										int num2;
										if (!onlineAppointmentBookingFilterParameters.AllowDoubleBookingStaff || !onlineAppointmentBookingFilterParameters.AllowDoubleBookingStudent)
										{
											num2 = appointmentManager.CreateAppointmentEnsureUsersNotDoubleBooked(false, appointment, (from h in new int[]
											{
												onlineAppointmentBookingFilterParameters.AllowDoubleBookingStudent ? 0 : studentPersonId,
												onlineAppointmentBookingFilterParameters.AllowDoubleBookingStaff ? 0 : num
											}
											where h > 0
											select h).ToArray<int>());
										}
										else
										{
											num2 = appointmentManager.CreateAppointment(false, appointment);
										}
										int num3 = num2;
										bool flag6 = num3 < 1;
										if (!flag6)
										{
											return new AppointmentBookingRes
											{
												PassedChecks = true,
												AppointmentId = num3
											};
										}
										appointmentBookingRes4 = new AppointmentBookingRes
										{
											PassedChecks = false,
											PublicMessage = "Something went wrong when attempting to book the appointment.",
											PrivateMessage = "Failed to book appointment"
										};
									}
								}
							}
							bool flag7 = appointmentBookingRes4 != null;
							if (flag7)
							{
								result = appointmentBookingRes4;
							}
							else
							{
								result = new AppointmentBookingRes
								{
									PassedChecks = false,
									PublicMessage = "Unspecified error when attempting to book the appointment",
									PrivateMessage = "Unspecified error"
								};
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0006F4D4 File Offset: 0x0006D6D4
		public AppointmentBookingRes ValidateBookStudentAppointment(int studentPersonId, DateTime? date, TimeSpan? startTime, TimeSpan? endTime)
		{
			IList<IStudentAppointmentBookingRuleManager> allStudentRuleManagers = StudentAppointmentBookingRuleFactory.GetAllStudentRuleManagers(this.OpContext);
			AppointmentBookingReq appointmentBookingReq = new AppointmentBookingReq
			{
				StudentPersonId = studentPersonId
			};
			AppointmentBookingFilterParameters onlineAppointmentBookingFilterParameters = this.GetOnlineAppointmentBookingFilterParameters();
			bool flag = studentPersonId > 0;
			if (flag)
			{
				AppointmentBookingRes appointmentBookingRes = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStudent, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
				bool flag2 = !appointmentBookingRes.PassedChecks;
				if (flag2)
				{
					return appointmentBookingRes;
				}
			}
			bool flag3 = date != null;
			if (flag3)
			{
				appointmentBookingReq.StartDateTime = date.Value;
				AppointmentBookingRes appointmentBookingRes2 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
				bool flag4 = !appointmentBookingRes2.PassedChecks;
				if (flag4)
				{
					return appointmentBookingRes2;
				}
				bool flag5 = startTime != null && endTime != null;
				if (flag5)
				{
					appointmentBookingReq.StartDateTime = date.Value.Add(startTime.Value);
					appointmentBookingReq.EndDateTime = date.Value.Add(endTime.Value);
					AppointmentBookingRes appointmentBookingRes3 = StudentAppointmentBookingRuleFactory.ExecuteBookingFilters(allStudentRuleManagers, eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, appointmentBookingReq, onlineAppointmentBookingFilterParameters);
					bool flag6 = !appointmentBookingRes3.PassedChecks;
					if (flag6)
					{
						return appointmentBookingRes3;
					}
				}
			}
			return new AppointmentBookingRes
			{
				PassedChecks = true
			};
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0006F600 File Offset: 0x0006D800
		public DateTime? MarkStudentBannedFromOnlineAppointmentBooking(int PersonId)
		{
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
			bool flag = settingValue < 1;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int settingValue2 = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedNumDays);
				DateTime dateTime = DateTime.Now.Date.AddDays((double)settingValue2).AddDays(1.0).AddMinutes(-1.0);
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				DynamicData item = new DynamicData
				{
					Field = dynamicFieldManager.LoadFieldByControlId(settingValue),
					Value = dateTime
				};
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				dynamicDataManager.SaveData(new DynamicDataContext
				{
					PrimaryId = PersonId
				}, new List<DynamicData>
				{
					item
				}, eDynamicFormType.PerStudent);
				result = new DateTime?(dateTime);
			}
			return result;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
		public bool IsStudentBannedFromOnlineAppointmentBooking(int PersonId)
		{
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
			bool flag = settingValue < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				IList<IDynamicDataSerializableItem> list = dynamicDataManager.LoadDynamicDataItemsByControlIds(new DynamicDataContext
				{
					PrimaryId = PersonId
				}, new List<int>
				{
					settingValue
				}, eDynamicFormType.PerStudent);
				bool flag2 = list == null || list.Count < 1;
				if (flag2)
				{
					result = false;
				}
				else
				{
					result = (from t in list
					select t.WriteToStorage() into storageItem
					where storageItem.DateTimeValue != null
					select storageItem.DateTimeValue.Value >= DateTime.Now).FirstOrDefault<bool>();
				}
			}
			return result;
		}
	}
}
