using System;
using System.Data.Objects.Internal;
using System.Linq.Expressions;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D5 RID: 981
	internal class TranslatorResult
	{
		// Token: 0x060034D4 RID: 13524 RVA: 0x000CBF76 File Offset: 0x000CA176
		internal TranslatorResult(Expression returnedExpression, Type requestedType)
		{
			this.RequestedType = requestedType;
			this.ReturnedExpression = returnedExpression;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000CBF8C File Offset: 0x000CA18C
		internal Expression Expression
		{
			get
			{
				return Translator.Emit_EnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000CBFAC File Offset: 0x000CA1AC
		internal Expression UnconvertedExpression
		{
			get
			{
				return this.ReturnedExpression;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000CBFB4 File Offset: 0x000CA1B4
		internal Expression UnwrappedExpression
		{
			get
			{
				if (!typeof(IEntityWrapper).IsAssignableFrom(this.ReturnedExpression.Type))
				{
					return this.ReturnedExpression;
				}
				return Translator.Emit_UnwrapAndEnsureType(this.ReturnedExpression, this.RequestedType);
			}
		}

		// Token: 0x04001725 RID: 5925
		private readonly Expression ReturnedExpression;

		// Token: 0x04001726 RID: 5926
		private readonly Type RequestedType;
	}
}
