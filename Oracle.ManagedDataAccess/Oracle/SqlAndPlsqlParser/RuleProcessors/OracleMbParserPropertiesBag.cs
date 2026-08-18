using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors
{
	// Token: 0x02000322 RID: 802
	internal class OracleMbParserPropertiesBag
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0011F2F8 File Offset: 0x0011D4F8
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0011F300 File Offset: 0x0011D500
		public string Name
		{
			get
			{
				return this.m_vName;
			}
			set
			{
				this.m_vName = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0011F30C File Offset: 0x0011D50C
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x0011F314 File Offset: 0x0011D514
		public string Owner
		{
			get
			{
				return this.m_vOwner;
			}
			set
			{
				this.m_vOwner = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x0011F320 File Offset: 0x0011D520
		public Dictionary<string, object> Properties
		{
			get
			{
				return this.m_vPropertiesBag;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0011F328 File Offset: 0x0011D528
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0011F348 File Offset: 0x0011D548
		public string DefaultSchemaName
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_vDefaultSchemaName))
				{
					this.m_vDefaultSchemaName = "DefaultSchema";
				}
				return this.m_vDefaultSchemaName;
			}
			set
			{
				this.m_vDefaultSchemaName = value;
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0011F368 File Offset: 0x0011D568
		internal void RemoveKeyFromProperties(string key)
		{
			if (!string.IsNullOrEmpty(key) && this.Properties.ContainsKey(key))
			{
				this.Properties.Remove(key);
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0011F390 File Offset: 0x0011D590
		public virtual void Clear()
		{
			this.m_vPropertiesBag.Clear();
			this.m_vName = null;
			this.m_vOwner = null;
			this.m_vDefaultSchemaName = "DefaultSchema";
		}

		// Token: 0x04001D84 RID: 7556
		private const string c_strDoubleQuotes = "\"";

		// Token: 0x04001D85 RID: 7557
		private static readonly char[] c_strDoubleQuotesCharArr = new char[]
		{
			'"'
		};

		// Token: 0x04001D86 RID: 7558
		protected string m_vName;

		// Token: 0x04001D87 RID: 7559
		protected string m_vOwner;

		// Token: 0x04001D88 RID: 7560
		protected Dictionary<string, object> m_vPropertiesBag = new Dictionary<string, object>();

		// Token: 0x04001D89 RID: 7561
		protected string m_vDefaultSchemaName;
	}
}
