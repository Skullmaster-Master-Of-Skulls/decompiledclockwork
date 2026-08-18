using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000244 RID: 580
	internal sealed class StorageAssociationSetModificationFunctionMapping
	{
		// Token: 0x06002474 RID: 9332 RVA: 0x00083ED4 File Offset: 0x000820D4
		internal StorageAssociationSetModificationFunctionMapping(AssociationSet associationSet, StorageModificationFunctionMapping deleteFunctionMapping, StorageModificationFunctionMapping insertFunctionMapping)
		{
			this.AssociationSet = EntityUtil.CheckArgumentNull<AssociationSet>(associationSet, "associationSet");
			this.DeleteFunctionMapping = deleteFunctionMapping;
			this.InsertFunctionMapping = insertFunctionMapping;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00083EFC File Offset: 0x000820FC
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

		// Token: 0x06002476 RID: 9334 RVA: 0x00083F4C File Offset: 0x0008214C
		internal void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Association Set Function Mapping");
			stringBuilder.Append("   ");
			stringBuilder.Append(this.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x04001024 RID: 4132
		internal readonly AssociationSet AssociationSet;

		// Token: 0x04001025 RID: 4133
		internal readonly StorageModificationFunctionMapping DeleteFunctionMapping;

		// Token: 0x04001026 RID: 4134
		internal readonly StorageModificationFunctionMapping InsertFunctionMapping;
	}
}
