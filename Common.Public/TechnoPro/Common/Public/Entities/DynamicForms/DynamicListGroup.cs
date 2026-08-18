using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000369 RID: 873
	public class DynamicListGroup : BusinessBase<int>
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0001EDC0 File Offset: 0x0001CFC0
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int LookupGroupId
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

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public string Description { get; set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0001EDE9 File Offset: 0x0001CFE9
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x0001EDF1 File Offset: 0x0001CFF1
		public string ChildList { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0001EDFA File Offset: 0x0001CFFA
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x0001EE02 File Offset: 0x0001D002
		public int SortBy { get; set; }
	}
}
