using System;
using System.Collections.Generic;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AD RID: 173
	internal sealed class ExpressionFingerprintChain : IEquatable<ExpressionFingerprintChain>
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public bool Equals(ExpressionFingerprintChain other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Elements.Count != other.Elements.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Elements.Count; i++)
			{
				if (!object.Equals(this.Elements[i], other.Elements[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000D648 File Offset: 0x0000B848
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExpressionFingerprintChain);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000D658 File Offset: 0x0000B858
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			this.Elements.ForEach(new Action<ExpressionFingerprint>(hashCodeCombiner.AddFingerprint));
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x0400014A RID: 330
		public readonly List<ExpressionFingerprint> Elements = new List<ExpressionFingerprint>();
	}
}
