using System;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A83 RID: 2691
	internal class MessageTraceRecord : TraceRecord
	{
		// Token: 0x06006A32 RID: 27186 RVA: 0x0018C0F3 File Offset: 0x0018A2F3
		internal MessageTraceRecord(Message message)
		{
			this.message = message;
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06006A33 RID: 27187 RVA: 0x0018C102 File Offset: 0x0018A302
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("Message");
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06006A34 RID: 27188 RVA: 0x0018C10F File Offset: 0x0018A30F
		protected Message Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x0018C118 File Offset: 0x0018A318
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.message != null && this.message.State != MessageState.Closed && this.message.Headers != null)
			{
				try
				{
					xml.WriteStartElement("MessageProperties");
					if (this.message.Properties.Encoder != null)
					{
						xml.WriteElementString("Encoder", this.message.Properties.Encoder.ToString());
					}
					xml.WriteElementString("AllowOutputBatching", this.message.Properties.AllowOutputBatching.ToString());
					if (this.message.Properties.Security != null && this.message.Properties.Security.ServiceSecurityContext != null)
					{
						xml.WriteStartElement("Security");
						xml.WriteElementString("IsAnonymous", this.message.Properties.Security.ServiceSecurityContext.IsAnonymous.ToString());
						xml.WriteElementString("WindowsIdentityUsed", (this.message.Properties.Security.ServiceSecurityContext.WindowsIdentity != null && !string.IsNullOrEmpty(this.message.Properties.Security.ServiceSecurityContext.WindowsIdentity.Name)).ToString());
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							xml.WriteStartElement("Claims");
							AuthorizationContext authorizationContext = this.message.Properties.Security.ServiceSecurityContext.AuthorizationContext;
							for (int i = 0; i < authorizationContext.ClaimSets.Count; i++)
							{
								ClaimSet claimSet = authorizationContext.ClaimSets[i];
								xml.WriteStartElement("ClaimSet");
								xml.WriteAttributeString("ClrType", base.XmlEncode(claimSet.GetType().AssemblyQualifiedName));
								for (int j = 0; j < claimSet.Count; j++)
								{
									SecurityTraceRecordHelper.WriteClaim(xml, claimSet[j]);
								}
								xml.WriteEndElement();
							}
							xml.WriteEndElement();
						}
						xml.WriteEndElement();
					}
					if (this.message.Properties.Via != null)
					{
						xml.WriteElementString("Via", this.message.Properties.Via.ToString());
					}
					xml.WriteEndElement();
					xml.WriteStartElement("MessageHeaders");
					for (int k = 0; k < this.message.Headers.Count; k++)
					{
						this.message.Headers.WriteHeader(k, xml);
					}
					xml.WriteEndElement();
				}
				catch (CommunicationException exception)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 131082, SR.GetString("TraceCodeDiagnosticsFailedMessageTrace"), exception, this.message);
					}
				}
			}
		}

		// Token: 0x04003CA3 RID: 15523
		private Message message;
	}
}
