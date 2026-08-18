using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200069A RID: 1690
	[ConfigurationCollection(typeof(AssemblyInfo))]
	public sealed class AssemblyCollection : ConfigurationElementCollection
	{
		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x00117DD3 File Offset: 0x00115FD3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyCollection._properties;
			}
		}

		// Token: 0x17001756 RID: 5974
		public AssemblyInfo this[int index]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(index);
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

		// Token: 0x17001757 RID: 5975
		public AssemblyInfo this[string assemblyName]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(assemblyName);
			}
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(AssemblyInfo assemblyInformation)
		{
			this.BaseAdd(assemblyInformation);
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00117E2B File Offset: 0x0011602B
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyInfo();
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x00117E32 File Offset: 0x00116032
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyInfo)element).Assembly;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x00117E47 File Offset: 0x00116047
		internal bool IsRemoved(string key)
		{
			return base.BaseIsRemoved(key);
		}

		// Token: 0x04002AFA RID: 11002
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
