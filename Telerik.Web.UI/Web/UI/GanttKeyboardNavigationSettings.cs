using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200032B RID: 811
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GanttKeyboardNavigationSettings : ObjectWithState
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x00056FCE File Offset: 0x000551CE
		public GanttKeyboardNavigationSettings(StateBag ownerStateBag) : base("KeyboradNavigationSettings", ownerStateBag)
		{
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00056FDC File Offset: 0x000551DC
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00056FFD File Offset: 0x000551FD
		[Description("This property sets the key that is used to focus RadGantt. It is always used in combination with FocusKey.")]
		[DefaultValue(typeof(GanttKeyboardNavigationSettings.GanttCommandKey), "Alt")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual GanttKeyboardNavigationSettings.GanttCommandKey CommandKey
		{
			get
			{
				return (GanttKeyboardNavigationSettings.GanttCommandKey)(base.ViewState["CommandKey"] ?? GanttKeyboardNavigationSettings.GanttCommandKey.Alt);
			}
			set
			{
				base.ViewState["CommandKey"] = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00057015 File Offset: 0x00055215
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x00057037 File Offset: 0x00055237
		[Description("This property sets the key that is used to focus RadGantt. It is always used in combination with CommandKey.")]
		[Category("Client")]
		[DefaultValue(typeof(GanttKeyboardNavigationSettings.GanttFocusKey), "G")]
		[NotifyParentProperty(true)]
		public virtual GanttKeyboardNavigationSettings.GanttFocusKey FocusKey
		{
			get
			{
				return (GanttKeyboardNavigationSettings.GanttFocusKey)(base.ViewState["FocusKey"] ?? GanttKeyboardNavigationSettings.GanttFocusKey.G);
			}
			set
			{
				base.ViewState["FocusKey"] = value;
			}
		}

		// Token: 0x0200032C RID: 812
		[Flags]
		public enum GanttFocusKey
		{
			// Token: 0x040006D0 RID: 1744
			A = 65,
			// Token: 0x040006D1 RID: 1745
			B = 66,
			// Token: 0x040006D2 RID: 1746
			C = 67,
			// Token: 0x040006D3 RID: 1747
			D = 68,
			// Token: 0x040006D4 RID: 1748
			D0 = 48,
			// Token: 0x040006D5 RID: 1749
			D1 = 49,
			// Token: 0x040006D6 RID: 1750
			D2 = 50,
			// Token: 0x040006D7 RID: 1751
			D3 = 51,
			// Token: 0x040006D8 RID: 1752
			D4 = 52,
			// Token: 0x040006D9 RID: 1753
			D5 = 53,
			// Token: 0x040006DA RID: 1754
			D6 = 54,
			// Token: 0x040006DB RID: 1755
			D7 = 55,
			// Token: 0x040006DC RID: 1756
			D8 = 56,
			// Token: 0x040006DD RID: 1757
			D9 = 57,
			// Token: 0x040006DE RID: 1758
			E = 69,
			// Token: 0x040006DF RID: 1759
			F = 70,
			// Token: 0x040006E0 RID: 1760
			G = 71,
			// Token: 0x040006E1 RID: 1761
			H = 72,
			// Token: 0x040006E2 RID: 1762
			I = 73,
			// Token: 0x040006E3 RID: 1763
			J = 74,
			// Token: 0x040006E4 RID: 1764
			K = 75,
			// Token: 0x040006E5 RID: 1765
			L = 76,
			// Token: 0x040006E6 RID: 1766
			M = 77,
			// Token: 0x040006E7 RID: 1767
			N = 78,
			// Token: 0x040006E8 RID: 1768
			O = 79,
			// Token: 0x040006E9 RID: 1769
			P = 80,
			// Token: 0x040006EA RID: 1770
			Q = 81,
			// Token: 0x040006EB RID: 1771
			R = 82,
			// Token: 0x040006EC RID: 1772
			S = 83,
			// Token: 0x040006ED RID: 1773
			T = 84,
			// Token: 0x040006EE RID: 1774
			U = 85,
			// Token: 0x040006EF RID: 1775
			V = 86,
			// Token: 0x040006F0 RID: 1776
			W = 87,
			// Token: 0x040006F1 RID: 1777
			X = 88,
			// Token: 0x040006F2 RID: 1778
			Y = 89,
			// Token: 0x040006F3 RID: 1779
			Z = 90
		}

		// Token: 0x0200032D RID: 813
		[Flags]
		public enum GanttCommandKey
		{
			// Token: 0x040006F5 RID: 1781
			Alt = 1,
			// Token: 0x040006F6 RID: 1782
			Ctrl = 2,
			// Token: 0x040006F7 RID: 1783
			AltCtrl = 3,
			// Token: 0x040006F8 RID: 1784
			Shift = 4,
			// Token: 0x040006F9 RID: 1785
			AltShift = 5,
			// Token: 0x040006FA RID: 1786
			CtrlShift = 6
		}
	}
}
