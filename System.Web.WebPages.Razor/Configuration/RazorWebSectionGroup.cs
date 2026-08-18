using System;
using System.Configuration;

namespace System.Web.WebPages.Razor.Configuration
{
	// Token: 0x02000008 RID: 8
	public class RazorWebSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023D3 File Offset: 0x000005D3
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023F9 File Offset: 0x000005F9
		[ConfigurationProperty("host", IsRequired = false)]
		public HostSection Host
		{
			get
			{
				if (!this._hostSet)
				{
					return (HostSection)base.Sections["host"];
				}
				return this._host;
			}
			set
			{
				this._host = value;
				this._hostSet = true;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002409 File Offset: 0x00000609
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000242F File Offset: 0x0000062F
		[ConfigurationProperty("pages", IsRequired = false)]
		public RazorPagesSection Pages
		{
			get
			{
				if (!this._pagesSet)
				{
					return (RazorPagesSection)base.Sections["pages"];
				}
				return this._pages;
			}
			set
			{
				this._pages = value;
				this._pagesSet = true;
			}
		}

		// Token: 0x04000012 RID: 18
		public static readonly string GroupName = "system.web.webPages.razor";

		// Token: 0x04000013 RID: 19
		private bool _hostSet;

		// Token: 0x04000014 RID: 20
		private bool _pagesSet;

		// Token: 0x04000015 RID: 21
		private HostSection _host;

		// Token: 0x04000016 RID: 22
		private RazorPagesSection _pages;
	}
}
