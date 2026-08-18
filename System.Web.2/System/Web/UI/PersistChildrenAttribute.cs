using System;

namespace System.Web.UI
{
	// Token: 0x020002EE RID: 750
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PersistChildrenAttribute : Attribute
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x00071BD7 File Offset: 0x0006FDD7
		public PersistChildrenAttribute(bool persist)
		{
			this._persist = persist;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00071BE6 File Offset: 0x0006FDE6
		public PersistChildrenAttribute(bool persist, bool usesCustomPersistence) : this(persist)
		{
			this._usesCustomPersistence = usesCustomPersistence;
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x00071BF6 File Offset: 0x0006FDF6
		public bool Persist
		{
			get
			{
				return this._persist;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x00071BFE File Offset: 0x0006FDFE
		public bool UsesCustomPersistence
		{
			get
			{
				return !this._persist && this._usesCustomPersistence;
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00071C10 File Offset: 0x0006FE10
		public override int GetHashCode()
		{
			return this.Persist.GetHashCode();
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00071C2B File Offset: 0x0006FE2B
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is PersistChildrenAttribute && ((PersistChildrenAttribute)obj).Persist == this._persist);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00071C53 File Offset: 0x0006FE53
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PersistChildrenAttribute.Default);
		}

		// Token: 0x04001C78 RID: 7288
		public static readonly PersistChildrenAttribute Yes = new PersistChildrenAttribute(true);

		// Token: 0x04001C79 RID: 7289
		public static readonly PersistChildrenAttribute No = new PersistChildrenAttribute(false);

		// Token: 0x04001C7A RID: 7290
		public static readonly PersistChildrenAttribute Default = PersistChildrenAttribute.Yes;

		// Token: 0x04001C7B RID: 7291
		private bool _persist;

		// Token: 0x04001C7C RID: 7292
		private bool _usesCustomPersistence;
	}
}
