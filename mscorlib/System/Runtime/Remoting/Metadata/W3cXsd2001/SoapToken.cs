using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078E RID: 1934
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x000EB407 File Offset: 0x000EA407
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x000EB40E File Offset: 0x000EA40E
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000EB415 File Offset: 0x000EA415
		public SoapToken()
		{
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000EB41D File Offset: 0x000EA41D
		public SoapToken(string value)
		{
			this._value = this.Validate(value);
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x000EB432 File Offset: 0x000EA432
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x000EB43A File Offset: 0x000EA43A
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

		// Token: 0x060044F0 RID: 17648 RVA: 0x000EB449 File Offset: 0x000EA449
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000EB456 File Offset: 0x000EA456
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000EB460 File Offset: 0x000EA460
		private string Validate(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			char[] anyOf = new char[]
			{
				'\r',
				'\t'
			};
			int num = value.LastIndexOfAny(anyOf);
			if (num > -1)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:token",
					value
				}));
			}
			if (value.Length > 0 && (char.IsWhiteSpace(value[0]) || char.IsWhiteSpace(value[value.Length - 1])))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:token",
					value
				}));
			}
			num = value.IndexOf("  ");
			if (num > -1)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"xsd:token",
					value
				}));
			}
			return value;
		}

		// Token: 0x04002261 RID: 8801
		private string _value;
	}
}
