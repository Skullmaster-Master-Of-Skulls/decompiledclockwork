using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000313 RID: 787
	public abstract class DerGenerator : Asn1Generator
	{
		// Token: 0x06001CB2 RID: 7346 RVA: 0x000AB814 File Offset: 0x000AA814
		protected DerGenerator(Stream outStream) : base(outStream)
		{
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000AB81D File Offset: 0x000AA81D
		protected DerGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream)
		{
			this._tagged = true;
			this._isExplicit = isExplicit;
			this._tagNo = tagNo;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000AB83C File Offset: 0x000AA83C
		private static void WriteLength(Stream outStr, int length)
		{
			if (length > 127)
			{
				int num = 1;
				int num2 = length;
				while ((num2 >>= 8) != 0)
				{
					num++;
				}
				outStr.WriteByte((byte)(num | 128));
				for (int i = (num - 1) * 8; i >= 0; i -= 8)
				{
					outStr.WriteByte((byte)(length >> i));
				}
				return;
			}
			outStr.WriteByte((byte)length);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000AB893 File Offset: 0x000AA893
		internal static void WriteDerEncoded(Stream outStream, int tag, byte[] bytes)
		{
			outStream.WriteByte((byte)tag);
			DerGenerator.WriteLength(outStream, bytes.Length);
			outStream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000AB8B4 File Offset: 0x000AA8B4
		internal void WriteDerEncoded(int tag, byte[] bytes)
		{
			if (!this._tagged)
			{
				DerGenerator.WriteDerEncoded(base.Out, tag, bytes);
				return;
			}
			int num = this._tagNo | 128;
			if (this._isExplicit)
			{
				int tag2 = this._tagNo | 32 | 128;
				MemoryStream memoryStream = new MemoryStream();
				DerGenerator.WriteDerEncoded(memoryStream, tag, bytes);
				DerGenerator.WriteDerEncoded(base.Out, tag2, memoryStream.ToArray());
				return;
			}
			if ((tag & 32) != 0)
			{
				num |= 32;
			}
			DerGenerator.WriteDerEncoded(base.Out, num, bytes);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000AB935 File Offset: 0x000AA935
		internal static void WriteDerEncoded(Stream outStr, int tag, Stream inStr)
		{
			DerGenerator.WriteDerEncoded(outStr, tag, Streams.ReadAll(inStr));
		}

		// Token: 0x040013CD RID: 5069
		private bool _tagged;

		// Token: 0x040013CE RID: 5070
		private bool _isExplicit;

		// Token: 0x040013CF RID: 5071
		private int _tagNo;
	}
}
