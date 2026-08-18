using System;
using System.ComponentModel;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x020002D5 RID: 725
	public class DataAdapter : Component, IDataAdapter
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x00122368 File Offset: 0x00121768
		[Conditional("DEBUG")]
		private void AssertReaderHandleFieldCount(DataReaderContainer readerHandler)
		{
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00122378 File Offset: 0x00121778
		[Conditional("DEBUG")]
		private void AssertSchemaMapping(SchemaMapping mapping)
		{
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x00122388 File Offset: 0x00121788
		protected DataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x001223D4 File Offset: 0x001217D4
		protected DataAdapter(DataAdapter from)
		{
			this.CloneFrom(from);
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x00122424 File Offset: 0x00121824
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00122438 File Offset: 0x00121838
		[DefaultValue(true)]
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DataAdapter_AcceptChangesDuringFill")]
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

		// Token: 0x06002CEB RID: 11499 RVA: 0x0012244C File Offset: 0x0012184C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return this._fillLoadOption == (LoadOption)0;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x00122464 File Offset: 0x00121864
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x00122478 File Offset: 0x00121878
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x0012248C File Offset: 0x0012188C
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x001224A0 File Offset: 0x001218A0
		[DefaultValue(false)]
		[ResCategory("DataCategory_Update")]
		[ResDescription("DataAdapter_ContinueUpdateOnError")]
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

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x001224B4 File Offset: 0x001218B4
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x001224D4 File Offset: 0x001218D4
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
				if (value <= LoadOption.Upsert)
				{
					this._fillLoadOption = value;
					return;
				}
				throw ADP.InvalidLoadOption(value);
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x001224F4 File Offset: 0x001218F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this._fillLoadOption = (LoadOption)0;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00122508 File Offset: 0x00121908
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return this._fillLoadOption > (LoadOption)0;
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x00122520 File Offset: 0x00121920
		// (set) Token: 0x06002CF5 RID: 11509 RVA: 0x00122534 File Offset: 0x00121934
		[DefaultValue(MissingMappingAction.Passthrough)]
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataAdapter_MissingMappingAction")]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
			set
			{
				if (value - MissingMappingAction.Passthrough <= 2)
				{
					this._missingMappingAction = value;
					return;
				}
				throw ADP.InvalidMissingMappingAction(value);
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x00122558 File Offset: 0x00121958
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x0012256C File Offset: 0x0012196C
		[ResCategory("DataCategory_Mapping")]
		[DefaultValue(MissingSchemaAction.Add)]
		[ResDescription("DataAdapter_MissingSchemaAction")]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this._missingSchemaAction;
			}
			set
			{
				if (value - MissingSchemaAction.Add <= 3)
				{
					this._missingSchemaAction = value;
					return;
				}
				throw ADP.InvalidMissingSchemaAction(value);
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x00122590 File Offset: 0x00121990
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x001225A4 File Offset: 0x001219A4
		// (set) Token: 0x06002CFA RID: 11514 RVA: 0x001225B8 File Offset: 0x001219B8
		[ResCategory("DataCategory_Fill")]
		[DefaultValue(false)]
		[ResDescription("DataAdapter_ReturnProviderSpecificTypes")]
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

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x001225CC File Offset: 0x001219CC
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

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x001225FC File Offset: 0x001219FC
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x00122610 File Offset: 0x00121A10
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x00122620 File Offset: 0x00121A20
		protected bool HasTableMappings()
		{
			return this._tableMappings != null && 0 < this.TableMappings.Count;
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002CFF RID: 11519 RVA: 0x00122648 File Offset: 0x00121A48
		// (remove) Token: 0x06002D00 RID: 11520 RVA: 0x00122670 File Offset: 0x00121A70
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

		// Token: 0x06002D01 RID: 11521 RVA: 0x00122690 File Offset: 0x00121A90
		[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected virtual DataAdapter CloneInternals()
		{
			DataAdapter dataAdapter = (DataAdapter)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			dataAdapter.CloneFrom(this);
			return dataAdapter;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x001226C0 File Offset: 0x00121AC0
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

		// Token: 0x06002D03 RID: 11523 RVA: 0x001227B4 File Offset: 0x00121BB4
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			Bid.Trace("<comm.DataAdapter.CreateTableMappings|API> %d#\n", this.ObjectID);
			return new DataTableMappingCollection();
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x001227D8 File Offset: 0x00121BD8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._tableMappings = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x001227F8 File Offset: 0x00121BF8
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0012280C File Offset: 0x00121C0C
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

		// Token: 0x06002D07 RID: 11527 RVA: 0x001228B0 File Offset: 0x00121CB0
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

		// Token: 0x06002D08 RID: 11528 RVA: 0x0012293C File Offset: 0x00121D3C
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

		// Token: 0x06002D09 RID: 11529 RVA: 0x001229DC File Offset: 0x00121DDC
		public virtual int Fill(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x001229F0 File Offset: 0x00121DF0
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

		// Token: 0x06002D0B RID: 11531 RVA: 0x00122AB4 File Offset: 0x00121EB4
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			DataTable[] dataTables = new DataTable[]
			{
				dataTable
			};
			return this.Fill(dataTables, dataReader, 0, 0);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x00122AD8 File Offset: 0x00121ED8
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
							goto IL_B3;
						}
						if (num2 == 0)
						{
							bool flag2;
							do
							{
								flag2 = this.FillNextResult(dataReaderContainer);
							}
							while (flag2 && dataReaderContainer.FieldCount <= 0);
							if (flag2)
							{
								goto IL_B3;
							}
							break;
						}
						IL_DA:
						num2++;
						continue;
						IL_B3:
						if (0 < num2 && !this.FillNextResult(dataReaderContainer))
						{
							break;
						}
						int num3 = this.FillFromReader(null, dataTables[num2], null, dataReaderContainer, startRecord, maxRecords, null, null);
						if (num2 == 0)
						{
							num = num3;
							goto IL_DA;
						}
						goto IL_DA;
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

		// Token: 0x06002D0D RID: 11533 RVA: 0x00122C3C File Offset: 0x0012203C
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

		// Token: 0x06002D0E RID: 11534 RVA: 0x00122CF0 File Offset: 0x001220F0
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

		// Token: 0x06002D0F RID: 11535 RVA: 0x00122D98 File Offset: 0x00122198
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

		// Token: 0x06002D10 RID: 11536 RVA: 0x00122E24 File Offset: 0x00122224
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

		// Token: 0x06002D11 RID: 11537 RVA: 0x00122E5C File Offset: 0x0012225C
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

		// Token: 0x06002D12 RID: 11538 RVA: 0x00122ED4 File Offset: 0x001222D4
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

		// Token: 0x06002D13 RID: 11539 RVA: 0x00122F34 File Offset: 0x00122334
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IDataParameter[] GetFillParameters()
		{
			return new IDataParameter[0];
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x00122F48 File Offset: 0x00122348
		internal DataTableMapping GetTableMappingBySchemaAction(string sourceTableName, string dataSetTableName, MissingMappingAction mappingAction)
		{
			return DataTableMappingCollection.GetTableMappingBySchemaAction(this._tableMappings, sourceTableName, dataSetTableName, mappingAction);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x00122F64 File Offset: 0x00122364
		internal int IndexOfDataSetTable(string dataSetTable)
		{
			if (this._tableMappings != null)
			{
				return this.TableMappings.IndexOfDataSetTable(dataSetTable);
			}
			return -1;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x00122F88 File Offset: 0x00122388
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			FillErrorEventHandler fillErrorEventHandler = (FillErrorEventHandler)base.Events[DataAdapter.EventFillError];
			if (fillErrorEventHandler != null)
			{
				fillErrorEventHandler(this, value);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x00122FB8 File Offset: 0x001223B8
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

		// Token: 0x06002D18 RID: 11544 RVA: 0x00122FF4 File Offset: 0x001223F4
		public virtual int Update(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x00123008 File Offset: 0x00122408
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

		// Token: 0x06002D1A RID: 11546 RVA: 0x00123054 File Offset: 0x00122454
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04001C30 RID: 7216
		private static readonly object EventFillError = new object();

		// Token: 0x04001C31 RID: 7217
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04001C32 RID: 7218
		private bool _acceptChangesDuringUpdateAfterInsert = true;

		// Token: 0x04001C33 RID: 7219
		private bool _continueUpdateOnError;

		// Token: 0x04001C34 RID: 7220
		private bool _hasFillErrorHandler;

		// Token: 0x04001C35 RID: 7221
		private bool _returnProviderSpecificTypes;

		// Token: 0x04001C36 RID: 7222
		private bool _acceptChangesDuringFill = true;

		// Token: 0x04001C37 RID: 7223
		private LoadOption _fillLoadOption;

		// Token: 0x04001C38 RID: 7224
		private MissingMappingAction _missingMappingAction = MissingMappingAction.Passthrough;

		// Token: 0x04001C39 RID: 7225
		private MissingSchemaAction _missingSchemaAction = MissingSchemaAction.Add;

		// Token: 0x04001C3A RID: 7226
		private DataTableMappingCollection _tableMappings;

		// Token: 0x04001C3B RID: 7227
		private static int _objectTypeCount;

		// Token: 0x04001C3C RID: 7228
		internal readonly int _objectID = Interlocked.Increment(ref DataAdapter._objectTypeCount);
	}
}
