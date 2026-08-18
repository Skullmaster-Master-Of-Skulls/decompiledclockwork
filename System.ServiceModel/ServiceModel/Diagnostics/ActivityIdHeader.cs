using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A70 RID: 2672
	internal class ActivityIdHeader : DictionaryHeader
	{
		// Token: 0x0600695D RID: 26973 RVA: 0x00189383 File Offset: 0x00187583
		internal ActivityIdHeader(Guid activityId)
		{
			this.guid = activityId;
			this.headerId = Guid.NewGuid();
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x0600695E RID: 26974 RVA: 0x0018939D File Offset: 0x0018759D
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.ActivityIdFlowDictionary.ActivityId;
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x0600695F RID: 26975 RVA: 0x001893A9 File Offset: 0x001875A9
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return XD.ActivityIdFlowDictionary.ActivityIdNamespace;
			}
		}

		// Token: 0x06006960 RID: 26976 RVA: 0x001893B8 File Offset: 0x001875B8
		internal static Guid ExtractActivityId(Message message)
		{
			Guid result = Guid.Empty;
			try
			{
				if (message != null && message.State != MessageState.Closed && message.Headers != null)
				{
					int num = message.Headers.FindHeader("ActivityId", "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics");
					if (num >= 0)
					{
						using (XmlDictionaryReader readerAtHeader = message.Headers.GetReaderAtHeader(num))
						{
							result = readerAtHeader.ReadElementContentAsGuid();
						}
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 131079, SR.GetString("TraceCodeFailedToReadAnActivityIdHeader"), null, exception);
				}
			}
			return result;
		}

		// Token: 0x06006961 RID: 26977 RVA: 0x00189464 File Offset: 0x00187664
		internal static bool ExtractActivityAndCorrelationId(Message message, out Guid activityId, out Guid correlationId)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			activityId = Guid.Empty;
			correlationId = Guid.Empty;
			try
			{
				if (message.State != MessageState.Closed && message.Headers != null)
				{
					int num = message.Headers.FindHeader("ActivityId", "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics");
					if (num >= 0)
					{
						using (XmlDictionaryReader readerAtHeader = message.Headers.GetReaderAtHeader(num))
						{
							correlationId = Fx.CreateGuid(readerAtHeader.GetAttribute("CorrelationId", null));
							activityId = readerAtHeader.ReadElementContentAsGuid();
							return activityId != Guid.Empty;
						}
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 131079, SR.GetString("TraceCodeFailedToReadAnActivityIdHeader"), null, exception);
				}
			}
			return false;
		}

		// Token: 0x06006962 RID: 26978 RVA: 0x00189560 File Offset: 0x00187760
		internal void AddTo(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (message.State != MessageState.Closed && message.Headers.MessageVersion.Envelope != EnvelopeVersion.None)
			{
				int num = message.Headers.FindHeader("ActivityId", "http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics");
				if (num < 0)
				{
					message.Headers.Add(this);
				}
			}
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x001895C6 File Offset: 0x001877C6
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteAttributeString("CorrelationId", this.headerId.ToString());
			writer.WriteValue(this.guid);
		}

		// Token: 0x04003C34 RID: 15412
		private Guid guid;

		// Token: 0x04003C35 RID: 15413
		private Guid headerId;
	}
}
