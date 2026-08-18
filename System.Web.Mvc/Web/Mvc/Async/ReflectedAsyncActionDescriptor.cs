using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x0200011F RID: 287
	public class ReflectedAsyncActionDescriptor : AsyncActionDescriptor, IMethodInfoActionDescriptor
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x000145B3 File Offset: 0x000127B3
		public ReflectedAsyncActionDescriptor(MethodInfo asyncMethodInfo, MethodInfo completedMethodInfo, string actionName, ControllerDescriptor controllerDescriptor) : this(asyncMethodInfo, completedMethodInfo, actionName, controllerDescriptor, true)
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000145C4 File Offset: 0x000127C4
		internal ReflectedAsyncActionDescriptor(MethodInfo asyncMethodInfo, MethodInfo completedMethodInfo, string actionName, ControllerDescriptor controllerDescriptor, bool validateMethods)
		{
			if (asyncMethodInfo == null)
			{
				throw new ArgumentNullException("asyncMethodInfo");
			}
			if (completedMethodInfo == null)
			{
				throw new ArgumentNullException("completedMethodInfo");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("actionName");
			}
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			if (validateMethods)
			{
				string text = ActionDescriptor.VerifyActionMethodIsCallable(asyncMethodInfo);
				if (text != null)
				{
					throw new ArgumentException(text, "asyncMethodInfo");
				}
				string text2 = ActionDescriptor.VerifyActionMethodIsCallable(completedMethodInfo);
				if (text2 != null)
				{
					throw new ArgumentException(text2, "completedMethodInfo");
				}
			}
			this.AsyncMethodInfo = asyncMethodInfo;
			this.CompletedMethodInfo = completedMethodInfo;
			this._actionName = actionName;
			this._controllerDescriptor = controllerDescriptor;
			this._uniqueId = new Lazy<string>(new Func<string>(this.CreateUniqueId));
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00014690 File Offset: 0x00012890
		public override string ActionName
		{
			get
			{
				return this._actionName;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00014698 File Offset: 0x00012898
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x000146A0 File Offset: 0x000128A0
		public MethodInfo AsyncMethodInfo { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000146A9 File Offset: 0x000128A9
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x000146B1 File Offset: 0x000128B1
		public MethodInfo CompletedMethodInfo { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000146BA File Offset: 0x000128BA
		public override ControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000146C2 File Offset: 0x000128C2
		public MethodInfo MethodInfo
		{
			get
			{
				return this.AsyncMethodInfo;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000146CA File Offset: 0x000128CA
		public override string UniqueId
		{
			get
			{
				return this._uniqueId.Value;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000148BC File Offset: 0x00012ABC
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
			AsyncManager asyncManager = AsyncActionDescriptor.GetAsyncManager(controllerContext.Controller);
			BeginInvokeDelegate beginDelegate = delegate(AsyncCallback asyncCallback, object asyncState)
			{
				ParameterInfo[] parameters2 = this.AsyncMethodInfo.GetParameters();
				IEnumerable<object> source = from parameterInfo in parameters2
				select ActionDescriptor.ExtractParameterFromDictionary(parameterInfo, parameters, this.AsyncMethodInfo);
				object[] parameters3 = source.ToArray<object>();
				TriggerListener triggerListener = new TriggerListener();
				SimpleAsyncResult asyncResult = new SimpleAsyncResult(asyncState);
				Trigger finishTrigger = triggerListener.CreateTrigger();
				asyncManager.Finished += delegate(object param0, EventArgs param1)
				{
					finishTrigger.Fire();
				};
				asyncManager.OutstandingOperations.Increment();
				triggerListener.SetContinuation(delegate
				{
					ThreadPool.QueueUserWorkItem(delegate(object _)
					{
						asyncResult.MarkCompleted(false, asyncCallback);
					});
				});
				ActionMethodDispatcher dispatcher = this.DispatcherCache.GetDispatcher(this.AsyncMethodInfo);
				dispatcher.Execute(controllerContext.Controller, parameters3);
				asyncManager.OutstandingOperations.Decrement();
				triggerListener.Activate();
				return asyncResult;
			};
			EndInvokeDelegate<object> endDelegate = delegate(IAsyncResult asyncResult)
			{
				ParameterInfo[] parameters2 = this.CompletedMethodInfo.GetParameters();
				IEnumerable<object> source = from parameterInfo in parameters2
				select ActionDescriptor.ExtractParameterOrDefaultFromDictionary(parameterInfo, asyncManager.Parameters);
				object[] parameters3 = source.ToArray<object>();
				ActionMethodDispatcher dispatcher = this.DispatcherCache.GetDispatcher(this.CompletedMethodInfo);
				return dispatcher.Execute(controllerContext.Controller, parameters3);
			};
			return AsyncResultWrapper.Begin<object>(callback, state, beginDelegate, endDelegate, this._executeTag, asyncManager.Timeout);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00014955 File Offset: 0x00012B55
		private string CreateUniqueId()
		{
			return base.UniqueId + DescriptorUtil.CreateUniqueId(this.AsyncMethodInfo, this.CompletedMethodInfo);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00014973 File Offset: 0x00012B73
		public override object EndExecute(IAsyncResult asyncResult)
		{
			return AsyncResultWrapper.End<object>(asyncResult, this._executeTag);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00014981 File Offset: 0x00012B81
		public override object[] GetCustomAttributes(bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.AsyncMethodInfo, inherit);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001498F File Offset: 0x00012B8F
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.AsyncMethodInfo, attributeType, inherit);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001499E File Offset: 0x00012B9E
		public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			if (useCache && base.GetType() == typeof(ReflectedAsyncActionDescriptor))
			{
				return ReflectedAttributeCache.GetMethodFilterAttributes(this.AsyncMethodInfo);
			}
			return base.GetFilterAttributes(useCache);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000149CD File Offset: 0x00012BCD
		public override ParameterDescriptor[] GetParameters()
		{
			return ActionDescriptorHelper.GetParameters(this, this.AsyncMethodInfo, ref this._parametersCache);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000149E1 File Offset: 0x00012BE1
		public override ICollection<ActionSelector> GetSelectors()
		{
			return ActionDescriptorHelper.GetSelectors(this.AsyncMethodInfo);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000149EE File Offset: 0x00012BEE
		internal override ICollection<ActionNameSelector> GetNameSelectors()
		{
			return ActionDescriptorHelper.GetNameSelectors(this.AsyncMethodInfo);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000149FB File Offset: 0x00012BFB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.IsDefined(this.AsyncMethodInfo, attributeType, inherit);
		}

		// Token: 0x04000215 RID: 533
		private readonly object _executeTag = new object();

		// Token: 0x04000216 RID: 534
		private readonly string _actionName;

		// Token: 0x04000217 RID: 535
		private readonly ControllerDescriptor _controllerDescriptor;

		// Token: 0x04000218 RID: 536
		private readonly Lazy<string> _uniqueId;

		// Token: 0x04000219 RID: 537
		private ParameterDescriptor[] _parametersCache;
	}
}
