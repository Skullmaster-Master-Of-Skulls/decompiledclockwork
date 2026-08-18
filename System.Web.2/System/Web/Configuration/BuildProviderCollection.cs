using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006AF RID: 1711
	[ConfigurationCollection(typeof(BuildProvider))]
	public sealed class BuildProviderCollection : ConfigurationElementCollection
	{
		// Token: 0x060052F5 RID: 21237 RVA: 0x001240D1 File Offset: 0x001222D1
		public BuildProviderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x001240DE File Offset: 0x001222DE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProviderCollection._properties;
			}
		}

		// Token: 0x1700179E RID: 6046
		public BuildProvider this[string name]
		{
			get
			{
				return (BuildProvider)base.BaseGet(name);
			}
		}

		// Token: 0x1700179F RID: 6047
		public BuildProvider this[int index]
		{
			get
			{
				return (BuildProvider)base.BaseGet(index);
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

		// Token: 0x060052FA RID: 21242 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(BuildProvider buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00124101 File Offset: 0x00122301
		protected override ConfigurationElement CreateNewElement()
		{
			return new BuildProvider();
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00124108 File Offset: 0x00122308
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BuildProvider)element).Extension;
		}

		// Token: 0x04002B7E RID: 11134
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
