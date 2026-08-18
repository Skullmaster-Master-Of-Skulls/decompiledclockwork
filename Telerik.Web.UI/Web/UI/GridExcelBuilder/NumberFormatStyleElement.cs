using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B1E RID: 6942
	public class NumberFormatStyleElement : ElementBase
	{
		// Token: 0x170051D1 RID: 20945
		// (get) Token: 0x06010CB7 RID: 68791 RVA: 0x003BA424 File Offset: 0x003B8624
		// (set) Token: 0x06010CB8 RID: 68792 RVA: 0x003BA42C File Offset: 0x003B862C
		public NumberFormatType FormatType
		{
			get
			{
				return this._formatType;
			}
			set
			{
				this._formatType = value;
			}
		}

		// Token: 0x170051D2 RID: 20946
		// (get) Token: 0x06010CB9 RID: 68793 RVA: 0x003BA435 File Offset: 0x003B8635
		protected override string StartTag
		{
			get
			{
				return "<NumberFormat{0}>";
			}
		}

		// Token: 0x170051D3 RID: 20947
		// (get) Token: 0x06010CBA RID: 68794 RVA: 0x003BA43C File Offset: 0x003B863C
		protected override string EndTag
		{
			get
			{
				return "</NumberFormat>";
			}
		}

		// Token: 0x06010CBB RID: 68795 RVA: 0x003BA444 File Offset: 0x003B8644
		protected virtual string ConvertNumberFormatTypesToString(NumberFormatType formatType)
		{
			switch (formatType)
			{
			case NumberFormatType.GeneralNumber:
				return "General Number";
			case NumberFormatType.GeneralDate:
				return "General Date";
			case NumberFormatType.LongDate:
				return "Long Date";
			case NumberFormatType.MediumDate:
				return "Medium Date";
			case NumberFormatType.ShortDate:
				return "Short Date";
			case NumberFormatType.LongTime:
				return "Long Time";
			case NumberFormatType.MediumTime:
				return "Medium Time";
			case NumberFormatType.ShortTime:
				return "Short Time";
			case NumberFormatType.EuroCurrency:
				return "Euro Currency";
			case NumberFormatType.YesNo:
				return "Yes/No";
			case NumberFormatType.TrueFalse:
				return "True/False";
			case NumberFormatType.OnOff:
				return "On/Off";
			}
			return formatType.ToString();
		}

		// Token: 0x06010CBC RID: 68796 RVA: 0x003BA52C File Offset: 0x003B872C
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this._formatType != NumberFormatType.General)
			{
				base.Attributes.Add("ss:Format", this.ConvertNumberFormatTypesToString(this.FormatType));
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004B22 RID: 19234
		private NumberFormatType _formatType;
	}
}
