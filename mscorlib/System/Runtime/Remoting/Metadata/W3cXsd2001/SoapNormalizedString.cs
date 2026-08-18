using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078D RID: 1933
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNormalizedString : ISoapXsd
	{
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000EB33B File Offset: 0x000EA33B
		public static string XsdType
		{
			get
			{
				return "normalizedString";
			}
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x000EB342 File Offset: 0x000EA342
		public string GetXsdType()
		{
			return SoapNormalizedString.XsdType;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x000EB349 File Offset: 0x000EA349
		public SoapNormalizedString()
		{
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x000EB351 File Offset: 0x000EA351
		public SoapNormalizedString(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x000EB366 File Offset: 0x000EA366
		// (set) Token: 0x060044E6 RID: 17638 RVA: 0x000EB36E File Offset: 0x000EA36E
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = this.Validate(value);
			}
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x000EB37D File Offset: 0x000EA37D
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x000EB38A File Offset: 0x000EA38A
		public static SoapNormalizedString Parse(string value)
		{
			return new SoapNormalizedString(value);
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x000EB3A0 File Offset: 0x000EA3A0
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] anyOf = new char[]
			{
				'\r',
				'\n',
				'\t'
			};
			int num = value.LastIndexOfAny(anyOf);
			if (num > -1)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:normalizedString",
					value
				}));
			}
			return value;
		}

		// Token: 0x04002260 RID: 8800
		private string _value;
	}
}
