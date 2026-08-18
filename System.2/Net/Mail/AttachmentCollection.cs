using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x02000258 RID: 600
	public sealed class AttachmentCollection : Collection<Attachment>, IDisposable
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x00076158 File Offset: 0x00074358
		internal AttachmentCollection()
		{
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00076160 File Offset: 0x00074360
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

		// Token: 0x060016F1 RID: 5873 RVA: 0x000761C0 File Offset: 0x000743C0
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000761E2 File Offset: 0x000743E2
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00076203 File Offset: 0x00074403
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

		// Token: 0x060016F4 RID: 5876 RVA: 0x00076234 File Offset: 0x00074434
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

		// Token: 0x04001776 RID: 6006
		private bool disposed;
	}
}
