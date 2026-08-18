using System;
using System.IO;
using System.Net.Configuration;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000197 RID: 407
	internal sealed class ChunkParser
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000523D9 File Offset: 0x000505D9
		private bool IsAsync
		{
			get
			{
				return this.userAsyncResult != null;
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000523E4 File Offset: 0x000505E4
		static ChunkParser()
		{
			for (int i = 33; i < 127; i++)
			{
				ChunkParser.tokenChars[i] = true;
			}
			ChunkParser.tokenChars[40] = false;
			ChunkParser.tokenChars[41] = false;
			ChunkParser.tokenChars[60] = false;
			ChunkParser.tokenChars[62] = false;
			ChunkParser.tokenChars[64] = false;
			ChunkParser.tokenChars[44] = false;
			ChunkParser.tokenChars[59] = false;
			ChunkParser.tokenChars[58] = false;
			ChunkParser.tokenChars[92] = false;
			ChunkParser.tokenChars[34] = false;
			ChunkParser.tokenChars[47] = false;
			ChunkParser.tokenChars[91] = false;
			ChunkParser.tokenChars[93] = false;
			ChunkParser.tokenChars[63] = false;
			ChunkParser.tokenChars[61] = false;
			ChunkParser.tokenChars[123] = false;
			ChunkParser.tokenChars[125] = false;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000524AF File Offset: 0x000506AF
		public ChunkParser(Stream dataSource, byte[] internalBuffer, int initialBufferOffset, int initialBufferCount, int maxBufferLength)
		{
			this.dataSource = dataSource;
			this.buffer = internalBuffer;
			this.bufferCurrentPos = initialBufferOffset;
			this.bufferFillLength = initialBufferOffset + initialBufferCount;
			this.maxBufferLength = maxBufferLength;
			this.currentChunkLength = -1;
			this.readState = ChunkParser.ReadState.ChunkLength;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000524EC File Offset: 0x000506EC
		public IAsyncResult ReadAsync(object caller, byte[] userBuffer, int userBufferOffset, int userBufferCount, AsyncCallback callback, object state)
		{
			this.SetReadParameters(userBuffer, userBufferOffset, userBufferCount);
			this.userAsyncResult = new LazyAsyncResult(caller, state, callback);
			IAsyncResult result = this.userAsyncResult;
			try
			{
				this.ProcessResponse();
			}
			catch (Exception result2)
			{
				this.CompleteUserRead(result2);
			}
			return result;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00052540 File Offset: 0x00050740
		public int Read(byte[] userBuffer, int userBufferOffset, int userBufferCount)
		{
			this.SetReadParameters(userBuffer, userBufferOffset, userBufferCount);
			try
			{
				this.ProcessResponse();
			}
			catch (Exception)
			{
				this.TransitionToErrorState();
				throw;
			}
			return this.syncResult;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00052580 File Offset: 0x00050780
		private void SetReadParameters(byte[] userBuffer, int userBufferOffset, int userBufferCount)
		{
			if (Interlocked.CompareExchange<byte[]>(ref this.userBuffer, userBuffer, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("net_inasync"));
			}
			this.userBufferCount = userBufferCount;
			this.userBufferOffset = userBufferOffset;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000525B0 File Offset: 0x000507B0
		public bool TryGetLeftoverBytes(out byte[] buffer, out int leftoverBufferOffset, out int leftoverBufferSize)
		{
			leftoverBufferOffset = 0;
			leftoverBufferSize = 0;
			buffer = null;
			if (this.readState != ChunkParser.ReadState.Done)
			{
				return false;
			}
			if (this.bufferCurrentPos == this.bufferFillLength)
			{
				return false;
			}
			leftoverBufferOffset = this.bufferCurrentPos;
			leftoverBufferSize = this.bufferFillLength - this.bufferCurrentPos;
			buffer = this.buffer;
			return true;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00052604 File Offset: 0x00050804
		private void ProcessResponse()
		{
			while (this.readState < ChunkParser.ReadState.Done)
			{
				DataParseStatus dataParseStatus;
				switch (this.readState)
				{
				case ChunkParser.ReadState.ChunkLength:
					dataParseStatus = this.ParseChunkLength();
					break;
				case ChunkParser.ReadState.Extension:
					dataParseStatus = this.ParseExtension();
					break;
				case ChunkParser.ReadState.Payload:
					dataParseStatus = this.HandlePayload();
					break;
				case ChunkParser.ReadState.PayloadEnd:
					dataParseStatus = this.ParsePayloadEnd();
					break;
				case ChunkParser.ReadState.Trailer:
					dataParseStatus = this.ParseTrailer();
					break;
				default:
					throw new InternalException();
				}
				switch (dataParseStatus)
				{
				case DataParseStatus.NeedMoreData:
					if (!this.TryGetMoreData())
					{
						return;
					}
					break;
				case DataParseStatus.ContinueParsing:
					break;
				case DataParseStatus.Done:
					return;
				case DataParseStatus.Invalid:
				case DataParseStatus.DataTooBig:
					this.CompleteUserRead(new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						SR.GetString("net_io_connectionclosed")
					})));
					return;
				default:
					throw new InternalException();
				}
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000526D0 File Offset: 0x000508D0
		private void CompleteUserRead(object result)
		{
			bool flag = result is Exception;
			this.userBuffer = null;
			this.userBufferCount = 0;
			this.userBufferOffset = 0;
			if (flag)
			{
				this.TransitionToErrorState();
			}
			if (this.IsAsync)
			{
				LazyAsyncResult lazyAsyncResult = this.userAsyncResult;
				this.userAsyncResult = null;
				lazyAsyncResult.InvokeCallback(result);
				return;
			}
			if (flag)
			{
				throw result as Exception;
			}
			this.syncResult = (int)result;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00052739 File Offset: 0x00050939
		private void TransitionToErrorState()
		{
			this.readState = ChunkParser.ReadState.Error;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00052744 File Offset: 0x00050944
		private bool TryGetMoreData()
		{
			this.PrepareBufferForMoreData();
			int num = this.buffer.Length - this.bufferFillLength;
			if (this.readState == ChunkParser.ReadState.ChunkLength)
			{
				num = Math.Min(12, num);
			}
			int bytesRead;
			if (this.IsAsync)
			{
				IAsyncResult asyncResult = this.dataSource.BeginRead(this.buffer, this.bufferFillLength, num, new AsyncCallback(this.ReadCallback), null);
				this.CheckAsyncResult(asyncResult);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				bytesRead = this.dataSource.EndRead(asyncResult);
			}
			else
			{
				bytesRead = this.dataSource.Read(this.buffer, this.bufferFillLength, num);
			}
			this.CompleteMetaDataReadOperation(bytesRead);
			return true;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000527EC File Offset: 0x000509EC
		private void PrepareBufferForMoreData()
		{
			int num = this.bufferCurrentPos;
			this.bufferCurrentPos = 0;
			if (num == this.bufferFillLength)
			{
				this.bufferFillLength = 0;
				return;
			}
			if (num > 0 || this.bufferFillLength < this.buffer.Length)
			{
				if (num > 0)
				{
					int count = this.bufferFillLength - num;
					Buffer.BlockCopy(this.buffer, num, this.buffer, 0, count);
					this.bufferFillLength = count;
				}
				return;
			}
			if (this.buffer.Length == this.maxBufferLength)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			int num2 = Math.Min(this.maxBufferLength, this.buffer.Length * 2);
			byte[] dst = new byte[num2];
			Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
			this.buffer = dst;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000528C2 File Offset: 0x00050AC2
		private void CheckAsyncResult(IAsyncResult ar)
		{
			if (ar == null)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000528D9 File Offset: 0x00050AD9
		private void CompleteMetaDataReadOperation(int bytesRead)
		{
			if (bytesRead == 0)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			this.bufferFillLength += bytesRead;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00052910 File Offset: 0x00050B10
		public void ReadCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			try
			{
				int bytesRead = this.dataSource.EndRead(ar);
				if (this.readState == ChunkParser.ReadState.Payload)
				{
					this.CompletePayloadReadOperation(bytesRead);
				}
				else
				{
					this.CompleteMetaDataReadOperation(bytesRead);
					this.ProcessResponse();
				}
			}
			catch (Exception result)
			{
				this.CompleteUserRead(result);
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00052970 File Offset: 0x00050B70
		private DataParseStatus HandlePayload()
		{
			if (this.bufferCurrentPos < this.bufferFillLength)
			{
				int num = Math.Min(Math.Min(this.userBufferCount, this.bufferFillLength - this.bufferCurrentPos), this.currentChunkLength - this.currentChunkBytesRead);
				Buffer.BlockCopy(this.buffer, this.bufferCurrentPos, this.userBuffer, this.userBufferOffset, num);
				this.bufferCurrentPos += num;
				if (this.currentChunkBytesRead + num == this.currentChunkLength || num == this.userBufferCount)
				{
					this.CompletePayloadReadOperation(num);
					return DataParseStatus.Done;
				}
				this.currentOperationBytesRead += num;
				this.currentChunkBytesRead += num;
			}
			int count = Math.Min(this.userBufferCount - this.currentOperationBytesRead, this.currentChunkLength - this.currentChunkBytesRead);
			if (this.IsAsync)
			{
				IAsyncResult asyncResult = this.dataSource.BeginRead(this.userBuffer, this.userBufferOffset + this.currentOperationBytesRead, count, new AsyncCallback(this.ReadCallback), null);
				this.CheckAsyncResult(asyncResult);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompletePayloadReadOperation(this.dataSource.EndRead(asyncResult));
				}
			}
			else
			{
				int bytesRead = this.dataSource.Read(this.userBuffer, this.userBufferOffset + this.currentOperationBytesRead, count);
				this.CompletePayloadReadOperation(bytesRead);
			}
			return DataParseStatus.Done;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00052AC4 File Offset: 0x00050CC4
		private void CompletePayloadReadOperation(int bytesRead)
		{
			if (bytesRead == 0)
			{
				throw new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.ConnectionClosed), WebExceptionStatus.ConnectionClosed);
			}
			this.currentChunkBytesRead += bytesRead;
			int num = this.currentOperationBytesRead + bytesRead;
			if (this.currentChunkBytesRead == this.currentChunkLength)
			{
				this.readState = ChunkParser.ReadState.PayloadEnd;
			}
			this.currentOperationBytesRead = 0;
			this.CompleteUserRead(num);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00052B28 File Offset: 0x00050D28
		private DataParseStatus ParseChunkLength()
		{
			int num = -1;
			int i = this.bufferCurrentPos;
			while (i < this.bufferFillLength)
			{
				byte b = this.buffer[i];
				if ((b < 48 || b > 57) && (b < 65 || b > 70) && (b < 97 || b > 102))
				{
					if (num == -1)
					{
						return DataParseStatus.Invalid;
					}
					this.bufferCurrentPos = i;
					this.currentChunkLength = num;
					this.readState = ChunkParser.ReadState.Extension;
					return DataParseStatus.ContinueParsing;
				}
				else
				{
					byte b2 = (b < 65) ? (b - 48) : (10 + ((b < 97) ? (b - 65) : (b - 97)));
					if (num == -1)
					{
						num = (int)b2;
					}
					else
					{
						if (num >= 134217728)
						{
							return DataParseStatus.Invalid;
						}
						num = (num << 4) + (int)b2;
					}
					i++;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00052BCC File Offset: 0x00050DCC
		private DataParseStatus ParseExtension()
		{
			int num = this.bufferCurrentPos;
			DataParseStatus dataParseStatus = this.ParseWhitespaces(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			dataParseStatus = this.ParseExtensionNameValuePairs(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			dataParseStatus = this.ParseCRLF(ref num);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			this.bufferCurrentPos = num;
			if (this.currentChunkLength == 0)
			{
				this.readState = ChunkParser.ReadState.Trailer;
			}
			else
			{
				this.readState = ChunkParser.ReadState.Payload;
			}
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00052C30 File Offset: 0x00050E30
		private DataParseStatus ParsePayloadEnd()
		{
			DataParseStatus dataParseStatus = this.ParseCRLF(ref this.bufferCurrentPos);
			if (dataParseStatus != DataParseStatus.ContinueParsing)
			{
				return dataParseStatus;
			}
			this.currentChunkLength = -1;
			this.currentChunkBytesRead = 0;
			this.readState = ChunkParser.ReadState.ChunkLength;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00052C68 File Offset: 0x00050E68
		private DataParseStatus ParseTrailer()
		{
			if (this.ParseWhitespaces(ref this.bufferCurrentPos) == DataParseStatus.NeedMoreData)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = this.bufferCurrentPos;
			WebParseError webParseError;
			webParseError.Section = WebParseErrorSection.Generic;
			webParseError.Code = WebParseErrorCode.Generic;
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			DataParseStatus dataParseStatus;
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				dataParseStatus = webHeaderCollection.ParseHeaders(this.buffer, this.bufferFillLength, ref num, ref this.totalTrailerHeadersLength, this.maxBufferLength, ref webParseError);
			}
			else
			{
				dataParseStatus = webHeaderCollection.ParseHeadersStrict(this.buffer, this.bufferFillLength, ref num, ref this.totalTrailerHeadersLength, this.maxBufferLength, ref webParseError);
			}
			if (dataParseStatus == DataParseStatus.NeedMoreData || dataParseStatus == DataParseStatus.Done)
			{
				this.bufferCurrentPos = num;
			}
			if (dataParseStatus != DataParseStatus.Done)
			{
				return dataParseStatus;
			}
			this.readState = ChunkParser.ReadState.Done;
			this.CompleteUserRead(0);
			return DataParseStatus.Done;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00052D1E File Offset: 0x00050F1E
		private DataParseStatus ParseCRLF(ref int pos)
		{
			if (pos + 2 > this.bufferFillLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			if (this.buffer[pos] != 13 || this.buffer[pos + 1] != 10)
			{
				return DataParseStatus.Invalid;
			}
			pos += 2;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00052D54 File Offset: 0x00050F54
		private DataParseStatus ParseWhitespaces(ref int pos)
		{
			for (int i = pos; i < this.bufferFillLength; i++)
			{
				byte c = this.buffer[i];
				if (!ChunkParser.IsWhiteSpace(c))
				{
					pos = i;
					return DataParseStatus.ContinueParsing;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00052D8A File Offset: 0x00050F8A
		private static bool IsWhiteSpace(byte c)
		{
			return c == 32 || c == 9;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00052D98 File Offset: 0x00050F98
		private DataParseStatus ParseExtensionNameValuePairs(ref int pos)
		{
			int num = pos;
			while (this.buffer[num] == 59)
			{
				num++;
				DataParseStatus dataParseStatus = this.ParseWhitespaces(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				dataParseStatus = this.ParseToken(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				dataParseStatus = this.ParseWhitespaces(ref num);
				if (dataParseStatus != DataParseStatus.ContinueParsing)
				{
					return dataParseStatus;
				}
				if (this.buffer[num] == 61)
				{
					num++;
					dataParseStatus = this.ParseWhitespaces(ref num);
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
					dataParseStatus = this.ParseToken(ref num);
					if (dataParseStatus == DataParseStatus.Invalid)
					{
						dataParseStatus = this.ParseQuotedString(ref num);
					}
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
					dataParseStatus = this.ParseWhitespaces(ref num);
					if (dataParseStatus != DataParseStatus.ContinueParsing)
					{
						return dataParseStatus;
					}
				}
			}
			pos = num;
			return DataParseStatus.ContinueParsing;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00052E38 File Offset: 0x00051038
		private DataParseStatus ParseQuotedString(ref int pos)
		{
			if (pos == this.bufferFillLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			if (this.buffer[pos] != 34)
			{
				return DataParseStatus.Invalid;
			}
			for (int i = pos + 1; i < this.bufferFillLength; i++)
			{
				if (this.buffer[i] == 34)
				{
					pos = i + 1;
					return DataParseStatus.ContinueParsing;
				}
				if (this.buffer[i] == 92)
				{
					i++;
					if (i == this.bufferFillLength)
					{
						return DataParseStatus.NeedMoreData;
					}
					if (this.buffer[i] <= 127)
					{
						i++;
						continue;
					}
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00052EB8 File Offset: 0x000510B8
		private DataParseStatus ParseToken(ref int pos)
		{
			int i = pos;
			while (i < this.bufferFillLength)
			{
				if (!ChunkParser.IsTokenChar(this.buffer[i]))
				{
					if (i > pos)
					{
						pos = i;
						return DataParseStatus.ContinueParsing;
					}
					return DataParseStatus.Invalid;
				}
				else
				{
					i++;
				}
			}
			return DataParseStatus.NeedMoreData;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00052EF3 File Offset: 0x000510F3
		private static bool IsTokenChar(byte character)
		{
			return character <= 127 && ChunkParser.tokenChars[(int)character];
		}

		// Token: 0x040012E9 RID: 4841
		private const int chunkLengthBuffer = 12;

		// Token: 0x040012EA RID: 4842
		private const int noChunkLength = -1;

		// Token: 0x040012EB RID: 4843
		private static readonly bool[] tokenChars = new bool[128];

		// Token: 0x040012EC RID: 4844
		private byte[] buffer;

		// Token: 0x040012ED RID: 4845
		private int bufferCurrentPos;

		// Token: 0x040012EE RID: 4846
		private int bufferFillLength;

		// Token: 0x040012EF RID: 4847
		private int maxBufferLength;

		// Token: 0x040012F0 RID: 4848
		private byte[] userBuffer;

		// Token: 0x040012F1 RID: 4849
		private int userBufferOffset;

		// Token: 0x040012F2 RID: 4850
		private int userBufferCount;

		// Token: 0x040012F3 RID: 4851
		private LazyAsyncResult userAsyncResult;

		// Token: 0x040012F4 RID: 4852
		private Stream dataSource;

		// Token: 0x040012F5 RID: 4853
		private ChunkParser.ReadState readState;

		// Token: 0x040012F6 RID: 4854
		private int totalTrailerHeadersLength;

		// Token: 0x040012F7 RID: 4855
		private int currentChunkLength;

		// Token: 0x040012F8 RID: 4856
		private int currentChunkBytesRead;

		// Token: 0x040012F9 RID: 4857
		private int currentOperationBytesRead;

		// Token: 0x040012FA RID: 4858
		private int syncResult;

		// Token: 0x02000743 RID: 1859
		private enum ReadState
		{
			// Token: 0x040031D6 RID: 12758
			ChunkLength,
			// Token: 0x040031D7 RID: 12759
			Extension,
			// Token: 0x040031D8 RID: 12760
			Payload,
			// Token: 0x040031D9 RID: 12761
			PayloadEnd,
			// Token: 0x040031DA RID: 12762
			Trailer,
			// Token: 0x040031DB RID: 12763
			Done,
			// Token: 0x040031DC RID: 12764
			Error
		}
	}
}
