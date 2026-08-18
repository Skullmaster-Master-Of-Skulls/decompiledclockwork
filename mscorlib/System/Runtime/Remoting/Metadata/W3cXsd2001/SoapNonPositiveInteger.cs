using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000787 RID: 1927
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonPositiveInteger : ISoapXsd
	{
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000EAE8C File Offset: 0x000E9E8C
		public static string XsdType
		{
			get
			{
				return "nonPositiveInteger";
			}
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x000EAE93 File Offset: 0x000E9E93
		public string GetXsdType()
		{
			return SoapNonPositiveInteger.XsdType;
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x000EAE9A File Offset: 0x000E9E9A
		public SoapNonPositiveInteger()
		{
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000EAEA4 File Offset: 0x000E9EA4
		public SoapNonPositiveInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value > 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:nonPositiveInteger",
					value
				}));
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000EAF09 File Offset: 0x000E9F09
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x000EAF14 File Offset: 0x000E9F14
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
					{
						"xsd:nonPositiveInteger",
						value
					}));
				}
			}
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000EAF73 File Offset: 0x000E9F73
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000EAF85 File Offset: 0x000E9F85
		public static SoapNonPositiveInteger Parse(string value)
		{
			return new SoapNonPositiveInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002258 RID: 8792
		private decimal _value;
	}
}
