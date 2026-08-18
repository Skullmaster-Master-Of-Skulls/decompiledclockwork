using System;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200022F RID: 559
	internal class DisplayInformation
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x000AC286 File Offset: 0x000AA486
		static DisplayInformation()
		{
			SystemEvents.UserPreferenceChanging += DisplayInformation.UserPreferenceChanging;
			SystemEvents.DisplaySettingsChanging += DisplayInformation.DisplaySettingsChanging;
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x000AC2AA File Offset: 0x000AA4AA
		public static short BitsPerPixel
		{
			get
			{
				if (DisplayInformation.bitsPerPixel == 0)
				{
					DisplayInformation.bitsPerPixel = (short)Screen.PrimaryScreen.BitsPerPixel;
				}
				return DisplayInformation.bitsPerPixel;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x000AC2C8 File Offset: 0x000AA4C8
		public static bool LowResolution
		{
			get
			{
				if (DisplayInformation.lowResSettingValid && !DisplayInformation.lowRes)
				{
					return DisplayInformation.lowRes;
				}
				DisplayInformation.lowRes = (DisplayInformation.BitsPerPixel <= 8);
				DisplayInformation.lowResSettingValid = true;
				return DisplayInformation.lowRes;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000AC2F9 File Offset: 0x000AA4F9
		public static bool HighContrast
		{
			get
			{
				if (DisplayInformation.highContrastSettingValid)
				{
					return DisplayInformation.highContrast;
				}
				DisplayInformation.highContrast = SystemInformation.HighContrast;
				DisplayInformation.highContrastSettingValid = true;
				return DisplayInformation.highContrast;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000AC31D File Offset: 0x000AA51D
		public static bool IsDropShadowEnabled
		{
			get
			{
				if (DisplayInformation.dropShadowSettingValid)
				{
					return DisplayInformation.dropShadowEnabled;
				}
				DisplayInformation.dropShadowEnabled = SystemInformation.IsDropShadowEnabled;
				DisplayInformation.dropShadowSettingValid = true;
				return DisplayInformation.dropShadowEnabled;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000AC341 File Offset: 0x000AA541
		public static bool TerminalServer
		{
			get
			{
				if (DisplayInformation.terminalSettingValid)
				{
					return DisplayInformation.isTerminalServerSession;
				}
				DisplayInformation.isTerminalServerSession = SystemInformation.TerminalServerSession;
				DisplayInformation.terminalSettingValid = true;
				return DisplayInformation.isTerminalServerSession;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000AC365 File Offset: 0x000AA565
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				if (DisplayInformation.menuAccessKeysUnderlinedValid)
				{
					return DisplayInformation.menuAccessKeysUnderlined;
				}
				DisplayInformation.menuAccessKeysUnderlined = SystemInformation.MenuAccessKeysUnderlined;
				DisplayInformation.menuAccessKeysUnderlinedValid = true;
				return DisplayInformation.menuAccessKeysUnderlined;
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000AC389 File Offset: 0x000AA589
		private static void DisplaySettingsChanging(object obj, EventArgs ea)
		{
			DisplayInformation.highContrastSettingValid = false;
			DisplayInformation.lowResSettingValid = false;
			DisplayInformation.terminalSettingValid = false;
			DisplayInformation.dropShadowSettingValid = false;
			DisplayInformation.menuAccessKeysUnderlinedValid = false;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000AC3A9 File Offset: 0x000AA5A9
		private static void UserPreferenceChanging(object obj, UserPreferenceChangingEventArgs e)
		{
			DisplayInformation.highContrastSettingValid = false;
			DisplayInformation.lowResSettingValid = false;
			DisplayInformation.terminalSettingValid = false;
			DisplayInformation.dropShadowSettingValid = false;
			DisplayInformation.bitsPerPixel = 0;
			if (e.Category == UserPreferenceCategory.General)
			{
				DisplayInformation.menuAccessKeysUnderlinedValid = false;
			}
		}

		// Token: 0x04000EF4 RID: 3828
		private static bool highContrast;

		// Token: 0x04000EF5 RID: 3829
		private static bool lowRes;

		// Token: 0x04000EF6 RID: 3830
		private static bool isTerminalServerSession;

		// Token: 0x04000EF7 RID: 3831
		private static bool highContrastSettingValid;

		// Token: 0x04000EF8 RID: 3832
		private static bool lowResSettingValid;

		// Token: 0x04000EF9 RID: 3833
		private static bool terminalSettingValid;

		// Token: 0x04000EFA RID: 3834
		private static short bitsPerPixel;

		// Token: 0x04000EFB RID: 3835
		private static bool dropShadowSettingValid;

		// Token: 0x04000EFC RID: 3836
		private static bool dropShadowEnabled;

		// Token: 0x04000EFD RID: 3837
		private static bool menuAccessKeysUnderlinedValid;

		// Token: 0x04000EFE RID: 3838
		private static bool menuAccessKeysUnderlined;
	}
}
