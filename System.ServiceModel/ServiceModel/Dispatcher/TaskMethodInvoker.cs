using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A9 RID: 1449
	internal class TaskMethodInvoker : IOperationInvoker
	{
		// Token: 0x06003890 RID: 14480 RVA: 0x000DA054 File Offset: 0x000D8254
		public TaskMethodInvoker(MethodInfo taskMethod, Type taskType)
		{
			if (taskMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("taskMethod"));
			}
			this.taskMethod = taskMethod;
			if (taskType != ServiceReflector.VoidType)
			{
				this.taskTResultGetMethod = ((PropertyInfo)taskMethod.ReturnType.GetMember("Result")[0]).GetGetMethod();
				this.isGenericTask = true;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000DA0C2 File Offset: 0x000D82C2
		public bool IsSynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000DA0C5 File Offset: 0x000D82C5
		public MethodInfo TaskMethod
		{
			get
			{
				return this.taskMethod;
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000DA0CD File Offset: 0x000D82CD
		public object[] AllocateInputs()
		{
			this.EnsureIsInitialized();
			return EmptyArray<object>.Allocate(this.inputParameterCount);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000DA0E0 File Offset: 0x000D82E0
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000DA0F1 File Offset: 0x000D82F1
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			return TaskMethodInvoker.ToApm<Tuple<object, object[]>>(this.InvokeAsync(instance, inputs), callback, state);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000DA104 File Offset: 0x000D8304
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			if (instance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoServiceObject")));
			}
			bool callFailed = true;
			bool callFaulted = false;
			ServiceModelActivity activity = null;
			Activity activity2 = null;
			object result2;
			try
			{
				AsyncMethodInvoker.GetActivityInfo(ref activity, ref activity2);
				Task<Tuple<object, object[]>> task = result as Task<Tuple<object, object[]>>;
				if (task == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException("SFxInvalidCallbackIAsyncResult"));
				}
				AggregateException ex = null;
				Tuple<object, object[]> tuple = null;
				Task task2 = null;
				if (task.IsFaulted)
				{
					ex = task.Exception;
				}
				else
				{
					tuple = task.Result;
					task2 = (tuple.Item1 as Task);
					if (task2 == null)
					{
						outputs = tuple.Item2;
						return null;
					}
					if (task2.IsFaulted)
					{
						ex = task2.Exception;
					}
				}
				if (ex != null && ex.InnerException != null)
				{
					if (ex.InnerException is FaultException)
					{
						callFaulted = true;
						callFailed = false;
					}
					if (ex.InnerException is SecurityException)
					{
						DiagnosticUtility.TraceHandledException(ex.InnerException, TraceEventType.Warning);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthorizationBehavior.CreateAccessDeniedFaultException());
					}
					task.GetAwaiter().GetResult();
				}
				if (task2.IsCanceled)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TaskCanceledException(task2));
				}
				outputs = tuple.Item2;
				object obj;
				if (!this.isGenericTask)
				{
					obj = null;
				}
				else
				{
					MethodBase methodBase = this.taskTResultGetMethod;
					object obj2 = task2;
					object[] emptyTypes = Type.EmptyTypes;
					obj = methodBase.Invoke(obj2, emptyTypes);
				}
				object obj3 = obj;
				callFailed = false;
				result2 = obj3;
			}
			finally
			{
				if (activity2 != null)
				{
					((IDisposable)activity2).Dispose();
				}
				ServiceModelActivity.Stop(activity);
				AsyncMethodInvoker.StopOperationInvokeTrace(callFailed, callFaulted, this.TaskMethod.Name);
				AsyncMethodInvoker.StopOperationInvokePerformanceCounters(callFailed, callFaulted, this.TaskMethod.Name);
			}
			return result2;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000DA2BC File Offset: 0x000D84BC
		private Task<Tuple<object, object[]>> InvokeAsync(object instance, object[] inputs)
		{
			TaskMethodInvoker.<InvokeAsync>d__16 <InvokeAsync>d__;
			<InvokeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<object, object[]>>.Create();
			<InvokeAsync>d__.<>4__this = this;
			<InvokeAsync>d__.instance = instance;
			<InvokeAsync>d__.inputs = inputs;
			<InvokeAsync>d__.<>1__state = -1;
			<InvokeAsync>d__.<>t__builder.Start<TaskMethodInvoker.<InvokeAsync>d__16>(ref <InvokeAsync>d__);
			return <InvokeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000DA310 File Offset: 0x000D8510
		private static Task<TResult> ToApm<TResult>(Task<TResult> task, AsyncCallback callback, object state)
		{
			if (task.AsyncState == state)
			{
				if (callback != null)
				{
					task.ContinueWith(delegate(Task<TResult> antecedent, object obj)
					{
						AsyncCallback asyncCallback = (AsyncCallback)obj;
						asyncCallback(antecedent);
					}, callback, CancellationToken.None, TaskContinuationOptions.HideScheduler, TaskScheduler.Default);
				}
				return task;
			}
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>(state);
			Tuple<TaskCompletionSource<TResult>, AsyncCallback> state2 = Tuple.Create<TaskCompletionSource<TResult>, AsyncCallback>(taskCompletionSource, callback);
			task.ContinueWith(delegate(Task<TResult> antecedent, object obj)
			{
				Tuple<TaskCompletionSource<TResult>, AsyncCallback> tuple = (Tuple<TaskCompletionSource<TResult>, AsyncCallback>)obj;
				TaskCompletionSource<TResult> item = tuple.Item1;
				AsyncCallback item2 = tuple.Item2;
				if (antecedent.IsFaulted)
				{
					item.TrySetException(antecedent.Exception.InnerException);
				}
				else if (antecedent.IsCanceled)
				{
					item.TrySetCanceled();
				}
				else
				{
					item.TrySetResult(antecedent.Result);
				}
				if (item2 != null)
				{
					item2(item.Task);
				}
			}, state2, CancellationToken.None, TaskContinuationOptions.HideScheduler, TaskScheduler.Default);
			return taskCompletionSource.Task;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000DA3A8 File Offset: 0x000D85A8
		private void EnsureIsInitialized()
		{
			if (this.invokeDelegate == null)
			{
				int num;
				int num2;
				InvokeDelegate invokeDelegate = new InvokerUtil().GenerateInvokeDelegate(this.taskMethod, out num, out num2);
				this.inputParameterCount = num;
				this.outputParameterCount = num2;
				this.invokeDelegate = invokeDelegate;
			}
		}

		// Token: 0x0400299C RID: 10652
		private const string ResultMethodName = "Result";

		// Token: 0x0400299D RID: 10653
		private readonly MethodInfo taskMethod;

		// Token: 0x0400299E RID: 10654
		private InvokeDelegate invokeDelegate;

		// Token: 0x0400299F RID: 10655
		private int inputParameterCount;

		// Token: 0x040029A0 RID: 10656
		private int outputParameterCount;

		// Token: 0x040029A1 RID: 10657
		private MethodInfo taskTResultGetMethod;

		// Token: 0x040029A2 RID: 10658
		private bool isGenericTask;
	}
}
