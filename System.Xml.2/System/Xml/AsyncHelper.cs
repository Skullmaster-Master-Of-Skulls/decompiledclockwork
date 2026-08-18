using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000061 RID: 97
	internal static class AsyncHelper
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000D932 File Offset: 0x0000BB32
		public static bool IsSuccess(this Task task)
		{
			return task.IsCompleted && task.Exception == null;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000D947 File Offset: 0x0000BB47
		public static Task CallVoidFuncWhenFinish(this Task task, Action func)
		{
			if (task.IsSuccess())
			{
				func();
				return AsyncHelper.DoneTask;
			}
			return task._CallVoidFuncWhenFinish(func);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D964 File Offset: 0x0000BB64
		private static Task _CallVoidFuncWhenFinish(this Task task, Action func)
		{
			AsyncHelper.<_CallVoidFuncWhenFinish>d__6 <_CallVoidFuncWhenFinish>d__;
			<_CallVoidFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_CallVoidFuncWhenFinish>d__.task = task;
			<_CallVoidFuncWhenFinish>d__.func = func;
			<_CallVoidFuncWhenFinish>d__.<>1__state = -1;
			<_CallVoidFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallVoidFuncWhenFinish>d__6>(ref <_CallVoidFuncWhenFinish>d__);
			return <_CallVoidFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D9AF File Offset: 0x0000BBAF
		public static Task<bool> ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			if (!task.IsSuccess())
			{
				return task._ReturnTaskBoolWhenFinish(ret);
			}
			if (ret)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		public static Task<bool> _ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			AsyncHelper.<_ReturnTaskBoolWhenFinish>d__8 <_ReturnTaskBoolWhenFinish>d__;
			<_ReturnTaskBoolWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReturnTaskBoolWhenFinish>d__.task = task;
			<_ReturnTaskBoolWhenFinish>d__.ret = ret;
			<_ReturnTaskBoolWhenFinish>d__.<>1__state = -1;
			<_ReturnTaskBoolWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_ReturnTaskBoolWhenFinish>d__8>(ref <_ReturnTaskBoolWhenFinish>d__);
			return <_ReturnTaskBoolWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000DA1B File Offset: 0x0000BC1B
		public static Task CallTaskFuncWhenFinish(this Task task, Func<Task> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return AsyncHelper._CallTaskFuncWhenFinish(task, func);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000DA34 File Offset: 0x0000BC34
		private static Task _CallTaskFuncWhenFinish(Task task, Func<Task> func)
		{
			AsyncHelper.<_CallTaskFuncWhenFinish>d__10 <_CallTaskFuncWhenFinish>d__;
			<_CallTaskFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_CallTaskFuncWhenFinish>d__.task = task;
			<_CallTaskFuncWhenFinish>d__.func = func;
			<_CallTaskFuncWhenFinish>d__.<>1__state = -1;
			<_CallTaskFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallTaskFuncWhenFinish>d__10>(ref <_CallTaskFuncWhenFinish>d__);
			return <_CallTaskFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000DA7F File Offset: 0x0000BC7F
		public static Task<bool> CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return task._CallBoolTaskFuncWhenFinish(func);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000DA98 File Offset: 0x0000BC98
		private static Task<bool> _CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12 <_CallBoolTaskFuncWhenFinish>d__;
			<_CallBoolTaskFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_CallBoolTaskFuncWhenFinish>d__.task = task;
			<_CallBoolTaskFuncWhenFinish>d__.func = func;
			<_CallBoolTaskFuncWhenFinish>d__.<>1__state = -1;
			<_CallBoolTaskFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12>(ref <_CallBoolTaskFuncWhenFinish>d__);
			return <_CallBoolTaskFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		public static Task<bool> ContinueBoolTaskFuncWhenFalse(this Task<bool> task, Func<Task<bool>> func)
		{
			if (!task.IsSuccess())
			{
				return AsyncHelper._ContinueBoolTaskFuncWhenFalse(task, func);
			}
			if (task.Result)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return func();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		private static Task<bool> _ContinueBoolTaskFuncWhenFalse(Task<bool> task, Func<Task<bool>> func)
		{
			AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14 <_ContinueBoolTaskFuncWhenFalse>d__;
			<_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ContinueBoolTaskFuncWhenFalse>d__.task = task;
			<_ContinueBoolTaskFuncWhenFalse>d__.func = func;
			<_ContinueBoolTaskFuncWhenFalse>d__.<>1__state = -1;
			<_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder.Start<AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14>(ref <_ContinueBoolTaskFuncWhenFalse>d__);
			return <_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder.Task;
		}

		// Token: 0x0400018C RID: 396
		public static readonly Task DoneTask = Task.FromResult<bool>(true);

		// Token: 0x0400018D RID: 397
		public static readonly Task<bool> DoneTaskTrue = Task.FromResult<bool>(true);

		// Token: 0x0400018E RID: 398
		public static readonly Task<bool> DoneTaskFalse = Task.FromResult<bool>(false);

		// Token: 0x0400018F RID: 399
		public static readonly Task<int> DoneTaskZero = Task.FromResult<int>(0);
	}
}
