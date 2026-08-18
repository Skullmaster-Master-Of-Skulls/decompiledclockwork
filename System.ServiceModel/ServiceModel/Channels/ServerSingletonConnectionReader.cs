using System;
using System.Net;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081B RID: 2075
	internal class ServerSingletonConnectionReader : SingletonConnectionReader
	{
		// Token: 0x06004D8C RID: 19852 RVA: 0x0011B408 File Offset: 0x00119608
		public ServerSingletonConnectionReader(ServerSingletonPreambleConnectionReader preambleReader, IConnection upgradedConnection, ConnectionDemuxer connectionDemuxer) : base(upgradedConnection, preambleReader.BufferOffset, preambleReader.BufferSize, preambleReader.Security, preambleReader.TransportSettings, preambleReader.Via)
		{
			this.decoder = preambleReader.Decoder;
			this.contentType = this.decoder.ContentType;
			this.connectionDemuxer = connectionDemuxer;
			this.rawConnection = preambleReader.RawConnection;
			this.channelBindingToken = preambleReader.ChannelBinding;
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06004D8D RID: 19853 RVA: 0x0011B476 File Offset: 0x00119676
		protected override string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x0011B47E File Offset: 0x0011967E
		protected override long StreamPosition
		{
			get
			{
				return this.decoder.StreamPosition;
			}
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0011B48C File Offset: 0x0011968C
		protected override bool DecodeBytes(byte[] buffer, ref int offset, ref int size, ref bool isAtEof)
		{
			while (size > 0)
			{
				int num = this.decoder.Decode(buffer, offset, size);
				if (num > 0)
				{
					offset += num;
					size -= num;
				}
				ServerSingletonDecoder.State currentState = this.decoder.CurrentState;
				if (currentState == ServerSingletonDecoder.State.EnvelopeStart)
				{
					return true;
				}
				if (currentState == ServerSingletonDecoder.State.End)
				{
					isAtEof = true;
					return false;
				}
			}
			return false;
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0011B4E4 File Offset: 0x001196E4
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.Connection.Write(SingletonEncoder.EndBytes, 0, SingletonEncoder.EndBytes.Length, true, timeoutHelper.RemainingTime());
			this.connectionDemuxer.ReuseConnection(this.rawConnection, timeoutHelper.RemainingTime());
			ChannelBindingUtility.Dispose(ref this.channelBindingToken);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0011B53C File Offset: 0x0011973C
		protected override void PrepareMessage(Message message)
		{
			base.PrepareMessage(message);
			IPEndPoint remoteIPEndPoint = this.rawConnection.RemoteIPEndPoint;
			if (remoteIPEndPoint != null)
			{
				RemoteEndpointMessageProperty property = new RemoteEndpointMessageProperty(remoteIPEndPoint);
				message.Properties.Add(RemoteEndpointMessageProperty.Name, property);
			}
			if (this.channelBindingToken != null)
			{
				ChannelBindingMessageProperty channelBindingMessageProperty = new ChannelBindingMessageProperty(this.channelBindingToken, false);
				channelBindingMessageProperty.AddTo(message);
				channelBindingMessageProperty.Dispose();
			}
		}

		// Token: 0x04003083 RID: 12419
		private ConnectionDemuxer connectionDemuxer;

		// Token: 0x04003084 RID: 12420
		private ServerSingletonDecoder decoder;

		// Token: 0x04003085 RID: 12421
		private IConnection rawConnection;

		// Token: 0x04003086 RID: 12422
		private string contentType;

		// Token: 0x04003087 RID: 12423
		private ChannelBinding channelBindingToken;
	}
}
