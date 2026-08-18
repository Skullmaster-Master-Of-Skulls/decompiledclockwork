using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020006D6 RID: 1750
	internal class SmtpReplyReaderFactory
	{
		// Token: 0x0600360A RID: 13834 RVA: 0x000E67B6 File Offset: 0x000E57B6
		internal SmtpReplyReaderFactory(Stream stream)
		{
			this.bufferedStream = new BufferedReadStream(stream);
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000E67CA File Offset: 0x000E57CA
		internal SmtpReplyReader CurrentReader
		{
			get
			{
				return this.currentReader;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x000E67D2 File Offset: 0x000E57D2
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000E67DC File Offset: 0x000E57DC
		internal IAsyncResult BeginReadLines(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000E67FC File Offset: 0x000E57FC
		internal IAsyncResult BeginReadLine(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state, true);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000E681C File Offset: 0x000E581C
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

		// Token: 0x06003610 RID: 13840 RVA: 0x000E6872 File Offset: 0x000E5872
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000E687C File Offset: 0x000E587C
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			LineInfo[] array = SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000E68AF File Offset: 0x000E58AF
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

		// Token: 0x06003613 RID: 13843 RVA: 0x000E68E0 File Offset: 0x000E58E0
		private unsafe int ProcessRead(byte[] buffer, int offset, int read, bool readLine)
		{
			if (read == 0)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					"net_io_connectionclosed"
				}));
			}
			fixed (byte* ptr = buffer)
			{
				byte* ptr2 = ptr + offset;
				byte* ptr3 = ptr2;
				byte* ptr4 = ptr3 + read;
				switch (this.readState)
				{
				case SmtpReplyReaderFactory.ReadState.Status0:
					goto IL_83;
				case SmtpReplyReaderFactory.ReadState.Status1:
					goto IL_C8;
				case SmtpReplyReaderFactory.ReadState.Status2:
					goto IL_114;
				case SmtpReplyReaderFactory.ReadState.ContinueFlag:
					goto IL_15D;
				case SmtpReplyReaderFactory.ReadState.ContinueCR:
					break;
				case SmtpReplyReaderFactory.ReadState.ContinueLF:
					goto IL_1B0;
				case SmtpReplyReaderFactory.ReadState.LastCR:
					goto IL_1FC;
				case SmtpReplyReaderFactory.ReadState.LastLF:
					goto IL_209;
				case SmtpReplyReaderFactory.ReadState.Done:
					goto IL_231;
				default:
					goto IL_247;
				}
				IL_1A0:
				while (ptr3 < ptr4)
				{
					if (*(ptr3++) == 13)
					{
						goto IL_1B0;
					}
				}
				this.readState = SmtpReplyReaderFactory.ReadState.ContinueCR;
				goto IL_247;
				IL_1FC:
				while (ptr3 < ptr4)
				{
					if (*(ptr3++) == 13)
					{
						goto IL_209;
					}
				}
				this.readState = SmtpReplyReaderFactory.ReadState.LastCR;
				goto IL_247;
				IL_83:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.Status0;
					goto IL_247;
				}
				byte b = *(ptr3++);
				if (b < 48 && b > 57)
				{
					throw new FormatException(SR.GetString("SmtpInvalidResponse"));
				}
				this.statusCode = (SmtpStatusCode)(100 * (b - 48));
				IL_C8:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.Status1;
					goto IL_247;
				}
				byte b2 = *(ptr3++);
				if (b2 < 48 && b2 > 57)
				{
					throw new FormatException(SR.GetString("SmtpInvalidResponse"));
				}
				this.statusCode += (int)(10 * (b2 - 48));
				IL_114:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.Status2;
					goto IL_247;
				}
				byte b3 = *(ptr3++);
				if (b3 < 48 && b3 > 57)
				{
					throw new FormatException(SR.GetString("SmtpInvalidResponse"));
				}
				this.statusCode += (int)(b3 - 48);
				IL_15D:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.ContinueFlag;
					goto IL_247;
				}
				byte b4 = *(ptr3++);
				if (b4 == 32)
				{
					goto IL_1FC;
				}
				if (b4 != 45)
				{
					throw new FormatException(SR.GetString("SmtpInvalidResponse"));
				}
				goto IL_1A0;
				IL_1B0:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.ContinueLF;
					goto IL_247;
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
				goto IL_83;
				IL_209:
				if (ptr3 >= ptr4)
				{
					this.readState = SmtpReplyReaderFactory.ReadState.LastLF;
					goto IL_247;
				}
				if (*(ptr3++) != 10)
				{
					throw new FormatException(SR.GetString("SmtpInvalidResponse"));
				}
				IL_231:
				int result = (int)((long)(ptr3 - ptr2));
				this.readState = SmtpReplyReaderFactory.ReadState.Done;
				return result;
				IL_247:
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000E6B44 File Offset: 0x000E5B44
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

		// Token: 0x06003615 RID: 13845 RVA: 0x000E6B9C File Offset: 0x000E5B9C
		internal LineInfo ReadLine(SmtpReplyReader caller)
		{
			LineInfo[] array = this.ReadLines(caller, true);
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000E6BD1 File Offset: 0x000E5BD1
		internal LineInfo[] ReadLines(SmtpReplyReader caller)
		{
			return this.ReadLines(caller, false);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000E6BDC File Offset: 0x000E5BDC
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
			if (this.charBuffer == null)
			{
				this.charBuffer = new char[256];
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
				for (int i = num2; i < num2 + num4; i++)
				{
					this.charBuffer[i] = (char)this.byteBuffer[i];
				}
				stringBuilder.Append(this.charBuffer, num2, num4);
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
					goto Block_9;
				}
			}
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
			Block_9:
			arrayList.Add(new LineInfo(this.statusCode, stringBuilder.ToString(0, stringBuilder.Length - 2)));
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
		}

		// Token: 0x04003128 RID: 12584
		private const int DefaultBufferSize = 256;

		// Token: 0x04003129 RID: 12585
		private BufferedReadStream bufferedStream;

		// Token: 0x0400312A RID: 12586
		private byte[] byteBuffer;

		// Token: 0x0400312B RID: 12587
		private char[] charBuffer;

		// Token: 0x0400312C RID: 12588
		private SmtpReplyReader currentReader;

		// Token: 0x0400312D RID: 12589
		private SmtpReplyReaderFactory.ReadState readState;

		// Token: 0x0400312E RID: 12590
		private SmtpStatusCode statusCode;

		// Token: 0x020006D7 RID: 1751
		private enum ReadState
		{
			// Token: 0x04003130 RID: 12592
			Status0,
			// Token: 0x04003131 RID: 12593
			Status1,
			// Token: 0x04003132 RID: 12594
			Status2,
			// Token: 0x04003133 RID: 12595
			ContinueFlag,
			// Token: 0x04003134 RID: 12596
			ContinueCR,
			// Token: 0x04003135 RID: 12597
			ContinueLF,
			// Token: 0x04003136 RID: 12598
			LastCR,
			// Token: 0x04003137 RID: 12599
			LastLF,
			// Token: 0x04003138 RID: 12600
			Done
		}

		// Token: 0x020006D8 RID: 1752
		private class ReadLinesAsyncResult : LazyAsyncResult
		{
			// Token: 0x06003618 RID: 13848 RVA: 0x000E6DA2 File Offset: 0x000E5DA2
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x06003619 RID: 13849 RVA: 0x000E6DB4 File Offset: 0x000E5DB4
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state, bool oneLine) : base(null, state, callback)
			{
				this.oneLine = oneLine;
				this.parent = parent;
			}

			// Token: 0x0600361A RID: 13850 RVA: 0x000E6DD0 File Offset: 0x000E5DD0
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
				if (this.parent.charBuffer == null)
				{
					this.parent.charBuffer = new char[256];
				}
				this.builder = new StringBuilder();
				this.lines = new ArrayList();
				this.Read();
			}

			// Token: 0x0600361B RID: 13851 RVA: 0x000E6E60 File Offset: 0x000E5E60
			internal static LineInfo[] End(IAsyncResult result)
			{
				SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = (SmtpReplyReaderFactory.ReadLinesAsyncResult)result;
				readLinesAsyncResult.InternalWaitForCompletion();
				return (LineInfo[])readLinesAsyncResult.lines.ToArray(typeof(LineInfo));
			}

			// Token: 0x0600361C RID: 13852 RVA: 0x000E6E98 File Offset: 0x000E5E98
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

			// Token: 0x0600361D RID: 13853 RVA: 0x000E6F00 File Offset: 0x000E5F00
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
					catch
					{
						ex = new Exception(SR.GetString("net_nonClsCompliantException"));
					}
					if (ex != null)
					{
						readLinesAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x0600361E RID: 13854 RVA: 0x000E6F88 File Offset: 0x000E5F88
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
					for (int i = num; i < num + num2; i++)
					{
						this.parent.charBuffer[i] = (char)this.parent.byteBuffer[i];
					}
					this.builder.Append(this.parent.charBuffer, num, num2);
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

			// Token: 0x04003139 RID: 12601
			private StringBuilder builder;

			// Token: 0x0400313A RID: 12602
			private ArrayList lines;

			// Token: 0x0400313B RID: 12603
			private SmtpReplyReaderFactory parent;

			// Token: 0x0400313C RID: 12604
			private static AsyncCallback readCallback = new AsyncCallback(SmtpReplyReaderFactory.ReadLinesAsyncResult.ReadCallback);

			// Token: 0x0400313D RID: 12605
			private int read;

			// Token: 0x0400313E RID: 12606
			private int statusRead;

			// Token: 0x0400313F RID: 12607
			private bool oneLine;
		}
	}
}
