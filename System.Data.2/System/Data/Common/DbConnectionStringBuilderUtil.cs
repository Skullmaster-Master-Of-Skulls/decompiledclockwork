using System;
using System.Data.SqlClient;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020002EA RID: 746
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x06002F3C RID: 12092 RVA: 0x0012A7D0 File Offset: 0x00129BD0
		internal static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool result;
				try
				{
					result = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
				}
				catch (InvalidCastException innerException)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), innerException);
				}
				return result;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string x = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(x, "true") || StringComparer.OrdinalIgnoreCase.Equals(x, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(x, "false") && !StringComparer.OrdinalIgnoreCase.Equals(x, "no") && bool.Parse(text));
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0012A8DC File Offset: 0x00129CDC
		internal static bool ConvertToIntegratedSecurity(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool result;
				try
				{
					result = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
				}
				catch (InvalidCastException innerException)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), innerException);
				}
				return result;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string x = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(x, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(x, "true") || StringComparer.OrdinalIgnoreCase.Equals(x, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(x, "false") && !StringComparer.OrdinalIgnoreCase.Equals(x, "no") && bool.Parse(text));
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0012AA0C File Offset: 0x00129E0C
		internal static int ConvertToInt32(object value)
		{
			int result;
			try
			{
				result = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException innerException)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(int), innerException);
			}
			return result;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0012AA64 File Offset: 0x00129E64
		internal static string ConvertToString(object value)
		{
			string result;
			try
			{
				result = ((IConvertible)value).ToString(CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException innerException)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(string), innerException);
			}
			return result;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x0012AABC File Offset: 0x00129EBC
		internal static bool TryConvertToPoolBlockingPeriod(string value, out PoolBlockingPeriod result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "Auto"))
			{
				result = PoolBlockingPeriod.Auto;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "AlwaysBlock"))
			{
				result = PoolBlockingPeriod.AlwaysBlock;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "NeverBlock"))
			{
				result = PoolBlockingPeriod.NeverBlock;
				return true;
			}
			result = PoolBlockingPeriod.Auto;
			return false;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0012AB14 File Offset: 0x00129F14
		internal static bool IsValidPoolBlockingPeriodValue(PoolBlockingPeriod value)
		{
			return value == PoolBlockingPeriod.Auto || value == PoolBlockingPeriod.AlwaysBlock || value == PoolBlockingPeriod.NeverBlock;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0012AB30 File Offset: 0x00129F30
		internal static string PoolBlockingPeriodToString(PoolBlockingPeriod value)
		{
			if (value == PoolBlockingPeriod.AlwaysBlock)
			{
				return "AlwaysBlock";
			}
			if (value == PoolBlockingPeriod.NeverBlock)
			{
				return "NeverBlock";
			}
			return "Auto";
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0012AB58 File Offset: 0x00129F58
		internal static PoolBlockingPeriod ConvertToPoolBlockingPeriod(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				PoolBlockingPeriod result;
				if (DbConnectionStringBuilderUtil.TryConvertToPoolBlockingPeriod(text, out result))
				{
					return result;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToPoolBlockingPeriod(text, out result))
				{
					return result;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				PoolBlockingPeriod poolBlockingPeriod;
				if (value is PoolBlockingPeriod)
				{
					poolBlockingPeriod = (PoolBlockingPeriod)value;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(PoolBlockingPeriod), null);
					}
					try
					{
						poolBlockingPeriod = (PoolBlockingPeriod)Enum.ToObject(typeof(PoolBlockingPeriod), value);
					}
					catch (ArgumentException innerException)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(PoolBlockingPeriod), innerException);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidPoolBlockingPeriodValue(poolBlockingPeriod))
				{
					return poolBlockingPeriod;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)poolBlockingPeriod);
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0012AC34 File Offset: 0x0012A034
		internal static bool TryConvertToApplicationIntent(string value, out ApplicationIntent result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadOnly"))
			{
				result = ApplicationIntent.ReadOnly;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadWrite"))
			{
				result = ApplicationIntent.ReadWrite;
				return true;
			}
			result = ApplicationIntent.ReadWrite;
			return false;
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x0012AC74 File Offset: 0x0012A074
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x0012AC8C File Offset: 0x0012A08C
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0012ACA8 File Offset: 0x0012A0A8
		internal static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				ApplicationIntent result;
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out result))
				{
					return result;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out result))
				{
					return result;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				ApplicationIntent applicationIntent;
				if (value is ApplicationIntent)
				{
					applicationIntent = (ApplicationIntent)value;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), null);
					}
					try
					{
						applicationIntent = (ApplicationIntent)Enum.ToObject(typeof(ApplicationIntent), value);
					}
					catch (ArgumentException innerException)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), innerException);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(applicationIntent))
				{
					return applicationIntent;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)applicationIntent);
			}
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0012AD84 File Offset: 0x0012A184
		internal static bool TryConvertToAuthenticationType(string value, out SqlAuthenticationMethod result)
		{
			bool result2 = false;
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Sql Password"))
			{
				result = SqlAuthenticationMethod.SqlPassword;
				result2 = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Password"))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryPassword;
				result2 = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Integrated"))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryIntegrated;
				result2 = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Active Directory Interactive"))
			{
				result = SqlAuthenticationMethod.ActiveDirectoryInteractive;
				result2 = true;
			}
			else
			{
				result = DbConnectionStringDefaults.Authentication;
			}
			return result2;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x0012AE00 File Offset: 0x0012A200
		internal static bool TryConvertToColumnEncryptionSetting(string value, out SqlConnectionColumnEncryptionSetting result)
		{
			bool result2 = false;
			if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Enabled"))
			{
				result = SqlConnectionColumnEncryptionSetting.Enabled;
				result2 = true;
			}
			else if (StringComparer.InvariantCultureIgnoreCase.Equals(value, "Disabled"))
			{
				result = SqlConnectionColumnEncryptionSetting.Disabled;
				result2 = true;
			}
			else
			{
				result = DbConnectionStringDefaults.ColumnEncryptionSetting;
			}
			return result2;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x0012AE4C File Offset: 0x0012A24C
		internal static bool IsValidColumnEncryptionSetting(SqlConnectionColumnEncryptionSetting value)
		{
			return value == SqlConnectionColumnEncryptionSetting.Enabled || value == SqlConnectionColumnEncryptionSetting.Disabled;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x0012AE64 File Offset: 0x0012A264
		internal static string ColumnEncryptionSettingToString(SqlConnectionColumnEncryptionSetting value)
		{
			if (value == SqlConnectionColumnEncryptionSetting.Disabled)
			{
				return "Disabled";
			}
			if (value == SqlConnectionColumnEncryptionSetting.Enabled)
			{
				return "Enabled";
			}
			return null;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x0012AE88 File Offset: 0x0012A288
		internal static bool IsValidAuthenticationTypeValue(SqlAuthenticationMethod value)
		{
			return value == SqlAuthenticationMethod.SqlPassword || value == SqlAuthenticationMethod.ActiveDirectoryPassword || value == SqlAuthenticationMethod.ActiveDirectoryIntegrated || value == SqlAuthenticationMethod.ActiveDirectoryInteractive || value == SqlAuthenticationMethod.NotSpecified;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0012AEAC File Offset: 0x0012A2AC
		internal static string AuthenticationTypeToString(SqlAuthenticationMethod value)
		{
			switch (value)
			{
			case SqlAuthenticationMethod.SqlPassword:
				return "Sql Password";
			case SqlAuthenticationMethod.ActiveDirectoryPassword:
				return "Active Directory Password";
			case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
				return "Active Directory Integrated";
			case SqlAuthenticationMethod.ActiveDirectoryInteractive:
				return "Active Directory Interactive";
			default:
				return null;
			}
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x0012AEEC File Offset: 0x0012A2EC
		internal static SqlAuthenticationMethod ConvertToAuthenticationType(string keyword, object value)
		{
			if (value == null)
			{
				return DbConnectionStringDefaults.Authentication;
			}
			string text = value as string;
			if (text != null)
			{
				SqlAuthenticationMethod result;
				if (DbConnectionStringBuilderUtil.TryConvertToAuthenticationType(text, out result))
				{
					return result;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToAuthenticationType(text, out result))
				{
					return result;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlAuthenticationMethod sqlAuthenticationMethod;
				if (value is SqlAuthenticationMethod)
				{
					sqlAuthenticationMethod = (SqlAuthenticationMethod)value;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlAuthenticationMethod), null);
					}
					try
					{
						sqlAuthenticationMethod = (SqlAuthenticationMethod)Enum.ToObject(typeof(SqlAuthenticationMethod), value);
					}
					catch (ArgumentException innerException)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlAuthenticationMethod), innerException);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidAuthenticationTypeValue(sqlAuthenticationMethod))
				{
					return sqlAuthenticationMethod;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlAuthenticationMethod), (int)sqlAuthenticationMethod);
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x0012AFD0 File Offset: 0x0012A3D0
		internal static SqlConnectionColumnEncryptionSetting ConvertToColumnEncryptionSetting(string keyword, object value)
		{
			if (value == null)
			{
				return DbConnectionStringDefaults.ColumnEncryptionSetting;
			}
			string text = value as string;
			if (text != null)
			{
				SqlConnectionColumnEncryptionSetting result;
				if (DbConnectionStringBuilderUtil.TryConvertToColumnEncryptionSetting(text, out result))
				{
					return result;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToColumnEncryptionSetting(text, out result))
				{
					return result;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				SqlConnectionColumnEncryptionSetting sqlConnectionColumnEncryptionSetting;
				if (value is SqlConnectionColumnEncryptionSetting)
				{
					sqlConnectionColumnEncryptionSetting = (SqlConnectionColumnEncryptionSetting)value;
				}
				else
				{
					if (value.GetType().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionColumnEncryptionSetting), null);
					}
					try
					{
						sqlConnectionColumnEncryptionSetting = (SqlConnectionColumnEncryptionSetting)Enum.ToObject(typeof(SqlConnectionColumnEncryptionSetting), value);
					}
					catch (ArgumentException innerException)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(SqlConnectionColumnEncryptionSetting), innerException);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidColumnEncryptionSetting(sqlConnectionColumnEncryptionSetting))
				{
					return sqlConnectionColumnEncryptionSetting;
				}
				throw ADP.InvalidEnumerationValue(typeof(SqlConnectionColumnEncryptionSetting), (int)sqlConnectionColumnEncryptionSetting);
			}
		}

		// Token: 0x04001CE5 RID: 7397
		private const string PoolBlockingPeriodAutoString = "Auto";

		// Token: 0x04001CE6 RID: 7398
		private const string PoolBlockingPeriodAlwaysBlockString = "AlwaysBlock";

		// Token: 0x04001CE7 RID: 7399
		private const string PoolBlockingPeriodNeverBlockString = "NeverBlock";

		// Token: 0x04001CE8 RID: 7400
		private const string ApplicationIntentReadWriteString = "ReadWrite";

		// Token: 0x04001CE9 RID: 7401
		private const string ApplicationIntentReadOnlyString = "ReadOnly";

		// Token: 0x04001CEA RID: 7402
		private const string SqlPasswordString = "Sql Password";

		// Token: 0x04001CEB RID: 7403
		private const string ActiveDirectoryPasswordString = "Active Directory Password";

		// Token: 0x04001CEC RID: 7404
		private const string ActiveDirectoryIntegratedString = "Active Directory Integrated";

		// Token: 0x04001CED RID: 7405
		private const string ActiveDirectoryInteractiveString = "Active Directory Interactive";

		// Token: 0x04001CEE RID: 7406
		private const string ColumnEncryptionSettingEnabledString = "Enabled";

		// Token: 0x04001CEF RID: 7407
		private const string ColumnEncryptionSettingDisabledString = "Disabled";
	}
}
