using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200066A RID: 1642
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeBindingCollection : StateManagedCollection
	{
		// Token: 0x0600509D RID: 20637 RVA: 0x00144091 File Offset: 0x00143091
		internal TreeNodeBindingCollection()
		{
		}

		// Token: 0x1700147A RID: 5242
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

		// Token: 0x060050A0 RID: 20640 RVA: 0x001440B1 File Offset: 0x001430B1
		public int Add(TreeNodeBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x001440BA File Offset: 0x001430BA
		public bool Contains(TreeNodeBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x001440C3 File Offset: 0x001430C3
		public void CopyTo(TreeNodeBinding[] bindingArray, int index)
		{
			base.CopyTo(bindingArray, index);
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x001440CD File Offset: 0x001430CD
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeBinding();
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x001440D4 File Offset: 0x001430D4
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

		// Token: 0x060050A5 RID: 20645 RVA: 0x00144148 File Offset: 0x00143148
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

		// Token: 0x060050A6 RID: 20646 RVA: 0x00144240 File Offset: 0x00143240
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeBindingCollection.knownTypes;
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00144247 File Offset: 0x00143247
		public int IndexOf(TreeNodeBinding binding)
		{
			return ((IList)this).IndexOf(binding);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00144250 File Offset: 0x00143250
		public void Insert(int index, TreeNodeBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0014425A File Offset: 0x0014325A
		protected override void OnClear()
		{
			base.OnClear();
			this._defaultBinding = null;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00144269 File Offset: 0x00143269
		protected override void OnRemoveComplete(int index, object value)
		{
			if (value == this._defaultBinding)
			{
				this.FindDefaultBinding();
			}
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0014427C File Offset: 0x0014327C
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			TreeNodeBinding treeNodeBinding = value as TreeNodeBinding;
			if (treeNodeBinding != null && treeNodeBinding.DataMember.Length == 0 && treeNodeBinding.Depth == -1)
			{
				this._defaultBinding = treeNodeBinding;
			}
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x001442B7 File Offset: 0x001432B7
		public void Remove(TreeNodeBinding binding)
		{
			((IList)this).Remove(binding);
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x001442C0 File Offset: 0x001432C0
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x001442C9 File Offset: 0x001432C9
		protected override void SetDirtyObject(object o)
		{
			if (o is TreeNodeBinding)
			{
				((TreeNodeBinding)o).SetDirty();
			}
		}

		// Token: 0x04002D24 RID: 11556
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(TreeNodeBinding)
		};

		// Token: 0x04002D25 RID: 11557
		private TreeNodeBinding _defaultBinding;
	}
}
