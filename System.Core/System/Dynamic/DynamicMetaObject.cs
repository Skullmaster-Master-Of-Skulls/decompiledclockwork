using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.Remoting;

namespace System.Dynamic
{
	// Token: 0x020000C3 RID: 195
	[__DynamicallyInvokable]
	public class DynamicMetaObject
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0001143A File Offset: 0x0000F63A
		[__DynamicallyInvokable]
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			this._expression = expression;
			this._restrictions = restrictions;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00011466 File Offset: 0x0000F666
		[__DynamicallyInvokable]
		public DynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value) : this(expression, restrictions)
		{
			this._value = value;
			this._hasValue = true;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001147E File Offset: 0x0000F67E
		[__DynamicallyInvokable]
		public Expression Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00011486 File Offset: 0x0000F686
		[__DynamicallyInvokable]
		public BindingRestrictions Restrictions
		{
			[__DynamicallyInvokable]
			get
			{
				return this._restrictions;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001148E File Offset: 0x0000F68E
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00011496 File Offset: 0x0000F696
		[__DynamicallyInvokable]
		public bool HasValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000114A0 File Offset: 0x0000F6A0
		[__DynamicallyInvokable]
		public Type RuntimeType
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this._hasValue)
				{
					return null;
				}
				Type type = this.Expression.Type;
				if (type.IsValueType)
				{
					return type;
				}
				if (this._value != null)
				{
					return this._value.GetType();
				}
				return null;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000114E2 File Offset: 0x0000F6E2
		[__DynamicallyInvokable]
		public Type LimitType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.RuntimeType ?? this.Expression.Type;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000114F9 File Offset: 0x0000F6F9
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackConvert(this);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001150D File Offset: 0x0000F70D
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetMember(this);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00011521 File Offset: 0x0000F721
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetMember(this, value);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00011536 File Offset: 0x0000F736
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteMember(this);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001154A File Offset: 0x0000F74A
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackGetIndex(this, indexes);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001155F File Offset: 0x0000F75F
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackSetIndex(this, indexes, value);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00011575 File Offset: 0x0000F775
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackDeleteIndex(this, indexes);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001158A File Offset: 0x0000F78A
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvokeMember(this, args);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001159F File Offset: 0x0000F79F
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackInvoke(this, args);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000115B4 File Offset: 0x0000F7B4
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackCreateInstance(this, args);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000115C9 File Offset: 0x0000F7C9
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackUnaryOperation(this);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000115DD File Offset: 0x0000F7DD
		[__DynamicallyInvokable]
		public virtual DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			return binder.FallbackBinaryOperation(this, arg);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000115F2 File Offset: 0x0000F7F2
		[__DynamicallyInvokable]
		public virtual IEnumerable<string> GetDynamicMemberNames()
		{
			return new string[0];
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000115FC File Offset: 0x0000F7FC
		internal static Expression[] GetExpressions(DynamicMetaObject[] objects)
		{
			ContractUtils.RequiresNotNull(objects, "objects");
			Expression[] array = new Expression[objects.Length];
			for (int i = 0; i < objects.Length; i++)
			{
				DynamicMetaObject dynamicMetaObject = objects[i];
				ContractUtils.RequiresNotNull(dynamicMetaObject, "objects");
				Expression expression = dynamicMetaObject.Expression;
				ContractUtils.RequiresNotNull(expression, "objects");
				array[i] = expression;
			}
			return array;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00011654 File Offset: 0x0000F854
		[__DynamicallyInvokable]
		public static DynamicMetaObject Create(object value, Expression expression)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = value as IDynamicMetaObjectProvider;
			if (dynamicMetaObjectProvider == null || RemotingServices.IsObjectOutOfAppDomain(value))
			{
				return new DynamicMetaObject(expression, BindingRestrictions.Empty, value);
			}
			DynamicMetaObject metaObject = dynamicMetaObjectProvider.GetMetaObject(expression);
			if (metaObject == null || !metaObject.HasValue || metaObject.Value == null || metaObject.Expression != expression)
			{
				throw Error.InvalidMetaObjectCreated(dynamicMetaObjectProvider.GetType());
			}
			return metaObject;
		}

		// Token: 0x040005A2 RID: 1442
		private readonly Expression _expression;

		// Token: 0x040005A3 RID: 1443
		private readonly BindingRestrictions _restrictions;

		// Token: 0x040005A4 RID: 1444
		private readonly object _value;

		// Token: 0x040005A5 RID: 1445
		private readonly bool _hasValue;

		// Token: 0x040005A6 RID: 1446
		[__DynamicallyInvokable]
		public static readonly DynamicMetaObject[] EmptyMetaObjects = new DynamicMetaObject[0];
	}
}
