using System;

namespace System.Reflection
{
	// Token: 0x02000004 RID: 4
	internal struct Blob
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000025AD File Offset: 0x000007AD
		public int Length { get; }

		// Token: 0x0600005A RID: 90 RVA: 0x000025B5 File Offset: 0x000007B5
		internal Blob(byte[] buffer, int start, int length)
		{
			this.Buffer = buffer;
			this.Start = start;
			this.Length = length;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025CC File Offset: 0x000007CC
		public bool IsDefault
		{
			get
			{
				return this.Buffer == null;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000025D7 File Offset: 0x000007D7
		public ArraySegment<byte> GetBytes()
		{
			return new ArraySegment<byte>(this.Buffer, this.Start, this.Length);
		}

		// Token: 0x04000003 RID: 3
		internal readonly byte[] Buffer;

		// Token: 0x04000004 RID: 4
		internal readonly int Start;
	}
}
