using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000046 RID: 70
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0003B81C File Offset: 0x0003AC1C
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0003B854 File Offset: 0x0003AC54
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0003B890 File Offset: 0x0003AC90
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0003B8D0 File Offset: 0x0003ACD0
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0003B948 File Offset: 0x0003AD48
		internal bool AllowsDBNull
		{
			get
			{
				return this._allowsDBNull;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0003B95C File Offset: 0x0003AD5C
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0003B970 File Offset: 0x0003AD70
		internal string CatalogName
		{
			get
			{
				return this._catalogName;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0003B984 File Offset: 0x0003AD84
		internal string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0003B998 File Offset: 0x0003AD98
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0003B9AC File Offset: 0x0003ADAC
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0003B9C0 File Offset: 0x0003ADC0
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0003B9D4 File Offset: 0x0003ADD4
		internal bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0003B9E8 File Offset: 0x0003ADE8
		internal bool IsColumnSet
		{
			get
			{
				return this._isColumnSet;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0003B9FC File Offset: 0x0003ADFC
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}         AllowsDBNull={2}\n\t{1}           ServerName='{3}'\n\t{1}          CatalogName='{4}'\n\t{1}           SchemaName='{5}'\n\t{1}            TableName='{6}'\n\t{1}           ColumnName='{7}'\n\t{1}                IsKey={8}\n\t{1}           IsIdentity={9}\n\t", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				this.AllowsDBNull,
				(this.ServerName != null) ? this.ServerName : "<null>",
				(this.CatalogName != null) ? this.CatalogName : "<null>",
				(this.SchemaName != null) ? this.SchemaName : "<null>",
				(this.TableName != null) ? this.TableName : "<null>",
				(this.ColumnName != null) ? this.ColumnName : "<null>",
				this.IsKey,
				this.IsIdentity
			});
		}

		// Token: 0x04000153 RID: 339
		private bool _allowsDBNull;

		// Token: 0x04000154 RID: 340
		private string _serverName;

		// Token: 0x04000155 RID: 341
		private string _catalogName;

		// Token: 0x04000156 RID: 342
		private string _schemaName;

		// Token: 0x04000157 RID: 343
		private string _tableName;

		// Token: 0x04000158 RID: 344
		private string _columnName;

		// Token: 0x04000159 RID: 345
		private SqlBoolean _isKey;

		// Token: 0x0400015A RID: 346
		private bool _isIdentity;

		// Token: 0x0400015B RID: 347
		private bool _isColumnSet;
	}
}
