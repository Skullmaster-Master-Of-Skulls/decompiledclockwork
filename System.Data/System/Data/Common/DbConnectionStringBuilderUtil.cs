using System;
using System.Data.SqlClient;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000131 RID: 305
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0023DAF8 File Offset: 0x0023CEF8
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

		// Token: 0x06001402 RID: 5122 RVA: 0x0023DC08 File Offset: 0x0023D008
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

		// Token: 0x06001403 RID: 5123 RVA: 0x0023DD38 File Offset: 0x0023D138
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

		// Token: 0x06001404 RID: 5124 RVA: 0x0023DD98 File Offset: 0x0023D198
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

		// Token: 0x06001405 RID: 5125 RVA: 0x0023DDF8 File Offset: 0x0023D1F8
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

		// Token: 0x06001406 RID: 5126 RVA: 0x0023DE38 File Offset: 0x0023D238
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0023DE58 File Offset: 0x0023D258
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0023DE78 File Offset: 0x0023D278
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

		// Token: 0x04000C3B RID: 3131
		private const string ApplicationIntentReadWriteString = "ReadWrite";

		// Token: 0x04000C3C RID: 3132
		private const string ApplicationIntentReadOnlyString = "ReadOnly";
	}
}
