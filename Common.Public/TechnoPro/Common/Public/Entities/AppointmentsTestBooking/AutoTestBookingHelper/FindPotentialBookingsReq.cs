using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000540 RID: 1344
	public class FindPotentialBookingsReq : ICloneable<FindPotentialBookingsReq>, ICloneable
	{
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0002DD42 File Offset: 0x0002BF42
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x0002DD4A File Offset: 0x0002BF4A
		public bool DebugMode { get; set; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0002DD53 File Offset: 0x0002BF53
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x0002DD5B File Offset: 0x0002BF5B
		public int Pid { get; set; }

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0002DD64 File Offset: 0x0002BF64
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x0002DD6C File Offset: 0x0002BF6C
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0002DD75 File Offset: 0x0002BF75
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x0002DD7D File Offset: 0x0002BF7D
		public int Lucid { get; set; }

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0002DD86 File Offset: 0x0002BF86
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x0002DD8E File Offset: 0x0002BF8E
		public DateTime DayToLookIn { get; set; }

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0002DD97 File Offset: 0x0002BF97
		// (set) Token: 0x06002AD7 RID: 10967 RVA: 0x0002DD9F File Offset: 0x0002BF9F
		public Test ClassTest { get; set; }

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
		// (set) Token: 0x06002AD9 RID: 10969 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		public IList<Accommodation> Accommodations { get; set; }

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x0002DDB9 File Offset: 0x0002BFB9
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x0002DDC1 File Offset: 0x0002BFC1
		public IList<Asset> AvailableAssets { get; set; }

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x0002DDCA File Offset: 0x0002BFCA
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x0002DDD2 File Offset: 0x0002BFD2
		public IList<Room> AvailableRooms0 { get; set; }

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0002DDDB File Offset: 0x0002BFDB
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x0002DDE3 File Offset: 0x0002BFE3
		public IList<SpecialAccommodation> SpecialAccommodations { get; set; }

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x0002DDEC File Offset: 0x0002BFEC
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x0002DDF4 File Offset: 0x0002BFF4
		public IList<TestRule> Rules { get; set; }

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x0002DDFD File Offset: 0x0002BFFD
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x0002DE05 File Offset: 0x0002C005
		public int OverrideRoomAvailabilityPid { get; set; }

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0002DE0E File Offset: 0x0002C00E
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x0002DE16 File Offset: 0x0002C016
		public IList<Booking> UnavailableRoomBookings { get; set; }

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x0002DE1F File Offset: 0x0002C01F
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x0002DE27 File Offset: 0x0002C027
		public bool LoadRoomSchedules { get; set; }

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0002DE30 File Offset: 0x0002C030
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x0002DE38 File Offset: 0x0002C038
		public bool ApplySpecialAccommodationRules { get; set; }

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0002DE41 File Offset: 0x0002C041
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x0002DE49 File Offset: 0x0002C049
		public int AppIdToIgnoreWhenCheckingStudentsSchedule { get; set; }

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x0002DE52 File Offset: 0x0002C052
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x0002DE5A File Offset: 0x0002C05A
		public CustomTestBookingRulesClass CustomTestBookingRules { get; set; }

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x0002DE63 File Offset: 0x0002C063
		// (set) Token: 0x06002AEF RID: 10991 RVA: 0x0002DE6B File Offset: 0x0002C06B
		public bool IgnoreTimetable { get; set; }

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x0002DE74 File Offset: 0x0002C074
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x0002DE7C File Offset: 0x0002C07C
		public int TimetableShiftMaxNumMinutesBeforeClassTime { get; set; }

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0002DE85 File Offset: 0x0002C085
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x0002DE8D File Offset: 0x0002C08D
		public int TimetableShiftMaxNumMinutesAfterClassTime { get; set; }

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x0002DE96 File Offset: 0x0002C096
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x0002DE9E File Offset: 0x0002C09E
		public bool RestrictByCampus { get; set; }

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x0002DEA7 File Offset: 0x0002C0A7
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x0002DEAF File Offset: 0x0002C0AF
		public int BufferMinutesPre { get; set; }

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x0002DEB8 File Offset: 0x0002C0B8
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public int BufferMinutesPost { get; set; }

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0002DEC9 File Offset: 0x0002C0C9
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0002DED1 File Offset: 0x0002C0D1
		public IList<int> IgnoreStudentAppointmentIds { get; set; }

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0002DEDA File Offset: 0x0002C0DA
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0002DEE2 File Offset: 0x0002C0E2
		public bool IgnoreStudentsSchedule { get; set; }

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0002DEEB File Offset: 0x0002C0EB
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x0002DEF3 File Offset: 0x0002C0F3
		public bool IgnoreTwoTestsSameCourseSameDay { get; set; }

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0002DEFC File Offset: 0x0002C0FC
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x0002DF04 File Offset: 0x0002C104
		public eTestExamSettingType TestBookingType { get; set; }

		// Token: 0x06002B02 RID: 11010 RVA: 0x0002DF0D File Offset: 0x0002C10D
		public FindPotentialBookingsReq()
		{
			this.ApplySpecialAccommodationRules = true;
			this.TestBookingType = eTestExamSettingType.Midterm;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0002DF28 File Offset: 0x0002C128
		public FindPotentialBookingsReq(FindPotentialBookingsReq req)
		{
			bool flag = req == null;
			if (!flag)
			{
				this.DebugMode = req.DebugMode;
				this.Pid = req.Pid;
				this.Lucid = req.Lucid;
				this.DayToLookIn = req.DayToLookIn;
				this.ClassTest = req.ClassTest;
				this.TestBookingContext = req.TestBookingContext;
				IList<Accommodation> accommodations;
				if (req.Accommodations != null)
				{
					accommodations = (from g in req.Accommodations
					where true
					select g).ToList<Accommodation>();
				}
				else
				{
					accommodations = null;
				}
				this.Accommodations = accommodations;
				IList<Asset> availableAssets;
				if (req.AvailableAssets != null)
				{
					availableAssets = (from g in req.AvailableAssets
					where true
					select g).ToList<Asset>();
				}
				else
				{
					availableAssets = null;
				}
				this.AvailableAssets = availableAssets;
				IList<Room> availableRooms;
				if (req.AvailableRooms0 != null)
				{
					availableRooms = (from g in req.AvailableRooms0
					where true
					select g).ToList<Room>();
				}
				else
				{
					availableRooms = null;
				}
				this.AvailableRooms0 = availableRooms;
				IList<SpecialAccommodation> specialAccommodations;
				if (req.SpecialAccommodations != null)
				{
					specialAccommodations = (from g in req.SpecialAccommodations
					where true
					select g).ToList<SpecialAccommodation>();
				}
				else
				{
					specialAccommodations = null;
				}
				this.SpecialAccommodations = specialAccommodations;
				IList<TestRule> rules;
				if (req.Rules != null)
				{
					rules = (from g in req.Rules
					where true
					select g).ToList<TestRule>();
				}
				else
				{
					rules = null;
				}
				this.Rules = rules;
				IList<Booking> unavailableRoomBookings;
				if (req.UnavailableRoomBookings != null)
				{
					unavailableRoomBookings = (from g in req.UnavailableRoomBookings
					where true
					select g).ToList<Booking>();
				}
				else
				{
					unavailableRoomBookings = null;
				}
				this.UnavailableRoomBookings = unavailableRoomBookings;
				IList<int> ignoreStudentAppointmentIds;
				if (req.IgnoreStudentAppointmentIds != null)
				{
					ignoreStudentAppointmentIds = (from g in req.IgnoreStudentAppointmentIds
					where true
					select g).ToList<int>();
				}
				else
				{
					ignoreStudentAppointmentIds = null;
				}
				this.IgnoreStudentAppointmentIds = ignoreStudentAppointmentIds;
				this.OverrideRoomAvailabilityPid = req.OverrideRoomAvailabilityPid;
				this.UnavailableRoomBookings = req.UnavailableRoomBookings;
				this.LoadRoomSchedules = req.LoadRoomSchedules;
				this.ApplySpecialAccommodationRules = req.ApplySpecialAccommodationRules;
				this.AppIdToIgnoreWhenCheckingStudentsSchedule = req.AppIdToIgnoreWhenCheckingStudentsSchedule;
				this.CustomTestBookingRules = req.CustomTestBookingRules;
				this.IgnoreTimetable = req.IgnoreTimetable;
				this.TimetableShiftMaxNumMinutesAfterClassTime = req.TimetableShiftMaxNumMinutesAfterClassTime;
				this.TimetableShiftMaxNumMinutesBeforeClassTime = req.TimetableShiftMaxNumMinutesBeforeClassTime;
				this.RestrictByCampus = req.RestrictByCampus;
				this.BufferMinutesPre = req.BufferMinutesPre;
				this.BufferMinutesPost = req.BufferMinutesPost;
				this.IgnoreStudentsSchedule = req.IgnoreStudentsSchedule;
				this.IgnoreTwoTestsSameCourseSameDay = req.IgnoreTwoTestsSameCourseSameDay;
				this.TestBookingType = req.TestBookingType;
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0002E224 File Offset: 0x0002C424
		public FindPotentialBookingsReq Clone()
		{
			return new FindPotentialBookingsReq(this);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0002E23C File Offset: 0x0002C43C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
