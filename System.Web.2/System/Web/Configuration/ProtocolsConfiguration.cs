using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x02000739 RID: 1849
	internal class ProtocolsConfiguration
	{
		// Token: 0x0600593A RID: 22842 RVA: 0x0013742C File Offset: 0x0013562C
		internal ProtocolsConfiguration(XmlNode section)
		{
			HandlerBase.CheckForUnrecognizedAttributes(section);
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!this.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "add")
					{
						string text = HandlerBase.RemoveRequiredAttribute(xmlNode, "id");
						string processHandlerType = HandlerBase.RemoveRequiredAttribute(xmlNode, "processHandlerType");
						string appDomainHandlerType = HandlerBase.RemoveRequiredAttribute(xmlNode, "appDomainHandlerType");
						bool validate = true;
						HandlerBase.GetAndRemoveBooleanAttribute(xmlNode, "validate", ref validate);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						HandlerBase.CheckForNonCommentChildNodes(xmlNode);
						try
						{
							this._protocolEntries[text] = new ProtocolsConfigurationEntry(text, processHandlerType, appDomainHandlerType, validate, ConfigurationErrorsException.GetFilename(xmlNode), ConfigurationErrorsException.GetLineNumber(xmlNode));
							continue;
						}
						catch
						{
							continue;
						}
					}
					HandlerBase.ThrowUnrecognizedElement(xmlNode);
				}
			}
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00137538 File Offset: 0x00135738
		private bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
			{
				return true;
			}
			if (node.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
			}
			return false;
		}

		// Token: 0x04002F51 RID: 12113
		private Hashtable _protocolEntries = new Hashtable();
	}
}
