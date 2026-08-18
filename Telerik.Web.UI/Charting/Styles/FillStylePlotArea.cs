using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178E RID: 6030
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class FillStylePlotArea : FillStyle
	{
		// Token: 0x0600EB2B RID: 60203 RVA: 0x00359188 File Offset: 0x00357388
		public FillStylePlotArea()
		{
			this.fillStyleFillSettings = new FillSettingsVerticalGradient();
		}

		// Token: 0x17004739 RID: 18233
		// (get) Token: 0x0600EB2C RID: 60204 RVA: 0x0035919B File Offset: 0x0035739B
		// (set) Token: 0x0600EB2D RID: 60205 RVA: 0x003591C0 File Offset: 0x003573C0
		[DefaultValue(typeof(Color), "92, 92, 92")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Chart plot area main color")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ColorConverter))]
		public override Color MainColor
		{
			get
			{
				return (Color)(base.ViewState["MainColor"] ?? FillStylePlotArea.defMainColor);
			}
			set
			{
				base.MainColor = value;
			}
		}

		// Token: 0x1700473A RID: 18234
		// (get) Token: 0x0600EB2E RID: 60206 RVA: 0x003591C9 File Offset: 0x003573C9
		// (set) Token: 0x0600EB2F RID: 60207 RVA: 0x003591EE File Offset: 0x003573EE
		[Description("Chart plot area main color")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(typeof(Color), "67, 67, 67")]
		[TypeConverter(typeof(ColorConverter))]
		public override Color SecondColor
		{
			get
			{
				return (Color)(base.ViewState["SecondColor"] ?? FillStylePlotArea.defSecondColor);
			}
			set
			{
				base.SecondColor = value;
			}
		}

		// Token: 0x1700473B RID: 18235
		// (get) Token: 0x0600EB30 RID: 60208 RVA: 0x003591F7 File Offset: 0x003573F7
		// (set) Token: 0x0600EB31 RID: 60209 RVA: 0x00359218 File Offset: 0x00357418
		[Description("Specifies which of fill styles (Hatch, Solid, Image, Gradient) should be used")]
		[SkinnableProperty]
		[DefaultValue(typeof(FillType), "Gradient")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public override FillType FillType
		{
			get
			{
				return (FillType)(base.ViewState["FillType"] ?? FillType.Gradient);
			}
			set
			{
				base.FillType = value;
			}
		}

		// Token: 0x0600EB32 RID: 60210 RVA: 0x00359221 File Offset: 0x00357421
		internal override void Reset()
		{
			base.Reset();
			this.MainColor = FillStylePlotArea.defMainColor;
			this.SecondColor = FillStylePlotArea.defSecondColor;
			this.FillType = FillType.Gradient;
			this.fillStyleFillSettings = new FillSettingsVerticalGradient();
		}

		// Token: 0x040043F1 RID: 17393
		internal static Color defMainColor = Color.FromArgb(92, 92, 92);

		// Token: 0x040043F2 RID: 17394
		internal static Color defSecondColor = Color.FromArgb(67, 67, 67);
	}
}
