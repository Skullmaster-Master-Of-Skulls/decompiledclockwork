using System;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001F5 RID: 501
	internal sealed class SqlSequentialStream : Stream
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x000D7E4C File Offset: 0x000D724C
		internal SqlSequentialStream(SqlDataReader reader, int columnIndex)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
			if (reader.Command != null && reader.Command.CommandTimeout != 0)
			{
				this._readTimeout = (int)Math.Min((long)reader.Command.CommandTimeout * 1000L, 2147483647L);
				return;
			}
			this._readTimeout = -1;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x000D7EC4 File Offset: 0x000D72C4
		public override bool CanRead
		{
			get
			{
				return this._reader != null && !this._reader.IsClosed;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000D7EEC File Offset: 0x000D72EC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x000D7EFC File Offset: 0x000D72FC
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000D7F0C File Offset: 0x000D730C
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000D7F1C File Offset: 0x000D731C
		public override void Flush()
		{
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000D7F2C File Offset: 0x000D732C
		public override long Length
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x000D7F40 File Offset: 0x000D7340
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x000D7F54 File Offset: 0x000D7354
		public override long Position
		{
			get
			{
				throw ADP.NotSupported();
			}
			set
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000D7F68 File Offset: 0x000D7368
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000D7F7C File Offset: 0x000D737C
		public override int ReadTimeout
		{
			get
			{
				return this._readTimeout;
			}
			set
			{
				if (value > 0 || value == -1)
				{
					this._readTimeout = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("value");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000D7FA4 File Offset: 0x000D73A4
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000D7FB8 File Offset: 0x000D73B8
		public override int Read(byte[] buffer, int offset, int count)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int bytesInternalSequential;
			try
			{
				bytesInternalSequential = this._reader.GetBytesInternalSequential(this._columnIndex, buffer, offset, count, new long?((long)this._readTimeout));
			}
			catch (SqlException internalException)
			{
				throw ADP.ErrorReadingFromStream(internalException);
			}
			return bytesInternalSequential;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000D8038 File Offset: 0x000D7438
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw ADP.ObjectDisposed(this);
			}
			Task task = this.ReadAsync(buffer, offset, count, CancellationToken.None);
			if (callback != null)
			{
				task.ContinueWith(delegate(Task t)
				{
					callback(t);
				}, TaskScheduler.Default);
			}
			return task;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000D8094 File Offset: 0x000D7494
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw ADP.ArgumentNull("asyncResult");
			}
			Task<int> task = (Task<int>)asyncResult;
			try
			{
				task.Wait();
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			return task.Result;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000D80EC File Offset: 0x000D74EC
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SqlSequentialStream.ValidateReadParameters(buffer, offset, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (!this.CanRead)
			{
				completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
			}
			else
			{
				try
				{
					Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, completion.Task, null);
					if (task != null)
					{
						completion.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					}
					else
					{
						CancellationTokenSource combinedTokenSource;
						if (!cancellationToken.CanBeCanceled)
						{
							combinedTokenSource = this._disposalTokenSource;
						}
						else
						{
							combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this._disposalTokenSource.Token);
						}
						int result = 0;
						Task<int> task2 = null;
						SqlDataReader reader = this._reader;
						if (reader != null && !cancellationToken.IsCancellationRequested && !this._disposalTokenSource.Token.IsCancellationRequested)
						{
							task2 = reader.GetBytesAsync(this._columnIndex, buffer, offset, count, this._readTimeout, combinedTokenSource.Token, out result);
						}
						if (task2 == null)
						{
							this._currentTask = null;
							if (cancellationToken.IsCancellationRequested)
							{
								completion.SetCanceled();
							}
							else if (!this.CanRead)
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
							else
							{
								completion.SetResult(result);
							}
							if (combinedTokenSource != this._disposalTokenSource)
							{
								combinedTokenSource.Dispose();
							}
						}
						else
						{
							task2.ContinueWith(delegate(Task<int> t)
							{
								this._currentTask = null;
								if (t.Status == TaskStatus.RanToCompletion && this.CanRead)
								{
									completion.SetResult(t.Result);
								}
								else if (t.Status == TaskStatus.Faulted)
								{
									if (t.Exception.InnerException is SqlException)
									{
										completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
									}
									else
									{
										completion.SetException(t.Exception.InnerException);
									}
								}
								else if (!this.CanRead)
								{
									completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
								}
								else
								{
									completion.SetCanceled();
								}
								if (combinedTokenSource != this._disposalTokenSource)
								{
									combinedTokenSource.Dispose();
								}
							}, TaskScheduler.Default);
						}
					}
				}
				catch (Exception exception)
				{
					completion.TrySetException(exception);
					Interlocked.CompareExchange<Task>(ref this._currentTask, null, completion.Task);
					throw;
				}
			}
			return completion.Task;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000D82C8 File Offset: 0x000D76C8
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000D82DC File Offset: 0x000D76DC
		public override void SetLength(long value)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000D82F0 File Offset: 0x000D76F0
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000D8304 File Offset: 0x000D7704
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000D833C File Offset: 0x000D773C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000D835C File Offset: 0x000D775C
		internal static void ValidateReadParameters(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (offset < 0)
			{
				throw ADP.ArgumentOutOfRange("offset");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(offset + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x0400119D RID: 4509
		private SqlDataReader _reader;

		// Token: 0x0400119E RID: 4510
		private int _columnIndex;

		// Token: 0x0400119F RID: 4511
		private Task _currentTask;

		// Token: 0x040011A0 RID: 4512
		private int _readTimeout;

		// Token: 0x040011A1 RID: 4513
		private CancellationTokenSource _disposalTokenSource;
	}
}
