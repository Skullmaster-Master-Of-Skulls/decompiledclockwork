using System;
using System.ComponentModel;
using System.Data.ProviderBase;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x02000117 RID: 279
	public class DataAdapter : Component, IDataAdapter
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x00234988 File Offset: 0x00233D88
		protected DataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x002349D8 File Offset: 0x00233DD8
		protected DataAdapter(DataAdapter from)
		{
			this.CloneFrom(from);
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00234A28 File Offset: 0x00233E28
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x00234A48 File Offset: 0x00233E48
		[ResDescription("DataAdapter_AcceptChangesDuringFill")]
		[ResCategory("DataCategory_Fill")]
		[DefaultValue(true)]
		public bool AcceptChangesDuringFill
		{
			get
			{
				return this._acceptChangesDuringFill;
			}
			set
			{
				this._acceptChangesDuringFill = value;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00234A68 File Offset: 0x00233E68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return (LoadOption)0 == this._fillLoadOption;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00234A88 File Offset: 0x00233E88
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00234AA8 File Offset: 0x00233EA8
		[ResDescription("DataAdapter_AcceptChangesDuringUpdate")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(true)]
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00234AC8 File Offset: 0x00233EC8
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00234AE8 File Offset: 0x00233EE8
		[ResDescription("DataAdapter_ContinueUpdateOnError")]
		[DefaultValue(false)]
		[ResCategory("DataCategory_Update")]
		public bool ContinueUpdateOnError
		{
			get
			{
				return this._continueUpdateOnError;
			}
			set
			{
				this._continueUpdateOnError = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00234B08 File Offset: 0x00233F08
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00234B28 File Offset: 0x00233F28
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DataAdapter_FillLoadOption")]
		public LoadOption FillLoadOption
		{
			get
			{
				if (this._fillLoadOption == (LoadOption)0)
				{
					return LoadOption.OverwriteChanges;
				}
				return this._fillLoadOption;
			}
			set
			{
				switch (value)
				{
				case (LoadOption)0:
				case LoadOption.OverwriteChanges:
				case LoadOption.PreserveChanges:
				case LoadOption.Upsert:
					this._fillLoadOption = value;
					return;
				default:
					throw ADP.InvalidLoadOption(value);
				}
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00234B68 File Offset: 0x00233F68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this._fillLoadOption = (LoadOption)0;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00234B88 File Offset: 0x00233F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return (LoadOption)0 != this._fillLoadOption;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00234BA8 File Offset: 0x00233FA8
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00234BC8 File Offset: 0x00233FC8
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataAdapter_MissingMappingAction")]
		[DefaultValue(MissingMappingAction.Passthrough)]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
			set
			{
				switch (value)
				{
				case MissingMappingAction.Passthrough:
				case MissingMappingAction.Ignore:
				case MissingMappingAction.Error:
					this._missingMappingAction = value;
					return;
				default:
					throw ADP.InvalidMissingMappingAction(value);
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00234C08 File Offset: 0x00234008
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00234C28 File Offset: 0x00234028
		[DefaultValue(MissingSchemaAction.Add)]
		[ResDescription("DataAdapter_MissingSchemaAction")]
		[ResCategory("DataCategory_Mapping")]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this._missingSchemaAction;
			}
			set
			{
				switch (value)
				{
				case MissingSchemaAction.Add:
				case MissingSchemaAction.Ignore:
				case MissingSchemaAction.Error:
				case MissingSchemaAction.AddWithKey:
					this._missingSchemaAction = value;
					return;
				default:
					throw ADP.InvalidMissingSchemaAction(value);
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00234C68 File Offset: 0x00234068
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00234C88 File Offset: 0x00234088
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x00234CA8 File Offset: 0x002340A8
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DataAdapter_ReturnProviderSpecificTypes")]
		[DefaultValue(false)]
		public virtual bool ReturnProviderSpecificTypes
		{
			get
			{
				return this._returnProviderSpecificTypes;
			}
			set
			{
				this._returnProviderSpecificTypes = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00234CC8 File Offset: 0x002340C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataAdapter_TableMappings")]
		public DataTableMappingCollection TableMappings
		{
			get
			{
				DataTableMappingCollection dataTableMappingCollection = this._tableMappings;
				if (dataTableMappingCollection == null)
				{
					dataTableMappingCollection = this.CreateTableMappings();
					if (dataTableMappingCollection == null)
					{
						dataTableMappingCollection = new DataTableMappingCollection();
					}
					this._tableMappings = dataTableMappingCollection;
				}
				return dataTableMappingCollection;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00234CF8 File Offset: 0x002340F8
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00234D18 File Offset: 0x00234118
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00234D28 File Offset: 0x00234128
		protected bool HasTableMappings()
		{
			return this._tableMappings != null && 0 < this.TableMappings.Count;
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060011BE RID: 4542 RVA: 0x00234D58 File Offset: 0x00234158
		// (remove) Token: 0x060011BF RID: 4543 RVA: 0x00234D88 File Offset: 0x00234188
		[ResDescription("DataAdapter_FillError")]
		[ResCategory("DataCategory_Fill")]
		public event FillErrorEventHandler FillError
		{
			add
			{
				this._hasFillErrorHandler = true;
				base.Events.AddHandler(DataAdapter.EventFillError, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataAdapter.EventFillError, value);
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00234DA8 File Offset: 0x002341A8
		[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DataAdapter CloneInternals()
		{
			DataAdapter dataAdapter = (DataAdapter)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			dataAdapter.CloneFrom(this);
			return dataAdapter;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00234DD8 File Offset: 0x002341D8
		private void CloneFrom(DataAdapter from)
		{
			this._acceptChangesDuringUpdate = from._acceptChangesDuringUpdate;
			this._acceptChangesDuringUpdateAfterInsert = from._acceptChangesDuringUpdateAfterInsert;
			this._continueUpdateOnError = from._continueUpdateOnError;
			this._returnProviderSpecificTypes = from._returnProviderSpecificTypes;
			this._acceptChangesDuringFill = from._acceptChangesDuringFill;
			this._fillLoadOption = from._fillLoadOption;
			this._missingMappingAction = from._missingMappingAction;
			this._missingSchemaAction = from._missingSchemaAction;
			if (from._tableMappings != null && 0 < from.TableMappings.Count)
			{
				DataTableMappingCollection tableMappings = this.TableMappings;
				foreach (object obj in from.TableMappings)
				{
					tableMappings.Add((obj is ICloneable) ? ((ICloneable)obj).Clone() : obj);
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00234ED8 File Offset: 0x002342D8
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			Bid.Trace("<comm.DataAdapter.CreateTableMappings|API> %d#\n", this.ObjectID);
			return new DataTableMappingCollection();
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00234F08 File Offset: 0x00234308
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._tableMappings = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00234F28 File Offset: 0x00234328
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00234F48 File Offset: 0x00234348
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DataAdapter.FillSchema|API> %d#, dataSet, schemaType=%d{ds.SchemaType}, srcTable, dataReader\n", this.ObjectID, (int)schemaType);
			DataTable[] result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				object obj = this.FillSchemaFromReader(dataSet, null, schemaType, srcTable, dataReader);
				result = (DataTable[])obj;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00234FF8 File Offset: 0x002343F8
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DataAdapter.FillSchema|API> %d#, dataTable, schemaType, dataReader\n", this.ObjectID);
			DataTable result;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				object obj = this.FillSchemaFromReader(null, dataTable, schemaType, null, dataReader);
				result = (DataTable)obj;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00235088 File Offset: 0x00234488
		internal object FillSchemaFromReader(DataSet dataset, DataTable datatable, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			DataTable[] array = null;
			int num = 0;
			SchemaMapping schemaMapping;
			for (;;)
			{
				DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
				if (0 < dataReaderContainer.FieldCount)
				{
					string sourceTableName = null;
					if (dataset != null)
					{
						sourceTableName = DataAdapter.GetSourceTableName(srcTable, num);
						num++;
					}
					schemaMapping = new SchemaMapping(this, dataset, datatable, dataReaderContainer, true, schemaType, sourceTableName, false, null, null);
					if (datatable != null)
					{
						break;
					}
					if (schemaMapping.DataTable != null)
					{
						if (array == null)
						{
							array = new DataTable[]
							{
								schemaMapping.DataTable
							};
						}
						else
						{
							array = DataAdapter.AddDataTableToArray(array, schemaMapping.DataTable);
						}
					}
				}
				if (!dataReader.NextResult())
				{
					goto Block_6;
				}
			}
			return schemaMapping.DataTable;
			Block_6:
			object obj = array;
			if (obj == null && datatable == null)
			{
				obj = new DataTable[0];
			}
			return obj;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00235138 File Offset: 0x00234538
		public virtual int Fill(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00235158 File Offset: 0x00234558
		protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DataAdapter.Fill|API> %d#, dataSet, srcTable, dataReader, startRecord, maxRecords\n", this.ObjectID);
			int result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (dataReader.IsClosed)
				{
					result = 0;
				}
				else
				{
					DataReaderContainer dataReader2 = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					result = this.FillFromReader(dataSet, null, srcTable, dataReader2, startRecord, maxRecords, null, null);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00235228 File Offset: 0x00234628
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			DataTable[] dataTables = new DataTable[]
			{
				dataTable
			};
			return this.Fill(dataTables, dataReader, 0, 0);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00235258 File Offset: 0x00234658
		protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<comm.DataAdapter.Fill|API> %d#, dataTables[], dataReader, startRecord, maxRecords\n", this.ObjectID);
			int result;
			try
			{
				ADP.CheckArgumentLength(dataTables, "tables");
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.NotSupported();
				}
				int num = 0;
				bool flag = false;
				DataSet dataSet = dataTables[0].DataSet;
				try
				{
					if (dataSet != null)
					{
						flag = dataSet.EnforceConstraints;
						dataSet.EnforceConstraints = false;
					}
					int num2 = 0;
					while (num2 < dataTables.Length && !dataReader.IsClosed)
					{
						DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
						if (dataReaderContainer.FieldCount > 0)
						{
							if (0 < num2 && !this.FillNextResult(dataReaderContainer))
							{
								break;
							}
							int num3 = this.FillFromReader(null, dataTables[num2], null, dataReaderContainer, startRecord, maxRecords, null, null);
							if (num2 == 0)
							{
								num = num3;
							}
						}
						num2++;
					}
				}
				catch (ConstraintException)
				{
					flag = false;
					throw;
				}
				finally
				{
					if (flag)
					{
						dataSet.EnforceConstraints = true;
					}
				}
				result = num;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x002353A8 File Offset: 0x002347A8
		internal int FillFromReader(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int startRecord, int maxRecords, DataColumn parentChapterColumn, object parentChapterValue)
		{
			int result = 0;
			int num = 0;
			do
			{
				if (0 < dataReader.FieldCount)
				{
					SchemaMapping schemaMapping = this.FillMapping(dataset, datatable, srcTable, dataReader, num, parentChapterColumn, parentChapterValue);
					num++;
					if (schemaMapping != null && schemaMapping.DataValues != null && schemaMapping.DataTable != null)
					{
						schemaMapping.DataTable.BeginLoadData();
						try
						{
							if (1 == num && (0 < startRecord || 0 < maxRecords))
							{
								result = this.FillLoadDataRowChunk(schemaMapping, startRecord, maxRecords);
							}
							else
							{
								int num2 = this.FillLoadDataRow(schemaMapping);
								if (1 == num)
								{
									result = num2;
								}
							}
						}
						finally
						{
							schemaMapping.DataTable.EndLoadData();
						}
						if (datatable != null)
						{
							break;
						}
					}
				}
			}
			while (this.FillNextResult(dataReader));
			return result;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00235468 File Offset: 0x00234868
		private int FillLoadDataRowChunk(SchemaMapping mapping, int startRecord, int maxRecords)
		{
			DataReaderContainer dataReader = mapping.DataReader;
			while (0 < startRecord)
			{
				if (!dataReader.Read())
				{
					return 0;
				}
				startRecord--;
			}
			int i = 0;
			if (0 < maxRecords)
			{
				while (i < maxRecords)
				{
					if (!dataReader.Read())
					{
						break;
					}
					if (this._hasFillErrorHandler)
					{
						try
						{
							mapping.LoadDataRowWithClear();
							i++;
							continue;
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							ADP.TraceExceptionForCapture(e);
							this.OnFillErrorHandler(e, mapping.DataTable, mapping.DataValues);
							continue;
						}
					}
					mapping.LoadDataRow();
					i++;
				}
			}
			else
			{
				i = this.FillLoadDataRow(mapping);
			}
			return i;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00235518 File Offset: 0x00234918
		private int FillLoadDataRow(SchemaMapping mapping)
		{
			int num = 0;
			DataReaderContainer dataReader = mapping.DataReader;
			if (this._hasFillErrorHandler)
			{
				while (dataReader.Read())
				{
					try
					{
						mapping.LoadDataRowWithClear();
						num++;
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionForCapture(e);
						this.OnFillErrorHandler(e, mapping.DataTable, mapping.DataValues);
					}
				}
			}
			else
			{
				while (dataReader.Read())
				{
					mapping.LoadDataRow();
					num++;
				}
			}
			return num;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x002355A8 File Offset: 0x002349A8
		private SchemaMapping FillMappingInternal(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			bool keyInfo = MissingSchemaAction.AddWithKey == this.MissingSchemaAction;
			string sourceTableName = null;
			if (dataset != null)
			{
				sourceTableName = DataAdapter.GetSourceTableName(srcTable, schemaCount);
			}
			return new SchemaMapping(this, dataset, datatable, dataReader, keyInfo, SchemaType.Mapped, sourceTableName, true, parentChapterColumn, parentChapterValue);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x002355E8 File Offset: 0x002349E8
		private SchemaMapping FillMapping(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			SchemaMapping result = null;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionForCapture(e);
					this.OnFillErrorHandler(e, null, null);
					return result;
				}
			}
			result = this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
			return result;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00235668 File Offset: 0x00234A68
		private bool FillNextResult(DataReaderContainer dataReader)
		{
			bool result = true;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return dataReader.NextResult();
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionForCapture(e);
					this.OnFillErrorHandler(e, null, null);
					return result;
				}
			}
			result = dataReader.NextResult();
			return result;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x002356C8 File Offset: 0x00234AC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IDataParameter[] GetFillParameters()
		{
			return new IDataParameter[0];
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x002356E8 File Offset: 0x00234AE8
		internal DataTableMapping GetTableMappingBySchemaAction(string sourceTableName, string dataSetTableName, MissingMappingAction mappingAction)
		{
			return DataTableMappingCollection.GetTableMappingBySchemaAction(this._tableMappings, sourceTableName, dataSetTableName, mappingAction);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00235708 File Offset: 0x00234B08
		internal int IndexOfDataSetTable(string dataSetTable)
		{
			if (this._tableMappings != null)
			{
				return this.TableMappings.IndexOfDataSetTable(dataSetTable);
			}
			return -1;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00235738 File Offset: 0x00234B38
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			FillErrorEventHandler fillErrorEventHandler = (FillErrorEventHandler)base.Events[DataAdapter.EventFillError];
			if (fillErrorEventHandler != null)
			{
				fillErrorEventHandler(this, value);
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00235768 File Offset: 0x00234B68
		private void OnFillErrorHandler(Exception e, DataTable dataTable, object[] dataValues)
		{
			FillErrorEventArgs fillErrorEventArgs = new FillErrorEventArgs(dataTable, dataValues);
			fillErrorEventArgs.Errors = e;
			this.OnFillError(fillErrorEventArgs);
			if (fillErrorEventArgs.Continue)
			{
				return;
			}
			if (fillErrorEventArgs.Errors != null)
			{
				throw fillErrorEventArgs.Errors;
			}
			throw e;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x002357A8 File Offset: 0x00234BA8
		public virtual int Update(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x002357C8 File Offset: 0x00234BC8
		private static DataTable[] AddDataTableToArray(DataTable[] tables, DataTable newTable)
		{
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] == newTable)
				{
					return tables;
				}
			}
			DataTable[] array = new DataTable[tables.Length + 1];
			for (int j = 0; j < tables.Length; j++)
			{
				array[j] = tables[j];
			}
			array[tables.Length] = newTable;
			return array;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00235818 File Offset: 0x00234C18
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04000B7C RID: 2940
		private static readonly object EventFillError = new object();

		// Token: 0x04000B7D RID: 2941
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04000B7E RID: 2942
		private bool _acceptChangesDuringUpdateAfterInsert = true;

		// Token: 0x04000B7F RID: 2943
		private bool _continueUpdateOnError;

		// Token: 0x04000B80 RID: 2944
		private bool _hasFillErrorHandler;

		// Token: 0x04000B81 RID: 2945
		private bool _returnProviderSpecificTypes;

		// Token: 0x04000B82 RID: 2946
		private bool _acceptChangesDuringFill = true;

		// Token: 0x04000B83 RID: 2947
		private LoadOption _fillLoadOption;

		// Token: 0x04000B84 RID: 2948
		private MissingMappingAction _missingMappingAction = MissingMappingAction.Passthrough;

		// Token: 0x04000B85 RID: 2949
		private MissingSchemaAction _missingSchemaAction = MissingSchemaAction.Add;

		// Token: 0x04000B86 RID: 2950
		private DataTableMappingCollection _tableMappings;

		// Token: 0x04000B87 RID: 2951
		private static int _objectTypeCount;

		// Token: 0x04000B88 RID: 2952
		internal readonly int _objectID = Interlocked.Increment(ref DataAdapter._objectTypeCount);
	}
}
