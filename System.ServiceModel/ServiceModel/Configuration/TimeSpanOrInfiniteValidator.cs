using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DD RID: 1757
	internal class TimeSpanOrInfiniteValidator : TimeSpanValidator
	{
		// Token: 0x060043E2 RID: 17378 RVA: 0x00100681 File Offset: 0x000FE881
		public TimeSpanOrInfiniteValidator(TimeSpan minValue, TimeSpan maxValue) : base(minValue, maxValue)
		{
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0010068B File Offset: 0x000FE88B
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
