using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B2 RID: 178
	internal sealed class IndexExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public IndexExpressionFingerprint(ExpressionType nodeType, Type type, PropertyInfo indexer) : base(nodeType, type)
		{
			this.Indexer = indexer;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000DC51 File Offset: 0x0000BE51
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000DC59 File Offset: 0x0000BE59
		public PropertyInfo Indexer { get; private set; }

		// Token: 0x060004DA RID: 1242 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public override bool Equals(object obj)
		{
			IndexExpressionFingerprint indexExpressionFingerprint = obj as IndexExpressionFingerprint;
			return indexExpressionFingerprint != null && object.Equals(this.Indexer, indexExpressionFingerprint.Indexer) && base.Equals(indexExpressionFingerprint);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000DC97 File Offset: 0x0000BE97
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.Indexer);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
