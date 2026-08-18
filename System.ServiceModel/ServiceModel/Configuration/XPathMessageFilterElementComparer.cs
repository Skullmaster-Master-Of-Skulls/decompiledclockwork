using System;
using System.Collections;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006EC RID: 1772
	public class XPathMessageFilterElementComparer : IComparer
	{
		// Token: 0x06004415 RID: 17429 RVA: 0x00101288 File Offset: 0x000FF488
		int IComparer.Compare(object x, object y)
		{
			string strA = this.TranslateObjectToElementKey(x);
			string strB = this.TranslateObjectToElementKey(y);
			return string.Compare(strA, strB, StringComparison.Ordinal);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x001012AD File Offset: 0x000FF4AD
		internal static string ParseXPathString(XPathMessageFilter filter)
		{
			return XPathMessageFilterElementComparer.ParseXPathString(filter, false);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x001012B8 File Offset: 0x000FF4B8
		internal static string ParseXPathString(XPathMessageFilter filter, bool throwOnFailure)
		{
			XPathLexer lexer = new XPathLexer(filter.XPath);
			return XPathMessageFilterElementComparer.ParseXPathString(lexer, filter.Namespaces, throwOnFailure);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x001012E0 File Offset: 0x000FF4E0
		private static string ParseXPathString(XPathLexer lexer, XmlNamespaceManager namespaceManager, bool throwOnFailure)
		{
			string result = string.Empty;
			int firstTokenChar = lexer.FirstTokenChar;
			if (lexer.MoveNext())
			{
				XPathToken token = lexer.Token;
				StringBuilder stringBuilder = new StringBuilder(XPathMessageFilterElementComparer.ParseXPathString(lexer, namespaceManager, throwOnFailure));
				if (XPathTokenID.NameTest == token.TokenID)
				{
					string prefix = token.Prefix;
					if (!string.IsNullOrEmpty(prefix))
					{
						string text = namespaceManager.LookupNamespace(prefix);
						if (!string.IsNullOrEmpty(text))
						{
							stringBuilder = stringBuilder.Replace(prefix, text, firstTokenChar, prefix.Length);
						}
						else if (throwOnFailure)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new IndexOutOfRangeException(SR.GetString("ConfigXPathNamespacePrefixNotFound", new object[]
							{
								prefix
							})));
						}
					}
				}
				result = stringBuilder.ToString();
			}
			else
			{
				result = lexer.ConsumedSubstring();
			}
			return result;
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x0010139C File Offset: 0x000FF59C
		private string TranslateObjectToElementKey(object obj)
		{
			string text = null;
			if (obj.GetType().IsAssignableFrom(typeof(XPathMessageFilter)))
			{
				text = XPathMessageFilterElementComparer.ParseXPathString((XPathMessageFilter)obj);
			}
			else if (obj.GetType().IsAssignableFrom(typeof(XPathMessageFilterElement)))
			{
				text = XPathMessageFilterElementComparer.ParseXPathString(((XPathMessageFilterElement)obj).Filter);
			}
			else if (obj.GetType().IsAssignableFrom(typeof(string)))
			{
				text = (string)obj;
			}
			if (string.IsNullOrEmpty(text))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ConfigCannotParseXPathFilter", new object[]
				{
					obj.GetType().AssemblyQualifiedName
				})));
			}
			return text;
		}
	}
}
