using System;

namespace ReportFunctions
{
	// Token: 0x0200000E RID: 14
	public class ReportGroupNode : ReportNode
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00005F19 File Offset: 0x00004F19
		public ReportGroupNode()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005F24 File Offset: 0x00004F24
		public ReportGroupNode(ReportGroup reportGroup, int orderNum)
		{
			this.reportGroup = reportGroup;
			base.OrderNum = orderNum;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005F40 File Offset: 0x00004F40
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00005F58 File Offset: 0x00004F58
		public ReportGroup ReportGroup
		{
			get
			{
				return this.reportGroup;
			}
			set
			{
				this.reportGroup = value;
			}
		}

		// Token: 0x040000DE RID: 222
		private ReportGroup reportGroup;
	}
}
