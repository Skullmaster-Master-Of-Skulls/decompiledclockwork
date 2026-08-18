using System;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsTestBooking;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking;
using TechnoPro.Common.UI.Web.Entity.DynamicForms.Accommodations;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsTestBooking
{
	// Token: 0x02000021 RID: 33
	public class AutoTestBookingWebClientManager : IAutoTestBookingWebClientManager
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000731C File Offset: 0x0000551C
		private DateTime? GetCutoffDate(eClassTestType testType)
		{
			Setting setting = (testType == eClassTestType.FinalExam) ? Setting.EXAMBOOKING_CutoffBookingDate : Setting.TESTBOOKING_CutoffBookingDate;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			CutoffTime cutoffTime = webSettingsClientManager.GetSettingValue<string>(setting).CutoffTimeFromXml() ?? CutoffTime.None;
			bool enabled = cutoffTime.Enabled;
			if (enabled)
			{
				DateTime? minimumDateForBeforeTypeCutoff = cutoffTime.GetMinimumDateForBeforeTypeCutoff();
				bool flag = minimumDateForBeforeTypeCutoff != null;
				if (flag)
				{
					return minimumDateForBeforeTypeCutoff;
				}
			}
			setting = ((testType == eClassTestType.FinalExam) ? Setting.EXAMBOOKING_WizardSetting_MinDaysAheadToBook : Setting.TESTBOOKING_WizardSetting_MinDaysAheadToBook);
			return new DateTime?(DateTime.Now.Date.AddDays((double)webSettingsClientManager.GetSettingValue<int>(setting)));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000073BC File Offset: 0x000055BC
		private Range<DateTime> GetFinalExamPeriodRange()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			DateTime settingValue = webSettingsClientManager.GetSettingValue<DateTime>(Setting.EXAMBOOKING_FinalExamRequest_FinalsStartDate);
			DateTime settingValue2 = webSettingsClientManager.GetSettingValue<DateTime>(Setting.EXAMBOOKING_FinalExamRequest_FinalsEndDate);
			DateTime? dateTime = (settingValue == default(DateTime) || settingValue == DateTime.MinValue) ? null : new DateTime?(settingValue);
			DateTime? dateTime2 = (settingValue2 == default(DateTime) || settingValue2 == DateTime.MinValue) ? null : new DateTime?(settingValue2);
			bool flag = dateTime == null && dateTime2 == null;
			Range<DateTime> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = dateTime != null && dateTime2 != null;
				if (flag2)
				{
					result = new Range<DateTime>(dateTime.Value, dateTime2.Value);
				}
				else
				{
					result = ((dateTime != null) ? new Range<DateTime>(dateTime.Value, dateTime.Value.AddMonths(12)) : new Range<DateTime>(DateTime.Now.Date, dateTime2.Value));
				}
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000074E8 File Offset: 0x000056E8
		public MinMaxDateRangeValue FigureOutMinMaxDateRangeStudentIsAllowedToBookForExam(int PersonId)
		{
			DateTime date = DateTime.Now.Date;
			DateTime? cutoffDate = this.GetCutoffDate(eClassTestType.FinalExam);
			DateTime start = (cutoffDate != null) ? cutoffDate.Value : date;
			Range<DateTime> range = new Range<DateTime>
			{
				Start = start,
				End = date.AddMonths(8)
			};
			Range<DateTime> finalExamPeriodRange = this.GetFinalExamPeriodRange();
			bool flag = finalExamPeriodRange != null;
			if (flag)
			{
				bool flag2 = finalExamPeriodRange.End < date;
				if (flag2)
				{
					return new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.FinalExamPeriodRangeIsInThePast
					};
				}
				this.MergeAllowedDateRanges(range, finalExamPeriodRange);
				bool flag3 = range.Start > range.End;
				if (flag3)
				{
					return new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.InvalidFinalExamPeriodRangeAndOrCutoffDate
					};
				}
			}
			IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
			AccommodationsExpiryDate studentAccommodationsExpiryDate = accommodationsWebClientManager.GetStudentAccommodationsExpiryDate(PersonId, true);
			bool flag4 = studentAccommodationsExpiryDate.Status == eAccommodationsExpiryDateStatus.BlankAndMeansExpired;
			MinMaxDateRangeValue result;
			if (flag4)
			{
				result = new MinMaxDateRangeValue
				{
					Status = eMinMaxDateRangeInvalidReason.AccommodationsExpiredBecauseDateIsBlank
				};
			}
			else
			{
				bool flag5 = studentAccommodationsExpiryDate.Status == eAccommodationsExpiryDateStatus.Normal && studentAccommodationsExpiryDate.ExpiryDate != null;
				if (flag5)
				{
					bool flag6 = range.End > studentAccommodationsExpiryDate.ExpiryDate.Value;
					if (flag6)
					{
						range.End = studentAccommodationsExpiryDate.ExpiryDate.Value;
					}
				}
				bool flag7 = range.End < range.Start;
				if (flag7)
				{
					result = new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.AccommodationsExpiredBeforeMinBookingDate
					};
				}
				else
				{
					result = new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.IsValid,
						DateRange = range
					};
				}
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007688 File Offset: 0x00005888
		private void MergeAllowedDateRanges(Range<DateTime> rangeToKeep, Range<DateTime> rangeToMergeIn)
		{
			bool flag = rangeToKeep.Start < rangeToMergeIn.Start;
			if (flag)
			{
				rangeToKeep.Start = rangeToMergeIn.Start;
			}
			bool flag2 = rangeToKeep.End > rangeToMergeIn.End;
			if (flag2)
			{
				rangeToKeep.End = rangeToMergeIn.End;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000076DC File Offset: 0x000058DC
		public MinMaxDateRangeValue FigureOutMinMaxDateRangeStudentIsAllowedToBookForTest(int PersonId)
		{
			DateTime today = DateTime.Today;
			DateTime? cutoffDate = this.GetCutoffDate(eClassTestType.Midterm);
			DateTime start = (cutoffDate != null) ? cutoffDate.Value : today;
			Range<DateTime> range = new Range<DateTime>
			{
				Start = start,
				End = today.AddMonths(8)
			};
			Range<DateTime> finalExamPeriodRange = this.GetFinalExamPeriodRange();
			bool flag = finalExamPeriodRange != null;
			if (flag)
			{
				DateTime date = finalExamPeriodRange.Start.Date;
				DateTime date2 = finalExamPeriodRange.End.Date;
				bool flag2 = date < range.Start && date2 > range.End;
				if (flag2)
				{
					return new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.InvalidFinalExamPeriodRangeAndOrCutoffDate
					};
				}
				bool flag3 = date > range.Start;
				if (flag3)
				{
					bool flag4 = date2 >= range.Start && date2 <= range.End;
					if (flag4)
					{
						range.Start = range.Start.AddDays(1.0);
					}
					else
					{
						bool flag5 = date >= range.Start && date <= range.End;
						if (flag5)
						{
						}
					}
				}
			}
			bool flag6 = finalExamPeriodRange != null && finalExamPeriodRange.End > today;
			if (flag6)
			{
				DateTime dateTime = finalExamPeriodRange.Start.AddDays(-1.0);
				bool flag7 = dateTime < range.Start;
				if (flag7)
				{
					return new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.InvalidFinalExamPeriodRangeAndOrCutoffDate
					};
				}
				range.End = dateTime;
			}
			IAccommodationsWebClientManager accommodationsWebClientManager = new AccommodationsWebClientManager();
			AccommodationsExpiryDate studentAccommodationsExpiryDate = accommodationsWebClientManager.GetStudentAccommodationsExpiryDate(PersonId, true);
			bool flag8 = studentAccommodationsExpiryDate.Status == eAccommodationsExpiryDateStatus.BlankAndMeansExpired;
			MinMaxDateRangeValue result;
			if (flag8)
			{
				result = new MinMaxDateRangeValue
				{
					Status = eMinMaxDateRangeInvalidReason.AccommodationsExpiredBecauseDateIsBlank
				};
			}
			else
			{
				bool flag9 = studentAccommodationsExpiryDate.Status == eAccommodationsExpiryDateStatus.Normal && studentAccommodationsExpiryDate.ExpiryDate != null;
				if (flag9)
				{
					bool flag10 = range.End > studentAccommodationsExpiryDate.ExpiryDate.Value;
					if (flag10)
					{
						range.End = studentAccommodationsExpiryDate.ExpiryDate.Value;
					}
				}
				bool flag11 = range.End < range.Start;
				if (flag11)
				{
					result = new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.AccommodationsExpiredBeforeMinBookingDate
					};
				}
				else
				{
					result = new MinMaxDateRangeValue
					{
						Status = eMinMaxDateRangeInvalidReason.IsValid,
						DateRange = range
					};
				}
			}
			return result;
		}
	}
}
