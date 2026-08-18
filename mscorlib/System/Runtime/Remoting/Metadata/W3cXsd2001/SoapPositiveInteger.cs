using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000786 RID: 1926
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x000EAD81 File Offset: 0x000E9D81
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000EAD88 File Offset: 0x000E9D88
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000EAD8F File Offset: 0x000E9D8F
		public SoapPositiveInteger()
		{
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000EAD98 File Offset: 0x000E9D98
		public SoapPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:positiveInteger",
					value
				}));
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x000EADFD File Offset: 0x000E9DFD
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x000EAE08 File Offset: 0x000E9E08
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
					{
						"xsd:positiveInteger",
						value
					}));
				}
			}
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x000EAE67 File Offset: 0x000E9E67
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x000EAE79 File Offset: 0x000E9E79
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002257 RID: 8791
		private decimal _value;
	}
}
