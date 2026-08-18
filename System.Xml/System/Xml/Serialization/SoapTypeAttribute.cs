using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F4 RID: 756
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class SoapTypeAttribute : Attribute
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000A81F4 File Offset: 0x000A71F4
		public SoapTypeAttribute()
		{
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000A8203 File Offset: 0x000A7203
		public SoapTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000A8219 File Offset: 0x000A7219
		public SoapTypeAttribute(string typeName, string ns)
		{
			this.typeName = typeName;
			this.ns = ns;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000A8236 File Offset: 0x000A7236
		// (set) Token: 0x0600236B RID: 9067 RVA: 0x000A823E File Offset: 0x000A723E
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

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000A8247 File Offset: 0x000A7247
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x000A825D File Offset: 0x000A725D
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

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000A8266 File Offset: 0x000A7266
		// (set) Token: 0x0600236F RID: 9071 RVA: 0x000A826E File Offset: 0x000A726E
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

		// Token: 0x040014F6 RID: 5366
		private string ns;

		// Token: 0x040014F7 RID: 5367
		private string typeName;

		// Token: 0x040014F8 RID: 5368
		private bool includeInSchema = true;
	}
}
