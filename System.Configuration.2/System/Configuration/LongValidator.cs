using System;

namespace System.Configuration
{
	// Token: 0x0200006D RID: 109
	public class LongValidator : ConfigurationValidatorBase
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0001456F File Offset: 0x0001276F
		public LongValidator(long minValue, long maxValue) : this(minValue, maxValue, false, 1L)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001457C File Offset: 0x0001277C
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 1L)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001458C File Offset: 0x0001278C
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive, long resolution)
		{
			if (resolution <= 0L)
			{
				throw new ArgumentOutOfRangeException("resolution");
			}
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", SR.GetString("Validator_min_greater_than_max"));
			}
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._resolution = resolution;
			this._flags = (rangeIsExclusive ? LongValidator.ValidationFlags.ExclusiveRange : LongValidator.ValidationFlags.None);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014612 File Offset: 0x00012812
		public override bool CanValidate(Type type)
		{
			return type == typeof(long);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014624 File Offset: 0x00012824
		public override void Validate(object value)
		{
			ValidatorUtils.HelperParamValidation(value, typeof(long));
			ValidatorUtils.ValidateScalar<long>((long)value, this._minValue, this._maxValue, this._resolution, this._flags == LongValidator.ValidationFlags.ExclusiveRange);
		}

		// Token: 0x0400029C RID: 668
		private LongValidator.ValidationFlags _flags;

		// Token: 0x0400029D RID: 669
		private long _minValue = long.MinValue;

		// Token: 0x0400029E RID: 670
		private long _maxValue = long.MaxValue;

		// Token: 0x0400029F RID: 671
		private long _resolution = 1L;

		// Token: 0x020000D5 RID: 213
		private enum ValidationFlags
		{
			// Token: 0x040004A7 RID: 1191
			None,
			// Token: 0x040004A8 RID: 1192
			ExclusiveRange
		}
	}
}
