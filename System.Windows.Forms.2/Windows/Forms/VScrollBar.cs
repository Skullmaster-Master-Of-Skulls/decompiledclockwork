using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200042F RID: 1071
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionVScrollBar")]
	public class VScrollBar : ScrollBar
	{
		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x001378F8 File Offset: 0x00135AF8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 1;
				return createParams;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x0013791B File Offset: 0x00135B1B
		protected override Size DefaultSize
		{
			get
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					return new Size(SystemInformation.GetVerticalScrollBarWidthForDpi(this.deviceDpi), base.LogicalToDeviceUnits(80));
				}
				return new Size(SystemInformation.VerticalScrollBarWidth, 80);
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06004A1E RID: 18974 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return RightToLeft.No;
			}
			set
			{
			}
		}

		// Token: 0x140003B9 RID: 953
		// (add) Token: 0x06004A1F RID: 18975 RVA: 0x000E34AF File Offset: 0x000E16AF
		// (remove) Token: 0x06004A20 RID: 18976 RVA: 0x000E34B8 File Offset: 0x000E16B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		// Token: 0x040027D8 RID: 10200
		private const int VERTICAL_SCROLLBAR_HEIGHT = 80;
	}
}
