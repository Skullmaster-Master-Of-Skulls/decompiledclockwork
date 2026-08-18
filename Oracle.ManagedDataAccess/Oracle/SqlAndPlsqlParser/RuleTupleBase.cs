using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	internal abstract class RuleTupleBase<T> : IComparable<RuleTupleBase<T>>
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00107970 File Offset: 0x00105B70
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x00107978 File Offset: 0x00105B78
		public T Head
		{
			get
			{
				return this.m_vHead;
			}
			set
			{
				this.m_vHead = value;
				this.m_vHashValue = null;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00107990 File Offset: 0x00105B90
		public T[] RHS
		{
			get
			{
				return this.m_vRhs;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00107998 File Offset: 0x00105B98
		public bool IsUnary
		{
			get
			{
				return this.m_vIsUnary;
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x001079A0 File Offset: 0x00105BA0
		public RuleTupleBase(T h, List<T> r)
		{
			this.m_vHead = h;
			this.m_vRhs = r.ToArray();
			this.m_vIsUnary = (this.m_vRhs.Length == 1);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x001079D8 File Offset: 0x00105BD8
		public RuleTupleBase(T h, T[] r)
		{
			this.m_vHead = h;
			this.m_vRhs = r;
			this.m_vIsUnary = (this.m_vRhs.Length == 1);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00107A0C File Offset: 0x00105C0C
		public RuleTupleBase()
		{
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00107A20 File Offset: 0x00105C20
		public override bool Equals(object obj)
		{
			return this == obj || this.CompareTo(obj as RuleTupleBase<T>) == 0;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00107A38 File Offset: 0x00105C38
		public override int GetHashCode()
		{
			if (this.m_vHashValue == null)
			{
				this.m_vHashValue = new int?((this.m_vHead != null) ? this.m_vHead.GetHashCode() : RuleTupleBase<T>.s_vNullHeadHashCode);
			}
			return this.m_vHashValue.Value;
		}

		// Token: 0x06001902 RID: 6402
		public abstract int CompareTo(RuleTupleBase<T> src);

		// Token: 0x04001B69 RID: 7017
		protected int? m_vHashValue = null;

		// Token: 0x04001B6A RID: 7018
		public T m_vHead;

		// Token: 0x04001B6B RID: 7019
		public T[] m_vRhs;

		// Token: 0x04001B6C RID: 7020
		protected static int s_vNullHeadHashCode;

		// Token: 0x04001B6D RID: 7021
		private bool m_vIsUnary;

		// Token: 0x04001B6E RID: 7022
		protected static char[] s_vColonDelimiter = new char[]
		{
			':'
		};

		// Token: 0x04001B6F RID: 7023
		protected static char[] s_vSpaceDelimiter = new char[]
		{
			' '
		};

		// Token: 0x04001B70 RID: 7024
		protected static string[] s_vEmptyStringArray = new string[0];
	}
}
