using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001010 RID: 4112
	internal class TimeDataList : DataList
	{
		// Token: 0x17003332 RID: 13106
		// (get) Token: 0x0600A1C6 RID: 41414 RVA: 0x0023F4F1 File Offset: 0x0023D6F1
		// (set) Token: 0x0600A1C7 RID: 41415 RVA: 0x0023F51C File Offset: 0x0023D71C
		public override int RepeatColumns
		{
			get
			{
				if (this.ViewState["RepeatColumns"] == null)
				{
					return 3;
				}
				return (int)this.ViewState["RepeatColumns"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		// Token: 0x17003333 RID: 13107
		// (get) Token: 0x0600A1C8 RID: 41416 RVA: 0x0023F543 File Offset: 0x0023D743
		// (set) Token: 0x0600A1C9 RID: 41417 RVA: 0x0023F56E File Offset: 0x0023D76E
		public override RepeatDirection RepeatDirection
		{
			get
			{
				if (this.ViewState["RepeatDirection"] == null)
				{
					return RepeatDirection.Horizontal;
				}
				return (RepeatDirection)this.ViewState["RepeatDirection"];
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		// Token: 0x17003334 RID: 13108
		// (get) Token: 0x0600A1CA RID: 41418 RVA: 0x0023F599 File Offset: 0x0023D799
		// (set) Token: 0x0600A1CB RID: 41419 RVA: 0x0023F5C4 File Offset: 0x0023D7C4
		public override bool UseAccessibleHeader
		{
			get
			{
				return this.ViewState["UseAccessibleHeader"] == null || (bool)this.ViewState["UseAccessibleHeader"];
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}
	}
}
