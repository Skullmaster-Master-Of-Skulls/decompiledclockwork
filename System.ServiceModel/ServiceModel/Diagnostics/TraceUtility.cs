using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA2 RID: 2722
	internal static class TraceUtility
	{
		// Token: 0x06006BBC RID: 27580 RVA: 0x00190DB4 File Offset: 0x0018EFB4
		public static InputQueue<T> CreateInputQueue<T>() where T : class
		{
			if (TraceUtility.asyncCallbackGenerator == null)
			{
				TraceUtility.asyncCallbackGenerator = new Func<Action<AsyncCallback, IAsyncResult>>(TraceUtility.CallbackGenerator);
			}
			InputQueue<T> inputQueue = new InputQueue<T>(TraceUtility.asyncCallbackGenerator);
			inputQueue.DisposeItemCallback = delegate(T value)
			{
				if (value is ICommunicationObject)
				{
					((ICommunicationObject)((object)value)).Abort();
				}
			};
			return inputQueue;
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x00190E08 File Offset: 0x0018F008
		private static Action<AsyncCallback, IAsyncResult> CallbackGenerator()
		{
			if (DiagnosticUtility.ShouldUseActivity)
			{
				ServiceModelActivity callbackActivity = ServiceModelActivity.Current;
				if (callbackActivity != null)
				{
					return delegate(AsyncCallback callback, IAsyncResult result)
					{
						using (ServiceModelActivity.BoundOperation(callbackActivity))
						{
							callback(result);
						}
					};
				}
			}
			return null;
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x00190E44 File Offset: 0x0018F044
		internal static void AddActivityHeader(Message message)
		{
			try
			{
				ActivityIdHeader activityIdHeader = new ActivityIdHeader(TraceUtility.ExtractActivityId(message));
				activityIdHeader.AddTo(message);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				TraceUtility.TraceEvent(TraceEventType.Error, 131078, SR.GetString("TraceCodeFailedToAddAnActivityIdHeader"), exception, message);
			}
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x00190E9C File Offset: 0x0018F09C
		internal static void AddAmbientActivityToMessage(Message message)
		{
			try
			{
				ActivityIdHeader activityIdHeader = new ActivityIdHeader(DiagnosticTraceBase.ActivityId);
				activityIdHeader.AddTo(message);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				TraceUtility.TraceEvent(TraceEventType.Error, 131078, SR.GetString("TraceCodeFailedToAddAnActivityIdHeader"), exception, message);
			}
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x00190EF4 File Offset: 0x0018F0F4
		internal static void CopyActivity(Message source, Message destination)
		{
			if (DiagnosticUtility.ShouldUseActivity)
			{
				TraceUtility.SetActivity(destination, TraceUtility.ExtractActivity(source));
			}
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x00190F0C File Offset: 0x0018F10C
		internal static long GetUtcBasedDurationForTrace(long startTicks)
		{
			if (startTicks > 0L)
			{
				TimeSpan timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - startTicks);
				return (long)timeSpan.TotalMilliseconds;
			}
			return 0L;
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x00190F40 File Offset: 0x0018F140
		internal static ServiceModelActivity ExtractActivity(Message message)
		{
			ServiceModelActivity result = null;
			object obj;
			if ((DiagnosticUtility.ShouldUseActivity || TraceUtility.ShouldPropagateActivityGlobal) && message != null && message.State != MessageState.Closed && message.GetProperty("ActivityId", out obj))
			{
				result = (obj as ServiceModelActivity);
			}
			return result;
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x00190F80 File Offset: 0x0018F180
		internal static ServiceModelActivity ExtractActivity(RequestContext request)
		{
			try
			{
				return TraceUtility.ExtractActivity(request.RequestMessage);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x00190FBC File Offset: 0x0018F1BC
		internal static Guid ExtractActivityId(Message message)
		{
			if (TraceUtility.MessageFlowTracingOnly)
			{
				return ActivityIdHeader.ExtractActivityId(message);
			}
			ServiceModelActivity serviceModelActivity = TraceUtility.ExtractActivity(message);
			if (serviceModelActivity != null)
			{
				return serviceModelActivity.Id;
			}
			return Guid.Empty;
		}

		// Token: 0x06006BC5 RID: 27589 RVA: 0x00190FF0 File Offset: 0x0018F1F0
		internal static Guid GetReceivedActivityId(OperationContext operationContext)
		{
			object obj;
			if (!operationContext.IncomingMessageProperties.TryGetValue("E2EActivityId", out obj))
			{
				return TraceUtility.ExtractActivityId(operationContext.IncomingMessage);
			}
			return (Guid)obj;
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x00191024 File Offset: 0x0018F224
		internal static ServiceModelActivity ExtractAndRemoveActivity(Message message)
		{
			ServiceModelActivity serviceModelActivity = TraceUtility.ExtractActivity(message);
			if (serviceModelActivity != null)
			{
				message.SetProperty("ActivityId", false);
			}
			return serviceModelActivity;
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x00191050 File Offset: 0x0018F250
		internal static void ProcessIncomingMessage(Message message, EventTraceActivity eventTraceActivity)
		{
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.Current;
			if (serviceModelActivity != null && DiagnosticUtility.ShouldUseActivity)
			{
				ServiceModelActivity serviceModelActivity2 = TraceUtility.ExtractActivity(message);
				if (serviceModelActivity2 != null && serviceModelActivity2.Id != serviceModelActivity.Id)
				{
					using (ServiceModelActivity.BoundOperation(serviceModelActivity2))
					{
						if (FxTrace.Trace != null)
						{
							FxTrace.Trace.TraceTransfer(serviceModelActivity.Id);
						}
					}
				}
				TraceUtility.SetActivity(message, serviceModelActivity);
			}
			TraceUtility.MessageFlowAtMessageReceived(message, null, eventTraceActivity, true);
			if (MessageLogger.LogMessagesAtServiceLevel)
			{
				MessageLogger.LogMessage(ref message, MessageLoggingSource.ServiceLevelReceiveReply | MessageLoggingSource.LastChance);
			}
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x001910E8 File Offset: 0x0018F2E8
		internal static void ProcessOutgoingMessage(Message message, EventTraceActivity eventTraceActivity)
		{
			ServiceModelActivity activity = ServiceModelActivity.Current;
			if (DiagnosticUtility.ShouldUseActivity)
			{
				TraceUtility.SetActivity(message, activity);
			}
			if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
			{
				TraceUtility.AddAmbientActivityToMessage(message);
			}
			TraceUtility.MessageFlowAtMessageSent(message, eventTraceActivity);
			if (MessageLogger.LogMessagesAtServiceLevel)
			{
				MessageLogger.LogMessage(ref message, MessageLoggingSource.ServiceLevelSendRequest | MessageLoggingSource.LastChance);
			}
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x00191137 File Offset: 0x0018F337
		internal static void SetActivity(Message message, ServiceModelActivity activity)
		{
			if (DiagnosticUtility.ShouldUseActivity && message != null && message.State != MessageState.Closed)
			{
				message.SetProperty("ActivityId", activity);
			}
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x00191158 File Offset: 0x0018F358
		internal static void TraceDroppedMessage(Message message, EndpointDispatcher dispatcher)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				EndpointAddress endpointAddress = null;
				if (dispatcher != null)
				{
					endpointAddress = dispatcher.EndpointAddress;
				}
				TraceUtility.TraceEvent(TraceEventType.Information, 524353, SR.GetString("TraceCodeDroppedAMessage"), new MessageDroppedTraceRecord(message, endpointAddress));
			}
		}

		// Token: 0x06006BCB RID: 27595 RVA: 0x00191194 File Offset: 0x0018F394
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription)
		{
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, null, traceDescription, null);
		}

		// Token: 0x06006BCC RID: 27596 RVA: 0x001911A1 File Offset: 0x0018F3A1
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData)
		{
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, extendedData, null, null);
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x001911AE File Offset: 0x0018F3AE
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, object source)
		{
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, null, source, null);
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x001911BB File Offset: 0x0018F3BB
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, object source, Exception exception)
		{
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, null, source, exception);
		}

		// Token: 0x06006BCF RID: 27599 RVA: 0x001911C9 File Offset: 0x0018F3C9
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, Message message)
		{
			if (message == null)
			{
				TraceUtility.TraceEvent(severity, traceCode, traceDescription, null, null);
				return;
			}
			TraceUtility.TraceEvent(severity, traceCode, traceDescription, message, message);
		}

		// Token: 0x06006BD0 RID: 27600 RVA: 0x001911E4 File Offset: 0x0018F3E4
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, object source, Message message)
		{
			Guid activityId = TraceUtility.ExtractActivityId(message);
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, new MessageTraceRecord(message), null, activityId, message);
			}
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x00191220 File Offset: 0x0018F420
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, Exception exception, Message message)
		{
			Guid activityId = TraceUtility.ExtractActivityId(message);
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, new MessageTraceRecord(message), exception, activityId, null);
			}
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x0019125A File Offset: 0x0018F45A
		internal static void TraceEventNoCheck(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception)
		{
			DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, extendedData, exception, source);
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x00191274 File Offset: 0x0018F474
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception)
		{
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, extendedData, exception, source);
			}
		}

		// Token: 0x06006BD4 RID: 27604 RVA: 0x00191298 File Offset: 0x0018F498
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception, Message message)
		{
			Guid activityId = TraceUtility.ExtractActivityId(message);
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, extendedData, exception, activityId, source);
			}
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x001912D0 File Offset: 0x0018F4D0
		internal static void TraceEventNoCheck(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception, Guid activityId)
		{
			DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, extendedData, exception, activityId, source);
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x001912F8 File Offset: 0x0018F4F8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceEvent(TraceEventType severity, int traceCode, string traceDescription, TraceRecord extendedData, object source, Exception exception, Guid activityId)
		{
			if (DiagnosticUtility.ShouldTrace(severity))
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(severity, traceCode, TraceUtility.GenerateMsdnTraceCode(traceCode), traceDescription, extendedData, exception, activityId, source);
			}
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x00191328 File Offset: 0x0018F528
		private static string GenerateMsdnTraceCode(int traceCode)
		{
			int num = (int)((long)traceCode & (long)((ulong)-65536));
			string traceSource;
			if (num <= 262144)
			{
				if (num <= 131072)
				{
					if (num == 65536)
					{
						traceSource = "System.ServiceModel.Administration";
						goto IL_BE;
					}
					if (num == 131072)
					{
						traceSource = "System.ServiceModel.Diagnostics";
						goto IL_BE;
					}
				}
				else
				{
					if (num == 196608)
					{
						traceSource = "System.Runtime.Serialization";
						goto IL_BE;
					}
					if (num == 262144)
					{
						traceSource = "System.ServiceModel.Channels";
						goto IL_BE;
					}
				}
			}
			else
			{
				if (num > 458752)
				{
					if (num != 524288)
					{
						if (num == 655360)
						{
							traceSource = "System.ServiceModel.PortSharing";
							goto IL_BE;
						}
						if (num != 917504)
						{
							goto IL_B8;
						}
					}
					traceSource = "System.ServiceModel";
					goto IL_BE;
				}
				if (num == 327680)
				{
					traceSource = "System.ServiceModel.ComIntegration";
					goto IL_BE;
				}
				if (num == 458752)
				{
					traceSource = "System.ServiceModel.Security";
					goto IL_BE;
				}
			}
			IL_B8:
			traceSource = string.Empty;
			IL_BE:
			return LegacyDiagnosticTrace.GenerateMsdnTraceCode(traceSource, TraceUtility.traceCodes[traceCode]);
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x00191404 File Offset: 0x0018F604
		internal static Exception ThrowHelperError(Exception exception, Message message)
		{
			Guid activityId = TraceUtility.ExtractActivityId(message);
			if (DiagnosticUtility.ShouldTraceError)
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(TraceEventType.Error, 131075, TraceUtility.GenerateMsdnTraceCode(131075), TraceSR.GetString("ThrowingException"), null, exception, activityId, null);
			}
			return exception;
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x00191448 File Offset: 0x0018F648
		internal static Exception ThrowHelperError(Exception exception, Guid activityId, object source)
		{
			if (DiagnosticUtility.ShouldTraceError)
			{
				DiagnosticUtility.DiagnosticTrace.TraceEvent(TraceEventType.Error, 131075, TraceUtility.GenerateMsdnTraceCode(131075), TraceSR.GetString("ThrowingException"), null, exception, activityId, source);
			}
			return exception;
		}

		// Token: 0x06006BDA RID: 27610 RVA: 0x00191488 File Offset: 0x0018F688
		internal static Exception ThrowHelperWarning(Exception exception, Message message)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				Guid activityId = TraceUtility.ExtractActivityId(message);
				DiagnosticUtility.DiagnosticTrace.TraceEvent(TraceEventType.Warning, 131075, TraceUtility.GenerateMsdnTraceCode(131075), TraceSR.GetString("ThrowingException"), null, exception, activityId, null);
			}
			return exception;
		}

		// Token: 0x06006BDB RID: 27611 RVA: 0x001914CC File Offset: 0x0018F6CC
		internal static ArgumentException ThrowHelperArgument(string paramName, string message, Message msg)
		{
			return (ArgumentException)TraceUtility.ThrowHelperError(new ArgumentException(message, paramName), msg);
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x001914E0 File Offset: 0x0018F6E0
		internal static ArgumentNullException ThrowHelperArgumentNull(string paramName, Message message)
		{
			return (ArgumentNullException)TraceUtility.ThrowHelperError(new ArgumentNullException(paramName), message);
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x001914F4 File Offset: 0x0018F6F4
		internal static string CreateSourceString(object source)
		{
			return source.GetType().ToString() + "/" + source.GetHashCode().ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06006BDE RID: 27614 RVA: 0x0019152C File Offset: 0x0018F72C
		internal static void TraceHttpConnectionInformation(string localEndpoint, string remoteEndpoint, object source)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(2)
				{
					{
						"LocalEndpoint",
						localEndpoint
					},
					{
						"RemoteEndpoint",
						remoteEndpoint
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, 262168, SR.GetString("TraceCodeConnectToIPEndpoint"), new DictionaryTraceRecord(dictionary), source, null);
			}
		}

		// Token: 0x06006BDF RID: 27615 RVA: 0x0019157C File Offset: 0x0018F77C
		internal static void TraceUserCodeException(Exception e, MethodInfo method)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				StringTraceRecord trace = new StringTraceRecord("Comment", SR.GetString("SFxUserCodeThrewException", new object[]
				{
					method.DeclaringType.FullName,
					method.Name
				}));
				DiagnosticUtility.DiagnosticTrace.TraceEvent(TraceEventType.Warning, 524352, TraceUtility.GenerateMsdnTraceCode(524352), SR.GetString("TraceCodeUnhandledExceptionInUserOperation", new object[]
				{
					method.DeclaringType.FullName,
					method.Name
				}), trace, e, null);
			}
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x00191608 File Offset: 0x0018F808
		static TraceUtility()
		{
			TraceUtility.SetEtwProviderId();
			TraceUtility.SetEndToEndTracingFlags();
			if (DiagnosticUtility.DiagnosticTrace != null)
			{
				DiagnosticTraceSource diagnosticTraceSource = (DiagnosticTraceSource)DiagnosticUtility.DiagnosticTrace.TraceSource;
				TraceUtility.shouldPropagateActivity = (diagnosticTraceSource.PropagateActivity || TraceUtility.shouldPropagateActivityGlobal);
			}
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x00192E64 File Offset: 0x00191064
		[SecuritySafeCritical]
		private static void SetEndToEndTracingFlags()
		{
			EndToEndTracingElement endToEndTracing = DiagnosticSection.UnsafeGetSection().EndToEndTracing;
			TraceUtility.shouldPropagateActivityGlobal = endToEndTracing.PropagateActivity;
			TraceUtility.shouldPropagateActivity = (TraceUtility.shouldPropagateActivityGlobal || TraceUtility.shouldPropagateActivity);
			DiagnosticUtility.ShouldUseActivity = (DiagnosticUtility.ShouldUseActivity || endToEndTracing.ActivityTracing);
			TraceUtility.activityTracing = DiagnosticUtility.ShouldUseActivity;
			TraceUtility.messageFlowTracing = (endToEndTracing.MessageFlowTracing || TraceUtility.activityTracing);
			TraceUtility.messageFlowTracingOnly = (endToEndTracing.MessageFlowTracing && !endToEndTracing.ActivityTracing);
			DiagnosticUtility.TracingEnabled = (DiagnosticUtility.TracingEnabled || TraceUtility.activityTracing);
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x00192EFC File Offset: 0x001910FC
		public static long RetrieveMessageNumber()
		{
			return Interlocked.Increment(ref TraceUtility.messageNumber);
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06006BE3 RID: 27619 RVA: 0x00192F08 File Offset: 0x00191108
		public static bool PropagateUserActivity
		{
			get
			{
				return TraceUtility.ShouldPropagateActivity && TraceUtility.PropagateUserActivityCore;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x00192F18 File Offset: 0x00191118
		private static bool PropagateUserActivityCore
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return !DiagnosticUtility.TracingEnabled && DiagnosticTraceBase.ActivityId != Guid.Empty;
			}
		}

		// Token: 0x06006BE5 RID: 27621 RVA: 0x00192F34 File Offset: 0x00191134
		internal static string GetCallerInfo(OperationContext context)
		{
			object obj;
			if (context != null && context.IncomingMessageProperties != null && context.IncomingMessageProperties.TryGetValue(RemoteEndpointMessageProperty.Name, out obj))
			{
				RemoteEndpointMessageProperty remoteEndpointMessageProperty = obj as RemoteEndpointMessageProperty;
				if (remoteEndpointMessageProperty != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
					{
						remoteEndpointMessageProperty.Address,
						remoteEndpointMessageProperty.Port
					});
				}
			}
			return "null";
		}

		// Token: 0x06006BE6 RID: 27622 RVA: 0x00192F9C File Offset: 0x0019119C
		[SecuritySafeCritical]
		internal static void SetEtwProviderId()
		{
			DiagnosticSection diagnosticSection = DiagnosticSection.UnsafeGetSectionNoTrace();
			Guid defaultEtwProviderId = Guid.Empty;
			if (PartialTrustHelpers.HasEtwPermissions() || diagnosticSection.IsEtwProviderIdFromConfigFile())
			{
				defaultEtwProviderId = Fx.CreateGuid(diagnosticSection.EtwProviderId);
			}
			EtwDiagnosticTrace.DefaultEtwProviderId = defaultEtwProviderId;
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x00192FD8 File Offset: 0x001911D8
		internal static void SetActivityId(MessageProperties properties)
		{
			Guid activityId;
			if (properties != null && properties.TryGetValue<Guid>("E2EActivityId", out activityId))
			{
				DiagnosticTraceBase.ActivityId = activityId;
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x00192FFD File Offset: 0x001911FD
		internal static bool ShouldPropagateActivity
		{
			get
			{
				return TraceUtility.shouldPropagateActivity;
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x00193004 File Offset: 0x00191204
		internal static bool ShouldPropagateActivityGlobal
		{
			get
			{
				return TraceUtility.shouldPropagateActivityGlobal;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x0019300B File Offset: 0x0019120B
		internal static bool ActivityTracing
		{
			get
			{
				return TraceUtility.activityTracing;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06006BEB RID: 27627 RVA: 0x00193012 File Offset: 0x00191212
		internal static bool MessageFlowTracing
		{
			get
			{
				return TraceUtility.messageFlowTracing;
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06006BEC RID: 27628 RVA: 0x00193019 File Offset: 0x00191219
		internal static bool MessageFlowTracingOnly
		{
			get
			{
				return TraceUtility.messageFlowTracingOnly;
			}
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x00193020 File Offset: 0x00191220
		internal static void MessageFlowAtMessageSent(Message message, EventTraceActivity eventTraceActivity)
		{
			if (TraceUtility.MessageFlowTracing)
			{
				Guid guid;
				Guid correlationId;
				bool flag = ActivityIdHeader.ExtractActivityAndCorrelationId(message, out guid, out correlationId);
				if (TraceUtility.MessageFlowTracingOnly && flag && guid != DiagnosticTraceBase.ActivityId)
				{
					DiagnosticTraceBase.ActivityId = guid;
				}
				if (TD.MessageSentToTransportIsEnabled())
				{
					TD.MessageSentToTransport(eventTraceActivity, correlationId);
				}
			}
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x0019306C File Offset: 0x0019126C
		internal static void MessageFlowAtMessageReceived(Message message, OperationContext context, EventTraceActivity eventTraceActivity, bool createNewActivityId)
		{
			if (TraceUtility.MessageFlowTracing)
			{
				Guid newId;
				Guid correlationId;
				bool flag = ActivityIdHeader.ExtractActivityAndCorrelationId(message, out newId, out correlationId);
				if (TraceUtility.MessageFlowTracingOnly)
				{
					if (createNewActivityId)
					{
						if (!flag)
						{
							newId = Guid.NewGuid();
							flag = true;
						}
						DiagnosticTraceBase.ActivityId = Guid.Empty;
					}
					if (flag)
					{
						FxTrace.Trace.SetAndTraceTransfer(newId, !createNewActivityId);
						message.Properties["E2EActivityId"] = Trace.CorrelationManager.ActivityId;
					}
				}
				if (TD.MessageReceivedFromTransportIsEnabled())
				{
					if (context == null)
					{
						context = OperationContext.Current;
					}
					TD.MessageReceivedFromTransport(eventTraceActivity, correlationId, TraceUtility.GetAnnotation(context));
				}
			}
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x001930FC File Offset: 0x001912FC
		internal static string GetAnnotation(OperationContext context)
		{
			object annotationFromHost;
			if (context != null && context.IncomingMessage != null && MessageState.Closed != context.IncomingMessage.State)
			{
				if (!context.IncomingMessageProperties.TryGetValue("TraceApplicationReference", out annotationFromHost))
				{
					annotationFromHost = AspNetEnvironment.Current.GetAnnotationFromHost(context.Host);
					context.IncomingMessageProperties.Add("TraceApplicationReference", annotationFromHost);
				}
			}
			else
			{
				annotationFromHost = AspNetEnvironment.Current.GetAnnotationFromHost(null);
			}
			return (string)annotationFromHost;
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x0019316C File Offset: 0x0019136C
		internal static void TransferFromTransport(Message message)
		{
			if (message != null && DiagnosticUtility.ShouldUseActivity)
			{
				Guid guid = Guid.Empty;
				if (TraceUtility.ShouldPropagateActivity)
				{
					guid = ActivityIdHeader.ExtractActivityId(message);
				}
				if (guid == Guid.Empty)
				{
					guid = Guid.NewGuid();
				}
				ServiceModelActivity serviceModelActivity = null;
				bool flag = true;
				if (ServiceModelActivity.Current != null)
				{
					if (ServiceModelActivity.Current.Id == guid || ServiceModelActivity.Current.ActivityType == ActivityType.ProcessAction)
					{
						serviceModelActivity = ServiceModelActivity.Current;
						flag = false;
					}
					else if (ServiceModelActivity.Current.PreviousActivity != null && ServiceModelActivity.Current.PreviousActivity.Id == guid)
					{
						serviceModelActivity = ServiceModelActivity.Current.PreviousActivity;
						flag = false;
					}
				}
				if (serviceModelActivity == null)
				{
					serviceModelActivity = ServiceModelActivity.CreateActivity(guid);
				}
				if (DiagnosticUtility.ShouldUseActivity && flag)
				{
					if (FxTrace.Trace != null)
					{
						FxTrace.Trace.TraceTransfer(guid);
					}
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessAction", new object[]
					{
						message.Headers.Action
					}), ActivityType.ProcessAction);
				}
				message.Properties["ActivityId"] = serviceModelActivity;
			}
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x00193270 File Offset: 0x00191470
		internal static void UpdateAsyncOperationContextWithActivity(object activity)
		{
			if (OperationContext.Current != null && activity != null)
			{
				OperationContext.Current.OutgoingMessageProperties["AsyncOperationActivity"] = activity;
			}
		}

		// Token: 0x06006BF2 RID: 27634 RVA: 0x00193294 File Offset: 0x00191494
		internal static object ExtractAsyncOperationContextActivity()
		{
			object result = null;
			if (OperationContext.Current != null && OperationContext.Current.OutgoingMessageProperties.TryGetValue("AsyncOperationActivity", out result))
			{
				OperationContext.Current.OutgoingMessageProperties.Remove("AsyncOperationActivity");
			}
			return result;
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x001932D8 File Offset: 0x001914D8
		internal static void UpdateAsyncOperationContextWithStartTime(EventTraceActivity eventTraceActivity, long startTime)
		{
			if (OperationContext.Current != null)
			{
				OperationContext.Current.OutgoingMessageProperties["AsyncOperationStartTime"] = new TraceUtility.EventTraceActivityTimeProperty(eventTraceActivity, startTime);
			}
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x001932FC File Offset: 0x001914FC
		internal static void ExtractAsyncOperationStartTime(out EventTraceActivity eventTraceActivity, out long startTime)
		{
			TraceUtility.EventTraceActivityTimeProperty eventTraceActivityTimeProperty = null;
			eventTraceActivity = null;
			startTime = 0L;
			if (OperationContext.Current != null && OperationContext.Current.OutgoingMessageProperties.TryGetValue<TraceUtility.EventTraceActivityTimeProperty>("AsyncOperationStartTime", out eventTraceActivityTimeProperty))
			{
				OperationContext.Current.OutgoingMessageProperties.Remove("AsyncOperationStartTime");
				eventTraceActivity = eventTraceActivityTimeProperty.EventTraceActivity;
				startTime = eventTraceActivityTimeProperty.StartTime;
			}
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x00193356 File Offset: 0x00191556
		internal static AsyncCallback WrapExecuteUserCodeAsyncCallback(AsyncCallback callback)
		{
			if (!DiagnosticUtility.ShouldUseActivity || callback == null)
			{
				return callback;
			}
			return new TraceUtility.ExecuteUserCodeAsync(callback).Callback;
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x00193370 File Offset: 0x00191570
		internal static string GetRemoteEndpointAddressPort(IPEndPoint iPEndPoint)
		{
			if (iPEndPoint != null)
			{
				try
				{
					return iPEndPoint.Address.ToString() + ":" + iPEndPoint.Port.ToString();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x001933CC File Offset: 0x001915CC
		internal static string GetRemoteEndpointAddressPort(RemoteEndpointMessageProperty remoteEndpointMessageProperty)
		{
			try
			{
				if (remoteEndpointMessageProperty != null)
				{
					return remoteEndpointMessageProperty.Address + ":" + remoteEndpointMessageProperty.Port.ToString();
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
			}
			return string.Empty;
		}

		// Token: 0x04003E89 RID: 16009
		private const string ActivityIdKey = "ActivityId";

		// Token: 0x04003E8A RID: 16010
		private const string AsyncOperationActivityKey = "AsyncOperationActivity";

		// Token: 0x04003E8B RID: 16011
		private const string AsyncOperationStartTimeKey = "AsyncOperationStartTime";

		// Token: 0x04003E8C RID: 16012
		private static bool shouldPropagateActivity;

		// Token: 0x04003E8D RID: 16013
		private static bool shouldPropagateActivityGlobal;

		// Token: 0x04003E8E RID: 16014
		private static bool activityTracing;

		// Token: 0x04003E8F RID: 16015
		private static bool messageFlowTracing;

		// Token: 0x04003E90 RID: 16016
		private static bool messageFlowTracingOnly;

		// Token: 0x04003E91 RID: 16017
		private static long messageNumber = 0L;

		// Token: 0x04003E92 RID: 16018
		private static Func<Action<AsyncCallback, IAsyncResult>> asyncCallbackGenerator;

		// Token: 0x04003E93 RID: 16019
		private static SortedList<int, string> traceCodes = new SortedList<int, string>(382)
		{
			{
				65537,
				"WmiPut"
			},
			{
				131073,
				"AppDomainUnload"
			},
			{
				131074,
				"EventLog"
			},
			{
				131075,
				"ThrowingException"
			},
			{
				131076,
				"TraceHandledException"
			},
			{
				131077,
				"UnhandledException"
			},
			{
				131078,
				"FailedToAddAnActivityIdHeader"
			},
			{
				131079,
				"FailedToReadAnActivityIdHeader"
			},
			{
				131080,
				"FilterNotMatchedNodeQuotaExceeded"
			},
			{
				131081,
				"MessageCountLimitExceeded"
			},
			{
				131082,
				"DiagnosticsFailedMessageTrace"
			},
			{
				131083,
				"MessageNotLoggedQuotaExceeded"
			},
			{
				131084,
				"TraceTruncatedQuotaExceeded"
			},
			{
				131085,
				"ActivityBoundary"
			},
			{
				196615,
				""
			},
			{
				262145,
				"ConnectionAbandoned"
			},
			{
				262146,
				"ConnectionPoolCloseException"
			},
			{
				262147,
				"ConnectionPoolIdleTimeoutReached"
			},
			{
				262148,
				"ConnectionPoolLeaseTimeoutReached"
			},
			{
				262149,
				"ConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached"
			},
			{
				262150,
				"ServerMaxPooledConnectionsQuotaReached"
			},
			{
				262151,
				"EndpointListenerClose"
			},
			{
				262152,
				"EndpointListenerOpen"
			},
			{
				262153,
				"HttpResponseReceived"
			},
			{
				262154,
				"HttpChannelConcurrentReceiveQuotaReached"
			},
			{
				262155,
				"HttpChannelMessageReceiveFailed"
			},
			{
				262156,
				"HttpChannelUnexpectedResponse"
			},
			{
				262157,
				"HttpChannelRequestAborted"
			},
			{
				262158,
				"HttpChannelResponseAborted"
			},
			{
				262159,
				"HttpsClientCertificateInvalid"
			},
			{
				262160,
				"HttpsClientCertificateNotPresent"
			},
			{
				262161,
				"NamedPipeChannelMessageReceiveFailed"
			},
			{
				262162,
				"NamedPipeChannelMessageReceived"
			},
			{
				262163,
				"MessageReceived"
			},
			{
				262164,
				"MessageSent"
			},
			{
				262165,
				"RequestChannelReplyReceived"
			},
			{
				262166,
				"TcpChannelMessageReceiveFailed"
			},
			{
				262167,
				"TcpChannelMessageReceived"
			},
			{
				262168,
				"ConnectToIPEndpoint"
			},
			{
				262169,
				"SocketConnectionCreate"
			},
			{
				262170,
				"SocketConnectionClose"
			},
			{
				262171,
				"SocketConnectionAbort"
			},
			{
				262172,
				"SocketConnectionAbortClose"
			},
			{
				262173,
				"PipeConnectionAbort"
			},
			{
				262174,
				"RequestContextAbort"
			},
			{
				262175,
				"ChannelCreated"
			},
			{
				262176,
				"ChannelDisposed"
			},
			{
				262177,
				"ListenerCreated"
			},
			{
				262178,
				"ListenerDisposed"
			},
			{
				262179,
				"PrematureDatagramEof"
			},
			{
				262180,
				"MaxPendingConnectionsReached"
			},
			{
				262181,
				"MaxAcceptedChannelsReached"
			},
			{
				262182,
				"ChannelConnectionDropped"
			},
			{
				262183,
				"HttpAuthFailed"
			},
			{
				262184,
				"NoExistingTransportManager"
			},
			{
				262185,
				"IncompatibleExistingTransportManager"
			},
			{
				262186,
				"InitiatingNamedPipeConnection"
			},
			{
				262187,
				"InitiatingTcpConnection"
			},
			{
				262188,
				"OpenedListener"
			},
			{
				262189,
				"SslClientCertMissing"
			},
			{
				262190,
				"StreamSecurityUpgradeAccepted"
			},
			{
				262191,
				"TcpConnectError"
			},
			{
				262192,
				"FailedAcceptFromPool"
			},
			{
				262193,
				"FailedPipeConnect"
			},
			{
				262194,
				"SystemTimeResolution"
			},
			{
				262195,
				"PeerNeighborCloseFailed"
			},
			{
				262196,
				"PeerNeighborClosingFailed"
			},
			{
				262197,
				"PeerNeighborNotAccepted"
			},
			{
				262198,
				"PeerNeighborNotFound"
			},
			{
				262199,
				"PeerNeighborOpenFailed"
			},
			{
				262200,
				"PeerNeighborStateChanged"
			},
			{
				262201,
				"PeerNeighborStateChangeFailed"
			},
			{
				262202,
				"PeerNeighborMessageReceived"
			},
			{
				262203,
				"PeerNeighborManagerOffline"
			},
			{
				262204,
				"PeerNeighborManagerOnline"
			},
			{
				262205,
				"PeerChannelMessageReceived"
			},
			{
				262206,
				"PeerChannelMessageSent"
			},
			{
				262207,
				"PeerNodeAddressChanged"
			},
			{
				262208,
				"PeerNodeOpening"
			},
			{
				262209,
				"PeerNodeOpened"
			},
			{
				262210,
				"PeerNodeOpenFailed"
			},
			{
				262211,
				"PeerNodeClosing"
			},
			{
				262212,
				"PeerNodeClosed"
			},
			{
				262213,
				"PeerFloodedMessageReceived"
			},
			{
				262214,
				"PeerFloodedMessageNotPropagated"
			},
			{
				262215,
				"PeerFloodedMessageNotMatched"
			},
			{
				262216,
				"PnrpRegisteredAddresses"
			},
			{
				262217,
				"PnrpUnregisteredAddresses"
			},
			{
				262218,
				"PnrpResolvedAddresses"
			},
			{
				262219,
				"PnrpResolveException"
			},
			{
				262220,
				"PeerReceiveMessageAuthenticationFailure"
			},
			{
				262221,
				"PeerNodeAuthenticationFailure"
			},
			{
				262222,
				"PeerNodeAuthenticationTimeout"
			},
			{
				262223,
				"PeerFlooderReceiveMessageQuotaExceeded"
			},
			{
				262224,
				"PeerServiceOpened"
			},
			{
				262225,
				"PeerMaintainerActivity"
			},
			{
				262226,
				"MsmqCannotPeekOnQueue"
			},
			{
				262227,
				"MsmqCannotReadQueues"
			},
			{
				262228,
				"MsmqDatagramSent"
			},
			{
				262229,
				"MsmqDatagramReceived"
			},
			{
				262230,
				"MsmqDetected"
			},
			{
				262231,
				"MsmqEnteredBatch"
			},
			{
				262232,
				"MsmqExpectedException"
			},
			{
				262233,
				"MsmqFoundBaseAddress"
			},
			{
				262234,
				"MsmqLeftBatch"
			},
			{
				262235,
				"MsmqMatchedApplicationFound"
			},
			{
				262236,
				"MsmqMessageDropped"
			},
			{
				262237,
				"MsmqMessageLockedUnderTheTransaction"
			},
			{
				262238,
				"MsmqMessageRejected"
			},
			{
				262239,
				"MsmqMoveOrDeleteAttemptFailed"
			},
			{
				262240,
				"MsmqPoisonMessageMovedPoison"
			},
			{
				262241,
				"MsmqPoisonMessageMovedRetry"
			},
			{
				262242,
				"MsmqPoisonMessageRejected"
			},
			{
				262243,
				"MsmqPoolFull"
			},
			{
				262244,
				"MsmqPotentiallyPoisonMessageDetected"
			},
			{
				262245,
				"MsmqQueueClosed"
			},
			{
				262246,
				"MsmqQueueOpened"
			},
			{
				262247,
				"MsmqQueueTransactionalStatusUnknown"
			},
			{
				262248,
				"MsmqScanStarted"
			},
			{
				262249,
				"MsmqSessiongramReceived"
			},
			{
				262250,
				"MsmqSessiongramSent"
			},
			{
				262251,
				"MsmqStartingApplication"
			},
			{
				262252,
				"MsmqStartingService"
			},
			{
				262253,
				"MsmqUnexpectedAcknowledgment"
			},
			{
				262254,
				"WsrmNegativeElapsedTimeDetected"
			},
			{
				262255,
				"TcpTransferError"
			},
			{
				262256,
				"TcpConnectionResetError"
			},
			{
				262257,
				"TcpConnectionTimedOut"
			},
			{
				327681,
				"ComIntegrationServiceHostStartingService"
			},
			{
				327682,
				"ComIntegrationServiceHostStartedService"
			},
			{
				327683,
				"ComIntegrationServiceHostCreatedServiceContract"
			},
			{
				327684,
				"ComIntegrationServiceHostStartedServiceDetails"
			},
			{
				327685,
				"ComIntegrationServiceHostCreatedServiceEndpoint"
			},
			{
				327686,
				"ComIntegrationServiceHostStoppingService"
			},
			{
				327687,
				"ComIntegrationServiceHostStoppedService"
			},
			{
				327688,
				"ComIntegrationDllHostInitializerStarting"
			},
			{
				327689,
				"ComIntegrationDllHostInitializerAddingHost"
			},
			{
				327690,
				"ComIntegrationDllHostInitializerStarted"
			},
			{
				327691,
				"ComIntegrationDllHostInitializerStopping"
			},
			{
				327692,
				"ComIntegrationDllHostInitializerStopped"
			},
			{
				327693,
				"ComIntegrationTLBImportStarting"
			},
			{
				327694,
				"ComIntegrationTLBImportFromAssembly"
			},
			{
				327695,
				"ComIntegrationTLBImportFromTypelib"
			},
			{
				327696,
				"ComIntegrationTLBImportConverterEvent"
			},
			{
				327697,
				"ComIntegrationTLBImportFinished"
			},
			{
				327698,
				"ComIntegrationInstanceCreationRequest"
			},
			{
				327699,
				"ComIntegrationInstanceCreationSuccess"
			},
			{
				327700,
				"ComIntegrationInstanceReleased"
			},
			{
				327701,
				"ComIntegrationEnteringActivity"
			},
			{
				327702,
				"ComIntegrationExecutingCall"
			},
			{
				327703,
				"ComIntegrationLeftActivity"
			},
			{
				327704,
				"ComIntegrationInvokingMethod"
			},
			{
				327705,
				"ComIntegrationInvokedMethod"
			},
			{
				327706,
				"ComIntegrationInvokingMethodNewTransaction"
			},
			{
				327707,
				"ComIntegrationInvokingMethodContextTransaction"
			},
			{
				327708,
				"ComIntegrationServiceMonikerParsed"
			},
			{
				327709,
				"ComIntegrationWsdlChannelBuilderLoaded"
			},
			{
				327710,
				"ComIntegrationTypedChannelBuilderLoaded"
			},
			{
				327711,
				"ComIntegrationChannelCreated"
			},
			{
				327712,
				"ComIntegrationDispatchMethod"
			},
			{
				327713,
				"ComIntegrationTxProxyTxCommitted"
			},
			{
				327714,
				"ComIntegrationTxProxyTxAbortedByContext"
			},
			{
				327715,
				"ComIntegrationTxProxyTxAbortedByTM"
			},
			{
				327716,
				"ComIntegrationMexMonikerMetadataExchangeComplete"
			},
			{
				327717,
				"ComIntegrationMexChannelBuilderLoaded"
			},
			{
				458752,
				"Security"
			},
			{
				458753,
				"SecurityIdentityVerificationSuccess"
			},
			{
				458754,
				"SecurityIdentityVerificationFailure"
			},
			{
				458755,
				"SecurityIdentityDeterminationSuccess"
			},
			{
				458756,
				"SecurityIdentityDeterminationFailure"
			},
			{
				458757,
				"SecurityIdentityHostNameNormalizationFailure"
			},
			{
				458758,
				"SecurityImpersonationSuccess"
			},
			{
				458759,
				"SecurityImpersonationFailure"
			},
			{
				458760,
				"SecurityNegotiationProcessingFailure"
			},
			{
				458761,
				"IssuanceTokenProviderRemovedCachedToken"
			},
			{
				458762,
				"IssuanceTokenProviderUsingCachedToken"
			},
			{
				458763,
				"IssuanceTokenProviderBeginSecurityNegotiation"
			},
			{
				458764,
				"IssuanceTokenProviderEndSecurityNegotiation"
			},
			{
				458765,
				"IssuanceTokenProviderRedirectApplied"
			},
			{
				458766,
				"IssuanceTokenProviderServiceTokenCacheFull"
			},
			{
				458767,
				"NegotiationTokenProviderAttached"
			},
			{
				458784,
				"SpnegoClientNegotiationCompleted"
			},
			{
				458785,
				"SpnegoServiceNegotiationCompleted"
			},
			{
				458786,
				"SpnegoClientNegotiation"
			},
			{
				458787,
				"SpnegoServiceNegotiation"
			},
			{
				458788,
				"NegotiationAuthenticatorAttached"
			},
			{
				458789,
				"ServiceSecurityNegotiationCompleted"
			},
			{
				458790,
				"SecurityContextTokenCacheFull"
			},
			{
				458791,
				"ExportSecurityChannelBindingEntry"
			},
			{
				458792,
				"ExportSecurityChannelBindingExit"
			},
			{
				458793,
				"ImportSecurityChannelBindingEntry"
			},
			{
				458794,
				"ImportSecurityChannelBindingExit"
			},
			{
				458795,
				"SecurityTokenProviderOpened"
			},
			{
				458796,
				"SecurityTokenProviderClosed"
			},
			{
				458797,
				"SecurityTokenAuthenticatorOpened"
			},
			{
				458798,
				"SecurityTokenAuthenticatorClosed"
			},
			{
				458799,
				"SecurityBindingOutgoingMessageSecured"
			},
			{
				458800,
				"SecurityBindingIncomingMessageVerified"
			},
			{
				458801,
				"SecurityBindingSecureOutgoingMessageFailure"
			},
			{
				458802,
				"SecurityBindingVerifyIncomingMessageFailure"
			},
			{
				458803,
				"SecuritySpnToSidMappingFailure"
			},
			{
				458804,
				"SecuritySessionRedirectApplied"
			},
			{
				458805,
				"SecurityClientSessionCloseSent"
			},
			{
				458806,
				"SecurityClientSessionCloseResponseSent"
			},
			{
				458807,
				"SecurityClientSessionCloseMessageReceived"
			},
			{
				458808,
				"SecuritySessionKeyRenewalFaultReceived"
			},
			{
				458809,
				"SecuritySessionAbortedFaultReceived"
			},
			{
				458810,
				"SecuritySessionClosedResponseReceived"
			},
			{
				458811,
				"SecurityClientSessionPreviousKeyDiscarded"
			},
			{
				458812,
				"SecurityClientSessionKeyRenewed"
			},
			{
				458813,
				"SecurityPendingServerSessionAdded"
			},
			{
				458814,
				"SecurityPendingServerSessionClosed"
			},
			{
				458815,
				"SecurityPendingServerSessionActivated"
			},
			{
				458816,
				"SecurityActiveServerSessionRemoved"
			},
			{
				458817,
				"SecurityNewServerSessionKeyIssued"
			},
			{
				458818,
				"SecurityInactiveSessionFaulted"
			},
			{
				458819,
				"SecurityServerSessionKeyUpdated"
			},
			{
				458820,
				"SecurityServerSessionCloseReceived"
			},
			{
				458821,
				"SecurityServerSessionRenewalFaultSent"
			},
			{
				458822,
				"SecurityServerSessionAbortedFaultSent"
			},
			{
				458823,
				"SecuritySessionCloseResponseSent"
			},
			{
				458824,
				"SecuritySessionServerCloseSent"
			},
			{
				458825,
				"SecurityServerSessionCloseResponseReceived"
			},
			{
				458826,
				"SecuritySessionRenewFaultSendFailure"
			},
			{
				458827,
				"SecuritySessionAbortedFaultSendFailure"
			},
			{
				458828,
				"SecuritySessionClosedResponseSendFailure"
			},
			{
				458829,
				"SecuritySessionServerCloseSendFailure"
			},
			{
				458830,
				"SecuritySessionRequestorStartOperation"
			},
			{
				458831,
				"SecuritySessionRequestorOperationSuccess"
			},
			{
				458832,
				"SecuritySessionRequestorOperationFailure"
			},
			{
				458833,
				"SecuritySessionResponderOperationFailure"
			},
			{
				458834,
				"SecuritySessionDemuxFailure"
			},
			{
				458835,
				"SecurityAuditWrittenSuccess"
			},
			{
				458836,
				"SecurityAuditWrittenFailure"
			},
			{
				524289,
				"AsyncCallbackThrewException"
			},
			{
				524290,
				"CommunicationObjectAborted"
			},
			{
				524291,
				"CommunicationObjectAbortFailed"
			},
			{
				524292,
				"CommunicationObjectCloseFailed"
			},
			{
				524293,
				"CommunicationObjectOpenFailed"
			},
			{
				524294,
				"CommunicationObjectClosing"
			},
			{
				524295,
				"CommunicationObjectClosed"
			},
			{
				524296,
				"CommunicationObjectCreated"
			},
			{
				524297,
				"CommunicationObjectDisposing"
			},
			{
				524298,
				"CommunicationObjectFaultReason"
			},
			{
				524299,
				"CommunicationObjectFaulted"
			},
			{
				524300,
				"CommunicationObjectOpening"
			},
			{
				524301,
				"CommunicationObjectOpened"
			},
			{
				524302,
				"DidNotUnderstandMessageHeader"
			},
			{
				524303,
				"UnderstoodMessageHeader"
			},
			{
				524304,
				"MessageClosed"
			},
			{
				524305,
				"MessageClosedAgain"
			},
			{
				524306,
				"MessageCopied"
			},
			{
				524307,
				"MessageRead"
			},
			{
				524308,
				"MessageWritten"
			},
			{
				524309,
				"BeginExecuteMethod"
			},
			{
				524310,
				"ConfigurationIsReadOnly"
			},
			{
				524311,
				"ConfiguredExtensionTypeNotFound"
			},
			{
				524312,
				"EvaluationContextNotFound"
			},
			{
				524313,
				"EndExecuteMethod"
			},
			{
				524314,
				"ExtensionCollectionDoesNotExist"
			},
			{
				524315,
				"ExtensionCollectionNameNotFound"
			},
			{
				524316,
				"ExtensionCollectionIsEmpty"
			},
			{
				524317,
				"ExtensionElementAlreadyExistsInCollection"
			},
			{
				524318,
				"ElementTypeDoesntMatchConfiguredType"
			},
			{
				524319,
				"ErrorInvokingUserCode"
			},
			{
				524320,
				"GetBehaviorElement"
			},
			{
				524321,
				"GetCommonBehaviors"
			},
			{
				524322,
				"GetConfiguredBinding"
			},
			{
				524323,
				"GetChannelEndpointElement"
			},
			{
				524324,
				"GetConfigurationSection"
			},
			{
				524325,
				"GetDefaultConfiguredBinding"
			},
			{
				524326,
				"GetServiceElement"
			},
			{
				524327,
				"MessageProcessingPaused"
			},
			{
				524328,
				"ManualFlowThrottleLimitReached"
			},
			{
				524329,
				"OverridingDuplicateConfigurationKey"
			},
			{
				524330,
				"RemoveBehavior"
			},
			{
				524331,
				"ServiceChannelLifetime"
			},
			{
				524332,
				"ServiceHostCreation"
			},
			{
				524333,
				"ServiceHostBaseAddresses"
			},
			{
				524334,
				"ServiceHostTimeoutOnClose"
			},
			{
				524335,
				"ServiceHostFaulted"
			},
			{
				524336,
				"ServiceHostErrorOnReleasePerformanceCounter"
			},
			{
				524337,
				"ServiceThrottleLimitReached"
			},
			{
				524338,
				"ServiceOperationMissingReply"
			},
			{
				524339,
				"ServiceOperationMissingReplyContext"
			},
			{
				524340,
				"ServiceOperationExceptionOnReply"
			},
			{
				524341,
				"SkipBehavior"
			},
			{
				524342,
				"TransportListen"
			},
			{
				524343,
				"UnhandledAction"
			},
			{
				524344,
				"PerformanceCounterFailedToLoad"
			},
			{
				524345,
				"PerformanceCountersFailed"
			},
			{
				524346,
				"PerformanceCountersFailedDuringUpdate"
			},
			{
				524347,
				"PerformanceCountersFailedForService"
			},
			{
				524348,
				"PerformanceCountersFailedOnRelease"
			},
			{
				524349,
				"WsmexNonCriticalWsdlExportError"
			},
			{
				524350,
				"WsmexNonCriticalWsdlImportError"
			},
			{
				524351,
				"FailedToOpenIncomingChannel"
			},
			{
				524352,
				"UnhandledExceptionInUserOperation"
			},
			{
				524353,
				"DroppedAMessage"
			},
			{
				524354,
				"CannotBeImportedInCurrentFormat"
			},
			{
				524355,
				"GetConfiguredEndpoint"
			},
			{
				524356,
				"GetDefaultConfiguredEndpoint"
			},
			{
				524357,
				"ExtensionTypeNotFound"
			},
			{
				524358,
				"DefaultEndpointsAdded"
			},
			{
				524379,
				"MetadataExchangeClientSendRequest"
			},
			{
				524380,
				"MetadataExchangeClientReceiveReply"
			},
			{
				524381,
				"WarnHelpPageEnabledNoBaseAddress"
			},
			{
				524382,
				"WarnServiceHealthEnabledNoBaseAddress"
			},
			{
				655361,
				"PortSharingClosed"
			},
			{
				655362,
				"PortSharingDuplicatedPipe"
			},
			{
				655363,
				"PortSharingDupHandleGranted"
			},
			{
				655364,
				"PortSharingDuplicatedSocket"
			},
			{
				655365,
				"PortSharingListening"
			},
			{
				655374,
				"SharedManagerServiceEndpointNotExist"
			},
			{
				917505,
				"TxSourceTxScopeRequiredIsTransactedTransport"
			},
			{
				917506,
				"TxSourceTxScopeRequiredIsTransactionFlow"
			},
			{
				917507,
				"TxSourceTxScopeRequiredIsAttachedTransaction"
			},
			{
				917508,
				"TxSourceTxScopeRequiredIsCreateNewTransaction"
			},
			{
				917509,
				"TxCompletionStatusCompletedForAutocomplete"
			},
			{
				917510,
				"TxCompletionStatusCompletedForError"
			},
			{
				917511,
				"TxCompletionStatusCompletedForSetComplete"
			},
			{
				917512,
				"TxCompletionStatusCompletedForTACOSC"
			},
			{
				917513,
				"TxCompletionStatusCompletedForAsyncAbort"
			},
			{
				917514,
				"TxCompletionStatusRemainsAttached"
			},
			{
				917515,
				"TxCompletionStatusAbortedOnSessionClose"
			},
			{
				917516,
				"TxReleaseServiceInstanceOnCompletion"
			},
			{
				917517,
				"TxAsyncAbort"
			},
			{
				917518,
				"TxFailedToNegotiateOleTx"
			},
			{
				917519,
				"TxSourceTxScopeRequiredUsingExistingTransaction"
			},
			{
				983040,
				"ActivatingMessageReceived"
			},
			{
				983041,
				"InstanceContextBoundToDurableInstance"
			},
			{
				983042,
				"InstanceContextDetachedFromDurableInstance"
			},
			{
				983043,
				"ContextChannelFactoryChannelCreated"
			},
			{
				983044,
				"ContextChannelListenerChannelAccepted"
			},
			{
				983045,
				"ContextProtocolContextAddedToMessage"
			},
			{
				983046,
				"ContextProtocolContextRetrievedFromMessage"
			},
			{
				983047,
				"DICPInstanceContextCached"
			},
			{
				983048,
				"DICPInstanceContextRemovedFromCache"
			},
			{
				983049,
				"ServiceDurableInstanceDeleted"
			},
			{
				983050,
				"ServiceDurableInstanceDisposed"
			},
			{
				983051,
				"ServiceDurableInstanceLoaded"
			},
			{
				983052,
				"ServiceDurableInstanceSaved"
			},
			{
				983053,
				"SqlPersistenceProviderSQLCallStart"
			},
			{
				983054,
				"SqlPersistenceProviderSQLCallEnd"
			},
			{
				983055,
				"SqlPersistenceProviderOpenParameters"
			},
			{
				983056,
				"SyncContextSchedulerServiceTimerCancelled"
			},
			{
				983057,
				"SyncContextSchedulerServiceTimerCreated"
			},
			{
				983058,
				"WorkflowDurableInstanceLoaded"
			},
			{
				983059,
				"WorkflowDurableInstanceAborted"
			},
			{
				983060,
				"WorkflowDurableInstanceActivated"
			},
			{
				983061,
				"WorkflowOperationInvokerItemQueued"
			},
			{
				983062,
				"WorkflowRequestContextReplySent"
			},
			{
				983063,
				"WorkflowRequestContextFaultSent"
			},
			{
				983064,
				"WorkflowServiceHostCreated"
			},
			{
				983065,
				"SyndicationReadFeedBegin"
			},
			{
				983066,
				"SyndicationReadFeedEnd"
			},
			{
				983067,
				"SyndicationReadItemBegin"
			},
			{
				983068,
				"SyndicationReadItemEnd"
			},
			{
				983069,
				"SyndicationWriteFeedBegin"
			},
			{
				983070,
				"SyndicationWriteFeedEnd"
			},
			{
				983071,
				"SyndicationWriteItemBegin"
			},
			{
				983072,
				"SyndicationWriteItemEnd"
			},
			{
				983073,
				"SyndicationProtocolElementIgnoredOnRead"
			},
			{
				983074,
				"SyndicationProtocolElementIgnoredOnWrite"
			},
			{
				983075,
				"SyndicationProtocolElementInvalid"
			},
			{
				983076,
				"WebUnknownQueryParameterIgnored"
			},
			{
				983077,
				"WebRequestMatchesOperation"
			},
			{
				983078,
				"WebRequestDoesNotMatchOperations"
			},
			{
				983079,
				"WebRequestRedirect"
			},
			{
				983080,
				"SyndicationReadServiceDocumentBegin"
			},
			{
				983081,
				"SyndicationReadServiceDocumentEnd"
			},
			{
				983082,
				"SyndicationReadCategoriesDocumentBegin"
			},
			{
				983083,
				"SyndicationReadCategoriesDocumentEnd"
			},
			{
				983084,
				"SyndicationWriteServiceDocumentBegin"
			},
			{
				983085,
				"SyndicationWriteServiceDocumentEnd"
			},
			{
				983086,
				"SyndicationWriteCategoriesDocumentBegin"
			},
			{
				983087,
				"SyndicationWriteCategoriesDocumentEnd"
			},
			{
				983088,
				"AutomaticFormatSelectedOperationDefault"
			},
			{
				983089,
				"AutomaticFormatSelectedRequestBased"
			},
			{
				983090,
				"RequestFormatSelectedFromContentTypeMapper"
			},
			{
				983091,
				"RequestFormatSelectedByEncoderDefaults"
			},
			{
				983092,
				"AddingResponseToOutputCache"
			},
			{
				983093,
				"AddingAuthenticatedResponseToOutputCache"
			},
			{
				983095,
				"JsonpCallbackNameSet"
			}
		};

		// Token: 0x04003E94 RID: 16020
		public const string E2EActivityId = "E2EActivityId";

		// Token: 0x04003E95 RID: 16021
		public const string TraceApplicationReference = "TraceApplicationReference";

		// Token: 0x02000EC4 RID: 3780
		internal class TracingAsyncCallbackState
		{
			// Token: 0x0600846D RID: 33901 RVA: 0x001E98A8 File Offset: 0x001E7AA8
			internal TracingAsyncCallbackState(object innerState)
			{
				this.innerState = innerState;
				this.activityId = DiagnosticTraceBase.ActivityId;
			}

			// Token: 0x17001D27 RID: 7463
			// (get) Token: 0x0600846E RID: 33902 RVA: 0x001E98C2 File Offset: 0x001E7AC2
			internal object InnerState
			{
				get
				{
					return this.innerState;
				}
			}

			// Token: 0x17001D28 RID: 7464
			// (get) Token: 0x0600846F RID: 33903 RVA: 0x001E98CA File Offset: 0x001E7ACA
			internal Guid ActivityId
			{
				get
				{
					return this.activityId;
				}
			}

			// Token: 0x04004CAD RID: 19629
			private object innerState;

			// Token: 0x04004CAE RID: 19630
			private Guid activityId;
		}

		// Token: 0x02000EC5 RID: 3781
		private sealed class ExecuteUserCodeAsync
		{
			// Token: 0x06008470 RID: 33904 RVA: 0x001E98D2 File Offset: 0x001E7AD2
			public ExecuteUserCodeAsync(AsyncCallback callback)
			{
				this.callback = callback;
			}

			// Token: 0x17001D29 RID: 7465
			// (get) Token: 0x06008471 RID: 33905 RVA: 0x001E98E1 File Offset: 0x001E7AE1
			public AsyncCallback Callback
			{
				get
				{
					return Fx.ThunkCallback(new AsyncCallback(this.ExecuteUserCode));
				}
			}

			// Token: 0x06008472 RID: 33906 RVA: 0x001E98F4 File Offset: 0x001E7AF4
			private void ExecuteUserCode(IAsyncResult result)
			{
				using (ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateBoundedActivity())
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityCallback"), ActivityType.ExecuteUserCode);
					this.callback(result);
				}
			}

			// Token: 0x04004CAF RID: 19631
			private AsyncCallback callback;
		}

		// Token: 0x02000EC6 RID: 3782
		private class EventTraceActivityTimeProperty
		{
			// Token: 0x06008473 RID: 33907 RVA: 0x001E9940 File Offset: 0x001E7B40
			public EventTraceActivityTimeProperty(EventTraceActivity eventTraceActivity, long startTime)
			{
				this.eventTraceActivity = eventTraceActivity;
				this.startTime = startTime;
			}

			// Token: 0x17001D2A RID: 7466
			// (get) Token: 0x06008474 RID: 33908 RVA: 0x001E9956 File Offset: 0x001E7B56
			internal long StartTime
			{
				get
				{
					return this.startTime;
				}
			}

			// Token: 0x17001D2B RID: 7467
			// (get) Token: 0x06008475 RID: 33909 RVA: 0x001E995E File Offset: 0x001E7B5E
			internal EventTraceActivity EventTraceActivity
			{
				get
				{
					return this.eventTraceActivity;
				}
			}

			// Token: 0x04004CB0 RID: 19632
			private long startTime;

			// Token: 0x04004CB1 RID: 19633
			private EventTraceActivity eventTraceActivity;
		}
	}
}
