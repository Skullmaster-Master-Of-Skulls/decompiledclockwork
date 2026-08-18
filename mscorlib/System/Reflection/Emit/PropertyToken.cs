using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000846 RID: 2118
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x06004C22 RID: 19490 RVA: 0x0010A953 File Offset: 0x00109953
		internal PropertyToken(int str)
		{
			this.m_property = str;
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x0010A95C File Offset: 0x0010995C
		public int Token
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0010A964 File Offset: 0x00109964
		public override int GetHashCode()
		{
			return this.m_property;
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0010A96C File Offset: 0x0010996C
		public override bool Equals(object obj)
		{
			return obj is PropertyToken && this.Equals((PropertyToken)obj);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0010A984 File Offset: 0x00109984
		public bool Equals(PropertyToken obj)
		{
			return obj.m_property == this.m_property;
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0010A995 File Offset: 0x00109995
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0010A99F File Offset: 0x0010999F
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !(a == b);
		}

		// Token: 0x040027DB RID: 10203
		public static readonly PropertyToken Empty = default(PropertyToken);

		// Token: 0x040027DC RID: 10204
		internal int m_property;
	}
}
