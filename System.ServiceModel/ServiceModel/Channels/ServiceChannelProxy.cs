using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099F RID: 2463
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class ServiceChannelProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x060060A0 RID: 24736 RVA: 0x00169404 File Offset: 0x00167604
		internal ServiceChannelProxy(Type interfaceType, Type proxiedType, MessageDirection direction, ServiceChannel serviceChannel) : base(proxiedType)
		{
			if (!MessageDirectionHelper.IsDefined(direction))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("direction"));
			}
			this.interfaceType = interfaceType;
			this.proxiedType = proxiedType;
			this.serviceChannel = serviceChannel;
			this.proxyRuntime = serviceChannel.ClientRuntime.GetRuntime();
			this.methodDataCache = new ServiceChannelProxy.MethodDataCache();
			this.objectWrapper = new ServiceChannelProxy.MbrObject(this, proxiedType);
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x00169475 File Offset: 0x00167675
		private static LogicalCallContext SetActivityIdInLogicalCallContext(LogicalCallContext logicalCallContext)
		{
			if (TraceUtility.ActivityTracing)
			{
				logicalCallContext.SetData("E2ETrace.ActivityID", DiagnosticTraceBase.ActivityId);
			}
			return logicalCallContext;
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x00169494 File Offset: 0x00167694
		private IMethodReturnMessage CreateReturnMessage(object ret, object[] returnArgs, IMethodCallMessage methodCall)
		{
			if (returnArgs != null)
			{
				return this.CreateReturnMessage(ret, returnArgs, returnArgs.Length, ServiceChannelProxy.SetActivityIdInLogicalCallContext(methodCall.LogicalCallContext), methodCall);
			}
			return new ServiceChannelProxy.SingleReturnMessage(ret, methodCall);
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x001694B8 File Offset: 0x001676B8
		private IMethodReturnMessage CreateReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			return new ReturnMessage(ret, outArgs, outArgsCount, callCtx, mcm);
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x001694C6 File Offset: 0x001676C6
		private IMethodReturnMessage CreateReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			return new ReturnMessage(e, mcm);
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x001694D0 File Offset: 0x001676D0
		private ServiceChannelProxy.MethodData GetMethodData(IMethodCallMessage methodCall)
		{
			MethodBase methodBase = methodCall.MethodBase;
			ServiceChannelProxy.MethodData methodData;
			if (this.methodDataCache.TryGetMethodData(methodBase, out methodData))
			{
				return methodData;
			}
			Type declaringType = methodBase.DeclaringType;
			bool flag;
			if (declaringType == typeof(object))
			{
				ServiceChannelProxy.MethodType methodType;
				if (methodCall.MethodBase == typeof(object).GetMethod("GetType"))
				{
					methodType = ServiceChannelProxy.MethodType.GetType;
				}
				else
				{
					methodType = ServiceChannelProxy.MethodType.Object;
				}
				flag = true;
				methodData = new ServiceChannelProxy.MethodData(methodBase, methodType);
			}
			else if (declaringType.IsInstanceOfType(this.serviceChannel))
			{
				flag = true;
				methodData = new ServiceChannelProxy.MethodData(methodBase, ServiceChannelProxy.MethodType.Channel);
			}
			else
			{
				ProxyOperationRuntime operation = this.proxyRuntime.GetOperation(methodBase, methodCall.Args, out flag);
				if (operation == null)
				{
					if (this.serviceChannel.Factory != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxMethodNotSupported1", new object[]
						{
							methodBase.Name
						})));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxMethodNotSupportedOnCallback1", new object[]
					{
						methodBase.Name
					})));
				}
				else
				{
					ServiceChannelProxy.MethodType methodType2;
					if (operation.IsTaskCall(methodCall))
					{
						methodType2 = ServiceChannelProxy.MethodType.TaskService;
					}
					else if (operation.IsSyncCall(methodCall))
					{
						methodType2 = ServiceChannelProxy.MethodType.Service;
					}
					else if (operation.IsBeginCall(methodCall))
					{
						methodType2 = ServiceChannelProxy.MethodType.BeginService;
					}
					else
					{
						methodType2 = ServiceChannelProxy.MethodType.EndService;
					}
					methodData = new ServiceChannelProxy.MethodData(methodBase, methodType2, operation);
				}
			}
			if (flag)
			{
				this.methodDataCache.SetMethodData(methodData);
			}
			return methodData;
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x0016962C File Offset: 0x0016782C
		internal ServiceChannel GetServiceChannel()
		{
			return this.serviceChannel;
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x00169634 File Offset: 0x00167834
		public override IMessage Invoke(IMessage message)
		{
			IMessage result;
			try
			{
				IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
				if (methodCallMessage == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxExpectedIMethodCallMessage")));
				}
				ServiceChannelProxy.MethodData methodData = this.GetMethodData(methodCallMessage);
				switch (methodData.MethodType)
				{
				case ServiceChannelProxy.MethodType.Service:
					result = this.InvokeService(methodCallMessage, methodData.Operation);
					break;
				case ServiceChannelProxy.MethodType.BeginService:
					result = this.InvokeBeginService(methodCallMessage, methodData.Operation);
					break;
				case ServiceChannelProxy.MethodType.EndService:
					result = this.InvokeEndService(methodCallMessage, methodData.Operation);
					break;
				case ServiceChannelProxy.MethodType.Channel:
					result = this.InvokeChannel(methodCallMessage);
					break;
				case ServiceChannelProxy.MethodType.Object:
					result = this.InvokeObject(methodCallMessage);
					break;
				case ServiceChannelProxy.MethodType.GetType:
					result = this.InvokeGetType(methodCallMessage);
					break;
				case ServiceChannelProxy.MethodType.TaskService:
					result = this.InvokeTaskService(methodCallMessage, methodData.Operation);
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid proxy method type", new object[0])));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				result = this.CreateReturnMessage(ex, message as IMethodCallMessage);
			}
			return result;
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x00169758 File Offset: 0x00167958
		private IMessage InvokeTaskService(IMethodCallMessage methodCall, ProxyOperationRuntime operation)
		{
			Task ret = ServiceChannelProxy.TaskCreator.CreateTask(this.serviceChannel, methodCall, operation);
			return this.CreateReturnMessage(ret, null, methodCall);
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x0016977C File Offset: 0x0016797C
		private IMethodReturnMessage InvokeChannel(IMethodCallMessage methodCall)
		{
			string text = null;
			ActivityType activityType = ActivityType.Unknown;
			if (DiagnosticUtility.ShouldUseActivity && (ServiceModelActivity.Current == null || ServiceModelActivity.Current.ActivityType != ActivityType.Close))
			{
				ServiceChannelProxy.MethodData methodData = this.GetMethodData(methodCall);
				if (methodData.MethodBase.DeclaringType == typeof(ICommunicationObject) && methodData.MethodBase.Name.Equals("Close", StringComparison.Ordinal))
				{
					text = SR.GetString("ActivityClose", new object[]
					{
						this.serviceChannel.GetType().FullName
					});
					activityType = ActivityType.Close;
				}
			}
			IMethodReturnMessage result;
			using (ServiceModelActivity serviceModelActivity = string.IsNullOrEmpty(text) ? null : ServiceModelActivity.CreateBoundedActivity())
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, text, activityType);
				}
				result = this.ExecuteMessage(this.serviceChannel, methodCall);
			}
			return result;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x0016985C File Offset: 0x00167A5C
		private IMethodReturnMessage InvokeGetType(IMethodCallMessage methodCall)
		{
			return this.CreateReturnMessage(this.proxiedType, null, 0, ServiceChannelProxy.SetActivityIdInLogicalCallContext(methodCall.LogicalCallContext), methodCall);
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x00169878 File Offset: 0x00167A78
		private IMethodReturnMessage InvokeObject(IMethodCallMessage methodCall)
		{
			return RemotingServices.ExecuteMessage(this.objectWrapper, methodCall);
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x00169888 File Offset: 0x00167A88
		private IMethodReturnMessage InvokeBeginService(IMethodCallMessage methodCall, ProxyOperationRuntime operation)
		{
			AsyncCallback callback;
			object asyncState;
			object[] ins = operation.MapAsyncBeginInputs(methodCall, out callback, out asyncState);
			object ret = this.serviceChannel.BeginCall(operation.Action, operation.IsOneWay, operation, ins, callback, asyncState);
			return this.CreateReturnMessage(ret, null, methodCall);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x001698C8 File Offset: 0x00167AC8
		private IMethodReturnMessage InvokeEndService(IMethodCallMessage methodCall, ProxyOperationRuntime operation)
		{
			IAsyncResult result;
			object[] outs;
			operation.MapAsyncEndInputs(methodCall, out result, out outs);
			object ret = this.serviceChannel.EndCall(operation.Action, outs, result);
			object[] returnArgs = operation.MapAsyncOutputs(methodCall, outs, ref ret);
			return this.CreateReturnMessage(ret, returnArgs, methodCall);
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x00169908 File Offset: 0x00167B08
		private IMethodReturnMessage InvokeService(IMethodCallMessage methodCall, ProxyOperationRuntime operation)
		{
			object[] outs;
			object[] ins = operation.MapSyncInputs(methodCall, out outs);
			object ret = this.serviceChannel.Call(operation.Action, operation.IsOneWay, operation, ins, outs);
			object[] returnArgs = operation.MapSyncOutputs(methodCall, outs, ref ret);
			return this.CreateReturnMessage(ret, returnArgs, methodCall);
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x00169950 File Offset: 0x00167B50
		private IMethodReturnMessage ExecuteMessage(object target, IMethodCallMessage methodCall)
		{
			MethodBase methodBase = methodCall.MethodBase;
			object[] args = methodCall.Args;
			object ret = null;
			try
			{
				ret = methodBase.Invoke(target, args);
			}
			catch (TargetInvocationException ex)
			{
				return this.CreateReturnMessage(ex.InnerException, methodCall);
			}
			return this.CreateReturnMessage(ret, args, args.Length, null, methodCall);
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x001699AC File Offset: 0x00167BAC
		bool IRemotingTypeInfo.CanCastTo(Type toType, object o)
		{
			return toType.IsAssignableFrom(this.proxiedType) || this.serviceChannel.CanCastTo(toType);
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x060060B1 RID: 24753 RVA: 0x001699CA File Offset: 0x00167BCA
		// (set) Token: 0x060060B2 RID: 24754 RVA: 0x001699D7 File Offset: 0x00167BD7
		string IRemotingTypeInfo.TypeName
		{
			get
			{
				return this.proxiedType.FullName;
			}
			set
			{
			}
		}

		// Token: 0x04003894 RID: 14484
		private const string activityIdSlotName = "E2ETrace.ActivityID";

		// Token: 0x04003895 RID: 14485
		private Type proxiedType;

		// Token: 0x04003896 RID: 14486
		private Type interfaceType;

		// Token: 0x04003897 RID: 14487
		private ServiceChannel serviceChannel;

		// Token: 0x04003898 RID: 14488
		private ServiceChannelProxy.MbrObject objectWrapper;

		// Token: 0x04003899 RID: 14489
		private ImmutableClientRuntime proxyRuntime;

		// Token: 0x0400389A RID: 14490
		private ServiceChannelProxy.MethodDataCache methodDataCache;

		// Token: 0x02000E2D RID: 3629
		private static class TaskCreator
		{
			// Token: 0x06008262 RID: 33378 RVA: 0x001E2B28 File Offset: 0x001E0D28
			private static Func<ServiceChannel, ProxyOperationRuntime, object[], Task> GetOrCreateTaskDelegate(Type taskResultType)
			{
				Func<ServiceChannel, ProxyOperationRuntime, object[], Task> func = ServiceChannelProxy.TaskCreator.createGenericTaskDelegateCache[taskResultType] as Func<ServiceChannel, ProxyOperationRuntime, object[], Task>;
				if (func != null)
				{
					return func;
				}
				Hashtable obj = ServiceChannelProxy.TaskCreator.createGenericTaskDelegateCache;
				lock (obj)
				{
					func = (ServiceChannelProxy.TaskCreator.createGenericTaskDelegateCache[taskResultType] as Func<ServiceChannel, ProxyOperationRuntime, object[], Task>);
					if (func != null)
					{
						return func;
					}
					MethodInfo method = ServiceChannelProxy.TaskCreator.createGenericTaskMI.MakeGenericMethod(new Type[]
					{
						taskResultType
					});
					func = (Delegate.CreateDelegate(typeof(Func<ServiceChannel, ProxyOperationRuntime, object[], Task>), method) as Func<ServiceChannel, ProxyOperationRuntime, object[], Task>);
					ServiceChannelProxy.TaskCreator.createGenericTaskDelegateCache[taskResultType] = func;
				}
				return func;
			}

			// Token: 0x06008263 RID: 33379 RVA: 0x001E2BCC File Offset: 0x001E0DCC
			public static Task CreateTask(ServiceChannel channel, IMethodCallMessage methodCall, ProxyOperationRuntime operation)
			{
				if (operation.TaskTResult == ServiceReflector.VoidType)
				{
					return ServiceChannelProxy.TaskCreator.CreateTask(channel, operation, methodCall.InArgs);
				}
				return ServiceChannelProxy.TaskCreator.CreateGenericTask(channel, operation, methodCall.InArgs);
			}

			// Token: 0x06008264 RID: 33380 RVA: 0x001E2BFC File Offset: 0x001E0DFC
			private static Task CreateGenericTask(ServiceChannel channel, ProxyOperationRuntime operation, object[] inputParameters)
			{
				Func<ServiceChannel, ProxyOperationRuntime, object[], Task> orCreateTaskDelegate = ServiceChannelProxy.TaskCreator.GetOrCreateTaskDelegate(operation.TaskTResult);
				return orCreateTaskDelegate(channel, operation, inputParameters);
			}

			// Token: 0x06008265 RID: 33381 RVA: 0x001E2C20 File Offset: 0x001E0E20
			private static Task CreateTask(ServiceChannel channel, ProxyOperationRuntime operation, object[] inputParameters)
			{
				Action<IAsyncResult> endMethod = delegate(IAsyncResult asyncResult)
				{
					OperationContext value = OperationContext.Current;
					OperationContext.Current = (asyncResult.AsyncState as OperationContext);
					try
					{
						channel.EndCall(operation.Action, ProxyOperationRuntime.EmptyArray, asyncResult);
					}
					finally
					{
						OperationContext.Current = value;
					}
				};
				return Task.Factory.FromAsync<ServiceChannel, ProxyOperationRuntime, object[]>(ServiceChannelProxy.TaskCreator.beginCallDelegate, endMethod, channel, operation, inputParameters, OperationContext.Current);
			}

			// Token: 0x06008266 RID: 33382 RVA: 0x001E2C70 File Offset: 0x001E0E70
			public static Task<T> CreateGenericTask<T>(ServiceChannel channel, ProxyOperationRuntime operation, object[] inputParameters)
			{
				Func<IAsyncResult, T> endMethod = delegate(IAsyncResult asyncResult)
				{
					OperationContext value = OperationContext.Current;
					OperationContext.Current = (asyncResult.AsyncState as OperationContext);
					T result;
					try
					{
						result = (T)((object)channel.EndCall(operation.Action, ProxyOperationRuntime.EmptyArray, asyncResult));
					}
					finally
					{
						OperationContext.Current = value;
					}
					return result;
				};
				return Task<T>.Factory.FromAsync<ServiceChannel, ProxyOperationRuntime, object[]>(ServiceChannelProxy.TaskCreator.beginCallDelegate, endMethod, channel, operation, inputParameters, OperationContext.Current);
			}

			// Token: 0x04004A06 RID: 18950
			private static readonly Func<ServiceChannel, ProxyOperationRuntime, object[], AsyncCallback, object, IAsyncResult> beginCallDelegate = new Func<ServiceChannel, ProxyOperationRuntime, object[], AsyncCallback, object, IAsyncResult>(ServiceChannel.BeginCall);

			// Token: 0x04004A07 RID: 18951
			private static readonly Hashtable createGenericTaskDelegateCache = new Hashtable();

			// Token: 0x04004A08 RID: 18952
			private static readonly MethodInfo createGenericTaskMI = typeof(ServiceChannelProxy.TaskCreator).GetMethod("CreateGenericTask", new Type[]
			{
				typeof(ServiceChannel),
				typeof(ProxyOperationRuntime),
				typeof(object[])
			});
		}

		// Token: 0x02000E2E RID: 3630
		private class MethodDataCache
		{
			// Token: 0x06008268 RID: 33384 RVA: 0x001E2D2E File Offset: 0x001E0F2E
			public MethodDataCache()
			{
				this.methodDatas = new ServiceChannelProxy.MethodData[4];
			}

			// Token: 0x17001CB7 RID: 7351
			// (get) Token: 0x06008269 RID: 33385 RVA: 0x001E2D42 File Offset: 0x001E0F42
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600826A RID: 33386 RVA: 0x001E2D48 File Offset: 0x001E0F48
			public bool TryGetMethodData(MethodBase method, out ServiceChannelProxy.MethodData methodData)
			{
				object thisLock = this.ThisLock;
				bool result;
				lock (thisLock)
				{
					ServiceChannelProxy.MethodData[] array = this.methodDatas;
					int num = ServiceChannelProxy.MethodDataCache.FindMethod(array, method);
					if (num >= 0)
					{
						methodData = array[num];
						result = true;
					}
					else
					{
						methodData = default(ServiceChannelProxy.MethodData);
						result = false;
					}
				}
				return result;
			}

			// Token: 0x0600826B RID: 33387 RVA: 0x001E2DB4 File Offset: 0x001E0FB4
			private static int FindMethod(ServiceChannelProxy.MethodData[] methodDatas, MethodBase methodToFind)
			{
				for (int i = 0; i < methodDatas.Length; i++)
				{
					MethodBase methodBase = methodDatas[i].MethodBase;
					if (methodBase == null)
					{
						break;
					}
					if (methodBase == methodToFind)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600826C RID: 33388 RVA: 0x001E2DF4 File Offset: 0x001E0FF4
			public void SetMethodData(ServiceChannelProxy.MethodData methodData)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					int num = ServiceChannelProxy.MethodDataCache.FindMethod(this.methodDatas, methodData.MethodBase);
					if (num < 0)
					{
						for (int i = 0; i < this.methodDatas.Length; i++)
						{
							if (this.methodDatas[i].MethodBase == null)
							{
								this.methodDatas[i] = methodData;
								return;
							}
						}
						ServiceChannelProxy.MethodData[] array = new ServiceChannelProxy.MethodData[this.methodDatas.Length * 2];
						Array.Copy(this.methodDatas, array, this.methodDatas.Length);
						array[this.methodDatas.Length] = methodData;
						this.methodDatas = array;
					}
				}
			}

			// Token: 0x04004A09 RID: 18953
			private ServiceChannelProxy.MethodData[] methodDatas;
		}

		// Token: 0x02000E2F RID: 3631
		private enum MethodType
		{
			// Token: 0x04004A0B RID: 18955
			Service,
			// Token: 0x04004A0C RID: 18956
			BeginService,
			// Token: 0x04004A0D RID: 18957
			EndService,
			// Token: 0x04004A0E RID: 18958
			Channel,
			// Token: 0x04004A0F RID: 18959
			Object,
			// Token: 0x04004A10 RID: 18960
			GetType,
			// Token: 0x04004A11 RID: 18961
			TaskService
		}

		// Token: 0x02000E30 RID: 3632
		private struct MethodData
		{
			// Token: 0x0600826D RID: 33389 RVA: 0x001E2EC0 File Offset: 0x001E10C0
			public MethodData(MethodBase methodBase, ServiceChannelProxy.MethodType methodType)
			{
				this = new ServiceChannelProxy.MethodData(methodBase, methodType, null);
			}

			// Token: 0x0600826E RID: 33390 RVA: 0x001E2ECB File Offset: 0x001E10CB
			public MethodData(MethodBase methodBase, ServiceChannelProxy.MethodType methodType, ProxyOperationRuntime operation)
			{
				this.methodBase = methodBase;
				this.methodType = methodType;
				this.operation = operation;
			}

			// Token: 0x17001CB8 RID: 7352
			// (get) Token: 0x0600826F RID: 33391 RVA: 0x001E2EE2 File Offset: 0x001E10E2
			public MethodBase MethodBase
			{
				get
				{
					return this.methodBase;
				}
			}

			// Token: 0x17001CB9 RID: 7353
			// (get) Token: 0x06008270 RID: 33392 RVA: 0x001E2EEA File Offset: 0x001E10EA
			public ServiceChannelProxy.MethodType MethodType
			{
				get
				{
					return this.methodType;
				}
			}

			// Token: 0x17001CBA RID: 7354
			// (get) Token: 0x06008271 RID: 33393 RVA: 0x001E2EF2 File Offset: 0x001E10F2
			public ProxyOperationRuntime Operation
			{
				get
				{
					return this.operation;
				}
			}

			// Token: 0x04004A12 RID: 18962
			private MethodBase methodBase;

			// Token: 0x04004A13 RID: 18963
			private ServiceChannelProxy.MethodType methodType;

			// Token: 0x04004A14 RID: 18964
			private ProxyOperationRuntime operation;
		}

		// Token: 0x02000E31 RID: 3633
		private class MbrObject : MarshalByRefObject
		{
			// Token: 0x06008272 RID: 33394 RVA: 0x001E2EFA File Offset: 0x001E10FA
			internal MbrObject(RealProxy proxy, Type targetType)
			{
				this.proxy = proxy;
				this.targetType = targetType;
			}

			// Token: 0x06008273 RID: 33395 RVA: 0x001E2F10 File Offset: 0x001E1110
			public override bool Equals(object obj)
			{
				return obj == this.proxy.GetTransparentProxy();
			}

			// Token: 0x06008274 RID: 33396 RVA: 0x001E2F20 File Offset: 0x001E1120
			public override string ToString()
			{
				return this.targetType.ToString();
			}

			// Token: 0x06008275 RID: 33397 RVA: 0x001E2F2D File Offset: 0x001E112D
			public override int GetHashCode()
			{
				return this.proxy.GetHashCode();
			}

			// Token: 0x04004A15 RID: 18965
			private RealProxy proxy;

			// Token: 0x04004A16 RID: 18966
			private Type targetType;
		}

		// Token: 0x02000E32 RID: 3634
		private class SingleReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
		{
			// Token: 0x06008276 RID: 33398 RVA: 0x001E2F3A File Offset: 0x001E113A
			public SingleReturnMessage(object ret, IMethodCallMessage methodCall)
			{
				this.ret = ret;
				this.methodCall = methodCall;
				this.properties = new ServiceChannelProxy.SingleReturnMessage.PropertyDictionary();
			}

			// Token: 0x17001CBB RID: 7355
			// (get) Token: 0x06008277 RID: 33399 RVA: 0x001E2F5B File Offset: 0x001E115B
			public int ArgCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17001CBC RID: 7356
			// (get) Token: 0x06008278 RID: 33400 RVA: 0x001E2F5E File Offset: 0x001E115E
			public object[] Args
			{
				get
				{
					return EmptyArray.Instance;
				}
			}

			// Token: 0x17001CBD RID: 7357
			// (get) Token: 0x06008279 RID: 33401 RVA: 0x001E2F65 File Offset: 0x001E1165
			public Exception Exception
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001CBE RID: 7358
			// (get) Token: 0x0600827A RID: 33402 RVA: 0x001E2F68 File Offset: 0x001E1168
			public bool HasVarArgs
			{
				get
				{
					return this.methodCall.HasVarArgs;
				}
			}

			// Token: 0x17001CBF RID: 7359
			// (get) Token: 0x0600827B RID: 33403 RVA: 0x001E2F75 File Offset: 0x001E1175
			public LogicalCallContext LogicalCallContext
			{
				get
				{
					return ServiceChannelProxy.SetActivityIdInLogicalCallContext(this.methodCall.LogicalCallContext);
				}
			}

			// Token: 0x17001CC0 RID: 7360
			// (get) Token: 0x0600827C RID: 33404 RVA: 0x001E2F87 File Offset: 0x001E1187
			public MethodBase MethodBase
			{
				get
				{
					return this.methodCall.MethodBase;
				}
			}

			// Token: 0x17001CC1 RID: 7361
			// (get) Token: 0x0600827D RID: 33405 RVA: 0x001E2F94 File Offset: 0x001E1194
			public string MethodName
			{
				get
				{
					return this.methodCall.MethodName;
				}
			}

			// Token: 0x17001CC2 RID: 7362
			// (get) Token: 0x0600827E RID: 33406 RVA: 0x001E2FA1 File Offset: 0x001E11A1
			public object MethodSignature
			{
				get
				{
					return this.methodCall.MethodSignature;
				}
			}

			// Token: 0x17001CC3 RID: 7363
			// (get) Token: 0x0600827F RID: 33407 RVA: 0x001E2FAE File Offset: 0x001E11AE
			public object[] OutArgs
			{
				get
				{
					return EmptyArray.Instance;
				}
			}

			// Token: 0x17001CC4 RID: 7364
			// (get) Token: 0x06008280 RID: 33408 RVA: 0x001E2FB5 File Offset: 0x001E11B5
			public int OutArgCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17001CC5 RID: 7365
			// (get) Token: 0x06008281 RID: 33409 RVA: 0x001E2FB8 File Offset: 0x001E11B8
			public IDictionary Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x17001CC6 RID: 7366
			// (get) Token: 0x06008282 RID: 33410 RVA: 0x001E2FC0 File Offset: 0x001E11C0
			public object ReturnValue
			{
				get
				{
					return this.ret;
				}
			}

			// Token: 0x17001CC7 RID: 7367
			// (get) Token: 0x06008283 RID: 33411 RVA: 0x001E2FC8 File Offset: 0x001E11C8
			public string TypeName
			{
				get
				{
					return this.methodCall.TypeName;
				}
			}

			// Token: 0x17001CC8 RID: 7368
			// (get) Token: 0x06008284 RID: 33412 RVA: 0x001E2FD5 File Offset: 0x001E11D5
			public string Uri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06008285 RID: 33413 RVA: 0x001E2FD8 File Offset: 0x001E11D8
			public object GetArg(int index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
			}

			// Token: 0x06008286 RID: 33414 RVA: 0x001E2FEE File Offset: 0x001E11EE
			public string GetArgName(int index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
			}

			// Token: 0x06008287 RID: 33415 RVA: 0x001E3004 File Offset: 0x001E1204
			public object GetOutArg(int index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
			}

			// Token: 0x06008288 RID: 33416 RVA: 0x001E301A File Offset: 0x001E121A
			public string GetOutArgName(int index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index"));
			}

			// Token: 0x04004A17 RID: 18967
			private IMethodCallMessage methodCall;

			// Token: 0x04004A18 RID: 18968
			private object ret;

			// Token: 0x04004A19 RID: 18969
			private ServiceChannelProxy.SingleReturnMessage.PropertyDictionary properties;

			// Token: 0x02000F8C RID: 3980
			private class PropertyDictionary : IDictionary, ICollection, IEnumerable
			{
				// Token: 0x17001DA0 RID: 7584
				public object this[object key]
				{
					get
					{
						return this.Properties[key];
					}
					set
					{
						this.Properties[key] = value;
					}
				}

				// Token: 0x17001DA1 RID: 7585
				// (get) Token: 0x0600883D RID: 34877 RVA: 0x001FAA25 File Offset: 0x001F8C25
				public int Count
				{
					get
					{
						return this.Properties.Count;
					}
				}

				// Token: 0x17001DA2 RID: 7586
				// (get) Token: 0x0600883E RID: 34878 RVA: 0x001FAA32 File Offset: 0x001F8C32
				public bool IsFixedSize
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17001DA3 RID: 7587
				// (get) Token: 0x0600883F RID: 34879 RVA: 0x001FAA35 File Offset: 0x001F8C35
				public bool IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17001DA4 RID: 7588
				// (get) Token: 0x06008840 RID: 34880 RVA: 0x001FAA38 File Offset: 0x001F8C38
				public bool IsSynchronized
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17001DA5 RID: 7589
				// (get) Token: 0x06008841 RID: 34881 RVA: 0x001FAA3B File Offset: 0x001F8C3B
				public ICollection Keys
				{
					get
					{
						return this.Properties.Keys;
					}
				}

				// Token: 0x17001DA6 RID: 7590
				// (get) Token: 0x06008842 RID: 34882 RVA: 0x001FAA48 File Offset: 0x001F8C48
				private ListDictionary Properties
				{
					get
					{
						if (this.properties == null)
						{
							this.properties = new ListDictionary();
						}
						return this.properties;
					}
				}

				// Token: 0x17001DA7 RID: 7591
				// (get) Token: 0x06008843 RID: 34883 RVA: 0x001FAA63 File Offset: 0x001F8C63
				public ICollection Values
				{
					get
					{
						return this.Properties.Values;
					}
				}

				// Token: 0x17001DA8 RID: 7592
				// (get) Token: 0x06008844 RID: 34884 RVA: 0x001FAA70 File Offset: 0x001F8C70
				public object SyncRoot
				{
					get
					{
						return null;
					}
				}

				// Token: 0x06008845 RID: 34885 RVA: 0x001FAA73 File Offset: 0x001F8C73
				public void Add(object key, object value)
				{
					this.Properties.Add(key, value);
				}

				// Token: 0x06008846 RID: 34886 RVA: 0x001FAA82 File Offset: 0x001F8C82
				public void Clear()
				{
					this.Properties.Clear();
				}

				// Token: 0x06008847 RID: 34887 RVA: 0x001FAA8F File Offset: 0x001F8C8F
				public bool Contains(object key)
				{
					return this.Properties.Contains(key);
				}

				// Token: 0x06008848 RID: 34888 RVA: 0x001FAA9D File Offset: 0x001F8C9D
				public void CopyTo(Array array, int index)
				{
					this.Properties.CopyTo(array, index);
				}

				// Token: 0x06008849 RID: 34889 RVA: 0x001FAAAC File Offset: 0x001F8CAC
				public IDictionaryEnumerator GetEnumerator()
				{
					if (this.properties == null)
					{
						return ServiceChannelProxy.SingleReturnMessage.PropertyDictionary.EmptyEnumerator.Instance;
					}
					return this.properties.GetEnumerator();
				}

				// Token: 0x0600884A RID: 34890 RVA: 0x001FAAC7 File Offset: 0x001F8CC7
				IEnumerator IEnumerable.GetEnumerator()
				{
					return ((IEnumerable)this.Properties).GetEnumerator();
				}

				// Token: 0x0600884B RID: 34891 RVA: 0x001FAAD4 File Offset: 0x001F8CD4
				public void Remove(object key)
				{
					this.Properties.Remove(key);
				}

				// Token: 0x04004F82 RID: 20354
				private ListDictionary properties;

				// Token: 0x02000FC9 RID: 4041
				private class EmptyEnumerator : IDictionaryEnumerator, IEnumerator
				{
					// Token: 0x060088E7 RID: 35047 RVA: 0x001FDDC4 File Offset: 0x001FBFC4
					private EmptyEnumerator()
					{
					}

					// Token: 0x17001DB6 RID: 7606
					// (get) Token: 0x060088E8 RID: 35048 RVA: 0x001FDDCC File Offset: 0x001FBFCC
					public static ServiceChannelProxy.SingleReturnMessage.PropertyDictionary.EmptyEnumerator Instance
					{
						get
						{
							return ServiceChannelProxy.SingleReturnMessage.PropertyDictionary.EmptyEnumerator.instance;
						}
					}

					// Token: 0x060088E9 RID: 35049 RVA: 0x001FDDD3 File Offset: 0x001FBFD3
					public bool MoveNext()
					{
						return false;
					}

					// Token: 0x17001DB7 RID: 7607
					// (get) Token: 0x060088EA RID: 35050 RVA: 0x001FDDD6 File Offset: 0x001FBFD6
					public object Current
					{
						get
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDictionaryIsEmpty")));
						}
					}

					// Token: 0x060088EB RID: 35051 RVA: 0x001FDDF1 File Offset: 0x001FBFF1
					public void Reset()
					{
					}

					// Token: 0x17001DB8 RID: 7608
					// (get) Token: 0x060088EC RID: 35052 RVA: 0x001FDDF3 File Offset: 0x001FBFF3
					public object Key
					{
						get
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDictionaryIsEmpty")));
						}
					}

					// Token: 0x17001DB9 RID: 7609
					// (get) Token: 0x060088ED RID: 35053 RVA: 0x001FDE0E File Offset: 0x001FC00E
					public object Value
					{
						get
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDictionaryIsEmpty")));
						}
					}

					// Token: 0x17001DBA RID: 7610
					// (get) Token: 0x060088EE RID: 35054 RVA: 0x001FDE29 File Offset: 0x001FC029
					public DictionaryEntry Entry
					{
						get
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxDictionaryIsEmpty")));
						}
					}

					// Token: 0x04005080 RID: 20608
					private static ServiceChannelProxy.SingleReturnMessage.PropertyDictionary.EmptyEnumerator instance = new ServiceChannelProxy.SingleReturnMessage.PropertyDictionary.EmptyEnumerator();
				}
			}
		}
	}
}
