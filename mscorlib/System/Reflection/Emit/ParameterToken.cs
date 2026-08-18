using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000844 RID: 2116
	[ComVisible(true)]
	[Serializable]
	public struct ParameterToken
	{
		// Token: 0x06004BF9 RID: 19449 RVA: 0x0010A510 File Offset: 0x00109510
		internal ParameterToken(int tkParam)
		{
			this.m_tkParameter = tkParam;
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x0010A519 File Offset: 0x00109519
		public int Token
		{
			get
			{
				return this.m_tkParameter;
			}
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0010A521 File Offset: 0x00109521
		public override int GetHashCode()
		{
			return this.m_tkParameter;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0010A529 File Offset: 0x00109529
		public override bool Equals(object obj)
		{
			return obj is ParameterToken && this.Equals((ParameterToken)obj);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0010A541 File Offset: 0x00109541
		public bool Equals(ParameterToken obj)
		{
			return obj.m_tkParameter == this.m_tkParameter;
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0010A552 File Offset: 0x00109552
		public static bool operator ==(ParameterToken a, ParameterToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0010A55C File Offset: 0x0010955C
		public static bool operator !=(ParameterToken a, ParameterToken b)
		{
			return !(a == b);
		}

		// Token: 0x040027CF RID: 10191
		public static readonly ParameterToken Empty = default(ParameterToken);

		// Token: 0x040027D0 RID: 10192
		internal int m_tkParameter;
	}
}
