using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002E0 RID: 736
	internal class RecordStateFactory
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x00080BD4 File Offset: 0x0007EDD4
		public RecordStateFactory(int stateSlotNumber, int columnCount, RecordStateFactory[] nestedRecordStateFactories, DataRecordInfo dataRecordInfo, Expression<Func<Shaper, bool>> gatherData, string[] propertyNames, TypeUsage[] typeUsages, bool[] isColumnNested)
		{
			this.StateSlotNumber = stateSlotNumber;
			this.ColumnCount = columnCount;
			this.NestedRecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(nestedRecordStateFactories);
			this.DataRecordInfo = dataRecordInfo;
			this.GatherData = gatherData.Compile();
			this.Description = gatherData.ToString();
			this.ColumnNames = new ReadOnlyCollection<string>(propertyNames);
			this.TypeUsages = new ReadOnlyCollection<TypeUsage>(typeUsages);
			this.FieldNameLookup = new FieldNameLookup(this.ColumnNames);
			if (isColumnNested == null)
			{
				isColumnNested = new bool[columnCount];
				int i = 0;
				while (i < columnCount)
				{
					BuiltInTypeKind builtInTypeKind = typeUsages[i].EdmType.BuiltInTypeKind;
					switch (builtInTypeKind)
					{
					case BuiltInTypeKind.CollectionType:
					case BuiltInTypeKind.ComplexType:
						goto IL_AA;
					case BuiltInTypeKind.CollectionKind:
						goto IL_B8;
					default:
						if (builtInTypeKind == BuiltInTypeKind.EntityType || builtInTypeKind == BuiltInTypeKind.RowType)
						{
							goto IL_AA;
						}
						goto IL_B8;
					}
					IL_BD:
					i++;
					continue;
					IL_AA:
					isColumnNested[i] = true;
					this.HasNestedColumns = true;
					goto IL_BD;
					IL_B8:
					isColumnNested[i] = false;
					goto IL_BD;
				}
			}
			this.IsColumnNested = new ReadOnlyCollection<bool>(isColumnNested);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00080CB4 File Offset: 0x0007EEB4
		public RecordStateFactory(int stateSlotNumber, int columnCount, RecordStateFactory[] nestedRecordStateFactories, DataRecordInfo dataRecordInfo, Expression gatherData, string[] propertyNames, TypeUsage[] typeUsages) : this(stateSlotNumber, columnCount, nestedRecordStateFactories, dataRecordInfo, CodeGenEmitter.BuildShaperLambda<bool>(gatherData), propertyNames, typeUsages, null)
		{
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00080CD8 File Offset: 0x0007EED8
		internal RecordState Create(CoordinatorFactory coordinatorFactory)
		{
			return new RecordState(this, coordinatorFactory);
		}

		// Token: 0x040008EB RID: 2283
		internal readonly int StateSlotNumber;

		// Token: 0x040008EC RID: 2284
		internal readonly int ColumnCount;

		// Token: 0x040008ED RID: 2285
		internal readonly DataRecordInfo DataRecordInfo;

		// Token: 0x040008EE RID: 2286
		internal readonly Func<Shaper, bool> GatherData;

		// Token: 0x040008EF RID: 2287
		internal readonly ReadOnlyCollection<RecordStateFactory> NestedRecordStateFactories;

		// Token: 0x040008F0 RID: 2288
		internal readonly ReadOnlyCollection<string> ColumnNames;

		// Token: 0x040008F1 RID: 2289
		internal readonly ReadOnlyCollection<TypeUsage> TypeUsages;

		// Token: 0x040008F2 RID: 2290
		internal readonly ReadOnlyCollection<bool> IsColumnNested;

		// Token: 0x040008F3 RID: 2291
		internal readonly bool HasNestedColumns;

		// Token: 0x040008F4 RID: 2292
		internal readonly FieldNameLookup FieldNameLookup;

		// Token: 0x040008F5 RID: 2293
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
		private readonly string Description;
	}
}
