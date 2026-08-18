using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200015A RID: 346
	[ClientCssResource("PieChart")]
	[ToolboxBitmap(typeof(Accessor), "PieChart.bmp")]
	[ClientScriptResource("Sys.Extended.UI.PieChart", "PieChart")]
	public class PieChart : ChartBase
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00017F19 File Offset: 0x00016119
		[ExtenderControlProperty(true, true)]
		[Browsable(false)]
		[ClientPropertyName("pieChartClientValues")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<PieChartValue> PieChartClientValues
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00017F21 File Offset: 0x00016121
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Editor(typeof(ChartBaseSeriesEditor<PieChartValue>), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<PieChartValue> PieChartValues
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00017F2C File Offset: 0x0001612C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.IsDesignMode)
			{
				return;
			}
			foreach (PieChartValue pieChartValue in this.PieChartValues)
			{
				if (string.IsNullOrWhiteSpace(pieChartValue.Category))
				{
					throw new Exception("Category is missing in the PieChartValue. Please provide a Category in the PieChartValue.");
				}
			}
		}

		// Token: 0x0400039C RID: 924
		private List<PieChartValue> _values = new List<PieChartValue>();
	}
}
