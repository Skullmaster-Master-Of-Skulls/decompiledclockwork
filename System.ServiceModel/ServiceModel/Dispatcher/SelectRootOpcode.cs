using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004F7 RID: 1271
	internal class SelectRootOpcode : Opcode
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x000B88C5 File Offset: 0x000B6AC5
		internal SelectRootOpcode() : base(OpcodeID.SelectRoot)
		{
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000B88D0 File Offset: 0x000B6AD0
		internal override Opcode Eval(ProcessingContext context)
		{
			int iterationCount = context.IterationCount;
			Opcode opcode = this.next;
			context.PushSequenceFrame();
			NodeSequence nodeSequence = context.CreateSequence();
			if (this.next != null && (this.next.Flags & OpcodeFlags.CompressableSelect) != OpcodeFlags.None)
			{
				SeekableXPathNavigator contextNode = context.Processor.ContextNode;
				contextNode.MoveToRoot();
				for (opcode = this.next.Eval(nodeSequence, contextNode); opcode != null; opcode = opcode.Next)
				{
					if ((opcode.Flags & OpcodeFlags.CompressableSelect) == OpcodeFlags.None)
					{
						break;
					}
				}
			}
			else
			{
				nodeSequence.StartNodeset();
				SeekableXPathNavigator contextNode2 = context.Processor.ContextNode;
				contextNode2.MoveToRoot();
				nodeSequence.Add(contextNode2);
				nodeSequence.StopNodeset();
			}
			if (nodeSequence.Count == 0)
			{
				context.ReleaseSequence(nodeSequence);
				nodeSequence = NodeSequence.Empty;
			}
			for (int i = 0; i < iterationCount; i++)
			{
				context.PushSequence(nodeSequence);
			}
			if (iterationCount > 1)
			{
				nodeSequence.refCount += iterationCount - 1;
			}
			return opcode;
		}
	}
}
