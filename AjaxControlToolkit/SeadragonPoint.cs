using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x02000188 RID: 392
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SeadragonPoint
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
		public SeadragonPoint()
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public SeadragonPoint(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0001C6BE File Offset: 0x0001A8BE
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0001C6C6 File Offset: 0x0001A8C6
		public float X { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001C6CF File Offset: 0x0001A8CF
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0001C6D7 File Offset: 0x0001A8D7
		public float Y { get; set; }
	}
}
