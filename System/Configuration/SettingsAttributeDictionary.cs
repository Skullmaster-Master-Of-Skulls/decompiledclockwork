using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000704 RID: 1796
	[Serializable]
	public class SettingsAttributeDictionary : Hashtable
	{
		// Token: 0x06003759 RID: 14169 RVA: 0x000EB4AD File Offset: 0x000EA4AD
		public SettingsAttributeDictionary()
		{
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000EB4B5 File Offset: 0x000EA4B5
		public SettingsAttributeDictionary(SettingsAttributeDictionary attributes) : base(attributes)
		{
		}
	}
}
