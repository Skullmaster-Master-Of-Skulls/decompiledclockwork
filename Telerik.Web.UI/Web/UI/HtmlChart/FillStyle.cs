using System;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B86 RID: 2950
	public class FillStyle : FillStyleBase
	{
		// Token: 0x06006F77 RID: 28535 RVA: 0x001A0891 File Offset: 0x0019EA91
		public FillStyle(string prefix, StateBag OwnerStateBag) : base("fs" + prefix, OwnerStateBag)
		{
			this._serializationMember = "background";
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x001A08B0 File Offset: 0x0019EAB0
		public FillStyle(string prefix, StateBag OwnerStateBag, bool isSeries) : base("sfs" + prefix, OwnerStateBag)
		{
			this._serializationMember = (isSeries ? "color" : "background");
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x001A08DC File Offset: 0x0019EADC
		internal override string Serialize()
		{
			bool flag = this._serializationMember == "color";
			Color backgroundColor = base.BackgroundColor;
			if (backgroundColor == Color.Empty)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(flag ? "" : "{");
			stringBuilder.Append(this._serializationMember).Append(": '").Append(HtmlChartHelper.ColorToHex(backgroundColor)).Append("'");
			if (!flag)
			{
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001E08 RID: 7688
		private readonly string _serializationMember;
	}
}
