using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8F RID: 3215
	public static class TestBookingAdapter
	{
		// Token: 0x060042F7 RID: 17143 RVA: 0x00023A00 File Offset: 0x00021C00
		public static int GetDurationMinutes(this ClassTestDTO ClassTest)
		{
			return (ClassTest == null) ? 0 : TestBookingAdapter.GetDurationMinutes(ClassTest.StartDateTime, ClassTest.EndDateTime);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x00023A2C File Offset: 0x00021C2C
		public static int GetDurationMinutes(this ClassTestBaseDTO ClassTest)
		{
			return (ClassTest == null) ? 0 : TestBookingAdapter.GetDurationMinutes(ClassTest.StartDateTime, ClassTest.EndDateTime);
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x00023A58 File Offset: 0x00021C58
		private static int GetDurationMinutes(DateTime startDateTime, DateTime endDateTime)
		{
			DateTime date = startDateTime.Date;
			DateTime d = date.Add(startDateTime.TimeOfDay);
			DateTime d2 = date.Add(endDateTime.TimeOfDay);
			return Convert.ToInt32((d2 - d).TotalMinutes);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x00023AA8 File Offset: 0x00021CA8
		public static string GetInstructorName(this LookupInstructorDTO Instructor)
		{
			bool flag = Instructor == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (Instructor.Name ?? "");
			}
			return result;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00023ADC File Offset: 0x00021CDC
		public static string GetDescription(this AccommodationForTestDTO AccommodationForTest)
		{
			bool discrepency = AccommodationForTest.Discrepency;
			string result;
			if (discrepency)
			{
				result = string.Format("** {0} ** {1}", AccommodationForTest.DynamicFieldData.ToString(), AccommodationForTest.DiscrepencyMessage);
			}
			else
			{
				result = AccommodationForTest.DynamicFieldData.ToString();
			}
			return result;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x00023B24 File Offset: 0x00021D24
		public static PersonBaseDTO GetFirstStudent(this TestDTO Test)
		{
			foreach (AttendeeDTO attendeeDTO in Test.Attendees)
			{
				bool flag = attendeeDTO.Person.CoreGroup == eCoreGroupDTO.Students;
				if (flag)
				{
					return attendeeDTO.Person;
				}
			}
			return null;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x00023B98 File Offset: 0x00021D98
		public static string GetSittingTitle(this SittingDTO Sitting)
		{
			return Sitting.GetSittingTitle("invigilator");
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public static string GetSittingTitle(this SittingDTO Sitting, string InvigilatorLabel)
		{
			PersonBaseDTO invigilator = Sitting.Invigilator;
			bool flag = invigilator == null;
			string text;
			if (flag)
			{
				text = "No " + InvigilatorLabel + " assigned";
			}
			else
			{
				text = string.Format("{0}-{1}", (invigilator.FirstName.Length > 0) ? invigilator.FirstName.Substring(0, 1) : "", invigilator.LastName);
			}
			Range<DateTime> range = new Range<DateTime>
			{
				Start = DateTime.Now,
				End = DateTime.Now.AddHours(1.0)
			};
			AppointmentRoomDTO room = Sitting.Room;
			string location = Sitting.Location;
			string text2 = string.Format("{0}{1}{2}", (room == null) ? "" : (room.RoomTitle ?? ""), (room == null || string.IsNullOrEmpty(location)) ? "" : " ", (location == null) ? "" : location);
			bool flag2 = text2.Length < 1;
			if (flag2)
			{
				text2 = "No room";
			}
			return string.Format("{0}_{1}_{2}_{3} - {4}_{5}", new object[]
			{
				text,
				Sitting.ExamDate.ToString("ddMMMyyyy"),
				text2,
				range.Start.ToString("H:mm"),
				range.End.ToString("H:mm"),
				Sitting.SittingId.ToString()
			});
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x00023D34 File Offset: 0x00021F34
		public static bool GetIsAllDay(this TestDTO Test)
		{
			return Test.StartDateTime.Hour == 0 && Test.EndDateTime.Hour == 23;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x00023D6C File Offset: 0x00021F6C
		public static string GetShortString(this SittingDTO Sitting)
		{
			return Sitting.GetShortString("invigilator");
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x00023D8C File Offset: 0x00021F8C
		public static string GetShortString(this SittingDTO Sitting, string InvigilatorLabel)
		{
			bool flag = Sitting == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				DateTime? scheduledStartDateTime = Sitting.ScheduledStartDateTime;
				DateTime? scheduledEndDateTime = Sitting.ScheduledEndDateTime;
				DateTime dateTime = (Sitting.VirtualMinStartDateTimeFromBookings != null) ? Sitting.VirtualMinStartDateTimeFromBookings.Value : DateTime.MinValue;
				DateTime dateTime2 = (Sitting.VirtualMaxEndDateTimeFromBookings != null) ? Sitting.VirtualMaxEndDateTimeFromBookings.Value : DateTime.MinValue;
				DateTime d = (scheduledStartDateTime != null && scheduledStartDateTime.Value != DateTime.MinValue) ? scheduledStartDateTime.Value : dateTime;
				DateTime d2 = (scheduledEndDateTime != null && scheduledEndDateTime.Value != DateTime.MinValue) ? scheduledEndDateTime.Value : dateTime2;
				string sittingTitle = Sitting.GetSittingTitle(InvigilatorLabel);
				int sittingId = Sitting.SittingId;
				bool flag2 = d == DateTime.MinValue || d2 == DateTime.MinValue;
				if (flag2)
				{
					result = string.Format("{0}", string.IsNullOrEmpty(sittingTitle) ? sittingId.ToString() : string.Format("{0} ({1})", sittingTitle, sittingId.ToString()));
				}
				else
				{
					result = string.Format("{0} [{1} to {2}]", string.IsNullOrEmpty(sittingTitle) ? sittingId.ToString() : string.Format("{0} ({1})", sittingTitle, sittingId.ToString()), d.ToString("MMM d, yyyy . h:mm tt"), d2.ToString("MMM d, yyyy . h:mm tt"));
				}
			}
			return result;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x00023F14 File Offset: 0x00022114
		public static LookupCourseBaseDTO GetCourse(this TestDTO test)
		{
			bool flag = test == null || (test.ClassTestInfo == null && test.StudentClassTestInfo == null);
			LookupCourseBaseDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = test.ClassTestInfo != null;
				if (flag2)
				{
					result = test.ClassTestInfo.Course;
				}
				else
				{
					result = test.StudentClassTestInfo.Course;
				}
			}
			return result;
		}
	}
}
