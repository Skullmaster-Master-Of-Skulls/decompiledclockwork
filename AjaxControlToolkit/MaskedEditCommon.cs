using System;
using System.Globalization;
using System.Text;

namespace AjaxControlToolkit
{
	// Token: 0x02000137 RID: 311
	public static class MaskedEditCommon
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x000149BC File Offset: 0x00012BBC
		public static int GetFirstMaskPosition(string text)
		{
			bool flag = false;
			text = MaskedEditCommon.ConvertMask(text);
			for (int i = 0; i < text.Length; i++)
			{
				if (text.Substring(i, 1) == "\\" && !flag)
				{
					flag = true;
				}
				else
				{
					if ("9L$CAN?".IndexOf(text.Substring(i, 1), StringComparison.Ordinal) != -1 && !flag)
					{
						return i;
					}
					if (flag)
					{
						flag = false;
					}
				}
			}
			return -1;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00014A20 File Offset: 0x00012C20
		public static int GetLastMaskPosition(string text)
		{
			bool flag = false;
			text = MaskedEditCommon.ConvertMask(text);
			int result = -1;
			for (int i = 0; i < text.Length; i++)
			{
				if (text.Substring(i, 1) == "\\" && !flag)
				{
					flag = true;
				}
				else if ("9L$CAN?".IndexOf(text.Substring(i, 1), StringComparison.Ordinal) != -1 && !flag)
				{
					result = i;
				}
				else if (flag)
				{
					flag = false;
				}
			}
			return result;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00014A88 File Offset: 0x00012C88
		public static string GetValidMask(string text)
		{
			int firstMaskPosition = MaskedEditCommon.GetFirstMaskPosition(text);
			int lastMaskPosition = MaskedEditCommon.GetLastMaskPosition(text);
			text = MaskedEditCommon.ConvertMask(text);
			return text.Substring(firstMaskPosition, lastMaskPosition - firstMaskPosition + 1);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00014AB8 File Offset: 0x00012CB8
		public static string ConvertMask(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			string value = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				if ("9L$CAN?".IndexOf(text.Substring(i, 1), StringComparison.Ordinal) != -1)
				{
					if (stringBuilder2.Length == 0)
					{
						stringBuilder.Append(text.Substring(i, 1));
						stringBuilder2.Length = 0;
						value = text.Substring(i, 1);
					}
					else if (text.Substring(i, 1) == "9")
					{
						stringBuilder2.Append("9");
					}
					else if (text.Substring(i, 1) == "0")
					{
						stringBuilder2.Append("0");
					}
				}
				else if ("9L$CAN?".IndexOf(text.Substring(i, 1), StringComparison.Ordinal) == -1 && text.Substring(i, 1) != "{" && text.Substring(i, 1) != "}")
				{
					if (stringBuilder2.Length == 0)
					{
						stringBuilder.Append(text.Substring(i, 1));
						stringBuilder2.Length = 0;
						value = string.Empty;
					}
					else if ("0123456789".IndexOf(text.Substring(i, 1), StringComparison.Ordinal) != -1)
					{
						stringBuilder2.Append(text.Substring(i, 1));
					}
				}
				else if (text.Substring(i, 1) == "{" && stringBuilder2.Length == 0)
				{
					stringBuilder2.Length = 0;
					stringBuilder2.Append("0");
				}
				else if (text.Substring(i, 1) == "}" && stringBuilder2.Length != 0)
				{
					int num = int.Parse(stringBuilder2.ToString(), CultureInfo.InvariantCulture) - 1;
					if (num > 0)
					{
						for (int j = 0; j < num; j++)
						{
							stringBuilder.Append(value);
						}
					}
					stringBuilder2.Length = 0;
					stringBuilder2.Append("0");
					value = string.Empty;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400032E RID: 814
		private const string _charEscape = "\\";

		// Token: 0x0400032F RID: 815
		private const string _charsEditMask = "9L$CAN?";

		// Token: 0x04000330 RID: 816
		private const string _charNumbers = "0123456789";
	}
}
