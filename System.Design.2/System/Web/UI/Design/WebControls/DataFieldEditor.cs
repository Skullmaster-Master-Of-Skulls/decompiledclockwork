using System;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B9 RID: 185
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DataFieldEditor : DataFieldCollectionEditor
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001F13B File Offset: 0x0001D33B
		public DataFieldEditor(Type type) : base(type)
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001F144 File Offset: 0x0001D344
		protected override Type CreateCollectionItemType()
		{
			return base.CollectionType.GetElementType();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001F154 File Offset: 0x0001D354
		protected override object[] GetItems(object editValue)
		{
			if (editValue is Array)
			{
				Array array = (Array)editValue;
				object[] array2 = new object[array.GetLength(0)];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
			return new object[0];
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001F190 File Offset: 0x0001D390
		protected override object SetItems(object editValue, object[] value)
		{
			if (editValue is Array || editValue == null)
			{
				Array array = Array.CreateInstance(base.CollectionItemType, value.Length);
				Array.Copy(value, array, value.Length);
				return array;
			}
			return editValue;
		}
	}
}
