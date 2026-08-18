using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x02000678 RID: 1656
	public sealed class AlternateViewCollection : Collection<AlternateView>, IDisposable
	{
		// Token: 0x06003333 RID: 13107 RVA: 0x000D863C File Offset: 0x000D763C
		internal AlternateViewCollection()
		{
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000D8644 File Offset: 0x000D7644
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

		// Token: 0x06003335 RID: 13109 RVA: 0x000D86A4 File Offset: 0x000D76A4
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000D86C6 File Offset: 0x000D76C6
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000D86E7 File Offset: 0x000D76E7
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

		// Token: 0x06003338 RID: 13112 RVA: 0x000D8718 File Offset: 0x000D7718
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

		// Token: 0x04002F8D RID: 12173
		private bool disposed;
	}
}
