using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003E RID: 62
	internal class SmiExtendedMetaData : SmiMetaData
	{
		// Token: 0x06000235 RID: 565 RVA: 0x001DE598 File Offset: 0x001DD998
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x001DE5C8 File Offset: 0x001DD9C8
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x001DE5F8 File Offset: 0x001DD9F8
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x001DE628 File Offset: 0x001DDA28
		internal SmiExtendedMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties)
		{
			this._name = name;
			this._typeSpecificNamePart1 = typeSpecificNamePart1;
			this._typeSpecificNamePart2 = typeSpecificNamePart2;
			this._typeSpecificNamePart3 = typeSpecificNamePart3;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000239 RID: 569 RVA: 0x001DE678 File Offset: 0x001DDA78
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600023A RID: 570 RVA: 0x001DE698 File Offset: 0x001DDA98
		internal string TypeSpecificNamePart1
		{
			get
			{
				return this._typeSpecificNamePart1;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600023B RID: 571 RVA: 0x001DE6B8 File Offset: 0x001DDAB8
		internal string TypeSpecificNamePart2
		{
			get
			{
				return this._typeSpecificNamePart2;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600023C RID: 572 RVA: 0x001DE6D8 File Offset: 0x001DDAD8
		internal string TypeSpecificNamePart3
		{
			get
			{
				return this._typeSpecificNamePart3;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x001DE6F8 File Offset: 0x001DDAF8
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

		// Token: 0x040005DC RID: 1500
		private string _name;

		// Token: 0x040005DD RID: 1501
		private string _typeSpecificNamePart1;

		// Token: 0x040005DE RID: 1502
		private string _typeSpecificNamePart2;

		// Token: 0x040005DF RID: 1503
		private string _typeSpecificNamePart3;
	}
}
