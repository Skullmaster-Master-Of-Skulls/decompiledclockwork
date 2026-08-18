using System;

namespace System.Configuration
{
	// Token: 0x0200009D RID: 157
	internal abstract class Update
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0001D592 File Offset: 0x0001B792
		internal Update(string configKey, bool moved, string updatedXml)
		{
			this._configKey = configKey;
			this._moved = moved;
			this._updatedXml = updatedXml;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001D5AF File Offset: 0x0001B7AF
		internal string ConfigKey
		{
			get
			{
				return this._configKey;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
		internal bool Moved
		{
			get
			{
				return this._moved;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001D5BF File Offset: 0x0001B7BF
		internal string UpdatedXml
		{
			get
			{
				return this._updatedXml;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001D5C7 File Offset: 0x0001B7C7
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		internal bool Retrieved
		{
			get
			{
				return this._retrieved;
			}
			set
			{
				this._retrieved = value;
			}
		}

		// Token: 0x04000362 RID: 866
		private bool _moved;

		// Token: 0x04000363 RID: 867
		private bool _retrieved;

		// Token: 0x04000364 RID: 868
		private string _configKey;

		// Token: 0x04000365 RID: 869
		private string _updatedXml;
	}
}
