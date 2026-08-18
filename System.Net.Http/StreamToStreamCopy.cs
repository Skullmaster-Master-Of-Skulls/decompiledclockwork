using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200001A RID: 26
	internal class StreamToStreamCopy
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00006514 File Offset: 0x00004714
		public StreamToStreamCopy(Stream source, Stream destination, int bufferSize, bool disposeSource)
		{
			this.buffer = new byte[bufferSize];
			this.source = source;
			this.destination = destination;
			this.sourceIsMemoryStream = (source is MemoryStream);
			this.destinationIsMemoryStream = (destination is MemoryStream);
			this.bufferSize = bufferSize;
			this.bufferReadCallback = new AsyncCallback(this.BufferReadCallback);
			this.bufferWrittenCallback = new AsyncCallback(this.BufferWrittenCallback);
			this.disposeSource = disposeSource;
			this.tcs = new TaskCompletionSource<object>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000065A0 File Offset: 0x000047A0
		public Task StartAsync()
		{
			if (this.sourceIsMemoryStream && this.destinationIsMemoryStream)
			{
				MemoryStream memoryStream = this.source as MemoryStream;
				try
				{
					int num = (int)memoryStream.Position;
					this.destination.Write(memoryStream.ToArray(), num, (int)this.source.Length - num);
					this.SetCompleted(null);
					goto IL_5D;
				}
				catch (Exception completed)
				{
					this.SetCompleted(completed);
					goto IL_5D;
				}
			}
			this.StartRead();
			IL_5D:
			return this.tcs.Task;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006628 File Offset: 0x00004828
		private void StartRead()
		{
			try
			{
				for (;;)
				{
					bool flag;
					if (this.sourceIsMemoryStream)
					{
						int num = this.source.Read(this.buffer, 0, this.bufferSize);
						if (num == 0)
						{
							break;
						}
						flag = this.TryStartWriteSync(num);
					}
					else
					{
						IAsyncResult asyncResult = this.source.BeginRead(this.buffer, 0, this.bufferSize, this.bufferReadCallback, null);
						flag = asyncResult.CompletedSynchronously;
						if (flag)
						{
							int num = this.source.EndRead(asyncResult);
							if (num == 0)
							{
								goto Block_4;
							}
							flag = this.TryStartWriteSync(num);
						}
					}
					if (!flag)
					{
						goto Block_5;
					}
				}
				this.SetCompleted(null);
				return;
				Block_4:
				this.SetCompleted(null);
				Block_5:;
			}
			catch (Exception completed)
			{
				this.SetCompleted(completed);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000066E0 File Offset: 0x000048E0
		private bool TryStartWriteSync(int bytesRead)
		{
			if (this.destinationIsMemoryStream)
			{
				this.destination.Write(this.buffer, 0, bytesRead);
				return true;
			}
			IAsyncResult asyncResult = this.destination.BeginWrite(this.buffer, 0, bytesRead, this.bufferWrittenCallback, null);
			if (asyncResult.CompletedSynchronously)
			{
				this.destination.EndWrite(asyncResult);
				return true;
			}
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000673C File Offset: 0x0000493C
		private void BufferReadCallback(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				try
				{
					int num = this.source.EndRead(ar);
					if (num == 0)
					{
						this.SetCompleted(null);
					}
					else if (this.TryStartWriteSync(num))
					{
						this.StartRead();
					}
				}
				catch (Exception completed)
				{
					this.SetCompleted(completed);
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006798 File Offset: 0x00004998
		private void BufferWrittenCallback(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				try
				{
					this.destination.EndWrite(ar);
					this.StartRead();
				}
				catch (Exception completed)
				{
					this.SetCompleted(completed);
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000067DC File Offset: 0x000049DC
		private void SetCompleted(Exception error)
		{
			try
			{
				if (this.disposeSource)
				{
					this.source.Dispose();
				}
			}
			catch (Exception e)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Http, this, "SetCompleted", e);
				}
			}
			if (error == null)
			{
				this.tcs.TrySetResult(null);
				return;
			}
			this.tcs.TrySetException(error);
		}

		// Token: 0x040000C3 RID: 195
		private byte[] buffer;

		// Token: 0x040000C4 RID: 196
		private int bufferSize;

		// Token: 0x040000C5 RID: 197
		private Stream source;

		// Token: 0x040000C6 RID: 198
		private Stream destination;

		// Token: 0x040000C7 RID: 199
		private AsyncCallback bufferReadCallback;

		// Token: 0x040000C8 RID: 200
		private AsyncCallback bufferWrittenCallback;

		// Token: 0x040000C9 RID: 201
		private TaskCompletionSource<object> tcs;

		// Token: 0x040000CA RID: 202
		private bool sourceIsMemoryStream;

		// Token: 0x040000CB RID: 203
		private bool destinationIsMemoryStream;

		// Token: 0x040000CC RID: 204
		private bool disposeSource;
	}
}
