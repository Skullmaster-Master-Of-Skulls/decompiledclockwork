using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	// Token: 0x020000D0 RID: 208
	[__DynamicallyInvokable]
	public abstract class UnaryOperationBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x000126AC File Offset: 0x000108AC
		[__DynamicallyInvokable]
		protected UnaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(UnaryOperationBinder.OperationIsValid(operation), "operation");
			this._operation = operation;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000126CC File Offset: 0x000108CC
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				ExpressionType operation = this._operation;
				if (operation - ExpressionType.IsTrue <= 1)
				{
					return typeof(bool);
				}
				return typeof(object);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000126FC File Offset: 0x000108FC
		[__DynamicallyInvokable]
		public ExpressionType Operation
		{
			[__DynamicallyInvokable]
			get
			{
				return this._operation;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00012704 File Offset: 0x00010904
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target)
		{
			return this.FallbackUnaryOperation(target, null);
		}

		// Token: 0x0600062C RID: 1580
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackUnaryOperation(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		// Token: 0x0600062D RID: 1581 RVA: 0x0001270E File Offset: 0x0001090E
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindUnaryOperation(this);
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00012737 File Offset: 0x00010937
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001273A File Offset: 0x0001093A
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Decrement)
			{
				if (operation - ExpressionType.Negate > 1 && operation != ExpressionType.Not && operation != ExpressionType.Decrement)
				{
					return false;
				}
			}
			else if (operation != ExpressionType.Extension && operation != ExpressionType.Increment && operation - ExpressionType.OnesComplement > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x040005BE RID: 1470
		private ExpressionType _operation;
	}
}
