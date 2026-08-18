using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar;
using TechnoPro.Common.UI.Web.Entity.appt;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar
{
	// Token: 0x02000023 RID: 35
	public class AppointmentClientCalendarWebClientManager : IAppointmentClientCalendarWebClientManager
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000082F4 File Offset: 0x000064F4
		private IList<AppointmentDTO> FixAppsForMultiplePersonIdsBehindOneUser(IList<AttendeeView> users, IList<AppointmentDTO> apps)
		{
			bool flag = apps == null;
			IList<AppointmentDTO> result;
			if (flag)
			{
				result = new List<AppointmentDTO>();
			}
			else
			{
				foreach (AppointmentDTO appointmentDTO in apps)
				{
					bool flag2 = appointmentDTO.Attendees != null;
					if (flag2)
					{
						using (IEnumerator<AttendeeView> enumerator2 = users.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AttendeeView user = enumerator2.Current;
								bool flag3 = user.PersonIds != null && user.PersonIds.Count > 0;
								if (flag3)
								{
									List<AttendeeDTO> list = (from g in appointmentDTO.Attendees
									where user.PersonIds.Contains(g.Person.PersonId)
									select g).ToList<AttendeeDTO>();
									foreach (AttendeeDTO attendeeDTO in list)
									{
										attendeeDTO.Person.PersonId = user.PersonId;
										attendeeDTO.Person.FirstName = user.Name;
										attendeeDTO.Person.LastName = "";
										attendeeDTO.Person.MiddleName = "";
										attendeeDTO.Person.Student_no = "";
									}
								}
							}
						}
					}
				}
				result = apps;
			}
			return result;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000084D0 File Offset: 0x000066D0
		private IDictionary<int, IList<AvailabilityScheduleItemDTO>> FixAvailabilityForMultiplePersonIdsBehindOneuser(IList<AttendeeView> users, IDictionary<int, IList<AvailabilityScheduleItemDTO>> availabilityItems)
		{
			bool flag = availabilityItems == null;
			IDictionary<int, IList<AvailabilityScheduleItemDTO>> result;
			if (flag)
			{
				result = new Dictionary<int, IList<AvailabilityScheduleItemDTO>>();
			}
			else
			{
				foreach (int key in availabilityItems.Keys)
				{
					using (IEnumerator<AvailabilityScheduleItemDTO> enumerator2 = availabilityItems[key].GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							AvailabilityScheduleItemDTO av = enumerator2.Current;
							AttendeeView attendeeView = users.FirstOrDefault((AttendeeView g) => av.Context != null && g.PersonIds.Contains(av.Context.PersonId));
							bool flag2 = attendeeView != null;
							if (flag2)
							{
								av.Context.PersonId = attendeeView.PersonId;
							}
						}
					}
				}
				result = availabilityItems;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000085C0 File Offset: 0x000067C0
		private void FixAvailabilityForMultiplePersonIdsBehindOneuser(ref IDictionary<int, IList<AvailabilityScheduleItemDTO>> availability)
		{
			foreach (KeyValuePair<int, IList<AvailabilityScheduleItemDTO>> keyValuePair in availability)
			{
				foreach (AvailabilityScheduleItemDTO item in keyValuePair.Value)
				{
					this.FixAvailabilityForMultiplePersonIdsBehindOneuser(item);
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00008648 File Offset: 0x00006848
		private void FixAvailabilityForMultiplePersonIdsBehindOneuser(AvailabilityScheduleItemDTO item)
		{
			TimeSpan timeOfDay = item.StartDateTime.TimeOfDay;
			TimeSpan timeOfDay2 = item.EndDateTime.TimeOfDay;
			TimeSpan timeSpan = this.RoundMinutes(timeOfDay);
			TimeSpan timeSpan2 = this.RoundMinutes(timeOfDay2);
			bool flag = timeSpan != timeOfDay;
			if (flag)
			{
				item.StartDateTime = item.StartDateTime.Date.Add(timeSpan);
			}
			bool flag2 = timeSpan2 != timeOfDay2;
			if (flag2)
			{
				item.EndDateTime = item.EndDateTime.Date.Add(timeSpan2);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000086E0 File Offset: 0x000068E0
		private TimeSpan RoundMinutes(TimeSpan ts)
		{
			int minutes = ts.Minutes;
			bool flag = this.IsInRange(minutes, 1, 7);
			TimeSpan result;
			if (flag)
			{
				result = new TimeSpan(ts.Hours, 0, 0);
			}
			else
			{
				bool flag2 = this.IsInRange(minutes, 8, 14);
				if (flag2)
				{
					result = new TimeSpan(ts.Hours, 15, 0);
				}
				else
				{
					bool flag3 = this.IsInRange(minutes, 16, 22);
					if (flag3)
					{
						result = new TimeSpan(ts.Hours, 15, 0);
					}
					else
					{
						bool flag4 = this.IsInRange(minutes, 23, 29);
						if (flag4)
						{
							result = new TimeSpan(ts.Hours, 30, 0);
						}
						else
						{
							bool flag5 = this.IsInRange(minutes, 31, 37);
							if (flag5)
							{
								result = new TimeSpan(ts.Hours, 30, 0);
							}
							else
							{
								bool flag6 = this.IsInRange(minutes, 38, 44);
								if (flag6)
								{
									result = new TimeSpan(ts.Hours, 45, 0);
								}
								else
								{
									bool flag7 = this.IsInRange(minutes, 46, 52);
									if (flag7)
									{
										result = new TimeSpan(ts.Hours, 45, 0);
									}
									else
									{
										bool flag8 = this.IsInRange(minutes, 53, 59);
										if (flag8)
										{
											result = new TimeSpan(ts.Hours + 1, 0, 0);
										}
										else
										{
											result = ts;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008818 File Offset: 0x00006A18
		private bool IsInRange(int minute, int min, int max)
		{
			return minute >= min && minute <= max;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008838 File Offset: 0x00006A38
		public IList<AppointmentView> LoadAvailabilityForAppointmentBookingModule(int studentPid, IList<AttendeeView> users, Channel activeChannel, DateTime StartDate, DateTime EndDate)
		{
			bool flag = users == null;
			if (flag)
			{
				users = new List<AttendeeView>();
			}
			List<AppointmentView> list = new List<AppointmentView>();
			int num = 1;
			List<int> list2 = new List<int>();
			Dictionary<int, IList<int>> dictionary = new Dictionary<int, IList<int>>();
			foreach (AttendeeView attendeeView in users)
			{
				using (IEnumerator<int> enumerator2 = attendeeView.PersonIds.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int pid = enumerator2.Current;
						bool flag2 = !list2.Contains(pid);
						if (flag2)
						{
							list2.Add(pid);
							List<int> list3 = new List<int>();
							Func<ChannelUnderlyingPerson, bool> <>9__1;
							Func<ChannelPersonCollection, bool> <>9__0;
							foreach (ChannelAvailability channelAvailability in activeChannel.Availabilities)
							{
								IEnumerable<ChannelPersonCollection> personCollection = channelAvailability.PersonCollection;
								Func<ChannelPersonCollection, bool> predicate;
								if ((predicate = <>9__0) == null)
								{
									predicate = (<>9__0 = delegate(ChannelPersonCollection g)
									{
										IEnumerable<ChannelUnderlyingPerson> underlyingPeople = g.UnderlyingPeople;
										Func<ChannelUnderlyingPerson, bool> predicate3;
										if ((predicate3 = <>9__1) == null)
										{
											predicate3 = (<>9__1 = ((ChannelUnderlyingPerson h) => h.PersonId == pid));
										}
										return underlyingPeople.FirstOrDefault(predicate3) != null;
									});
								}
								List<ChannelPersonCollection> list4 = personCollection.Where(predicate).ToList<ChannelPersonCollection>();
								bool flag3 = list4.Count > 0;
								if (flag3)
								{
									bool flag4 = !list3.Contains(channelAvailability.AvailabilityGroupId);
									if (flag4)
									{
										list3.Add(channelAvailability.AvailabilityGroupId);
									}
								}
							}
							dictionary.Add(pid, list3);
						}
					}
				}
			}
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			AppointmentsWithAvailabilityAndTimetableDTO appointmentsWithAvailabilityAndTimetableDTO = appointmentClientManager.LoadAppointmentsAndAvailability(new AppointmentLoadOptionsDTO
			{
				StartDateTime = StartDate,
				EndDateTime = EndDate,
				HideCancelledAppointments = true,
				PersonIds = list2,
				LoadRecurringSchedule = true,
				DontLoadHolidays = true,
				AvailabilityGroupIdsByPersonId = dictionary
			});
			IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
			IList<AvailabilityGroupDTO> source = availabilityScheduleClientManager.LoadAllAvailabilityGroups();
			IDictionary<int, IList<AvailabilityScheduleItemDTO>> dictionary2 = new Dictionary<int, IList<AvailabilityScheduleItemDTO>>();
			int num2 = 0;
			foreach (AvailabilityScheduleItemsForContextDTO availabilityScheduleItemsForContextDTO in appointmentsWithAvailabilityAndTimetableDTO.AvailabilitySchedules)
			{
				int personId = availabilityScheduleItemsForContextDTO.Context.PersonId;
				int avGid = availabilityScheduleItemsForContextDTO.Context.AvailabilityGroupId;
				AvailabilityGroupDTO availabilityGroupDTO;
				if ((availabilityGroupDTO = source.FirstOrDefault((AvailabilityGroupDTO g) => g.AvailabilityGroupId == avGid)) == null)
				{
					AvailabilityGroupDTO availabilityGroupDTO2 = new AvailabilityGroupDTO();
					availabilityGroupDTO2.AvailabilityGroupId = avGid;
					availabilityGroupDTO2.Description = "_";
					availabilityGroupDTO = availabilityGroupDTO2;
					availabilityGroupDTO2.Title = ".";
				}
				AvailabilityGroupDTO availabilityGroup = availabilityGroupDTO;
				bool flag5 = !dictionary2.ContainsKey(personId);
				if (flag5)
				{
					dictionary2.Add(personId, new List<AvailabilityScheduleItemDTO>());
				}
				IList<AvailabilityScheduleItemDTO> list5 = dictionary2[personId];
				foreach (AvailabilityScheduleItemInfoDTO availabilityScheduleItemInfoDTO in availabilityScheduleItemsForContextDTO.AvailabilityScheduleItems)
				{
					list5.Add(new AvailabilityScheduleItemDTO
					{
						AvailabilityGroup = availabilityGroup,
						AvailabilityScheduleId = num2++,
						Context = availabilityScheduleItemsForContextDTO.Context,
						StartDateTime = availabilityScheduleItemInfoDTO.DayAndTime.Date.Date.Add(availabilityScheduleItemInfoDTO.DayAndTime.Time.StartTime),
						EndDateTime = availabilityScheduleItemInfoDTO.DayAndTime.Date.Date.Add(availabilityScheduleItemInfoDTO.DayAndTime.Time.EndTime)
					});
				}
			}
			foreach (HolidayDTO holidayDTO in appointmentsWithAvailabilityAndTimetableDTO.Holidays)
			{
				AvailabilityScheduleItemDTO availabilityScheduleItemDTO = new AvailabilityScheduleItemDTO
				{
					AvailabilityGroup = new AvailabilityGroupDTO
					{
						Title = holidayDTO.Title,
						Description = holidayDTO.Description
					},
					StartDateTime = holidayDTO.Date,
					EndDateTime = holidayDTO.Date.AddDays(1.0).AddMinutes(-1.0)
				};
				foreach (KeyValuePair<int, IList<AvailabilityScheduleItemDTO>> keyValuePair in dictionary2)
				{
					availabilityScheduleItemDTO.Context = new AvailabilityScheduleContextDTO
					{
						PersonId = keyValuePair.Key
					};
					IList<AvailabilityScheduleItemDTO> value = keyValuePair.Value;
					value.Add(availabilityScheduleItemDTO);
				}
			}
			IList<AppointmentDTO> appointments = appointmentsWithAvailabilityAndTimetableDTO.Appointments;
			IList<AppointmentDTO> list6 = this.FixAppsForMultiplePersonIdsBehindOneUser(users, appointments);
			IDictionary<int, IList<AvailabilityScheduleItemDTO>> dictionary3 = this.FixAvailabilityForMultiplePersonIdsBehindOneuser(users, dictionary2);
			this.FixAvailabilityForMultiplePersonIdsBehindOneuser(ref dictionary3);
			foreach (int num3 in dictionary3.Keys)
			{
				int currUserPid = num3;
				IList<AvailabilityScheduleItemDTO> list7 = dictionary3[num3];
				using (IEnumerator<AvailabilityScheduleItemDTO> enumerator9 = list7.GetEnumerator())
				{
					Func<AttendeeDTO, bool> <>9__7;
					Func<AppointmentDTO, bool> <>9__4;
					while (enumerator9.MoveNext())
					{
						AvailabilityScheduleItemDTO availabilityItem = enumerator9.Current;
						bool flag6 = availabilityItem.Context != null;
						if (flag6)
						{
							Func<ChannelUnderlyingPerson, bool> <>9__6;
							Func<ChannelPersonCollection, bool> <>9__5;
							ChannelAvailability channelAvailability2 = activeChannel.Availabilities.FirstOrDefault(delegate(ChannelAvailability g)
							{
								bool result;
								if (g.AvailabilityGroupId == availabilityItem.Context.AvailabilityGroupId)
								{
									IEnumerable<ChannelPersonCollection> source4 = g.PersonCollection ?? new List<ChannelPersonCollection>();
									Func<ChannelPersonCollection, bool> predicate3;
									if ((predicate3 = <>9__5) == null)
									{
										predicate3 = (<>9__5 = delegate(ChannelPersonCollection m)
										{
											bool result2;
											if (m.IsActive && m.UnderlyingPeople != null)
											{
												IEnumerable<ChannelUnderlyingPerson> underlyingPeople = m.UnderlyingPeople;
												Func<ChannelUnderlyingPerson, bool> predicate4;
												if ((predicate4 = <>9__6) == null)
												{
													predicate4 = (<>9__6 = ((ChannelUnderlyingPerson n) => n.PersonId == availabilityItem.Context.PersonId));
												}
												result2 = underlyingPeople.Any(predicate4);
											}
											else
											{
												result2 = false;
											}
											return result2;
										});
									}
									result = source4.Any(predicate3);
								}
								else
								{
									result = false;
								}
								return result;
							});
							int num4 = (channelAvailability2 != null) ? channelAvailability2.SlotSizeInMinutes : 60;
							string title = (channelAvailability2 == null) ? (activeChannel.Title ?? "") : (channelAvailability2.Title ?? "");
							IEnumerable<AppointmentDTO> source2 = list6;
							Func<AppointmentDTO, bool> predicate2;
							if ((predicate2 = <>9__4) == null)
							{
								predicate2 = (<>9__4 = delegate(AppointmentDTO g)
								{
									bool result;
									if (g.Attendees != null)
									{
										IEnumerable<AttendeeDTO> attendees = g.Attendees;
										Func<AttendeeDTO, bool> predicate3;
										if ((predicate3 = <>9__7) == null)
										{
											predicate3 = (<>9__7 = ((AttendeeDTO h) => h.Person.PersonId == currUserPid));
										}
										result = (attendees.FirstOrDefault(predicate3) != null);
									}
									else
									{
										result = false;
									}
									return result;
								});
							}
							List<AppointmentDTO> source3 = source2.Where(predicate2).ToList<AppointmentDTO>();
							DateTime dateTime = availabilityItem.StartDateTime;
							while (dateTime < availabilityItem.EndDateTime)
							{
								Range<DateTime> proposedNewTime = new Range<DateTime>
								{
									Start = dateTime,
									End = dateTime.AddMinutes((double)num4)
								};
								bool flag7 = proposedNewTime.End > availabilityItem.EndDateTime;
								if (flag7)
								{
									break;
								}
								List<AppointmentDTO> list8 = (from g in source3
								where !(proposedNewTime.End <= g.StartDateTime) && !(proposedNewTime.Start >= g.EndDateTime)
								select g).ToList<AppointmentDTO>();
								bool flag8 = list8.Count > 0;
								if (flag8)
								{
									list8.Sort((AppointmentDTO g1, AppointmentDTO g2) => g1.StartDateTime.CompareTo(g2.StartDateTime));
									DateTime endDateTime = list8[list8.Count - 1].EndDateTime;
									dateTime = ((endDateTime == dateTime) ? dateTime.AddMinutes((double)num4) : endDateTime);
								}
								else
								{
									AppointmentView av2 = new AppointmentView(availabilityItem, users);
									av2.Title = title;
									av2.StartDateTime = proposedNewTime.Start;
									av2.EndDateTime = proposedNewTime.End;
									av2.ID = ((channelAvailability2 == null || string.IsNullOrEmpty(channelAvailability2.Title)) ? num++.ToString() : (num++.ToString() + ":" + channelAvailability2.Title));
									bool flag9 = list.FirstOrDefault((AppointmentView g) => g.PrimaryAttendeeID == av2.PrimaryAttendeeID && g.StartDateTime == av2.StartDateTime && g.EndDateTime == av2.EndDateTime) == null;
									if (flag9)
									{
										av2.AppointmentId = num++;
										list.Add(av2);
									}
									dateTime = proposedNewTime.End;
								}
							}
						}
					}
				}
			}
			bool flag10 = studentPid > 0;
			if (flag10)
			{
				Func<AttendeeDTO, bool> <>9__12;
				List<AppointmentDTO> list9 = list6.Where(delegate(AppointmentDTO g)
				{
					bool result;
					if (g.Attendees != null)
					{
						IEnumerable<AttendeeDTO> attendees = g.Attendees;
						Func<AttendeeDTO, bool> predicate3;
						if ((predicate3 = <>9__12) == null)
						{
							predicate3 = (<>9__12 = ((AttendeeDTO h) => h.Person.PersonId == studentPid));
						}
						result = (attendees.FirstOrDefault(predicate3) != null);
					}
					else
					{
						result = false;
					}
					return result;
				}).ToList<AppointmentDTO>();
				using (List<AppointmentDTO>.Enumerator enumerator10 = list9.GetEnumerator())
				{
					while (enumerator10.MoveNext())
					{
						AppointmentDTO wapp = enumerator10.Current;
						AppointmentView appointmentView = new AppointmentView(wapp);
						AttendeeView attendeeView2 = users.FirstOrDefault((AttendeeView h) => wapp.Attendees != null && wapp.Attendees.FirstOrDefault((AttendeeDTO q) => q.Person.PersonId == h.PersonId) != null);
						bool flag11 = attendeeView2 != null;
						if (flag11)
						{
							appointmentView.PrimaryAttendeeID = attendeeView2.PersonId.ToString();
						}
						appointmentView.Title = "Booked (you)";
						list.Add(appointmentView);
					}
				}
			}
			return list;
		}
	}
}
