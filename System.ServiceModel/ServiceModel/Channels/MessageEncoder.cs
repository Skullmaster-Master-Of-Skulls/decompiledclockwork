using System;
using System.IO;
using System.Net.Mime;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E0 RID: 2528
	[__DynamicallyInvokable]
	public abstract class MessageEncoder
	{
		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060063C9 RID: 25545
		[__DynamicallyInvokable]
		public abstract string ContentType { [__DynamicallyInvokable] get; }

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060063CA RID: 25546
		[__DynamicallyInvokable]
		public abstract string MediaType { [__DynamicallyInvokable] get; }

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060063CB RID: 25547
		[__DynamicallyInvokable]
		public abstract MessageVersion MessageVersion { [__DynamicallyInvokable] get; }

		// Token: 0x060063CC RID: 25548 RVA: 0x00174B08 File Offset: 0x00172D08
		[__DynamicallyInvokable]
		public virtual T GetProperty<T>() where T : class
		{
			if (typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)FaultConverter.GetDefaultFaultConverter(this.MessageVersion));
			}
			return default(T);
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x00174B4A File Offset: 0x00172D4A
		[__DynamicallyInvokable]
		public Message ReadMessage(Stream stream, int maxSizeOfHeaders)
		{
			return this.ReadMessage(stream, maxSizeOfHeaders, null);
		}

		// Token: 0x060063CE RID: 25550
		[__DynamicallyInvokable]
		public abstract Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType);

		// Token: 0x060063CF RID: 25551 RVA: 0x00174B58 File Offset: 0x00172D58
		[__DynamicallyInvokable]
		public Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager)
		{
			return this.ReadMessage(buffer, bufferManager, null);
		}

		// Token: 0x060063D0 RID: 25552
		[__DynamicallyInvokable]
		public abstract Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType);

		// Token: 0x060063D1 RID: 25553 RVA: 0x00174B70 File Offset: 0x00172D70
		internal ArraySegment<byte> BufferMessageStream(Stream stream, BufferManager bufferManager, int maxBufferSize)
		{
			byte[] array = bufferManager.TakeBuffer(8192);
			int i = 0;
			int num = Math.Min(array.Length, maxBufferSize);
			while (i < num)
			{
				int num2 = stream.Read(array, i, num - i);
				if (num2 == 0)
				{
					stream.Close();
					break;
				}
				i += num2;
				if (i == num)
				{
					if (num >= maxBufferSize)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(MaxMessageSizeStream.CreateMaxReceivedMessageSizeExceededException((long)maxBufferSize));
					}
					num = Math.Min(num * 2, maxBufferSize);
					byte[] array2 = bufferManager.TakeBuffer(num);
					Buffer.BlockCopy(array, 0, array2, 0, i);
					bufferManager.ReturnBuffer(array);
					array = array2;
				}
			}
			return new ArraySegment<byte>(array, 0, i);
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x00174C00 File Offset: 0x00172E00
		internal virtual Message ReadMessage(Stream stream, BufferManager bufferManager, int maxBufferSize, string contentType)
		{
			return this.ReadMessage(this.BufferMessageStream(stream, bufferManager, maxBufferSize), bufferManager, contentType);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x00174C14 File Offset: 0x00172E14
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ContentType;
		}

		// Token: 0x060063D4 RID: 25556
		[__DynamicallyInvokable]
		public abstract void WriteMessage(Message message, Stream stream);

		// Token: 0x060063D5 RID: 25557 RVA: 0x00174C1C File Offset: 0x00172E1C
		public virtual IAsyncResult BeginWriteMessage(Message message, Stream stream, AsyncCallback callback, object state)
		{
			return new MessageEncoder.WriteMessageAsyncResult(message, stream, this, callback, state);
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x00174C29 File Offset: 0x00172E29
		public virtual void EndWriteMessage(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x00174C34 File Offset: 0x00172E34
		[__DynamicallyInvokable]
		public ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager)
		{
			return this.WriteMessage(message, maxMessageSize, bufferManager, 0);
		}

		// Token: 0x060063D8 RID: 25560
		[__DynamicallyInvokable]
		public abstract ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset);

		// Token: 0x060063D9 RID: 25561 RVA: 0x00174C4D File Offset: 0x00172E4D
		[__DynamicallyInvokable]
		public virtual bool IsContentTypeSupported(string contentType)
		{
			if (contentType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contentType"));
			}
			return this.IsContentTypeSupported(contentType, this.ContentType, this.MediaType);
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x00174C7C File Offset: 0x00172E7C
		internal bool IsContentTypeSupported(string contentType, string supportedContentType, string supportedMediaType)
		{
			if (supportedContentType == contentType)
			{
				return true;
			}
			if (contentType.Length > supportedContentType.Length && contentType.StartsWith(supportedContentType, StringComparison.Ordinal) && contentType[supportedContentType.Length] == ';')
			{
				return true;
			}
			if (contentType.StartsWith(supportedContentType, StringComparison.OrdinalIgnoreCase))
			{
				if (contentType.Length == supportedContentType.Length)
				{
					return true;
				}
				if (contentType.Length > supportedContentType.Length)
				{
					char c = contentType[supportedContentType.Length];
					if (c == ';')
					{
						return true;
					}
					int i = supportedContentType.Length;
					if (c == '\r' && contentType.Length > supportedContentType.Length + 1 && contentType[i + 1] == '\n')
					{
						i += 2;
						c = contentType[i];
					}
					if (c == ' ' || c == '\t')
					{
						for (i++; i < contentType.Length; i++)
						{
							c = contentType[i];
							if (c != ' ' && c != '\t')
							{
								break;
							}
						}
					}
					if (c == ';' || i == contentType.Length)
					{
						return true;
					}
				}
			}
			try
			{
				ContentType contentType2 = new ContentType(contentType);
				if (supportedMediaType.Length > 0 && !supportedMediaType.Equals(contentType2.MediaType, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				if (!this.IsCharSetSupported(contentType2.CharSet))
				{
					return false;
				}
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x00174DC4 File Offset: 0x00172FC4
		internal virtual bool IsCharSetSupported(string charset)
		{
			return false;
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x00174DC7 File Offset: 0x00172FC7
		internal void ThrowIfMismatchedMessageVersion(Message message)
		{
			if (message.Version != this.MessageVersion)
			{
				throw TraceUtility.ThrowHelperError(new ProtocolException(SR.GetString("EncoderMessageVersionMismatch", new object[]
				{
					message.Version,
					this.MessageVersion
				})), message);
			}
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x00174E05 File Offset: 0x00173005
		internal string GetTraceSourceString()
		{
			if (this.traceSourceString == null)
			{
				this.traceSourceString = DiagnosticTraceBase.CreateDefaultSourceString(this);
			}
			return this.traceSourceString;
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x00174E21 File Offset: 0x00173021
		[__DynamicallyInvokable]
		protected MessageEncoder()
		{
		}

		// Token: 0x0400399B RID: 14747
		private string traceSourceString;

		// Token: 0x02000E53 RID: 3667
		private class WriteMessageAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x06008319 RID: 33561 RVA: 0x001E5088 File Offset: 0x001E3288
			public WriteMessageAsyncResult(Message message, Stream stream, MessageEncoder encoder, AsyncCallback callback, object state) : base(callback, state)
			{
				this.encoder = encoder;
				this.message = message;
				this.stream = stream;
				base.Schedule();
			}

			// Token: 0x0600831A RID: 33562 RVA: 0x001E50AF File Offset: 0x001E32AF
			protected override void OnDoWork()
			{
				this.encoder.WriteMessage(this.message, this.stream);
			}

			// Token: 0x04004A8A RID: 19082
			private MessageEncoder encoder;

			// Token: 0x04004A8B RID: 19083
			private Message message;

			// Token: 0x04004A8C RID: 19084
			private Stream stream;
		}
	}
}
