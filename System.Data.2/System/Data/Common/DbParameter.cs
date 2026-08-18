using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x020002F4 RID: 756
	public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06003031 RID: 12337
		// (set) Token: 0x06003032 RID: 12338
		[ResDescription("DbParameter_DbType")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		public abstract DbType DbType { get; set; }

		// Token: 0x06003033 RID: 12339
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003034 RID: 12340
		// (set) Token: 0x06003035 RID: 12341
		[ResCategory("DataCategory_Data")]
		[DefaultValue(ParameterDirection.Input)]
		[ResDescription("DbParameter_Direction")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract ParameterDirection Direction { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003036 RID: 12342
		// (set) Token: 0x06003037 RID: 12343
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool IsNullable { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06003038 RID: 12344
		// (set) Token: 0x06003039 RID: 12345
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_ParameterName")]
		[DefaultValue("")]
		public abstract string ParameterName { get; set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x0012E2FC File Offset: 0x0012D6FC
		// (set) Token: 0x0600303B RID: 12347 RVA: 0x0012E30C File Offset: 0x0012D70C
		byte IDbDataParameter.Precision
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x0012E31C File Offset: 0x0012D71C
		// (set) Token: 0x0600303D RID: 12349 RVA: 0x0012E32C File Offset: 0x0012D72C
		byte IDbDataParameter.Scale
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0012E33C File Offset: 0x0012D73C
		// (set) Token: 0x0600303F RID: 12351 RVA: 0x0012E350 File Offset: 0x0012D750
		public virtual byte Precision
		{
			get
			{
				return ((IDbDataParameter)this).Precision;
			}
			set
			{
				((IDbDataParameter)this).Precision = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0012E364 File Offset: 0x0012D764
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x0012E378 File Offset: 0x0012D778
		public virtual byte Scale
		{
			get
			{
				return ((IDbDataParameter)this).Scale;
			}
			set
			{
				((IDbDataParameter)this).Scale = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003042 RID: 12354
		// (set) Token: 0x06003043 RID: 12355
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Size")]
		public abstract int Size { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003044 RID: 12356
		// (set) Token: 0x06003045 RID: 12357
		[ResDescription("DbParameter_SourceColumn")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Update")]
		public abstract string SourceColumn { get; set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003046 RID: 12358
		// (set) Token: 0x06003047 RID: 12359
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbParameter_SourceColumnNullMapping")]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[ResCategory("DataCategory_Update")]
		public abstract bool SourceColumnNullMapping { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x0012E38C File Offset: 0x0012D78C
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x0012E3A0 File Offset: 0x0012D7A0
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbParameter_SourceVersion")]
		[DefaultValue(DataRowVersion.Current)]
		public virtual DataRowVersion SourceVersion
		{
			get
			{
				return DataRowVersion.Default;
			}
			set
			{
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600304A RID: 12362
		// (set) Token: 0x0600304B RID: 12363
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Value")]
		[DefaultValue(null)]
		public abstract object Value { get; set; }
	}
}
