using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C8 RID: 1224
	public abstract class StructuralType : EdmType
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x000DB126 File Offset: 0x000D9326
		internal StructuralType()
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000DB14B File Offset: 0x000D934B
		internal StructuralType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._members = new MemberCollection(this);
			this._readOnlyMembers = this._members.AsReadOnlyMetadataCollection();
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000DB173 File Offset: 0x000D9373
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> Members
		{
			get
			{
				return this._readOnlyMembers;
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000DB17B File Offset: 0x000D937B
		internal ReadOnlyMetadataCollection<T> GetDeclaredOnlyMembers<T>() where T : EdmMember
		{
			return this._members.GetDeclaredOnlyMembers<T>();
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000DB188 File Offset: 0x000D9388
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06002D34 RID: 11572
		internal abstract void ValidateMemberForAdd(EdmMember member);

		// Token: 0x06002D35 RID: 11573 RVA: 0x000DB1A9 File Offset: 0x000D93A9
		public void AddMember(EdmMember member)
		{
			this.AddMember(member, false);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000DB1B4 File Offset: 0x000D93B4
		internal void AddMember(EdmMember member, bool forceAdd)
		{
			Check.NotNull<EdmMember>(member, "member");
			if (!forceAdd)
			{
				Util.ThrowIfReadOnly(this);
			}
			if (this.DataSpace != member.TypeUsage.EdmType.DataSpace && this.BuiltInTypeKind != BuiltInTypeKind.RowType)
			{
				throw new ArgumentException(Strings.AttemptToAddEdmMemberFromWrongDataSpace(member.Name, this.Name, member.TypeUsage.EdmType.DataSpace, this.DataSpace), "member");
			}
			if (BuiltInTypeKind.RowType == this.BuiltInTypeKind)
			{
				if (this._members.Count == 0)
				{
					this.DataSpace = member.TypeUsage.EdmType.DataSpace;
				}
				else if (this.DataSpace != (DataSpace)(-1) && member.TypeUsage.EdmType.DataSpace != this.DataSpace)
				{
					this.DataSpace = (DataSpace)(-1);
				}
			}
			if (this._members.IsReadOnly && forceAdd)
			{
				this._members.ResetReadOnly();
				this._members.Add(member);
				this._members.SetReadOnly();
				return;
			}
			this._members.Add(member);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000DB2CB File Offset: 0x000D94CB
		public virtual void RemoveMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			this._members.Remove(member);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000DB2EC File Offset: 0x000D94EC
		internal virtual bool HasMember(EdmMember member)
		{
			return this._members.Contains(member);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000DB2FA File Offset: 0x000D94FA
		internal virtual void NotifyItemIdentityChanged(EdmMember item, string initialIdentity)
		{
			this._members.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x04001095 RID: 4245
		private readonly MemberCollection _members;

		// Token: 0x04001096 RID: 4246
		private readonly ReadOnlyMetadataCollection<EdmMember> _readOnlyMembers;
	}
}
