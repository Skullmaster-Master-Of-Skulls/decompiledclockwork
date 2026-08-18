using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000784 RID: 1924
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x000EAC50 File Offset: 0x000E9C50
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000EAC57 File Offset: 0x000E9C57
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000EAC5E File Offset: 0x000E9C5E
		public SoapBase64Binary()
		{
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000EAC66 File Offset: 0x000E9C66
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x000EAC75 File Offset: 0x000E9C75
		// (set) Token: 0x06004498 RID: 17560 RVA: 0x000EAC7D File Offset: 0x000E9C7D
		public byte[] Value
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

		// Token: 0x06004499 RID: 17561 RVA: 0x000EAC86 File Offset: 0x000E9C86
		public override string ToString()
		{
			if (this._value == null)
			{
				return null;
			}
			return SoapType.LineFeedsBin64(Convert.ToBase64String(this._value));
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000EACA4 File Offset: 0x000E9CA4
		public static SoapBase64Binary Parse(string value)
		{
			if (value == null || value.Length == 0)
			{
				return new SoapBase64Binary(new byte[0]);
			}
			byte[] value2;
			try
			{
				value2 = Convert.FromBase64String(SoapType.FilterBin64(value));
			}
			catch (Exception)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), new object[]
				{
					"base64Binary",
					value
				}));
			}
			return new SoapBase64Binary(value2);
		}

		// Token: 0x04002255 RID: 8789
		private byte[] _value;
	}
}
