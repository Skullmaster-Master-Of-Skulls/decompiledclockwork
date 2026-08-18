using System;

namespace System.Web.Helpers
{
	// Token: 0x0200001C RID: 28
	internal sealed class SortInfo : IEquatable<SortInfo>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000618F File Offset: 0x0000438F
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006197 File Offset: 0x00004397
		public string SortColumn { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000061A0 File Offset: 0x000043A0
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000061A8 File Offset: 0x000043A8
		public SortDirection SortDirection { get; set; }

		// Token: 0x06000135 RID: 309 RVA: 0x000061B1 File Offset: 0x000043B1
		public bool Equals(SortInfo other)
		{
			return other != null && string.Equals(this.SortColumn, other.SortColumn, StringComparison.OrdinalIgnoreCase) && this.SortDirection == other.SortDirection;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000061DC File Offset: 0x000043DC
		public override bool Equals(object obj)
		{
			SortInfo sortInfo = obj as SortInfo;
			if (sortInfo != null)
			{
				return this.Equals(sortInfo);
			}
			return base.Equals(obj);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006202 File Offset: 0x00004402
		public override int GetHashCode()
		{
			return this.SortColumn.GetHashCode();
		}
	}
}
