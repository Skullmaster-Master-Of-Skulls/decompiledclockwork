using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000525 RID: 1317
	internal abstract class XPathMessageFunction : IXsltContextFunction
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x000C10F4 File Offset: 0x000BF2F4
		static XPathMessageFunction()
		{
			XPathMessageFunction.Namespaces.AddNamespace("s11", "http://schemas.xmlsoap.org/soap/envelope/");
			XPathMessageFunction.Namespaces.AddNamespace("s12", "http://www.w3.org/2003/05/soap-envelope");
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000C1149 File Offset: 0x000BF349
		protected XPathMessageFunction(XPathResultType[] argTypes, int max, int min, XPathResultType retType)
		{
			this.argTypes = argTypes;
			this.maxArgs = max;
			this.minArgs = min;
			this.retType = retType;
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000C116E File Offset: 0x000BF36E
		public XPathResultType[] ArgTypes
		{
			get
			{
				return this.argTypes;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000C1176 File Offset: 0x000BF376
		public int Maxargs
		{
			get
			{
				return this.maxArgs;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x000C117E File Offset: 0x000BF37E
		public int Minargs
		{
			get
			{
				return this.minArgs;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000C1186 File Offset: 0x000BF386
		public XPathResultType ReturnType
		{
			get
			{
				return this.retType;
			}
		}

		// Token: 0x06003227 RID: 12839
		public abstract object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);

		// Token: 0x06003228 RID: 12840
		internal abstract void InvokeInternal(ProcessingContext context, int argCount);

		// Token: 0x06003229 RID: 12841 RVA: 0x000C1190 File Offset: 0x000BF390
		internal static bool MoveToAddressingHeader(XPathNavigator nav, string name)
		{
			if (!XPathMessageFunction.MoveToHeader(nav))
			{
				return false;
			}
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			while (!(nav.LocalName == name) || (!(nav.NamespaceURI == "http://www.w3.org/2005/08/addressing") && !(nav.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/08/addressing") && !(nav.NamespaceURI == "http://schemas.microsoft.com/ws/2005/05/addressing/none")))
			{
				if (!nav.MoveToNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000C1200 File Offset: 0x000BF400
		internal static bool MoveToChild(XPathNavigator nav, string name, string ns)
		{
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			while (!(nav.LocalName == name) || !(nav.NamespaceURI == ns))
			{
				if (!nav.MoveToNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000C1234 File Offset: 0x000BF434
		internal static bool MoveToAddressingHeaderSibling(XPathNavigator nav, string name)
		{
			while (nav.MoveToNext())
			{
				if (nav.LocalName == name && (nav.NamespaceURI == "http://www.w3.org/2005/08/addressing" || nav.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/08/addressing"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000C1280 File Offset: 0x000BF480
		internal static bool MoveToSibling(XPathNavigator nav, string name, string ns)
		{
			while (nav.MoveToNext())
			{
				if (nav.LocalName == name && nav.NamespaceURI == ns)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000C12AC File Offset: 0x000BF4AC
		internal static bool MoveToHeader(XPathNavigator nav)
		{
			nav.MoveToRoot();
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			string namespaceURI = nav.NamespaceURI;
			if (nav.LocalName != "Envelope" || (namespaceURI != "http://schemas.xmlsoap.org/soap/envelope/" && namespaceURI != "http://www.w3.org/2003/05/soap-envelope"))
			{
				return false;
			}
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			while (!(nav.LocalName == "Header") || !(nav.NamespaceURI == namespaceURI))
			{
				if (!nav.MoveToNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000C1334 File Offset: 0x000BF534
		internal static bool MoveToBody(XPathNavigator nav)
		{
			nav.MoveToRoot();
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			string namespaceURI = nav.NamespaceURI;
			if (nav.LocalName != "Envelope" || (namespaceURI != "http://schemas.xmlsoap.org/soap/envelope/" && namespaceURI != "http://www.w3.org/2003/05/soap-envelope"))
			{
				return false;
			}
			if (!nav.MoveToFirstChild())
			{
				return false;
			}
			while (!(nav.LocalName == "Body") || !(nav.NamespaceURI == namespaceURI))
			{
				if (!nav.MoveToNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000C13BC File Offset: 0x000BF5BC
		internal static string ToString(object o)
		{
			if (o is bool)
			{
				return QueryValueModel.String((bool)o);
			}
			if (o is string)
			{
				return (string)o;
			}
			if (o is double)
			{
				return QueryValueModel.String((double)o);
			}
			if (o is XPathNodeIterator)
			{
				XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)o;
				xpathNodeIterator.MoveNext();
				return xpathNodeIterator.Current.Value;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryFunctionStringArg")));
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000C143C File Offset: 0x000BF63C
		internal static double ConvertDate(DateTime date)
		{
			if (date.Kind != DateTimeKind.Utc)
			{
				date = date.ToUniversalTime();
			}
			return (date - XPathMessageFunction.ZeroDate).TotalDays;
		}

		// Token: 0x0400270D RID: 9997
		internal static readonly DateTime ZeroDate = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400270E RID: 9998
		internal static readonly XmlNamespaceManager Namespaces = new XmlNamespaceManager(new NameTable());

		// Token: 0x0400270F RID: 9999
		private XPathResultType[] argTypes;

		// Token: 0x04002710 RID: 10000
		private int maxArgs;

		// Token: 0x04002711 RID: 10001
		private int minArgs;

		// Token: 0x04002712 RID: 10002
		private XPathResultType retType;
	}
}
