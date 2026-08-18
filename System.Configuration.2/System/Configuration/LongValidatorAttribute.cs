using System;

namespace System.Configuration
{
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LongValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001465C File Offset: 0x0001285C
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new LongValidator(this._min, this._max, this._excludeRange);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001469B File Offset: 0x0001289B
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x000146A3 File Offset: 0x000128A3
		public long MinValue
		{
			get
			{
				return this._min;
			}
			set
			{
				if (this._max < value)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._min = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000146CA File Offset: 0x000128CA
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x000146D2 File Offset: 0x000128D2
		public long MaxValue
		{
			get
			{
				return this._max;
			}
			set
			{
				if (this._min > value)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._max = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000146F9 File Offset: 0x000128F9
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00014701 File Offset: 0x00012901
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

		// Token: 0x040002A0 RID: 672
		private long _min = long.MinValue;

		// Token: 0x040002A1 RID: 673
		private long _max = long.MaxValue;

		// Token: 0x040002A2 RID: 674
		private bool _excludeRange;
	}
}
