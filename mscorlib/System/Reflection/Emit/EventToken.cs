using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000824 RID: 2084
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x06004A2C RID: 18988 RVA: 0x00101E50 File Offset: 0x00100E50
		internal EventToken(int str)
		{
			this.m_event = str;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x00101E59 File Offset: 0x00100E59
		public int Token
		{
			get
			{
				return this.m_event;
			}
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00101E61 File Offset: 0x00100E61
		public override int GetHashCode()
		{
			return this.m_event;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00101E69 File Offset: 0x00100E69
		public override bool Equals(object obj)
		{
			return obj is EventToken && this.Equals((EventToken)obj);
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x00101E81 File Offset: 0x00100E81
		public bool Equals(EventToken obj)
		{
			return obj.m_event == this.m_event;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x00101E92 File Offset: 0x00100E92
		public static bool operator ==(EventToken a, EventToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00101E9C File Offset: 0x00100E9C
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !(a == b);
		}

		// Token: 0x040025E5 RID: 9701
		public static readonly EventToken Empty = default(EventToken);

		// Token: 0x040025E6 RID: 9702
		internal int m_event;
	}
}
