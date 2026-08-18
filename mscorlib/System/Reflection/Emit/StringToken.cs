using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000849 RID: 2121
	[ComVisible(true)]
	[Serializable]
	public struct StringToken
	{
		// Token: 0x06004C6B RID: 19563 RVA: 0x0010BB70 File Offset: 0x0010AB70
		internal StringToken(int str)
		{
			this.m_string = str;
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x0010BB79 File Offset: 0x0010AB79
		public int Token
		{
			get
			{
				return this.m_string;
			}
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0010BB81 File Offset: 0x0010AB81
		public override int GetHashCode()
		{
			return this.m_string;
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0010BB89 File Offset: 0x0010AB89
		public override bool Equals(object obj)
		{
			return obj is StringToken && this.Equals((StringToken)obj);
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0010BBA1 File Offset: 0x0010ABA1
		public bool Equals(StringToken obj)
		{
			return obj.m_string == this.m_string;
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0010BBB2 File Offset: 0x0010ABB2
		public static bool operator ==(StringToken a, StringToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0010BBBC File Offset: 0x0010ABBC
		public static bool operator !=(StringToken a, StringToken b)
		{
			return !(a == b);
		}

		// Token: 0x0400281D RID: 10269
		internal int m_string;
	}
}
