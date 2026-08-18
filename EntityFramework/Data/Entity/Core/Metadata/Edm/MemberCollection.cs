using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EA RID: 1258
	internal sealed class MemberCollection : MetadataCollection<EdmMember>
	{
		// Token: 0x06002EDC RID: 11996 RVA: 0x000E0412 File Offset: 0x000DE612
		public MemberCollection(StructuralType declaringType) : this(declaringType, null)
		{
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000E041C File Offset: 0x000DE61C
		public MemberCollection(StructuralType declaringType, IEnumerable<EdmMember> items) : base(items)
		{
			this._declaringType = declaringType;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000E042C File Offset: 0x000DE62C
		public override ReadOnlyCollection<EdmMember> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<EdmMember>(this);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000E0434 File Offset: 0x000DE634
		public override int Count
		{
			get
			{
				return this.GetBaseTypeMemberCount() + base.Count;
			}
		}

		// Token: 0x170006FE RID: 1790
		public override EdmMember this[int index]
		{
			get
			{
				int relativeIndex = this.GetRelativeIndex(index);
				if (relativeIndex < 0)
				{
					return ((StructuralType)this._declaringType.BaseType).Members[index];
				}
				return base[relativeIndex];
			}
			set
			{
				int relativeIndex = this.GetRelativeIndex(index);
				if (relativeIndex < 0)
				{
					((StructuralType)this._declaringType.BaseType).Members.Source[index] = value;
					return;
				}
				base[relativeIndex] = value;
			}
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000E04C3 File Offset: 0x000DE6C3
		public override void Add(EdmMember member)
		{
			this.ValidateMemberForAdd(member, "member");
			base.Add(member);
			member.ChangeDeclaringTypeWithoutCollectionFixup(this._declaringType);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000E04E4 File Offset: 0x000DE6E4
		public override bool ContainsIdentity(string identity)
		{
			if (base.ContainsIdentity(identity))
			{
				return true;
			}
			EdmType baseType = this._declaringType.BaseType;
			return baseType != null && ((StructuralType)baseType).Members.Contains(identity);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000E0524 File Offset: 0x000DE724
		public override int IndexOf(EdmMember item)
		{
			int num = base.IndexOf(item);
			if (num != -1)
			{
				return num + this.GetBaseTypeMemberCount();
			}
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000E0568 File Offset: 0x000DE768
		public override void CopyTo(EdmMember[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			if (base.Count + baseTypeMemberCount > array.Length - arrayIndex)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (baseTypeMemberCount > 0)
			{
				((StructuralType)this._declaringType.BaseType).Members.CopyTo(array, arrayIndex);
			}
			base.CopyTo(array, arrayIndex + baseTypeMemberCount);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000E05D0 File Offset: 0x000DE7D0
		public override bool TryGetValue(string identity, bool ignoreCase, out EdmMember item)
		{
			if (!base.TryGetValue(identity, ignoreCase, out item))
			{
				EdmType baseType = this._declaringType.BaseType;
				if (baseType != null)
				{
					((StructuralType)baseType).Members.TryGetValue(identity, ignoreCase, out item);
				}
			}
			return item != null;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000E0614 File Offset: 0x000DE814
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			MetadataCollection<T> metadataCollection = new MetadataCollection<T>();
			for (int i = 0; i < base.Count; i++)
			{
				T t = base[i] as T;
				if (t != null)
				{
					metadataCollection.Add(t);
				}
			}
			return new ReadOnlyMetadataCollection<T>(metadataCollection);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000E0660 File Offset: 0x000DE860
		private int GetBaseTypeMemberCount()
		{
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.Count;
			}
			return 0;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000E0690 File Offset: 0x000DE890
		private int GetRelativeIndex(int index)
		{
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			int count = base.Count;
			if (index < 0 || index >= baseTypeMemberCount + count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index - baseTypeMemberCount;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000E06C3 File Offset: 0x000DE8C3
		private void ValidateMemberForAdd(EdmMember member, string argumentName)
		{
			Check.NotNull<EdmMember>(member, argumentName);
			this._declaringType.ValidateMemberForAdd(member);
		}

		// Token: 0x040011CE RID: 4558
		private readonly StructuralType _declaringType;
	}
}
