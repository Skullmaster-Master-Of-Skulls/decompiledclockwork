using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A4 RID: 1188
	internal class QuerySingleFxEngineResultOpcode : SingleFxEngineResultOpcode
	{
		// Token: 0x06002D7E RID: 11646 RVA: 0x000B1DF1 File Offset: 0x000AFFF1
		internal QuerySingleFxEngineResultOpcode() : base(OpcodeID.QuerySingleFx)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000B1DFC File Offset: 0x000AFFFC
		internal override Opcode Eval(ProcessingContext context)
		{
			SeekableXPathNavigator contextNode = context.Processor.ContextNode;
			XPathResult xpathResult = this.Select(contextNode);
			if (context.Processor.ResultSet == null)
			{
				context.Processor.QueryResult = xpathResult;
			}
			else
			{
				context.Processor.ResultSet.Add(new KeyValuePair<MessageQuery, XPathResult>((MessageQuery)this.item, xpathResult));
			}
			return this.next;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000B1E60 File Offset: 0x000B0060
		internal XPathResult Select(XPathNavigator nav)
		{
			object obj = base.Evaluate(nav);
			XPathResult result;
			switch (this.xpath.ReturnType)
			{
			case XPathResultType.Number:
				result = new XPathResult((double)obj);
				break;
			case XPathResultType.String:
				result = new XPathResult((string)obj);
				break;
			case XPathResultType.Boolean:
				result = new XPathResult((bool)obj);
				break;
			case XPathResultType.NodeSet:
				result = new XPathResult((XPathNodeIterator)obj);
				break;
			default:
				result = new XPathResult(string.Empty);
				break;
			}
			return result;
		}
	}
}
