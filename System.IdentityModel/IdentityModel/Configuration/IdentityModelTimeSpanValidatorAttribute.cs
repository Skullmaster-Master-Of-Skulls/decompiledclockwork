using System;
using System.Configuration;
using System.Runtime;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D5 RID: 469
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class IdentityModelTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x00044174 File Offset: 0x00042374
		public IdentityModelTimeSpanValidatorAttribute()
		{
			this.innerValidatorAttribute = new TimeSpanValidatorAttribute();
			this.innerValidatorAttribute.MaxValueString = TimeoutHelper.MaxWait.ToString();
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000441B0 File Offset: 0x000423B0
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new TimeSpanOrInfiniteValidator(this.MinValue, this.MaxValue);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x000441C3 File Offset: 0x000423C3
		public TimeSpan MinValue
		{
			get
			{
				return this.innerValidatorAttribute.MinValue;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x000441D0 File Offset: 0x000423D0
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x000441DD File Offset: 0x000423DD
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x000441EB File Offset: 0x000423EB
		public TimeSpan MaxValue
		{
			get
			{
				return this.innerValidatorAttribute.MaxValue;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x000441F8 File Offset: 0x000423F8
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x00044205 File Offset: 0x00042405
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

		// Token: 0x04000D94 RID: 3476
		private TimeSpanValidatorAttribute innerValidatorAttribute;
	}
}
