using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar
{
	// Token: 0x02000022 RID: 34
	public class AppointmentBookingStudentWebClientManager : IAppointmentBookingStudentWebClientManager
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00007960 File Offset: 0x00005B60
		private static void FillInAssignedAdvisorIntoAvailabilitiesWhereIndicated(ref IList<Channel> channels, int studentPersonId)
		{
			List<Channel> list = (from g in channels
			where g.Availabilities.Any((ChannelAvailability h) => h.UseAssignedAdvisorInsteadOfPersonCollection)
			select g).ToList<Channel>();
			bool flag = list.Count < 1;
			if (!flag)
			{
				IStudentCommonInfoClientManager studentCommonInfoClientManager = null;
				IDynamicDataClientManager dynamicDataClientManager = null;
				BasicPersonDTO basicPersonDTO = null;
				bool flag2 = false;
				bool flag3 = false;
				foreach (Channel channel in list)
				{
					List<ChannelAvailability> list2 = (from g in channel.Availabilities
					where g.UseAssignedAdvisorInsteadOfPersonCollection
					select g).ToList<ChannelAvailability>();
					foreach (ChannelAvailability channelAvailability in list2)
					{
						List<BasicPersonDTO> list3 = new List<BasicPersonDTO>();
						List<int> list4 = (from g in channelAvailability.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids ?? new int[0]
						where g > 0
						select g).Distinct<int>().ToList<int>();
						bool flag4 = list4.Count < 1;
						if (flag4)
						{
							bool flag5 = !flag2;
							if (flag5)
							{
								bool flag6 = studentCommonInfoClientManager == null;
								if (flag6)
								{
									studentCommonInfoClientManager = new StudentCommonInfoClientManager();
								}
								StudentCommonInfoDTO studentCommonInfoDTO = studentCommonInfoClientManager.LoadStudentCommonInfo(studentPersonId);
								BasicPersonDTO basicPersonDTO2;
								if (studentCommonInfoDTO != null && studentCommonInfoDTO.AssignedCounsellor != null && studentCommonInfoDTO.AssignedCounsellor.PersonId >= 1)
								{
									PersonBaseDTO assignedCounsellor = studentCommonInfoDTO.AssignedCounsellor;
									basicPersonDTO2 = ((assignedCounsellor != null) ? assignedCounsellor.BasicPersonFromPersonBase() : null);
								}
								else
								{
									basicPersonDTO2 = null;
								}
								basicPersonDTO = basicPersonDTO2;
								flag2 = true;
							}
							list3 = new List<BasicPersonDTO>();
							bool flag7 = basicPersonDTO != null;
							if (flag7)
							{
								list3.Add(basicPersonDTO);
							}
						}
						else
						{
							bool flag8 = dynamicDataClientManager == null;
							if (flag8)
							{
								dynamicDataClientManager = new DynamicDataClientManager();
							}
							IList<BasicPersonDTO> list5 = dynamicDataClientManager.LoadAssignedAdvisors(eDynamicFormType.PerStudent, studentPersonId, list4.ToArray());
							list3 = ((list5 != null) ? list5.ToList<BasicPersonDTO>() : null);
						}
						channelAvailability.PersonCollection = new List<ChannelPersonCollection>();
						bool flag9 = list3.Count == 1;
						if (flag9)
						{
							BasicPersonDTO basicPersonDTO3 = list3[0];
							channelAvailability.PersonCollection.Add(new ChannelPersonCollection
							{
								IsActive = true,
								UnderlyingPeople = new List<ChannelUnderlyingPerson>
								{
									new ChannelUnderlyingPerson
									{
										PersonId = basicPersonDTO3.PersonId
									}
								},
								Title = basicPersonDTO3.GetName()
							});
						}
						else
						{
							bool flag10 = list3.Count > 1;
							if (flag10)
							{
								bool flag11 = flag3;
								if (flag11)
								{
									List<ChannelUnderlyingPerson> list6 = new List<ChannelUnderlyingPerson>();
									foreach (BasicPersonDTO basicPersonDTO4 in list3)
									{
										list6.Add(new ChannelUnderlyingPerson
										{
											PersonId = basicPersonDTO4.PersonId
										});
									}
									channelAvailability.PersonCollection.Add(new ChannelPersonCollection
									{
										IsActive = true,
										UnderlyingPeople = list6,
										Title = "Assigned advisor"
									});
								}
								else
								{
									foreach (BasicPersonDTO basicPersonDTO5 in list3)
									{
										channelAvailability.PersonCollection.Add(new ChannelPersonCollection
										{
											IsActive = true,
											UnderlyingPeople = new List<ChannelUnderlyingPerson>
											{
												new ChannelUnderlyingPerson
												{
													PersonId = basicPersonDTO5.PersonId
												}
											},
											Title = basicPersonDTO5.GetName()
										});
									}
								}
							}
							else
							{
								channelAvailability.PersonCollection.Add(new ChannelPersonCollection
								{
									IsActive = true,
									UnderlyingPeople = new List<ChannelUnderlyingPerson>(),
									Title = "Assigned advisor"
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007D9C File Offset: 0x00005F9C
		public IList<Channel> GetAppointmentBookingActiveChannels(int studentPersonId)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			IList<Channel> list = (IList<Channel>)session["userCampusChannels"];
			bool flag = list == null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_availabilitygroupidsdurations);
				string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_Channels);
				IList<Channel> appointmentBooking_ActiveChannels = this.GetAppointmentBooking_ActiveChannels(settingValue, settingValue2);
				bool flag2 = appointmentBooking_ActiveChannels == null || appointmentBooking_ActiveChannels.Count < 1;
				if (flag2)
				{
					return appointmentBooking_ActiveChannels;
				}
				AppointmentBookingStudentWebClientManager.FillInAssignedAdvisorIntoAvailabilitiesWhereIndicated(ref appointmentBooking_ActiveChannels, studentPersonId);
				Channel channel = appointmentBooking_ActiveChannels.FirstOrDefault(delegate(Channel g)
				{
					bool result;
					if (g.Availabilities != null)
					{
						result = (g.Availabilities.FirstOrDefault(delegate(ChannelAvailability h)
						{
							bool result2;
							if (h.PersonCollection != null)
							{
								result2 = (h.PersonCollection.FirstOrDefault((ChannelPersonCollection gh) => gh.Campus != null && !string.IsNullOrEmpty(gh.Campus.CampusName)) != null);
							}
							else
							{
								result2 = false;
							}
							return result2;
						}) != null);
					}
					else
					{
						result = false;
					}
					return result;
				});
				bool flag3 = channel == null;
				if (flag3)
				{
					return appointmentBooking_ActiveChannels;
				}
				object obj = session["userCampusChannels"];
				string text = obj as string;
				bool flag4 = text == null;
				if (flag4)
				{
					IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
					DynamicFieldDTO dynamicFieldDTO = dynamicFieldClientManager.LoadFieldByName("campus");
					bool flag5 = dynamicFieldDTO != null;
					if (!flag5)
					{
						CWLogger.Logger.Warn("Common.UI.ClientManager.Web.Core.Impl.AppointmentsCalendar.AppointmentBookingStudentWebClientManager.GetAppointmentBookingActiveChannels:Can't find campus control (name=campus)");
						return appointmentBooking_ActiveChannels;
					}
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					IList<DynamicDataDTO> list2 = dynamicDataClientManager.LoadDataByFields(new DynamicDataContextDTO
					{
						PrimaryId = studentPersonId
					}, new List<int>
					{
						dynamicFieldDTO.ControlId
					}, eDynamicFormTypeDTO.PerStudent);
					text = ((list2 == null || list2.Count < 1 || list2[0].Value == null) ? "" : list2[0].Value.ToString());
				}
				list = this.ReturnCopyOfChannelsByCampus(appointmentBooking_ActiveChannels, text);
				session.Add("userCampusChannels", list);
			}
			return list;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007F54 File Offset: 0x00006154
		public bool IsStudentBannedFromOnlineAppointmentBooking(int PersonId)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
			bool flag = settingValue > 0;
			bool result;
			if (flag)
			{
				IsStudentBannedFromOnlineAppointmentBookingReq isStudentBannedFromOnlineAppointmentBookingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsStudentBannedFromOnlineAppointmentBookingReq>();
				isStudentBannedFromOnlineAppointmentBookingReq.PersonId = PersonId;
				result = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().IsStudentBannedFromOnlineAppointmentBooking(isStudentBannedFromOnlineAppointmentBookingReq).StudentIsBanned;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007FAC File Offset: 0x000061AC
		public DateTime? MarkStudentBannedFromOnlineAppointmentBooking(int PersonId)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.APPOINTMENTBOOKING_bannedExpiryDateCid);
			bool flag = settingValue > 0;
			DateTime? result;
			if (flag)
			{
				MarkStudentBannedFromOnlineAppointmentBookingReq markStudentBannedFromOnlineAppointmentBookingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkStudentBannedFromOnlineAppointmentBookingReq>();
				markStudentBannedFromOnlineAppointmentBookingReq.PersonId = PersonId;
				result = ClientServiceFactory.GetClientInstance<IAppointmentBookingStudent>().MarkStudentBannedFromOnlineAppointmentBooking(markStudentBannedFromOnlineAppointmentBookingReq).DateStudentWasBannedUntil;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000800C File Offset: 0x0000620C
		private IList<Channel> ReturnCopyOfChannelsByCampus(IList<Channel> allChannels, string campus)
		{
			bool flag = campus == null;
			IList<Channel> result;
			if (flag)
			{
				result = allChannels;
			}
			else
			{
				List<Channel> list = new List<Channel>();
				Func<ChannelPersonCollection, bool> <>9__0;
				foreach (Channel channel in allChannels)
				{
					Channel channel2 = channel.Clone();
					foreach (ChannelAvailability channelAvailability in channel2.Availabilities)
					{
						ChannelAvailability channelAvailability2 = channelAvailability;
						IList<ChannelPersonCollection> personCollection = channelAvailability.PersonCollection;
						IList<ChannelPersonCollection> list2;
						if (personCollection == null)
						{
							list2 = null;
						}
						else
						{
							Func<ChannelPersonCollection, bool> predicate;
							if ((predicate = <>9__0) == null)
							{
								predicate = (<>9__0 = delegate(ChannelPersonCollection g)
								{
									SchoolCampus campus2 = g.Campus;
									return (((campus2 != null) ? campus2.CampusName : null) ?? "").Equals(campus, StringComparison.OrdinalIgnoreCase);
								});
							}
							list2 = personCollection.Where(predicate).ToList<ChannelPersonCollection>();
						}
						channelAvailability2.PersonCollection = (list2 ?? new List<ChannelPersonCollection>());
					}
					list.Add(channel2);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008124 File Offset: 0x00006324
		private IList<Channel> GetAppointmentBooking_ActiveChannels(string xml_APPOINTMENTBOOKING_availabilitygroupidsdurations, string legacyChannelsXml)
		{
			string key = "AppointmentBookingAvailabilityGroupidsDurations_ActiveChannels2";
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IList<Channel> list = (IList<Channel>)clientCache[key];
			bool flag = list == null;
			if (flag)
			{
				bool flag2;
				IList<Channel> channelsFromXml = xml_APPOINTMENTBOOKING_availabilitygroupidsdurations.GetChannelsFromXml(out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel> appointmentBooking_Legacy_ActiveChannels = this.GetAppointmentBooking_Legacy_ActiveChannels(legacyChannelsXml);
					using (IEnumerator<Channel> enumerator = channelsFromXml.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Channel channel = enumerator.Current;
							AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel appointmentBookingAvailabilityGroupIdsDurations_Channel = appointmentBooking_Legacy_ActiveChannels.FirstOrDefault((AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel g) => g.Id.Equals(channel.Id, StringComparison.OrdinalIgnoreCase));
							bool flag4 = appointmentBookingAvailabilityGroupIdsDurations_Channel != null;
							if (flag4)
							{
								foreach (ChannelAvailability channelAvailability in channel.Availabilities)
								{
									channelAvailability.SlotSizeInMinutes = appointmentBookingAvailabilityGroupIdsDurations_Channel.DurationMinutes;
									channelAvailability.AppTypeIdToBookWith = appointmentBookingAvailabilityGroupIdsDurations_Channel.AppTypeId;
								}
							}
						}
					}
				}
				list = (from g in channelsFromXml
				where g.IsActive
				select g).ToList<Channel>();
				clientCache.Insert(key, list, TimeSpan.FromMinutes(60.0));
			}
			return list;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000829C File Offset: 0x0000649C
		private IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel> GetAppointmentBooking_Legacy_ActiveChannels(string legacyChannelsXml)
		{
			string key = "AppointmentBookingAvailabilityGroupIdsDurations_ActiveChannels";
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel> list = (IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel>)clientCache[key];
			bool flag = list == null;
			if (flag)
			{
				list = AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel.GetActiveChannels(legacyChannelsXml);
				clientCache.Insert(key, list, TimeSpan.FromMinutes(60.0));
			}
			return list;
		}

		// Token: 0x02000035 RID: 53
		private class AppointmentBookingAvailabilityGroupIdsDurations_Channel
		{
			// Token: 0x06000141 RID: 321 RVA: 0x0000A500 File Offset: 0x00008700
			public static IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel> GetActiveChannels(string xml)
			{
				bool flag = string.IsNullOrEmpty(xml);
				IList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel> result;
				if (flag)
				{
					result = new List<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
				}
				else
				{
					try
					{
						XDocument xdocument = XDocument.Parse(xml);
						return (from g in xdocument.Descendants("channel")
						let xTitle = g.Element("title")
						let xId = g.Element("id")
						let xDescription = g.Element("description")
						let xAppTypeId = g.Element("apptypeid")
						let xDurationMinutes = g.Element("duration")
						let xBookingFormScreenNum = g.Element("bookingformscreennum")
						let xIsActive = g.Element("isactive")
						select new AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel
						{
							Title = ((xTitle == null) ? "" : (xTitle.Value ?? "")),
							Id = ((xId == null) ? "" : (xId.Value ?? "")),
							Description = ((xDescription == null) ? "" : (xDescription.Value ?? "")),
							AppTypeId = ((xAppTypeId == null) ? 0 : xAppTypeId.GetIntFromAttribute(0)),
							DurationMinutes = ((xDurationMinutes == null) ? 0 : xDurationMinutes.GetIntFromAttribute(0)),
							BookingFormScreenNum = ((xBookingFormScreenNum == null) ? 0 : xBookingFormScreenNum.GetIntFromAttribute(0)),
							IsActive = ("1trueyes".IndexOf(((xId == null) ? "" : (xId.Value ?? "")).ToLower().Trim()) >= 0)
						}).ToList<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("Common.UI.Web.Entity.AppointmentBooking.AppointmentBookingAvailabilityGroupIdsDurations_Channel:ErrorParsingXml:xml={0}", xml ?? "NULL");
					}
					result = new List<AppointmentBookingStudentWebClientManager.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
				}
				return result;
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000142 RID: 322 RVA: 0x0000A6AC File Offset: 0x000088AC
			// (set) Token: 0x06000143 RID: 323 RVA: 0x0000A6B4 File Offset: 0x000088B4
			public string Title { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000144 RID: 324 RVA: 0x0000A6BD File Offset: 0x000088BD
			// (set) Token: 0x06000145 RID: 325 RVA: 0x0000A6C5 File Offset: 0x000088C5
			public string Id { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000146 RID: 326 RVA: 0x0000A6CE File Offset: 0x000088CE
			// (set) Token: 0x06000147 RID: 327 RVA: 0x0000A6D6 File Offset: 0x000088D6
			public string Description { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000148 RID: 328 RVA: 0x0000A6DF File Offset: 0x000088DF
			// (set) Token: 0x06000149 RID: 329 RVA: 0x0000A6E7 File Offset: 0x000088E7
			public int AppTypeId { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600014A RID: 330 RVA: 0x0000A6F0 File Offset: 0x000088F0
			// (set) Token: 0x0600014B RID: 331 RVA: 0x0000A6F8 File Offset: 0x000088F8
			public int DurationMinutes { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600014C RID: 332 RVA: 0x0000A701 File Offset: 0x00008901
			// (set) Token: 0x0600014D RID: 333 RVA: 0x0000A709 File Offset: 0x00008909
			public int BookingFormScreenNum { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600014E RID: 334 RVA: 0x0000A712 File Offset: 0x00008912
			// (set) Token: 0x0600014F RID: 335 RVA: 0x0000A71A File Offset: 0x0000891A
			public bool IsActive { get; set; }
		}
	}
}
