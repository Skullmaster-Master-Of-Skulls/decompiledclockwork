using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Description
{
	// Token: 0x020003FE RID: 1022
	internal class ConfigWriter
	{
		// Token: 0x060026F9 RID: 9977 RVA: 0x0008ED7C File Offset: 0x0008CF7C
		internal ConfigWriter(Configuration configuration)
		{
			this.bindingTable = new Dictionary<Binding, ConfigWriter.BindingDictionaryValue>();
			this.bindingsSection = BindingsSection.GetSection(configuration);
			ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
			this.channels = sectionGroup.Client.Endpoints;
			this.config = configuration;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008EDC8 File Offset: 0x0008CFC8
		internal ChannelEndpointElement WriteChannelDescription(ServiceEndpoint endpoint, string typeName)
		{
			ConfigWriter.BindingDictionaryValue bindingDictionaryValue = this.CreateBindingConfig(endpoint.Binding);
			ChannelEndpointElement channelEndpointElement = new ChannelEndpointElement(endpoint.Address, typeName);
			channelEndpointElement.Name = NamingHelper.GetUniqueName(NamingHelper.CodeName(endpoint.Name), new NamingHelper.DoesNameExist(this.CheckIfChannelNameInUse), null);
			channelEndpointElement.BindingConfiguration = bindingDictionaryValue.BindingName;
			channelEndpointElement.Binding = bindingDictionaryValue.BindingSectionName;
			this.channels.Add(channelEndpointElement);
			return channelEndpointElement;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008EE3C File Offset: 0x0008D03C
		internal void WriteBinding(Binding binding, out string bindingSectionName, out string configurationName)
		{
			ConfigWriter.BindingDictionaryValue bindingDictionaryValue = this.CreateBindingConfig(binding);
			configurationName = bindingDictionaryValue.BindingName;
			bindingSectionName = bindingDictionaryValue.BindingSectionName;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0008EE64 File Offset: 0x0008D064
		private ConfigWriter.BindingDictionaryValue CreateBindingConfig(Binding binding)
		{
			ConfigWriter.BindingDictionaryValue bindingDictionaryValue;
			if (!this.bindingTable.TryGetValue(binding, out bindingDictionaryValue))
			{
				string uniqueName = NamingHelper.GetUniqueName(NamingHelper.CodeName(binding.Name), new NamingHelper.DoesNameExist(this.CheckIfBindingNameInUse), null);
				string bindingSectionName;
				if (!BindingsSection.TryAdd(uniqueName, binding, this.config, out bindingSectionName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ConfigBindingCannotBeConfigured"), "endpoint.Binding"));
				}
				bindingDictionaryValue = new ConfigWriter.BindingDictionaryValue(uniqueName, bindingSectionName);
				this.bindingTable.Add(binding, bindingDictionaryValue);
			}
			return bindingDictionaryValue;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0008EEE8 File Offset: 0x0008D0E8
		private bool CheckIfBindingNameInUse(string name, object nameCollection)
		{
			foreach (BindingCollectionElement bindingCollectionElement in this.bindingsSection.BindingCollections)
			{
				if (bindingCollectionElement.ContainsKey(name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0008EF4C File Offset: 0x0008D14C
		private bool CheckIfChannelNameInUse(string name, object namingCollection)
		{
			foreach (object obj in this.channels)
			{
				ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
				if (channelEndpointElement.Name == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040021D2 RID: 8658
		private readonly Dictionary<Binding, ConfigWriter.BindingDictionaryValue> bindingTable;

		// Token: 0x040021D3 RID: 8659
		private readonly BindingsSection bindingsSection;

		// Token: 0x040021D4 RID: 8660
		private readonly ChannelEndpointElementCollection channels;

		// Token: 0x040021D5 RID: 8661
		private readonly Configuration config;

		// Token: 0x02000BAF RID: 2991
		private sealed class BindingDictionaryValue
		{
			// Token: 0x06007424 RID: 29732 RVA: 0x001B1AB4 File Offset: 0x001AFCB4
			public BindingDictionaryValue(string bindingName, string bindingSectionName)
			{
				this.BindingName = bindingName;
				this.BindingSectionName = bindingSectionName;
			}

			// Token: 0x040041C8 RID: 16840
			public readonly string BindingName;

			// Token: 0x040041C9 RID: 16841
			public readonly string BindingSectionName;
		}
	}
}
