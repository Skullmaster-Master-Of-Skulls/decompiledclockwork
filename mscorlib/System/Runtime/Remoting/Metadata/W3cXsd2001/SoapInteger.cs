using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000785 RID: 1925
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x000EAD1C File Offset: 0x000E9D1C
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x000EAD23 File Offset: 0x000E9D23
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x000EAD2A File Offset: 0x000E9D2A
		public SoapInteger()
		{
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000EAD32 File Offset: 0x000E9D32
		public SoapInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x000EAD46 File Offset: 0x000E9D46
		// (set) Token: 0x060044A0 RID: 17568 RVA: 0x000EAD4E File Offset: 0x000E9D4E
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
			}
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000EAD5C File Offset: 0x000E9D5C
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000EAD6E File Offset: 0x000E9D6E
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002256 RID: 8790
		private decimal _value;
	}
}
