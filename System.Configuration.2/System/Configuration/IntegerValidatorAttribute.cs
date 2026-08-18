using System;

namespace System.Configuration
{
	// Token: 0x02000066 RID: 102
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00014170 File Offset: 0x00012370
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new IntegerValidator(this._min, this._max, this._excludeRange);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00014189 File Offset: 0x00012389
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00014191 File Offset: 0x00012391
		public int MinValue
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000141B8 File Offset: 0x000123B8
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x000141C0 File Offset: 0x000123C0
		public int MaxValue
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000141E7 File Offset: 0x000123E7
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x000141EF File Offset: 0x000123EF
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

		// Token: 0x0400028A RID: 650
		private int _min = int.MinValue;

		// Token: 0x0400028B RID: 651
		private int _max = int.MaxValue;

		// Token: 0x0400028C RID: 652
		private bool _excludeRange;
	}
}
