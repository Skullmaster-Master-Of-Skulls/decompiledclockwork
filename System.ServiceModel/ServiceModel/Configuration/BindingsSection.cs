using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Security;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F8 RID: 1528
	public sealed class BindingsSection : ConfigurationSection, IConfigurationContextProviderInternal
	{
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000E1C90 File Offset: 0x000DFE90
		private Dictionary<string, BindingCollectionElement> BindingCollectionElements
		{
			get
			{
				Dictionary<string, BindingCollectionElement> dictionary = new Dictionary<string, BindingCollectionElement>();
				foreach (object obj in this.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					dictionary.Add(configurationProperty.Name, this[configurationProperty.Name]);
				}
				return dictionary;
			}
		}

		// Token: 0x17000DEF RID: 3567
		public BindingCollectionElement this[string binding]
		{
			get
			{
				return (BindingCollectionElement)base[binding];
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000E1D12 File Offset: 0x000DFF12
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				this.UpdateBindingSections();
				return this.properties;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000E1D33 File Offset: 0x000DFF33
		[ConfigurationProperty("basicHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public BasicHttpBindingCollectionElement BasicHttpBinding
		{
			get
			{
				return (BasicHttpBindingCollectionElement)base["basicHttpBinding"];
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000E1D45 File Offset: 0x000DFF45
		[ConfigurationProperty("basicHttpsBinding", Options = ConfigurationPropertyOptions.None)]
		public BasicHttpsBindingCollectionElement BasicHttpsBinding
		{
			get
			{
				return (BasicHttpsBindingCollectionElement)base["basicHttpsBinding"];
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000E1D57 File Offset: 0x000DFF57
		// (set) Token: 0x06003AD9 RID: 15065 RVA: 0x000E1D5E File Offset: 0x000DFF5E
		private static Configuration Configuration
		{
			get
			{
				return BindingsSection.configuration;
			}
			set
			{
				BindingsSection.configuration = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000E1D66 File Offset: 0x000DFF66
		[ConfigurationProperty("customBinding", Options = ConfigurationPropertyOptions.None)]
		public CustomBindingCollectionElement CustomBinding
		{
			get
			{
				return (CustomBindingCollectionElement)base["customBinding"];
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000E1D78 File Offset: 0x000DFF78
		[ConfigurationProperty("msmqIntegrationBinding", Options = ConfigurationPropertyOptions.None)]
		public MsmqIntegrationBindingCollectionElement MsmqIntegrationBinding
		{
			get
			{
				return (MsmqIntegrationBindingCollectionElement)base["msmqIntegrationBinding"];
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000E1D8A File Offset: 0x000DFF8A
		[ConfigurationProperty("netHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public NetHttpBindingCollectionElement NetHttpBinding
		{
			get
			{
				return (NetHttpBindingCollectionElement)base["netHttpBinding"];
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000E1D9C File Offset: 0x000DFF9C
		[ConfigurationProperty("netHttpsBinding", Options = ConfigurationPropertyOptions.None)]
		public NetHttpsBindingCollectionElement NetHttpsBinding
		{
			get
			{
				return (NetHttpsBindingCollectionElement)base["netHttpsBinding"];
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000E1DAE File Offset: 0x000DFFAE
		[ConfigurationProperty("netPeerTcpBinding", Options = ConfigurationPropertyOptions.None)]
		[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
		public NetPeerTcpBindingCollectionElement NetPeerTcpBinding
		{
			get
			{
				return (NetPeerTcpBindingCollectionElement)base["netPeerTcpBinding"];
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000E1DC0 File Offset: 0x000DFFC0
		[ConfigurationProperty("netMsmqBinding", Options = ConfigurationPropertyOptions.None)]
		public NetMsmqBindingCollectionElement NetMsmqBinding
		{
			get
			{
				return (NetMsmqBindingCollectionElement)base["netMsmqBinding"];
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000E1DD2 File Offset: 0x000DFFD2
		[ConfigurationProperty("netNamedPipeBinding", Options = ConfigurationPropertyOptions.None)]
		public NetNamedPipeBindingCollectionElement NetNamedPipeBinding
		{
			get
			{
				return (NetNamedPipeBindingCollectionElement)base["netNamedPipeBinding"];
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000E1DE4 File Offset: 0x000DFFE4
		[ConfigurationProperty("netTcpBinding", Options = ConfigurationPropertyOptions.None)]
		public NetTcpBindingCollectionElement NetTcpBinding
		{
			get
			{
				return (NetTcpBindingCollectionElement)base["netTcpBinding"];
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000E1DF6 File Offset: 0x000DFFF6
		[ConfigurationProperty("wsFederationHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public WSFederationHttpBindingCollectionElement WSFederationHttpBinding
		{
			get
			{
				return (WSFederationHttpBindingCollectionElement)base["wsFederationHttpBinding"];
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000E1E08 File Offset: 0x000E0008
		[ConfigurationProperty("ws2007FederationHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public WS2007FederationHttpBindingCollectionElement WS2007FederationHttpBinding
		{
			get
			{
				return (WS2007FederationHttpBindingCollectionElement)base["ws2007FederationHttpBinding"];
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000E1E1A File Offset: 0x000E001A
		[ConfigurationProperty("wsHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public WSHttpBindingCollectionElement WSHttpBinding
		{
			get
			{
				return (WSHttpBindingCollectionElement)base["wsHttpBinding"];
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000E1E2C File Offset: 0x000E002C
		[ConfigurationProperty("ws2007HttpBinding", Options = ConfigurationPropertyOptions.None)]
		public WS2007HttpBindingCollectionElement WS2007HttpBinding
		{
			get
			{
				return (WS2007HttpBindingCollectionElement)base["ws2007HttpBinding"];
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000E1E3E File Offset: 0x000E003E
		[ConfigurationProperty("wsDualHttpBinding", Options = ConfigurationPropertyOptions.None)]
		public WSDualHttpBindingCollectionElement WSDualHttpBinding
		{
			get
			{
				return (WSDualHttpBindingCollectionElement)base["wsDualHttpBinding"];
			}
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x000E1E50 File Offset: 0x000E0050
		public static BindingsSection GetSection(Configuration config)
		{
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			return (BindingsSection)config.GetSection(ConfigurationStrings.BindingsSectionGroupPath);
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000E1E78 File Offset: 0x000E0078
		public List<BindingCollectionElement> BindingCollections
		{
			get
			{
				List<BindingCollectionElement> list = new List<BindingCollectionElement>();
				foreach (object obj in this.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					list.Add(this[configurationProperty.Name]);
				}
				return list;
			}
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000E1EE4 File Offset: 0x000E00E4
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigBindingExtensionNotFound", new object[]
			{
				ConfigurationHelpers.GetBindingsSectionPath(elementName)
			})));
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000E1F10 File Offset: 0x000E0110
		internal static bool TryAdd(string name, Binding binding, Configuration config, out string bindingSectionName)
		{
			bool result = false;
			BindingsSection.Configuration = config;
			try
			{
				result = BindingsSection.TryAdd(name, binding, out bindingSectionName);
			}
			finally
			{
				BindingsSection.Configuration = null;
			}
			return result;
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000E1F48 File Offset: 0x000E0148
		internal static bool TryAdd(string name, Binding binding, out string bindingSectionName)
		{
			if (BindingsSection.Configuration == null)
			{
				DiagnosticUtility.FailFast("The TryAdd(string name, Binding binding, Configuration config, out string binding) variant of this function should always be called first. The Configuration object is not set.");
			}
			bool flag = false;
			string text = null;
			BindingsSection section = BindingsSection.GetSection(BindingsSection.Configuration);
			section.UpdateBindingSections();
			foreach (string text2 in section.BindingCollectionElements.Keys)
			{
				BindingCollectionElement bindingCollectionElement = section.BindingCollectionElements[text2];
				if (!(bindingCollectionElement is CustomBindingCollectionElement))
				{
					MethodInfo method = bindingCollectionElement.GetType().GetMethod("TryAdd", BindingFlags.Instance | BindingFlags.NonPublic);
					if (method != null)
					{
						flag = (bool)method.Invoke(bindingCollectionElement, new object[]
						{
							name,
							binding,
							BindingsSection.Configuration
						});
						if (flag)
						{
							text = text2;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				CustomBindingCollectionElement bindingCollectionElement2 = CustomBindingCollectionElement.GetBindingCollectionElement();
				flag = bindingCollectionElement2.TryAdd(name, binding, BindingsSection.Configuration);
				if (flag)
				{
					text = "customBinding";
				}
			}
			bindingSectionName = text;
			return flag;
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000E204C File Offset: 0x000E024C
		private void UpdateBindingSections()
		{
			this.UpdateBindingSections(ConfigurationHelpers.GetEvaluationContext(this));
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000E205C File Offset: 0x000E025C
		[SecuritySafeCritical]
		internal void UpdateBindingSections(ContextInformation evaluationContext)
		{
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection("bindingExtensions", evaluationContext);
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

		// Token: 0x06003AEE RID: 15086 RVA: 0x000E2120 File Offset: 0x000E0320
		[SecuritySafeCritical]
		internal static void ValidateBindingReference(string binding, string bindingConfiguration, ContextInformation evaluationContext, ConfigurationElement configurationElement)
		{
			if (evaluationContext == null)
			{
				DiagnosticUtility.FailFast("ValidateBindingReference() should only called with valid ContextInformation");
			}
			if (!string.IsNullOrEmpty(binding))
			{
				BindingCollectionElement bindingCollectionElement;
				if (evaluationContext != null)
				{
					bindingCollectionElement = ConfigurationHelpers.UnsafeGetAssociatedBindingCollectionElement(evaluationContext, binding);
				}
				else
				{
					bindingCollectionElement = ConfigurationHelpers.UnsafeGetBindingCollectionElement(binding);
				}
				if (bindingCollectionElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidSection", new object[]
					{
						ConfigurationHelpers.GetBindingsSectionPath(binding)
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
				if (!string.IsNullOrEmpty(bindingConfiguration) && !bindingCollectionElement.ContainsKey(bindingConfiguration))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingName", new object[]
					{
						bindingConfiguration,
						ConfigurationHelpers.GetBindingsSectionPath(binding),
						"bindingConfiguration"
					}), configurationElement.ElementInformation.Source, configurationElement.ElementInformation.LineNumber));
				}
			}
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000E21FA File Offset: 0x000E03FA
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000E2202 File Offset: 0x000E0402
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x04002A78 RID: 10872
		private static Configuration configuration;

		// Token: 0x04002A79 RID: 10873
		private ConfigurationPropertyCollection properties;
	}
}
