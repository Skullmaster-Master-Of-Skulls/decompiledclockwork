using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000030 RID: 48
	internal abstract class AncestralNamespaceContextManager
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00006397 File Offset: 0x00004597
		internal NamespaceFrame GetScopeAt(int i)
		{
			return (NamespaceFrame)this.m_ancestorStack[i];
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000063AA File Offset: 0x000045AA
		internal NamespaceFrame GetCurrentScope()
		{
			return this.GetScopeAt(this.m_ancestorStack.Count - 1);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000063C0 File Offset: 0x000045C0
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

		// Token: 0x06000139 RID: 313 RVA: 0x00006404 File Offset: 0x00004604
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

		// Token: 0x0600013A RID: 314 RVA: 0x00006445 File Offset: 0x00004645
		internal void EnterElementContext()
		{
			this.m_ancestorStack.Add(new NamespaceFrame());
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006458 File Offset: 0x00004658
		internal void ExitElementContext()
		{
			this.m_ancestorStack.RemoveAt(this.m_ancestorStack.Count - 1);
		}

		// Token: 0x0600013C RID: 316
		internal abstract void TrackNamespaceNode(XmlAttribute attr, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600013D RID: 317
		internal abstract void TrackXmlNamespaceNode(XmlAttribute attr, SortedList nsListToRender, SortedList attrListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600013E RID: 318
		internal abstract void GetNamespacesToRender(XmlElement element, SortedList attrListToRender, SortedList nsListToRender, Hashtable nsLocallyDeclared);

		// Token: 0x0600013F RID: 319 RVA: 0x00006474 File Offset: 0x00004674
		internal void LoadUnrenderedNamespaces(Hashtable nsLocallyDeclared)
		{
			object[] array = new object[nsLocallyDeclared.Count];
			nsLocallyDeclared.Values.CopyTo(array, 0);
			foreach (object obj in array)
			{
				this.AddUnrendered((XmlAttribute)obj);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000064BC File Offset: 0x000046BC
		internal void LoadRenderedNamespaces(SortedList nsRenderedList)
		{
			foreach (object obj in nsRenderedList.GetKeyList())
			{
				this.AddRendered((XmlAttribute)obj);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006518 File Offset: 0x00004718
		internal void AddRendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddRendered(attr);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006526 File Offset: 0x00004726
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.GetCurrentScope().AddUnrendered(attr);
		}

		// Token: 0x040003A3 RID: 931
		internal ArrayList m_ancestorStack = new ArrayList();
	}
}
