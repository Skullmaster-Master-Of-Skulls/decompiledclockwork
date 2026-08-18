using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004C7 RID: 1223
	internal class SwitchesDictionarySectionHandler : DictionarySectionHandler
	{
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000CDA4C File Offset: 0x000CBC4C
		protected override string KeyAttributeName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x000CDA53 File Offset: 0x000CBC53
		internal override bool ValueRequired
		{
			get
			{
				return true;
			}
		}
	}
}
