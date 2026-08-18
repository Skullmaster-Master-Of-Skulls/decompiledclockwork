using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F5 RID: 6133
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class TextPropertiesError : TextProperties
	{
		// Token: 0x17004838 RID: 18488
		// (get) Token: 0x0600EE9E RID: 61086 RVA: 0x003653D4 File Offset: 0x003635D4
		// (set) Token: 0x0600EE9F RID: 61087 RVA: 0x003653F9 File Offset: 0x003635F9
		[Description("Gets or sets the text color")]
		[DefaultValue(typeof(Color), "Red")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ColorConverter))]
		[SkinnableProperty]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_ERROR_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x17004839 RID: 18489
		// (get) Token: 0x0600EEA0 RID: 61088 RVA: 0x00365402 File Offset: 0x00363602
		// (set) Token: 0x0600EEA1 RID: 61089 RVA: 0x00365422 File Offset: 0x00363622
		[TypeConverter(typeof(FontConverter))]
		[DefaultValue(typeof(Font), "Verdana, 10pt, style=Bold")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public override Font Font
		{
			get
			{
				return (Font)(base.ViewState["Font"] ?? DefaultValues.VERDANA10_BOLD);
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x0600EEA2 RID: 61090 RVA: 0x0036542B File Offset: 0x0036362B
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_ERROR_COLOR;
			this.Font = DefaultValues.VERDANA10_BOLD;
		}
	}
}
