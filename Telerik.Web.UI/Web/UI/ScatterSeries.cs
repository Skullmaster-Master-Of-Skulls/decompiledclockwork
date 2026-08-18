using System;
using System.ComponentModel;
using System.Text;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x02000B93 RID: 2963
	public class ScatterSeries : ScatterSeriesBase
	{
		// Token: 0x06006FE7 RID: 28647 RVA: 0x001A2736 File Offset: 0x001A0936
		public ScatterSeries()
		{
			this.sType = SeriesType.Scatter;
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x001A2745 File Offset: 0x001A0945
		// (set) Token: 0x06006FE9 RID: 28649 RVA: 0x001A2766 File Offset: 0x001A0966
		[Browsable(false)]
		[DefaultValue(MissingValuesBehavior.Gap)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override MissingValuesBehavior MissingValues
		{
			get
			{
				return (MissingValuesBehavior)(base.ViewState["MissingValues"] ?? MissingValuesBehavior.Gap);
			}
			set
			{
				base.ViewState["MissingValues"] = value;
			}
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x001A277E File Offset: 0x001A097E
		internal override string Serialize()
		{
			return string.Format("{{{0}}}", base.Serialize());
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x001A2790 File Offset: 0x001A0990
		protected internal override void SerializeMissingValues(StringBuilder sb)
		{
		}
	}
}
