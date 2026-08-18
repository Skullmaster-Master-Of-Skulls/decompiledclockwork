using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002E7 RID: 743
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapAttributeAttribute : Attribute
	{
		// Token: 0x060022C8 RID: 8904 RVA: 0x000A3ABB File Offset: 0x000A2ABB
		public SoapAttributeAttribute()
		{
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A3AC3 File Offset: 0x000A2AC3
		public SoapAttributeAttribute(string attributeName)
		{
			this.attributeName = attributeName;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000A3AD2 File Offset: 0x000A2AD2
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x000A3AE8 File Offset: 0x000A2AE8
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000A3AF1 File Offset: 0x000A2AF1
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x000A3AF9 File Offset: 0x000A2AF9
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

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000A3B02 File Offset: 0x000A2B02
		// (set) Token: 0x060022CF RID: 8911 RVA: 0x000A3B18 File Offset: 0x000A2B18
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

		// Token: 0x040014D1 RID: 5329
		private string attributeName;

		// Token: 0x040014D2 RID: 5330
		private string ns;

		// Token: 0x040014D3 RID: 5331
		private string dataType;
	}
}
