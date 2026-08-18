using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000501 RID: 1281
	public class PgpOnePassSignatureList : PgpObject
	{
		// Token: 0x06002BC2 RID: 11202 RVA: 0x00108CB8 File Offset: 0x00107CB8
		public PgpOnePassSignatureList(PgpOnePassSignature[] sigs)
		{
			this.sigs = (PgpOnePassSignature[])sigs.Clone();
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x00108CD4 File Offset: 0x00107CD4
		public PgpOnePassSignatureList(PgpOnePassSignature sig)
		{
			this.sigs = new PgpOnePassSignature[]
			{
				sig
			};
		}

		// Token: 0x17000789 RID: 1929
		public PgpOnePassSignature this[int index]
		{
			get
			{
				return this.sigs[index];
			}
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x00108D03 File Offset: 0x00107D03
		[Obsolete("Use 'object[index]' syntax instead")]
		public PgpOnePassSignature Get(int index)
		{
			return this[index];
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x00108D0C File Offset: 0x00107D0C
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00108D16 File Offset: 0x00107D16
		public int Count
		{
			get
			{
				return this.sigs.Length;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x00108D20 File Offset: 0x00107D20
		public bool IsEmpty
		{
			get
			{
				return this.sigs.Length == 0;
			}
		}

		// Token: 0x04001E3F RID: 7743
		private readonly PgpOnePassSignature[] sigs;
	}
}
