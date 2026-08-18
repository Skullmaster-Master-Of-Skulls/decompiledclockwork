using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000098 RID: 152
	internal class ExcCanonicalXml
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000F434 File Offset: 0x0000E434
		internal ExcCanonicalXml(Stream inputStream, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			this.m_c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this.m_c14nDoc.XmlResolver = resolver;
			this.m_c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			this.m_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000F490 File Offset: 0x0000E490
		internal ExcCanonicalXml(XmlDocument document, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.m_c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this.m_c14nDoc.XmlResolver = resolver;
			this.m_c14nDoc.Load(new XmlNodeReader(document));
			this.m_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000F4E8 File Offset: 0x0000E4E8
		internal ExcCanonicalXml(XmlNodeList nodeList, bool includeComments, string inclusiveNamespacesPrefixList, XmlResolver resolver)
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
			this.m_ancMgr = new ExcAncestralNamespaceContextManager(inclusiveNamespacesPrefixList);
			ExcCanonicalXml.MarkInclusionStateForNodes(nodeList, ownerDocument, this.m_c14nDoc);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000F564 File Offset: 0x0000E564
		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.m_c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, this.m_ancMgr);
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			return utf8Encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000F5A0 File Offset: 0x0000E5A0
		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			this.m_c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, this.m_ancMgr);
			hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] result = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F5E8 File Offset: 0x0000E5E8
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
						ExcCanonicalXml.MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes != null)
					{
						for (int j = 0; j < attributes.Count; j++)
						{
							if (Utils.NodeInList(attributes[j], nodeList))
							{
								ExcCanonicalXml.MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
							}
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F6F5 File Offset: 0x0000E6F5
		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}

		// Token: 0x040004F7 RID: 1271
		private CanonicalXmlDocument m_c14nDoc;

		// Token: 0x040004F8 RID: 1272
		private ExcAncestralNamespaceContextManager m_ancMgr;
	}
}
