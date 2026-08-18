using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x02000272 RID: 626
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class XhtmlConformanceSection : ConfigurationSection
	{
		// Token: 0x060020B7 RID: 8375 RVA: 0x0008E52D File Offset: 0x0008D52D
		static XhtmlConformanceSection()
		{
			XhtmlConformanceSection._properties = new ConfigurationPropertyCollection();
			XhtmlConformanceSection._properties.Add(XhtmlConformanceSection._propMode);
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0008E568 File Offset: 0x0008D568
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return XhtmlConformanceSection._properties;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0008E56F File Offset: 0x0008D56F
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x0008E581 File Offset: 0x0008D581
		[ConfigurationProperty("mode", DefaultValue = XhtmlConformanceMode.Transitional)]
		public XhtmlConformanceMode Mode
		{
			get
			{
				return (XhtmlConformanceMode)base[XhtmlConformanceSection._propMode];
			}
			set
			{
				base[XhtmlConformanceSection._propMode] = value;
			}
		}

		// Token: 0x04001AC0 RID: 6848
		internal const XhtmlConformanceMode DefaultMode = XhtmlConformanceMode.Transitional;

		// Token: 0x04001AC1 RID: 6849
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04001AC2 RID: 6850
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("mode", typeof(XhtmlConformanceMode), XhtmlConformanceMode.Transitional, ConfigurationPropertyOptions.None);
	}
}
