using System;
using System.Globalization;
using System.Text;

namespace Telerik.Web.Apoc.Render.Pdf
{
	// Token: 0x0200169D RID: 5789
	internal sealed class PdfNumber
	{
		// Token: 0x0600DF74 RID: 57204 RVA: 0x0031A2BF File Offset: 0x003184BF
		private PdfNumber()
		{
		}

		// Token: 0x0600DF75 RID: 57205 RVA: 0x0031A2C8 File Offset: 0x003184C8
		internal static string doubleOut(double doubleDown)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (doubleDown < 0.0)
			{
				doubleDown = -doubleDown;
				stringBuilder.Append("-");
			}
			double num = doubleDown % 1.0;
			if (num > 0.95)
			{
				stringBuilder.Append((int)doubleDown + 1);
			}
			else if (num < 0.05)
			{
				stringBuilder.Append((int)doubleDown);
			}
			else
			{
				string text = doubleDown.ToString(CultureInfo.InvariantCulture.NumberFormat);
				int num2 = text.IndexOf(".");
				if (num2 != -1)
				{
					stringBuilder.Append(text.Substring(0, num2));
					if (text.Length - num2 > 6)
					{
						stringBuilder.Append(text.Substring(num2, 6));
					}
					else
					{
						stringBuilder.Append(text.Substring(num2));
					}
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600DF76 RID: 57206 RVA: 0x0031A3A0 File Offset: 0x003185A0
		internal static string doubleOut(double doubleDown, int dec)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (doubleDown < 0.0)
			{
				doubleDown = -doubleDown;
				stringBuilder.Append("-");
			}
			double num = doubleDown % 1.0;
			if (num > 1.0 - 5.0 / Math.Pow(10.0, (double)dec))
			{
				stringBuilder.Append((int)doubleDown + 1);
			}
			else if (num < 5.0 / Math.Pow(10.0, (double)dec))
			{
				stringBuilder.Append((int)doubleDown);
			}
			else
			{
				string text = doubleDown.ToString(CultureInfo.InvariantCulture.NumberFormat);
				int num2 = text.IndexOf(".");
				if (num2 != -1)
				{
					stringBuilder.Append(text.Substring(0, num2));
					if (text.Length - num2 > dec)
					{
						stringBuilder.Append(text.Substring(num2, dec));
					}
					else
					{
						stringBuilder.Append(text.Substring(num2));
					}
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
