using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x02000009 RID: 9
	internal struct XPathEvaluator
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00003A2C File Offset: 0x00001C2C
		public object Evaluate<T>(XNode node, string expression, IXmlNamespaceResolver resolver) where T : class
		{
			XPathNavigator xpathNavigator = node.CreateNavigator();
			object obj = xpathNavigator.Evaluate(expression, resolver);
			if (obj is XPathNodeIterator)
			{
				return this.EvaluateIterator<T>((XPathNodeIterator)obj);
			}
			if (!(obj is T))
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_UnexpectedEvaluation", new object[]
				{
					obj.GetType()
				}));
			}
			return (T)((object)obj);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003A90 File Offset: 0x00001C90
		private IEnumerable<T> EvaluateIterator<T>(XPathNodeIterator result)
		{
			foreach (object obj in result)
			{
				XPathNavigator xpathNavigator = (XPathNavigator)obj;
				object r = xpathNavigator.UnderlyingObject;
				if (!(r is T))
				{
					throw new InvalidOperationException(Res.GetString("InvalidOperation_UnexpectedEvaluation", new object[]
					{
						r.GetType()
					}));
				}
				yield return (T)((object)r);
				XText t = r as XText;
				if (t != null && t.parent != null)
				{
					while (t != t.parent.content)
					{
						t = (t.next as XText);
						if (t == null)
						{
							break;
						}
						yield return (T)((object)t);
					}
				}
				r = null;
				t = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}
	}
}
