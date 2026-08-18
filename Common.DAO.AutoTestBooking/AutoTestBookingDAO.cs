using System;
using System.Collections.Generic;
using System.Linq;
using Databases;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.AutoTestBooking.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.DAO.AutoTestBooking
{
	// Token: 0x02000003 RID: 3
	public class AutoTestBookingDAO : IAutoTestBookingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002383 File Offset: 0x00000583
		public AutoTestBookingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002392 File Offset: 0x00000592
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000239A File Offset: 0x0000059A
		public OperationContext OpContext { get; set; }

		// Token: 0x0600000A RID: 10 RVA: 0x000023A3 File Offset: 0x000005A3
		public FindPotentialBookingsResp FindPotentialBookingsExplicit(FindPotentialBookingsReq req)
		{
			return Booker.FindPotentialTestBookings(req, this.OpContext);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023B4 File Offset: 0x000005B4
		public int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse, IList<SpecialAccommodation> AllSpecialAccommodations)
		{
			List<AccommodationBasic> accommodationsToUse = AccommodationsToUse.ToList<Accommodation>().ConvertAll<AccommodationBasic>((Accommodation g) => new AccommodationBasic
			{
				ControlId = g.ControlId,
				ControlCaptionAndValue = string.Concat(new string[]
				{
					g.Title,
					" ",
					g.LookupText,
					" ",
					g.SubText
				})
			});
			return Booker.CalculateExtraTime(ClassTestDurationInMinutes, accommodationsToUse, AllSpecialAccommodations);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023F8 File Offset: 0x000005F8
		public int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse, IList<SpecialAccommodation> AllSpecialAccommodations)
		{
			List<AccommodationBasic> accommodationsToUse = AccommodationsToUse.ToList<Accommodation>().ConvertAll<AccommodationBasic>((Accommodation g) => new AccommodationBasic
			{
				ControlId = g.ControlId,
				ControlCaptionAndValue = string.Concat(new string[]
				{
					g.Title,
					" ",
					g.LookupText,
					" ",
					g.SubText
				})
			});
			return Booker.CalculateBreakTime(ClassTestDurationInMinutes, accommodationsToUse, AllSpecialAccommodations);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000243C File Offset: 0x0000063C
		public ApplySpecialAccommodationsResp ApplySpecialAccommodationRules(bool debugMode, int pid, int lucid, IList<SpecialAccommodation> specialAccommodations, DateTime classTestStartDateTime, DateTime classTestEndDateTime, IList<Accommodation> accommodationsToUse, int appIdToIgnoreWhenCheckingStudentsSchedule, int overrideRoomAvailabilityPid, IList<Room> availableRooms, bool IgnoreStudentsSchedule, IList<int> IgnoreStudentAppointmentIds)
		{
			return Booker.ApplySpecialAccommodationRules(debugMode, pid, lucid, specialAccommodations, classTestStartDateTime, classTestEndDateTime, accommodationsToUse, appIdToIgnoreWhenCheckingStudentsSchedule, overrideRoomAvailabilityPid, availableRooms, IgnoreStudentsSchedule, IgnoreStudentAppointmentIds, this.OpContext);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000246C File Offset: 0x0000066C
		public int FindFinalExamAppTypeToUseForNewExamAutoBooking()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			object obj = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).ExecuteScalar("SELECT TOP 1 a.apptypeid FROM exams e LEFT JOIN appointments a ON a.examid = e.examid WHERE e.typecode = 'F'\r\nAND a.apptypeid > 0\r\nORDER BY e.dateoftest DESC");
			if (obj == null || obj is DBNull || !(obj is int))
			{
				return -1;
			}
			return (int)obj;
		}
	}
}
