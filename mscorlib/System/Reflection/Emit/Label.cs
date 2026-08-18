using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200082D RID: 2093
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x06004A7C RID: 19068 RVA: 0x00102CB8 File Offset: 0x00101CB8
		internal Label(int label)
		{
			this.m_label = label;
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00102CC1 File Offset: 0x00101CC1
		internal int GetLabelValue()
		{
			return this.m_label;
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00102CC9 File Offset: 0x00101CC9
		public override int GetHashCode()
		{
			return this.m_label;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00102CD1 File Offset: 0x00101CD1
		public override bool Equals(object obj)
		{
			return obj is Label && this.Equals((Label)obj);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00102CE9 File Offset: 0x00101CE9
		public bool Equals(Label obj)
		{
			return obj.m_label == this.m_label;
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00102CFA File Offset: 0x00101CFA
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00102D04 File Offset: 0x00101D04
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04002620 RID: 9760
		internal int m_label;
	}
}
