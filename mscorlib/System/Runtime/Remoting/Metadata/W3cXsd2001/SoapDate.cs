using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200077D RID: 1917
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x000EA49C File Offset: 0x000E949C
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x000EA4A3 File Offset: 0x000E94A3
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000EA4AC File Offset: 0x000E94AC
		public SoapDate()
		{
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x000EA4D4 File Offset: 0x000E94D4
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000EA504 File Offset: 0x000E9504
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x000EA538 File Offset: 0x000E9538
		// (set) Token: 0x06004450 RID: 17488 RVA: 0x000EA540 File Offset: 0x000E9540
		public DateTime Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value.Date;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x000EA54F File Offset: 0x000E954F
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x000EA557 File Offset: 0x000E9557
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

		// Token: 0x06004453 RID: 17491 RVA: 0x000EA560 File Offset: 0x000E9560
		public override string ToString()
		{
			if (this._sign < 0)
			{
				return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x000EA598 File Offset: 0x000E9598
		public static SoapDate Parse(string value)
		{
			int sign = 0;
			if (value[0] == '-')
			{
				sign = -1;
			}
			return new SoapDate(DateTime.ParseExact(value, SoapDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.None), sign);
		}

		// Token: 0x04002244 RID: 8772
		private DateTime _value = DateTime.MinValue.Date;

		// Token: 0x04002245 RID: 8773
		private int _sign;

		// Token: 0x04002246 RID: 8774
		private static string[] formats = new string[]
		{
			"yyyy-MM-dd",
			"'+'yyyy-MM-dd",
			"'-'yyyy-MM-dd",
			"yyyy-MM-ddzzz",
			"'+'yyyy-MM-ddzzz",
			"'-'yyyy-MM-ddzzz"
		};
	}
}
