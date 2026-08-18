using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000850 RID: 2128
	[ComVisible(true)]
	[Serializable]
	public struct TypeToken
	{
		// Token: 0x06004DF0 RID: 19952 RVA: 0x0010F503 File Offset: 0x0010E503
		internal TypeToken(int str)
		{
			this.m_class = str;
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x0010F50C File Offset: 0x0010E50C
		public int Token
		{
			get
			{
				return this.m_class;
			}
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0010F514 File Offset: 0x0010E514
		public override int GetHashCode()
		{
			return this.m_class;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0010F51C File Offset: 0x0010E51C
		public override bool Equals(object obj)
		{
			return obj is TypeToken && this.Equals((TypeToken)obj);
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x0010F534 File Offset: 0x0010E534
		public bool Equals(TypeToken obj)
		{
			return obj.m_class == this.m_class;
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0010F545 File Offset: 0x0010E545
		public static bool operator ==(TypeToken a, TypeToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x0010F54F File Offset: 0x0010E54F
		public static bool operator !=(TypeToken a, TypeToken b)
		{
			return !(a == b);
		}

		// Token: 0x0400284F RID: 10319
		public static readonly TypeToken Empty = default(TypeToken);

		// Token: 0x04002850 RID: 10320
		internal int m_class;
	}
}
