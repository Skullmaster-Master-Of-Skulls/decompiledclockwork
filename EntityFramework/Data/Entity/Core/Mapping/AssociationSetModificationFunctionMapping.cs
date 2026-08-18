using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C5 RID: 965
	public sealed class AssociationSetModificationFunctionMapping : MappingItem
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x000A4DFA File Offset: 0x000A2FFA
		public AssociationSetModificationFunctionMapping(AssociationSet associationSet, ModificationFunctionMapping deleteFunctionMapping, ModificationFunctionMapping insertFunctionMapping)
		{
			Check.NotNull<AssociationSet>(associationSet, "associationSet");
			this._associationSet = associationSet;
			this._deleteFunctionMapping = deleteFunctionMapping;
			this._insertFunctionMapping = insertFunctionMapping;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000A4E23 File Offset: 0x000A3023
		public AssociationSet AssociationSet
		{
			get
			{
				return this._associationSet;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000A4E2B File Offset: 0x000A302B
		public ModificationFunctionMapping DeleteFunctionMapping
		{
			get
			{
				return this._deleteFunctionMapping;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000A4E33 File Offset: 0x000A3033
		public ModificationFunctionMapping InsertFunctionMapping
		{
			get
			{
				return this._insertFunctionMapping;
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000A4E3C File Offset: 0x000A303C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "AS{{{0}}}:{3}DFunc={{{1}}},{3}IFunc={{{2}}}", new object[]
			{
				this.AssociationSet,
				this.DeleteFunctionMapping,
				this.InsertFunctionMapping,
				Environment.NewLine + "  "
			});
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000A4E8D File Offset: 0x000A308D
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._deleteFunctionMapping);
			MappingItem.SetReadOnly(this._insertFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x04000C66 RID: 3174
		private readonly AssociationSet _associationSet;

		// Token: 0x04000C67 RID: 3175
		private readonly ModificationFunctionMapping _deleteFunctionMapping;

		// Token: 0x04000C68 RID: 3176
		private readonly ModificationFunctionMapping _insertFunctionMapping;
	}
}
