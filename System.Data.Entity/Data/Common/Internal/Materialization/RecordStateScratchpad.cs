using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CF RID: 975
	internal class RecordStateScratchpad
	{
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x000CAEB7 File Offset: 0x000C90B7
		// (set) Token: 0x06003491 RID: 13457 RVA: 0x000CAEBF File Offset: 0x000C90BF
		internal int StateSlotNumber
		{
			get
			{
				return this._stateSlotNumber;
			}
			set
			{
				this._stateSlotNumber = value;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000CAEC8 File Offset: 0x000C90C8
		// (set) Token: 0x06003493 RID: 13459 RVA: 0x000CAED0 File Offset: 0x000C90D0
		internal int ColumnCount
		{
			get
			{
				return this._columnCount;
			}
			set
			{
				this._columnCount = value;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000CAED9 File Offset: 0x000C90D9
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x000CAEE1 File Offset: 0x000C90E1
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				return this._dataRecordInfo;
			}
			set
			{
				this._dataRecordInfo = value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000CAEEA File Offset: 0x000C90EA
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x000CAEF2 File Offset: 0x000C90F2
		internal Expression GatherData
		{
			get
			{
				return this._gatherData;
			}
			set
			{
				this._gatherData = value;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000CAEFB File Offset: 0x000C90FB
		// (set) Token: 0x06003499 RID: 13465 RVA: 0x000CAF03 File Offset: 0x000C9103
		internal string[] PropertyNames
		{
			get
			{
				return this._propertyNames;
			}
			set
			{
				this._propertyNames = value;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000CAF0C File Offset: 0x000C910C
		// (set) Token: 0x0600349B RID: 13467 RVA: 0x000CAF14 File Offset: 0x000C9114
		internal TypeUsage[] TypeUsages
		{
			get
			{
				return this._typeUsages;
			}
			set
			{
				this._typeUsages = value;
			}
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000CAF20 File Offset: 0x000C9120
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal RecordStateFactory Compile()
		{
			RecordStateFactory[] array = new RecordStateFactory[this._nestedRecordStateScratchpads.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._nestedRecordStateScratchpads[i].Compile();
			}
			return (RecordStateFactory)Activator.CreateInstance(typeof(RecordStateFactory), new object[]
			{
				this.StateSlotNumber,
				this.ColumnCount,
				array,
				this.DataRecordInfo,
				this.GatherData,
				this.PropertyNames,
				this.TypeUsages
			});
		}

		// Token: 0x04001709 RID: 5897
		private int _stateSlotNumber;

		// Token: 0x0400170A RID: 5898
		private int _columnCount;

		// Token: 0x0400170B RID: 5899
		private DataRecordInfo _dataRecordInfo;

		// Token: 0x0400170C RID: 5900
		private Expression _gatherData;

		// Token: 0x0400170D RID: 5901
		private string[] _propertyNames;

		// Token: 0x0400170E RID: 5902
		private TypeUsage[] _typeUsages;

		// Token: 0x0400170F RID: 5903
		private List<RecordStateScratchpad> _nestedRecordStateScratchpads = new List<RecordStateScratchpad>();
	}
}
