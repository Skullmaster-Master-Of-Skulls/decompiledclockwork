using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200008F RID: 143
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlUrlEditor : UrlEditor
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00013FE9 File Offset: 0x000121E9
		protected override string Caption
		{
			get
			{
				return SR.GetString("UrlPicker_XmlCaption");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013FF5 File Offset: 0x000121F5
		protected override string Filter
		{
			get
			{
				return SR.GetString("UrlPicker_XmlFilter");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.NoAbsolute;
			}
		}
	}
}
