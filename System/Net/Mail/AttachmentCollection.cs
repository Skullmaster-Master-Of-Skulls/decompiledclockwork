using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x0200067A RID: 1658
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x0600334A RID: 13130 RVA: 0x000D89C4 File Offset: 0x000D79C4
		internal AttachmentCollection()
		{
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000D89CC File Offset: 0x000D79CC
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (Attachment attachment in this)
			{
				attachment.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000D8A2C File Offset: 0x000D7A2C
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000D8A4E File Offset: 0x000D7A4E
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000D8A6F File Offset: 0x000D7A6F
		protected override void SetItem(int index, Attachment item)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000D8AA0 File Offset: 0x000D7AA0
		protected override void InsertItem(int index, Attachment item)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x04002F90 RID: 12176
		private bool disposed;
	}
}
