using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.DAO.AvailabilitySchedule;
using TechnoPro.Common.DAO.Impl.AvailabilitySchedule;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AvailabilitySchedule;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.AvailabilitySchedule
{
	// Token: 0x02000125 RID: 293
	public class AvailabilityScheduleManager : IAvailabilityScheduleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00055D5D File Offset: 0x00053F5D
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00055D65 File Offset: 0x00053F65
		public IAvailabilityScheduleDAO dao { get; set; }

		// Token: 0x06000C4B RID: 3147 RVA: 0x00055D6E File Offset: 0x00053F6E
		public AvailabilityScheduleManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AvailabilityScheduleDAO(opContext);
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00055D8D File Offset: 0x00053F8D
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00055D95 File Offset: 0x00053F95
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C4E RID: 3150 RVA: 0x00055DA0 File Offset: 0x00053FA0
		public IList<AvailabilityScheduleItemsForContext> LoadUnbookedAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContext> contexts, DateTime startDate, int numDays)
		{
			List<AvailabilityScheduleItemsForContext> list = (from g in contexts
			select this.LoadAvailabilityItemsByContextAndDateRange(g, startDate, numDays)).ToList<AvailabilityScheduleItemsForContext>();
			IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			IDictionary<int, IList<AppointmentBasicSlot>> dictionary = appointmentManager.LoadUncancelledBookedSlots((from g in contexts
			select g.PersonId).ToList<int>(), startDate, numDays);
			foreach (AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext in list)
			{
				int personId = availabilityScheduleItemsForContext.Context.PersonId;
				bool flag = !dictionary.ContainsKey(personId);
				if (!flag)
				{
					IList<AppointmentBasicSlot> source = dictionary[personId];
					List<AvailabilityScheduleItemInfo> list2 = new List<AvailabilityScheduleItemInfo>();
					foreach (AvailabilityScheduleItemInfo availabilityScheduleItemInfo in availabilityScheduleItemsForContext.AvailabilityScheduleItems)
					{
						DateTime dt = availabilityScheduleItemInfo.DayAndTime.Date.Date;
						DateTime sdt = dt.Add(availabilityScheduleItemInfo.DayAndTime.Time.StartTime);
						DateTime edt = dt.Add(availabilityScheduleItemInfo.DayAndTime.Time.EndTime);
						double totalMinutes = availabilityScheduleItemInfo.DayAndTime.Time.StartTime.TotalMinutes;
						double totalMinutes2 = availabilityScheduleItemInfo.DayAndTime.Time.EndTime.TotalMinutes;
						List<AppointmentBasicSlot> source2 = (from g in source
						where g.StartDateTime.Date == dt
						select g).ToList<AppointmentBasicSlot>();
						var list3 = (from potentialConflictSlot in source2
						where !(potentialConflictSlot.EndDateTime <= sdt) && !(potentialConflictSlot.StartDateTime >= edt)
						select potentialConflictSlot into g
						select new
						{
							StartNum = g.StartDateTime.TimeOfDay.TotalMinutes,
							EndNum = g.EndDateTime.TimeOfDay.TotalMinutes
						}).ToList();
						list3.Sort((g1, g2) => g1.StartNum.CompareTo(g2.StartNum));
						AvailabilityScheduleManager.TimeRange timeRange = new AvailabilityScheduleManager.TimeRange(availabilityScheduleItemInfo.DayAndTime.Time.StartTime, availabilityScheduleItemInfo.DayAndTime.Time.EndTime);
						foreach (var <>f__AnonymousType in list3)
						{
							timeRange.Subtract(<>f__AnonymousType.StartNum, <>f__AnonymousType.EndNum);
						}
						foreach (Pair<TimeSpan, TimeSpan> pair in timeRange.ConvertToTimeRangeList())
						{
							list2.Add(this.CloneNewScheduleTime(availabilityScheduleItemInfo, pair.Item1.TotalMinutes, pair.Item2.TotalMinutes));
						}
					}
					availabilityScheduleItemsForContext.AvailabilityScheduleItems = list2;
				}
			}
			return list;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00056138 File Offset: 0x00054338
		private AvailabilityScheduleItemInfo CloneNewScheduleTime(AvailabilityScheduleItemInfo master, double newStartMinutes, double newEndMinutes)
		{
			AvailabilityScheduleItemInfo availabilityScheduleItemInfo = master.Clone();
			availabilityScheduleItemInfo.DayAndTime.Time = new AvailabilityScheduleTime
			{
				StartTime = TimeSpan.FromMinutes(newStartMinutes),
				EndTime = TimeSpan.FromMinutes(newEndMinutes)
			};
			return availabilityScheduleItemInfo;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00056180 File Offset: 0x00054380
		public IList<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByMultipleContextsAndDateRange(IList<AvailabilityScheduleContext> contexts, DateTime startDate, int numDays)
		{
			return (from g in contexts
			select this.LoadAvailabilityItemsByContextAndDateRange(g, startDate, numDays)).ToList<AvailabilityScheduleItemsForContext>();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000561C4 File Offset: 0x000543C4
		public AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDateRange(AvailabilityScheduleContext context, DateTime startDate, int numDays)
		{
			return this.dao.LoadAvailabilityItemsByContextAndDateRange(context, startDate, numDays);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000561E4 File Offset: 0x000543E4
		[DebuggerStepThrough]
		public Task<AvailabilityScheduleItemsForContext> LoadAvailabilityItemsByContextAndDateRangeAsync(AvailabilityScheduleContext context, DateTime startDate, int numDays)
		{
			AvailabilityScheduleManager.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__14 <LoadAvailabilityItemsByContextAndDateRangeAsync>d__ = new AvailabilityScheduleManager.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__14();
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AvailabilityScheduleItemsForContext>.Create();
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>4__this = this;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.context = context;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.startDate = startDate;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.numDays = numDays;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>1__state = -1;
			<LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder.Start<AvailabilityScheduleManager.<LoadAvailabilityItemsByContextAndDateRangeAsync>d__14>(ref <LoadAvailabilityItemsByContextAndDateRangeAsync>d__);
			return <LoadAvailabilityItemsByContextAndDateRangeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00056240 File Offset: 0x00054440
		public AvailabilityScheduleItemsForContext LoadAvailabilityItemsByContextAndDates(AvailabilityScheduleContext context, IList<DateTime> days)
		{
			return this.dao.LoadAvailabilityItemsByContextAndDates(context, days);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00056260 File Offset: 0x00054460
		public AddAvailabilitiesActionResult AddAvailabilityTimesByContextAndDate(AvailabilityScheduleContext context, DateTime date, IList<AvailabilityScheduleTime> times, bool abortIfAnyProblems)
		{
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = this.LoadAvailabilityItemsByContextAndDateRange(context, date, 1);
			List<Range<TimeSpan>> list = (from g in ((availabilityScheduleItemsForContext != null) ? availabilityScheduleItemsForContext.AvailabilityScheduleItems : null) ?? new List<AvailabilityScheduleItemInfo>()
			select new Range<TimeSpan>(g.DayAndTime.Time.StartTime, g.DayAndTime.Time.EndTime)).ToList<Range<TimeSpan>>();
			bool flag = list.Count < 1;
			AddAvailabilitiesActionResult result;
			if (flag)
			{
				this.dao.ResetAvailabilityByContextAndDate(context, date, (from g in times
				select new Range<TimeSpan>(g.StartTime, g.EndTime)).ToList<Range<TimeSpan>>());
				result = new AddAvailabilitiesActionResult
				{
					AbortedEntireProcess = false,
					Results = (from g in times
					select new AddAvailabilityActionResult
					{
						Date = date,
						Time = g,
						Status = new AvailabilityScheduleItemActionResult
						{
							ActionTaken = eAvailabilityScheduleAction.AddedItem
						}
					}).ToList<AddAvailabilityActionResult>()
				};
			}
			else
			{
				List<AddAvailabilityActionResult> list2 = new List<AddAvailabilityActionResult>();
				using (IEnumerator<AvailabilityScheduleTime> enumerator = times.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AvailabilityScheduleTime time = enumerator.Current;
						List<Range<TimeSpan>> list3 = (from g in list
						where AvailabilityScheduleManager.DoTimeSpansOverlap(g.Start, g.End, time.StartTime, time.EndTime)
						select g).ToList<Range<TimeSpan>>();
						bool flag2 = list3.Count > 0;
						if (flag2)
						{
							List<AddAvailabilityActionResult> list4 = list2;
							AddAvailabilityActionResult addAvailabilityActionResult = new AddAvailabilityActionResult();
							addAvailabilityActionResult.Date = date;
							addAvailabilityActionResult.Time = time;
							AvailabilityScheduleItemActionResult availabilityScheduleItemActionResult = new AvailabilityScheduleItemActionResult();
							availabilityScheduleItemActionResult.ActionTaken = eAvailabilityScheduleAction.None;
							availabilityScheduleItemActionResult.FailureReason = eAvailabilityScheduleActionFailureReason.ConflictWithExistingSchedule;
							availabilityScheduleItemActionResult.PublicMessage = "Conflict with: " + string.Join(",", (from h in list3
							select h.Start.TimeSpanToString("h:mm tt") + " - " + h.End.TimeSpanToString("h:mm tt")).ToArray<string>());
							addAvailabilityActionResult.Status = availabilityScheduleItemActionResult;
							list4.Add(addAvailabilityActionResult);
						}
						else
						{
							list.Add(new Range<TimeSpan>(time.StartTime, time.EndTime));
							list2.Add(new AddAvailabilityActionResult
							{
								Date = date,
								Time = time,
								Status = new AvailabilityScheduleItemActionResult
								{
									ActionTaken = eAvailabilityScheduleAction.AddedItem
								}
							});
						}
					}
				}
				bool flag3;
				if (abortIfAnyProblems)
				{
					flag3 = list2.All((AddAvailabilityActionResult g) => g.Status.ActionTaken != eAvailabilityScheduleAction.AddedItem);
				}
				else
				{
					flag3 = false;
				}
				bool flag4 = flag3;
				if (flag4)
				{
					result = new AddAvailabilitiesActionResult
					{
						AbortedEntireProcess = true,
						Results = list2
					};
				}
				else
				{
					this.dao.ResetAvailabilityByContextAndDate(context, date, list);
					result = new AddAvailabilitiesActionResult
					{
						AbortedEntireProcess = false,
						Results = list2
					};
				}
			}
			return result;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00056540 File Offset: 0x00054740
		public AddAvailabilitiesActionResult AddAvailabilityDatesAndTimesByContext(AvailabilityScheduleContext context, IList<DateTime> dates, IList<AvailabilityScheduleTime> times, bool abortIfAnyProblems)
		{
			AddAvailabilitiesActionResult addAvailabilitiesActionResult = new AddAvailabilitiesActionResult
			{
				Results = new List<AddAvailabilityActionResult>()
			};
			foreach (DateTime date in dates)
			{
				AddAvailabilitiesActionResult addAvailabilitiesActionResult2 = this.AddAvailabilityTimesByContextAndDate(context, date, times, abortIfAnyProblems);
				foreach (AddAvailabilityActionResult item in addAvailabilitiesActionResult2.Results)
				{
					addAvailabilitiesActionResult.Results.Add(item);
				}
				addAvailabilitiesActionResult.AbortedEntireProcess = (addAvailabilitiesActionResult.AbortedEntireProcess || addAvailabilitiesActionResult2.AbortedEntireProcess);
			}
			return addAvailabilitiesActionResult;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00056610 File Offset: 0x00054810
		private static bool DoTimeSpansOverlap(TimeSpan t1a, TimeSpan t1b, TimeSpan t2a, TimeSpan t2b)
		{
			return !(t2b <= t1a) && !(t2a >= t1b);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00056638 File Offset: 0x00054838
		public DeleteAvailabilityActionResult DeleteAvailabilityTimeByContext(AvailabilityScheduleContext context, AvailabilityScheduleDateAndTime dayAndTime)
		{
			TimeSpan startTime = dayAndTime.Time.StartTime;
			TimeSpan endTime = dayAndTime.Time.EndTime;
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = this.LoadAvailabilityItemsByContextAndDateRange(context, dayAndTime.Date, 1);
			List<AvailabilityScheduleItemInfo> list = (from g in availabilityScheduleItemsForContext.AvailabilityScheduleItems
			where g.DayAndTime.Time.StartTime.Hours != startTime.Hours || g.DayAndTime.Time.StartTime.Minutes != startTime.Minutes || g.DayAndTime.Time.EndTime.Hours != endTime.Hours || g.DayAndTime.Time.EndTime.Minutes != endTime.Minutes
			select g).ToList<AvailabilityScheduleItemInfo>();
			bool flag = availabilityScheduleItemsForContext.AvailabilityScheduleItems.Count == list.Count;
			DeleteAvailabilityActionResult result;
			if (flag)
			{
				result = new DeleteAvailabilityActionResult
				{
					Status = new AvailabilityScheduleItemActionResult
					{
						ActionTaken = eAvailabilityScheduleAction.None,
						FailureReason = eAvailabilityScheduleActionFailureReason.InvalidParametersItemNotFound
					},
					Date = dayAndTime.Date,
					Time = dayAndTime.Time
				};
			}
			else
			{
				this.dao.ResetAvailabilityByContextAndDate(context, dayAndTime.Date, (from g in list
				select new Range<TimeSpan>(g.DayAndTime.Time.StartTime, g.DayAndTime.Time.EndTime)).ToList<Range<TimeSpan>>());
				result = new DeleteAvailabilityActionResult
				{
					Status = new AvailabilityScheduleItemActionResult
					{
						ActionTaken = eAvailabilityScheduleAction.DeletedItem
					},
					Date = dayAndTime.Date,
					Time = dayAndTime.Time
				};
			}
			return result;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00056768 File Offset: 0x00054968
		public IList<DeleteAvailabilityActionResult> DeleteAvailabilityDatesAndTimesByContext(AvailabilityScheduleContext context, IList<AvailabilityScheduleDateAndTime> dayAndTimes)
		{
			return (from g in dayAndTimes
			select this.DeleteAvailabilityTimeByContext(context, g)).ToList<DeleteAvailabilityActionResult>();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000567A5 File Offset: 0x000549A5
		public void ClearAvailabilityForTheDay(AvailabilityScheduleContext context, IList<DateTime> days)
		{
			this.dao.ClearAvailabilityForTheDay(context, days);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000567B8 File Offset: 0x000549B8
		public IList<DateTime> LoadDaysWithAvailability(int PersonId, IList<int> AvailabilityGroupIds, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadDaysWithAvailability(PersonId, AvailabilityGroupIds, StartDate, EndDate);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000567DC File Offset: 0x000549DC
		public IList<AvailabilityGroup> LoadAllAvailabilityGroups()
		{
			return this.dao.LoadAllAvailabilityGroups();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000567FC File Offset: 0x000549FC
		public IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays)
		{
			return this.dao.LoadAvailabilityForMultipleContextsAndDates(personIds, availabilityGroupIds, startDate, numDays);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00056820 File Offset: 0x00054A20
		[DebuggerStepThrough]
		public Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, int numDays)
		{
			AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__25 <LoadAvailabilityForMultipleContextsAndDatesAsync>d__ = new AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__25();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<AvailabilityScheduleItemsForContext>>.Create();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>4__this = this;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.personIds = personIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.availabilityGroupIds = availabilityGroupIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.startDate = startDate;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.numDays = numDays;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>1__state = -1;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Start<AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__25>(ref <LoadAvailabilityForMultipleContextsAndDatesAsync>d__);
			return <LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00056884 File Offset: 0x00054A84
		[DebuggerStepThrough]
		public Task<IList<AvailabilityScheduleItemsForContext>> LoadAvailabilityForMultipleContextsAndDatesAsync(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate)
		{
			AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__26 <LoadAvailabilityForMultipleContextsAndDatesAsync>d__ = new AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__26();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<AvailabilityScheduleItemsForContext>>.Create();
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>4__this = this;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.personIds = personIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.availabilityGroupIds = availabilityGroupIds;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.startDate = startDate;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.endDate = endDate;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>1__state = -1;
			<LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Start<AvailabilityScheduleManager.<LoadAvailabilityForMultipleContextsAndDatesAsync>d__26>(ref <LoadAvailabilityForMultipleContextsAndDatesAsync>d__);
			return <LoadAvailabilityForMultipleContextsAndDatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000568E8 File Offset: 0x00054AE8
		public IList<AvailabilityScheduleItemsForContext> LoadAvailabilityForMultipleContextsAndDates(IList<int> personIds, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate)
		{
			int numDays = Convert.ToInt32((endDate.Date - startDate.Date).TotalDays) + 1;
			return this.dao.LoadAvailabilityForMultipleContextsAndDates(personIds, availabilityGroupIds, startDate, numDays);
		}

		// Token: 0x02000388 RID: 904
		internal class TimeRange
		{
			// Token: 0x060017F4 RID: 6132 RVA: 0x0008CBE8 File Offset: 0x0008ADE8
			public TimeRange()
			{
				this._minutes = new bool[1440];
			}

			// Token: 0x060017F5 RID: 6133 RVA: 0x0008CC04 File Offset: 0x0008AE04
			public TimeRange(TimeSpan start, TimeSpan end)
			{
				this._minutes = new bool[1440];
				int num = Convert.ToInt32(start.TotalMinutes);
				int num2 = Convert.ToInt32(end.TotalMinutes);
				for (int i = num; i < num2; i++)
				{
					this._minutes[i] = true;
				}
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0008CC5D File Offset: 0x0008AE5D
			// (set) Token: 0x060017F7 RID: 6135 RVA: 0x0008CC65 File Offset: 0x0008AE65
			private bool[] _minutes { get; set; }

			// Token: 0x060017F8 RID: 6136 RVA: 0x0008CC70 File Offset: 0x0008AE70
			public void Subtract(TimeSpan start, TimeSpan end)
			{
				int startMins = Convert.ToInt32(start.TotalMinutes);
				int endMins = Convert.ToInt32(end.TotalMinutes);
				this.Subtract(startMins, endMins);
			}

			// Token: 0x060017F9 RID: 6137 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
			public void Subtract(int startMins, int endMins)
			{
				for (int i = startMins; i < endMins; i++)
				{
					this._minutes[i] = false;
				}
			}

			// Token: 0x060017FA RID: 6138 RVA: 0x0008CCCC File Offset: 0x0008AECC
			public void Subtract(double startMins, double endMins)
			{
				int num = Convert.ToInt32(startMins);
				int num2 = Convert.ToInt32(endMins);
				for (int i = num; i < num2; i++)
				{
					this._minutes[i] = false;
				}
			}

			// Token: 0x060017FB RID: 6139 RVA: 0x0008CD04 File Offset: 0x0008AF04
			public IList<Pair<TimeSpan, TimeSpan>> ConvertToTimeRangeList()
			{
				List<Pair<TimeSpan, TimeSpan>> list = new List<Pair<TimeSpan, TimeSpan>>();
				int i = 0;
				while (i < this._minutes.Length)
				{
					bool flag = !this._minutes[i];
					if (flag)
					{
						i++;
					}
					else
					{
						int j;
						for (j = i + 1; j < this._minutes.Length; j++)
						{
							bool flag2 = !this._minutes[j];
							if (flag2)
							{
								break;
							}
						}
						list.Add(new Pair<TimeSpan, TimeSpan>(TimeSpan.FromMinutes((double)i), TimeSpan.FromMinutes((double)j)));
						i = j;
					}
				}
				return list;
			}
		}
	}
}
