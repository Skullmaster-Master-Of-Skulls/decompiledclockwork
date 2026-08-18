using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000339 RID: 825
	internal class StringArrayEditor : StringCollectionEditor
	{
		// Token: 0x0600207E RID: 8318 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		public StringArrayEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0001F144 File Offset: 0x0001D344
		protected override Type CreateCollectionItemType()
		{
			return base.CollectionType.GetElementType();
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000C595C File Offset: 0x000C3B5C
		protected override object[] GetItems(object editValue)
		{
			Array array = editValue as Array;
			if (array == null)
			{
				return new object[0];
			}
			object[] array2 = new object[array.GetLength(0)];
			Array.Copy(array, array2, array2.Length);
			return array2;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000C5994 File Offset: 0x000C3B94
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
