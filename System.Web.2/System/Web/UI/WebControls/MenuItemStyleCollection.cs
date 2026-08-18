using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000474 RID: 1140
	public sealed class MenuItemStyleCollection : StateManagedCollection
	{
		// Token: 0x06003854 RID: 14420 RVA: 0x00095F2B File Offset: 0x0009412B
		internal MenuItemStyleCollection()
		{
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000B7BB0 File Offset: 0x000B5DB0
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			if (value is MenuItemStyle)
			{
				MenuItemStyle menuItemStyle = (MenuItemStyle)value;
				menuItemStyle.Font.Underline = menuItemStyle.Font.Underline;
				return;
			}
			throw new ArgumentException(SR.GetString("MenuItemStyleCollection_InvalidArgument"), "value");
		}

		// Token: 0x17001086 RID: 4230
		public MenuItemStyle this[int i]
		{
			get
			{
				return (MenuItemStyle)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(MenuItemStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(MenuItemStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000B7C0D File Offset: 0x000B5E0D
		public void CopyTo(MenuItemStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(MenuItemStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, MenuItemStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000B7C17 File Offset: 0x000B5E17
		protected override object CreateKnownType(int index)
		{
			return new MenuItemStyle();
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000B7C1E File Offset: 0x000B5E1E
		protected override Type[] GetKnownTypes()
		{
			return MenuItemStyleCollection.knownTypes;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(MenuItemStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000B7C25 File Offset: 0x000B5E25
		protected override void SetDirtyObject(object o)
		{
			if (o is MenuItemStyle)
			{
				((MenuItemStyle)o).SetDirty();
			}
		}

		// Token: 0x0400227F RID: 8831
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(MenuItemStyle)
		};
	}
}
