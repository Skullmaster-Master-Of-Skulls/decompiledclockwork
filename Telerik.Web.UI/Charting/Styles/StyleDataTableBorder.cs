using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A2 RID: 6050
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleDataTableBorder : StyleBorder
	{
		// Token: 0x1700475F RID: 18271
		// (get) Token: 0x0600EBC0 RID: 60352 RVA: 0x0035A67E File Offset: 0x0035887E
		// (set) Token: 0x0600EBC1 RID: 60353 RVA: 0x0035A6A3 File Offset: 0x003588A3
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "150, 150, 150")]
		[SkinnableProperty]
		[Description("Border color")]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_DATATABLE_BORDER_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBC2 RID: 60354 RVA: 0x0035A6AC File Offset: 0x003588AC
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_DATATABLE_BORDER_COLOR;
		}
	}
}
