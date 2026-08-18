using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200178C RID: 6028
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class FillStyleSeriesPoint : FillStyle
	{
		// Token: 0x0600EB22 RID: 60194 RVA: 0x003590C7 File Offset: 0x003572C7
		public FillStyleSeriesPoint(object containerObject) : this()
		{
			this.fillStyleContainerObject = containerObject;
		}

		// Token: 0x0600EB23 RID: 60195 RVA: 0x003590D6 File Offset: 0x003572D6
		public FillStyleSeriesPoint()
		{
		}

		// Token: 0x0600EB24 RID: 60196 RVA: 0x003590DE File Offset: 0x003572DE
		internal override void Reset()
		{
			base.Reset();
		}
	}
}
