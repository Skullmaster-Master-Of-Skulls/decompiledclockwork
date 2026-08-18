using System;

namespace System.Web.UI
{
	// Token: 0x02000284 RID: 644
	public class EmptyControlCollection : ControlCollection
	{
		// Token: 0x06001E6C RID: 7788 RVA: 0x00061D30 File Offset: 0x0005FF30
		public EmptyControlCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00061D39 File Offset: 0x0005FF39
		private void ThrowNotSupportedException()
		{
			throw new HttpException(SR.GetString("Control_does_not_allow_children", new object[]
			{
				base.Owner.GetType().ToString()
			}));
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00061D63 File Offset: 0x0005FF63
		public override void Add(Control child)
		{
			this.ThrowNotSupportedException();
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00061D63 File Offset: 0x0005FF63
		public override void AddAt(int index, Control child)
		{
			this.ThrowNotSupportedException();
		}
	}
}
