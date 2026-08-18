using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	// Token: 0x02000269 RID: 617
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x06001735 RID: 5941 RVA: 0x000769A4 File Offset: 0x00074BA4
		internal LinkedResourceCollection()
		{
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000769AC File Offset: 0x00074BAC
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

		// Token: 0x06001737 RID: 5943 RVA: 0x00076A0C File Offset: 0x00074C0C
		protected override void RemoveItem(int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00076A2E File Offset: 0x00074C2E
		protected override void ClearItems()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			base.ClearItems();
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00076A4F File Offset: 0x00074C4F
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

		// Token: 0x0600173A RID: 5946 RVA: 0x00076A80 File Offset: 0x00074C80
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

		// Token: 0x040017AD RID: 6061
		private bool disposed;
	}
}
