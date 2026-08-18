using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000032 RID: 50
	internal class C14NAncestralNamespaceContextManager : AncestralNamespaceContextManager
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00006827 File Offset: 0x00004A27
		internal C14NAncestralNamespaceContextManager()
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006830 File Offset: 0x00004A30
		private void GetNamespaceToRender(string nsPrefix, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			foreach (object obj in nsListToRender.GetKeyList())
			{
				if (Utils.HasNamespacePrefix((XmlAttribute)obj, nsPrefix))
				{
					return;
				}
			}
			foreach (object obj2 in attrListToRender.GetKeyList())
			{
				if (((XmlAttribute)obj2).LocalName.Equals(nsPrefix))
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
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
						return;
					}
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
					if (Utils.IsXmlNamespaceNode(nearestUnrenderedNamespaceWithMatchingPrefix))
					{
						attrListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
						return;
					}
					nsListToRender.Add(nearestUnrenderedNamespaceWithMatchingPrefix, null);
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006974 File Offset: 0x00004B74
		internal override void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (object obj in array)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				int num;
				XmlAttribute nearestRenderedNamespaceWithMatchingPrefix = base.GetNearestRenderedNamespaceWithMatchingPrefix(Utils.GetNamespacePrefix(xmlAttribute), out num);
				if (Utils.IsNonRedundantNamespaceDecl(xmlAttribute, nearestRenderedNamespaceWithMatchingPrefix))
				{
					nsLocallyDeclared.Remove(Utils.GetNamespacePrefix(xmlAttribute));
					if (Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						attrListToRender.Add(xmlAttribute, null);
					}
					else
					{
						nsListToRender.Add(xmlAttribute, null);
					}
				}
			}
			for (int j = this.m_ancestorStack.Count - 1; j >= 0; j--)
			{
				foreach (object obj2 in base.GetScopeAt(j).GetUnrendered().Values)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj2;
					if (xmlAttribute != null)
					{
						this.GetNamespaceToRender(Utils.GetNamespacePrefix(xmlAttribute), attrListToRender, nsListToRender, nsLocallyDeclared);
					}
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006A88 File Offset: 0x00004C88
		internal override void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006A97 File Offset: 0x00004C97
		internal override void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared)
		{
			nsLocallyDeclared.Add(Utils.GetNamespacePrefix(attr), attr);
		}
	}
}
