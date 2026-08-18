using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F1 RID: 753
	internal class ImageCollectionEditor : CollectionEditor
	{
		// Token: 0x06001E11 RID: 7697 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ImageCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000B6730 File Offset: 0x000B4930
		protected override string GetDisplayText(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(value)["Name"];
			string text;
			if (propertyDescriptor != null)
			{
				text = (string)propertyDescriptor.GetValue(value);
				if (text != null && text.Length > 0)
				{
					return text;
				}
			}
			if (value is ImageListImage)
			{
				value = ((ImageListImage)value).Image;
			}
			text = TypeDescriptor.GetConverter(value).ConvertToString(value);
			if (text == null || text.Length == 0)
			{
				text = value.GetType().Name;
			}
			return text;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000B67B0 File Offset: 0x000B49B0
		protected override object CreateInstance(Type type)
		{
			UITypeEditor uitypeEditor = (UITypeEditor)TypeDescriptor.GetEditor(typeof(ImageListImage), typeof(UITypeEditor));
			return uitypeEditor.EditValue(base.Context, null);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B67EC File Offset: 0x000B49EC
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("ImageCollectionEditorFormText");
			return collectionForm;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B6814 File Offset: 0x000B4A14
		protected override IList GetObjectsFromInstance(object instance)
		{
			ArrayList arrayList = instance as ArrayList;
			if (arrayList != null)
			{
				return arrayList;
			}
			return null;
		}
	}
}
