using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
	// Token: 0x0200026E RID: 622
	internal sealed class StackGuard
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x00049918 File Offset: 0x00047B18
		public bool TryEnterOnCurrentStack()
		{
			try
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
			}
			catch (InsufficientExecutionStackException obj) when (this._executionStackCount < 1024)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00049964 File Offset: 0x00047B64
		public void RunOnEmptyStack<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2>, T1, T2> tuple = (Tuple<Action<T1, T2>, T1, T2>)s;
				tuple.Item1(tuple.Item2, tuple.Item3);
				return null;
			}, Tuple.Create<Action<T1, T2>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00049994 File Offset: 0x00047B94
		public void RunOnEmptyStack<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2, T3>, T1, T2, T3> tuple = (Tuple<Action<T1, T2, T3>, T1, T2, T3>)s;
				tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
				return null;
			}, Tuple.Create<Action<T1, T2, T3>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000499C6 File Offset: 0x00047BC6
		public R RunOnEmptyStack<T1, T2, R>(Func<T1, T2, R> action, T1 arg1, T2 arg2)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, R>, T1, T2> tuple = (Tuple<Func<T1, T2, R>, T1, T2>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3);
			}, Tuple.Create<Func<T1, T2, R>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000499F5 File Offset: 0x00047BF5
		public R RunOnEmptyStack<T1, T2, T3, R>(Func<T1, T2, T3, R> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, T3, R>, T1, T2, T3> tuple = (Tuple<Func<T1, T2, T3, R>, T1, T2, T3>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
			}, Tuple.Create<Func<T1, T2, T3, R>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00049A28 File Offset: 0x00047C28
		private R RunOnEmptyStackCore<R>(Func<object, R> action, object state)
		{
			this._executionStackCount++;
			R result;
			try
			{
				Task<R> task = Task.Factory.StartNew<R>(action, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
				TaskAwaiter<R> awaiter = task.GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					((IAsyncResult)task).AsyncWaitHandle.WaitOne();
				}
				result = awaiter.GetResult();
			}
			finally
			{
				this._executionStackCount--;
			}
			return result;
		}

		// Token: 0x04000A61 RID: 2657
		private const int MaxExecutionStackCount = 1024;

		// Token: 0x04000A62 RID: 2658
		private int _executionStackCount;
	}
}
