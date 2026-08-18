using System;
using System.IO;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081D RID: 2077
	internal static class StreamingConnectionHelper
	{
		// Token: 0x06004DA3 RID: 19875 RVA: 0x0011B9A0 File Offset: 0x00119BA0
		public static void WriteMessage(Message message, IConnection connection, bool isRequest, IConnectionOrientedTransportFactorySettings settings, ref TimeoutHelper timeoutHelper)
		{
			byte[] array = null;
			if (message != null)
			{
				MessageEncoder encoder = settings.MessageEncoderFactory.Encoder;
				byte[] envelopeStartBytes = SingletonEncoder.EnvelopeStartBytes;
				bool flag;
				if (isRequest)
				{
					array = SingletonEncoder.EnvelopeEndFramingEndBytes;
					flag = TransferModeHelper.IsRequestStreamed(settings.TransferMode);
				}
				else
				{
					array = SingletonEncoder.EnvelopeEndBytes;
					flag = TransferModeHelper.IsResponseStreamed(settings.TransferMode);
				}
				if (flag)
				{
					connection.Write(envelopeStartBytes, 0, envelopeStartBytes.Length, false, timeoutHelper.RemainingTime());
					Stream stream = new StreamingConnectionHelper.StreamingOutputConnectionStream(connection, settings);
					Stream stream2 = new TimeoutStream(stream, ref timeoutHelper);
					encoder.WriteMessage(message, stream2);
				}
				else
				{
					ArraySegment<byte> messageFrame = encoder.WriteMessage(message, int.MaxValue, settings.BufferManager, envelopeStartBytes.Length + 5);
					messageFrame = SingletonEncoder.EncodeMessageFrame(messageFrame);
					Buffer.BlockCopy(envelopeStartBytes, 0, messageFrame.Array, messageFrame.Offset - envelopeStartBytes.Length, envelopeStartBytes.Length);
					connection.Write(messageFrame.Array, messageFrame.Offset - envelopeStartBytes.Length, messageFrame.Count + envelopeStartBytes.Length, true, timeoutHelper.RemainingTime(), settings.BufferManager);
				}
			}
			else if (isRequest)
			{
				array = SingletonEncoder.EndBytes;
			}
			if (array != null)
			{
				connection.Write(array, 0, array.Length, true, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x0011BAB5 File Offset: 0x00119CB5
		public static IAsyncResult BeginWriteMessage(Message message, IConnection connection, bool isRequest, IConnectionOrientedTransportFactorySettings settings, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
		{
			return new StreamingConnectionHelper.WriteMessageAsyncResult(message, connection, isRequest, settings, ref timeoutHelper, callback, state);
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x0011BAC6 File Offset: 0x00119CC6
		public static void EndWriteMessage(IAsyncResult result)
		{
			StreamingConnectionHelper.WriteMessageAsyncResult.End(result);
		}

		// Token: 0x02000D1A RID: 3354
		private class StreamingOutputConnectionStream : ConnectionStream
		{
			// Token: 0x06007B71 RID: 31601 RVA: 0x001CC9D0 File Offset: 0x001CABD0
			public StreamingOutputConnectionStream(IConnection connection, IDefaultCommunicationTimeouts timeouts) : base(connection, timeouts, default(TimeSpan), false)
			{
				this.encodedSize = new byte[5];
			}

			// Token: 0x06007B72 RID: 31602 RVA: 0x001CC9FC File Offset: 0x001CABFC
			private void WriteChunkSize(int size)
			{
				if (size > 0)
				{
					int size2 = IntEncoder.Encode(size, this.encodedSize, 0);
					base.Connection.Write(this.encodedSize, 0, size2, false, TimeSpan.FromMilliseconds((double)this.WriteTimeout));
				}
			}

			// Token: 0x06007B73 RID: 31603 RVA: 0x001CCA3B File Offset: 0x001CAC3B
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.WriteChunkSize(count);
				return base.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06007B74 RID: 31604 RVA: 0x001CCA51 File Offset: 0x001CAC51
			public override void WriteByte(byte value)
			{
				this.WriteChunkSize(1);
				base.WriteByte(value);
			}

			// Token: 0x06007B75 RID: 31605 RVA: 0x001CCA61 File Offset: 0x001CAC61
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.WriteChunkSize(count);
				base.Write(buffer, offset, count);
			}

			// Token: 0x040046D7 RID: 18135
			private byte[] encodedSize;
		}

		// Token: 0x02000D1B RID: 3355
		private class WriteMessageAsyncResult : AsyncResult
		{
			// Token: 0x06007B76 RID: 31606 RVA: 0x001CCA74 File Offset: 0x001CAC74
			public WriteMessageAsyncResult(Message message, IConnection connection, bool isRequest, IConnectionOrientedTransportFactorySettings settings, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
			{
				this.connection = connection;
				this.encoder = settings.MessageEncoderFactory.Encoder;
				this.bufferManager = settings.BufferManager;
				this.timeoutHelper = timeoutHelper;
				this.message = message;
				this.settings = settings;
				bool flag = true;
				bool flag2 = false;
				if (message == null)
				{
					if (isRequest)
					{
						this.endBytes = SingletonEncoder.EndBytes;
					}
					flag2 = this.WriteEndBytes();
				}
				else
				{
					try
					{
						byte[] envelopeStartBytes = SingletonEncoder.EnvelopeStartBytes;
						bool flag3;
						if (isRequest)
						{
							this.endBytes = SingletonEncoder.EnvelopeEndFramingEndBytes;
							flag3 = TransferModeHelper.IsRequestStreamed(settings.TransferMode);
						}
						else
						{
							this.endBytes = SingletonEncoder.EnvelopeEndBytes;
							flag3 = TransferModeHelper.IsResponseStreamed(settings.TransferMode);
						}
						if (flag3)
						{
							if (StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytes == null)
							{
								StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytes = Fx.ThunkCallback(new WaitCallback(StreamingConnectionHelper.WriteMessageAsyncResult.OnWriteStartBytes));
							}
							AsyncCompletionResult asyncCompletionResult = connection.BeginWrite(envelopeStartBytes, 0, envelopeStartBytes.Length, true, timeoutHelper.RemainingTime(), StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytes, this);
							if (asyncCompletionResult == AsyncCompletionResult.Completed)
							{
								if (StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytesScheduled == null)
								{
									StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytesScheduled = new Action<object>(StreamingConnectionHelper.WriteMessageAsyncResult.OnWriteStartBytes);
								}
								ActionItem.Schedule(StreamingConnectionHelper.WriteMessageAsyncResult.onWriteStartBytesScheduled, this);
							}
						}
						else
						{
							ArraySegment<byte> messageFrame = settings.MessageEncoderFactory.Encoder.WriteMessage(message, int.MaxValue, this.bufferManager, envelopeStartBytes.Length + 5);
							messageFrame = SingletonEncoder.EncodeMessageFrame(messageFrame);
							this.bufferToFree = messageFrame.Array;
							Buffer.BlockCopy(envelopeStartBytes, 0, messageFrame.Array, messageFrame.Offset - envelopeStartBytes.Length, envelopeStartBytes.Length);
							if (StreamingConnectionHelper.WriteMessageAsyncResult.onWriteBufferedMessage == null)
							{
								StreamingConnectionHelper.WriteMessageAsyncResult.onWriteBufferedMessage = Fx.ThunkCallback(new WaitCallback(StreamingConnectionHelper.WriteMessageAsyncResult.OnWriteBufferedMessage));
							}
							AsyncCompletionResult asyncCompletionResult2 = connection.BeginWrite(messageFrame.Array, messageFrame.Offset - envelopeStartBytes.Length, messageFrame.Count + envelopeStartBytes.Length, true, timeoutHelper.RemainingTime(), StreamingConnectionHelper.WriteMessageAsyncResult.onWriteBufferedMessage, this);
							if (asyncCompletionResult2 == AsyncCompletionResult.Completed)
							{
								flag2 = this.HandleWriteBufferedMessage();
							}
						}
						flag = false;
					}
					finally
					{
						if (flag)
						{
							this.Cleanup();
						}
					}
				}
				if (flag2)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007B77 RID: 31607 RVA: 0x001CCC7C File Offset: 0x001CAE7C
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<StreamingConnectionHelper.WriteMessageAsyncResult>(result);
			}

			// Token: 0x06007B78 RID: 31608 RVA: 0x001CCC85 File Offset: 0x001CAE85
			private void Cleanup()
			{
				if (this.bufferToFree != null)
				{
					this.bufferManager.ReturnBuffer(this.bufferToFree);
				}
			}

			// Token: 0x06007B79 RID: 31609 RVA: 0x001CCCA0 File Offset: 0x001CAEA0
			private bool HandleWriteStartBytes()
			{
				this.connection.EndWrite();
				Stream stream = new StreamingConnectionHelper.StreamingOutputConnectionStream(this.connection, this.settings);
				Stream stream2 = new TimeoutStream(stream, ref this.timeoutHelper);
				this.encoder.WriteMessage(this.message, stream2);
				return this.WriteEndBytes();
			}

			// Token: 0x06007B7A RID: 31610 RVA: 0x001CCCEF File Offset: 0x001CAEEF
			private bool HandleWriteBufferedMessage()
			{
				this.connection.EndWrite();
				return this.WriteEndBytes();
			}

			// Token: 0x06007B7B RID: 31611 RVA: 0x001CCD04 File Offset: 0x001CAF04
			private bool WriteEndBytes()
			{
				if (this.endBytes == null)
				{
					this.Cleanup();
					return true;
				}
				return this.connection.BeginWrite(this.endBytes, 0, this.endBytes.Length, true, this.timeoutHelper.RemainingTime(), StreamingConnectionHelper.WriteMessageAsyncResult.onWriteEndBytes, this) != AsyncCompletionResult.Queued && this.HandleWriteEndBytes();
			}

			// Token: 0x06007B7C RID: 31612 RVA: 0x001CCD59 File Offset: 0x001CAF59
			private bool HandleWriteEndBytes()
			{
				this.connection.EndWrite();
				this.Cleanup();
				return true;
			}

			// Token: 0x06007B7D RID: 31613 RVA: 0x001CCD6D File Offset: 0x001CAF6D
			private static void OnWriteStartBytes(object asyncState)
			{
				StreamingConnectionHelper.WriteMessageAsyncResult.OnWriteStartBytesCallbackHelper(asyncState);
			}

			// Token: 0x06007B7E RID: 31614 RVA: 0x001CCD78 File Offset: 0x001CAF78
			private static void OnWriteStartBytesCallbackHelper(object asyncState)
			{
				StreamingConnectionHelper.WriteMessageAsyncResult writeMessageAsyncResult = (StreamingConnectionHelper.WriteMessageAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				bool flag2 = true;
				try
				{
					flag = writeMessageAsyncResult.HandleWriteStartBytes();
					flag2 = false;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				finally
				{
					if (flag2)
					{
						writeMessageAsyncResult.Cleanup();
					}
				}
				if (flag)
				{
					writeMessageAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B7F RID: 31615 RVA: 0x001CCDE4 File Offset: 0x001CAFE4
			private static void OnWriteBufferedMessage(object asyncState)
			{
				StreamingConnectionHelper.WriteMessageAsyncResult writeMessageAsyncResult = (StreamingConnectionHelper.WriteMessageAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				bool flag2 = true;
				try
				{
					flag = writeMessageAsyncResult.HandleWriteBufferedMessage();
					flag2 = false;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				finally
				{
					if (flag2)
					{
						writeMessageAsyncResult.Cleanup();
					}
				}
				if (flag)
				{
					writeMessageAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B80 RID: 31616 RVA: 0x001CCE50 File Offset: 0x001CB050
			private static void OnWriteEndBytes(object asyncState)
			{
				StreamingConnectionHelper.WriteMessageAsyncResult writeMessageAsyncResult = (StreamingConnectionHelper.WriteMessageAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag = writeMessageAsyncResult.HandleWriteEndBytes();
					flag2 = true;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				finally
				{
					if (!flag2)
					{
						writeMessageAsyncResult.Cleanup();
					}
				}
				if (flag)
				{
					writeMessageAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x040046D8 RID: 18136
			private IConnection connection;

			// Token: 0x040046D9 RID: 18137
			private MessageEncoder encoder;

			// Token: 0x040046DA RID: 18138
			private BufferManager bufferManager;

			// Token: 0x040046DB RID: 18139
			private Message message;

			// Token: 0x040046DC RID: 18140
			private static WaitCallback onWriteBufferedMessage;

			// Token: 0x040046DD RID: 18141
			private static WaitCallback onWriteStartBytes;

			// Token: 0x040046DE RID: 18142
			private static Action<object> onWriteStartBytesScheduled;

			// Token: 0x040046DF RID: 18143
			private static WaitCallback onWriteEndBytes = Fx.ThunkCallback(new WaitCallback(StreamingConnectionHelper.WriteMessageAsyncResult.OnWriteEndBytes));

			// Token: 0x040046E0 RID: 18144
			private byte[] bufferToFree;

			// Token: 0x040046E1 RID: 18145
			private IConnectionOrientedTransportFactorySettings settings;

			// Token: 0x040046E2 RID: 18146
			private TimeoutHelper timeoutHelper;

			// Token: 0x040046E3 RID: 18147
			private byte[] endBytes;
		}
	}
}
