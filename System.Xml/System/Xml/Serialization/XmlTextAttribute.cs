using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033D RID: 829
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlTextAttribute : Attribute
	{
		// Token: 0x06002896 RID: 10390 RVA: 0x000D1CCB File Offset: 0x000D0CCB
		public XmlTextAttribute()
		{
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000D1CD3 File Offset: 0x000D0CD3
		public XmlTextAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000D1CE2 File Offset: 0x000D0CE2
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x000D1CEA File Offset: 0x000D0CEA
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x000D1CF3 File Offset: 0x000D0CF3
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x000D1D09 File Offset: 0x000D0D09
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

		// Token: 0x04001687 RID: 5767
		private Type type;

		// Token: 0x04001688 RID: 5768
		private string dataType;
	}
}
