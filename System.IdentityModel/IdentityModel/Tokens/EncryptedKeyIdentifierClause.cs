using System;
using System.Globalization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011B RID: 283
	public sealed class EncryptedKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00020A48 File Offset: 0x0001EC48
		public EncryptedKeyIdentifierClause(byte[] encryptedKey, string encryptionMethod) : this(encryptedKey, encryptionMethod, null)
		{
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020A53 File Offset: 0x0001EC53
		public EncryptedKeyIdentifierClause(byte[] encryptedKey, string encryptionMethod, SecurityKeyIdentifier encryptingKeyIdentifier) : this(encryptedKey, encryptionMethod, encryptingKeyIdentifier, null)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00020A5F File Offset: 0x0001EC5F
		public EncryptedKeyIdentifierClause(byte[] encryptedKey, string encryptionMethod, SecurityKeyIdentifier encryptingKeyIdentifier, string carriedKeyName) : this(encryptedKey, encryptionMethod, encryptingKeyIdentifier, carriedKeyName, true, null, 0)
		{
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00020A6F File Offset: 0x0001EC6F
		public EncryptedKeyIdentifierClause(byte[] encryptedKey, string encryptionMethod, SecurityKeyIdentifier encryptingKeyIdentifier, string carriedKeyName, byte[] derivationNonce, int derivationLength) : this(encryptedKey, encryptionMethod, encryptingKeyIdentifier, carriedKeyName, true, derivationNonce, derivationLength)
		{
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00020A81 File Offset: 0x0001EC81
		internal EncryptedKeyIdentifierClause(byte[] encryptedKey, string encryptionMethod, SecurityKeyIdentifier encryptingKeyIdentifier, string carriedKeyName, bool cloneBuffer, byte[] derivationNonce, int derivationLength) : base("http://www.w3.org/2001/04/xmlenc#EncryptedKey", encryptedKey, cloneBuffer, derivationNonce, derivationLength)
		{
			if (encryptionMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encryptionMethod");
			}
			this.carriedKeyName = carriedKeyName;
			this.encryptionMethod = encryptionMethod;
			this.encryptingKeyIdentifier = encryptingKeyIdentifier;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00020ABE File Offset: 0x0001ECBE
		public string CarriedKeyName
		{
			get
			{
				return this.carriedKeyName;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00020AC6 File Offset: 0x0001ECC6
		public SecurityKeyIdentifier EncryptingKeyIdentifier
		{
			get
			{
				return this.encryptingKeyIdentifier;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00020ACE File Offset: 0x0001ECCE
		public string EncryptionMethod
		{
			get
			{
				return this.encryptionMethod;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = keyIdentifierClause as EncryptedKeyIdentifierClause;
			return this == encryptedKeyIdentifierClause || (encryptedKeyIdentifierClause != null && encryptedKeyIdentifierClause.Matches(base.GetRawBuffer(), this.encryptionMethod, this.carriedKeyName));
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00020B0F File Offset: 0x0001ED0F
		public bool Matches(byte[] encryptedKey, string encryptionMethod, string carriedKeyName)
		{
			return base.Matches(encryptedKey) && this.encryptionMethod == encryptionMethod && this.carriedKeyName == carriedKeyName;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetEncryptedKey()
		{
			return base.GetBuffer();
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00020B36 File Offset: 0x0001ED36
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "EncryptedKeyIdentifierClause(EncryptedKey = {0}, Method '{1}')", new object[]
			{
				Convert.ToBase64String(base.GetRawBuffer()),
				this.EncryptionMethod
			});
		}

		// Token: 0x04000AD8 RID: 2776
		private readonly string carriedKeyName;

		// Token: 0x04000AD9 RID: 2777
		private readonly string encryptionMethod;

		// Token: 0x04000ADA RID: 2778
		private readonly SecurityKeyIdentifier encryptingKeyIdentifier;
	}
}
