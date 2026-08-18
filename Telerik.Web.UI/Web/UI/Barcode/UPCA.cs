using System;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000A05 RID: 2565
	internal class UPCA : EAN13
	{
		// Token: 0x06006149 RID: 24905 RVA: 0x0016E440 File Offset: 0x0016C640
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			value += base.GetChecksum(value);
			this.SetTextboxValues(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = base.Parity['0'];
			string text2 = value.Substring(0, 6);
			for (int i = 0; i < text2.Length; i++)
			{
				string key = text[i].ToString() + text2[i].ToString();
				stringBuilder.Append(base.Encoding[key]);
			}
			string text3 = value.Substring(6, 6);
			for (int j = 0; j < text3.Length; j++)
			{
				string key2 = EAN13.Right + text3[j];
				stringBuilder.Append(base.Encoding[key2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0016E53C File Offset: 0x0016C73C
		protected override string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 12;
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length >= num)
			{
				return new StringBuilder(base.GetSymbols(stringBuilder.ToString().Substring(0, num - 1), num)).ToString();
			}
			if (value.Length < num)
			{
				stringBuilder = new StringBuilder(base.GetSymbols(stringBuilder.ToString(), num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0016E5C7 File Offset: 0x0016C7C7
		protected override void SetTextboxValues(string value)
		{
			base.LeadingTextboxText = base.GetHeadText(value);
			base.LeftTextboxText = this.GetLeftText(value);
			base.RightTextboxText = this.GetRightText(value);
			base.EndTextboxText = this.GetTailText(value);
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x0016E5FD File Offset: 0x0016C7FD
		protected string GetTailText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(11, 1);
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x0016E616 File Offset: 0x0016C816
		protected override string GetLeftText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(1, 5);
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x0016E62E File Offset: 0x0016C82E
		protected override string GetRightText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(6, 5);
		}
	}
}
