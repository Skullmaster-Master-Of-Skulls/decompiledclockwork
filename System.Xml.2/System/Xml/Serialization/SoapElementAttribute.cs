using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000172 RID: 370
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0006C9DD File Offset: 0x0006ABDD
		public SoapElementAttribute()
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0006C9E5 File Offset: 0x0006ABE5
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0006C9F4 File Offset: 0x0006ABF4
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0006CA0A File Offset: 0x0006AC0A
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0006CA13 File Offset: 0x0006AC13
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0006CA29 File Offset: 0x0006AC29
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0006CA32 File Offset: 0x0006AC32
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x0006CA3A File Offset: 0x0006AC3A
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

		// Token: 0x04000B4C RID: 2892
		private string elementName;

		// Token: 0x04000B4D RID: 2893
		private string dataType;

		// Token: 0x04000B4E RID: 2894
		private bool nullable;
	}
}
