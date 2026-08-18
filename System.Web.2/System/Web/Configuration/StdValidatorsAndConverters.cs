using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000758 RID: 1880
	internal static class StdValidatorsAndConverters
	{
		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x0013B958 File Offset: 0x00139B58
		internal static TypeConverter InfiniteTimeSpanConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_infiniteTimeSpanConverter == null)
				{
					StdValidatorsAndConverters.s_infiniteTimeSpanConverter = new InfiniteTimeSpanConverter();
				}
				return StdValidatorsAndConverters.s_infiniteTimeSpanConverter;
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06005AA6 RID: 23206 RVA: 0x0013B970 File Offset: 0x00139B70
		internal static TypeConverter TimeSpanMinutesConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_timeSpanMinutesConverter == null)
				{
					StdValidatorsAndConverters.s_timeSpanMinutesConverter = new TimeSpanMinutesConverter();
				}
				return StdValidatorsAndConverters.s_timeSpanMinutesConverter;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x06005AA7 RID: 23207 RVA: 0x0013B988 File Offset: 0x00139B88
		internal static TypeConverter TimeSpanMinutesOrInfiniteConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_timeSpanMinutesOrInfiniteConverter == null)
				{
					StdValidatorsAndConverters.s_timeSpanMinutesOrInfiniteConverter = new TimeSpanMinutesOrInfiniteConverter();
				}
				return StdValidatorsAndConverters.s_timeSpanMinutesOrInfiniteConverter;
			}
		}

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x0013B9A0 File Offset: 0x00139BA0
		internal static TypeConverter TimeSpanSecondsConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_timeSpanSecondsConverter == null)
				{
					StdValidatorsAndConverters.s_timeSpanSecondsConverter = new TimeSpanSecondsConverter();
				}
				return StdValidatorsAndConverters.s_timeSpanSecondsConverter;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06005AA9 RID: 23209 RVA: 0x0013B9B8 File Offset: 0x00139BB8
		internal static TypeConverter TimeSpanSecondsOrInfiniteConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_timeSpanSecondsOrInfiniteConverter == null)
				{
					StdValidatorsAndConverters.s_timeSpanSecondsOrInfiniteConverter = new TimeSpanSecondsOrInfiniteConverter();
				}
				return StdValidatorsAndConverters.s_timeSpanSecondsOrInfiniteConverter;
			}
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x0013B9D0 File Offset: 0x00139BD0
		internal static TypeConverter WhiteSpaceTrimStringConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_whiteSpaceTrimStringConverter == null)
				{
					StdValidatorsAndConverters.s_whiteSpaceTrimStringConverter = new WhiteSpaceTrimStringConverter();
				}
				return StdValidatorsAndConverters.s_whiteSpaceTrimStringConverter;
			}
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x06005AAB RID: 23211 RVA: 0x0013B9E8 File Offset: 0x00139BE8
		internal static TypeConverter VersionConverter
		{
			get
			{
				if (StdValidatorsAndConverters.s_versionConverter == null)
				{
					StdValidatorsAndConverters.s_versionConverter = new VersionConverter();
				}
				return StdValidatorsAndConverters.s_versionConverter;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x0013BA00 File Offset: 0x00139C00
		internal static ConfigurationValidatorBase RegexMatchTimeoutValidator
		{
			get
			{
				if (StdValidatorsAndConverters.s_regexMatchTimeoutValidator == null)
				{
					StdValidatorsAndConverters.s_regexMatchTimeoutValidator = new RegexMatchTimeoutValidator();
				}
				return StdValidatorsAndConverters.s_regexMatchTimeoutValidator;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06005AAD RID: 23213 RVA: 0x0013BA18 File Offset: 0x00139C18
		internal static ConfigurationValidatorBase PositiveTimeSpanValidator
		{
			get
			{
				if (StdValidatorsAndConverters.s_positiveTimeSpanValidator == null)
				{
					StdValidatorsAndConverters.s_positiveTimeSpanValidator = new PositiveTimeSpanValidator();
				}
				return StdValidatorsAndConverters.s_positiveTimeSpanValidator;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x0013BA30 File Offset: 0x00139C30
		internal static ConfigurationValidatorBase NonEmptyStringValidator
		{
			get
			{
				if (StdValidatorsAndConverters.s_nonEmptyStringValidator == null)
				{
					StdValidatorsAndConverters.s_nonEmptyStringValidator = new StringValidator(1);
				}
				return StdValidatorsAndConverters.s_nonEmptyStringValidator;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x0013BA49 File Offset: 0x00139C49
		internal static ConfigurationValidatorBase NonZeroPositiveIntegerValidator
		{
			get
			{
				if (StdValidatorsAndConverters.s_nonZeroPositiveIntegerValidator == null)
				{
					StdValidatorsAndConverters.s_nonZeroPositiveIntegerValidator = new IntegerValidator(1, int.MaxValue);
				}
				return StdValidatorsAndConverters.s_nonZeroPositiveIntegerValidator;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x0013BA67 File Offset: 0x00139C67
		internal static ConfigurationValidatorBase PositiveIntegerValidator
		{
			get
			{
				if (StdValidatorsAndConverters.s_positiveIntegerValidator == null)
				{
					StdValidatorsAndConverters.s_positiveIntegerValidator = new IntegerValidator(0, int.MaxValue);
				}
				return StdValidatorsAndConverters.s_positiveIntegerValidator;
			}
		}

		// Token: 0x04002FFF RID: 12287
		private static TypeConverter s_infiniteTimeSpanConverter;

		// Token: 0x04003000 RID: 12288
		private static TypeConverter s_timeSpanMinutesConverter;

		// Token: 0x04003001 RID: 12289
		private static TypeConverter s_timeSpanMinutesOrInfiniteConverter;

		// Token: 0x04003002 RID: 12290
		private static TypeConverter s_timeSpanSecondsConverter;

		// Token: 0x04003003 RID: 12291
		private static TypeConverter s_timeSpanSecondsOrInfiniteConverter;

		// Token: 0x04003004 RID: 12292
		private static TypeConverter s_whiteSpaceTrimStringConverter;

		// Token: 0x04003005 RID: 12293
		private static TypeConverter s_versionConverter;

		// Token: 0x04003006 RID: 12294
		private static ConfigurationValidatorBase s_regexMatchTimeoutValidator;

		// Token: 0x04003007 RID: 12295
		private static ConfigurationValidatorBase s_positiveTimeSpanValidator;

		// Token: 0x04003008 RID: 12296
		private static ConfigurationValidatorBase s_nonEmptyStringValidator;

		// Token: 0x04003009 RID: 12297
		private static ConfigurationValidatorBase s_nonZeroPositiveIntegerValidator;

		// Token: 0x0400300A RID: 12298
		private static ConfigurationValidatorBase s_positiveIntegerValidator;
	}
}
