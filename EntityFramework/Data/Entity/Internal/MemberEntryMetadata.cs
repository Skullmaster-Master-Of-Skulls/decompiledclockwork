using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200077D RID: 1917
	internal abstract class MemberEntryMetadata
	{
		// Token: 0x060056EF RID: 22255 RVA: 0x00177F17 File Offset: 0x00176117
		protected MemberEntryMetadata(Type declaringType, Type elementType, string memberName)
		{
			this._declaringType = declaringType;
			this._elementType = elementType;
			this._memberName = memberName;
		}

		// Token: 0x060056F0 RID: 22256
		public abstract InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry);

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060056F1 RID: 22257
		public abstract MemberEntryType MemberEntryType { get; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x00177F34 File Offset: 0x00176134
		public string MemberName
		{
			get
			{
				return this._memberName;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060056F3 RID: 22259 RVA: 0x00177F3C File Offset: 0x0017613C
		public Type DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x00177F44 File Offset: 0x00176144
		public Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060056F5 RID: 22261
		public abstract Type MemberType { get; }

		// Token: 0x04002314 RID: 8980
		private readonly Type _declaringType;

		// Token: 0x04002315 RID: 8981
		private readonly Type _elementType;

		// Token: 0x04002316 RID: 8982
		private readonly string _memberName;
	}
}
