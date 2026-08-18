using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000717 RID: 1815
	public sealed class MembershipSection : ConfigurationSection
	{
		// Token: 0x0600575F RID: 22367 RVA: 0x00132468 File Offset: 0x00130668
		static MembershipSection()
		{
			MembershipSection._properties = new ConfigurationPropertyCollection();
			MembershipSection._properties.Add(MembershipSection._propProviders);
			MembershipSection._properties.Add(MembershipSection._propDefaultProvider);
			MembershipSection._properties.Add(MembershipSection._propUserIsOnlineTimeWindow);
			MembershipSection._properties.Add(MembershipSection._propHashAlgorithmType);
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06005761 RID: 22369 RVA: 0x00132564 File Offset: 0x00130764
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return MembershipSection._properties;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06005762 RID: 22370 RVA: 0x0013256B File Offset: 0x0013076B
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[MembershipSection._propProviders];
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06005763 RID: 22371 RVA: 0x0013257D File Offset: 0x0013077D
		// (set) Token: 0x06005764 RID: 22372 RVA: 0x0013258F File Offset: 0x0013078F
		[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlMembershipProvider")]
		[StringValidator(MinLength = 1)]
		public string DefaultProvider
		{
			get
			{
				return (string)base[MembershipSection._propDefaultProvider];
			}
			set
			{
				base[MembershipSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x0013259D File Offset: 0x0013079D
		// (set) Token: 0x06005766 RID: 22374 RVA: 0x001325AF File Offset: 0x001307AF
		[ConfigurationProperty("hashAlgorithmType", DefaultValue = "")]
		public string HashAlgorithmType
		{
			get
			{
				return (string)base[MembershipSection._propHashAlgorithmType];
			}
			set
			{
				base[MembershipSection._propHashAlgorithmType] = value;
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x001325C0 File Offset: 0x001307C0
		internal void ThrowHashAlgorithmException()
		{
			throw new ConfigurationErrorsException(SR.GetString("Invalid_hash_algorithm_type", new object[]
			{
				this.HashAlgorithmType
			}), base.ElementInformation.Properties["hashAlgorithmType"].Source, base.ElementInformation.Properties["hashAlgorithmType"].LineNumber);
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06005768 RID: 22376 RVA: 0x0013261F File Offset: 0x0013081F
		// (set) Token: 0x06005769 RID: 22377 RVA: 0x00132631 File Offset: 0x00130831
		[ConfigurationProperty("userIsOnlineTimeWindow", DefaultValue = "00:15:00")]
		[TypeConverter(typeof(TimeSpanMinutesConverter))]
		[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
		public TimeSpan UserIsOnlineTimeWindow
		{
			get
			{
				return (TimeSpan)base[MembershipSection._propUserIsOnlineTimeWindow];
			}
			set
			{
				base[MembershipSection._propUserIsOnlineTimeWindow] = value;
			}
		}

		// Token: 0x04002E7D RID: 11901
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E7E RID: 11902
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002E7F RID: 11903
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlMembershipProvider", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002E80 RID: 11904
		private static readonly ConfigurationProperty _propUserIsOnlineTimeWindow = new ConfigurationProperty("userIsOnlineTimeWindow", typeof(TimeSpan), TimeSpan.FromMinutes(15.0), StdValidatorsAndConverters.TimeSpanMinutesConverter, new TimeSpanValidator(TimeSpan.FromMinutes(1.0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x04002E81 RID: 11905
		private static readonly ConfigurationProperty _propHashAlgorithmType = new ConfigurationProperty("hashAlgorithmType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
