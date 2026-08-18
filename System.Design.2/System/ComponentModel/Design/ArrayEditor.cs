using System;

namespace System.ComponentModel.Design
{
	// Token: 0x02000194 RID: 404
	public class ArrayEditor : CollectionEditor
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ArrayEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0001F144 File Offset: 0x0001D344
		protected override Type CreateCollectionItemType()
		{
			return base.CollectionType.GetElementType();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000547A0 File Offset: 0x000529A0
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

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000547DC File Offset: 0x000529DC
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
