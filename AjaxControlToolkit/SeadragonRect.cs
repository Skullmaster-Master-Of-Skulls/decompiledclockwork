using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x02000189 RID: 393
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SeadragonRect
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public SeadragonRect()
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001C6E8 File Offset: 0x0001A8E8
		public SeadragonRect(float width, float height)
		{
			this.Height = height;
			this.Width = width;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001C6FE File Offset: 0x0001A8FE
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x0001C706 File Offset: 0x0001A906
		public float Height { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001C70F File Offset: 0x0001A90F
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0001C717 File Offset: 0x0001A917
		public float Width { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001C720 File Offset: 0x0001A920
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SeadragonPoint Point
		{
			get
			{
				if (this.point == null)
				{
					this.point = new SeadragonPoint();
				}
				return this.point;
			}
		}

		// Token: 0x04000428 RID: 1064
		private SeadragonPoint point;
	}
}
