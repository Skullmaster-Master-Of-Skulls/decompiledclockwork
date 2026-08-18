using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E6 RID: 1510
	internal class DiscriminatedCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06003C04 RID: 15364 RVA: 0x00118A0A File Offset: 0x00116C0A
		internal DiscriminatedCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys, SimpleColumnMap discriminator, object discriminatorValue) : base(type, name, elementMap, keys, foreignKeys)
		{
			this.m_discriminator = discriminator;
			this.m_discriminatorValue = discriminatorValue;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x00118A29 File Offset: 0x00116C29
		internal SimpleColumnMap Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x00118A31 File Offset: 0x00116C31
		internal object DiscriminatorValue
		{
			get
			{
				return this.m_discriminatorValue;
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x00118A39 File Offset: 0x00116C39
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x00118A43 File Offset: 0x00116C43
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x00118A50 File Offset: 0x00116C50
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "M{{{0}}}", new object[]
			{
				base.Element
			});
		}

		// Token: 0x04001680 RID: 5760
		private readonly SimpleColumnMap m_discriminator;

		// Token: 0x04001681 RID: 5761
		private readonly object m_discriminatorValue;
	}
}
