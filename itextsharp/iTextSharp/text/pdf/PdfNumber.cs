using System;
using System.Globalization;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200027F RID: 639
	public class PdfNumber : PdfObject
	{
		// Token: 0x06001838 RID: 6200 RVA: 0x0008C604 File Offset: 0x0008B604
		public PdfNumber(string content) : base(2)
		{
			try
			{
				this.value = double.Parse(content.Trim(), NumberFormatInfo.InvariantInfo);
				base.Content = content;
			}
			catch (Exception ex)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("1.is.not.a.valid.number.2", content, ex.ToString()));
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0008C660 File Offset: 0x0008B660
		public PdfNumber(int value) : base(2)
		{
			this.value = (double)value;
			base.Content = value.ToString();
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0008C67E File Offset: 0x0008B67E
		public PdfNumber(double value) : base(2)
		{
			this.value = value;
			base.Content = ByteBuffer.FormatDouble(value);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0008C69A File Offset: 0x0008B69A
		public PdfNumber(float value) : this((double)value)
		{
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0008C6A4 File Offset: 0x0008B6A4
		public int IntValue
		{
			get
			{
				return (int)this.value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0008C6AD File Offset: 0x0008B6AD
		public double DoubleValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0008C6B5 File Offset: 0x0008B6B5
		public float FloatValue
		{
			get
			{
				return (float)this.value;
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0008C6BE File Offset: 0x0008B6BE
		public void Increment()
		{
			this.value += 1.0;
			base.Content = ByteBuffer.FormatDouble(this.value);
		}

		// Token: 0x04001054 RID: 4180
		private double value;
	}
}
