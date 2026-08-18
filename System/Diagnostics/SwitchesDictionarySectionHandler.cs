using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200074A RID: 1866
	internal class SwitchesDictionarySectionHandler : DictionarySectionHandler
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000F0820 File Offset: 0x000EF820
		protected override string KeyAttributeName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000F0827 File Offset: 0x000EF827
		internal override bool ValueRequired
		{
			get
			{
				return true;
			}
		}
	}
}
