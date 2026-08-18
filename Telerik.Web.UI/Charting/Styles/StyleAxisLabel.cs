using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DB RID: 6107
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleAxisLabel : StyleLabelHidden
	{
		// Token: 0x0600EDA7 RID: 60839 RVA: 0x00362CFE File Offset: 0x00360EFE
		public StyleAxisLabel() : base(new PositionCenter())
		{
		}

		// Token: 0x0600EDA8 RID: 60840 RVA: 0x00362D0B File Offset: 0x00360F0B
		internal override void Reset()
		{
			base.Reset();
			this.position = new PositionCenter();
		}
	}
}
