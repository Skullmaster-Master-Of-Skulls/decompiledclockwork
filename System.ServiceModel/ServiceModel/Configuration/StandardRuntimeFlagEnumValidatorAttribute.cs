using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DA RID: 1754
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class StandardRuntimeFlagEnumValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x060043D4 RID: 17364 RVA: 0x00100443 File Offset: 0x000FE643
		public StandardRuntimeFlagEnumValidatorAttribute(Type enumType)
		{
			StandardRuntimeFlagEnumValidatorAttribute.ValidateFlagEnumType(enumType);
			this.EnumType = enumType;
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060043D5 RID: 17365 RVA: 0x00100458 File Offset: 0x000FE658
		// (set) Token: 0x060043D6 RID: 17366 RVA: 0x00100460 File Offset: 0x000FE660
		public Type EnumType
		{
			get
			{
				return this.enumType;
			}
			set
			{
				StandardRuntimeFlagEnumValidatorAttribute.ValidateFlagEnumType(value);
				this.enumType = value;
			}
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x0010046F File Offset: 0x000FE66F
		private static bool IsPowerOfTwo(int value)
		{
			return value > 0 && (value & value - 1) == 0;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x00100480 File Offset: 0x000FE680
		internal static void ValidateFlagEnumType(Type value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("EnumType");
			}
			bool flag = value.GetCustomAttributes(typeof(FlagsAttribute), true).Length != 0;
			if (!value.IsEnum || !flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("EnumType", SR.GetString("FlagEnumTypeExpected", new object[]
				{
					value
				}));
			}
			int[] array = (int[])Enum.GetValues(value);
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != 0 && !StandardRuntimeFlagEnumValidatorAttribute.IsPowerOfTwo(array[i]) && !StandardRuntimeFlagEnumValidatorAttribute.IsCombinedValue(array[i], array, i - 1))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("EnumType", SR.GetString("InvalidFlagEnumType"));
					}
				}
			}
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x00100544 File Offset: 0x000FE744
		internal static bool IsCombinedValue(int combinedValue, int[] allowedValues, int startPosition)
		{
			int num = startPosition;
			while (num >= 0 && combinedValue > 0)
			{
				if ((combinedValue & allowedValues[num]) == allowedValues[num])
				{
					combinedValue -= allowedValues[num];
				}
				num--;
			}
			return combinedValue == 0;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x00100576 File Offset: 0x000FE776
		private void EnsureValidatorType()
		{
			if (this.validatorType == null)
			{
				this.validatorType = typeof(StandardRuntimeFlagEnumValidator<>).MakeGenericType(new Type[]
				{
					this.enumType
				});
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x001005AA File Offset: 0x000FE7AA
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				this.EnsureValidatorType();
				return (ConfigurationValidatorBase)Activator.CreateInstance(this.validatorType, null);
			}
		}

		// Token: 0x04002D27 RID: 11559
		private Type enumType;

		// Token: 0x04002D28 RID: 11560
		private Type validatorType;
	}
}
