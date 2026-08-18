using System;
using System.Configuration;
using System.Runtime;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D3 RID: 1747
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ServiceModelTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x060043AD RID: 17325 RVA: 0x000FFD84 File Offset: 0x000FDF84
		public ServiceModelTimeSpanValidatorAttribute()
		{
			this.innerValidatorAttribute = new TimeSpanValidatorAttribute();
			this.innerValidatorAttribute.MaxValueString = TimeoutHelper.MaxWait.ToString();
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x000FFDC0 File Offset: 0x000FDFC0
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new TimeSpanOrInfiniteValidator(this.MinValue, this.MaxValue);
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000FFDD3 File Offset: 0x000FDFD3
		public TimeSpan MinValue
		{
			get
			{
				return this.innerValidatorAttribute.MinValue;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060043B0 RID: 17328 RVA: 0x000FFDE0 File Offset: 0x000FDFE0
		// (set) Token: 0x060043B1 RID: 17329 RVA: 0x000FFDED File Offset: 0x000FDFED
		public string MinValueString
		{
			get
			{
				return this.innerValidatorAttribute.MinValueString;
			}
			set
			{
				this.innerValidatorAttribute.MinValueString = value;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x000FFDFB File Offset: 0x000FDFFB
		public TimeSpan MaxValue
		{
			get
			{
				return this.innerValidatorAttribute.MaxValue;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000FFE08 File Offset: 0x000FE008
		// (set) Token: 0x060043B4 RID: 17332 RVA: 0x000FFE15 File Offset: 0x000FE015
		public string MaxValueString
		{
			get
			{
				return this.innerValidatorAttribute.MaxValueString;
			}
			set
			{
				this.innerValidatorAttribute.MaxValueString = value;
			}
		}

		// Token: 0x04002D22 RID: 11554
		private TimeSpanValidatorAttribute innerValidatorAttribute;
	}
}
