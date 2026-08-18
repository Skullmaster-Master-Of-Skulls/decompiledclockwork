using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9D RID: 7069
	public class AggregateFunctionsGroup : Group
	{
		// Token: 0x17005386 RID: 21382
		// (get) Token: 0x060111BB RID: 70075 RVA: 0x003C5ECE File Offset: 0x003C40CE
		// (set) Token: 0x060111BC RID: 70076 RVA: 0x003C5ED6 File Offset: 0x003C40D6
		public object AggregateFunctionsProjection { get; set; }

		// Token: 0x060111BD RID: 70077 RVA: 0x003C5EE0 File Offset: 0x003C40E0
		public AggregateResultCollection GetAggregateResults(IEnumerable<AggregateFunction> functions)
		{
			if (functions == null)
			{
				throw new ArgumentNullException("functions");
			}
			AggregateResultCollection aggregateResultCollection = new AggregateResultCollection();
			if (this.AggregateFunctionsProjection == null)
			{
				return aggregateResultCollection;
			}
			IDictionary<string, object> propertyValues = AggregateFunctionsGroup.ExtractPropertyValues(this.AggregateFunctionsProjection);
			IEnumerable<AggregateResult> items = AggregateFunctionsGroup.CreateAggregateResultsForPropertyValues(functions, propertyValues);
			aggregateResultCollection.AddRange(items);
			return aggregateResultCollection;
		}

		// Token: 0x060111BE RID: 70078 RVA: 0x003C6114 File Offset: 0x003C4314
		private static IEnumerable<AggregateResult> CreateAggregateResultsForPropertyValues(IEnumerable<AggregateFunction> functions, IDictionary<string, object> propertyValues)
		{
			foreach (AggregateFunction function in functions)
			{
				string propertyName = function.FunctionName;
				if (propertyValues.ContainsKey(propertyName))
				{
					object value = propertyValues[propertyName];
					AggregateResult result = new AggregateResult(value, function);
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x060111BF RID: 70079 RVA: 0x003C63B0 File Offset: 0x003C45B0
		private static IDictionary<string, object> ExtractPropertyValues(object obj)
		{
			return (from p in obj.GetType().GetProperties()
			let value = p.GetValue(obj, null)
			select new
			{
				Key = p.Name,
				Value = value
			}).ToDictionary(pair => pair.Key, pair => pair.Value);
		}
	}
}
