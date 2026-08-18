using System;

namespace System.Configuration
{
	// Token: 0x02000092 RID: 146
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class StringValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001CB03 File Offset: 0x0001AD03
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new StringValidator(this._minLength, this._maxLength, this._invalidChars);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0001CB24 File Offset: 0x0001AD24
		public int MinLength
		{
			get
			{
				return this._minLength;
			}
			set
			{
				if (this._maxLength < value)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._minLength = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001CB4B File Offset: 0x0001AD4B
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0001CB53 File Offset: 0x0001AD53
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				if (this._minLength > value)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("Validator_min_greater_than_max"));
				}
				this._maxLength = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001CB7A File Offset: 0x0001AD7A
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001CB82 File Offset: 0x0001AD82
		public string InvalidCharacters
		{
			get
			{
				return this._invalidChars;
			}
			set
			{
				this._invalidChars = value;
			}
		}

		// Token: 0x04000351 RID: 849
		private int _minLength;

		// Token: 0x04000352 RID: 850
		private int _maxLength = int.MaxValue;

		// Token: 0x04000353 RID: 851
		private string _invalidChars;
	}
}
