using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FC RID: 1276
	public sealed class TreeNodeBindingCollection : StateManagedCollection
	{
		// Token: 0x06003FED RID: 16365 RVA: 0x00095F2B File Offset: 0x0009412B
		internal TreeNodeBindingCollection()
		{
		}

		// Token: 0x170012B6 RID: 4790
		public TreeNodeBinding this[int i]
		{
			get
			{
				return (TreeNodeBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(TreeNodeBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(TreeNodeBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(TreeNodeBinding[] bindingArray, int index)
		{
			base.CopyTo(bindingArray, index);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000CE83F File Offset: 0x000CCA3F
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeBinding();
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000CE848 File Offset: 0x000CCA48
		private void FindDefaultBinding()
		{
			this._defaultBinding = null;
			foreach (object obj in this)
			{
				TreeNodeBinding treeNodeBinding = (TreeNodeBinding)obj;
				if (treeNodeBinding.Depth == -1 && treeNodeBinding.DataMember.Length == 0)
				{
					this._defaultBinding = treeNodeBinding;
					break;
				}
			}
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000CE8BC File Offset: 0x000CCABC
		internal TreeNodeBinding GetBinding(string dataMember, int depth)
		{
			TreeNodeBinding treeNodeBinding = null;
			int num = 0;
			if (dataMember != null && dataMember.Length == 0)
			{
				dataMember = null;
			}
			foreach (object obj in this)
			{
				TreeNodeBinding treeNodeBinding2 = (TreeNodeBinding)obj;
				if (treeNodeBinding2.Depth == depth)
				{
					if (string.Equals(treeNodeBinding2.DataMember, dataMember, StringComparison.CurrentCultureIgnoreCase))
					{
						return treeNodeBinding2;
					}
					if (num < 1 && treeNodeBinding2.DataMember.Length == 0)
					{
						treeNodeBinding = treeNodeBinding2;
						num = 1;
					}
				}
				else if (string.Equals(treeNodeBinding2.DataMember, dataMember, StringComparison.CurrentCultureIgnoreCase) && num < 2 && treeNodeBinding2.Depth == -1)
				{
					treeNodeBinding = treeNodeBinding2;
					num = 2;
				}
			}
			if (treeNodeBinding == null && this._defaultBinding != null)
			{
				if (this._defaultBinding.Depth != -1 || this._defaultBinding.DataMember.Length != 0)
				{
					this.FindDefaultBinding();
				}
				treeNodeBinding = this._defaultBinding;
			}
			return treeNodeBinding;
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000CE9B4 File Offset: 0x000CCBB4
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeBindingCollection.knownTypes;
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(TreeNodeBinding binding)
		{
			return ((IList)this).IndexOf(binding);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, TreeNodeBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000CE9BB File Offset: 0x000CCBBB
		protected override void OnClear()
		{
			base.OnClear();
			this._defaultBinding = null;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000CE9CA File Offset: 0x000CCBCA
		protected override void OnRemoveComplete(int index, object value)
		{
			if (value == this._defaultBinding)
			{
				this.FindDefaultBinding();
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000CE9DC File Offset: 0x000CCBDC
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			TreeNodeBinding treeNodeBinding = value as TreeNodeBinding;
			if (treeNodeBinding != null && treeNodeBinding.DataMember.Length == 0 && treeNodeBinding.Depth == -1)
			{
				this._defaultBinding = treeNodeBinding;
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(TreeNodeBinding binding)
		{
			((IList)this).Remove(binding);
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000CEA17 File Offset: 0x000CCC17
		protected override void SetDirtyObject(object o)
		{
			if (o is TreeNodeBinding)
			{
				((TreeNodeBinding)o).SetDirty();
			}
		}

		// Token: 0x04002464 RID: 9316
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(TreeNodeBinding)
		};

		// Token: 0x04002465 RID: 9317
		private TreeNodeBinding _defaultBinding;
	}
}
