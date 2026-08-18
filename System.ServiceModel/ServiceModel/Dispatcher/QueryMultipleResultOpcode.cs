using System;
using System.Collections.Generic;
using System.Runtime;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004EE RID: 1262
	internal class QueryMultipleResultOpcode : MultipleResultOpcode
	{
		// Token: 0x06002FBB RID: 12219 RVA: 0x000B75EA File Offset: 0x000B57EA
		internal QueryMultipleResultOpcode() : base(OpcodeID.QueryMultipleResult)
		{
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000B75F4 File Offset: 0x000B57F4
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			XPathResult xpathResult;
			switch (context.Values[topArg.basePtr].Type)
			{
			case ValueDataType.Boolean:
			{
				bool boolean = context.Values[topArg.basePtr].GetBoolean();
				xpathResult = new XPathResult(boolean);
				goto IL_D6;
			}
			case ValueDataType.Double:
			{
				double @double = context.Values[topArg.basePtr].GetDouble();
				xpathResult = new XPathResult(@double);
				goto IL_D6;
			}
			case ValueDataType.Sequence:
			{
				SafeNodeSequenceIterator nodeSetResult = new SafeNodeSequenceIterator(context.Values[topArg.basePtr].GetSequence(), context);
				xpathResult = new XPathResult(nodeSetResult);
				goto IL_D6;
			}
			case ValueDataType.String:
			{
				string @string = context.Values[topArg.basePtr].GetString();
				xpathResult = new XPathResult(@string);
				goto IL_D6;
			}
			}
			throw Fx.AssertAndThrow("Unexpected result type.");
			IL_D6:
			context.Processor.ResultSet.Add(new KeyValuePair<MessageQuery, XPathResult>((MessageQuery)this.results[0], xpathResult));
			for (int i = 1; i < this.results.Count; i++)
			{
				context.Processor.ResultSet.Add(new KeyValuePair<MessageQuery, XPathResult>((MessageQuery)this.results[i], xpathResult.Copy()));
			}
			context.PopFrame();
			return this.next;
		}
	}
}
