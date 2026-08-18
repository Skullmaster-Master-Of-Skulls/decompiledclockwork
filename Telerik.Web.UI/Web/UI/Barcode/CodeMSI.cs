using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D5 RID: 2517
	internal class CodeMSI : Symbology1D
	{
		// Token: 0x0600605A RID: 24666 RVA: 0x00129F70 File Offset: 0x00128170
		public CodeMSI()
		{
			this.charset = new List<char>();
			this.charset.Add('0');
			this.charset.Add('1');
			this.charset.Add('2');
			this.charset.Add('3');
			this.charset.Add('4');
			this.charset.Add('5');
			this.charset.Add('6');
			this.charset.Add('7');
			this.charset.Add('8');
			this.charset.Add('9');
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "100100100100");
			this.encoding.Add('1', "100100100110");
			this.encoding.Add('2', "100100110100");
			this.encoding.Add('3', "100100110110");
			this.encoding.Add('4', "100110100100");
			this.encoding.Add('5', "100110100110");
			this.encoding.Add('6', "100110110100");
			this.encoding.Add('7', "100110110110");
			this.encoding.Add('8', "110100100100");
			this.encoding.Add('9', "110100100110");
			this.encoding.Add('[', "110");
			this.encoding.Add(']', "1001");
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x0012A109 File Offset: 0x00128309
		// (set) Token: 0x0600605C RID: 24668 RVA: 0x0012A111 File Offset: 0x00128311
		public CheckMSI Algorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x0012A11C File Offset: 0x0012831C
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			base.CheckSum = this.GetChecksum(value);
			if (base.CalculateCheckSum)
			{
				value += base.CheckSum;
			}
			if (!value.StartsWith(this.prefix))
			{
				value = this.prefix + value;
			}
			if (!value.EndsWith(this.suffix))
			{
				value += this.suffix;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				stringBuilder.Append(this.encoding[value[i]]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x0012A1D4 File Offset: 0x001283D4
		public string GetChecksum(string value)
		{
			int length = value.Length;
			switch (this.algorithm)
			{
			case CheckMSI.Modulo10:
				value += this.GetChecksum(value, 10);
				break;
			case CheckMSI.Modulo11:
				value += this.GetChecksum(value, 7, 11);
				break;
			case CheckMSI.Modulo1010:
				value += this.GetChecksum(value, 10);
				value += this.GetChecksum(value, 10);
				break;
			case CheckMSI.Modulo1110:
				value += this.GetChecksum(value, 7, 11);
				value += this.GetChecksum(value, 10);
				break;
			}
			return value.Substring(length);
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x0012A2A0 File Offset: 0x001284A0
		public string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x0012A2E4 File Offset: 0x001284E4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private char GetChecksum(string value, int modulo)
		{
			int num = 0;
			int num2 = 0;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = (int)(value[i] - '0') * (++num2 % 2 + 1);
				num3 = num3 % 10 + num3 / 10;
				num += num3;
			}
			return this.charset[num * (modulo - 1) % modulo];
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x0012A340 File Offset: 0x00128540
		private char GetChecksum(string value, int length, int modulo)
		{
			int num = 0;
			int num2 = 2;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = this.charset.IndexOf(value[i]);
				num += num3 * num2++;
				if (num2 > length)
				{
					num2 = 2;
				}
			}
			num = (11 - num % modulo) % modulo;
			return this.charset[num];
		}

		// Token: 0x04001761 RID: 5985
		private string prefix = "[";

		// Token: 0x04001762 RID: 5986
		private string suffix = "]";

		// Token: 0x04001763 RID: 5987
		private List<char> charset;

		// Token: 0x04001764 RID: 5988
		private Dictionary<char, string> encoding;

		// Token: 0x04001765 RID: 5989
		private CheckMSI algorithm;
	}
}
