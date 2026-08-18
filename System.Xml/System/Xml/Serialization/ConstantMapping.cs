using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002CB RID: 715
	internal class ConstantMapping : Mapping
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x0009F3DD File Offset: 0x0009E3DD
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0009F3F3 File Offset: 0x0009E3F3
		internal string XmlName
		{
			get
			{
				if (this.xmlName != null)
				{
					return this.xmlName;
				}
				return string.Empty;
			}
			set
			{
				this.xmlName = value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0009F3FC File Offset: 0x0009E3FC
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0009F412 File Offset: 0x0009E412
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x0009F41B File Offset: 0x0009E41B
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x0009F423 File Offset: 0x0009E423
		internal long Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x0400147E RID: 5246
		private string xmlName;

		// Token: 0x0400147F RID: 5247
		private string name;

		// Token: 0x04001480 RID: 5248
		private long value;
	}
}
