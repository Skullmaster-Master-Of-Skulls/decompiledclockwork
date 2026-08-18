using System;
using System.Configuration;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005FC RID: 1532
	[ConfigurationCollection(typeof(ChannelEndpointElement), AddItemName = "endpoint")]
	public sealed class ChannelEndpointElementCollection : ServiceModelEnhancedConfigurationElementCollection<ChannelEndpointElement>
	{
		// Token: 0x06003B1A RID: 15130 RVA: 0x000E29BC File Offset: 0x000E0BBC
		public ChannelEndpointElementCollection() : base("endpoint")
		{
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000E29CC File Offset: 0x000E0BCC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)element;
			return string.Format(CultureInfo.InvariantCulture, "contractType:{0};name:{1}", new object[]
			{
				channelEndpointElement.Contract,
				channelEndpointElement.Name
			});
		}
	}
}
