using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004A3 RID: 1187
	internal class MatchSingleFxEngineResultOpcode : SingleFxEngineResultOpcode
	{
		// Token: 0x06002D7B RID: 11643 RVA: 0x000B1CE0 File Offset: 0x000AFEE0
		internal MatchSingleFxEngineResultOpcode() : base(OpcodeID.MatchSingleFx)
		{
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000B1CEC File Offset: 0x000AFEEC
		internal override Opcode Eval(ProcessingContext context)
		{
			SeekableXPathNavigator contextNode = context.Processor.ContextNode;
			bool flag = this.Match(contextNode);
			context.Processor.Result = flag;
			if (flag && this.item != null && context.Processor.MatchSet != null)
			{
				context.Processor.MatchSet.Add((MessageFilter)this.item);
			}
			return this.next;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000B1D54 File Offset: 0x000AFF54
		internal bool Match(XPathNavigator nav)
		{
			object obj = base.Evaluate(nav);
			bool result;
			switch (this.xpath.ReturnType)
			{
			case XPathResultType.Number:
				result = ((double)obj != 0.0);
				break;
			case XPathResultType.String:
			{
				string text = (string)obj;
				result = (text != null && text.Length > 0);
				break;
			}
			case XPathResultType.Boolean:
				result = (bool)obj;
				break;
			case XPathResultType.NodeSet:
			{
				XPathNodeIterator xpathNodeIterator = (XPathNodeIterator)obj;
				result = (xpathNodeIterator != null && xpathNodeIterator.Count > 0);
				break;
			}
			default:
				result = false;
				break;
			case XPathResultType.Any:
				result = (obj != null);
				break;
			}
			return result;
		}
	}
}
