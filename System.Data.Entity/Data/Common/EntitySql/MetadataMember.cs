using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200034F RID: 847
	internal abstract class MetadataMember : ExpressionResolution
	{
		// Token: 0x0600318B RID: 12683 RVA: 0x000C2CEB File Offset: 0x000C0EEB
		protected MetadataMember(MetadataMemberClass @class, string name) : base(ExpressionResolutionClass.MetadataMember)
		{
			this.MetadataMemberClass = @class;
			this.Name = name;
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000C2D02 File Offset: 0x000C0F02
		internal override string ExpressionClassName
		{
			get
			{
				return MetadataMember.MetadataMemberExpressionClassName;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000C2D09 File Offset: 0x000C0F09
		internal static string MetadataMemberExpressionClassName
		{
			get
			{
				return Strings.LocalizedMetadataMemberExpression;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600318E RID: 12686
		internal abstract string MetadataMemberClassName { get; }

		// Token: 0x0600318F RID: 12687 RVA: 0x000C2D10 File Offset: 0x000C0F10
		internal static IEqualityComparer<MetadataMember> CreateMetadataMemberNameEqualityComparer(StringComparer stringComparer)
		{
			return new MetadataMember.MetadataMemberNameEqualityComparer(stringComparer);
		}

		// Token: 0x0400158E RID: 5518
		internal readonly MetadataMemberClass MetadataMemberClass;

		// Token: 0x0400158F RID: 5519
		internal readonly string Name;

		// Token: 0x02000665 RID: 1637
		private sealed class MetadataMemberNameEqualityComparer : IEqualityComparer<MetadataMember>
		{
			// Token: 0x06004452 RID: 17490 RVA: 0x000F7792 File Offset: 0x000F5992
			internal MetadataMemberNameEqualityComparer(StringComparer stringComparer)
			{
				this._stringComparer = stringComparer;
			}

			// Token: 0x06004453 RID: 17491 RVA: 0x000F77A1 File Offset: 0x000F59A1
			bool IEqualityComparer<MetadataMember>.Equals(MetadataMember x, MetadataMember y)
			{
				return this._stringComparer.Equals(x.Name, y.Name);
			}

			// Token: 0x06004454 RID: 17492 RVA: 0x000F77BA File Offset: 0x000F59BA
			int IEqualityComparer<MetadataMember>.GetHashCode(MetadataMember obj)
			{
				return this._stringComparer.GetHashCode(obj.Name);
			}

			// Token: 0x04001F58 RID: 8024
			private readonly StringComparer _stringComparer;
		}
	}
}
