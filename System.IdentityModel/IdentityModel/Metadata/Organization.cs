using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000100 RID: 256
	public class Organization
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0001EF95 File Offset: 0x0001D195
		public Organization() : this(new LocalizedEntryCollection<LocalizedName>(), new LocalizedEntryCollection<LocalizedName>(), new LocalizedEntryCollection<LocalizedUri>())
		{
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001EFAC File Offset: 0x0001D1AC
		public Organization(LocalizedEntryCollection<LocalizedName> names, LocalizedEntryCollection<LocalizedName> displayNames, LocalizedEntryCollection<LocalizedUri> urls)
		{
			if (names == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("names");
			}
			if (displayNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("displayNames");
			}
			if (urls == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("urls");
			}
			this.names = names;
			this.displayNames = displayNames;
			this.urls = urls;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001F02E File Offset: 0x0001D22E
		public LocalizedEntryCollection<LocalizedName> DisplayNames
		{
			get
			{
				return this.displayNames;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001F036 File Offset: 0x0001D236
		public LocalizedEntryCollection<LocalizedName> Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001F03E File Offset: 0x0001D23E
		public LocalizedEntryCollection<LocalizedUri> Urls
		{
			get
			{
				return this.urls;
			}
		}

		// Token: 0x04000A8B RID: 2699
		private LocalizedEntryCollection<LocalizedName> displayNames = new LocalizedEntryCollection<LocalizedName>();

		// Token: 0x04000A8C RID: 2700
		private LocalizedEntryCollection<LocalizedName> names = new LocalizedEntryCollection<LocalizedName>();

		// Token: 0x04000A8D RID: 2701
		private LocalizedEntryCollection<LocalizedUri> urls = new LocalizedEntryCollection<LocalizedUri>();
	}
}
