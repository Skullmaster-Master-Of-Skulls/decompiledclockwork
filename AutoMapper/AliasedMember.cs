using System;

namespace AutoMapper
{
	// Token: 0x02000007 RID: 7
	public class AliasedMember
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002668 File Offset: 0x00000868
		public AliasedMember(string member, string alias)
		{
			this.Member = member;
			this.Alias = alias;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000267E File Offset: 0x0000087E
		public string Member { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002686 File Offset: 0x00000886
		public string Alias { get; }

		// Token: 0x06000022 RID: 34 RVA: 0x0000268E File Offset: 0x0000088E
		public bool Equals(AliasedMember other)
		{
			return other != null && (this == other || (object.Equals(other.Member, this.Member) && object.Equals(other.Alias, this.Alias)));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026C1 File Offset: 0x000008C1
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(AliasedMember)) && this.Equals((AliasedMember)obj)));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026F3 File Offset: 0x000008F3
		public override int GetHashCode()
		{
			return this.Member.GetHashCode() * 397 ^ this.Alias.GetHashCode();
		}
	}
}
