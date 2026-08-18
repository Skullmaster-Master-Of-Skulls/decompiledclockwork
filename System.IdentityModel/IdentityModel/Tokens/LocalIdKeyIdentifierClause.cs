using System;
using System.Globalization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012A RID: 298
	public class LocalIdKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x00022AAE File Offset: 0x00020CAE
		public LocalIdKeyIdentifierClause(string localId) : this(localId, null)
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00022AB8 File Offset: 0x00020CB8
		public LocalIdKeyIdentifierClause(string localId, Type ownerType)
		{
			Type[] array;
			if (!(ownerType == null))
			{
				(array = new Type[1])[0] = ownerType;
			}
			else
			{
				array = null;
			}
			this..ctor(localId, array);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00022AD7 File Offset: 0x00020CD7
		public LocalIdKeyIdentifierClause(string localId, byte[] derivationNonce, int derivationLength, Type ownerType)
		{
			string text = null;
			Type[] array;
			if (!(ownerType == null))
			{
				(array = new Type[1])[0] = ownerType;
			}
			else
			{
				array = null;
			}
			this..ctor(text, derivationNonce, derivationLength, array);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00022AFA File Offset: 0x00020CFA
		internal LocalIdKeyIdentifierClause(string localId, Type[] ownerTypes) : this(localId, null, 0, ownerTypes)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00022B08 File Offset: 0x00020D08
		internal LocalIdKeyIdentifierClause(string localId, byte[] derivationNonce, int derivationLength, Type[] ownerTypes) : base(null, derivationNonce, derivationLength)
		{
			if (localId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localId");
			}
			if (localId == string.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("LocalIdCannotBeEmpty"));
			}
			this.localId = localId;
			this.ownerTypes = ownerTypes;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00022B62 File Offset: 0x00020D62
		public string LocalId
		{
			get
			{
				return this.localId;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00022B6A File Offset: 0x00020D6A
		public Type OwnerType
		{
			get
			{
				if (this.ownerTypes != null && this.ownerTypes.Length != 0)
				{
					return this.ownerTypes[0];
				}
				return null;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00022B88 File Offset: 0x00020D88
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			LocalIdKeyIdentifierClause localIdKeyIdentifierClause = keyIdentifierClause as LocalIdKeyIdentifierClause;
			return this == localIdKeyIdentifierClause || (localIdKeyIdentifierClause != null && localIdKeyIdentifierClause.Matches(this.localId, this.OwnerType));
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00022BBC File Offset: 0x00020DBC
		public bool Matches(string localId, Type ownerType)
		{
			if (string.IsNullOrEmpty(localId))
			{
				return false;
			}
			if (this.localId != localId)
			{
				return false;
			}
			if (this.ownerTypes == null || ownerType == null)
			{
				return true;
			}
			for (int i = 0; i < this.ownerTypes.Length; i++)
			{
				if (this.ownerTypes[i] == null || this.ownerTypes[i] == ownerType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00022C2C File Offset: 0x00020E2C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "LocalIdKeyIdentifierClause(LocalId = '{0}', Owner = '{1}')", new object[]
			{
				this.LocalId,
				this.OwnerType
			});
		}

		// Token: 0x04000B0C RID: 2828
		private readonly string localId;

		// Token: 0x04000B0D RID: 2829
		private readonly Type[] ownerTypes;
	}
}
