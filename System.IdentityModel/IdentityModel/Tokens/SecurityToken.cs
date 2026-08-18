using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000170 RID: 368
	public abstract class SecurityToken
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B80 RID: 2944
		public abstract string Id { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B81 RID: 2945
		public abstract ReadOnlyCollection<SecurityKey> SecurityKeys { get; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B82 RID: 2946
		public abstract DateTime ValidFrom { get; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B83 RID: 2947
		public abstract DateTime ValidTo { get; }

		// Token: 0x06000B84 RID: 2948 RVA: 0x00036C4B File Offset: 0x00034E4B
		public virtual bool CanCreateKeyIdentifierClause<T>() where T : SecurityKeyIdentifierClause
		{
			return typeof(T) == typeof(LocalIdKeyIdentifierClause) && this.CanCreateLocalKeyIdentifierClause();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00036C70 File Offset: 0x00034E70
		public virtual T CreateKeyIdentifierClause<T>() where T : SecurityKeyIdentifierClause
		{
			if (typeof(T) == typeof(LocalIdKeyIdentifierClause) && this.CanCreateLocalKeyIdentifierClause())
			{
				return new LocalIdKeyIdentifierClause(this.Id, base.GetType()) as T;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("TokenDoesNotSupportKeyIdentifierClauseCreation", new object[]
			{
				base.GetType().Name,
				typeof(T).Name
			})));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00036CFC File Offset: 0x00034EFC
		public virtual bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			LocalIdKeyIdentifierClause localIdKeyIdentifierClause = keyIdentifierClause as LocalIdKeyIdentifierClause;
			return localIdKeyIdentifierClause != null && localIdKeyIdentifierClause.Matches(this.Id, base.GetType());
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00036D27 File Offset: 0x00034F27
		public virtual SecurityKey ResolveKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (this.SecurityKeys.Count != 0 && this.MatchesKeyIdentifierClause(keyIdentifierClause))
			{
				return this.SecurityKeys[0];
			}
			return null;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00036D4D File Offset: 0x00034F4D
		private bool CanCreateLocalKeyIdentifierClause()
		{
			return this.Id != null;
		}
	}
}
