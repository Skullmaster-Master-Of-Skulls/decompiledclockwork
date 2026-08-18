using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000554 RID: 1364
	internal class DispatchOperationRuntime
	{
		// Token: 0x060034AB RID: 13483 RVA: 0x000CB988 File Offset: 0x000C9B88
		internal DispatchOperationRuntime(DispatchOperation operation, ImmutableDispatchRuntime parent)
		{
			if (operation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("operation");
			}
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			if (operation.Invoker == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RuntimeRequiresInvoker0")));
			}
			this.disposeParameters = (operation.AutoDisposeParameters && !operation.HasNoDisposableParameters);
			this.parent = parent;
			this.callContextInitializers = EmptyArray<ICallContextInitializer>.ToArray(operation.CallContextInitializers);
			this.inspectors = EmptyArray<IParameterInspector>.ToArray(operation.ParameterInspectors);
			this.faultFormatter = operation.FaultFormatter;
			this.impersonation = operation.Impersonation;
			this.deserializeRequest = operation.DeserializeRequest;
			this.serializeReply = operation.SerializeReply;
			this.formatter = operation.Formatter;
			this.invoker = operation.Invoker;
			try
			{
				this.isSynchronous = operation.Invoker.IsSynchronous;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
			this.isTerminating = operation.IsTerminating;
			this.isSessionOpenNotificationEnabled = operation.IsSessionOpenNotificationEnabled;
			this.action = operation.Action;
			this.name = operation.Name;
			this.releaseInstanceAfterCall = operation.ReleaseInstanceAfterCall;
			this.releaseInstanceBeforeCall = operation.ReleaseInstanceBeforeCall;
			this.replyAction = operation.ReplyAction;
			this.isOneWay = operation.IsOneWay;
			this.transactionAutoComplete = operation.TransactionAutoComplete;
			this.transactionRequired = operation.TransactionRequired;
			this.receiveContextAcknowledgementMode = operation.ReceiveContextAcknowledgementMode;
			this.bufferedReceiveEnabled = operation.BufferedReceiveEnabled;
			this.isInsideTransactedReceiveScope = operation.IsInsideTransactedReceiveScope;
			if (this.formatter == null && (this.deserializeRequest || this.serializeReply))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DispatchRuntimeRequiresFormatter0", new object[]
				{
					this.name
				})));
			}
			if (operation.Parent.InstanceProvider == null && operation.Parent.Type != null)
			{
				SyncMethodInvoker syncMethodInvoker = this.invoker as SyncMethodInvoker;
				if (syncMethodInvoker != null)
				{
					this.ValidateInstanceType(operation.Parent.Type, syncMethodInvoker.Method);
				}
				AsyncMethodInvoker asyncMethodInvoker = this.invoker as AsyncMethodInvoker;
				if (asyncMethodInvoker != null)
				{
					this.ValidateInstanceType(operation.Parent.Type, asyncMethodInvoker.BeginMethod);
					this.ValidateInstanceType(operation.Parent.Type, asyncMethodInvoker.EndMethod);
				}
				TaskMethodInvoker taskMethodInvoker = this.invoker as TaskMethodInvoker;
				if (taskMethodInvoker != null)
				{
					this.ValidateInstanceType(operation.Parent.Type, taskMethodInvoker.TaskMethod);
				}
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x000CBC34 File Offset: 0x000C9E34
		internal string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x000CBC3C File Offset: 0x000C9E3C
		internal ICallContextInitializer[] CallContextInitializers
		{
			get
			{
				return this.callContextInitializers;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x000CBC44 File Offset: 0x000C9E44
		internal bool DisposeParameters
		{
			get
			{
				return this.disposeParameters;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060034AF RID: 13487 RVA: 0x000CBC4C File Offset: 0x000C9E4C
		internal bool HasDefaultUnhandledActionInvoker
		{
			get
			{
				return this.invoker is DispatchRuntime.UnhandledActionInvoker;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000CBC5C File Offset: 0x000C9E5C
		internal bool SerializeReply
		{
			get
			{
				return this.serializeReply;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000CBC64 File Offset: 0x000C9E64
		internal IDispatchFaultFormatter FaultFormatter
		{
			get
			{
				return this.faultFormatter;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x000CBC6C File Offset: 0x000C9E6C
		internal IDispatchMessageFormatter Formatter
		{
			get
			{
				return this.formatter;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000CBC74 File Offset: 0x000C9E74
		internal ImpersonationOption Impersonation
		{
			get
			{
				return this.impersonation;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000CBC7C File Offset: 0x000C9E7C
		internal IOperationInvoker Invoker
		{
			get
			{
				return this.invoker;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000CBC84 File Offset: 0x000C9E84
		internal bool IsSynchronous
		{
			get
			{
				return this.isSynchronous;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000CBC8C File Offset: 0x000C9E8C
		internal bool IsOneWay
		{
			get
			{
				return this.isOneWay;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000CBC94 File Offset: 0x000C9E94
		internal bool IsTerminating
		{
			get
			{
				return this.isTerminating;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000CBC9C File Offset: 0x000C9E9C
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
		internal IParameterInspector[] ParameterInspectors
		{
			get
			{
				return this.inspectors;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000CBCAC File Offset: 0x000C9EAC
		internal ImmutableDispatchRuntime Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000CBCB4 File Offset: 0x000C9EB4
		internal ReceiveContextAcknowledgementMode ReceiveContextAcknowledgementMode
		{
			get
			{
				return this.receiveContextAcknowledgementMode;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000CBCBC File Offset: 0x000C9EBC
		internal bool ReleaseInstanceAfterCall
		{
			get
			{
				return this.releaseInstanceAfterCall;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000CBCC4 File Offset: 0x000C9EC4
		internal bool ReleaseInstanceBeforeCall
		{
			get
			{
				return this.releaseInstanceBeforeCall;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000CBCCC File Offset: 0x000C9ECC
		internal string ReplyAction
		{
			get
			{
				return this.replyAction;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000CBCD4 File Offset: 0x000C9ED4
		internal bool TransactionAutoComplete
		{
			get
			{
				return this.transactionAutoComplete;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x000CBCDC File Offset: 0x000C9EDC
		internal bool TransactionRequired
		{
			get
			{
				return this.transactionRequired;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000CBCE4 File Offset: 0x000C9EE4
		internal bool IsInsideTransactedReceiveScope
		{
			get
			{
				return this.isInsideTransactedReceiveScope;
			}
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000CBCEC File Offset: 0x000C9EEC
		private void DeserializeInputs(ref MessageRpc rpc)
		{
			bool flag = false;
			try
			{
				try
				{
					rpc.InputParameters = this.Invoker.AllocateInputs();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (ErrorBehavior.ShouldRethrowExceptionAsIs(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
				try
				{
					if (!this.isSessionOpenNotificationEnabled)
					{
						if (this.deserializeRequest)
						{
							if (TD.DispatchFormatterDeserializeRequestStartIsEnabled())
							{
								TD.DispatchFormatterDeserializeRequestStart(rpc.EventTraceActivity);
							}
							bool flag2 = DS.MessageFormatterIsEnabled();
							Stopwatch stopwatch = null;
							if (flag2)
							{
								stopwatch = Stopwatch.StartNew();
							}
							this.Formatter.DeserializeRequest(rpc.Request, rpc.InputParameters);
							if (flag2)
							{
								DS.DispatchMessageFormatterDeserialize(this.Formatter.GetType(), stopwatch.Elapsed);
							}
							if (TD.DispatchFormatterDeserializeRequestStopIsEnabled())
							{
								TD.DispatchFormatterDeserializeRequestStop(rpc.EventTraceActivity);
							}
						}
						else
						{
							rpc.InputParameters[0] = rpc.Request;
						}
					}
					flag = true;
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (ErrorBehavior.ShouldRethrowExceptionAsIs(ex2))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex2);
				}
			}
			finally
			{
				rpc.DidDeserializeRequestBody = (rpc.Request.State > MessageState.Created);
				if (!flag && MessageLogger.LoggingEnabled)
				{
					MessageLogger.LogMessage(ref rpc.Request, MessageLoggingSource.Malformed);
				}
			}
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000CBE38 File Offset: 0x000CA038
		private void InitializeCallContext(ref MessageRpc rpc)
		{
			if (this.CallContextInitializers.Length != 0)
			{
				this.InitializeCallContextCore(ref rpc);
			}
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000CBE4C File Offset: 0x000CA04C
		private void InitializeCallContextCore(ref MessageRpc rpc)
		{
			IClientChannel channel = rpc.Channel.Proxy as IClientChannel;
			int callContextCorrelationOffset = this.Parent.CallContextCorrelationOffset;
			try
			{
				for (int i = 0; i < rpc.Operation.CallContextInitializers.Length; i++)
				{
					ICallContextInitializer callContextInitializer = this.CallContextInitializers[i];
					rpc.Correlation[callContextCorrelationOffset + i] = callContextInitializer.BeforeInvoke(rpc.InstanceContext, channel, rpc.Request);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ErrorBehavior.ShouldRethrowExceptionAsIs(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000CBEEC File Offset: 0x000CA0EC
		private void UninitializeCallContext(ref MessageRpc rpc)
		{
			if (this.CallContextInitializers.Length != 0)
			{
				this.UninitializeCallContextCore(ref rpc);
			}
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000CBF00 File Offset: 0x000CA100
		private void UninitializeCallContextCore(ref MessageRpc rpc)
		{
			IClientChannel clientChannel = rpc.Channel.Proxy as IClientChannel;
			int callContextCorrelationOffset = this.Parent.CallContextCorrelationOffset;
			try
			{
				for (int i = this.CallContextInitializers.Length - 1; i >= 0; i--)
				{
					ICallContextInitializer callContextInitializer = this.CallContextInitializers[i];
					callContextInitializer.AfterInvoke(rpc.Correlation[callContextCorrelationOffset + i]);
				}
			}
			catch (Exception ex)
			{
				DiagnosticUtility.FailFast(string.Format(CultureInfo.InvariantCulture, "ICallContextInitializer.BeforeInvoke threw an exception of type {0}: {1}", new object[]
				{
					ex.GetType(),
					ex.Message
				}));
			}
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000CBFA0 File Offset: 0x000CA1A0
		private void InspectInputs(ref MessageRpc rpc)
		{
			if (this.ParameterInspectors.Length != 0)
			{
				this.InspectInputsCore(ref rpc);
			}
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000CBFB4 File Offset: 0x000CA1B4
		private void InspectInputsCore(ref MessageRpc rpc)
		{
			int parameterInspectorCorrelationOffset = this.Parent.ParameterInspectorCorrelationOffset;
			bool flag = DS.ParameterInspectorIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = new Stopwatch();
			}
			for (int i = 0; i < this.ParameterInspectors.Length; i++)
			{
				IParameterInspector parameterInspector = this.ParameterInspectors[i];
				if (flag)
				{
					stopwatch.Restart();
				}
				rpc.Correlation[parameterInspectorCorrelationOffset + i] = parameterInspector.BeforeCall(this.Name, rpc.InputParameters);
				if (flag)
				{
					DS.ParameterInspectorBefore(parameterInspector.GetType(), stopwatch.Elapsed);
				}
				if (TD.ParameterInspectorBeforeCallInvokedIsEnabled())
				{
					TD.ParameterInspectorBeforeCallInvoked(rpc.EventTraceActivity, parameterInspector.GetType().FullName);
				}
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000CC054 File Offset: 0x000CA254
		private void InspectOutputs(ref MessageRpc rpc)
		{
			if (this.ParameterInspectors.Length != 0)
			{
				this.InspectOutputsCore(ref rpc);
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000CC068 File Offset: 0x000CA268
		private void InspectOutputsCore(ref MessageRpc rpc)
		{
			int parameterInspectorCorrelationOffset = this.Parent.ParameterInspectorCorrelationOffset;
			bool flag = DS.ParameterInspectorIsEnabled();
			Stopwatch stopwatch = null;
			if (flag)
			{
				stopwatch = new Stopwatch();
			}
			for (int i = this.ParameterInspectors.Length - 1; i >= 0; i--)
			{
				IParameterInspector parameterInspector = this.ParameterInspectors[i];
				if (flag)
				{
					stopwatch.Restart();
				}
				parameterInspector.AfterCall(this.Name, rpc.OutputParameters, rpc.ReturnParameter, rpc.Correlation[parameterInspectorCorrelationOffset + i]);
				if (flag)
				{
					DS.ParameterInspectorAfter(parameterInspector.GetType(), stopwatch.Elapsed);
				}
				if (TD.ParameterInspectorAfterCallInvokedIsEnabled())
				{
					TD.ParameterInspectorAfterCallInvoked(rpc.EventTraceActivity, parameterInspector.GetType().FullName);
				}
			}
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000CC110 File Offset: 0x000CA310
		[DebuggerStepperBoundary]
		[SecuritySafeCritical]
		internal void InvokeBegin(ref MessageRpc rpc)
		{
			if (rpc.Error == null)
			{
				try
				{
					this.InitializeCallContext(ref rpc);
					object instance = rpc.Instance;
					this.DeserializeInputs(ref rpc);
					this.InspectInputs(ref rpc);
					this.ValidateMustUnderstand(ref rpc);
					IAsyncResult asyncResult = null;
					IDisposable impersonationContext = null;
					IPrincipal originalPrincipal = null;
					bool isThreadPrincipalSet = false;
					bool flag = this.Parent.IsConcurrent(ref rpc);
					try
					{
						if (this.parent.RequireClaimsPrincipalOnOperationContext)
						{
							this.SetClaimsPrincipalToOperationContext(rpc);
						}
						if (this.parent.SecurityImpersonation != null)
						{
							this.parent.SecurityImpersonation.StartImpersonation(ref rpc, out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
						}
						IManualConcurrencyOperationInvoker manualConcurrencyOperationInvoker = this.Invoker as IManualConcurrencyOperationInvoker;
						if (DS.OperationInvokerIsEnabled())
						{
							DS.InvokeOperationStart(this.Invoker.GetType(), Stopwatch.GetTimestamp());
						}
						if (this.isSynchronous)
						{
							if (manualConcurrencyOperationInvoker != null && flag)
							{
								if (this.bufferedReceiveEnabled)
								{
									rpc.OperationContext.IncomingMessageProperties.Add(BufferedReceiveMessageProperty.Name, new BufferedReceiveMessageProperty(ref rpc));
								}
								rpc.ReturnParameter = manualConcurrencyOperationInvoker.Invoke(instance, rpc.InputParameters, rpc.InvokeNotification, out rpc.OutputParameters);
							}
							else
							{
								rpc.ReturnParameter = this.Invoker.Invoke(instance, rpc.InputParameters, out rpc.OutputParameters);
							}
						}
						else
						{
							bool flag2 = false;
							if (manualConcurrencyOperationInvoker != null && flag && this.bufferedReceiveEnabled)
							{
								rpc.OperationContext.IncomingMessageProperties.Add(BufferedReceiveMessageProperty.Name, new BufferedReceiveMessageProperty(ref rpc));
							}
							IResumeMessageRpc state = rpc.Pause();
							try
							{
								if (manualConcurrencyOperationInvoker != null && flag)
								{
									asyncResult = manualConcurrencyOperationInvoker.InvokeBegin(instance, rpc.InputParameters, rpc.InvokeNotification, DispatchOperationRuntime.invokeCallback, state);
								}
								else
								{
									asyncResult = this.Invoker.InvokeBegin(instance, rpc.InputParameters, DispatchOperationRuntime.invokeCallback, state);
								}
								flag2 = true;
							}
							finally
							{
								if (!flag2)
								{
									rpc.UnPause();
								}
							}
						}
					}
					finally
					{
						try
						{
							if (this.parent.SecurityImpersonation != null)
							{
								this.parent.SecurityImpersonation.StopImpersonation(ref rpc, impersonationContext, originalPrincipal, isThreadPrincipalSet);
							}
						}
						catch
						{
							string message = null;
							try
							{
								message = SR.GetString("SFxRevertImpersonationFailed0");
							}
							finally
							{
								DiagnosticUtility.FailFast(message);
							}
						}
						if (this.isSynchronous && DS.OperationInvokerIsEnabled())
						{
							DS.InvokeOperationStop(Stopwatch.GetTimestamp());
						}
					}
					if (this.isSynchronous)
					{
						this.InspectOutputs(ref rpc);
						this.SerializeOutputs(ref rpc);
					}
					else
					{
						if (asyncResult == null)
						{
							throw TraceUtility.ThrowHelperError(new ArgumentNullException("IOperationInvoker.BeginDispatch"), rpc.Request);
						}
						if (asyncResult.CompletedSynchronously)
						{
							rpc.UnPause();
							rpc.AsyncResult = asyncResult;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					this.UninitializeCallContext(ref rpc);
				}
			}
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000CC414 File Offset: 0x000CA614
		private void SetClaimsPrincipalToOperationContext(MessageRpc rpc)
		{
			ServiceSecurityContext serviceSecurityContext = rpc.SecurityContext;
			if (!rpc.HasSecurityContext)
			{
				SecurityMessageProperty security = rpc.Request.Properties.Security;
				if (security != null)
				{
					serviceSecurityContext = security.ServiceSecurityContext;
				}
			}
			object obj;
			if (serviceSecurityContext == null || !serviceSecurityContext.AuthorizationContext.Properties.TryGetValue("ClaimsPrincipal", out obj))
			{
				return;
			}
			ClaimsPrincipal claimsPrincipal = obj as ClaimsPrincipal;
			if (claimsPrincipal != null)
			{
				OperationContext.Current.ClaimsPrincipal = claimsPrincipal;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoPrincipalSpecifiedInAuthorizationContext")));
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000CC498 File Offset: 0x000CA698
		private static void InvokeCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			IResumeMessageRpc resumeMessageRpc = result.AsyncState as IResumeMessageRpc;
			if (resumeMessageRpc == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SFxInvalidAsyncResultState0"));
			}
			resumeMessageRpc.SignalConditionalResume(result);
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000CC4DC File Offset: 0x000CA6DC
		[DebuggerStepperBoundary]
		[SecuritySafeCritical]
		internal void InvokeEnd(ref MessageRpc rpc)
		{
			if (rpc.Error == null && !this.isSynchronous)
			{
				try
				{
					this.InitializeCallContext(ref rpc);
					if (this.parent.RequireClaimsPrincipalOnOperationContext)
					{
						this.SetClaimsPrincipalToOperationContext(rpc);
					}
					IDisposable impersonationContext = null;
					IPrincipal originalPrincipal = null;
					bool isThreadPrincipalSet = false;
					try
					{
						if (this.parent.SecurityImpersonation != null)
						{
							this.parent.SecurityImpersonation.StartImpersonation(ref rpc, out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
						}
						rpc.ReturnParameter = this.Invoker.InvokeEnd(rpc.Instance, out rpc.OutputParameters, rpc.AsyncResult);
					}
					finally
					{
						try
						{
							if (this.parent.SecurityImpersonation != null)
							{
								this.parent.SecurityImpersonation.StopImpersonation(ref rpc, impersonationContext, originalPrincipal, isThreadPrincipalSet);
							}
						}
						catch
						{
							string message = null;
							try
							{
								message = SR.GetString("SFxRevertImpersonationFailed0");
							}
							finally
							{
								DiagnosticUtility.FailFast(message);
							}
						}
						if (DS.OperationInvokerIsEnabled())
						{
							DS.InvokeOperationStop(Stopwatch.GetTimestamp());
						}
					}
					this.InspectOutputs(ref rpc);
					this.SerializeOutputs(ref rpc);
				}
				catch
				{
					throw;
				}
				finally
				{
					this.UninitializeCallContext(ref rpc);
				}
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000CC61C File Offset: 0x000CA81C
		private void SerializeOutputs(ref MessageRpc rpc)
		{
			if (!this.IsOneWay && this.parent.EnableFaults)
			{
				Message message;
				if (this.serializeReply)
				{
					try
					{
						if (TD.DispatchFormatterSerializeReplyStartIsEnabled())
						{
							TD.DispatchFormatterSerializeReplyStart(rpc.EventTraceActivity);
						}
						bool flag = DS.MessageFormatterIsEnabled();
						Stopwatch stopwatch = null;
						if (flag)
						{
							stopwatch = Stopwatch.StartNew();
						}
						message = this.Formatter.SerializeReply(rpc.RequestVersion, rpc.OutputParameters, rpc.ReturnParameter);
						if (flag)
						{
							DS.DispatchMessageFormatterSerialize(this.Formatter.GetType(), stopwatch.Elapsed);
						}
						if (TD.DispatchFormatterSerializeReplyStopIsEnabled())
						{
							TD.DispatchFormatterSerializeReplyStop(rpc.EventTraceActivity);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (ErrorBehavior.ShouldRethrowExceptionAsIs(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
					}
					if (message == null)
					{
						string @string = SR.GetString("SFxNullReplyFromFormatter2", new object[]
						{
							this.Formatter.GetType().ToString(),
							this.name ?? ""
						});
						ErrorBehavior.ThrowAndCatch(new InvalidOperationException(@string));
					}
				}
				else
				{
					if (rpc.ReturnParameter == null && rpc.OperationContext.RequestContext != null)
					{
						string string2 = SR.GetString("SFxDispatchRuntimeMessageCannotBeNull", new object[]
						{
							this.name
						});
						ErrorBehavior.ThrowAndCatch(new InvalidOperationException(string2));
					}
					message = (Message)rpc.ReturnParameter;
					if (message != null && !ProxyOperationRuntime.IsValidAction(message, this.ReplyAction))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidReplyAction", new object[]
						{
							this.Name,
							message.Headers.Action ?? "{NULL}",
							this.ReplyAction
						})));
					}
				}
				if (DiagnosticUtility.ShouldUseActivity && rpc.Activity != null && message != null)
				{
					TraceUtility.SetActivity(message, rpc.Activity);
					if (TraceUtility.ShouldPropagateActivity)
					{
						TraceUtility.AddActivityHeader(message);
					}
				}
				else if (TraceUtility.ShouldPropagateActivity && message != null && rpc.ResponseActivityId != Guid.Empty)
				{
					ActivityIdHeader activityIdHeader = new ActivityIdHeader(rpc.ResponseActivityId);
					activityIdHeader.AddTo(message);
				}
				if (TraceUtility.MessageFlowTracingOnly)
				{
					if (rpc.OperationContext.IncomingMessage != null && MessageState.Closed != rpc.OperationContext.IncomingMessage.State)
					{
						FxTrace.Trace.SetAndTraceTransfer(TraceUtility.GetReceivedActivityId(rpc.OperationContext), true);
					}
					else if (rpc.ResponseActivityId != Guid.Empty)
					{
						FxTrace.Trace.SetAndTraceTransfer(rpc.ResponseActivityId, true);
					}
				}
				if (message != null && this.parent.IsImpersonationEnabledOnSerializingReply)
				{
					bool flag2 = this.parent.SecurityImpersonation != null && this.parent.SecurityImpersonation.IsImpersonationEnabledOnCurrentOperation(ref rpc);
					if (flag2)
					{
						message.Properties.Add(ImpersonateOnSerializingReplyMessageProperty.Name, new ImpersonateOnSerializingReplyMessageProperty(ref rpc));
						message = new ImpersonatingMessage(message);
					}
				}
				if (MessageLogger.LoggingEnabled && message != null)
				{
					MessageLogger.LogMessage(ref message, MessageLoggingSource.ServiceLevelSendReply | MessageLoggingSource.LastChance);
				}
				rpc.Reply = message;
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000CC910 File Offset: 0x000CAB10
		private void ValidateInstanceType(Type type, MethodInfo method)
		{
			if (!method.DeclaringType.IsAssignableFrom(type))
			{
				string @string = SR.GetString("SFxMethodNotSupportedByType2", new object[]
				{
					type.FullName,
					method.DeclaringType.FullName
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(@string));
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000CC964 File Offset: 0x000CAB64
		private void ValidateMustUnderstand(ref MessageRpc rpc)
		{
			if (this.parent.ValidateMustUnderstand)
			{
				rpc.NotUnderstoodHeaders = rpc.Request.Headers.GetHeadersNotUnderstood();
				if (rpc.NotUnderstoodHeaders != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MustUnderstandSoapException(rpc.NotUnderstoodHeaders, rpc.Request.Version.Envelope));
				}
			}
		}

		// Token: 0x04002811 RID: 10257
		private static AsyncCallback invokeCallback = Fx.ThunkCallback(new AsyncCallback(DispatchOperationRuntime.InvokeCallback));

		// Token: 0x04002812 RID: 10258
		private readonly string action;

		// Token: 0x04002813 RID: 10259
		private readonly ICallContextInitializer[] callContextInitializers;

		// Token: 0x04002814 RID: 10260
		private readonly IDispatchFaultFormatter faultFormatter;

		// Token: 0x04002815 RID: 10261
		private readonly IDispatchMessageFormatter formatter;

		// Token: 0x04002816 RID: 10262
		private readonly ImpersonationOption impersonation;

		// Token: 0x04002817 RID: 10263
		private readonly IParameterInspector[] inspectors;

		// Token: 0x04002818 RID: 10264
		private readonly IOperationInvoker invoker;

		// Token: 0x04002819 RID: 10265
		private readonly bool isTerminating;

		// Token: 0x0400281A RID: 10266
		private readonly bool isSessionOpenNotificationEnabled;

		// Token: 0x0400281B RID: 10267
		private readonly bool isSynchronous;

		// Token: 0x0400281C RID: 10268
		private readonly string name;

		// Token: 0x0400281D RID: 10269
		private readonly ImmutableDispatchRuntime parent;

		// Token: 0x0400281E RID: 10270
		private readonly bool releaseInstanceAfterCall;

		// Token: 0x0400281F RID: 10271
		private readonly bool releaseInstanceBeforeCall;

		// Token: 0x04002820 RID: 10272
		private readonly string replyAction;

		// Token: 0x04002821 RID: 10273
		private readonly bool transactionAutoComplete;

		// Token: 0x04002822 RID: 10274
		private readonly bool transactionRequired;

		// Token: 0x04002823 RID: 10275
		private readonly bool deserializeRequest;

		// Token: 0x04002824 RID: 10276
		private readonly bool serializeReply;

		// Token: 0x04002825 RID: 10277
		private readonly bool isOneWay;

		// Token: 0x04002826 RID: 10278
		private readonly bool disposeParameters;

		// Token: 0x04002827 RID: 10279
		private readonly ReceiveContextAcknowledgementMode receiveContextAcknowledgementMode;

		// Token: 0x04002828 RID: 10280
		private readonly bool bufferedReceiveEnabled;

		// Token: 0x04002829 RID: 10281
		private readonly bool isInsideTransactedReceiveScope;
	}
}
