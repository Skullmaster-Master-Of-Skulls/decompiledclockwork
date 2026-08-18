using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000322 RID: 802
	internal sealed class Vertex : IEquatable<Vertex>
	{
		// Token: 0x06001BAD RID: 7085 RVA: 0x0008831C File Offset: 0x0008651C
		private Vertex()
		{
			this.Variable = int.MaxValue;
			this.Children = new Vertex[0];
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0008833B File Offset: 0x0008653B
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.EntityUtil.BoolExprAssert(System.Boolean,System.String)")]
		internal Vertex(int variable, Vertex[] children)
		{
			if (variable >= 2147483647)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.BoolExprAssert, 0, "exceeded number of supported variables");
			}
			this.Variable = variable;
			this.Children = children;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0008836C File Offset: 0x0008656C
		[Conditional("DEBUG")]
		private static void AssertConstructorArgumentsValid(int variable, Vertex[] children)
		{
			foreach (Vertex vertex in children)
			{
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0008838D File Offset: 0x0008658D
		internal bool IsOne()
		{
			return object.ReferenceEquals(Vertex.One, this);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0008839A File Offset: 0x0008659A
		internal bool IsZero()
		{
			return object.ReferenceEquals(Vertex.Zero, this);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000883A7 File Offset: 0x000865A7
		internal bool IsSink()
		{
			return this.Variable == int.MaxValue;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000883B6 File Offset: 0x000865B6
		public bool Equals(Vertex other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000883BF File Offset: 0x000865BF
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000883C8 File Offset: 0x000865C8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000883D0 File Offset: 0x000865D0
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

		// Token: 0x040009B2 RID: 2482
		internal static readonly Vertex One = new Vertex();

		// Token: 0x040009B3 RID: 2483
		internal static readonly Vertex Zero = new Vertex();

		// Token: 0x040009B4 RID: 2484
		internal readonly int Variable;

		// Token: 0x040009B5 RID: 2485
		internal readonly Vertex[] Children;
	}
}
