using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000471 RID: 1137
	public sealed class MenuItemBindingCollection : StateManagedCollection
	{
		// Token: 0x0600381B RID: 14363 RVA: 0x00095F2B File Offset: 0x0009412B
		private MenuItemBindingCollection()
		{
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000B6D89 File Offset: 0x000B4F89
		internal MenuItemBindingCollection(Menu owner)
		{
			this._owner = owner;
		}

		// Token: 0x1700107B RID: 4219
		public MenuItemBinding this[int i]
		{
			get
			{
				return (MenuItemBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(MenuItemBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(MenuItemBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(MenuItemBinding[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000B6DB0 File Offset: 0x000B4FB0
		protected override object CreateKnownType(int index)
		{
			return new MenuItemBinding();
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000B6DB8 File Offset: 0x000B4FB8
		private void FindDefaultBinding()
		{
			this._defaultBinding = null;
			foreach (object obj in this)
			{
				MenuItemBinding menuItemBinding = (MenuItemBinding)obj;
				if (menuItemBinding.Depth == -1 && menuItemBinding.DataMember.Length == 0)
				{
					this._defaultBinding = menuItemBinding;
					break;
				}
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000B6E2C File Offset: 0x000B502C
		internal MenuItemBinding GetBinding(string dataMember, int depth)
		{
			MenuItemBinding menuItemBinding = null;
			int num = 0;
			if (dataMember != null && dataMember.Length == 0)
			{
				dataMember = null;
			}
			foreach (object obj in this)
			{
				MenuItemBinding menuItemBinding2 = (MenuItemBinding)obj;
				if (menuItemBinding2.Depth == depth)
				{
					if (string.Equals(menuItemBinding2.DataMember, dataMember, StringComparison.CurrentCultureIgnoreCase))
					{
						return menuItemBinding2;
					}
					if (num < 1 && menuItemBinding2.DataMember.Length == 0)
					{
						menuItemBinding = menuItemBinding2;
						num = 1;
					}
				}
				else if (string.Equals(menuItemBinding2.DataMember, dataMember, StringComparison.CurrentCultureIgnoreCase) && num < 2 && menuItemBinding2.Depth == -1)
				{
					menuItemBinding = menuItemBinding2;
					num = 2;
				}
			}
			if (menuItemBinding == null && this._defaultBinding != null)
			{
				if (this._defaultBinding.Depth != -1 || this._defaultBinding.DataMember.Length != 0)
				{
					this.FindDefaultBinding();
				}
				menuItemBinding = this._defaultBinding;
			}
			return menuItemBinding;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000B6F24 File Offset: 0x000B5124
		protected override Type[] GetKnownTypes()
		{
			return MenuItemBindingCollection.knownTypes;
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(MenuItemBinding value)
		{
			return ((IList)this).IndexOf(value);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, MenuItemBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000B6F2B File Offset: 0x000B512B
		protected override void OnClear()
		{
			this._defaultBinding = null;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000B6F34 File Offset: 0x000B5134
		protected override void OnRemoveComplete(int index, object value)
		{
			if (value == this._defaultBinding)
			{
				this.FindDefaultBinding();
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000B6F48 File Offset: 0x000B5148
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			MenuItemBinding menuItemBinding = value as MenuItemBinding;
			if (menuItemBinding != null && menuItemBinding.DataMember.Length == 0 && menuItemBinding.Depth == -1)
			{
				this._defaultBinding = menuItemBinding;
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(MenuItemBinding binding)
		{
			((IList)this).Remove(binding);
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000B6F83 File Offset: 0x000B5183
		protected override void SetDirtyObject(object o)
		{
			if (o is MenuItemBinding)
			{
				((MenuItemBinding)o).SetDirty();
			}
		}

		// Token: 0x04002273 RID: 8819
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(MenuItemBinding)
		};

		// Token: 0x04002274 RID: 8820
		private Menu _owner;

		// Token: 0x04002275 RID: 8821
		private MenuItemBinding _defaultBinding;
	}
}
