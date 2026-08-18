using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000162 RID: 354
	internal class ConstantModel
	{
		// Token: 0x06001817 RID: 6167 RVA: 0x000692AF File Offset: 0x000674AF
		internal ConstantModel(FieldInfo fieldInfo, long value)
		{
			this.fieldInfo = fieldInfo;
			this.value = value;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x000692C5 File Offset: 0x000674C5
		internal string Name
		{
			get
			{
				return this.fieldInfo.Name;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x000692D2 File Offset: 0x000674D2
		internal long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x000692DA File Offset: 0x000674DA
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x04000B2A RID: 2858
		private FieldInfo fieldInfo;

		// Token: 0x04000B2B RID: 2859
		private long value;
	}
}
