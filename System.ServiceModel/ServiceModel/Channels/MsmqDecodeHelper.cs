using System;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel.Security;
using System.Transactions;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DF RID: 2271
	internal static class MsmqDecodeHelper
	{
		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x0013C499 File Offset: 0x0013A699
		private static ActiveXSerializer ActiveXSerializer
		{
			get
			{
				if (MsmqDecodeHelper.activeXSerializer == null)
				{
					MsmqDecodeHelper.activeXSerializer = new ActiveXSerializer();
				}
				return MsmqDecodeHelper.activeXSerializer;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06005667 RID: 22119 RVA: 0x0013C4B1 File Offset: 0x0013A6B1
		private static BinaryFormatter BinaryFormatter
		{
			get
			{
				if (MsmqDecodeHelper.binaryFormatter == null)
				{
					MsmqDecodeHelper.binaryFormatter = new BinaryFormatter();
				}
				return MsmqDecodeHelper.binaryFormatter;
			}
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0013C4CC File Offset: 0x0013A6CC
		private static void ReadServerMode(MsmqChannelListenerBase listener, ServerModeDecoder modeDecoder, byte[] incoming, long lookupId, ref int offset, ref int size)
		{
			while (size > 0)
			{
				int num = modeDecoder.Decode(incoming, offset, size);
				offset += num;
				size -= num;
				if (ServerModeDecoder.State.Done == modeDecoder.CurrentState)
				{
					return;
				}
			}
			throw listener.NormalizePoisonException(lookupId, modeDecoder.CreatePrematureEOFException());
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x0013C514 File Offset: 0x0013A714
		internal static Message DecodeTransportDatagram(MsmqInputChannelListener listener, MsmqReceiveHelper receiver, MsmqInputMessage msmqMessage, MsmqMessageProperty messageProperty)
		{
			Message result;
			using (MsmqDiagnostics.BoundReceiveBytesOperation())
			{
				long value = msmqMessage.LookupId.Value;
				int i = msmqMessage.BodyLength.Value;
				int num = 0;
				byte[] buffer = msmqMessage.Body.Buffer;
				ServerModeDecoder serverModeDecoder = new ServerModeDecoder();
				try
				{
					MsmqDecodeHelper.ReadServerMode(listener, serverModeDecoder, buffer, messageProperty.LookupId, ref num, ref i);
				}
				catch (ProtocolException innerException)
				{
					receiver.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, innerException);
				}
				if (serverModeDecoder.Mode != FramingMode.SingletonSized)
				{
					receiver.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadFrame")));
				}
				ServerSingletonSizedDecoder serverSingletonSizedDecoder = new ServerSingletonSizedDecoder(0L, 2048, 256);
				try
				{
					while (i > 0)
					{
						int num2 = serverSingletonSizedDecoder.Decode(buffer, num, i);
						num += num2;
						i -= num2;
						if (serverSingletonSizedDecoder.CurrentState == ServerSingletonSizedDecoder.State.Start)
						{
							goto IL_F5;
						}
					}
					throw listener.NormalizePoisonException(messageProperty.LookupId, serverSingletonSizedDecoder.CreatePrematureEOFException());
				}
				catch (ProtocolException innerException2)
				{
					receiver.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, innerException2);
				}
				IL_F5:
				if ((long)i > listener.MaxReceivedMessageSize)
				{
					receiver.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException(listener.MaxReceivedMessageSize));
				}
				if (!listener.MessageEncoderFactory.Encoder.IsContentTypeSupported(serverSingletonSizedDecoder.ContentType))
				{
					receiver.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadContentType")));
				}
				byte[] array = listener.BufferManager.TakeBuffer(i);
				Buffer.BlockCopy(buffer, num, array, 0, i);
				Message message = null;
				using (MsmqDiagnostics.BoundDecodeOperation())
				{
					try
					{
						message = listener.MessageEncoderFactory.Encoder.ReadMessage(new ArraySegment<byte>(array, 0, i), listener.BufferManager);
					}
					catch (XmlException innerException3)
					{
						receiver.FinalDisposition(messageProperty);
						throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadXml"), innerException3));
					}
					bool flag = true;
					try
					{
						SecurityMessageProperty securityMessageProperty = listener.ValidateSecurity(msmqMessage);
						if (securityMessageProperty != null)
						{
							message.Properties.Security = securityMessageProperty;
						}
						flag = false;
						MsmqDiagnostics.TransferFromTransport(message);
						result = message;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						receiver.FinalDisposition(messageProperty);
						throw listener.NormalizePoisonException(messageProperty.LookupId, ex);
					}
					finally
					{
						if (flag)
						{
							message.Close();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x0013C80C File Offset: 0x0013AA0C
		internal static IInputSessionChannel DecodeTransportSessiongram(MsmqInputSessionChannelListener listener, MsmqInputMessage msmqMessage, MsmqMessageProperty messageProperty, MsmqReceiveContextLockManager receiveContextManager)
		{
			IInputSessionChannel result;
			using (MsmqDiagnostics.BoundReceiveBytesOperation())
			{
				long value = msmqMessage.LookupId.Value;
				int i = msmqMessage.BodyLength.Value;
				int num = 0;
				byte[] buffer = msmqMessage.Body.Buffer;
				MsmqReceiveHelper msmqReceiveHelper = listener.MsmqReceiveHelper;
				ServerModeDecoder serverModeDecoder = new ServerModeDecoder();
				try
				{
					MsmqDecodeHelper.ReadServerMode(listener, serverModeDecoder, buffer, messageProperty.LookupId, ref num, ref i);
				}
				catch (ProtocolException innerException)
				{
					msmqReceiveHelper.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, innerException);
				}
				if (serverModeDecoder.Mode != FramingMode.Simplex)
				{
					msmqReceiveHelper.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadFrame")));
				}
				MsmqInputSessionChannel msmqInputSessionChannel = null;
				ServerSessionDecoder serverSessionDecoder = new ServerSessionDecoder(0L, 2048, 256);
				try
				{
					while (i > 0)
					{
						int num2 = serverSessionDecoder.Decode(buffer, num, i);
						num += num2;
						i -= num2;
						if (ServerSessionDecoder.State.EnvelopeStart == serverSessionDecoder.CurrentState)
						{
							goto IL_104;
						}
					}
					throw listener.NormalizePoisonException(messageProperty.LookupId, serverSessionDecoder.CreatePrematureEOFException());
				}
				catch (ProtocolException innerException2)
				{
					msmqReceiveHelper.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, innerException2);
				}
				IL_104:
				MessageEncoder messageEncoder = listener.MessageEncoderFactory.CreateSessionEncoder();
				if (!messageEncoder.IsContentTypeSupported(serverSessionDecoder.ContentType))
				{
					msmqReceiveHelper.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadContentType")));
				}
				ReceiveContext sessiongramReceiveContext = null;
				if (msmqReceiveHelper.MsmqReceiveParameters.ReceiveContextSettings.Enabled)
				{
					sessiongramReceiveContext = receiveContextManager.CreateMsmqReceiveContext(msmqMessage.LookupId.Value);
				}
				msmqInputSessionChannel = new MsmqInputSessionChannel(listener, Transaction.Current, sessiongramReceiveContext);
				Message message = MsmqDecodeHelper.DecodeSessiongramMessage(listener, msmqInputSessionChannel, messageEncoder, messageProperty, buffer, num, serverSessionDecoder.EnvelopeSize);
				SecurityMessageProperty securityMessageProperty = null;
				try
				{
					securityMessageProperty = listener.ValidateSecurity(msmqMessage);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					msmqInputSessionChannel.FaultChannel();
					msmqReceiveHelper.FinalDisposition(messageProperty);
					throw listener.NormalizePoisonException(messageProperty.LookupId, ex);
				}
				if (securityMessageProperty != null)
				{
					message.Properties.Security = securityMessageProperty;
				}
				message.Properties["MsmqMessageProperty"] = messageProperty;
				msmqInputSessionChannel.EnqueueAndDispatch(message);
				listener.RaiseMessageReceived();
				for (;;)
				{
					int num3;
					try
					{
						if (i <= 0)
						{
							msmqInputSessionChannel.FaultChannel();
							msmqReceiveHelper.FinalDisposition(messageProperty);
							throw listener.NormalizePoisonException(messageProperty.LookupId, serverSessionDecoder.CreatePrematureEOFException());
						}
						num3 = serverSessionDecoder.Decode(buffer, num, i);
					}
					catch (ProtocolException innerException3)
					{
						msmqInputSessionChannel.FaultChannel();
						msmqReceiveHelper.FinalDisposition(messageProperty);
						throw listener.NormalizePoisonException(messageProperty.LookupId, innerException3);
					}
					num += num3;
					i -= num3;
					if (ServerSessionDecoder.State.End == serverSessionDecoder.CurrentState)
					{
						break;
					}
					if (ServerSessionDecoder.State.EnvelopeStart == serverSessionDecoder.CurrentState)
					{
						message = MsmqDecodeHelper.DecodeSessiongramMessage(listener, msmqInputSessionChannel, messageEncoder, messageProperty, buffer, num, serverSessionDecoder.EnvelopeSize);
						if (securityMessageProperty != null)
						{
							message.Properties.Security = (SecurityMessageProperty)securityMessageProperty.CreateCopy();
						}
						message.Properties["MsmqMessageProperty"] = messageProperty;
						msmqInputSessionChannel.EnqueueAndDispatch(message);
						listener.RaiseMessageReceived();
					}
				}
				msmqInputSessionChannel.Shutdown();
				MsmqDiagnostics.SessiongramReceived(msmqInputSessionChannel.Session.Id, msmqMessage.MessageId, msmqInputSessionChannel.InternalPendingItems);
				result = msmqInputSessionChannel;
			}
			return result;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0013CB9C File Offset: 0x0013AD9C
		private static Message DecodeSessiongramMessage(MsmqInputSessionChannelListener listener, MsmqInputSessionChannel channel, MessageEncoder encoder, MsmqMessageProperty messageProperty, byte[] buffer, int offset, int size)
		{
			if ((long)size > listener.MaxReceivedMessageSize)
			{
				channel.FaultChannel();
				listener.MsmqReceiveHelper.FinalDisposition(messageProperty);
				throw listener.NormalizePoisonException(messageProperty.LookupId, MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException(listener.MaxReceivedMessageSize));
			}
			if (size + offset > buffer.Length)
			{
				listener.MsmqReceiveHelper.FinalDisposition(messageProperty);
				throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadFrame")));
			}
			byte[] array = listener.BufferManager.TakeBuffer(size);
			Buffer.BlockCopy(buffer, offset, array, 0, size);
			Message result;
			try
			{
				Message message = null;
				using (MsmqDiagnostics.BoundDecodeOperation())
				{
					message = encoder.ReadMessage(new ArraySegment<byte>(array, 0, size), listener.BufferManager);
					MsmqDiagnostics.TransferFromTransport(message);
				}
				result = message;
			}
			catch (XmlException innerException)
			{
				channel.FaultChannel();
				listener.MsmqReceiveHelper.FinalDisposition(messageProperty);
				throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqBadXml"), innerException));
			}
			return result;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0013CCB0 File Offset: 0x0013AEB0
		internal static Message DecodeIntegrationDatagram(MsmqIntegrationChannelListener listener, MsmqReceiveHelper receiver, MsmqIntegrationInputMessage msmqMessage, MsmqMessageProperty messageProperty)
		{
			Message result;
			using (MsmqDiagnostics.BoundReceiveBytesOperation())
			{
				Message message = Message.CreateMessage(MessageVersion.None, null);
				bool flag = true;
				try
				{
					SecurityMessageProperty securityMessageProperty = listener.ValidateSecurity(msmqMessage);
					if (securityMessageProperty != null)
					{
						message.Properties.Security = securityMessageProperty;
					}
					MsmqIntegrationMessageProperty msmqIntegrationMessageProperty = new MsmqIntegrationMessageProperty();
					msmqMessage.SetMessageProperties(msmqIntegrationMessageProperty);
					int value = msmqMessage.BodyLength.Value;
					if ((long)value > listener.MaxReceivedMessageSize)
					{
						receiver.FinalDisposition(messageProperty);
						throw listener.NormalizePoisonException(messageProperty.LookupId, MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException(listener.MaxReceivedMessageSize));
					}
					byte[] bufferCopy = msmqMessage.Body.GetBufferCopy(value);
					MemoryStream memoryStream = new MemoryStream(bufferCopy, 0, bufferCopy.Length, false);
					object body = null;
					using (MsmqDiagnostics.BoundDecodeOperation())
					{
						try
						{
							body = MsmqDecodeHelper.DeserializeForIntegration(listener, memoryStream, msmqIntegrationMessageProperty, messageProperty.LookupId);
						}
						catch (SerializationException innerException)
						{
							receiver.FinalDisposition(messageProperty);
							throw listener.NormalizePoisonException(messageProperty.LookupId, new ProtocolException(SR.GetString("MsmqDeserializationError"), innerException));
						}
						msmqIntegrationMessageProperty.Body = body;
						message.Properties["MsmqIntegrationMessageProperty"] = msmqIntegrationMessageProperty;
						memoryStream.Seek(0L, SeekOrigin.Begin);
						message.Headers.To = listener.Uri;
						flag = false;
						MsmqDiagnostics.TransferFromTransport(message);
					}
					result = message;
				}
				finally
				{
					if (flag)
					{
						message.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0013CE60 File Offset: 0x0013B060
		private static object DeserializeForIntegration(MsmqIntegrationChannelListener listener, Stream bodyStream, MsmqIntegrationMessageProperty property, long lookupId)
		{
			MsmqMessageSerializationFormat serializationFormat = (listener.ReceiveParameters as MsmqIntegrationReceiveParameters).SerializationFormat;
			switch (serializationFormat)
			{
			case MsmqMessageSerializationFormat.Xml:
				return MsmqDecodeHelper.XmlDeserializeForIntegration(listener, bodyStream, lookupId);
			case MsmqMessageSerializationFormat.Binary:
				return MsmqDecodeHelper.BinaryFormatter.Deserialize(bodyStream);
			case MsmqMessageSerializationFormat.ActiveX:
			{
				int value = property.BodyType.Value;
				return MsmqDecodeHelper.ActiveXSerializer.Deserialize(bodyStream as MemoryStream, value);
			}
			case MsmqMessageSerializationFormat.ByteArray:
				return (bodyStream as MemoryStream).ToArray();
			case MsmqMessageSerializationFormat.Stream:
				return bodyStream;
			default:
				throw new SerializationException(SR.GetString("MsmqUnsupportedSerializationFormat", new object[]
				{
					serializationFormat
				}));
			}
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x0013CEFC File Offset: 0x0013B0FC
		private static object XmlDeserializeForIntegration(MsmqIntegrationChannelListener listener, Stream stream, long lookupId)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(stream);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
			xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
			try
			{
				foreach (XmlSerializer xmlSerializer in listener.XmlSerializerList)
				{
					if (xmlSerializer.CanDeserialize(xmlTextReader))
					{
						return xmlSerializer.Deserialize(xmlTextReader);
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new SerializationException(ex.Message);
			}
			throw new SerializationException(SR.GetString("MsmqCannotDeserializeXmlMessage"));
		}

		// Token: 0x04003568 RID: 13672
		private static ActiveXSerializer activeXSerializer;

		// Token: 0x04003569 RID: 13673
		private static BinaryFormatter binaryFormatter;

		// Token: 0x0400356A RID: 13674
		private const int defaultMaxViaSize = 2048;

		// Token: 0x0400356B RID: 13675
		private const int defaultMaxContentTypeSize = 256;
	}
}
