using System;
using System.IO;

namespace Spire.CompoundFile.XLS.Net
{
	// Token: 0x020005EF RID: 1519
	public class ClipboardData : ICloneable
	{
		// Token: 0x060059EB RID: 23019 RVA: 0x00386384 File Offset: 0x00385384
		public object Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			ClipboardData clipboardData = (ClipboardData)base.MemberwiseClone();
			clipboardData.Data = sprἽ.ᜀ(this.Data);
			return clipboardData;
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x003863E0 File Offset: 0x003853E0
		public int Serialize(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int num = 0;
			int num2 = this.Data.Length;
			num += spr\u23D6.ᜂ(stream, num2);
			num += spr\u23D6.ᜂ(stream, this.Format);
			stream.Write(this.Data, 0, num2);
			return num + num2;
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x00386454 File Offset: 0x00385454
		public void Parse(Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			byte[] a_ = new byte[4];
			int num = spr\u23D6.ᜁ(stream, a_);
			this.Format = spr\u23D6.ᜁ(stream, a_);
			this.Data = new byte[num];
			stream.Read(this.Data, 0, num);
		}

		// Token: 0x04002C17 RID: 11287
		private byte \u2593\u0094\u0093\u008A;

		// Token: 0x04002C18 RID: 11288
		public int Format;

		// Token: 0x04002C19 RID: 11289
		private float[] \u2593\u00A1\u00AE\u0094;

		// Token: 0x04002C1A RID: 11290
		private byte \u25D9\u008F\u008A\u00A3;

		// Token: 0x04002C1B RID: 11291
		private int[] \u2593\u007F\u0093\u0097;

		// Token: 0x04002C1C RID: 11292
		public byte[] Data;
	}
}
