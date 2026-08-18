using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006E0 RID: 1760
	[ConfigurationCollection(typeof(FormsAuthenticationUser), AddItemName = "user", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class FormsAuthenticationUserCollection : ConfigurationElementCollection
	{
		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x1700182C RID: 6188
		public FormsAuthenticationUser this[string name]
		{
			get
			{
				return (FormsAuthenticationUser)base.BaseGet(name);
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x060054B7 RID: 21687 RVA: 0x00128872 File Offset: 0x00126A72
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FormsAuthenticationUserCollection._properties;
			}
		}

		// Token: 0x1700182E RID: 6190
		public FormsAuthenticationUser this[int index]
		{
			get
			{
				return (FormsAuthenticationUser)base.BaseGet(index);
			}
			set
			{
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00128887 File Offset: 0x00126A87
		protected override ConfigurationElement CreateNewElement()
		{
			return new FormsAuthenticationUser();
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0012888E File Offset: 0x00126A8E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FormsAuthenticationUser)element).Name;
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x0012889B File Offset: 0x00126A9B
		protected override string ElementName
		{
			get
			{
				return "user";
			}
		}

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x060054BE RID: 21694 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(FormsAuthenticationUser user)
		{
			this.BaseAdd(user);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00128879 File Offset: 0x00126A79
		public FormsAuthenticationUser Get(int index)
		{
			return (FormsAuthenticationUser)base.BaseGet(index);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x00128864 File Offset: 0x00126A64
		public FormsAuthenticationUser Get(string name)
		{
			return (FormsAuthenticationUser)base.BaseGet(name);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(FormsAuthenticationUser user)
		{
			base.BaseAdd(user, false);
		}

		// Token: 0x04002C70 RID: 11376
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
