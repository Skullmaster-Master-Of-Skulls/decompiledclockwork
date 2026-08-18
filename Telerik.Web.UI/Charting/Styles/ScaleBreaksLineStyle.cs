using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A4 RID: 6052
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class ScaleBreaksLineStyle : LineStyle
	{
		// Token: 0x17004761 RID: 18273
		// (get) Token: 0x0600EBC8 RID: 60360 RVA: 0x0035A710 File Offset: 0x00358910
		// (set) Token: 0x0600EBC9 RID: 60361 RVA: 0x0035A735 File Offset: 0x00358935
		[DefaultValue(typeof(Color), "Gray")]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Line color")]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_SCALE_BREAK_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBCA RID: 60362 RVA: 0x0035A73E File Offset: 0x0035893E
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_SCALE_BREAK_COLOR;
		}
	}
}
