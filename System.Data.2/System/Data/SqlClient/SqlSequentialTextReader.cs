using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001F7 RID: 503
	internal sealed class SqlSequentialTextReader : TextReader
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x000D85B8 File Offset: 0x000D79B8
		internal SqlSequentialTextReader(SqlDataReader reader, int columnIndex, Encoding encoding)
		{
			this._reader = reader;
			this._columnIndex = columnIndex;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			this._leftOverBytes = null;
			this._peekedChar = -1;
			this._currentTask = null;
			this._disposalTokenSource = new CancellationTokenSource();
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000D860C File Offset: 0x000D7A0C
		internal int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000D8620 File Offset: 0x000D7A20
		public override int Peek()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (!this.HasPeekedChar)
			{
				this._peekedChar = this.Read();
			}
			return this._peekedChar;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000D8664 File Offset: 0x000D7A64
		public override int Read()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			int result = -1;
			if (this.HasPeekedChar)
			{
				result = this._peekedChar;
				this._peekedChar = -1;
			}
			else
			{
				char[] array = new char[1];
				int num = this.InternalRead(array, 0, 1);
				if (num == 1)
				{
					result = (int)array[0];
				}
			}
			return result;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000D86C4 File Offset: 0x000D7AC4
		public override int Read(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			if (this.IsClosed)
			{
				throw ADP.ObjectDisposed(this);
			}
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			int num = 0;
			int num2 = count;
			if (num2 > 0 && this.HasPeekedChar)
			{
				buffer[index + num] = (char)this._peekedChar;
				num++;
				num2--;
				this._peekedChar = -1;
			}
			return num + this.InternalRead(buffer, index + num, num2);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000D8730 File Offset: 0x000D7B30
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			SqlSequentialTextReader.ValidateReadParameters(buffer, index, count);
			TaskCompletionSource<int> completion = new TaskCompletionSource<int>();
			if (this.IsClosed)
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
						bool flag = true;
						int charsRead = 0;
						int adjustedIndex = index;
						int charsNeeded = count;
						if (this.HasPeekedChar && charsNeeded > 0)
						{
							int peekedChar = this._peekedChar;
							if (peekedChar >= 0)
							{
								buffer[adjustedIndex] = (char)peekedChar;
								int num = adjustedIndex;
								adjustedIndex = num + 1;
								num = charsRead;
								charsRead = num + 1;
								num = charsNeeded;
								charsNeeded = num - 1;
								this._peekedChar = -1;
							}
						}
						int byteBufferUsed;
						byte[] byteBuffer = this.PrepareByteBuffer(charsNeeded, out byteBufferUsed);
						if (byteBufferUsed < byteBuffer.Length || byteBuffer.Length == 0)
						{
							SqlDataReader reader = this._reader;
							if (reader != null)
							{
								int num2;
								Task<int> bytesAsync = reader.GetBytesAsync(this._columnIndex, byteBuffer, byteBufferUsed, byteBuffer.Length - byteBufferUsed, -1, this._disposalTokenSource.Token, out num2);
								if (bytesAsync == null)
								{
									byteBufferUsed += num2;
								}
								else
								{
									flag = false;
									bytesAsync.ContinueWith(delegate(Task<int> t)
									{
										this._currentTask = null;
										if (t.Status == TaskStatus.RanToCompletion && !this.IsClosed)
										{
											try
											{
												int result = t.Result;
												byteBufferUsed += result;
												if (byteBufferUsed > 0)
												{
													charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
												}
												completion.SetResult(charsRead);
												return;
											}
											catch (Exception exception2)
											{
												completion.SetException(exception2);
												return;
											}
										}
										if (this.IsClosed)
										{
											completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
											return;
										}
										if (t.Status == TaskStatus.Faulted)
										{
											if (t.Exception.InnerException is SqlException)
											{
												completion.SetException(ADP.ExceptionWithStackTrace(ADP.ErrorReadingFromStream(t.Exception.InnerException)));
												return;
											}
											completion.SetException(t.Exception.InnerException);
											return;
										}
										else
										{
											completion.SetCanceled();
										}
									}, TaskScheduler.Default);
								}
								if (flag && byteBufferUsed > 0)
								{
									charsRead += this.DecodeBytesToChars(byteBuffer, byteBufferUsed, buffer, adjustedIndex, charsNeeded);
								}
							}
							else
							{
								completion.SetException(ADP.ExceptionWithStackTrace(ADP.ObjectDisposed(this)));
							}
						}
						if (flag)
						{
							this._currentTask = null;
							if (this.IsClosed)
							{
								completion.SetCanceled();
							}
							else
							{
								completion.SetResult(charsRead);
							}
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

		// Token: 0x06001F46 RID: 8006 RVA: 0x000D89C4 File Offset: 0x000D7DC4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetClosed();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000D89E4 File Offset: 0x000D7DE4
		internal void SetClosed()
		{
			this._disposalTokenSource.Cancel();
			this._reader = null;
			this._peekedChar = -1;
			Task currentTask = this._currentTask;
			if (currentTask != null)
			{
				((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000D8A20 File Offset: 0x000D7E20
		private int InternalRead(char[] buffer, int index, int count)
		{
			int result;
			try
			{
				int num;
				byte[] array = this.PrepareByteBuffer(count, out num);
				num += this._reader.GetBytesInternalSequential(this._columnIndex, array, num, array.Length - num, null);
				if (num > 0)
				{
					result = this.DecodeBytesToChars(array, num, buffer, index, count);
				}
				else
				{
					result = 0;
				}
			}
			catch (SqlException internalException)
			{
				throw ADP.ErrorReadingFromStream(internalException);
			}
			return result;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000D8A9C File Offset: 0x000D7E9C
		private byte[] PrepareByteBuffer(int numberOfChars, out int byteBufferUsed)
		{
			byte[] array;
			if (numberOfChars == 0)
			{
				array = new byte[0];
				byteBufferUsed = 0;
			}
			else
			{
				int maxByteCount = this._encoding.GetMaxByteCount(numberOfChars);
				if (this._leftOverBytes != null)
				{
					if (this._leftOverBytes.Length > maxByteCount)
					{
						array = this._leftOverBytes;
						byteBufferUsed = array.Length;
					}
					else
					{
						array = new byte[maxByteCount];
						Array.Copy(this._leftOverBytes, array, this._leftOverBytes.Length);
						byteBufferUsed = this._leftOverBytes.Length;
					}
				}
				else
				{
					array = new byte[maxByteCount];
					byteBufferUsed = 0;
				}
			}
			return array;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000D8B18 File Offset: 0x000D7F18
		private int DecodeBytesToChars(byte[] inBuffer, int inBufferCount, char[] outBuffer, int outBufferOffset, int outBufferCount)
		{
			int num;
			int result;
			bool flag;
			this._decoder.Convert(inBuffer, 0, inBufferCount, outBuffer, outBufferOffset, outBufferCount, false, out num, out result, out flag);
			if (!flag && num < inBufferCount)
			{
				this._leftOverBytes = new byte[inBufferCount - num];
				Array.Copy(inBuffer, num, this._leftOverBytes, 0, this._leftOverBytes.Length);
			}
			else
			{
				this._leftOverBytes = null;
			}
			return result;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x000D8B74 File Offset: 0x000D7F74
		private bool IsClosed
		{
			get
			{
				return this._reader == null;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000D8B8C File Offset: 0x000D7F8C
		private bool IsDataLeft
		{
			get
			{
				return this._leftOverBytes != null || this._reader.ColumnDataBytesRemaining() > 0L;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000D8BB4 File Offset: 0x000D7FB4
		private bool HasPeekedChar
		{
			get
			{
				return this._peekedChar >= 0;
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000D8BD0 File Offset: 0x000D7FD0
		internal static void ValidateReadParameters(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw ADP.ArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw ADP.ArgumentOutOfRange("index");
			}
			if (count < 0)
			{
				throw ADP.ArgumentOutOfRange("count");
			}
			try
			{
				if (checked(index + count) > buffer.Length)
				{
					throw ExceptionBuilder.InvalidOffsetLength();
				}
			}
			catch (OverflowException)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
		}

		// Token: 0x040011A7 RID: 4519
		private SqlDataReader _reader;

		// Token: 0x040011A8 RID: 4520
		private int _columnIndex;

		// Token: 0x040011A9 RID: 4521
		private Encoding _encoding;

		// Token: 0x040011AA RID: 4522
		private Decoder _decoder;

		// Token: 0x040011AB RID: 4523
		private byte[] _leftOverBytes;

		// Token: 0x040011AC RID: 4524
		private int _peekedChar;

		// Token: 0x040011AD RID: 4525
		private Task _currentTask;

		// Token: 0x040011AE RID: 4526
		private CancellationTokenSource _disposalTokenSource;
	}
}
