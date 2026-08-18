using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000044 RID: 68
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0003B550 File Offset: 0x0003A950
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0003B578 File Offset: 0x0003A978
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0003B5A4 File Offset: 0x0003A9A4
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0003B5D4 File Offset: 0x0003A9D4
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0003B61C File Offset: 0x0003AA1C
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0003B630 File Offset: 0x0003AA30
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0003B644 File Offset: 0x0003AA44
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0003B658 File Offset: 0x0003AA58
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0003B66C File Offset: 0x0003AA6C
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{2}                 Name={0}{1}{2}TypeSpecificNamePart1='{3}'\n\t{2}TypeSpecificNamePart2='{4}'\n\t{2}TypeSpecificNamePart3='{5}'\n\t", new object[]
			{
				(this._name != null) ? this._name : "<null>",
				base.TraceString(indent),
				new string(' ', indent),
				(this.TypeSpecificNamePart1 != null) ? this.TypeSpecificNamePart1 : "<null>",
				(this.TypeSpecificNamePart2 != null) ? this.TypeSpecificNamePart2 : "<null>",
				(this.TypeSpecificNamePart3 != null) ? this.TypeSpecificNamePart3 : "<null>"
			});
		}

		// Token: 0x0400014E RID: 334
		private string _name;

		// Token: 0x0400014F RID: 335
		private string _typeSpecificNamePart1;

		// Token: 0x04000150 RID: 336
		private string _typeSpecificNamePart2;

		// Token: 0x04000151 RID: 337
		private string _typeSpecificNamePart3;
	}
}
