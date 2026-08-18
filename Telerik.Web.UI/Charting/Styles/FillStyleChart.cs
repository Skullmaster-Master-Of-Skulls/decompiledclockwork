using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178D RID: 6029
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class FillStyleChart : FillStyle
	{
		// Token: 0x0600EB25 RID: 60197 RVA: 0x003590E6 File Offset: 0x003572E6
		public FillStyleChart()
		{
			base.MainColor = Color.White;
			base.FillType = FillType.Solid;
			this.fillStyleFillSettings = new FillSettingsVerticalGradient();
		}

		// Token: 0x17004737 RID: 18231
		// (get) Token: 0x0600EB26 RID: 60198 RVA: 0x0035910B File Offset: 0x0035730B
		// (set) Token: 0x0600EB27 RID: 60199 RVA: 0x0035912C File Offset: 0x0035732C
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

		// Token: 0x17004738 RID: 18232
		// (get) Token: 0x0600EB28 RID: 60200 RVA: 0x00359135 File Offset: 0x00357335
		// (set) Token: 0x0600EB29 RID: 60201 RVA: 0x0035915A File Offset: 0x0035735A
		[DefaultValue(typeof(Color), "White")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Chart background main color")]
		[TypeConverter(typeof(ColorConverter))]
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

		// Token: 0x0600EB2A RID: 60202 RVA: 0x00359163 File Offset: 0x00357363
		internal override void Reset()
		{
			base.Reset();
			this.MainColor = Color.White;
			this.FillType = FillType.Solid;
			this.fillStyleFillSettings = new FillSettingsVerticalGradient();
		}
	}
}
