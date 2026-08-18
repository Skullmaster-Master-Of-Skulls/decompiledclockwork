using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000095 RID: 149
	internal abstract class AncestralNamespaceContextManager
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000ED18 File Offset: 0x0000DD18
		internal NamespaceFrame GetScopeAt(int i)
		{
			return (NamespaceFrame)this.m_ancestorStack[i];
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000ED2B File Offset: 0x0000DD2B
		internal NamespaceFrame GetCurrentScope()
		{
			return this.GetScopeAt(this.m_ancestorStack.Count - 1);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000ED40 File Offset: 0x0000DD40
		protected XmlAttribute GetNearestRenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this.m_ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute rendered;
				if ((rendered = this.GetScopeAt(i).GetRendered(nsPrefix)) != null)
				{
					depth = i;
					return rendered;
				}
			}
			return null;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000ED84 File Offset: 0x0000DD84
		protected XmlAttribute GetNearestUnrenderedNamespaceWithMatchingPrefix(string nsPrefix, out int depth)
		{
			depth = -1;
			for (int i = this.m_ancestorStack.Count - 1; i >= 0; i--)
			{
				XmlAttribute unrendered;
				if ((unrendered = this.GetScopeAt(i).GetUnrendered(nsPrefix)) != null)
				{
					depth = i;
					return unrendered;
				}
			}
			return null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000EDC5 File Offset: 0x0000DDC5
		internal void EnterElementContext()
		{
			this.m_ancestorStack.Add(new NamespaceFrame());
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000EDD8 File Offset: 0x0000DDD8
		internal void ExitElementContext()
		{
			this.m_ancestorStack.RemoveAt(this.m_ancestorStack.Count - 1);
		}

		// Token: 0x060002B8 RID: 696
		internal abstract void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x060002B9 RID: 697
		internal abstract void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x060002BA RID: 698
		internal abstract void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x060002BB RID: 699 RVA: 0x0000EDF4 File Offset: 0x0000DDF4
		internal void LoadUnrenderedNamespaces(Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (object obj in array)
			{
				this.AddUnrendered((XmlAttribute)obj);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000EE3C File Offset: 0x0000DE3C
		internal void LoadRenderedNamespaces(SortedList nsRenderedList)
		{
			foreach (object obj in nsRenderedList.GetKeyList())
			{
				this.AddRendered((XmlAttribute)obj);
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000EE98 File Offset: 0x0000DE98
		internal void AddRendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddRendered(attr);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000EEA6 File Offset: 0x0000DEA6
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddUnrendered(attr);
		}

		// Token: 0x040004F4 RID: 1268
		internal ArrayList m_ancestorStack = new ArrayList();
	}
}
