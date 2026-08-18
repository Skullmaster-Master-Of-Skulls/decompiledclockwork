using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DB RID: 987
	public class SeriesLabelsAppearanceBase : LabelsAppearanceBase
	{
		// Token: 0x06002430 RID: 9264 RVA: 0x0007855F File Offset: 0x0007675F
		public SeriesLabelsAppearanceBase(string prefix, StateBag OwnerStateBag) : base("sla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x00078573 File Offset: 0x00076773
		// (set) Token: 0x06002432 RID: 9266 RVA: 0x00078593 File Offset: 0x00076793
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		public string DataField
		{
			get
			{
				return (string)(base.ViewState["DataField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000785A8 File Offset: 0x000767A8
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			if (this.Visible == true && !string.IsNullOrEmpty(this.DataField) && string.IsNullOrEmpty(base.ClientTemplate) && string.IsNullOrEmpty(base.DataFormatString))
			{
				string arg = string.Format("#= dataItem.{0} #", this.DataField);
				stringBuilder.Insert(stringBuilder.Length - 1, string.Format(",template: '{0}'", arg));
			}
			return stringBuilder.ToString();
		}
	}
}
