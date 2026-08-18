using System;

namespace System.Configuration
{
	// Token: 0x0200070D RID: 1805
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		// Token: 0x06003768 RID: 14184 RVA: 0x000EB549 File Offset: 0x000EA549
		public SettingsProviderAttribute(string providerTypeName)
		{
			this._providerTypeName = providerTypeName;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000EB558 File Offset: 0x000EA558
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType != null)
			{
				this._providerTypeName = providerType.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000EB56F File Offset: 0x000EA56F
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x040031CF RID: 12751
		private readonly string _providerTypeName;
	}
}
