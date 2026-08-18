using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x02000698 RID: 1688
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x06003417 RID: 13335 RVA: 0x000DBA24 File Offset: 0x000DAA24
		internal LinkedResourceCollection()
		{
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000DBA2C File Offset: 0x000DAA2C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			foreach (LinkedResource linkedResource in this)
			{
				linkedResource.Dispose();
			}
			base.Clear();
			this.disposed = true;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000DBA8C File Offset: 0x000DAA8C
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000DBAAE File Offset: 0x000DAAAE
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000DBACF File Offset: 0x000DAACF
		protected override void SetItem(int index, LinkedResource item)
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

		// Token: 0x0600341C RID: 13340 RVA: 0x000DBB00 File Offset: 0x000DAB00
		protected override void InsertItem(int index, LinkedResource item)
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

		// Token: 0x04003002 RID: 12290
		private bool disposed;
	}
}
