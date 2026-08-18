using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000663 RID: 1635
	public static class ModelValidatorProviders
	{
		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x0011530F File Offset: 0x0011350F
		public static ModelValidatorProviderCollection Providers
		{
			get
			{
				return ModelValidatorProviders._providers;
			}
		}

		// Token: 0x04002AB8 RID: 10936
		private static readonly ModelValidatorProviderCollection _providers = new ModelValidatorProviderCollection
		{
			new DataAnnotationsModelValidatorProvider()
		};
	}
}
