using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x0200093F RID: 2367
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x06005568 RID: 21864 RVA: 0x001358B4 File Offset: 0x001348B4
		internal IdentityReference()
		{
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005569 RID: 21865
		public abstract string Value { get; }

		// Token: 0x0600556A RID: 21866
		public abstract bool IsValidTargetType(Type targetType);

		// Token: 0x0600556B RID: 21867
		public abstract IdentityReference Translate(Type targetType);

		// Token: 0x0600556C RID: 21868
		public abstract override bool Equals(object o);

		// Token: 0x0600556D RID: 21869
		public abstract override int GetHashCode();

		// Token: 0x0600556E RID: 21870
		public abstract override string ToString();

		// Token: 0x0600556F RID: 21871 RVA: 0x001358BC File Offset: 0x001348BC
		public static bool operator ==(IdentityReference left, IdentityReference right)
		{
			return (left == null && right == null) || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x001358E4 File Offset: 0x001348E4
		public static bool operator !=(IdentityReference left, IdentityReference right)
		{
			return !(left == right);
		}
	}
}
