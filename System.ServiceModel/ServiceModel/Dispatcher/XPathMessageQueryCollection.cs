using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200051F RID: 1311
	public class XPathMessageQueryCollection : MessageQueryCollection
	{
		// Token: 0x060031E8 RID: 12776 RVA: 0x000BFA6F File Offset: 0x000BDC6F
		public XPathMessageQueryCollection()
		{
			this.matcher = new InverseQueryMatcher(false);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000BFA84 File Offset: 0x000BDC84
		public override IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (typeof(TResult) == typeof(XPathResult) || typeof(TResult) == typeof(string) || typeof(TResult) == typeof(bool) || typeof(TResult) == typeof(object))
			{
				return (IEnumerable<KeyValuePair<MessageQuery, TResult>>)this.matcher.Evaluate<TResult>(message, false);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TResult", SR.GetString("UnsupportedMessageQueryResultType", new object[]
			{
				typeof(TResult)
			}));
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000BFB50 File Offset: 0x000BDD50
		public override IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (typeof(TResult) == typeof(XPathResult) || typeof(TResult) == typeof(string) || typeof(TResult) == typeof(bool) || typeof(TResult) == typeof(object))
			{
				return (IEnumerable<KeyValuePair<MessageQuery, TResult>>)this.matcher.Evaluate<TResult>(buffer);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TResult", SR.GetString("UnsupportedMessageQueryResultType", new object[]
			{
				typeof(TResult)
			}));
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000BFC1C File Offset: 0x000BDE1C
		protected override void InsertItem(int index, MessageQuery item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			if (!(item is XPathMessageQuery))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item");
			}
			base.InsertItem(index, item);
			XPathMessageQuery xpathMessageQuery = (XPathMessageQuery)item;
			this.matcher.Add(xpathMessageQuery.Expression, xpathMessageQuery.Namespaces, xpathMessageQuery, false);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000BFC7C File Offset: 0x000BDE7C
		protected override void RemoveItem(int index)
		{
			this.matcher.Remove((XPathMessageQuery)base[index]);
			base.RemoveItem(index);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000BFC9C File Offset: 0x000BDE9C
		protected override void SetItem(int index, MessageQuery item)
		{
			if (!(item is XPathMessageQuery))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("item");
			}
			this.matcher.Remove((XPathMessageQuery)base[index]);
			XPathMessageQuery xpathMessageQuery = (XPathMessageQuery)item;
			base.SetItem(index, item);
			this.matcher.Add(xpathMessageQuery.Expression, xpathMessageQuery.Namespaces, xpathMessageQuery, false);
		}

		// Token: 0x04002680 RID: 9856
		private InverseQueryMatcher matcher;
	}
}
