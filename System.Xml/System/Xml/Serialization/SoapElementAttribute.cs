using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002EC RID: 748
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		// Token: 0x060022F6 RID: 8950 RVA: 0x000A4579 File Offset: 0x000A3579
		public SoapElementAttribute()
		{
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000A4581 File Offset: 0x000A3581
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000A4590 File Offset: 0x000A3590
		// (set) Token: 0x060022F9 RID: 8953 RVA: 0x000A45A6 File Offset: 0x000A35A6
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x000A45AF File Offset: 0x000A35AF
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x000A45C5 File Offset: 0x000A35C5
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000A45CE File Offset: 0x000A35CE
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x000A45D6 File Offset: 0x000A35D6
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x040014E0 RID: 5344
		private string elementName;

		// Token: 0x040014E1 RID: 5345
		private string dataType;

		// Token: 0x040014E2 RID: 5346
		private bool nullable;
	}
}
