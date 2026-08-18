using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Xml.Linq
{
	// Token: 0x0200002D RID: 45
	[__DynamicallyInvokable]
	public static class Extensions
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00009AF8 File Offset: 0x00007CF8
		[__DynamicallyInvokable]
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAttributes(source, null);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009B0F File Offset: 0x00007D0F
		[__DynamicallyInvokable]
		public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XAttribute.EmptySequence;
			}
			return Extensions.GetAttributes(source, name);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009B35 File Offset: 0x00007D35
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAncestors<T>(source, null, false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009B4D File Offset: 0x00007D4D
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source, XName name) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetAncestors<T>(source, name, false);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009B74 File Offset: 0x00007D74
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetAncestors<XElement>(source, null, true);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009B8C File Offset: 0x00007D8C
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetAncestors<XElement>(source, name, true);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009BB3 File Offset: 0x00007DB3
		[__DynamicallyInvokable]
		public static IEnumerable<XNode> Nodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (T t in source)
			{
				XContainer root = t;
				if (root != null)
				{
					XNode i = root.LastNode;
					if (i != null)
					{
						do
						{
							i = i.next;
							yield return i;
						}
						while (i.parent == root && i != root.content);
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009BC3 File Offset: 0x00007DC3
		[__DynamicallyInvokable]
		public static IEnumerable<XNode> DescendantNodes<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendantNodes<T>(source, false);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009BDA File Offset: 0x00007DDA
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendants<T>(source, null, false);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009BF2 File Offset: 0x00007DF2
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetDescendants<T>(source, name, false);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009C19 File Offset: 0x00007E19
		[__DynamicallyInvokable]
		public static IEnumerable<XNode> DescendantNodesAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendantNodes<XElement>(source, true);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009C30 File Offset: 0x00007E30
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetDescendants<XElement>(source, null, true);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009C48 File Offset: 0x00007E48
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source, XName name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetDescendants<XElement>(source, name, true);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009C6F File Offset: 0x00007E6F
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Extensions.GetElements<T>(source, null);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009C86 File Offset: 0x00007E86
		[__DynamicallyInvokable]
		public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source, XName name) where T : XContainer
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return Extensions.GetElements<T>(source, name);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009CAC File Offset: 0x00007EAC
		[__DynamicallyInvokable]
		public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : XNode
		{
			return source.OrderBy((T n) => n, XNode.DocumentOrderComparer);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009CD8 File Offset: 0x00007ED8
		[__DynamicallyInvokable]
		public static void Remove(this IEnumerable<XAttribute> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (XAttribute xattribute in new List<XAttribute>(source))
			{
				if (xattribute != null)
				{
					xattribute.Remove();
				}
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009D3C File Offset: 0x00007F3C
		[__DynamicallyInvokable]
		public static void Remove<T>(this IEnumerable<T> source) where T : XNode
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (T t in new List<T>(source))
			{
				if (t != null)
				{
					t.Remove();
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009DAC File Offset: 0x00007FAC
		private static IEnumerable<XAttribute> GetAttributes(IEnumerable<XElement> source, XName name)
		{
			foreach (XElement e in source)
			{
				if (e != null)
				{
					XAttribute a = e.lastAttr;
					if (a != null)
					{
						do
						{
							a = a.next;
							if (name == null || a.name == name)
							{
								yield return a;
							}
						}
						while (a.parent == e && a != e.lastAttr);
					}
					a = null;
				}
				e = null;
			}
			IEnumerator<XElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009DC3 File Offset: 0x00007FC3
		private static IEnumerable<XElement> GetAncestors<T>(IEnumerable<T> source, XName name, bool self) where T : XNode
		{
			foreach (T t in source)
			{
				XNode xnode = t;
				if (xnode != null)
				{
					XElement e;
					for (e = ((self ? xnode : xnode.parent) as XElement); e != null; e = (e.parent as XElement))
					{
						if (name == null || e.name == name)
						{
							yield return e;
						}
					}
					e = null;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009DE1 File Offset: 0x00007FE1
		private static IEnumerable<XNode> GetDescendantNodes<T>(IEnumerable<T> source, bool self) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer root = t;
				if (root != null)
				{
					if (self)
					{
						yield return root;
					}
					XNode i = root;
					for (;;)
					{
						XContainer xcontainer = i as XContainer;
						XNode firstNode;
						if (xcontainer != null && (firstNode = xcontainer.FirstNode) != null)
						{
							i = firstNode;
						}
						else
						{
							while (i != null && i != root && i == i.parent.content)
							{
								i = i.parent;
							}
							if (i == null || i == root)
							{
								break;
							}
							i = i.next;
						}
						yield return i;
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009DF8 File Offset: 0x00007FF8
		private static IEnumerable<XElement> GetDescendants<T>(IEnumerable<T> source, XName name, bool self) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer root = t;
				if (root != null)
				{
					if (self)
					{
						XElement xelement = (XElement)root;
						if (name == null || xelement.name == name)
						{
							yield return xelement;
						}
					}
					XNode i = root;
					XContainer xcontainer = root;
					for (;;)
					{
						if (xcontainer != null && xcontainer.content is XNode)
						{
							i = ((XNode)xcontainer.content).next;
						}
						else
						{
							while (i != null && i != root && i == i.parent.content)
							{
								i = i.parent;
							}
							if (i == null || i == root)
							{
								break;
							}
							i = i.next;
						}
						XElement e = i as XElement;
						if (e != null && (name == null || e.name == name))
						{
							yield return e;
						}
						xcontainer = e;
						e = null;
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009E16 File Offset: 0x00008016
		private static IEnumerable<XElement> GetElements<T>(IEnumerable<T> source, XName name) where T : XContainer
		{
			foreach (T t in source)
			{
				XContainer root = t;
				if (root != null)
				{
					XNode i = root.content as XNode;
					if (i != null)
					{
						do
						{
							i = i.next;
							XElement xelement = i as XElement;
							if (xelement != null && (name == null || xelement.name == name))
							{
								yield return xelement;
							}
						}
						while (i.parent == root && i != root.content);
					}
					i = null;
				}
				root = null;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}
	}
}
