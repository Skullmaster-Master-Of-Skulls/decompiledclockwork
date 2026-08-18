using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A7 RID: 1959
	internal static class ContextBindingElementPolicy
	{
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x00110A1C File Offset: 0x0010EC1C
		private static XmlDocument Document
		{
			get
			{
				if (ContextBindingElementPolicy.document == null)
				{
					ContextBindingElementPolicy.document = new XmlDocument();
				}
				return ContextBindingElementPolicy.document;
			}
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00110A34 File Offset: 0x0010EC34
		public static void ExportRequireContextAssertion(ContextBindingElement bindingElement, PolicyAssertionCollection assertions)
		{
			if (bindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElement");
			}
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			if (bindingElement.ContextExchangeMechanism == ContextExchangeMechanism.ContextSoapHeader)
			{
				XmlElement xmlElement = ContextBindingElementPolicy.Document.CreateElement(null, "IncludeContext", "http://schemas.microsoft.com/ws/2006/05/context");
				XmlAttribute xmlAttribute = ContextBindingElementPolicy.Document.CreateAttribute("ProtectionLevel");
				ProtectionLevel protectionLevel = bindingElement.ProtectionLevel;
				if (protectionLevel != ProtectionLevel.Sign)
				{
					if (protectionLevel == ProtectionLevel.EncryptAndSign)
					{
						xmlAttribute.Value = "EncryptAndSign";
					}
					else
					{
						xmlAttribute.Value = "None";
					}
				}
				else
				{
					xmlAttribute.Value = "Sign";
				}
				xmlElement.Attributes.Append(xmlAttribute);
				assertions.Add(xmlElement);
				return;
			}
			XmlElement item = ContextBindingElementPolicy.Document.CreateElement(null, "HttpUseCookie", "http://schemas.xmlsoap.org/soap/http");
			assertions.Add(item);
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x00110AFC File Offset: 0x0010ECFC
		public static bool TryGetHttpUseCookieAssertion(ICollection<XmlElement> assertions, out XmlElement httpUseCookieAssertion)
		{
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			httpUseCookieAssertion = null;
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.LocalName == "HttpUseCookie" && xmlElement.NamespaceURI == "http://schemas.xmlsoap.org/soap/http" && xmlElement.ChildNodes.Count == 0)
				{
					httpUseCookieAssertion = xmlElement;
					break;
				}
			}
			return httpUseCookieAssertion != null;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00110B90 File Offset: 0x0010ED90
		private static bool ContainOnlyWhitespaceChild(XmlElement parent)
		{
			if (parent.ChildNodes.Count == 0)
			{
				return true;
			}
			foreach (object obj in parent.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00110C00 File Offset: 0x0010EE00
		public static bool TryImportRequireContextAssertion(PolicyAssertionCollection assertions, out ContextBindingElement bindingElement)
		{
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			bindingElement = null;
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.LocalName == "IncludeContext" && xmlElement.NamespaceURI == "http://schemas.microsoft.com/ws/2006/05/context" && ContextBindingElementPolicy.ContainOnlyWhitespaceChild(xmlElement))
				{
					string attribute = xmlElement.GetAttribute("ProtectionLevel");
					if ("EncryptAndSign".Equals(attribute, StringComparison.Ordinal))
					{
						bindingElement = new ContextBindingElement(ProtectionLevel.EncryptAndSign);
					}
					else if ("Sign".Equals(attribute, StringComparison.Ordinal))
					{
						bindingElement = new ContextBindingElement(ProtectionLevel.Sign);
					}
					else if ("None".Equals(attribute, StringComparison.Ordinal))
					{
						bindingElement = new ContextBindingElement(ProtectionLevel.None);
					}
					if (bindingElement != null)
					{
						assertions.Remove(xmlElement);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04002EFC RID: 12028
		private const string EncryptAndSignName = "EncryptAndSign";

		// Token: 0x04002EFD RID: 12029
		private const string HttpNamespace = "http://schemas.xmlsoap.org/soap/http";

		// Token: 0x04002EFE RID: 12030
		private const string HttpUseCookieName = "HttpUseCookie";

		// Token: 0x04002EFF RID: 12031
		private const string IncludeContextName = "IncludeContext";

		// Token: 0x04002F00 RID: 12032
		private const string NoneName = "None";

		// Token: 0x04002F01 RID: 12033
		private const string ProtectionLevelName = "ProtectionLevel";

		// Token: 0x04002F02 RID: 12034
		private const string SignName = "Sign";

		// Token: 0x04002F03 RID: 12035
		private const string WscNamespace = "http://schemas.microsoft.com/ws/2006/05/context";

		// Token: 0x04002F04 RID: 12036
		private static XmlDocument document;
	}
}
