using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000278 RID: 632
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionHScrollBar")]
	public class HScrollBar : ScrollBar
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000BAED0 File Offset: 0x000B90D0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 0;
				return createParams;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000BAEF3 File Offset: 0x000B90F3
		protected override Size DefaultSize
		{
			get
			{
				return new Size(80, SystemInformation.HorizontalScrollBarHeight);
			}
		}
	}
}
