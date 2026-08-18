using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000395 RID: 917
	public sealed class CircleHotSpot : HotSpot
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x0008EDF6 File Offset: 0x0008CFF6
		protected internal override string MarkupName
		{
			get
			{
				return "circle";
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x0008EE00 File Offset: 0x0008D000
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x0008EE29 File Offset: 0x0008D029
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[WebSysDescription("CircleHotSpot_Radius")]
		public int Radius
		{
			get
			{
				object obj = base.ViewState["Radius"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Radius"] = value;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x0008EE50 File Offset: 0x0008D050
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x0008EE79 File Offset: 0x0008D079
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[WebSysDescription("CircleHotSpot_X")]
		public int X
		{
			get
			{
				object obj = base.ViewState["X"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x0008EE94 File Offset: 0x0008D094
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x0008EEBD File Offset: 0x0008D0BD
		[DefaultValue(0)]
		[WebCategory("Appearance")]
		[WebSysDescription("CircleHotSpot_Y")]
		public int Y
		{
			get
			{
				object obj = base.ViewState["Y"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		public override string GetCoordinates()
		{
			return string.Concat(new string[]
			{
				this.X.ToString(),
				",",
				this.Y.ToString(),
				",",
				this.Radius.ToString()
			});
		}
	}
}
