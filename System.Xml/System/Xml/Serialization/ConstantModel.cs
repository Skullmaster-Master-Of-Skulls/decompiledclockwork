using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002DD RID: 733
	internal class ConstantModel
	{
		// Token: 0x06002262 RID: 8802 RVA: 0x000A0EEC File Offset: 0x0009FEEC
		internal ConstantModel(FieldInfo fieldInfo, long value)
		{
			this.fieldInfo = fieldInfo;
			this.value = value;
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000A0F02 File Offset: 0x0009FF02
		internal string Name
		{
			get
			{
				return this.fieldInfo.Name;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000A0F0F File Offset: 0x0009FF0F
		internal long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000A0F17 File Offset: 0x0009FF17
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x040014BE RID: 5310
		private FieldInfo fieldInfo;

		// Token: 0x040014BF RID: 5311
		private long value;
	}
}
