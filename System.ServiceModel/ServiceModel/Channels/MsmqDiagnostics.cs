using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E1 RID: 2273
	internal static class MsmqDiagnostics
	{
		// Token: 0x06005677 RID: 22135 RVA: 0x0013D888 File Offset: 0x0013BA88
		public static void CannotPeekOnQueue(string formatName, Exception ex)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262226, SR.GetString("TraceCodeMsmqCannotPeekOnQueue"), new StringTraceRecord("QueueFormatName", formatName), null, ex);
			}
		}

		// Token: 0x06005678 RID: 22136 RVA: 0x0013D8B4 File Offset: 0x0013BAB4
		public static void CannotReadQueues(string host, bool publicQueues, Exception ex)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
				dictionary["Host"] = host;
				dictionary["PublicQueues"] = Convert.ToString(publicQueues, CultureInfo.InvariantCulture);
				TraceUtility.TraceEvent(TraceEventType.Warning, 262227, SR.GetString("TraceCodeMsmqCannotReadQueues"), new DictionaryTraceRecord(dictionary), null, ex);
			}
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x0013D910 File Offset: 0x0013BB10
		public static ServiceModelActivity StartListenAtActivity(MsmqReceiveHelper receiver)
		{
			ServiceModelActivity serviceModelActivity = receiver.Activity;
			if (DiagnosticUtility.ShouldUseActivity && serviceModelActivity == null)
			{
				serviceModelActivity = ServiceModelActivity.CreateActivity(true);
				if (FxTrace.Trace != null)
				{
					FxTrace.Trace.TraceTransfer(serviceModelActivity.Id);
				}
				ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityListenAt", new object[]
				{
					receiver.ListenUri.ToString()
				}), ActivityType.ListenAt);
			}
			return serviceModelActivity;
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x0013D972 File Offset: 0x0013BB72
		public static Activity BoundOpenOperation(MsmqReceiveHelper receiver)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 524342, SR.GetString("TraceCodeTransportListen", new object[]
				{
					receiver.ListenUri.ToString()
				}), receiver);
			}
			return ServiceModelActivity.BoundOperation(receiver.Activity);
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x0013D9B0 File Offset: 0x0013BBB0
		public static Activity BoundReceiveOperation(MsmqReceiveHelper receiver)
		{
			if (DiagnosticUtility.ShouldUseActivity && ServiceModelActivity.Current != null && ActivityType.ProcessAction != ServiceModelActivity.Current.ActivityType)
			{
				return ServiceModelActivity.BoundOperation(receiver.Activity);
			}
			return null;
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x0013D9DC File Offset: 0x0013BBDC
		public static ServiceModelActivity BoundDecodeOperation()
		{
			ServiceModelActivity serviceModelActivity = null;
			if (DiagnosticUtility.ShouldUseActivity)
			{
				serviceModelActivity = ServiceModelActivity.CreateBoundedActivity(true);
				ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
				{
					TraceUtility.RetrieveMessageNumber()
				}), ActivityType.ProcessMessage);
			}
			return serviceModelActivity;
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x0013DA20 File Offset: 0x0013BC20
		public static ServiceModelActivity BoundReceiveBytesOperation()
		{
			ServiceModelActivity serviceModelActivity = null;
			if (DiagnosticUtility.ShouldUseActivity)
			{
				serviceModelActivity = ServiceModelActivity.CreateBoundedActivityWithTransferInOnly(Guid.NewGuid());
				ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityReceiveBytes", new object[]
				{
					TraceUtility.RetrieveMessageNumber()
				}), ActivityType.ReceiveBytes);
			}
			return serviceModelActivity;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x0013DA67 File Offset: 0x0013BC67
		public static void TransferFromTransport(Message message)
		{
			if (DiagnosticUtility.ShouldUseActivity)
			{
				TraceUtility.TransferFromTransport(message);
			}
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x0013DA76 File Offset: 0x0013BC76
		public static void ExpectedException(Exception ex)
		{
			DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x0013DA7F File Offset: 0x0013BC7F
		public static void ScanStarted()
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262248, SR.GetString("TraceCodeMsmqScanStarted"), null, null, null);
			}
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0013DAA4 File Offset: 0x0013BCA4
		public static void MatchedApplicationFound(string host, string queueName, bool isPrivate, string canonicalPath)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				dictionary["Host"] = host;
				dictionary["QueueName"] = queueName;
				dictionary["Private"] = Convert.ToString(isPrivate, CultureInfo.InvariantCulture);
				dictionary["CanonicalPath"] = canonicalPath;
				TraceUtility.TraceEvent(TraceEventType.Information, 262235, SR.GetString("TraceCodeMsmqMatchedApplicationFound"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0013DB16 File Offset: 0x0013BD16
		public static void StartingApplication(string application)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262251, SR.GetString("TraceCodeMsmqStartingApplication"), new StringTraceRecord("Application", application), null, null);
			}
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x0013DB44 File Offset: 0x0013BD44
		public static void StartingService(string host, string name, bool isPrivate, string processedVirtualPath)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				dictionary["Host"] = host;
				dictionary["Name"] = name;
				dictionary["Private"] = Convert.ToString(isPrivate, CultureInfo.InvariantCulture);
				dictionary["VirtualPath"] = processedVirtualPath;
				TraceUtility.TraceEvent(TraceEventType.Information, 262252, SR.GetString("TraceCodeMsmqStartingService"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x0013DBB8 File Offset: 0x0013BDB8
		public static void FoundBaseAddress(Uri uri, string virtualPath)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(2)
				{
					{
						"Uri",
						uri.ToString()
					},
					{
						"VirtualPath",
						virtualPath
					}
				};
				TraceUtility.TraceEvent(TraceEventType.Information, 262233, SR.GetString("TraceCodeMsmqFoundBaseAddress"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x0013DC10 File Offset: 0x0013BE10
		private static void DatagramSentOrReceived(NativeMsmqMessage.BufferProperty messageId, Message message, int traceCode, string traceDescription)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Guid guid = MsmqDiagnostics.MessageIdToGuid(messageId);
				UniqueId messageId2 = message.Headers.MessageId;
				TraceRecord extendedData;
				if (null == messageId2)
				{
					extendedData = new StringTraceRecord("MSMQMessageId", guid.ToString());
				}
				else
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(2)
					{
						{
							"MSMQMessageId",
							guid.ToString()
						},
						{
							"WCFMessageId",
							messageId2.ToString()
						}
					};
					extendedData = new DictionaryTraceRecord(dictionary);
				}
				TraceUtility.TraceEvent(TraceEventType.Verbose, traceCode, traceDescription, extendedData, null, null);
			}
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x0013DC9F File Offset: 0x0013BE9F
		public static void DatagramReceived(NativeMsmqMessage.BufferProperty messageId, Message message)
		{
			MsmqDiagnostics.DatagramSentOrReceived(messageId, message, 262229, SR.GetString("TraceCodeMsmqDatagramReceived"));
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x0013DCB7 File Offset: 0x0013BEB7
		public static void DatagramSent(NativeMsmqMessage.BufferProperty messageId, Message message)
		{
			MsmqDiagnostics.DatagramSentOrReceived(messageId, message, 262228, SR.GetString("TraceCodeMsmqDatagramSent"));
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x0013DCD0 File Offset: 0x0013BED0
		private static Guid MessageIdToGuid(NativeMsmqMessage.BufferProperty messageId)
		{
			int num = messageId.Buffer.Length;
			byte[] array = new byte[16];
			Buffer.BlockCopy(messageId.Buffer, 4, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x0013DD08 File Offset: 0x0013BF08
		public static void MessageConsumed(string uri, string messageId, bool rejected)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, rejected ? 262238 : 262236, rejected ? SR.GetString("TraceCodeMsmqMessageRejected") : SR.GetString("TraceCodeMsmqMessageDropped"), new StringTraceRecord("MSMQMessageId", messageId), null, null);
			}
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				if (rejected)
				{
					PerformanceCounters.MsmqRejectedMessage(uri);
					return;
				}
				PerformanceCounters.MsmqDroppedMessage(uri);
			}
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x0013DD6E File Offset: 0x0013BF6E
		public static void MessageLockedUnderTheTransaction(long lookupId)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262237, SR.GetString("TraceCodeMsmqMessageLockedUnderTheTransaction"), new StringTraceRecord("MSMQMessageLookupId", Convert.ToString(lookupId, CultureInfo.InvariantCulture)), null, null);
			}
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x0013DDA3 File Offset: 0x0013BFA3
		public static void MoveOrDeleteAttemptFailed(long lookupId)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262239, SR.GetString("TraceCodeMsmqMoveOrDeleteAttemptFailed"), new StringTraceRecord("MSMQMessageLookupId", Convert.ToString(lookupId, CultureInfo.InvariantCulture)), null, null);
			}
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x0013DDD8 File Offset: 0x0013BFD8
		public static void MsmqDetected(Version version)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262230, SR.GetString("TraceCodeMsmqDetected"), new StringTraceRecord("MSMQVersion", version.ToString()), null, null);
			}
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x0013DE08 File Offset: 0x0013C008
		public static void PoisonMessageMoved(string messageId, bool poisonQueue, string uri)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, poisonQueue ? 262240 : 262241, poisonQueue ? SR.GetString("TraceCodeMsmqPoisonMessageMovedPoison") : SR.GetString("TraceCodeMsmqPoisonMessageMovedRetry"), new StringTraceRecord("MSMQMessageId", messageId), null, null);
			}
			if (poisonQueue && PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.MsmqPoisonMessage(uri);
			}
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x0013DE67 File Offset: 0x0013C067
		public static void PoisonMessageRejected(string messageId, string uri)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262242, SR.GetString("TraceCodeMsmqPoisonMessageRejected"), new StringTraceRecord("MSMQMessageId", messageId), null, null);
			}
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.MsmqPoisonMessage(uri);
			}
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x0013DE9F File Offset: 0x0013C09F
		public static void PoolFull(int poolSize)
		{
			if (DiagnosticUtility.ShouldTraceInformation && !MsmqDiagnostics.poolFullReported)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262243, SR.GetString("TraceCodeMsmqPoolFull"), null, null, null);
				MsmqDiagnostics.poolFullReported = true;
			}
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x0013DECD File Offset: 0x0013C0CD
		public static void PotentiallyPoisonMessageDetected(string messageId)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262244, SR.GetString("TraceCodeMsmqPotentiallyPoisonMessageDetected"), new StringTraceRecord("MSMQMessageId", messageId), null, null);
			}
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0013DEF8 File Offset: 0x0013C0F8
		public static void QueueClosed(string formatName)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262245, SR.GetString("TraceCodeMsmqQueueClosed"), new StringTraceRecord("FormatName", formatName), null, null);
			}
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x0013DF23 File Offset: 0x0013C123
		public static void QueueOpened(string formatName)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262246, SR.GetString("TraceCodeMsmqQueueOpened"), new StringTraceRecord("FormatName", formatName), null, null);
			}
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x0013DF4E File Offset: 0x0013C14E
		public static void QueueTransactionalStatusUnknown(string formatName)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262247, SR.GetString("TraceCodeMsmqQueueTransactionalStatusUnknown"), new StringTraceRecord("FormatName", formatName), null, null);
			}
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x0013DF7C File Offset: 0x0013C17C
		public static void SessiongramSent(string sessionId, NativeMsmqMessage.BufferProperty messageId, int numberOfMessages)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(3);
				dictionary["SessionId"] = sessionId;
				dictionary["MSMQMessageId"] = MsmqMessageId.ToString(messageId.Buffer);
				dictionary["NumberOfMessages"] = Convert.ToString(numberOfMessages, CultureInfo.InvariantCulture);
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262250, SR.GetString("TraceCodeMsmqSessiongramSent"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0013DFF0 File Offset: 0x0013C1F0
		public static void SessiongramReceived(string sessionId, NativeMsmqMessage.BufferProperty messageId, int numberOfMessages)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(3);
				dictionary["SessionId"] = sessionId;
				dictionary["MSMQMessageId"] = MsmqMessageId.ToString(messageId.Buffer);
				dictionary["NumberOfMessages"] = Convert.ToString(numberOfMessages, CultureInfo.InvariantCulture);
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262249, SR.GetString("TraceCodeMsmqSessiongramReceived"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x0013E064 File Offset: 0x0013C264
		public static void UnexpectedAcknowledgment(string messageId, int acknowledgment)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
				dictionary["MSMQMessageId"] = messageId;
				dictionary["Acknowledgment"] = Convert.ToString(acknowledgment, CultureInfo.InvariantCulture);
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262253, SR.GetString("TraceCodeMsmqUnexpectedAcknowledgment"), new DictionaryTraceRecord(dictionary), null, null);
			}
		}

		// Token: 0x04003571 RID: 13681
		private static bool poolFullReported;
	}
}
