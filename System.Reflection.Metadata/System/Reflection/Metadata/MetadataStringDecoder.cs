using System;
using System.Reflection.Internal;
using System.Text;

namespace System.Reflection.Metadata
{
	// Token: 0x0200007F RID: 127
	public class MetadataStringDecoder
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000E3A9 File Offset: 0x0000C5A9
		public static MetadataStringDecoder DefaultUTF8
		{
			get
			{
				return MetadataStringDecoder.s_defaultUTF8;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		public MetadataStringDecoder(Encoding encoding)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._encoding = encoding;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000E3CD File Offset: 0x0000C5CD
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000E3D5 File Offset: 0x0000C5D5
		public unsafe virtual string GetString(byte* bytes, int byteCount)
		{
			return this._encoding.GetString(bytes, byteCount);
		}

		// Token: 0x040003A8 RID: 936
		private static readonly MetadataStringDecoder s_defaultUTF8 = new MetadataStringDecoder(Encoding.UTF8);

		// Token: 0x040003A9 RID: 937
		private readonly Encoding _encoding;
	}
}
