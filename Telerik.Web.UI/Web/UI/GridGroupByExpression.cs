using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200119E RID: 4510
	public class GridGroupByExpression : IStateManager
	{
		// Token: 0x17003BDB RID: 15323
		// (get) Token: 0x0600B936 RID: 47414 RVA: 0x0028F839 File Offset: 0x0028DA39
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridGroupByFieldList SelectFields
		{
			get
			{
				return this._selectFields;
			}
		}

		// Token: 0x17003BDC RID: 15324
		// (get) Token: 0x0600B937 RID: 47415 RVA: 0x0028F841 File Offset: 0x0028DA41
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridGroupByFieldList GroupByFields
		{
			get
			{
				return this._groupByFields;
			}
		}

		// Token: 0x0600B938 RID: 47416 RVA: 0x0028F849 File Offset: 0x0028DA49
		public GridGroupByExpression()
		{
			this.InitializeSelectFieldsValidation();
		}

		// Token: 0x0600B939 RID: 47417 RVA: 0x0028F86D File Offset: 0x0028DA6D
		public GridGroupByExpression(string expression)
		{
			this.InitializeSelectFieldsValidation();
			this.SetExpression(expression);
		}

		// Token: 0x0600B93A RID: 47418 RVA: 0x0028F898 File Offset: 0x0028DA98
		private void InitializeSelectFieldsValidation()
		{
			this._selectFields.ValidateField += this._selectFields_ValidateField;
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x0028F8B4 File Offset: 0x0028DAB4
		private void _selectFields_ValidateField(object sender, ValidateFieldEventArgs e)
		{
			GridGroupByField newField = e.NewField;
			string sortOrderAsString = newField.GetSortOrderAsString();
			if (!string.IsNullOrEmpty(sortOrderAsString))
			{
				throw new GridGroupByException("Setting SortOrder is not supported for the 'Select' fields in a 'Group By' expression.");
			}
		}

		// Token: 0x0600B93C RID: 47420 RVA: 0x0028F8E4 File Offset: 0x0028DAE4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public GridGroupByExpression(GridColumn column)
		{
			if (!column.Groupable)
			{
				throw new GridGroupByException("Cannot group by this column!");
			}
			string text = column.GroupByExpression;
			if (string.IsNullOrEmpty(text))
			{
				text = column.GetDefaultGroupByExpression();
			}
			this.Expression = text;
			foreach (object obj in this.SelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				if (column.IsBoundToFieldName(gridGroupByField.FieldName))
				{
					string text2 = null;
					if (column is GridBoundColumn)
					{
						text2 = (column as GridBoundColumn).DataFormatString;
					}
					else if (column is GridButtonColumn)
					{
						text2 = (column as GridButtonColumn).DataTextFormatString;
					}
					else if (column is GridDropDownColumn)
					{
						text2 = (column as GridDropDownColumn).ListTextFormatString;
					}
					else if (column is GridHyperLinkColumn)
					{
						text2 = (column as GridHyperLinkColumn).DataTextFormatString;
					}
					else if (column is GridImageColumn)
					{
						text2 = (column as GridImageColumn).DataAlternateTextFormatString;
					}
					else if (column is GridBinaryImageColumn)
					{
						text2 = (column as GridBinaryImageColumn).DataAlternateTextFormatString;
					}
					else if (column is GridAttachmentColumn)
					{
						GridAttachmentColumn gridAttachmentColumn = (GridAttachmentColumn)column;
						if (gridAttachmentColumn.IsBoundToFieldName(gridAttachmentColumn.DataTextField))
						{
							text2 = gridAttachmentColumn.DataTextFormatString;
						}
						else if (gridAttachmentColumn.IsBoundToFieldName(gridAttachmentColumn.FileNameTextField))
						{
							text2 = gridAttachmentColumn.FileNameTextFormatString;
						}
					}
					if (string.IsNullOrEmpty(text2) || text2.IndexOf("{0") < 0)
					{
						text2 = "{0}";
					}
					if (gridGroupByField.FieldName == gridGroupByField.FieldAlias && !string.IsNullOrEmpty(column.HeaderText))
					{
						gridGroupByField.HeaderText = column.HeaderText;
					}
					gridGroupByField.FormatString = text2;
				}
			}
			foreach (object obj2 in this.GroupByFields)
			{
				GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
				if (column.IsBoundToFieldName(gridGroupByField2.FieldName))
				{
					string text3 = null;
					if (column is GridBoundColumn)
					{
						text3 = (column as GridBoundColumn).DataFormatString;
					}
					else if (column is GridButtonColumn)
					{
						text3 = (column as GridButtonColumn).DataTextFormatString;
					}
					else if (column is GridDropDownColumn)
					{
						text3 = (column as GridDropDownColumn).ListTextFormatString;
					}
					else if (column is GridHyperLinkColumn)
					{
						text3 = (column as GridHyperLinkColumn).DataTextFormatString;
					}
					else if (column is GridImageColumn)
					{
						text3 = (column as GridImageColumn).DataAlternateTextFormatString;
					}
					else if (column is GridBinaryImageColumn)
					{
						text3 = (column as GridBinaryImageColumn).DataAlternateTextFormatString;
					}
					else if (column is GridAttachmentColumn)
					{
						GridAttachmentColumn gridAttachmentColumn2 = (GridAttachmentColumn)column;
						if (!string.IsNullOrEmpty(gridAttachmentColumn2.DataTextField))
						{
							text3 = gridAttachmentColumn2.DataTextFormatString;
						}
						else if (!string.IsNullOrEmpty(gridAttachmentColumn2.FileNameTextField))
						{
							text3 = gridAttachmentColumn2.FileNameTextFormatString;
						}
					}
					if (string.IsNullOrEmpty(text3) || text3.IndexOf("{0") < 0)
					{
						text3 = "{0}";
					}
					if (gridGroupByField2.FieldName == gridGroupByField2.FieldAlias && !string.IsNullOrEmpty(column.HeaderText))
					{
						gridGroupByField2.HeaderText = column.HeaderText;
					}
					gridGroupByField2.FormatString = text3;
				}
			}
		}

		// Token: 0x0600B93D RID: 47421 RVA: 0x0028FC5C File Offset: 0x0028DE5C
		private void SetExpression(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				throw new GridGroupByException("Expression cannot be null or empty");
			}
			this._expressionAsString = expression;
			this.ParseFieldLists();
		}

		// Token: 0x17003BDD RID: 15325
		// (get) Token: 0x0600B93E RID: 47422 RVA: 0x0028FC7E File Offset: 0x0028DE7E
		// (set) Token: 0x0600B93F RID: 47423 RVA: 0x0028FC8C File Offset: 0x0028DE8C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("Group By")]
		[NotifyParentProperty(true)]
		public string Expression
		{
			get
			{
				this.BuildStringExpression();
				return this._expressionAsString;
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x0600B940 RID: 47424 RVA: 0x0028FC95 File Offset: 0x0028DE95
		public static GridGroupByExpression Parse(string expression)
		{
			return new GridGroupByExpression(expression);
		}

		// Token: 0x0600B941 RID: 47425 RVA: 0x0028FCA0 File Offset: 0x0028DEA0
		private void BuildStringExpression()
		{
			string text = string.Empty;
			foreach (object obj in this.SelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				text += gridGroupByField.ToString();
				text += ", ";
			}
			text = text.TrimEnd(new char[]
			{
				',',
				' '
			});
			text += " Group By ";
			foreach (object obj2 in this.GroupByFields)
			{
				GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
				text += gridGroupByField2.ToString();
				text += ", ";
			}
			text = text.TrimEnd(new char[]
			{
				',',
				' '
			});
			this._expressionAsString = text;
		}

		// Token: 0x0600B942 RID: 47426 RVA: 0x0028FDC4 File Offset: 0x0028DFC4
		public override string ToString()
		{
			return this.Expression;
		}

		// Token: 0x17003BDE RID: 15326
		// (get) Token: 0x0600B943 RID: 47427 RVA: 0x0028FDCC File Offset: 0x0028DFCC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x0600B944 RID: 47428 RVA: 0x0028FDD4 File Offset: 0x0028DFD4
		object IStateManager.SaveViewState()
		{
			object obj = null;
			object obj2 = null;
			object obj3 = null;
			if (this.SelectFields.Count > 0 && this.GroupByFields.Count > 0)
			{
				obj = this.Expression;
				Hashtable hashtable = new Hashtable();
				foreach (object obj4 in this.SelectFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj4;
					hashtable[gridGroupByField.FieldAlias] = gridGroupByField.FormatString;
					hashtable[gridGroupByField.FieldAlias + "HF"] = gridGroupByField.HeaderText;
					hashtable[gridGroupByField.FieldAlias + "SEP"] = gridGroupByField.HeaderValueSeparator;
				}
				obj2 = hashtable;
				Hashtable hashtable2 = new Hashtable();
				foreach (object obj5 in this.GroupByFields)
				{
					GridGroupByField gridGroupByField2 = (GridGroupByField)obj5;
					hashtable2[gridGroupByField2.FieldAlias] = gridGroupByField2.FormatString;
					hashtable2[gridGroupByField2.FieldAlias + "HF"] = gridGroupByField2.HeaderText;
					hashtable2[gridGroupByField2.FieldAlias + "SEP"] = gridGroupByField2.HeaderValueSeparator;
				}
				obj3 = hashtable2;
			}
			return new object[]
			{
				obj,
				obj2,
				obj3
			};
		}

		// Token: 0x0600B945 RID: 47429 RVA: 0x0028FF78 File Offset: 0x0028E178
		void IStateManager.LoadViewState(object SavedState)
		{
			object[] array = (object[])SavedState;
			if (array[0] != null)
			{
				this.Expression = (string)array[0];
			}
			if (array[1] != null)
			{
				Hashtable hashtable = (Hashtable)array[1];
				foreach (object obj in this.SelectFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					gridGroupByField.FormatString = (string)hashtable[gridGroupByField.FieldAlias];
					gridGroupByField.HeaderText = (string)hashtable[gridGroupByField.FieldAlias + "HF"];
					gridGroupByField.HeaderValueSeparator = (string)hashtable[gridGroupByField.FieldAlias + "SEP"];
				}
			}
			if (array[2] != null)
			{
				Hashtable hashtable2 = (Hashtable)array[2];
				foreach (object obj2 in this.GroupByFields)
				{
					GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
					gridGroupByField2.FormatString = (string)hashtable2[gridGroupByField2.FieldAlias];
					gridGroupByField2.HeaderText = (string)hashtable2[gridGroupByField2.FieldAlias + "HF"];
					gridGroupByField2.HeaderValueSeparator = (string)hashtable2[gridGroupByField2.FieldAlias + "SEP"];
				}
			}
		}

		// Token: 0x0600B946 RID: 47430 RVA: 0x00290110 File Offset: 0x0028E310
		void IStateManager.TrackViewState()
		{
		}

		// Token: 0x17003BDF RID: 15327
		// (get) Token: 0x0600B947 RID: 47431 RVA: 0x00290112 File Offset: 0x0028E312
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B948 RID: 47432 RVA: 0x00290118 File Offset: 0x0028E318
		private string[] SplitbyGroupBy()
		{
			string text = this._expressionAsString.Trim();
			int num = text.ToLower().IndexOf("Group By".ToLower());
			if (num < 0)
			{
				throw new GridGroupByException("Invalid group by expression: 'Group By' clause missing");
			}
			string[] array = new string[2];
			if (num > 0)
			{
				array[0] = text.Substring(0, num - 1);
			}
			else
			{
				array[0] = string.Empty;
			}
			int num2 = num + "Group By".Length;
			array[1] = text.Substring(num2, text.Length - num2);
			return array;
		}

		// Token: 0x0600B949 RID: 47433 RVA: 0x00290198 File Offset: 0x0028E398
		private string GetGroupByFieldExpression()
		{
			string[] array = this.SplitbyGroupBy();
			return array[1];
		}

		// Token: 0x0600B94A RID: 47434 RVA: 0x002901B0 File Offset: 0x0028E3B0
		private string GetFieldList()
		{
			string[] array = this.SplitbyGroupBy();
			return array[0];
		}

		// Token: 0x0600B94B RID: 47435 RVA: 0x002901C7 File Offset: 0x0028E3C7
		internal void SetIndex(int value)
		{
			this._index = value;
		}

		// Token: 0x0600B94C RID: 47436 RVA: 0x002901D0 File Offset: 0x0028E3D0
		private void ParseFieldLists()
		{
			string groupByFieldExpression = this.GetGroupByFieldExpression();
			string fieldList = this.GetFieldList();
			this.ParseGroupByFieldList(fieldList);
			this.ParseFieldList(groupByFieldExpression, false);
			if (this.GroupByFields.Count == 0)
			{
				throw new GridGroupByException("There should be at least one column specified to group by");
			}
		}

		// Token: 0x0600B94D RID: 47437 RVA: 0x00290214 File Offset: 0x0028E414
		private string[] Tokenize(string expression)
		{
			return expression.Trim().Split(new char[]
			{
				' '
			});
		}

		// Token: 0x0600B94E RID: 47438 RVA: 0x0029023C File Offset: 0x0028E43C
		private void ParseGroupByFieldList(string FieldList)
		{
			this.SelectFields.Clear();
			if (string.IsNullOrEmpty(FieldList))
			{
				return;
			}
			string[] array = FieldList.Split(new char[]
			{
				','
			});
			for (int i = 0; i <= array.Length - 1; i++)
			{
				GridGroupByField gridGroupByField = new GridGroupByField();
				if (array[i].IndexOf("[") > 0 && array[i].IndexOf("]") > 0)
				{
					string text = array[i].Substring(array[i].IndexOf("["), array[i].IndexOf("]") - array[i].IndexOf("[") + 1);
					array[i] = array[i].Replace(text, "");
					gridGroupByField.HeaderText = text.Replace("[", "").Replace("]", "");
				}
				string[] array2 = this.Tokenize(array[i]);
				switch (array2.Length)
				{
				case 1:
					break;
				case 2:
					gridGroupByField.FieldAlias = array2[1];
					break;
				default:
					throw new ArgumentException("Too many spaces in field definition: '" + array[i] + "'.");
				}
				array2 = array2[0].Split(new char[]
				{
					'('
				});
				switch (array2.Length)
				{
				case 1:
					gridGroupByField.FieldName = array2[0];
					break;
				case 2:
					gridGroupByField.SetAggregate(array2[0].Trim());
					gridGroupByField.FieldName = array2[1].Trim(new char[]
					{
						' ',
						')'
					});
					break;
				default:
					throw new ArgumentException("Invalid field definition: '" + array[i] + "'.");
				}
				if (gridGroupByField.FieldAlias == null)
				{
					if (gridGroupByField.Aggregate == GridAggregateFunction.None)
					{
						gridGroupByField.FieldAlias = gridGroupByField.FieldName;
					}
					else
					{
						gridGroupByField.FieldAlias = gridGroupByField.Aggregate.ToString() + " of " + gridGroupByField.FieldName;
					}
				}
				this._selectFields.Add(gridGroupByField);
			}
		}

		// Token: 0x0600B94F RID: 47439 RVA: 0x0029043C File Offset: 0x0028E63C
		private void ParseFieldList(string FieldList, bool AllowRelation)
		{
			this.GroupByFields.Clear();
			if (string.IsNullOrEmpty(FieldList))
			{
				return;
			}
			string[] array = FieldList.Split(new char[]
			{
				','
			});
			for (int i = 0; i <= array.Length - 1; i++)
			{
				GridGroupByField gridGroupByField = new GridGroupByField();
				string[] array2 = this.Tokenize(array[i]);
				switch (array2.Length)
				{
				case 1:
					break;
				case 2:
					gridGroupByField.SortOrder = GridSortExpression.SortOrderFromString(array2[1]);
					break;
				default:
					throw new Exception("Too many spaces in field definition: '" + array[i] + "'.");
				}
				array2 = array2[0].Split(new char[]
				{
					'!'
				});
				switch (array2.Length)
				{
				case 1:
					gridGroupByField.FieldName = array2[0];
					break;
				case 2:
					if (!AllowRelation)
					{
						throw new Exception("Relation specifiers not permitted in field list: '" + array[i] + "'.");
					}
					gridGroupByField.RelationName = array2[0].Trim();
					gridGroupByField.FieldName = array2[1].Trim();
					break;
				default:
					throw new Exception("Invalid field definition: " + array[i] + "'.");
				}
				if (gridGroupByField.FieldAlias == null)
				{
					gridGroupByField.FieldAlias = gridGroupByField.FieldName;
				}
				this.GroupByFields.Add(gridGroupByField);
			}
		}

		// Token: 0x0600B950 RID: 47440 RVA: 0x00290588 File Offset: 0x0028E788
		public bool IsSame(GridGroupByExpression expression)
		{
			foreach (object obj in expression.SelectFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				bool flag = false;
				foreach (object obj2 in this.SelectFields)
				{
					GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
					if (gridGroupByField2.FieldName.ToLower() == gridGroupByField.FieldName.ToLower())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			foreach (object obj3 in expression.GroupByFields)
			{
				GridGroupByField gridGroupByField3 = (GridGroupByField)obj3;
				bool flag2 = false;
				foreach (object obj4 in this.GroupByFields)
				{
					GridGroupByField gridGroupByField4 = (GridGroupByField)obj4;
					if (gridGroupByField4.FieldName.ToLower() == gridGroupByField3.FieldName.ToLower())
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600B951 RID: 47441 RVA: 0x00290720 File Offset: 0x0028E920
		public bool ContainsSameGroupByField(GridGroupByExpression expression)
		{
			bool flag = false;
			foreach (object obj in expression.GroupByFields)
			{
				GridGroupByField gridGroupByField = (GridGroupByField)obj;
				foreach (object obj2 in this.GroupByFields)
				{
					GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
					if (gridGroupByField2.FieldName.ToUpper() == gridGroupByField.FieldName.ToUpper())
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x0600B952 RID: 47442 RVA: 0x002907E4 File Offset: 0x0028E9E4
		public void CopyFrom(GridGroupByExpression expression)
		{
			this.SelectFields.Clear();
			this.GroupByFields.Clear();
			foreach (object obj in expression.SelectFields)
			{
				GridGroupByField field = (GridGroupByField)obj;
				GridGroupByField gridGroupByField = new GridGroupByField();
				gridGroupByField.CopyFrom(field);
				this.SelectFields.Add(gridGroupByField);
			}
			foreach (object obj2 in expression.GroupByFields)
			{
				GridGroupByField field2 = (GridGroupByField)obj2;
				GridGroupByField gridGroupByField2 = new GridGroupByField();
				gridGroupByField2.CopyFrom(field2);
				this.GroupByFields.Add(gridGroupByField2);
			}
		}

		// Token: 0x0600B953 RID: 47443 RVA: 0x002908D0 File Offset: 0x0028EAD0
		internal GridGroupByExpression Clone()
		{
			GridGroupByExpression gridGroupByExpression = new GridGroupByExpression();
			gridGroupByExpression.CopyFrom(this);
			return gridGroupByExpression;
		}

		// Token: 0x040030F5 RID: 12533
		public const string GroupByClause = "Group By";

		// Token: 0x040030F6 RID: 12534
		private string _expressionAsString;

		// Token: 0x040030F7 RID: 12535
		private int _index;

		// Token: 0x040030F8 RID: 12536
		private GridGroupByFieldList _selectFields = new GridGroupByFieldList();

		// Token: 0x040030F9 RID: 12537
		private GridGroupByFieldList _groupByFields = new GridGroupByFieldList();
	}
}
