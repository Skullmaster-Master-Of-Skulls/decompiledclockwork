using System;
using System.Drawing;
using System.Windows.Forms;

namespace TechnoPro.Common.Win32
{
	// Token: 0x0200000F RID: 15
	public static class WinFormsSystemInformation
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003F3C File Offset: 0x0000213C
		public static eWinFormsHighContrastMode HighContrastMode
		{
			get
			{
				if (!SystemInformation.HighContrast)
				{
					return eWinFormsHighContrastMode.None;
				}
				Color controlText = SystemColors.ControlText;
				if (controlText.R == 255 && controlText.G == 255 && controlText.B == 255)
				{
					return eWinFormsHighContrastMode.HighContrastBlack;
				}
				if (controlText.R != 0 || controlText.G != 0 || controlText.B != 0)
				{
					return eWinFormsHighContrastMode.HighContrastIndeterminate;
				}
				return eWinFormsHighContrastMode.HighContrastWhite;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003FA2 File Offset: 0x000021A2
		public static bool InHighContrastMode
		{
			get
			{
				return SystemInformation.HighContrast;
			}
		}
	}
}
