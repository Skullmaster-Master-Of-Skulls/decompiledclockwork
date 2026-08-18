using System;
using System.Data.Entity.Core.Objects.Internal;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002D6 RID: 726
	internal class TranslatorResult
	{
		// Token: 0x0600196C RID: 6508 RVA: 0x0007F036 File Offset: 0x0007D236
		internal TranslatorResult(Expression returnedExpression, Type requestedType)
		{
			this.RequestedType = requestedType;
			this.ReturnedExpression = returnedExpression;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0007F04C File Offset: 0x0007D24C
		internal Expression Expression
		{
			get
			{
				return CodeGenEmitter.Emit_EnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0007F06C File Offset: 0x0007D26C
		internal Expression UnconvertedExpression
		{
			get
			{
				return this.ReturnedExpression;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0007F074 File Offset: 0x0007D274
		internal Expression UnwrappedExpression
		{
			get
			{
				if (!typeof(IEntityWrapper).IsAssignableFrom(this.ReturnedExpression.Type))
				{
					return this.ReturnedExpression;
				}
				return CodeGenEmitter.Emit_UnwrapAndEnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x040008CA RID: 2250
		private readonly Expression ReturnedExpression;

		// Token: 0x040008CB RID: 2251
		private readonly Type RequestedType;
	}
}
