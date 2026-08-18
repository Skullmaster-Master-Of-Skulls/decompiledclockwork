using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x0200012D RID: 301
	internal static class ParameterInfoUtil
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000157AC File Offset: 0x000139AC
		public static bool TryGetDefaultValue(ParameterInfo parameterInfo, out object value)
		{
			object defaultValue = parameterInfo.DefaultValue;
			if (defaultValue != DBNull.Value)
			{
				value = defaultValue;
				return true;
			}
			DefaultValueAttribute[] array = (DefaultValueAttribute[])parameterInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false);
			if (array == null || array.Length == 0)
			{
				value = null;
				return false;
			}
			value = array[0].Value;
			return true;
		}
	}
}
