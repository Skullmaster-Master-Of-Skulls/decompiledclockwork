using System;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x0200009A RID: 154
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001CEFD File Offset: 0x0001B0FD
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new TimeSpanValidator(this._min, this._max, this._excludeRange);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001CF16 File Offset: 0x0001B116
		public TimeSpan MinValue
		{
			get
			{
				return this._min;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001CF1E File Offset: 0x0001B11E
		public TimeSpan MaxValue
		{
			get
			{
				return this._max;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001CF26 File Offset: 0x0001B126
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public string MinValueString
		{
			get
			{
				return this._min.ToString();
			}
			set
			{
				TimeSpan timeSpan = TimeSpan.Parse(value, CultureInfo.InvariantCulture);
				if (this._max < timeSpan)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._min = timeSpan;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001CF7F File Offset: 0x0001B17F
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0001CF94 File Offset: 0x0001B194
		public string MaxValueString
		{
			get
			{
				return this._max.ToString();
			}
			set
			{
				TimeSpan timeSpan = TimeSpan.Parse(value, CultureInfo.InvariantCulture);
				if (this._min > timeSpan)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._max = timeSpan;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001CFD7 File Offset: 0x0001B1D7
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001CFDF File Offset: 0x0001B1DF
		public bool ExcludeRange
		{
			get
			{
				return this._excludeRange;
			}
			set
			{
				this._excludeRange = value;
			}
		}

		// Token: 0x0400035A RID: 858
		private TimeSpan _min = TimeSpan.MinValue;

		// Token: 0x0400035B RID: 859
		private TimeSpan _max = TimeSpan.MaxValue;

		// Token: 0x0400035C RID: 860
		private bool _excludeRange;

		// Token: 0x0400035D RID: 861
		public const string TimeSpanMinValue = "-10675199.02:48:05.4775808";

		// Token: 0x0400035E RID: 862
		public const string TimeSpanMaxValue = "10675199.02:48:05.4775807";
	}
}
