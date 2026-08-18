using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000245 RID: 581
	internal class TagNamespaceRegisterEntryTable : Hashtable
	{
		// Token: 0x06001AF5 RID: 6901 RVA: 0x000548B1 File Offset: 0x00052AB1
		public TagNamespaceRegisterEntryTable() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000548C0 File Offset: 0x00052AC0
		public override object Clone()
		{
			TagNamespaceRegisterEntryTable tagNamespaceRegisterEntryTable = new TagNamespaceRegisterEntryTable();
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				tagNamespaceRegisterEntryTable[dictionaryEntry.Key] = ((ArrayList)dictionaryEntry.Value).Clone();
			}
			return tagNamespaceRegisterEntryTable;
		}
	}
}
