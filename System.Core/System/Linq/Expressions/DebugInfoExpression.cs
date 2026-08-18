using System;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000228 RID: 552
	[DebuggerTypeProxy(typeof(Expression.DebugInfoExpressionProxy))]
	[__DynamicallyInvokable]
	public class DebugInfoExpression : Expression
	{
		// Token: 0x06001412 RID: 5138 RVA: 0x00044009 File Offset: 0x00042209
		internal DebugInfoExpression(SymbolDocumentInfo document)
		{
			this._document = document;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00044018 File Offset: 0x00042218
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(void);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00044024 File Offset: 0x00042224
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.DebugInfo;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00044028 File Offset: 0x00042228
		[__DynamicallyInvokable]
		public virtual int StartLine
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004402F File Offset: 0x0004222F
		[__DynamicallyInvokable]
		public virtual int StartColumn
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00044036 File Offset: 0x00042236
		[__DynamicallyInvokable]
		public virtual int EndLine
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004403D File Offset: 0x0004223D
		[__DynamicallyInvokable]
		public virtual int EndColumn
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00044044 File Offset: 0x00042244
		[__DynamicallyInvokable]
		public SymbolDocumentInfo Document
		{
			[__DynamicallyInvokable]
			get
			{
				return this._document;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0004404C File Offset: 0x0004224C
		[__DynamicallyInvokable]
		public virtual bool IsClear
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00044053 File Offset: 0x00042253
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x04000981 RID: 2433
		private readonly SymbolDocumentInfo _document;
	}
}
