using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177D RID: 6013
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class DimensionsChart : Dimensions
	{
		// Token: 0x17004713 RID: 18195
		// (get) Token: 0x0600EA95 RID: 60053 RVA: 0x0035741F File Offset: 0x0035561F
		// (set) Token: 0x0600EA96 RID: 60054 RVA: 0x0035743F File Offset: 0x0035563F
		public override Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? DimensionsChart.defHeight);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Height = value;
				}
			}
		}

		// Token: 0x17004714 RID: 18196
		// (get) Token: 0x0600EA97 RID: 60055 RVA: 0x00357455 File Offset: 0x00355655
		// (set) Token: 0x0600EA98 RID: 60056 RVA: 0x00357475 File Offset: 0x00355675
		public override Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? DimensionsChart.defWidth);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Width = value;
				}
			}
		}

		// Token: 0x0600EA99 RID: 60057 RVA: 0x0035748B File Offset: 0x0035568B
		protected override void ResetHeight()
		{
			this.Height = DimensionsChart.defHeight;
		}

		// Token: 0x0600EA9A RID: 60058 RVA: 0x00357498 File Offset: 0x00355698
		protected override void ResetWidth()
		{
			this.Width = DimensionsChart.defWidth;
		}

		// Token: 0x0600EA9B RID: 60059 RVA: 0x003574A5 File Offset: 0x003556A5
		internal override void Reset()
		{
			base.Reset();
			this.Width = DimensionsChart.defWidth;
			this.Height = DimensionsChart.defHeight;
		}

		// Token: 0x040043D0 RID: 17360
		internal static Unit defWidth = Unit.Pixel(400f);

		// Token: 0x040043D1 RID: 17361
		internal static Unit defHeight = Unit.Pixel(300f);
	}
}
