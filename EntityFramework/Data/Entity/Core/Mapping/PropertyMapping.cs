using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200000F RID: 15
	public abstract class PropertyMapping : MappingItem
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00004A82 File Offset: 0x00002C82
		internal PropertyMapping(EdmProperty property)
		{
			this._property = property;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004A91 File Offset: 0x00002C91
		internal PropertyMapping()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004A99 File Offset: 0x00002C99
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00004AA1 File Offset: 0x00002CA1
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		public virtual EdmProperty Property
		{
			get
			{
				return this._property;
			}
			internal set
			{
				this._property = value;
			}
		}

		// Token: 0x0400001D RID: 29
		private EdmProperty _property;
	}
}
