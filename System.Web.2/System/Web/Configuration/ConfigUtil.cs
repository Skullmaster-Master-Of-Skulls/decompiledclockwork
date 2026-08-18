using System;
using System.Configuration;
using System.Threading;
using System.Web.Compilation;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006C7 RID: 1735
	internal class ConfigUtil
	{
		// Token: 0x060053C4 RID: 21444 RVA: 0x000030B5 File Offset: 0x000012B5
		private ConfigUtil()
		{
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x0012663C File Offset: 0x0012483C
		internal static void CheckBaseType(Type expectedBaseType, Type userBaseType, string propertyName, ConfigurationElement configElement)
		{
			if (!expectedBaseType.IsAssignableFrom(userBaseType))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_type_to_inherit_from", new object[]
				{
					userBaseType.FullName,
					expectedBaseType.FullName
				}), configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber);
			}
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x001266A8 File Offset: 0x001248A8
		internal static Type GetType(string typeName, string propertyName, ConfigurationElement configElement, XmlNode node, bool checkAptcaBit, bool ignoreCase)
		{
			Type type;
			try
			{
				type = BuildManager.GetType(typeName, true, ignoreCase);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (node != null)
				{
					throw new ConfigurationErrorsException(ex.Message, ex, node);
				}
				if (configElement != null)
				{
					throw new ConfigurationErrorsException(ex.Message, ex, configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber);
				}
				throw new ConfigurationErrorsException(ex.Message, ex);
			}
			if (checkAptcaBit)
			{
				if (node != null)
				{
					HttpRuntime.FailIfNoAPTCABit(type, node);
				}
				else
				{
					HttpRuntime.FailIfNoAPTCABit(type, (configElement != null) ? configElement.ElementInformation : null, propertyName);
				}
			}
			return type;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0012676C File Offset: 0x0012496C
		internal static Type GetType(string typeName, string propertyName, ConfigurationElement configElement)
		{
			return ConfigUtil.GetType(typeName, propertyName, configElement, true);
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00126777 File Offset: 0x00124977
		internal static Type GetType(string typeName, string propertyName, ConfigurationElement configElement, bool checkAptcaBit)
		{
			return ConfigUtil.GetType(typeName, propertyName, configElement, checkAptcaBit, false);
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00126783 File Offset: 0x00124983
		internal static Type GetType(string typeName, string propertyName, ConfigurationElement configElement, bool checkAptcaBit, bool ignoreCase)
		{
			return ConfigUtil.GetType(typeName, propertyName, configElement, null, checkAptcaBit, ignoreCase);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00126791 File Offset: 0x00124991
		internal static Type GetType(string typeName, XmlNode node)
		{
			return ConfigUtil.GetType(typeName, node, false);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0012679B File Offset: 0x0012499B
		internal static Type GetType(string typeName, XmlNode node, bool ignoreCase)
		{
			return ConfigUtil.GetType(typeName, null, null, node, true, ignoreCase);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x001267A8 File Offset: 0x001249A8
		internal static void CheckAssignableType(Type baseType, Type type, ConfigurationElement configElement, string propertyName)
		{
			if (!baseType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}), configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber);
			}
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x00126814 File Offset: 0x00124A14
		internal static void CheckAssignableType(Type baseType, Type baseType2, Type type, ConfigurationElement configElement, string propertyName)
		{
			if (!baseType.IsAssignableFrom(type) && !baseType2.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Type_doesnt_inherit_from_type", new object[]
				{
					type.FullName,
					baseType.FullName
				}), configElement.ElementInformation.Properties[propertyName].Source, configElement.ElementInformation.Properties[propertyName].LineNumber);
			}
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00126889 File Offset: 0x00124A89
		internal static bool IsTypeHandlerOrFactory(Type t)
		{
			return typeof(IHttpHandler).IsAssignableFrom(t) || typeof(IHttpHandlerFactory).IsAssignableFrom(t);
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x001268AF File Offset: 0x00124AAF
		internal static ConfigurationErrorsException MakeConfigurationErrorsException(string message, Exception innerException = null, PropertyInformation configProperty = null)
		{
			return new ConfigurationErrorsException(message, innerException, (configProperty != null) ? configProperty.Source : null, (configProperty != null) ? configProperty.LineNumber : 0);
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x001268D0 File Offset: 0x00124AD0
		internal static void SetFX45DefaultValue(ConfigurationSection configSection, ConfigurationProperty property, object newDefaultValue)
		{
			if (BinaryCompatibility.Current.TargetsAtLeastFramework45 && !configSection.IsReadOnly())
			{
				PropertyInformation propertyInformation = configSection.ElementInformation.Properties[property.Name];
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.Default)
				{
					try
					{
						propertyInformation.Value = newDefaultValue;
					}
					catch (ConfigurationErrorsException)
					{
					}
				}
			}
		}
	}
}
