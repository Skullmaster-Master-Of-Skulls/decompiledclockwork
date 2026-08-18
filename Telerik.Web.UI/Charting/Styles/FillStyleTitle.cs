using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178F RID: 6031
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FillStyleTitle : FillStyle
	{
		// Token: 0x1700473C RID: 18236
		// (get) Token: 0x0600EB34 RID: 60212 RVA: 0x00359273 File Offset: 0x00357473
		// (set) Token: 0x0600EB35 RID: 60213 RVA: 0x00359298 File Offset: 0x00357498
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "White")]
		[Description("Chart title main color")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ColorConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		public override Color MainColor
		{
			get
			{
				return (Color)(base.ViewState["MainColor"] ?? Color.White);
			}
			set
			{
				base.MainColor = value;
			}
		}

		// Token: 0x1700473D RID: 18237
		// (get) Token: 0x0600EB36 RID: 60214 RVA: 0x003592A1 File Offset: 0x003574A1
		// (set) Token: 0x0600EB37 RID: 60215 RVA: 0x003592C2 File Offset: 0x003574C2
		[Description("Specifies which of fill styles (Hatch, Solid, Image, Gradient) should be used")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[DefaultValue(typeof(FillType), "Solid")]
		public override FillType FillType
		{
			get
			{
				return (FillType)(base.ViewState["FillType"] ?? FillType.Solid);
			}
			set
			{
				base.FillType = value;
			}
		}

		// Token: 0x0600EB38 RID: 60216 RVA: 0x003592CB File Offset: 0x003574CB
		internal override void Reset()
		{
			base.Reset();
			this.MainColor = Color.White;
			this.FillType = FillType.Solid;
		}
	}
}
