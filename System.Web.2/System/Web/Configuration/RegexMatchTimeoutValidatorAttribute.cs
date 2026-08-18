using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006C8 RID: 1736
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class RegexMatchTimeoutValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x0012692C File Offset: 0x00124B2C
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new RegexMatchTimeoutValidator();
			}
		}
	}
}
