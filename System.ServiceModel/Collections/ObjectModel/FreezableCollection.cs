using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace System.Collections.ObjectModel
{
	// Token: 0x0200001E RID: 30
	internal class FreezableCollection<T> : Collection<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000724E File Offset: 0x0000544E
		public FreezableCollection()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007256 File Offset: 0x00005456
		public FreezableCollection(IList<T> list) : base(list)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000725F File Offset: 0x0000545F
		public bool IsFrozen
		{
			get
			{
				return this.frozen;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007267 File Offset: 0x00005467
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return this.frozen;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000726F File Offset: 0x0000546F
		public void Freeze()
		{
			this.frozen = true;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007278 File Offset: 0x00005478
		protected override void ClearItems()
		{
			this.ThrowIfFrozen();
			base.ClearItems();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007286 File Offset: 0x00005486
		protected override void InsertItem(int index, T item)
		{
			this.ThrowIfFrozen();
			base.InsertItem(index, item);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007296 File Offset: 0x00005496
		protected override void RemoveItem(int index)
		{
			this.ThrowIfFrozen();
			base.RemoveItem(index);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000072A5 File Offset: 0x000054A5
		protected override void SetItem(int index, T item)
		{
			this.ThrowIfFrozen();
			base.SetItem(index, item);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000072B5 File Offset: 0x000054B5
		private void ThrowIfFrozen()
		{
			if (this.frozen)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException("ObjectIsReadOnly"));
			}
		}

		// Token: 0x0400017B RID: 379
		private bool frozen;
	}
}
