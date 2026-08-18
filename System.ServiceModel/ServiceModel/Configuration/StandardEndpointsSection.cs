using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D6 RID: 1750
	public sealed class StandardEndpointsSection : ConfigurationSection, IConfigurationContextProviderInternal
	{
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000FFEAC File Offset: 0x000FE0AC
		private Dictionary<string, EndpointCollectionElement> EndpointCollectionElements
		{
			get
			{
				Dictionary<string, EndpointCollectionElement> dictionary = new Dictionary<string, EndpointCollectionElement>();
				foreach (object obj in this.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					dictionary.Add(configurationProperty.Name, this[configurationProperty.Name]);
				}
				return dictionary;
			}
		}

		// Token: 0x17001189 RID: 4489
		public EndpointCollectionElement this[string endpoint]
		{
			get
			{
				return (EndpointCollectionElement)base[endpoint];
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000FFF2E File Offset: 0x000FE12E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				this.UpdateEndpointSections();
				return this.properties;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000FFF4F File Offset: 0x000FE14F
		[ConfigurationProperty("mexEndpoint", Options = ConfigurationPropertyOptions.None)]
		public ServiceMetadataEndpointCollectionElement MexEndpoint
		{
			get
			{
				return (ServiceMetadataEndpointCollectionElement)base["mexEndpoint"];
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060043BE RID: 17342 RVA: 0x000FFF61 File Offset: 0x000FE161
		// (set) Token: 0x060043BF RID: 17343 RVA: 0x000FFF68 File Offset: 0x000FE168
		private static Configuration Configuration
		{
			get
			{
				return StandardEndpointsSection.configuration;
			}
			set
			{
				StandardEndpointsSection.configuration = value;
			}
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000FFF70 File Offset: 0x000FE170
		public static StandardEndpointsSection GetSection(Configuration config)
		{
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			return (StandardEndpointsSection)config.GetSection(ConfigurationStrings.StandardEndpointsSectionPath);
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000FFF98 File Offset: 0x000FE198
		public List<EndpointCollectionElement> EndpointCollections
		{
			get
			{
				List<EndpointCollectionElement> list = new List<EndpointCollectionElement>();
				foreach (object obj in this.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					list.Add(this[configurationProperty.Name]);
				}
				return list;
			}
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x00100004 File Offset: 0x000FE204
		internal static bool TryAdd(string name, ServiceEndpoint endpoint, Configuration config, out string endpointSectionName)
		{
			bool result = false;
			StandardEndpointsSection.Configuration = config;
			try
			{
				result = StandardEndpointsSection.TryAdd(name, endpoint, out endpointSectionName);
			}
			finally
			{
				StandardEndpointsSection.Configuration = null;
			}
			return result;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x0010003C File Offset: 0x000FE23C
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigEndpointExtensionNotFound", new object[]
			{
				ConfigurationHelpers.GetEndpointsSectionPath(elementName)
			})));
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x00100068 File Offset: 0x000FE268
		internal static bool TryAdd(string name, ServiceEndpoint endpoint, out string endpointSectionName)
		{
			if (StandardEndpointsSection.Configuration == null)
			{
				DiagnosticUtility.FailFast("The TryAdd(string name, ServiceEndpoint endpoint, Configuration config, out string endpointSectionName) variant of this function should always be called first. The Configuration object is not set.");
			}
			bool flag = false;
			string text = null;
			StandardEndpointsSection section = StandardEndpointsSection.GetSection(StandardEndpointsSection.Configuration);
			section.UpdateEndpointSections();
			foreach (string text2 in section.EndpointCollectionElements.Keys)
			{
				EndpointCollectionElement endpointCollectionElement = section.EndpointCollectionElements[text2];
				MethodInfo method = endpointCollectionElement.GetType().GetMethod("TryAdd", BindingFlags.Instance | BindingFlags.NonPublic);
				if (method != null)
				{
					flag = (bool)method.Invoke(endpointCollectionElement, new object[]
					{
						name,
						endpoint,
						StandardEndpointsSection.Configuration
					});
					if (flag)
					{
						text = text2;
						break;
					}
				}
			}
			endpointSectionName = text;
			return flag;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x00100140 File Offset: 0x000FE340
		private void UpdateEndpointSections()
		{
			this.UpdateEndpointSections(ConfigurationHelpers.GetEvaluationContext(this));
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x00100150 File Offset: 0x000FE350
		[SecuritySafeCritical]
		internal void UpdateEndpointSections(ContextInformation evaluationContext)
		{
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection("endpointExtensions", evaluationContext);
			if (extensionElementCollection.Count != this.properties.Count)
			{
				foreach (object obj in extensionElementCollection)
				{
					ExtensionElement extensionElement = (ExtensionElement)obj;
					if (extensionElement != null && !this.properties.Contains(extensionElement.Name))
					{
						Type type = Type.GetType(extensionElement.Type, false);
						if (type == null)
						{
							ConfigurationHelpers.TraceExtensionTypeNotFound(extensionElement);
						}
						else
						{
							ConfigurationProperty property = new ConfigurationProperty(extensionElement.Name, type, null, ConfigurationPropertyOptions.None);
							this.properties.Add(property);
						}
					}
				}
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x00100214 File Offset: 0x000FE414
		[SecuritySafeCritical]
		internal static void ValidateEndpointReference(string endpoint, string endpointConfiguration, ContextInformation evaluationContext, ConfigurationElement configurationElement)
		{
			if (evaluationContext == null)
			{
				DiagnosticUtility.FailFast("ValidateEndpointReference() should only called with valid ContextInformation");
			}
			if (!string.IsNullOrEmpty(endpoint))
			{
				EndpointCollectionElement endpointCollectionElement;
				if (evaluationContext != null)
				{
					endpointCollectionElement = ConfigurationHelpers.UnsafeGetAssociatedEndpointCollectionElement(evaluationContext, endpoint);
				}
				else
				{
					endpointCollectionElement = ConfigurationHelpers.UnsafeGetEndpointCollectionElement(endpoint);
				}
				if (endpointCollectionElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidSection", new object[]
					{
						ConfigurationHelpers.GetEndpointsSectionPath(endpoint)
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
				if (!string.IsNullOrEmpty(endpointConfiguration) && !endpointCollectionElement.ContainsKey(endpointConfiguration))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidEndpointName", new object[]
					{
						endpointConfiguration,
						ConfigurationHelpers.GetEndpointsSectionPath(endpoint),
						"endpointConfiguration"
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
			}
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001002EE File Offset: 0x000FE4EE
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x001002F6 File Offset: 0x000FE4F6
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x04002D23 RID: 11555
		private static Configuration configuration;

		// Token: 0x04002D24 RID: 11556
		private ConfigurationPropertyCollection properties;
	}
}
