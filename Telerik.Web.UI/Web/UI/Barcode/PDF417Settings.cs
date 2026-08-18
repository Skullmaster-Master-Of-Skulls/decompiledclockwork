using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Barcode.PDF417ClassLibrary;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020000A1 RID: 161
	public class PDF417Settings : ObjectWithState
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00010E0C File Offset: 0x0000F00C
		public PDF417Settings(StateBag ownerStateBag) : base("pdf417_", ownerStateBag)
		{
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00010E1A File Offset: 0x0000F01A
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00010E45 File Offset: 0x0000F045
		[DefaultValue(EncodingMode.Byte)]
		[Category("Behavior")]
		[Description("There are four values available for this property - Auto, Byte, Numeric, Text")]
		public EncodingMode EncodingMode
		{
			get
			{
				if (base.ViewState["EncodingMode"] != null)
				{
					return (EncodingMode)base.ViewState["EncodingMode"];
				}
				return EncodingMode.Byte;
			}
			set
			{
				base.ViewState["EncodingMode"] = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00010E5D File Offset: 0x0000F05D
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00010E88 File Offset: 0x0000F088
		[DefaultValue(0)]
		[Category("Behavior")]
		[Description("")]
		public int ErrorCorrectionLevel
		{
			get
			{
				if (base.ViewState["ErrorCorrectionLevel"] != null)
				{
					return (int)base.ViewState["ErrorCorrectionLevel"];
				}
				return 0;
			}
			set
			{
				base.ViewState["ErrorCorrectionLevel"] = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00010ECB File Offset: 0x0000F0CB
		[DefaultValue(3)]
		[Category("Behavior")]
		[Description("")]
		public int AspectRatio
		{
			get
			{
				if (base.ViewState["AspectRatio"] != null)
				{
					return (int)base.ViewState["AspectRatio"];
				}
				return 3;
			}
			set
			{
				if (value < 1)
				{
					base.ViewState["AspectRatio"] = 1;
					return;
				}
				base.ViewState["AspectRatio"] = value;
			}
		}
	}
}
