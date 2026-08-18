using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200061B RID: 1563
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PolygonHotSpot : HotSpot
	{
		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x0013AD34 File Offset: 0x00139D34
		// (set) Token: 0x06004D99 RID: 19865 RVA: 0x0013AD61 File Offset: 0x00139D61
		[DefaultValue("")]
		[WebSysDescription("PolygonHotSpot_Coordinates")]
		[WebCategory("Appearance")]
		public string Coordinates
		{
			get
			{
				string text = base.ViewState["Coordinates"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.ViewState["Coordinates"] = value;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06004D9A RID: 19866 RVA: 0x0013AD74 File Offset: 0x00139D74
		protected internal override string MarkupName
		{
			get
			{
				return "poly";
			}
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0013AD7B File Offset: 0x00139D7B
		public override string GetCoordinates()
		{
			return this.Coordinates;
		}
	}
}
