using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000625 RID: 1573
	internal class SimplePolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x0011B181 File Offset: 0x00119381
		internal SimplePolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap typeDiscriminator, Dictionary<object, TypedColumnMap> typeChoices) : base(type, name, baseTypeColumns)
		{
			this.m_typedColumnMap = typeChoices;
			this.m_typeDiscriminator = typeDiscriminator;
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x0011B19C File Offset: 0x0011939C
		internal SimpleColumnMap TypeDiscriminator
		{
			get
			{
				return this.m_typeDiscriminator;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x0011B1A4 File Offset: 0x001193A4
		internal Dictionary<object, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typedColumnMap;
			}
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x0011B1AC File Offset: 0x001193AC
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0011B1B6 File Offset: 0x001193B6
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0011B1C0 File Offset: 0x001193C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "P{{TypeId={0}, ", new object[]
			{
				this.TypeDiscriminator
			});
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in this.TypeChoices)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}({1},{2})", new object[]
				{
					text,
					keyValuePair.Key,
					keyValuePair.Value
				});
				text = ",";
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400172F RID: 5935
		private readonly SimpleColumnMap m_typeDiscriminator;

		// Token: 0x04001730 RID: 5936
		private readonly Dictionary<object, TypedColumnMap> m_typedColumnMap;
	}
}
