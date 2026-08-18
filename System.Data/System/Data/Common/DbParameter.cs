using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x0200013B RID: 315
	public abstract class DbParameter : MarshalByRefObject, IDbDataParameter, IDataParameter
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001493 RID: 5267
		// (set) Token: 0x06001494 RID: 5268
		[Browsable(false)]
		[ResDescription("DbParameter_DbType")]
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.All)]
		public abstract DbType DbType { get; set; }

		// Token: 0x06001495 RID: 5269
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public abstract void ResetDbType();

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001496 RID: 5270
		// (set) Token: 0x06001497 RID: 5271
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(ParameterDirection.Input)]
		[ResDescription("DbParameter_Direction")]
		public abstract ParameterDirection Direction { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001498 RID: 5272
		// (set) Token: 0x06001499 RID: 5273
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public abstract bool IsNullable { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600149A RID: 5274
		// (set) Token: 0x0600149B RID: 5275
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[ResDescription("DbParameter_ParameterName")]
		public abstract string ParameterName { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00241138 File Offset: 0x00240538
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00241148 File Offset: 0x00240548
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00241158 File Offset: 0x00240558
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00241168 File Offset: 0x00240568
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

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060014A0 RID: 5280
		// (set) Token: 0x060014A1 RID: 5281
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Size")]
		public abstract int Size { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060014A2 RID: 5282
		// (set) Token: 0x060014A3 RID: 5283
		[ResDescription("DbParameter_SourceColumn")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Update")]
		public abstract string SourceColumn { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060014A4 RID: 5284
		// (set) Token: 0x060014A5 RID: 5285
		[ResCategory("DataCategory_Update")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbParameter_SourceColumnNullMapping")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(false)]
		public abstract bool SourceColumnNullMapping { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060014A6 RID: 5286
		// (set) Token: 0x060014A7 RID: 5287
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbParameter_SourceVersion")]
		[DefaultValue(DataRowVersion.Current)]
		public abstract DataRowVersion SourceVersion { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060014A8 RID: 5288
		// (set) Token: 0x060014A9 RID: 5289
		[DefaultValue(null)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Value")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract object Value { get; set; }
	}
}
