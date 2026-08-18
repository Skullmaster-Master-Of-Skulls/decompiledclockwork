using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A80 RID: 2688
	internal static class MessageLogger
	{
		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x0018AA15 File Offset: 0x00188C15
		private static int FilterCount
		{
			get
			{
				return MessageLogger.Filters.Count;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x0018AA21 File Offset: 0x00188C21
		private static bool FilterMessages
		{
			get
			{
				return MessageLogger.FilterCount > 0 && (MessageLogger.numberOfMessagesToLog > 0 || MessageLogger.numberOfMessagesToLog == -1);
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x0018AA3F File Offset: 0x00188C3F
		// (set) Token: 0x060069F6 RID: 27126 RVA: 0x0018AA46 File Offset: 0x00188C46
		internal static bool LogKnownPii
		{
			get
			{
				return MessageLogger.logKnownPii;
			}
			set
			{
				MessageLogger.logKnownPii = value;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x060069F7 RID: 27127 RVA: 0x0018AA4E File Offset: 0x00188C4E
		// (set) Token: 0x060069F8 RID: 27128 RVA: 0x0018AA60 File Offset: 0x00188C60
		internal static bool LogMalformedMessages
		{
			get
			{
				return (MessageLogger.Sources & MessageLoggingSource.Malformed) > MessageLoggingSource.None;
			}
			set
			{
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					bool flag2 = MessageLogger.ShouldProcessAudit(MessageLoggingSource.Malformed, value);
					if (value)
					{
						MessageLogger.EnsureMessageTraceSource();
						if (!MessageLogger.inPartialTrust)
						{
							MessageLogger.sources |= MessageLoggingSource.Malformed;
						}
					}
					else
					{
						MessageLogger.sources &= (MessageLoggingSource)2147482623;
					}
					if (flag2)
					{
						MessageLogger.ProcessAudit(value);
					}
				}
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x060069F9 RID: 27129 RVA: 0x0018AAE0 File Offset: 0x00188CE0
		// (set) Token: 0x060069FA RID: 27130 RVA: 0x0018AAF0 File Offset: 0x00188CF0
		internal static bool LogMessagesAtServiceLevel
		{
			get
			{
				return (MessageLogger.Sources & MessageLoggingSource.ServiceLevel) > MessageLoggingSource.None;
			}
			set
			{
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					bool flag2 = MessageLogger.ShouldProcessAudit(MessageLoggingSource.ServiceLevel, value);
					if (value)
					{
						MessageLogger.EnsureMessageTraceSource();
						if (!MessageLogger.inPartialTrust)
						{
							MessageLogger.sources |= MessageLoggingSource.ServiceLevel;
						}
					}
					else
					{
						MessageLogger.sources &= (MessageLoggingSource)2147482639;
					}
					if (flag2)
					{
						MessageLogger.ProcessAudit(value);
					}
				}
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x060069FB RID: 27131 RVA: 0x0018AB70 File Offset: 0x00188D70
		// (set) Token: 0x060069FC RID: 27132 RVA: 0x0018AB7C File Offset: 0x00188D7C
		internal static bool LogMessagesAtTransportLevel
		{
			get
			{
				return (MessageLogger.Sources & MessageLoggingSource.Transport) > MessageLoggingSource.None;
			}
			set
			{
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					bool flag2 = MessageLogger.ShouldProcessAudit(MessageLoggingSource.Transport, value);
					if (value)
					{
						MessageLogger.EnsureMessageTraceSource();
						if (!MessageLogger.inPartialTrust)
						{
							MessageLogger.sources |= MessageLoggingSource.Transport;
						}
					}
					else
					{
						MessageLogger.sources &= (MessageLoggingSource)2147483641;
					}
					if (flag2)
					{
						MessageLogger.ProcessAudit(value);
					}
				}
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x060069FD RID: 27133 RVA: 0x0018ABF4 File Offset: 0x00188DF4
		// (set) Token: 0x060069FE RID: 27134 RVA: 0x0018ABFB File Offset: 0x00188DFB
		internal static bool LogMessageBody
		{
			get
			{
				return MessageLogger.logMessageBody;
			}
			set
			{
				MessageLogger.logMessageBody = value;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x060069FF RID: 27135 RVA: 0x0018AC03 File Offset: 0x00188E03
		internal static bool LoggingEnabled
		{
			get
			{
				return MessageLogger.Sources > MessageLoggingSource.None;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06006A00 RID: 27136 RVA: 0x0018AC0D File Offset: 0x00188E0D
		// (set) Token: 0x06006A01 RID: 27137 RVA: 0x0018AC14 File Offset: 0x00188E14
		internal static int MaxMessageSize
		{
			get
			{
				return MessageLogger.maxMessageSize;
			}
			set
			{
				MessageLogger.maxMessageSize = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06006A02 RID: 27138 RVA: 0x0018AC1C File Offset: 0x00188E1C
		// (set) Token: 0x06006A03 RID: 27139 RVA: 0x0018AC24 File Offset: 0x00188E24
		internal static int MaxNumberOfMessagesToLog
		{
			get
			{
				return MessageLogger.maxMessagesToLog;
			}
			set
			{
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					MessageLogger.maxMessagesToLog = value;
					MessageLogger.numberOfMessagesToLog = MessageLogger.maxMessagesToLog;
				}
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06006A04 RID: 27140 RVA: 0x0018AC70 File Offset: 0x00188E70
		private static List<XPathMessageFilter> Filters
		{
			get
			{
				if (MessageLogger.messageFilterTable == null)
				{
					object obj = MessageLogger.filterLock;
					lock (obj)
					{
						if (MessageLogger.messageFilterTable == null)
						{
							List<XPathMessageFilter> list = new List<XPathMessageFilter>();
							MessageLogger.messageFilterTable = list;
						}
					}
				}
				return MessageLogger.messageFilterTable;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06006A05 RID: 27141 RVA: 0x0018ACC8 File Offset: 0x00188EC8
		private static MessageLoggingSource Sources
		{
			get
			{
				if (!MessageLogger.initialized)
				{
					MessageLogger.EnsureInitialized();
				}
				return MessageLogger.sources;
			}
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x0018ACDB File Offset: 0x00188EDB
		private static bool AddFilter(XPathMessageFilter filter)
		{
			if (filter == null)
			{
				filter = new XPathMessageFilter("");
			}
			MessageLogger.Filters.Add(filter);
			return true;
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06006A07 RID: 27143 RVA: 0x0018ACF8 File Offset: 0x00188EF8
		internal static bool ShouldLogMalformed
		{
			get
			{
				return MessageLogger.ShouldLogMessages(MessageLoggingSource.Malformed);
			}
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0018AD04 File Offset: 0x00188F04
		private static bool ShouldLogMessages(MessageLoggingSource source)
		{
			return (source & MessageLogger.Sources) != MessageLoggingSource.None && (MessageLogger.MessageTraceSource != null || ((source & MessageLoggingSource.Malformed) != MessageLoggingSource.None && TD.MessageLogWarningIsEnabled()) || TD.MessageLogInfoIsEnabled());
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0018AD30 File Offset: 0x00188F30
		internal static void LogMessage(MessageLoggingSource source, string data)
		{
			try
			{
				if (MessageLogger.ShouldLogMessages(MessageLoggingSource.Malformed))
				{
					MessageLogger.LogInternal(source, data);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				MessageLogger.FailedToLogMessage(ex);
			}
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x0018AD78 File Offset: 0x00188F78
		internal static void LogMessage(Stream stream, MessageLoggingSource source)
		{
			try
			{
				MessageLogger.ThrowIfNotMalformed(source);
				if (MessageLogger.ShouldLogMessages(source))
				{
					MessageLogger.LogInternal(new MessageLogTraceRecord(stream, source));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				MessageLogger.FailedToLogMessage(ex);
			}
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x0018ADC4 File Offset: 0x00188FC4
		internal static void LogMessage(ArraySegment<byte> buffer, MessageLoggingSource source)
		{
			try
			{
				MessageLogger.ThrowIfNotMalformed(source);
				if (MessageLogger.ShouldLogMessages(source))
				{
					MessageLogger.LogInternal(new MessageLogTraceRecord(buffer, source));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				MessageLogger.FailedToLogMessage(ex);
			}
		}

		// Token: 0x06006A0C RID: 27148 RVA: 0x0018AE10 File Offset: 0x00189010
		internal static void LogMessage(ref Message message, XmlReader reader, MessageLoggingSource source)
		{
			try
			{
				if (MessageLogger.ShouldLogMessages(source))
				{
					MessageLogger.LogMessageImpl(ref message, reader, source);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				MessageLogger.FailedToLogMessage(ex);
			}
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x0018AE54 File Offset: 0x00189054
		internal static void LogMessage(ref Message message, MessageLoggingSource source)
		{
			MessageLogger.LogMessage(ref message, null, source);
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0018AE60 File Offset: 0x00189060
		private static void LogMessageImpl(ref Message message, XmlReader reader, MessageLoggingSource source)
		{
			ServiceModelActivity activity = DiagnosticUtility.ShouldUseActivity ? TraceUtility.ExtractActivity(message) : null;
			using (ServiceModelActivity.BoundOperation(activity))
			{
				if (MessageLogger.ShouldLogMessages(source) && (MessageLogger.numberOfMessagesToLog > 0 || MessageLogger.numberOfMessagesToLog == -1))
				{
					bool flag = (source & MessageLoggingSource.LastChance) != MessageLoggingSource.None || (source & MessageLoggingSource.TransportSend) > MessageLoggingSource.None;
					source &= ~MessageLoggingSource.LastChance;
					if ((flag || message is NullMessage || message.Version.Addressing != AddressingVersion.None) && MessageLogger.MatchFilters(message, source) && (MessageLogger.numberOfMessagesToLog == -1 || MessageLogger.numberOfMessagesToLog > 0))
					{
						MessageLogTraceRecord record = new MessageLogTraceRecord(ref message, reader, source, MessageLogger.LogMessageBody);
						MessageLogger.LogInternal(record);
					}
				}
			}
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x0018AF24 File Offset: 0x00189124
		private static bool HasSecurityAction(Message message)
		{
			string action = message.Headers.Action;
			bool result = false;
			if (string.IsNullOrEmpty(action))
			{
				result = true;
			}
			else
			{
				foreach (string strB in MessageLogger.SecurityActions)
				{
					if (string.CompareOrdinal(action, strB) == 0)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x0018AF74 File Offset: 0x00189174
		private static void LogInternal(MessageLogTraceRecord record)
		{
			PlainXmlWriter plainXmlWriter = new PlainXmlWriter(MessageLogger.MaxMessageSize);
			try
			{
				record.WriteTo(plainXmlWriter);
				plainXmlWriter.Close();
				TraceXPathNavigator navigator = plainXmlWriter.Navigator;
				if ((MessageLogger.messageTraceSource != null && !MessageLogger.messageTraceSource.ShouldLogPii) || !MessageLogger.LogKnownPii)
				{
					navigator.RemovePii(MessageLogger.PiiHeadersPaths);
					if (MessageLogger.LogMessageBody && record.Message != null && MessageLogger.HasSecurityAction(record.Message))
					{
						navigator.RemovePii(MessageLogger.PiiBodyPaths);
					}
				}
				MessageLogger.LogInternal(record.MessageLoggingSource, navigator);
			}
			catch (PlainXmlWriter.MaxSizeExceededException)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 131083, SR.GetString("TraceCodeMessageNotLoggedQuotaExceeded"), record.Message);
				}
			}
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x0018B030 File Offset: 0x00189230
		private static void IncrementLoggedMessagesCount(object data)
		{
			if (MessageLogger.numberOfMessagesToLog > 0)
			{
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					if (MessageLogger.numberOfMessagesToLog > 0)
					{
						MessageLogger.numberOfMessagesToLog--;
						if (MessageLogger.numberOfMessagesToLog == 0 && DiagnosticUtility.ShouldTraceInformation)
						{
							TraceUtility.TraceEvent(TraceEventType.Information, 131081, SR.GetString("TraceCodeMessageCountLimitExceeded"), data);
						}
					}
				}
			}
			object obj2 = MessageLogger.syncObject;
			lock (obj2)
			{
				if (!MessageLogger.lastWriteSucceeded)
				{
					MessageLogger.lastWriteSucceeded = true;
				}
			}
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x0018B0E0 File Offset: 0x001892E0
		private static void FailedToLogMessage(Exception e)
		{
			bool flag = false;
			object obj = MessageLogger.syncObject;
			lock (obj)
			{
				if (MessageLogger.lastWriteSucceeded)
				{
					MessageLogger.lastWriteSucceeded = false;
					flag = true;
				}
			}
			if (flag)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 7, 3221356549U, new string[]
				{
					e.ToString()
				});
			}
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x0018B150 File Offset: 0x00189350
		private static void LogInternal(MessageLoggingSource source, object data)
		{
			if ((source & MessageLoggingSource.Malformed) != MessageLoggingSource.None)
			{
				if (TD.MessageLogWarningIsEnabled() && !TD.MessageLogWarning(data.ToString()) && TD.MessageLogEventSizeExceededIsEnabled())
				{
					TD.MessageLogEventSizeExceeded();
				}
			}
			else if (TD.MessageLogInfoIsEnabled() && !TD.MessageLogInfo(data.ToString()) && TD.MessageLogEventSizeExceededIsEnabled())
			{
				TD.MessageLogEventSizeExceeded();
			}
			if (MessageLogger.MessageTraceSource != null)
			{
				MessageLogger.MessageTraceSource.TraceData(TraceEventType.Information, 0, data);
			}
			MessageLogger.IncrementLoggedMessagesCount(data);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x0018B1C4 File Offset: 0x001893C4
		private static bool MatchFilters(Message message, MessageLoggingSource source)
		{
			bool result = true;
			if (MessageLogger.FilterMessages && (source & MessageLoggingSource.Malformed) == MessageLoggingSource.None)
			{
				result = false;
				List<XPathMessageFilter> list = new List<XPathMessageFilter>();
				object obj = MessageLogger.syncObject;
				lock (obj)
				{
					foreach (XPathMessageFilter xpathMessageFilter in MessageLogger.Filters)
					{
						try
						{
							if (xpathMessageFilter.Match(message))
							{
								result = true;
								break;
							}
						}
						catch (FilterInvalidBodyAccessException)
						{
							list.Add(xpathMessageFilter);
						}
						catch (MessageFilterException exception)
						{
							if (DiagnosticUtility.ShouldTraceInformation)
							{
								TraceUtility.TraceEvent(TraceEventType.Information, 131080, SR.GetString("TraceCodeFilterNotMatchedNodeQuotaExceeded"), exception, message);
							}
						}
					}
					foreach (XPathMessageFilter xpathMessageFilter2 in list)
					{
						MessageLogger.Filters.Remove(xpathMessageFilter2);
						PlainXmlWriter plainXmlWriter = new PlainXmlWriter();
						xpathMessageFilter2.WriteXPathTo(plainXmlWriter, null, "filter", null, true);
						DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 7, 3221356550U, new string[]
						{
							plainXmlWriter.Navigator.ToString()
						});
					}
					if (MessageLogger.FilterCount == 0)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x0018B33C File Offset: 0x0018953C
		private static void ReadFiltersFromConfig(DiagnosticSection section)
		{
			for (int i = 0; i < section.MessageLogging.Filters.Count; i++)
			{
				XPathMessageFilterElement xpathMessageFilterElement = section.MessageLogging.Filters[i];
				MessageLogger.AddFilter(xpathMessageFilterElement.Filter);
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06006A16 RID: 27158 RVA: 0x0018B382 File Offset: 0x00189582
		internal static TraceSource MessageTraceSource
		{
			get
			{
				return MessageLogger.messageTraceSource;
			}
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x0018B38C File Offset: 0x0018958C
		internal static void EnsureInitialized()
		{
			object obj = MessageLogger.syncObject;
			lock (obj)
			{
				if (!MessageLogger.initialized && !MessageLogger.initializing)
				{
					try
					{
						MessageLogger.Initialize();
					}
					catch (SecurityException ex)
					{
						MessageLogger.inPartialTrust = true;
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 131076, SR.GetString("PartialTrustMessageLoggingNotEnabled"), null, ex);
						}
						MessageLogger.LogNonFatalInitializationException(new SecurityException(SR.GetString("PartialTrustMessageLoggingNotEnabled"), ex));
					}
					MessageLogger.initialized = true;
				}
			}
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x0018B428 File Offset: 0x00189628
		private static void EnsureMessageTraceSource()
		{
			if (!MessageLogger.initialized)
			{
				MessageLogger.EnsureInitialized();
			}
			if (MessageLogger.MessageTraceSource == null && !MessageLogger.attemptedTraceSourceInitialization)
			{
				MessageLogger.InitializeMessageTraceSource();
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06006A19 RID: 27161 RVA: 0x0018B44C File Offset: 0x0018964C
		private static string[][] PiiBodyPaths
		{
			get
			{
				if (MessageLogger.piiBodyPaths == null)
				{
					MessageLogger.piiBodyPaths = new string[][]
					{
						new string[]
						{
							"MessageLogTraceRecord",
							"Envelope",
							"Body",
							"RequestSecurityToken"
						},
						new string[]
						{
							"MessageLogTraceRecord",
							"Envelope",
							"Body",
							"RequestSecurityTokenResponse"
						},
						new string[]
						{
							"MessageLogTraceRecord",
							"Envelope",
							"Body",
							"RequestSecurityTokenResponseCollection"
						}
					};
				}
				return MessageLogger.piiBodyPaths;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x0018B4F0 File Offset: 0x001896F0
		private static string[][] PiiHeadersPaths
		{
			get
			{
				if (MessageLogger.piiHeadersPaths == null)
				{
					MessageLogger.piiHeadersPaths = new string[][]
					{
						new string[]
						{
							"MessageLogTraceRecord",
							"Envelope",
							"Header",
							"Security"
						},
						new string[]
						{
							"MessageLogTraceRecord",
							"Envelope",
							"Header",
							"IssuedTokens"
						}
					};
				}
				return MessageLogger.piiHeadersPaths;
			}
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06006A1B RID: 27163 RVA: 0x0018B568 File Offset: 0x00189768
		private static string[] SecurityActions
		{
			get
			{
				if (MessageLogger.securityActions == null)
				{
					MessageLogger.securityActions = new string[]
					{
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Renew",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Cancel",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Validate",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Amend",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Amend",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Renew",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Renew",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/SCT/Cancel",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/SCT/Cancel",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RST/KET",
						"http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/KET",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/SCT-Amend",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/SCT-Amend",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Issue",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Issue",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Renew",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Renew",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Validate",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Validate",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/KET",
						"http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/KET"
					};
				}
				return MessageLogger.securityActions;
			}
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x0018B698 File Offset: 0x00189898
		[SecuritySafeCritical]
		private static void Initialize()
		{
			MessageLogger.initializing = true;
			DiagnosticSection diagnosticSection = DiagnosticSection.UnsafeGetSection();
			if (diagnosticSection != null)
			{
				MessageLogger.LogKnownPii = (diagnosticSection.MessageLogging.LogKnownPii && MachineSettingsSection.EnableLoggingKnownPii);
				MessageLogger.LogMalformedMessages = diagnosticSection.MessageLogging.LogMalformedMessages;
				MessageLogger.LogMessageBody = diagnosticSection.MessageLogging.LogEntireMessage;
				MessageLogger.LogMessagesAtServiceLevel = diagnosticSection.MessageLogging.LogMessagesAtServiceLevel;
				MessageLogger.LogMessagesAtTransportLevel = diagnosticSection.MessageLogging.LogMessagesAtTransportLevel;
				MessageLogger.MaxNumberOfMessagesToLog = diagnosticSection.MessageLogging.MaxMessagesToLog;
				MessageLogger.MaxMessageSize = diagnosticSection.MessageLogging.MaxSizeOfMessageToLog;
				MessageLogger.ReadFiltersFromConfig(diagnosticSection);
			}
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x0018B738 File Offset: 0x00189938
		private static void InitializeMessageTraceSource()
		{
			try
			{
				MessageLogger.attemptedTraceSourceInitialization = true;
				PiiTraceSource piiTraceSource = new PiiTraceSource("System.ServiceModel.MessageLogging", "System.ServiceModel 4.0.0.0");
				piiTraceSource.Switch.Level = SourceLevels.Information;
				piiTraceSource.Listeners.Remove("Default");
				if (piiTraceSource.Listeners.Count > 0)
				{
					AppDomain.CurrentDomain.DomainUnload += MessageLogger.ExitOrUnloadEventHandler;
					AppDomain.CurrentDomain.ProcessExit += MessageLogger.ExitOrUnloadEventHandler;
					AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MessageLogger.ExitOrUnloadEventHandler);
				}
				else
				{
					piiTraceSource = null;
				}
				MessageLogger.messageTraceSource = piiTraceSource;
			}
			catch (ConfigurationErrorsException)
			{
				throw;
			}
			catch (SecurityException ex)
			{
				MessageLogger.inPartialTrust = true;
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 131076, SR.GetString("PartialTrustMessageLoggingNotEnabled"), null, ex);
				}
				MessageLogger.LogNonFatalInitializationException(new SecurityException(SR.GetString("PartialTrustMessageLoggingNotEnabled"), ex));
			}
			catch (Exception ex2)
			{
				MessageLogger.messageTraceSource = null;
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				MessageLogger.LogNonFatalInitializationException(ex2);
			}
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x0018B854 File Offset: 0x00189A54
		[SecuritySafeCritical]
		private static void LogNonFatalInitializationException(Exception e)
		{
			DiagnosticUtility.UnsafeEventLog.UnsafeLogEvent(TraceEventType.Critical, 7, 3221356551U, true, new string[]
			{
				e.ToString()
			});
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x0018B884 File Offset: 0x00189A84
		private static void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			object obj = MessageLogger.syncObject;
			lock (obj)
			{
				if (MessageLogger.MessageTraceSource != null)
				{
					MessageLogger.MessageTraceSource.Close();
					MessageLogger.messageTraceSource = null;
				}
			}
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x0018B8D4 File Offset: 0x00189AD4
		private static void ThrowIfNotMalformed(MessageLoggingSource source)
		{
			if ((source & MessageLoggingSource.Malformed) == MessageLoggingSource.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("OnlyMalformedMessagesAreSupported"), "source"));
			}
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x0018B8FE File Offset: 0x00189AFE
		private static void ProcessAudit(bool turningOn)
		{
			if (turningOn)
			{
				if (MessageLogger.messageTraceSource != null)
				{
					DiagnosticUtility.EventLog.LogEvent(TraceEventType.Information, 7, 3221356552U, new string[0]);
					return;
				}
			}
			else
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Information, 7, 3221356553U, new string[0]);
			}
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x0018B93C File Offset: 0x00189B3C
		private static bool ShouldProcessAudit(MessageLoggingSource source, bool turningOn)
		{
			bool result;
			if (turningOn)
			{
				result = (MessageLogger.sources == MessageLoggingSource.None);
			}
			else
			{
				result = (MessageLogger.sources == source);
			}
			return result;
		}

		// Token: 0x04003C76 RID: 15478
		private const string MessageTraceSourceName = "System.ServiceModel.MessageLogging";

		// Token: 0x04003C77 RID: 15479
		private const string DefaultTraceListenerName = "Default";

		// Token: 0x04003C78 RID: 15480
		private const int Unlimited = -1;

		// Token: 0x04003C79 RID: 15481
		private static MessageLoggingSource sources = MessageLoggingSource.None;

		// Token: 0x04003C7A RID: 15482
		private static bool logKnownPii;

		// Token: 0x04003C7B RID: 15483
		private static bool logMessageBody = false;

		// Token: 0x04003C7C RID: 15484
		private static int maxMessagesToLog;

		// Token: 0x04003C7D RID: 15485
		private static int numberOfMessagesToLog;

		// Token: 0x04003C7E RID: 15486
		private static int maxMessageSize;

		// Token: 0x04003C7F RID: 15487
		private static PiiTraceSource messageTraceSource;

		// Token: 0x04003C80 RID: 15488
		private static bool attemptedTraceSourceInitialization = false;

		// Token: 0x04003C81 RID: 15489
		private static bool initialized = false;

		// Token: 0x04003C82 RID: 15490
		private static bool initializing = false;

		// Token: 0x04003C83 RID: 15491
		private static bool inPartialTrust = false;

		// Token: 0x04003C84 RID: 15492
		private static object syncObject = new object();

		// Token: 0x04003C85 RID: 15493
		private static object filterLock = new object();

		// Token: 0x04003C86 RID: 15494
		private static List<XPathMessageFilter> messageFilterTable;

		// Token: 0x04003C87 RID: 15495
		private static bool lastWriteSucceeded = true;

		// Token: 0x04003C88 RID: 15496
		private static string[][] piiBodyPaths;

		// Token: 0x04003C89 RID: 15497
		private static string[][] piiHeadersPaths;

		// Token: 0x04003C8A RID: 15498
		private static string[] securityActions;
	}
}
