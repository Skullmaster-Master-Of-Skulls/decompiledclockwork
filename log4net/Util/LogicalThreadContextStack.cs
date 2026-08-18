using System;
using System.Collections;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000FF RID: 255
	public sealed class LogicalThreadContextStack : IFixingRequired
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00017327 File Offset: 0x00015527
		internal LogicalThreadContextStack(string propertyKey, TwoArgAction<string, LogicalThreadContextStack> registerNew)
		{
			this.m_propertyKey = propertyKey;
			this.m_registerNew = registerNew;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00017348 File Offset: 0x00015548
		public int Count
		{
			get
			{
				return this.m_stack.Count;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00017355 File Offset: 0x00015555
		public void Clear()
		{
			this.m_registerNew(this.m_propertyKey, new LogicalThreadContextStack(this.m_propertyKey, this.m_registerNew));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001737C File Offset: 0x0001557C
		public string Pop()
		{
			Stack stack = new Stack(new Stack(this.m_stack));
			string result = "";
			if (stack.Count > 0)
			{
				result = ((LogicalThreadContextStack.StackFrame)stack.Pop()).Message;
			}
			LogicalThreadContextStack logicalThreadContextStack = new LogicalThreadContextStack(this.m_propertyKey, this.m_registerNew);
			logicalThreadContextStack.m_stack = stack;
			this.m_registerNew(this.m_propertyKey, logicalThreadContextStack);
			return result;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000173E8 File Offset: 0x000155E8
		public IDisposable Push(string message)
		{
			Stack stack = new Stack(new Stack(this.m_stack));
			stack.Push(new LogicalThreadContextStack.StackFrame(message, (stack.Count > 0) ? ((LogicalThreadContextStack.StackFrame)stack.Peek()) : null));
			LogicalThreadContextStack logicalThreadContextStack = new LogicalThreadContextStack(this.m_propertyKey, this.m_registerNew);
			logicalThreadContextStack.m_stack = stack;
			this.m_registerNew(this.m_propertyKey, logicalThreadContextStack);
			return new LogicalThreadContextStack.AutoPopStackFrame(logicalThreadContextStack, stack.Count - 1);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00017468 File Offset: 0x00015668
		internal string GetFullMessage()
		{
			Stack stack = this.m_stack;
			if (stack.Count > 0)
			{
				return ((LogicalThreadContextStack.StackFrame)stack.Peek()).FullMessage;
			}
			return null;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00017497 File Offset: 0x00015697
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0001749F File Offset: 0x0001569F
		internal Stack InternalStack
		{
			get
			{
				return this.m_stack;
			}
			set
			{
				this.m_stack = value;
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000174A8 File Offset: 0x000156A8
		public override string ToString()
		{
			return this.GetFullMessage();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000174B0 File Offset: 0x000156B0
		object IFixingRequired.GetFixedObject()
		{
			return this.GetFullMessage();
		}

		// Token: 0x040002B8 RID: 696
		private Stack m_stack = new Stack();

		// Token: 0x040002B9 RID: 697
		private string m_propertyKey;

		// Token: 0x040002BA RID: 698
		private TwoArgAction<string, LogicalThreadContextStack> m_registerNew;

		// Token: 0x02000100 RID: 256
		private sealed class StackFrame
		{
			// Token: 0x06000761 RID: 1889 RVA: 0x000174B8 File Offset: 0x000156B8
			internal StackFrame(string message, LogicalThreadContextStack.StackFrame parent)
			{
				this.m_message = message;
				this.m_parent = parent;
				if (parent == null)
				{
					this.m_fullMessage = message;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000762 RID: 1890 RVA: 0x000174D8 File Offset: 0x000156D8
			internal string Message
			{
				get
				{
					return this.m_message;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000763 RID: 1891 RVA: 0x000174E0 File Offset: 0x000156E0
			internal string FullMessage
			{
				get
				{
					if (this.m_fullMessage == null && this.m_parent != null)
					{
						this.m_fullMessage = this.m_parent.FullMessage + " " + this.m_message;
					}
					return this.m_fullMessage;
				}
			}

			// Token: 0x040002BB RID: 699
			private readonly string m_message;

			// Token: 0x040002BC RID: 700
			private readonly LogicalThreadContextStack.StackFrame m_parent;

			// Token: 0x040002BD RID: 701
			private string m_fullMessage;
		}

		// Token: 0x02000101 RID: 257
		private struct AutoPopStackFrame : IDisposable
		{
			// Token: 0x06000764 RID: 1892 RVA: 0x00017519 File Offset: 0x00015719
			internal AutoPopStackFrame(LogicalThreadContextStack logicalThreadContextStack, int frameDepth)
			{
				this.m_frameDepth = frameDepth;
				this.m_logicalThreadContextStack = logicalThreadContextStack;
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x0001752C File Offset: 0x0001572C
			public void Dispose()
			{
				if (this.m_frameDepth >= 0 && this.m_logicalThreadContextStack.m_stack != null)
				{
					Stack stack = new Stack(new Stack(this.m_logicalThreadContextStack.m_stack));
					while (stack.Count > this.m_frameDepth)
					{
						stack.Pop();
					}
					LogicalThreadContextStack logicalThreadContextStack = new LogicalThreadContextStack(this.m_logicalThreadContextStack.m_propertyKey, this.m_logicalThreadContextStack.m_registerNew);
					logicalThreadContextStack.m_stack = stack;
					this.m_logicalThreadContextStack.m_registerNew(this.m_logicalThreadContextStack.m_propertyKey, logicalThreadContextStack);
				}
			}

			// Token: 0x040002BE RID: 702
			private int m_frameDepth;

			// Token: 0x040002BF RID: 703
			private LogicalThreadContextStack m_logicalThreadContextStack;
		}
	}
}
