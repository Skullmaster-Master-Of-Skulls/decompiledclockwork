using System;

namespace System.Web.UI
{
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class HtmlControlPersistableAttribute : Attribute
	{
		// Token: 0x06001EFE RID: 7934 RVA: 0x00063655 File Offset: 0x00061855
		internal HtmlControlPersistableAttribute(bool persistable)
		{
			this.persistable = persistable;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0006366B File Offset: 0x0006186B
		internal bool HtmlControlPersistable
		{
			get
			{
				return this.persistable;
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00063674 File Offset: 0x00061874
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			HtmlControlPersistableAttribute htmlControlPersistableAttribute = obj as HtmlControlPersistableAttribute;
			return htmlControlPersistableAttribute != null && htmlControlPersistableAttribute.HtmlControlPersistable == this.persistable;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000636A1 File Offset: 0x000618A1
		public override int GetHashCode()
		{
			return this.persistable.GetHashCode();
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000636AE File Offset: 0x000618AE
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HtmlControlPersistableAttribute.Default);
		}

		// Token: 0x040019C4 RID: 6596
		internal static readonly HtmlControlPersistableAttribute Yes = new HtmlControlPersistableAttribute(true);

		// Token: 0x040019C5 RID: 6597
		internal static readonly HtmlControlPersistableAttribute No = new HtmlControlPersistableAttribute(false);

		// Token: 0x040019C6 RID: 6598
		internal static readonly HtmlControlPersistableAttribute Default = HtmlControlPersistableAttribute.Yes;

		// Token: 0x040019C7 RID: 6599
		private bool persistable = true;
	}
}
