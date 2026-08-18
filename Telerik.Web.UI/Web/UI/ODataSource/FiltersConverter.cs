using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ODataSource
{
	// Token: 0x02000BC2 RID: 3010
	internal class FiltersConverter : JavaScriptConverter
	{
		// Token: 0x06007351 RID: 29521 RVA: 0x001AFC3C File Offset: 0x001ADE3C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			FilterExpression filterExpression = (FilterExpression)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("logic", Enum.GetName(typeof(ODataSourceFilterLogic), filterExpression.LogicOperator).ToLower());
			dictionary.Add("modelID", filterExpression.DataModelID);
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (FilterEntry filterEntry in filterExpression.FilterExpressionEntries)
			{
				list.Add(new Dictionary<string, object>
				{
					{
						"field",
						filterEntry.FieldName
					},
					{
						"operator",
						Enum.GetName(typeof(ODataSourceFilters), filterEntry.Operator).ToLower()
					},
					{
						"value",
						filterEntry.Value
					}
				});
			}
			dictionary.Add("filters", list);
			return dictionary;
		}

		// Token: 0x06007352 RID: 29522 RVA: 0x001AFD44 File Offset: 0x001ADF44
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x06007353 RID: 29523 RVA: 0x001AFD4C File Offset: 0x001ADF4C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(FilterExpression)
				};
			}
		}
	}
}
