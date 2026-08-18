using System;

namespace System.Configuration
{
	// Token: 0x02000053 RID: 83
	public sealed class DefaultValidator : ConfigurationValidatorBase
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000874E File Offset: 0x0000694E
		public override bool CanValidate(Type type)
		{
			return true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00005E74 File Offset: 0x00004074
		public override void Validate(object value)
		{
		}
	}
}
