using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ODataSource
{
	// Token: 0x02000BC1 RID: 3009
	internal class SortsConverter : JavaScriptConverter
	{
		// Token: 0x0600734D RID: 29517 RVA: 0x001AFB3C File Offset: 0x001ADD3C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SortExpression sortExpression = (SortExpression)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("modelID", sortExpression.DataModelID);
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (SortEntry sortEntry in sortExpression.SortExpressionEntries)
			{
				list.Add(new Dictionary<string, object>
				{
					{
						"field",
						sortEntry.FieldName
					},
					{
						"dir",
						Enum.GetName(typeof(ODataSourceOrder), sortEntry.Order).ToLower()
					}
				});
			}
			dictionary.Add("sorts", list);
			return dictionary;
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x001AFC08 File Offset: 0x001ADE08
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x0600734F RID: 29519 RVA: 0x001AFC10 File Offset: 0x001ADE10
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SortExpression)
				};
			}
		}
	}
}
