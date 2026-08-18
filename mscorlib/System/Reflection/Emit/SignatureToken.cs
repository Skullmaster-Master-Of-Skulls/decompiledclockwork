using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000848 RID: 2120
	[ComVisible(true)]
	public struct SignatureToken
	{
		// Token: 0x06004C63 RID: 19555 RVA: 0x0010BB04 File Offset: 0x0010AB04
		internal SignatureToken(int str, ModuleBuilder mod)
		{
			this.m_signature = str;
			this.m_moduleBuilder = mod;
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x0010BB14 File Offset: 0x0010AB14
		public int Token
		{
			get
			{
				return this.m_signature;
			}
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0010BB1C File Offset: 0x0010AB1C
		public override int GetHashCode()
		{
			return this.m_signature;
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0010BB24 File Offset: 0x0010AB24
		public override bool Equals(object obj)
		{
			return obj is SignatureToken && this.Equals((SignatureToken)obj);
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x0010BB3C File Offset: 0x0010AB3C
		public bool Equals(SignatureToken obj)
		{
			return obj.m_signature == this.m_signature;
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0010BB4D File Offset: 0x0010AB4D
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0010BB57 File Offset: 0x0010AB57
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !(a == b);
		}

		// Token: 0x0400281A RID: 10266
		public static readonly SignatureToken Empty = default(SignatureToken);

		// Token: 0x0400281B RID: 10267
		internal int m_signature;

		// Token: 0x0400281C RID: 10268
		internal ModuleBuilder m_moduleBuilder;
	}
}
