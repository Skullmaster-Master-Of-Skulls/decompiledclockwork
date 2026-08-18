using System;
using System.IO;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200007F RID: 127
	public class PgpLiteralData : PgpObject
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0001603D File Offset: 0x0001503D
		public PgpLiteralData(BcpgInputStream bcpgInput)
		{
			this.data = (LiteralDataPacket)bcpgInput.ReadPacket();
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00016056 File Offset: 0x00015056
		public int Format
		{
			get
			{
				return this.data.Format;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00016063 File Offset: 0x00015063
		public string FileName
		{
			get
			{
				return this.data.FileName;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00016070 File Offset: 0x00015070
		public byte[] GetRawFileName()
		{
			return this.data.GetRawFileName();
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0001607D File Offset: 0x0001507D
		public DateTime ModificationTime
		{
			get
			{
				return DateTimeUtilities.UnixMsToDateTime(this.data.ModificationTime);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001608F File Offset: 0x0001508F
		public Stream GetInputStream()
		{
			return this.data.GetInputStream();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001609C File Offset: 0x0001509C
		public Stream GetDataStream()
		{
			return this.GetInputStream();
		}

		// Token: 0x04000214 RID: 532
		public const char Binary = 'b';

		// Token: 0x04000215 RID: 533
		public const char Text = 't';

		// Token: 0x04000216 RID: 534
		public const char Utf8 = 'u';

		// Token: 0x04000217 RID: 535
		public const string Console = "_CONSOLE";

		// Token: 0x04000218 RID: 536
		private LiteralDataPacket data;
	}
}
