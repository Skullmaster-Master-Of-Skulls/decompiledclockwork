using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000273 RID: 627
	public class PdfTextArray
	{
		// Token: 0x0600179F RID: 6047 RVA: 0x0008751F File Offset: 0x0008651F
		public PdfTextArray(string str)
		{
			this.Add(str);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00087544 File Offset: 0x00086544
		public PdfTextArray()
		{
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00087562 File Offset: 0x00086562
		public void Add(PdfNumber number)
		{
			this.Add((float)number.DoubleValue);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00087574 File Offset: 0x00086574
		public void Add(float number)
		{
			if (number != 0f)
			{
				if (!float.IsNaN(this.lastNum))
				{
					this.lastNum += number;
					if (this.lastNum != 0f)
					{
						this.ReplaceLast(this.lastNum);
					}
					else
					{
						this.arrayList.RemoveAt(this.arrayList.Count - 1);
					}
				}
				else
				{
					this.lastNum = number;
					this.arrayList.Add(this.lastNum);
				}
				this.lastStr = null;
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00087604 File Offset: 0x00086604
		public void Add(string str)
		{
			if (str.Length > 0)
			{
				if (this.lastStr != null)
				{
					this.lastStr += str;
					this.ReplaceLast(this.lastStr);
				}
				else
				{
					this.lastStr = str;
					this.arrayList.Add(this.lastStr);
				}
				this.lastNum = float.NaN;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00087665 File Offset: 0x00086665
		internal List<object> ArrayList
		{
			get
			{
				return this.arrayList;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0008766D File Offset: 0x0008666D
		private void ReplaceLast(object obj)
		{
			this.arrayList[this.arrayList.Count - 1] = obj;
		}

		// Token: 0x04001012 RID: 4114
		private List<object> arrayList = new List<object>();

		// Token: 0x04001013 RID: 4115
		private string lastStr;

		// Token: 0x04001014 RID: 4116
		private float lastNum = float.NaN;
	}
}
