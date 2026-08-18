using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.Navigator
{
	// Token: 0x020004EF RID: 1263
	public class SelectionHint : ObjectWithState
	{
		// Token: 0x06002D0A RID: 11530 RVA: 0x000940C4 File Offset: 0x000922C4
		public SelectionHint(StateBag OwnerStateBag) : base("csh", OwnerStateBag)
		{
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000940D2 File Offset: 0x000922D2
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x000940F2 File Offset: 0x000922F2
		[DefaultValue("")]
		public string DataFormatString
		{
			get
			{
				return ((string)base.ViewState["DataFormatString"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x00094105 File Offset: 0x00092305
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x00094125 File Offset: 0x00092325
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string ClientTemplate
		{
			get
			{
				return ((string)base.ViewState["ClientTemplate"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x00094138 File Offset: 0x00092338
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x00094159 File Offset: 0x00092359
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x00094174 File Offset: 0x00092374
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("hint:{");
			if (!string.IsNullOrEmpty(this.DataFormatString))
			{
				stringBuilder.AppendFormat("format:'{0}',", this.DataFormatString);
			}
			if (!string.IsNullOrEmpty(this.ClientTemplate))
			{
				stringBuilder.AppendFormat("template: '{0}',", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(this.ClientTemplate));
			}
			if (!this.Visible)
			{
				stringBuilder.Append("visible:false,");
			}
			HtmlChartHelper.RemoveEndingComma(stringBuilder);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
