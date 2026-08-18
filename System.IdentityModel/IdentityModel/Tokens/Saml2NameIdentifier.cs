using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013F RID: 319
	public class Saml2NameIdentifier
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x00024F62 File Offset: 0x00023162
		public Saml2NameIdentifier(string name) : this(name, null)
		{
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00024F6C File Offset: 0x0002316C
		public Saml2NameIdentifier(string name, Uri format)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (null != format && !format.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("format", SR.GetString("ID0013"));
			}
			this.format = format;
			this.value = name;
			this.externalEncryptedKeys = new Collection<EncryptedKeyIdentifierClause>();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00024FDB File Offset: 0x000231DB
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00024FE3 File Offset: 0x000231E3
		public EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this.encryptingCredentials;
			}
			set
			{
				this.encryptingCredentials = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00024FEC File Offset: 0x000231EC
		public Collection<EncryptedKeyIdentifierClause> ExternalEncryptedKeys
		{
			get
			{
				return this.externalEncryptedKeys;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00024FF4 File Offset: 0x000231F4
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00024FFC File Offset: 0x000231FC
		public Uri Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (null != value && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this.format = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00025030 File Offset: 0x00023230
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00025038 File Offset: 0x00023238
		public string NameQualifier
		{
			get
			{
				return this.nameQualifier;
			}
			set
			{
				this.nameQualifier = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00025046 File Offset: 0x00023246
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0002504E File Offset: 0x0002324E
		public string SPNameQualifier
		{
			get
			{
				return this.serviceProviderPointNameQualifier;
			}
			set
			{
				this.serviceProviderPointNameQualifier = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002505C File Offset: 0x0002325C
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00025064 File Offset: 0x00023264
		public string SPProvidedId
		{
			get
			{
				return this.serviceProviderdId;
			}
			set
			{
				this.serviceProviderdId = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00025072 File Offset: 0x00023272
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0002507A File Offset: 0x0002327A
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.value = value;
			}
		}

		// Token: 0x04000B56 RID: 2902
		private Uri format;

		// Token: 0x04000B57 RID: 2903
		private string nameQualifier;

		// Token: 0x04000B58 RID: 2904
		private string serviceProviderPointNameQualifier;

		// Token: 0x04000B59 RID: 2905
		private string serviceProviderdId;

		// Token: 0x04000B5A RID: 2906
		private string value;

		// Token: 0x04000B5B RID: 2907
		private EncryptingCredentials encryptingCredentials;

		// Token: 0x04000B5C RID: 2908
		private Collection<EncryptedKeyIdentifierClause> externalEncryptedKeys;
	}
}
