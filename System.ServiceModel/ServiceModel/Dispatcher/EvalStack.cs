using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FF RID: 1279
	internal struct EvalStack
	{
		// Token: 0x06003056 RID: 12374 RVA: 0x000B8E00 File Offset: 0x000B7000
		internal EvalStack(int frameCapacity, int stackCapacity)
		{
			this.buffer = new QueryBuffer<Value>(frameCapacity + stackCapacity);
			this.stack = new StackRegion(new QueryRange(0, stackCapacity - 1));
			this.buffer.Reserve(stackCapacity);
			this.frames = new StackRegion(new QueryRange(stackCapacity, stackCapacity + frameCapacity - 1));
			this.buffer.Reserve(frameCapacity);
			this.contextOnTopOfStack = false;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x000B8E64 File Offset: 0x000B7064
		internal Value[] Buffer
		{
			get
			{
				return this.buffer.buffer;
			}
		}

		// Token: 0x17000B7B RID: 2939
		internal StackFrame this[int frameIndex]
		{
			get
			{
				return this.buffer.buffer[this.frames.stackPtr - frameIndex].Frame;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000B8E95 File Offset: 0x000B7095
		internal StackFrame SecondArg
		{
			get
			{
				return this[1];
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000B8E9E File Offset: 0x000B709E
		internal StackFrame TopArg
		{
			get
			{
				return this[0];
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000B8EA7 File Offset: 0x000B70A7
		internal void Clear()
		{
			this.stack.Clear();
			this.frames.Clear();
			this.contextOnTopOfStack = false;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000B8EC6 File Offset: 0x000B70C6
		internal void CopyFrom(ref EvalStack stack)
		{
			this.buffer.CopyFrom(ref stack.buffer);
			this.frames = stack.frames;
			this.stack = stack.stack;
			this.contextOnTopOfStack = stack.contextOnTopOfStack;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000B8F00 File Offset: 0x000B7100
		internal int CalculateNodecount()
		{
			if (this.stack.stackPtr < 0)
			{
				return 0;
			}
			StackFrame topArg = this.TopArg;
			int num = 0;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				num += this.buffer[i].NodeCount;
			}
			return num;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000B8F54 File Offset: 0x000B7154
		private void GrowFrames()
		{
			int count = this.frames.Count;
			this.buffer.ReserveAt(this.frames.bounds.end + 1, count);
			this.frames.Grow(count);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000B8F98 File Offset: 0x000B7198
		private void GrowStack(int growthNeeded)
		{
			int num = this.stack.bounds.Count;
			if (growthNeeded > num)
			{
				num = growthNeeded;
			}
			this.buffer.ReserveAt(this.stack.bounds.end + 1, num);
			this.stack.Grow(num);
			this.frames.Shift(num);
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000B8FF2 File Offset: 0x000B71F2
		internal bool InUse
		{
			get
			{
				if (this.contextOnTopOfStack)
				{
					return this.frames.Count > 1;
				}
				return this.frames.Count > 0;
			}
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000B9019 File Offset: 0x000B7219
		internal bool PeekBoolean(int index)
		{
			return this.buffer.buffer[index].GetBoolean();
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000B9031 File Offset: 0x000B7231
		internal double PeekDouble(int index)
		{
			return this.buffer.buffer[index].GetDouble();
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000B9049 File Offset: 0x000B7249
		internal NodeSequence PeekSequence(int index)
		{
			return this.buffer.buffer[index].GetSequence();
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000B9061 File Offset: 0x000B7261
		internal string PeekString(int index)
		{
			return this.buffer.buffer[index].GetString();
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000B907C File Offset: 0x000B727C
		internal void PopFrame(ProcessingContext context)
		{
			StackFrame topArg = this.TopArg;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				this.buffer.buffer[i].Clear(context);
			}
			this.stack.stackPtr = topArg.basePtr - 1;
			this.frames.stackPtr = this.frames.stackPtr - 1;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000B90E0 File Offset: 0x000B72E0
		internal void PushFrame()
		{
			this.frames.stackPtr = this.frames.stackPtr + 1;
			if (this.frames.NeedsGrowth)
			{
				this.GrowFrames();
			}
			this.buffer.buffer[this.frames.stackPtr].StartFrame(this.stack.stackPtr);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B913C File Offset: 0x000B733C
		internal void PopSequenceFrameTo(ref EvalStack dest)
		{
			StackFrame topArg = this.TopArg;
			dest.PushFrame();
			int count = topArg.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					dest.Push(this.buffer.buffer, topArg.basePtr, count);
				}
				else
				{
					dest.Push(this.buffer.buffer[topArg.basePtr].Sequence);
				}
			}
			this.stack.stackPtr = topArg.basePtr - 1;
			this.frames.stackPtr = this.frames.stackPtr - 1;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B91C4 File Offset: 0x000B73C4
		internal void Push(string val)
		{
			this.stack.stackPtr = this.stack.stackPtr + 1;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(1);
			}
			this.buffer.buffer[this.stack.stackPtr].String = val;
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B9244 File Offset: 0x000B7444
		internal void Push(string val, int addCount)
		{
			int i = this.stack.stackPtr;
			this.stack.stackPtr = this.stack.stackPtr + addCount;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(addCount);
			}
			int num = i + addCount;
			while (i < num)
			{
				this.buffer.buffer[++i].String = val;
			}
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000B92D4 File Offset: 0x000B74D4
		internal void Push(bool val)
		{
			this.stack.stackPtr = this.stack.stackPtr + 1;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(1);
			}
			this.buffer.buffer[this.stack.stackPtr].Boolean = val;
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000B9354 File Offset: 0x000B7554
		internal void Push(bool val, int addCount)
		{
			int i = this.stack.stackPtr;
			this.stack.stackPtr = this.stack.stackPtr + addCount;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(addCount);
			}
			int num = i + addCount;
			while (i < num)
			{
				this.buffer.buffer[++i].Boolean = val;
			}
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000B93E4 File Offset: 0x000B75E4
		internal void Push(double val)
		{
			this.stack.stackPtr = this.stack.stackPtr + 1;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(1);
			}
			this.buffer.buffer[this.stack.stackPtr].Double = val;
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B9464 File Offset: 0x000B7664
		internal void Push(double val, int addCount)
		{
			int i = this.stack.stackPtr;
			this.stack.stackPtr = this.stack.stackPtr + addCount;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(addCount);
			}
			int num = i + addCount;
			while (i < num)
			{
				this.buffer.buffer[++i].Double = val;
			}
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B94F4 File Offset: 0x000B76F4
		internal void Push(NodeSequence val)
		{
			this.stack.stackPtr = this.stack.stackPtr + 1;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(1);
			}
			this.buffer.buffer[this.stack.stackPtr].Sequence = val;
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000B9574 File Offset: 0x000B7774
		internal void Push(NodeSequence val, int addCount)
		{
			val.refCount += addCount - 1;
			int i = this.stack.stackPtr;
			this.stack.stackPtr = this.stack.stackPtr + addCount;
			if (this.stack.NeedsGrowth)
			{
				this.GrowStack(addCount);
			}
			int num = i + addCount;
			while (i < num)
			{
				this.buffer.buffer[++i].Sequence = val;
			}
			this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000B9614 File Offset: 0x000B7814
		internal void Push(Value[] buffer, int startAt, int addCount)
		{
			if (addCount > 0)
			{
				int num = this.stack.stackPtr + 1;
				this.stack.stackPtr = this.stack.stackPtr + addCount;
				if (this.stack.NeedsGrowth)
				{
					this.GrowStack(addCount);
				}
				if (1 == addCount)
				{
					this.buffer.buffer[num] = buffer[startAt];
				}
				else
				{
					Array.Copy(buffer, startAt, this.buffer.buffer, num, addCount);
				}
				this.buffer.buffer[this.frames.stackPtr].FrameEndPtr = this.stack.stackPtr;
			}
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000B96B7 File Offset: 0x000B78B7
		internal void ReplaceAt(int index, NodeSequence seq)
		{
			this.buffer.buffer[index].Sequence = seq;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000B96D0 File Offset: 0x000B78D0
		internal void SetValue(ProcessingContext context, int index, bool val)
		{
			this.buffer.buffer[index].Update(context, val);
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000B96EA File Offset: 0x000B78EA
		internal void SetValue(ProcessingContext context, int index, double val)
		{
			this.buffer.buffer[index].Update(context, val);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000B9704 File Offset: 0x000B7904
		internal void SetValue(ProcessingContext context, int index, string val)
		{
			this.buffer.buffer[index].Update(context, val);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000B971E File Offset: 0x000B791E
		internal void SetValue(ProcessingContext context, int index, NodeSequence val)
		{
			this.buffer.buffer[index].Update(context, val);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B9738 File Offset: 0x000B7938
		internal void TransferPositionsTo(ref EvalStack stack)
		{
			StackFrame topArg = this.TopArg;
			stack.PushFrame();
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				NodeSequence sequence = this.buffer.buffer[i].Sequence;
				int count = sequence.Count;
				if (this.stack.stackPtr + count > this.stack.bounds.end)
				{
					this.GrowStack(count);
				}
				for (int j = 0; j < count; j++)
				{
					stack.Push((double)sequence.Items[j].Position);
				}
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000B97D8 File Offset: 0x000B79D8
		internal void TransferSequenceSizeTo(ref EvalStack stack)
		{
			StackFrame topArg = this.TopArg;
			stack.PushFrame();
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				NodeSequence sequence = this.buffer.buffer[i].Sequence;
				int count = sequence.Count;
				if (this.stack.stackPtr + count > this.stack.bounds.end)
				{
					this.GrowStack(count);
				}
				for (int j = 0; j < count; j++)
				{
					stack.Push((double)NodeSequence.GetContextSize(sequence, j));
				}
			}
		}

		// Token: 0x040025FB RID: 9723
		internal QueryBuffer<Value> buffer;

		// Token: 0x040025FC RID: 9724
		internal StackRegion frames;

		// Token: 0x040025FD RID: 9725
		internal StackRegion stack;

		// Token: 0x040025FE RID: 9726
		internal const int DefaultSize = 2;

		// Token: 0x040025FF RID: 9727
		internal bool contextOnTopOfStack;
	}
}
