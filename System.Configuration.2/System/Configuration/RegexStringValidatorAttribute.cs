using System;

namespace System.Configuration
{
	// Token: 0x02000083 RID: 131
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RegexStringValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00019EC2 File Offset: 0x000180C2
		public RegexStringValidatorAttribute(string regex)
		{
			this._regex = regex;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00019ED1 File Offset: 0x000180D1
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new RegexStringValidator(this._regex);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00019EDE File Offset: 0x000180DE
		public string Regex
		{
			get
			{
				return this._regex;
			}
		}

		// Token: 0x040002DE RID: 734
		private string _regex;
	}
}
