using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A0 RID: 160
	internal class MultipleDiscriminatorPolymorphicColumnMap : TypedColumnMap
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00036034 File Offset: 0x00034234
		internal MultipleDiscriminatorPolymorphicColumnMap(TypeUsage type, string name, ColumnMap[] baseTypeColumns, SimpleColumnMap[] typeDiscriminators, Dictionary<EntityType, TypedColumnMap> typeChoices, Func<object[], EntityType> discriminate) : base(type, name, baseTypeColumns)
		{
			this.m_typeDiscriminators = typeDiscriminators;
			this.m_typeChoices = typeChoices;
			this.m_discriminate = discriminate;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00036057 File Offset: 0x00034257
		internal SimpleColumnMap[] TypeDiscriminators
		{
			get
			{
				return this.m_typeDiscriminators;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0003605F File Offset: 0x0003425F
		internal Dictionary<EntityType, TypedColumnMap> TypeChoices
		{
			get
			{
				return this.m_typeChoices;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00036067 File Offset: 0x00034267
		internal Func<object[], EntityType> Discriminate
		{
			get
			{
				return this.m_discriminate;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0003606F File Offset: 0x0003426F
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00036079 File Offset: 0x00034279
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00036084 File Offset: 0x00034284
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

		// Token: 0x040008B9 RID: 2233
		private readonly SimpleColumnMap[] m_typeDiscriminators;

		// Token: 0x040008BA RID: 2234
		private readonly Dictionary<EntityType, TypedColumnMap> m_typeChoices;

		// Token: 0x040008BB RID: 2235
		private readonly Func<object[], EntityType> m_discriminate;
	}
}
