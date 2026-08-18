using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200024F RID: 591
	internal class QEncodedStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x00074211 File Offset: 0x00072411
		internal QEncodedStream(WriteStateInfoBase wsi)
		{
			this.writeState = wsi;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00074220 File Offset: 0x00072420
		private QEncodedStream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new QEncodedStream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0007423B File Offset: 0x0007243B
		internal WriteStateInfoBase WriteState
		{
			get
			{
				return this.writeState;
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00074244 File Offset: 0x00072444
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
			QEncodedStream.WriteAsyncResult writeAsyncResult = new QEncodedStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0007429C File Offset: 0x0007249C
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000742AC File Offset: 0x000724AC
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
							byte b = QEncodedStream.hexDecodeMap[(int)(*ptr3)];
							byte b2 = QEncodedStream.hexDecodeMap[(int)ptr3[1]];
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
							byte b3 = QEncodedStream.hexDecodeMap[(int)this.ReadState.Byte];
							byte b4 = QEncodedStream.hexDecodeMap[(int)(*ptr3)];
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
									byte b5 = QEncodedStream.hexDecodeMap[(int)ptr3[1]];
									byte b6 = QEncodedStream.hexDecodeMap[(int)ptr3[2]];
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
					if (*ptr3 == 95)
					{
						*(ptr4++) = 32;
						ptr3++;
					}
					else
					{
						*(ptr4++) = *(ptr3++);
					}
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00074564 File Offset: 0x00072764
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			this.writeState.AppendHeader();
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.WriteState.CurrentLineLength + 3 + this.WriteState.FooterLength >= this.WriteState.MaxLineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.WriteState.CurrentLineLength + this.writeState.FooterLength >= this.WriteState.MaxLineLength)
				{
					this.WriteState.AppendCRLF(true);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					i++;
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
				else if (buffer[i] == 32)
				{
					this.WriteState.Append(95);
				}
				else if (Uri.IsAsciiLetterOrDigit((char)buffer[i]))
				{
					this.WriteState.Append(buffer[i]);
				}
				else
				{
					this.WriteState.Append(61);
					this.WriteState.Append(QEncodedStream.hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QEncodedStream.hexEncodeMap[(int)(buffer[i] & 15)]);
				}
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000746B4 File Offset: 0x000728B4
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000746B7 File Offset: 0x000728B7
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000746DA File Offset: 0x000728DA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QEncodedStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000746E2 File Offset: 0x000728E2
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000746F0 File Offset: 0x000728F0
		private void FlushInternal()
		{
			if (this.writeState != null && this.writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.Reset();
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00074730 File Offset: 0x00072930
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

		// Token: 0x04001744 RID: 5956
		private const int sizeOfFoldingCRLF = 3;

		// Token: 0x04001745 RID: 5957
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

		// Token: 0x04001746 RID: 5958
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

		// Token: 0x04001747 RID: 5959
		private QEncodedStream.ReadStateInfo readState;

		// Token: 0x04001748 RID: 5960
		private WriteStateInfoBase writeState;

		// Token: 0x02000799 RID: 1945
		private class ReadStateInfo
		{
			// Token: 0x17000F41 RID: 3905
			// (get) Token: 0x060042E2 RID: 17122 RVA: 0x00118258 File Offset: 0x00116458
			// (set) Token: 0x060042E3 RID: 17123 RVA: 0x00118260 File Offset: 0x00116460
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

			// Token: 0x17000F42 RID: 3906
			// (get) Token: 0x060042E4 RID: 17124 RVA: 0x00118269 File Offset: 0x00116469
			// (set) Token: 0x060042E5 RID: 17125 RVA: 0x00118271 File Offset: 0x00116471
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

			// Token: 0x0400339D RID: 13213
			private bool isEscaped;

			// Token: 0x0400339E RID: 13214
			private short b1 = -1;
		}

		// Token: 0x0200079A RID: 1946
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042E7 RID: 17127 RVA: 0x00118289 File Offset: 0x00116489
			internal WriteAsyncResult(QEncodedStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x060042E8 RID: 17128 RVA: 0x001182B3 File Offset: 0x001164B3
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.Reset();
			}

			// Token: 0x060042E9 RID: 17129 RVA: 0x001182D8 File Offset: 0x001164D8
			internal static void End(IAsyncResult result)
			{
				QEncodedStream.WriteAsyncResult writeAsyncResult = (QEncodedStream.WriteAsyncResult)result;
				writeAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x060042EA RID: 17130 RVA: 0x001182F4 File Offset: 0x001164F4
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QEncodedStream.WriteAsyncResult writeAsyncResult = (QEncodedStream.WriteAsyncResult)result.AsyncState;
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

			// Token: 0x060042EB RID: 17131 RVA: 0x00118340 File Offset: 0x00116540
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, QEncodedStream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x0400339F RID: 13215
			private QEncodedStream parent;

			// Token: 0x040033A0 RID: 13216
			private byte[] buffer;

			// Token: 0x040033A1 RID: 13217
			private int offset;

			// Token: 0x040033A2 RID: 13218
			private int count;

			// Token: 0x040033A3 RID: 13219
			private static AsyncCallback onWrite = new AsyncCallback(QEncodedStream.WriteAsyncResult.OnWrite);

			// Token: 0x040033A4 RID: 13220
			private int written;
		}
	}
}
