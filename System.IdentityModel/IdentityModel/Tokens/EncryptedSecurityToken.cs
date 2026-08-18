using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011C RID: 284
	public class EncryptedSecurityToken : SecurityToken
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00020B64 File Offset: 0x0001ED64
		public EncryptedSecurityToken(SecurityToken token, EncryptingCredentials encryptingCredentials)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (encryptingCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encryptingCredentials");
			}
			this._encryptingCredentials = encryptingCredentials;
			this._realToken = token;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return this._realToken.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00020BAD File Offset: 0x0001EDAD
		public override T CreateKeyIdentifierClause<T>()
		{
			return this._realToken.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00020BBA File Offset: 0x0001EDBA
		public EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this._encryptingCredentials;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00020BC2 File Offset: 0x0001EDC2
		public override string Id
		{
			get
			{
				return this._realToken.Id;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00020BCF File Offset: 0x0001EDCF
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this._realToken.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00020BDD File Offset: 0x0001EDDD
		public override SecurityKey ResolveKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this._realToken.ResolveKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00020BEB File Offset: 0x0001EDEB
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this._realToken.SecurityKeys;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public SecurityToken Token
		{
			get
			{
				return this._realToken;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00020C00 File Offset: 0x0001EE00
		public override DateTime ValidFrom
		{
			get
			{
				return this._realToken.ValidFrom;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00020C0D File Offset: 0x0001EE0D
		public override DateTime ValidTo
		{
			get
			{
				return this._realToken.ValidTo;
			}
		}

		// Token: 0x04000ADB RID: 2779
		private EncryptingCredentials _encryptingCredentials;

		// Token: 0x04000ADC RID: 2780
		private SecurityToken _realToken;
	}
}
