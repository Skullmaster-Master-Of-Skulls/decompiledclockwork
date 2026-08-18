using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public class PropertyCollection : Hashtable
	{
		// Token: 0x06000CCA RID: 3274 RVA: 0x002122E8 File Offset: 0x002116E8
		public PropertyCollection()
		{
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00212308 File Offset: 0x00211708
		protected PropertyCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
