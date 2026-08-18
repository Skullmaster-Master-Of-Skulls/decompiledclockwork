using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000446 RID: 1094
	public class Extent : StateManager, IDefaultCheck
	{
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0007FF1E File Offset: 0x0007E11E
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x0007FF47 File Offset: 0x0007E147
		[DefaultValue(0.0)]
		public double NorthWestLatitude
		{
			get
			{
				return (double)(base.ViewState["NorthWestLatitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["NorthWestLatitude"] = value;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x0007FF5F File Offset: 0x0007E15F
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x0007FF88 File Offset: 0x0007E188
		[DefaultValue(0.0)]
		public double NorthWestLongitude
		{
			get
			{
				return (double)(base.ViewState["NorthWestLongitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["NorthWestLongitude"] = value;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0007FFA0 File Offset: 0x0007E1A0
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x0007FFC9 File Offset: 0x0007E1C9
		[DefaultValue(0.0)]
		public double SouthEastLatitude
		{
			get
			{
				return (double)(base.ViewState["SouthEastLatitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["SouthEastLatitude"] = value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x0007FFE1 File Offset: 0x0007E1E1
		// (set) Token: 0x06002765 RID: 10085 RVA: 0x0008000A File Offset: 0x0007E20A
		[DefaultValue(0.0)]
		public double SouthEastLongitude
		{
			get
			{
				return (double)(base.ViewState["SouthEastLongitude"] ?? 0.0);
			}
			set
			{
				base.ViewState["SouthEastLongitude"] = value;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x00080024 File Offset: 0x0007E224
		public bool IsDefault
		{
			get
			{
				return this.NorthWestLatitude == 0.0 && this.NorthWestLongitude == 0.0 && this.SouthEastLatitude == 0.0 && this.SouthEastLongitude == 0.0;
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00080078 File Offset: 0x0007E278
		internal double[] ToArray()
		{
			return new double[]
			{
				this.NorthWestLatitude,
				this.NorthWestLongitude,
				this.SouthEastLatitude,
				this.SouthEastLongitude
			};
		}
	}
}
