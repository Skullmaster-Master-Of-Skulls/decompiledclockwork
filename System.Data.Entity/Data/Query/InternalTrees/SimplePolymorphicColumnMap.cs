using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009F RID: 159
	internal class SimplePolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00035F35 File Offset: 0x00034135
		internal SimplePolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap typeDiscriminator, Dictionary<object, TypedColumnMap> typeChoices) : base(type, name, baseTypeColumns)
		{
			this.m_typedColumnMap = typeChoices;
			this.m_typeDiscriminator = typeDiscriminator;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00035F50 File Offset: 0x00034150
		internal SimpleColumnMap TypeDiscriminator
		{
			get
			{
				return this.m_typeDiscriminator;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00035F58 File Offset: 0x00034158
		internal Dictionary<object, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typedColumnMap;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00035F60 File Offset: 0x00034160
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00035F6A File Offset: 0x0003416A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00035F74 File Offset: 0x00034174
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

		// Token: 0x040008B7 RID: 2231
		private SimpleColumnMap m_typeDiscriminator;

		// Token: 0x040008B8 RID: 2232
		private Dictionary<object, TypedColumnMap> m_typedColumnMap;
	}
}
