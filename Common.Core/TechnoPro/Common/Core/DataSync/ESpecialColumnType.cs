using System;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x02000112 RID: 274
	internal enum ESpecialColumnType
	{
		// Token: 0x040001FD RID: 509
		[SpecialColumnType(true, new string[]
		{
			"startdate"
		})]
		StartDate,
		// Token: 0x040001FE RID: 510
		[SpecialColumnType(true, new string[]
		{
			"enddate"
		})]
		EndDate,
		// Token: 0x040001FF RID: 511
		[SpecialColumnType(true, new string[]
		{
			"term"
		})]
		Term,
		// Token: 0x04000200 RID: 512
		[SpecialColumnType(true, new string[]
		{
			"subject"
		})]
		Subject,
		// Token: 0x04000201 RID: 513
		[SpecialColumnType(true, new string[]
		{
			"course",
			"coursecode"
		})]
		CourseCode,
		// Token: 0x04000202 RID: 514
		[SpecialColumnType(true, new string[]
		{
			"section"
		})]
		Section,
		// Token: 0x04000203 RID: 515
		[SpecialColumnType(new string[]
		{
			"duration"
		})]
		Duration,
		// Token: 0x04000204 RID: 516
		[SpecialColumnType(new string[]
		{
			"timeofday"
		})]
		TimeOfDay,
		// Token: 0x04000205 RID: 517
		[SpecialColumnType(new string[]
		{
			"campus"
		})]
		Campus,
		// Token: 0x04000206 RID: 518
		[SpecialColumnType(new string[]
		{
			"department"
		})]
		Department,
		// Token: 0x04000207 RID: 519
		[SpecialColumnType(new string[]
		{
			"location",
			"room"
		})]
		LocationRoom,
		// Token: 0x04000208 RID: 520
		[SpecialColumnType(new string[]
		{
			"externalcourseid"
		})]
		ExternalCourseId,
		// Token: 0x04000209 RID: 521
		[SpecialColumnType(new string[]
		{
			"instructorname"
		})]
		InstructorName,
		// Token: 0x0400020A RID: 522
		[SpecialColumnType(new string[]
		{
			"instructorusername"
		})]
		InstructorUsername,
		// Token: 0x0400020B RID: 523
		[SpecialColumnType(new string[]
		{
			"instructoremployeeid",
			"instructorid"
		})]
		InstructorEmployeeId,
		// Token: 0x0400020C RID: 524
		[SpecialColumnType(new string[]
		{
			"instructorexternalid"
		})]
		InstructorExternalId,
		// Token: 0x0400020D RID: 525
		[SpecialColumnType(new string[]
		{
			"instructorphone"
		})]
		InstructorPhone,
		// Token: 0x0400020E RID: 526
		[SpecialColumnType(new string[]
		{
			"instructoremail"
		})]
		InstructorEmail,
		// Token: 0x0400020F RID: 527
		[SpecialColumnType(new string[]
		{
			"instructorisprimary"
		})]
		InstructorIsPrimary,
		// Token: 0x04000210 RID: 528
		[SpecialColumnType(new string[]
		{
			"instructorpercentage"
		})]
		InstructorPercentage,
		// Token: 0x04000211 RID: 529
		[SpecialColumnType(new string[]
		{
			"starttime"
		})]
		TimetableStartTime,
		// Token: 0x04000212 RID: 530
		[SpecialColumnType(new string[]
		{
			"endtime"
		})]
		TimetableEndTime,
		// Token: 0x04000213 RID: 531
		[SpecialColumnType(new string[]
		{
			"timetableroom"
		})]
		TimetableRoom,
		// Token: 0x04000214 RID: 532
		[SpecialColumnType(new string[]
		{
			"dayofweek"
		})]
		TimetableDayOfWeek,
		// Token: 0x04000215 RID: 533
		[SpecialColumnType(new string[]
		{
			"examstartdate"
		})]
		FinalExamInfoStartDate,
		// Token: 0x04000216 RID: 534
		[SpecialColumnType(new string[]
		{
			"examenddate"
		})]
		FinalExamInfoEndDate,
		// Token: 0x04000217 RID: 535
		[SpecialColumnType(new string[]
		{
			"examlocation"
		})]
		FinalExamInfoLocation,
		// Token: 0x04000218 RID: 536
		[Obsolete("Not implemented yet - it isn't stored anywhere on the class test def currently")]
		[SpecialColumnType(new string[]
		{
			"examid"
		})]
		FinalExamInfoId,
		// Token: 0x04000219 RID: 537
		[SpecialColumnType(new string[]
		{
			"coursenote"
		})]
		CourseNote,
		// Token: 0x0400021A RID: 538
		[SpecialColumnType(new string[]
		{
			"credits"
		})]
		Credits,
		// Token: 0x0400021B RID: 539
		[SpecialColumnType(new string[]
		{
			"subjectlong"
		})]
		SubjectLong,
		// Token: 0x0400021C RID: 540
		[SpecialColumnType(true, false, new string[]
		{
			"grade"
		})]
		CourseGrade,
		// Token: 0x0400021D RID: 541
		[SpecialColumnType(true, false, new string[]
		{
			"gradeletter"
		})]
		CourseGradeLetter,
		// Token: 0x0400021E RID: 542
		[SpecialColumnType(true, false, new string[]
		{
			"inprogressgrade"
		})]
		CourseInProgressGrade,
		// Token: 0x0400021F RID: 543
		[SpecialColumnType(true, false, new string[]
		{
			"inprogressgradeletter"
		})]
		CourseInProgressGradeLetter,
		// Token: 0x04000220 RID: 544
		[SpecialColumnType(true, false, new string[]
		{
			"tuition"
		})]
		Tuition,
		// Token: 0x04000221 RID: 545
		[SpecialColumnType(true, false, new string[]
		{
			"registrationdate"
		})]
		RegistrationDate,
		// Token: 0x04000222 RID: 546
		[SpecialColumnType(true, false, new string[]
		{
			"registrationnote"
		})]
		RegistrationNote,
		// Token: 0x04000223 RID: 547
		MonStartTime,
		// Token: 0x04000224 RID: 548
		MonEndTime,
		// Token: 0x04000225 RID: 549
		TueStartTime,
		// Token: 0x04000226 RID: 550
		TueEndTime,
		// Token: 0x04000227 RID: 551
		WedStartTime,
		// Token: 0x04000228 RID: 552
		WedEndTime,
		// Token: 0x04000229 RID: 553
		ThuStartTime,
		// Token: 0x0400022A RID: 554
		ThuEndTime,
		// Token: 0x0400022B RID: 555
		FriStartTime,
		// Token: 0x0400022C RID: 556
		FriEndTime,
		// Token: 0x0400022D RID: 557
		SatStartTime,
		// Token: 0x0400022E RID: 558
		SatEndTime,
		// Token: 0x0400022F RID: 559
		SunStartTime,
		// Token: 0x04000230 RID: 560
		SunEndTime
	}
}
