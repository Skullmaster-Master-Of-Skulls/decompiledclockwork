using System;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200069F RID: 1695
	internal static class DescriptionIndexMapExtensions
	{
		// Token: 0x06003D3A RID: 15674 RVA: 0x000C5484 File Offset: 0x000C3684
		public static AggregateMapResult MapAggregate(IDescriptionIndexMap map, int index)
		{
			MapResult mapResult = map.Map(FieldRoles.Value, index);
			if (mapResult.Success && mapResult.Role == FieldRoles.Value)
			{
				return new AggregateMapResult
				{
					Success = true,
					Index = mapResult.Level
				};
			}
			return new AggregateMapResult
			{
				Success = false,
				Index = -1
			};
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000C54E8 File Offset: 0x000C36E8
		public static GroupMapResult MapGroup(IDescriptionIndexMap map, PivotAxis axis, int level)
		{
			FieldRoles role = (axis == PivotAxis.Rows) ? FieldRoles.Row : FieldRoles.Column;
			MapResult mapResult = map.Map(role, level);
			if (mapResult.Success)
			{
				switch (mapResult.Role)
				{
				case FieldRoles.Row:
					return new GroupMapResult
					{
						Axis = PivotAxis.Rows,
						Index = mapResult.Level,
						Success = true
					};
				case FieldRoles.Column:
					return new GroupMapResult
					{
						Axis = PivotAxis.Columns,
						Index = mapResult.Level,
						Success = true
					};
				}
			}
			return new GroupMapResult
			{
				Success = false,
				Index = -1,
				Axis = PivotAxis.Rows
			};
		}
	}
}
