using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200013F RID: 319
	public static class NestedDiagnosticsContext
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00019BA5 File Offset: 0x00017DA5
		public static string TopMessage
		{
			get
			{
				return FormatHelper.ConvertToString(NestedDiagnosticsContext.TopObject, null);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00019BB4 File Offset: 0x00017DB4
		public static object TopObject
		{
			get
			{
				Stack<object> threadStack = NestedDiagnosticsContext.ThreadStack;
				if (threadStack.Count <= 0)
				{
					return null;
				}
				return threadStack.Peek();
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00019BD8 File Offset: 0x00017DD8
		private static Stack<object> ThreadStack
		{
			get
			{
				return ThreadLocalStorageHelper.GetDataForSlot<Stack<object>>(NestedDiagnosticsContext.dataSlot);
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public static IDisposable Push(string text)
		{
			return NestedDiagnosticsContext.Push(text);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00019BEC File Offset: 0x00017DEC
		public static IDisposable Push(object value)
		{
			Stack<object> threadStack = NestedDiagnosticsContext.ThreadStack;
			int count = threadStack.Count;
			threadStack.Push(value);
			return new NestedDiagnosticsContext.StackPopper(threadStack, count);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00019C14 File Offset: 0x00017E14
		public static string Pop()
		{
			return NestedDiagnosticsContext.Pop(null);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00019C1C File Offset: 0x00017E1C
		public static string Pop(IFormatProvider formatProvider)
		{
			return FormatHelper.ConvertToString(NestedDiagnosticsContext.PopObject(), formatProvider);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00019C2C File Offset: 0x00017E2C
		public static object PopObject()
		{
			Stack<object> threadStack = NestedDiagnosticsContext.ThreadStack;
			if (threadStack.Count <= 0)
			{
				return null;
			}
			return threadStack.Pop();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00019C50 File Offset: 0x00017E50
		public static void Clear()
		{
			NestedDiagnosticsContext.ThreadStack.Clear();
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00019C5C File Offset: 0x00017E5C
		public static string[] GetAllMessages()
		{
			return NestedDiagnosticsContext.GetAllMessages(null);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static string[] GetAllMessages(IFormatProvider formatProvider)
		{
			return (from o in NestedDiagnosticsContext.ThreadStack
			select FormatHelper.ConvertToString(o, formatProvider)).ToArray<string>();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00019CB1 File Offset: 0x00017EB1
		public static object[] GetAllObjects()
		{
			return NestedDiagnosticsContext.ThreadStack.ToArray();
		}

		// Token: 0x040002B9 RID: 697
		private static readonly object dataSlot = ThreadLocalStorageHelper.AllocateDataSlot();

		// Token: 0x02000140 RID: 320
		private class StackPopper : IDisposable
		{
			// Token: 0x06000B48 RID: 2888 RVA: 0x00019CC9 File Offset: 0x00017EC9
			public StackPopper(Stack<object> stack, int previousCount)
			{
				this.stack = stack;
				this.previousCount = previousCount;
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x00019CDF File Offset: 0x00017EDF
			void IDisposable.Dispose()
			{
				while (this.stack.Count > this.previousCount)
				{
					this.stack.Pop();
				}
			}

			// Token: 0x040002BA RID: 698
			private Stack<object> stack;

			// Token: 0x040002BB RID: 699
			private int previousCount;
		}
	}
}
