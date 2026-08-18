using System;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013E RID: 318
	public class Saml2Id
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00024E9D File Offset: 0x0002309D
		public Saml2Id() : this(UniqueId.CreateRandomId())
		{
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00024EAC File Offset: 0x000230AC
		public Saml2Id(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			try
			{
				this.value = XmlConvert.VerifyNCName(value);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID4128"), "value", innerException));
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00024F18 File Offset: 0x00023118
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00024F20 File Offset: 0x00023120
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			Saml2Id saml2Id = obj as Saml2Id;
			return saml2Id != null && StringComparer.Ordinal.Equals(this.value, saml2Id.Value);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00024F55 File Offset: 0x00023155
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00024F18 File Offset: 0x00023118
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x04000B55 RID: 2901
		private string value;
	}
}
