using System;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	// Token: 0x020000BC RID: 188
	[__DynamicallyInvokable]
	public abstract class BinaryOperationBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00010F08 File Offset: 0x0000F108
		[__DynamicallyInvokable]
		protected BinaryOperationBinder(ExpressionType operation)
		{
			ContractUtils.Requires(BinaryOperationBinder.OperationIsValid(operation), "operation");
			this._operation = operation;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00010F27 File Offset: 0x0000F127
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00010F33 File Offset: 0x0000F133
		[__DynamicallyInvokable]
		public ExpressionType Operation
		{
			[__DynamicallyInvokable]
			get
			{
				return this._operation;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00010F3B File Offset: 0x0000F13B
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg)
		{
			return this.FallbackBinaryOperation(target, arg, null);
		}

		// Token: 0x06000566 RID: 1382
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackBinaryOperation(DynamicMetaObject target, DynamicMetaObject arg, DynamicMetaObject errorSuggestion);

		// Token: 0x06000567 RID: 1383 RVA: 0x00010F48 File Offset: 0x0000F148
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject dynamicMetaObject = args[0];
			ContractUtils.RequiresNotNull(dynamicMetaObject, "args");
			return target.BindBinaryOperation(this, dynamicMetaObject);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00010F92 File Offset: 0x0000F192
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00010F98 File Offset: 0x0000F198
		internal static bool OperationIsValid(ExpressionType operation)
		{
			if (operation <= ExpressionType.Multiply)
			{
				if (operation != ExpressionType.Add && operation != ExpressionType.And)
				{
					switch (operation)
					{
					case ExpressionType.Divide:
					case ExpressionType.Equal:
					case ExpressionType.ExclusiveOr:
					case ExpressionType.GreaterThan:
					case ExpressionType.GreaterThanOrEqual:
					case ExpressionType.LeftShift:
					case ExpressionType.LessThan:
					case ExpressionType.LessThanOrEqual:
					case ExpressionType.Modulo:
					case ExpressionType.Multiply:
						break;
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.ListInit:
					case ExpressionType.MemberAccess:
					case ExpressionType.MemberInit:
						return false;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (operation)
				{
				case ExpressionType.NotEqual:
				case ExpressionType.Or:
				case ExpressionType.Power:
				case ExpressionType.RightShift:
				case ExpressionType.Subtract:
					break;
				case ExpressionType.OrElse:
				case ExpressionType.Parameter:
				case ExpressionType.Quote:
					return false;
				default:
					if (operation != ExpressionType.Extension && operation - ExpressionType.AddAssign > 10)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x04000595 RID: 1429
		private ExpressionType _operation;
	}
}
