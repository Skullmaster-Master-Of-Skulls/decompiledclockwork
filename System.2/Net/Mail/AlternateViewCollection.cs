using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x02000255 RID: 597
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x00075798 File Offset: 0x00073998
		internal AlternateViewCollection()
		{
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000757A0 File Offset: 0x000739A0
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (AlternateView alternateView in this)
			{
				alternateView.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00075800 File Offset: 0x00073A00
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00075822 File Offset: 0x00073A22
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00075843 File Offset: 0x00073A43
		protected override void SetItem(int index, AlternateView item)
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

		// Token: 0x060016C3 RID: 5827 RVA: 0x00075874 File Offset: 0x00073A74
		protected override void InsertItem(int index, AlternateView item)
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

		// Token: 0x04001771 RID: 6001
		private bool disposed;
	}
}
