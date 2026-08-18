using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000720 RID: 1824
	[ConfigurationCollection(typeof(OutputCacheProfile))]
	public sealed class OutputCacheProfileCollection : ConfigurationElementCollection
	{
		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x060057CE RID: 22478 RVA: 0x00133801 File Offset: 0x00131A01
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return OutputCacheProfileCollection._properties;
			}
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x001240D1 File Offset: 0x001222D1
		public OutputCacheProfileCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x17001955 RID: 6485
		public OutputCacheProfile this[string name]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(name);
			}
		}

		// Token: 0x17001956 RID: 6486
		public OutputCacheProfile this[int index]
		{
			get
			{
				return (OutputCacheProfile)base.BaseGet(index);
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

		// Token: 0x060057D4 RID: 22484 RVA: 0x00133824 File Offset: 0x00131A24
		protected override ConfigurationElement CreateNewElement()
		{
			return new OutputCacheProfile();
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x0013382B File Offset: 0x00131A2B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((OutputCacheProfile)element).Name;
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(OutputCacheProfile name)
		{
			this.BaseAdd(name);
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x00133816 File Offset: 0x00131A16
		public OutputCacheProfile Get(int index)
		{
			return (OutputCacheProfile)base.BaseGet(index);
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00133808 File Offset: 0x00131A08
		public OutputCacheProfile Get(string name)
		{
			return (OutputCacheProfile)base.BaseGet(name);
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060057DC RID: 22492 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(OutputCacheProfile user)
		{
			base.BaseAdd(user, false);
		}

		// Token: 0x04002EA5 RID: 11941
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
