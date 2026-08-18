using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001789 RID: 6025
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class FillSettingsVerticalGradient : FillSettings
	{
		// Token: 0x1700472C RID: 18220
		// (get) Token: 0x0600EAFB RID: 60155 RVA: 0x00358A7C File Offset: 0x00356C7C
		// (set) Token: 0x0600EAFC RID: 60156 RVA: 0x00358A9D File Offset: 0x00356C9D
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[DefaultValue(typeof(GradientFillStyle), "Vertical")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public override GradientFillStyle GradientMode
		{
			get
			{
				return (GradientFillStyle)(base.ViewState["GradientMode"] ?? GradientFillStyle.Vertical);
			}
			set
			{
				base.GradientMode = value;
			}
		}

		// Token: 0x0600EAFD RID: 60157 RVA: 0x00358AA6 File Offset: 0x00356CA6
		internal override void Reset()
		{
			base.Reset();
			this.GradientMode = GradientFillStyle.Vertical;
		}
	}
}
