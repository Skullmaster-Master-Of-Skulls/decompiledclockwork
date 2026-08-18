using System;
using System.ComponentModel.Design;
using System.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000122 RID: 290
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SubMenuStyleCollectionEditor : CollectionEditor
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x00023ABB File Offset: 0x00021CBB
		public SubMenuStyleCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00043320 File Offset: 0x00041520
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("CollectionEditorCaption", new object[]
			{
				"SubMenuStyle"
			});
			return collectionForm;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002CBAB File Offset: 0x0002ADAB
		protected override object CreateInstance(Type itemType)
		{
			return Activator.CreateInstance(itemType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00043353 File Offset: 0x00041553
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(SubMenuStyle)
			};
		}
	}
}
