using System;

namespace System.Configuration
{
	// Token: 0x02000091 RID: 145
	public class StringValidator : ConfigurationValidatorBase
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0001C9BF File Offset: 0x0001ABBF
		public StringValidator(int minLength) : this(minLength, int.MaxValue, null)
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001C9CE File Offset: 0x0001ABCE
		public StringValidator(int minLength, int maxLength) : this(minLength, maxLength, null)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001C9D9 File Offset: 0x0001ABD9
		public StringValidator(int minLength, int maxLength, string invalidCharacters)
		{
			this._minLength = minLength;
			this._maxLength = maxLength;
			this._invalidChars = invalidCharacters;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019E56 File Offset: 0x00018056
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public override void Validate(object value)
		{
			ValidatorUtils.HelperParamValidation(value, typeof(string));
			string text = value as string;
			int num = (text == null) ? 0 : text.Length;
			if (num < this._minLength)
			{
				throw new ArgumentException(SR.GetString("Validator_string_min_length", new object[]
				{
					this._minLength
				}));
			}
			if (num > this._maxLength)
			{
				throw new ArgumentException(SR.GetString("Validator_string_max_length", new object[]
				{
					this._maxLength
				}));
			}
			if (num > 0 && this._invalidChars != null && this._invalidChars.Length > 0)
			{
				char[] array = new char[this._invalidChars.Length];
				this._invalidChars.CopyTo(0, array, 0, this._invalidChars.Length);
				if (text.IndexOfAny(array) != -1)
				{
					throw new ArgumentException(SR.GetString("Validator_string_invalid_chars", new object[]
					{
						this._invalidChars
					}));
				}
			}
		}

		// Token: 0x0400034E RID: 846
		private int _minLength;

		// Token: 0x0400034F RID: 847
		private int _maxLength;

		// Token: 0x04000350 RID: 848
		private string _invalidChars;
	}
}
