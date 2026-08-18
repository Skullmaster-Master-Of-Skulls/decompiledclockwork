using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor;
using TechnoPro.Common.ClientManager.Core.AvailabilitySchedule;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.TutoringTutors
{
	// Token: 0x02000038 RID: 56
	public class user_TutoringTutors_availability : Page
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000A558 File Offset: 0x00008758
		protected void Page_Load(object sender, EventArgs e)
		{
			ScriptManager.RegisterStartupScript(this.updatePanel1, this.updatePanel1.GetType(), "fixeverything", "fixEverything();", true);
			int tutorPersonId = user_TutoringTutors_availability.LookupStudentPid();
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				tutoringClientWebClientManager.EnforceTutoringRedirects(tutorPersonId, this.Page, eClockWorkWebPage.TutoringTutors_Calendar);
				bool flag2 = base.Master != null && base.Master is IClockWorkMasterPage;
				if (flag2)
				{
					((IClockWorkMasterPage)base.Master).SetCurrentPage(eClockWorkWebPage.TutoringTutors_Availability);
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private static int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(null);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A608 File Offset: 0x00008808
		[WebMethod]
		public static AddAvailabilityResult AddAvailability(string startTime, int durationInMinutes, string[] dates)
		{
			IList<AvailabilityDuration> availabilityDurations = user_TutoringTutors_availability.GetAvailabilityDurations();
			bool flag = durationInMinutes < 1 || availabilityDurations.All((AvailabilityDuration g) => g.DurationInMinutes != durationInMinutes);
			AddAvailabilityResult result;
			if (flag)
			{
				result = new AddAvailabilityResult
				{
					PublicMessage = "Invalid duration",
					Result = new AddAvailabilitiesActionResultDTO
					{
						AbortedEntireProcess = true
					}
				};
			}
			else
			{
				string text = new string((from c in (startTime ?? "").ToLower().Trim()
				where char.IsDigit(c) || c == ':' || c == ' ' || c == 'a' || c == 'm' || c == 'p'
				select c).ToArray<char>()).Trim();
				string s = DateTime.Now.ToString("yyyy-MM-dd") + " " + (text ?? "");
				DateTime dateTime;
				bool flag2 = !DateTime.TryParse(s, out dateTime);
				if (flag2)
				{
					result = new AddAvailabilityResult
					{
						PublicMessage = "Invalid start time",
						Result = new AddAvailabilitiesActionResultDTO
						{
							AbortedEntireProcess = true
						}
					};
				}
				else
				{
					bool flag3 = dates == null || dates.Length < 1;
					if (flag3)
					{
						result = new AddAvailabilityResult
						{
							PublicMessage = "Invalid empty date list",
							Result = new AddAvailabilitiesActionResultDTO
							{
								AbortedEntireProcess = true
							}
						};
					}
					else
					{
						List<DateTime> list = new List<DateTime>();
						DateTime now = DateTime.Now;
						foreach (string text2 in dates)
						{
							bool flag4 = string.IsNullOrEmpty(text2);
							if (!flag4)
							{
								DateTime dateTime2;
								bool flag5 = !DateTime.TryParse(text2, out dateTime2);
								if (flag5)
								{
									return new AddAvailabilityResult
									{
										PublicMessage = "Invalid date - " + text2,
										Result = new AddAvailabilitiesActionResultDTO
										{
											AbortedEntireProcess = true
										}
									};
								}
								DateTime dateTime3 = dateTime2.Date.Add(dateTime.TimeOfDay);
								bool flag6 = dateTime3 <= now;
								if (flag6)
								{
									return new AddAvailabilityResult
									{
										PublicMessage = "Invalid date (date is in the past) - " + dateTime3.ToString("yyyy-MM-dd h:mm tt"),
										Result = new AddAvailabilitiesActionResultDTO
										{
											AbortedEntireProcess = true
										}
									};
								}
								list.Add(dateTime3);
							}
						}
						bool flag7 = list.Count < 1;
						if (flag7)
						{
							result = new AddAvailabilityResult
							{
								PublicMessage = "Invalid empty date list.",
								Result = new AddAvailabilitiesActionResultDTO
								{
									AbortedEntireProcess = true
								}
							};
						}
						else
						{
							DateTime dateTime4 = list[0].AddMinutes((double)durationInMinutes);
							bool flag8 = dateTime4.Date != list[0].Date;
							if (flag8)
							{
								result = new AddAvailabilityResult
								{
									PublicMessage = "Invalid start time and duration (availabilty cannot go past midnight) - " + list[0].ToString("yyyy-MM-dd h:mm tt") + " to " + dateTime4.ToString("yyyy-MM-dd h:mm tt"),
									Result = new AddAvailabilitiesActionResultDTO
									{
										AbortedEntireProcess = true
									}
								};
							}
							else
							{
								ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
								int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
								int personId = user_TutoringTutors_availability.LookupStudentPid();
								AvailabilityScheduleContextDTO context = new AvailabilityScheduleContextDTO
								{
									PersonId = personId,
									AvailabilityGroupId = tutorAvailabilityScheduleGroupId
								};
								List<AvailabilityScheduleTimeDTO> times = new List<AvailabilityScheduleTimeDTO>
								{
									new AvailabilityScheduleTimeDTO
									{
										StartTime = dateTime.TimeOfDay,
										EndTime = dateTime.TimeOfDay.Add(TimeSpan.FromMinutes((double)durationInMinutes))
									}
								};
								IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
								AddAvailabilitiesActionResultDTO result2 = availabilityScheduleClientManager.AddAvailabilityDatesAndTimesByContext(context, (from g in list
								select g.Date).ToList<DateTime>(), times, true);
								result = new AddAvailabilityResult
								{
									Result = result2,
									PublicMessage = ""
								};
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AA04 File Offset: 0x00008C04
		[WebMethod]
		public static IList<AvailabilityDuration> GetAvailabilityDurations()
		{
			string text = new WebSettingsClientManager().GetSettingValue<string>(Setting.TUTORING_Availability_DurationsAvailable) ?? "";
			string[] array = text.Split(new char[]
			{
				','
			});
			List<AvailabilityDuration> list = new List<AvailabilityDuration>();
			foreach (string text2 in array)
			{
				int num;
				bool flag = int.TryParse(text2.Trim(), out num) && num > 0;
				if (flag)
				{
					string durationDescription = num.GetDurationDescription();
					list.Add(new AvailabilityDuration
					{
						DurationInMinutes = num,
						DurationDescription = durationDescription
					});
				}
			}
			return list;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		[WebMethod]
		public static IList<DateTime> GetSpecialDates(int year)
		{
			int personId = user_TutoringTutors_availability.LookupStudentPid();
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
			return availabilityScheduleClientManager.LoadDaysWithAvailability(personId, new int[]
			{
				tutorAvailabilityScheduleGroupId
			}, new DateTime(year, 1, 1), new DateTime(year, 12, 31));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AB00 File Offset: 0x00008D00
		[WebMethod]
		public static IList<DeleteAvailabilityActionResultDTO> DeleteAvailabilities(AvailabilityScheduleDateAndTimeWrapper[] dayAndTimes)
		{
			bool flag = dayAndTimes == null || dayAndTimes.Length < 1;
			IList<DeleteAvailabilityActionResultDTO> result;
			if (flag)
			{
				result = new List<DeleteAvailabilityActionResultDTO>
				{
					user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.InvalidParameters, "")
				};
			}
			else
			{
				int num = user_TutoringTutors_availability.LookupStudentPid();
				bool flag2 = num < 1;
				if (flag2)
				{
					result = new List<DeleteAvailabilityActionResultDTO>
					{
						user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.AuthenticationProblem, "")
					};
				}
				else
				{
					List<AvailabilityScheduleDateAndTimeDTO> list = new List<AvailabilityScheduleDateAndTimeDTO>();
					foreach (AvailabilityScheduleDateAndTimeWrapper availabilityScheduleDateAndTimeWrapper in dayAndTimes)
					{
						DateTime date;
						bool flag3 = !DateTime.TryParse(availabilityScheduleDateAndTimeWrapper.Date, out date);
						if (flag3)
						{
							return new List<DeleteAvailabilityActionResultDTO>
							{
								user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.InvalidParameters, "")
							};
						}
						list.Add(new AvailabilityScheduleDateAndTimeDTO
						{
							Date = date,
							Time = new AvailabilityScheduleTimeDTO
							{
								StartTime = TimeSpan.FromMinutes((double)availabilityScheduleDateAndTimeWrapper.StartMinutes),
								EndTime = TimeSpan.FromMinutes((double)availabilityScheduleDateAndTimeWrapper.EndMinutes)
							}
						});
					}
					bool flag4 = list.Count < 1;
					if (flag4)
					{
						result = new List<DeleteAvailabilityActionResultDTO>
						{
							user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.InvalidParameters, "")
						};
					}
					else
					{
						ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
						int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
						AvailabilityScheduleContextDTO context = new AvailabilityScheduleContextDTO
						{
							PersonId = num,
							AvailabilityGroupId = tutorAvailabilityScheduleGroupId
						};
						IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
						result = availabilityScheduleClientManager.DeleteAvailabilityDatesAndTimesByContext(context, list);
					}
				}
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AC7C File Offset: 0x00008E7C
		private static DeleteAvailabilityActionResultDTO GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason failureReason, string publicMessage)
		{
			return new DeleteAvailabilityActionResultDTO
			{
				Status = new AvailabilityScheduleItemActionResultDTO
				{
					PublicMessage = publicMessage,
					FailureReason = failureReason
				}
			};
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		[WebMethod]
		public static DeleteAvailabilityActionResultDTO DeleteSingleAvailability(string dt, int startMinutes, int endMinutes)
		{
			DateTime minValue;
			bool flag = !DateTime.TryParse(dt, out minValue);
			if (flag)
			{
				minValue = DateTime.MinValue;
			}
			bool flag2 = minValue == DateTime.MinValue || startMinutes < 0 || endMinutes < 0 || endMinutes <= startMinutes;
			DeleteAvailabilityActionResultDTO result;
			if (flag2)
			{
				result = user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.InvalidParameters, "");
			}
			else
			{
				int num = user_TutoringTutors_availability.LookupStudentPid();
				bool flag3 = num < 1;
				if (flag3)
				{
					result = user_TutoringTutors_availability.GetStandardDeleteAvailabilityFailure(eAvailabilityScheduleActionFailureReason.AuthenticationProblem, "");
				}
				else
				{
					ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
					int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
					AvailabilityScheduleContextDTO context = new AvailabilityScheduleContextDTO
					{
						PersonId = num,
						AvailabilityGroupId = tutorAvailabilityScheduleGroupId
					};
					IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
					result = availabilityScheduleClientManager.DeleteAvailabilityTimeByContext(context, new AvailabilityScheduleDateAndTimeDTO
					{
						Date = minValue,
						Time = new AvailabilityScheduleTimeDTO
						{
							StartTime = TimeSpan.FromMinutes((double)startMinutes),
							EndTime = TimeSpan.FromMinutes((double)endMinutes)
						}
					});
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AD9C File Offset: 0x00008F9C
		[WebMethod]
		public static IList<AvailabilityScheduleDateAndTimesWrapper> LoadAvailabilities(string sd, string ed)
		{
			DateTime sd2;
			DateTime ed2;
			bool flag = !DateTime.TryParse(sd, out sd2) || !DateTime.TryParse(ed, out ed2);
			IList<AvailabilityScheduleDateAndTimesWrapper> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AvailabilityScheduleItemsForContextDTO availabilityScheduleItemsForContextDTO = user_TutoringTutors_availability.LoadAvailability(sd2, ed2);
				bool flag2 = availabilityScheduleItemsForContextDTO == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<AvailabilityScheduleDateAndTimesWrapper> list = new List<AvailabilityScheduleDateAndTimesWrapper>();
					List<AvailabilityScheduleItemInfoDTO> list2 = availabilityScheduleItemsForContextDTO.AvailabilityScheduleItems.ToList<AvailabilityScheduleItemInfoDTO>();
					list2.Sort((AvailabilityScheduleItemInfoDTO g1, AvailabilityScheduleItemInfoDTO g2) => g1.DayAndTime.Date.Date.CompareTo(g2.DayAndTime.Date.Date));
					int j;
					for (int i = 0; i < list2.Count; i = j)
					{
						AvailabilityScheduleItemInfoDTO item0 = list2[i];
						DateTime date = item0.DayAndTime.Date.Date;
						j = i;
						List<AvailabilityScheduleTimeDTO> list3 = new List<AvailabilityScheduleTimeDTO>();
						while (j < list2.Count)
						{
							AvailabilityScheduleItemInfoDTO availabilityScheduleItemInfoDTO = list2[j];
							DateTime date2 = availabilityScheduleItemInfoDTO.DayAndTime.Date.Date;
							bool flag3 = date2 != date;
							if (flag3)
							{
								break;
							}
							list3.Add(availabilityScheduleItemInfoDTO.DayAndTime.Time);
							j++;
						}
						list.Add(new AvailabilityScheduleDateAndTimesWrapper
						{
							Date = date,
							Times = (from g in list3
							select new AvailabilityScheduleTimeWrapper(item0.DayAndTime.Date, g.StartTime, g.EndTime)).ToList<AvailabilityScheduleTimeWrapper>()
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AF24 File Offset: 0x00009124
		private static AvailabilityScheduleItemsForContextDTO LoadAvailability(DateTime sd, DateTime ed)
		{
			int personId = user_TutoringTutors_availability.LookupStudentPid();
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			IAvailabilityScheduleClientManager availabilityScheduleClientManager = new AvailabilityScheduleClientManager();
			return availabilityScheduleClientManager.LoadAvailabilityItemsByContextAndDateRange(new AvailabilityScheduleContextDTO
			{
				PersonId = personId,
				AvailabilityGroupId = tutorAvailabilityScheduleGroupId
			}, sd, Convert.ToInt32((ed.Date - sd.Date).TotalDays) + 1);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void ctrlSelectedAvailabilityTimeView1_OnDateRemovedFromList(object sender, DateEventArgs e)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000AF94 File Offset: 0x00009194
		protected void ctrlSelectedAvailabilityTimeView1_OnClearSelectionRequested(object sender, EventArgs e)
		{
			this.ClearSelection();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003E0A File Offset: 0x0000200A
		private void ClearSelection()
		{
		}

		// Token: 0x0400011C RID: 284
		protected UpdatePanel updatePanel1;
	}
}
