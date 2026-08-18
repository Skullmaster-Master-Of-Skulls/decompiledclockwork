using System;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x020000A1 RID: 161
	internal static class ValidatorUtils
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001D9AA File Offset: 0x0001BBAA
		public static void HelperParamValidation(object value, Type allowedType)
		{
			if (value == null)
			{
				return;
			}
			if (value.GetType() != allowedType)
			{
				throw new ArgumentException(SR.GetString("Validator_value_type_invalid"), string.Empty);
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		public static void ValidateScalar<T>(T value, T min, T max, T resolution, bool exclusiveRange) where T : IComparable<T>
		{
			ValidatorUtils.ValidateRangeImpl<T>(value, min, max, exclusiveRange);
			ValidatorUtils.ValidateResolution(resolution.ToString(), Convert.ToInt64(value, CultureInfo.InvariantCulture), Convert.ToInt64(resolution, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001DA14 File Offset: 0x0001BC14
		private static void ValidateRangeImpl<T>(T value, T min, T max, bool exclusiveRange) where T : IComparable<T>
		{
			IComparable<T> comparable = value;
			IComparable<T> comparable2 = max;
			bool flag = false;
			if (comparable.CompareTo(min) >= 0)
			{
				flag = true;
			}
			if (flag && comparable.CompareTo(max) > 0)
			{
				flag = false;
			}
			if (!(flag ^ exclusiveRange))
			{
				string @string;
				if (min.Equals(max))
				{
					if (exclusiveRange)
					{
						@string = SR.GetString("Validation_scalar_range_violation_not_different");
					}
					else
					{
						@string = SR.GetString("Validation_scalar_range_violation_not_equal");
					}
				}
				else if (exclusiveRange)
				{
					@string = SR.GetString("Validation_scalar_range_violation_not_outside_range");
				}
				else
				{
					@string = SR.GetString("Validation_scalar_range_violation_not_in_range");
				}
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, @string, new object[]
				{
					min.ToString(),
					max.ToString()
				}));
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001DADA File Offset: 0x0001BCDA
		private static void ValidateResolution(string resolutionAsString, long value, long resolution)
		{
			if (value % resolution != 0L)
			{
				throw new ArgumentException(SR.GetString("Validator_scalar_resolution_violation", new object[]
				{
					resolutionAsString
				}));
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public static void ValidateScalar(TimeSpan value, TimeSpan min, TimeSpan max, long resolutionInSeconds, bool exclusiveRange)
		{
			ValidatorUtils.ValidateRangeImpl<TimeSpan>(value, min, max, exclusiveRange);
			if (resolutionInSeconds > 0L)
			{
				ValidatorUtils.ValidateResolution(TimeSpan.FromSeconds((double)resolutionInSeconds).ToString(), value.Ticks, resolutionInSeconds * 10000000L);
			}
		}
	}
}
