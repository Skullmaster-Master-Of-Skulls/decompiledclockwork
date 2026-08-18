using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000045 RID: 69
	internal sealed class SmiParameterMetaData : SmiExtendedMetaData
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0003B704 File Offset: 0x0003AB04
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0003B72C File Offset: 0x0003AB2C
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0003B758 File Offset: 0x0003AB58
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0003B788 File Offset: 0x0003AB88
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._direction = direction;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0003B7C0 File Offset: 0x0003ABC0
		internal ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0003B7D4 File Offset: 0x0003ABD4
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}            Direction={2:g}\n\t", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				this.Direction
			});
		}

		// Token: 0x04000152 RID: 338
		private ParameterDirection _direction;
	}
}
