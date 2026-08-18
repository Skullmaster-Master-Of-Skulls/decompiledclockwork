using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200077E RID: 1918
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x000EA616 File Offset: 0x000E9616
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000EA61D File Offset: 0x000E961D
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x000EA624 File Offset: 0x000E9624
		public SoapYearMonth()
		{
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000EA637 File Offset: 0x000E9637
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000EA651 File Offset: 0x000E9651
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x000EA672 File Offset: 0x000E9672
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x000EA67A File Offset: 0x000E967A
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000EA683 File Offset: 0x000E9683
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x000EA68B File Offset: 0x000E968B
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000EA694 File Offset: 0x000E9694
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000EA6CC File Offset: 0x000E96CC
		public static SoapYearMonth Parse(string value)
		{
			int sign = 0;
			if (value[0] == '-')
			{
				sign = -1;
			}
			return new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
		}

		// Token: 0x04002247 RID: 8775
		private DateTime _value = DateTime.MinValue;

		// Token: 0x04002248 RID: 8776
		private int _sign;

		// Token: 0x04002249 RID: 8777
		private static string[] formats = new string[]
		{
			"yyyy-MM",
			"'+'yyyy-MM",
			"'-'yyyy-MM",
			"yyyy-MMzzz",
			"'+'yyyy-MMzzz",
			"'-'yyyy-MMzzz"
		};
	}
}
