using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B5 RID: 1205
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SqlDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x060038CE RID: 14542 RVA: 0x000F1095 File Offset: 0x000F0095
		public SqlDataSourceView(SqlDataSource owner, string name, HttpContext context) : base(owner, name)
		{
			this._owner = owner;
			this._context = context;
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x000F10B4 File Offset: 0x000F00B4
		// (set) Token: 0x060038D0 RID: 14544 RVA: 0x000F10BC File Offset: 0x000F00BC
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

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000F10D9 File Offset: 0x000F00D9
		public override bool CanDelete
		{
			get
			{
				return this.DeleteCommand.Length != 0;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000F10EC File Offset: 0x000F00EC
		public override bool CanInsert
		{
			get
			{
				return this.InsertCommand.Length != 0;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000F10FF File Offset: 0x000F00FF
		public override bool CanPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000F1102 File Offset: 0x000F0102
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x000F1105 File Offset: 0x000F0105
		public override bool CanSort
		{
			get
			{
				return this._owner.DataSourceMode == SqlDataSourceMode.DataSet || this.SortParameterName.Length > 0;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000F1125 File Offset: 0x000F0125
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateCommand.Length != 0;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000F1138 File Offset: 0x000F0138
		// (set) Token: 0x060038D8 RID: 14552 RVA: 0x000F1140 File Offset: 0x000F0140
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

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060038D9 RID: 14553 RVA: 0x000F1167 File Offset: 0x000F0167
		// (set) Token: 0x060038DA RID: 14554 RVA: 0x000F117D File Offset: 0x000F017D
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

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000F1186 File Offset: 0x000F0186
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000F118E File Offset: 0x000F018E
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

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000F11AA File Offset: 0x000F01AA
		[DefaultValue(null)]
		[WebSysDescription("SqlDataSource_DeleteParameters")]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000F11C5 File Offset: 0x000F01C5
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x000F11DB File Offset: 0x000F01DB
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

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000F1200 File Offset: 0x000F0200
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000F1250 File Offset: 0x000F0250
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000F1266 File Offset: 0x000F0266
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

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000F126F File Offset: 0x000F026F
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000F1277 File Offset: 0x000F0277
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

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000F1293 File Offset: 0x000F0293
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000F12AE File Offset: 0x000F02AE
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000F12B6 File Offset: 0x000F02B6
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000F12CC File Offset: 0x000F02CC
		[WebSysDescription("DataSource_OldValuesParameterFormatString")]
		[DefaultValue("{0}")]
		[WebCategory("Data")]
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

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000F12E0 File Offset: 0x000F02E0
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

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000F1317 File Offset: 0x000F0317
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x000F132D File Offset: 0x000F032D
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

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000F134F File Offset: 0x000F034F
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000F1357 File Offset: 0x000F0357
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

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000F1374 File Offset: 0x000F0374
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

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x000F13C4 File Offset: 0x000F03C4
		// (set) Token: 0x060038F0 RID: 14576 RVA: 0x000F13DA File Offset: 0x000F03DA
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

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000F13FC File Offset: 0x000F03FC
		// (set) Token: 0x060038F2 RID: 14578 RVA: 0x000F1412 File Offset: 0x000F0412
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

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000F141B File Offset: 0x000F041B
		// (set) Token: 0x060038F4 RID: 14580 RVA: 0x000F1423 File Offset: 0x000F0423
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

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000F143F File Offset: 0x000F043F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(null)]
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

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060038F6 RID: 14582 RVA: 0x000F145A File Offset: 0x000F045A
		// (remove) Token: 0x060038F7 RID: 14583 RVA: 0x000F146D File Offset: 0x000F046D
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

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060038F8 RID: 14584 RVA: 0x000F1480 File Offset: 0x000F0480
		// (remove) Token: 0x060038F9 RID: 14585 RVA: 0x000F1493 File Offset: 0x000F0493
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

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060038FA RID: 14586 RVA: 0x000F14A6 File Offset: 0x000F04A6
		// (remove) Token: 0x060038FB RID: 14587 RVA: 0x000F14B9 File Offset: 0x000F04B9
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

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060038FC RID: 14588 RVA: 0x000F14CC File Offset: 0x000F04CC
		// (remove) Token: 0x060038FD RID: 14589 RVA: 0x000F14DF File Offset: 0x000F04DF
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

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060038FE RID: 14590 RVA: 0x000F14F2 File Offset: 0x000F04F2
		// (remove) Token: 0x060038FF RID: 14591 RVA: 0x000F1505 File Offset: 0x000F0505
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

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06003900 RID: 14592 RVA: 0x000F1518 File Offset: 0x000F0518
		// (remove) Token: 0x06003901 RID: 14593 RVA: 0x000F152B File Offset: 0x000F052B
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

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06003902 RID: 14594 RVA: 0x000F153E File Offset: 0x000F053E
		// (remove) Token: 0x06003903 RID: 14595 RVA: 0x000F1551 File Offset: 0x000F0551
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

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06003904 RID: 14596 RVA: 0x000F1564 File Offset: 0x000F0564
		// (remove) Token: 0x06003905 RID: 14597 RVA: 0x000F1577 File Offset: 0x000F0577
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

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06003906 RID: 14598 RVA: 0x000F158A File Offset: 0x000F058A
		// (remove) Token: 0x06003907 RID: 14599 RVA: 0x000F159D File Offset: 0x000F059D
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

		// Token: 0x06003908 RID: 14600 RVA: 0x000F15B0 File Offset: 0x000F05B0
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

		// Token: 0x06003909 RID: 14601 RVA: 0x000F1740 File Offset: 0x000F0740
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

		// Token: 0x0600390A RID: 14602 RVA: 0x000F1850 File Offset: 0x000F0850
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000F185C File Offset: 0x000F085C
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

		// Token: 0x0600390C RID: 14604 RVA: 0x000F19C0 File Offset: 0x000F09C0
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

		// Token: 0x0600390D RID: 14605 RVA: 0x000F1B18 File Offset: 0x000F0B18
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

		// Token: 0x0600390E RID: 14606 RVA: 0x000F1C00 File Offset: 0x000F0C00
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
			switch (this._owner.DataSourceMode)
			{
			case SqlDataSourceMode.DataReader:
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
				bool flag2 = false;
				try
				{
					if (dbConnection.State != ConnectionState.Open)
					{
						dbConnection.Open();
					}
					result = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
					flag2 = true;
					SqlDataSourceStatusEventArgs e = new SqlDataSourceStatusEventArgs(dbCommand, 0, null);
					this.OnSelected(e);
				}
				catch (Exception ex)
				{
					if (!flag2)
					{
						SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, 0, ex);
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
				break;
			}
			case SqlDataSourceMode.DataSet:
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
				bool flag4 = false;
				try
				{
					affectedRows = dbDataAdapter.Fill(dataSet2, base.Name);
					flag4 = true;
					SqlDataSourceStatusEventArgs e2 = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows, null);
					this.OnSelected(e2);
				}
				catch (Exception ex2)
				{
					if (!flag4)
					{
						SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs2 = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows, ex2);
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
				break;
			}
			}
			return result;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000F2168 File Offset: 0x000F1168
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

		// Token: 0x06003910 RID: 14608 RVA: 0x000F22CD File Offset: 0x000F12CD
		private static CommandType GetCommandType(SqlDataSourceCommandType commandType)
		{
			if (commandType == SqlDataSourceCommandType.Text)
			{
				return CommandType.Text;
			}
			return CommandType.StoredProcedure;
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000F22D8 File Offset: 0x000F12D8
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
							DbType dbType = databaseType;
							if (dbType != DbType.Date)
							{
								if (dbType == DbType.Time)
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

		// Token: 0x06003912 RID: 14610 RVA: 0x000F2468 File Offset: 0x000F1468
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000F2474 File Offset: 0x000F1474
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

		// Token: 0x06003914 RID: 14612 RVA: 0x000F24C0 File Offset: 0x000F14C0
		protected virtual void OnDeleted(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventDeleted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000F24F0 File Offset: 0x000F14F0
		protected virtual void OnDeleting(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventDeleting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000F2520 File Offset: 0x000F1520
		protected virtual void OnFiltering(SqlDataSourceFilteringEventArgs e)
		{
			SqlDataSourceFilteringEventHandler sqlDataSourceFilteringEventHandler = base.Events[SqlDataSourceView.EventFiltering] as SqlDataSourceFilteringEventHandler;
			if (sqlDataSourceFilteringEventHandler != null)
			{
				sqlDataSourceFilteringEventHandler(this, e);
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000F2550 File Offset: 0x000F1550
		protected virtual void OnInserted(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventInserted] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000F2580 File Offset: 0x000F1580
		protected virtual void OnInserting(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventInserting] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000F25B0 File Offset: 0x000F15B0
		protected virtual void OnSelected(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventSelected] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000F25E0 File Offset: 0x000F15E0
		protected virtual void OnSelecting(SqlDataSourceSelectingEventArgs e)
		{
			SqlDataSourceSelectingEventHandler sqlDataSourceSelectingEventHandler = base.Events[SqlDataSourceView.EventSelecting] as SqlDataSourceSelectingEventHandler;
			if (sqlDataSourceSelectingEventHandler != null)
			{
				sqlDataSourceSelectingEventHandler(this, e);
			}
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000F2610 File Offset: 0x000F1610
		protected virtual void OnUpdated(SqlDataSourceStatusEventArgs e)
		{
			SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler = base.Events[SqlDataSourceView.EventUpdated] as SqlDataSourceStatusEventHandler;
			if (sqlDataSourceStatusEventHandler != null)
			{
				sqlDataSourceStatusEventHandler(this, e);
			}
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000F2640 File Offset: 0x000F1640
		protected virtual void OnUpdating(SqlDataSourceCommandEventArgs e)
		{
			SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler = base.Events[SqlDataSourceView.EventUpdating] as SqlDataSourceCommandEventHandler;
			if (sqlDataSourceCommandEventHandler != null)
			{
				sqlDataSourceCommandEventHandler(this, e);
			}
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000F2670 File Offset: 0x000F1670
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

		// Token: 0x0600391E RID: 14622 RVA: 0x000F2720 File Offset: 0x000F1720
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

		// Token: 0x0600391F RID: 14623 RVA: 0x000F2794 File Offset: 0x000F1794
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

		// Token: 0x06003920 RID: 14624 RVA: 0x000F27F2 File Offset: 0x000F17F2
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000F27FB File Offset: 0x000F17FB
		private void SelectParametersChangedEventHandler(object o, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000F2808 File Offset: 0x000F1808
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

		// Token: 0x06003923 RID: 14627 RVA: 0x000F2837 File Offset: 0x000F1837
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000F2842 File Offset: 0x000F1842
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000F284A File Offset: 0x000F184A
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000F2853 File Offset: 0x000F1853
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000F285B File Offset: 0x000F185B
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x040025ED RID: 9709
		private const int MustDeclareVariableSqlExceptionNumber = 137;

		// Token: 0x040025EE RID: 9710
		private const int ProcedureExpectsParameterSqlExceptionNumber = 201;

		// Token: 0x040025EF RID: 9711
		private static readonly object EventDeleted = new object();

		// Token: 0x040025F0 RID: 9712
		private static readonly object EventDeleting = new object();

		// Token: 0x040025F1 RID: 9713
		private static readonly object EventFiltering = new object();

		// Token: 0x040025F2 RID: 9714
		private static readonly object EventInserted = new object();

		// Token: 0x040025F3 RID: 9715
		private static readonly object EventInserting = new object();

		// Token: 0x040025F4 RID: 9716
		private static readonly object EventSelected = new object();

		// Token: 0x040025F5 RID: 9717
		private static readonly object EventSelecting = new object();

		// Token: 0x040025F6 RID: 9718
		private static readonly object EventUpdated = new object();

		// Token: 0x040025F7 RID: 9719
		private static readonly object EventUpdating = new object();

		// Token: 0x040025F8 RID: 9720
		private HttpContext _context;

		// Token: 0x040025F9 RID: 9721
		private SqlDataSource _owner;

		// Token: 0x040025FA RID: 9722
		private bool _tracking;

		// Token: 0x040025FB RID: 9723
		private bool _cancelSelectOnNullParameter = true;

		// Token: 0x040025FC RID: 9724
		private ConflictOptions _conflictDetection;

		// Token: 0x040025FD RID: 9725
		private string _deleteCommand;

		// Token: 0x040025FE RID: 9726
		private SqlDataSourceCommandType _deleteCommandType;

		// Token: 0x040025FF RID: 9727
		private ParameterCollection _deleteParameters;

		// Token: 0x04002600 RID: 9728
		private string _filterExpression;

		// Token: 0x04002601 RID: 9729
		private ParameterCollection _filterParameters;

		// Token: 0x04002602 RID: 9730
		private string _insertCommand;

		// Token: 0x04002603 RID: 9731
		private SqlDataSourceCommandType _insertCommandType;

		// Token: 0x04002604 RID: 9732
		private ParameterCollection _insertParameters;

		// Token: 0x04002605 RID: 9733
		private string _oldValuesParameterFormatString;

		// Token: 0x04002606 RID: 9734
		private string _selectCommand;

		// Token: 0x04002607 RID: 9735
		private SqlDataSourceCommandType _selectCommandType;

		// Token: 0x04002608 RID: 9736
		private ParameterCollection _selectParameters;

		// Token: 0x04002609 RID: 9737
		private string _sortParameterName;

		// Token: 0x0400260A RID: 9738
		private string _updateCommand;

		// Token: 0x0400260B RID: 9739
		private SqlDataSourceCommandType _updateCommandType;

		// Token: 0x0400260C RID: 9740
		private ParameterCollection _updateParameters;
	}
}
