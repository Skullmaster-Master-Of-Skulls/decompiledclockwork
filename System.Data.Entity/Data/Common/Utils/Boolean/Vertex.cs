using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A9 RID: 937
	internal sealed class Vertex : IEquatable<Vertex>
	{
		// Token: 0x06003388 RID: 13192 RVA: 0x000C86A3 File Offset: 0x000C68A3
		private Vertex()
		{
			this.Variable = int.MaxValue;
			this.Children = new Vertex[0];
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000C86C2 File Offset: 0x000C68C2
		internal Vertex(int variable, Vertex[] children)
		{
			EntityUtil.BoolExprAssert(variable < int.MaxValue, "exceeded number of supported variables");
			this.Variable = variable;
			this.Children = children;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000C86EC File Offset: 0x000C68EC
		[Conditional("DEBUG")]
		private static void AssertConstructorArgumentsValid(int variable, Vertex[] children)
		{
			foreach (Vertex vertex in children)
			{
			}
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000C870D File Offset: 0x000C690D
		internal bool IsOne()
		{
			return Vertex.One == this;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000C8717 File Offset: 0x000C6917
		internal bool IsZero()
		{
			return Vertex.Zero == this;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000C8721 File Offset: 0x000C6921
		internal bool IsSink()
		{
			return this.Variable == int.MaxValue;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0005AF88 File Offset: 0x00059188
		public bool Equals(Vertex other)
		{
			return this == other;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000A1177 File Offset: 0x0009F377
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0009B148 File Offset: 0x00099348
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000C8730 File Offset: 0x000C6930
		public override string ToString()
		{
			if (this.IsOne())
			{
				return "_1_";
			}
			if (this.IsZero())
			{
				return "_0_";
			}
			return string.Format(CultureInfo.InvariantCulture, "<{0}, {1}>", new object[]
			{
				this.Variable,
				StringUtil.ToCommaSeparatedString(this.Children)
			});
		}

		// Token: 0x04001690 RID: 5776
		internal static readonly Vertex One = new Vertex();

		// Token: 0x04001691 RID: 5777
		internal static readonly Vertex Zero = new Vertex();

		// Token: 0x04001692 RID: 5778
		internal readonly int Variable;

		// Token: 0x04001693 RID: 5779
		internal readonly Vertex[] Children;
	}
}
