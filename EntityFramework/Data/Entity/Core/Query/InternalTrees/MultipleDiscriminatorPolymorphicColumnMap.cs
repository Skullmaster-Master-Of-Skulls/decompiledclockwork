using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000602 RID: 1538
	internal class MultipleDiscriminatorPolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06003CAB RID: 15531 RVA: 0x001194F1 File Offset: 0x001176F1
		internal MultipleDiscriminatorPolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap[] typeDiscriminators, Dictionary<EntityType, TypedColumnMap> typeChoices, Func<object[], EntityType> discriminate) : base(type, name, baseTypeColumns)
		{
			this.m_typeDiscriminators = typeDiscriminators;
			this.m_typeChoices = typeChoices;
			this.m_discriminate = discriminate;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00119514 File Offset: 0x00117714
		internal SimpleColumnMap[] TypeDiscriminators
		{
			get
			{
				return this.m_typeDiscriminators;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x0011951C File Offset: 0x0011771C
		internal Dictionary<EntityType, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typeChoices;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x00119524 File Offset: 0x00117724
		internal Func<object[], EntityType> Discriminate
		{
			get
			{
				return this.m_discriminate;
			}
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x0011952C File Offset: 0x0011772C
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x00119536 File Offset: 0x00117736
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x00119540 File Offset: 0x00117740
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "P{{TypeId=<{0}>, ", new object[]
			{
				StringUtil.ToCommaSeparatedString(this.TypeDiscriminators)
			});
			foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in this.TypeChoices)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}(<{1}>,{2})", new object[]
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

		// Token: 0x040016B4 RID: 5812
		private readonly SimpleColumnMap[] m_typeDiscriminators;

		// Token: 0x040016B5 RID: 5813
		private readonly Dictionary<EntityType, TypedColumnMap> m_typeChoices;

		// Token: 0x040016B6 RID: 5814
		private readonly Func<object[], EntityType> m_discriminate;
	}
}
