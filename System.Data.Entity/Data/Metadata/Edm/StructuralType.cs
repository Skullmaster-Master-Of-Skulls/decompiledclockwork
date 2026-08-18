using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F8 RID: 504
	public abstract class StructuralType : EdmType
	{
		// Token: 0x06002129 RID: 8489 RVA: 0x00074B51 File Offset: 0x00072D51
		internal StructuralType()
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00074B76 File Offset: 0x00072D76
		internal StructuralType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00074B9E File Offset: 0x00072D9E
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> Members
		{
			get
			{
				return this._readOnlyMembers;
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00074BA6 File Offset: 0x00072DA6
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			return this._members.GetDeclaredOnlyMembers<T>();
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00074BB3 File Offset: 0x00072DB3
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x0600212E RID: 8494
		internal abstract void ValidateMemberForAdd(EdmMember member);

		// Token: 0x0600212F RID: 8495 RVA: 0x00074BD4 File Offset: 0x00072DD4
		internal void AddMember(EdmMember member)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (BuiltInTypeKind.RowType == this.BuiltInTypeKind)
			{
				if (this._members.Count == 0)
				{
					base.DataSpace = member.TypeUsage.EdmType.DataSpace;
				}
				else if (base.DataSpace != (DataSpace)(-1) && member.TypeUsage.EdmType.DataSpace != base.DataSpace)
				{
					base.DataSpace = (DataSpace)(-1);
				}
			}
			this._members.Add(member);
		}

		// Token: 0x04000EAB RID: 3755
		private readonly MemberCollection _members;

		// Token: 0x04000EAC RID: 3756
		private readonly ReadOnlyMetadataCollection<EdmMember> _readOnlyMembers;
	}
}
