using System;
using System.Data;

namespace ClockWorkAPI
{
	// Token: 0x020000A5 RID: 165
	public class ReportListItem : IComparable
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x00031BF4 File Offset: 0x00030BF4
		public ReportListItem(DataRow dr)
		{
			this.dr = dr;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00031C08 File Offset: 0x00030C08
		public DataRow Dr
		{
			get
			{
				return this.dr;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00031C20 File Offset: 0x00030C20
		public int CompareTo(object obj)
		{
			int result;
			if (obj == null || !(obj is ReportListItem))
			{
				result = -1;
			}
			else
			{
				ReportListItem reportListItem = (ReportListItem)obj;
				result = ((this == reportListItem) ? 0 : -1);
			}
			return result;
		}

		// Token: 0x04000425 RID: 1061
		public static string colNameSubstringIndicator = "_flag";

		// Token: 0x04000426 RID: 1062
		private DataRow dr;
	}
}
