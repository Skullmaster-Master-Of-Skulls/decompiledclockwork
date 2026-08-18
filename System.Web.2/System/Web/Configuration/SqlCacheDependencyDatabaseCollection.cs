using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000756 RID: 1878
	[ConfigurationCollection(typeof(SqlCacheDependencyDatabase))]
	public sealed class SqlCacheDependencyDatabaseCollection : ConfigurationElementCollection
	{
		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06005A8C RID: 23180 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x17001A5E RID: 6750
		public SqlCacheDependencyDatabase this[string name]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(name);
			}
		}

		// Token: 0x17001A5F RID: 6751
		public SqlCacheDependencyDatabase this[int index]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(index);
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

		// Token: 0x06005A90 RID: 23184 RVA: 0x0013B738 File Offset: 0x00139938
		protected override ConfigurationElement CreateNewElement()
		{
			return new SqlCacheDependencyDatabase();
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x0013B73F File Offset: 0x0013993F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SqlCacheDependencyDatabase)element).Name;
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(SqlCacheDependencyDatabase name)
		{
			this.BaseAdd(name);
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x0013B72A File Offset: 0x0013992A
		public SqlCacheDependencyDatabase Get(int index)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(index);
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0013B71C File Offset: 0x0013991C
		public SqlCacheDependencyDatabase Get(string name)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(name);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(SqlCacheDependencyDatabase user)
		{
			base.BaseAdd(user, false);
		}

		// Token: 0x04002FF9 RID: 12281
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
