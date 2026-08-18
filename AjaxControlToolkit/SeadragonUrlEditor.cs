using System;
using System.Security.Permissions;
using System.Web.UI.Design;

namespace AjaxControlToolkit
{
	// Token: 0x0200018B RID: 395
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SeadragonUrlEditor : UrlEditor
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001C76A File Offset: 0x0001A96A
		protected override string Caption
		{
			get
			{
				return base.Caption;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001C772 File Offset: 0x0001A972
		protected override string Filter
		{
			get
			{
				return "DZI Files (*.dzi)|*.dzi|XML Files (*.xml)|*.xml";
			}
		}
	}
}
