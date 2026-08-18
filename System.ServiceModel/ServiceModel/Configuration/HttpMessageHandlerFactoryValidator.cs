using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000625 RID: 1573
	internal class HttpMessageHandlerFactoryValidator : ConfigurationValidatorBase
	{
		// Token: 0x06003C6E RID: 15470 RVA: 0x000E6CEB File Offset: 0x000E4EEB
		public override bool CanValidate(Type type)
		{
			return type == typeof(HttpMessageHandlerFactoryElement);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x000E6D00 File Offset: 0x000E4F00
		public override void Validate(object value)
		{
			HttpMessageHandlerFactoryElement httpMessageHandlerFactoryElement = (HttpMessageHandlerFactoryElement)value;
			if (!string.IsNullOrWhiteSpace(httpMessageHandlerFactoryElement.Type) && httpMessageHandlerFactoryElement.Handlers != null && httpMessageHandlerFactoryElement.Handlers.Count > 0)
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("HttpMessageHandlerFactoryConfigInvalid_WithBothTypeAndHandlerList", new object[]
				{
					"messageHandlerFactory",
					"type",
					"handlers"
				})));
			}
		}
	}
}
