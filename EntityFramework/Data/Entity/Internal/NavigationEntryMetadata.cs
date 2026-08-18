using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200077F RID: 1919
	internal class NavigationEntryMetadata : MemberEntryMetadata
	{
		// Token: 0x060056F6 RID: 22262 RVA: 0x00177F4C File Offset: 0x0017614C
		public NavigationEntryMetadata(Type declaringType, Type propertyType, string propertyName, bool isCollection) : base(declaringType, propertyType, propertyName)
		{
			this._isCollection = isCollection;
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060056F7 RID: 22263 RVA: 0x00177F5F File Offset: 0x0017615F
		public override MemberEntryType MemberEntryType
		{
			get
			{
				if (!this._isCollection)
				{
					return MemberEntryType.ReferenceNavigationProperty;
				}
				return MemberEntryType.CollectionNavigationProperty;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x00177F6C File Offset: 0x0017616C
		public override Type MemberType
		{
			get
			{
				if (!this._isCollection)
				{
					return base.ElementType;
				}
				return DbHelpers.CollectionType(base.ElementType);
			}
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x00177F88 File Offset: 0x00176188
		public override InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry)
		{
			if (!this._isCollection)
			{
				return new InternalReferenceEntry(internalEntityEntry, this);
			}
			return new InternalCollectionEntry(internalEntityEntry, this);
		}

		// Token: 0x0400231C RID: 8988
		private readonly bool _isCollection;
	}
}
