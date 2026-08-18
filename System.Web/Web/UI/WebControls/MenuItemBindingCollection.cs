using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E5 RID: 1509
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemBindingCollection : StateManagedCollection
	{
		// Token: 0x06004ABE RID: 19134 RVA: 0x001316B5 File Offset: 0x001306B5
		private MenuItemBindingCollection()
		{
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x001316BD File Offset: 0x001306BD
		internal MenuItemBindingCollection(Menu owner)
		{
			this._owner = owner;
		}

		// Token: 0x170012BC RID: 4796
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

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001316E4 File Offset: 0x001306E4
		public int Add(MenuItemBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x001316ED File Offset: 0x001306ED
		public bool Contains(MenuItemBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x001316F6 File Offset: 0x001306F6
		public void CopyTo(MenuItemBinding[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00131700 File Offset: 0x00130700
		protected override object CreateKnownType(int index)
		{
			return new MenuItemBinding();
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00131708 File Offset: 0x00130708
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

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0013177C File Offset: 0x0013077C
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

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00131874 File Offset: 0x00130874
		protected override Type[] GetKnownTypes()
		{
			return MenuItemBindingCollection.knownTypes;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0013187B File Offset: 0x0013087B
		public int IndexOf(MenuItemBinding value)
		{
			return ((IList)this).IndexOf(value);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00131884 File Offset: 0x00130884
		public void Insert(int index, MenuItemBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0013188E File Offset: 0x0013088E
		protected override void OnClear()
		{
			this._defaultBinding = null;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00131897 File Offset: 0x00130897
		protected override void OnRemoveComplete(int index, object value)
		{
			if (value == this._defaultBinding)
			{
				this.FindDefaultBinding();
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x001318A8 File Offset: 0x001308A8
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			MenuItemBinding menuItemBinding = value as MenuItemBinding;
			if (menuItemBinding != null && menuItemBinding.DataMember.Length == 0 && menuItemBinding.Depth == -1)
			{
				this._defaultBinding = menuItemBinding;
			}
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x001318E3 File Offset: 0x001308E3
		public void Remove(MenuItemBinding binding)
		{
			((IList)this).Remove(binding);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x001318EC File Offset: 0x001308EC
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001318F5 File Offset: 0x001308F5
		protected override void SetDirtyObject(object o)
		{
			if (o is MenuItemBinding)
			{
				((MenuItemBinding)o).SetDirty();
			}
		}

		// Token: 0x04002B80 RID: 11136
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(MenuItemBinding)
		};

		// Token: 0x04002B81 RID: 11137
		private Menu _owner;

		// Token: 0x04002B82 RID: 11138
		private MenuItemBinding _defaultBinding;
	}
}
