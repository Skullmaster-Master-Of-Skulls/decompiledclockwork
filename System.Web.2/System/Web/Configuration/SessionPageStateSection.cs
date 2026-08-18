using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000752 RID: 1874
	public sealed class SessionPageStateSection : ConfigurationSection
	{
		// Token: 0x06005A43 RID: 23107 RVA: 0x0013A9F8 File Offset: 0x00138BF8
		static SessionPageStateSection()
		{
			SessionPageStateSection._properties = new ConfigurationPropertyCollection();
			SessionPageStateSection._properties.Add(SessionPageStateSection._propHistorySize);
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x0013AA45 File Offset: 0x00138C45
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionPageStateSection._properties;
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x0013AA4C File Offset: 0x00138C4C
		// (set) Token: 0x06005A46 RID: 23110 RVA: 0x0013AA5E File Offset: 0x00138C5E
		[ConfigurationProperty("historySize", DefaultValue = 9)]
		[IntegerValidator(MinValue = 1)]
		public int HistorySize
		{
			get
			{
				return (int)base[SessionPageStateSection._propHistorySize];
			}
			set
			{
				base[SessionPageStateSection._propHistorySize] = value;
			}
		}

		// Token: 0x04002FD3 RID: 12243
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002FD4 RID: 12244
		public const int DefaultHistorySize = 9;

		// Token: 0x04002FD5 RID: 12245
		private static readonly ConfigurationProperty _propHistorySize = new ConfigurationProperty("historySize", typeof(int), 9, null, StdValidatorsAndConverters.NonZeroPositiveIntegerValidator, ConfigurationPropertyOptions.None);
	}
}
