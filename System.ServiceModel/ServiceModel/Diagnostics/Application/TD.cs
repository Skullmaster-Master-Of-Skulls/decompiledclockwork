using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.ServiceModel.Diagnostics.Application
{
	// Token: 0x02000AAF RID: 2735
	internal class TD
	{
		// Token: 0x06006C20 RID: 27680 RVA: 0x00193D1C File Offset: 0x00191F1C
		private TD()
		{
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x00193D24 File Offset: 0x00191F24
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TD.resourceManager == null)
				{
					TD.resourceManager = new ResourceManager("System.ServiceModel.Diagnostics.Application.TD", typeof(TD).Assembly);
				}
				return TD.resourceManager;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x00193D50 File Offset: 0x00191F50
		// (set) Token: 0x06006C23 RID: 27683 RVA: 0x00193D57 File Offset: 0x00191F57
		internal static CultureInfo Culture
		{
			get
			{
				return TD.resourceCulture;
			}
			set
			{
				TD.resourceCulture = value;
			}
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x00193D5F File Offset: 0x00191F5F
		internal static bool ClientOperationPreparedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(0);
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x00193D70 File Offset: 0x00191F70
		internal static void ClientOperationPrepared(EventTraceActivity eventTraceActivity, string Action, string ContractName, string Destination, Guid relatedActivityId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(0))
			{
				TD.WriteEtwTransferEvent(0, eventTraceActivity, relatedActivityId, Action, ContractName, Destination, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x00193DAF File Offset: 0x00191FAF
		internal static bool ClientMessageInspectorAfterReceiveInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(1);
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x00193DC0 File Offset: 0x00191FC0
		internal static void ClientMessageInspectorAfterReceiveInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(1))
			{
				TD.WriteEtwEvent(1, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x00193DFB File Offset: 0x00191FFB
		internal static bool ClientMessageInspectorBeforeSendInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(2);
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x00193E0C File Offset: 0x0019200C
		internal static void ClientMessageInspectorBeforeSendInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(2))
			{
				TD.WriteEtwEvent(2, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C2A RID: 27690 RVA: 0x00193E47 File Offset: 0x00192047
		internal static bool ClientParameterInspectorAfterCallInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(3);
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x00193E58 File Offset: 0x00192058
		internal static void ClientParameterInspectorAfterCallInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(3))
			{
				TD.WriteEtwEvent(3, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x00193E93 File Offset: 0x00192093
		internal static bool ClientParameterInspectorBeforeCallInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(4);
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x00193EA4 File Offset: 0x001920A4
		internal static void ClientParameterInspectorBeforeCallInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(4))
			{
				TD.WriteEtwEvent(4, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x00193EDF File Offset: 0x001920DF
		internal static bool OperationInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(5);
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x00193EF0 File Offset: 0x001920F0
		internal static void OperationInvoked(EventTraceActivity eventTraceActivity, string MethodName, string CallerInfo)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(5))
			{
				TD.WriteEtwEvent(5, eventTraceActivity, MethodName, CallerInfo, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x00193F2C File Offset: 0x0019212C
		internal static bool ErrorHandlerInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(6);
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x00193F40 File Offset: 0x00192140
		internal static void ErrorHandlerInvoked(string TypeName, bool Handled, string ExceptionTypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(6))
			{
				TD.WriteEtwEvent(6, null, TypeName, Handled, ExceptionTypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x00193F7D File Offset: 0x0019217D
		internal static bool FaultProviderInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(7);
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x00193F90 File Offset: 0x00192190
		internal static void FaultProviderInvoked(string TypeName, string ExceptionTypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(7))
			{
				TD.WriteEtwEvent(7, null, TypeName, ExceptionTypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x00193FCC File Offset: 0x001921CC
		internal static bool MessageInspectorAfterReceiveInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(8);
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x00193FE0 File Offset: 0x001921E0
		internal static void MessageInspectorAfterReceiveInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(8))
			{
				TD.WriteEtwEvent(8, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C36 RID: 27702 RVA: 0x0019401B File Offset: 0x0019221B
		internal static bool MessageInspectorBeforeSendInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(9);
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x00194030 File Offset: 0x00192230
		internal static void MessageInspectorBeforeSendInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(9))
			{
				TD.WriteEtwEvent(9, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x0019406D File Offset: 0x0019226D
		internal static bool MessageThrottleExceededIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(10);
		}

		// Token: 0x06006C39 RID: 27705 RVA: 0x00194080 File Offset: 0x00192280
		internal static void MessageThrottleExceeded(string ThrottleName, long Limit)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(10))
			{
				TD.WriteEtwEvent(10, null, ThrottleName, Limit, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C3A RID: 27706 RVA: 0x001940BE File Offset: 0x001922BE
		internal static bool ParameterInspectorAfterCallInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(11);
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x001940D0 File Offset: 0x001922D0
		internal static void ParameterInspectorAfterCallInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(11))
			{
				TD.WriteEtwEvent(11, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x0019410D File Offset: 0x0019230D
		internal static bool ParameterInspectorBeforeCallInvokedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(12);
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x00194120 File Offset: 0x00192320
		internal static void ParameterInspectorBeforeCallInvoked(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(12))
			{
				TD.WriteEtwEvent(12, eventTraceActivity, TypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x0019415D File Offset: 0x0019235D
		internal static bool OperationCompletedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(13);
		}

		// Token: 0x06006C3F RID: 27711 RVA: 0x00194170 File Offset: 0x00192370
		internal static void OperationCompleted(EventTraceActivity eventTraceActivity, string MethodName, long Duration)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(13))
			{
				TD.WriteEtwEvent(13, eventTraceActivity, MethodName, Duration, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C40 RID: 27712 RVA: 0x001941AE File Offset: 0x001923AE
		internal static bool MessageReceivedByTransportIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(14);
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x001941C0 File Offset: 0x001923C0
		internal static void MessageReceivedByTransport(EventTraceActivity eventTraceActivity, string ListenAddress, Guid relatedActivityId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(14))
			{
				TD.WriteEtwTransferEvent(14, eventTraceActivity, relatedActivityId, ListenAddress, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x001941FE File Offset: 0x001923FE
		internal static bool MessageSentByTransportIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(15);
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x00194210 File Offset: 0x00192410
		internal static void MessageSentByTransport(EventTraceActivity eventTraceActivity, string DestinationAddress)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(15))
			{
				TD.WriteEtwEvent(15, eventTraceActivity, DestinationAddress, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x0019424D File Offset: 0x0019244D
		internal static bool MessageLogInfoIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(16);
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x00194260 File Offset: 0x00192460
		internal static bool MessageLogInfo(string param0)
		{
			bool result = true;
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(16))
			{
				result = TD.WriteEtwEvent(16, null, param0, serializedPayload.AppDomainFriendlyName);
			}
			return result;
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x00194298 File Offset: 0x00192498
		internal static bool MessageLogWarningIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(17);
		}

		// Token: 0x06006C47 RID: 27719 RVA: 0x001942AC File Offset: 0x001924AC
		internal static bool MessageLogWarning(string param0)
		{
			bool result = true;
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(17))
			{
				result = TD.WriteEtwEvent(17, null, param0, serializedPayload.AppDomainFriendlyName);
			}
			return result;
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x001942E4 File Offset: 0x001924E4
		internal static bool MessageLogEventSizeExceededIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(18);
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x001942F8 File Offset: 0x001924F8
		internal static void MessageLogEventSizeExceeded()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(18))
			{
				TD.WriteEtwEvent(18, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x0019432C File Offset: 0x0019252C
		internal static bool ResumeSignpostEventIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(19);
		}

		// Token: 0x06006C4B RID: 27723 RVA: 0x00194340 File Offset: 0x00192540
		internal static void ResumeSignpostEvent(TraceRecord traceRecord)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, traceRecord, null);
			if (TD.IsEtwEventEnabled(19))
			{
				TD.WriteEtwEvent(19, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C4C RID: 27724 RVA: 0x0019437B File Offset: 0x0019257B
		internal static bool StartSignpostEventIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(20);
		}

		// Token: 0x06006C4D RID: 27725 RVA: 0x00194390 File Offset: 0x00192590
		internal static void StartSignpostEvent(TraceRecord traceRecord)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, traceRecord, null);
			if (TD.IsEtwEventEnabled(20))
			{
				TD.WriteEtwEvent(20, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C4E RID: 27726 RVA: 0x001943CB File Offset: 0x001925CB
		internal static bool StopSignpostEventIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(21);
		}

		// Token: 0x06006C4F RID: 27727 RVA: 0x001943E0 File Offset: 0x001925E0
		internal static void StopSignpostEvent(TraceRecord traceRecord)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, traceRecord, null);
			if (TD.IsEtwEventEnabled(21))
			{
				TD.WriteEtwEvent(21, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C50 RID: 27728 RVA: 0x0019441B File Offset: 0x0019261B
		internal static bool SuspendSignpostEventIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(22);
		}

		// Token: 0x06006C51 RID: 27729 RVA: 0x00194430 File Offset: 0x00192630
		internal static void SuspendSignpostEvent(TraceRecord traceRecord)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, traceRecord, null);
			if (TD.IsEtwEventEnabled(22))
			{
				TD.WriteEtwEvent(22, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x0019446B File Offset: 0x0019266B
		internal static bool ServiceChannelCallStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(23);
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x00194480 File Offset: 0x00192680
		internal static void ServiceChannelCallStop(EventTraceActivity eventTraceActivity, string Action, string ContractName, string Destination)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(23))
			{
				TD.WriteEtwEvent(23, eventTraceActivity, Action, ContractName, Destination, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x001944BF File Offset: 0x001926BF
		internal static bool ServiceExceptionIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(24);
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x001944D4 File Offset: 0x001926D4
		internal static void ServiceException(EventTraceActivity eventTraceActivity, string ExceptionToString, string ExceptionTypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(24))
			{
				TD.WriteEtwEvent(24, eventTraceActivity, ExceptionToString, ExceptionTypeName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x00194512 File Offset: 0x00192712
		internal static bool OperationFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(25);
		}

		// Token: 0x06006C57 RID: 27735 RVA: 0x00194524 File Offset: 0x00192724
		internal static void OperationFailed(EventTraceActivity eventTraceActivity, string MethodName, long Duration)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(25))
			{
				TD.WriteEtwEvent(25, eventTraceActivity, MethodName, Duration, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C58 RID: 27736 RVA: 0x00194562 File Offset: 0x00192762
		internal static bool OperationFaultedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(26);
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x00194574 File Offset: 0x00192774
		internal static void OperationFaulted(EventTraceActivity eventTraceActivity, string MethodName, long Duration)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(26))
			{
				TD.WriteEtwEvent(26, eventTraceActivity, MethodName, Duration, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x001945B2 File Offset: 0x001927B2
		internal static bool MessageThrottleAtSeventyPercentIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(27);
		}

		// Token: 0x06006C5B RID: 27739 RVA: 0x001945C4 File Offset: 0x001927C4
		internal static void MessageThrottleAtSeventyPercent(string ThrottleName, long Limit)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(27))
			{
				TD.WriteEtwEvent(27, null, ThrottleName, Limit, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C5C RID: 27740 RVA: 0x00194602 File Offset: 0x00192802
		internal static bool MessageReceivedFromTransportIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(28);
		}

		// Token: 0x06006C5D RID: 27741 RVA: 0x00194614 File Offset: 0x00192814
		internal static void MessageReceivedFromTransport(EventTraceActivity eventTraceActivity, Guid CorrelationId, string reference)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(28))
			{
				TD.WriteEtwEvent(28, eventTraceActivity, CorrelationId, reference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x0019464A File Offset: 0x0019284A
		internal static bool MessageSentToTransportIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(29);
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x0019465C File Offset: 0x0019285C
		internal static void MessageSentToTransport(EventTraceActivity eventTraceActivity, Guid CorrelationId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(29))
			{
				TD.WriteEtwEvent(29, eventTraceActivity, CorrelationId, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x00194699 File Offset: 0x00192899
		internal static bool ServiceHostOpenStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(30);
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x001946AC File Offset: 0x001928AC
		internal static void ServiceHostOpenStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(30))
			{
				TD.WriteEtwEvent(30, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x001946E0 File Offset: 0x001928E0
		internal static bool ServiceHostOpenStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(31);
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x001946F4 File Offset: 0x001928F4
		internal static void ServiceHostOpenStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(31))
			{
				TD.WriteEtwEvent(31, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x00194728 File Offset: 0x00192928
		internal static bool ServiceChannelOpenStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(32);
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x0019473C File Offset: 0x0019293C
		internal static void ServiceChannelOpenStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(32))
			{
				TD.WriteEtwEvent(32, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x00194770 File Offset: 0x00192970
		internal static bool ServiceChannelOpenStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(33);
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x00194784 File Offset: 0x00192984
		internal static void ServiceChannelOpenStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(33))
			{
				TD.WriteEtwEvent(33, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x001947B8 File Offset: 0x001929B8
		internal static bool ServiceChannelCallStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(34);
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x001947CC File Offset: 0x001929CC
		internal static void ServiceChannelCallStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(34))
			{
				TD.WriteEtwEvent(34, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C6A RID: 27754 RVA: 0x00194800 File Offset: 0x00192A00
		internal static bool ServiceChannelBeginCallStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(35);
		}

		// Token: 0x06006C6B RID: 27755 RVA: 0x00194814 File Offset: 0x00192A14
		internal static void ServiceChannelBeginCallStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(35))
			{
				TD.WriteEtwEvent(35, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C6C RID: 27756 RVA: 0x00194848 File Offset: 0x00192A48
		internal static bool HttpSendMessageStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(36);
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x0019485C File Offset: 0x00192A5C
		internal static void HttpSendMessageStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(36))
			{
				TD.WriteEtwEvent(36, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x00194890 File Offset: 0x00192A90
		internal static bool HttpSendStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(37);
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x001948A4 File Offset: 0x00192AA4
		internal static void HttpSendStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(37))
			{
				TD.WriteEtwEvent(37, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x001948D8 File Offset: 0x00192AD8
		internal static bool HttpMessageReceiveStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(38);
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x001948EC File Offset: 0x00192AEC
		internal static void HttpMessageReceiveStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(38))
			{
				TD.WriteEtwEvent(38, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x00194920 File Offset: 0x00192B20
		internal static bool DispatchMessageStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(39);
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x00194934 File Offset: 0x00192B34
		internal static void DispatchMessageStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(39))
			{
				TD.WriteEtwEvent(39, eventTraceActivity, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x00194970 File Offset: 0x00192B70
		internal static bool HttpContextBeforeProcessAuthenticationIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(40);
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x00194984 File Offset: 0x00192B84
		internal static void HttpContextBeforeProcessAuthentication(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(40))
			{
				TD.WriteEtwEvent(40, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x001949B8 File Offset: 0x00192BB8
		internal static bool DispatchMessageBeforeAuthorizationIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(41);
		}

		// Token: 0x06006C77 RID: 27767 RVA: 0x001949CC File Offset: 0x00192BCC
		internal static void DispatchMessageBeforeAuthorization(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(41))
			{
				TD.WriteEtwEvent(41, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C78 RID: 27768 RVA: 0x00194A00 File Offset: 0x00192C00
		internal static bool DispatchMessageStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(42);
		}

		// Token: 0x06006C79 RID: 27769 RVA: 0x00194A14 File Offset: 0x00192C14
		internal static void DispatchMessageStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(42))
			{
				TD.WriteEtwEvent(42, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C7A RID: 27770 RVA: 0x00194A48 File Offset: 0x00192C48
		internal static bool ClientChannelOpenStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(43);
		}

		// Token: 0x06006C7B RID: 27771 RVA: 0x00194A5C File Offset: 0x00192C5C
		internal static void ClientChannelOpenStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(43))
			{
				TD.WriteEtwEvent(43, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C7C RID: 27772 RVA: 0x00194A90 File Offset: 0x00192C90
		internal static bool ClientChannelOpenStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(44);
		}

		// Token: 0x06006C7D RID: 27773 RVA: 0x00194AA4 File Offset: 0x00192CA4
		internal static void ClientChannelOpenStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(44))
			{
				TD.WriteEtwEvent(44, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C7E RID: 27774 RVA: 0x00194AD8 File Offset: 0x00192CD8
		internal static bool HttpSendStreamedMessageStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(45);
		}

		// Token: 0x06006C7F RID: 27775 RVA: 0x00194AEC File Offset: 0x00192CEC
		internal static void HttpSendStreamedMessageStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(45))
			{
				TD.WriteEtwEvent(45, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C80 RID: 27776 RVA: 0x00194B20 File Offset: 0x00192D20
		internal static bool ReceiveContextAbandonFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(46));
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00194B3C File Offset: 0x00192D3C
		internal static void ReceiveContextAbandonFailed(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(46))
			{
				TD.WriteEtwEvent(46, eventTraceActivity, TypeName, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("ReceiveContextAbandonFailed", TD.Culture), new object[]
				{
					TypeName
				});
				TD.WriteTraceSource(46, description, serializedPayload);
			}
		}

		// Token: 0x06006C82 RID: 27778 RVA: 0x00194BAA File Offset: 0x00192DAA
		internal static bool ReceiveContextAbandonWithExceptionIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(47));
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x00194BC8 File Offset: 0x00192DC8
		internal static void ReceiveContextAbandonWithException(EventTraceActivity eventTraceActivity, string TypeName, string ExceptionToString)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(47))
			{
				TD.WriteEtwEvent(47, eventTraceActivity, TypeName, ExceptionToString, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("ReceiveContextAbandonWithException", TD.Culture), new object[]
				{
					TypeName,
					ExceptionToString
				});
				TD.WriteTraceSource(47, description, serializedPayload);
			}
		}

		// Token: 0x06006C84 RID: 27780 RVA: 0x00194C3B File Offset: 0x00192E3B
		internal static bool ReceiveContextCompleteFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(48));
		}

		// Token: 0x06006C85 RID: 27781 RVA: 0x00194C58 File Offset: 0x00192E58
		internal static void ReceiveContextCompleteFailed(EventTraceActivity eventTraceActivity, string TypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(48))
			{
				TD.WriteEtwEvent(48, eventTraceActivity, TypeName, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("ReceiveContextCompleteFailed", TD.Culture), new object[]
				{
					TypeName
				});
				TD.WriteTraceSource(48, description, serializedPayload);
			}
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x00194CC6 File Offset: 0x00192EC6
		internal static bool ReceiveContextFaultedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(49));
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x00194CE4 File Offset: 0x00192EE4
		internal static void ReceiveContextFaulted(EventTraceActivity eventTraceActivity, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(49))
			{
				TD.WriteEtwEvent(49, eventTraceActivity, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("ReceiveContextFaulted", TD.Culture), new object[0]);
				TD.WriteTraceSource(49, description, serializedPayload);
			}
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x00194D54 File Offset: 0x00192F54
		internal static bool ClientBaseCachedChannelFactoryCountIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(50);
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x00194D68 File Offset: 0x00192F68
		internal static void ClientBaseCachedChannelFactoryCount(int Count, int MaxNum, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(50))
			{
				TD.WriteEtwEvent(50, null, Count, MaxNum, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x00194DA5 File Offset: 0x00192FA5
		internal static bool ClientBaseChannelFactoryAgedOutofCacheIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(51);
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x00194DB8 File Offset: 0x00192FB8
		internal static void ClientBaseChannelFactoryAgedOutofCache(int Count, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(51))
			{
				TD.WriteEtwEvent(51, null, Count, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x00194DF4 File Offset: 0x00192FF4
		internal static bool ClientBaseChannelFactoryCacheHitIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(52);
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x00194E08 File Offset: 0x00193008
		internal static void ClientBaseChannelFactoryCacheHit(object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(52))
			{
				TD.WriteEtwEvent(52, null, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x00194E43 File Offset: 0x00193043
		internal static bool ClientBaseUsingLocalChannelFactoryIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(53);
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x00194E58 File Offset: 0x00193058
		internal static void ClientBaseUsingLocalChannelFactory(object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(53))
			{
				TD.WriteEtwEvent(53, null, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x00194E93 File Offset: 0x00193093
		internal static bool QueryCompositionExecutedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(54);
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x00194EA8 File Offset: 0x001930A8
		internal static void QueryCompositionExecuted(EventTraceActivity eventTraceActivity, string TypeName, string Uri, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(54))
			{
				TD.WriteEtwEvent(54, eventTraceActivity, TypeName, Uri, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x00194EE5 File Offset: 0x001930E5
		internal static bool DispatchFailedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(55);
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x00194EF8 File Offset: 0x001930F8
		internal static void DispatchFailed(EventTraceActivity eventTraceActivity, string OperationName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(55))
			{
				TD.WriteEtwEvent(55, eventTraceActivity, OperationName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x00194F35 File Offset: 0x00193135
		internal static bool DispatchSuccessfulIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(56);
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x00194F48 File Offset: 0x00193148
		internal static void DispatchSuccessful(EventTraceActivity eventTraceActivity, string OperationName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(56))
			{
				TD.WriteEtwEvent(56, eventTraceActivity, OperationName, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x00194F85 File Offset: 0x00193185
		internal static bool MessageReadByEncoderIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(57);
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x00194F98 File Offset: 0x00193198
		internal static void MessageReadByEncoder(EventTraceActivity eventTraceActivity, int Size, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(57))
			{
				TD.WriteEtwEvent(57, eventTraceActivity, Size, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x00194FD4 File Offset: 0x001931D4
		internal static bool MessageWrittenByEncoderIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(58);
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x00194FE8 File Offset: 0x001931E8
		internal static void MessageWrittenByEncoder(EventTraceActivity eventTraceActivity, int Size, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(58))
			{
				TD.WriteEtwEvent(58, eventTraceActivity, Size, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x00195024 File Offset: 0x00193224
		internal static bool SessionIdleTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(59);
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x00195038 File Offset: 0x00193238
		internal static void SessionIdleTimeout(string RemoteAddress)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(59))
			{
				TD.WriteEtwEvent(59, null, RemoteAddress, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x0019506D File Offset: 0x0019326D
		internal static bool SocketAcceptEnqueuedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(60);
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x00195080 File Offset: 0x00193280
		internal static void SocketAcceptEnqueued(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(60))
			{
				TD.WriteEtwEvent(60, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x001950B4 File Offset: 0x001932B4
		internal static bool SocketAcceptedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(61);
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x001950C8 File Offset: 0x001932C8
		internal static void SocketAccepted(EventTraceActivity eventTraceActivity, int ListenerHashCode, int SocketHashCode)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(61))
			{
				TD.WriteEtwEvent(61, eventTraceActivity, ListenerHashCode, SocketHashCode, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x001950FE File Offset: 0x001932FE
		internal static bool ConnectionPoolMissIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(62);
		}

		// Token: 0x06006CA1 RID: 27809 RVA: 0x00195110 File Offset: 0x00193310
		internal static void ConnectionPoolMiss(string PoolKey, int busy)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(62))
			{
				TD.WriteEtwEvent(62, null, PoolKey, busy, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x00195146 File Offset: 0x00193346
		internal static bool DispatchFormatterDeserializeRequestStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(63);
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x00195158 File Offset: 0x00193358
		internal static void DispatchFormatterDeserializeRequestStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(63))
			{
				TD.WriteEtwEvent(63, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0019518C File Offset: 0x0019338C
		internal static bool DispatchFormatterDeserializeRequestStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(64);
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x001951A0 File Offset: 0x001933A0
		internal static void DispatchFormatterDeserializeRequestStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(64))
			{
				TD.WriteEtwEvent(64, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x001951D4 File Offset: 0x001933D4
		internal static bool DispatchFormatterSerializeReplyStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(65);
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x001951E8 File Offset: 0x001933E8
		internal static void DispatchFormatterSerializeReplyStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(65))
			{
				TD.WriteEtwEvent(65, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x0019521C File Offset: 0x0019341C
		internal static bool DispatchFormatterSerializeReplyStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(66);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x00195230 File Offset: 0x00193430
		internal static void DispatchFormatterSerializeReplyStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(66))
			{
				TD.WriteEtwEvent(66, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x00195264 File Offset: 0x00193464
		internal static bool ClientFormatterSerializeRequestStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(67);
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x00195278 File Offset: 0x00193478
		internal static void ClientFormatterSerializeRequestStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(67))
			{
				TD.WriteEtwEvent(67, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x001952AC File Offset: 0x001934AC
		internal static bool ClientFormatterSerializeRequestStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(68);
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x001952C0 File Offset: 0x001934C0
		internal static void ClientFormatterSerializeRequestStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(68))
			{
				TD.WriteEtwEvent(68, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x001952F4 File Offset: 0x001934F4
		internal static bool ClientFormatterDeserializeReplyStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(69);
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x00195308 File Offset: 0x00193508
		internal static void ClientFormatterDeserializeReplyStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(69))
			{
				TD.WriteEtwEvent(69, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x0019533C File Offset: 0x0019353C
		internal static bool ClientFormatterDeserializeReplyStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(70);
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x00195350 File Offset: 0x00193550
		internal static void ClientFormatterDeserializeReplyStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(70))
			{
				TD.WriteEtwEvent(70, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x00195384 File Offset: 0x00193584
		internal static bool SecurityNegotiationStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(71);
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x00195398 File Offset: 0x00193598
		internal static void SecurityNegotiationStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(71))
			{
				TD.WriteEtwEvent(71, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x001953CC File Offset: 0x001935CC
		internal static bool SecurityNegotiationStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(72);
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x001953E0 File Offset: 0x001935E0
		internal static void SecurityNegotiationStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(72))
			{
				TD.WriteEtwEvent(72, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x00195414 File Offset: 0x00193614
		internal static bool SecurityTokenProviderOpenedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(73);
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x00195428 File Offset: 0x00193628
		internal static void SecurityTokenProviderOpened(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(73))
			{
				TD.WriteEtwEvent(73, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x0019545C File Offset: 0x0019365C
		internal static bool OutgoingMessageSecuredIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(74);
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x00195470 File Offset: 0x00193670
		internal static void OutgoingMessageSecured(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(74))
			{
				TD.WriteEtwEvent(74, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x001954A4 File Offset: 0x001936A4
		internal static bool IncomingMessageVerifiedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(75);
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x001954B8 File Offset: 0x001936B8
		internal static void IncomingMessageVerified(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(75))
			{
				TD.WriteEtwEvent(75, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x001954EC File Offset: 0x001936EC
		internal static bool GetServiceInstanceStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(76);
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x00195500 File Offset: 0x00193700
		internal static void GetServiceInstanceStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(76))
			{
				TD.WriteEtwEvent(76, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00195534 File Offset: 0x00193734
		internal static bool GetServiceInstanceStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(77);
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x00195548 File Offset: 0x00193748
		internal static void GetServiceInstanceStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(77))
			{
				TD.WriteEtwEvent(77, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x0019557C File Offset: 0x0019377C
		internal static bool ChannelReceiveStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(78);
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x00195590 File Offset: 0x00193790
		internal static void ChannelReceiveStart(EventTraceActivity eventTraceActivity, int ChannelId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(78))
			{
				TD.WriteEtwEvent(78, eventTraceActivity, ChannelId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x001955C5 File Offset: 0x001937C5
		internal static bool ChannelReceiveStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(79);
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x001955D8 File Offset: 0x001937D8
		internal static void ChannelReceiveStop(EventTraceActivity eventTraceActivity, int ChannelId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(79))
			{
				TD.WriteEtwEvent(79, eventTraceActivity, ChannelId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x0019560D File Offset: 0x0019380D
		internal static bool ChannelFactoryCreatedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(80);
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x00195620 File Offset: 0x00193820
		internal static void ChannelFactoryCreated(object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(80))
			{
				TD.WriteEtwEvent(80, null, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x0019565B File Offset: 0x0019385B
		internal static bool PipeConnectionAcceptStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(81);
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x00195670 File Offset: 0x00193870
		internal static void PipeConnectionAcceptStart(EventTraceActivity eventTraceActivity, string uri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(81))
			{
				TD.WriteEtwEvent(81, eventTraceActivity, uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x001956A5 File Offset: 0x001938A5
		internal static bool PipeConnectionAcceptStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(82);
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x001956B8 File Offset: 0x001938B8
		internal static void PipeConnectionAcceptStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(82))
			{
				TD.WriteEtwEvent(82, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x001956EC File Offset: 0x001938EC
		internal static bool EstablishConnectionStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(83);
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x00195700 File Offset: 0x00193900
		internal static void EstablishConnectionStart(EventTraceActivity eventTraceActivity, string Key)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(83))
			{
				TD.WriteEtwEvent(83, eventTraceActivity, Key, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x00195735 File Offset: 0x00193935
		internal static bool EstablishConnectionStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(84);
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x00195748 File Offset: 0x00193948
		internal static void EstablishConnectionStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(84))
			{
				TD.WriteEtwEvent(84, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x0019577C File Offset: 0x0019397C
		internal static bool SessionPreambleUnderstoodIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(85);
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x00195790 File Offset: 0x00193990
		internal static void SessionPreambleUnderstood(string Via)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(85))
			{
				TD.WriteEtwEvent(85, null, Via, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x001957C5 File Offset: 0x001939C5
		internal static bool ConnectionReaderSendFaultIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(86);
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x001957D8 File Offset: 0x001939D8
		internal static void ConnectionReaderSendFault(string FaultString)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(86))
			{
				TD.WriteEtwEvent(86, null, FaultString, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x0019580D File Offset: 0x00193A0D
		internal static bool SocketAcceptClosedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(87);
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00195820 File Offset: 0x00193A20
		internal static void SocketAcceptClosed(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(87))
			{
				TD.WriteEtwEvent(87, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x00195854 File Offset: 0x00193A54
		internal static bool ServiceHostFaultedIsEnabled()
		{
			return FxTrace.ShouldTraceCritical && TD.IsEtwEventEnabled(88);
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x00195868 File Offset: 0x00193A68
		internal static void ServiceHostFaulted(EventTraceActivity eventTraceActivity, object source)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(source, null, null);
			if (TD.IsEtwEventEnabled(88))
			{
				TD.WriteEtwEvent(88, eventTraceActivity, serializedPayload.EventSource, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x001958A3 File Offset: 0x00193AA3
		internal static bool ListenerOpenStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(89);
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x001958B8 File Offset: 0x00193AB8
		internal static void ListenerOpenStart(EventTraceActivity eventTraceActivity, string Uri, Guid relatedActivityId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(89))
			{
				TD.WriteEtwTransferEvent(89, eventTraceActivity, relatedActivityId, Uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x001958EE File Offset: 0x00193AEE
		internal static bool ListenerOpenStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(90);
		}

		// Token: 0x06006CD9 RID: 27865 RVA: 0x00195900 File Offset: 0x00193B00
		internal static void ListenerOpenStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(90))
			{
				TD.WriteEtwEvent(90, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CDA RID: 27866 RVA: 0x00195934 File Offset: 0x00193B34
		internal static bool ServerMaxPooledConnectionsQuotaReachedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(91);
		}

		// Token: 0x06006CDB RID: 27867 RVA: 0x00195948 File Offset: 0x00193B48
		internal static void ServerMaxPooledConnectionsQuotaReached()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(91))
			{
				TD.WriteEtwEvent(91, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x0019597C File Offset: 0x00193B7C
		internal static bool TcpConnectionTimedOutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(92);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x00195990 File Offset: 0x00193B90
		internal static void TcpConnectionTimedOut(int SocketId, string Uri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(92))
			{
				TD.WriteEtwEvent(92, null, SocketId, Uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x001959C6 File Offset: 0x00193BC6
		internal static bool TcpConnectionResetErrorIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(93);
		}

		// Token: 0x06006CDF RID: 27871 RVA: 0x001959D8 File Offset: 0x00193BD8
		internal static void TcpConnectionResetError(int SocketId, string Uri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(93))
			{
				TD.WriteEtwEvent(93, null, SocketId, Uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CE0 RID: 27872 RVA: 0x00195A0E File Offset: 0x00193C0E
		internal static bool ServiceSecurityNegotiationCompletedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(94);
		}

		// Token: 0x06006CE1 RID: 27873 RVA: 0x00195A20 File Offset: 0x00193C20
		internal static void ServiceSecurityNegotiationCompleted(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(94))
			{
				TD.WriteEtwEvent(94, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x00195A54 File Offset: 0x00193C54
		internal static bool SecurityNegotiationProcessingFailureIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(95);
		}

		// Token: 0x06006CE3 RID: 27875 RVA: 0x00195A68 File Offset: 0x00193C68
		internal static void SecurityNegotiationProcessingFailure(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(95))
			{
				TD.WriteEtwEvent(95, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CE4 RID: 27876 RVA: 0x00195A9C File Offset: 0x00193C9C
		internal static bool SecurityIdentityVerificationSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(96);
		}

		// Token: 0x06006CE5 RID: 27877 RVA: 0x00195AB0 File Offset: 0x00193CB0
		internal static void SecurityIdentityVerificationSuccess(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(96))
			{
				TD.WriteEtwEvent(96, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CE6 RID: 27878 RVA: 0x00195AE4 File Offset: 0x00193CE4
		internal static bool SecurityIdentityVerificationFailureIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(97);
		}

		// Token: 0x06006CE7 RID: 27879 RVA: 0x00195AF8 File Offset: 0x00193CF8
		internal static void SecurityIdentityVerificationFailure(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(97))
			{
				TD.WriteEtwEvent(97, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CE8 RID: 27880 RVA: 0x00195B2C File Offset: 0x00193D2C
		internal static bool PortSharingDuplicatedSocketIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(98);
		}

		// Token: 0x06006CE9 RID: 27881 RVA: 0x00195B40 File Offset: 0x00193D40
		internal static void PortSharingDuplicatedSocket(EventTraceActivity eventTraceActivity, string Uri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(98))
			{
				TD.WriteEtwEvent(98, eventTraceActivity, Uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CEA RID: 27882 RVA: 0x00195B75 File Offset: 0x00193D75
		internal static bool SecurityImpersonationSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(99);
		}

		// Token: 0x06006CEB RID: 27883 RVA: 0x00195B88 File Offset: 0x00193D88
		internal static void SecurityImpersonationSuccess(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(99))
			{
				TD.WriteEtwEvent(99, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CEC RID: 27884 RVA: 0x00195BBC File Offset: 0x00193DBC
		internal static bool SecurityImpersonationFailureIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(100);
		}

		// Token: 0x06006CED RID: 27885 RVA: 0x00195BD0 File Offset: 0x00193DD0
		internal static void SecurityImpersonationFailure(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(100))
			{
				TD.WriteEtwEvent(100, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CEE RID: 27886 RVA: 0x00195C04 File Offset: 0x00193E04
		internal static bool HttpChannelRequestAbortedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(101);
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x00195C18 File Offset: 0x00193E18
		internal static void HttpChannelRequestAborted(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(101))
			{
				TD.WriteEtwEvent(101, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x00195C4C File Offset: 0x00193E4C
		internal static bool HttpChannelResponseAbortedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(102);
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x00195C60 File Offset: 0x00193E60
		internal static void HttpChannelResponseAborted(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(102))
			{
				TD.WriteEtwEvent(102, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x00195C94 File Offset: 0x00193E94
		internal static bool HttpAuthFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(103);
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x00195CA8 File Offset: 0x00193EA8
		internal static void HttpAuthFailed(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(103))
			{
				TD.WriteEtwEvent(103, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x00195CDC File Offset: 0x00193EDC
		internal static bool SharedListenerProxyRegisterStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(104);
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x00195CF0 File Offset: 0x00193EF0
		internal static void SharedListenerProxyRegisterStart(string Uri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(104))
			{
				TD.WriteEtwEvent(104, null, Uri, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x00195D25 File Offset: 0x00193F25
		internal static bool SharedListenerProxyRegisterStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(105);
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x00195D38 File Offset: 0x00193F38
		internal static void SharedListenerProxyRegisterStop()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(105))
			{
				TD.WriteEtwEvent(105, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x00195D6C File Offset: 0x00193F6C
		internal static bool SharedListenerProxyRegisterFailedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(106);
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x00195D80 File Offset: 0x00193F80
		internal static void SharedListenerProxyRegisterFailed(string Status)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(106))
			{
				TD.WriteEtwEvent(106, null, Status, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x00195DB5 File Offset: 0x00193FB5
		internal static bool ConnectionPoolPreambleFailedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(107);
		}

		// Token: 0x06006CFB RID: 27899 RVA: 0x00195DC8 File Offset: 0x00193FC8
		internal static void ConnectionPoolPreambleFailed(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(107))
			{
				TD.WriteEtwEvent(107, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x00195DFC File Offset: 0x00193FFC
		internal static bool SslOnInitiateUpgradeIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(108);
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x00195E10 File Offset: 0x00194010
		internal static void SslOnInitiateUpgrade()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(108))
			{
				TD.WriteEtwEvent(108, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x00195E44 File Offset: 0x00194044
		internal static bool SslOnAcceptUpgradeIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(109);
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x00195E58 File Offset: 0x00194058
		internal static void SslOnAcceptUpgrade(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(109))
			{
				TD.WriteEtwEvent(109, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x00195E8C File Offset: 0x0019408C
		internal static bool BinaryMessageEncodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(110);
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x00195EA0 File Offset: 0x001940A0
		internal static void BinaryMessageEncodingStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(110))
			{
				TD.WriteEtwEvent(110, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x00195ED4 File Offset: 0x001940D4
		internal static bool MtomMessageEncodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(111);
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x00195EE8 File Offset: 0x001940E8
		internal static void MtomMessageEncodingStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(111))
			{
				TD.WriteEtwEvent(111, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x00195F1C File Offset: 0x0019411C
		internal static bool TextMessageEncodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(112);
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x00195F30 File Offset: 0x00194130
		internal static void TextMessageEncodingStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(112))
			{
				TD.WriteEtwEvent(112, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x00195F64 File Offset: 0x00194164
		internal static bool BinaryMessageDecodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(113);
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x00195F78 File Offset: 0x00194178
		internal static void BinaryMessageDecodingStart()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(113))
			{
				TD.WriteEtwEvent(113, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x00195FAC File Offset: 0x001941AC
		internal static bool MtomMessageDecodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(114);
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x00195FC0 File Offset: 0x001941C0
		internal static void MtomMessageDecodingStart()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(114))
			{
				TD.WriteEtwEvent(114, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x00195FF4 File Offset: 0x001941F4
		internal static bool TextMessageDecodingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(115);
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x00196008 File Offset: 0x00194208
		internal static void TextMessageDecodingStart()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(115))
			{
				TD.WriteEtwEvent(115, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x0019603C File Offset: 0x0019423C
		internal static bool HttpResponseReceiveStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(116);
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x00196050 File Offset: 0x00194250
		internal static void HttpResponseReceiveStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(116))
			{
				TD.WriteEtwEvent(116, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x00196084 File Offset: 0x00194284
		internal static bool SocketReadStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(117);
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x00196098 File Offset: 0x00194298
		internal static void SocketReadStop(int SocketId, int Size, string Endpoint)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(117))
			{
				TD.WriteEtwEvent(117, null, SocketId, Size, Endpoint, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x001960CF File Offset: 0x001942CF
		internal static bool SocketAsyncReadStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(118);
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x001960E4 File Offset: 0x001942E4
		internal static void SocketAsyncReadStop(int SocketId, int Size, string Endpoint)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(118))
			{
				TD.WriteEtwEvent(118, null, SocketId, Size, Endpoint, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x0019611B File Offset: 0x0019431B
		internal static bool SocketWriteStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(119);
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x00196130 File Offset: 0x00194330
		internal static void SocketWriteStart(int SocketId, int Size, string Endpoint)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(119))
			{
				TD.WriteEtwEvent(119, null, SocketId, Size, Endpoint, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x00196167 File Offset: 0x00194367
		internal static bool SocketAsyncWriteStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(120);
		}

		// Token: 0x06006D15 RID: 27925 RVA: 0x0019617C File Offset: 0x0019437C
		internal static void SocketAsyncWriteStart(int SocketId, int Size, string Endpoint)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(120))
			{
				TD.WriteEtwEvent(120, null, SocketId, Size, Endpoint, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x001961B3 File Offset: 0x001943B3
		internal static bool SequenceAcknowledgementSentIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(121);
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x001961C8 File Offset: 0x001943C8
		internal static void SequenceAcknowledgementSent(string SessionId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(121))
			{
				TD.WriteEtwEvent(121, null, SessionId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x001961FD File Offset: 0x001943FD
		internal static bool ClientReliableSessionReconnectIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(122);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x00196210 File Offset: 0x00194410
		internal static void ClientReliableSessionReconnect(string SessionId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(122))
			{
				TD.WriteEtwEvent(122, null, SessionId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x00196245 File Offset: 0x00194445
		internal static bool ReliableSessionChannelFaultedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(123);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x00196258 File Offset: 0x00194458
		internal static void ReliableSessionChannelFaulted(string SessionId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(123))
			{
				TD.WriteEtwEvent(123, null, SessionId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x0019628D File Offset: 0x0019448D
		internal static bool WindowsStreamSecurityOnInitiateUpgradeIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(124);
		}

		// Token: 0x06006D1D RID: 27933 RVA: 0x001962A0 File Offset: 0x001944A0
		internal static void WindowsStreamSecurityOnInitiateUpgrade()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(124))
			{
				TD.WriteEtwEvent(124, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D1E RID: 27934 RVA: 0x001962D4 File Offset: 0x001944D4
		internal static bool WindowsStreamSecurityOnAcceptUpgradeIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(125);
		}

		// Token: 0x06006D1F RID: 27935 RVA: 0x001962E8 File Offset: 0x001944E8
		internal static void WindowsStreamSecurityOnAcceptUpgrade(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(125))
			{
				TD.WriteEtwEvent(125, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D20 RID: 27936 RVA: 0x0019631C File Offset: 0x0019451C
		internal static bool SocketConnectionAbortIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(126);
		}

		// Token: 0x06006D21 RID: 27937 RVA: 0x00196330 File Offset: 0x00194530
		internal static void SocketConnectionAbort(int SocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(126))
			{
				TD.WriteEtwEvent(126, null, SocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x00196365 File Offset: 0x00194565
		internal static bool HttpGetContextStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(127);
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x00196378 File Offset: 0x00194578
		internal static void HttpGetContextStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(127))
			{
				TD.WriteEtwEvent(127, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D24 RID: 27940 RVA: 0x001963AC File Offset: 0x001945AC
		internal static bool ClientSendPreambleStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(128);
		}

		// Token: 0x06006D25 RID: 27941 RVA: 0x001963C4 File Offset: 0x001945C4
		internal static void ClientSendPreambleStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(128))
			{
				TD.WriteEtwEvent(128, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D26 RID: 27942 RVA: 0x001963FE File Offset: 0x001945FE
		internal static bool ClientSendPreambleStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(129);
		}

		// Token: 0x06006D27 RID: 27943 RVA: 0x00196414 File Offset: 0x00194614
		internal static void ClientSendPreambleStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(129))
			{
				TD.WriteEtwEvent(129, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D28 RID: 27944 RVA: 0x0019644E File Offset: 0x0019464E
		internal static bool HttpMessageReceiveFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(130);
		}

		// Token: 0x06006D29 RID: 27945 RVA: 0x00196464 File Offset: 0x00194664
		internal static void HttpMessageReceiveFailed()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(130))
			{
				TD.WriteEtwEvent(130, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D2A RID: 27946 RVA: 0x0019649E File Offset: 0x0019469E
		internal static bool TransactionScopeCreateIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(131);
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x001964B4 File Offset: 0x001946B4
		internal static void TransactionScopeCreate(EventTraceActivity eventTraceActivity, string LocalId, Guid Distributed)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(131))
			{
				TD.WriteEtwEvent(131, eventTraceActivity, LocalId, Distributed, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D2C RID: 27948 RVA: 0x001964F0 File Offset: 0x001946F0
		internal static bool StreamedMessageReadByEncoderIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(132);
		}

		// Token: 0x06006D2D RID: 27949 RVA: 0x00196508 File Offset: 0x00194708
		internal static void StreamedMessageReadByEncoder(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(132))
			{
				TD.WriteEtwEvent(132, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D2E RID: 27950 RVA: 0x00196542 File Offset: 0x00194742
		internal static bool StreamedMessageWrittenByEncoderIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(133);
		}

		// Token: 0x06006D2F RID: 27951 RVA: 0x00196558 File Offset: 0x00194758
		internal static void StreamedMessageWrittenByEncoder(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(133))
			{
				TD.WriteEtwEvent(133, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x00196592 File Offset: 0x00194792
		internal static bool MessageWrittenAsynchronouslyByEncoderIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(134);
		}

		// Token: 0x06006D31 RID: 27953 RVA: 0x001965A8 File Offset: 0x001947A8
		internal static void MessageWrittenAsynchronouslyByEncoder(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(134))
			{
				TD.WriteEtwEvent(134, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D32 RID: 27954 RVA: 0x001965E2 File Offset: 0x001947E2
		internal static bool BufferedAsyncWriteStartIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(135);
		}

		// Token: 0x06006D33 RID: 27955 RVA: 0x001965F8 File Offset: 0x001947F8
		internal static void BufferedAsyncWriteStart(EventTraceActivity eventTraceActivity, int BufferId, int Size)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(135))
			{
				TD.WriteEtwEvent(135, eventTraceActivity, BufferId, Size, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D34 RID: 27956 RVA: 0x00196634 File Offset: 0x00194834
		internal static bool BufferedAsyncWriteStopIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(136);
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x0019664C File Offset: 0x0019484C
		internal static void BufferedAsyncWriteStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(136))
			{
				TD.WriteEtwEvent(136, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D36 RID: 27958 RVA: 0x00196686 File Offset: 0x00194886
		internal static bool ChannelInitializationTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(137);
		}

		// Token: 0x06006D37 RID: 27959 RVA: 0x0019669C File Offset: 0x0019489C
		internal static void ChannelInitializationTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(137))
			{
				TD.WriteEtwEvent(137, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x001966D7 File Offset: 0x001948D7
		internal static bool CloseTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(138);
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x001966EC File Offset: 0x001948EC
		internal static void CloseTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(138))
			{
				TD.WriteEtwEvent(138, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x00196727 File Offset: 0x00194927
		internal static bool IdleTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(139);
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x0019673C File Offset: 0x0019493C
		internal static void IdleTimeout(string msg, string key)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(139))
			{
				TD.WriteEtwEvent(139, null, msg, key, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x00196778 File Offset: 0x00194978
		internal static bool LeaseTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(140);
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x00196790 File Offset: 0x00194990
		internal static void LeaseTimeout(string msg, string key)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(140))
			{
				TD.WriteEtwEvent(140, null, msg, key, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x001967CC File Offset: 0x001949CC
		internal static bool OpenTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(141);
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x001967E4 File Offset: 0x001949E4
		internal static void OpenTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(141))
			{
				TD.WriteEtwEvent(141, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D40 RID: 27968 RVA: 0x0019681F File Offset: 0x00194A1F
		internal static bool ReceiveTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(142);
		}

		// Token: 0x06006D41 RID: 27969 RVA: 0x00196834 File Offset: 0x00194A34
		internal static void ReceiveTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(142))
			{
				TD.WriteEtwEvent(142, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x0019686F File Offset: 0x00194A6F
		internal static bool SendTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(143);
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x00196884 File Offset: 0x00194A84
		internal static void SendTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(143))
			{
				TD.WriteEtwEvent(143, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x001968BF File Offset: 0x00194ABF
		internal static bool InactivityTimeoutIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(144);
		}

		// Token: 0x06006D45 RID: 27973 RVA: 0x001968D4 File Offset: 0x00194AD4
		internal static void InactivityTimeout(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(144))
			{
				TD.WriteEtwEvent(144, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x0019690F File Offset: 0x00194B0F
		internal static bool MaxReceivedMessageSizeExceededIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(145);
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x00196924 File Offset: 0x00194B24
		internal static void MaxReceivedMessageSizeExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(145))
			{
				TD.WriteEtwEvent(145, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x0019695F File Offset: 0x00194B5F
		internal static bool MaxSentMessageSizeExceededIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(146);
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x00196974 File Offset: 0x00194B74
		internal static void MaxSentMessageSizeExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(146))
			{
				TD.WriteEtwEvent(146, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x001969AF File Offset: 0x00194BAF
		internal static bool MaxOutboundConnectionsPerEndpointExceededIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(147);
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x001969C4 File Offset: 0x00194BC4
		internal static void MaxOutboundConnectionsPerEndpointExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(147))
			{
				TD.WriteEtwEvent(147, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x001969FF File Offset: 0x00194BFF
		internal static bool MaxPendingConnectionsExceededIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(148);
		}

		// Token: 0x06006D4D RID: 27981 RVA: 0x00196A14 File Offset: 0x00194C14
		internal static void MaxPendingConnectionsExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(148))
			{
				TD.WriteEtwEvent(148, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x00196A4F File Offset: 0x00194C4F
		internal static bool NegotiateTokenAuthenticatorStateCacheExceededIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(149);
		}

		// Token: 0x06006D4F RID: 27983 RVA: 0x00196A64 File Offset: 0x00194C64
		internal static void NegotiateTokenAuthenticatorStateCacheExceeded(string msg)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(149))
			{
				TD.WriteEtwEvent(149, null, msg, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D50 RID: 27984 RVA: 0x00196A9F File Offset: 0x00194C9F
		internal static bool NegotiateTokenAuthenticatorStateCacheRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(150);
		}

		// Token: 0x06006D51 RID: 27985 RVA: 0x00196AB4 File Offset: 0x00194CB4
		internal static void NegotiateTokenAuthenticatorStateCacheRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(150))
			{
				TD.WriteEtwEvent(150, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D52 RID: 27986 RVA: 0x00196AF0 File Offset: 0x00194CF0
		internal static bool SecuritySessionRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(151);
		}

		// Token: 0x06006D53 RID: 27987 RVA: 0x00196B08 File Offset: 0x00194D08
		internal static void SecuritySessionRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(151))
			{
				TD.WriteEtwEvent(151, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D54 RID: 27988 RVA: 0x00196B44 File Offset: 0x00194D44
		internal static bool PendingConnectionsRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(152);
		}

		// Token: 0x06006D55 RID: 27989 RVA: 0x00196B5C File Offset: 0x00194D5C
		internal static void PendingConnectionsRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(152))
			{
				TD.WriteEtwEvent(152, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x00196B98 File Offset: 0x00194D98
		internal static bool OutboundConnectionsPerEndpointRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(153);
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x00196BB0 File Offset: 0x00194DB0
		internal static void OutboundConnectionsPerEndpointRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(153))
			{
				TD.WriteEtwEvent(153, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x00196BEC File Offset: 0x00194DEC
		internal static bool ConcurrentInstancesRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(154);
		}

		// Token: 0x06006D59 RID: 27993 RVA: 0x00196C04 File Offset: 0x00194E04
		internal static void ConcurrentInstancesRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(154))
			{
				TD.WriteEtwEvent(154, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D5A RID: 27994 RVA: 0x00196C40 File Offset: 0x00194E40
		internal static bool ConcurrentSessionsRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(155);
		}

		// Token: 0x06006D5B RID: 27995 RVA: 0x00196C58 File Offset: 0x00194E58
		internal static void ConcurrentSessionsRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(155))
			{
				TD.WriteEtwEvent(155, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x00196C94 File Offset: 0x00194E94
		internal static bool ConcurrentCallsRatioIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(156);
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x00196CAC File Offset: 0x00194EAC
		internal static void ConcurrentCallsRatio(int cur, int max)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(156))
			{
				TD.WriteEtwEvent(156, null, cur, max, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x00196CE8 File Offset: 0x00194EE8
		internal static bool PendingAcceptsAtZeroIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && TD.IsEtwEventEnabled(157);
		}

		// Token: 0x06006D5F RID: 27999 RVA: 0x00196D00 File Offset: 0x00194F00
		internal static void PendingAcceptsAtZero()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(157))
			{
				TD.WriteEtwEvent(157, null, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x00196D3A File Offset: 0x00194F3A
		internal static bool MaxSessionSizeReachedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(158);
		}

		// Token: 0x06006D61 RID: 28001 RVA: 0x00196D50 File Offset: 0x00194F50
		internal static void MaxSessionSizeReached(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(158))
			{
				TD.WriteEtwEvent(158, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x00196D8B File Offset: 0x00194F8B
		internal static bool ReceiveRetryCountReachedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(159);
		}

		// Token: 0x06006D63 RID: 28003 RVA: 0x00196DA0 File Offset: 0x00194FA0
		internal static void ReceiveRetryCountReached(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(159))
			{
				TD.WriteEtwEvent(159, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D64 RID: 28004 RVA: 0x00196DDB File Offset: 0x00194FDB
		internal static bool MaxRetryCyclesExceededMsmqIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(160);
		}

		// Token: 0x06006D65 RID: 28005 RVA: 0x00196DF0 File Offset: 0x00194FF0
		internal static void MaxRetryCyclesExceededMsmq(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(160))
			{
				TD.WriteEtwEvent(160, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D66 RID: 28006 RVA: 0x00196E2B File Offset: 0x0019502B
		internal static bool ReadPoolMissIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(161);
		}

		// Token: 0x06006D67 RID: 28007 RVA: 0x00196E40 File Offset: 0x00195040
		internal static void ReadPoolMiss(string itemTypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(161))
			{
				TD.WriteEtwEvent(161, null, itemTypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D68 RID: 28008 RVA: 0x00196E7B File Offset: 0x0019507B
		internal static bool WritePoolMissIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(162);
		}

		// Token: 0x06006D69 RID: 28009 RVA: 0x00196E90 File Offset: 0x00195090
		internal static void WritePoolMiss(string itemTypeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(162))
			{
				TD.WriteEtwEvent(162, null, itemTypeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D6A RID: 28010 RVA: 0x00196ECB File Offset: 0x001950CB
		internal static bool MaxRetryCyclesExceededIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(163);
		}

		// Token: 0x06006D6B RID: 28011 RVA: 0x00196EE0 File Offset: 0x001950E0
		internal static void MaxRetryCyclesExceeded(string param0)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(163))
			{
				TD.WriteEtwEvent(163, null, param0, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D6C RID: 28012 RVA: 0x00196F1B File Offset: 0x0019511B
		internal static bool PipeSharedMemoryCreatedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(164);
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x00196F30 File Offset: 0x00195130
		internal static void PipeSharedMemoryCreated(string sharedMemoryName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(164))
			{
				TD.WriteEtwEvent(164, null, sharedMemoryName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D6E RID: 28014 RVA: 0x00196F6B File Offset: 0x0019516B
		internal static bool NamedPipeCreatedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(165);
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x00196F80 File Offset: 0x00195180
		internal static void NamedPipeCreated(string pipeName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(165))
			{
				TD.WriteEtwEvent(165, null, pipeName, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D70 RID: 28016 RVA: 0x00196FBB File Offset: 0x001951BB
		internal static bool EncryptedDataProcessingStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(166);
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x00196FD0 File Offset: 0x001951D0
		internal static void EncryptedDataProcessingStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(166))
			{
				TD.WriteEtwEvent(166, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x0019700A File Offset: 0x0019520A
		internal static bool EncryptedDataProcessingSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(167);
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x00197020 File Offset: 0x00195220
		internal static void EncryptedDataProcessingSuccess(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(167))
			{
				TD.WriteEtwEvent(167, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x0019705A File Offset: 0x0019525A
		internal static bool SignatureVerificationStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(168);
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x00197070 File Offset: 0x00195270
		internal static void SignatureVerificationStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(168))
			{
				TD.WriteEtwEvent(168, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x001970AA File Offset: 0x001952AA
		internal static bool SignatureVerificationSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(169);
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x001970C0 File Offset: 0x001952C0
		internal static void SignatureVerificationSuccess(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(169))
			{
				TD.WriteEtwEvent(169, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x001970FA File Offset: 0x001952FA
		internal static bool WrappedKeyDecryptionStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(170);
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x00197110 File Offset: 0x00195310
		internal static void WrappedKeyDecryptionStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(170))
			{
				TD.WriteEtwEvent(170, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x0019714A File Offset: 0x0019534A
		internal static bool WrappedKeyDecryptionSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(171);
		}

		// Token: 0x06006D7B RID: 28027 RVA: 0x00197160 File Offset: 0x00195360
		internal static void WrappedKeyDecryptionSuccess(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(171))
			{
				TD.WriteEtwEvent(171, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0019719A File Offset: 0x0019539A
		internal static bool HttpPipelineProcessInboundRequestStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(172);
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x001971B0 File Offset: 0x001953B0
		internal static void HttpPipelineProcessInboundRequestStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(172))
			{
				TD.WriteEtwEvent(172, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x001971EA File Offset: 0x001953EA
		internal static bool HttpPipelineBeginProcessInboundRequestStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(173);
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x00197200 File Offset: 0x00195400
		internal static void HttpPipelineBeginProcessInboundRequestStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(173))
			{
				TD.WriteEtwEvent(173, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D80 RID: 28032 RVA: 0x0019723A File Offset: 0x0019543A
		internal static bool HttpPipelineProcessInboundRequestStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(174);
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x00197250 File Offset: 0x00195450
		internal static void HttpPipelineProcessInboundRequestStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(174))
			{
				TD.WriteEtwEvent(174, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D82 RID: 28034 RVA: 0x0019728A File Offset: 0x0019548A
		internal static bool HttpPipelineFaultedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && TD.IsEtwEventEnabled(175);
		}

		// Token: 0x06006D83 RID: 28035 RVA: 0x001972A0 File Offset: 0x001954A0
		internal static void HttpPipelineFaulted(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(175))
			{
				TD.WriteEtwEvent(175, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D84 RID: 28036 RVA: 0x001972DA File Offset: 0x001954DA
		internal static bool HttpPipelineTimeoutExceptionIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(176);
		}

		// Token: 0x06006D85 RID: 28037 RVA: 0x001972F0 File Offset: 0x001954F0
		internal static void HttpPipelineTimeoutException(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(176))
			{
				TD.WriteEtwEvent(176, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x0019732A File Offset: 0x0019552A
		internal static bool HttpPipelineProcessResponseStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(177);
		}

		// Token: 0x06006D87 RID: 28039 RVA: 0x00197340 File Offset: 0x00195540
		internal static void HttpPipelineProcessResponseStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(177))
			{
				TD.WriteEtwEvent(177, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D88 RID: 28040 RVA: 0x0019737A File Offset: 0x0019557A
		internal static bool HttpPipelineBeginProcessResponseStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(178);
		}

		// Token: 0x06006D89 RID: 28041 RVA: 0x00197390 File Offset: 0x00195590
		internal static void HttpPipelineBeginProcessResponseStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(178))
			{
				TD.WriteEtwEvent(178, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D8A RID: 28042 RVA: 0x001973CA File Offset: 0x001955CA
		internal static bool HttpPipelineProcessResponseStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(179);
		}

		// Token: 0x06006D8B RID: 28043 RVA: 0x001973E0 File Offset: 0x001955E0
		internal static void HttpPipelineProcessResponseStop(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(179))
			{
				TD.WriteEtwEvent(179, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D8C RID: 28044 RVA: 0x0019741A File Offset: 0x0019561A
		internal static bool WebSocketConnectionRequestSendStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(180);
		}

		// Token: 0x06006D8D RID: 28045 RVA: 0x00197430 File Offset: 0x00195630
		internal static void WebSocketConnectionRequestSendStart(EventTraceActivity eventTraceActivity, string remoteAddress)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(180))
			{
				TD.WriteEtwEvent(180, eventTraceActivity, remoteAddress, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D8E RID: 28046 RVA: 0x0019746B File Offset: 0x0019566B
		internal static bool WebSocketConnectionRequestSendStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(181);
		}

		// Token: 0x06006D8F RID: 28047 RVA: 0x00197480 File Offset: 0x00195680
		internal static void WebSocketConnectionRequestSendStop(EventTraceActivity eventTraceActivity, int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(181))
			{
				TD.WriteEtwEvent(181, eventTraceActivity, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x001974BB File Offset: 0x001956BB
		internal static bool WebSocketConnectionAcceptStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(182);
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x001974D0 File Offset: 0x001956D0
		internal static void WebSocketConnectionAcceptStart(EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(182))
			{
				TD.WriteEtwEvent(182, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x0019750A File Offset: 0x0019570A
		internal static bool WebSocketConnectionAcceptedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(183);
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x00197520 File Offset: 0x00195720
		internal static void WebSocketConnectionAccepted(EventTraceActivity eventTraceActivity, int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(183))
			{
				TD.WriteEtwEvent(183, eventTraceActivity, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x0019755B File Offset: 0x0019575B
		internal static bool WebSocketConnectionDeclinedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(184);
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x00197570 File Offset: 0x00195770
		internal static void WebSocketConnectionDeclined(EventTraceActivity eventTraceActivity, string errorMessage)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(184))
			{
				TD.WriteEtwEvent(184, eventTraceActivity, errorMessage, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x001975AB File Offset: 0x001957AB
		internal static bool WebSocketConnectionFailedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(185);
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x001975C0 File Offset: 0x001957C0
		internal static void WebSocketConnectionFailed(EventTraceActivity eventTraceActivity, string errorMessage)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(185))
			{
				TD.WriteEtwEvent(185, eventTraceActivity, errorMessage, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x001975FB File Offset: 0x001957FB
		internal static bool WebSocketConnectionAbortedIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(186);
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x00197610 File Offset: 0x00195810
		internal static void WebSocketConnectionAborted(EventTraceActivity eventTraceActivity, int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(186))
			{
				TD.WriteEtwEvent(186, eventTraceActivity, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x0019764B File Offset: 0x0019584B
		internal static bool WebSocketAsyncWriteStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(187);
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x00197660 File Offset: 0x00195860
		internal static void WebSocketAsyncWriteStart(int websocketId, int byteCount, string remoteAddress)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(187))
			{
				TD.WriteEtwEvent(187, null, websocketId, byteCount, remoteAddress, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D9C RID: 28060 RVA: 0x0019769D File Offset: 0x0019589D
		internal static bool WebSocketAsyncWriteStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(188);
		}

		// Token: 0x06006D9D RID: 28061 RVA: 0x001976B4 File Offset: 0x001958B4
		internal static void WebSocketAsyncWriteStop(int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(188))
			{
				TD.WriteEtwEvent(188, null, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006D9E RID: 28062 RVA: 0x001976EF File Offset: 0x001958EF
		internal static bool WebSocketAsyncReadStartIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(189);
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x00197704 File Offset: 0x00195904
		internal static void WebSocketAsyncReadStart(int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(189))
			{
				TD.WriteEtwEvent(189, null, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DA0 RID: 28064 RVA: 0x0019773F File Offset: 0x0019593F
		internal static bool WebSocketAsyncReadStopIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(190);
		}

		// Token: 0x06006DA1 RID: 28065 RVA: 0x00197754 File Offset: 0x00195954
		internal static void WebSocketAsyncReadStop(int websocketId, int byteCount, string remoteAddress)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(190))
			{
				TD.WriteEtwEvent(190, null, websocketId, byteCount, remoteAddress, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DA2 RID: 28066 RVA: 0x00197791 File Offset: 0x00195991
		internal static bool WebSocketCloseSentIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(191);
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x001977A8 File Offset: 0x001959A8
		internal static void WebSocketCloseSent(int websocketId, string remoteAddress, string closeStatus)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(191))
			{
				TD.WriteEtwEvent(191, null, websocketId, remoteAddress, closeStatus, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x001977E5 File Offset: 0x001959E5
		internal static bool WebSocketCloseOutputSentIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(192);
		}

		// Token: 0x06006DA5 RID: 28069 RVA: 0x001977FC File Offset: 0x001959FC
		internal static void WebSocketCloseOutputSent(int websocketId, string remoteAddress, string closeStatus)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(192))
			{
				TD.WriteEtwEvent(192, null, websocketId, remoteAddress, closeStatus, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DA6 RID: 28070 RVA: 0x00197839 File Offset: 0x00195A39
		internal static bool WebSocketConnectionClosedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(193);
		}

		// Token: 0x06006DA7 RID: 28071 RVA: 0x00197850 File Offset: 0x00195A50
		internal static void WebSocketConnectionClosed(int websocketId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(193))
			{
				TD.WriteEtwEvent(193, null, websocketId, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DA8 RID: 28072 RVA: 0x0019788B File Offset: 0x00195A8B
		internal static bool WebSocketCloseStatusReceivedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(194);
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x001978A0 File Offset: 0x00195AA0
		internal static void WebSocketCloseStatusReceived(int websocketId, string closeStatus)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(194))
			{
				TD.WriteEtwEvent(194, null, websocketId, closeStatus, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x001978DC File Offset: 0x00195ADC
		internal static bool WebSocketUseVersionFromClientWebSocketFactoryIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(195);
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x001978F4 File Offset: 0x00195AF4
		internal static void WebSocketUseVersionFromClientWebSocketFactory(EventTraceActivity eventTraceActivity, string clientWebSocketFactoryType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(195))
			{
				TD.WriteEtwEvent(195, eventTraceActivity, clientWebSocketFactoryType, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x0019792F File Offset: 0x00195B2F
		internal static bool WebSocketCreateClientWebSocketWithFactoryIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(196);
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x00197944 File Offset: 0x00195B44
		internal static void WebSocketCreateClientWebSocketWithFactory(EventTraceActivity eventTraceActivity, string clientWebSocketFactoryType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(196))
			{
				TD.WriteEtwEvent(196, eventTraceActivity, clientWebSocketFactoryType, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x00197980 File Offset: 0x00195B80
		[SecuritySafeCritical]
		private static void CreateEventDescriptors()
		{
			EventDescriptor[] array = new EventDescriptor[]
			{
				new EventDescriptor(217, 0, 19, 4, 20, 2514, 1152921504607371268L),
				new EventDescriptor(201, 0, 19, 4, 16, 2514, 1152921504607371268L),
				new EventDescriptor(202, 0, 18, 4, 17, 2514, 2305843009214218244L),
				new EventDescriptor(203, 0, 18, 4, 19, 2514, 2305843009214218244L),
				new EventDescriptor(204, 0, 18, 4, 18, 2514, 2305843009214218244L),
				new EventDescriptor(205, 0, 18, 4, 53, 2533, 2305843009214218244L),
				new EventDescriptor(206, 0, 18, 4, 0, 0, 2305843009214218244L),
				new EventDescriptor(207, 0, 18, 4, 0, 0, 2305843009214218244L),
				new EventDescriptor(208, 0, 18, 4, 51, 2533, 2305843009214218244L),
				new EventDescriptor(209, 0, 18, 4, 52, 2533, 2305843009214218244L),
				new EventDescriptor(210, 0, 18, 3, 0, 0, 2305843009218805764L),
				new EventDescriptor(211, 0, 18, 4, 56, 2533, 2305843009214218244L),
				new EventDescriptor(212, 0, 18, 4, 55, 2533, 2305843009214218244L),
				new EventDescriptor(214, 0, 18, 4, 54, 2533, 2305843009214611460L),
				new EventDescriptor(215, 0, 18, 4, 2, 2599, 2305843009214219264L),
				new EventDescriptor(216, 0, 18, 4, 2, 2600, 2305843009214219264L),
				new EventDescriptor(451, 0, 18, 4, 0, 0, 2305843009214218272L),
				new EventDescriptor(452, 0, 18, 3, 0, 0, 2305843009214218272L),
				new EventDescriptor(4600, 0, 19, 3, 0, 0, 1152921504606847008L),
				new EventDescriptor(404, 0, 18, 4, 7, 2588, 2305843009214218244L),
				new EventDescriptor(402, 0, 18, 4, 1, 2588, 2305843009214218244L),
				new EventDescriptor(401, 0, 18, 4, 2, 2588, 2305843009214218244L),
				new EventDescriptor(403, 0, 18, 4, 8, 2588, 2305843009214218244L),
				new EventDescriptor(218, 0, 18, 4, 2, 2576, 2305843009214218244L),
				new EventDescriptor(219, 0, 18, 2, 0, 2533, 2305843009214611460L),
				new EventDescriptor(222, 0, 18, 3, 0, 2533, 2305843009214611460L),
				new EventDescriptor(223, 0, 18, 3, 0, 2533, 2305843009214611460L),
				new EventDescriptor(224, 0, 18, 3, 0, 2533, 2305843009218805764L),
				new EventDescriptor(221, 0, 18, 4, 0, 0, 2305843009214350336L),
				new EventDescriptor(220, 0, 18, 4, 0, 0, 2305843009214350336L),
				new EventDescriptor(509, 0, 18, 4, 1, 2583, 2305843009213693953L),
				new EventDescriptor(510, 0, 18, 4, 2, 2583, 2305843009213693953L),
				new EventDescriptor(701, 0, 18, 4, 1, 2577, 2305843009213693956L),
				new EventDescriptor(702, 0, 18, 4, 2, 2577, 2305843009213693956L),
				new EventDescriptor(703, 0, 18, 4, 1, 2576, 2305843009213693956L),
				new EventDescriptor(704, 0, 18, 4, 1, 2576, 2305843009213693956L),
				new EventDescriptor(706, 0, 19, 5, 1, 2600, 1152921504606847232L),
				new EventDescriptor(707, 0, 19, 5, 2, 2600, 1152921504606847232L),
				new EventDescriptor(708, 0, 18, 5, 1, 2599, 2305843009213694208L),
				new EventDescriptor(709, 0, 18, 4, 49, 2533, 2305843009213693956L),
				new EventDescriptor(710, 0, 18, 5, 128, 2599, 2305843009213693956L),
				new EventDescriptor(711, 0, 18, 5, 48, 2533, 2305843009213693956L),
				new EventDescriptor(712, 0, 18, 4, 50, 2533, 2305843009213693956L),
				new EventDescriptor(715, 0, 18, 4, 14, 2514, 2305843009213693956L),
				new EventDescriptor(716, 0, 18, 4, 15, 2514, 2305843009213693956L),
				new EventDescriptor(717, 0, 18, 4, 1, 2600, 2305843009213694208L),
				new EventDescriptor(3301, 0, 19, 3, 0, 0, 1152921504606851072L),
				new EventDescriptor(3303, 0, 19, 4, 0, 0, 1152921504606851072L),
				new EventDescriptor(3300, 0, 18, 3, 0, 0, 2305843009213698048L),
				new EventDescriptor(3302, 0, 18, 3, 0, 2533, 2305843009213693956L),
				new EventDescriptor(3305, 0, 19, 4, 0, 2511, 1152921504606846980L),
				new EventDescriptor(3306, 0, 19, 4, 0, 2511, 1152921504606846980L),
				new EventDescriptor(3307, 0, 19, 4, 0, 2511, 1152921504606846980L),
				new EventDescriptor(3308, 0, 19, 4, 0, 2511, 1152921504606846980L),
				new EventDescriptor(3309, 0, 19, 4, 0, 0, 1152921504606846980L),
				new EventDescriptor(3310, 0, 18, 2, 0, 2533, 2305843009213693956L),
				new EventDescriptor(3311, 0, 18, 4, 2, 2533, 2305843009213693956L),
				new EventDescriptor(3312, 0, 19, 4, 2, 2555, 1152921504606851072L),
				new EventDescriptor(3313, 0, 19, 4, 2, 2556, 1152921504606851072L),
				new EventDescriptor(3314, 0, 18, 2, 0, 2595, 2305843009213693956L),
				new EventDescriptor(3319, 0, 19, 5, 1, 2521, 1152921504606847488L),
				new EventDescriptor(3320, 0, 19, 5, 2, 2521, 1152921504606847488L),
				new EventDescriptor(3321, 0, 19, 5, 0, 2522, 1152921504606851072L),
				new EventDescriptor(3322, 0, 19, 5, 1, 2540, 1152921504606846980L),
				new EventDescriptor(3323, 0, 19, 5, 2, 2540, 1152921504606846980L),
				new EventDescriptor(3324, 0, 19, 5, 1, 2541, 1152921504606846980L),
				new EventDescriptor(3325, 0, 19, 5, 2, 2541, 1152921504606846980L),
				new EventDescriptor(3326, 0, 19, 5, 1, 2542, 1152921504606846980L),
				new EventDescriptor(3327, 0, 19, 5, 2, 2542, 1152921504606846980L),
				new EventDescriptor(3328, 0, 19, 5, 1, 2539, 1152921504606846980L),
				new EventDescriptor(3329, 0, 19, 5, 2, 2539, 1152921504606846980L),
				new EventDescriptor(3330, 0, 19, 5, 1, 2573, 1152921504606846992L),
				new EventDescriptor(3331, 0, 19, 5, 2, 2573, 1152921504606846992L),
				new EventDescriptor(3332, 0, 19, 5, 1, 2571, 1152921504606846992L),
				new EventDescriptor(3333, 0, 19, 5, 2, 2571, 1152921504606846992L),
				new EventDescriptor(3334, 0, 19, 5, 0, 2574, 1152921504606846996L),
				new EventDescriptor(3335, 0, 19, 5, 1, 2584, 1152921504606846980L),
				new EventDescriptor(3336, 0, 19, 5, 2, 2584, 1152921504606846980L),
				new EventDescriptor(3337, 0, 19, 5, 1, 2513, 1152921504606851072L),
				new EventDescriptor(3338, 0, 19, 5, 2, 2513, 1152921504606851072L),
				new EventDescriptor(3339, 0, 19, 5, 0, 2512, 1152921504606846980L),
				new EventDescriptor(3340, 0, 19, 5, 1, 2521, 1152921504606851072L),
				new EventDescriptor(3341, 0, 19, 5, 2, 2521, 1152921504606851072L),
				new EventDescriptor(3342, 0, 19, 5, 1, 2519, 1152921504606851072L),
				new EventDescriptor(3343, 0, 19, 5, 2, 2519, 1152921504606851072L),
				new EventDescriptor(3345, 0, 19, 5, 0, 2519, 1152921504606851072L),
				new EventDescriptor(3346, 0, 19, 2, 0, 2519, 1152921504606851072L),
				new EventDescriptor(3347, 0, 19, 5, 2, 2521, 1152921504606847488L),
				new EventDescriptor(3348, 0, 18, 1, 0, 2582, 2305843009213694464L),
				new EventDescriptor(3349, 0, 19, 5, 1, 2552, 1152921504606851072L),
				new EventDescriptor(3350, 0, 19, 5, 2, 2552, 1152921504606851072L),
				new EventDescriptor(3351, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(3352, 0, 18, 2, 0, 2519, 2305843009213694464L),
				new EventDescriptor(3353, 0, 18, 3, 0, 2519, 2305843009213694464L),
				new EventDescriptor(3354, 0, 19, 5, 0, 2573, 1152921504606846992L),
				new EventDescriptor(3355, 0, 18, 2, 0, 2573, 2305843009213693968L),
				new EventDescriptor(3356, 0, 19, 5, 0, 2574, 1152921504606846992L),
				new EventDescriptor(3357, 0, 18, 2, 0, 2574, 2305843009213693968L),
				new EventDescriptor(3358, 0, 19, 5, 0, 2501, 1152921504606849024L),
				new EventDescriptor(3359, 0, 19, 5, 0, 2572, 1152921504606846992L),
				new EventDescriptor(3360, 0, 18, 3, 0, 2572, 2305843009213693968L),
				new EventDescriptor(3361, 0, 18, 3, 0, 2599, 2305843009213694208L),
				new EventDescriptor(3362, 0, 18, 3, 0, 2600, 2305843009213694208L),
				new EventDescriptor(3363, 0, 18, 3, 0, 2574, 2305843009213694208L),
				new EventDescriptor(3364, 0, 18, 5, 1, 2502, 2305843009213696000L),
				new EventDescriptor(3365, 0, 18, 5, 2, 2502, 2305843009213696000L),
				new EventDescriptor(3366, 0, 18, 2, 0, 2502, 2305843009213696000L),
				new EventDescriptor(3367, 0, 18, 2, 0, 2586, 2305843009213698048L),
				new EventDescriptor(3368, 0, 18, 5, 115, 2587, 2305843009213693968L),
				new EventDescriptor(3369, 0, 18, 5, 114, 2587, 2305843009213693968L),
				new EventDescriptor(3370, 0, 19, 5, 1, 2556, 1152921504606851072L),
				new EventDescriptor(3371, 0, 19, 5, 1, 2556, 1152921504606851072L),
				new EventDescriptor(3372, 0, 19, 5, 1, 2556, 1152921504606851072L),
				new EventDescriptor(3373, 0, 19, 5, 1, 2555, 1152921504606851072L),
				new EventDescriptor(3374, 0, 19, 5, 1, 2555, 1152921504606851072L),
				new EventDescriptor(3375, 0, 19, 5, 1, 2555, 1152921504606851072L),
				new EventDescriptor(3376, 0, 19, 4, 1, 2599, 1152921504606847232L),
				new EventDescriptor(3377, 0, 19, 5, 2, 2599, 1152921504606847488L),
				new EventDescriptor(3378, 0, 19, 5, 2, 2599, 1152921504606847488L),
				new EventDescriptor(3379, 0, 19, 5, 1, 2600, 1152921504606847488L),
				new EventDescriptor(3380, 0, 19, 5, 1, 2600, 1152921504606847488L),
				new EventDescriptor(3381, 0, 19, 5, 79, 2561, 1152921504606851072L),
				new EventDescriptor(3382, 0, 19, 4, 78, 2561, 1152921504606851072L),
				new EventDescriptor(3383, 0, 19, 4, 77, 2561, 1152921504606851072L),
				new EventDescriptor(3384, 0, 18, 5, 115, 2587, 2305843009213693968L),
				new EventDescriptor(3385, 0, 18, 5, 114, 2587, 2305843009213693968L),
				new EventDescriptor(3386, 0, 18, 3, 0, 2520, 2305843009213694464L),
				new EventDescriptor(3388, 0, 18, 5, 1, 2599, 2305843009213694208L),
				new EventDescriptor(3389, 0, 19, 5, 1, 2515, 1152921504606851072L),
				new EventDescriptor(3390, 0, 19, 5, 2, 2515, 1152921504606851072L),
				new EventDescriptor(3391, 0, 18, 3, 1, 2599, 2305843009213694208L),
				new EventDescriptor(3392, 0, 19, 4, 57, 2533, 1152921504606846980L),
				new EventDescriptor(3393, 0, 19, 4, 2, 2555, 1152921504606851072L),
				new EventDescriptor(3394, 0, 19, 4, 2, 2556, 1152921504606851072L),
				new EventDescriptor(3395, 0, 19, 4, 2, 2556, 1152921504606851072L),
				new EventDescriptor(3396, 0, 19, 4, 1, 2600, 1152921504606851072L),
				new EventDescriptor(3397, 0, 19, 4, 2, 2600, 1152921504606851072L),
				new EventDescriptor(1400, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1401, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1402, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1403, 0, 18, 4, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1405, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1406, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1407, 0, 18, 2, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1409, 0, 18, 4, 0, 2596, 2305843009213693956L),
				new EventDescriptor(1416, 0, 18, 2, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1417, 0, 18, 2, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1418, 0, 19, 4, 0, 2560, 1152921504611041280L),
				new EventDescriptor(1419, 0, 19, 4, 0, 2560, 1152921504611041280L),
				new EventDescriptor(1422, 0, 18, 2, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1423, 0, 19, 5, 0, 2560, 1152921504611041280L),
				new EventDescriptor(1424, 0, 19, 5, 0, 2560, 1152921504611041280L),
				new EventDescriptor(1430, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1433, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1438, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1432, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1431, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1439, 0, 19, 4, 0, 2560, 1152921504611041280L),
				new EventDescriptor(1441, 0, 18, 3, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1442, 0, 18, 3, 0, 2558, 2305843009217888256L),
				new EventDescriptor(1443, 0, 18, 2, 0, 2558, 2305843009217888256L),
				new EventDescriptor(1445, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1446, 0, 18, 5, 0, 2560, 2305843009217888256L),
				new EventDescriptor(1451, 0, 18, 2, 0, 2560, 2305843009217888256L),
				new EventDescriptor(3398, 0, 19, 5, 0, 2552, 1152921504606851072L),
				new EventDescriptor(3399, 0, 19, 5, 0, 2552, 1152921504606851072L),
				new EventDescriptor(3405, 0, 19, 5, 0, 2615, 1152921504606846992L),
				new EventDescriptor(3406, 0, 19, 5, 0, 2615, 1152921504606846992L),
				new EventDescriptor(3401, 0, 19, 5, 1, 2611, 1152921504606846992L),
				new EventDescriptor(3402, 0, 19, 5, 0, 2611, 1152921504606846992L),
				new EventDescriptor(3403, 0, 19, 5, 0, 2614, 1152921504606846992L),
				new EventDescriptor(3404, 0, 19, 5, 0, 2614, 1152921504606846992L),
				new EventDescriptor(3407, 0, 19, 5, 1, 2599, 1152921504606847232L),
				new EventDescriptor(3408, 0, 19, 5, 1, 2599, 1152921504606847232L),
				new EventDescriptor(3409, 0, 19, 5, 2, 2599, 1152921504606847232L),
				new EventDescriptor(3410, 0, 18, 3, 0, 2599, 2305843009213694208L),
				new EventDescriptor(3411, 0, 18, 2, 0, 2519, 2305843009213694208L),
				new EventDescriptor(3412, 0, 19, 5, 1, 2600, 1152921504606847232L),
				new EventDescriptor(3413, 0, 19, 5, 1, 2600, 1152921504606847232L),
				new EventDescriptor(3414, 0, 19, 5, 2, 2600, 1152921504606847232L),
				new EventDescriptor(3415, 0, 19, 5, 1, 2519, 1152921504606847232L),
				new EventDescriptor(3416, 0, 19, 5, 2, 2519, 1152921504606847232L),
				new EventDescriptor(3417, 0, 19, 5, 1, 2519, 1152921504606847232L),
				new EventDescriptor(3418, 0, 19, 5, 2, 2519, 1152921504606847232L),
				new EventDescriptor(3419, 0, 18, 2, 0, 2519, 2305843009213694208L),
				new EventDescriptor(3420, 0, 18, 2, 0, 2519, 2305843009213694208L),
				new EventDescriptor(3421, 0, 18, 2, 0, 2519, 2305843009213694208L),
				new EventDescriptor(3422, 0, 19, 5, 1, 2600, 1152921504606847232L),
				new EventDescriptor(3423, 0, 19, 5, 2, 2600, 1152921504606847232L),
				new EventDescriptor(3424, 0, 19, 5, 1, 2599, 1152921504606847232L),
				new EventDescriptor(3425, 0, 19, 5, 2, 2599, 1152921504606847232L),
				new EventDescriptor(3426, 0, 19, 5, 0, 2519, 1152921504606847232L),
				new EventDescriptor(3427, 0, 19, 5, 0, 2519, 1152921504606847232L),
				new EventDescriptor(3428, 0, 19, 5, 0, 2519, 1152921504606847232L),
				new EventDescriptor(3429, 0, 19, 5, 0, 2519, 1152921504606847232L),
				new EventDescriptor(3430, 0, 19, 5, 0, 2519, 1152921504606847232L),
				new EventDescriptor(3431, 0, 19, 5, 0, 2519, 1152921504606847232L)
			};
			FxTrace.UpdateEventDefinitions(array, new List<ushort>(120)
			{
				201,
				202,
				203,
				204,
				205,
				208,
				209,
				211,
				212,
				214,
				215,
				216,
				217,
				218,
				219,
				220,
				221,
				222,
				223,
				509,
				510,
				701,
				702,
				703,
				704,
				706,
				707,
				708,
				709,
				710,
				711,
				712,
				715,
				716,
				717,
				3300,
				3301,
				3302,
				3303,
				3309,
				3310,
				3311,
				3312,
				3313,
				3319,
				3320,
				3322,
				3323,
				3324,
				3325,
				3326,
				3327,
				3328,
				3329,
				3330,
				3331,
				3332,
				3333,
				3334,
				3335,
				3336,
				3337,
				3338,
				3340,
				3341,
				3342,
				3343,
				3347,
				3348,
				3349,
				3350,
				3354,
				3355,
				3356,
				3357,
				3358,
				3359,
				3360,
				3361,
				3362,
				3363,
				3367,
				3369,
				3370,
				3371,
				3372,
				3376,
				3385,
				3388,
				3389,
				3390,
				3392,
				3393,
				3394,
				3395,
				3396,
				3397,
				3401,
				3402,
				3403,
				3404,
				3405,
				3406,
				3407,
				3408,
				3409,
				3410,
				3411,
				3412,
				3413,
				3414,
				3415,
				3416,
				3417,
				3418,
				3419,
				3420,
				3421,
				3430,
				3431
			}.ToArray());
			TD.eventDescriptors = array;
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x00199C08 File Offset: 0x00197E08
		private static void EnsureEventDescriptors()
		{
			if (TD.eventDescriptorsCreated)
			{
				return;
			}
			lock (TD.syncLock)
			{
				if (!TD.eventDescriptorsCreated)
				{
					TD.CreateEventDescriptors();
					TD.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x00199C60 File Offset: 0x00197E60
		private static bool IsEtwEventEnabled(int eventIndex)
		{
			if (FxTrace.Trace.IsEtwProviderEnabled)
			{
				TD.EnsureEventDescriptors();
				return FxTrace.IsEventEnabled(eventIndex);
			}
			return false;
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x00199C7B File Offset: 0x00197E7B
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x00199CA1 File Offset: 0x00197EA1
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x00199CCC File Offset: 0x00197ECC
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, bool eventParam2, string eventParam3, string eventParam4, string eventParam5)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3,
				eventParam4,
				eventParam5
			});
		}

		// Token: 0x06006DB4 RID: 28084 RVA: 0x00199D1B File Offset: 0x00197F1B
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, long eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x06006DB5 RID: 28085 RVA: 0x00199D43 File Offset: 0x00197F43
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x00199D67 File Offset: 0x00197F67
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x00199D8A File Offset: 0x00197F8A
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4, string eventParam5)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4, eventParam5);
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x00199DB4 File Offset: 0x00197FB4
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, Guid eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x00199DDC File Offset: 0x00197FDC
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, int eventParam1, int eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3,
				eventParam4
			});
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x00199E2B File Offset: 0x0019802B
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, int eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x00199E65 File Offset: 0x00198065
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, int eventParam1, int eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x06006DBC RID: 28092 RVA: 0x00199EA4 File Offset: 0x001980A4
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, int eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x06006DBD RID: 28093 RVA: 0x00199EDE File Offset: 0x001980DE
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, int eventParam1, string eventParam2)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2
			});
		}

		// Token: 0x06006DBE RID: 28094 RVA: 0x00199F13 File Offset: 0x00198113
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, Guid eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3
			});
		}

		// Token: 0x06006DBF RID: 28095 RVA: 0x00199F4D File Offset: 0x0019814D
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, int eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, new object[]
			{
				eventParam1,
				eventParam2,
				eventParam3,
				eventParam4
			});
		}

		// Token: 0x06006DC0 RID: 28096 RVA: 0x00199F8C File Offset: 0x0019818C
		[SecuritySafeCritical]
		private static bool WriteEtwTransferEvent(int eventIndex, EventTraceActivity eventParam0, Guid eventParam1, string eventParam2, string eventParam3, string eventParam4, string eventParam5, string eventParam6)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteTransferEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, new object[]
			{
				eventParam2,
				eventParam3,
				eventParam4,
				eventParam5,
				eventParam6
			});
		}

		// Token: 0x06006DC1 RID: 28097 RVA: 0x00199FD8 File Offset: 0x001981D8
		[SecuritySafeCritical]
		private static bool WriteEtwTransferEvent(int eventIndex, EventTraceActivity eventParam0, Guid eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteTransferEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, new object[]
			{
				eventParam2,
				eventParam3,
				eventParam4
			});
		}

		// Token: 0x06006DC2 RID: 28098 RVA: 0x0019A00F File Offset: 0x0019820F
		[SecuritySafeCritical]
		private static bool WriteEtwTransferEvent(int eventIndex, EventTraceActivity eventParam0, Guid eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteTransferEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x06006DC3 RID: 28099 RVA: 0x0019A035 File Offset: 0x00198235
		[SecuritySafeCritical]
		private static void WriteTraceSource(int eventIndex, string description, TracePayload payload)
		{
			TD.EnsureEventDescriptors();
			FxTrace.Trace.WriteTraceSource(ref TD.eventDescriptors[eventIndex], description, payload);
		}

		// Token: 0x04003EB9 RID: 16057
		private static ResourceManager resourceManager;

		// Token: 0x04003EBA RID: 16058
		private static CultureInfo resourceCulture;

		// Token: 0x04003EBB RID: 16059
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04003EBC RID: 16060
		private static object syncLock = new object();

		// Token: 0x04003EBD RID: 16061
		private static volatile bool eventDescriptorsCreated;
	}
}
