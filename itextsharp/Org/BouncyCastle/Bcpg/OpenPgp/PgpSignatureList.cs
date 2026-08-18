using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200007E RID: 126
	public class PgpSignatureList : PgpObject
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00015FC9 File Offset: 0x00014FC9
		public PgpSignatureList(PgpSignature[] sigs)
		{
			this.sigs = (PgpSignature[])sigs.Clone();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00015FE4 File Offset: 0x00014FE4
		public PgpSignatureList(PgpSignature sig)
		{
			this.sigs = new PgpSignature[]
			{
				sig
			};
		}

		// Token: 0x170000B4 RID: 180
		public PgpSignature this[int index]
		{
			get
			{
				return this.sigs[index];
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00016013 File Offset: 0x00015013
		[Obsolete("Use 'object[index]' syntax instead")]
		public PgpSignature Get(int index)
		{
			return this[index];
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001601C File Offset: 0x0001501C
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00016026 File Offset: 0x00015026
		public int Count
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00016030 File Offset: 0x00015030
		public bool IsEmpty
		{
			get
			{
				return this.sigs.Length == 0;
			}
		}

		// Token: 0x04000213 RID: 531
		private PgpSignature[] sigs;
	}
}
