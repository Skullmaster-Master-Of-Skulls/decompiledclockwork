using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000259 RID: 601
	internal abstract class MetadataMember : ExpressionResolution
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x00062F66 File Offset: 0x00061166
		protected MetadataMember(MetadataMemberClass @class, string name) : base(ExpressionResolutionClass.MetadataMember)
		{
			this.MetadataMemberClass = @class;
			this.Name = name;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00062F7D File Offset: 0x0006117D
		internal override string ExpressionClassName
		{
			get
			{
				return MetadataMember.MetadataMemberExpressionClassName;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00062F84 File Offset: 0x00061184
		internal static string MetadataMemberExpressionClassName
		{
			get
			{
				return Strings.LocalizedMetadataMemberExpression;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060014E1 RID: 5345
		internal abstract string MetadataMemberClassName { get; }

		// Token: 0x060014E2 RID: 5346 RVA: 0x00062F8B File Offset: 0x0006118B
		internal static IEqualityComparer<MetadataMember> CreateMetadataMemberNameEqualityComparer(StringComparer stringComparer)
		{
			return new MetadataMember.MetadataMemberNameEqualityComparer(stringComparer);
		}

		// Token: 0x04000734 RID: 1844
		internal readonly MetadataMemberClass MetadataMemberClass;

		// Token: 0x04000735 RID: 1845
		internal readonly string Name;

		// Token: 0x0200025A RID: 602
		private sealed class MetadataMemberNameEqualityComparer : IEqualityComparer<MetadataMember>
		{
			// Token: 0x060014E3 RID: 5347 RVA: 0x00062F93 File Offset: 0x00061193
			internal MetadataMemberNameEqualityComparer(StringComparer stringComparer)
			{
				this._stringComparer = stringComparer;
			}

			// Token: 0x060014E4 RID: 5348 RVA: 0x00062FA2 File Offset: 0x000611A2
			bool IEqualityComparer<MetadataMember>.Equals(MetadataMember x, MetadataMember y)
			{
				return this._stringComparer.Equals(x.Name, y.Name);
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x00062FBB File Offset: 0x000611BB
			int IEqualityComparer<MetadataMember>.GetHashCode(MetadataMember obj)
			{
				return this._stringComparer.GetHashCode(obj.Name);
			}

			// Token: 0x04000736 RID: 1846
			private readonly StringComparer _stringComparer;
		}
	}
}
