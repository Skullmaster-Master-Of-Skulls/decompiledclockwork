using System;

namespace System.ComponentModel
{
	// Token: 0x020005BF RID: 1471
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		// Token: 0x06003725 RID: 14117 RVA: 0x000EFEAD File Offset: 0x000EE0AD
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000EFEBC File Offset: 0x000EE0BC
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000EFEC4 File Offset: 0x000EE0C4
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000EFEEC File Offset: 0x000EE0EC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000EFEF4 File Offset: 0x000EE0F4
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		// Token: 0x04002AD0 RID: 10960
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		// Token: 0x04002AD1 RID: 10961
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		// Token: 0x04002AD2 RID: 10962
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x04002AD3 RID: 10963
		private bool notifyParent;
	}
}
