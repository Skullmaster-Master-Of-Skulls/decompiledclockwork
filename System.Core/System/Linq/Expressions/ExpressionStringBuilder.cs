using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Linq.Expressions
{
	// Token: 0x02000239 RID: 569
	internal sealed class ExpressionStringBuilder : ExpressionVisitor
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x00046268 File Offset: 0x00044468
		private ExpressionStringBuilder()
		{
			this._out = new StringBuilder();
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004627B File Offset: 0x0004447B
		public override string ToString()
		{
			return this._out.ToString();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00046288 File Offset: 0x00044488
		private void AddLabel(LabelTarget label)
		{
			if (this._ids == null)
			{
				this._ids = new Dictionary<object, int>();
				this._ids.Add(label, 0);
				return;
			}
			if (!this._ids.ContainsKey(label))
			{
				this._ids.Add(label, this._ids.Count);
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x000462DC File Offset: 0x000444DC
		private int GetLabelId(LabelTarget label)
		{
			if (this._ids == null)
			{
				this._ids = new Dictionary<object, int>();
				this.AddLabel(label);
				return 0;
			}
			int count;
			if (!this._ids.TryGetValue(label, out count))
			{
				count = this._ids.Count;
				this.AddLabel(label);
			}
			return count;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0004632C File Offset: 0x0004452C
		private void AddParam(ParameterExpression p)
		{
			if (this._ids == null)
			{
				this._ids = new Dictionary<object, int>();
				this._ids.Add(this._ids, 0);
				return;
			}
			if (!this._ids.ContainsKey(p))
			{
				this._ids.Add(p, this._ids.Count);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00046384 File Offset: 0x00044584
		private int GetParamId(ParameterExpression p)
		{
			if (this._ids == null)
			{
				this._ids = new Dictionary<object, int>();
				this.AddParam(p);
				return 0;
			}
			int count;
			if (!this._ids.TryGetValue(p, out count))
			{
				count = this._ids.Count;
				this.AddParam(p);
			}
			return count;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x000463D1 File Offset: 0x000445D1
		private void Out(string s)
		{
			this._out.Append(s);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000463E0 File Offset: 0x000445E0
		private void Out(char c)
		{
			this._out.Append(c);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000463F0 File Offset: 0x000445F0
		internal static string ExpressionToString(Expression node)
		{
			ExpressionStringBuilder expressionStringBuilder = new ExpressionStringBuilder();
			expressionStringBuilder.Visit(node);
			return expressionStringBuilder.ToString();
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00046414 File Offset: 0x00044614
		internal static string CatchBlockToString(CatchBlock node)
		{
			ExpressionStringBuilder expressionStringBuilder = new ExpressionStringBuilder();
			expressionStringBuilder.VisitCatchBlock(node);
			return expressionStringBuilder.ToString();
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00046438 File Offset: 0x00044638
		internal static string SwitchCaseToString(SwitchCase node)
		{
			ExpressionStringBuilder expressionStringBuilder = new ExpressionStringBuilder();
			expressionStringBuilder.VisitSwitchCase(node);
			return expressionStringBuilder.ToString();
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0004645C File Offset: 0x0004465C
		internal static string MemberBindingToString(MemberBinding node)
		{
			ExpressionStringBuilder expressionStringBuilder = new ExpressionStringBuilder();
			expressionStringBuilder.VisitMemberBinding(node);
			return expressionStringBuilder.ToString();
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00046480 File Offset: 0x00044680
		internal static string ElementInitBindingToString(ElementInit node)
		{
			ExpressionStringBuilder expressionStringBuilder = new ExpressionStringBuilder();
			expressionStringBuilder.VisitElementInit(node);
			return expressionStringBuilder.ToString();
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x000464A4 File Offset: 0x000446A4
		private static string FormatBinder(CallSiteBinder binder)
		{
			ConvertBinder convertBinder;
			if ((convertBinder = (binder as ConvertBinder)) != null)
			{
				string str = "Convert ";
				Type type = convertBinder.Type;
				return str + ((type != null) ? type.ToString() : null);
			}
			GetMemberBinder getMemberBinder;
			if ((getMemberBinder = (binder as GetMemberBinder)) != null)
			{
				return "GetMember " + getMemberBinder.Name;
			}
			SetMemberBinder setMemberBinder;
			if ((setMemberBinder = (binder as SetMemberBinder)) != null)
			{
				return "SetMember " + setMemberBinder.Name;
			}
			DeleteMemberBinder deleteMemberBinder;
			if ((deleteMemberBinder = (binder as DeleteMemberBinder)) != null)
			{
				return "DeleteMember " + deleteMemberBinder.Name;
			}
			if (binder is GetIndexBinder)
			{
				return "GetIndex";
			}
			if (binder is SetIndexBinder)
			{
				return "SetIndex";
			}
			if (binder is DeleteIndexBinder)
			{
				return "DeleteIndex";
			}
			InvokeMemberBinder invokeMemberBinder;
			if ((invokeMemberBinder = (binder as InvokeMemberBinder)) != null)
			{
				return "Call " + invokeMemberBinder.Name;
			}
			if (binder is InvokeBinder)
			{
				return "Invoke";
			}
			if (binder is CreateInstanceBinder)
			{
				return "Create";
			}
			UnaryOperationBinder unaryOperationBinder;
			if ((unaryOperationBinder = (binder as UnaryOperationBinder)) != null)
			{
				return unaryOperationBinder.Operation.ToString();
			}
			BinaryOperationBinder binaryOperationBinder;
			if ((binaryOperationBinder = (binder as BinaryOperationBinder)) != null)
			{
				return binaryOperationBinder.Operation.ToString();
			}
			return "CallSiteBinder";
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x000465D5 File Offset: 0x000447D5
		private void VisitExpressions<T>(char open, IList<T> expressions, char close) where T : Expression
		{
			this.VisitExpressions<T>(open, expressions, close, ", ");
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000465E8 File Offset: 0x000447E8
		private void VisitExpressions<T>(char open, IList<T> expressions, char close, string seperator) where T : Expression
		{
			this.Out(open);
			if (expressions != null)
			{
				bool flag = true;
				foreach (T t in expressions)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this.Out(seperator);
					}
					this.Visit(t);
				}
			}
			this.Out(close);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00046658 File Offset: 0x00044858
		protected internal override Expression VisitDynamic(DynamicExpression node)
		{
			this.Out(ExpressionStringBuilder.FormatBinder(node.Binder));
			this.VisitExpressions<Expression>('(', node.Arguments, ')');
			return node;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004667C File Offset: 0x0004487C
		protected internal override Expression VisitBinary(BinaryExpression node)
		{
			if (node.NodeType == ExpressionType.ArrayIndex)
			{
				this.Visit(node.Left);
				this.Out("[");
				this.Visit(node.Right);
				this.Out("]");
			}
			else
			{
				ExpressionType nodeType = node.NodeType;
				string s;
				switch (nodeType)
				{
				case ExpressionType.Add:
					s = "+";
					goto IL_3BE;
				case ExpressionType.AddChecked:
					s = "+";
					goto IL_3BE;
				case ExpressionType.And:
					if (node.Type == typeof(bool) || node.Type == typeof(bool?))
					{
						s = "And";
						goto IL_3BE;
					}
					s = "&";
					goto IL_3BE;
				case ExpressionType.AndAlso:
					s = "AndAlso";
					goto IL_3BE;
				case ExpressionType.ArrayLength:
				case ExpressionType.ArrayIndex:
				case ExpressionType.Call:
				case ExpressionType.Conditional:
				case ExpressionType.Constant:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.Invoke:
				case ExpressionType.Lambda:
				case ExpressionType.ListInit:
				case ExpressionType.MemberAccess:
				case ExpressionType.MemberInit:
				case ExpressionType.Negate:
				case ExpressionType.UnaryPlus:
				case ExpressionType.NegateChecked:
				case ExpressionType.New:
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
				case ExpressionType.Not:
				case ExpressionType.Parameter:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
				case ExpressionType.TypeIs:
					break;
				case ExpressionType.Coalesce:
					s = "??";
					goto IL_3BE;
				case ExpressionType.Divide:
					s = "/";
					goto IL_3BE;
				case ExpressionType.Equal:
					s = "==";
					goto IL_3BE;
				case ExpressionType.ExclusiveOr:
					s = "^";
					goto IL_3BE;
				case ExpressionType.GreaterThan:
					s = ">";
					goto IL_3BE;
				case ExpressionType.GreaterThanOrEqual:
					s = ">=";
					goto IL_3BE;
				case ExpressionType.LeftShift:
					s = "<<";
					goto IL_3BE;
				case ExpressionType.LessThan:
					s = "<";
					goto IL_3BE;
				case ExpressionType.LessThanOrEqual:
					s = "<=";
					goto IL_3BE;
				case ExpressionType.Modulo:
					s = "%";
					goto IL_3BE;
				case ExpressionType.Multiply:
					s = "*";
					goto IL_3BE;
				case ExpressionType.MultiplyChecked:
					s = "*";
					goto IL_3BE;
				case ExpressionType.NotEqual:
					s = "!=";
					goto IL_3BE;
				case ExpressionType.Or:
					if (node.Type == typeof(bool) || node.Type == typeof(bool?))
					{
						s = "Or";
						goto IL_3BE;
					}
					s = "|";
					goto IL_3BE;
				case ExpressionType.OrElse:
					s = "OrElse";
					goto IL_3BE;
				case ExpressionType.Power:
					s = "^";
					goto IL_3BE;
				case ExpressionType.RightShift:
					s = ">>";
					goto IL_3BE;
				case ExpressionType.Subtract:
					s = "-";
					goto IL_3BE;
				case ExpressionType.SubtractChecked:
					s = "-";
					goto IL_3BE;
				case ExpressionType.Assign:
					s = "=";
					goto IL_3BE;
				default:
					switch (nodeType)
					{
					case ExpressionType.AddAssign:
						s = "+=";
						goto IL_3BE;
					case ExpressionType.AndAssign:
						if (node.Type == typeof(bool) || node.Type == typeof(bool?))
						{
							s = "&&=";
							goto IL_3BE;
						}
						s = "&=";
						goto IL_3BE;
					case ExpressionType.DivideAssign:
						s = "/=";
						goto IL_3BE;
					case ExpressionType.ExclusiveOrAssign:
						s = "^=";
						goto IL_3BE;
					case ExpressionType.LeftShiftAssign:
						s = "<<=";
						goto IL_3BE;
					case ExpressionType.ModuloAssign:
						s = "%=";
						goto IL_3BE;
					case ExpressionType.MultiplyAssign:
						s = "*=";
						goto IL_3BE;
					case ExpressionType.OrAssign:
						if (node.Type == typeof(bool) || node.Type == typeof(bool?))
						{
							s = "||=";
							goto IL_3BE;
						}
						s = "|=";
						goto IL_3BE;
					case ExpressionType.PowerAssign:
						s = "**=";
						goto IL_3BE;
					case ExpressionType.RightShiftAssign:
						s = ">>=";
						goto IL_3BE;
					case ExpressionType.SubtractAssign:
						s = "-=";
						goto IL_3BE;
					case ExpressionType.AddAssignChecked:
						s = "+=";
						goto IL_3BE;
					case ExpressionType.MultiplyAssignChecked:
						s = "*=";
						goto IL_3BE;
					case ExpressionType.SubtractAssignChecked:
						s = "-=";
						goto IL_3BE;
					}
					break;
				}
				throw new InvalidOperationException();
				IL_3BE:
				this.Out("(");
				this.Visit(node.Left);
				this.Out(' ');
				this.Out(s);
				this.Out(' ');
				this.Visit(node.Right);
				this.Out(")");
			}
			return node;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00046A90 File Offset: 0x00044C90
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			if (node.IsByRef)
			{
				this.Out("ref ");
			}
			string name = node.Name;
			if (string.IsNullOrEmpty(name))
			{
				this.Out("Param_" + this.GetParamId(node).ToString());
			}
			else
			{
				this.Out(name);
			}
			return node;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00046AE8 File Offset: 0x00044CE8
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			if (node.Parameters.Count == 1)
			{
				this.Visit(node.Parameters[0]);
			}
			else
			{
				this.VisitExpressions<ParameterExpression>('(', node.Parameters, ')');
			}
			this.Out(" => ");
			this.Visit(node.Body);
			return node;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x00046B44 File Offset: 0x00044D44
		protected internal override Expression VisitListInit(ListInitExpression node)
		{
			this.Visit(node.NewExpression);
			this.Out(" {");
			int i = 0;
			int count = node.Initializers.Count;
			while (i < count)
			{
				if (i > 0)
				{
					this.Out(", ");
				}
				this.Out(node.Initializers[i].ToString());
				i++;
			}
			this.Out("}");
			return node;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00046BB4 File Offset: 0x00044DB4
		protected internal override Expression VisitConditional(ConditionalExpression node)
		{
			this.Out("IIF(");
			this.Visit(node.Test);
			this.Out(", ");
			this.Visit(node.IfTrue);
			this.Out(", ");
			this.Visit(node.IfFalse);
			this.Out(")");
			return node;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00046C18 File Offset: 0x00044E18
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			if (node.Value != null)
			{
				string text = node.Value.ToString();
				if (node.Value is string)
				{
					this.Out("\"");
					this.Out(text);
					this.Out("\"");
				}
				else if (text == node.Value.GetType().ToString())
				{
					this.Out("value(");
					this.Out(text);
					this.Out(")");
				}
				else
				{
					this.Out(text);
				}
			}
			else
			{
				this.Out("null");
			}
			return node;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00046CB4 File Offset: 0x00044EB4
		protected internal override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			string s = string.Format(CultureInfo.CurrentCulture, "<DebugInfo({0}: {1}, {2}, {3}, {4})>", new object[]
			{
				node.Document.FileName,
				node.StartLine,
				node.StartColumn,
				node.EndLine,
				node.EndColumn
			});
			this.Out(s);
			return node;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00046D25 File Offset: 0x00044F25
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			this.VisitExpressions<ParameterExpression>('(', node.Variables, ')');
			return node;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00046D38 File Offset: 0x00044F38
		private void OutMember(Expression instance, MemberInfo member)
		{
			if (instance != null)
			{
				this.Visit(instance);
				this.Out("." + member.Name);
				return;
			}
			this.Out(member.DeclaringType.Name + "." + member.Name);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00046D88 File Offset: 0x00044F88
		protected internal override Expression VisitMember(MemberExpression node)
		{
			this.OutMember(node.Expression, node.Member);
			return node;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00046DA0 File Offset: 0x00044FA0
		protected internal override Expression VisitMemberInit(MemberInitExpression node)
		{
			if (node.NewExpression.Arguments.Count == 0 && node.NewExpression.Type.Name.Contains("<"))
			{
				this.Out("new");
			}
			else
			{
				this.Visit(node.NewExpression);
			}
			this.Out(" {");
			int i = 0;
			int count = node.Bindings.Count;
			while (i < count)
			{
				MemberBinding node2 = node.Bindings[i];
				if (i > 0)
				{
					this.Out(", ");
				}
				this.VisitMemberBinding(node2);
				i++;
			}
			this.Out("}");
			return node;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00046E48 File Offset: 0x00045048
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Out(assignment.Member.Name);
			this.Out(" = ");
			this.Visit(assignment.Expression);
			return assignment;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00046E74 File Offset: 0x00045074
		protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(" = {");
			int i = 0;
			int count = binding.Initializers.Count;
			while (i < count)
			{
				if (i > 0)
				{
					this.Out(", ");
				}
				this.VisitElementInit(binding.Initializers[i]);
				i++;
			}
			this.Out("}");
			return binding;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00046EE4 File Offset: 0x000450E4
		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(" = {");
			int i = 0;
			int count = binding.Bindings.Count;
			while (i < count)
			{
				if (i > 0)
				{
					this.Out(", ");
				}
				this.VisitMemberBinding(binding.Bindings[i]);
				i++;
			}
			this.Out("}");
			return binding;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00046F54 File Offset: 0x00045154
		protected override ElementInit VisitElementInit(ElementInit initializer)
		{
			this.Out(initializer.AddMethod.ToString());
			string seperator = ", ";
			this.VisitExpressions<Expression>('(', initializer.Arguments, ')', seperator);
			return initializer;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00046F8C File Offset: 0x0004518C
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			this.Out("Invoke(");
			this.Visit(node.Expression);
			string s = ", ";
			int i = 0;
			int count = node.Arguments.Count;
			while (i < count)
			{
				this.Out(s);
				this.Visit(node.Arguments[i]);
				i++;
			}
			this.Out(")");
			return node;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00046FF8 File Offset: 0x000451F8
		protected internal override Expression VisitMethodCall(MethodCallExpression node)
		{
			int num = 0;
			Expression expression = node.Object;
			if (Attribute.GetCustomAttribute(node.Method, typeof(ExtensionAttribute)) != null)
			{
				num = 1;
				expression = node.Arguments[0];
			}
			if (expression != null)
			{
				this.Visit(expression);
				this.Out(".");
			}
			this.Out(node.Method.Name);
			this.Out("(");
			int i = num;
			int count = node.Arguments.Count;
			while (i < count)
			{
				if (i > num)
				{
					this.Out(", ");
				}
				this.Visit(node.Arguments[i]);
				i++;
			}
			this.Out(")");
			return node;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000470AC File Offset: 0x000452AC
		protected internal override Expression VisitNewArray(NewArrayExpression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType != ExpressionType.NewArrayInit)
			{
				if (nodeType == ExpressionType.NewArrayBounds)
				{
					this.Out("new " + node.Type.ToString());
					this.VisitExpressions<Expression>('(', node.Expressions, ')');
				}
			}
			else
			{
				this.Out("new [] ");
				this.VisitExpressions<Expression>('{', node.Expressions, '}');
			}
			return node;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00047114 File Offset: 0x00045314
		protected internal override Expression VisitNew(NewExpression node)
		{
			this.Out("new " + node.Type.Name);
			this.Out("(");
			ReadOnlyCollection<MemberInfo> members = node.Members;
			for (int i = 0; i < node.Arguments.Count; i++)
			{
				if (i > 0)
				{
					this.Out(", ");
				}
				if (members != null)
				{
					string name = members[i].Name;
					this.Out(name);
					this.Out(" = ");
				}
				this.Visit(node.Arguments[i]);
			}
			this.Out(")");
			return node;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000471B4 File Offset: 0x000453B4
		protected internal override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			this.Out("(");
			this.Visit(node.Expression);
			ExpressionType nodeType = node.NodeType;
			if (nodeType != ExpressionType.TypeIs)
			{
				if (nodeType == ExpressionType.TypeEqual)
				{
					this.Out(" TypeEqual ");
				}
			}
			else
			{
				this.Out(" Is ");
			}
			this.Out(node.TypeOperand.Name);
			this.Out(")");
			return node;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00047224 File Offset: 0x00045424
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Decrement)
			{
				if (nodeType <= ExpressionType.Quote)
				{
					switch (nodeType)
					{
					case ExpressionType.Negate:
					case ExpressionType.NegateChecked:
						this.Out("-");
						goto IL_130;
					case ExpressionType.UnaryPlus:
						this.Out("+");
						goto IL_130;
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						this.Out("Not(");
						goto IL_130;
					default:
						if (nodeType == ExpressionType.Quote)
						{
							goto IL_130;
						}
						break;
					}
				}
				else
				{
					if (nodeType == ExpressionType.TypeAs)
					{
						this.Out("(");
						goto IL_130;
					}
					if (nodeType == ExpressionType.Decrement)
					{
						this.Out("Decrement(");
						goto IL_130;
					}
				}
			}
			else if (nodeType <= ExpressionType.Throw)
			{
				if (nodeType == ExpressionType.Increment)
				{
					this.Out("Increment(");
					goto IL_130;
				}
				if (nodeType == ExpressionType.Throw)
				{
					this.Out("throw(");
					goto IL_130;
				}
			}
			else
			{
				if (nodeType == ExpressionType.PreIncrementAssign)
				{
					this.Out("++");
					goto IL_130;
				}
				if (nodeType == ExpressionType.PreDecrementAssign)
				{
					this.Out("--");
					goto IL_130;
				}
				if (nodeType == ExpressionType.OnesComplement)
				{
					this.Out("~(");
					goto IL_130;
				}
			}
			this.Out(node.NodeType.ToString());
			this.Out("(");
			IL_130:
			this.Visit(node.Operand);
			ExpressionType nodeType2 = node.NodeType;
			if (nodeType2 <= ExpressionType.Quote)
			{
				if (nodeType2 - ExpressionType.Negate <= 2 || nodeType2 == ExpressionType.Quote)
				{
					return node;
				}
			}
			else
			{
				if (nodeType2 == ExpressionType.TypeAs)
				{
					this.Out(" As ");
					this.Out(node.Type.Name);
					this.Out(")");
					return node;
				}
				switch (nodeType2)
				{
				case ExpressionType.PreIncrementAssign:
				case ExpressionType.PreDecrementAssign:
					return node;
				case ExpressionType.PostIncrementAssign:
					this.Out("++");
					return node;
				case ExpressionType.PostDecrementAssign:
					this.Out("--");
					return node;
				}
			}
			this.Out(")");
			return node;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000473F8 File Offset: 0x000455F8
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			this.Out("{");
			foreach (ParameterExpression node2 in node.Variables)
			{
				this.Out("var ");
				this.Visit(node2);
				this.Out(";");
			}
			this.Out(" ... }");
			return node;
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00047474 File Offset: 0x00045674
		protected internal override Expression VisitDefault(DefaultExpression node)
		{
			this.Out("default(");
			this.Out(node.Type.Name);
			this.Out(")");
			return node;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004749E File Offset: 0x0004569E
		protected internal override Expression VisitLabel(LabelExpression node)
		{
			this.Out("{ ... } ");
			this.DumpLabel(node.Target);
			this.Out(":");
			return node;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000474C4 File Offset: 0x000456C4
		protected internal override Expression VisitGoto(GotoExpression node)
		{
			this.Out(node.Kind.ToString().ToLower(CultureInfo.CurrentCulture));
			this.DumpLabel(node.Target);
			if (node.Value != null)
			{
				this.Out(" (");
				this.Visit(node.Value);
				this.Out(") ");
			}
			return node;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004752D File Offset: 0x0004572D
		protected internal override Expression VisitLoop(LoopExpression node)
		{
			this.Out("loop { ... }");
			return node;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004753B File Offset: 0x0004573B
		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			this.Out("case ");
			this.VisitExpressions<Expression>('(', node.TestValues, ')');
			this.Out(": ...");
			return node;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00047564 File Offset: 0x00045764
		protected internal override Expression VisitSwitch(SwitchExpression node)
		{
			this.Out("switch ");
			this.Out("(");
			this.Visit(node.SwitchValue);
			this.Out(") { ... }");
			return node;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00047598 File Offset: 0x00045798
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			this.Out("catch (" + node.Test.Name);
			if (node.Variable != null)
			{
				this.Out(node.Variable.Name ?? "");
			}
			this.Out(") { ... }");
			return node;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x000475EE File Offset: 0x000457EE
		protected internal override Expression VisitTry(TryExpression node)
		{
			this.Out("try { ... }");
			return node;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000475FC File Offset: 0x000457FC
		protected internal override Expression VisitIndex(IndexExpression node)
		{
			if (node.Object != null)
			{
				this.Visit(node.Object);
			}
			else
			{
				this.Out(node.Indexer.DeclaringType.Name);
			}
			if (node.Indexer != null)
			{
				this.Out(".");
				this.Out(node.Indexer.Name);
			}
			this.VisitExpressions<Expression>('[', node.Arguments, ']');
			return node;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00047674 File Offset: 0x00045874
		protected internal override Expression VisitExtension(Expression node)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding;
			MethodInfo method = node.GetType().GetMethod("ToString", bindingAttr, null, Type.EmptyTypes, null);
			if (method.DeclaringType != typeof(Expression))
			{
				this.Out(node.ToString());
				return node;
			}
			this.Out("[");
			if (node.NodeType == ExpressionType.Extension)
			{
				this.Out(node.GetType().FullName);
			}
			else
			{
				this.Out(node.NodeType.ToString());
			}
			this.Out("]");
			return node;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00047714 File Offset: 0x00045914
		private void DumpLabel(LabelTarget target)
		{
			if (!string.IsNullOrEmpty(target.Name))
			{
				this.Out(target.Name);
				return;
			}
			this.Out("UnamedLabel_" + this.GetLabelId(target).ToString());
		}

		// Token: 0x040009A6 RID: 2470
		private StringBuilder _out;

		// Token: 0x040009A7 RID: 2471
		private Dictionary<object, int> _ids;
	}
}
