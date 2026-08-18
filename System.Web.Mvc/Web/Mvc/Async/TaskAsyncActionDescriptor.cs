using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Async
{
	// Token: 0x0200005C RID: 92
	public class TaskAsyncActionDescriptor : AsyncActionDescriptor, IMethodInfoActionDescriptor
	{
		// Token: 0x06000258 RID: 600 RVA: 0x000081F9 File Offset: 0x000063F9
		public TaskAsyncActionDescriptor(MethodInfo taskMethodInfo, string actionName, ControllerDescriptor controllerDescriptor) : this(taskMethodInfo, actionName, controllerDescriptor, true)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008208 File Offset: 0x00006408
		internal TaskAsyncActionDescriptor(MethodInfo taskMethodInfo, string actionName, ControllerDescriptor controllerDescriptor, bool validateMethod)
		{
			if (taskMethodInfo == null)
			{
				throw new ArgumentNullException("taskMethodInfo");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("actionName");
			}
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			if (validateMethod)
			{
				string text = ActionDescriptor.VerifyActionMethodIsCallable(taskMethodInfo);
				if (text != null)
				{
					throw new ArgumentException(text, "taskMethodInfo");
				}
			}
			this.TaskMethodInfo = taskMethodInfo;
			this._actionName = actionName;
			this._controllerDescriptor = controllerDescriptor;
			this._uniqueId = new Lazy<string>(new Func<string>(this.CreateUniqueId));
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00008296 File Offset: 0x00006496
		public override string ActionName
		{
			get
			{
				return this._actionName;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000829E File Offset: 0x0000649E
		// (set) Token: 0x0600025C RID: 604 RVA: 0x000082A6 File Offset: 0x000064A6
		public MethodInfo TaskMethodInfo { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000082AF File Offset: 0x000064AF
		public override ControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000082B7 File Offset: 0x000064B7
		public MethodInfo MethodInfo
		{
			get
			{
				return this.TaskMethodInfo;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000082BF File Offset: 0x000064BF
		public override string UniqueId
		{
			get
			{
				return this._uniqueId.Value;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000082CC File Offset: 0x000064CC
		private string CreateUniqueId()
		{
			StringBuilder stringBuilder = new StringBuilder(base.UniqueId);
			DescriptorUtil.AppendUniqueId(stringBuilder, this.MethodInfo);
			return stringBuilder.ToString();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000083F8 File Offset: 0x000065F8
		public override IAsyncResult BeginExecute(ControllerContext controllerContext, IDictionary<string, object> parameters, AsyncCallback callback, object state)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			ParameterInfo[] parameters2 = this.TaskMethodInfo.GetParameters();
			IEnumerable<object> source = from parameterInfo in parameters2
			select ActionDescriptor.ExtractParameterFromDictionary(parameterInfo, parameters, this.TaskMethodInfo);
			object[] array = source.ToArray<object>();
			CancellationTokenSource tokenSource = null;
			bool disposedTimer = false;
			Timer taskCancelledTimer = null;
			bool flag = false;
			int timeout = AsyncActionDescriptor.GetAsyncManager(controllerContext.Controller).Timeout;
			for (int i = 0; i < array.Length; i++)
			{
				if (default(CancellationToken).Equals(array[i]))
				{
					tokenSource = new CancellationTokenSource();
					array[i] = tokenSource.Token;
					flag = (timeout > -1);
					break;
				}
			}
			ActionMethodDispatcher dispatcher = base.DispatcherCache.GetDispatcher(this.TaskMethodInfo);
			if (flag)
			{
				taskCancelledTimer = new Timer(delegate(object _)
				{
					CancellationTokenSource tokenSource;
					lock (tokenSource)
					{
						if (!disposedTimer)
						{
							tokenSource.Cancel();
						}
					}
				}, null, timeout, -1);
			}
			Task task = dispatcher.Execute(controllerContext.Controller, array) as Task;
			Action cleanupThunk = delegate()
			{
				if (taskCancelledTimer != null)
				{
					taskCancelledTimer.Dispose();
				}
				CancellationTokenSource tokenSource;
				if (tokenSource != null)
				{
					lock (tokenSource)
					{
						disposedTimer = true;
						tokenSource.Dispose();
						if (tokenSource.IsCancellationRequested)
						{
							throw new TimeoutException();
						}
					}
				}
			};
			TaskWrapperAsyncResult result = new TaskWrapperAsyncResult(task, state, cleanupThunk);
			if (callback != null)
			{
				if (task.IsCompleted)
				{
					result.CompletedSynchronously = true;
					callback(result);
				}
				else
				{
					result.CompletedSynchronously = false;
					task.ContinueWith(delegate(Task _)
					{
						callback(result);
					});
				}
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000085D0 File Offset: 0x000067D0
		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.TaskAsyncActionDescriptor_CannotExecuteSynchronously, new object[]
			{
				this.ActionName
			});
			throw new InvalidOperationException(message);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008604 File Offset: 0x00006804
		public override object EndExecute(IAsyncResult asyncResult)
		{
			TaskWrapperAsyncResult taskWrapperAsyncResult = (TaskWrapperAsyncResult)asyncResult;
			try
			{
				taskWrapperAsyncResult.Task.ThrowIfFaulted();
			}
			finally
			{
				if (taskWrapperAsyncResult.CleanupThunk != null)
				{
					taskWrapperAsyncResult.CleanupThunk();
				}
			}
			return TaskAsyncActionDescriptor._taskValueExtractors.GetOrAdd(this.TaskMethodInfo.ReturnType, new Func<Type, Func<object, object>>(TaskAsyncActionDescriptor.CreateTaskValueExtractor))(taskWrapperAsyncResult.Task);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000867C File Offset: 0x0000687C
		private static Func<object, object> CreateTaskValueExtractor(Type taskType)
		{
			if (taskType.IsGenericType && taskType.GetGenericTypeDefinition() == typeof(Task<>))
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
				UnaryExpression expression = Expression.Convert(parameterExpression, taskType);
				MemberExpression expression2 = Expression.Property(expression, "Result");
				UnaryExpression body = Expression.Convert(expression2, typeof(object));
				Expression<Func<object, object>> expression3 = Expression.Lambda<Func<object, object>>(body, new ParameterExpression[]
				{
					parameterExpression
				});
				return expression3.Compile();
			}
			return (object theTask) => null;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008719 File Offset: 0x00006919
		public override object[] GetCustomAttributes(bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.TaskMethodInfo, inherit);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008727 File Offset: 0x00006927
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.TaskMethodInfo, attributeType, inherit);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008736 File Offset: 0x00006936
		public override ParameterDescriptor[] GetParameters()
		{
			return ActionDescriptorHelper.GetParameters(this, this.TaskMethodInfo, ref this._parametersCache);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000874A File Offset: 0x0000694A
		public override ICollection<ActionSelector> GetSelectors()
		{
			return ActionDescriptorHelper.GetSelectors(this.TaskMethodInfo);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008757 File Offset: 0x00006957
		internal override ICollection<ActionNameSelector> GetNameSelectors()
		{
			return ActionDescriptorHelper.GetNameSelectors(this.TaskMethodInfo);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008764 File Offset: 0x00006964
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.IsDefined(this.TaskMethodInfo, attributeType, inherit);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008773 File Offset: 0x00006973
		public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			if (useCache && base.GetType() == typeof(TaskAsyncActionDescriptor))
			{
				return ReflectedAttributeCache.GetMethodFilterAttributes(this.TaskMethodInfo);
			}
			return base.GetFilterAttributes(useCache);
		}

		// Token: 0x04000073 RID: 115
		private static readonly ConcurrentDictionary<Type, Func<object, object>> _taskValueExtractors = new ConcurrentDictionary<Type, Func<object, object>>();

		// Token: 0x04000074 RID: 116
		private readonly string _actionName;

		// Token: 0x04000075 RID: 117
		private readonly ControllerDescriptor _controllerDescriptor;

		// Token: 0x04000076 RID: 118
		private readonly Lazy<string> _uniqueId;

		// Token: 0x04000077 RID: 119
		private ParameterDescriptor[] _parametersCache;
	}
}
