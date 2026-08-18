using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200017A RID: 378
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class SoapTypeAttribute : Attribute
	{
		// Token: 0x0600191E RID: 6430 RVA: 0x0007057C File Offset: 0x0006E77C
		public SoapTypeAttribute()
		{
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0007058B File Offset: 0x0006E78B
		public SoapTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000705A1 File Offset: 0x0006E7A1
		public SoapTypeAttribute(string typeName, string ns)
		{
			this.typeName = typeName;
			this.ns = ns;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x000705BE File Offset: 0x0006E7BE
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x000705C6 File Offset: 0x0006E7C6
		public bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x000705CF File Offset: 0x0006E7CF
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x000705E5 File Offset: 0x0006E7E5
		public string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x000705EE File Offset: 0x0006E7EE
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x000705F6 File Offset: 0x0006E7F6
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

		// Token: 0x04000B62 RID: 2914
		private string ns;

		// Token: 0x04000B63 RID: 2915
		private string typeName;

		// Token: 0x04000B64 RID: 2916
		private bool includeInSchema = true;
	}
}
