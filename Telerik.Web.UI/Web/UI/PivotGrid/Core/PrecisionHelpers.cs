using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C4F RID: 3151
	internal static class PrecisionHelpers
	{
		// Token: 0x06007713 RID: 30483 RVA: 0x001BA0C4 File Offset: 0x001B82C4
		static PrecisionHelpers()
		{
			PrecisionHelpers.precisionForTypeTable = new Dictionary<Type, Precision>();
			PrecisionHelpers.precisionForTypeTable.Add(typeof(byte), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(sbyte), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(short), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(int), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(long), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(byte?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(sbyte?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(short?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(int?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(long?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(uint), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(uint?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(ushort), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(ushort?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(ulong), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(ulong?), Precision.Int64);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(float), Precision.Double);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(double), Precision.Double);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(float?), Precision.Double);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(double?), Precision.Double);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(decimal), Precision.Decimal);
			PrecisionHelpers.precisionForTypeTable.Add(typeof(decimal?), Precision.Decimal);
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x001BA2B4 File Offset: 0x001B84B4
		public static Precision GetPrecision(Type type)
		{
			if (type == null)
			{
				return Precision.Double;
			}
			Precision result;
			if (PrecisionHelpers.precisionForTypeTable.TryGetValue(type, out result))
			{
				return result;
			}
			return Precision.Unknown;
		}

		// Token: 0x040020B9 RID: 8377
		private static IDictionary<Type, Precision> precisionForTypeTable = new Dictionary<Type, Precision>();
	}
}
