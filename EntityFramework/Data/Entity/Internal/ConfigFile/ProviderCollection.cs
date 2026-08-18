using System;
using System.Configuration;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200029F RID: 671
	internal class ProviderCollection : ConfigurationElementCollection
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x00078E27 File Offset: 0x00077027
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderElement();
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00078E2E File Offset: 0x0007702E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderElement)element).InvariantName;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00078E3B File Offset: 0x0007703B
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00078E3E File Offset: 0x0007703E
		protected override string ElementName
		{
			get
			{
				return "provider";
			}
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00078E45 File Offset: 0x00077045
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (!this.ValidateProviderElement(element))
			{
				base.BaseAdd(element);
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00078E57 File Offset: 0x00077057
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!this.ValidateProviderElement(element))
			{
				base.BaseAdd(index, element);
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00078E6C File Offset: 0x0007706C
		private bool ValidateProviderElement(ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			ProviderElement providerElement = (ProviderElement)base.BaseGet(elementKey);
			if (providerElement != null && providerElement.ProviderTypeName != ((ProviderElement)element).ProviderTypeName)
			{
				throw new InvalidOperationException(Strings.ProviderInvariantRepeatedInConfig(elementKey));
			}
			return providerElement != null;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00078EBC File Offset: 0x000770BC
		public ProviderElement AddProvider(string invariantName, string providerTypeName)
		{
			ProviderElement providerElement = (ProviderElement)this.CreateNewElement();
			base.BaseAdd(providerElement);
			providerElement.InvariantName = invariantName;
			providerElement.ProviderTypeName = providerTypeName;
			return providerElement;
		}

		// Token: 0x04000859 RID: 2137
		private const string ProviderKey = "provider";
	}
}
