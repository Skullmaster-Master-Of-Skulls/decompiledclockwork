using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061D RID: 1565
	public abstract class EndpointCollectionElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x06003C1A RID: 15386
		protected internal abstract StandardEndpointElement GetDefaultStandardEndpointElement();

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000E5B94 File Offset: 0x000E3D94
		public string EndpointName
		{
			get
			{
				if (string.IsNullOrEmpty(this.endpointName))
				{
					this.endpointName = this.GetEndpointName();
				}
				return this.endpointName;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003C1C RID: 15388
		public abstract Type EndpointType { get; }

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003C1D RID: 15389
		public abstract ReadOnlyCollection<StandardEndpointElement> ConfiguredEndpoints { get; }

		// Token: 0x06003C1E RID: 15390
		public abstract bool ContainsKey(string name);

		// Token: 0x06003C1F RID: 15391
		protected internal abstract bool TryAdd(string name, ServiceEndpoint endpoint, Configuration config);

		// Token: 0x06003C20 RID: 15392 RVA: 0x000E5BB8 File Offset: 0x000E3DB8
		[SecuritySafeCritical]
		private string GetEndpointName()
		{
			string text = string.Empty;
			Type type = base.GetType();
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection("endpointExtensions", ConfigurationHelpers.GetEvaluationContext(this));
			if (extensionElementCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigExtensionCollectionNotFound", new object[]
				{
					"endpointExtensions"
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			for (int i = 0; i < extensionElementCollection.Count; i++)
			{
				ExtensionElement extensionElement = extensionElementCollection[i];
				if (extensionElement.Type.Equals(type.AssemblyQualifiedName, StringComparison.Ordinal))
				{
					text = extensionElement.Name;
					break;
				}
				Type type2 = Type.GetType(extensionElement.Type, false);
				if (null != type2 && type.Equals(type2))
				{
					text = extensionElement.Name;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigExtensionTypeNotRegisteredInCollection", new object[]
				{
					type.AssemblyQualifiedName,
					"endpointExtensions"
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			return text;
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x000E5CDC File Offset: 0x000E3EDC
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000E5CE4 File Offset: 0x000E3EE4
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x04002C7B RID: 11387
		private string endpointName = string.Empty;
	}
}
