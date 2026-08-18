using System;
using System.Text;

namespace System.Net
{
	// Token: 0x020004A7 RID: 1191
	internal class HostHeaderString
	{
		// Token: 0x06002478 RID: 9336 RVA: 0x0008F67E File Offset: 0x0008E67E
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0008F68D File Offset: 0x0008E68D
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0008F69C File Offset: 0x0008E69C
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0008F6B4 File Offset: 0x0008E6B4
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x0008F71D File Offset: 0x0008E71D
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x0008F725 File Offset: 0x0008E725
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0008F72E File Offset: 0x0008E72E
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x0008F73E File Offset: 0x0008E73E
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0008F74C File Offset: 0x0008E74C
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x040024C6 RID: 9414
		private bool m_Converted;

		// Token: 0x040024C7 RID: 9415
		private string m_String;

		// Token: 0x040024C8 RID: 9416
		private byte[] m_Bytes;
	}
}
