using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A0C RID: 2572
	internal class OperationSelector : IDispatchOperationSelector
	{
		// Token: 0x060065D7 RID: 26071 RVA: 0x0017B74F File Offset: 0x0017994F
		public OperationSelector(IPeerNodeMessageHandling messageHandler)
		{
			this.messageHandler = messageHandler;
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x0017B760 File Offset: 0x00179960
		public static void TurnOffSecurityHeader(Message message)
		{
			int num = message.Headers.FindHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			if (num >= 0)
			{
				message.Headers.AddUnderstood(num);
			}
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x0017B794 File Offset: 0x00179994
		public string SelectOperation(ref Message message)
		{
			string action = message.Headers.Action;
			string a = null;
			byte[] defaultId = PeerNodeImplementation.DefaultId;
			string text = PeerStrings.FindAction(action);
			Uri uri = null;
			Uri peerTo = null;
			bool flag = false;
			PeerMessageProperty peerMessageProperty = new PeerMessageProperty();
			if (text != null)
			{
				return text;
			}
			try
			{
				a = PeerMessageHelpers.GetHeaderString(message.Headers, "FloodMessage", "http://schemas.microsoft.com/net/2006/05/peer");
				uri = PeerMessageHelpers.GetHeaderUri(message.Headers, "PeerVia", "http://schemas.microsoft.com/net/2006/05/peer");
				peerTo = PeerMessageHelpers.GetHeaderUri(message.Headers, "PeerTo", "http://schemas.microsoft.com/net/2006/05/peer");
			}
			catch (MessageHeaderException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				return "Fault";
			}
			catch (SerializationException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
				return "Fault";
			}
			catch (XmlException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Warning);
				return "Fault";
			}
			peerMessageProperty.PeerVia = uri;
			peerMessageProperty.PeerTo = peerTo;
			message.Properties.Add("PeerProperty", peerMessageProperty);
			if (a == "PeerFlooder")
			{
				try
				{
					if (!this.messageHandler.ValidateIncomingMessage(ref message, uri))
					{
						peerMessageProperty.SkipLocalChannels = true;
						flag = true;
						OperationSelector.TurnOffSecurityHeader(message);
					}
					if (this.messageHandler.IsNotSeenBefore(message, out defaultId, out peerMessageProperty.CacheMiss))
					{
						peerMessageProperty.MessageVerified = true;
					}
					else if (!flag)
					{
						peerMessageProperty.SkipLocalChannels = true;
					}
					if (defaultId == PeerNodeImplementation.DefaultId)
					{
						return "Fault";
					}
				}
				catch (MessageHeaderException exception4)
				{
					DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Warning);
					return "Fault";
				}
				catch (SerializationException exception5)
				{
					DiagnosticUtility.TraceHandledException(exception5, TraceEventType.Warning);
					return "Fault";
				}
				catch (XmlException exception6)
				{
					DiagnosticUtility.TraceHandledException(exception6, TraceEventType.Warning);
					return "Fault";
				}
				catch (MessageSecurityException ex)
				{
					if (!ex.ReplayDetected)
					{
						return "Fault";
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				return "FloodMessage";
			}
			return null;
		}

		// Token: 0x04003AC1 RID: 15041
		private IPeerNodeMessageHandling messageHandler;
	}
}
