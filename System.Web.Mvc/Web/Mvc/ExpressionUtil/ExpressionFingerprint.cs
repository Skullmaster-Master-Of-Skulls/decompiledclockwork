using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000A6 RID: 166
	internal abstract class ExpressionFingerprint
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x0000D11E File Offset: 0x0000B31E
		protected ExpressionFingerprint(ExpressionType nodeType, Type type)
		{
			this.NodeType = nodeType;
			this.Type = type;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000D134 File Offset: 0x0000B334
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0000D13C File Offset: 0x0000B33C
		public ExpressionType NodeType { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000D145 File Offset: 0x0000B345
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000D14D File Offset: 0x0000B34D
		public Type Type { get; private set; }

		// Token: 0x06000488 RID: 1160 RVA: 0x0000D156 File Offset: 0x0000B356
		internal virtual void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddInt32((int)this.NodeType);
			combiner.AddObject(this.Type);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000D170 File Offset: 0x0000B370
		protected bool Equals(ExpressionFingerprint other)
		{
			return other != null && this.NodeType == other.NodeType && object.Equals(this.Type, other.Type);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000D196 File Offset: 0x0000B396
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExpressionFingerprint);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			this.AddToHashCodeCombiner(hashCodeCombiner);
			return hashCodeCombiner.CombinedHash;
		}
	}
}
