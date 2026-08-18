using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000611 RID: 1553
	public struct MemberRelationship
	{
		// Token: 0x060038E3 RID: 14563 RVA: 0x000F241E File Offset: 0x000F061E
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this._owner = owner;
			this._member = member;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000F244A File Offset: 0x000F064A
		public bool IsEmpty
		{
			get
			{
				return this._owner == null;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000F2455 File Offset: 0x000F0655
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000F245D File Offset: 0x000F065D
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000F2468 File Offset: 0x000F0668
		public override bool Equals(object obj)
		{
			if (!(obj is MemberRelationship))
			{
				return false;
			}
			MemberRelationship memberRelationship = (MemberRelationship)obj;
			return memberRelationship.Owner == this.Owner && memberRelationship.Member == this.Member;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000F24A6 File Offset: 0x000F06A6
		public override int GetHashCode()
		{
			if (this._owner == null)
			{
				return base.GetHashCode();
			}
			return this._owner.GetHashCode() ^ this._member.GetHashCode();
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000F24D8 File Offset: 0x000F06D8
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000F24FC File Offset: 0x000F06FC
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		// Token: 0x04002B80 RID: 11136
		private object _owner;

		// Token: 0x04002B81 RID: 11137
		private MemberDescriptor _member;

		// Token: 0x04002B82 RID: 11138
		public static readonly MemberRelationship Empty;
	}
}
