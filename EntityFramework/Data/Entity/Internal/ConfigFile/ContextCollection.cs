using System;
using System.Configuration;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x020006BB RID: 1723
	[SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
	internal class ContextCollection : ConfigurationElementCollection
	{
		// Token: 0x06004492 RID: 17554 RVA: 0x001444B1 File Offset: 0x001426B1
		protected override ConfigurationElement CreateNewElement()
		{
			return new ContextElement();
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x001444B8 File Offset: 0x001426B8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContextElement)element).ContextTypeName;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x001444C5 File Offset: 0x001426C5
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x001444C8 File Offset: 0x001426C8
		protected override string ElementName
		{
			get
			{
				return "context";
			}
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001444D0 File Offset: 0x001426D0
		protected override void BaseAdd(ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (base.BaseGet(elementKey) != null)
			{
				throw Error.ContextConfiguredMultipleTimes(elementKey);
			}
			base.BaseAdd(element);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x001444FC File Offset: 0x001426FC
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (base.BaseGet(elementKey) != null)
			{
				throw Error.ContextConfiguredMultipleTimes(elementKey);
			}
			base.BaseAdd(index, element);
		}

		// Token: 0x04001942 RID: 6466
		private const string ContextKey = "context";
	}
}
