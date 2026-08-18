using System;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000816 RID: 2070
	internal class ClientDuplexConnectionReader : SessionConnectionReader
	{
		// Token: 0x06004D65 RID: 19813 RVA: 0x0011AA6F File Offset: 0x00118C6F
		public ClientDuplexConnectionReader(ClientFramingDuplexSessionChannel channel, IConnection connection, ClientDuplexDecoder decoder, IConnectionOrientedTransportFactorySettings settings, MessageEncoder messageEncoder) : base(connection, null, 0, 0, null)
		{
			this.decoder = decoder;
			this.maxBufferSize = settings.MaxBufferSize;
			this.bufferManager = settings.BufferManager;
			this.messageEncoder = messageEncoder;
			this.channel = channel;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x0011AAAC File Offset: 0x00118CAC
		protected override void EnsureDecoderAtEof()
		{
			if (this.decoder.CurrentState != ClientFramingDecoderState.End && this.decoder.CurrentState != ClientFramingDecoderState.EnvelopeEnd && this.decoder.CurrentState != ClientFramingDecoderState.ReadingUpgradeRecord && this.decoder.CurrentState != ClientFramingDecoderState.UpgradeResponse)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
			}
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x0011AB08 File Offset: 0x00118D08
		private static IDisposable CreateProcessActionActivity()
		{
			IDisposable result = null;
			if (DiagnosticUtility.ShouldUseActivity && (ServiceModelActivity.Current == null || ServiceModelActivity.Current.ActivityType != ActivityType.ProcessAction))
			{
				if (ServiceModelActivity.Current != null && ServiceModelActivity.Current.PreviousActivity != null && ServiceModelActivity.Current.PreviousActivity.ActivityType == ActivityType.ProcessAction)
				{
					result = ServiceModelActivity.BoundOperation(ServiceModelActivity.Current.PreviousActivity);
				}
				else
				{
					ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateBoundedActivity(true);
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
					{
						TraceUtility.RetrieveMessageNumber()
					}), ActivityType.ProcessMessage);
					result = serviceModelActivity;
				}
			}
			return result;
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x0011AB98 File Offset: 0x00118D98
		protected override Message DecodeMessage(byte[] buffer, ref int offset, ref int size, ref bool isAtEOF, TimeSpan timeout)
		{
			while (size > 0)
			{
				int num = this.decoder.Decode(buffer, offset, size);
				if (num > 0)
				{
					if (base.EnvelopeBuffer != null)
					{
						if (buffer != base.EnvelopeBuffer)
						{
							Buffer.BlockCopy(buffer, offset, base.EnvelopeBuffer, base.EnvelopeOffset, num);
						}
						base.EnvelopeOffset += num;
					}
					offset += num;
					size -= num;
				}
				ClientFramingDecoderState currentState = this.decoder.CurrentState;
				if (currentState == ClientFramingDecoderState.Fault)
				{
					this.channel.Session.CloseOutputSession(this.channel.InternalCloseTimeout);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(FaultStringDecoder.GetFaultException(this.decoder.Fault, this.channel.RemoteAddress.Uri.ToString(), this.messageEncoder.ContentType));
				}
				switch (currentState)
				{
				case ClientFramingDecoderState.EnvelopeStart:
				{
					int envelopeSize = this.decoder.EnvelopeSize;
					if (envelopeSize > this.maxBufferSize)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException((long)this.maxBufferSize));
					}
					base.EnvelopeBuffer = this.bufferManager.TakeBuffer(envelopeSize);
					base.EnvelopeOffset = 0;
					base.EnvelopeSize = envelopeSize;
					break;
				}
				case ClientFramingDecoderState.EnvelopeEnd:
					if (base.EnvelopeBuffer != null)
					{
						Message message = null;
						try
						{
							IDisposable disposable = ClientDuplexConnectionReader.CreateProcessActionActivity();
							using (disposable)
							{
								message = this.messageEncoder.ReadMessage(new ArraySegment<byte>(base.EnvelopeBuffer, 0, base.EnvelopeSize), this.bufferManager);
								if (DiagnosticUtility.ShouldUseActivity)
								{
									TraceUtility.TransferFromTransport(message);
								}
							}
						}
						catch (XmlException innerException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
						}
						base.EnvelopeBuffer = null;
						return message;
					}
					break;
				case ClientFramingDecoderState.End:
					isAtEOF = true;
					return null;
				}
			}
			return null;
		}

		// Token: 0x0400306E RID: 12398
		private ClientDuplexDecoder decoder;

		// Token: 0x0400306F RID: 12399
		private int maxBufferSize;

		// Token: 0x04003070 RID: 12400
		private BufferManager bufferManager;

		// Token: 0x04003071 RID: 12401
		private MessageEncoder messageEncoder;

		// Token: 0x04003072 RID: 12402
		private ClientFramingDuplexSessionChannel channel;
	}
}
