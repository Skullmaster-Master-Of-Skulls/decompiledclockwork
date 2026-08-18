using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x0200022C RID: 556
	internal abstract class ObjectMemberMapping
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x00081600 File Offset: 0x0007F800
		protected ObjectMemberMapping(EdmMember edmMember, EdmMember clrMember)
		{
			this.m_edmMember = edmMember;
			this.m_clrMember = clrMember;
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x00081616 File Offset: 0x0007F816
		internal EdmMember EdmMember
		{
			get
			{
				return this.m_edmMember;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0008161E File Offset: 0x0007F81E
		internal EdmMember ClrMember
		{
			get
			{
				return this.m_clrMember;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060023D0 RID: 9168
		internal abstract MemberMappingKind MemberMappingKind { get; }

		// Token: 0x04000FE3 RID: 4067
		private EdmMember m_edmMember;

		// Token: 0x04000FE4 RID: 4068
		private EdmMember m_clrMember;
	}
}
