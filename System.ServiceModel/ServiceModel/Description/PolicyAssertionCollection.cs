using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000418 RID: 1048
	public class PolicyAssertionCollection : Collection<XmlElement>
	{
		// Token: 0x06002816 RID: 10262 RVA: 0x00096EA5 File Offset: 0x000950A5
		public PolicyAssertionCollection()
		{
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00096EAD File Offset: 0x000950AD
		public PolicyAssertionCollection(IEnumerable<XmlElement> elements)
		{
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			this.AddRange(elements);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00096ED0 File Offset: 0x000950D0
		internal void AddRange(IEnumerable<XmlElement> elements)
		{
			foreach (XmlElement item in elements)
			{
				base.Add(item);
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00096F18 File Offset: 0x00095118
		public bool Contains(string localName, string namespaceUri)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			for (int i = 0; i < base.Count; i++)
			{
				XmlElement xmlElement = base[i];
				if (xmlElement.LocalName == localName && xmlElement.NamespaceURI == namespaceUri)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00096F83 File Offset: 0x00095183
		public XmlElement Find(string localName, string namespaceUri)
		{
			return this.Find(localName, namespaceUri, false);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x00096F8E File Offset: 0x0009518E
		public XmlElement Remove(string localName, string namespaceUri)
		{
			return this.Find(localName, namespaceUri, true);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00096F9C File Offset: 0x0009519C
		private XmlElement Find(string localName, string namespaceUri, bool remove)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			for (int i = 0; i < base.Count; i++)
			{
				XmlElement xmlElement = base[i];
				if (xmlElement.LocalName == localName && xmlElement.NamespaceURI == namespaceUri)
				{
					if (remove)
					{
						base.RemoveAt(i);
					}
					return xmlElement;
				}
			}
			return null;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00097011 File Offset: 0x00095211
		public Collection<XmlElement> FindAll(string localName, string namespaceUri)
		{
			return this.FindAll(localName, namespaceUri, false);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0009701C File Offset: 0x0009521C
		public Collection<XmlElement> RemoveAll(string localName, string namespaceUri)
		{
			return this.FindAll(localName, namespaceUri, true);
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00097028 File Offset: 0x00095228
		private Collection<XmlElement> FindAll(string localName, string namespaceUri, bool remove)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			Collection<XmlElement> collection = new Collection<XmlElement>();
			for (int i = 0; i < base.Count; i++)
			{
				XmlElement xmlElement = base[i];
				if (xmlElement.LocalName == localName && xmlElement.NamespaceURI == namespaceUri)
				{
					if (remove)
					{
						base.RemoveAt(i);
						i--;
					}
					collection.Add(xmlElement);
				}
			}
			return collection;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000970AC File Offset: 0x000952AC
		protected override void InsertItem(int index, XmlElement item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000970C9 File Offset: 0x000952C9
		protected override void SetItem(int index, XmlElement item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
