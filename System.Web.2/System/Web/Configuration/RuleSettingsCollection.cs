using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200074C RID: 1868
	[ConfigurationCollection(typeof(RuleSettings))]
	public sealed class RuleSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x00139E23 File Offset: 0x00138023
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RuleSettingsCollection._properties;
			}
		}

		// Token: 0x17001A0A RID: 6666
		public RuleSettings this[int index]
		{
			get
			{
				return (RuleSettings)base.BaseGet(index);
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

		// Token: 0x17001A0B RID: 6667
		public RuleSettings this[string key]
		{
			get
			{
				return (RuleSettings)base.BaseGet(key);
			}
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00139E46 File Offset: 0x00138046
		protected override ConfigurationElement CreateNewElement()
		{
			return new RuleSettings();
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x00139E4D File Offset: 0x0013804D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((RuleSettings)element).Name;
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(RuleSettings ruleSettings)
		{
			this.BaseAdd(ruleSettings);
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x00118E82 File Offset: 0x00117082
		public void Insert(int index, RuleSettings eventSettings)
		{
			this.BaseAdd(index, eventSettings);
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x00139E5C File Offset: 0x0013805C
		public int IndexOf(string name)
		{
			ConfigurationElement configurationElement = base.BaseGet(name);
			if (configurationElement == null)
			{
				return -1;
			}
			return base.BaseIndexOf(configurationElement);
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00139E7D File Offset: 0x0013807D
		public bool Contains(string name)
		{
			return this.IndexOf(name) != -1;
		}

		// Token: 0x04002FBD RID: 12221
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
