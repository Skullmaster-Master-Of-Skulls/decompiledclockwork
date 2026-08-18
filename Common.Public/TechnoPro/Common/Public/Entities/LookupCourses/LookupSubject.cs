using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002EE RID: 750
	[Serializable]
	public class LookupSubject : BusinessBase<int>, ICloneable<LookupSubject>, ICloneable
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		public LookupSubject()
		{
			this.SubjectId = 0;
			this.SubjectCode = "";
			this.SubjectDescription = "";
			this.SubjectEmail = "";
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0001BF24 File Offset: 0x0001A124
		public LookupSubject(LookupSubject item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.SubjectId = item.SubjectId;
				this.SubjectCode = item.SubjectCode;
				this.SubjectDescription = item.SubjectDescription;
				this.SubjectEmail = item.SubjectEmail;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0001BF78 File Offset: 0x0001A178
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SubjectId
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

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0001BF90 File Offset: 0x0001A190
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0001BF98 File Offset: 0x0001A198
		public string SubjectCode { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0001BFA1 File Offset: 0x0001A1A1
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x0001BFA9 File Offset: 0x0001A1A9
		public string SubjectDescription { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0001BFB2 File Offset: 0x0001A1B2
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x0001BFBA File Offset: 0x0001A1BA
		public string SubjectEmail { get; set; }

		// Token: 0x060016B0 RID: 5808 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public LookupSubject Clone()
		{
			return new LookupSubject(this);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
