using System;
using System.Configuration;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D6 RID: 470
	internal class TimeSpanOrInfiniteValidator : TimeSpanValidator
	{
		// Token: 0x06000F6C RID: 3948 RVA: 0x00044213 File Offset: 0x00042413
		public TimeSpanOrInfiniteValidator(TimeSpan minValue, TimeSpan maxValue) : base(minValue, maxValue)
		{
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0004421D File Offset: 0x0004241D
		public override void Validate(object value)
		{
			if (value.GetType() == typeof(TimeSpan) && (TimeSpan)value == TimeSpan.MaxValue)
			{
				return;
			}
			base.Validate(value);
		}
	}
}
