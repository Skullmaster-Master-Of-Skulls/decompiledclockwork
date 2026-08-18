using System;

namespace System.Configuration
{
	// Token: 0x02000065 RID: 101
	public class IntegerValidator : ConfigurationValidatorBase
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00014074 File Offset: 0x00012274
		public IntegerValidator(int minValue, int maxValue) : this(minValue, maxValue, false, 1)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00014080 File Offset: 0x00012280
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 1)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001408C File Offset: 0x0001228C
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			if (resolution <= 0)
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
			this._flags = (rangeIsExclusive ? IntegerValidator.ValidationFlags.ExclusiveRange : IntegerValidator.ValidationFlags.None);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00014108 File Offset: 0x00012308
		public override bool CanValidate(Type type)
		{
			return type == typeof(int);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001411A File Offset: 0x0001231A
		public override void Validate(object value)
		{
			ValidatorUtils.HelperParamValidation(value, typeof(int));
			ValidatorUtils.ValidateScalar<int>((int)value, this._minValue, this._maxValue, this._resolution, this._flags == IntegerValidator.ValidationFlags.ExclusiveRange);
		}

		// Token: 0x04000286 RID: 646
		private IntegerValidator.ValidationFlags _flags;

		// Token: 0x04000287 RID: 647
		private int _minValue = int.MinValue;

		// Token: 0x04000288 RID: 648
		private int _maxValue = int.MaxValue;

		// Token: 0x04000289 RID: 649
		private int _resolution = 1;

		// Token: 0x020000D4 RID: 212
		private enum ValidationFlags
		{
			// Token: 0x040004A4 RID: 1188
			None,
			// Token: 0x040004A5 RID: 1189
			ExclusiveRange
		}
	}
}
