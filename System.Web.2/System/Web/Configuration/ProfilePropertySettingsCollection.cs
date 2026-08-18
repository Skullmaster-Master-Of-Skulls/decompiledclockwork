using System;
using System.Configuration;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000735 RID: 1845
	[ConfigurationCollection(typeof(ProfilePropertySettings))]
	public class ProfilePropertySettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x060058F7 RID: 22775 RVA: 0x00136DF0 File Offset: 0x00134FF0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfilePropertySettingsCollection._properties;
			}
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x060058F9 RID: 22777 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool AllowClear
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x00136DF8 File Offset: 0x00134FF8
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (!this.AllowClear && elementName == "clear")
			{
				throw new ConfigurationErrorsException(SR.GetString("Clear_not_valid"), reader);
			}
			if (elementName == "group")
			{
				throw new ConfigurationErrorsException(SR.GetString("Nested_group_not_valid"), reader);
			}
			return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x170019C9 RID: 6601
		public ProfilePropertySettings this[string name]
		{
			get
			{
				return (ProfilePropertySettings)base.BaseGet(name);
			}
		}

		// Token: 0x170019CA RID: 6602
		public ProfilePropertySettings this[int index]
		{
			get
			{
				return (ProfilePropertySettings)base.BaseGet(index);
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

		// Token: 0x06005900 RID: 22784 RVA: 0x00136E6D File Offset: 0x0013506D
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfilePropertySettings();
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x00136E74 File Offset: 0x00135074
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfilePropertySettings)element).Name;
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ProfilePropertySettings propertySettings)
		{
			this.BaseAdd(propertySettings);
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x00136E5F File Offset: 0x0013505F
		public ProfilePropertySettings Get(int index)
		{
			return (ProfilePropertySettings)base.BaseGet(index);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00136E51 File Offset: 0x00135051
		public ProfilePropertySettings Get(string name)
		{
			return (ProfilePropertySettings)base.BaseGet(name);
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(ProfilePropertySettings propertySettings)
		{
			base.BaseAdd(propertySettings, false);
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0012E49C File Offset: 0x0012C69C
		public int IndexOf(ProfilePropertySettings propertySettings)
		{
			return base.BaseIndexOf(propertySettings);
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x04002F40 RID: 12096
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
