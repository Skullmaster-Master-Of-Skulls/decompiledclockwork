using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000086 RID: 134
	internal class CanonicalizationDispatcher
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000DB22 File Offset: 0x0000CB22
		private CanonicalizationDispatcher()
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DB2C File Offset: 0x0000CB2C
		public static void Write(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			int dangerousMaxRecursionDepth = Utils.GetDangerousMaxRecursionDepth();
			if (dangerousMaxRecursionDepth > 0 && CanonicalizationDispatcher.t_depth > dangerousMaxRecursionDepth)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_DEPTH_EXCEEDED");
			}
			CanonicalizationDispatcher.t_depth++;
			try
			{
				if (node is ICanonicalizableNode)
				{
					((ICanonicalizableNode)node).Write(strBuilder, docPos, anc);
				}
				else
				{
					CanonicalizationDispatcher.WriteGenericNode(node, strBuilder, docPos, anc);
				}
			}
			finally
			{
				CanonicalizationDispatcher.t_depth--;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DBAC File Offset: 0x0000CBAC
		public static void WriteGenericNode(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			XmlNodeList childNodes = node.ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode node2 = (XmlNode)obj;
				CanonicalizationDispatcher.Write(node2, strBuilder, docPos, anc);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DC18 File Offset: 0x0000CC18
		public static void WriteHash(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			int dangerousMaxRecursionDepth = Utils.GetDangerousMaxRecursionDepth();
			if (dangerousMaxRecursionDepth > 0 && CanonicalizationDispatcher.t_depth > dangerousMaxRecursionDepth)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "MAX_DEPTH_EXCEEDED");
			}
			CanonicalizationDispatcher.t_depth++;
			try
			{
				if (node is ICanonicalizableNode)
				{
					((ICanonicalizableNode)node).WriteHash(hash, docPos, anc);
				}
				else
				{
					CanonicalizationDispatcher.WriteHashGenericNode(node, hash, docPos, anc);
				}
			}
			finally
			{
				CanonicalizationDispatcher.t_depth--;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DC98 File Offset: 0x0000CC98
		public static void WriteHashGenericNode(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			XmlNodeList childNodes = node.ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode node2 = (XmlNode)obj;
				CanonicalizationDispatcher.WriteHash(node2, hash, docPos, anc);
			}
		}

		// Token: 0x040004E3 RID: 1251
		[ThreadStatic]
		private static int t_depth;
	}
}
