using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000839 RID: 2105
	[ComVisible(true)]
	[Serializable]
	public struct MethodToken
	{
		// Token: 0x06004B64 RID: 19300 RVA: 0x00105A40 File Offset: 0x00104A40
		internal MethodToken(int str)
		{
			this.m_method = str;
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x00105A49 File Offset: 0x00104A49
		public int Token
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00105A51 File Offset: 0x00104A51
		public override int GetHashCode()
		{
			return this.m_method;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x00105A59 File Offset: 0x00104A59
		public override bool Equals(object obj)
		{
			return obj is MethodToken && this.Equals((MethodToken)obj);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00105A71 File Offset: 0x00104A71
		public bool Equals(MethodToken obj)
		{
			return obj.m_method == this.m_method;
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00105A82 File Offset: 0x00104A82
		public static bool operator ==(MethodToken a, MethodToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x00105A8C File Offset: 0x00104A8C
		public static bool operator !=(MethodToken a, MethodToken b)
		{
			return !(a == b);
		}

		// Token: 0x04002685 RID: 9861
		public static readonly MethodToken Empty = default(MethodToken);

		// Token: 0x04002686 RID: 9862
		internal int m_method;
	}
}
