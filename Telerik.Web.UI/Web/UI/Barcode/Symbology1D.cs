using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009C7 RID: 2503
	internal abstract class Symbology1D : SymbologyBase
	{
		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x001249E7 File Offset: 0x00122BE7
		// (set) Token: 0x06006017 RID: 24599 RVA: 0x001249EF File Offset: 0x00122BEF
		public string CheckSum
		{
			get
			{
				return this.checkSum;
			}
			set
			{
				this.checkSum = value;
			}
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x001249F8 File Offset: 0x00122BF8
		public virtual List<RectangleF> GenerateGeometry(string barCodeEncodedText)
		{
			List<RectangleF> list = new List<RectangleF>();
			float num = 1f / float.Parse(barCodeEncodedText.Length.ToString());
			int num2 = 0;
			for (int i = 0; i < barCodeEncodedText.Length; i++)
			{
				if (barCodeEncodedText[i] == '1')
				{
					num2++;
				}
				if (num2 > 0 && (i == barCodeEncodedText.Length - 1 || barCodeEncodedText[i + 1] != '1'))
				{
					list.Add(new RectangleF((float)(i + 1) * num - (float)num2 * num, 0f, num * (float)num2, 1f));
					num2 = 0;
				}
			}
			return list;
		}

		// Token: 0x06006019 RID: 24601
		internal abstract string GetEncoding(string value);

		// Token: 0x04001739 RID: 5945
		public static readonly string GapChar = "0";

		// Token: 0x0400173A RID: 5946
		public static readonly string BarChar = "1";

		// Token: 0x0400173B RID: 5947
		private string checkSum;
	}
}
