using System;
using System.Collections.Generic;

namespace ReportFunctions
{
	// Token: 0x02000040 RID: 64
	public class ReportGroup
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00044524 File Offset: 0x00043524
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0004453B File Offset: 0x0004353B
		public string ParentGroupTitle { get; set; }

		// Token: 0x060003BE RID: 958 RVA: 0x00044544 File Offset: 0x00043544
		public ReportGroup()
		{
			this.title = "";
			this.iconName = "";
			this.reports = new List<Report>();
			this.Description = "";
			this.IsTechnoProGroup = false;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00044598 File Offset: 0x00043598
		public ReportGroup(int id, string title, string iconName, int orderNum)
		{
			this.title = title;
			this.iconName = iconName;
			this.id = id;
			this.orderNum = orderNum;
			this.reports = new List<Report>();
			this.Description = "";
			this.IsTechnoProGroup = false;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000445F4 File Offset: 0x000435F4
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0004460C File Offset: 0x0004360C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00044618 File Offset: 0x00043618
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00044630 File Offset: 0x00043630
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0004463C File Offset: 0x0004363C
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00044654 File Offset: 0x00043654
		public string IconName
		{
			get
			{
				return this.iconName;
			}
			set
			{
				this.iconName = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00044660 File Offset: 0x00043660
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00044678 File Offset: 0x00043678
		public List<Report> Reports
		{
			get
			{
				return this.reports;
			}
			set
			{
				this.reports = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00044684 File Offset: 0x00043684
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0004469C File Offset: 0x0004369C
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000446A8 File Offset: 0x000436A8
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000446BF File Offset: 0x000436BF
		public bool IsTechnoProGroup { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000446C8 File Offset: 0x000436C8
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000446DF File Offset: 0x000436DF
		public string Description { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000446E8 File Offset: 0x000436E8
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000446FF File Offset: 0x000436FF
		public int ParentGroupId { get; set; }

		// Token: 0x040001E3 RID: 483
		private string title;

		// Token: 0x040001E4 RID: 484
		private string iconName;

		// Token: 0x040001E5 RID: 485
		private int id;

		// Token: 0x040001E6 RID: 486
		private int orderNum = 0;

		// Token: 0x040001E7 RID: 487
		private List<Report> reports;
	}
}
