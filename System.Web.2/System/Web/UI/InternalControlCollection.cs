using System;

namespace System.Web.UI
{
	// Token: 0x020002AF RID: 687
	internal class InternalControlCollection : ControlCollection
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x00061D30 File Offset: 0x0005FF30
		internal InternalControlCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00061D39 File Offset: 0x0005FF39
		private void ThrowNotSupportedException()
		{
			throw new HttpException(SR.GetString("Control_does_not_allow_children", new object[]
			{
				base.Owner.GetType().ToString()
			}));
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0006574D File Offset: 0x0006394D
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0006574D File Offset: 0x0006394D
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}
