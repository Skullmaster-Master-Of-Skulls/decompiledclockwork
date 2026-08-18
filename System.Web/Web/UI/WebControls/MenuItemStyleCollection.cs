using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005EB RID: 1515
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemStyleCollection : StateManagedCollection
	{
		// Token: 0x06004B01 RID: 19201 RVA: 0x001326AD File Offset: 0x001316AD
		internal MenuItemStyleCollection()
		{
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x001326B8 File Offset: 0x001316B8
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

		// Token: 0x170012CC RID: 4812
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

		// Token: 0x06004B05 RID: 19205 RVA: 0x0013271F File Offset: 0x0013171F
		public int Add(MenuItemStyle style)
		{
			return ((IList)this).Add(style);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00132728 File Offset: 0x00131728
		public bool Contains(MenuItemStyle style)
		{
			return ((IList)this).Contains(style);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00132731 File Offset: 0x00131731
		public void CopyTo(MenuItemStyle[] styleArray, int index)
		{
			base.CopyTo(styleArray, index);
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0013273B File Offset: 0x0013173B
		public int IndexOf(MenuItemStyle style)
		{
			return ((IList)this).IndexOf(style);
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00132744 File Offset: 0x00131744
		public void Insert(int index, MenuItemStyle style)
		{
			((IList)this).Insert(index, style);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x0013274E File Offset: 0x0013174E
		protected override object CreateKnownType(int index)
		{
			return new MenuItemStyle();
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00132755 File Offset: 0x00131755
		protected override Type[] GetKnownTypes()
		{
			return MenuItemStyleCollection.knownTypes;
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x0013275C File Offset: 0x0013175C
		public void Remove(MenuItemStyle style)
		{
			((IList)this).Remove(style);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00132765 File Offset: 0x00131765
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0013276E File Offset: 0x0013176E
		protected override void SetDirtyObject(object o)
		{
			if (o is MenuItemStyle)
			{
				((MenuItemStyle)o).SetDirty();
			}
		}

		// Token: 0x04002B97 RID: 11159
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(MenuItemStyle)
		};
	}
}
