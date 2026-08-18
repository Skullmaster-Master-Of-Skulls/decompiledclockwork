using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E0 RID: 480
	internal sealed class MemberCollection : MetadataCollection<EdmMember>
	{
		// Token: 0x0600204D RID: 8269 RVA: 0x00070919 File Offset: 0x0006EB19
		public MemberCollection(StructuralType declaringType) : this(declaringType, null)
		{
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00070923 File Offset: 0x0006EB23
		public MemberCollection(StructuralType declaringType, IEnumerable<EdmMember> items) : base(items)
		{
			this._declaringType = declaringType;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00070933 File Offset: 0x0006EB33
		public override ReadOnlyCollection<EdmMember> AsReadOnly
		{
			get
			{
				return new ReadOnlyCollection<EdmMember>(this);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0007093B File Offset: 0x0006EB3B
		public override int Count
		{
			get
			{
				return this.GetBaseTypeMemberCount() + base.Count;
			}
		}

		// Token: 0x17000687 RID: 1671
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
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x17000688 RID: 1672
		public override EdmMember this[string identity]
		{
			get
			{
				return this.GetValue(identity, false);
			}
			set
			{
				throw EntityUtil.OperationOnReadOnlyCollection();
			}
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00070992 File Offset: 0x0006EB92
		public override void Add(EdmMember member)
		{
			this.ValidateMemberForAdd(member, "member");
			base.Add(member);
			member.ChangeDeclaringTypeWithoutCollectionFixup(this._declaringType);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000709B4 File Offset: 0x0006EBB4
		public override bool ContainsIdentity(string identity)
		{
			if (base.ContainsIdentity(identity))
			{
				return true;
			}
			EdmType baseType = this._declaringType.BaseType;
			return baseType != null && ((StructuralType)baseType).Members.Contains(identity);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000709F4 File Offset: 0x0006EBF4
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

		// Token: 0x06002058 RID: 8280 RVA: 0x00070A38 File Offset: 0x0006EC38
		public override void CopyTo(EdmMember[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				throw EntityUtil.ArgumentOutOfRange("arrayIndex");
			}
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			if (base.Count + baseTypeMemberCount > array.Length - arrayIndex)
			{
				throw EntityUtil.Argument("arrayIndex");
			}
			if (baseTypeMemberCount > 0)
			{
				((StructuralType)this._declaringType.BaseType).Members.CopyTo(array, arrayIndex);
			}
			base.CopyTo(array, arrayIndex + baseTypeMemberCount);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00070AA0 File Offset: 0x0006ECA0
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

		// Token: 0x0600205A RID: 8282 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		public override EdmMember GetValue(string identity, bool ignoreCase)
		{
			EdmMember result = null;
			if (!this.TryGetValue(identity, ignoreCase, out result))
			{
				throw EntityUtil.ItemInvalidIdentity(identity, "identity");
			}
			return result;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x00070B08 File Offset: 0x0006ED08
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
			return metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x00070B54 File Offset: 0x0006ED54
		private int GetBaseTypeMemberCount()
		{
			StructuralType structuralType = this._declaringType.BaseType as StructuralType;
			if (structuralType != null)
			{
				return structuralType.Members.Count;
			}
			return 0;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00070B84 File Offset: 0x0006ED84
		private int GetRelativeIndex(int index)
		{
			int baseTypeMemberCount = this.GetBaseTypeMemberCount();
			int count = base.Count;
			if (index < 0 || index >= baseTypeMemberCount + count)
			{
				throw EntityUtil.ArgumentOutOfRange("index");
			}
			return index - baseTypeMemberCount;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x00070BB7 File Offset: 0x0006EDB7
		private void ValidateMemberForAdd(EdmMember member, string argumentName)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, argumentName);
			this._declaringType.ValidateMemberForAdd(member);
		}

		// Token: 0x04000E43 RID: 3651
		private StructuralType _declaringType;
	}
}
