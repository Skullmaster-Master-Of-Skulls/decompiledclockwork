using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000245 RID: 581
	internal sealed class StorageEntityTypeModificationFunctionMapping
	{
		// Token: 0x06002477 RID: 9335 RVA: 0x00083F97 File Offset: 0x00082197
		internal StorageEntityTypeModificationFunctionMapping(EntityType entityType, StorageModificationFunctionMapping deleteFunctionMapping, StorageModificationFunctionMapping insertFunctionMapping, StorageModificationFunctionMapping updateFunctionMapping)
		{
			this.EntityType = EntityUtil.CheckArgumentNull<EntityType>(entityType, "entityType");
			this.DeleteFunctionMapping = deleteFunctionMapping;
			this.InsertFunctionMapping = insertFunctionMapping;
			this.UpdateFunctionMapping = updateFunctionMapping;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00083FC8 File Offset: 0x000821C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ET{{{0}}}:{4}DFunc={{{1}}},{4}IFunc={{{2}}},{4}UFunc={{{3}}}", new object[]
			{
				this.EntityType,
				this.DeleteFunctionMapping,
				this.InsertFunctionMapping,
				this.UpdateFunctionMapping,
				Environment.NewLine + "  "
			});
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00084020 File Offset: 0x00082220
		internal void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Entity Type Function Mapping");
			stringBuilder.Append("   ");
			stringBuilder.Append(this.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x04001027 RID: 4135
		internal readonly EntityType EntityType;

		// Token: 0x04001028 RID: 4136
		internal readonly StorageModificationFunctionMapping DeleteFunctionMapping;

		// Token: 0x04001029 RID: 4137
		internal readonly StorageModificationFunctionMapping InsertFunctionMapping;

		// Token: 0x0400102A RID: 4138
		internal readonly StorageModificationFunctionMapping UpdateFunctionMapping;
	}
}
