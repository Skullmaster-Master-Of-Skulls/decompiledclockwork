using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000039 RID: 57
	[SecurityCritical]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	internal class DesignTimeHtmlTextWriter : HtmlTextWriter
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public DesignTimeHtmlTextWriter(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000DBB1 File Offset: 0x0000BDB1
		public DesignTimeHtmlTextWriter(TextWriter writer, string tabString) : base(writer, tabString)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000DBBB File Offset: 0x0000BDBB
		public override void AddAttribute(HtmlTextWriterAttribute key, string value)
		{
			if (key == HtmlTextWriterAttribute.Src || key == HtmlTextWriterAttribute.Href || key == HtmlTextWriterAttribute.Background)
			{
				base.AddAttribute(key.ToString(), value, key);
				return;
			}
			base.AddAttribute(key, value);
		}
	}
}
