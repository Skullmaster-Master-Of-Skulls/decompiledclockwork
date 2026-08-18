using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net
{
	// Token: 0x02000224 RID: 548
	internal class Base64Stream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x0006AB58 File Offset: 0x00068D58
		internal Base64Stream(Stream stream, Base64WriteStateInfo writeStateInfo) : base(stream)
		{
			this.writeState = new Base64WriteStateInfo();
			this.lineLength = writeStateInfo.MaxLineLength;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0006AB78 File Offset: 0x00068D78
		internal Base64Stream(Stream stream, int lineLength) : base(stream)
		{
			this.lineLength = lineLength;
			this.writeState = new Base64WriteStateInfo();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0006AB93 File Offset: 0x00068D93
		internal Base64Stream(Base64WriteStateInfo writeStateInfo)
		{
			this.lineLength = writeStateInfo.MaxLineLength;
			this.writeState = writeStateInfo;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0006ABAE File Offset: 0x00068DAE
		public override bool CanWrite
		{
			get
			{
				return base.CanWrite;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0006ABB6 File Offset: 0x00068DB6
		private Base64Stream.ReadStateInfo ReadState
		{
			get
			{
				if (this.readState == null)
				{
					this.readState = new Base64Stream.ReadStateInfo();
				}
				return this.readState;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0006ABD1 File Offset: 0x00068DD1
		internal Base64WriteStateInfo WriteState
		{
			get
			{
				return this.writeState;
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0006ABDC File Offset: 0x00068DDC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
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
			Base64Stream.ReadAsyncResult readAsyncResult = new Base64Stream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			readAsyncResult.Read();
			return readAsyncResult;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006AC34 File Offset: 0x00068E34
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
			Base64Stream.WriteAsyncResult writeAsyncResult = new Base64Stream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006AC8C File Offset: 0x00068E8C
		public override void Close()
		{
			if (this.writeState != null && this.WriteState.Length > 0)
			{
				int padding = this.WriteState.Padding;
				if (padding != 1)
				{
					if (padding == 2)
					{
						this.WriteState.Append(new byte[]
						{
							Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits],
							Base64Stream.base64EncodeMap[64],
							Base64Stream.base64EncodeMap[64]
						});
					}
				}
				else
				{
					this.WriteState.Append(new byte[]
					{
						Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits],
						Base64Stream.base64EncodeMap[64]
					});
				}
				this.WriteState.Padding = 0;
				this.FlushInternal();
			}
			base.Close();
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006AD50 File Offset: 0x00068F50
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
				while (ptr3 < ptr5)
				{
					if (*ptr3 == 13 || *ptr3 == 10 || *ptr3 == 61 || *ptr3 == 32 || *ptr3 == 9)
					{
						ptr3++;
					}
					else
					{
						byte b = Base64Stream.base64DecodeMap[(int)(*ptr3)];
						if (b == 255)
						{
							throw new FormatException(SR.GetString("MailBase64InvalidCharacter"));
						}
						switch (this.ReadState.Pos)
						{
						case 0:
						{
							this.ReadState.Val = (byte)(b << 2);
							Base64Stream.ReadStateInfo readStateInfo = this.ReadState;
							byte pos = readStateInfo.Pos;
							readStateInfo.Pos = pos + 1;
							break;
						}
						case 1:
						{
							*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 4));
							this.ReadState.Val = (byte)(b << 4);
							Base64Stream.ReadStateInfo readStateInfo2 = this.ReadState;
							byte pos = readStateInfo2.Pos;
							readStateInfo2.Pos = pos + 1;
							break;
						}
						case 2:
						{
							*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 2));
							this.ReadState.Val = (byte)(b << 6);
							Base64Stream.ReadStateInfo readStateInfo3 = this.ReadState;
							byte pos = readStateInfo3.Pos;
							readStateInfo3.Pos = pos + 1;
							break;
						}
						case 3:
							*(ptr4++) = this.ReadState.Val + b;
							this.ReadState.Pos = 0;
							break;
						}
						ptr3++;
					}
				}
				count = (int)((long)(ptr4 - ptr2));
			}
			return count;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0006AEEA File Offset: 0x000690EA
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			return this.EncodeBytes(buffer, offset, count, true, true);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0006AEF8 File Offset: 0x000690F8
		internal int EncodeBytes(byte[] buffer, int offset, int count, bool dontDeferFinalBytes, bool shouldAppendSpaceToCRLF)
		{
			int i = offset;
			this.WriteState.AppendHeader();
			int padding = this.WriteState.Padding;
			if (padding != 1)
			{
				if (padding == 2)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits | (buffer[i] & 240) >> 4]);
					if (count == 1)
					{
						this.WriteState.LastBits = (byte)((buffer[i] & 15) << 2);
						this.WriteState.Padding = 1;
						i++;
						return i - offset;
					}
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i] & 15) << 2 | (buffer[i + 1] & 192) >> 6]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 1] & 63)]);
					i += 2;
					count -= 2;
					this.WriteState.Padding = 0;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)this.WriteState.LastBits | (buffer[i] & 192) >> 6]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i] & 63)]);
				i++;
				count--;
				this.WriteState.Padding = 0;
			}
			int num = i + (count - count % 3);
			while (i < num)
			{
				if (this.lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this.writeState.FooterLength > this.lineLength)
				{
					this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
				}
				this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i] & 3) << 4 | (buffer[i + 1] & 240) >> 4]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 1] & 15) << 2 | (buffer[i + 2] & 192) >> 6]);
				this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 2] & 63)]);
				i += 3;
			}
			i = num;
			if (count % 3 != 0 && this.lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this.writeState.FooterLength >= this.lineLength)
			{
				this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
			}
			int num2 = count % 3;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i] & 3) << 4 | (buffer[i + 1] & 240) >> 4]);
					if (dontDeferFinalBytes)
					{
						this.WriteState.Append(Base64Stream.base64EncodeMap[(int)(buffer[i + 1] & 15) << 2]);
						this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
						this.WriteState.Padding = 0;
					}
					else
					{
						this.WriteState.LastBits = (byte)((buffer[i + 1] & 15) << 2);
						this.WriteState.Padding = 1;
					}
					i += 2;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.base64EncodeMap[(buffer[i] & 252) >> 2]);
				if (dontDeferFinalBytes)
				{
					this.WriteState.Append(Base64Stream.base64EncodeMap[(int)((byte)((buffer[i] & 3) << 4))]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
					this.WriteState.Append(Base64Stream.base64EncodeMap[64]);
					this.WriteState.Padding = 0;
				}
				else
				{
					this.WriteState.LastBits = (byte)((buffer[i] & 3) << 4);
					this.WriteState.Padding = 2;
				}
				i++;
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0006B299 File Offset: 0x00069499
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006B29C File Offset: 0x0006949C
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0006B2C0 File Offset: 0x000694C0
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return Base64Stream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0006B2E3 File Offset: 0x000694E3
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Base64Stream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0006B2F9 File Offset: 0x000694F9
		public override void Flush()
		{
			if (this.writeState != null && this.WriteState.Length > 0)
			{
				this.FlushInternal();
			}
			base.Flush();
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0006B31D File Offset: 0x0006951D
		private void FlushInternal()
		{
			base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
			this.WriteState.Reset();
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0006B348 File Offset: 0x00069548
		public override int Read(byte[] buffer, int offset, int count)
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
			for (;;)
			{
				int num = base.Read(buffer, offset, count);
				if (num == 0)
				{
					break;
				}
				num = this.DecodeBytes(buffer, offset, num);
				if (num > 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0006B3AC File Offset: 0x000695AC
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
				num += this.EncodeBytes(buffer, offset + num, count - num, false, false);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x04001620 RID: 5664
		private static byte[] base64DecodeMap = new byte[]
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
			62,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
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
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
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

		// Token: 0x04001621 RID: 5665
		private static byte[] base64EncodeMap = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
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
			43,
			47,
			61
		};

		// Token: 0x04001622 RID: 5666
		private int lineLength;

		// Token: 0x04001623 RID: 5667
		private Base64Stream.ReadStateInfo readState;

		// Token: 0x04001624 RID: 5668
		private Base64WriteStateInfo writeState;

		// Token: 0x04001625 RID: 5669
		private const int sizeOfBase64EncodedChar = 4;

		// Token: 0x04001626 RID: 5670
		private const byte invalidBase64Value = 255;

		// Token: 0x02000765 RID: 1893
		private class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004243 RID: 16963 RVA: 0x00112FF5 File Offset: 0x001111F5
			internal ReadAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06004244 RID: 16964 RVA: 0x00113020 File Offset: 0x00111220
			private bool CompleteRead(IAsyncResult result)
			{
				this.read = this.parent.BaseStream.EndRead(result);
				if (this.read == 0)
				{
					base.InvokeCallback();
					return true;
				}
				this.read = this.parent.DecodeBytes(this.buffer, this.offset, this.read);
				if (this.read > 0)
				{
					base.InvokeCallback();
					return true;
				}
				return false;
			}

			// Token: 0x06004245 RID: 16965 RVA: 0x0011308C File Offset: 0x0011128C
			internal void Read()
			{
				IAsyncResult asyncResult;
				do
				{
					asyncResult = this.parent.BaseStream.BeginRead(this.buffer, this.offset, this.count, Base64Stream.ReadAsyncResult.onRead, this);
				}
				while (asyncResult.CompletedSynchronously && !this.CompleteRead(asyncResult));
			}

			// Token: 0x06004246 RID: 16966 RVA: 0x001130D4 File Offset: 0x001112D4
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result.AsyncState;
					try
					{
						if (!readAsyncResult.CompleteRead(result))
						{
							readAsyncResult.Read();
						}
					}
					catch (Exception result2)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x06004247 RID: 16967 RVA: 0x0011312C File Offset: 0x0011132C
			internal static int End(IAsyncResult result)
			{
				Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult.read;
			}

			// Token: 0x04003248 RID: 12872
			private Base64Stream parent;

			// Token: 0x04003249 RID: 12873
			private byte[] buffer;

			// Token: 0x0400324A RID: 12874
			private int offset;

			// Token: 0x0400324B RID: 12875
			private int count;

			// Token: 0x0400324C RID: 12876
			private int read;

			// Token: 0x0400324D RID: 12877
			private static AsyncCallback onRead = new AsyncCallback(Base64Stream.ReadAsyncResult.OnRead);
		}

		// Token: 0x02000766 RID: 1894
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06004249 RID: 16969 RVA: 0x00113160 File Offset: 0x00111360
			internal WriteAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this.parent = parent;
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x0600424A RID: 16970 RVA: 0x0011318C File Offset: 0x0011138C
			internal void Write()
			{
				for (;;)
				{
					this.written += this.parent.EncodeBytes(this.buffer, this.offset + this.written, this.count - this.written, false, false);
					if (this.written >= this.count)
					{
						break;
					}
					IAsyncResult asyncResult = this.parent.BaseStream.BeginWrite(this.parent.WriteState.Buffer, 0, this.parent.WriteState.Length, Base64Stream.WriteAsyncResult.onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x0600424B RID: 16971 RVA: 0x00113233 File Offset: 0x00111433
			private void CompleteWrite(IAsyncResult result)
			{
				this.parent.BaseStream.EndWrite(result);
				this.parent.WriteState.Reset();
			}

			// Token: 0x0600424C RID: 16972 RVA: 0x00113258 File Offset: 0x00111458
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception result2)
					{
						if (writeAsyncResult.IsCompleted)
						{
							throw;
						}
						writeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x0600424D RID: 16973 RVA: 0x001132AC File Offset: 0x001114AC
			internal static void End(IAsyncResult result)
			{
				Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result;
				writeAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x0400324E RID: 12878
			private Base64Stream parent;

			// Token: 0x0400324F RID: 12879
			private byte[] buffer;

			// Token: 0x04003250 RID: 12880
			private int offset;

			// Token: 0x04003251 RID: 12881
			private int count;

			// Token: 0x04003252 RID: 12882
			private static AsyncCallback onWrite = new AsyncCallback(Base64Stream.WriteAsyncResult.OnWrite);

			// Token: 0x04003253 RID: 12883
			private int written;
		}

		// Token: 0x02000767 RID: 1895
		private class ReadStateInfo
		{
			// Token: 0x17000F2A RID: 3882
			// (get) Token: 0x0600424F RID: 16975 RVA: 0x001132DA File Offset: 0x001114DA
			// (set) Token: 0x06004250 RID: 16976 RVA: 0x001132E2 File Offset: 0x001114E2
			internal byte Val
			{
				get
				{
					return this.val;
				}
				set
				{
					this.val = value;
				}
			}

			// Token: 0x17000F2B RID: 3883
			// (get) Token: 0x06004251 RID: 16977 RVA: 0x001132EB File Offset: 0x001114EB
			// (set) Token: 0x06004252 RID: 16978 RVA: 0x001132F3 File Offset: 0x001114F3
			internal byte Pos
			{
				get
				{
					return this.pos;
				}
				set
				{
					this.pos = value;
				}
			}

			// Token: 0x04003254 RID: 12884
			private byte val;

			// Token: 0x04003255 RID: 12885
			private byte pos;
		}
	}
}
