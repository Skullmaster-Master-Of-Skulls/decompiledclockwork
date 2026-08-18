using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000170 RID: 368
	public static class ExternalAppointmentAdapter
	{
		// Token: 0x0600103B RID: 4155 RVA: 0x000770F0 File Offset: 0x000752F0
		public static ClockWorkSyncAppointment ToClockWorkSyncAppointment(this ExternalAppointment outApp, SyncApplicationSettings syncApplicationSettings)
		{
			ClockWorkSyncAppointment clockWorkSyncAppointment = new ClockWorkSyncAppointment
			{
				AppointmentId = ((outApp.Mapping != null) ? outApp.Mapping.ClockWorkAppointmentId : 0),
				StartDateTime = outApp.StartDate,
				EndDateTime = outApp.EndDate,
				Memo = (string.IsNullOrEmpty(outApp.Memo) ? string.Empty : (outApp.Memo.SplitMemoTextAndAttendees()[0] ?? string.Empty)),
				Subtitle = outApp.Subject,
				IsCancelled = outApp.IsCancelled,
				IsPrivate = outApp.IsPrivate,
				Location = outApp.Location,
				Mapping = outApp.Mapping,
				IsAllDayEvent = outApp.IsAllDayEvent
			};
			List<string> list = new List<string>();
			using (IEnumerator<ExternalAttendee> enumerator = outApp.Attendees.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExternalAttendee attendee = enumerator.Current;
					ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser = syncApplicationSettings.SyncUsers.Find((ClockWorkExternalApplicationSyncUser su) => su.ExternalApplicationUsername.Equals(attendee.Username, StringComparison.OrdinalIgnoreCase));
					bool flag = clockWorkExternalApplicationSyncUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId > 0;
					if (flag)
					{
						clockWorkSyncAppointment.Attendees.Add(new ClockWorkSyncAttendee
						{
							Attendee = new ClockWorkSyncPersonBase
							{
								PersonId = clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId,
								FirstName = clockWorkExternalApplicationSyncUser.ClockWorkUser.FirstName,
								LastName = clockWorkExternalApplicationSyncUser.ClockWorkUser.LastName,
								Student_no = clockWorkExternalApplicationSyncUser.ClockWorkUser.Student_no
							}
						});
					}
					else
					{
						clockWorkExternalApplicationSyncUser = syncApplicationSettings.DisabledSyncUsers.Find((ClockWorkExternalApplicationSyncUser su) => su.ExternalApplicationUsername.Equals(attendee.Username, StringComparison.OrdinalIgnoreCase));
						bool flag2 = clockWorkExternalApplicationSyncUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId > 0;
						if (flag2)
						{
							clockWorkSyncAppointment.Attendees.Add(new ClockWorkSyncAttendee
							{
								Attendee = new ClockWorkSyncPersonBase
								{
									PersonId = clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId,
									FirstName = clockWorkExternalApplicationSyncUser.ClockWorkUser.FirstName,
									LastName = clockWorkExternalApplicationSyncUser.ClockWorkUser.LastName,
									Student_no = clockWorkExternalApplicationSyncUser.ClockWorkUser.Student_no
								}
							});
						}
						else
						{
							string item = string.IsNullOrEmpty(attendee.Name) ? (attendee.Username ?? string.Empty) : attendee.Name;
							list.Add(item);
						}
					}
				}
			}
			bool flag3 = clockWorkSyncAppointment.Attendees.Count < 1;
			if (flag3)
			{
				ClockWorkExternalApplicationSyncUser delegateSyncUser = syncApplicationSettings.GetDelegateSyncUser();
				bool flag4 = delegateSyncUser != null && delegateSyncUser.ClockWorkUser != null && delegateSyncUser.ClockWorkUser.PersonId > 0;
				if (flag4)
				{
					clockWorkSyncAppointment.Attendees.Add(new ClockWorkSyncAttendee
					{
						Attendee = new ClockWorkSyncPersonBase
						{
							PersonId = delegateSyncUser.ClockWorkUser.PersonId
						}
					});
				}
			}
			bool flag5 = list.Count > 0;
			if (flag5)
			{
				clockWorkSyncAppointment.Memo = ExternalAppointmentAdapter.AddCustomTextToMemo(clockWorkSyncAppointment.Memo, string.Join(", ", list.ToArray()));
			}
			return clockWorkSyncAppointment;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00077464 File Offset: 0x00075664
		public static ExternalAppointment ToExternalCalendarAppointment(this ClockWorkSyncAppointment clockworkApp, SyncApplicationSettings syncApplicationSettings)
		{
			string text = (clockworkApp.AppointmentType == null) ? "" : (clockworkApp.AppointmentType.Description ?? "");
			string text2 = clockworkApp.Subtitle ?? "";
			bool flag = string.IsNullOrEmpty(text2);
			string subject;
			if (flag)
			{
				subject = text;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					subject = text2;
				}
				else
				{
					bool flag3 = text2.StartsWith(text, StringComparison.OrdinalIgnoreCase);
					if (flag3)
					{
						subject = text2;
					}
					else
					{
						subject = string.Format("{0}: {1}", text, text2);
					}
				}
			}
			ExternalAppointment externalAppointment = new ExternalAppointment
			{
				UniqueId = ((clockworkApp.Mapping != null) ? clockworkApp.Mapping.ExternalApplicationUniqueAppointmentId : ""),
				StartDate = clockworkApp.StartDateTime,
				EndDate = clockworkApp.EndDateTime,
				IsCancelled = clockworkApp.IsCancelled,
				IsPrivate = clockworkApp.IsPrivate,
				Memo = clockworkApp.Memo,
				Subject = subject,
				Location = clockworkApp.Location,
				Mapping = clockworkApp.Mapping,
				IsAllDayEvent = clockworkApp.IsAllDayEvent,
				LegacyGlobalAppointmentId = ((clockworkApp.Mapping != null) ? clockworkApp.Mapping.ExternalApplicationGlobalAppointmentId : string.Empty)
			};
			List<string> list = new List<string>();
			using (List<ClockWorkSyncAttendee>.Enumerator enumerator = clockworkApp.Attendees.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClockWorkSyncAttendee attendee = enumerator.Current;
					ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser = syncApplicationSettings.SyncUsers.Find((ClockWorkExternalApplicationSyncUser su) => su.ClockWorkUser != null && su.ClockWorkUser.PersonId == attendee.Attendee.PersonId);
					bool flag4 = clockWorkExternalApplicationSyncUser != null && !string.IsNullOrEmpty(clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					if (flag4)
					{
						externalAppointment.Attendees.Add(new ExternalAttendee
						{
							Name = attendee.Attendee.GetName(),
							Username = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername
						});
					}
					else
					{
						clockWorkExternalApplicationSyncUser = syncApplicationSettings.DisabledSyncUsers.Find((ClockWorkExternalApplicationSyncUser su) => su.ClockWorkUser != null && su.ClockWorkUser.PersonId == attendee.Attendee.PersonId);
						bool flag5 = clockWorkExternalApplicationSyncUser != null && !string.IsNullOrEmpty(clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
						if (flag5)
						{
							externalAppointment.Attendees.Add(new ExternalAttendee
							{
								Name = attendee.Attendee.GetName(),
								Username = clockWorkExternalApplicationSyncUser.ExternalApplicationUsername
							});
						}
						else
						{
							list.Add(attendee.Attendee.GetName());
						}
					}
				}
			}
			bool flag6 = list.Count > 0 && syncApplicationSettings.ShowNonOutlookUsersInMemoWhenCreatingUpdatingOutlookAppointment;
			if (flag6)
			{
				externalAppointment.Memo = ExternalAppointmentAdapter.AddCustomTextToMemo(externalAppointment.Memo, string.Join(", ", list.ToArray()));
			}
			else
			{
				string[] array = externalAppointment.Memo.SplitMemoTextAndAttendees();
				externalAppointment.Memo = array[0];
			}
			bool flag7 = externalAppointment.Attendees != null && externalAppointment.Attendees.Count > 0;
			ExternalAppointment result;
			if (flag7)
			{
				externalAppointment.Organizer = (((List<ExternalAttendee>)externalAppointment.Attendees).Find((ExternalAttendee a) => a.AttendeeType == eAttendeeType.EVENT_ORGANIZER) ?? externalAppointment.Attendees[0]);
				result = externalAppointment;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000777D8 File Offset: 0x000759D8
		public static bool MergeWithAppointment(this ExternalAppointment app1, ExternalAppointment app2, SyncApplicationSettings syncSettings)
		{
			bool result = false;
			bool flag = app1.EndDate.Date != app2.EndDate.Date || app1.EndDate.Hour != app2.EndDate.Hour || app1.EndDate.Minute != app2.EndDate.Minute;
			if (flag)
			{
				result = true;
				app1.EndDate = app2.EndDate;
			}
			bool flag2 = app1.StartDate.Date != app2.StartDate.Date || app1.StartDate.Hour != app2.StartDate.Hour || app1.StartDate.Minute != app2.StartDate.Minute;
			if (flag2)
			{
				result = true;
				app1.StartDate = app2.StartDate;
			}
			bool flag3 = app1.IsAllDayEvent != app2.IsAllDayEvent;
			if (flag3)
			{
				result = true;
				app1.IsAllDayEvent = app2.IsAllDayEvent;
			}
			bool flag4 = app1.IsCancelled != app2.IsCancelled;
			if (flag4)
			{
				result = true;
				app1.IsCancelled = app2.IsCancelled;
			}
			bool flag5 = app1.IsPrivate != app2.IsPrivate;
			if (flag5)
			{
				result = true;
				app1.IsPrivate = app2.IsPrivate;
			}
			bool flag6 = app1.IsRecurring != app2.IsRecurring;
			if (flag6)
			{
				result = true;
				app1.IsRecurring = app2.IsRecurring;
			}
			bool flag7 = app1.Location != app2.Location;
			if (flag7)
			{
				result = true;
				app1.Location = app2.Location;
			}
			bool flag8 = app1.Memo != app2.Memo;
			if (flag8)
			{
				result = true;
				app1.Memo = app2.Memo;
			}
			bool flag9 = app1.Subject != app2.Subject;
			if (flag9)
			{
				result = true;
				app1.Subject = app2.Subject;
			}
			List<ExternalAttendee> att1 = (List<ExternalAttendee>)app1.Attendees;
			List<ExternalAttendee> att2 = (List<ExternalAttendee>)app2.Attendees;
			List<ExternalAttendee> list = att1.FindAll((ExternalAttendee a1) => att2.Find((ExternalAttendee a2) => a1.Username == a2.Username) == null && (syncSettings.SyncUsers.Find((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(a1.Username, StringComparison.OrdinalIgnoreCase)) != null || syncSettings.DisabledSyncUsers.Find((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(a1.Username, StringComparison.OrdinalIgnoreCase)) != null));
			List<ExternalAttendee> list2 = att2.FindAll((ExternalAttendee a2) => att1.Find((ExternalAttendee a1) => a1.Username == a2.Username) == null);
			bool flag10 = list.Count > 0;
			if (flag10)
			{
				result = true;
				foreach (ExternalAttendee item in list)
				{
					app1.Attendees.Remove(item);
				}
			}
			bool flag11 = list2.Count > 0;
			if (flag11)
			{
				result = true;
				foreach (ExternalAttendee item2 in list2)
				{
					app1.Attendees.Add(item2);
				}
			}
			return result;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00077B28 File Offset: 0x00075D28
		private static string AddCustomTextToMemo(string existingMemo, string newText)
		{
			int num = 0;
			string result;
			try
			{
				string text = "";
				int num2 = existingMemo.IndexOf("\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n");
				num = 1;
				bool flag = num2 >= 0;
				string text2;
				if (flag)
				{
					num = 2;
					text2 = existingMemo.Substring(0, num2);
					num = 3;
					int num3 = existingMemo.IndexOf("\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n", num2 + 1);
					num = 4;
					bool flag2 = num3 >= 0;
					if (flag2)
					{
						num = 5;
						text = existingMemo.Substring(num3 + "\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n".Length);
					}
					num = 6;
				}
				else
				{
					num = 7;
					text2 = existingMemo;
				}
				num = 8;
				bool flag3 = !string.IsNullOrEmpty(newText);
				if (flag3)
				{
					num = 9;
					result = string.Format("{0}{1}\n{2}{3}{4}", new object[]
					{
						text2,
						"\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n",
						newText,
						"\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n",
						text
					});
				}
				else
				{
					num = 10;
					result = text2;
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("AddCustomTextToMemo:LOC={0}:error={1}", num.ToString(), ex.ToString());
				result = existingMemo;
			}
			return result;
		}

		// Token: 0x040002E7 RID: 743
		internal const string CUSTOM_MEMO_HEADER = "\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n";

		// Token: 0x040002E8 RID: 744
		internal const string CUSTOM_MEMO_FOOTER = "\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n";
	}
}
