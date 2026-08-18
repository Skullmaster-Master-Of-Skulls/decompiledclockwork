using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000598 RID: 1432
	internal class ProxyOperationRuntime
	{
		// Token: 0x0600376C RID: 14188 RVA: 0x000D5A44 File Offset: 0x000D3C44
		internal ProxyOperationRuntime(ClientOperation operation, ImmutableClientRuntime parent)
		{
			if (operation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("operation");
			}
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			this.parent = parent;
			this.formatter = operation.Formatter;
			this.isInitiating = operation.IsInitiating;
			this.isOneWay = operation.IsOneWay;
			this.isTerminating = operation.IsTerminating;
			this.isSessionOpenNotificationEnabled = operation.IsSessionOpenNotificationEnabled;
			this.name = operation.Name;
			this.parameterInspectors = EmptyArray<IParameterInspector>.ToArray(operation.ParameterInspectors);
			this.faultFormatter = operation.FaultFormatter;
			this.serializeRequest = operation.SerializeRequest;
			this.deserializeReply = operation.DeserializeReply;
			this.action = operation.Action;
			this.replyAction = operation.ReplyAction;
			this.beginMethod = operation.BeginMethod;
			this.syncMethod = operation.SyncMethod;
			this.taskMethod = operation.TaskMethod;
			this.TaskTResult = operation.TaskTResult;
			if (this.beginMethod != null)
			{
				this.inParams = ServiceReflector.GetInputParameters(this.beginMethod, true);
				if (this.syncMethod != null)
				{
					this.outParams = ServiceReflector.GetOutputParameters(this.syncMethod, false);
				}
				else
				{
					this.outParams = ProxyOperationRuntime.NoParams;
				}
				this.endOutParams = ServiceReflector.GetOutputParameters(operation.EndMethod, true);
				this.returnParam = operation.EndMethod.ReturnParameter;
			}
			else if (this.syncMethod != null)
			{
				this.inParams = ServiceReflector.GetInputParameters(this.syncMethod, false);
				this.outParams = ServiceReflector.GetOutputParameters(this.syncMethod, false);
				this.returnParam = this.syncMethod.ReturnParameter;
			}
			if (this.formatter == null && (this.serializeRequest || this.deserializeReply))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientRuntimeRequiresFormatter0", new object[]
				{
					this.name
				})));
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000D5C3F File Offset: 0x000D3E3F
		internal string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000D5C47 File Offset: 0x000D3E47
		internal IClientFaultFormatter FaultFormatter
		{
			get
			{
				return this.faultFormatter;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000D5C4F File Offset: 0x000D3E4F
		internal bool IsInitiating
		{
			get
			{
				return this.isInitiating;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000D5C57 File Offset: 0x000D3E57
		internal bool IsOneWay
		{
			get
			{
				return this.isOneWay;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000D5C5F File Offset: 0x000D3E5F
		internal bool IsTerminating
		{
			get
			{
				return this.isTerminating;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000D5C67 File Offset: 0x000D3E67
		internal bool IsSessionOpenNotificationEnabled
		{
			get
			{
				return this.isSessionOpenNotificationEnabled;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000D5C6F File Offset: 0x000D3E6F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000D5C77 File Offset: 0x000D3E77
		internal ImmutableClientRuntime Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000D5C7F File Offset: 0x000D3E7F
		internal string ReplyAction
		{
			get
			{
				return this.replyAction;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x000D5C87 File Offset: 0x000D3E87
		internal bool DeserializeReply
		{
			get
			{
				return this.deserializeReply;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000D5C8F File Offset: 0x000D3E8F
		internal bool SerializeRequest
		{
			get
			{
				return this.serializeRequest;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000D5C97 File Offset: 0x000D3E97
		// (set) Token: 0x06003779 RID: 14201 RVA: 0x000D5C9F File Offset: 0x000D3E9F
		internal Type TaskTResult { get; set; }

		// Token: 0x0600377A RID: 14202 RVA: 0x000D5CA8 File Offset: 0x000D3EA8
		internal void AfterReply(ref ProxyRpc rpc)
		{
			if (!this.isOneWay)
			{
				Message reply = rpc.Reply;
				if (this.deserializeReply)
				{
					if (TD.ClientFormatterDeserializeReplyStartIsEnabled())
					{
						TD.ClientFormatterDeserializeReplyStart(rpc.EventTraceActivity);
					}
					bool flag = DS.MessageFormatterIsEnabled();
					Stopwatch stopwatch = null;
					if (flag)
					{
						stopwatch = Stopwatch.StartNew();
					}
					rpc.ReturnValue = this.formatter.DeserializeReply(reply, rpc.OutputParameters);
					if (flag)
					{
						DS.ClientMessageFormatterDeserialize(this.formatter.GetType(), stopwatch.Elapsed);
					}
					if (TD.ClientFormatterDeserializeReplyStopIsEnabled())
					{
						TD.ClientFormatterDeserializeReplyStop(rpc.EventTraceActivity);
					}
				}
				else
				{
					rpc.ReturnValue = reply;
				}
				int parameterInspectorCorrelationOffset = this.parent.ParameterInspectorCorrelationOffset;
				try
				{
					bool flag2 = DS.ParameterInspectorIsEnabled();
					Stopwatch stopwatch2 = null;
					if (flag2)
					{
						stopwatch2 = new Stopwatch();
					}
					for (int i = this.parameterInspectors.Length - 1; i >= 0; i--)
					{
						if (flag2)
						{
							stopwatch2.Restart();
						}
						this.parameterInspectors[i].AfterCall(this.name, rpc.OutputParameters, rpc.ReturnValue, rpc.Correlation[parameterInspectorCorrelationOffset + i]);
						if (flag2)
						{
							DS.ParameterInspectorAfter(this.parameterInspectors[i].GetType(), stopwatch2.Elapsed);
						}
						if (TD.ClientParameterInspectorAfterCallInvokedIsEnabled())
						{
							TD.ClientParameterInspectorAfterCallInvoked(rpc.EventTraceActivity, this.parameterInspectors[i].GetType().FullName);
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
				if (this.parent.ValidateMustUnderstand)
				{
					Collection<MessageHeaderInfo> headersNotUnderstood = reply.Headers.GetHeadersNotUnderstood();
					if (headersNotUnderstood != null && headersNotUnderstood.Count > 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("SFxHeaderNotUnderstood", new object[]
						{
							headersNotUnderstood[0].Name,
							headersNotUnderstood[0].Namespace
						})));
					}
				}
			}
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000D5E94 File Offset: 0x000D4094
		internal void BeforeRequest(ref ProxyRpc rpc)
		{
			int parameterInspectorCorrelationOffset = this.parent.ParameterInspectorCorrelationOffset;
			try
			{
				bool flag = DS.ParameterInspectorIsEnabled();
				Stopwatch stopwatch = null;
				if (flag)
				{
					stopwatch = new Stopwatch();
				}
				for (int i = 0; i < this.parameterInspectors.Length; i++)
				{
					if (flag)
					{
						stopwatch.Restart();
					}
					rpc.Correlation[parameterInspectorCorrelationOffset + i] = this.parameterInspectors[i].BeforeCall(this.name, rpc.InputParameters);
					if (flag)
					{
						DS.ParameterInspectorBefore(this.parameterInspectors[i].GetType(), stopwatch.Elapsed);
					}
					if (TD.ClientParameterInspectorBeforeCallInvokedIsEnabled())
					{
						TD.ClientParameterInspectorBeforeCallInvoked(rpc.EventTraceActivity, this.parameterInspectors[i].GetType().FullName);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowClientSideExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
			if (this.serializeRequest)
			{
				if (TD.ClientFormatterSerializeRequestStartIsEnabled())
				{
					TD.ClientFormatterSerializeRequestStart(rpc.EventTraceActivity);
				}
				bool flag2 = DS.MessageFormatterIsEnabled();
				Stopwatch stopwatch2 = null;
				if (flag2)
				{
					stopwatch2 = Stopwatch.StartNew();
				}
				rpc.Request = this.formatter.SerializeRequest(rpc.MessageVersion, rpc.InputParameters);
				if (flag2)
				{
					DS.ClientMessageFormatterSerialize(this.formatter.GetType(), stopwatch2.Elapsed);
				}
				if (TD.ClientFormatterSerializeRequestStopIsEnabled())
				{
					TD.ClientFormatterSerializeRequestStop(rpc.EventTraceActivity);
					return;
				}
			}
			else
			{
				if (rpc.InputParameters[0] == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxProxyRuntimeMessageCannotBeNull", new object[]
					{
						this.name
					})));
				}
				rpc.Request = (Message)rpc.InputParameters[0];
				if (!ProxyOperationRuntime.IsValidAction(rpc.Request, this.Action))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidRequestAction", new object[]
					{
						this.Name,
						rpc.Request.Headers.Action ?? "{NULL}",
						this.Action
					})));
				}
			}
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000D6098 File Offset: 0x000D4298
		internal static object GetDefaultParameterValue(Type parameterType)
		{
			if (!parameterType.IsValueType || !(parameterType != typeof(void)))
			{
				return null;
			}
			return Activator.CreateInstance(parameterType);
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000D60BC File Offset: 0x000D42BC
		[SecurityCritical]
		internal bool IsSyncCall(IMethodCallMessage methodCall)
		{
			return !(this.syncMethod == null) && methodCall.MethodBase.MethodHandle == this.syncMethod.MethodHandle;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000D60E9 File Offset: 0x000D42E9
		[SecurityCritical]
		internal bool IsBeginCall(IMethodCallMessage methodCall)
		{
			return !(this.beginMethod == null) && methodCall.MethodBase.MethodHandle == this.beginMethod.MethodHandle;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000D6116 File Offset: 0x000D4316
		[SecurityCritical]
		internal bool IsTaskCall(IMethodCallMessage methodCall)
		{
			return !(this.taskMethod == null) && methodCall.MethodBase.MethodHandle == this.taskMethod.MethodHandle;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000D6143 File Offset: 0x000D4343
		[SecurityCritical]
		internal object[] MapSyncInputs(IMethodCallMessage methodCall, out object[] outs)
		{
			if (this.outParams.Length == 0)
			{
				outs = ProxyOperationRuntime.EmptyArray;
			}
			else
			{
				outs = new object[this.outParams.Length];
			}
			if (this.inParams.Length == 0)
			{
				return ProxyOperationRuntime.EmptyArray;
			}
			return methodCall.InArgs;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000D617C File Offset: 0x000D437C
		[SecurityCritical]
		internal object[] MapAsyncBeginInputs(IMethodCallMessage methodCall, out AsyncCallback callback, out object asyncState)
		{
			object[] array;
			if (this.inParams.Length == 0)
			{
				array = ProxyOperationRuntime.EmptyArray;
			}
			else
			{
				array = new object[this.inParams.Length];
			}
			object[] args = methodCall.Args;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[this.inParams[i].Position];
			}
			callback = (args[methodCall.ArgCount - 2] as AsyncCallback);
			asyncState = args[methodCall.ArgCount - 1];
			return array;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000D61ED File Offset: 0x000D43ED
		[SecurityCritical]
		internal void MapAsyncEndInputs(IMethodCallMessage methodCall, out IAsyncResult result, out object[] outs)
		{
			outs = new object[this.endOutParams.Length];
			result = (methodCall.Args[methodCall.ArgCount - 1] as IAsyncResult);
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000D6214 File Offset: 0x000D4414
		[SecurityCritical]
		internal object[] MapSyncOutputs(IMethodCallMessage methodCall, object[] outs, ref object ret)
		{
			return this.MapOutputs(this.outParams, methodCall, outs, ref ret);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000D6225 File Offset: 0x000D4425
		[SecurityCritical]
		internal object[] MapAsyncOutputs(IMethodCallMessage methodCall, object[] outs, ref object ret)
		{
			return this.MapOutputs(this.endOutParams, methodCall, outs, ref ret);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000D6238 File Offset: 0x000D4438
		[SecurityCritical]
		private object[] MapOutputs(ParameterInfo[] parameters, IMethodCallMessage methodCall, object[] outs, ref object ret)
		{
			if (ret == null && this.returnParam != null)
			{
				ret = ProxyOperationRuntime.GetDefaultParameterValue(TypeLoader.GetParameterType(this.returnParam));
			}
			if (parameters.Length == 0)
			{
				return null;
			}
			object[] args = methodCall.Args;
			for (int i = 0; i < parameters.Length; i++)
			{
				if (outs[i] == null)
				{
					args[parameters[i].Position] = ProxyOperationRuntime.GetDefaultParameterValue(TypeLoader.GetParameterType(parameters[i]));
				}
				else
				{
					args[parameters[i].Position] = outs[i];
				}
			}
			return args;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000D62AC File Offset: 0x000D44AC
		internal static bool IsValidAction(Message message, string action)
		{
			return message != null && (message.IsFault || action == "*" || string.CompareOrdinal(message.Headers.Action, action) == 0);
		}

		// Token: 0x0400292F RID: 10543
		internal static readonly ParameterInfo[] NoParams = new ParameterInfo[0];

		// Token: 0x04002930 RID: 10544
		internal static readonly object[] EmptyArray = new object[0];

		// Token: 0x04002931 RID: 10545
		private readonly IClientMessageFormatter formatter;

		// Token: 0x04002932 RID: 10546
		private readonly bool isInitiating;

		// Token: 0x04002933 RID: 10547
		private readonly bool isOneWay;

		// Token: 0x04002934 RID: 10548
		private readonly bool isTerminating;

		// Token: 0x04002935 RID: 10549
		private readonly bool isSessionOpenNotificationEnabled;

		// Token: 0x04002936 RID: 10550
		private readonly string name;

		// Token: 0x04002937 RID: 10551
		private readonly IParameterInspector[] parameterInspectors;

		// Token: 0x04002938 RID: 10552
		private readonly IClientFaultFormatter faultFormatter;

		// Token: 0x04002939 RID: 10553
		private readonly ImmutableClientRuntime parent;

		// Token: 0x0400293A RID: 10554
		private bool serializeRequest;

		// Token: 0x0400293B RID: 10555
		private bool deserializeReply;

		// Token: 0x0400293C RID: 10556
		private string action;

		// Token: 0x0400293D RID: 10557
		private string replyAction;

		// Token: 0x0400293E RID: 10558
		private MethodInfo beginMethod;

		// Token: 0x0400293F RID: 10559
		private MethodInfo syncMethod;

		// Token: 0x04002940 RID: 10560
		private MethodInfo taskMethod;

		// Token: 0x04002941 RID: 10561
		private ParameterInfo[] inParams;

		// Token: 0x04002942 RID: 10562
		private ParameterInfo[] outParams;

		// Token: 0x04002943 RID: 10563
		private ParameterInfo[] endOutParams;

		// Token: 0x04002944 RID: 10564
		private ParameterInfo returnParam;
	}
}
