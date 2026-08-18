using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000110 RID: 272
	[TypeConverter("Oracle.VsDevTools.OracleVSGOracleParameterTypeConverter, Oracle.VsDevTools, Version=4.112.3.0, Culture=neutral, PublicKeyToken=89b483f429c47342, processorArchitecture=X86")]
	public sealed class OracleParameter : DbParameter, IDisposable, ICloneable
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x00062C93 File Offset: 0x00061C93
		static OracleParameter()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00062CA1 File Offset: 0x00061CA1
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00062CCC File Offset: 0x00061CCC
		[Description("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Data")]
		public override DbType DbType
		{
			get
			{
				if (!this.m_bSetDbType)
				{
					this.m_dbType = (DbType)OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[(int)this.m_oraDbType];
					this.m_bSetDbType = true;
				}
				return this.m_dbType;
			}
			set
			{
				if (value == DbType.Boolean || value == DbType.Currency || value == DbType.Guid || value == DbType.SByte || value == DbType.UInt16 || value == DbType.UInt32 || value == DbType.UInt64 || value == DbType.VarNumeric)
				{
					throw new ArgumentException();
				}
				if (value < DbType.AnsiString || value > DbType.StringFixedLength)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_dbType = value;
				this.m_oraDbType = (OracleDbType)OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[(int)value];
				this.m_bSetDbType = true;
				this.m_modified = true;
				this.m_enumType = PrmEnumType.DBTYPE;
				this.m_bOracleDbTypeExSet = false;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00062D46 File Offset: 0x00061D46
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00062D4E File Offset: 0x00061D4E
		[DefaultValue(false)]
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this.m_sourceColumnNullMapping;
			}
			set
			{
				this.m_sourceColumnNullMapping = value;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00062D57 File Offset: 0x00061D57
		private bool ShouldSerializeDbType()
		{
			return this.m_enumType == PrmEnumType.DBTYPE;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00062D62 File Offset: 0x00061D62
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00062D6A File Offset: 0x00061D6A
		[Category("Data")]
		[DefaultValue(ParameterDirection.Input)]
		[Description("")]
		public override ParameterDirection Direction
		{
			get
			{
				return this.m_direction;
			}
			set
			{
				if (value != ParameterDirection.Input && value != ParameterDirection.Output && value != ParameterDirection.InputOutput && value != ParameterDirection.ReturnValue)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_direction = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00062D90 File Offset: 0x00061D90
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00062D98 File Offset: 0x00061D98
		[DefaultValue(false)]
		[Category("Data")]
		[Description("")]
		public override bool IsNullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				this.m_nullable = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00062DA8 File Offset: 0x00061DA8
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00062DB0 File Offset: 0x00061DB0
		[Browsable(false)]
		[DefaultValue(0)]
		public int Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_offset = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00062DCA File Offset: 0x00061DCA
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00062DD2 File Offset: 0x00061DD2
		[Browsable(false)]
		[DbProviderSpecificTypeProperty(true)]
		[Category("Data")]
		[Description("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OracleDbType OracleDbTypeEx
		{
			get
			{
				return this.m_oraDbType;
			}
			set
			{
				this.OracleDbType = value;
				this.m_bOracleDbTypeExSet = true;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00062DE2 File Offset: 0x00061DE2
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x00062DEC File Offset: 0x00061DEC
		[Category("Data")]
		[Description("")]
		public OracleDbType OracleDbType
		{
			get
			{
				return this.m_oraDbType;
			}
			set
			{
				if (this.m_oraDbType != value)
				{
					if (value < OracleDbType.BFile || value > OracleDbType.BinaryFloat)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.m_oraDbType = value;
				}
				this.m_bSetDbType = false;
				this.m_enumType = PrmEnumType.ORADBTYPE;
				this.m_bOracleDbTypeExSet = false;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00062E33 File Offset: 0x00061E33
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00062E3B File Offset: 0x00061E3B
		internal PrmEnumType ParameterEnumType
		{
			get
			{
				return this.m_enumType;
			}
			set
			{
				if (this.m_enumType != value)
				{
					this.m_enumType = value;
				}
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00062E4D File Offset: 0x00061E4D
		private bool ShouldSerializeOracleDbType()
		{
			return this.m_enumType != PrmEnumType.DBTYPE;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00062E5B File Offset: 0x00061E5B
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x00062E71 File Offset: 0x00061E71
		[DefaultValue("")]
		public override string ParameterName
		{
			get
			{
				if (this.m_paramName != null)
				{
					return this.m_paramName;
				}
				return string.Empty;
			}
			set
			{
				this.m_paramName = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00062E7A File Offset: 0x00061E7A
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00062E8E File Offset: 0x00061E8E
		[DefaultValue(0)]
		[Category("Data")]
		[Description("")]
		public new byte Precision
		{
			get
			{
				if (this.m_precision == 100)
				{
					return 0;
				}
				return this.m_precision;
			}
			set
			{
				this.m_precision = value;
				this.m_modified = true;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00062E9E File Offset: 0x00061E9E
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00062EB5 File Offset: 0x00061EB5
		[DefaultValue(0)]
		[Description("")]
		[Category("Data")]
		public new byte Scale
		{
			get
			{
				if (this.m_scale == 129)
				{
					return 0;
				}
				return this.m_scale;
			}
			set
			{
				this.m_scale = value;
				this.m_modified = true;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00062EC5 File Offset: 0x00061EC5
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x00062EE8 File Offset: 0x00061EE8
		[DefaultValue(0)]
		[Category("Data")]
		[Description("")]
		public override int Size
		{
			get
			{
				if (this.m_curSize != -1)
				{
					return this.m_curSize;
				}
				if (this.m_maxSize != -1)
				{
					return this.m_maxSize;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value != 0)
				{
					this.m_maxSize = value;
				}
				else
				{
					this.m_maxSize = -1;
				}
				this.m_curSize = -1;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00062F0E File Offset: 0x00061F0E
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x00062F45 File Offset: 0x00061F45
		[DefaultValue(null)]
		[Browsable(false)]
		public int[] ArrayBindSize
		{
			get
			{
				if (this.m_curArrayBindSize != null && this.m_curArrayBindSize[0] != -1)
				{
					return this.m_curArrayBindSize;
				}
				if (this.m_maxArrayBindSize != null && this.m_maxArrayBindSize[0] != -1)
				{
					return this.m_maxArrayBindSize;
				}
				return null;
			}
			set
			{
				this.m_maxArrayBindSize = value;
				this.m_curArrayBindSize = null;
				this.m_modified = true;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00062F5C File Offset: 0x00061F5C
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00062F72 File Offset: 0x00061F72
		[DefaultValue("")]
		[Description("")]
		[Category("Data")]
		public override string SourceColumn
		{
			get
			{
				if (this.m_sourceColumn != null)
				{
					return this.m_sourceColumn;
				}
				return string.Empty;
			}
			set
			{
				this.m_sourceColumn = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00062F7B File Offset: 0x00061F7B
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00062F83 File Offset: 0x00061F83
		[Category("Data")]
		[DefaultValue(DataRowVersion.Current)]
		[Description("")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this.m_sourceVersion;
			}
			set
			{
				if (value != DataRowVersion.Original && value != DataRowVersion.Current && value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_sourceVersion = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00062FB2 File Offset: 0x00061FB2
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00062FBA File Offset: 0x00061FBA
		[Browsable(false)]
		[DefaultValue(OracleParameterStatus.Success)]
		public OracleParameterStatus Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				if (value != OracleParameterStatus.Success && value != OracleParameterStatus.NullInsert && value != OracleParameterStatus.NullFetched && value != OracleParameterStatus.Truncation)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_status = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00062FDF File Offset: 0x00061FDF
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00062FF1 File Offset: 0x00061FF1
		[DefaultValue(null)]
		[Browsable(false)]
		public OracleParameterStatus[] ArrayBindStatus
		{
			get
			{
				if (this.m_arrayBindStatus != null)
				{
					return this.m_arrayBindStatus;
				}
				return null;
			}
			set
			{
				this.m_arrayBindStatus = value;
				this.m_modified = true;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00063001 File Offset: 0x00062001
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00063009 File Offset: 0x00062009
		[DefaultValue(OracleCollectionType.None)]
		[Browsable(false)]
		public OracleCollectionType CollectionType
		{
			get
			{
				return this.m_collType;
			}
			set
			{
				if (value != OracleCollectionType.None && value != OracleCollectionType.PLSQLAssociativeArray)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_collType = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0006301F File Offset: 0x0006201F
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00063028 File Offset: 0x00062028
		[DefaultValue(null)]
		[Description("")]
		[Category("Data")]
		public override object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				if (value != null && value != DBNull.Value && this.m_enumType == PrmEnumType.NOTSET)
				{
					Type type = value.GetType();
					if (type == typeof(bool) || type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
					{
						throw new ArgumentException();
					}
					object obj = OraDb_DbTypeTable.s_table[type];
					if (obj != null)
					{
						this.m_oraDbType = (OracleDbType)obj;
					}
					else
					{
						if (!(value is IOracleCustomType))
						{
							throw new ArgumentException();
						}
						this.m_oraDbType = OracleDbType.Object;
					}
					this.m_bSetDbType = false;
					this.m_enumType = PrmEnumType.VALUE;
				}
				this.m_value = value;
				this.m_modifedAfterBind = true;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0006310D File Offset: 0x0006210D
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00063123 File Offset: 0x00062123
		[Category("Data")]
		[DefaultValue("")]
		[Description("")]
		public string UdtTypeName
		{
			get
			{
				if (this.m_udtTypeName != null)
				{
					return this.m_udtTypeName;
				}
				return string.Empty;
			}
			set
			{
				if (this.m_udtTypeName != value)
				{
					this.m_udtTypeName = value;
				}
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0006313C File Offset: 0x0006213C
		public OracleParameter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(1)\n"
				});
			}
			this.m_enumType = PrmEnumType.NOTSET;
			this.m_direction = ParameterDirection.Input;
			this.m_oraDbType = OracleDbType.Varchar2;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_maxSize = -1;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(1)\n"
				});
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000631E4 File Offset: 0x000621E4
		public OracleParameter(string parameterName, OracleDbType oraType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(2)\n"
				});
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_direction = ParameterDirection.Input;
			if (oraType < OracleDbType.BFile || oraType > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_oraDbType = oraType;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_maxSize = -1;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(2)\n"
				});
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000632A8 File Offset: 0x000622A8
		public OracleParameter(string parameterName, object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(3)\n"
				});
			}
			if (obj != null && obj != DBNull.Value)
			{
				Type type = obj.GetType();
				if (type == typeof(bool) || type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
				{
					throw new ArgumentException();
				}
				this.m_oraDbType = (OracleDbType)OraDb_DbTypeTable.s_table[type];
				this.m_enumType = PrmEnumType.VALUE;
				this.m_value = obj;
			}
			else
			{
				this.m_oraDbType = OracleDbType.Varchar2;
				this.m_enumType = PrmEnumType.NOTSET;
			}
			this.m_direction = ParameterDirection.Input;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_maxSize = -1;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(3)\n"
				});
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000633F4 File Offset: 0x000623F4
		public OracleParameter(string parameterName, OracleDbType type, ParameterDirection direction)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(4)\n"
				});
			}
			if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_direction = direction;
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_oraDbType = type;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_maxSize = -1;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(4)\n"
				});
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000634D4 File Offset: 0x000624D4
		public OracleParameter(string parameterName, OracleDbType type, int size, object obj, ParameterDirection direction)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(5)\n"
				});
			}
			if (size < 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = direction;
			this.m_paramName = parameterName;
			this.m_value = obj;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(5)\n"
				});
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000635E0 File Offset: 0x000625E0
		public OracleParameter(string parameterName, OracleDbType type, object obj, ParameterDirection direction)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(5)\n"
				});
			}
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = direction;
			this.m_paramName = parameterName;
			this.m_value = obj;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_maxSize = -1;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(5)\n"
				});
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000636CC File Offset: 0x000626CC
		public OracleParameter(string parameterName, OracleDbType type, int size)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(6)\n"
				});
			}
			if (size < 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = ParameterDirection.Input;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(6)\n"
				});
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000637AC File Offset: 0x000627AC
		public OracleParameter(string parameterName, OracleDbType type, int size, string srcColumn)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(7)\n"
				});
			}
			if (size < 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = ParameterDirection.Input;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			this.m_sourceColumn = srcColumn;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(7)\n"
				});
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00063894 File Offset: 0x00062894
		internal OracleParameter(string parameterName, OracleDbType type, int size, string srcColumn, DataRowVersion version)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(8)\n"
				});
			}
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (version != DataRowVersion.Original && version != DataRowVersion.Current && version != DataRowVersion.Proposed && version != DataRowVersion.Default)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = ParameterDirection.Input;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			this.m_sourceColumn = srcColumn;
			this.m_sourceVersion = version;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(8)\n"
				});
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000639A4 File Offset: 0x000629A4
		internal OracleParameter(string parameterName, OracleDbType type, int size, string srcColumn, DataRowVersion version, object obj)
		{
			if (type < OracleDbType.BFile || type > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (version != DataRowVersion.Original && version != DataRowVersion.Current && version != DataRowVersion.Proposed && version != DataRowVersion.Default)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = type;
			this.m_direction = ParameterDirection.Input;
			this.m_paramName = parameterName;
			this.m_precision = 100;
			this.m_scale = 129;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			this.m_sourceColumn = srcColumn;
			this.m_sourceVersion = version;
			this.m_value = obj;
			if (size != 0)
			{
				this.m_maxSize = size;
				return;
			}
			this.m_maxSize = -1;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00063A84 File Offset: 0x00062A84
		public OracleParameter(string parameterName, OracleDbType oraType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::OracleParameter(9)\n"
				});
			}
			if (size < 0)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (oraType < OracleDbType.BFile || oraType > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (srcVersion != DataRowVersion.Original && srcVersion != DataRowVersion.Current && srcVersion != DataRowVersion.Proposed && srcVersion != DataRowVersion.Default)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = PrmEnumType.ORADBTYPE;
			this.m_oraDbType = oraType;
			this.m_direction = direction;
			this.m_paramName = parameterName;
			this.m_precision = precision;
			this.m_scale = scale;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			this.m_sourceColumn = srcColumn;
			this.m_sourceVersion = srcVersion;
			this.m_value = obj;
			this.m_nullable = isNullable;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::OracleParameter(9)\n"
				});
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00063BD4 File Offset: 0x00062BD4
		internal OracleParameter(DbType type, ParameterDirection direction, bool isNullable, int offSet, OracleDbType oraDbType, string paramName, byte precision, byte scale, int size, string srcColumn, DataRowVersion srcVersion, OracleParameterStatus paramStatus, object obj, bool bSetDbType, PrmEnumType enumType, bool modified, string udtTypeName)
		{
			if (direction != ParameterDirection.Input && direction != ParameterDirection.Output && direction != ParameterDirection.InputOutput && direction != ParameterDirection.ReturnValue)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (oraDbType < OracleDbType.BFile || oraDbType > OracleDbType.BinaryFloat)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			if (srcVersion != DataRowVersion.Original && srcVersion != DataRowVersion.Current && srcVersion != DataRowVersion.Proposed && srcVersion != DataRowVersion.Default)
			{
				GC.SuppressFinalize(this);
				throw new ArgumentOutOfRangeException();
			}
			this.m_enumType = enumType;
			this.m_modified = modified;
			this.m_oraDbType = oraDbType;
			this.m_direction = direction;
			this.m_paramName = paramName;
			this.m_precision = precision;
			this.m_scale = scale;
			this.m_sourceVersion = DataRowVersion.Current;
			this.m_curSize = -1;
			this.m_sourceColumn = srcColumn;
			this.m_sourceVersion = srcVersion;
			this.m_value = obj;
			this.m_nullable = isNullable;
			this.m_offset = offSet;
			this.m_status = paramStatus;
			this.m_udtTypeName = udtTypeName;
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			if (bSetDbType)
			{
				this.m_dbType = type;
				this.m_bSetDbType = true;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00063D0C File Offset: 0x00062D0C
		public object Clone()
		{
			OracleParameter oracleParameter;
			if (this.m_value != null && this.m_value.GetType().IsArray)
			{
				oracleParameter = new OracleParameter(this.m_dbType, this.m_direction, this.m_nullable, this.m_offset, this.m_oraDbType, this.m_paramName, this.m_precision, this.m_scale, this.m_maxSize, this.m_sourceColumn, this.m_sourceVersion, this.m_status, ((Array)this.m_value).Clone(), this.m_bSetDbType, this.m_enumType, this.m_modified, this.m_udtTypeName);
			}
			else
			{
				oracleParameter = new OracleParameter(this.m_dbType, this.m_direction, this.m_nullable, this.m_offset, this.m_oraDbType, this.m_paramName, this.m_precision, this.m_scale, this.m_maxSize, this.m_sourceColumn, this.m_sourceVersion, this.m_status, this.m_value, this.m_bSetDbType, this.m_enumType, this.m_modified, this.m_udtTypeName);
			}
			oracleParameter.m_collType = this.m_collType;
			oracleParameter.m_bOracleDbTypeExSet = this.m_bOracleDbTypeExSet;
			oracleParameter.m_bReturnDateTimeOffset = this.m_bReturnDateTimeOffset;
			return oracleParameter;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00063E41 File Offset: 0x00062E41
		public override string ToString()
		{
			if (this.m_paramName != null)
			{
				return this.m_paramName;
			}
			return string.Empty;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00063E58 File Offset: 0x00062E58
		public void Dispose()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleParameter::Dispose()\n"
				});
			}
			if (this.m_pDataBuffer != IntPtr.Zero)
			{
				try
				{
					Marshal.FreeCoTaskMem(this.m_pDataBuffer);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_pDataBuffer = IntPtr.Zero;
			}
			if (!this.m_disposed)
			{
				this.m_modified = true;
				this.m_disposed = true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleParameter::Dispose()\n"
				});
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00063F00 File Offset: 0x00062F00
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_modified = true;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00063F1B File Offset: 0x00062F1B
		public override void ResetDbType()
		{
			this.m_enumType = PrmEnumType.NOTSET;
			this.DbType = DbType.String;
			this.OracleDbType = OracleDbType.Varchar2;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00063F34 File Offset: 0x00062F34
		public void ResetOracleDbType()
		{
			this.m_enumType = PrmEnumType.NOTSET;
			this.DbType = DbType.String;
			this.OracleDbType = OracleDbType.Varchar2;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00063F50 File Offset: 0x00062F50
		internal unsafe int ResetCtx(int arraySize)
		{
			int num = 0;
			this.m_curSize = -1;
			if (this.m_bArrayBind)
			{
				if (this.m_collType == OracleCollectionType.PLSQLAssociativeArray)
				{
					if (this.m_direction == ParameterDirection.Input)
					{
						if (this.m_value != null && this.m_value != DBNull.Value && this.m_value is Array)
						{
							if (this.m_maxSize > 0 && ((Array)this.m_value).Length > this.m_maxSize)
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = (this.m_bindElemCount = this.m_maxSize)));
							}
							else
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = (this.m_bindElemCount = ((Array)this.m_value).Length)));
							}
						}
						else
						{
							if (this.m_arrayBindStatus == null)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									"OracleParameter.Value"
								}));
							}
							this.m_bindElemCount = 0;
							if (this.m_maxSize > 0 && this.m_arrayBindStatus.Length > this.m_maxSize)
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = this.m_maxSize));
							}
							else
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = this.m_arrayBindStatus.Length));
							}
						}
						if (this.m_arrBindCount == 0)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								"OracleParameter.Value"
							}));
						}
					}
					else if (this.m_direction == ParameterDirection.InputOutput)
					{
						if (this.m_maxSize <= 0)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								"OracleParameter.Size"
							}));
						}
						if (this.m_value != null && this.m_value != DBNull.Value && this.m_value is Array)
						{
							if (((Array)this.m_value).Length > this.m_maxSize)
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_bindElemCount = this.m_maxSize);
							}
							else
							{
								this.m_pOpoPrmValCtx->curelep = (this.m_bindElemCount = ((Array)this.m_value).Length);
							}
						}
						else
						{
							if (this.m_arrayBindStatus == null)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									"OracleParameter.Value"
								}));
							}
							this.m_bindElemCount = 0;
							if (this.m_arrayBindStatus.Length > this.m_maxSize)
							{
								this.m_pOpoPrmValCtx->curelep = this.m_maxSize;
							}
							else
							{
								this.m_pOpoPrmValCtx->curelep = this.m_arrayBindStatus.Length;
							}
						}
						this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = this.m_maxSize);
					}
					else
					{
						if (this.m_maxSize <= 0)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								"OracleParameter.Size"
							}));
						}
						this.m_bindElemCount = 0;
						this.m_pOpoPrmValCtx->curelep = 0;
						this.m_pOpoPrmValCtx->maxarr_len = (this.m_arrBindCount = this.m_maxSize);
					}
					arraySize = this.m_arrBindCount;
				}
				else
				{
					this.m_arrBindCount = (this.m_bindElemCount = arraySize);
				}
			}
			else
			{
				this.m_arrBindCount = (this.m_bindElemCount = 1);
			}
			if (arraySize > this.m_pOpoPrmValCtx->NumArrBindElems)
			{
				try
				{
					num = OpsPrm.ReAllocValCtx(this.m_pOpoPrmValCtx, arraySize);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num != 0)
				{
					return num;
				}
			}
			if (this.m_bArrayBind)
			{
				if (this.m_direction != ParameterDirection.Input && (this.OracleDbType == OracleDbType.Char || this.OracleDbType == OracleDbType.Varchar2 || this.OracleDbType == OracleDbType.Raw || this.OracleDbType == OracleDbType.Long || this.OracleDbType == OracleDbType.NChar || this.OracleDbType == OracleDbType.NVarchar2 || this.OracleDbType == OracleDbType.LongRaw) && (this.m_maxArrayBindSize == null || (this.m_maxArrayBindSize != null && this.m_maxArrayBindSize.Length < arraySize)))
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						"OracleParameter.ArrayBindSize"
					}));
				}
				if (this.m_curArrayBindSize == null || this.m_curArrayBindSize.Length != arraySize)
				{
					this.m_curArrayBindSize = new int[arraySize];
					for (int i = 0; i < arraySize; i++)
					{
						this.m_curArrayBindSize[i] = -1;
					}
				}
				if (this.m_maxArrayBindSize == null)
				{
					this.m_maxArrayBindSize = new int[arraySize];
					for (int i = 0; i < arraySize; i++)
					{
						this.m_maxArrayBindSize[i] = -1;
					}
				}
				else if (this.m_maxArrayBindSize.Length < arraySize)
				{
					int[] array = new int[arraySize];
					int i;
					for (i = 0; i < this.m_maxArrayBindSize.Length; i++)
					{
						array[i] = this.m_maxArrayBindSize[i];
					}
					while (i < arraySize)
					{
						array[i] = -1;
						i++;
					}
					this.m_maxArrayBindSize = array;
				}
				if (this.m_arrayBindStatus == null)
				{
					this.m_arrayBindStatus = new OracleParameterStatus[arraySize];
					for (int i = 0; i < arraySize; i++)
					{
						this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
					}
				}
				else if (this.m_arrayBindStatus.Length < arraySize)
				{
					OracleParameterStatus[] array2 = new OracleParameterStatus[arraySize];
					int i;
					for (i = 0; i < this.m_arrayBindStatus.Length; i++)
					{
						array2[i] = this.m_arrayBindStatus[i];
					}
					while (i < arraySize)
					{
						array2[i] = OracleParameterStatus.Success;
						i++;
					}
					this.m_arrayBindStatus = array2;
				}
			}
			this.SetStatus(arraySize);
			return 0;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000644FC File Offset: 0x000634FC
		internal void SetSize(int size)
		{
			if (size != 0)
			{
				this.m_maxSize = size;
			}
			else
			{
				this.m_maxSize = -1;
			}
			this.m_curSize = -1;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00064518 File Offset: 0x00063518
		private bool IsProviderSpecificNullValue(object value)
		{
			bool result = false;
			switch (this.m_oraDbType)
			{
			case OracleDbType.BFile:
			{
				OracleBFile oracleBFile;
				if ((oracleBFile = (value as OracleBFile)) != null && oracleBFile.IsNull)
				{
					result = true;
				}
				break;
			}
			case OracleDbType.Blob:
			{
				OracleBlob oracleBlob;
				if (((oracleBlob = (value as OracleBlob)) != null && oracleBlob.IsNull) || (value is OracleBinary && ((OracleBinary)value).IsNull))
				{
					result = true;
				}
				break;
			}
			case OracleDbType.Byte:
				if ((value is OracleDecimal && ((OracleDecimal)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			case OracleDbType.Char:
			case OracleDbType.Long:
			case OracleDbType.NChar:
			case OracleDbType.NVarchar2:
			case OracleDbType.Varchar2:
				if (value is OracleString && ((OracleString)value).IsNull)
				{
					result = true;
				}
				break;
			case OracleDbType.Clob:
			case OracleDbType.NClob:
			{
				OracleClob oracleClob;
				if (((oracleClob = (value as OracleClob)) != null && oracleClob.IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			}
			case OracleDbType.Date:
			case OracleDbType.TimeStamp:
			case OracleDbType.TimeStampLTZ:
			case OracleDbType.TimeStampTZ:
				if ((value is OracleDate && ((OracleDate)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull) || (value is OracleTimeStamp && ((OracleTimeStamp)value).IsNull) || (value is OracleTimeStampTZ && ((OracleTimeStampTZ)value).IsNull) || (value is OracleTimeStampLTZ && ((OracleTimeStampLTZ)value).IsNull))
				{
					result = true;
				}
				break;
			case OracleDbType.Decimal:
			case OracleDbType.Double:
			case OracleDbType.Int16:
			case OracleDbType.Int32:
			case OracleDbType.Int64:
			case OracleDbType.Single:
			case OracleDbType.BinaryDouble:
			case OracleDbType.BinaryFloat:
				if ((value is OracleDecimal && ((OracleDecimal)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
				if (value is OracleBinary && ((OracleBinary)value).IsNull)
				{
					result = true;
				}
				break;
			case OracleDbType.IntervalDS:
				if ((value is OracleIntervalDS && ((OracleIntervalDS)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			case OracleDbType.IntervalYM:
				if ((value is OracleIntervalYM && ((OracleIntervalYM)value).IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			case OracleDbType.RefCursor:
			{
				OracleRefCursor oracleRefCursor;
				if ((oracleRefCursor = (value as OracleRefCursor)) != null && oracleRefCursor.IsNull)
				{
					result = true;
				}
				break;
			}
			case OracleDbType.XmlType:
			{
				OracleXmlType oracleXmlType;
				OracleClob oracleClob2;
				if (((oracleXmlType = (value as OracleXmlType)) != null && oracleXmlType.IsNull) || ((oracleClob2 = (value as OracleClob)) != null && oracleClob2.IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			}
			case OracleDbType.Object:
				if (value is INullable && ((INullable)value).IsNull)
				{
					result = true;
				}
				break;
			case OracleDbType.Ref:
			{
				OracleRef oracleRef;
				if (((oracleRef = (value as OracleRef)) != null && oracleRef.IsNull) || (value is OracleString && ((OracleString)value).IsNull))
				{
					result = true;
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000648E4 File Offset: 0x000638E4
		internal unsafe void SetStatus(int arraySize)
		{
			if (this.m_value == null)
			{
				this.m_value = DBNull.Value;
			}
			if (this.m_bArrayBind)
			{
				if (this.m_direction != ParameterDirection.Input && this.m_direction != ParameterDirection.InputOutput)
				{
					for (int i = 0; i < arraySize; i++)
					{
						this.m_pOpoPrmValCtx->pInd[i] = 0;
						this.m_pOpoPrmValCtx->pSrcInd[i] = 0;
					}
					return;
				}
				if (this.m_value == DBNull.Value || this.IsProviderSpecificNullValue(this.m_value))
				{
					for (int i = 0; i < arraySize; i++)
					{
						this.m_pOpoPrmValCtx->pInd[i] = -1;
						this.m_pOpoPrmValCtx->pSrcInd[i] = -1;
					}
					return;
				}
				int length = ((Array)this.m_value).Length;
				for (int i = 0; i < arraySize; i++)
				{
					if (this.m_arrayBindStatus[i] == OracleParameterStatus.NullInsert)
					{
						this.m_pOpoPrmValCtx->pInd[i] = -1;
						this.m_pOpoPrmValCtx->pSrcInd[i] = -1;
					}
					else if (length > i && (((Array)this.m_value).GetValue(i) == null || ((Array)this.m_value).GetValue(i) == DBNull.Value || (((Array)this.m_value).GetValue(i) is INullable && ((INullable)((Array)this.m_value).GetValue(i)).IsNull)))
					{
						this.m_pOpoPrmValCtx->pInd[i] = -1;
						this.m_pOpoPrmValCtx->pSrcInd[i] = -1;
					}
					else
					{
						this.m_pOpoPrmValCtx->pInd[i] = 0;
						this.m_pOpoPrmValCtx->pSrcInd[i] = 0;
					}
				}
				return;
			}
			else
			{
				if (((this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput) && (this.m_value == DBNull.Value || this.IsProviderSpecificNullValue(this.m_value) || this.m_status == OracleParameterStatus.NullInsert)) || this.m_direction == ParameterDirection.Output)
				{
					*this.m_pOpoPrmValCtx->pInd = -1;
					*this.m_pOpoPrmValCtx->pSrcInd = -1;
					return;
				}
				*this.m_pOpoPrmValCtx->pInd = 0;
				*this.m_pOpoPrmValCtx->pSrcInd = 0;
				return;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00064B1E File Offset: 0x00063B1E
		internal void PreBind(OracleConnection conn, IntPtr errCtx, int arraySize)
		{
			this.PreBind(conn, errCtx, arraySize, false, false);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00064B2C File Offset: 0x00063B2C
		internal unsafe void PreBind(OracleConnection conn, IntPtr errCtx, int arraySize, bool bIsFromEF, bool bIsSelectStmt)
		{
			this.m_bArrayBind = (arraySize != 0 || this.m_collType == OracleCollectionType.PLSQLAssociativeArray);
			if (this.m_bArrayBind)
			{
				this.ResetCtx(arraySize);
			}
			else
			{
				this.m_curSize = -1;
				this.m_arrBindCount = (this.m_bindElemCount = 1);
				this.SetStatus(arraySize);
			}
			switch (this.m_oraDbType)
			{
			case OracleDbType.BFile:
				this.PreBind_BFile(conn);
				break;
			case OracleDbType.Blob:
				this.PreBind_Blob(conn, bIsFromEF);
				break;
			case OracleDbType.Byte:
				this.PreBind_Byte();
				break;
			case OracleDbType.Char:
			case OracleDbType.Long:
			case OracleDbType.Varchar2:
				this.m_pOpoPrmValCtx->CharSetForm = 1;
				this.PreBind_Char();
				break;
			case OracleDbType.Clob:
				this.m_pOpoPrmValCtx->CharSetForm = 1;
				this.PreBind_Clob(conn, bIsFromEF, bIsSelectStmt);
				break;
			case OracleDbType.Date:
				this.PreBind_Date();
				break;
			case OracleDbType.Decimal:
				this.PreBind_Decimal();
				break;
			case OracleDbType.Double:
			case OracleDbType.BinaryDouble:
				this.PreBind_Double(conn, this.m_oraDbType);
				break;
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
				this.PreBind_Raw();
				break;
			case OracleDbType.Int16:
				this.PreBind_Int16();
				break;
			case OracleDbType.Int32:
				this.PreBind_Int32();
				break;
			case OracleDbType.Int64:
				this.PreBind_Int64();
				break;
			case OracleDbType.IntervalDS:
				this.PreBind_IntervalDS();
				break;
			case OracleDbType.IntervalYM:
				this.PreBind_IntervalYM();
				break;
			case OracleDbType.NClob:
				this.m_pOpoPrmValCtx->CharSetForm = 2;
				this.PreBind_Clob(conn, bIsFromEF, bIsSelectStmt);
				break;
			case OracleDbType.NChar:
			case OracleDbType.NVarchar2:
				this.m_pOpoPrmValCtx->CharSetForm = 2;
				this.PreBind_Char();
				break;
			case OracleDbType.RefCursor:
				this.PreBind_RefCursor(conn);
				break;
			case OracleDbType.Single:
			case OracleDbType.BinaryFloat:
				this.PreBind_Single(conn, this.m_oraDbType);
				break;
			case OracleDbType.TimeStamp:
				this.PreBind_TimeStamp(conn, errCtx);
				break;
			case OracleDbType.TimeStampLTZ:
				this.PreBind_TimeStampLTZ(conn, errCtx);
				break;
			case OracleDbType.TimeStampTZ:
				this.PreBind_TimeStampTZ(conn, errCtx);
				break;
			case OracleDbType.XmlType:
				this.PreBind_XmlType(conn);
				break;
			case OracleDbType.Array:
				this.ResetUDTInd();
				if (this.m_enumType != PrmEnumType.ORADBTYPE && this.m_modifedAfterBind)
				{
					this.PreBind_Object(conn);
				}
				else
				{
					this.PreBind_Collection(conn);
				}
				break;
			case OracleDbType.Object:
				this.ResetUDTInd();
				this.PreBind_Object(conn);
				break;
			case OracleDbType.Ref:
				this.ResetUDTInd();
				if (this.m_enumType != PrmEnumType.ORADBTYPE && this.m_modifedAfterBind)
				{
					this.PreBind_Object(conn);
				}
				else
				{
					this.PreBind_OracleRef(conn);
				}
				break;
			}
			this.m_modifedAfterBind = false;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00064DB8 File Offset: 0x00063DB8
		internal unsafe bool PostBind(OracleConnection conn, OpoSqlValCtx* pOpoSqlValCtx, int arraySize)
		{
			if (this.CollectionType == OracleCollectionType.PLSQLAssociativeArray)
			{
				this.m_arrBindCount = this.m_pOpoPrmValCtx->curelep;
				this.Size = this.m_arrBindCount;
			}
			switch (this.m_oraDbType)
			{
			case OracleDbType.BFile:
				this.PostBind_BFile(conn);
				break;
			case OracleDbType.Blob:
				this.PostBind_Blob(conn);
				break;
			case OracleDbType.Byte:
				this.PostBind_Byte();
				break;
			case OracleDbType.Char:
			case OracleDbType.Long:
			case OracleDbType.NChar:
			case OracleDbType.NVarchar2:
			case OracleDbType.Varchar2:
				this.PostBind_Char();
				break;
			case OracleDbType.Clob:
				this.PostBind_Clob(conn, false);
				break;
			case OracleDbType.Date:
				this.PostBind_Date();
				break;
			case OracleDbType.Decimal:
				this.PostBind_Decimal();
				break;
			case OracleDbType.Double:
			case OracleDbType.BinaryDouble:
				this.PostBind_Double();
				break;
			case OracleDbType.LongRaw:
			case OracleDbType.Raw:
				this.PostBind_Raw();
				break;
			case OracleDbType.Int16:
				this.PostBind_Int16();
				break;
			case OracleDbType.Int32:
				this.PostBind_Int32();
				break;
			case OracleDbType.Int64:
				this.PostBind_Int64();
				break;
			case OracleDbType.IntervalDS:
				this.PostBind_IntervalDS();
				break;
			case OracleDbType.IntervalYM:
				this.PostBind_IntervalYM();
				break;
			case OracleDbType.NClob:
				this.PostBind_Clob(conn, true);
				break;
			case OracleDbType.RefCursor:
				this.PostBind_RefCursor(conn, pOpoSqlValCtx, this.m_commandText, this.m_paramPosOrName);
				break;
			case OracleDbType.Single:
			case OracleDbType.BinaryFloat:
				this.PostBind_Single();
				break;
			case OracleDbType.TimeStamp:
				this.PostBind_TimeStamp();
				break;
			case OracleDbType.TimeStampLTZ:
				this.PostBind_TimeStampLTZ();
				break;
			case OracleDbType.TimeStampTZ:
				this.PostBind_TimeStampTZ();
				break;
			case OracleDbType.XmlType:
				this.PostBind_XmlType(conn);
				break;
			case OracleDbType.Array:
				this.PostBind_Collection(conn);
				break;
			case OracleDbType.Object:
				this.PostBind_OracleObject(conn);
				break;
			case OracleDbType.Ref:
				this.PostBind_OracleRef(conn);
				break;
			}
			return true;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00064F7C File Offset: 0x00063F7C
		internal unsafe void PreBindFree(OracleConnection conn, int arraySize)
		{
			if (this.m_oraDbType != OracleDbType.Blob && this.m_oraDbType != OracleDbType.Clob && this.m_oraDbType != OracleDbType.NClob && this.m_oraDbType != OracleDbType.Ref)
			{
				this.m_saveValue = null;
			}
			switch (this.m_oraDbType)
			{
			case OracleDbType.Blob:
			case OracleDbType.Clob:
			case OracleDbType.NClob:
				if (this.m_redirected)
				{
					this.m_redirected = false;
					if (this.m_pDataBuffer != IntPtr.Zero)
					{
						try
						{
							Marshal.FreeCoTaskMem(this.m_pDataBuffer);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						this.m_pDataBuffer = IntPtr.Zero;
					}
				}
				else if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
				{
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						try
						{
							if (this.m_saveValue[i] is OracleBlob)
							{
								((OracleBlob)this.m_saveValue[i]).Dispose();
							}
							else if (this.m_saveValue[i] is OracleClob)
							{
								((OracleClob)this.m_saveValue[i]).Dispose();
							}
							this.m_saveValue[i] = null;
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
					}
				}
				this.m_saveValue = null;
				return;
			case OracleDbType.Byte:
			case OracleDbType.Date:
			case OracleDbType.Decimal:
			case OracleDbType.Double:
			case OracleDbType.Int16:
			case OracleDbType.Int32:
			case OracleDbType.Int64:
			case (OracleDbType)118:
			case OracleDbType.RefCursor:
			case OracleDbType.Single:
				break;
			case OracleDbType.Char:
			case OracleDbType.Long:
			case OracleDbType.LongRaw:
			case OracleDbType.NChar:
			case OracleDbType.NVarchar2:
			case OracleDbType.Raw:
			case OracleDbType.Varchar2:
				if (this.m_pDataBuffer != IntPtr.Zero)
				{
					try
					{
						Marshal.FreeCoTaskMem(this.m_pDataBuffer);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					this.m_pDataBuffer = IntPtr.Zero;
					return;
				}
				break;
			case OracleDbType.IntervalDS:
				if (this.m_direction == ParameterDirection.Input && (this.m_value is TimeSpan || this.m_value is TimeSpan[]))
				{
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1 && (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)) != IntPtr.Zero)
						{
							try
							{
								OpsIDS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
							}
							*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8) = (long)IntPtr.Zero;
						}
					}
					return;
				}
				break;
			case OracleDbType.IntervalYM:
				if (this.m_direction == ParameterDirection.Input && (this.m_value is byte || this.m_value is short || this.m_value is int || this.m_value is long || this.m_value is byte[] || this.m_value is short[] || this.m_value is int[] || this.m_value is long[]))
				{
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1 && (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)) != IntPtr.Zero)
						{
							try
							{
								OpsIYM.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							catch (Exception ex5)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex5);
								}
							}
							*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8) = (long)IntPtr.Zero;
						}
					}
					return;
				}
				break;
			case OracleDbType.TimeStamp:
			case OracleDbType.TimeStampLTZ:
			case OracleDbType.TimeStampTZ:
				if (this.m_direction == ParameterDirection.Input && (this.m_value is DateTime || this.m_value is DateTime[] || this.m_value is Array))
				{
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if ((!(this.m_value is Array) || ((Array)this.m_value).GetValue(i) is DateTime) && this.m_pOpoPrmValCtx->pInd[i] != -1 && (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)) != IntPtr.Zero)
						{
							try
							{
								OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							catch (Exception ex6)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex6);
								}
							}
							*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8) = (long)IntPtr.Zero;
						}
					}
					return;
				}
				break;
			case OracleDbType.XmlType:
			case OracleDbType.Array:
			case OracleDbType.Object:
			case OracleDbType.Ref:
				if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
				{
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_oraDbType == OracleDbType.Ref && this.m_pOpoPrmValCtx->pSrcInd[i] != -1 && !(this.m_value is OracleRef) && !this.IsElemType(typeof(OracleRef), this.m_value, i))
						{
							if (this.m_saveValue[i] != null)
							{
								((OracleRef)this.m_saveValue[i]).Dispose();
							}
							this.m_saveValue[i] = null;
						}
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if ((this.m_oraDbType == OracleDbType.Object || this.m_oraDbType == OracleDbType.Array) && this.m_pOpoPrmValCtx->pOpoUdtValCtx != null)
							{
								try
								{
									OpsPrm.FreeUdtInObjects(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx);
								}
								catch (Exception ex7)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex7);
									}
								}
							}
							if ((IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)) != IntPtr.Zero)
							{
								*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8) = (long)IntPtr.Zero;
							}
						}
					}
				}
				this.m_saveValue = null;
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000655B4 File Offset: 0x000645B4
		private unsafe void SetPrmValCtx(OracleUdtDescriptor udtDesc)
		{
			this.m_udtDescriptor = udtDesc;
			this.m_pOpoPrmValCtx->pOpsDscCtx = udtDesc.m_opsDscCtx;
			this.m_pOpoPrmValCtx->bIsFinalType = udtDesc.m_pOpoDscValCtx->bIsFinalType;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000655E4 File Offset: 0x000645E4
		internal unsafe void SetPrmValCtx(IntPtr ip, int index)
		{
			*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 8) = ip.ToInt64();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000655FE File Offset: 0x000645FE
		internal unsafe void SetPrmValCtx(void* vp, int index)
		{
			*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 8) = vp;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00065613 File Offset: 0x00064613
		internal unsafe void SetPrmValCtx(byte b, int index)
		{
			((byte*)this.m_pOpoPrmValCtx->pBltVal)[index] = b;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00065624 File Offset: 0x00064624
		internal unsafe void SetPrmValCtx(int i, int index)
		{
			*(int*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 4) = i;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00065638 File Offset: 0x00064638
		internal unsafe void SetPrmValCtx(long i, int index)
		{
			*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 22) = i;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0006564D File Offset: 0x0006464D
		internal unsafe void SetPrmValCtx(float s, int index)
		{
			*(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 4) = s;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00065661 File Offset: 0x00064661
		internal unsafe void SetPrmValCtx(double d, int index)
		{
			*(double*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 8) = d;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00065675 File Offset: 0x00064675
		internal unsafe void SetPrmValCtx(short i, int index)
		{
			*(short*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * 2) = i;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0006568C File Offset: 0x0006468C
		internal unsafe void SetPrmValCtx(OraType oraType, int valueSize, int[] alenp)
		{
			this.m_pOpoPrmValCtx->BindType = (ushort)oraType;
			this.m_pOpoPrmValCtx->OraDbType = (int)this.m_oraDbType;
			this.m_pOpoPrmValCtx->Direction = (byte)this.m_direction;
			this.m_pOpoPrmValCtx->PrmEnumType = (byte)this.m_enumType;
			this.SetArrayOfLen(valueSize, alenp);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000656E3 File Offset: 0x000646E3
		internal unsafe void SetPrmValCtx(OraType oraType, int valueSize, int[] alenp, OracleDbType oradbtype)
		{
			this.SetPrmValCtx(oraType, valueSize, alenp);
			this.m_pOpoPrmValCtx->OraDbType = (int)oradbtype;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000656FC File Offset: 0x000646FC
		private unsafe void SetArrayOfLen(int valueSize, int[] alenp)
		{
			int num;
			if (!this.m_bArrayBind)
			{
				num = this.m_maxSize;
			}
			else
			{
				num = this.m_maxArrayBindSize[0];
				int num2 = this.m_maxArrayBindSize.Length;
				for (int i = 0; i < num2; i++)
				{
					if (this.m_maxArrayBindSize[i] > num)
					{
						num = this.m_maxArrayBindSize[i];
					}
				}
			}
			if (num <= -1)
			{
				num = valueSize;
			}
			if (this.m_direction == ParameterDirection.Input)
			{
				this.m_pOpoPrmValCtx->Size = valueSize;
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (alenp != null)
					{
						this.m_pOpoPrmValCtx->alenp[i] = (ushort)alenp[i];
						this.m_pOpoPrmValCtx->objalenp[i] = alenp[i];
					}
					else
					{
						this.m_pOpoPrmValCtx->alenp[i] = (ushort)valueSize;
						this.m_pOpoPrmValCtx->objalenp[i] = valueSize;
					}
				}
				return;
			}
			if (this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_pOpoPrmValCtx->Size = num;
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (alenp != null)
					{
						this.m_pOpoPrmValCtx->alenp[i] = (ushort)alenp[i];
						this.m_pOpoPrmValCtx->objalenp[i] = alenp[i];
					}
					else
					{
						this.m_pOpoPrmValCtx->alenp[i] = (ushort)valueSize;
						this.m_pOpoPrmValCtx->objalenp[i] = valueSize;
					}
				}
				return;
			}
			this.m_pOpoPrmValCtx->Size = num;
			for (int i = 0; i < this.m_arrBindCount; i++)
			{
				if (alenp != null)
				{
					this.m_pOpoPrmValCtx->alenp[i] = (ushort)alenp[i];
					this.m_pOpoPrmValCtx->objalenp[i] = alenp[i];
				}
				else if (!this.m_bArrayBind)
				{
					this.m_pOpoPrmValCtx->alenp[i] = (ushort)num;
					this.m_pOpoPrmValCtx->objalenp[i] = num;
				}
				else if (num == 0)
				{
					this.m_pOpoPrmValCtx->alenp[i] = (ushort)num;
					this.m_pOpoPrmValCtx->objalenp[i] = num;
				}
				else
				{
					this.m_pOpoPrmValCtx->alenp[i] = (ushort)this.m_maxArrayBindSize[i];
					this.m_pOpoPrmValCtx->objalenp[i] = this.m_maxArrayBindSize[i];
				}
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00065928 File Offset: 0x00064928
		private unsafe void PreBind_BFile(OracleConnection conn)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array = this.m_value as Array;
							if (array == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						OracleBFile oracleBFile;
						if ((oracleBFile = (value as OracleBFile)) == null)
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
						}
						OracleBFile oracleBFile2 = oracleBFile;
						if (oracleBFile2.m_connection != conn && (!oracleBFile2.m_connection.m_contextConnection || !conn.m_contextConnection))
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
						}
						if (oracleBFile2.m_conSignature != conn.m_conSignature)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
						}
						this.SetPrmValCtx(oracleBFile2.LobCtx, i);
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_OCIBFileLocator, -1, null);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00065A60 File Offset: 0x00064A60
		private unsafe void PostBind_BFile(OracleConnection conn)
		{
			OracleBFile[] array = null;
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						this.m_value = new OracleBFile(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal));
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleBFile.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleBFile[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							array[i] = new OracleBFile(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)));
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleBFile.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
						{
							this.m_value = new OracleBFile(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal));
							this.m_status = OracleParameterStatus.Success;
						}
					}
					else
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd != -1)
						{
							((OracleBFile)this.m_value).Dispose();
						}
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleBFile.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleBFile[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								array[i] = new OracleBFile(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)));
							}
							else
							{
								array[i] = ((OracleBFile[])this.m_value)[i];
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
							{
								((OracleBFile[])this.m_value)[i].Dispose();
							}
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleBFile.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_enumType == PrmEnumType.DBTYPE && this.m_bOracleDbTypeExSet && this.m_direction != ParameterDirection.Input)
			{
				if (this.m_bArrayBind)
				{
					byte[][] array2 = new byte[this.m_arrBindCount][];
					for (int j = 0; j < this.m_arrBindCount; j++)
					{
						OracleBFile oracleBFile = array[j];
						if (oracleBFile == null || oracleBFile.IsNull)
						{
							array2[j] = null;
						}
						else
						{
							array2[j] = oracleBFile.Value;
							oracleBFile.Dispose();
						}
					}
					this.m_value = array2;
					return;
				}
				if (this.m_value != DBNull.Value)
				{
					OracleBFile oracleBFile2 = (OracleBFile)this.m_value;
					if (oracleBFile2.IsNull)
					{
						this.m_value = DBNull.Value;
						return;
					}
					this.m_value = oracleBFile2.Value;
					oracleBFile2.Dispose();
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00065DF0 File Offset: 0x00064DF0
		private unsafe void PreBind_Blob(OracleConnection conn, bool bIsFromEF)
		{
			if (conn.m_majorVersion >= 9 && this.m_direction == ParameterDirection.Input && !this.m_bArrayBind && !(this.m_value is OracleBlob))
			{
				int num;
				if (*this.m_pOpoPrmValCtx->pInd != -1)
				{
					num = this.GetBindingSize_Raw(0);
				}
				else
				{
					num = 0;
				}
				if (num < 4000 && num > 0)
				{
					this.m_redirected = true;
					this.PreBind_Raw();
					return;
				}
			}
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array = this.m_value as Array;
							if (array == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						OracleBlob oracleBlob = value as OracleBlob;
						if (oracleBlob != null)
						{
							if (oracleBlob.m_connection != conn && (!oracleBlob.m_connection.m_contextConnection || !conn.m_contextConnection))
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
							}
							if (OraTrace.m_TraceLevel != 0U)
							{
								if (oracleBlob.IsNull)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) OracleBlob passed by App(PreBind): OracleBlob.Null \n"
									});
								}
								else
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) OracleBlob passed by App(PreBind): " + oracleBlob.LobCtx.ToString() + "\n"
									});
								}
							}
							if (oracleBlob.m_conSignature != conn.m_conSignature)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
							}
							if (oracleBlob.m_isTemporaryLob)
							{
								oracleBlob.CreateTempLob();
							}
							this.SetPrmValCtx(oracleBlob.LobCtx, i);
						}
						else
						{
							byte[] preBindBuffer_Raw = this.GetPreBindBuffer_Raw(i);
							int bindingSize = this.GetBindingSize(preBindBuffer_Raw, i);
							if (OraTrace.m_TraceLevel != 0U)
							{
								if (this.m_direction == ParameterDirection.Input)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating OracleBlob object(PreBind-Input)\n"
									});
								}
								else
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating OracleBlob object(PreBind-IO)\n"
									});
								}
							}
							oracleBlob = new OracleBlob(conn);
							oracleBlob.Write(preBindBuffer_Raw, this.m_offset, bindingSize, bIsFromEF);
							this.SetPrmValCtx(oracleBlob.LobCtx, i);
							this.m_saveValue[i] = oracleBlob;
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_OCIBLobLocator, -1, null);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00066090 File Offset: 0x00065090
		private unsafe void PostBind_Blob(OracleConnection conn)
		{
			OracleBlob[] array = null;
			if (this.m_redirected)
			{
				this.m_redirected = false;
				this.PostBind_Raw();
				return;
			}
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1 && !(this.m_value is OracleBlob) && !this.IsElemType(typeof(OracleBlob), this.m_value, i))
					{
						((OracleBlob)this.m_saveValue[i]).Dispose();
					}
				}
				this.m_saveValue = null;
				break;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (LOB) Creating OracleBlob object:O/RV(PostBind)\n"
							});
						}
						this.m_value = new OracleBlob(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false, false);
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleBlob.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleBlob[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									" (LOB) Creating OracleBlob objects:O/RV(PostBind)\n"
								});
							}
							array[i] = new OracleBlob(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), false, false);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleBlob.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									" (LOB) Creating OracleBlob object:IO(PostBind)\n"
								});
							}
							this.m_value = new OracleBlob(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false, false);
						}
						else if (!(this.m_value is OracleBlob))
						{
							this.m_value = this.m_saveValue[0];
							this.m_saveValue = null;
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd != -1)
						{
							if (this.m_value is OracleBlob)
							{
								((OracleBlob)this.m_value).Dispose();
							}
							else
							{
								((OracleBlob)this.m_saveValue[0]).Dispose();
								this.m_saveValue = null;
							}
						}
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleBlob.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleBlob[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating OracleBlob objects:IO(PostBind)\n"
									});
								}
								array[i] = new OracleBlob(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), false, false);
							}
							else if (!(this.m_value is OracleBlob[]))
							{
								array[i] = (OracleBlob)this.m_saveValue[i];
								this.m_saveValue[i] = null;
							}
							else
							{
								array[i] = ((OracleBlob[])this.m_value)[i];
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
							{
								if (this.m_value is OracleBlob[])
								{
									((OracleBlob[])this.m_value)[i].Dispose();
								}
								else
								{
									if (((Array)this.m_value).GetValue(i) is OracleBlob)
									{
										((OracleBlob)((Array)this.m_value).GetValue(i)).Dispose();
										((Array)this.m_value).SetValue(null, i);
									}
									if (this.m_saveValue[i] is OracleBlob)
									{
										((OracleBlob)this.m_saveValue[i]).Dispose();
										this.m_saveValue[i] = null;
									}
								}
							}
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleBlob.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_enumType == PrmEnumType.DBTYPE && this.m_bOracleDbTypeExSet && this.m_direction != ParameterDirection.Input)
			{
				if (this.m_bArrayBind)
				{
					byte[][] array2 = new byte[this.m_arrBindCount][];
					for (int j = 0; j < this.m_arrBindCount; j++)
					{
						OracleBlob oracleBlob = array[j];
						if (oracleBlob == null || oracleBlob.IsNull)
						{
							array2[j] = null;
						}
						else
						{
							array2[j] = oracleBlob.Value;
							oracleBlob.Dispose();
						}
					}
					this.m_value = array2;
					return;
				}
				if (this.m_value != DBNull.Value)
				{
					OracleBlob oracleBlob2 = (OracleBlob)this.m_value;
					if (oracleBlob2.IsNull)
					{
						this.m_value = DBNull.Value;
						return;
					}
					this.m_value = oracleBlob2.Value;
					oracleBlob2.Dispose();
				}
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00066628 File Offset: 0x00065628
		private unsafe void PreBind_Clob(OracleConnection conn, bool bIsFromEF, bool bIsSelectStmt)
		{
			if (conn.m_majorVersion >= 9 && this.m_direction == ParameterDirection.Input && !this.m_bArrayBind && !(this.m_value is OracleClob))
			{
				int num;
				if (*this.m_pOpoPrmValCtx->pInd != -1)
				{
					num = this.GetBindingSize_Char(0);
				}
				else
				{
					num = 0;
				}
				if (this.m_pOpoPrmValCtx->CharSetForm == 2)
				{
					int maxBytesPerNChar = OpsCon.GetMaxBytesPerNChar(conn.m_opoConCtx.opsConCtx);
					if (num < 4000 / maxBytesPerNChar && (num > 0 || (num == 0 && bIsFromEF && bIsSelectStmt)))
					{
						this.m_redirected = true;
						this.PreBind_Char();
						return;
					}
				}
				else if (num < 4000 && (num > 0 || (num == 0 && bIsFromEF && bIsSelectStmt)))
				{
					this.m_redirected = true;
					this.PreBind_Char();
					return;
				}
			}
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array = this.m_value as Array;
							if (array == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						OracleClob oracleClob = value as OracleClob;
						if (oracleClob != null)
						{
							if (oracleClob.m_connection != conn && (!oracleClob.m_connection.m_contextConnection || !conn.m_contextConnection))
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
							}
							if (OraTrace.m_TraceLevel != 0U)
							{
								if (oracleClob.IsNull)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Clob passed by App(PreBind): OracleClob.Null \n"
									});
								}
								else
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Clob passed by App(PreBind): " + oracleClob.LobCtx.ToString() + "\n"
									});
								}
							}
							if (oracleClob.m_conSignature != conn.m_conSignature)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
							}
							if (oracleClob.m_isTemporaryLob)
							{
								oracleClob.CreateTempLob();
							}
							this.SetPrmValCtx(oracleClob.LobCtx, i);
						}
						else
						{
							char[] preBindBuffer_Char = this.GetPreBindBuffer_Char(i);
							int bindingSize = this.GetBindingSize(preBindBuffer_Char, i);
							if (OraTrace.m_TraceLevel != 0U)
							{
								if (this.m_direction == ParameterDirection.Input)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating Clob object(PreBind-Input)\n"
									});
								}
								else
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating Clob object(PreBind-IO)\n"
									});
								}
							}
							if (this.m_oraDbType == OracleDbType.NClob)
							{
								oracleClob = new OracleClob(conn, false, true);
							}
							else
							{
								oracleClob = new OracleClob(conn, false, false);
							}
							this.m_saveValue[i] = oracleClob;
							oracleClob.Write(preBindBuffer_Char, this.m_offset, bindingSize, bIsFromEF);
							this.SetPrmValCtx(oracleClob.LobCtx, i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_OCICLobLocator, -1, null);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0006693C File Offset: 0x0006593C
		private unsafe void PostBind_Clob(OracleConnection conn, bool isNClob)
		{
			OracleClob[] array = null;
			if (this.m_redirected)
			{
				this.m_redirected = false;
				this.PostBind_Char();
				return;
			}
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1 && !(this.m_value is OracleClob) && !this.IsElemType(typeof(OracleClob), this.m_value, i))
					{
						((OracleClob)this.m_saveValue[i]).Dispose();
					}
				}
				this.m_saveValue = null;
				break;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						bool bNClob = this.m_oraDbType == OracleDbType.NClob;
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (LOB) Creating Clob object:O/RV(PostBind)\n"
							});
						}
						this.m_value = new OracleClob(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false, bNClob, false);
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleClob.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleClob[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							bool bNClob2 = this.m_oraDbType == OracleDbType.NClob;
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									" (LOB) Creating Clob objects:O/RV(PostBind)\n"
								});
							}
							array[i] = new OracleClob(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), false, bNClob2, false);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleClob.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
						{
							bool bNClob3 = this.m_oraDbType == OracleDbType.NClob;
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									" (LOB) Creating Clob object:IO(PostBind)\n"
								});
							}
							this.m_value = new OracleClob(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false, bNClob3, false);
						}
						else if (!(this.m_value is OracleClob))
						{
							this.m_value = this.m_saveValue[0];
							this.m_saveValue = null;
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd != -1)
						{
							if (this.m_value is OracleClob)
							{
								((OracleClob)this.m_value).Dispose();
							}
							else
							{
								((OracleClob)this.m_saveValue[0]).Dispose();
								this.m_saveValue = null;
							}
						}
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleClob.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleClob[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								bool bNClob4 = this.m_oraDbType == OracleDbType.NClob;
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (LOB) Creating Clob objects:IO(PostBind)\n"
									});
								}
								array[i] = new OracleClob(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), false, bNClob4, false);
							}
							else if (this.m_value is OracleClob[])
							{
								array[i] = ((OracleClob[])this.m_value)[i];
							}
							else
							{
								array[i] = (OracleClob)this.m_saveValue[i];
								this.m_saveValue[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
							{
								if (this.m_value is OracleClob[])
								{
									((OracleClob[])this.m_value)[i].Dispose();
								}
								else
								{
									if (((Array)this.m_value).GetValue(i) is OracleClob)
									{
										((OracleClob)((Array)this.m_value).GetValue(i)).Dispose();
										((Array)this.m_value).SetValue(null, i);
									}
									if (this.m_saveValue[i] is OracleClob)
									{
										((OracleClob)this.m_saveValue[i]).Dispose();
										this.m_saveValue[i] = null;
									}
								}
							}
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleClob.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_enumType == PrmEnumType.DBTYPE && this.m_bOracleDbTypeExSet && this.m_direction != ParameterDirection.Input)
			{
				if (this.m_bArrayBind)
				{
					string[] array2 = new string[this.m_arrBindCount];
					for (int j = 0; j < this.m_arrBindCount; j++)
					{
						OracleClob oracleClob = array[j];
						if (oracleClob == null || oracleClob.IsNull)
						{
							array2[j] = null;
						}
						else
						{
							array2[j] = oracleClob.Value;
							oracleClob.Dispose();
						}
					}
					this.m_value = array2;
					return;
				}
				if (this.m_value != DBNull.Value)
				{
					OracleClob oracleClob2 = (OracleClob)this.m_value;
					if (oracleClob2.IsNull)
					{
						this.m_value = DBNull.Value;
						return;
					}
					this.m_value = oracleClob2.Value;
					oracleClob2.Dispose();
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00066F2C File Offset: 0x00065F2C
		private bool IsElemType(Type type, object value, int index)
		{
			Type left = null;
			bool result = false;
			if (value != null)
			{
				Array array = value as Array;
				if (array != null && array.Length > 0 && array.GetValue(index) != null)
				{
					left = array.GetValue(index).GetType();
				}
				result = (left == type);
			}
			return result;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00066F74 File Offset: 0x00065F74
		private unsafe void PreBind_Char()
		{
			OraType oraType = OraType.ORA_CHARN;
			if (this.m_oraDbType == OracleDbType.Char || this.m_oraDbType == OracleDbType.NChar)
			{
				oraType = OraType.ORA_CHAR;
			}
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				int[] array;
				int num;
				int num2;
				if (this.m_bArrayBind)
				{
					array = new int[this.m_arrBindCount];
					num = 0;
					num2 = 0;
					for (int i = 0; i < this.m_bindElemCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] == -1)
						{
							array[i] = 0;
						}
						else
						{
							array[i] = this.GetBindingSize_Char(i);
						}
						if (num2 < array[i])
						{
							num2 = array[i];
						}
						if (this.m_direction == ParameterDirection.InputOutput && this.m_maxArrayBindSize != null && num2 < this.m_maxArrayBindSize[i])
						{
							num2 = this.m_maxArrayBindSize[i];
						}
					}
				}
				else
				{
					array = null;
					if (*this.m_pOpoPrmValCtx->pInd == -1)
					{
						num = (num2 = 0);
					}
					else
					{
						num = (num2 = this.GetBindingSize_Char(0));
					}
					if (this.m_direction == ParameterDirection.InputOutput && num < this.m_maxSize)
					{
						num2 = this.m_maxSize;
					}
				}
				if (num2 > 0)
				{
					try
					{
						this.m_pDataBuffer = Marshal.AllocCoTaskMem(num2 * this.m_arrBindCount * 2);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					if (this.m_pDataBuffer != IntPtr.Zero)
					{
						for (int j = 0; j < this.m_bindElemCount; j++)
						{
							object value;
							int num3;
							if (!this.m_bArrayBind)
							{
								value = this.m_value;
								num3 = num;
							}
							else
							{
								Array array2 = this.m_value as Array;
								if (array2 == null)
								{
									throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
								}
								value = array2.GetValue(j);
								num3 = array[j];
							}
							if (num3 > 0)
							{
								string text;
								char[] source;
								if ((text = (value as string)) != null)
								{
									Marshal.Copy(text.ToCharArray(), this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)(j * num2) * 2)), num3);
								}
								else if ((source = (value as char[])) != null)
								{
									Marshal.Copy(source, this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)(j * num2) * 2)), num3);
								}
								else if (value is OracleString)
								{
									Marshal.Copy(((OracleString)value).Value.ToCharArray(), this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)(j * num2) * 2)), num3);
								}
								else if (value is char)
								{
									Marshal.Copy(new char[]
									{
										(char)value
									}, 0, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)(j * num2) * 2)), num3);
								}
								else
								{
									string text2 = Convert.ToString(value);
									Marshal.Copy(text2.ToCharArray(), this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)(j * num2) * 2)), num3);
								}
							}
						}
						this.SetPrmValCtx(this.m_pDataBuffer, 0);
					}
				}
				if (this.m_bArrayBind)
				{
					this.SetPrmValCtx(oraType, num2, array);
					return;
				}
				if (this.m_oraDbType == OracleDbType.Clob)
				{
					this.SetPrmValCtx(oraType, num, null, OracleDbType.Varchar2);
					return;
				}
				if (this.m_oraDbType == OracleDbType.NClob)
				{
					this.SetPrmValCtx(oraType, num, null, OracleDbType.NVarchar2);
					return;
				}
				this.SetPrmValCtx(oraType, num, null);
				return;
			}
			else
			{
				int num2;
				if (!this.m_bArrayBind)
				{
					if (this.m_maxSize == -1)
					{
						num2 = 0;
					}
					else
					{
						num2 = this.m_maxSize;
					}
				}
				else if (this.m_maxArrayBindSize != null)
				{
					num2 = this.m_maxArrayBindSize[0];
					for (int k = 0; k < this.m_arrBindCount; k++)
					{
						if (this.m_maxArrayBindSize[k] > num2)
						{
							num2 = this.m_maxArrayBindSize[k];
						}
					}
					if (num2 == -1)
					{
						num2 = 0;
					}
				}
				else
				{
					num2 = 0;
				}
				if (num2 > 0)
				{
					try
					{
						this.m_pDataBuffer = Marshal.AllocCoTaskMem(num2 * this.m_arrBindCount * 2);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					if (this.m_pDataBuffer != IntPtr.Zero)
					{
						this.SetPrmValCtx(this.m_pDataBuffer, 0);
					}
				}
				if (!this.m_bArrayBind)
				{
					this.SetPrmValCtx(oraType, num2, null);
					return;
				}
				this.SetPrmValCtx(oraType, num2, this.m_maxArrayBindSize);
				return;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000673A4 File Offset: 0x000663A4
		private int GetBindingSize_Char(int idx)
		{
			int bufferLength = 0;
			bool flag = false;
			if (this.m_bArrayBind)
			{
				string[] array;
				char[][] array2;
				OracleString[] array3;
				if ((array = (this.m_value as string[])) != null)
				{
					bufferLength = array[idx].Length;
				}
				else if ((array2 = (this.m_value as char[][])) != null)
				{
					bufferLength = array2[idx].Length;
				}
				else if (this.m_value is char[])
				{
					bufferLength = 1;
				}
				else if ((array3 = (this.m_value as OracleString[])) != null)
				{
					bufferLength = array3[idx].Length;
				}
				else
				{
					flag = true;
				}
			}
			if (!this.m_bArrayBind || flag)
			{
				object value;
				if (!this.m_bArrayBind)
				{
					value = this.m_value;
				}
				else
				{
					value = ((Array)this.m_value).GetValue(idx);
				}
				string text;
				char[] array4;
				if ((text = (value as string)) != null)
				{
					bufferLength = text.Length;
				}
				else if ((array4 = (value as char[])) != null)
				{
					bufferLength = array4.Length;
				}
				else if (value is char)
				{
					bufferLength = 1;
				}
				else if (value is OracleString)
				{
					bufferLength = ((OracleString)value).Length;
				}
				else
				{
					bufferLength = Convert.ToString(value).Length;
				}
			}
			return this.GetBindingSize(bufferLength, idx);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000674BC File Offset: 0x000664BC
		private int GetBindingSize_Raw(int idx)
		{
			int bufferLength = 0;
			bool flag = false;
			if (this.m_bArrayBind)
			{
				byte[][] array;
				OracleBinary[] array2;
				if ((array = (this.m_value as byte[][])) != null)
				{
					bufferLength = array[idx].Length;
				}
				else if ((array2 = (this.m_value as OracleBinary[])) != null)
				{
					bufferLength = array2[idx].Length;
				}
				else
				{
					flag = true;
				}
			}
			if (!this.m_bArrayBind || flag)
			{
				object value;
				if (!this.m_bArrayBind)
				{
					value = this.m_value;
				}
				else
				{
					value = ((Array)this.m_value).GetValue(idx);
				}
				byte[] array3;
				if ((array3 = (value as byte[])) != null)
				{
					bufferLength = array3.Length;
				}
				else if (value is OracleBinary)
				{
					bufferLength = ((OracleBinary)value).Length;
				}
				else
				{
					if (!(value is Guid))
					{
						throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
					}
					bufferLength = 16;
				}
			}
			return this.GetBindingSize(bufferLength, idx);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000675A4 File Offset: 0x000665A4
		private int GetBindingSize(int bufferLength, int idx)
		{
			int num;
			if (!this.m_bArrayBind)
			{
				if (this.m_maxSize == -1)
				{
					num = bufferLength;
				}
				else
				{
					num = this.m_maxSize;
				}
			}
			else if (this.m_maxArrayBindSize[idx] == -1)
			{
				num = bufferLength;
			}
			else
			{
				num = this.m_maxArrayBindSize[idx];
			}
			if (this.m_offset > bufferLength)
			{
				throw new ArgumentException("Invalid offset", this.ParameterName);
			}
			if (this.m_offset + num > bufferLength)
			{
				num = bufferLength - this.m_offset;
			}
			return num;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00067618 File Offset: 0x00066618
		private int GetBindingSize(Array buffer, int idx)
		{
			int num;
			if (!this.m_bArrayBind)
			{
				if (this.m_maxSize == -1)
				{
					num = buffer.Length;
				}
				else
				{
					num = this.m_maxSize;
				}
			}
			else if (this.m_maxArrayBindSize[idx] == -1)
			{
				num = buffer.Length;
			}
			else
			{
				num = this.m_maxArrayBindSize[idx];
			}
			if (this.m_offset > buffer.Length)
			{
				throw new ArgumentException("Invalid offset", this.ParameterName);
			}
			if (this.m_offset + num > buffer.Length)
			{
				num = buffer.Length - this.m_offset;
			}
			return num;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000676A4 File Offset: 0x000666A4
		private char[] GetPreBindBuffer_Char(int idx)
		{
			object value;
			if (!this.m_bArrayBind)
			{
				value = this.m_value;
			}
			else
			{
				Array array;
				if ((array = (this.m_value as Array)) == null)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
				}
				value = array.GetValue(idx);
			}
			string text;
			char[] result;
			char[] array2;
			if ((text = (value as string)) != null)
			{
				result = text.ToCharArray();
			}
			else if ((array2 = (value as char[])) != null)
			{
				result = array2;
			}
			else if (value is char)
			{
				result = ((char)value).ToString().ToCharArray();
			}
			else if (value is OracleString)
			{
				result = ((OracleString)value).Value.ToCharArray();
			}
			else
			{
				result = Convert.ToString(value).ToCharArray();
			}
			return result;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0006776C File Offset: 0x0006676C
		private string GetPreBindBuffer_Str(int idx)
		{
			object value;
			if (!this.m_bArrayBind)
			{
				value = this.m_value;
			}
			else
			{
				Array array;
				if ((array = (this.m_value as Array)) == null)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
				}
				value = array.GetValue(idx);
			}
			string text;
			string result;
			char[] value2;
			if ((text = (value as string)) != null)
			{
				result = text;
			}
			else if ((value2 = (value as char[])) != null)
			{
				result = new string(value2);
			}
			else if (value is char)
			{
				result = new string((char)value, 1);
			}
			else if (value is OracleString)
			{
				result = ((OracleString)value).Value;
			}
			else
			{
				result = Convert.ToString(value);
			}
			return result;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00067820 File Offset: 0x00066820
		private byte[] GetPreBindBuffer_Raw(int idx)
		{
			object value;
			if (!this.m_bArrayBind)
			{
				value = this.m_value;
			}
			else
			{
				Array array;
				if ((array = (this.m_value as Array)) == null)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
				}
				value = array.GetValue(idx);
			}
			byte[] array2;
			byte[] result;
			if ((array2 = (value as byte[])) != null)
			{
				result = array2;
			}
			else
			{
				if (!(value is OracleBinary))
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
				}
				result = (byte[])((OracleBinary)value);
			}
			return result;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000678B8 File Offset: 0x000668B8
		private unsafe void PostBind_Char()
		{
			try
			{
				switch (this.m_direction)
				{
				case ParameterDirection.Output:
				case ParameterDirection.InputOutput:
				case ParameterDirection.ReturnValue:
					if (!this.m_bArrayBind)
					{
						if (*this.m_pOpoPrmValCtx->pInd != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								this.m_value = new OracleString(new string((char*)((void*)this.m_pDataBuffer), 0, (int)(*this.m_pOpoPrmValCtx->alenp / 2)));
							}
							else
							{
								this.m_value = new string((char*)((void*)this.m_pDataBuffer), 0, (int)(*this.m_pOpoPrmValCtx->alenp / 2));
							}
							this.m_curSize = (int)(*this.m_pOpoPrmValCtx->alenp / 2);
							this.m_status = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								this.m_value = OracleString.Null;
							}
							else
							{
								this.m_value = DBNull.Value;
							}
							this.m_status = OracleParameterStatus.NullFetched;
						}
					}
					else
					{
						OracleString[] array = null;
						string[] array2 = null;
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							array = new OracleString[this.m_arrBindCount];
						}
						else
						{
							array2 = new string[this.m_arrBindCount];
						}
						for (int i = 0; i < this.m_arrBindCount; i++)
						{
							int num;
							if ((int)this.m_pOpoPrmValCtx->alenp[i] > this.m_maxArrayBindSize[i] * 2)
							{
								if (this.m_maxArrayBindSize[i] > -1)
								{
									num = this.m_maxArrayBindSize[i] * 2;
								}
								else
								{
									num = 0;
								}
							}
							else
							{
								num = (int)this.m_pOpoPrmValCtx->alenp[i];
							}
							if (this.m_pOpoPrmValCtx->pInd[i] != -1)
							{
								if (this.m_enumType == PrmEnumType.ORADBTYPE)
								{
									array[i] = new OracleString(new string((char*)((void*)this.m_pDataBuffer), this.m_pOpoPrmValCtx->Size / 2 * i, num / 2));
								}
								else
								{
									array2[i] = new string((char*)((void*)this.m_pDataBuffer), this.m_pOpoPrmValCtx->Size / 2 * i, num / 2);
								}
								this.m_curArrayBindSize[i] = num / 2;
								this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
							}
							else
							{
								this.m_curSize = 0;
								this.m_curArrayBindSize[i] = 0;
								if (this.m_enumType == PrmEnumType.ORADBTYPE)
								{
									array[i] = OracleString.Null;
								}
								else
								{
									array2[i] = null;
								}
								this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
							}
						}
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = array;
						}
						else
						{
							this.m_value = array2;
						}
					}
					break;
				}
			}
			finally
			{
				if (this.m_pDataBuffer != IntPtr.Zero)
				{
					try
					{
						Marshal.FreeCoTaskMem(this.m_pDataBuffer);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_pDataBuffer = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00067BA0 File Offset: 0x00066BA0
		private unsafe void PreBind_Decimal()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				OracleDecimal value = OracleDecimal.Null;
				OracleDecimal oracleDecimal = OracleDecimal.Null;
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value2;
						if (!this.m_bArrayBind)
						{
							value2 = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value2 = array.GetValue(i);
						}
						if (value2 is decimal)
						{
							if (this.m_precision != 100 || this.m_scale != 129)
							{
								value = new OracleDecimal((decimal)value2);
							}
							else
							{
								DecimalConv.GetBytes((decimal)value2, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
							}
						}
						else
						{
							if (value2 is byte)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal((int)((byte)value2));
									goto IL_6C0;
								}
								byte b = (byte)value2;
								try
								{
									OpsDec.GetValCtxFromInteger((void*)(&b), 1, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
									goto IL_6C0;
								}
								catch (Exception ex)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex);
									}
									throw;
								}
							}
							if (value2 is short)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal((int)((short)value2));
									goto IL_6C0;
								}
								short num = (short)value2;
								try
								{
									OpsDec.GetValCtxFromInteger((void*)(&num), 2, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
									goto IL_6C0;
								}
								catch (Exception ex2)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex2);
									}
									throw;
								}
							}
							if (value2 is int)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal((int)value2);
									goto IL_6C0;
								}
								int num2 = (int)value2;
								try
								{
									OpsDec.GetValCtxFromInteger((void*)(&num2), 4, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
									goto IL_6C0;
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
									throw;
								}
							}
							if (value2 is long)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal((long)value2);
									goto IL_6C0;
								}
								long num3 = (long)value2;
								try
								{
									OpsDec.GetValCtxFromInteger((void*)(&num3), 8, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
									goto IL_6C0;
								}
								catch (Exception ex4)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex4);
									}
									throw;
								}
							}
							if (value2 is float)
							{
								OracleDecimal value3 = new OracleDecimal((double)((float)value2));
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = OracleDecimal.SetPrecisionNoRound(value3, 7);
									goto IL_6C0;
								}
								try
								{
									OpsDec.GetValCtxForSetPrecNoRound(value3.m_opoDecCtx.m_pValCtx, 7, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
									goto IL_6C0;
								}
								catch (Exception ex5)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex5);
									}
									throw;
								}
							}
							string numStr;
							if (value2 is double)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal((double)value2);
								}
								else
								{
									byte* ptr = (byte*)((void*)new OracleDecimal((double)value2).m_opoDecCtx.m_pValCtx);
									byte* ptr2 = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22;
									for (int j = 0; j <= (int)(*ptr); j++)
									{
										ptr2[j] = ptr[j];
									}
								}
							}
							else if (value2 is OracleDecimal)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = (OracleDecimal)value2;
								}
								else
								{
									byte* ptr3 = (byte*)((void*)((OracleDecimal)value2).m_opoDecCtx.m_pValCtx);
									byte* ptr4 = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22;
									for (int k = 0; k <= (int)(*ptr3); k++)
									{
										ptr4[k] = ptr3[k];
									}
								}
							}
							else if (value2 is OracleString)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal(((OracleString)value2).Value);
								}
								else
								{
									byte* ptr5 = (byte*)((void*)new OracleDecimal(((OracleString)value2).Value).m_opoDecCtx.m_pValCtx);
									byte* ptr6 = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22;
									for (int l = 0; l <= (int)(*ptr5); l++)
									{
										ptr6[l] = ptr5[l];
									}
								}
							}
							else if ((numStr = (value2 as string)) != null)
							{
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal(numStr);
								}
								else
								{
									byte* ptr7 = (byte*)((void*)new OracleDecimal(numStr).m_opoDecCtx.m_pValCtx);
									byte* ptr8 = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22;
									for (int m = 0; m <= (int)(*ptr7); m++)
									{
										ptr8[m] = ptr7[m];
									}
								}
							}
							else
							{
								byte[] array2;
								if ((array2 = (value2 as byte[])) != null)
								{
									if (this.m_precision != 100 || this.m_scale != 129)
									{
										value = new OracleDecimal(array2);
										goto IL_6C0;
									}
									if (array2.Length != 22)
									{
										throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
									}
									int num4 = 0;
									GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
									try
									{
										try
										{
											num4 = OpsDec.GetValCtxFromBytes(gchandle.AddrOfPinnedObject(), (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
										}
										catch (Exception ex6)
										{
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.TraceExceptionInfo(ex6);
											}
											throw;
										}
										goto IL_6C0;
									}
									finally
									{
										if (gchandle.IsAllocated)
										{
											gchandle.Free();
										}
										if (num4 != 0)
										{
											throw new OracleTypeException(num4, new object[0]);
										}
									}
								}
								if (value2 is bool)
								{
									if (this.m_precision != 100 || this.m_scale != 129)
									{
										value = new OracleDecimal((int)(((bool)value2) ? 1 : 0));
										goto IL_6C0;
									}
									short num5 = ((bool)value2) ? 1 : 0;
									try
									{
										OpsDec.GetValCtxFromInteger((void*)(&num5), 2, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
										goto IL_6C0;
									}
									catch (Exception ex7)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex7);
										}
										throw;
									}
								}
								if (this.m_precision != 100 || this.m_scale != 129)
								{
									value = new OracleDecimal(Convert.ToDecimal(value2));
								}
								else
								{
									DecimalConv.GetBytes(Convert.ToDecimal(value2), (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
								}
							}
						}
						IL_6C0:
						if (this.m_precision != 100 || this.m_scale != 129)
						{
							oracleDecimal.m_opoDecCtx = null;
							if (this.m_precision != 100 && this.m_scale != 129)
							{
								oracleDecimal = OracleDecimal.ConvertToPrecScale(value, (int)this.m_precision, (int)this.m_scale);
							}
							else if (this.m_precision != 100)
							{
								oracleDecimal = OracleDecimal.SetPrecision(value, (int)this.m_precision);
							}
							else if (this.m_scale != 129)
							{
								oracleDecimal = OracleDecimal.AdjustScale(value, (int)this.m_scale, true);
							}
							byte* ptr9 = (byte*)((void*)oracleDecimal.m_opoDecCtx.m_pValCtx);
							byte* ptr10 = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22;
							for (int n = 0; n <= (int)(*ptr9); n++)
							{
								ptr10[n] = ptr9[n];
							}
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_VARNUM, 22, null);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000683B8 File Offset: 0x000673B8
		private unsafe void PostBind_Decimal()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						IntPtr zero = IntPtr.Zero;
						if (this.m_enumType == PrmEnumType.ORADBTYPE || this.m_precision != 100 || this.m_scale != 129)
						{
							int num = 0;
							try
							{
								num = OpsDec.AllocValCtxFromBytes((IntPtr)this.m_pOpoPrmValCtx->pBltVal, out zero);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								num = ErrRes.INT_ERR;
								throw;
							}
							finally
							{
								if (num != 0)
								{
									if (zero != IntPtr.Zero)
									{
										try
										{
											OpsDec.FreeValCtx(zero);
										}
										catch (Exception ex2)
										{
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.TraceExceptionInfo(ex2);
											}
										}
										zero = IntPtr.Zero;
									}
									if (num != ErrRes.INT_ERR)
									{
										throw new OracleTypeException(num, new object[0]);
									}
								}
							}
						}
						if (this.m_precision != 100 || this.m_scale != 129)
						{
							OracleDecimal value = new OracleDecimal(zero, false);
							OracleDecimal oracleDecimal = OracleDecimal.Null;
							if (this.m_precision != 100 && this.m_scale != 129)
							{
								oracleDecimal = OracleDecimal.ConvertToPrecScale(value, (int)this.m_precision, (int)this.m_scale);
							}
							else if (this.m_precision != 100)
							{
								oracleDecimal = OracleDecimal.SetPrecision(value, (int)this.m_precision);
							}
							else if (this.m_scale != 129)
							{
								oracleDecimal = OracleDecimal.AdjustScale(value, (int)this.m_scale, true);
							}
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								this.m_value = oracleDecimal;
							}
							else
							{
								this.m_value = oracleDecimal.Value;
							}
						}
						else if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleDecimal(zero, false);
						}
						else
						{
							this.m_value = DecimalConv.GetDecimal((IntPtr)this.m_pOpoPrmValCtx->pBltVal);
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = null;
					decimal[] array2 = null;
					IntPtr zero2 = IntPtr.Zero;
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						array = new OracleDecimal[this.m_arrBindCount];
					}
					else
					{
						array2 = new decimal[this.m_arrBindCount];
					}
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							zero2 = IntPtr.Zero;
							if (this.m_enumType == PrmEnumType.ORADBTYPE || this.m_precision != 100 || this.m_scale != 129)
							{
								int num2 = 0;
								try
								{
									num2 = OpsDec.AllocValCtxFromBytes((IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)), out zero2);
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
									num2 = ErrRes.INT_ERR;
									throw;
								}
								finally
								{
									if (num2 != 0)
									{
										if (zero2 != IntPtr.Zero)
										{
											try
											{
												OpsDec.FreeValCtx(zero2);
											}
											catch (Exception ex4)
											{
												if (OraTrace.m_TraceLevel != 0U)
												{
													OraTrace.TraceExceptionInfo(ex4);
												}
											}
											zero2 = IntPtr.Zero;
										}
										if (num2 != ErrRes.INT_ERR)
										{
											throw new OracleTypeException(num2, new object[0]);
										}
									}
								}
							}
							if (this.m_precision != 100 || this.m_scale != 129)
							{
								OracleDecimal value2 = new OracleDecimal(zero2, false);
								OracleDecimal oracleDecimal2 = OracleDecimal.Null;
								if (this.m_precision != 100 && this.m_scale != 129)
								{
									oracleDecimal2 = OracleDecimal.ConvertToPrecScale(value2, (int)this.m_precision, (int)this.m_scale);
								}
								else if (this.m_precision != 100)
								{
									oracleDecimal2 = OracleDecimal.SetPrecision(value2, (int)this.m_precision);
								}
								else if (this.m_scale != 129)
								{
									oracleDecimal2 = OracleDecimal.AdjustScale(value2, (int)this.m_scale, true);
								}
								if (this.m_enumType == PrmEnumType.ORADBTYPE)
								{
									array[i] = oracleDecimal2;
								}
								else
								{
									array2[i] = oracleDecimal2.Value;
								}
							}
							else if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleDecimal(zero2, false);
							}
							else
							{
								array2[i] = DecimalConv.GetDecimal((IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
							}
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000688B4 File Offset: 0x000678B4
		private unsafe void PreBind_Date()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] == -1)
					{
						this.SetPrmValCtx(0L, i);
					}
					else
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is OracleDate)
						{
							OracleDate.ToBytes(((OracleDate)value).GetValCtx(), (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if (value is DateTime)
						{
							DateTimeConv.ToBytes((DateTime)value, (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if (value is OracleTimeStamp)
						{
							OracleDate.ToBytes(((OracleTimeStamp)value).GetValCtx(), (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if (value is OracleTimeStampLTZ)
						{
							OracleDate.ToBytes(((OracleTimeStampLTZ)value).GetValCtx(), (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if (value is OracleTimeStampTZ)
						{
							OracleDate.ToBytes(((OracleTimeStampTZ)value).GetValCtx(), (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							int num = 0;
							string preBindBuffer_Str = this.GetPreBindBuffer_Str(i);
							OpoDatValCtx opoDatValCtx;
							try
							{
								num = OpsDat.GetValCtxFromStr(preBindBuffer_Str, &opoDatValCtx);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									throw new ArgumentException(OracleTypeException.GetTypeMsg(num, new object[]
									{
										this.ParameterName
									}));
								}
							}
							OracleDate.ToBytes(&opoDatValCtx, (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							int year = (int)((array2[0] - 100) * 100 + array2[1] - 100);
							int month = (int)array2[2];
							int day = (int)array2[3];
							int hour = (int)(array2[4] - 1);
							int minute = (int)(array2[5] - 1);
							int second = (int)(array2[6] - 1);
							if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, 0))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							byte* ptr = (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8;
							for (int j = 0; j < 7; j++)
							{
								ptr[j] = array2[j];
							}
						}
						else
						{
							DateTimeConv.ToBytes(Convert.ToDateTime(value), (byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_DATE, 7, null);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00068BB4 File Offset: 0x00067BB4
		private unsafe void PostBind_Date()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleDate(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else if (this.m_bOracleDbTypeExSet)
						{
							this.m_value = new OracleDate(*(long*)this.m_pOpoPrmValCtx->pBltVal).Value;
						}
						else
						{
							this.m_value = DateTimeConv.GetDateTime((byte*)this.m_pOpoPrmValCtx->pBltVal);
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleDate.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					DateTime[] array = new DateTime[this.m_arrBindCount];
					OracleDate[] array2 = new OracleDate[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array2[i] = new OracleDate(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else if (this.m_bOracleDbTypeExSet)
							{
								array[i] = new OracleDate(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)).Value;
							}
							else
							{
								array[i] = DateTimeConv.GetDateTime((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array2[i] = OracleDate.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array2;
					}
					else
					{
						this.m_value = array;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00068DE4 File Offset: 0x00067DE4
		private unsafe void PreBind_Byte()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array = this.m_value as Array;
							if (array == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is byte)
						{
							this.SetPrmValCtx((byte)value, i);
						}
						else if (value is OracleDecimal)
						{
							this.SetPrmValCtx(((OracleDecimal)value).ToByte(), i);
						}
						else if (value is OracleString)
						{
							this.SetPrmValCtx(Convert.ToByte(((OracleString)value).Value), i);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							byte b = 0;
							GCHandle gchandle = default(GCHandle);
							try
							{
								gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
								b = Convert.ToByte(DecimalConv.GetDecimal(gchandle.AddrOfPinnedObject()));
							}
							finally
							{
								if (gchandle.IsAllocated)
								{
									gchandle.Free();
								}
							}
							this.SetPrmValCtx(b, i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToByte(value), i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_SB1, 1, null);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00068F78 File Offset: 0x00067F78
		private unsafe void PostBind_Byte()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal((int)(*(byte*)this.m_pOpoPrmValCtx->pBltVal));
						}
						else
						{
							this.m_value = *(byte*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					byte[] array2 = new byte[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = new OracleDecimal((int)((byte*)this.m_pOpoPrmValCtx->pBltVal)[i]);
							}
							else
							{
								array2[i] = ((byte*)this.m_pOpoPrmValCtx->pBltVal)[i];
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00069118 File Offset: 0x00068118
		private unsafe void PreBind_Double(OracleConnection conn, OracleDbType OraDbType)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is double)
						{
							this.SetPrmValCtx((double)value, i);
						}
						else if (value is OracleDecimal)
						{
							this.SetPrmValCtx(((OracleDecimal)value).ToDouble(), i);
						}
						else if (value is OracleString)
						{
							this.SetPrmValCtx(Convert.ToDouble(((OracleString)value).Value), i);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							double d = 0.0;
							GCHandle gchandle = default(GCHandle);
							try
							{
								gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
								d = (double)DecimalConv.GetNum(gchandle.AddrOfPinnedObject(), DbType.Double);
							}
							finally
							{
								if (gchandle.IsAllocated)
								{
									gchandle.Free();
								}
							}
							this.SetPrmValCtx(d, i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToDouble(value), i);
						}
					}
				}
			}
			if (OraDbType != OracleDbType.BinaryDouble)
			{
				this.SetPrmValCtx(OraType.ORA_FLOAT, 8, null);
				return;
			}
			if (conn.m_majorVersion < 10)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
			}
			this.SetPrmValCtx(OraType.ORA_BDOUBLE, 8, null);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000692F0 File Offset: 0x000682F0
		private unsafe void PostBind_Double()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal(*(double*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = *(double*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					double[] array2 = new double[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleDecimal(*(double*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array2[i] = *(double*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0.0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000694A0 File Offset: 0x000684A0
		private unsafe void PreBind_Int16()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is short)
						{
							this.SetPrmValCtx((short)value, i);
						}
						else if (value is OracleDecimal)
						{
							this.SetPrmValCtx(((OracleDecimal)value).ToInt16(), i);
						}
						else if (value is OracleString)
						{
							this.SetPrmValCtx(Convert.ToInt16(((OracleString)value).Value), i);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							short i2 = 0;
							GCHandle gchandle = default(GCHandle);
							try
							{
								gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
								i2 = (short)DecimalConv.GetNum(gchandle.AddrOfPinnedObject(), DbType.Int16);
							}
							finally
							{
								if (gchandle.IsAllocated)
								{
									gchandle.Free();
								}
							}
							this.SetPrmValCtx(i2, i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToInt16(value), i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_SB1, 2, null);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00069638 File Offset: 0x00068638
		private unsafe void PostBind_Int16()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal((int)(*(short*)this.m_pOpoPrmValCtx->pBltVal));
						}
						else
						{
							this.m_value = *(short*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					short[] array2 = new short[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleDecimal((int)(*(short*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 2)));
							}
							else
							{
								array2[i] = *(short*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 2);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000697E0 File Offset: 0x000687E0
		private unsafe void PreBind_Int32()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is int)
						{
							this.SetPrmValCtx((int)value, i);
						}
						else if (value is OracleDecimal)
						{
							this.SetPrmValCtx(((OracleDecimal)value).ToInt32(), i);
						}
						else if (value is OracleString)
						{
							this.SetPrmValCtx(Convert.ToInt32(((OracleString)value).Value), i);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							int i2 = 0;
							GCHandle gchandle = default(GCHandle);
							try
							{
								gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
								i2 = (int)DecimalConv.GetNum(gchandle.AddrOfPinnedObject(), DbType.Int32);
							}
							finally
							{
								if (gchandle.IsAllocated)
								{
									gchandle.Free();
								}
							}
							this.SetPrmValCtx(i2, i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToInt32(value), i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_SB1, 4, null);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00069978 File Offset: 0x00068978
		private unsafe void PostBind_Int32()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal(*(int*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = *(int*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					int[] array2 = new int[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleDecimal(*(int*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 4));
							}
							else
							{
								array2[i] = *(int*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 4);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00069B20 File Offset: 0x00068B20
		private unsafe void PreBind_Int64()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is long)
						{
							this.SetPrmValCtx((long)value, i);
						}
						else if (value is OracleDecimal)
						{
							this.SetPrmValCtx(((OracleDecimal)value).ToInt64(), i);
						}
						else if (value is OracleString)
						{
							this.SetPrmValCtx(Convert.ToInt64(((OracleString)value).Value), i);
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							long i2 = 0L;
							GCHandle gchandle = default(GCHandle);
							try
							{
								gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
								i2 = (long)DecimalConv.GetNum(gchandle.AddrOfPinnedObject(), DbType.Int64);
							}
							finally
							{
								if (gchandle.IsAllocated)
								{
									gchandle.Free();
								}
							}
							this.SetPrmValCtx(i2, i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToInt64(value), i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_VARNUM, 22, null);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00069CBC File Offset: 0x00068CBC
		private unsafe void PostBind_Int64()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = *(long*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					long[] array2 = new long[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleDecimal(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22));
							}
							else
							{
								array2[i] = *(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0L;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00069E68 File Offset: 0x00068E68
		private unsafe void PreBind_Single(OracleConnection conn, OracleDbType OraDbType)
		{
			bool flag = false;
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				OracleDecimal oracleDecimal;
				oracleDecimal.m_opoDecCtx = null;
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] array2;
						if (value is float)
						{
							oracleDecimal = new OracleDecimal((double)((float)value));
						}
						else if (value is OracleDecimal)
						{
							oracleDecimal = (OracleDecimal)value;
							flag = true;
						}
						else if (value is OracleString)
						{
							oracleDecimal = new OracleDecimal(((OracleString)value).Value);
							flag = true;
						}
						else if ((array2 = (value as byte[])) != null)
						{
							if (array2.Length != 22)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							oracleDecimal = new OracleDecimal(array2);
							flag = true;
						}
						else
						{
							oracleDecimal = new OracleDecimal((double)Convert.ToSingle(value));
						}
						if (OraDbType != OracleDbType.BinaryFloat)
						{
							try
							{
								OpsDec.GetValCtxForSetPrecNoRound(oracleDecimal.m_opoDecCtx.m_pValCtx, 7, (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22)));
								goto IL_187;
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								throw;
							}
						}
						if (flag)
						{
							this.SetPrmValCtx(oracleDecimal.ToSingle(), i);
						}
						else
						{
							this.SetPrmValCtx(Convert.ToSingle(value), i);
						}
					}
					IL_187:;
				}
			}
			if (OraDbType != OracleDbType.BinaryFloat)
			{
				this.SetPrmValCtx(OraType.ORA_VARNUM, 22, null);
				return;
			}
			if (conn.m_majorVersion < 10)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
			}
			this.SetPrmValCtx(OraType.ORA_BFLOAT, 4, null);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0006A064 File Offset: 0x00069064
		private unsafe void PostBind_Single()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = new OracleDecimal(*(float*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = *(float*)this.m_pOpoPrmValCtx->pBltVal;
						}
						this.m_status = OracleParameterStatus.Success;
						return;
					}
					this.m_curSize = 0;
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = OracleDecimal.Null;
					}
					else
					{
						this.m_value = DBNull.Value;
					}
					this.m_status = OracleParameterStatus.NullFetched;
					return;
				}
				else
				{
					OracleDecimal[] array = new OracleDecimal[this.m_arrBindCount];
					float[] array2 = new float[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								if (this.m_oraDbType == OracleDbType.BinaryFloat)
								{
									array[i] = new OracleDecimal(*(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 4));
								}
								else
								{
									array[i] = new OracleDecimal(*(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22));
								}
							}
							else if (this.m_bOracleDbTypeExSet)
							{
								if (this.m_oraDbType == OracleDbType.BinaryFloat)
								{
									array2[i] = *(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 4);
								}
								else
								{
									array2[i] = *(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22);
								}
							}
							else
							{
								array2[i] = *(float*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 22);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleDecimal.Null;
							}
							else
							{
								array2[i] = 0f;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = array;
						return;
					}
					this.m_value = array2;
				}
				break;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0006A288 File Offset: 0x00069288
		private unsafe void PreBind_IntervalDS()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] binData;
						if (value is OracleIntervalDS)
						{
							this.SetPrmValCtx((void*)((OracleIntervalDS)value).GetValCtx(), i);
						}
						else if (value is TimeSpan)
						{
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)OracleIntervalDS.AllocValCtxFromData((TimeSpan)value), i);
							}
							else
							{
								OracleIntervalDS oracleIntervalDS = new OracleIntervalDS((TimeSpan)value);
								this.SetPrmValCtx((void*)oracleIntervalDS.GetValCtx(), i);
								this.m_saveValue[i] = oracleIntervalDS;
							}
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							OracleIntervalDS oracleIntervalDS = new OracleIntervalDS(this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx((void*)oracleIntervalDS.GetValCtx(), i);
							this.m_saveValue[i] = oracleIntervalDS;
						}
						else if ((binData = (value as byte[])) != null)
						{
							OracleIntervalDS oracleIntervalDS = new OracleIntervalDS(binData);
							this.SetPrmValCtx((void*)oracleIntervalDS.GetValCtx(), i);
							this.m_saveValue[i] = oracleIntervalDS;
						}
						else
						{
							if (!(value is decimal))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							TimeSpan data = TimeSpan.FromSeconds((double)((decimal)value));
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)OracleIntervalDS.AllocValCtxFromData(data), i);
							}
							else
							{
								OracleIntervalDS oracleIntervalDS = new OracleIntervalDS(data);
								this.SetPrmValCtx((void*)oracleIntervalDS.GetValCtx(), i);
								this.m_saveValue[i] = oracleIntervalDS;
							}
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_INTERVAL_DS, 0, null);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0006A4C0 File Offset: 0x000694C0
		private unsafe void PostBind_IntervalDS()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
					{
						if (!(this.m_value is TimeSpan))
						{
							if (!(this.m_value is TimeSpan[]))
							{
								goto IL_275;
							}
						}
						try
						{
							OpsIDS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
					IL_275:;
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleIntervalDS(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = TimeSpanConv.GetTimeSpan(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.IntervalDS);
							try
							{
								OpsIDS.FreeValCtx(*(long*)this.m_pOpoPrmValCtx->pBltVal);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleIntervalDS.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					OracleIntervalDS[] array = new OracleIntervalDS[this.m_arrBindCount];
					TimeSpan[] array2 = new TimeSpan[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleIntervalDS(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array2[i] = TimeSpanConv.GetTimeSpan(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8), OracleDbType.IntervalDS);
								try
								{
									OpsIDS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleIntervalDS.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
					}
					else
					{
						this.m_value = array2;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0006A780 File Offset: 0x00069780
		private unsafe void PreBind_IntervalYM()
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] binData;
						if (value is OracleIntervalYM)
						{
							this.SetPrmValCtx((void*)((OracleIntervalYM)value).GetValCtx(), i);
						}
						else if (value is byte || value is short || value is int || value is long)
						{
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)OracleIntervalYM.AllocValCtxFromData((long)value), i);
							}
							else
							{
								OracleIntervalYM oracleIntervalYM = new OracleIntervalYM((long)value);
								this.SetPrmValCtx((void*)oracleIntervalYM.GetValCtx(), i);
								this.m_saveValue[i] = oracleIntervalYM;
							}
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							OracleIntervalYM oracleIntervalYM = new OracleIntervalYM(this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx((void*)oracleIntervalYM.GetValCtx(), i);
							this.m_saveValue[i] = oracleIntervalYM;
						}
						else if ((binData = (value as byte[])) != null)
						{
							OracleIntervalYM oracleIntervalYM = new OracleIntervalYM(binData);
							this.SetPrmValCtx((void*)oracleIntervalYM.GetValCtx(), i);
							this.m_saveValue[i] = oracleIntervalYM;
						}
						else
						{
							if (!(value is decimal))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							long num = (long)((decimal)value);
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)OracleIntervalYM.AllocValCtxFromData(num), i);
							}
							else
							{
								OracleIntervalYM oracleIntervalYM = new OracleIntervalYM(num);
								this.SetPrmValCtx((void*)oracleIntervalYM.GetValCtx(), i);
								this.m_saveValue[i] = oracleIntervalYM;
							}
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_INTERVAL_YM, 0, null);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0006A9CC File Offset: 0x000699CC
		private unsafe void PostBind_IntervalYM()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
					{
						if (!(this.m_value is byte) && !(this.m_value is short) && !(this.m_value is int) && !(this.m_value is long) && (!this.m_bArrayBind || !(this.m_value is byte[])) && !(this.m_value is short[]) && !(this.m_value is int[]))
						{
							if (!(this.m_value is long[]))
							{
								goto IL_2CE;
							}
						}
						try
						{
							OpsIYM.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
					IL_2CE:;
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleIntervalYM(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = LongConv.GetLong(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.IntervalYM);
							try
							{
								OpsIYM.FreeValCtx(*(long*)this.m_pOpoPrmValCtx->pBltVal);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleIntervalYM.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					OracleIntervalYM[] array = new OracleIntervalYM[this.m_arrBindCount];
					long[] array2 = new long[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleIntervalYM(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array2[i] = LongConv.GetLong(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8), OracleDbType.IntervalYM);
								try
								{
									OpsIYM.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = OracleIntervalYM.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (this.m_enumType == PrmEnumType.ORADBTYPE)
					{
						this.m_value = array;
					}
					else
					{
						this.m_value = array2;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0006ACE8 File Offset: 0x00069CE8
		private unsafe void PreBind_Raw()
		{
			IntPtr zero = IntPtr.Zero;
			int[] array = null;
			int num = 0;
			int num2 = 0;
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				if (this.m_bArrayBind)
				{
					array = new int[this.m_arrBindCount];
					for (int i = 0; i < this.m_bindElemCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] == -1)
						{
							array[i] = 0;
						}
						else
						{
							array[i] = this.GetBindingSize_Raw(i);
						}
						if (num2 < array[i])
						{
							num2 = array[i];
						}
						if (this.m_direction == ParameterDirection.InputOutput && this.m_maxArrayBindSize != null && num2 < this.m_maxArrayBindSize[i])
						{
							num2 = this.m_maxArrayBindSize[i];
						}
					}
				}
				else
				{
					if (*this.m_pOpoPrmValCtx->pInd == -1)
					{
						num = (num2 = 0);
					}
					else
					{
						num = (num2 = this.GetBindingSize_Raw(0));
					}
					if (this.m_direction == ParameterDirection.InputOutput && num < this.m_maxSize)
					{
						num2 = this.m_maxSize;
					}
				}
				if (num2 > 0)
				{
					try
					{
						this.m_pDataBuffer = Marshal.AllocCoTaskMem(num2 * this.m_arrBindCount);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					if (this.m_pDataBuffer != IntPtr.Zero)
					{
						for (int j = 0; j < this.m_bindElemCount; j++)
						{
							object value;
							int num3;
							if (!this.m_bArrayBind)
							{
								value = this.m_value;
								num3 = num;
							}
							else
							{
								Array array2;
								if ((array2 = (this.m_value as Array)) == null)
								{
									throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
								}
								value = array2.GetValue(j);
								num3 = array[j];
							}
							if (num3 > 0)
							{
								byte[] source;
								if ((source = (value as byte[])) != null)
								{
									Marshal.Copy(source, this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)j * (IntPtr)num2)), num3);
								}
								else if (value is OracleBinary)
								{
									Marshal.Copy((byte[])((OracleBinary)value), this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)j * (IntPtr)num2)), num3);
								}
								else
								{
									if (!(value is Guid))
									{
										throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
									}
									Marshal.Copy(((Guid)value).ToByteArray(), this.m_offset, (IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)j * (IntPtr)num2)), num3);
								}
							}
						}
						this.SetPrmValCtx(this.m_pDataBuffer, 0);
					}
				}
				if (this.m_bArrayBind)
				{
					this.SetPrmValCtx(OraType.ORA_RAW, num2, array);
					return;
				}
				if (this.m_oraDbType == OracleDbType.Blob)
				{
					this.SetPrmValCtx(OraType.ORA_RAW, num, null, OracleDbType.Raw);
					return;
				}
				this.SetPrmValCtx(OraType.ORA_RAW, num, null);
				return;
			}
			else
			{
				if (!this.m_bArrayBind)
				{
					if (this.m_maxSize == -1)
					{
						num2 = 0;
					}
					else
					{
						num2 = this.m_maxSize;
					}
				}
				else if (this.m_maxArrayBindSize != null)
				{
					num2 = this.m_maxArrayBindSize[0];
					for (int k = 0; k < this.m_arrBindCount; k++)
					{
						if (this.m_maxArrayBindSize[k] > num2)
						{
							num2 = this.m_maxArrayBindSize[k];
						}
					}
					if (num2 == -1)
					{
						num2 = 0;
					}
				}
				else
				{
					num2 = 0;
				}
				try
				{
					this.m_pDataBuffer = Marshal.AllocCoTaskMem(num2 * this.m_arrBindCount);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				if (this.m_pDataBuffer != IntPtr.Zero)
				{
					this.SetPrmValCtx(this.m_pDataBuffer, 0);
				}
				if (!this.m_bArrayBind)
				{
					this.SetPrmValCtx(OraType.ORA_RAW, num2, null);
					return;
				}
				this.SetPrmValCtx(OraType.ORA_RAW, num2, this.m_maxArrayBindSize);
				return;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0006B080 File Offset: 0x0006A080
		private unsafe void PostBind_Raw()
		{
			try
			{
				switch (this.m_direction)
				{
				case ParameterDirection.Output:
				case ParameterDirection.InputOutput:
				case ParameterDirection.ReturnValue:
					if (!this.m_bArrayBind)
					{
						if (*this.m_pOpoPrmValCtx->pInd != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								byte[] array = new byte[(int)(*this.m_pOpoPrmValCtx->alenp)];
								Marshal.Copy(this.m_pDataBuffer, array, 0, (int)(*this.m_pOpoPrmValCtx->alenp));
								this.m_value = new OracleBinary(array, false);
							}
							else
							{
								this.m_value = new byte[(int)(*this.m_pOpoPrmValCtx->alenp)];
								Marshal.Copy(this.m_pDataBuffer, (byte[])this.m_value, 0, (int)(*this.m_pOpoPrmValCtx->alenp));
							}
							this.m_curSize = (int)(*this.m_pOpoPrmValCtx->alenp);
							this.m_status = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								this.m_value = OracleBinary.Null;
							}
							else
							{
								this.m_value = DBNull.Value;
							}
							this.m_status = OracleParameterStatus.NullFetched;
						}
					}
					else
					{
						OracleBinary[] array2 = null;
						byte[][] array3 = null;
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							array2 = new OracleBinary[this.m_arrBindCount];
						}
						else
						{
							array3 = new byte[this.m_arrBindCount][];
						}
						for (int i = 0; i < this.m_arrBindCount; i++)
						{
							int num;
							if ((int)this.m_pOpoPrmValCtx->alenp[i] > this.m_maxArrayBindSize[i])
							{
								if (this.m_maxArrayBindSize[i] > -1)
								{
									num = this.m_maxArrayBindSize[i];
								}
								else
								{
									num = 0;
								}
							}
							else
							{
								num = (int)this.m_pOpoPrmValCtx->alenp[i];
							}
							if (this.m_pOpoPrmValCtx->pInd[i] != -1)
							{
								if (this.m_enumType == PrmEnumType.ORADBTYPE)
								{
									byte[] array4 = new byte[num];
									Marshal.Copy((IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)this.m_pOpoPrmValCtx->Size * (IntPtr)i)), array4, 0, num);
									array2[i] = new OracleBinary(array4, false);
								}
								else
								{
									array3[i] = new byte[num];
									Marshal.Copy((IntPtr)((void*)((byte*)((void*)this.m_pDataBuffer) + (IntPtr)this.m_pOpoPrmValCtx->Size * (IntPtr)i)), array3[i], 0, num);
								}
								this.m_curArrayBindSize[i] = num;
								this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
							}
							else
							{
								this.m_curSize = 0;
								this.m_curArrayBindSize[i] = 0;
								if (this.m_enumType == PrmEnumType.ORADBTYPE)
								{
									array2[i] = OracleBinary.Null;
								}
								else
								{
									array3[i] = null;
								}
								this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
							}
						}
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = array2;
						}
						else
						{
							this.m_value = array3;
						}
					}
					break;
				}
			}
			finally
			{
				if (this.m_pDataBuffer != IntPtr.Zero)
				{
					try
					{
						Marshal.FreeCoTaskMem(this.m_pDataBuffer);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_pDataBuffer = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0006B3B0 File Offset: 0x0006A3B0
		private unsafe void PreBind_RefCursor(OracleConnection conn)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							if (!(this.m_value is Array))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = ((Array)this.m_value).GetValue(i);
						}
						if (!(value is OracleRefCursor))
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
						}
						OracleRefCursor oracleRefCursor = (OracleRefCursor)value;
						if (oracleRefCursor.m_connection != conn && (!oracleRefCursor.m_connection.m_contextConnection || !conn.m_contextConnection))
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
						}
						if (oracleRefCursor.m_conSignature != conn.m_conSignature)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
						}
						if (oracleRefCursor.SqlCtx != IntPtr.Zero)
						{
							this.SetPrmValCtx(oracleRefCursor.SqlCtx, i);
						}
						else
						{
							this.m_pOpoPrmValCtx->pInd[i] = -1;
							this.m_pOpoPrmValCtx->pSrcInd[i] = -1;
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_RESULTSET, -1, null);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0006B528 File Offset: 0x0006A528
		private unsafe void PostBind_RefCursor(OracleConnection conn, OpoSqlValCtx* pOpoSqlValCtx, string cmdText, string posOrName)
		{
			OracleRefCursor[] array = null;
			switch (this.m_direction)
			{
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						OpoSqlValCtx* pOpoSqlValCtx2 = null;
						try
						{
							OpsSql.CopySqlValCtx(pOpoSqlValCtx, ref pOpoSqlValCtx2);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						this.m_value = new OracleRefCursor(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), pOpoSqlValCtx2, cmdText, posOrName);
						pOpoSqlValCtx2 = null;
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleRefCursor.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleRefCursor[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							OpoSqlValCtx* pOpoSqlValCtx2 = null;
							try
							{
								OpsSql.CopySqlValCtx(pOpoSqlValCtx, ref pOpoSqlValCtx2);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
								throw;
							}
							array[i] = new OracleRefCursor(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), pOpoSqlValCtx2, cmdText, posOrName);
							pOpoSqlValCtx2 = null;
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleRefCursor.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
						{
							OpoSqlValCtx* pOpoSqlValCtx2 = null;
							try
							{
								OpsSql.CopySqlValCtx(pOpoSqlValCtx, ref pOpoSqlValCtx2);
							}
							catch (Exception ex3)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex3);
								}
								throw;
							}
							this.m_value = new OracleRefCursor(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), pOpoSqlValCtx2, cmdText, posOrName);
							pOpoSqlValCtx2 = null;
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						if (this.m_value != DBNull.Value)
						{
							((OracleRefCursor)this.m_value).Dispose();
						}
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleRefCursor.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleRefCursor[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								OpoSqlValCtx* pOpoSqlValCtx2 = null;
								try
								{
									OpsSql.CopySqlValCtx(pOpoSqlValCtx, ref pOpoSqlValCtx2);
								}
								catch (Exception ex4)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex4);
									}
									throw;
								}
								array[i] = new OracleRefCursor(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), pOpoSqlValCtx2, cmdText, posOrName);
								pOpoSqlValCtx2 = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (((OracleRefCursor[])this.m_value)[i] != null)
							{
								((OracleRefCursor[])this.m_value)[i].Dispose();
							}
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleRefCursor.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_enumType == PrmEnumType.DBTYPE && this.m_bOracleDbTypeExSet && this.m_direction != ParameterDirection.Input)
			{
				if (this.m_bArrayBind)
				{
					OracleDataReader[] array2 = new OracleDataReader[this.m_arrBindCount];
					for (int j = 0; j < this.m_arrBindCount; j++)
					{
						OracleRefCursor oracleRefCursor = array[j];
						if (oracleRefCursor == null || oracleRefCursor.IsNull)
						{
							array2[j] = null;
						}
						else
						{
							array2[j] = oracleRefCursor.GetDataReader(false);
							oracleRefCursor.Dispose();
						}
					}
					this.m_value = array2;
					return;
				}
				if (this.m_value != DBNull.Value)
				{
					OracleRefCursor oracleRefCursor2 = (OracleRefCursor)this.m_value;
					if (oracleRefCursor2.IsNull)
					{
						this.m_value = DBNull.Value;
						return;
					}
					this.m_value = oracleRefCursor2.GetDataReader(false);
					oracleRefCursor2.Dispose();
				}
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0006B97C File Offset: 0x0006A97C
		private unsafe void PreBind_TimeStamp(OracleConnection conn, IntPtr errCtx)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						if (value is DateTime)
						{
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)DateTimeConv.AllocTSValCtx((DateTime)value), i);
							}
							else
							{
								OracleTimeStamp oracleTimeStamp = new OracleTimeStamp((DateTime)value);
								this.SetPrmValCtx((void*)oracleTimeStamp.GetValCtx(), i);
								this.m_saveValue[i] = oracleTimeStamp;
							}
						}
						else if (value is OracleTimeStamp)
						{
							this.SetPrmValCtx((void*)((OracleTimeStamp)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStampLTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampLTZ)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStampTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampTZ)value).GetValCtx(), i);
						}
						else if (value is OracleDate)
						{
							this.SetPrmValCtx((void*)((OracleDate)value).GetValCtx(), i);
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							OracleTimeStamp oracleTimeStamp = new OracleTimeStamp(this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx((void*)oracleTimeStamp.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStamp;
						}
						else
						{
							byte[] binData;
							if ((binData = (value as byte[])) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							OracleTimeStamp oracleTimeStamp = new OracleTimeStamp(binData);
							this.SetPrmValCtx((void*)oracleTimeStamp.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStamp;
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_TIMESTAMP, 0, null);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0006BBC0 File Offset: 0x0006ABC0
		private unsafe void PostBind_TimeStamp()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
					{
						if (!(this.m_value is DateTime) && !(this.m_value is DateTime[]))
						{
							if (!(this.m_value is Array) || !(((Array)this.m_value).GetValue(i) is DateTime))
							{
								goto IL_2A5;
							}
						}
						try
						{
							OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
					IL_2A5:;
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleTimeStamp(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = DateTimeConv.GetDateTime(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.TimeStamp, true);
							try
							{
								OpsTS.FreeValCtx(*(long*)this.m_pOpoPrmValCtx->pBltVal);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleTimeStamp.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					OracleTimeStamp[] array = new OracleTimeStamp[this.m_arrBindCount];
					DateTime[] array2 = new DateTime[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleTimeStamp(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array2[i] = DateTimeConv.GetDateTime(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8), OracleDbType.TimeStamp, true);
								try
								{
									OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleTimeStamp.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = array;
					}
					else
					{
						this.m_value = array2;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0006BEB4 File Offset: 0x0006AEB4
		private unsafe void PreBind_TimeStampLTZ(OracleConnection conn, IntPtr errCtx)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						if (value is DateTime)
						{
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)DateTimeConv.AllocTSLValCtx((DateTime)value), i);
							}
							else
							{
								OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ((DateTime)value);
								this.SetPrmValCtx((void*)oracleTimeStampLTZ.GetValCtx(), i);
								this.m_saveValue[i] = oracleTimeStampLTZ;
							}
						}
						else if (value is OracleTimeStampLTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampLTZ)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStamp)
						{
							this.SetPrmValCtx((void*)((OracleTimeStamp)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStampTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampTZ)value).GetValCtx(), i);
						}
						else if (value is OracleDate)
						{
							this.SetPrmValCtx((void*)((OracleDate)value).GetValCtx(), i);
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ(this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx((void*)oracleTimeStampLTZ.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStampLTZ;
						}
						else
						{
							byte[] binData;
							if ((binData = (value as byte[])) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ(binData);
							this.SetPrmValCtx((void*)oracleTimeStampLTZ.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStampLTZ;
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_TIMESTAMP_LTZ, 0, null);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0006C0F8 File Offset: 0x0006B0F8
		private unsafe void PostBind_TimeStampLTZ()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
					{
						if (!(this.m_value is DateTime) && !(this.m_value is DateTime[]))
						{
							if (!(this.m_value is Array) || !(((Array)this.m_value).GetValue(i) is DateTime))
							{
								goto IL_2A5;
							}
						}
						try
						{
							OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
					IL_2A5:;
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleTimeStampLTZ(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							this.m_value = DateTimeConv.GetDateTime(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.TimeStampLTZ, true);
							try
							{
								OpsTS.FreeValCtx(*(long*)this.m_pOpoPrmValCtx->pBltVal);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleTimeStampLTZ.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					OracleTimeStampLTZ[] array = new OracleTimeStampLTZ[this.m_arrBindCount];
					DateTime[] array2 = new DateTime[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array[i] = new OracleTimeStampLTZ(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array2[i] = DateTimeConv.GetDateTime(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8), OracleDbType.TimeStampLTZ, true);
								try
								{
									OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleTimeStampLTZ.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = array;
					}
					else
					{
						this.m_value = array2;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0006C3EC File Offset: 0x0006B3EC
		private unsafe void PreBind_TimeStampTZ(OracleConnection conn, IntPtr errCtx)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						byte[] binData;
						if (value is DateTime)
						{
							if (this.m_direction == ParameterDirection.Input)
							{
								this.SetPrmValCtx((void*)DateTimeConv.AllocTSZValCtx((DateTime)value), i);
							}
							else
							{
								OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ((DateTime)value);
								this.SetPrmValCtx((void*)oracleTimeStampTZ.GetValCtx(), i);
								this.m_saveValue[i] = oracleTimeStampTZ;
							}
						}
						else if (value is OracleTimeStampTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampTZ)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStamp)
						{
							this.SetPrmValCtx((void*)((OracleTimeStamp)value).GetValCtx(), i);
						}
						else if (value is OracleTimeStampLTZ)
						{
							this.SetPrmValCtx((void*)((OracleTimeStampLTZ)value).GetValCtx(), i);
						}
						else if (value is OracleDate)
						{
							this.SetPrmValCtx((void*)((OracleDate)value).GetValCtx(), i);
						}
						else if (value is string || value is char[] || value is OracleString || value is char)
						{
							OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx((void*)oracleTimeStampTZ.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStampTZ;
						}
						else if ((binData = (value as byte[])) != null)
						{
							OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(binData);
							this.SetPrmValCtx((void*)oracleTimeStampTZ.GetValCtx(), i);
							this.m_saveValue[i] = oracleTimeStampTZ;
						}
						else
						{
							if (!(value is DateTimeOffset))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(((DateTimeOffset)value).DateTime, ((DateTimeOffset)value).Offset.ToString());
							this.SetPrmValCtx((void*)oracleTimeStampTZ.GetValCtx(), i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_TIMESTAMP_TZ, 0, null);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0006C680 File Offset: 0x0006B680
		private unsafe void PostBind_TimeStampTZ()
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
					{
						if (!(this.m_value is DateTime) && !(this.m_value is DateTime[]))
						{
							if (!(this.m_value is Array) || !(((Array)this.m_value).GetValue(i) is DateTime))
							{
								goto IL_2D2;
							}
						}
						try
						{
							OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
					IL_2D2:;
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (this.m_enumType == PrmEnumType.ORADBTYPE)
						{
							this.m_value = new OracleTimeStampTZ(*(long*)this.m_pOpoPrmValCtx->pBltVal);
						}
						else
						{
							if (this.m_bReturnDateTimeOffset)
							{
								this.m_value = DateTimeConv.GetDateTimeOffset(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.TimeStampTZ, true);
							}
							else
							{
								this.m_value = DateTimeConv.GetDateTime(*(long*)this.m_pOpoPrmValCtx->pBltVal, OracleDbType.TimeStampTZ, true);
							}
							try
							{
								OpsTS.FreeValCtx(*(long*)this.m_pOpoPrmValCtx->pBltVal);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleTimeStampTZ.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					DateTime[] array = new DateTime[this.m_arrBindCount];
					OracleTimeStampTZ[] array2 = new OracleTimeStampTZ[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_enumType == PrmEnumType.ORADBTYPE)
							{
								array2[i] = new OracleTimeStampTZ(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
							}
							else
							{
								array[i] = DateTimeConv.GetDateTime(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8), OracleDbType.TimeStampTZ, true);
								try
								{
									OpsTS.FreeValCtx(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8));
								}
								catch (Exception ex3)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex3);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array2[i] = OracleTimeStampTZ.Null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					if (PrmEnumType.ORADBTYPE == this.m_enumType)
					{
						this.m_value = array2;
					}
					else
					{
						this.m_value = array;
					}
				}
				break;
			}
			this.m_saveValue = null;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0006C9A0 File Offset: 0x0006B9A0
		private unsafe void PreBind_XmlType(OracleConnection conn)
		{
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							Array array;
							if ((array = (this.m_value as Array)) == null)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = array.GetValue(i);
						}
						OracleXmlType oracleXmlType;
						OracleClob oracleClob;
						if ((oracleXmlType = (value as OracleXmlType)) != null)
						{
							if (oracleXmlType.m_connection != conn && (!oracleXmlType.m_connection.m_contextConnection || !conn.m_contextConnection))
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
							}
							if (oracleXmlType.m_conSignature != conn.m_conSignature)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
							}
							this.SetPrmValCtx(oracleXmlType.OpsXmlTypeCtx, i);
						}
						else if ((oracleClob = (value as OracleClob)) != null)
						{
							if (oracleClob.m_connection != conn && (!oracleClob.m_connection.m_contextConnection || !conn.m_contextConnection))
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
							}
							if (oracleClob.m_conSignature != conn.m_conSignature)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
							}
							oracleXmlType = new OracleXmlType(oracleClob);
							this.SetPrmValCtx(oracleXmlType.OpsXmlTypeCtx, i);
							this.m_saveValue[i] = oracleXmlType;
						}
						else
						{
							oracleXmlType = new OracleXmlType(conn, this.GetPreBindBuffer_Str(i));
							this.SetPrmValCtx(oracleXmlType.OpsXmlTypeCtx, i);
							this.m_saveValue[i] = oracleXmlType;
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_NDT, 0, null);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0006CB78 File Offset: 0x0006BB78
		private unsafe void PostBind_XmlType(OracleConnection conn)
		{
			OracleXmlType[] array = null;
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1 && !(this.m_value is OracleXmlType) && !this.IsElemType(typeof(OracleXmlType), this.m_value, i))
					{
						((OracleXmlType)this.m_saveValue[i]).Dispose();
						this.m_saveValue[i] = null;
					}
					this.SetPrmValCtx(IntPtr.Zero, i);
				}
				this.m_saveValue = null;
				break;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						this.m_value = new OracleXmlType(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false);
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleXmlType.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleXmlType[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (*this.m_pOpoPrmValCtx->pInd != -1)
						{
							array[i] = new OracleXmlType(conn, (IntPtr)(*(long*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)i * 8)), false);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleXmlType.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!this.m_bArrayBind)
				{
					if (*this.m_pOpoPrmValCtx->pInd != -1)
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
						{
							this.m_value = new OracleXmlType(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal), false);
						}
						else if (!(this.m_value is OracleXmlType))
						{
							this.m_value = this.m_saveValue[0];
							this.m_saveValue[0] = null;
							this.m_saveValue = null;
						}
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						if (*this.m_pOpoPrmValCtx->pSrcInd != -1)
						{
							if (this.m_value is OracleXmlType)
							{
								((OracleXmlType)this.m_value).Dispose();
								this.m_value = null;
							}
							else
							{
								if (this.m_value is OracleClob)
								{
									((OracleClob)this.m_value).Dispose();
								}
								((OracleXmlType)this.m_saveValue[0]).Dispose();
								this.m_saveValue[0] = null;
								this.m_saveValue = null;
							}
						}
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							this.m_value = OracleXmlType.Null;
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					array = new OracleXmlType[this.m_arrBindCount];
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								array[i] = new OracleXmlType(conn, (IntPtr)(*(long*)this.m_pOpoPrmValCtx->pBltVal + (long)i), false);
							}
							else if (this.m_value is OracleXmlType[])
							{
								array[i] = ((OracleXmlType[])this.m_value)[i];
							}
							else
							{
								array[i] = (OracleXmlType)this.m_saveValue[i];
								this.m_saveValue[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
							{
								if (this.m_value is OracleXmlType[])
								{
									((OracleXmlType[])this.m_value)[i].Dispose();
								}
								else
								{
									if (this.m_value is OracleClob[])
									{
										((OracleClob[])this.m_value)[i].Dispose();
									}
									((OracleXmlType)this.m_saveValue[i]).Dispose();
									this.m_saveValue[i] = null;
								}
							}
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array[i] = OracleXmlType.Null;
							}
							else
							{
								array[i] = null;
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_saveValue = null;
					this.m_value = array;
				}
				break;
			}
			if (this.m_enumType == PrmEnumType.DBTYPE && this.m_bOracleDbTypeExSet && this.m_direction != ParameterDirection.Input)
			{
				if (this.m_bArrayBind)
				{
					string[] array2 = new string[this.m_arrBindCount];
					for (int j = 0; j < this.m_arrBindCount; j++)
					{
						OracleXmlType oracleXmlType = array[j];
						if (oracleXmlType == null || oracleXmlType.IsNull)
						{
							array2[j] = null;
						}
						else
						{
							array2[j] = oracleXmlType.Value;
							oracleXmlType.Dispose();
						}
					}
					this.m_value = array2;
					return;
				}
				if (this.m_value != DBNull.Value)
				{
					OracleXmlType oracleXmlType2 = (OracleXmlType)this.m_value;
					if (oracleXmlType2.IsNull)
					{
						this.m_value = DBNull.Value;
						return;
					}
					this.m_value = oracleXmlType2.Value;
					oracleXmlType2.Dispose();
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0006D07C File Offset: 0x0006C07C
		internal void FreeDataBuffer()
		{
			if (this.m_pDataBuffer != IntPtr.Zero)
			{
				try
				{
					Marshal.FreeCoTaskMem(this.m_pDataBuffer);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_pDataBuffer = IntPtr.Zero;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0006D0D4 File Offset: 0x0006C0D4
		private unsafe OracleRef CreateOracleRef(OracleConnection conn, int index)
		{
			IntPtr pOCIRef = (IntPtr)(*(IntPtr*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * (IntPtr)sizeof(void*)));
			IntPtr pObjInd = (IntPtr)(*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)index * (IntPtr)sizeof(void*)));
			OpoUdtCtx opoUdtCtx = new OpoUdtCtx(conn.m_opoConCtx.opsConCtx, IntPtr.Zero, pOCIRef, pObjInd);
			if (this.m_pOpoPrmValCtx->bIsFinalType == 0)
			{
				return new OracleRef(conn, opoUdtCtx);
			}
			return new OracleRef(this.m_udtDescriptor, opoUdtCtx);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0006D158 File Offset: 0x0006C158
		private string GetPreBindStr(object bindValue)
		{
			string result = null;
			if (bindValue is string)
			{
				result = (string)bindValue;
			}
			else if (bindValue is char[])
			{
				result = new string((char[])bindValue);
			}
			else if (bindValue is OracleString)
			{
				if (((OracleString)bindValue).IsNull)
				{
					result = string.Empty;
				}
				else
				{
					result = ((OracleString)bindValue).Value;
				}
			}
			return result;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0006D1C0 File Offset: 0x0006C1C0
		private void PreBind_Object(OracleConnection conn)
		{
			if (conn.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (this.m_enumType == PrmEnumType.ORADBTYPE)
			{
				this.PreBind_OracleObject(conn);
				return;
			}
			this.SetUdtDescriptor(conn);
			this.m_oraDbType = this.m_udtDescriptor.OracleDbType;
			if (this.m_oraDbType == OracleDbType.Array)
			{
				this.PreBind_Collection(conn);
				return;
			}
			this.PreBind_OracleObject(conn);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0006D230 File Offset: 0x0006C230
		private unsafe void SetUdtDescriptor(OracleConnection conn)
		{
			if (!(this.m_pOpoPrmValCtx->pOpsDscCtx == IntPtr.Zero))
			{
				return;
			}
			if (this.m_udtTypeName == null || this.m_udtTypeName.Length <= 0)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
			}
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(conn, this.m_udtTypeName);
			oracleUdtDescriptor.GetMetaDataTable();
			if (oracleUdtDescriptor != null)
			{
				this.SetPrmValCtx(oracleUdtDescriptor);
				return;
			}
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0006D2C4 File Offset: 0x0006C2C4
		private unsafe void SetUDTFromArray(OracleConnection conn, object array, int i)
		{
			int num = 0;
			if (this.m_udtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(this.m_udtDescriptor);
				this.m_udtDescriptor.DescribeCustomType(factory);
			}
			if ((IntPtr)((void*)this.m_pOpoPrmValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out this.m_pOpoPrmValCtx->pOpoUdtValCtx, this.m_bindElemCount);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					goto IL_12A;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->NumOpoUdtValCtx = this.m_bindElemCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if (this.m_pOpoPrmValCtx->NumOpoUdtValCtx < this.m_bindElemCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref this.m_pOpoPrmValCtx->pOpoUdtValCtx, this.m_pOpoPrmValCtx->NumOpoUdtValCtx, this.m_bindElemCount);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->NumOpoUdtValCtx = this.m_bindElemCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			IL_12A:
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpsErrCtx = conn.m_opoConCtx.opsErrCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pTDO = this.m_udtDescriptor.m_opsDscCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoDscValCtx = this.m_udtDescriptor.m_pOpoDscValCtx;
			OracleUdt.SetValue(conn, (IntPtr)((void*)(this.m_pOpoPrmValCtx->pOpoUdtValCtx + i)), 0, array);
			try
			{
				num = OpsUdt.SetArrayData(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx + i);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num == 0)
				{
					this.SetPrmValCtx(this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pUDT, i);
					*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)i * (IntPtr)sizeof(void*)) = (void*)this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pNullStruct;
				}
				else if (num != ErrRes.INT_ERR)
				{
					OracleException.HandleError(num, conn, this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpsErrCtx, this);
				}
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0006D5AC File Offset: 0x0006C5AC
		private unsafe void SetUDTFromCustomObject(OracleConnection conn, IOracleCustomType customObj, int i)
		{
			int num = 0;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor2(conn, (OpoDscRefCtx)OracleUdt.GetUdtName(customObj.GetType().FullName, conn.DataSource));
			if (oracleUdtDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			if ((IntPtr)((void*)this.m_pOpoPrmValCtx->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out this.m_pOpoPrmValCtx->pOpoUdtValCtx, this.m_bindElemCount);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					goto IL_148;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->NumOpoUdtValCtx = this.m_bindElemCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if (this.m_pOpoPrmValCtx->NumOpoUdtValCtx < this.m_bindElemCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref this.m_pOpoPrmValCtx->pOpoUdtValCtx, this.m_pOpoPrmValCtx->NumOpoUdtValCtx, this.m_bindElemCount);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->NumOpoUdtValCtx = this.m_bindElemCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			IL_148:
			if ((IntPtr)((void*)this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					goto IL_2BE;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			if (this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].NumOpoUdtValCtx < oracleUdtDescriptor.AttributeCount)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].NumOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num == 0)
					{
						this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
					}
					else if (num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
			IL_2BE:
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpsErrCtx = conn.m_opoConCtx.opsErrCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pTDO = oracleUdtDescriptor.m_opsDscCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			for (int j = 0; j < oracleUdtDescriptor.AttributeCount; j++)
			{
				this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pOpoUdtValCtx[j].bIsNull = 1;
			}
			customObj.FromCustomObject(conn, (IntPtr)((void*)(this.m_pOpoPrmValCtx->pOpoUdtValCtx + i)));
			try
			{
				if (oracleUdtDescriptor.m_pOpoDscValCtx->TypeCode == 122)
				{
					if (this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].bIsNull == 1)
					{
						this.m_pOpoPrmValCtx->pInd[i] = -1;
						this.m_pOpoPrmValCtx->pSrcInd[i] = -1;
						return;
					}
					num = OpsUdt.SetArrayData(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx + i);
				}
				else
				{
					num = OpsUdt.SetData(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx + i);
				}
			}
			catch (Exception ex5)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex5);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
				}
			}
			GC.KeepAlive(oracleUdtDescriptor);
			this.SetPrmValCtx(this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pUDT, i);
			*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)i * (IntPtr)sizeof(void*)) = (void*)this.m_pOpoPrmValCtx->pOpoUdtValCtx[i].pNullStruct;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0006DAF0 File Offset: 0x0006CAF0
		private unsafe void PreBind_OracleObject(OracleConnection conn)
		{
			if (conn.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			this.SetUdtDescriptor(conn);
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							if (!(this.m_value is Array))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = ((Array)this.m_value).GetValue(i);
						}
						if (!(value is IOracleCustomType))
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
						}
						this.SetUDTFromCustomObject(conn, (IOracleCustomType)value, i);
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_NDT, 0, null);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0006DBF4 File Offset: 0x0006CBF4
		private unsafe void PreBind_Collection(OracleConnection conn)
		{
			if (conn.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			this.SetUdtDescriptor(conn);
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							if (!(this.m_value is Array))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = ((Array)this.m_value).GetValue(i);
						}
						if (value is IOracleCustomType)
						{
							this.SetUDTFromCustomObject(conn, (IOracleCustomType)value, i);
						}
						else
						{
							if (!(value is Array))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							this.SetUDTFromArray(conn, value, i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_NDT, 0, null);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0006DD0C File Offset: 0x0006CD0C
		private unsafe void PreBind_OracleRef(OracleConnection conn)
		{
			if (conn.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			this.SetUdtDescriptor(conn);
			if (this.m_direction == ParameterDirection.Input || this.m_direction == ParameterDirection.InputOutput)
			{
				this.m_saveValue = new object[this.m_arrBindCount];
				for (int i = 0; i < this.m_bindElemCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pInd[i] != -1)
					{
						object value;
						if (!this.m_bArrayBind)
						{
							value = this.m_value;
						}
						else
						{
							if (!(this.m_value is Array))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							value = ((Array)this.m_value).GetValue(i);
						}
						if (value is OracleRef)
						{
							if (((OracleRef)value).m_connection != conn)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_DIFFERENT_CONNECTIONS, new string[0]));
							}
							if (((OracleRef)value).m_conSignature != conn.m_conSignature)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
							}
							this.SetPrmValCtx(((OracleRef)value).UdtDescriptor);
							this.SetPrmValCtx(((OracleRef)value).m_opoUdtCtx.m_pOCIRef, i);
						}
						else
						{
							string preBindStr;
							if (!((preBindStr = this.GetPreBindStr(value)) != string.Empty))
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), this.ParameterName);
							}
							this.m_saveValue[i] = new OracleRef(conn, preBindStr);
							this.SetPrmValCtx(((OracleRef)this.m_saveValue[i]).UdtDescriptor);
							this.SetPrmValCtx(((OracleRef)this.m_saveValue[i]).m_opoUdtCtx.m_pOCIRef, i);
						}
					}
				}
			}
			this.SetPrmValCtx(OraType.ORA_OCIRef, 0, null);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0006DEE8 File Offset: 0x0006CEE8
		private unsafe void PostBind_OracleRef(OracleConnection conn)
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1 && !(this.m_value is OracleRef) && !this.IsElemType(typeof(OracleRef), this.m_value, i))
					{
						((OracleRef)this.m_saveValue[i]).Dispose();
						this.m_saveValue[i] = null;
					}
					this.SetPrmValCtx(IntPtr.Zero, i);
				}
				this.m_saveValue = null;
				break;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				if (this.m_bArrayBind)
				{
					Array array = Array.CreateInstance(typeof(object), this.m_arrBindCount);
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							object value = this.CreateOracleRef(conn, i);
							array.SetValue(value, i);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							object value;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								value = OracleRef.Null;
							}
							else
							{
								value = DBNull.Value;
							}
							array.SetValue(value, i);
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
					return;
				}
				if (*this.m_pOpoPrmValCtx->pInd != -1)
				{
					this.m_value = this.CreateOracleRef(conn, 0);
					this.m_status = OracleParameterStatus.Success;
					return;
				}
				this.m_curSize = 0;
				if (PrmEnumType.ORADBTYPE == this.m_enumType)
				{
					this.m_value = OracleRef.Null;
				}
				else
				{
					this.m_value = DBNull.Value;
				}
				this.m_status = OracleParameterStatus.NullFetched;
				return;
			case ParameterDirection.InputOutput:
				if (this.m_bArrayBind)
				{
					Array array2 = Array.CreateInstance(typeof(object), this.m_arrBindCount);
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (this.m_pOpoPrmValCtx->pInd[i] != -1)
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] == -1)
							{
								object value2 = this.CreateOracleRef(conn, i);
								array2.SetValue(value2, i);
							}
							else
							{
								object value3 = ((Array)this.m_value).GetValue(i);
								if (!(value3 is OracleRef))
								{
									array2.SetValue(this.m_saveValue[i], i);
									this.m_saveValue[i] = null;
								}
								else
								{
									array2.SetValue(value3, i);
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							if (this.m_pOpoPrmValCtx->pSrcInd[i] != -1)
							{
								object value3 = ((Array)this.m_value).GetValue(i);
								if (value3 is OracleRef)
								{
									((OracleRef)value3).Dispose();
								}
								else
								{
									((OracleRef)this.m_saveValue[i]).Dispose();
									this.m_saveValue[i] = null;
								}
							}
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								array2.SetValue(OracleRef.Null, i);
							}
							else
							{
								array2.SetValue(DBNull.Value, i);
							}
						}
					}
					this.m_value = array2;
					this.m_saveValue = null;
					return;
				}
				if (*this.m_pOpoPrmValCtx->pInd != -1)
				{
					if (*this.m_pOpoPrmValCtx->pSrcInd == -1)
					{
						this.m_value = this.CreateOracleRef(conn, 0);
					}
					else if (!(this.m_value is OracleRef))
					{
						this.m_value = this.m_saveValue[0];
						this.m_saveValue[0] = null;
						this.m_saveValue = null;
					}
					this.m_status = OracleParameterStatus.Success;
					return;
				}
				if (*this.m_pOpoPrmValCtx->pSrcInd != -1)
				{
					if (this.m_value is OracleRef)
					{
						((OracleRef)this.m_value).Dispose();
						this.m_value = null;
					}
					else
					{
						((OracleRef)this.m_saveValue[0]).Dispose();
						this.m_saveValue[0] = null;
						this.m_saveValue = null;
					}
				}
				this.m_curSize = 0;
				if (PrmEnumType.ORADBTYPE == this.m_enumType)
				{
					this.m_value = OracleRef.Null;
				}
				else
				{
					this.m_value = DBNull.Value;
				}
				this.m_status = OracleParameterStatus.NullFetched;
				return;
			case (ParameterDirection)4:
			case (ParameterDirection)5:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0006E2B8 File Offset: 0x0006D2B8
		private unsafe object CreateCustomObject(OracleConnection conn, int index)
		{
			IntPtr intPtr = (IntPtr)(*(IntPtr*)((byte*)this.m_pOpoPrmValCtx->pTDOSubType + (IntPtr)index * (IntPtr)sizeof(void*)));
			IntPtr pNullStruct = (IntPtr)(*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)index * (IntPtr)sizeof(void*)));
			OracleUdtDescriptor oracleUdtDescriptor = null;
			if (this.m_pOpoPrmValCtx->bIsFinalType == 0)
			{
				bool flag;
				oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(conn, intPtr, false, out flag);
				*(IntPtr*)((byte*)this.m_pOpoPrmValCtx->pTDOSubType + (IntPtr)index * (IntPtr)sizeof(void*)) = (void*)IntPtr.Zero;
				if (!flag)
				{
					goto IL_A7;
				}
				try
				{
					OpsDsc.UnpinTDO(conn.m_opoConCtx.opsConCtx, intPtr);
					goto IL_A7;
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					goto IL_A7;
				}
			}
			oracleUdtDescriptor = this.m_udtDescriptor;
			IL_A7:
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			if (this.m_pOpoPrmValCtx->pOpoUdtValCtx == null)
			{
				OpsUdt.AllocValCtx(out this.m_pOpoPrmValCtx->pOpoUdtValCtx, this.m_bindElemCount);
				this.m_pOpoPrmValCtx->NumOpoUdtValCtx = this.m_bindElemCount;
			}
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pNullStruct = pNullStruct;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pOpsErrCtx = conn.m_opoConCtx.opsErrCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pTDO = oracleUdtDescriptor.m_opsDscCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
			this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].ppRefTDO = this.m_pOpoPrmValCtx->ppRefTDO;
			if (oracleUdtDescriptor.m_pOpoDscValCtx->TypeCode == 122)
			{
				this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pUDT = (IntPtr)((void*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * (IntPtr)sizeof(void*)));
				OpsUdt.GetArr(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx + index);
			}
			else
			{
				this.m_pOpoPrmValCtx->pOpoUdtValCtx[index].pUDT = (IntPtr)(*(IntPtr*)((byte*)this.m_pOpoPrmValCtx->pBltVal + (IntPtr)index * (IntPtr)sizeof(void*)));
				OpsUdt.GetObj(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx->pOpoUdtValCtx + index);
			}
			object obj;
			if (oracleUdtDescriptor.m_pOpoDscValCtx->bIsArrayType == 0)
			{
				obj = ((IOracleCustomTypeFactory)oracleUdtDescriptor.m_customTypeFactory).CreateObject();
				((IOracleCustomType)obj).ToCustomObject(conn, (IntPtr)((void*)(this.m_pOpoPrmValCtx->pOpoUdtValCtx + index)));
			}
			else
			{
				OracleUdtStatus oracleUdtStatus;
				object obj2;
				obj = OracleUdt.GetArrData(conn, (IntPtr)((void*)(this.m_pOpoPrmValCtx->pOpoUdtValCtx + index)), out oracleUdtStatus, out obj2);
			}
			GC.KeepAlive(oracleUdtDescriptor);
			return obj;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0006E5B4 File Offset: 0x0006D5B4
		private unsafe void PostBind_Collection(OracleConnection conn)
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					this.SetPrmValCtx(IntPtr.Zero, i);
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*(*(IntPtr*)((void*)this.m_pOpoPrmValCtx->ppTempInd)) != -1)
					{
						this.m_value = this.CreateCustomObject(conn, 0);
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						this.m_value = DBNull.Value;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							if (this.m_udtDescriptor.m_customTypeFactory == null)
							{
								object factory = OracleUdt.GetFactory(this.m_udtDescriptor);
								if (factory != null)
								{
									this.m_udtDescriptor.DescribeCustomType(factory);
								}
							}
							if (this.m_udtDescriptor.m_customTypeFactory is IOracleCustomTypeFactory)
							{
								IOracleCustomTypeFactory oracleCustomTypeFactory = (IOracleCustomTypeFactory)this.m_udtDescriptor.m_customTypeFactory;
								if (oracleCustomTypeFactory != null)
								{
									IOracleCustomType oracleCustomType = oracleCustomTypeFactory.CreateObject();
									Type type = oracleCustomType.GetType();
									PropertyInfo property = type.GetProperty("Null");
									this.m_value = property.GetValue(null, null);
								}
							}
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					Array array = Array.CreateInstance(typeof(object), this.m_arrBindCount);
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (*(*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)i * (IntPtr)sizeof(void*))) != -1)
						{
							object value = this.CreateCustomObject(conn, i);
							array.SetValue(value, i);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							array.SetValue(null, i);
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								if (this.m_udtDescriptor.m_customTypeFactory == null)
								{
									object factory2 = OracleUdt.GetFactory(this.m_udtDescriptor);
									if (factory2 != null)
									{
										this.m_udtDescriptor.DescribeCustomType(factory2);
									}
								}
								if (this.m_udtDescriptor.m_customTypeFactory is IOracleCustomTypeFactory)
								{
									IOracleCustomTypeFactory oracleCustomTypeFactory2 = (IOracleCustomTypeFactory)this.m_udtDescriptor.m_customTypeFactory;
									if (oracleCustomTypeFactory2 != null)
									{
										IOracleCustomType oracleCustomType2 = oracleCustomTypeFactory2.CreateObject();
										Type type2 = oracleCustomType2.GetType();
										PropertyInfo property2 = type2.GetProperty("Null");
										array.SetValue(property2.GetValue(null, null), i);
									}
								}
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_pOpoPrmValCtx->pOpoUdtValCtx != null)
			{
				int num = 0;
				try
				{
					num = OpsPrm.FreeUdtObjects(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0006E89C File Offset: 0x0006D89C
		private unsafe void PostBind_OracleObject(OracleConnection conn)
		{
			switch (this.m_direction)
			{
			case ParameterDirection.Input:
				for (int i = 0; i < this.m_arrBindCount; i++)
				{
					this.SetPrmValCtx(IntPtr.Zero, i);
				}
				break;
			case ParameterDirection.Output:
			case ParameterDirection.InputOutput:
			case ParameterDirection.ReturnValue:
				if (!this.m_bArrayBind)
				{
					if (*(*(IntPtr*)((void*)this.m_pOpoPrmValCtx->ppTempInd)) != -1)
					{
						this.m_value = this.CreateCustomObject(conn, 0);
						this.m_status = OracleParameterStatus.Success;
					}
					else
					{
						this.m_curSize = 0;
						if (PrmEnumType.ORADBTYPE == this.m_enumType)
						{
							if (this.m_udtDescriptor.m_customTypeFactory == null)
							{
								object factory = OracleUdt.GetFactory(this.m_udtDescriptor);
								if (factory != null)
								{
									this.m_udtDescriptor.DescribeCustomType(factory);
								}
							}
							IOracleCustomTypeFactory oracleCustomTypeFactory = (IOracleCustomTypeFactory)this.m_udtDescriptor.m_customTypeFactory;
							IOracleCustomType oracleCustomType = oracleCustomTypeFactory.CreateObject();
							Type type = oracleCustomType.GetType();
							PropertyInfo property = type.GetProperty("Null");
							this.m_value = property.GetValue(null, null);
						}
						else
						{
							this.m_value = DBNull.Value;
						}
						this.m_status = OracleParameterStatus.NullFetched;
					}
				}
				else
				{
					Array array = Array.CreateInstance(typeof(object), this.m_arrBindCount);
					for (int i = 0; i < this.m_arrBindCount; i++)
					{
						if (*(*(IntPtr*)((byte*)((void*)this.m_pOpoPrmValCtx->ppTempInd) + (IntPtr)i * (IntPtr)sizeof(void*))) != -1)
						{
							object value = this.CreateCustomObject(conn, i);
							array.SetValue(value, i);
							this.m_arrayBindStatus[i] = OracleParameterStatus.Success;
						}
						else
						{
							this.m_curSize = 0;
							this.m_curArrayBindSize[i] = 0;
							if (PrmEnumType.ORADBTYPE == this.m_enumType)
							{
								if (this.m_udtDescriptor.m_customTypeFactory == null)
								{
									object factory2 = OracleUdt.GetFactory(this.m_udtDescriptor);
									if (factory2 != null)
									{
										this.m_udtDescriptor.DescribeCustomType(factory2);
									}
								}
								IOracleCustomTypeFactory oracleCustomTypeFactory2 = (IOracleCustomTypeFactory)this.m_udtDescriptor.m_customTypeFactory;
								IOracleCustomType oracleCustomType2 = oracleCustomTypeFactory2.CreateObject();
								Type type2 = oracleCustomType2.GetType();
								PropertyInfo property2 = type2.GetProperty("Null");
								object value = property2.GetValue(null, null);
								array.SetValue(value, i);
							}
							else
							{
								array.SetValue(null, i);
							}
							this.m_arrayBindStatus[i] = OracleParameterStatus.NullFetched;
						}
					}
					this.m_value = array;
				}
				break;
			}
			if (this.m_pOpoPrmValCtx->pOpoUdtValCtx != null)
			{
				int num = 0;
				try
				{
					num = OpsPrm.FreeUdtObjects(conn.m_opoConCtx.opsConCtx, this.m_pOpoPrmValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0006EB5C File Offset: 0x0006DB5C
		private void ResetUDTInd()
		{
			try
			{
				OpsPrm.ResetValCtx(this.m_pOpoPrmValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
		}

		// Token: 0x040008AF RID: 2223
		private const int MaxOraDbType = 133;

		// Token: 0x040008B0 RID: 2224
		private const int MinOraDbType = 101;

		// Token: 0x040008B1 RID: 2225
		private const int DataThresholdSizeForCLOB = 4000;

		// Token: 0x040008B2 RID: 2226
		private const int DataThresholdSizeForBLOB = 4000;

		// Token: 0x040008B3 RID: 2227
		internal const byte MaxScale = 127;

		// Token: 0x040008B4 RID: 2228
		internal const sbyte MinScale = -84;

		// Token: 0x040008B5 RID: 2229
		internal const byte InvalidPrecision = 100;

		// Token: 0x040008B6 RID: 2230
		internal const byte InvalidScale = 129;

		// Token: 0x040008B7 RID: 2231
		internal const int InvalidSize = -1;

		// Token: 0x040008B8 RID: 2232
		internal unsafe OpoPrmValCtx* m_pOpoPrmValCtx;

		// Token: 0x040008B9 RID: 2233
		internal string m_paramName;

		// Token: 0x040008BA RID: 2234
		private string m_sourceColumn;

		// Token: 0x040008BB RID: 2235
		private DataRowVersion m_sourceVersion;

		// Token: 0x040008BC RID: 2236
		private DbType m_dbType;

		// Token: 0x040008BD RID: 2237
		internal OracleDbType m_oraDbType;

		// Token: 0x040008BE RID: 2238
		internal bool m_bOracleDbTypeExSet;

		// Token: 0x040008BF RID: 2239
		private int m_maxSize;

		// Token: 0x040008C0 RID: 2240
		private int[] m_maxArrayBindSize;

		// Token: 0x040008C1 RID: 2241
		private bool m_nullable;

		// Token: 0x040008C2 RID: 2242
		private object m_value;

		// Token: 0x040008C3 RID: 2243
		internal ParameterDirection m_direction;

		// Token: 0x040008C4 RID: 2244
		private OracleParameterStatus m_status;

		// Token: 0x040008C5 RID: 2245
		private OracleParameterStatus[] m_arrayBindStatus;

		// Token: 0x040008C6 RID: 2246
		internal PrmEnumType m_enumType;

		// Token: 0x040008C7 RID: 2247
		private int m_offset;

		// Token: 0x040008C8 RID: 2248
		private byte m_precision;

		// Token: 0x040008C9 RID: 2249
		private byte m_scale;

		// Token: 0x040008CA RID: 2250
		internal object[] m_saveValue;

		// Token: 0x040008CB RID: 2251
		private int m_curSize;

		// Token: 0x040008CC RID: 2252
		private int[] m_curArrayBindSize;

		// Token: 0x040008CD RID: 2253
		private int m_arrBindCount;

		// Token: 0x040008CE RID: 2254
		private bool m_bArrayBind;

		// Token: 0x040008CF RID: 2255
		private OracleCollectionType m_collType;

		// Token: 0x040008D0 RID: 2256
		internal bool m_disposed;

		// Token: 0x040008D1 RID: 2257
		internal bool m_modified;

		// Token: 0x040008D2 RID: 2258
		internal OracleParameterCollection m_collRef;

		// Token: 0x040008D3 RID: 2259
		private int m_bindElemCount;

		// Token: 0x040008D4 RID: 2260
		private IntPtr m_pDataBuffer;

		// Token: 0x040008D5 RID: 2261
		private bool m_bSetDbType;

		// Token: 0x040008D6 RID: 2262
		private bool m_redirected;

		// Token: 0x040008D7 RID: 2263
		private bool m_sourceColumnNullMapping;

		// Token: 0x040008D8 RID: 2264
		private string m_udtTypeName;

		// Token: 0x040008D9 RID: 2265
		private bool m_modifedAfterBind;

		// Token: 0x040008DA RID: 2266
		private OracleUdtDescriptor m_udtDescriptor;

		// Token: 0x040008DB RID: 2267
		internal string m_commandText = string.Empty;

		// Token: 0x040008DC RID: 2268
		internal string m_paramPosOrName = string.Empty;

		// Token: 0x040008DD RID: 2269
		internal bool m_bReturnDateTimeOffset;
	}
}
