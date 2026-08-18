using System;
using System.Configuration;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000731 RID: 1841
	[ConfigurationCollection(typeof(ProfileGroupSettings), AddItemName = "group")]
	public sealed class ProfileGroupSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x060058C0 RID: 22720 RVA: 0x00136815 File Offset: 0x00134A15
		public ProfileGroupSettingsCollection()
		{
			base.AddElementName = "group";
			base.ClearElementName = string.Empty;
			base.EmitClear = false;
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x060058C1 RID: 22721 RVA: 0x0013683A File Offset: 0x00134A3A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileGroupSettingsCollection._properties;
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x170019B8 RID: 6584
		public ProfileGroupSettings this[string name]
		{
			get
			{
				return (ProfileGroupSettings)base.BaseGet(name);
			}
		}

		// Token: 0x170019B9 RID: 6585
		public ProfileGroupSettings this[int index]
		{
			get
			{
				return (ProfileGroupSettings)base.BaseGet(index);
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

		// Token: 0x060058C6 RID: 22726 RVA: 0x00126C26 File Offset: 0x00124E26
		internal void AddOrReplace(ProfileGroupSettings groupSettings)
		{
			base.BaseAdd(groupSettings, false);
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0013685D File Offset: 0x00134A5D
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProfileGroupSettings();
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x00136864 File Offset: 0x00134A64
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProfileGroupSettings)element).Name;
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x00136871 File Offset: 0x00134A71
		internal bool InternalIsModified()
		{
			return this.IsModified();
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x00136879 File Offset: 0x00134A79
		internal void InternalResetModified()
		{
			this.ResetModified();
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x00136881 File Offset: 0x00134A81
		internal void InternalReset(ConfigurationElement parentElement)
		{
			this.Reset(parentElement);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0013688C File Offset: 0x00134A8C
		internal void InternalUnMerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			this.Unmerge(sourceElement, parentElement, saveMode);
			base.BaseClear();
			ProfileGroupSettingsCollection profileGroupSettingsCollection = sourceElement as ProfileGroupSettingsCollection;
			ProfileGroupSettingsCollection profileGroupSettingsCollection2 = parentElement as ProfileGroupSettingsCollection;
			foreach (object obj in profileGroupSettingsCollection)
			{
				ProfileGroupSettings profileGroupSettings = (ProfileGroupSettings)obj;
				ProfileGroupSettings parentElement2 = profileGroupSettingsCollection2.Get(profileGroupSettings.Name);
				ProfileGroupSettings profileGroupSettings2 = new ProfileGroupSettings();
				profileGroupSettings2.InternalUnmerge(profileGroupSettings, parentElement2, saveMode);
				this.BaseAdd(profileGroupSettings2);
			}
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x00136924 File Offset: 0x00134B24
		internal bool InternalSerialize(XmlWriter writer, bool serializeCollectionKey)
		{
			if (base.EmitClear)
			{
				throw new ConfigurationErrorsException(SR.GetString("Clear_not_valid"));
			}
			return this.SerializeElement(writer, serializeCollectionKey);
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(ProfileGroupSettings group)
		{
			this.BaseAdd(group);
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0013684F File Offset: 0x00134A4F
		public ProfileGroupSettings Get(int index)
		{
			return (ProfileGroupSettings)base.BaseGet(index);
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x00136841 File Offset: 0x00134A41
		public ProfileGroupSettings Get(string name)
		{
			return (ProfileGroupSettings)base.BaseGet(name);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(ProfileGroupSettings group)
		{
			base.BaseAdd(group, false);
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0012E49C File Offset: 0x0012C69C
		public int IndexOf(ProfileGroupSettings group)
		{
			return base.BaseIndexOf(group);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00136948 File Offset: 0x00134B48
		public void Remove(string name)
		{
			ConfigurationElement configurationElement = base.BaseGet(name);
			if (configurationElement == null)
			{
				return;
			}
			ElementInformation elementInformation = configurationElement.ElementInformation;
			if (elementInformation.IsPresent)
			{
				base.BaseRemove(name);
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00136988 File Offset: 0x00134B88
		public void RemoveAt(int index)
		{
			ConfigurationElement configurationElement = base.BaseGet(index);
			if (configurationElement == null)
			{
				return;
			}
			ElementInformation elementInformation = configurationElement.ElementInformation;
			if (elementInformation.IsPresent)
			{
				base.BaseRemoveAt(index);
				return;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_base_cannot_remove_inherited_items"));
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x001369C8 File Offset: 0x00134BC8
		public void Clear()
		{
			int num = base.Count - 1;
			this.bModified = true;
			for (int i = num; i >= 0; i--)
			{
				ConfigurationElement configurationElement = base.BaseGet(i);
				if (configurationElement != null)
				{
					ElementInformation elementInformation = configurationElement.ElementInformation;
					if (elementInformation.IsPresent)
					{
						base.BaseRemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x00136A12 File Offset: 0x00134C12
		protected override void ResetModified()
		{
			this.bModified = false;
			base.ResetModified();
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00136A21 File Offset: 0x00134C21
		protected override bool IsModified()
		{
			return this.bModified || base.IsModified();
		}

		// Token: 0x04002F2F RID: 12079
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F30 RID: 12080
		private bool bModified;
	}
}
