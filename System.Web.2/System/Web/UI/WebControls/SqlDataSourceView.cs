using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004DB RID: 1243
	public class SqlDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000C65B8 File Offset: 0x000C47B8
		public SqlDataSourceView(SqlDataSource owner, string name, HttpContext context) : base(owner, name)
		{
			this._owner = owner;
			this._context = context;
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000C65D7 File Offset: 0x000C47D7
		// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x000C65DF File Offset: 0x000C47DF
		public bool CancelSelectOnNullParameter
		{
			get
			{
				return this._cancelSelectOnNullParameter;
			}
			set
			{
				if (this.CancelSelectOnNullParameter != value)
				{
					this._cancelSelectOnNullParameter = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000C65FC File Offset: 0x000C47FC
		public override bool CanDelete
		{
			get
			{
				return this.DeleteCommand.Length != 0;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003DAB RID: 15787 RVA: 0x000C660C File Offset: 0x000C480C
		public override bool CanInsert
		{
			get
			{
				return this.InsertCommand.Length != 0;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003DAE RID: 15790 RVA: 0x000C661C File Offset: 0x000C481C
		public override bool CanSort
		{
			get
			{
				return this._owner.DataSourceMode == SqlDataSourceMode.DataSet || this.SortParameterName.Length > 0;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000C663C File Offset: 0x000C483C
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateCommand.Length != 0;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003DB0 RID: 15792 RVA: 0x000C664C File Offset: 0x000C484C
		// (set) Token: 0x06003DB1 RID: 15793 RVA: 0x000C6654 File Offset: 0x000C4854
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this._conflictDetection;
			}
			set
			{
				if (value < ConflictOptions.OverwriteChanges || value > ConflictOptions.CompareAllValues)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._conflictDetection = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x000C667B File Offset: 0x000C487B
		// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x000C6691 File Offset: 0x000C4891
		public string DeleteCommand
		{
			get
			{
				if (this._deleteCommand == null)
				{
					return string.Empty;
				}
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x000C669A File Offset: 0x000C489A
		// (set) Token: 0x06003DB5 RID: 15797 RVA: 0x000C66A2 File Offset: 0x000C48A2
		public SqlDataSourceCommandType DeleteCommandType
		{
			get
			{
				return this._deleteCommandType;
			}
			set
			{
				if (value < SqlDataSourceCommandType.Text || value > SqlDataSourceCommandType.StoredProcedure)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._deleteCommandType = value;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x000C66BE File Offset: 0x000C48BE
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("SqlDataSource_DeleteParameters")]
		public ParameterCollection DeleteParameters
		{
			get
			{
				if (this._deleteParameters == null)
				{
					this._deleteParameters = new ParameterCollection();
				}
				return this._deleteParameters;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000C66D9 File Offset: 0x000C48D9
		// (set) Token: 0x06003DB8 RID: 15800 RVA: 0x000C66EF File Offset: 0x000C48EF
		public string FilterExpression
		{
			get
			{
				if (this._filterExpression == null)
				{
					return string.Empty;
				}
				return this._filterExpression;
			}
			set
			{
				if (this.FilterExpression != value)
				{
					this._filterExpression = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000C6714 File Offset: 0x000C4914
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("SqlDataSource_FilterParameters")]
		public ParameterCollection FilterParameters
		{
			get
			{
				if (this._filterParameters == null)
				{
					this._filterParameters = new ParameterCollection();
					this._filterParameters.ParametersChanged += this.SelectParametersChangedEventHandler;
					if (this._tracking)
					{
						((IStateManager)this._filterParameters).TrackViewState();
					}
				}
				return this._filterParameters;
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000C6764 File Offset: 0x000C4964
		// (set) Token: 0x06003DBB RID: 15803 RVA: 0x000C677A File Offset: 0x000C497A
		public string InsertCommand
		{
			get
			{
				if (this._insertCommand == null)
				{
					return string.Empty;
				}
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000C6783 File Offset: 0x000C4983
		// (set) Token: 0x06003DBD RID: 15805 RVA: 0x000C678B File Offset: 0x000C498B
		public SqlDataSourceCommandType InsertCommandType
		{
			get
			{
				return this._insertCommandType;
			}
			set
			{
				if (value < SqlDataSourceCommandType.Text || value > SqlDataSourceCommandType.StoredProcedure)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._insertCommandType = value;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000C67A7 File Offset: 0x000C49A7
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("SqlDataSource_InsertParameters")]
		public ParameterCollection InsertParameters
		{
			get
			{
				if (this._insertParameters == null)
				{
					this._insertParameters = new ParameterCollection();
				}
				return this._insertParameters;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x000C67C2 File Offset: 0x000C49C2
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000C67CA File Offset: 0x000C49CA
		// (set) Token: 0x06003DC1 RID: 15809 RVA: 0x000C67E0 File Offset: 0x000C49E0
		[DefaultValue("{0}")]
		[WebCategory("Data")]
		[WebSysDescription("DataSource_OldValuesParameterFormatString")]
		public string OldValuesParameterFormatString
		{
			get
			{
				if (this._oldValuesParameterFormatString == null)
				{
					return "{0}";
				}
				return this._oldValuesParameterFormatString;
			}
			set
			{
				this._oldValuesParameterFormatString = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000C67F4 File Offset: 0x000C49F4
		protected virtual string ParameterPrefix
		{
			get
			{
				if (string.IsNullOrEmpty(this._owner.ProviderName) || string.Equals(this._owner.ProviderName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
				{
					return "@";
				}
				return string.Empty;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000C682B File Offset: 0x000C4A2B
		// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x000C6841 File Offset: 0x000C4A41
		public string SelectCommand
		{
			get
			{
				if (this._selectCommand == null)
				{
					return string.Empty;
				}
				return this._selectCommand;
			}
			set
			{
				if (this.SelectCommand != value)
				{
					this._selectCommand = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000C6863 File Offset: 0x000C4A63
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x000C686B File Offset: 0x000C4A6B
		public SqlDataSourceCommandType SelectCommandType
		{
			get
			{
				return this._selectCommandType;
			}
			set
			{
				if (value < SqlDataSourceCommandType.Text || value > SqlDataSourceCommandType.StoredProcedure)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._selectCommandType = value;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x000C6888 File Offset: 0x000C4A88
		public ParameterCollection SelectParameters
		{
			get
			{
				if (this._selectParameters == null)
				{
					this._selectParameters = new ParameterCollection();
					this._selectParameters.ParametersChanged += this.SelectParametersChangedEventHandler;
					if (this._tracking)
					{
						((IStateManager)this._selectParameters).TrackViewState();
					}
				}
				return this._selectParameters;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000C68D8 File Offset: 0x000C4AD8
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000C68EE File Offset: 0x000C4AEE
		public string SortParameterName
		{
			get
			{
				if (this._sortParameterName == null)
				{
					return string.Empty;
				}
				return this._sortParameterName;
			}
			set
			{
				if (this.SortParameterName != value)
				{
					this._sortParameterName = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000C6910 File Offset: 0x000C4B10
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x000C6926 File Offset: 0x000C4B26
		public string UpdateCommand
		{
			get
			{
				if (this._updateCommand == null)
				{
					return string.Empty;
				}
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x000C692F File Offset: 0x000C4B2F
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x000C6937 File Offset: 0x000C4B37
		public SqlDataSourceCommandType UpdateCommandType
		{
			get
			{
				return this._updateCommandType;
			}
			set
			{
				if (value < SqlDataSourceCommandType.Text || value > SqlDataSourceCommandType.StoredProcedure)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._updateCommandType = value;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x000C6953 File Offset: 0x000C4B53
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("SqlDataSource_UpdateParameters")]
		public ParameterCollection UpdateParameters
		{
			get
			{
				if (this._updateParameters == null)
				{
					this._updateParameters = new ParameterCollection();
				}
				return this._updateParameters;
			}
		}

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06003DCF RID: 15823 RVA: 0x000C696E File Offset: 0x000C4B6E
		// (remove) Token: 0x06003DD0 RID: 15824 RVA: 0x000C6981 File Offset: 0x000C4B81
		public event SqlDataSourceStatusEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventDeleted, value);
			}
		}

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x06003DD1 RID: 15825 RVA: 0x000C6994 File Offset: 0x000C4B94
		// (remove) Token: 0x06003DD2 RID: 15826 RVA: 0x000C69A7 File Offset: 0x000C4BA7
		public event SqlDataSourceCommandEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventDeleting, value);
			}
		}

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06003DD3 RID: 15827 RVA: 0x000C69BA File Offset: 0x000C4BBA
		// (remove) Token: 0x06003DD4 RID: 15828 RVA: 0x000C69CD File Offset: 0x000C4BCD
		public event SqlDataSourceFilteringEventHandler Filtering
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventFiltering, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventFiltering, value);
			}
		}

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06003DD5 RID: 15829 RVA: 0x000C69E0 File Offset: 0x000C4BE0
		// (remove) Token: 0x06003DD6 RID: 15830 RVA: 0x000C69F3 File Offset: 0x000C4BF3
		public event SqlDataSourceStatusEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventInserted, value);
			}
		}

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06003DD7 RID: 15831 RVA: 0x000C6A06 File Offset: 0x000C4C06
		// (remove) Token: 0x06003DD8 RID: 15832 RVA: 0x000C6A19 File Offset: 0x000C4C19
		public event SqlDataSourceCommandEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventInserting, value);
			}
		}

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06003DD9 RID: 15833 RVA: 0x000C6A2C File Offset: 0x000C4C2C
		// (remove) Token: 0x06003DDA RID: 15834 RVA: 0x000C6A3F File Offset: 0x000C4C3F
		public event SqlDataSourceStatusEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventSelected, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventSelected, value);
			}
		}

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06003DDB RID: 15835 RVA: 0x000C6A52 File Offset: 0x000C4C52
		// (remove) Token: 0x06003DDC RID: 15836 RVA: 0x000C6A65 File Offset: 0x000C4C65
		public event SqlDataSourceSelectingEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventSelecting, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventSelecting, value);
			}
		}

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06003DDD RID: 15837 RVA: 0x000C6A78 File Offset: 0x000C4C78
		// (remove) Token: 0x06003DDE RID: 15838 RVA: 0x000C6A8B File Offset: 0x000C4C8B
		public event SqlDataSourceStatusEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventUpdated, value);
			}
		}

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06003DDF RID: 15839 RVA: 0x000C6A9E File Offset: 0x000C4C9E
		// (remove) Token: 0x06003DE0 RID: 15840 RVA: 0x000C6AB1 File Offset: 0x000C4CB1
		public event SqlDataSourceCommandEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(SqlDataSourceView.EventUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataSourceView.EventUpdating, value);
			}
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x000C6AC4 File Offset: 0x000C4CC4
		private void AddParameters(DbCommand command, ParameterCollection reference, IDictionary parameters, IDictionary exclusionList, string oldValuesParameterFormatString)
		{
			IDictionary dictionary = null;
			if (exclusionList != null)
			{
				dictionary = new ListDictionary(StringComparer.OrdinalIgnoreCase);
				foreach (object obj in exclusionList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			if (parameters != null)
			{
				string parameterPrefix = this.ParameterPrefix;
				foreach (object obj2 in parameters)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					string text = (string)dictionaryEntry2.Key;
					if (dictionary == null || !dictionary.Contains(text))
					{
						string text2;
						if (oldValuesParameterFormatString == null)
						{
							text2 = text;
						}
						else
						{
							text2 = string.Format(CultureInfo.InvariantCulture, oldValuesParameterFormatString, new object[]
							{
								text
							});
						}
						object value = dictionaryEntry2.Value;
						Parameter parameter = reference[text2];
						if (parameter != null)
						{
							value = parameter.GetValue(dictionaryEntry2.Value, false);
						}
						text2 = parameterPrefix + text2;
						if (command.Parameters.Contains(text2))
						{
							if (value != null)
							{
								command.Parameters[text2].Value = value;
							}
						}
						else
						{
							DbParameter value2 = this._owner.CreateParameter(text2, value);
							command.Parameters.Add(value2);
						}
					}
				}
			}
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000C6C48 File Offset: 0x000C4E48
		private Exception BuildCustomException(Exception ex, DataSourceOperation operation, DbCommand command, out bool isCustomException)
		{
			SqlException ex2 = ex as SqlException;
			if (ex2 != null && (ex2.Number == 137 || ex2.Number == 201))
			{
				string text;
				if (command.Parameters.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = true;
					foreach (object obj in command.Parameters)
					{
						DbParameter dbParameter = (DbParameter)obj;
						if (!flag)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(dbParameter.ParameterName);
						flag = false;
					}
					text = stringBuilder.ToString();
				}
				else
				{
					text = SR.GetString("SqlDataSourceView_NoParameters");
				}
				isCustomException = true;
				return new InvalidOperationException(SR.GetString("SqlDataSourceView_MissingParameters", new object[]
				{
					operation,
					this._owner.ID,
					text
				}));
			}
			isCustomException = false;
			return ex;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000BB8B3 File Offset: 0x000B9AB3
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000C6D50 File Offset: 0x000C4F50
		private int ExecuteDbCommand(DbCommand command, DataSourceOperation operation)
		{
			int num = 0;
			bool flag = false;
			try
			{
				if (command.Connection.State != ConnectionState.Open)
				{
					command.Connection.Open();
				}
				num = command.ExecuteNonQuery();
				if (num > 0)
				{
					this.OnDataSourceViewChanged(EventArgs.Empty);
					DataSourceCache cache = this._owner.Cache;
					if (cache != null && cache.Enabled)
					{
						this._owner.InvalidateCacheEntry();
					}
				}
				flag = true;
				SqlDataSourceStatusEventArgs e = new SqlDataSourceStatusEventArgs(command, num, null);
				switch (operation)
				{
				case DataSourceOperation.Delete:
					this.OnDeleted(e);
					break;
				case DataSourceOperation.Insert:
					this.OnInserted(e);
					break;
				case DataSourceOperation.Update:
					this.OnUpdated(e);
					break;
				}
			}
			catch (Exception ex)
			{
				if (!flag)
				{
					SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(command, num, ex);
					switch (operation)
					{
					case DataSourceOperation.Delete:
						this.OnDeleted(sqlDataSourceStatusEventArgs);
						break;
					case DataSourceOperation.Insert:
						this.OnInserted(sqlDataSourceStatusEventArgs);
						break;
					case DataSourceOperation.Update:
						this.OnUpdated(sqlDataSourceStatusEventArgs);
						break;
					}
					if (!sqlDataSourceStatusEventArgs.ExceptionHandled)
					{
						throw;
					}
				}
				else
				{
					bool flag2;
					ex = this.BuildCustomException(ex, operation, command, out flag2);
					if (flag2)
					{
						throw ex;
					}
					throw;
				}
			}
			finally
			{
				if (command.Connection.State == ConnectionState.Open)
				{
					command.Connection.Close();
				}
			}
			return num;
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000C6E94 File Offset: 0x000C5094
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_DeleteNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			DbConnection dbConnection = this._owner.CreateConnection(this._owner.ConnectionString);
			if (dbConnection == null)
			{
				throw new InvalidOperationException(SR.GetString("SqlDataSourceView_CouldNotCreateConnection", new object[]
				{
					this._owner.ID
				}));
			}
			string oldValuesParameterFormatString = this.OldValuesParameterFormatString;
			DbCommand dbCommand = this._owner.CreateCommand(this.DeleteCommand, dbConnection);
			this.InitializeParameters(dbCommand, this.DeleteParameters, oldValues);
			this.AddParameters(dbCommand, this.DeleteParameters, keys, null, oldValuesParameterFormatString);
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				if (oldValues == null || oldValues.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("SqlDataSourceView_Pessimistic", new object[]
					{
						SR.GetString("DataSourceView_delete"),
						this._owner.ID,
						"values"
					}));
				}
				this.AddParameters(dbCommand, this.DeleteParameters, oldValues, null, oldValuesParameterFormatString);
			}
			dbCommand.CommandType = SqlDataSourceView.GetCommandType(this.DeleteCommandType);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnDeleting(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return 0;
			}
			this.ReplaceNullValues(dbCommand);
			return this.ExecuteDbCommand(dbCommand, DataSourceOperation.Delete);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000C6FD8 File Offset: 0x000C51D8
		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_InsertNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			DbConnection dbConnection = this._owner.CreateConnection(this._owner.ConnectionString);
			if (dbConnection == null)
			{
				throw new InvalidOperationException(SR.GetString("SqlDataSourceView_CouldNotCreateConnection", new object[]
				{
					this._owner.ID
				}));
			}
			DbCommand dbCommand = this._owner.CreateCommand(this.InsertCommand, dbConnection);
			this.InitializeParameters(dbCommand, this.InsertParameters, null);
			this.AddParameters(dbCommand, this.InsertParameters, values, null, null);
			dbCommand.CommandType = SqlDataSourceView.GetCommandType(this.InsertCommandType);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnInserting(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return 0;
			}
			this.ReplaceNullValues(dbCommand);
			return this.ExecuteDbCommand(dbCommand, DataSourceOperation.Insert);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000C70B8 File Offset: 0x000C52B8
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			if (this.SelectCommand.Length == 0)
			{
				return null;
			}
			DbConnection dbConnection = this._owner.CreateConnection(this._owner.ConnectionString);
			if (dbConnection == null)
			{
				throw new InvalidOperationException(SR.GetString("SqlDataSourceView_CouldNotCreateConnection", new object[]
				{
					this._owner.ID
				}));
			}
			DataSourceCache cache = this._owner.Cache;
			bool flag = cache != null && cache.Enabled;
			string sortExpression = arguments.SortExpression;
			if (this.CanPage)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.Page);
			}
			if (this.CanSort)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.Sort);
			}
			if (this.CanRetrieveTotalRowCount)
			{
				arguments.AddSupportedCapabilities(DataSourceCapabilities.RetrieveTotalRowCount);
			}
			if (flag)
			{
				if (this._owner.DataSourceMode != SqlDataSourceMode.DataSet)
				{
					throw new NotSupportedException(SR.GetString("SqlDataSourceView_CacheNotSupported", new object[]
					{
						this._owner.ID
					}));
				}
				arguments.RaiseUnsupportedCapabilitiesError(this);
				DataSet dataSet = this._owner.LoadDataFromCache(0, -1) as DataSet;
				if (dataSet != null)
				{
					IOrderedDictionary values = this.FilterParameters.GetValues(this._context, this._owner);
					if (this.FilterExpression.Length > 0)
					{
						SqlDataSourceFilteringEventArgs sqlDataSourceFilteringEventArgs = new SqlDataSourceFilteringEventArgs(values);
						this.OnFiltering(sqlDataSourceFilteringEventArgs);
						if (sqlDataSourceFilteringEventArgs.Cancel)
						{
							return null;
						}
					}
					return FilteredDataSetHelper.CreateFilteredDataView(dataSet.Tables[0], sortExpression, this.FilterExpression, values);
				}
			}
			DbCommand dbCommand = this._owner.CreateCommand(this.SelectCommand, dbConnection);
			this.InitializeParameters(dbCommand, this.SelectParameters, null);
			dbCommand.CommandType = SqlDataSourceView.GetCommandType(this.SelectCommandType);
			SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs = new SqlDataSourceSelectingEventArgs(dbCommand, arguments);
			this.OnSelecting(sqlDataSourceSelectingEventArgs);
			if (sqlDataSourceSelectingEventArgs.Cancel)
			{
				return null;
			}
			string sortParameterName = this.SortParameterName;
			if (sortParameterName.Length > 0)
			{
				if (dbCommand.CommandType != CommandType.StoredProcedure)
				{
					throw new NotSupportedException(SR.GetString("SqlDataSourceView_SortParameterRequiresStoredProcedure", new object[]
					{
						this._owner.ID
					}));
				}
				dbCommand.Parameters.Add(this._owner.CreateParameter(this.ParameterPrefix + sortParameterName, sortExpression));
				arguments.SortExpression = string.Empty;
			}
			arguments.RaiseUnsupportedCapabilitiesError(this);
			sortExpression = arguments.SortExpression;
			if (this.CancelSelectOnNullParameter)
			{
				int count = dbCommand.Parameters.Count;
				for (int i = 0; i < count; i++)
				{
					DbParameter dbParameter = dbCommand.Parameters[i];
					if (dbParameter != null && dbParameter.Value == null && (dbParameter.Direction == ParameterDirection.Input || dbParameter.Direction == ParameterDirection.InputOutput))
					{
						return null;
					}
				}
			}
			this.ReplaceNullValues(dbCommand);
			IEnumerable result = null;
			SqlDataSourceMode dataSourceMode = this._owner.DataSourceMode;
			if (dataSourceMode != SqlDataSourceMode.DataReader)
			{
				if (dataSourceMode == SqlDataSourceMode.DataSet)
				{
					SqlCacheDependency dependency = null;
					if (flag && cache is SqlDataSourceCache)
					{
						SqlDataSourceCache sqlDataSourceCache = (SqlDataSourceCache)cache;
						if (string.Equals(sqlDataSourceCache.SqlCacheDependency, "CommandNotification", StringComparison.OrdinalIgnoreCase))
						{
							if (!(dbCommand is SqlCommand))
							{
								throw new InvalidOperationException(SR.GetString("SqlDataSourceView_CommandNotificationNotSupported", new object[]
								{
									this._owner.ID
								}));
							}
							dependency = new SqlCacheDependency((SqlCommand)dbCommand);
						}
					}
					DbDataAdapter dbDataAdapter = this._owner.CreateDataAdapter(dbCommand);
					DataSet dataSet2 = new DataSet();
					int affectedRows = 0;
					bool flag2 = false;
					try
					{
						affectedRows = dbDataAdapter.Fill(dataSet2, base.Name);
						flag2 = true;
						SqlDataSourceStatusEventArgs e = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows, null);
						this.OnSelected(e);
					}
					catch (Exception ex)
					{
						if (!flag2)
						{
							SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows, ex);
							this.OnSelected(sqlDataSourceStatusEventArgs);
							if (!sqlDataSourceStatusEventArgs.ExceptionHandled)
							{
								throw;
							}
						}
						else
						{
							bool flag3;
							ex = this.BuildCustomException(ex, DataSourceOperation.Select, dbCommand, out flag3);
							if (flag3)
							{
								throw ex;
							}
							throw;
						}
					}
					finally
					{
						if (dbConnection.State == ConnectionState.Open)
						{
							dbConnection.Close();
						}
					}
					DataTable dataTable = (dataSet2.Tables.Count > 0) ? dataSet2.Tables[0] : null;
					if (flag && dataTable != null)
					{
						this._owner.SaveDataToCache(0, -1, dataSet2, dependency);
					}
					if (dataTable != null)
					{
						IOrderedDictionary values2 = this.FilterParameters.GetValues(this._context, this._owner);
						if (this.FilterExpression.Length > 0)
						{
							SqlDataSourceFilteringEventArgs sqlDataSourceFilteringEventArgs2 = new SqlDataSourceFilteringEventArgs(values2);
							this.OnFiltering(sqlDataSourceFilteringEventArgs2);
							if (sqlDataSourceFilteringEventArgs2.Cancel)
							{
								return null;
							}
						}
						result = FilteredDataSetHelper.CreateFilteredDataView(dataTable, sortExpression, this.FilterExpression, values2);
					}
				}
			}
			else
			{
				if (this.FilterExpression.Length > 0)
				{
					throw new NotSupportedException(SR.GetString("SqlDataSourceView_FilterNotSupported", new object[]
					{
						this._owner.ID
					}));
				}
				if (sortExpression.Length > 0)
				{
					throw new NotSupportedException(SR.GetString("SqlDataSourceView_SortNotSupported", new object[]
					{
						this._owner.ID
					}));
				}
				bool flag4 = false;
				try
				{
					if (dbConnection.State != ConnectionState.Open)
					{
						dbConnection.Open();
					}
					result = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
					flag4 = true;
					SqlDataSourceStatusEventArgs e2 = new SqlDataSourceStatusEventArgs(dbCommand, 0, null);
					this.OnSelected(e2);
				}
				catch (Exception ex2)
				{
					if (!flag4)
					{
						SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs2 = new SqlDataSourceStatusEventArgs(dbCommand, 0, ex2);
						this.OnSelected(sqlDataSourceStatusEventArgs2);
						if (!sqlDataSourceStatusEventArgs2.ExceptionHandled)
						{
							throw;
						}
					}
					else
					{
						bool flag5;
						ex2 = this.BuildCustomException(ex2, DataSourceOperation.Select, dbCommand, out flag5);
						if (flag5)
						{
							throw ex2;
						}
						throw;
					}
				}
			}
			return result;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x000C75FC File Offset: 0x000C57FC
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_UpdateNotSupported", new object[]
				{
					this._owner.ID
				}));
			}
			DbConnection dbConnection = this._owner.CreateConnection(this._owner.ConnectionString);
			if (dbConnection == null)
			{
				throw new InvalidOperationException(SR.GetString("SqlDataSourceView_CouldNotCreateConnection", new object[]
				{
					this._owner.ID
				}));
			}
			string oldValuesParameterFormatString = this.OldValuesParameterFormatString;
			DbCommand dbCommand = this._owner.CreateCommand(this.UpdateCommand, dbConnection);
			this.InitializeParameters(dbCommand, this.UpdateParameters, keys);
			this.AddParameters(dbCommand, this.UpdateParameters, values, null, null);
			this.AddParameters(dbCommand, this.UpdateParameters, keys, null, oldValuesParameterFormatString);
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				if (oldValues == null || oldValues.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("SqlDataSourceView_Pessimistic", new object[]
					{
						SR.GetString("DataSourceView_update"),
						this._owner.ID,
						"oldValues"
					}));
				}
				this.AddParameters(dbCommand, this.UpdateParameters, oldValues, null, oldValuesParameterFormatString);
			}
			dbCommand.CommandType = SqlDataSourceView.GetCommandType(this.UpdateCommandType);
			SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
			this.OnUpdating(sqlDataSourceCommandEventArgs);
			if (sqlDataSourceCommandEventArgs.Cancel)
			{
				return 0;
			}
			this.ReplaceNullValues(dbCommand);
			return this.ExecuteDbCommand(dbCommand, DataSourceOperation.Update);
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000C7750 File Offset: 0x000C5950
		private static CommandType GetCommandType(SqlDataSourceCommandType commandType)
		{
			if (commandType == SqlDataSourceCommandType.Text)
			{
				return CommandType.Text;
			}
			return CommandType.StoredProcedure;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000C7758 File Offset: 0x000C5958
		private void InitializeParameters(DbCommand command, ParameterCollection parameters, IDictionary exclusionList)
		{
			string parameterPrefix = this.ParameterPrefix;
			IDictionary dictionary = null;
			if (exclusionList != null)
			{
				dictionary = new ListDictionary(StringComparer.OrdinalIgnoreCase);
				foreach (object obj in exclusionList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			IOrderedDictionary values = parameters.GetValues(this._context, this._owner);
			for (int i = 0; i < parameters.Count; i++)
			{
				Parameter parameter = parameters[i];
				if (dictionary == null || !dictionary.Contains(parameter.Name))
				{
					DbParameter dbParameter = this._owner.CreateParameter(parameterPrefix + parameter.Name, values[i]);
					dbParameter.Direction = parameter.Direction;
					dbParameter.Size = parameter.Size;
					if (parameter.DbType != DbType.Object || (parameter.Type != TypeCode.Empty && parameter.Type != TypeCode.DBNull))
					{
						SqlParameter sqlParameter = dbParameter as SqlParameter;
						if (sqlParameter == null)
						{
							dbParameter.DbType = parameter.GetDatabaseType();
						}
						else
						{
							DbType databaseType = parameter.GetDatabaseType();
							if (databaseType != DbType.Date)
							{
								if (databaseType == DbType.Time)
								{
									sqlParameter.SqlDbType = SqlDbType.Time;
								}
								else
								{
									dbParameter.DbType = parameter.GetDatabaseType();
								}
							}
							else
							{
								sqlParameter.SqlDbType = SqlDbType.Date;
							}
						}
					}
					command.Parameters.Add(dbParameter);
				}
			}
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000BCA22 File Offset: 0x000BAC22
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000C78E0 File Offset: 0x000C5AE0
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			Pair pair = (Pair)savedState;
			if (pair.First != null)
			{
				((IStateManager)this.SelectParameters).LoadViewState(pair.First);
			}
			if (pair.Second != null)
			{
				((IStateManager)this.FilterParameters).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000C792C File Offset: 0x000C5B2C
		protected virtual void OnDeleted(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventDeleted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000C795C File Offset: 0x000C5B5C
		protected virtual void OnDeleting(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventDeleting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000C798C File Offset: 0x000C5B8C
		protected virtual void OnFiltering(SqlDataSourceFilteringEventArgs e)
		{
			SqlDataSourceFilteringEventHandler sqlDataSourceFilteringEventHandler = base.Events[SqlDataSourceView.EventFiltering] as SqlDataSourceFilteringEventHandler;
			if (sqlDataSourceFilteringEventHandler != null)
			{
				sqlDataSourceFilteringEventHandler(this, e);
			}
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000C79BC File Offset: 0x000C5BBC
		protected virtual void OnInserted(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventInserted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000C79EC File Offset: 0x000C5BEC
		protected virtual void OnInserting(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventInserting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000C7A1C File Offset: 0x000C5C1C
		protected virtual void OnSelected(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventSelected] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x000C7A4C File Offset: 0x000C5C4C
		protected virtual void OnSelecting(SqlDataSourceSelectingEventArgs e)
		{
			SqlDataSourceSelectingEventHandler sqlDataSourceSelectingEventHandler = base.Events[SqlDataSourceView.EventSelecting] as SqlDataSourceSelectingEventHandler;
			if (sqlDataSourceSelectingEventHandler != null)
			{
				sqlDataSourceSelectingEventHandler(this, e);
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x000C7A7C File Offset: 0x000C5C7C
		protected virtual void OnUpdated(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventUpdated] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x000C7AAC File Offset: 0x000C5CAC
		protected virtual void OnUpdating(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventUpdating] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000C7ADC File Offset: 0x000C5CDC
		protected internal override void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
		{
			if (!this.CanPage && (capability & DataSourceCapabilities.Page) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_NoPaging", new object[]
				{
					this._owner.ID
				}));
			}
			if (!this.CanSort && (capability & DataSourceCapabilities.Sort) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_NoSorting", new object[]
				{
					this._owner.ID
				}));
			}
			if (!this.CanRetrieveTotalRowCount && (capability & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None)
			{
				throw new NotSupportedException(SR.GetString("SqlDataSourceView_NoRowCount", new object[]
				{
					this._owner.ID
				}));
			}
			base.RaiseUnsupportedCapabilityError(capability);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000C7B84 File Offset: 0x000C5D84
		private void ReplaceNullValues(DbCommand command)
		{
			int count = command.Parameters.Count;
			foreach (object obj in command.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				if (dbParameter.Value == null)
				{
					dbParameter.Value = DBNull.Value;
				}
			}
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000C7BF8 File Offset: 0x000C5DF8
		protected virtual object SaveViewState()
		{
			Pair pair = new Pair();
			pair.First = ((this._selectParameters != null) ? ((IStateManager)this._selectParameters).SaveViewState() : null);
			pair.Second = ((this._filterParameters != null) ? ((IStateManager)this._filterParameters).SaveViewState() : null);
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000B940C File Offset: 0x000B760C
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000B9CA8 File Offset: 0x000B7EA8
		private void SelectParametersChangedEventHandler(object o, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000C7C56 File Offset: 0x000C5E56
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._selectParameters != null)
			{
				((IStateManager)this._selectParameters).TrackViewState();
			}
			if (this._filterParameters != null)
			{
				((IStateManager)this._filterParameters).TrackViewState();
			}
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000B9415 File Offset: 0x000B7615
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000C7C85 File Offset: 0x000C5E85
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000C7C8D File Offset: 0x000C5E8D
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000C7C96 File Offset: 0x000C5E96
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000C7C9E File Offset: 0x000C5E9E
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x040023CE RID: 9166
		private const int MustDeclareVariableSqlExceptionNumber = 137;

		// Token: 0x040023CF RID: 9167
		private const int ProcedureExpectsParameterSqlExceptionNumber = 201;

		// Token: 0x040023D0 RID: 9168
		private static readonly object EventDeleted = new object();

		// Token: 0x040023D1 RID: 9169
		private static readonly object EventDeleting = new object();

		// Token: 0x040023D2 RID: 9170
		private static readonly object EventFiltering = new object();

		// Token: 0x040023D3 RID: 9171
		private static readonly object EventInserted = new object();

		// Token: 0x040023D4 RID: 9172
		private static readonly object EventInserting = new object();

		// Token: 0x040023D5 RID: 9173
		private static readonly object EventSelected = new object();

		// Token: 0x040023D6 RID: 9174
		private static readonly object EventSelecting = new object();

		// Token: 0x040023D7 RID: 9175
		private static readonly object EventUpdated = new object();

		// Token: 0x040023D8 RID: 9176
		private static readonly object EventUpdating = new object();

		// Token: 0x040023D9 RID: 9177
		private HttpContext _context;

		// Token: 0x040023DA RID: 9178
		private SqlDataSource _owner;

		// Token: 0x040023DB RID: 9179
		private bool _tracking;

		// Token: 0x040023DC RID: 9180
		private bool _cancelSelectOnNullParameter = true;

		// Token: 0x040023DD RID: 9181
		private ConflictOptions _conflictDetection;

		// Token: 0x040023DE RID: 9182
		private string _deleteCommand;

		// Token: 0x040023DF RID: 9183
		private SqlDataSourceCommandType _deleteCommandType;

		// Token: 0x040023E0 RID: 9184
		private ParameterCollection _deleteParameters;

		// Token: 0x040023E1 RID: 9185
		private string _filterExpression;

		// Token: 0x040023E2 RID: 9186
		private ParameterCollection _filterParameters;

		// Token: 0x040023E3 RID: 9187
		private string _insertCommand;

		// Token: 0x040023E4 RID: 9188
		private SqlDataSourceCommandType _insertCommandType;

		// Token: 0x040023E5 RID: 9189
		private ParameterCollection _insertParameters;

		// Token: 0x040023E6 RID: 9190
		private string _oldValuesParameterFormatString;

		// Token: 0x040023E7 RID: 9191
		private string _selectCommand;

		// Token: 0x040023E8 RID: 9192
		private SqlDataSourceCommandType _selectCommandType;

		// Token: 0x040023E9 RID: 9193
		private ParameterCollection _selectParameters;

		// Token: 0x040023EA RID: 9194
		private string _sortParameterName;

		// Token: 0x040023EB RID: 9195
		private string _updateCommand;

		// Token: 0x040023EC RID: 9196
		private SqlDataSourceCommandType _updateCommandType;

		// Token: 0x040023ED RID: 9197
		private ParameterCollection _updateParameters;
	}
}
