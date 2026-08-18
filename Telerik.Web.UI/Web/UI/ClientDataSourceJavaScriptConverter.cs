using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.DataSourceSettings;

namespace Telerik.Web.UI
{
	// Token: 0x020000FD RID: 253
	public class ClientDataSourceJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x00025A84 File Offset: 0x00023C84
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DataSourceControlSettings dataSourceControlSettings = obj as DataSourceControlSettings;
			if (dataSourceControlSettings != null)
			{
				if (dataSourceControlSettings.AllowAutomaticDeletes)
				{
					dictionary.Add("allowAutomaticDeletes", true);
				}
				if (dataSourceControlSettings.AllowAutomaticInserts)
				{
					dictionary.Add("allowAutomaticInserts", true);
				}
				if (dataSourceControlSettings.AllowAutomaticUpdates)
				{
					dictionary.Add("allowAutomaticUpdates", true);
				}
				if (dataSourceControlSettings.ShouldSerializeDataFields)
				{
					dictionary.Add("dataFields", dataSourceControlSettings.DataFields);
				}
				if (dataSourceControlSettings.ShouldSerializeDataKeyNames)
				{
					dictionary.Add("dataKeyNames", dataSourceControlSettings.DataKeyNames);
				}
				if (dataSourceControlSettings.ShouldSerializeDataMember)
				{
					dictionary.Add("dataMember", dataSourceControlSettings.DataMember);
				}
				if (dataSourceControlSettings.ShouldSerializeDataModelID)
				{
					dictionary.Add("dataModelID", dataSourceControlSettings.DataModelID);
				}
				if (dataSourceControlSettings.ShouldSerializeDataSourceID)
				{
					dictionary.Add("dataSourceID", dataSourceControlSettings.DataSourceID);
				}
				if (dataSourceControlSettings.ShouldSerializeDeleteMethod)
				{
					dictionary.Add("deleteMethod", dataSourceControlSettings.DeleteMethod);
				}
				if (dataSourceControlSettings.ShouldSerializeInsertMethod)
				{
					dictionary.Add("insertMethod", dataSourceControlSettings.InsertMethod);
				}
				if (dataSourceControlSettings.ShouldSerializeSelectMethod)
				{
					dictionary.Add("selectMethod", dataSourceControlSettings.SelectMethod);
				}
				if (dataSourceControlSettings.ShouldSerializeUpdateMethod)
				{
					dictionary.Add("updateMethod", dataSourceControlSettings.UpdateMethod);
				}
			}
			WebServiceDataSourceSettings webServiceDataSourceSettings = obj as WebServiceDataSourceSettings;
			if (webServiceDataSourceSettings != null)
			{
				if (webServiceDataSourceSettings.ServiceType != ClientDataSourceServiceType.Default)
				{
					dictionary.Add("serviceType", webServiceDataSourceSettings.ServiceType.ToString().ToLower());
				}
				if (!string.IsNullOrEmpty(webServiceDataSourceSettings.BaseUrl))
				{
					dictionary.Add("baseUrl", webServiceDataSourceSettings.BaseUrl);
				}
				if (!string.IsNullOrEmpty(webServiceDataSourceSettings.Insert.Url))
				{
					dictionary.Add("create", webServiceDataSourceSettings.Insert);
				}
				if (!string.IsNullOrEmpty(webServiceDataSourceSettings.Update.Url))
				{
					dictionary.Add("update", webServiceDataSourceSettings.Update);
				}
				if (!string.IsNullOrEmpty(webServiceDataSourceSettings.Select.Url))
				{
					dictionary.Add("read", webServiceDataSourceSettings.Select);
				}
				if (!string.IsNullOrEmpty(webServiceDataSourceSettings.Delete.Url))
				{
					dictionary.Add("destroy", webServiceDataSourceSettings.Delete);
				}
			}
			WebServiceBaseSettings webServiceBaseSettings = obj as WebServiceBaseSettings;
			if (webServiceBaseSettings != null)
			{
				if (webServiceBaseSettings.ShouldSerializeContentType)
				{
					dictionary.Add("contentType", webServiceBaseSettings.ContentType);
				}
				if (webServiceBaseSettings.ShouldSerializeDataType)
				{
					dictionary.Add("dataType", webServiceBaseSettings.DataType.ToString());
				}
				if (webServiceBaseSettings.EnableCaching)
				{
					dictionary.Add("cache", webServiceBaseSettings.EnableCaching);
				}
				if (webServiceBaseSettings.ShouldSerializeUrl)
				{
					dictionary.Add("url", webServiceBaseSettings.Url);
				}
				if (webServiceBaseSettings.ShouldSerializeHttpMethod)
				{
					dictionary.Add("type", webServiceBaseSettings.RequestType.ToString().ToUpper());
				}
			}
			ClientDataSourceSchema clientDataSourceSchema = obj as ClientDataSourceSchema;
			if (clientDataSourceSchema != null)
			{
				if (clientDataSourceSchema.ShouldSerializeDataName)
				{
					dictionary.Add("data", clientDataSourceSchema.DataName);
				}
				if (clientDataSourceSchema.ShouldSerializeAggregateResultsName)
				{
					dictionary.Add("aggregates", clientDataSourceSchema.AggregateResultsName);
				}
				if (clientDataSourceSchema.ShouldSerializeErrorsName)
				{
					dictionary.Add("errors", clientDataSourceSchema.ErrorsName);
				}
				if (clientDataSourceSchema.ShouldSerializeGroupsName)
				{
					dictionary.Add("groups", clientDataSourceSchema.GroupsName);
				}
				if (clientDataSourceSchema.ShouldSerializeTotalName)
				{
					dictionary.Add("total", clientDataSourceSchema.TotalName);
				}
				if (clientDataSourceSchema.ShouldSerializeResponseType)
				{
					dictionary.Add("type", clientDataSourceSchema.ResponseType.ToString());
				}
				if (clientDataSourceSchema.Model != null)
				{
					dictionary.Add("model", clientDataSourceSchema.Model);
				}
			}
			ClientDataSourceModel clientDataSourceModel = obj as ClientDataSourceModel;
			if (clientDataSourceModel != null)
			{
				if (clientDataSourceModel.ShouldSerializeID)
				{
					dictionary.Add("id", clientDataSourceModel.ID);
				}
				if (clientDataSourceModel.ShouldSerializeFields)
				{
					dictionary.Add("fields", clientDataSourceModel.Fields);
				}
			}
			ClientDataSourceModelField clientDataSourceModelField = obj as ClientDataSourceModelField;
			if (clientDataSourceModelField != null && clientDataSourceModelField.ShouldSerializeFieldName)
			{
				dictionary.Add("fieldName", clientDataSourceModelField.FieldName);
				if (clientDataSourceModelField.ShouldSerializeOriginalFieldName)
				{
					dictionary.Add("from", clientDataSourceModelField.OriginalFieldName);
				}
				if (!clientDataSourceModelField.IgnoreCase)
				{
					dictionary.Add("ignoreCase", false);
				}
				if (clientDataSourceModelField.ShouldSerializeParseFunctionName)
				{
					dictionary.Add("parse", clientDataSourceModelField.ParseFunctionName);
				}
				if (clientDataSourceModelField.ShouldSerializeDataType)
				{
					dictionary.Add("type", clientDataSourceModelField.DataType.ToString());
				}
				if (clientDataSourceModelField.ShouldSerializeDefaultValue)
				{
					dictionary.Add("defaultValue", clientDataSourceModelField.DefaultValue);
				}
				if (!clientDataSourceModelField.Editable)
				{
					dictionary.Add("editable", clientDataSourceModelField.Editable);
				}
				if (!clientDataSourceModelField.Nullable)
				{
					dictionary.Add("nullable", clientDataSourceModelField.Nullable);
				}
			}
			ClientDataSourceFilterExpression clientDataSourceFilterExpression = obj as ClientDataSourceFilterExpression;
			if (clientDataSourceFilterExpression != null)
			{
				if (clientDataSourceFilterExpression.ShouldSerializeFilters)
				{
					ClientDataSourceFilterBaseCollection clientDataSourceFilterBaseCollection = new ClientDataSourceFilterBaseCollection();
					foreach (object obj2 in clientDataSourceFilterExpression.Filters)
					{
						ClientDataSourceFilterExpression clientDataSourceFilterExpression2 = obj2 as ClientDataSourceFilterExpression;
						if (clientDataSourceFilterExpression2 != null)
						{
							if (clientDataSourceFilterExpression2.Filters.Count > 0)
							{
								clientDataSourceFilterBaseCollection.Add(clientDataSourceFilterExpression2);
							}
						}
						else
						{
							ClientDataSourceFilterEntry item = obj2 as ClientDataSourceFilterEntry;
							clientDataSourceFilterBaseCollection.Add(item);
						}
					}
					dictionary.Add("filters", clientDataSourceFilterBaseCollection);
				}
				if (clientDataSourceFilterExpression.ShouldSerializeLogicOperator)
				{
					dictionary.Add("logic", clientDataSourceFilterExpression.LogicOperator.ToString().ToLower());
				}
			}
			ClientDataSourceFilterEntry clientDataSourceFilterEntry = obj as ClientDataSourceFilterEntry;
			if (clientDataSourceFilterEntry != null && !string.IsNullOrEmpty(clientDataSourceFilterEntry.FieldName))
			{
				dictionary.Add("field", clientDataSourceFilterEntry.FieldName);
				dictionary.Add("operator", clientDataSourceFilterEntry.Operator);
				dictionary.Add("value", clientDataSourceFilterEntry.Value);
			}
			ClientDataSourceSortExpression clientDataSourceSortExpression = obj as ClientDataSourceSortExpression;
			if (clientDataSourceSortExpression != null && !string.IsNullOrEmpty(clientDataSourceSortExpression.FieldName))
			{
				dictionary.Add("field", clientDataSourceSortExpression.FieldName);
				dictionary.Add("dir", clientDataSourceSortExpression.SortOrder.ToString().ToLower());
			}
			ClientDataSourceGroupExpression clientDataSourceGroupExpression = obj as ClientDataSourceGroupExpression;
			if (clientDataSourceGroupExpression != null && !string.IsNullOrEmpty(clientDataSourceGroupExpression.FieldName))
			{
				dictionary.Add("field", clientDataSourceGroupExpression.FieldName);
				dictionary.Add("dir", clientDataSourceGroupExpression.SortOrder.ToString().ToLower());
				dictionary.Add("aggregates", clientDataSourceGroupExpression.Aggregates);
			}
			ClientDataSourceAggregate clientDataSourceAggregate = obj as ClientDataSourceAggregate;
			if (clientDataSourceAggregate != null)
			{
				dictionary.Add("field", clientDataSourceAggregate.Field);
				dictionary.Add("aggregate", clientDataSourceAggregate.Aggregate.ToString().ToLower());
			}
			return dictionary;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002618C File Offset: 0x0002438C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			ClientDataSourceContext clientDataSourceContext = new ClientDataSourceContext();
			if (dictionary.ContainsKey("filter"))
			{
				ClientDataSourceFilterExpression clientDataSourceFilterExpression = new ClientDataSourceFilterExpression();
				Dictionary<string, object> dictionary2 = dictionary["filter"] as Dictionary<string, object>;
				ArrayList arrayList = dictionary2["_array"] as ArrayList;
				foreach (object obj in arrayList)
				{
					ClientDataSourceFilterExpression clientDataSourceFilterExpression2 = new ClientDataSourceFilterExpression();
					clientDataSourceFilterExpression2.LogicOperator = (ClientDataSourceFilterLogicOperator)(obj as Dictionary<string, object>)["_logicOperator"];
					ArrayList arrayList2 = (obj as Dictionary<string, object>)["_array"] as ArrayList;
					foreach (object obj2 in arrayList2)
					{
						Dictionary<string, object> dictionary3 = obj2 as Dictionary<string, object>;
						if (dictionary3.ContainsKey("_value"))
						{
							ClientDataSourceFilterEntry clientDataSourceFilterEntry = new ClientDataSourceFilterEntry();
							clientDataSourceFilterEntry.FieldName = dictionary3["_fieldName"].ToString();
							clientDataSourceFilterEntry.Operator = (ClientDataSourceFilterOperator)dictionary3["_operator"];
							clientDataSourceFilterEntry.Value = dictionary3["_value"].ToString();
							clientDataSourceFilterExpression2.Filters.Add(clientDataSourceFilterEntry);
						}
					}
					clientDataSourceFilterExpression.Filters.Add(clientDataSourceFilterExpression2);
				}
				clientDataSourceContext.FilterExpression = clientDataSourceFilterExpression;
			}
			if (dictionary.ContainsKey("commandName"))
			{
				clientDataSourceContext.CommandName = dictionary["commandName"].ToString();
			}
			if (dictionary.ContainsKey("commandArguments"))
			{
				Dictionary<string, object> dictionary4 = dictionary["commandArguments"] as Dictionary<string, object>;
				if (dictionary4.ContainsKey("keys"))
				{
					Dictionary<string, object> d = dictionary4["keys"] as Dictionary<string, object>;
					clientDataSourceContext.IDKeys = new Hashtable(d);
				}
				if (dictionary4.ContainsKey("oldValues"))
				{
					Dictionary<string, object> d2 = dictionary4["oldValues"] as Dictionary<string, object>;
					clientDataSourceContext.OldValues = new Hashtable(d2);
				}
				if (dictionary4.ContainsKey("newValues"))
				{
					Dictionary<string, object> d3 = dictionary4["newValues"] as Dictionary<string, object>;
					clientDataSourceContext.NewValues = new Hashtable(d3);
				}
			}
			if (dictionary.ContainsKey("sort"))
			{
				ClientDataSourceSortExpressionCollection clientDataSourceSortExpressionCollection = new ClientDataSourceSortExpressionCollection();
				Dictionary<string, object> dictionary5 = dictionary["sort"] as Dictionary<string, object>;
				ArrayList arrayList3 = dictionary5["_array"] as ArrayList;
				foreach (object obj3 in arrayList3)
				{
					Dictionary<string, object> dictionary6 = obj3 as Dictionary<string, object>;
					clientDataSourceSortExpressionCollection.Add(new ClientDataSourceSortExpression
					{
						FieldName = dictionary6["_fieldName"].ToString(),
						SortOrder = (ClientDataSourceSortOrder)dictionary6["_sortOrder"]
					});
				}
				clientDataSourceContext.SortExpressions = clientDataSourceSortExpressionCollection;
			}
			if (dictionary.ContainsKey("pageSize"))
			{
				clientDataSourceContext.PageSize = (int)dictionary["pageSize"];
			}
			if (dictionary.ContainsKey("pageIndex"))
			{
				clientDataSourceContext.CurrentPageIndex = (int)dictionary["pageIndex"];
			}
			return clientDataSourceContext;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00026790 File Offset: 0x00024990
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ClientDataSourceContext);
				yield return typeof(DataSourceControlSettings);
				yield return typeof(ClientDataSourceAggregate);
				yield return typeof(WebServiceDataSourceSettings);
				yield return typeof(WebServiceBaseSettings);
				yield return typeof(ClientDataSourceSchema);
				yield return typeof(ClientDataSourceModel);
				yield return typeof(ClientDataSourceModelField);
				yield return typeof(ClientDataSourceSortExpression);
				yield return typeof(ClientDataSourceFilterExpression);
				yield return typeof(ClientDataSourceFilterEntry);
				yield return typeof(ClientDataSourceGroupExpression);
				yield break;
			}
		}
	}
}
