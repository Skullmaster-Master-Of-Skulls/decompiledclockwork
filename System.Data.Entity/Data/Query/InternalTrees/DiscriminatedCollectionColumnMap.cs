using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A6 RID: 166
	internal class DiscriminatedCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0003629E File Offset: 0x0003449E
		internal DiscriminatedCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys, SimpleColumnMap discriminator, object discriminatorValue) : base(type, name, elementMap, keys, foreignKeys)
		{
			this.m_discriminator = discriminator;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000362BD File Offset: 0x000344BD
		internal SimpleColumnMap Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000362C5 File Offset: 0x000344C5
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000362CD File Offset: 0x000344CD
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000362D7 File Offset: 0x000344D7
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000362E4 File Offset: 0x000344E4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "M{{{0}}}", new object[]
			{
				base.Element.ToString()
			});
		}

		// Token: 0x040008C2 RID: 2242
		private SimpleColumnMap m_discriminator;

		// Token: 0x040008C3 RID: 2243
		private object m_discriminatorValue;
	}
}
