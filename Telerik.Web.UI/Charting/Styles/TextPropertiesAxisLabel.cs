using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F7 RID: 6135
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class TextPropertiesAxisLabel : TextProperties
	{
		// Token: 0x1700483B RID: 18491
		// (get) Token: 0x0600EEA8 RID: 61096 RVA: 0x0036549A File Offset: 0x0036369A
		// (set) Token: 0x0600EEA9 RID: 61097 RVA: 0x003654BF File Offset: 0x003636BF
		[DefaultValue(typeof(Color), "160, 160, 160")]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the text color")]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_AXIS_TEXT_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EEAA RID: 61098 RVA: 0x003654C8 File Offset: 0x003636C8
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_AXIS_TEXT_COLOR;
		}
	}
}
