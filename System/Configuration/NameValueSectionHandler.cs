using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000702 RID: 1794
	public class NameValueSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x000EB338 File Offset: 0x000EA338
		public object Create(object parent, object context, XmlNode section)
		{
			return NameValueSectionHandler.CreateStatic(parent, section, this.KeyAttributeName, this.ValueAttributeName);
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000EB34D File Offset: 0x000EA34D
		internal static object CreateStatic(object parent, XmlNode section)
		{
			return NameValueSectionHandler.CreateStatic(parent, section, "key", "value");
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000EB360 File Offset: 0x000EA360
		internal static object CreateStatic(object parent, XmlNode section, string keyAttriuteName, string valueAttributeName)
		{
			ReadOnlyNameValueCollection readOnlyNameValueCollection;
			if (parent == null)
			{
				readOnlyNameValueCollection = new ReadOnlyNameValueCollection(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				ReadOnlyNameValueCollection value = (ReadOnlyNameValueCollection)parent;
				readOnlyNameValueCollection = new ReadOnlyNameValueCollection(value);
			}
			HandlerBase.CheckForUnrecognizedAttributes(section);
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "add")
					{
						string name = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
						string value2 = HandlerBase.RemoveRequiredAttribute(xmlNode, valueAttributeName, true);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection[name] = value2;
					}
					else if (xmlNode.Name == "remove")
					{
						string name2 = HandlerBase.RemoveRequiredAttribute(xmlNode, keyAttriuteName);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection.Remove(name2);
					}
					else if (xmlNode.Name.Equals("clear"))
					{
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						readOnlyNameValueCollection.Clear();
					}
					else
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
				}
			}
			readOnlyNameValueCollection.SetReadOnly();
			return readOnlyNameValueCollection;
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000EB47C File Offset: 0x000EA47C
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000EB483 File Offset: 0x000EA483
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}

		// Token: 0x040031C8 RID: 12744
		private const string defaultKeyAttribute = "key";

		// Token: 0x040031C9 RID: 12745
		private const string defaultValueAttribute = "value";
	}
}
