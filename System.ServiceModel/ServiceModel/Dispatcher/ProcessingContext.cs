using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004DD RID: 1245
	internal class ProcessingContext
	{
		// Token: 0x06002F2B RID: 12075 RVA: 0x000B634F File Offset: 0x000B454F
		internal ProcessingContext()
		{
			this.valueStack = new EvalStack(2, 4);
			this.sequenceStack = new EvalStack(1, 2);
			this.nodeCount = -1;
		}

		// Token: 0x17000B30 RID: 2864
		internal StackFrame this[int frameIndex]
		{
			get
			{
				return this.valueStack[frameIndex];
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000B6386 File Offset: 0x000B4586
		internal int IterationCount
		{
			get
			{
				if (-1 == this.nodeCount)
				{
					this.nodeCount = this.sequenceStack.CalculateNodecount();
					if (this.nodeCount == 0 && !this.sequenceStack.InUse)
					{
						this.nodeCount = 1;
					}
				}
				return this.nodeCount;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000B63C4 File Offset: 0x000B45C4
		// (set) Token: 0x06002F2F RID: 12079 RVA: 0x000B63CC File Offset: 0x000B45CC
		internal int NodeCount
		{
			get
			{
				return this.nodeCount;
			}
			set
			{
				this.nodeCount = value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000B63D5 File Offset: 0x000B45D5
		// (set) Token: 0x06002F31 RID: 12081 RVA: 0x000B63DD File Offset: 0x000B45DD
		internal ProcessingContext Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000B63E6 File Offset: 0x000B45E6
		// (set) Token: 0x06002F33 RID: 12083 RVA: 0x000B63EE File Offset: 0x000B45EE
		internal QueryProcessor Processor
		{
			get
			{
				return this.processor;
			}
			set
			{
				this.processor = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000B63F7 File Offset: 0x000B45F7
		internal StackFrame SecondArg
		{
			get
			{
				return this.valueStack.SecondArg;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000B6404 File Offset: 0x000B4604
		internal Value[] Sequences
		{
			get
			{
				return this.sequenceStack.Buffer;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000B6411 File Offset: 0x000B4611
		internal bool SequenceStackInUse
		{
			get
			{
				return this.sequenceStack.InUse;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002F37 RID: 12087 RVA: 0x000B641E File Offset: 0x000B461E
		internal bool StacksInUse
		{
			get
			{
				return this.valueStack.frames.Count > 0 || this.sequenceStack.frames.Count > 0;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000B6448 File Offset: 0x000B4648
		internal StackFrame TopArg
		{
			get
			{
				return this.valueStack.TopArg;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000B6455 File Offset: 0x000B4655
		internal StackFrame TopSequenceArg
		{
			get
			{
				return this.sequenceStack.TopArg;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000B6462 File Offset: 0x000B4662
		internal Value[] Values
		{
			get
			{
				return this.valueStack.Buffer;
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000B646F File Offset: 0x000B466F
		internal ProcessingContext Clone()
		{
			return this.processor.CloneContext(this);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000B647D File Offset: 0x000B467D
		internal void ClearContext()
		{
			this.sequenceStack.Clear();
			this.valueStack.Clear();
			this.nodeCount = -1;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000B649C File Offset: 0x000B469C
		internal void CopyFrom(ProcessingContext context)
		{
			this.processor = context.processor;
			if (context.sequenceStack.frames.Count > 0)
			{
				this.sequenceStack.CopyFrom(ref context.sequenceStack);
			}
			else
			{
				this.sequenceStack.Clear();
			}
			if (context.valueStack.frames.Count > 0)
			{
				this.valueStack.CopyFrom(ref context.valueStack);
			}
			else
			{
				this.valueStack.Clear();
			}
			this.nodeCount = context.nodeCount;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000B6524 File Offset: 0x000B4724
		internal NodeSequence CreateSequence()
		{
			NodeSequence nodeSequence = this.processor.PopSequence();
			if (nodeSequence == null)
			{
				nodeSequence = new NodeSequence();
			}
			nodeSequence.OwnerContext = this;
			nodeSequence.refCount++;
			return nodeSequence;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000B655C File Offset: 0x000B475C
		internal bool LoadVariable(int var)
		{
			return this.Processor.LoadVariable(this, var);
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000B656B File Offset: 0x000B476B
		internal void EvalCodeBlock(Opcode block)
		{
			this.processor.Eval(block, this);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000B657A File Offset: 0x000B477A
		internal bool PeekBoolean(int index)
		{
			return this.valueStack.PeekBoolean(index);
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000B6588 File Offset: 0x000B4788
		internal double PeekDouble(int index)
		{
			return this.valueStack.PeekDouble(index);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000B6596 File Offset: 0x000B4796
		internal NodeSequence PeekSequence(int index)
		{
			return this.valueStack.PeekSequence(index);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000B65A4 File Offset: 0x000B47A4
		internal string PeekString(int index)
		{
			return this.valueStack.PeekString(index);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000B65B2 File Offset: 0x000B47B2
		internal void PopFrame()
		{
			this.valueStack.PopFrame(this);
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000B65C0 File Offset: 0x000B47C0
		internal void PopSequenceFrame()
		{
			this.sequenceStack.PopFrame(this);
			this.nodeCount = -1;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000B65D5 File Offset: 0x000B47D5
		internal void PopContextSequenceFrame()
		{
			this.PopSequenceFrame();
			if (!this.sequenceStack.InUse)
			{
				this.sequenceStack.contextOnTopOfStack = false;
			}
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000B65F6 File Offset: 0x000B47F6
		internal void Push(bool boolVal)
		{
			this.valueStack.Push(boolVal);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000B6604 File Offset: 0x000B4804
		internal void Push(bool boolVal, int addCount)
		{
			this.valueStack.Push(boolVal, addCount);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000B6613 File Offset: 0x000B4813
		internal void Push(double doubleVal, int addCount)
		{
			this.valueStack.Push(doubleVal, addCount);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000B6622 File Offset: 0x000B4822
		internal void Push(NodeSequence sequence)
		{
			this.valueStack.Push(sequence);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000B6630 File Offset: 0x000B4830
		internal void Push(NodeSequence sequence, int addCount)
		{
			this.valueStack.Push(sequence, addCount);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000B663F File Offset: 0x000B483F
		internal void Push(string stringVal)
		{
			this.valueStack.Push(stringVal);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000B664D File Offset: 0x000B484D
		internal void Push(string stringVal, int addCount)
		{
			this.valueStack.Push(stringVal, addCount);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000B665C File Offset: 0x000B485C
		internal void PushFrame()
		{
			this.valueStack.PushFrame();
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000B6669 File Offset: 0x000B4869
		internal void PopSequenceFrameToValueStack()
		{
			this.sequenceStack.PopSequenceFrameTo(ref this.valueStack);
			this.nodeCount = -1;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000B6683 File Offset: 0x000B4883
		internal void PushSequence(NodeSequence seq)
		{
			this.sequenceStack.Push(seq);
			this.nodeCount = -1;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000B6698 File Offset: 0x000B4898
		internal void PushSequenceFrame()
		{
			this.sequenceStack.PushFrame();
			this.nodeCount = -1;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000B66AC File Offset: 0x000B48AC
		internal void PushContextSequenceFrame()
		{
			if (!this.sequenceStack.InUse)
			{
				this.sequenceStack.contextOnTopOfStack = true;
			}
			this.PushSequenceFrame();
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000B66CD File Offset: 0x000B48CD
		internal void PushSequenceFrameFromValueStack()
		{
			this.valueStack.PopSequenceFrameTo(ref this.sequenceStack);
			this.nodeCount = -1;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000B66E7 File Offset: 0x000B48E7
		internal void ReleaseSequence(NodeSequence sequence)
		{
			if (this == sequence.OwnerContext)
			{
				sequence.refCount--;
				if (sequence.refCount == 0)
				{
					this.processor.ReleaseSequenceToPool(sequence);
				}
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000B6714 File Offset: 0x000B4914
		internal void Release()
		{
			this.processor.ReleaseContext(this);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000B6722 File Offset: 0x000B4922
		internal void ReplaceSequenceAt(int index, NodeSequence sequence)
		{
			this.sequenceStack.ReplaceAt(index, sequence);
			this.nodeCount = -1;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000B6738 File Offset: 0x000B4938
		internal void SaveVariable(int var, int count)
		{
			this.Processor.SaveVariable(this, var, count);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000B6748 File Offset: 0x000B4948
		internal void SetValue(ProcessingContext context, int index, bool val)
		{
			this.valueStack.SetValue(this, index, val);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000B6758 File Offset: 0x000B4958
		internal void SetValue(ProcessingContext context, int index, double val)
		{
			this.valueStack.SetValue(this, index, val);
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000B6768 File Offset: 0x000B4968
		internal void SetValue(ProcessingContext context, int index, string val)
		{
			this.valueStack.SetValue(this, index, val);
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000B6778 File Offset: 0x000B4978
		internal void SetValue(ProcessingContext context, int index, NodeSequence val)
		{
			this.valueStack.SetValue(this, index, val);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000B6788 File Offset: 0x000B4988
		internal void TransferSequenceSize()
		{
			this.sequenceStack.TransferSequenceSizeTo(ref this.valueStack);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000B679B File Offset: 0x000B499B
		internal void TransferSequencePositions()
		{
			this.sequenceStack.TransferPositionsTo(ref this.valueStack);
		}

		// Token: 0x040025BE RID: 9662
		internal ProcessingContext next;

		// Token: 0x040025BF RID: 9663
		private int nodeCount;

		// Token: 0x040025C0 RID: 9664
		private QueryProcessor processor;

		// Token: 0x040025C1 RID: 9665
		private EvalStack sequenceStack;

		// Token: 0x040025C2 RID: 9666
		private EvalStack valueStack;
	}
}
