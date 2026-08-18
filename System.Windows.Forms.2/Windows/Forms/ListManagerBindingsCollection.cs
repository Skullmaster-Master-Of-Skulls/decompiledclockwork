using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002D3 RID: 723
	[DefaultEvent("CollectionChanged")]
	internal class ListManagerBindingsCollection : BindingsCollection
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x000C90D4 File Offset: 0x000C72D4
		internal ListManagerBindingsCollection(BindingManagerBase bindingManagerBase)
		{
			this.bindingManagerBase = bindingManagerBase;
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000C90E4 File Offset: 0x000C72E4
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.BindingManagerBase == this.bindingManagerBase)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd1"), "dataBinding");
			}
			if (dataBinding.BindingManagerBase != null)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd2"), "dataBinding");
			}
			dataBinding.SetListManager(this.bindingManagerBase);
			base.AddCore(dataBinding);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000C9154 File Offset: 0x000C7354
		protected override void ClearCore()
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Binding binding = base[i];
				binding.SetListManager(null);
			}
			base.ClearCore();
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000C9189 File Offset: 0x000C7389
		protected override void RemoveCore(Binding dataBinding)
		{
			if (dataBinding.BindingManagerBase != this.bindingManagerBase)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionForeign"));
			}
			dataBinding.SetListManager(null);
			base.RemoveCore(dataBinding);
		}

		// Token: 0x0400129B RID: 4763
		private BindingManagerBase bindingManagerBase;
	}
}
