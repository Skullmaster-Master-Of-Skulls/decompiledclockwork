using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001700 RID: 5888
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultProperty("TextBlock")]
	public class BindableLegendItem : LabelItem
	{
		// Token: 0x170045CC RID: 17868
		// (get) Token: 0x0600E4DF RID: 58591 RVA: 0x0032D505 File Offset: 0x0032B705
		// (set) Token: 0x0600E4E0 RID: 58592 RVA: 0x0032D50D File Offset: 0x0032B70D
		[Browsable(false)]
		public object BindableLegendItemSource
		{
			get
			{
				return this.bindableLegendItemSource;
			}
			set
			{
				this.bindableLegendItemSource = value;
			}
		}

		// Token: 0x0600E4E1 RID: 58593 RVA: 0x0032D516 File Offset: 0x0032B716
		internal BindableLegendItem(StyleLabel appearance, object parent) : base(appearance, parent)
		{
		}

		// Token: 0x040041FB RID: 16891
		private object bindableLegendItemSource;
	}
}
