using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000738 RID: 1848
	[ConfigurationCollection(typeof(ProfileSettings))]
	public sealed class ProfileSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x0600592C RID: 22828 RVA: 0x001373C3 File Offset: 0x001355C3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSettingsCollection._properties;
			}
		}

		// Token: 0x170019DA RID: 6618
		public ProfileSettings this[int index]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(index);
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

		// Token: 0x170019DB RID: 6619
		public ProfileSettings this[string key]
		{
			get
			{
				return (ProfileSettings)base.BaseGet(key);
			}
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x001373E6 File Offset: 0x001355E6
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileSettings();
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x001373ED File Offset: 0x001355ED
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileSettings)element).Name;
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ProfileSettings profilesSettings)
		{
			this.BaseAdd(profilesSettings);
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x00118E82 File Offset: 0x00117082
		public void Insert(int index, ProfileSettings authorizationSettings)
		{
			this.BaseAdd(index, authorizationSettings);
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x001373FC File Offset: 0x001355FC
		public int IndexOf(string name)
		{
			ConfigurationElement configurationElement = base.BaseGet(name);
			if (configurationElement == null)
			{
				return -1;
			}
			return base.BaseIndexOf(configurationElement);
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0013741D File Offset: 0x0013561D
		public bool Contains(string name)
		{
			return this.IndexOf(name) != -1;
		}

		// Token: 0x04002F50 RID: 12112
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
