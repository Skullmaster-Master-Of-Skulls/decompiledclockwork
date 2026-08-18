using System;

namespace System.Configuration
{
	// Token: 0x02000075 RID: 117
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x00018F97 File Offset: 0x00017197
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00018FA9 File Offset: 0x000171A9
		public override void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((TimeSpan)value <= TimeSpan.Zero)
			{
				throw new ArgumentException(SR.GetString("Validator_timespan_value_must_be_positive"));
			}
		}
	}
}
