using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000294 RID: 660
	internal class SmtpReplyReaderFactory
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x0007CDD2 File Offset: 0x0007AFD2
		internal SmtpReplyReaderFactory(Stream stream)
		{
			this.bufferedStream = new BufferedReadStream(stream);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0007CDE6 File Offset: 0x0007AFE6
		internal SmtpReplyReader CurrentReader
		{
			get
			{
				return this.currentReader;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0007CDEE File Offset: 0x0007AFEE
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0007CDF8 File Offset: 0x0007AFF8
		internal IAsyncResult BeginReadLines(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0007CE18 File Offset: 0x0007B018
		internal IAsyncResult BeginReadLine(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state, true);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0007CE38 File Offset: 0x0007B038
		internal void Close(SmtpReplyReader caller)
		{
			if (this.currentReader == caller)
			{
				if (this.readState != SmtpReplyReaderFactory.ReadState.Done)
				{
					if (this.byteBuffer == null)
					{
						this.byteBuffer = new byte[256];
					}
					while (this.Read(caller, this.byteBuffer, 0, this.byteBuffer.Length) != 0)
					{
					}
				}
				this.currentReader = null;
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0007CE8E File Offset: 0x0007B08E
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0007CE98 File Offset: 0x0007B098
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			LineInfo[] array = SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0007CEC4 File Offset: 0x0007B0C4
		internal SmtpReplyReader GetNextReplyReader()
		{
			if (this.currentReader != null)
			{
				this.currentReader.Close();
			}
			this.readState = SmtpReplyReaderFactory.ReadState.Status0;
			this.currentReader = new SmtpReplyReader(this);
			return this.currentReader;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0007CEF4 File Offset: 0x0007B0F4
		private unsafe int ProcessRead(byte[] buffer, int offset, int read, bool readLine)
		{
			if (read == 0)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					"net_io_connectionclosed"
				}));
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			byte* ptr2 = ptr + offset;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr3 + read;
			switch (this.readState)
			{
			case SmtpReplyReaderFactory.ReadState.Status0:
				goto IL_7C;
			case SmtpReplyReaderFactory.ReadState.Status1:
				goto IL_C1;
			case SmtpReplyReaderFactory.ReadState.Status2:
				goto IL_10D;
			case SmtpReplyReaderFactory.ReadState.ContinueFlag:
				goto IL_156;
			case SmtpReplyReaderFactory.ReadState.ContinueCR:
				break;
			case SmtpReplyReaderFactory.ReadState.ContinueLF:
				goto IL_1A9;
			case SmtpReplyReaderFactory.ReadState.LastCR:
				goto IL_1F1;
			case SmtpReplyReaderFactory.ReadState.LastLF:
				goto IL_1FF;
			case SmtpReplyReaderFactory.ReadState.Done:
				goto IL_227;
			default:
				goto IL_23A;
			}
			IL_198:
			while (ptr3 < ptr4)
			{
				if (*(ptr3++) == 13)
				{
					goto IL_1A9;
				}
			}
			this.readState = SmtpReplyReaderFactory.ReadState.ContinueCR;
			goto IL_23A;
			IL_1F1:
			while (ptr3 < ptr4)
			{
				if (*(ptr3++) == 13)
				{
					goto IL_1FF;
				}
			}
			this.readState = SmtpReplyReaderFactory.ReadState.LastCR;
			goto IL_23A;
			IL_7C:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status0;
				goto IL_23A;
			}
			byte b = *(ptr3++);
			if (b < 48 && b > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode = (SmtpStatusCode)(100 * (b - 48));
			IL_C1:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status1;
				goto IL_23A;
			}
			byte b2 = *(ptr3++);
			if (b2 < 48 && b2 > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode += (int)(10 * (b2 - 48));
			IL_10D:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status2;
				goto IL_23A;
			}
			byte b3 = *(ptr3++);
			if (b3 < 48 && b3 > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode += (int)(b3 - 48);
			IL_156:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.ContinueFlag;
				goto IL_23A;
			}
			byte b4 = *(ptr3++);
			if (b4 == 32)
			{
				goto IL_1F1;
			}
			if (b4 != 45)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			goto IL_198;
			IL_1A9:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.ContinueLF;
				goto IL_23A;
			}
			if (*(ptr3++) != 10)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			if (readLine)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status0;
				return (int)((long)(ptr3 - ptr2));
			}
			goto IL_7C;
			IL_1FF:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.LastLF;
				goto IL_23A;
			}
			if (*(ptr3++) != 10)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			IL_227:
			int result = (int)((long)(ptr3 - ptr2));
			this.readState = SmtpReplyReaderFactory.ReadState.Done;
			return result;
			IL_23A:
			return (int)((long)(ptr3 - ptr2));
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0007D144 File Offset: 0x0007B344
		internal int Read(SmtpReplyReader caller, byte[] buffer, int offset, int count)
		{
			if (count == 0 || this.currentReader != caller || this.readState == SmtpReplyReaderFactory.ReadState.Done)
			{
				return 0;
			}
			int num = this.bufferedStream.Read(buffer, offset, count);
			int num2 = this.ProcessRead(buffer, offset, num, false);
			if (num2 < num)
			{
				this.bufferedStream.Push(buffer, offset + num2, num - num2);
			}
			return num2;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0007D19C File Offset: 0x0007B39C
		internal LineInfo ReadLine(SmtpReplyReader caller)
		{
			LineInfo[] array = this.ReadLines(caller, true);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0007D1CA File Offset: 0x0007B3CA
		internal LineInfo[] ReadLines(SmtpReplyReader caller)
		{
			return this.ReadLines(caller, false);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0007D1D4 File Offset: 0x0007B3D4
		internal LineInfo[] ReadLines(SmtpReplyReader caller, bool oneLine)
		{
			if (caller != this.currentReader || this.readState == SmtpReplyReaderFactory.ReadState.Done)
			{
				return new LineInfo[0];
			}
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[256];
			}
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				if (num2 == num3)
				{
					num3 = this.bufferedStream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					num2 = 0;
				}
				int num4 = this.ProcessRead(this.byteBuffer, num2, num3 - num2, true);
				if (num < 4)
				{
					int num5 = Math.Min(4 - num, num4);
					num += num5;
					num2 += num5;
					num4 -= num5;
					if (num4 == 0)
					{
						continue;
					}
				}
				stringBuilder.Append(Encoding.UTF8.GetString(this.byteBuffer, num2, num4));
				num2 += num4;
				if (this.readState == SmtpReplyReaderFactory.ReadState.Status0)
				{
					num = 0;
					arrayList.Add(new LineInfo(this.statusCode, stringBuilder.ToString(0, stringBuilder.Length - 2)));
					if (oneLine)
					{
						break;
					}
					stringBuilder = new StringBuilder();
				}
				else if (this.readState == SmtpReplyReaderFactory.ReadState.Done)
				{
					goto Block_7;
				}
			}
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
			Block_7:
			arrayList.Add(new LineInfo(this.statusCode, stringBuilder.ToString(0, stringBuilder.Length - 2)));
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
		}

		// Token: 0x0400186C RID: 6252
		private BufferedReadStream bufferedStream;

		// Token: 0x0400186D RID: 6253
		private byte[] byteBuffer;

		// Token: 0x0400186E RID: 6254
		private SmtpReplyReader currentReader;

		// Token: 0x0400186F RID: 6255
		private const int DefaultBufferSize = 256;

		// Token: 0x04001870 RID: 6256
		private SmtpReplyReaderFactory.ReadState readState;

		// Token: 0x04001871 RID: 6257
		private SmtpStatusCode statusCode;

		// Token: 0x020007A2 RID: 1954
		private enum ReadState
		{
			// Token: 0x040033CA RID: 13258
			Status0,
			// Token: 0x040033CB RID: 13259
			Status1,
			// Token: 0x040033CC RID: 13260
			Status2,
			// Token: 0x040033CD RID: 13261
			ContinueFlag,
			// Token: 0x040033CE RID: 13262
			ContinueCR,
			// Token: 0x040033CF RID: 13263
			ContinueLF,
			// Token: 0x040033D0 RID: 13264
			LastCR,
			// Token: 0x040033D1 RID: 13265
			LastLF,
			// Token: 0x040033D2 RID: 13266
			Done
		}

		// Token: 0x020007A3 RID: 1955
		private class ReadLinesAsyncResult : LazyAsyncResult
		{
			// Token: 0x0600430E RID: 17166 RVA: 0x00119353 File Offset: 0x00117553
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x0600430F RID: 17167 RVA: 0x00119365 File Offset: 0x00117565
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state, bool oneLine) : base(null, state, callback)
			{
				this.oneLine = oneLine;
				this.parent = parent;
			}

			// Token: 0x06004310 RID: 17168 RVA: 0x00119380 File Offset: 0x00117580
			internal void Read(SmtpReplyReader caller)
			{
				if (this.parent.currentReader != caller || this.parent.readState == SmtpReplyReaderFactory.ReadState.Done)
				{
					base.InvokeCallback();
					return;
				}
				if (this.parent.byteBuffer == null)
				{
					this.parent.byteBuffer = new byte[256];
				}
				this.builder = new StringBuilder();
				this.lines = new ArrayList();
				this.Read();
			}

			// Token: 0x06004311 RID: 17169 RVA: 0x001193F0 File Offset: 0x001175F0
			internal static LineInfo[] End(IAsyncResult result)
			{
				SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = (SmtpReplyReaderFactory.ReadLinesAsyncResult)result;
				readLinesAsyncResult.InternalWaitForCompletion();
				return (LineInfo[])readLinesAsyncResult.lines.ToArray(typeof(LineInfo));
			}

			// Token: 0x06004312 RID: 17170 RVA: 0x00119428 File Offset: 0x00117628
			private void Read()
			{
				for (;;)
				{
					IAsyncResult asyncResult = this.parent.bufferedStream.BeginRead(this.parent.byteBuffer, 0, this.parent.byteBuffer.Length, SmtpReplyReaderFactory.ReadLinesAsyncResult.readCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						break;
					}
					this.read = this.parent.bufferedStream.EndRead(asyncResult);
					if (!this.ProcessRead())
					{
						return;
					}
				}
			}

			// Token: 0x06004313 RID: 17171 RVA: 0x00119490 File Offset: 0x00117690
			private static void ReadCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Exception ex = null;
					SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = (SmtpReplyReaderFactory.ReadLinesAsyncResult)result.AsyncState;
					try
					{
						readLinesAsyncResult.read = readLinesAsyncResult.parent.bufferedStream.EndRead(result);
						if (readLinesAsyncResult.ProcessRead())
						{
							readLinesAsyncResult.Read();
						}
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
					if (ex != null)
					{
						readLinesAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x06004314 RID: 17172 RVA: 0x001194FC File Offset: 0x001176FC
			private bool ProcessRead()
			{
				if (this.read == 0)
				{
					throw new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						"net_io_connectionclosed"
					}));
				}
				int num = 0;
				while (num != this.read)
				{
					int num2 = this.parent.ProcessRead(this.parent.byteBuffer, num, this.read - num, true);
					if (this.statusRead < 4)
					{
						int num3 = Math.Min(4 - this.statusRead, num2);
						this.statusRead += num3;
						num += num3;
						num2 -= num3;
						if (num2 == 0)
						{
							continue;
						}
					}
					this.builder.Append(Encoding.UTF8.GetString(this.parent.byteBuffer, num, num2));
					num += num2;
					if (this.parent.readState == SmtpReplyReaderFactory.ReadState.Status0)
					{
						this.lines.Add(new LineInfo(this.parent.statusCode, this.builder.ToString(0, this.builder.Length - 2)));
						this.builder = new StringBuilder();
						this.statusRead = 0;
						if (this.oneLine)
						{
							this.parent.bufferedStream.Push(this.parent.byteBuffer, num, this.read - num);
							base.InvokeCallback();
							return false;
						}
					}
					else if (this.parent.readState == SmtpReplyReaderFactory.ReadState.Done)
					{
						this.lines.Add(new LineInfo(this.parent.statusCode, this.builder.ToString(0, this.builder.Length - 2)));
						this.parent.bufferedStream.Push(this.parent.byteBuffer, num, this.read - num);
						base.InvokeCallback();
						return false;
					}
				}
				return true;
			}

			// Token: 0x040033D3 RID: 13267
			private StringBuilder builder;

			// Token: 0x040033D4 RID: 13268
			private ArrayList lines;

			// Token: 0x040033D5 RID: 13269
			private SmtpReplyReaderFactory parent;

			// Token: 0x040033D6 RID: 13270
			private static AsyncCallback readCallback = new AsyncCallback(SmtpReplyReaderFactory.ReadLinesAsyncResult.ReadCallback);

			// Token: 0x040033D7 RID: 13271
			private int read;

			// Token: 0x040033D8 RID: 13272
			private int statusRead;

			// Token: 0x040033D9 RID: 13273
			private bool oneLine;
		}
	}
}
