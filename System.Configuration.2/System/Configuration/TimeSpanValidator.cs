using System;

namespace System.Configuration
{
	// Token: 0x02000099 RID: 153
	public class TimeSpanValidator : ConfigurationValidatorBase
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001CE12 File Offset: 0x0001B012
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue) : this(minValue, maxValue, false, 0L)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001CE1F File Offset: 0x0001B01F
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001CE2C File Offset: 0x0001B02C
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive, long resolutionInSeconds)
		{
			if (resolutionInSeconds < 0L)
			{
				throw new ArgumentOutOfRangeException("resolutionInSeconds");
			}
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", SR.GetString("Validator_min_greater_than_max"));
			}
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._resolution = resolutionInSeconds;
			this._flags = (rangeIsExclusive ? TimeSpanValidator.ValidationFlags.ExclusiveRange : TimeSpanValidator.ValidationFlags.None);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00018F97 File Offset: 0x00017197
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001CEA7 File Offset: 0x0001B0A7
		public override void Validate(object value)
		{
			ValidatorUtils.HelperParamValidation(value, typeof(TimeSpan));
			ValidatorUtils.ValidateScalar((TimeSpan)value, this._minValue, this._maxValue, this._resolution, this._flags == TimeSpanValidator.ValidationFlags.ExclusiveRange);
		}

		// Token: 0x04000356 RID: 854
		private TimeSpanValidator.ValidationFlags _flags;

		// Token: 0x04000357 RID: 855
		private TimeSpan _minValue = TimeSpan.MinValue;

		// Token: 0x04000358 RID: 856
		private TimeSpan _maxValue = TimeSpan.MaxValue;

		// Token: 0x04000359 RID: 857
		private long _resolution;

		// Token: 0x020000D8 RID: 216
		private enum ValidationFlags
		{
			// Token: 0x040004B1 RID: 1201
			None,
			// Token: 0x040004B2 RID: 1202
			ExclusiveRange
		}
	}
}
