using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x0200024F RID: 591
	internal abstract class StoragePropertyMapping
	{
		// Token: 0x060024FA RID: 9466 RVA: 0x0008A010 File Offset: 0x00088210
		internal StoragePropertyMapping(EdmProperty cdmMember)
		{
			this.m_cdmMember = cdmMember;
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x0008A01F File Offset: 0x0008821F
		internal virtual EdmProperty EdmProperty
		{
			get
			{
				return this.m_cdmMember;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void Print(int index)
		{
		}

		// Token: 0x0400110D RID: 4365
		private EdmProperty m_cdmMember;
	}
}
