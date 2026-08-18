using System;
using System.Globalization;
using System.Web.Optimization.Resources;

namespace System.Web.Optimization
{
	// Token: 0x02000035 RID: 53
	internal static class ExceptionUtil
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00005E64 File Offset: 0x00004064
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.Parameter_NullOrEmpty, new object[]
			{
				parameter
			}), parameter);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005E94 File Offset: 0x00004094
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.Property_NullOrEmpty, new object[]
			{
				property
			}), property);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005EC4 File Offset: 0x000040C4
		internal static Exception ValidateVirtualPath(string virtualPath, string argumentName)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				return ExceptionUtil.ParameterNullOrEmpty(argumentName);
			}
			if (!virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
			{
				return new ArgumentException(string.Format(CultureInfo.CurrentCulture, OptimizationResources.UrlMappings_only_app_relative_url_allowed, new object[]
				{
					virtualPath
				}), argumentName);
			}
			return null;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005F14 File Offset: 0x00004114
		internal static bool IsPureWildcardSearchPattern(string searchPattern)
		{
			if (!string.IsNullOrEmpty(searchPattern))
			{
				string a = searchPattern.Trim();
				if (string.Equals(a, "*", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "*.*", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
}
