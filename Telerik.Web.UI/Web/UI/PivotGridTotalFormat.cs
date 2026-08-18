using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000751 RID: 1873
	[Serializable]
	public class PivotGridTotalFormat
	{
		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x000D032A File Offset: 0x000CE52A
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x000D0334 File Offset: 0x000CE534
		[TypeConverter(typeof(StringToObjectConverter))]
		public object GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				ObjectWrapper objectWrapper = value as ObjectWrapper;
				if (objectWrapper != null)
				{
					this.groupName = objectWrapper.Value;
					return;
				}
				this.groupName = value;
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x000D035F File Offset: 0x000CE55F
		// (set) Token: 0x06004254 RID: 16980 RVA: 0x000D0367 File Offset: 0x000CE567
		public int Level { get; set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x000D0370 File Offset: 0x000CE570
		// (set) Token: 0x06004256 RID: 16982 RVA: 0x000D0378 File Offset: 0x000CE578
		public PivotGridAxis Axis { get; set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x000D0381 File Offset: 0x000CE581
		// (set) Token: 0x06004258 RID: 16984 RVA: 0x000D0389 File Offset: 0x000CE589
		public PivotGridTotalFunction TotalFunction { get; set; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x000D0392 File Offset: 0x000CE592
		// (set) Token: 0x0600425A RID: 16986 RVA: 0x000D039A File Offset: 0x000CE59A
		public PivotGridSortOrder SortOrder { get; set; }

		// Token: 0x04001194 RID: 4500
		private object groupName;
	}
}
