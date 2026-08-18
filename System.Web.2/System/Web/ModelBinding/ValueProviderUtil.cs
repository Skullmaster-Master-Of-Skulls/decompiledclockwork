using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200062A RID: 1578
	internal static class ValueProviderUtil
	{
		// Token: 0x06004ED8 RID: 20184 RVA: 0x001125C2 File Offset: 0x001107C2
		public static string CreateSubPropertyName(string prefix, string propertyName)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return propertyName;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				return prefix;
			}
			return prefix + "." + propertyName;
		}
	}
}
