using System;
using System.Globalization;
using System.Resources;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200005B RID: 91
	internal class TD
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00006351 File Offset: 0x00004551
		private TD()
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000D87B File Offset: 0x0000BA7B
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TD.resourceManager == null)
				{
					TD.resourceManager = new ResourceManager("System.ServiceModel.Discovery.TD", typeof(TD).Assembly);
				}
				return TD.resourceManager;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000D8A7 File Offset: 0x0000BAA7
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000D8AE File Offset: 0x0000BAAE
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

		// Token: 0x0600047B RID: 1147 RVA: 0x0000D8B6 File Offset: 0x0000BAB6
		internal static bool DiscoveryClientInClientChannelFailedToCloseIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(0));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		internal static void DiscoveryClientInClientChannelFailedToClose(Exception exception)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, exception);
			if (TD.IsEtwEventEnabled(0))
			{
				TD.WriteEtwEvent(0, null, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryClientInClientChannelFailedToClose", TD.Culture), new object[0]);
				TD.WriteTraceSource(0, description, serializedPayload);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000D93D File Offset: 0x0000BB3D
		internal static bool DiscoveryClientProtocolExceptionSuppressedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(1));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D958 File Offset: 0x0000BB58
		internal static void DiscoveryClientProtocolExceptionSuppressed(Exception exception)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, exception);
			if (TD.IsEtwEventEnabled(1))
			{
				TD.WriteEtwEvent(1, null, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryClientProtocolExceptionSuppressed", TD.Culture), new object[0]);
				TD.WriteTraceSource(1, description, serializedPayload);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D9C5 File Offset: 0x0000BBC5
		internal static bool DiscoveryClientReceivedMulticastSuppressionIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(2));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		internal static void DiscoveryClientReceivedMulticastSuppression()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(2))
			{
				TD.WriteEtwEvent(2, null, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryClientReceivedMulticastSuppression", TD.Culture), new object[0]);
				TD.WriteTraceSource(2, description, serializedPayload);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000DA46 File Offset: 0x0000BC46
		internal static bool DiscoveryMessageReceivedAfterOperationCompletedIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(3));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DA60 File Offset: 0x0000BC60
		internal static void DiscoveryMessageReceivedAfterOperationCompleted(EventTraceActivity eventTraceActivity, string discoveryMessageName, string messageId, string discoveryOperationName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(3))
			{
				TD.WriteEtwEvent(3, eventTraceActivity, discoveryMessageName, messageId, discoveryOperationName, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageReceivedAfterOperationCompleted", TD.Culture), new object[]
				{
					discoveryMessageName,
					messageId,
					discoveryOperationName
				});
				TD.WriteTraceSource(3, description, serializedPayload);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		internal static bool DiscoveryMessageWithInvalidContentIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(4));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		internal static void DiscoveryMessageWithInvalidContent(EventTraceActivity eventTraceActivity, string messageType, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(4))
			{
				TD.WriteEtwEvent(4, eventTraceActivity, messageType, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithInvalidContent", TD.Culture), new object[]
				{
					messageType,
					messageId
				});
				TD.WriteTraceSource(4, description, serializedPayload);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000DB60 File Offset: 0x0000BD60
		internal static bool DiscoveryMessageWithInvalidRelatesToOrOperationCompletedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(5));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		internal static void DiscoveryMessageWithInvalidRelatesToOrOperationCompleted(EventTraceActivity eventTraceActivity, string discoveryMessageName, string messageId, string relatesTo, string discoveryOperationName)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(5))
			{
				TD.WriteEtwEvent(5, eventTraceActivity, discoveryMessageName, messageId, relatesTo, discoveryOperationName, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithInvalidRelatesToOrOperationCompleted", TD.Culture), new object[]
				{
					discoveryMessageName,
					messageId,
					relatesTo,
					discoveryOperationName
				});
				TD.WriteTraceSource(5, description, serializedPayload);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		internal static bool DiscoveryMessageWithInvalidReplyToIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(6));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000DC14 File Offset: 0x0000BE14
		internal static void DiscoveryMessageWithInvalidReplyTo(EventTraceActivity eventTraceActivity, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(6))
			{
				TD.WriteEtwEvent(6, eventTraceActivity, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithInvalidReplyTo", TD.Culture), new object[]
				{
					messageId
				});
				TD.WriteTraceSource(6, description, serializedPayload);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000DC7F File Offset: 0x0000BE7F
		internal static bool DiscoveryMessageWithNoContentIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(7));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		internal static void DiscoveryMessageWithNoContent(EventTraceActivity eventTraceActivity, string messageType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(7))
			{
				TD.WriteEtwEvent(7, eventTraceActivity, messageType, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithNoContent", TD.Culture), new object[]
				{
					messageType
				});
				TD.WriteTraceSource(7, description, serializedPayload);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000DD07 File Offset: 0x0000BF07
		internal static bool DiscoveryMessageWithNullMessageIdIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(8));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DD24 File Offset: 0x0000BF24
		internal static void DiscoveryMessageWithNullMessageId(EventTraceActivity eventTraceActivity, string messageType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(8))
			{
				TD.WriteEtwEvent(8, eventTraceActivity, messageType, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithNullMessageId", TD.Culture), new object[]
				{
					messageType
				});
				TD.WriteTraceSource(8, description, serializedPayload);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000DD8F File Offset: 0x0000BF8F
		internal static bool DiscoveryMessageWithNullMessageSequenceIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(9));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		internal static void DiscoveryMessageWithNullMessageSequence(string discoveryMessageName, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(9))
			{
				TD.WriteEtwEvent(9, null, discoveryMessageName, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithNullMessageSequence", TD.Culture), new object[]
				{
					discoveryMessageName,
					messageId
				});
				TD.WriteTraceSource(9, description, serializedPayload);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000DE1F File Offset: 0x0000C01F
		internal static bool DiscoveryMessageWithNullRelatesToIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(10));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000DE3C File Offset: 0x0000C03C
		internal static void DiscoveryMessageWithNullRelatesTo(EventTraceActivity eventTraceActivity, string discoveryMessageName, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(10))
			{
				TD.WriteEtwEvent(10, eventTraceActivity, discoveryMessageName, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithNullRelatesTo", TD.Culture), new object[]
				{
					discoveryMessageName,
					messageId
				});
				TD.WriteTraceSource(10, description, serializedPayload);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000DEAF File Offset: 0x0000C0AF
		internal static bool DiscoveryMessageWithNullReplyToIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(11));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000DECC File Offset: 0x0000C0CC
		internal static void DiscoveryMessageWithNullReplyTo(EventTraceActivity eventTraceActivity, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(11))
			{
				TD.WriteEtwEvent(11, eventTraceActivity, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DiscoveryMessageWithNullReplyTo", TD.Culture), new object[]
				{
					messageId
				});
				TD.WriteTraceSource(11, description, serializedPayload);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000DF3A File Offset: 0x0000C13A
		internal static bool DuplicateDiscoveryMessageIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(12));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000DF58 File Offset: 0x0000C158
		internal static void DuplicateDiscoveryMessage(EventTraceActivity eventTraceActivity, string messageType, string messageId)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(12))
			{
				TD.WriteEtwEvent(12, eventTraceActivity, messageType, messageId, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("DuplicateDiscoveryMessage", TD.Culture), new object[]
				{
					messageType,
					messageId
				});
				TD.WriteTraceSource(12, description, serializedPayload);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		internal static bool EndpointDiscoverabilityDisabledIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(13));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		internal static void EndpointDiscoverabilityDisabled(string endpointAddress, string listenUri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(13))
			{
				TD.WriteEtwEvent(13, null, endpointAddress, listenUri, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("EndpointDiscoverabilityDisabled", TD.Culture), new object[]
				{
					endpointAddress,
					listenUri
				});
				TD.WriteTraceSource(13, description, serializedPayload);
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000E05B File Offset: 0x0000C25B
		internal static bool EndpointDiscoverabilityEnabledIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(14));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000E078 File Offset: 0x0000C278
		internal static void EndpointDiscoverabilityEnabled(string endpointAddress, string listenUri)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(14))
			{
				TD.WriteEtwEvent(14, null, endpointAddress, listenUri, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("EndpointDiscoverabilityEnabled", TD.Culture), new object[]
				{
					endpointAddress,
					listenUri
				});
				TD.WriteTraceSource(14, description, serializedPayload);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000E0EB File Offset: 0x0000C2EB
		internal static bool FindInitiatedInDiscoveryClientChannelIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && (FxTrace.ShouldTraceVerboseToTraceSource || TD.IsEtwEventEnabled(15));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E108 File Offset: 0x0000C308
		internal static void FindInitiatedInDiscoveryClientChannel()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(15))
			{
				TD.WriteEtwEvent(15, null, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceVerboseToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("FindInitiatedInDiscoveryClientChannel", TD.Culture), new object[0]);
				TD.WriteTraceSource(15, description, serializedPayload);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E171 File Offset: 0x0000C371
		internal static bool InnerChannelCreationFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(16));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E18C File Offset: 0x0000C38C
		internal static void InnerChannelCreationFailed(string endpointAddress, string via, Exception exception)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, exception);
			if (TD.IsEtwEventEnabled(16))
			{
				TD.WriteEtwEvent(16, null, endpointAddress, via, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("InnerChannelCreationFailed", TD.Culture), new object[]
				{
					endpointAddress,
					via
				});
				TD.WriteTraceSource(16, description, serializedPayload);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E206 File Offset: 0x0000C406
		internal static bool InnerChannelOpenFailedIsEnabled()
		{
			return FxTrace.ShouldTraceWarning && (FxTrace.ShouldTraceWarningToTraceSource || TD.IsEtwEventEnabled(17));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000E224 File Offset: 0x0000C424
		internal static void InnerChannelOpenFailed(string endpointAddress, string via, Exception exception)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, exception);
			if (TD.IsEtwEventEnabled(17))
			{
				TD.WriteEtwEvent(17, null, endpointAddress, via, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceWarningToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("InnerChannelOpenFailed", TD.Culture), new object[]
				{
					endpointAddress,
					via
				});
				TD.WriteTraceSource(17, description, serializedPayload);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000E29E File Offset: 0x0000C49E
		internal static bool InnerChannelOpenSucceededIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(18));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		internal static void InnerChannelOpenSucceeded(string endpointAddress, string via)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(18))
			{
				TD.WriteEtwEvent(18, null, endpointAddress, via, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("InnerChannelOpenSucceeded", TD.Culture), new object[]
				{
					endpointAddress,
					via
				});
				TD.WriteTraceSource(18, description, serializedPayload);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000E32F File Offset: 0x0000C52F
		internal static bool SynchronizationContextResetIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(19));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000E34C File Offset: 0x0000C54C
		internal static void SynchronizationContextReset(string synchronizationContextType)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(19))
			{
				TD.WriteEtwEvent(19, null, synchronizationContextType, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("SynchronizationContextReset", TD.Culture), new object[]
				{
					synchronizationContextType
				});
				TD.WriteTraceSource(19, description, serializedPayload);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000E3BA File Offset: 0x0000C5BA
		internal static bool SynchronizationContextSetToNullIsEnabled()
		{
			return FxTrace.ShouldTraceInformation && (FxTrace.ShouldTraceInformationToTraceSource || TD.IsEtwEventEnabled(20));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		internal static void SynchronizationContextSetToNull()
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null);
			if (TD.IsEtwEventEnabled(20))
			{
				TD.WriteEtwEvent(20, null, serializedPayload.AppDomainFriendlyName);
			}
			if (FxTrace.ShouldTraceInformationToTraceSource)
			{
				string description = string.Format(TD.Culture, TD.ResourceManager.GetString("SynchronizationContextSetToNull", TD.Culture), new object[0]);
				TD.WriteTraceSource(20, description, serializedPayload);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E444 File Offset: 0x0000C644
		private static void CreateEventDescriptors()
		{
			EventDescriptor[] array = new EventDescriptor[]
			{
				new EventDescriptor(4801, 0, 19, 3, 30, 2529, 1152921504606863360L),
				new EventDescriptor(4802, 0, 19, 4, 29, 2529, 1152921504606863360L),
				new EventDescriptor(4803, 0, 19, 4, 31, 2529, 1152921504606863360L),
				new EventDescriptor(4804, 0, 19, 4, 45, 2531, 1152921504606863360L),
				new EventDescriptor(4805, 0, 19, 3, 37, 2531, 1152921504606863360L),
				new EventDescriptor(4806, 0, 19, 3, 38, 2531, 1152921504606863360L),
				new EventDescriptor(4807, 0, 19, 3, 39, 2531, 1152921504606863360L),
				new EventDescriptor(4808, 0, 19, 3, 40, 2531, 1152921504606863360L),
				new EventDescriptor(4809, 0, 19, 3, 41, 2531, 1152921504606863360L),
				new EventDescriptor(4810, 0, 19, 3, 42, 2531, 1152921504606863360L),
				new EventDescriptor(4811, 0, 19, 3, 43, 2531, 1152921504606863360L),
				new EventDescriptor(4812, 0, 19, 3, 44, 2531, 1152921504606863360L),
				new EventDescriptor(4813, 0, 19, 3, 36, 2531, 1152921504606863360L),
				new EventDescriptor(4814, 0, 19, 4, 58, 2534, 1152921504606863360L),
				new EventDescriptor(4815, 0, 19, 4, 59, 2534, 1152921504606863360L),
				new EventDescriptor(4816, 0, 19, 5, 33, 2530, 1152921504606863360L),
				new EventDescriptor(4817, 0, 19, 3, 32, 2530, 1152921504606863360L),
				new EventDescriptor(4818, 0, 19, 3, 34, 2530, 1152921504606863360L),
				new EventDescriptor(4819, 0, 19, 4, 35, 2530, 1152921504606863360L),
				new EventDescriptor(4820, 0, 19, 4, 46, 2532, 1152921504606863360L),
				new EventDescriptor(4821, 0, 19, 4, 47, 2532, 1152921504606863360L)
			};
			ushort[] end2EndEvents = new ushort[]
			{
				4804,
				4805,
				4806,
				4807,
				4808,
				4809,
				4811,
				4812,
				4813
			};
			FxTrace.UpdateEventDefinitions(array, end2EndEvents);
			TD.eventDescriptors = array;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000E790 File Offset: 0x0000C990
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

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		private static bool IsEtwEventEnabled(int eventIndex)
		{
			if (FxTrace.Trace.IsEtwProviderEnabled)
			{
				TD.EnsureEventDescriptors();
				return FxTrace.IsEventEnabled(eventIndex);
			}
			return false;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000E803 File Offset: 0x0000CA03
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E827 File Offset: 0x0000CA27
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000E84A File Offset: 0x0000CA4A
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000E872 File Offset: 0x0000CA72
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000E898 File Offset: 0x0000CA98
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4, string eventParam5)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4, eventParam5);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000E8C2 File Offset: 0x0000CAC2
		private static void WriteTraceSource(int eventIndex, string description, TracePayload payload)
		{
			TD.EnsureEventDescriptors();
			FxTrace.Trace.WriteTraceSource(ref TD.eventDescriptors[eventIndex], description, payload);
		}

		// Token: 0x04000116 RID: 278
		private static ResourceManager resourceManager;

		// Token: 0x04000117 RID: 279
		private static CultureInfo resourceCulture;

		// Token: 0x04000118 RID: 280
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000119 RID: 281
		private static object syncLock = new object();

		// Token: 0x0400011A RID: 282
		private static volatile bool eventDescriptorsCreated;
	}
}
