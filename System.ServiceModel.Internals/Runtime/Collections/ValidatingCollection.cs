using System;
using System.Collections.ObjectModel;

namespace System.Runtime.Collections
{
	// Token: 0x02000053 RID: 83
	internal class ValidatingCollection<T> : Collection<T>
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001110E File Offset: 0x0000F30E
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00011116 File Offset: 0x0000F316
		public Action<T> OnAddValidationCallback { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001111F File Offset: 0x0000F31F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00011127 File Offset: 0x0000F327
		public Action OnMutateValidationCallback { get; set; }

		// Token: 0x06000341 RID: 833 RVA: 0x00011130 File Offset: 0x0000F330
		private void OnAdd(T item)
		{
			if (this.OnAddValidationCallback != null)
			{
				this.OnAddValidationCallback(item);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011146 File Offset: 0x0000F346
		private void OnMutate()
		{
			if (this.OnMutateValidationCallback != null)
			{
				this.OnMutateValidationCallback();
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001115B File Offset: 0x0000F35B
		protected override void ClearItems()
		{
			this.OnMutate();
			base.ClearItems();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00011169 File Offset: 0x0000F369
		protected override void InsertItem(int index, T item)
		{
			this.OnAdd(item);
			base.InsertItem(index, item);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001117A File Offset: 0x0000F37A
		protected override void RemoveItem(int index)
		{
			this.OnMutate();
			base.RemoveItem(index);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00011189 File Offset: 0x0000F389
		protected override void SetItem(int index, T item)
		{
			this.OnAdd(item);
			this.OnMutate();
			base.SetItem(index, item);
		}
	}
}
