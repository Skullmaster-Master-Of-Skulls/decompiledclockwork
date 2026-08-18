using System;
using System.Text.RegularExpressions;
using Google.Apis.Discovery;
using Google.Apis.Testing;

namespace Google.Apis.Requests.Parameters
{
	// Token: 0x02000017 RID: 23
	public static class ParameterValidator
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000332B File Offset: 0x0000152B
		[VisibleForTestOnly]
		public static bool ValidateRegex(IParameter param, string paramValue)
		{
			return string.IsNullOrEmpty(param.Pattern) || new Regex(param.Pattern).IsMatch(paramValue);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000334D File Offset: 0x0000154D
		public static bool ValidateParameter(IParameter parameter, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return !parameter.IsRequired;
			}
			return ParameterValidator.ValidateRegex(parameter, value);
		}
	}
}
