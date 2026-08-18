using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200016D RID: 365
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapAttributeAttribute : Attribute
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x0006BF1B File Offset: 0x0006A11B
		public SoapAttributeAttribute()
		{
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0006BF23 File Offset: 0x0006A123
		public SoapAttributeAttribute(string attributeName)
		{
			this.attributeName = attributeName;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0006BF32 File Offset: 0x0006A132
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0006BF48 File Offset: 0x0006A148
		public string AttributeName
		{
			get
			{
				if (this.attributeName != null)
				{
					return this.attributeName;
				}
				return string.Empty;
			}
			set
			{
				this.attributeName = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0006BF51 File Offset: 0x0006A151
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0006BF59 File Offset: 0x0006A159
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0006BF62 File Offset: 0x0006A162
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0006BF78 File Offset: 0x0006A178
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

		// Token: 0x04000B3D RID: 2877
		private string attributeName;

		// Token: 0x04000B3E RID: 2878
		private string ns;

		// Token: 0x04000B3F RID: 2879
		private string dataType;
	}
}
