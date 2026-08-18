using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Security;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F7 RID: 1527
	public abstract class BindingCollectionElement : ConfigurationElement, IConfigurationContextProviderInternal
	{
		// Token: 0x06003AC8 RID: 15048
		protected internal abstract Binding GetDefault();

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x000E1B23 File Offset: 0x000DFD23
		public string BindingName
		{
			get
			{
				if (string.IsNullOrEmpty(this.bindingName))
				{
					this.bindingName = this.GetBindingName();
				}
				return this.bindingName;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003ACA RID: 15050
		public abstract Type BindingType { get; }

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003ACB RID: 15051
		public abstract ReadOnlyCollection<IBindingConfigurationElement> ConfiguredBindings { get; }

		// Token: 0x06003ACC RID: 15052
		public abstract bool ContainsKey(string name);

		// Token: 0x06003ACD RID: 15053 RVA: 0x000E1B44 File Offset: 0x000DFD44
		[SecuritySafeCritical]
		private string GetBindingName()
		{
			string text = string.Empty;
			Type type = base.GetType();
			ExtensionElementCollection extensionElementCollection = ExtensionsSection.UnsafeLookupCollection("bindingExtensions", ConfigurationHelpers.GetEvaluationContext(this));
			if (extensionElementCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigExtensionCollectionNotFound", new object[]
				{
					"bindingExtensions"
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
					"bindingExtensions"
				}), base.ElementInformation.Source, base.ElementInformation.LineNumber));
			}
			return text;
		}

		// Token: 0x06003ACE RID: 15054
		protected internal abstract bool TryAdd(string name, Binding binding, Configuration config);

		// Token: 0x06003ACF RID: 15055 RVA: 0x000E1C68 File Offset: 0x000DFE68
		ContextInformation IConfigurationContextProviderInternal.GetEvaluationContext()
		{
			return base.EvaluationContext;
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000E1C70 File Offset: 0x000DFE70
		ContextInformation IConfigurationContextProviderInternal.GetOriginalEvaluationContext()
		{
			return null;
		}

		// Token: 0x04002A77 RID: 10871
		private string bindingName = string.Empty;
	}
}
