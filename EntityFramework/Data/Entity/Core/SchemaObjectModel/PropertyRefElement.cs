using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000370 RID: 880
	internal sealed class PropertyRefElement : SchemaElement
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x000960DD File Offset: 0x000942DD
		public PropertyRefElement(SchemaElement parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000960E7 File Offset: 0x000942E7
		public StructuredProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000960EF File Offset: 0x000942EF
		internal override void ResolveTopLevelNames()
		{
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000960F1 File Offset: 0x000942F1
		internal bool ResolveNames(SchemaEntityType entityType)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return true;
			}
			this._property = entityType.FindProperty(this.Name);
			return this._property != null;
		}

		// Token: 0x04000B4D RID: 2893
		private StructuredProperty _property;
	}
}
