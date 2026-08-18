using System;
using System.Collections;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006F7 RID: 1783
	public class DictionarySectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x0600370F RID: 14095 RVA: 0x000EA370 File Offset: 0x000E9370
		public virtual object Create(object parent, object context, XmlNode section)
		{
			Hashtable hashtable;
			if (parent == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = (Hashtable)((Hashtable)parent).Clone();
			}
			HandlerBase.CheckForUnrecognizedAttributes(section);
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
				{
					if (xmlNode.Name == "add")
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						string key = HandlerBase.RemoveRequiredAttribute(xmlNode, this.KeyAttributeName);
						string text;
						if (this.ValueRequired)
						{
							text = HandlerBase.RemoveRequiredAttribute(xmlNode, this.ValueAttributeName);
						}
						else
						{
							text = HandlerBase.RemoveAttribute(xmlNode, this.ValueAttributeName);
						}
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						if (text == null)
						{
							text = "";
						}
						hashtable[key] = text;
					}
					else if (xmlNode.Name == "remove")
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						string key2 = HandlerBase.RemoveRequiredAttribute(xmlNode, this.KeyAttributeName);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						hashtable.Remove(key2);
					}
					else if (xmlNode.Name.Equals("clear"))
					{
						HandlerBase.CheckForChildNodes(xmlNode);
						HandlerBase.CheckForUnrecognizedAttributes(xmlNode);
						hashtable.Clear();
					}
					else
					{
						HandlerBase.ThrowUnrecognizedElement(xmlNode);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000EA4C8 File Offset: 0x000E94C8
		protected virtual string KeyAttributeName
		{
			get
			{
				return "key";
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000EA4CF File Offset: 0x000E94CF
		protected virtual string ValueAttributeName
		{
			get
			{
				return "value";
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000EA4D6 File Offset: 0x000E94D6
		internal virtual bool ValueRequired
		{
			get
			{
				return false;
			}
		}
	}
}
