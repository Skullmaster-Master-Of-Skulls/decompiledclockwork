using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000043 RID: 67
	internal class ExcAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00009D55 File Offset: 0x00007F55
		internal ExcAncestralNamespaceContextManager(string inclusiveNamespacesPrefixList)
		{
			this.m_inclusivePrefixSet = Utils.TokenizePrefixListString(inclusiveNamespacesPrefixList);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009D6C File Offset: 0x00007F6C
		private bool HasNonRedundantInclusivePrefix(XmlAttribute attr)
		{
			string namespacePrefix = Utils.GetNamespacePrefix(attr);
			int num;
			return this.m_inclusivePrefixSet.ContainsKey(namespacePrefix) && Utils.IsNonRedundantNamespaceDecl(attr, base.GetNearestRenderedNamespaceWithMatchingPrefix(namespacePrefix, out num));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009DA0 File Offset: 0x00007FA0
		private void GatherNamespaceToRender(string nsPrefix, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			foreach (object obj in nsListToRender.GetKeyList())
			{
				if (Utils.HasNamespacePrefix((XmlAttribute)obj, nsPrefix))
				{
					return;
				}
			}
			XmlAttribute xmlAttribute = (XmlAttribute)nsLocallyDeclared[nsPrefix];
			int num;
			XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(nsPrefix, out num);
			if (xmlAttribute != null)
			{
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(nsPrefix);
					nsListToRender.Add(xmlAttribute, null);
					return;
				}
			}
			else
			{
				int num2;
				XmlAttribute nearestUnrenderedNamespaceWithMatchingPrefix = base.GetNearestUnrenderedNamespaceWithMatchingPrefix(nsPrefix, out num2);
				if (nearestUnrenderedNamespaceWithMatchingPrefix != null && num2 > num && Utils.IsNonRedundantNamespaceDecl(nearestUnrenderedNamespaceWithMatchingPrefix, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00009E5C File Offset: 0x0000805C
		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			this.GatherNamespaceToRender(element.Prefix, nsListToRender, nsLocallyDeclared);
			foreach (object obj in attrListToRender.GetKeyList())
			{
				string prefix = ((XmlAttribute)obj).Prefix;
				if (prefix.Length > 0)
				{
					this.GatherNamespaceToRender(prefix, nsListToRender, nsLocallyDeclared);
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009ED8 File Offset: 0x000080D8
		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			if (!Utils.IsXmlPrefixDefinitionNode(attr))
			{
				if (this.HasNonRedundantInclusivePrefix(attr))
				{
					nsListToRender.Add(attr, null);
					return;
				}
				nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009F01 File Offset: 0x00008101
		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			attrListToRender.Add(attr, null);
		}

		// Token: 0x040003E8 RID: 1000
		private Hashtable m_inclusivePrefixSet;
	}
}
