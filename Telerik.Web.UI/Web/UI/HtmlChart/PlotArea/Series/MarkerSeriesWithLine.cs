using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Series
{
	// Token: 0x020004FD RID: 1277
	public abstract class MarkerSeriesWithLine : MarkersSeries
	{
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00095F13 File Offset: 0x00094113
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("LineAppearance")]
		[Category("Appearance")]
		[Description("Series line visual settings")]
		public ExtendedLineAppearance LineAppearance
		{
			get
			{
				if (this._lineAppearance == null)
				{
					this._lineAppearance = new ExtendedLineAppearance("extendedLineAppearance", base.ViewState);
				}
				return this._lineAppearance;
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00095F3C File Offset: 0x0009413C
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0},", base.Serialize());
			this.SerializeSeriesSpecificProperties(stringBuilder);
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002DB4 RID: 11700
		protected abstract void SerializeLine(StringBuilder sb);

		// Token: 0x04000C3A RID: 3130
		private ExtendedLineAppearance _lineAppearance;
	}
}
