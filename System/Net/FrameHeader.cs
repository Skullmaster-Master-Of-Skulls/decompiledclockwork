using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200054E RID: 1358
	internal class FrameHeader
	{
		// Token: 0x06002938 RID: 10552 RVA: 0x000AC6B8 File Offset: 0x000AB6B8
		public FrameHeader()
		{
			this._MessageId = 22;
			this._MajorV = 1;
			this._MinorV = 0;
			this._PayloadSize = -1;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000AC6DD File Offset: 0x000AB6DD
		public FrameHeader(int messageId, int majorV, int minorV)
		{
			this._MessageId = messageId;
			this._MajorV = majorV;
			this._MinorV = minorV;
			this._PayloadSize = -1;
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x000AC701 File Offset: 0x000AB701
		public int Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000AC704 File Offset: 0x000AB704
		public int MaxMessageSize
		{
			get
			{
				return 65535;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000AC70B File Offset: 0x000AB70B
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x000AC713 File Offset: 0x000AB713
		public int MessageId
		{
			get
			{
				return this._MessageId;
			}
			set
			{
				this._MessageId = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x000AC71C File Offset: 0x000AB71C
		public int MajorV
		{
			get
			{
				return this._MajorV;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000AC724 File Offset: 0x000AB724
		public int MinorV
		{
			get
			{
				return this._MinorV;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000AC72C File Offset: 0x000AB72C
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x000AC734 File Offset: 0x000AB734
		public int PayloadSize
		{
			get
			{
				return this._PayloadSize;
			}
			set
			{
				if (value > this.MaxMessageSize)
				{
					throw new ArgumentException(SR.GetString("net_frame_max_size", new object[]
					{
						this.MaxMessageSize.ToString(NumberFormatInfo.InvariantInfo),
						value.ToString(NumberFormatInfo.InvariantInfo)
					}), "PayloadSize");
				}
				this._PayloadSize = value;
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000AC794 File Offset: 0x000AB794
		public void CopyTo(byte[] dest, int start)
		{
			dest[start++] = (byte)this._MessageId;
			dest[start++] = (byte)this._MajorV;
			dest[start++] = (byte)this._MinorV;
			dest[start++] = (byte)(this._PayloadSize >> 8 & 255);
			dest[start] = (byte)(this._PayloadSize & 255);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000AC7F8 File Offset: 0x000AB7F8
		public void CopyFrom(byte[] bytes, int start, FrameHeader verifier)
		{
			this._MessageId = (int)bytes[start++];
			this._MajorV = (int)bytes[start++];
			this._MinorV = (int)bytes[start++];
			this._PayloadSize = ((int)bytes[start++] << 8 | (int)bytes[start]);
			if (verifier.MessageId != -1 && this.MessageId != verifier.MessageId)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[]
				{
					"MessageId",
					this.MessageId,
					verifier.MessageId
				}));
			}
			if (verifier.MajorV != -1 && this.MajorV != verifier.MajorV)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[]
				{
					"MajorV",
					this.MajorV,
					verifier.MajorV
				}));
			}
			if (verifier.MinorV != -1 && this.MinorV != verifier.MinorV)
			{
				throw new InvalidOperationException(SR.GetString("net_io_header_id", new object[]
				{
					"MinorV",
					this.MinorV,
					verifier.MinorV
				}));
			}
		}

		// Token: 0x0400284F RID: 10319
		public const int IgnoreValue = -1;

		// Token: 0x04002850 RID: 10320
		public const int HandshakeDoneId = 20;

		// Token: 0x04002851 RID: 10321
		public const int HandshakeErrId = 21;

		// Token: 0x04002852 RID: 10322
		public const int HandshakeId = 22;

		// Token: 0x04002853 RID: 10323
		public const int DefaultMajorV = 1;

		// Token: 0x04002854 RID: 10324
		public const int DefaultMinorV = 0;

		// Token: 0x04002855 RID: 10325
		private int _MessageId;

		// Token: 0x04002856 RID: 10326
		private int _MajorV;

		// Token: 0x04002857 RID: 10327
		private int _MinorV;

		// Token: 0x04002858 RID: 10328
		private int _PayloadSize;
	}
}
