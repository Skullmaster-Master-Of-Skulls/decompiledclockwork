using System;

namespace System.Configuration
{
	// Token: 0x02000081 RID: 129
	[ConfigurationCollection(typeof(ProviderSettings))]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00012884 File Offset: 0x00010A84
		public ProviderSettingsCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00019DDD File Offset: 0x00017FDD
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettingsCollection._properties;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public void Add(ProviderSettings provider)
		{
			if (provider != null)
			{
				provider.UpdatePropertyCollection();
				this.BaseAdd(provider);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00012911 File Offset: 0x00010B11
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001292E File Offset: 0x00010B2E
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00019DF7 File Offset: 0x00017FF7
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019DFE File Offset: 0x00017FFE
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		// Token: 0x17000172 RID: 370
		public ProviderSettings this[string key]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(key);
			}
		}

		// Token: 0x17000173 RID: 371
		public ProviderSettings this[int index]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(index);
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

		// Token: 0x040002DB RID: 731
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
