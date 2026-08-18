using System;
using System.Collections;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x02000119 RID: 281
	public sealed class ThreadContextStack : IFixingRequired
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x000199FE File Offset: 0x00017BFE
		internal ThreadContextStack()
		{
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00019A11 File Offset: 0x00017C11
		public int Count
		{
			get
			{
				return this.m_stack.Count;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00019A1E File Offset: 0x00017C1E
		public void Clear()
		{
			this.m_stack.Clear();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00019A2C File Offset: 0x00017C2C
		public string Pop()
		{
			Stack stack = this.m_stack;
			if (stack.Count > 0)
			{
				return ((ThreadContextStack.StackFrame)stack.Pop()).Message;
			}
			return "";
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00019A60 File Offset: 0x00017C60
		public IDisposable Push(string message)
		{
			Stack stack = this.m_stack;
			stack.Push(new ThreadContextStack.StackFrame(message, (stack.Count > 0) ? ((ThreadContextStack.StackFrame)stack.Peek()) : null));
			return new ThreadContextStack.AutoPopStackFrame(stack, stack.Count - 1);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00019AAC File Offset: 0x00017CAC
		internal string GetFullMessage()
		{
			Stack stack = this.m_stack;
			if (stack.Count > 0)
			{
				return ((ThreadContextStack.StackFrame)stack.Peek()).FullMessage;
			}
			return null;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00019ADB File Offset: 0x00017CDB
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00019AE3 File Offset: 0x00017CE3
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

		// Token: 0x0600084E RID: 2126 RVA: 0x00019AEC File Offset: 0x00017CEC
		public override string ToString()
		{
			return this.GetFullMessage();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00019AF4 File Offset: 0x00017CF4
		object IFixingRequired.GetFixedObject()
		{
			return this.GetFullMessage();
		}

		// Token: 0x040002FF RID: 767
		private Stack m_stack = new Stack();

		// Token: 0x0200011A RID: 282
		private sealed class StackFrame
		{
			// Token: 0x06000850 RID: 2128 RVA: 0x00019AFC File Offset: 0x00017CFC
			internal StackFrame(string message, ThreadContextStack.StackFrame parent)
			{
				this.m_message = message;
				this.m_parent = parent;
				if (parent == null)
				{
					this.m_fullMessage = message;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000851 RID: 2129 RVA: 0x00019B1C File Offset: 0x00017D1C
			internal string Message
			{
				get
				{
					return this.m_message;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000852 RID: 2130 RVA: 0x00019B24 File Offset: 0x00017D24
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

			// Token: 0x04000300 RID: 768
			private readonly string m_message;

			// Token: 0x04000301 RID: 769
			private readonly ThreadContextStack.StackFrame m_parent;

			// Token: 0x04000302 RID: 770
			private string m_fullMessage;
		}

		// Token: 0x0200011B RID: 283
		private struct AutoPopStackFrame : IDisposable
		{
			// Token: 0x06000853 RID: 2131 RVA: 0x00019B5D File Offset: 0x00017D5D
			internal AutoPopStackFrame(Stack frameStack, int frameDepth)
			{
				this.m_frameStack = frameStack;
				this.m_frameDepth = frameDepth;
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00019B6D File Offset: 0x00017D6D
			public void Dispose()
			{
				if (this.m_frameDepth >= 0 && this.m_frameStack != null)
				{
					while (this.m_frameStack.Count > this.m_frameDepth)
					{
						this.m_frameStack.Pop();
					}
				}
			}

			// Token: 0x04000303 RID: 771
			private Stack m_frameStack;

			// Token: 0x04000304 RID: 772
			private int m_frameDepth;
		}
	}
}
