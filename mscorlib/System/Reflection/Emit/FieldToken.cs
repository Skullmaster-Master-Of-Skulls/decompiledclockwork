using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000826 RID: 2086
	[ComVisible(true)]
	[Serializable]
	public struct FieldToken
	{
		// Token: 0x06004A4E RID: 19022 RVA: 0x0010226D File Offset: 0x0010126D
		internal FieldToken(int field, Type fieldClass)
		{
			this.m_fieldTok = field;
			this.m_class = fieldClass;
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0010227D File Offset: 0x0010127D
		public int Token
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00102285 File Offset: 0x00101285
		public override int GetHashCode()
		{
			return this.m_fieldTok;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x0010228D File Offset: 0x0010128D
		public override bool Equals(object obj)
		{
			return obj is FieldToken && this.Equals((FieldToken)obj);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x001022A5 File Offset: 0x001012A5
		public bool Equals(FieldToken obj)
		{
			return obj.m_fieldTok == this.m_fieldTok && obj.m_class == this.m_class;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x001022C7 File Offset: 0x001012C7
		public static bool operator ==(FieldToken a, FieldToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x001022D1 File Offset: 0x001012D1
		public static bool operator !=(FieldToken a, FieldToken b)
		{
			return !(a == b);
		}

		// Token: 0x040025EE RID: 9710
		public static readonly FieldToken Empty = default(FieldToken);

		// Token: 0x040025EF RID: 9711
		internal int m_fieldTok;

		// Token: 0x040025F0 RID: 9712
		internal object m_class;
	}
}
