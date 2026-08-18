using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001790 RID: 6032
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class FillStyleMarkedZones : FillStyle
	{
		// Token: 0x1700473E RID: 18238
		// (get) Token: 0x0600EB3A RID: 60218 RVA: 0x003592ED File Offset: 0x003574ED
		// (set) Token: 0x0600EB3B RID: 60219 RVA: 0x00359312 File Offset: 0x00357512
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Marked zone main color")]
		[DefaultValue(typeof(Color), "White")]
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

		// Token: 0x1700473F RID: 18239
		// (get) Token: 0x0600EB3C RID: 60220 RVA: 0x0035931B File Offset: 0x0035751B
		// (set) Token: 0x0600EB3D RID: 60221 RVA: 0x0035933C File Offset: 0x0035753C
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[Description("Specifies which of fill styles (Hatch, Solid, Image, Gradient) should be used")]
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

		// Token: 0x0600EB3E RID: 60222 RVA: 0x00359345 File Offset: 0x00357545
		internal override void Reset()
		{
			base.Reset();
			this.MainColor = Color.White;
			this.FillType = FillType.Solid;
		}
	}
}
