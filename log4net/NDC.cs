using System;
using System.Collections;
using log4net.Util;

namespace log4net
{
	// Token: 0x02000126 RID: 294
	public sealed class NDC
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001A2DF File Offset: 0x000184DF
		private NDC()
		{
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001A2E7 File Offset: 0x000184E7
		public static int Depth
		{
			get
			{
				return ThreadContext.Stacks["NDC"].Count;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001A2FD File Offset: 0x000184FD
		public static void Clear()
		{
			ThreadContext.Stacks["NDC"].Clear();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001A313 File Offset: 0x00018513
		public static Stack CloneStack()
		{
			return ThreadContext.Stacks["NDC"].InternalStack;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001A329 File Offset: 0x00018529
		public static void Inherit(Stack stack)
		{
			ThreadContext.Stacks["NDC"].InternalStack = stack;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001A340 File Offset: 0x00018540
		public static string Pop()
		{
			return ThreadContext.Stacks["NDC"].Pop();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001A356 File Offset: 0x00018556
		public static IDisposable Push(string message)
		{
			return ThreadContext.Stacks["NDC"].Push(message);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001A36D File Offset: 0x0001856D
		public static IDisposable PushFormat(string messageFormat, params object[] args)
		{
			return NDC.Push(string.Format(messageFormat, args));
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001A37B File Offset: 0x0001857B
		public static void Remove()
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001A380 File Offset: 0x00018580
		public static void SetMaxDepth(int maxDepth)
		{
			if (maxDepth >= 0)
			{
				ThreadContextStack threadContextStack = ThreadContext.Stacks["NDC"];
				if (maxDepth == 0)
				{
					threadContextStack.Clear();
					return;
				}
				while (threadContextStack.Count > maxDepth)
				{
					threadContextStack.Pop();
				}
			}
		}
	}
}
