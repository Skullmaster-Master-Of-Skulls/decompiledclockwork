using System;
using System.Collections.ObjectModel;

namespace Telerik.Licensing
{
	// Token: 0x0200042C RID: 1068
	internal class TypesCollection : Collection<string>
	{
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x0600265A RID: 9818 RVA: 0x0007DAF8 File Offset: 0x0007BCF8
		// (remove) Token: 0x0600265B RID: 9819 RVA: 0x0007DB30 File Offset: 0x0007BD30
		public event CollectionChangedEventHandler CollectionChanged;

		// Token: 0x0600265C RID: 9820 RVA: 0x0007DB65 File Offset: 0x0007BD65
		public void TryAdd(string item)
		{
			if (!base.Contains(item))
			{
				base.Add(item);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0007DB77 File Offset: 0x0007BD77
		protected override void InsertItem(int index, string item)
		{
			base.InsertItem(index, item);
			this.RaiseCollectionChanged();
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0007DB87 File Offset: 0x0007BD87
		private void RaiseCollectionChanged()
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, new CollectionChangedEventArgs());
			}
		}
	}
}
