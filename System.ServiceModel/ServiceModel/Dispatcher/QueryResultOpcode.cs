using System;
using System.Runtime;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004EC RID: 1260
	internal class QueryResultOpcode : ResultOpcode
	{
		// Token: 0x06002FB1 RID: 12209 RVA: 0x000B73ED File Offset: 0x000B55ED
		internal QueryResultOpcode() : base(OpcodeID.QueryResult)
		{
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B73F8 File Offset: 0x000B55F8
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			XPathResult queryResult;
			switch (context.Values[topArg.basePtr].Type)
			{
			case ValueDataType.Boolean:
			{
				bool boolean = context.Values[topArg.basePtr].GetBoolean();
				queryResult = new XPathResult(boolean);
				goto IL_D6;
			}
			case ValueDataType.Double:
			{
				double @double = context.Values[topArg.basePtr].GetDouble();
				queryResult = new XPathResult(@double);
				goto IL_D6;
			}
			case ValueDataType.Sequence:
			{
				SafeNodeSequenceIterator nodeSetResult = new SafeNodeSequenceIterator(context.Values[topArg.basePtr].GetSequence(), context);
				queryResult = new XPathResult(nodeSetResult);
				goto IL_D6;
			}
			case ValueDataType.String:
			{
				string @string = context.Values[topArg.basePtr].GetString();
				queryResult = new XPathResult(@string);
				goto IL_D6;
			}
			}
			throw Fx.AssertAndThrow("Unexpected result type.");
			IL_D6:
			context.Processor.QueryResult = queryResult;
			context.PopFrame();
			return this.next;
		}
	}
}
