using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x020006B2 RID: 1714
	internal class QuotedPrintableStream : DelegatedStream
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x000E1035 File Offset: 0x000E0035
		internal QuotedPrintableStream(Stream stream, int lineLength) : base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this.lineLength = lineLength;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000E1054 File Offset: 0x000E0054
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF) : this(stream, QuotedPrintableStream.DefaultLineLength)
		{
			this.encodeCRLF = encodeCRLF;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000E1069 File Offset: 0x000E0069
		internal QuotedPrintableStream()
		{
			this.lineLength = QuotedPrintableStream.DefaultLineLength;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000E107C File Offset: 0x000E007C
		internal QuotedPrintableStream(int lineLength)
		{
			this.lineLength = lineLength;
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x000E108B File Offset: 0x000E008B
		private QuotedPrintableStream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new QuotedPrintableStream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000E10A6 File Offset: 0x000E00A6
		internal QuotedPrintableStream.WriteStateInfo WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new QuotedPrintableStream.WriteStateInfo(1024);
				}
				return this.writeState;
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000E10C8 File Offset: 0x000E00C8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			QuotedPrintableStream.WriteAsyncResult writeAsyncResult = new QuotedPrintableStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000E1120 File Offset: 0x000E0120
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000E1130 File Offset: 0x000E0130
		internal unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			try
			{
				fixed (byte* ptr = buffer)
				{
					byte* ptr2 = ptr + offset;
					byte* ptr3 = ptr2;
					byte* ptr4 = ptr2;
					byte* ptr5 = ptr2 + count;
					if (this.ReadState.IsEscaped)
					{
						if (this.ReadState.Byte == -1)
						{
							if (count == 1)
							{
								this.ReadState.Byte = (short)(*ptr3);
								return 0;
							}
							if (*ptr3 != 13 || ptr3[1] != 10)
							{
								byte b = QuotedPrintableStream.hexDecodeMap[(int)(*ptr3)];
								byte b2 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[1]];
								if (b == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b
									}));
								}
								if (b2 == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b2
									}));
								}
								*(ptr4++) = (byte)(((int)b << 4) + (int)b2);
							}
							ptr3 += 2;
						}
						else
						{
							if (this.ReadState.Byte != 13 || *ptr3 != 10)
							{
								byte b3 = QuotedPrintableStream.hexDecodeMap[(int)this.ReadState.Byte];
								byte b4 = QuotedPrintableStream.hexDecodeMap[(int)(*ptr3)];
								if (b3 == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b3
									}));
								}
								if (b4 == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b4
									}));
								}
								*(ptr4++) = (byte)(((int)b3 << 4) + (int)b4);
							}
							ptr3++;
						}
						this.ReadState.IsEscaped = false;
						this.ReadState.Byte = -1;
					}
					while (ptr3 < ptr5)
					{
						if (*ptr3 != 61)
						{
							*(ptr4++) = *(ptr3++);
						}
						else
						{
							long num = (long)(ptr5 - ptr3);
							if (num <= 2L && num >= 1L)
							{
								switch ((int)(num - 1L))
								{
								case 0:
									break;
								case 1:
									this.ReadState.Byte = (short)ptr3[1];
									break;
								default:
									goto IL_226;
								}
								this.ReadState.IsEscaped = true;
								break;
							}
							IL_226:
							if (ptr3[1] != 13 || ptr3[2] != 10)
							{
								byte b5 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[1]];
								byte b6 = QuotedPrintableStream.hexDecodeMap[(int)ptr3[2]];
								if (b5 == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b5
									}));
								}
								if (b6 == 255)
								{
									throw new FormatException(SR.GetString("InvalidHexDigit", new object[]
									{
										b6
									}));
								}
								*(ptr4++) = (byte)(((int)b5 << 4) + (int)b6);
							}
							ptr3 += 3;
						}
					}
					count = (int)((long)(ptr4 - ptr2));
				}
			}
			finally
			{
				byte* ptr = null;
			}
			return count;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000E143C File Offset: 0x000E043C
		internal int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if (this.lineLength != -1 && this.WriteState.CurrentLineLength + 5 >= this.lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10))
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.CurrentLineLength = 0;
					this.WriteState.Buffer[this.WriteState.Length++] = 61;
					this.WriteState.Buffer[this.WriteState.Length++] = 13;
					this.WriteState.Buffer[this.WriteState.Length++] = 10;
				}
				if (this.WriteState.CurrentLineLength == 0 && buffer[i] == 46)
				{
					this.WriteState.Buffer[this.WriteState.Length++] = 46;
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < (this.encodeCRLF ? 6 : 2))
					{
						return i - offset;
					}
					i++;
					if (this.encodeCRLF)
					{
						this.WriteState.Buffer[this.WriteState.Length++] = 61;
						this.WriteState.Buffer[this.WriteState.Length++] = 48;
						this.WriteState.Buffer[this.WriteState.Length++] = 68;
						this.WriteState.Buffer[this.WriteState.Length++] = 61;
						this.WriteState.Buffer[this.WriteState.Length++] = 48;
						this.WriteState.Buffer[this.WriteState.Length++] = 65;
						this.WriteState.CurrentLineLength += 6;
					}
					else
					{
						this.WriteState.Buffer[this.WriteState.Length++] = 13;
						this.WriteState.Buffer[this.WriteState.Length++] = 10;
						this.WriteState.CurrentLineLength = 0;
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.CurrentLineLength += 3;
					this.WriteState.Buffer[this.WriteState.Length++] = 61;
					this.WriteState.Buffer[this.WriteState.Length++] = QuotedPrintableStream.hexEncodeMap[buffer[i] >> 4];
					this.WriteState.Buffer[this.WriteState.Length++] = QuotedPrintableStream.hexEncodeMap[(int)(buffer[i] & 15)];
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					this.WriteState.CurrentLineLength++;
					this.WriteState.Buffer[this.WriteState.Length++] = buffer[i];
				}
			}
			return i - offset;
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000E185C File Offset: 0x000E085C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000E1864 File Offset: 0x000E0864
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000E1874 File Offset: 0x000E0874
		private void FlushInternal()
		{
			if (this.writeState != null && this.writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.Length = 0;
			}
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000E18C0 File Offset: 0x000E08C0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			for (;;)
			{
				num += this.EncodeBytes(buffer, offset + num, count - num);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x040030A2 RID: 12450
		private bool encodeCRLF;

		// Token: 0x040030A3 RID: 12451
		private static int DefaultLineLength = 76;

		// Token: 0x040030A4 RID: 12452
		private static byte[] hexDecodeMap = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040030A5 RID: 12453
		private static byte[] hexEncodeMap = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			65,
			66,
			67,
			68,
			69,
			70
		};

		// Token: 0x040030A6 RID: 12454
		private int lineLength;

		// Token: 0x040030A7 RID: 12455
		private QuotedPrintableStream.ReadStateInfo readState;

		// Token: 0x040030A8 RID: 12456
		private QuotedPrintableStream.WriteStateInfo writeState;

		// Token: 0x020006B3 RID: 1715
		private class ReadStateInfo
		{
			// Token: 0x17000C60 RID: 3168
			// (get) Token: 0x0600350C RID: 13580 RVA: 0x000E1A72 File Offset: 0x000E0A72
			// (set) Token: 0x0600350D RID: 13581 RVA: 0x000E1A7A File Offset: 0x000E0A7A
			internal bool IsEscaped
			{
				get
				{
					return this.isEscaped;
				}
				set
				{
					this.isEscaped = value;
				}
			}

			// Token: 0x17000C61 RID: 3169
			// (get) Token: 0x0600350E RID: 13582 RVA: 0x000E1A83 File Offset: 0x000E0A83
			// (set) Token: 0x0600350F RID: 13583 RVA: 0x000E1A8B File Offset: 0x000E0A8B
			internal short Byte
			{
				get
				{
					return this.b1;
				}
				set
				{
					this.b1 = value;
				}
			}

			// Token: 0x040030A9 RID: 12457
			private bool isEscaped;

			// Token: 0x040030AA RID: 12458
			private short b1 = -1;
		}

		// Token: 0x020006B4 RID: 1716
		internal class WriteStateInfo
		{
			// Token: 0x06003511 RID: 13585 RVA: 0x000E1AA3 File Offset: 0x000E0AA3
			internal WriteStateInfo(int bufferSize)
			{
				this.buffer = new byte[bufferSize];
			}

			// Token: 0x17000C62 RID: 3170
			// (get) Token: 0x06003512 RID: 13586 RVA: 0x000E1AB7 File Offset: 0x000E0AB7
			internal byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x17000C63 RID: 3171
			// (get) Token: 0x06003513 RID: 13587 RVA: 0x000E1ABF File Offset: 0x000E0ABF
			// (set) Token: 0x06003514 RID: 13588 RVA: 0x000E1AC7 File Offset: 0x000E0AC7
			internal int CurrentLineLength
			{
				get
				{
					return this.currentLineLength;
				}
				set
				{
					this.currentLineLength = value;
				}
			}

			// Token: 0x17000C64 RID: 3172
			// (get) Token: 0x06003515 RID: 13589 RVA: 0x000E1AD0 File Offset: 0x000E0AD0
			// (set) Token: 0x06003516 RID: 13590 RVA: 0x000E1AD8 File Offset: 0x000E0AD8
			internal int Length
			{
				get
				{
					return this.length;
				}
				set
				{
					this.length = value;
				}
			}

			// Token: 0x040030AB RID: 12459
			private int currentLineLength;

			// Token: 0x040030AC RID: 12460
			private byte[] buffer;

			// Token: 0x040030AD RID: 12461
			private int length;
		}

		// Token: 0x020006B5 RID: 1717
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06003517 RID: 13591 RVA: 0x000E1AE1 File Offset: 0x000E0AE1
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06003518 RID: 13592 RVA: 0x000E1B0B File Offset: 0x000E0B0B
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.Length = 0;
			}

			// Token: 0x06003519 RID: 13593 RVA: 0x000E1B30 File Offset: 0x000E0B30
			internal static void End(IAsyncResult result)
			{
				QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result;
				writeAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x0600351A RID: 13594 RVA: 0x000E1B4C File Offset: 0x000E0B4C
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception result2)
					{
						writeAsyncResult.InvokeCallback(result2);
					}
					catch
					{
						writeAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
					}
				}
			}

			// Token: 0x0600351B RID: 13595 RVA: 0x000E1BBC File Offset: 0x000E0BBC
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written);
					if (this.written >= this.count)
					{
						goto IL_93;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						break;
					}
					this.CompleteWrite(asyncResult);
				}
				return;
				IL_93:
				base.InvokeCallback();
			}

			// Token: 0x040030AE RID: 12462
			private QuotedPrintableStream parent;

			// Token: 0x040030AF RID: 12463
			private byte[] buffer;

			// Token: 0x040030B0 RID: 12464
			private int offset;

			// Token: 0x040030B1 RID: 12465
			private int count;

			// Token: 0x040030B2 RID: 12466
			private static AsyncCallback onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x040030B3 RID: 12467
			private int written;
		}
	}
}
