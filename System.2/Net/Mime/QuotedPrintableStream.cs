using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000250 RID: 592
	internal class QuotedPrintableStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x000747C4 File Offset: 0x000729C4
		internal QuotedPrintableStream(Stream stream, int lineLength) : base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this.lineLength = lineLength;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000747E3 File Offset: 0x000729E3
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF) : this(stream, EncodedStreamFactory.DefaultMaxLineLength)
		{
			this.encodeCRLF = encodeCRLF;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000747F8 File Offset: 0x000729F8
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00074813 File Offset: 0x00072A13
		internal WriteStateInfoBase WriteState
		{
			get
			{
				if (this.writeState == null)
				{
					this.writeState = new WriteStateInfoBase(1024, null, null, this.lineLength);
				}
				return this.writeState;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0007483C File Offset: 0x00072A3C
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

		// Token: 0x06001687 RID: 5767 RVA: 0x00074894 File Offset: 0x00072A94
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000748A4 File Offset: 0x00072AA4
		public unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
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
					if (*ptr3 == 61)
					{
						long num = (long)(ptr5 - ptr3);
						if (num != 1L)
						{
							if (num != 2L)
							{
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
								continue;
							}
							this.ReadState.Byte = (short)ptr3[1];
						}
						this.ReadState.IsEscaped = true;
						break;
					}
					*(ptr4++) = *(ptr3++);
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00074B40 File Offset: 0x00072D40
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.lineLength != -1 && this.WriteState.CurrentLineLength + 3 + 2 >= this.lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.writeState.CurrentLineLength + 3 + 2 >= EncodedStreamFactory.DefaultMaxLineLength)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.AppendCRLF(false);
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
						this.WriteState.Append(new byte[]
						{
							61,
							48,
							68,
							61,
							48,
							65
						});
					}
					else
					{
						this.WriteState.AppendCRLF(false);
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[(int)(buffer[i] & 15)]);
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					if ((buffer[i] == 9 || buffer[i] == 32) && i + 1 >= count + offset)
					{
						if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
						{
							return i - offset;
						}
						this.WriteState.Append(61);
						this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[buffer[i] >> 4]);
						this.WriteState.Append(QuotedPrintableStream.hexEncodeMap[(int)(buffer[i] & 15)]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			return i - offset;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00074D8B File Offset: 0x00072F8B
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00074D8E File Offset: 0x00072F8E
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00074DB1 File Offset: 0x00072FB1
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00074DB9 File Offset: 0x00072FB9
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00074DC7 File Offset: 0x00072FC7
		private void FlushInternal()
		{
			if (this.writeState != null && this.writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00074E08 File Offset: 0x00073008
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

		// Token: 0x04001749 RID: 5961
		private bool encodeCRLF;

		// Token: 0x0400174A RID: 5962
		private const int sizeOfSoftCRLF = 3;

		// Token: 0x0400174B RID: 5963
		private const int sizeOfEncodedChar = 3;

		// Token: 0x0400174C RID: 5964
		private const int sizeOfEncodedCRLF = 6;

		// Token: 0x0400174D RID: 5965
		private const int sizeOfNonEncodedCRLF = 2;

		// Token: 0x0400174E RID: 5966
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

		// Token: 0x0400174F RID: 5967
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

		// Token: 0x04001750 RID: 5968
		private int lineLength;

		// Token: 0x04001751 RID: 5969
		private QuotedPrintableStream.ReadStateInfo readState;

		// Token: 0x04001752 RID: 5970
		private WriteStateInfoBase writeState;

		// Token: 0x0200079B RID: 1947
		private class ReadStateInfo
		{
			// Token: 0x17000F43 RID: 3907
			// (get) Token: 0x060042ED RID: 17133 RVA: 0x001183F8 File Offset: 0x001165F8
			// (set) Token: 0x060042EE RID: 17134 RVA: 0x00118400 File Offset: 0x00116600
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

			// Token: 0x17000F44 RID: 3908
			// (get) Token: 0x060042EF RID: 17135 RVA: 0x00118409 File Offset: 0x00116609
			// (set) Token: 0x060042F0 RID: 17136 RVA: 0x00118411 File Offset: 0x00116611
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

			// Token: 0x040033A5 RID: 13221
			private bool isEscaped;

			// Token: 0x040033A6 RID: 13222
			private short b1 = -1;
		}

		// Token: 0x0200079C RID: 1948
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042F2 RID: 17138 RVA: 0x00118429 File Offset: 0x00116629
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x060042F3 RID: 17139 RVA: 0x00118453 File Offset: 0x00116653
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.BufferFlushed();
			}

			// Token: 0x060042F4 RID: 17140 RVA: 0x00118478 File Offset: 0x00116678
			internal static void End(IAsyncResult result)
			{
				QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result;
				writeAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x060042F5 RID: 17141 RVA: 0x00118494 File Offset: 0x00116694
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
				}
			}

			// Token: 0x060042F6 RID: 17142 RVA: 0x001184E0 File Offset: 0x001166E0
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x040033A7 RID: 13223
			private QuotedPrintableStream parent;

			// Token: 0x040033A8 RID: 13224
			private byte[] buffer;

			// Token: 0x040033A9 RID: 13225
			private int offset;

			// Token: 0x040033AA RID: 13226
			private int count;

			// Token: 0x040033AB RID: 13227
			private static AsyncCallback onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x040033AC RID: 13228
			private int written;
		}
	}
}
