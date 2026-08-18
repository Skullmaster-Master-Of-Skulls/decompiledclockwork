using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000736 RID: 1846
	public sealed class ProfileSection : ConfigurationSection
	{
		// Token: 0x0600590B RID: 22795 RVA: 0x00136E84 File Offset: 0x00135084
		static ProfileSection()
		{
			ProfileSection._properties = new ConfigurationPropertyCollection();
			ProfileSection._properties.Add(ProfileSection._propEnabled);
			ProfileSection._properties.Add(ProfileSection._propDefaultProvider);
			ProfileSection._properties.Add(ProfileSection._propProviders);
			ProfileSection._properties.Add(ProfileSection._propProfile);
			ProfileSection._properties.Add(ProfileSection._propInherits);
			ProfileSection._properties.Add(ProfileSection._propAutomaticSaveEnabled);
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x00136FAF File Offset: 0x001351AF
		internal long RecompilationHash
		{
			get
			{
				if (!this._recompilationHashCached)
				{
					this._recompilationHash = this.CalculateHash();
					this._recompilationHashCached = true;
				}
				return this._recompilationHash;
			}
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x00136FD4 File Offset: 0x001351D4
		private long CalculateHash()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			this.CalculateProfilePropertySettingsHash(this.PropertySettings, hashCodeCombiner);
			if (this.PropertySettings != null)
			{
				foreach (object obj in this.PropertySettings.GroupSettings)
				{
					ProfileGroupSettings profileGroupSettings = (ProfileGroupSettings)obj;
					hashCodeCombiner.AddObject(profileGroupSettings.Name);
					this.CalculateProfilePropertySettingsHash(profileGroupSettings.PropertySettings, hashCodeCombiner);
				}
			}
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x00137068 File Offset: 0x00135268
		private void CalculateProfilePropertySettingsHash(ProfilePropertySettingsCollection settings, HashCodeCombiner hashCombiner)
		{
			foreach (object obj in settings)
			{
				ProfilePropertySettings profilePropertySettings = (ProfilePropertySettings)obj;
				hashCombiner.AddObject(profilePropertySettings.Name);
				hashCombiner.AddObject(profilePropertySettings.Type);
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x001370D0 File Offset: 0x001352D0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProfileSection._properties;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06005911 RID: 22801 RVA: 0x001370D7 File Offset: 0x001352D7
		// (set) Token: 0x06005912 RID: 22802 RVA: 0x001370E9 File Offset: 0x001352E9
		[ConfigurationProperty("automaticSaveEnabled", DefaultValue = true)]
		public bool AutomaticSaveEnabled
		{
			get
			{
				return (bool)base[ProfileSection._propAutomaticSaveEnabled];
			}
			set
			{
				base[ProfileSection._propAutomaticSaveEnabled] = value;
			}
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x001370FC File Offset: 0x001352FC
		// (set) Token: 0x06005914 RID: 22804 RVA: 0x0013710E File Offset: 0x0013530E
		[ConfigurationProperty("enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base[ProfileSection._propEnabled];
			}
			set
			{
				base[ProfileSection._propEnabled] = value;
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x00137121 File Offset: 0x00135321
		// (set) Token: 0x06005916 RID: 22806 RVA: 0x00137133 File Offset: 0x00135333
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlProfileProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProfileSection._propDefaultProvider];
			}
			set
			{
				base[ProfileSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x00137141 File Offset: 0x00135341
		// (set) Token: 0x06005918 RID: 22808 RVA: 0x00137153 File Offset: 0x00135353
		[ConfigurationProperty("inherits", DefaultValue = "")]
		public string Inherits
		{
			get
			{
				return (string)base[ProfileSection._propInherits];
			}
			set
			{
				base[ProfileSection._propInherits] = value;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x00137161 File Offset: 0x00135361
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProfileSection._propProviders];
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x00137173 File Offset: 0x00135373
		[ConfigurationProperty("properties")]
		public RootProfilePropertySettingsCollection PropertySettings
		{
			get
			{
				return (RootProfilePropertySettingsCollection)base[ProfileSection._propProfile];
			}
		}

		// Token: 0x04002F41 RID: 12097
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002F42 RID: 12098
		private static readonly ConfigurationProperty _propEnabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F43 RID: 12099
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlProfileProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002F44 RID: 12100
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F45 RID: 12101
		private static readonly ConfigurationProperty _propProfile = new ConfigurationProperty("properties", typeof(RootProfilePropertySettingsCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002F46 RID: 12102
		private static readonly ConfigurationProperty _propInherits = new ConfigurationProperty("inherits", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002F47 RID: 12103
		private static readonly ConfigurationProperty _propAutomaticSaveEnabled = new ConfigurationProperty("automaticSaveEnabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002F48 RID: 12104
		private long _recompilationHash;

		// Token: 0x04002F49 RID: 12105
		private bool _recompilationHashCached;
	}
}
