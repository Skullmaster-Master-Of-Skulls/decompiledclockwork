using System;

namespace System.Web.Util
{
	// Token: 0x020001DE RID: 478
	internal static class SynchronizationContextUtil
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0004A376 File Offset: 0x00048576
		public static SynchronizationContextMode CurrentMode
		{
			get
			{
				if (AppSettings.UseTaskFriendlySynchronizationContext)
				{
					return SynchronizationContextMode.Normal;
				}
				return SynchronizationContextMode.Legacy;
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0004A384 File Offset: 0x00048584
		private static string FormatErrorMessage(string specificErrorMessage, SynchronizationContextMode requiredMode)
		{
			string name;
			if (HttpRuntime.TargetFramework < VersionUtil.Framework45 && requiredMode == SynchronizationContextMode.Normal)
			{
				name = "SynchronizationContextUtil_UpgradeToTargetFramework45Instructions";
			}
			else if (HttpRuntime.TargetFramework >= VersionUtil.Framework45 && requiredMode == SynchronizationContextMode.Legacy)
			{
				name = "SynchronizationContextUtil_AddDowngradeAppSettingsSwitch";
			}
			else
			{
				name = "SynchronizationContextUtil_RemoveAppSettingsSwitch";
			}
			return string.Concat(new string[]
			{
				SR.GetString(specificErrorMessage),
				" ",
				SR.GetString(name),
				"\r\n",
				SR.GetString("SynchronizationContextUtil_ForMoreInformation")
			});
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0004A40C File Offset: 0x0004860C
		public static void ValidateMode(SynchronizationContextMode currentMode, SynchronizationContextMode requiredMode, string specificErrorMessage)
		{
			if (currentMode != requiredMode)
			{
				string message = SynchronizationContextUtil.FormatErrorMessage(specificErrorMessage, requiredMode);
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0004A42C File Offset: 0x0004862C
		public static void ValidateModeForAspCompat()
		{
			SynchronizationContextUtil.ValidateMode(SynchronizationContextUtil.CurrentMode, SynchronizationContextMode.Legacy, "SynchronizationContextUtil_AspCompatModeNotCompatible");
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0004A43E File Offset: 0x0004863E
		public static void ValidateModeForPageAsyncVoidMethods()
		{
			SynchronizationContextUtil.ValidateMode(SynchronizationContextUtil.CurrentMode, SynchronizationContextMode.Normal, "SynchronizationContextUtil_PageAsyncVoidMethodsNotCompatible");
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0004A450 File Offset: 0x00048650
		public static void ValidateModeForWebSockets()
		{
			SynchronizationContextUtil.ValidateMode(SynchronizationContextUtil.CurrentMode, SynchronizationContextMode.Normal, "SynchronizationContextUtil_WebSocketsNotCompatible");
		}
	}
}
