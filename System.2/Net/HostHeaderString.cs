using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000184 RID: 388
	internal class HostHeaderString
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0004B8E1 File Offset: 0x00049AE1
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0004B8F0 File Offset: 0x00049AF0
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0004B908 File Offset: 0x00049B08
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0004B971 File Offset: 0x00049B71
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0004B979 File Offset: 0x00049B79
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0004B982 File Offset: 0x00049B82
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0004B992 File Offset: 0x00049B92
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0004B9A0 File Offset: 0x00049BA0
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x04001276 RID: 4726
		private bool m_Converted;

		// Token: 0x04001277 RID: 4727
		private string m_String;

		// Token: 0x04001278 RID: 4728
		private byte[] m_Bytes;
	}
}
