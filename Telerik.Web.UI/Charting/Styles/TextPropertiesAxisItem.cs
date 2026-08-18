using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F6 RID: 6134
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class TextPropertiesAxisItem : TextProperties
	{
		// Token: 0x1700483A RID: 18490
		// (get) Token: 0x0600EEA4 RID: 61092 RVA: 0x00365451 File Offset: 0x00363651
		// (set) Token: 0x0600EEA5 RID: 61093 RVA: 0x00365476 File Offset: 0x00363676
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "160, 160, 160")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the text color")]
		[TypeConverter(typeof(ColorConverter))]
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

		// Token: 0x0600EEA6 RID: 61094 RVA: 0x0036547F File Offset: 0x0036367F
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_AXIS_TEXT_COLOR;
		}
	}
}
