using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006E5 RID: 1765
	[ConfigurationCollection(typeof(string))]
	public sealed class FullTrustAssemblyCollection : ConfigurationElementCollection
	{
		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x00128A2F File Offset: 0x00126C2F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FullTrustAssemblyCollection._properties;
			}
		}

		// Token: 0x17001839 RID: 6201
		public FullTrustAssembly this[int index]
		{
			get
			{
				return (FullTrustAssembly)base.BaseGet(index);
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

		// Token: 0x060054DA RID: 21722 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(FullTrustAssembly fullTrustAssembly)
		{
			this.BaseAdd(fullTrustAssembly);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00128A44 File Offset: 0x00126C44
		protected override ConfigurationElement CreateNewElement()
		{
			return new FullTrustAssembly();
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x00128A4B File Offset: 0x00126C4B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FullTrustAssembly)element).AssemblyName + ((FullTrustAssembly)element).Version;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00117E47 File Offset: 0x00116047
		internal bool IsRemoved(string key)
		{
			return base.BaseIsRemoved(key);
		}

		// Token: 0x04002C83 RID: 11395
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
