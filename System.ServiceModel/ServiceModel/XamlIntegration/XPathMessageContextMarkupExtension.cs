using System;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using System.Windows.Markup;

namespace System.ServiceModel.XamlIntegration
{
	// Token: 0x0200045D RID: 1117
	[ContentProperty("Namespaces")]
	public class XPathMessageContextMarkupExtension : MarkupExtension
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x000A9560 File Offset: 0x000A7760
		static XPathMessageContextMarkupExtension()
		{
			foreach (string item in XPathMessageContext.defaultNamespaces.Keys)
			{
				XPathMessageContextMarkupExtension.implicitPrefixes.Add(item);
			}
			XPathMessageContextMarkupExtension.implicitPrefixes.Add("");
			XPathMessageContextMarkupExtension.implicitPrefixes.Add("xml");
			XPathMessageContextMarkupExtension.implicitPrefixes.Add("xmlns");
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000A95F4 File Offset: 0x000A77F4
		public XPathMessageContextMarkupExtension()
		{
			this.namespaces = new Dictionary<string, string>();
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000A9608 File Offset: 0x000A7808
		public XPathMessageContextMarkupExtension(XPathMessageContext context) : this()
		{
			foreach (object obj in context)
			{
				string text = (string)obj;
				if (!XPathMessageContextMarkupExtension.implicitPrefixes.Contains(text))
				{
					this.namespaces.Add(text, context.LookupNamespace(text));
				}
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000A967C File Offset: 0x000A787C
		public Dictionary<string, string> Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000A9684 File Offset: 0x000A7884
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			XPathMessageContext xpathMessageContext = new XPathMessageContext();
			foreach (KeyValuePair<string, string> keyValuePair in this.namespaces)
			{
				xpathMessageContext.AddNamespace(keyValuePair.Key, keyValuePair.Value);
			}
			return xpathMessageContext;
		}

		// Token: 0x0400240D RID: 9229
		private static List<string> implicitPrefixes = new List<string>();

		// Token: 0x0400240E RID: 9230
		private Dictionary<string, string> namespaces;
	}
}
