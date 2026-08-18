using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006CC RID: 1740
	public sealed class CustomError : ConfigurationElement
	{
		// Token: 0x060053DB RID: 21467 RVA: 0x00126A14 File Offset: 0x00124C14
		static CustomError()
		{
			CustomError._properties = new ConfigurationPropertyCollection();
			CustomError._properties.Add(CustomError._propStatusCode);
			CustomError._properties.Add(CustomError._propRedirect);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00117E9E File Offset: 0x0011609E
		internal CustomError()
		{
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00126A92 File Offset: 0x00124C92
		public CustomError(int statusCode, string redirect) : this()
		{
			this.StatusCode = statusCode;
			this.Redirect = redirect;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00126AA8 File Offset: 0x00124CA8
		public override bool Equals(object customError)
		{
			CustomError customError2 = customError as CustomError;
			return customError2 != null && customError2.StatusCode == this.StatusCode && customError2.Redirect == this.Redirect;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00126AE0 File Offset: 0x00124CE0
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this.StatusCode, this.Redirect.GetHashCode());
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x00126AF8 File Offset: 0x00124CF8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomError._properties;
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x00126AFF File Offset: 0x00124CFF
		// (set) Token: 0x060053E2 RID: 21474 RVA: 0x00126B11 File Offset: 0x00124D11
		[ConfigurationProperty("statusCode", IsRequired = true, IsKey = true)]
		[IntegerValidator(MinValue = 100, MaxValue = 999)]
		public int StatusCode
		{
			get
			{
				return (int)base[CustomError._propStatusCode];
			}
			set
			{
				base[CustomError._propStatusCode] = value;
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x00126B24 File Offset: 0x00124D24
		// (set) Token: 0x060053E4 RID: 21476 RVA: 0x00126B36 File Offset: 0x00124D36
		[ConfigurationProperty("redirect", IsRequired = true)]
		[StringValidator(MinLength = 1)]
		public string Redirect
		{
			get
			{
				return (string)base[CustomError._propRedirect];
			}
			set
			{
				base[CustomError._propRedirect] = value;
			}
		}

		// Token: 0x04002C22 RID: 11298
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C23 RID: 11299
		private static readonly ConfigurationProperty _propStatusCode = new ConfigurationProperty("statusCode", typeof(int), null, null, new IntegerValidator(100, 999), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C24 RID: 11300
		private static readonly ConfigurationProperty _propRedirect = new ConfigurationProperty("redirect", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
