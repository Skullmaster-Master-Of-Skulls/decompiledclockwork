using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000524 RID: 1316
	public class ClassTestDefinitionSmall : BusinessBase<int>
	{
		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0002A37C File Offset: 0x0002857C
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x0002A394 File Offset: 0x00028594
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x0002A39C File Offset: 0x0002859C
		public int LuCourseId { get; set; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x0002A3A5 File Offset: 0x000285A5
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x0002A3AD File Offset: 0x000285AD
		public int TestDuration { get; set; }

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x0002A3B6 File Offset: 0x000285B6
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x0002A3BE File Offset: 0x000285BE
		public string CourseDescription { get; set; }

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x0002A3C7 File Offset: 0x000285C7
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x0002A3CF File Offset: 0x000285CF
		public DateTime DateOfTest { get; set; }

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x0002A3D8 File Offset: 0x000285D8
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x0002A3E0 File Offset: 0x000285E0
		public DateTime TestStartTime { get; set; }

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0002A3E9 File Offset: 0x000285E9
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x0002A3F1 File Offset: 0x000285F1
		public DateTime TestEndTime { get; set; }

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x0002A3FA File Offset: 0x000285FA
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x0002A402 File Offset: 0x00028602
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x0002A40B File Offset: 0x0002860B
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x0002A413 File Offset: 0x00028613
		public string InstructorContactedNote { get; set; }

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x0002A41C File Offset: 0x0002861C
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x0002A424 File Offset: 0x00028624
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x0002A42D File Offset: 0x0002862D
		// (set) Token: 0x060028CD RID: 10445 RVA: 0x0002A435 File Offset: 0x00028635
		public string TestPickedUpNote { get; set; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x0002A43E File Offset: 0x0002863E
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x0002A446 File Offset: 0x00028646
		public eClassTestType TestType { get; set; }

		// Token: 0x17001116 RID: 4374
		public string this[int index]
		{
			get
			{
				return this._customs[index];
			}
			set
			{
				this._customs[index] = value;
			}
		}

		// Token: 0x04001D8A RID: 7562
		private const int MAX_CUSTOM = 20;

		// Token: 0x04001D96 RID: 7574
		private readonly string[] _customs = new string[20];
	}
}
