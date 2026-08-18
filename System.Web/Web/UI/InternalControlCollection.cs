using System;

namespace System.Web.UI
{
	// Token: 0x02000410 RID: 1040
	internal class InternalControlCollection : ControlCollection
	{
		// Token: 0x060032AC RID: 12972 RVA: 0x000DD6E5 File Offset: 0x000DC6E5
		internal InternalControlCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000DD6F0 File Offset: 0x000DC6F0
		private void ThrowNotSupportedException()
		{
			throw new HttpException(SR.GetString("Control_does_not_allow_children", new object[]
			{
				base.Owner.GetType().ToString()
			}));
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000DD727 File Offset: 0x000DC727
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000DD72F File Offset: 0x000DC72F
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}
