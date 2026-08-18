using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200021B RID: 539
	internal class FrameHeader
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x000692AC File Offset: 0x000674AC
		public FrameHeader()
		{
			this._MessageId = 22;
			this._MajorV = 1;
			this._MinorV = 0;
			this._PayloadSize = -1;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000692D1 File Offset: 0x000674D1
		public FrameHeader(int messageId, int majorV, int minorV)
		{
			this._MessageId = messageId;
			this._MajorV = majorV;
			this._MinorV = minorV;
			this._PayloadSize = -1;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x000692F5 File Offset: 0x000674F5
		public int Size
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000692F8 File Offset: 0x000674F8
		public int MaxMessageSize
		{
			get
			{
				return 65535;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x000692FF File Offset: 0x000674FF
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x00069307 File Offset: 0x00067507
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00069310 File Offset: 0x00067510
		public int MajorV
		{
			get
			{
				return this._MajorV;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00069318 File Offset: 0x00067518
		public int MinorV
		{
			get
			{
				return this._MinorV;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00069320 File Offset: 0x00067520
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00069328 File Offset: 0x00067528
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

		// Token: 0x060013E4 RID: 5092 RVA: 0x00069388 File Offset: 0x00067588
		public void CopyTo(byte[] dest, int start)
		{
			dest[start++] = (byte)this._MessageId;
			dest[start++] = (byte)this._MajorV;
			dest[start++] = (byte)this._MinorV;
			dest[start++] = (byte)(this._PayloadSize >> 8 & 255);
			dest[start] = (byte)(this._PayloadSize & 255);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x000693EC File Offset: 0x000675EC
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

		// Token: 0x040015E4 RID: 5604
		public const int IgnoreValue = -1;

		// Token: 0x040015E5 RID: 5605
		public const int HandshakeDoneId = 20;

		// Token: 0x040015E6 RID: 5606
		public const int HandshakeErrId = 21;

		// Token: 0x040015E7 RID: 5607
		public const int HandshakeId = 22;

		// Token: 0x040015E8 RID: 5608
		public const int DefaultMajorV = 1;

		// Token: 0x040015E9 RID: 5609
		public const int DefaultMinorV = 0;

		// Token: 0x040015EA RID: 5610
		private int _MessageId;

		// Token: 0x040015EB RID: 5611
		private int _MajorV;

		// Token: 0x040015EC RID: 5612
		private int _MinorV;

		// Token: 0x040015ED RID: 5613
		private int _PayloadSize;
	}
}
