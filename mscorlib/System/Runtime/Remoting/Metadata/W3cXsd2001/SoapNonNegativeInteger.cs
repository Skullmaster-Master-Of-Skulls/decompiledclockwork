using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000788 RID: 1928
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000EAF98 File Offset: 0x000E9F98
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000EAF9F File Offset: 0x000E9F9F
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000EAFA6 File Offset: 0x000E9FA6
		public SoapNonNegativeInteger()
		{
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000EAFB0 File Offset: 0x000E9FB0
		public SoapNonNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (this._value < 0m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:nonNegativeInteger",
					value
				}));
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x000EB015 File Offset: 0x000EA015
		// (set) Token: 0x060044B8 RID: 17592 RVA: 0x000EB020 File Offset: 0x000EA020
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value < 0m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
					{
						"xsd:nonNegativeInteger",
						value
					}));
				}
			}
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000EB07F File Offset: 0x000EA07F
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000EB091 File Offset: 0x000EA091
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x04002259 RID: 8793
		private decimal _value;
	}
}
