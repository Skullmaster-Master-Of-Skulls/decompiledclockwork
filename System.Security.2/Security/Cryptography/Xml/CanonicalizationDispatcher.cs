using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000021 RID: 33
	internal class CanonicalizationDispatcher
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000044A9 File Offset: 0x000026A9
		private CanonicalizationDispatcher()
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000051D0 File Offset: 0x000033D0
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

		// Token: 0x060000DA RID: 218 RVA: 0x00005250 File Offset: 0x00003450
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

		// Token: 0x060000DB RID: 219 RVA: 0x000052BC File Offset: 0x000034BC
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

		// Token: 0x060000DC RID: 220 RVA: 0x0000533C File Offset: 0x0000353C
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

		// Token: 0x04000392 RID: 914
		[ThreadStatic]
		private static int t_depth;
	}
}
