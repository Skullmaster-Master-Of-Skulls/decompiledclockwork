using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI
{
	// Token: 0x02000336 RID: 822
	public sealed class XPathBinder
	{
		// Token: 0x0600260A RID: 9738 RVA: 0x000030B5 File Offset: 0x000012B5
		private XPathBinder()
		{
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0007D704 File Offset: 0x0007B904
		public static object Eval(object container, string xPath)
		{
			IXmlNamespaceResolver resolver = null;
			return XPathBinder.Eval(container, xPath, resolver);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0007D71C File Offset: 0x0007B91C
		public static object Eval(object container, string xPath, IXmlNamespaceResolver resolver)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (string.IsNullOrEmpty(xPath))
			{
				throw new ArgumentNullException("xPath");
			}
			IXPathNavigable ixpathNavigable = container as IXPathNavigable;
			if (ixpathNavigable == null)
			{
				throw new ArgumentException(SR.GetString("XPathBinder_MustBeIXPathNavigable", new object[]
				{
					container.GetType().FullName
				}));
			}
			XPathNavigator xpathNavigator = ixpathNavigable.CreateNavigator();
			object obj = xpathNavigator.Evaluate(xPath, resolver);
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				if (xpathNodeIterator.MoveNext())
				{
					obj = xpathNodeIterator.Current.Value;
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0007D7AB File Offset: 0x0007B9AB
		public static string Eval(object container, string xPath, string format)
		{
			return XPathBinder.Eval(container, xPath, format, null);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0007D7B8 File Offset: 0x0007B9B8
		public static string Eval(object container, string xPath, string format, IXmlNamespaceResolver resolver)
		{
			object obj = XPathBinder.Eval(container, xPath, resolver);
			if (obj == null)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return obj.ToString();
			}
			return string.Format(format, obj);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0007D7ED File Offset: 0x0007B9ED
		public static IEnumerable Select(object container, string xPath)
		{
			return XPathBinder.Select(container, xPath, null);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0007D7F8 File Offset: 0x0007B9F8
		public static IEnumerable Select(object container, string xPath, IXmlNamespaceResolver resolver)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (string.IsNullOrEmpty(xPath))
			{
				throw new ArgumentNullException("xPath");
			}
			ArrayList arrayList = new ArrayList();
			IXPathNavigable ixpathNavigable = container as IXPathNavigable;
			if (ixpathNavigable == null)
			{
				throw new ArgumentException(SR.GetString("XPathBinder_MustBeIXPathNavigable", new object[]
				{
					container.GetType().FullName
				}));
			}
			XPathNavigator xpathNavigator = ixpathNavigable.CreateNavigator();
			XPathNodeIterator xpathNodeIterator = xpathNavigator.Select(xPath, resolver);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
				IHasXmlNode hasXmlNode = xpathNavigator2 as IHasXmlNode;
				if (hasXmlNode == null)
				{
					throw new InvalidOperationException(SR.GetString("XPathBinder_MustHaveXmlNodes"));
				}
				arrayList.Add(hasXmlNode.GetNode());
			}
			return arrayList;
		}
	}
}
