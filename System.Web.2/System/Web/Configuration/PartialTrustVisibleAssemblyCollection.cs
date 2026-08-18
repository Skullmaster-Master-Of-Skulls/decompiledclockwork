using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000727 RID: 1831
	[ConfigurationCollection(typeof(string))]
	public sealed class PartialTrustVisibleAssemblyCollection : ConfigurationElementCollection
	{
		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06005842 RID: 22594 RVA: 0x00134EC0 File Offset: 0x001330C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PartialTrustVisibleAssemblyCollection._properties;
			}
		}

		// Token: 0x1700198B RID: 6539
		public PartialTrustVisibleAssembly this[int index]
		{
			get
			{
				return (PartialTrustVisibleAssembly)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(PartialTrustVisibleAssembly partialTrustVisibleAssembly)
		{
			this.BaseAdd(partialTrustVisibleAssembly);
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x00134ED5 File Offset: 0x001330D5
		protected override ConfigurationElement CreateNewElement()
		{
			return new PartialTrustVisibleAssembly();
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x00134EDC File Offset: 0x001330DC
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((PartialTrustVisibleAssembly)element).AssemblyName;
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x00117E47 File Offset: 0x00116047
		internal bool IsRemoved(string key)
		{
			return base.BaseIsRemoved(key);
		}

		// Token: 0x04002EEA RID: 12010
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
