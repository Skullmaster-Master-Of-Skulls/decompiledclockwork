using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000A9 RID: 169
	[ConfigurationCollection(typeof(ChannelEndpointElement), AddItemName = "endpoint")]
	public sealed class AnnouncementChannelEndpointElementCollection : ServiceModelConfigurationElementCollection<ChannelEndpointElement>
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00012377 File Offset: 0x00010577
		public AnnouncementChannelEndpointElementCollection()
		{
			base.AddElementName = "endpoint";
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001238C File Offset: 0x0001058C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw FxTrace.Exception.ArgumentNull("element");
			}
			ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)element;
			string text = (channelEndpointElement.Address == null) ? "" : channelEndpointElement.Address.ToString().ToUpperInvariant();
			return string.Format(CultureInfo.InvariantCulture, "kind:{0};endpointConfiguration:{1};address:{2};bindingConfiguration:{3};binding:{4};", new object[]
			{
				channelEndpointElement.Kind,
				channelEndpointElement.EndpointConfiguration,
				text,
				channelEndpointElement.BindingConfiguration,
				channelEndpointElement.Binding
			});
		}
	}
}
