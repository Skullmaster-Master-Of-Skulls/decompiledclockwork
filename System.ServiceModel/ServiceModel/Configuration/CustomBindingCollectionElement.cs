using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000611 RID: 1553
	public sealed class CustomBindingCollectionElement : BindingCollectionElement
	{
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000E4C9D File Offset: 0x000E2E9D
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CustomBindingElementCollection Bindings
		{
			get
			{
				return (CustomBindingElementCollection)base[""];
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000E4CAF File Offset: 0x000E2EAF
		public override Type BindingType
		{
			get
			{
				return typeof(CustomBinding);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		public override ReadOnlyCollection<IBindingConfigurationElement> ConfiguredBindings
		{
			get
			{
				List<IBindingConfigurationElement> list = new List<IBindingConfigurationElement>();
				foreach (object obj in this.Bindings)
				{
					IBindingConfigurationElement item = (IBindingConfigurationElement)obj;
					list.Add(item);
				}
				return new ReadOnlyCollection<IBindingConfigurationElement>(list);
			}
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000E4D24 File Offset: 0x000E2F24
		public override bool ContainsKey(string name)
		{
			return this.Bindings.ContainsKey(name);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000E4D32 File Offset: 0x000E2F32
		protected internal override Binding GetDefault()
		{
			return Activator.CreateInstance<CustomBinding>();
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000E4D39 File Offset: 0x000E2F39
		internal static CustomBindingCollectionElement GetBindingCollectionElement()
		{
			return (CustomBindingCollectionElement)ConfigurationHelpers.GetBindingCollectionElement("customBinding");
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000E4D4C File Offset: 0x000E2F4C
		private bool TryCreateMatchingExtension(BindingElement bindingElement, ExtensionElementCollection collection, bool allowDerivedTypes, string assemblyName, out BindingElementExtensionElement result)
		{
			result = null;
			foreach (object obj in collection)
			{
				ExtensionElement extensionElement = (ExtensionElement)obj;
				BindingElementExtensionElement bindingElementExtensionElement = Activator.CreateInstance(Type.GetType(extensionElement.Type, true)) as BindingElementExtensionElement;
				if (bindingElementExtensionElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidExtensionType", new object[]
					{
						extensionElement.Type,
						assemblyName,
						"bindingElementExtensions"
					})));
				}
				bool flag;
				if (allowDerivedTypes)
				{
					flag = bindingElementExtensionElement.BindingElementType.IsAssignableFrom(bindingElement.GetType());
				}
				else
				{
					flag = bindingElementExtensionElement.BindingElementType.Equals(bindingElement.GetType());
				}
				if (flag)
				{
					result = bindingElementExtensionElement;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000E4E30 File Offset: 0x000E3030
		protected internal override bool TryAdd(string name, Binding binding, Configuration config)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (config == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("config");
			}
			ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(config);
			CustomBindingElementCollection bindings = sectionGroup.Bindings.CustomBinding.Bindings;
			CustomBindingElement customBindingElement = new CustomBindingElement(name);
			bindings.Add(customBindingElement);
			ExtensionElementCollection bindingElementExtensions = sectionGroup.Extensions.BindingElementExtensions;
			CustomBinding customBinding = (CustomBinding)binding;
			foreach (BindingElement bindingElement in customBinding.Elements)
			{
				BindingElementExtensionElement bindingElementExtensionElement;
				bool flag = this.TryCreateMatchingExtension(bindingElement, bindingElementExtensions, false, customBindingElement.CollectionElementBaseType.AssemblyQualifiedName, out bindingElementExtensionElement);
				if (!flag)
				{
					flag = this.TryCreateMatchingExtension(bindingElement, bindingElementExtensions, true, customBindingElement.CollectionElementBaseType.AssemblyQualifiedName, out bindingElementExtensionElement);
				}
				if (!flag)
				{
					break;
				}
				bindingElementExtensionElement.InitializeFrom(bindingElement);
				customBindingElement.Add(bindingElementExtensionElement);
			}
			bool flag2 = customBindingElement.Count == customBinding.Elements.Count;
			if (!flag2)
			{
				bindings.Remove(customBindingElement);
			}
			return flag2;
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000E4F68 File Offset: 0x000E3168
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(CustomBindingElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C73 RID: 11379
		private ConfigurationPropertyCollection properties;
	}
}
