using System;
using System.Text;

namespace System.Data.OracleClient
{
	// Token: 0x02000064 RID: 100
	internal sealed class OracleEncoding : Encoding
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00069074 File Offset: 0x00068474
		internal OciHandle Handle
		{
			get
			{
				OciHandle ociHandle = this._connection.SessionHandle;
				if (ociHandle == null || ociHandle.IsInvalid)
				{
					ociHandle = this._connection.EnvironmentHandle;
				}
				return ociHandle;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000690B4 File Offset: 0x000684B4
		public OracleEncoding(OracleInternalConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000690D4 File Offset: 0x000684D4
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.GetBytes(chars, index, count, null, 0);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000690F4 File Offset: 0x000684F4
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			OciHandle handle = this.Handle;
			return checked((int)handle.GetBytes(chars, charIndex, (uint)charCount, bytes, byteIndex));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00069124 File Offset: 0x00068524
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetChars(bytes, index, count, null, 0);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00069144 File Offset: 0x00068544
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			OciHandle handle = this.Handle;
			return checked((int)handle.GetChars(bytes, byteIndex, (uint)byteCount, chars, charIndex));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00069174 File Offset: 0x00068574
		public override int GetMaxByteCount(int charCount)
		{
			return checked(charCount * 4);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00069184 File Offset: 0x00068584
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x0400042A RID: 1066
		private OracleInternalConnection _connection;
	}
}
