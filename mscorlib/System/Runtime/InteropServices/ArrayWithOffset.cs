using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004D9 RID: 1241
	[ComVisible(true)]
	[Serializable]
	public struct ArrayWithOffset
	{
		// Token: 0x06003135 RID: 12597 RVA: 0x000A8F65 File Offset: 0x000A7F65
		public ArrayWithOffset(object array, int offset)
		{
			this.m_array = array;
			this.m_offset = offset;
			this.m_count = 0;
			this.m_count = this.CalculateCount();
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000A8F88 File Offset: 0x000A7F88
		public object GetArray()
		{
			return this.m_array;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000A8F90 File Offset: 0x000A7F90
		public int GetOffset()
		{
			return this.m_offset;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000A8F98 File Offset: 0x000A7F98
		public override int GetHashCode()
		{
			return this.m_count + this.m_offset;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000A8FA7 File Offset: 0x000A7FA7
		public override bool Equals(object obj)
		{
			return obj is ArrayWithOffset && this.Equals((ArrayWithOffset)obj);
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000A8FBF File Offset: 0x000A7FBF
		public bool Equals(ArrayWithOffset obj)
		{
			return obj.m_array == this.m_array && obj.m_offset == this.m_offset && obj.m_count == this.m_count;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000A8FF0 File Offset: 0x000A7FF0
		public static bool operator ==(ArrayWithOffset a, ArrayWithOffset b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000A8FFA File Offset: 0x000A7FFA
		public static bool operator !=(ArrayWithOffset a, ArrayWithOffset b)
		{
			return !(a == b);
		}

		// Token: 0x0600313D RID: 12605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int CalculateCount();

		// Token: 0x040018E9 RID: 6377
		private object m_array;

		// Token: 0x040018EA RID: 6378
		private int m_offset;

		// Token: 0x040018EB RID: 6379
		private int m_count;
	}
}
