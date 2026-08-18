using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000357 RID: 855
	public class PerDateEntry : BusinessBase<int>, ICloneable<PerDateEntry>, ICloneable
	{
		// Token: 0x06001A87 RID: 6791 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public PerDateEntry()
		{
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		public PerDateEntry(PerDateEntry perDateEntry)
		{
			bool flag = perDateEntry == null;
			if (!flag)
			{
				this.AppointmentId = perDateEntry.AppointmentId;
				this.DateEntered = perDateEntry.DateEntered;
				this.WhoEntered = perDateEntry.WhoEntered;
				this.Student = perDateEntry.Student;
				this.Description = perDateEntry.Description;
				this.ScreenNum = perDateEntry.ScreenNum;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0001E960 File Offset: 0x0001CB60
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0000E258 File Offset: 0x0000C458
		public int AppointmentId
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

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0001E978 File Offset: 0x0001CB78
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0001E980 File Offset: 0x0001CB80
		public DateTime DateEntered { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x0001E989 File Offset: 0x0001CB89
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0001E991 File Offset: 0x0001CB91
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0001E99A File Offset: 0x0001CB9A
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		public PersonBase Student { get; set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0001E9AB File Offset: 0x0001CBAB
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x0001E9B3 File Offset: 0x0001CBB3
		public string Description { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		public int ScreenNum { get; set; }

		// Token: 0x06001A95 RID: 6805 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		public PerDateEntry Clone()
		{
			return new PerDateEntry(this);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
