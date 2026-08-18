using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000626 RID: 1574
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class HttpMessageHandlerFactoryValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000E6D77 File Offset: 0x000E4F77
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new HttpMessageHandlerFactoryValidator();
			}
		}
	}
}
