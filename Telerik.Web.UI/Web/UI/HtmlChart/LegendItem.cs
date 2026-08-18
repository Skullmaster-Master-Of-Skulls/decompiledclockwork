using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003D7 RID: 983
	public class LegendItem : ObjectWithState
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x00077E3E File Offset: 0x0007603E
		public LegendItem(StateBag OwnerStateBag) : base("li", OwnerStateBag)
		{
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x00077E4C File Offset: 0x0007604C
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x00077E6C File Offset: 0x0007606C
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00077E80 File Offset: 0x00076080
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Visual != string.Empty)
			{
				stringBuilder.Append(",item:{").AppendFormat("visual:{0}", this.Visual).Append("}");
			}
			return stringBuilder.ToString();
		}
	}
}
