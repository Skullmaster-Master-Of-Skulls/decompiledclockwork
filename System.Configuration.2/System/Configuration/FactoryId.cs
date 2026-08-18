using System;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x0200005E RID: 94
	[DebuggerDisplay("FactoryId {ConfigKey}")]
	internal class FactoryId
	{
		// Token: 0x0600039E RID: 926 RVA: 0x000138C4 File Offset: 0x00011AC4
		internal FactoryId(string configKey, string group, string name)
		{
			this._configKey = configKey;
			this._group = group;
			this._name = name;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000138E1 File Offset: 0x00011AE1
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000138E9 File Offset: 0x00011AE9
		internal string Group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000138F1 File Offset: 0x00011AF1
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000269 RID: 617
		private string _configKey;

		// Token: 0x0400026A RID: 618
		private string _group;

		// Token: 0x0400026B RID: 619
		private string _name;
	}
}
