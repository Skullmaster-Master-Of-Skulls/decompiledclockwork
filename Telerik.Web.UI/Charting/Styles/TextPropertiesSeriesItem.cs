using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F8 RID: 6136
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class TextPropertiesSeriesItem : TextProperties
	{
		// Token: 0x1700483C RID: 18492
		// (get) Token: 0x0600EEAC RID: 61100 RVA: 0x003654E3 File Offset: 0x003636E3
		// (set) Token: 0x0600EEAD RID: 61101 RVA: 0x00365508 File Offset: 0x00363708
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "153, 153, 153")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the text color")]
		[TypeConverter(typeof(ColorConverter))]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_SERIESTEXT_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EEAE RID: 61102 RVA: 0x00365511 File Offset: 0x00363711
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_SERIESTEXT_COLOR;
		}
	}
}
