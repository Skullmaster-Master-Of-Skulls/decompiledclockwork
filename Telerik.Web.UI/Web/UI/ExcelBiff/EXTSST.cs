using System;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA4 RID: 2724
	internal sealed class EXTSST : BaseBiffRecord, IRecord
	{
		// Token: 0x060067DD RID: 26589 RVA: 0x00184908 File Offset: 0x00182B08
		public EXTSST(ushort stringsPerBucket, INSTINF[] instInfArray) : base(255)
		{
			this.dsst = stringsPerBucket;
			if (instInfArray != null)
			{
				this.rgInstinf = instInfArray;
				return;
			}
			base.Length = 2;
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x0018492E File Offset: 0x00182B2E
		public byte[] GetData()
		{
			return null;
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x00184934 File Offset: 0x00182B34
		public void WriteEXTSSTRecord(Stream stream)
		{
			if (stream != null)
			{
				if (this.rgInstinf != null)
				{
					int num = 2 + 8 * this.rgInstinf.Length;
					if (num + 4 > 8227)
					{
						num = 8218;
					}
					base.Length = (ushort)num;
				}
				stream.Write(base.GetBaseData(), 0, 4);
				byte[] bytes = BitConverter.GetBytes(this.dsst);
				stream.Write(bytes, 0, bytes.Length);
				if (this.rgInstinf != null)
				{
					int num2 = 0;
					for (int i = 0; i < this.rgInstinf.Length; i++)
					{
						num2++;
						if (num2 > 1027)
						{
							num2 = 1;
							Continue @continue = new Continue();
							if (i + 1027 < this.rgInstinf.Length)
							{
								@continue.Length = 8216;
							}
							else
							{
								@continue.Length = (ushort)(8 * (this.rgInstinf.Length - i));
							}
							stream.Write(@continue.GetBaseData(), 0, 4);
						}
						INSTINF instinf = this.rgInstinf[i];
						bytes = BitConverter.GetBytes(instinf.ib);
						stream.Write(bytes, 0, bytes.Length);
						bytes = BitConverter.GetBytes(instinf.cb);
						stream.Write(bytes, 0, bytes.Length);
						bytes = BitConverter.GetBytes(instinf.reserved);
						stream.Write(bytes, 0, bytes.Length);
					}
				}
			}
		}

		// Token: 0x04001AD1 RID: 6865
		private const ushort type = 255;

		// Token: 0x04001AD2 RID: 6866
		internal const int MaxInstInfLength = 8;

		// Token: 0x04001AD3 RID: 6867
		internal const int MaxInstInfPerRecord = 1027;

		// Token: 0x04001AD4 RID: 6868
		private ushort dsst;

		// Token: 0x04001AD5 RID: 6869
		private INSTINF[] rgInstinf;
	}
}
