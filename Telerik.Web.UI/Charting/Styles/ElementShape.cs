using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001769 RID: 5993
	[Editor(typeof(ElementShapeEditor), typeof(UITypeEditor))]
	[TypeConverter(typeof(ElementShapeConverter))]
	internal abstract class ElementShape : Component
	{
		// Token: 0x0600E9D6 RID: 59862 RVA: 0x00352B74 File Offset: 0x00350D74
		internal GraphicsPath GetElementContour(Rectangle bounds)
		{
			return this.CreateContour(bounds);
		}

		// Token: 0x0600E9D7 RID: 59863
		internal abstract GraphicsPath CreatePath(Rectangle bounds);

		// Token: 0x0600E9D8 RID: 59864 RVA: 0x00352B7D File Offset: 0x00350D7D
		protected virtual GraphicsPath CreateContour(Rectangle bounds)
		{
			return this.CreatePath(bounds);
		}

		// Token: 0x0600E9D9 RID: 59865 RVA: 0x00352B86 File Offset: 0x00350D86
		internal virtual string SerializeProperties()
		{
			return string.Empty;
		}

		// Token: 0x0600E9DA RID: 59866 RVA: 0x00352B8D File Offset: 0x00350D8D
		internal virtual void DeserializeProperties(string propertiesString)
		{
		}

		// Token: 0x0600E9DB RID: 59867 RVA: 0x00352B8F File Offset: 0x00350D8F
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.shape != null)
				{
					this.shape.Dispose();
					this.shape = null;
				}
				if (this.contour != null)
				{
					this.contour.Dispose();
					this.contour = null;
				}
			}
		}

		// Token: 0x04004341 RID: 17217
		private GraphicsPath shape;

		// Token: 0x04004342 RID: 17218
		private GraphicsPath contour;
	}
}
