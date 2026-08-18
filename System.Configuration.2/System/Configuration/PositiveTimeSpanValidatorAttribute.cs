using System;

namespace System.Configuration
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PositiveTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00018FE3 File Offset: 0x000171E3
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new PositiveTimeSpanValidator();
			}
		}
	}
}
