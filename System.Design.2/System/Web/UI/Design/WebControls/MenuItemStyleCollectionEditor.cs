using System;
using System.ComponentModel.Design;
using System.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000EA RID: 234
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MenuItemStyleCollectionEditor : CollectionEditor
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00023ABB File Offset: 0x00021CBB
		public MenuItemStyleCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002CB78 File Offset: 0x0002AD78
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("CollectionEditorCaption", new object[]
			{
				"MenuItemStyle"
			});
			return collectionForm;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002CBBB File Offset: 0x0002ADBB
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(MenuItemStyle)
			};
		}
	}
}
