using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000031 RID: 49
	internal class CanonicalXml
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00006548 File Offset: 0x00004748
		internal CanonicalXml(Stream inputStream, bool includeComments, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			this.m_c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this.m_c14nDoc.XmlResolver = resolver;
			this.m_c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			this.m_ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000065A1 File Offset: 0x000047A1
		internal CanonicalXml(XmlDocument document, XmlResolver resolver) : this(document, resolver, false)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000065AC File Offset: 0x000047AC
		internal CanonicalXml(XmlDocument document, XmlResolver resolver, bool includeComments)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.m_c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this.m_c14nDoc.XmlResolver = resolver;
			this.m_c14nDoc.Load(new XmlNodeReader(document));
			this.m_ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006604 File Offset: 0x00004804
		internal CanonicalXml(XmlNodeList nodeList, XmlResolver resolver, bool includeComments)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			XmlDocument ownerDocument = Utils.GetOwnerDocument(nodeList);
			if (ownerDocument == null)
			{
				throw new ArgumentException("nodeList");
			}
			this.m_c14nDoc = new CanonicalXmlDocument(false, includeComments);
			this.m_c14nDoc.XmlResolver = resolver;
			this.m_c14nDoc.Load(new XmlNodeReader(ownerDocument));
			this.m_ancMgr = new C14NAncestralNamespaceContextManager();
			CanonicalXml.MarkInclusionStateForNodes(nodeList, ownerDocument, this.m_c14nDoc);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000667C File Offset: 0x0000487C
		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006694 File Offset: 0x00004894
		private static void MarkInclusionStateForNodes(XmlNodeList nodeList, XmlDocument inputRoot, XmlDocument root)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			canonicalXmlNodeList.Add(inputRoot);
			canonicalXmlNodeList2.Add(root);
			int num = 0;
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList[num];
				XmlNode xmlNode2 = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				XmlNodeList childNodes2 = xmlNode2.ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					canonicalXmlNodeList.Add(childNodes[i]);
					canonicalXmlNodeList2.Add(childNodes2[i]);
					if (Utils.NodeInList(childNodes[i], nodeList))
					{
						CanonicalXml.MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes != null)
					{
						for (int j = 0; j < attributes.Count; j++)
						{
							if (Utils.NodeInList(attributes[j], nodeList))
							{
								CanonicalXml.MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
							}
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000067A4 File Offset: 0x000049A4
		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.m_c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, this.m_ancMgr);
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			return utf8Encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000067E0 File Offset: 0x000049E0
		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			this.m_c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, this.m_ancMgr);
			hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] result = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return result;
		}

		// Token: 0x040003A4 RID: 932
		private CanonicalXmlDocument m_c14nDoc;

		// Token: 0x040003A5 RID: 933
		private C14NAncestralNamespaceContextManager m_ancMgr;
	}
}
