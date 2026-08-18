using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000789 RID: 1929
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000EB0A4 File Offset: 0x000EA0A4
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000EB0AB File Offset: 0x000EA0AB
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000EB0B2 File Offset: 0x000EA0B2
		public SoapNegativeInteger()
		{
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000EB0BC File Offset: 0x000EA0BC
		public SoapNegativeInteger(decimal value)
		{
			this._value = decimal.Truncate(value);
			if (value > -1m)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:negativeInteger",
					value
				}));
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000EB11C File Offset: 0x000EA11C
		// (set) Token: 0x060044C0 RID: 17600 RVA: 0x000EB124 File Offset: 0x000EA124
		public decimal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = decimal.Truncate(value);
				if (this._value > -1m)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
					{
						"xsd:negativeInteger",
						value
					}));
				}
			}
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x000EB183 File Offset: 0x000EA183
		public override string ToString()
		{
			return this._value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000EB195 File Offset: 0x000EA195
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
		}

		// Token: 0x0400225A RID: 8794
		private decimal _value;
	}
}
