using System;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CE RID: 974
	internal class RecordStateFactory
	{
		// Token: 0x0600348E RID: 13454 RVA: 0x000CADDC File Offset: 0x000C8FDC
		public RecordStateFactory(int stateSlotNumber, int columnCount, RecordStateFactory[] nestedRecordStateFactories, DataRecordInfo dataRecordInfo, Expression gatherData, string[] propertyNames, TypeUsage[] typeUsages)
		{
			this.StateSlotNumber = stateSlotNumber;
			this.ColumnCount = columnCount;
			this.NestedRecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(nestedRecordStateFactories);
			this.DataRecordInfo = dataRecordInfo;
			this.GatherData = Translator.Compile<bool>(gatherData);
			this.Description = gatherData.ToString();
			this.ColumnNames = new ReadOnlyCollection<string>(propertyNames);
			this.TypeUsages = new ReadOnlyCollection<TypeUsage>(typeUsages);
			this.FieldNameLookup = new FieldNameLookup(this.ColumnNames, -1);
			bool[] array = new bool[columnCount];
			int i = 0;
			while (i < columnCount)
			{
				BuiltInTypeKind builtInTypeKind = typeUsages[i].EdmType.BuiltInTypeKind;
				if (builtInTypeKind <= BuiltInTypeKind.ComplexType)
				{
					if (builtInTypeKind != BuiltInTypeKind.CollectionType && builtInTypeKind != BuiltInTypeKind.ComplexType)
					{
						goto IL_AD;
					}
					goto IL_A0;
				}
				else
				{
					if (builtInTypeKind == BuiltInTypeKind.EntityType || builtInTypeKind == BuiltInTypeKind.RowType)
					{
						goto IL_A0;
					}
					goto IL_AD;
				}
				IL_B1:
				i++;
				continue;
				IL_A0:
				array[i] = true;
				this.HasNestedColumns = true;
				goto IL_B1;
				IL_AD:
				array[i] = false;
				goto IL_B1;
			}
			this.IsColumnNested = new ReadOnlyCollection<bool>(array);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000CAEAE File Offset: 0x000C90AE
		internal RecordState Create(CoordinatorFactory coordinatorFactory)
		{
			return new RecordState(this, coordinatorFactory);
		}

		// Token: 0x040016FE RID: 5886
		internal readonly int StateSlotNumber;

		// Token: 0x040016FF RID: 5887
		internal readonly int ColumnCount;

		// Token: 0x04001700 RID: 5888
		internal readonly DataRecordInfo DataRecordInfo;

		// Token: 0x04001701 RID: 5889
		internal readonly Func<Shaper, bool> GatherData;

		// Token: 0x04001702 RID: 5890
		internal readonly ReadOnlyCollection<RecordStateFactory> NestedRecordStateFactories;

		// Token: 0x04001703 RID: 5891
		internal readonly ReadOnlyCollection<string> ColumnNames;

		// Token: 0x04001704 RID: 5892
		internal readonly ReadOnlyCollection<TypeUsage> TypeUsages;

		// Token: 0x04001705 RID: 5893
		internal readonly ReadOnlyCollection<bool> IsColumnNested;

		// Token: 0x04001706 RID: 5894
		internal readonly bool HasNestedColumns;

		// Token: 0x04001707 RID: 5895
		internal readonly FieldNameLookup FieldNameLookup;

		// Token: 0x04001708 RID: 5896
		private readonly string Description;
	}
}
