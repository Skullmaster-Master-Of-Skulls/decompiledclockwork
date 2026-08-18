using System;
using System.Data.SqlClient;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x0200008B RID: 139
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x000760D4 File Offset: 0x000754D4
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

		// Token: 0x060007E1 RID: 2017 RVA: 0x000761E4 File Offset: 0x000755E4
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

		// Token: 0x060007E2 RID: 2018 RVA: 0x00076314 File Offset: 0x00075714
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

		// Token: 0x060007E3 RID: 2019 RVA: 0x00076374 File Offset: 0x00075774
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

		// Token: 0x060007E4 RID: 2020 RVA: 0x000763D4 File Offset: 0x000757D4
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

		// Token: 0x060007E5 RID: 2021 RVA: 0x00076414 File Offset: 0x00075814
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00076434 File Offset: 0x00075834
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00076454 File Offset: 0x00075854
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

		// Token: 0x0400050C RID: 1292
		private const string ApplicationIntentReadWriteString = "ReadWrite";

		// Token: 0x0400050D RID: 1293
		private const string ApplicationIntentReadOnlyString = "ReadOnly";
	}
}
