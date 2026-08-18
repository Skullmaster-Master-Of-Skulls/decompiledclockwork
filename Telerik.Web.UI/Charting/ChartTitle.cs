using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001743 RID: 5955
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class ChartTitle : ChartLabel
	{
		// Token: 0x0600E887 RID: 59527 RVA: 0x00342F6F File Offset: 0x0034116F
		public ChartTitle() : this(null, null)
		{
		}

		// Token: 0x0600E888 RID: 59528 RVA: 0x00342F79 File Offset: 0x00341179
		public ChartTitle(Chart parent) : this(parent, null)
		{
		}

		// Token: 0x0600E889 RID: 59529 RVA: 0x00342F83 File Offset: 0x00341183
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ChartTitle(Chart parent, IContainer container) : base(parent, container, new StyleLabelTitle(), new TextBlockTitle(), null)
		{
			this.chartBaseLabelMarker.Appearance.styleChart = parent;
			this.Appearance.Chart = parent;
		}
	}
}
