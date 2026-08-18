using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000865 RID: 2149
	internal abstract class BytesReadPositionStream : DelegatingStream
	{
		// Token: 0x060050EA RID: 20714 RVA: 0x00129C73 File Offset: 0x00127E73
		protected BytesReadPositionStream(Stream stream) : base(stream)
		{
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x00129C7C File Offset: 0x00127E7C
		// (set) Token: 0x060050EC RID: 20716 RVA: 0x00129C85 File Offset: 0x00127E85
		public override long Position
		{
			get
			{
				return (long)this.bytesSent;
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00129CA0 File Offset: 0x00127EA0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.bytesSent += count;
			return base.BaseStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x00129CC2 File Offset: 0x00127EC2
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.BaseStream.Write(buffer, offset, count);
			this.bytesSent += count;
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x00129CE0 File Offset: 0x00127EE0
		public override void WriteByte(byte value)
		{
			base.BaseStream.WriteByte(value);
			this.bytesSent++;
		}

		// Token: 0x040031EC RID: 12780
		private int bytesSent;
	}
}
