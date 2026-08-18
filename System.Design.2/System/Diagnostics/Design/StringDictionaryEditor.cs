using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Design;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020D RID: 525
	internal class StringDictionaryEditor : CollectionEditor
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x00023ABB File Offset: 0x00021CBB
		public StringDictionaryEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0006F92C File Offset: 0x0006DB2C
		protected override Type CreateCollectionItemType()
		{
			return typeof(EditableDictionaryEntry);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0006F938 File Offset: 0x0006DB38
		protected override object CreateInstance(Type itemType)
		{
			return new EditableDictionaryEntry("name", "value");
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006F94C File Offset: 0x0006DB4C
		protected override object SetItems(object editValue, object[] value)
		{
			StringDictionary stringDictionary = editValue as StringDictionary;
			if (stringDictionary == null)
			{
				throw new ArgumentNullException("editValue");
			}
			stringDictionary.Clear();
			foreach (EditableDictionaryEntry editableDictionaryEntry in value)
			{
				stringDictionary[editableDictionaryEntry.Name] = editableDictionaryEntry.Value;
			}
			return stringDictionary;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006F9A0 File Offset: 0x0006DBA0
		protected override object[] GetItems(object editValue)
		{
			if (editValue == null)
			{
				return new object[0];
			}
			StringDictionary stringDictionary = editValue as StringDictionary;
			if (stringDictionary == null)
			{
				throw new ArgumentNullException("editValue");
			}
			object[] array = new object[stringDictionary.Count];
			int num = 0;
			foreach (object obj in stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				EditableDictionaryEntry editableDictionaryEntry = new EditableDictionaryEntry((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				array[num++] = editableDictionaryEntry;
			}
			return array;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006FA4C File Offset: 0x0006DC4C
		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
			collectionForm.Text = SR.GetString("StringDictionaryEditorTitle");
			collectionForm.CollectionEditable = true;
			return collectionForm;
		}
	}
}
