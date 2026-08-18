using System;

namespace System.Configuration
{
	// Token: 0x0200070E RID: 1806
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsSerializeAsAttribute : Attribute
	{
		// Token: 0x0600376B RID: 14187 RVA: 0x000EB577 File Offset: 0x000EA577
		public SettingsSerializeAsAttribute(SettingsSerializeAs serializeAs)
		{
			this._serializeAs = serializeAs;
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000EB586 File Offset: 0x000EA586
		public SettingsSerializeAs SerializeAs
		{
			get
			{
				return this._serializeAs;
			}
		}

		// Token: 0x040031D0 RID: 12752
		private readonly SettingsSerializeAs _serializeAs;
	}
}
