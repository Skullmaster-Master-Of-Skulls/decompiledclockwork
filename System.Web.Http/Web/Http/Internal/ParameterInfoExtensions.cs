using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Web.Http.Internal
{
	// Token: 0x0200011E RID: 286
	internal static class ParameterInfoExtensions
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x000171B0 File Offset: 0x000153B0
		public static bool TryGetDefaultValue(this ParameterInfo parameterInfo, out object value)
		{
			if (parameterInfo == null)
			{
				throw Error.ArgumentNull("parameterInfo");
			}
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
