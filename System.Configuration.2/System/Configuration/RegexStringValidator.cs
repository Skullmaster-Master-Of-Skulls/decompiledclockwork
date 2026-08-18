using System;
using System.Text.RegularExpressions;

namespace System.Configuration
{
	// Token: 0x02000082 RID: 130
	public class RegexStringValidator : ConfigurationValidatorBase
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00019E27 File Offset: 0x00018027
		public RegexStringValidator(string regex)
		{
			if (string.IsNullOrEmpty(regex))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("regex");
			}
			this._expression = regex;
			this._regex = new Regex(regex, RegexOptions.Compiled);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019E56 File Offset: 0x00018056
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00019E68 File Offset: 0x00018068
		public override void Validate(object value)
		{
			ValidatorUtils.HelperParamValidation(value, typeof(string));
			if (value == null)
			{
				return;
			}
			Match match = this._regex.Match((string)value);
			if (!match.Success)
			{
				throw new ArgumentException(SR.GetString("Regex_validator_error", new object[]
				{
					this._expression
				}));
			}
		}

		// Token: 0x040002DC RID: 732
		private string _expression;

		// Token: 0x040002DD RID: 733
		private Regex _regex;
	}
}
