using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CA RID: 1738
	[ConfigurationCollection(typeof(ExtensionElement), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ExtensionElementCollection : ServiceModelConfigurationElementCollection<ExtensionElement>
	{
		// Token: 0x0600433A RID: 17210 RVA: 0x000FE05E File Offset: 0x000FC25E
		public ExtensionElementCollection() : base(ConfigurationElementCollectionType.BasicMap, "add")
		{
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000FE06C File Offset: 0x000FC26C
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (!this.InheritedElementExists((ExtensionElement)element))
			{
				this.EnforceUniqueElement((ExtensionElement)element);
				base.BaseAdd(element);
			}
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000FE08F File Offset: 0x000FC28F
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!this.InheritedElementExists((ExtensionElement)element))
			{
				this.EnforceUniqueElement((ExtensionElement)element);
				base.BaseAdd(index, element);
			}
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000FE0B4 File Offset: 0x000FC2B4
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ExtensionElement extensionElement = (ExtensionElement)element;
			return extensionElement.Name;
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000FE0E4 File Offset: 0x000FC2E4
		private bool InheritedElementExists(ExtensionElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (this.ContainsKey(elementKey))
			{
				ExtensionElement extensionElement = (ExtensionElement)base.BaseGet(elementKey);
				if (extensionElement != null && !extensionElement.ElementInformation.IsPresent && element.Type.Equals(extensionElement.Type, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000FE138 File Offset: 0x000FC338
		private void EnforceUniqueElement(ExtensionElement element)
		{
			foreach (object obj in this)
			{
				ExtensionElement extensionElement = (ExtensionElement)obj;
				if (element.Name.Equals(extensionElement.Name, StringComparison.Ordinal))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigDuplicateExtensionName", new object[]
					{
						element.Name
					})));
				}
				bool flag = false;
				if (element.Type.Equals(extensionElement.Type, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else if (element.TypeName.Equals(extensionElement.TypeName, StringComparison.Ordinal))
				{
					Type type = Type.GetType(element.Type, false);
					if (null != type && type.Equals(Type.GetType(extensionElement.Type, false)))
					{
						flag = true;
					}
				}
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigDuplicateExtensionType", new object[]
					{
						element.Type
					})));
				}
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06004340 RID: 17216 RVA: 0x000FE254 File Offset: 0x000FC454
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}
	}
}
