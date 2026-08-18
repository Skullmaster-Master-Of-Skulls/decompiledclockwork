using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Web.Util
{
	// Token: 0x020001C6 RID: 454
	internal static class EnableViewStateMacRegistryHelper
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x0004914C File Offset: 0x0004734C
		static EnableViewStateMacRegistryHelper()
		{
			bool flag = EnableViewStateMacRegistryHelper.IsMacEnforcementEnabledViaRegistry();
			if (flag)
			{
				EnableViewStateMacRegistryHelper.EnforceViewStateMac = true;
				EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsFromCrossPagePostbacks = true;
			}
			if (AppSettings.AllowInsecureDeserialization != null)
			{
				EnableViewStateMacRegistryHelper.EnforceViewStateMac = !AppSettings.AllowInsecureDeserialization.Value;
				EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsFromCrossPagePostbacks |= !AppSettings.AllowInsecureDeserialization.Value;
			}
			EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsAlways = AppSettings.AlwaysIgnoreViewStateValidationErrors;
			if (EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsAlways)
			{
				EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsFromCrossPagePostbacks = true;
				return;
			}
			if (EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsFromCrossPagePostbacks)
			{
				EnableViewStateMacRegistryHelper.WriteViewStateGeneratorField = true;
			}
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000491D4 File Offset: 0x000473D4
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsMacEnforcementEnabledViaRegistry()
		{
			bool result;
			try
			{
				string keyName = string.Format(CultureInfo.InvariantCulture, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework\\v{0}", new object[]
				{
					Environment.Version.ToString(3)
				});
				int num = (int)Registry.GetValue(keyName, "AspNetEnforceViewStateMac", 1);
				result = (num != 0);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x040016FC RID: 5884
		public static readonly bool EnforceViewStateMac;

		// Token: 0x040016FD RID: 5885
		public static readonly bool SuppressMacValidationErrorsAlways;

		// Token: 0x040016FE RID: 5886
		public static readonly bool SuppressMacValidationErrorsFromCrossPagePostbacks;

		// Token: 0x040016FF RID: 5887
		public static readonly bool WriteViewStateGeneratorField;
	}
}
