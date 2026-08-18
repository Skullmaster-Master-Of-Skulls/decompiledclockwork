using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003CA RID: 970
	internal abstract class ObjectMemberMapping
	{
		// Token: 0x0600236C RID: 9068 RVA: 0x000A5206 File Offset: 0x000A3406
		protected ObjectMemberMapping(EdmMember edmMember, EdmMember clrMember)
		{
			this.m_edmMember = edmMember;
			this.m_clrMember = clrMember;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000A521C File Offset: 0x000A341C
		internal EdmMember EdmMember
		{
			get
			{
				return this.m_edmMember;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000A5224 File Offset: 0x000A3424
		internal EdmMember ClrMember
		{
			get
			{
				return this.m_clrMember;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600236F RID: 9071
		internal abstract MemberMappingKind MemberMappingKind { get; }

		// Token: 0x04000C75 RID: 3189
		private readonly EdmMember m_edmMember;

		// Token: 0x04000C76 RID: 3190
		private readonly EdmMember m_clrMember;
	}
}
