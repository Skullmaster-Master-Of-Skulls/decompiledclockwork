using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E9 RID: 745
	public class LookupCourseBase : BusinessBase<int>, ICloneable<LookupCourseBase>, ICloneable
	{
		// Token: 0x06001646 RID: 5702 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public LookupCourseBase()
		{
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0001BA50 File Offset: 0x00019C50
		public LookupCourseBase(LookupCourseBase item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.LuCourseId = item.LuCourseId;
				this.StartDate = item.StartDate;
				this.EndDate = item.EndDate;
				this.Duration = item.Duration;
				this.Term = item.Term;
				LookupSubject subject = item.Subject;
				this.Subject = ((subject != null) ? subject.Clone() : null);
				this.Course = item.Course;
				this.Section = item.Section;
				this.TimeOfDay = item.TimeOfDay;
				this.Campus = item.Campus;
				this.Department = item.Department;
				this.Location = item.Location;
				this.CourseNote = item.CourseNote;
				this.Credits = item.Credits;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0001BB34 File Offset: 0x00019D34
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int LuCourseId
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

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0001BB4C File Offset: 0x00019D4C
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0001BB54 File Offset: 0x00019D54
		public DateTime StartDate { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0001BB5D File Offset: 0x00019D5D
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0001BB65 File Offset: 0x00019D65
		public DateTime EndDate { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0001BB6E File Offset: 0x00019D6E
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x0001BB76 File Offset: 0x00019D76
		public string Duration { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0001BB7F File Offset: 0x00019D7F
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0001BB87 File Offset: 0x00019D87
		public string Term { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0001BB90 File Offset: 0x00019D90
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0001BB98 File Offset: 0x00019D98
		public LookupSubject Subject { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0001BBA1 File Offset: 0x00019DA1
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x0001BBA9 File Offset: 0x00019DA9
		public string Course { get; set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0001BBB2 File Offset: 0x00019DB2
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x0001BBBA File Offset: 0x00019DBA
		public string Section { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0001BBC3 File Offset: 0x00019DC3
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0001BBCB File Offset: 0x00019DCB
		public string TimeOfDay { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0001BBD4 File Offset: 0x00019DD4
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x0001BBDC File Offset: 0x00019DDC
		public string Campus { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0001BBE5 File Offset: 0x00019DE5
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x0001BBED File Offset: 0x00019DED
		public string Department { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0001BBF6 File Offset: 0x00019DF6
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0001BBFE File Offset: 0x00019DFE
		public string Location { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0001BC07 File Offset: 0x00019E07
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0001BC0F File Offset: 0x00019E0F
		public string CourseNote { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0001BC18 File Offset: 0x00019E18
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x0001BC20 File Offset: 0x00019E20
		public decimal Credits { get; set; }

		// Token: 0x06001664 RID: 5732 RVA: 0x0001BC2C File Offset: 0x00019E2C
		public LookupCourseBase Clone()
		{
			return new LookupCourseBase(this);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0001BC44 File Offset: 0x00019E44
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
