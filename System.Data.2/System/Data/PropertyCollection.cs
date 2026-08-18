using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class PropertyCollection : Hashtable
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x000832C4 File Offset: 0x000826C4
		public PropertyCollection()
		{
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000832D8 File Offset: 0x000826D8
		protected PropertyCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000832F0 File Offset: 0x000826F0
		public override object Clone()
		{
			PropertyCollection propertyCollection = new PropertyCollection();
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				propertyCollection.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return propertyCollection;
		}
	}
}
