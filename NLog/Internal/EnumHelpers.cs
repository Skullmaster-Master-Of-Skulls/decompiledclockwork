using System;

namespace NLog.Internal
{
	// Token: 0x0200007D RID: 125
	internal static class EnumHelpers
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00009393 File Offset: 0x00007593
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
		{
			return EnumHelpers.TryParse<TEnum>(value, false, out result);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000939D File Offset: 0x0000759D
		public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, ignoreCase, out result);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000093A8 File Offset: 0x000075A8
		private static bool TryParseEnum_net3<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			Type typeFromHandle = typeof(TEnum);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeFromHandle.FullName));
			}
			if (StringHelpers.IsNullOrWhiteSpace(value))
			{
				result = default(TEnum);
				return false;
			}
			bool result2;
			try
			{
				result = (TEnum)((object)Enum.Parse(typeFromHandle, value, ignoreCase));
				result2 = true;
			}
			catch (Exception)
			{
				result = default(TEnum);
				result2 = false;
			}
			return result2;
		}
	}
}
