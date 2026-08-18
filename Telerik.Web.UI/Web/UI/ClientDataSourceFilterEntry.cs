using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000118 RID: 280
	public class ClientDataSourceFilterEntry : ClientDataSourceFilterBase
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00027C82 File Offset: 0x00025E82
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00027CA2 File Offset: 0x00025EA2
		[Description("Gets or sets the field name for the operation.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string FieldName
		{
			get
			{
				return (base.ViewState["FieldName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["FieldName"] = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00027CB5 File Offset: 0x00025EB5
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00027CE0 File Offset: 0x00025EE0
		[Description("Gets or sets the filtering operator.")]
		[Category("Behavior")]
		[DefaultValue(ClientDataSourceFilterOperator.EqualTo)]
		public ClientDataSourceFilterOperator Operator
		{
			get
			{
				if (base.ViewState["Operator"] != null)
				{
					return (ClientDataSourceFilterOperator)base.ViewState["Operator"];
				}
				return ClientDataSourceFilterOperator.EqualTo;
			}
			set
			{
				base.ViewState["Operator"] = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00027CF8 File Offset: 0x00025EF8
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00027D18 File Offset: 0x00025F18
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the value for the filtering operation.")]
		public string Value
		{
			get
			{
				return (base.ViewState["Value"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00027D2C File Offset: 0x00025F2C
		public RadListViewFilterExpression ToListViewExpression(ClientDataSourceFilterEntry filterEntry, ClientDataSourceModelFieldType fieldType)
		{
			RadListViewFilterExpression result = null;
			ClientDataSourceFilterOperator @operator = filterEntry.Operator;
			string fieldName = filterEntry.FieldName;
			switch (@operator)
			{
			case ClientDataSourceFilterOperator.EqualTo:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewEqualToFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewEqualToFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewEqualToFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Boolean:
					result = new RadListViewEqualToFilterExpression<bool>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToBoolean(filterEntry.Value)
					};
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewEqualToFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.NotEqualTo:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewNotEqualToFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewNotEqualToFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewNotEqualToFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Boolean:
					result = new RadListViewNotEqualToFilterExpression<bool>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToBoolean(filterEntry.Value)
					};
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewNotEqualToFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.GreaterThan:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewGreaterThanFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewGreaterThanFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewGreaterThanFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewGreaterThanFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.GreaterThanOrEqualTo:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewGreaterThenOrEqualToFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewGreaterThenOrEqualToFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewGreaterThenOrEqualToFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewGreaterThenOrEqualToFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.LessThan:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewLessThanFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewLessThanFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewLessThanFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewLessThanFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.LessThanOrEqualTo:
				switch (fieldType)
				{
				case ClientDataSourceModelFieldType.String:
					result = new RadListViewLessThanOrEqualToFilterExpression<string>
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
					break;
				case ClientDataSourceModelFieldType.Number:
					if (Convert.ToDecimal(filterEntry.Value) % 1m == 0m)
					{
						result = new RadListViewLessThanOrEqualToFilterExpression<int>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToInt32(filterEntry.Value)
						};
					}
					else
					{
						result = new RadListViewLessThanOrEqualToFilterExpression<double>
						{
							FieldName = fieldName,
							CurrentValue = Convert.ToDouble(filterEntry.Value)
						};
					}
					break;
				case ClientDataSourceModelFieldType.Date:
					result = new RadListViewLessThanOrEqualToFilterExpression<DateTime>
					{
						FieldName = fieldName,
						CurrentValue = Convert.ToDateTime(filterEntry.Value)
					};
					break;
				}
				break;
			case ClientDataSourceFilterOperator.Contains:
				if (fieldType == ClientDataSourceModelFieldType.String)
				{
					result = new RadListViewContainsFilterExpression
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
				}
				break;
			case ClientDataSourceFilterOperator.EndsWith:
				if (fieldType == ClientDataSourceModelFieldType.String)
				{
					result = new RadListViewEndsWithFilterExpression
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
				}
				break;
			case ClientDataSourceFilterOperator.StartsWith:
				if (fieldType == ClientDataSourceModelFieldType.String)
				{
					result = new RadListViewStartsWithFilterExpression
					{
						FieldName = fieldName,
						CurrentValue = filterEntry.Value
					};
				}
				break;
			}
			return result;
		}
	}
}
