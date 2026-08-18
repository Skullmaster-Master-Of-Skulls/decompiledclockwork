using System;
using System.Configuration;
using System.Reflection;
using System.Runtime;
using System.Xml;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001D4 RID: 468
	internal class TypeResolveHelper
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x00043FB0 File Offset: 0x000421B0
		public static T Resolve<T>(ConfigurationElementInterceptor customTypeElement, Type customType) where T : class
		{
			if (customTypeElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("customTypeElement");
			}
			if (customType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TypeLoadException(SR.GetString("ID8030", new object[]
				{
					customTypeElement.ElementAsXml.OuterXml
				})));
			}
			T result;
			try
			{
				if (!typeof(T).IsAssignableFrom(customType))
				{
					throw DiagnosticUtility.ThrowHelperConfigurationError(customTypeElement, "type", SR.GetString("ID1029", new object[]
					{
						customType.AssemblyQualifiedName,
						typeof(T)
					}));
				}
				if (customTypeElement.ElementAsXml != null)
				{
					foreach (object obj in customTypeElement.ElementAsXml.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.NodeType != XmlNodeType.Element)
						{
							customTypeElement.ElementAsXml.RemoveChild(xmlNode);
						}
					}
				}
				T t = (T)((object)Activator.CreateInstance(customType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null));
				if (customTypeElement.ElementAsXml != null && customTypeElement.ElementAsXml.ChildNodes.Count > 0)
				{
					ICustomIdentityConfiguration customIdentityConfiguration = t as ICustomIdentityConfiguration;
					if (customIdentityConfiguration != null)
					{
						customIdentityConfiguration.LoadCustomConfiguration(customTypeElement.ElementAsXml.ChildNodes);
					}
				}
				result = t;
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationErrorsException || Fx.IsFatal(ex))
				{
					throw;
				}
				if (ex is TargetInvocationException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ID0012", new object[]
					{
						customType.AssemblyQualifiedName
					}), ex));
				}
				throw DiagnosticUtility.ThrowHelperConfigurationError(customTypeElement, "type", ex);
			}
			return result;
		}
	}
}
