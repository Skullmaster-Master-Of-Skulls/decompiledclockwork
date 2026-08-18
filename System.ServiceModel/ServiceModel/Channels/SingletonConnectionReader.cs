using System;
using System.IO;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081C RID: 2076
	internal abstract class SingletonConnectionReader
	{
		// Token: 0x06004D92 RID: 19858 RVA: 0x0011B599 File Offset: 0x00119799
		protected SingletonConnectionReader(IConnection connection, int offset, int size, SecurityMessageProperty security, IConnectionOrientedTransportFactorySettings transportSettings, Uri via)
		{
			this.connection = connection;
			this.offset = offset;
			this.size = size;
			this.security = security;
			this.transportSettings = transportSettings;
			this.via = via;
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004D93 RID: 19859 RVA: 0x0011B5D9 File Offset: 0x001197D9
		protected IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x0011B5E1 File Offset: 0x001197E1
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06004D95 RID: 19861 RVA: 0x0011B5E9 File Offset: 0x001197E9
		protected virtual string ContentType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004D96 RID: 19862
		protected abstract long StreamPosition { get; }

		// Token: 0x06004D97 RID: 19863 RVA: 0x0011B5EC File Offset: 0x001197EC
		public void Abort()
		{
			this.connection.Abort();
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x0011B5F9 File Offset: 0x001197F9
		public void DoneReceiving(bool atEof)
		{
			this.DoneReceiving(atEof, this.transportSettings.CloseTimeout);
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0011B60D File Offset: 0x0011980D
		private void DoneReceiving(bool atEof, TimeSpan timeout)
		{
			if (!this.doneReceiving)
			{
				this.isAtEof = atEof;
				this.doneReceiving = true;
				if (this.doneSending)
				{
					this.Close(timeout);
				}
			}
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x0011B634 File Offset: 0x00119834
		public void Close(TimeSpan timeout)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.isClosed)
				{
					return;
				}
				this.isClosed = true;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool flag2 = false;
			try
			{
				if (this.inputStream != null)
				{
					byte[] array = DiagnosticUtility.Utility.AllocateByteArray(this.transportSettings.ConnectionBufferSize);
					while (!this.isAtEof)
					{
						this.inputStream.ReadTimeout = TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime());
						if (this.inputStream.Read(array, 0, array.Length) == 0)
						{
							this.isAtEof = true;
						}
					}
				}
				this.OnClose(timeoutHelper.RemainingTime());
				flag2 = true;
			}
			finally
			{
				if (!flag2)
				{
					this.Abort();
				}
			}
		}

		// Token: 0x06004D9B RID: 19867
		protected abstract void OnClose(TimeSpan timeout);

		// Token: 0x06004D9C RID: 19868 RVA: 0x0011B714 File Offset: 0x00119914
		public void DoneSending(TimeSpan timeout)
		{
			this.doneSending = true;
			if (this.doneReceiving)
			{
				this.Close(timeout);
			}
		}

		// Token: 0x06004D9D RID: 19869
		protected abstract bool DecodeBytes(byte[] buffer, ref int offset, ref int size, ref bool isAtEof);

		// Token: 0x06004D9E RID: 19870 RVA: 0x0011B72C File Offset: 0x0011992C
		protected virtual void PrepareMessage(Message message)
		{
			message.Properties.Via = this.via;
			message.Properties.Security = ((this.security != null) ? ((SecurityMessageProperty)this.security.CreateCopy()) : null);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x0011B768 File Offset: 0x00119968
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			Message requestMessage = this.Receive(timeout);
			return new SingletonConnectionReader.StreamedFramingRequestContext(this, requestMessage);
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x0011B784 File Offset: 0x00119984
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SingletonConnectionReader.ReceiveAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x0011B78F File Offset: 0x0011998F
		public virtual Message EndReceive(IAsyncResult result)
		{
			return SingletonConnectionReader.ReceiveAsyncResult.End(result);
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x0011B798 File Offset: 0x00119998
		public Message Receive(TimeSpan timeout)
		{
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(this.connection.AsyncReadBufferSize);
			if (this.size > 0)
			{
				Buffer.BlockCopy(this.connection.AsyncReadBuffer, this.offset, array, this.offset, this.size);
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			while (!this.DecodeBytes(array, ref this.offset, ref this.size, ref this.isAtEof))
			{
				if (this.isAtEof)
				{
					this.DoneReceiving(true, timeoutHelper.RemainingTime());
					return null;
				}
				if (this.size == 0)
				{
					this.offset = 0;
					this.size = this.connection.Read(array, 0, array.Length, timeoutHelper.RemainingTime());
					if (this.size == 0)
					{
						this.DoneReceiving(true, timeoutHelper.RemainingTime());
						return null;
					}
				}
			}
			IConnection innerConnection = this.connection;
			if (this.size > 0)
			{
				byte[] array2 = DiagnosticUtility.Utility.AllocateByteArray(this.size);
				Buffer.BlockCopy(array, this.offset, array2, 0, this.size);
				innerConnection = new PreReadConnection(innerConnection, array2);
			}
			Stream stream = new SingletonConnectionReader.SingletonInputConnectionStream(this, innerConnection, this.transportSettings);
			this.inputStream = new MaxMessageSizeStream(stream, this.transportSettings.MaxReceivedMessageSize);
			Message result;
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity(true) : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
					{
						TraceUtility.RetrieveMessageNumber()
					}), ActivityType.ProcessMessage);
				}
				Message message = null;
				try
				{
					message = this.transportSettings.MessageEncoderFactory.Encoder.ReadMessage(this.inputStream, this.transportSettings.MaxBufferSize, this.ContentType);
				}
				catch (XmlException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
				}
				if (DiagnosticUtility.ShouldUseActivity)
				{
					TraceUtility.TransferFromTransport(message);
				}
				this.PrepareMessage(message);
				result = message;
			}
			return result;
		}

		// Token: 0x04003088 RID: 12424
		private IConnection connection;

		// Token: 0x04003089 RID: 12425
		private bool doneReceiving;

		// Token: 0x0400308A RID: 12426
		private bool doneSending;

		// Token: 0x0400308B RID: 12427
		private bool isAtEof;

		// Token: 0x0400308C RID: 12428
		private bool isClosed;

		// Token: 0x0400308D RID: 12429
		private SecurityMessageProperty security;

		// Token: 0x0400308E RID: 12430
		private object thisLock = new object();

		// Token: 0x0400308F RID: 12431
		private int offset;

		// Token: 0x04003090 RID: 12432
		private int size;

		// Token: 0x04003091 RID: 12433
		private IConnectionOrientedTransportFactorySettings transportSettings;

		// Token: 0x04003092 RID: 12434
		private Uri via;

		// Token: 0x04003093 RID: 12435
		private Stream inputStream;

		// Token: 0x02000D17 RID: 3351
		private class ReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007B5D RID: 31581 RVA: 0x001CC416 File Offset: 0x001CA616
			public ReceiveAsyncResult(SingletonConnectionReader parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.timeout = timeout;
				ActionItem.Schedule(SingletonConnectionReader.ReceiveAsyncResult.onReceiveScheduled, this);
			}

			// Token: 0x06007B5E RID: 31582 RVA: 0x001CC43C File Offset: 0x001CA63C
			public static Message End(IAsyncResult result)
			{
				SingletonConnectionReader.ReceiveAsyncResult receiveAsyncResult = AsyncResult.End<SingletonConnectionReader.ReceiveAsyncResult>(result);
				return receiveAsyncResult.message;
			}

			// Token: 0x06007B5F RID: 31583 RVA: 0x001CC458 File Offset: 0x001CA658
			private static void OnReceiveScheduled(object state)
			{
				SingletonConnectionReader.ReceiveAsyncResult receiveAsyncResult = (SingletonConnectionReader.ReceiveAsyncResult)state;
				Exception exception = null;
				try
				{
					receiveAsyncResult.message = receiveAsyncResult.parent.Receive(receiveAsyncResult.timeout);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				receiveAsyncResult.Complete(false, exception);
			}

			// Token: 0x040046C8 RID: 18120
			private static Action<object> onReceiveScheduled = new Action<object>(SingletonConnectionReader.ReceiveAsyncResult.OnReceiveScheduled);

			// Token: 0x040046C9 RID: 18121
			private Message message;

			// Token: 0x040046CA RID: 18122
			private SingletonConnectionReader parent;

			// Token: 0x040046CB RID: 18123
			private TimeSpan timeout;
		}

		// Token: 0x02000D18 RID: 3352
		private class StreamedFramingRequestContext : RequestContextBase
		{
			// Token: 0x06007B61 RID: 31585 RVA: 0x001CC4C3 File Offset: 0x001CA6C3
			public StreamedFramingRequestContext(SingletonConnectionReader parent, Message requestMessage) : base(requestMessage, parent.transportSettings.CloseTimeout, parent.transportSettings.SendTimeout)
			{
				this.parent = parent;
				this.connection = parent.connection;
				this.settings = parent.transportSettings;
			}

			// Token: 0x06007B62 RID: 31586 RVA: 0x001CC501 File Offset: 0x001CA701
			protected override void OnAbort()
			{
				this.parent.Abort();
			}

			// Token: 0x06007B63 RID: 31587 RVA: 0x001CC50E File Offset: 0x001CA70E
			protected override void OnClose(TimeSpan timeout)
			{
				this.parent.Close(timeout);
			}

			// Token: 0x06007B64 RID: 31588 RVA: 0x001CC51C File Offset: 0x001CA71C
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				ICompressedMessageEncoder compressedMessageEncoder = this.settings.MessageEncoderFactory.Encoder as ICompressedMessageEncoder;
				if (compressedMessageEncoder != null && compressedMessageEncoder.CompressionEnabled)
				{
					compressedMessageEncoder.AddCompressedMessageProperties(message, this.parent.ContentType);
				}
				this.timeoutHelper = new TimeoutHelper(timeout);
				StreamingConnectionHelper.WriteMessage(message, this.connection, false, this.settings, ref this.timeoutHelper);
				this.parent.DoneSending(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x06007B65 RID: 31589 RVA: 0x001CC598 File Offset: 0x001CA798
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				ICompressedMessageEncoder compressedMessageEncoder = this.settings.MessageEncoderFactory.Encoder as ICompressedMessageEncoder;
				if (compressedMessageEncoder != null && compressedMessageEncoder.CompressionEnabled)
				{
					compressedMessageEncoder.AddCompressedMessageProperties(message, this.parent.ContentType);
				}
				this.timeoutHelper = new TimeoutHelper(timeout);
				return StreamingConnectionHelper.BeginWriteMessage(message, this.connection, false, this.settings, ref this.timeoutHelper, callback, state);
			}

			// Token: 0x06007B66 RID: 31590 RVA: 0x001CC600 File Offset: 0x001CA800
			protected override void OnEndReply(IAsyncResult result)
			{
				StreamingConnectionHelper.EndWriteMessage(result);
				this.parent.DoneSending(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x040046CC RID: 18124
			private IConnection connection;

			// Token: 0x040046CD RID: 18125
			private SingletonConnectionReader parent;

			// Token: 0x040046CE RID: 18126
			private IConnectionOrientedTransportFactorySettings settings;

			// Token: 0x040046CF RID: 18127
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000D19 RID: 3353
		private class SingletonInputConnectionStream : ConnectionStream
		{
			// Token: 0x06007B67 RID: 31591 RVA: 0x001CC620 File Offset: 0x001CA820
			public SingletonInputConnectionStream(SingletonConnectionReader reader, IConnection connection, IDefaultCommunicationTimeouts defaultTimeouts) : base(connection, defaultTimeouts, default(TimeSpan), false)
			{
				this.reader = reader;
				this.decoder = new SingletonMessageDecoder(reader.StreamPosition);
				this.chunkBytesRemaining = 0;
				this.chunkBuffer = new byte[5];
			}

			// Token: 0x06007B68 RID: 31592 RVA: 0x001CC66A File Offset: 0x001CA86A
			private void AbortReader()
			{
				this.reader.Abort();
			}

			// Token: 0x06007B69 RID: 31593 RVA: 0x001CC677 File Offset: 0x001CA877
			public override void Close()
			{
				this.reader.DoneReceiving(this.atEof);
			}

			// Token: 0x06007B6A RID: 31594 RVA: 0x001CC68C File Offset: 0x001CA88C
			private void DecodeData(byte[] buffer, int offset, int size)
			{
				while (size > 0)
				{
					int num = this.decoder.Decode(buffer, offset, size);
					offset += num;
					size -= num;
				}
			}

			// Token: 0x06007B6B RID: 31595 RVA: 0x001CC6B8 File Offset: 0x001CA8B8
			private void DecodeSize(byte[] buffer, ref int offset, ref int size)
			{
				while (size > 0)
				{
					int num = this.decoder.Decode(buffer, offset, size);
					if (num > 0)
					{
						offset += num;
						size -= num;
					}
					SingletonMessageDecoder.State currentState = this.decoder.CurrentState;
					if (currentState == SingletonMessageDecoder.State.ChunkStart)
					{
						this.chunkBytesRemaining = this.decoder.ChunkSize;
						if (size > 0 && buffer != this.chunkBuffer)
						{
							Buffer.BlockCopy(buffer, offset, this.chunkBuffer, 0, size);
							this.chunkBufferOffset = 0;
							this.chunkBufferSize = size;
						}
						return;
					}
					if (currentState == SingletonMessageDecoder.State.End)
					{
						this.ProcessEof();
						return;
					}
				}
			}

			// Token: 0x06007B6C RID: 31596 RVA: 0x001CC750 File Offset: 0x001CA950
			private int ReadCore(byte[] buffer, int offset, int count)
			{
				int num = -1;
				try
				{
					num = base.Read(buffer, offset, count);
					if (num == 0)
					{
						this.ProcessEof();
					}
				}
				finally
				{
					if (num == -1)
					{
						this.AbortReader();
					}
				}
				return num;
			}

			// Token: 0x06007B6D RID: 31597 RVA: 0x001CC790 File Offset: 0x001CA990
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = 0;
				while (count != 0)
				{
					if (this.atEof)
					{
						return num;
					}
					if (this.chunkBufferSize > 0)
					{
						int num2 = Math.Min(this.chunkBytesRemaining, Math.Min(this.chunkBufferSize, count));
						Buffer.BlockCopy(this.chunkBuffer, this.chunkBufferOffset, buffer, offset, num2);
						this.DecodeData(this.chunkBuffer, this.chunkBufferOffset, num2);
						this.chunkBufferOffset += num2;
						this.chunkBufferSize -= num2;
						this.chunkBytesRemaining -= num2;
						if (this.chunkBytesRemaining == 0 && this.chunkBufferSize > 0)
						{
							this.DecodeSize(this.chunkBuffer, ref this.chunkBufferOffset, ref this.chunkBufferSize);
						}
						num += num2;
						offset += num2;
						count -= num2;
					}
					else
					{
						if (this.chunkBytesRemaining > 0)
						{
							int count2 = count;
							if (2147483647 - this.chunkBytesRemaining >= 5)
							{
								count2 = Math.Min(count, this.chunkBytesRemaining + 5);
							}
							int num3 = this.ReadCore(buffer, offset, count2);
							this.DecodeData(buffer, offset, Math.Min(num3, this.chunkBytesRemaining));
							if (num3 > this.chunkBytesRemaining)
							{
								num += this.chunkBytesRemaining;
								int num4 = num3 - this.chunkBytesRemaining;
								int num5 = offset + this.chunkBytesRemaining;
								this.chunkBytesRemaining = 0;
								this.DecodeSize(buffer, ref num5, ref num4);
							}
							else
							{
								num += num3;
								this.chunkBytesRemaining -= num3;
							}
							return num;
						}
						if (count < 5)
						{
							this.chunkBufferOffset = 0;
							this.chunkBufferSize = this.ReadCore(this.chunkBuffer, 0, this.chunkBuffer.Length);
							this.DecodeSize(this.chunkBuffer, ref this.chunkBufferOffset, ref this.chunkBufferSize);
						}
						else
						{
							int num6 = this.ReadCore(buffer, offset, 5);
							int num7 = offset;
							this.DecodeSize(buffer, ref num7, ref num6);
						}
					}
				}
				return num;
			}

			// Token: 0x06007B6E RID: 31598 RVA: 0x001CC958 File Offset: 0x001CAB58
			private void ProcessEof()
			{
				if (!this.atEof)
				{
					this.atEof = true;
					if (this.chunkBufferSize > 0 || this.chunkBytesRemaining > 0 || this.decoder.CurrentState != SingletonMessageDecoder.State.End)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
					}
					this.reader.DoneReceiving(true);
				}
			}

			// Token: 0x06007B6F RID: 31599 RVA: 0x001CC9B6 File Offset: 0x001CABB6
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return new SingletonConnectionReader.SingletonInputConnectionStream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			}

			// Token: 0x06007B70 RID: 31600 RVA: 0x001CC9C5 File Offset: 0x001CABC5
			public override int EndRead(IAsyncResult result)
			{
				return SingletonConnectionReader.SingletonInputConnectionStream.ReadAsyncResult.End(result);
			}

			// Token: 0x040046D0 RID: 18128
			private SingletonMessageDecoder decoder;

			// Token: 0x040046D1 RID: 18129
			private SingletonConnectionReader reader;

			// Token: 0x040046D2 RID: 18130
			private bool atEof;

			// Token: 0x040046D3 RID: 18131
			private byte[] chunkBuffer;

			// Token: 0x040046D4 RID: 18132
			private int chunkBufferOffset;

			// Token: 0x040046D5 RID: 18133
			private int chunkBufferSize;

			// Token: 0x040046D6 RID: 18134
			private int chunkBytesRemaining;

			// Token: 0x02000F49 RID: 3913
			public class ReadAsyncResult : AsyncResult
			{
				// Token: 0x060086E1 RID: 34529 RVA: 0x001F4027 File Offset: 0x001F2227
				public ReadAsyncResult(SingletonConnectionReader.SingletonInputConnectionStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(callback, state)
				{
					this.parent = parent;
					this.result = this.parent.Read(buffer, offset, count);
					base.Complete(true);
				}

				// Token: 0x060086E2 RID: 34530 RVA: 0x001F4058 File Offset: 0x001F2258
				public static int End(IAsyncResult result)
				{
					SingletonConnectionReader.SingletonInputConnectionStream.ReadAsyncResult readAsyncResult = AsyncResult.End<SingletonConnectionReader.SingletonInputConnectionStream.ReadAsyncResult>(result);
					return readAsyncResult.result;
				}

				// Token: 0x04004E6B RID: 20075
				private SingletonConnectionReader.SingletonInputConnectionStream parent;

				// Token: 0x04004E6C RID: 20076
				private int result;
			}
		}
	}
}
