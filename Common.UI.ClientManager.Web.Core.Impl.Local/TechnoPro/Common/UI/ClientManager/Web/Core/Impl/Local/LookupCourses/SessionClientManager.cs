using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.CourseRegistrations;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.ICore.CourseRegistrations;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses
{
	// Token: 0x0200001A RID: 26
	public class SessionClientManager : TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00005780 File Offset: 0x00003980
		public SessionView GetSession(DateTime date)
		{
			int year = date.Year;
			IAcademicTermClientManager academicTermClientManager = new AcademicTermClientManager();
			IList<AcademicTermDTO> list = academicTermClientManager.LoadAcademicTerms(false);
			AcademicTermView academicTermView = null;
			DateTime now = DateTime.Now;
			DateTime dateTime = DateTime.Now;
			for (int i = 0; i < list.Count; i++)
			{
				academicTermView = list[i].ToView();
				now = new DateTime(year, academicTermView.StartMonthDay.Month, academicTermView.StartMonthDay.Day);
				dateTime = new DateTime(year, academicTermView.EndMonthDay.Month, academicTermView.EndMonthDay.Day).Add(new TimeSpan(23, 59, 59));
				bool flag = date >= now && date < dateTime;
				if (flag)
				{
					break;
				}
				bool flag2 = date < now;
				if (flag2)
				{
					break;
				}
			}
			bool flag3 = academicTermView == null;
			SessionView result;
			if (flag3)
			{
				result = null;
			}
			else
			{
				result = new SessionView
				{
					AcademicTerm = academicTermView.Clone(),
					StartDate = now,
					EndDate = dateTime
				};
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000058B0 File Offset: 0x00003AB0
		public SessionView GetSession(string sessionId)
		{
			DateTime date;
			return (string.IsNullOrEmpty(sessionId) || !DateTime.TryParse(sessionId, out date)) ? null : this.GetSession(date);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000058E0 File Offset: 0x00003AE0
		public List<SessionView> GetSessions(TermChooserAvailableSessionMode sessionMode = TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses, UserInfoForCourses userInfo = null)
		{
			TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new SessionClientManager();
			SessionView currentSession = sessionClientManager.GetCurrentSession();
			List<SessionView> list = new List<SessionView>();
			IList<DateTime> list2 = null;
			switch (sessionMode)
			{
			case TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses:
			{
				bool flag = userInfo == null;
				if (!flag)
				{
					bool flag2 = userInfo.PersonId > 0;
					if (flag2)
					{
						ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
						list2 = courseRegistrationClientManager.GetUniqueCourseRegistrationStartDatesByStudent(userInfo.PersonId);
					}
					else
					{
						bool flag3 = userInfo.NotetakerId > 0;
						if (flag3)
						{
							INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
							list2 = notetakingClientManager.LoadUniqueAvailableCourseStartDatesByNotetaker(userInfo.NotetakerId);
						}
						else
						{
							bool flag4 = userInfo.InstructorId > 0 || userInfo.AlternateContactId > 0;
							if (flag4)
							{
								bool flag5 = userInfo.InstructorId > 0;
								if (flag5)
								{
									ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
									list2 = lookupInstructorClientManager.GetUniqueCourseRegistrationStartDatesByInstructor(userInfo.InstructorId);
								}
								bool flag6 = userInfo.AlternateContactId > 0;
								if (flag6)
								{
									IAlternateContactClientManager alternateContactClientManager = new AlternateContactClientManager();
									IList<DateTime> uniqueCourseRegistrationStartDatesByAlternateContact = alternateContactClientManager.GetUniqueCourseRegistrationStartDatesByAlternateContact(userInfo.AlternateContactId);
									bool flag7 = list2 == null || list2.Count < 1;
									if (flag7)
									{
										list2 = uniqueCourseRegistrationStartDatesByAlternateContact;
									}
									else
									{
										using (IEnumerator<DateTime> enumerator = uniqueCourseRegistrationStartDatesByAlternateContact.GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												DateTime d = enumerator.Current;
												bool flag8 = list2.Any((DateTime d0) => d0.Date == d.Date);
												bool flag9 = !flag8;
												if (flag9)
												{
													list2.Add(d);
												}
											}
										}
									}
								}
							}
						}
					}
					bool flag10 = list2 != null;
					if (flag10)
					{
						List<DateTime> list3 = list2.ToList<DateTime>();
						list3.Sort((DateTime g1, DateTime g2) => g1.CompareTo(g2));
						list2 = list3;
						bool flag11 = list2.Count < 1;
						if (flag11)
						{
							list.Add(currentSession);
						}
						else
						{
							bool flag12 = false;
							Predicate<SessionView> <>9__2;
							foreach (DateTime dateTime in list2)
							{
								SessionView session = sessionClientManager.GetSession(dateTime);
								bool flag13 = !flag12 && dateTime >= currentSession.EndDate;
								SessionView sessionView;
								if (flag13)
								{
									List<SessionView> list4 = list;
									Predicate<SessionView> match;
									if ((match = <>9__2) == null)
									{
										match = (<>9__2 = ((SessionView s) => !(s.EndDate <= currentSession.StartDate) && !(s.StartDate > currentSession.EndDate)));
									}
									sessionView = list4.Find(match);
									bool flag14 = sessionView == null;
									if (flag14)
									{
										list.Add(currentSession);
										flag12 = true;
									}
								}
								sessionView = list.Find((SessionView s) => !(s.EndDate <= session.StartDate) && !(s.StartDate > session.EndDate));
								bool flag15 = sessionView == null;
								if (flag15)
								{
									list.Add(session);
								}
							}
							bool flag16 = !flag12;
							if (flag16)
							{
								SessionView sessionView2 = list.Find((SessionView s) => !(s.EndDate <= currentSession.StartDate) && !(s.StartDate > currentSession.EndDate));
								bool flag17 = sessionView2 == null;
								if (flag17)
								{
									list.Add(currentSession);
								}
							}
						}
					}
					else
					{
						SessionView item = sessionClientManager.SubtractSession(1, currentSession);
						SessionView item2 = sessionClientManager.AddSession(1, currentSession);
						list.Add(item);
						list.Add(currentSession);
						list.Add(item2);
					}
				}
				break;
			}
			case TermChooserAvailableSessionMode.CurrentTermAndNextTerm:
			{
				SessionView item3 = sessionClientManager.AddSession(1, currentSession);
				list.Add(currentSession);
				list.Add(item3);
				break;
			}
			case TermChooserAvailableSessionMode.PreviousTermAndCurrentTermAndNextTerm:
			{
				SessionView item4 = sessionClientManager.SubtractSession(1, currentSession);
				SessionView item5 = sessionClientManager.AddSession(1, currentSession);
				list.Add(item4);
				list.Add(currentSession);
				list.Add(item5);
				break;
			}
			}
			return list;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public List<SessionView> GetSessions(int? maxSessionsInThePast, TermChooserAvailableSessionMode sessionMode = TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses, UserInfoForCourses userInfo = null)
		{
			SessionView currentSession = this.GetCurrentSession();
			List<SessionView> list = new List<SessionView>();
			IList<DateTime> list2 = null;
			switch (sessionMode)
			{
			case TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses:
			{
				bool flag = userInfo == null;
				if (!flag)
				{
					bool flag2 = userInfo.PersonId > 0;
					if (flag2)
					{
						ICourseRegistrationClientManager courseRegistrationClientManager = new CourseRegistrationClientManager();
						list2 = courseRegistrationClientManager.GetUniqueCourseRegistrationStartDatesByStudent(userInfo.PersonId);
					}
					else
					{
						bool flag3 = userInfo.NotetakerId > 0;
						if (flag3)
						{
							INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
							list2 = notetakingClientManager.LoadUniqueAvailableCourseStartDatesByNotetaker(userInfo.NotetakerId);
						}
						else
						{
							bool flag4 = userInfo.InstructorId > 0 || userInfo.AlternateContactId > 0;
							if (flag4)
							{
								bool flag5 = userInfo.InstructorId > 0;
								if (flag5)
								{
									ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
									list2 = lookupInstructorClientManager.GetUniqueCourseRegistrationStartDatesByInstructor(userInfo.InstructorId);
								}
								bool flag6 = userInfo.AlternateContactId > 0;
								if (flag6)
								{
									IAlternateContactClientManager alternateContactClientManager = new AlternateContactClientManager();
									IList<DateTime> uniqueCourseRegistrationStartDatesByAlternateContact = alternateContactClientManager.GetUniqueCourseRegistrationStartDatesByAlternateContact(userInfo.AlternateContactId);
									bool flag7 = list2 == null || list2.Count < 1;
									if (flag7)
									{
										list2 = uniqueCourseRegistrationStartDatesByAlternateContact;
									}
									else
									{
										using (IEnumerator<DateTime> enumerator = uniqueCourseRegistrationStartDatesByAlternateContact.GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												DateTime d = enumerator.Current;
												bool flag8 = list2.Any((DateTime d0) => d0.Date == d.Date);
												bool flag9 = !flag8;
												if (flag9)
												{
													list2.Add(d);
												}
											}
										}
									}
								}
							}
						}
					}
					bool flag10 = list2 != null;
					if (flag10)
					{
						List<DateTime> list3 = list2.ToList<DateTime>();
						list3.Sort((DateTime g1, DateTime g2) => g1.CompareTo(g2));
						list2 = list3;
						bool flag11 = list2.Count < 1;
						if (flag11)
						{
							list.Add(currentSession);
						}
						else
						{
							bool flag12 = false;
							Predicate<SessionView> <>9__2;
							foreach (DateTime dateTime in list2)
							{
								SessionView session = this.GetSession(dateTime);
								bool flag13 = !flag12 && dateTime >= currentSession.EndDate;
								SessionView sessionView;
								if (flag13)
								{
									List<SessionView> list4 = list;
									Predicate<SessionView> match;
									if ((match = <>9__2) == null)
									{
										match = (<>9__2 = ((SessionView s) => !(s.EndDate <= currentSession.StartDate) && !(s.StartDate > currentSession.EndDate)));
									}
									sessionView = list4.Find(match);
									bool flag14 = sessionView == null;
									if (flag14)
									{
										list.Add(currentSession);
										flag12 = true;
									}
								}
								sessionView = list.Find((SessionView s) => !(s.EndDate <= session.StartDate) && !(s.StartDate > session.EndDate));
								bool flag15 = sessionView == null;
								if (flag15)
								{
									list.Add(session);
								}
							}
							bool flag16 = !flag12;
							if (flag16)
							{
								SessionView sessionView2 = list.Find((SessionView s) => !(s.EndDate <= currentSession.StartDate) && !(s.StartDate > currentSession.EndDate));
								bool flag17 = sessionView2 == null;
								if (flag17)
								{
									list.Add(currentSession);
								}
							}
						}
					}
					else
					{
						SessionView item = this.SubtractSession(1, currentSession);
						SessionView item2 = this.AddSession(1, currentSession);
						list.Add(item);
						list.Add(currentSession);
						list.Add(item2);
					}
				}
				break;
			}
			case TermChooserAvailableSessionMode.CurrentTermAndNextTerm:
			{
				SessionView item3 = this.AddSession(1, currentSession);
				list.Add(currentSession);
				list.Add(item3);
				break;
			}
			case TermChooserAvailableSessionMode.PreviousTermAndCurrentTermAndNextTerm:
			{
				SessionView item4 = this.SubtractSession(1, currentSession);
				SessionView item5 = this.AddSession(1, currentSession);
				list.Add(item4);
				list.Add(currentSession);
				list.Add(item5);
				break;
			}
			}
			bool flag18 = maxSessionsInThePast != null;
			if (flag18)
			{
				DateTime date = DateTime.Now.Date;
				int num = -1;
				for (int i = list.Count - 1; i >= 0; i--)
				{
					SessionView sessionView3 = list[i];
					bool flag19 = sessionView3.EndDate < date;
					if (flag19)
					{
						num = i;
						break;
					}
				}
				bool flag20 = num > 0;
				if (flag20)
				{
					int num2 = num - maxSessionsInThePast.Value + 1;
					bool flag21 = num2 > 0;
					if (flag21)
					{
						list = list.GetRange(num2, list.Count - num2);
					}
				}
			}
			return list;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006174 File Offset: 0x00004374
		public SessionView SubtractSession(int count, SessionView Session)
		{
			SessionView sessionView = Session;
			for (int i = 0; i < count; i++)
			{
				DateTime date = sessionView.StartDate.AddDays(-5.0);
				sessionView = this.GetSession(date);
			}
			return sessionView;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000061C0 File Offset: 0x000043C0
		public SessionView AddSession(int count, SessionView Session)
		{
			SessionView sessionView = Session;
			for (int i = 0; i < count; i++)
			{
				DateTime date = sessionView.EndDate.AddDays(5.0);
				sessionView = this.GetSession(date);
			}
			return sessionView;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000620C File Offset: 0x0000440C
		public SessionView GetCurrentSession()
		{
			return this.GetSession(DateTime.Now.Date);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006234 File Offset: 0x00004434
		public List<SessionView> GetSessions()
		{
			return this.GetSessions(TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006250 File Offset: 0x00004450
		public List<SessionView> GetSessions(TermChooserAvailableSessionMode sessionMode)
		{
			return this.GetSessions(sessionMode, null);
		}
	}
}
