using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x02000193 RID: 403
	internal static class CompModSwitches
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00054716 File Offset: 0x00052916
		public static BooleanSwitch CommonDesignerServices
		{
			get
			{
				if (CompModSwitches.commonDesignerServices == null)
				{
					CompModSwitches.commonDesignerServices = new BooleanSwitch("CommonDesignerServices", "Assert if any common designer service is not found.");
				}
				return CompModSwitches.commonDesignerServices;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00054738 File Offset: 0x00052938
		public static TraceSwitch DragDrop
		{
			get
			{
				if (CompModSwitches.dragDrop == null)
				{
					CompModSwitches.dragDrop = new TraceSwitch("DragDrop", "Debug OLEDragDrop support in Controls");
				}
				return CompModSwitches.dragDrop;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0005475A File Offset: 0x0005295A
		public static TraceSwitch MSAA
		{
			get
			{
				if (CompModSwitches.msaa == null)
				{
					CompModSwitches.msaa = new TraceSwitch("MSAA", "Debug Microsoft Active Accessibility");
				}
				return CompModSwitches.msaa;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0005477C File Offset: 0x0005297C
		public static TraceSwitch UserControlDesigner
		{
			get
			{
				if (CompModSwitches.userControlDesigner == null)
				{
					CompModSwitches.userControlDesigner = new TraceSwitch("UserControlDesigner", "User Control Designer : Trace service calls.");
				}
				return CompModSwitches.userControlDesigner;
			}
		}

		// Token: 0x040008A1 RID: 2209
		private static BooleanSwitch commonDesignerServices;

		// Token: 0x040008A2 RID: 2210
		private static TraceSwitch userControlDesigner;

		// Token: 0x040008A3 RID: 2211
		private static TraceSwitch dragDrop;

		// Token: 0x040008A4 RID: 2212
		private static TraceSwitch msaa;
	}
}
