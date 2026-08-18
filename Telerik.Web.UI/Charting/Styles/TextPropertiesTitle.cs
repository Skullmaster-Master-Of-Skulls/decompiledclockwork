using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F4 RID: 6132
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class TextPropertiesTitle : TextProperties
	{
		// Token: 0x17004837 RID: 18487
		// (get) Token: 0x0600EE9A RID: 61082 RVA: 0x00365385 File Offset: 0x00363585
		// (set) Token: 0x0600EE9B RID: 61083 RVA: 0x003653A5 File Offset: 0x003635A5
		[SkinnableProperty]
		[DefaultValue(typeof(Font), "Verdana, 15pt")]
		[TypeConverter(typeof(FontConverter))]
		[NotifyParentProperty(true)]
		public override Font Font
		{
			get
			{
				return (Font)(base.ViewState["Font"] ?? DefaultValues.VERDANA15);
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x0600EE9C RID: 61084 RVA: 0x003653AE File Offset: 0x003635AE
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_TEXT_COLOR;
			this.Font = DefaultValues.VERDANA15;
		}
	}
}
