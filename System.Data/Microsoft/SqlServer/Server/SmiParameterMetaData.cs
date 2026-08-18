using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003F RID: 63
	internal sealed class SmiParameterMetaData : SmiExtendedMetaData
	{
		// Token: 0x0600023E RID: 574 RVA: 0x001DE798 File Offset: 0x001DDB98
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x001DE7C8 File Offset: 0x001DDBC8
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x001DE7F8 File Offset: 0x001DDBF8
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, direction)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x001DE828 File Offset: 0x001DDC28
		internal SmiParameterMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, ParameterDirection direction) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._direction = direction;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000242 RID: 578 RVA: 0x001DE868 File Offset: 0x001DDC68
		internal ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x001DE888 File Offset: 0x001DDC88
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}            Direction={2:g}\n\t", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				this.Direction
			});
		}

		// Token: 0x040005E0 RID: 1504
		private ParameterDirection _direction;
	}
}
