using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200022B RID: 555
	internal sealed class DebugViewWriter : ExpressionVisitor
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x000440CF File Offset: 0x000422CF
		private DebugViewWriter(TextWriter file)
		{
			this._out = file;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000440E9 File Offset: 0x000422E9
		private int Base
		{
			get
			{
				if (this._stack.Count <= 0)
				{
					return 0;
				}
				return this._stack.Peek();
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00044106 File Offset: 0x00042306
		private int Delta
		{
			get
			{
				return this._delta;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0004410E File Offset: 0x0004230E
		private int Depth
		{
			get
			{
				return this.Base + this.Delta;
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004411D File Offset: 0x0004231D
		private void Indent()
		{
			this._delta += 4;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004412D File Offset: 0x0004232D
		private void Dedent()
		{
			this._delta -= 4;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004413D File Offset: 0x0004233D
		private void NewLine()
		{
			this._flow = DebugViewWriter.Flow.NewLine;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00044148 File Offset: 0x00042348
		private static int GetId<T>(T e, ref Dictionary<T, int> ids)
		{
			if (ids == null)
			{
				ids = new Dictionary<T, int>();
				ids.Add(e, 1);
				return 1;
			}
			int num;
			if (!ids.TryGetValue(e, out num))
			{
				num = ids.Count + 1;
				ids.Add(e, num);
			}
			return num;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004418B File Offset: 0x0004238B
		private int GetLambdaId(LambdaExpression le)
		{
			return DebugViewWriter.GetId<LambdaExpression>(le, ref this._lambdaIds);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00044199 File Offset: 0x00042399
		private int GetParamId(ParameterExpression p)
		{
			return DebugViewWriter.GetId<ParameterExpression>(p, ref this._paramIds);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x000441A7 File Offset: 0x000423A7
		private int GetLabelTargetId(LabelTarget target)
		{
			return DebugViewWriter.GetId<LabelTarget>(target, ref this._labelIds);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x000441B5 File Offset: 0x000423B5
		internal static void WriteTo(Expression node, TextWriter writer)
		{
			new DebugViewWriter(writer).WriteTo(node);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000441C4 File Offset: 0x000423C4
		private void WriteTo(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			if (lambdaExpression != null)
			{
				this.WriteLambda(lambdaExpression);
			}
			else
			{
				this.Visit(node);
			}
			while (this._lambdas != null && this._lambdas.Count > 0)
			{
				this.WriteLine();
				this.WriteLine();
				this.WriteLambda(this._lambdas.Dequeue());
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00044221 File Offset: 0x00042421
		private void Out(string s)
		{
			this.Out(DebugViewWriter.Flow.None, s, DebugViewWriter.Flow.None);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004422C File Offset: 0x0004242C
		private void Out(DebugViewWriter.Flow before, string s)
		{
			this.Out(before, s, DebugViewWriter.Flow.None);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00044237 File Offset: 0x00042437
		private void Out(string s, DebugViewWriter.Flow after)
		{
			this.Out(DebugViewWriter.Flow.None, s, after);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00044244 File Offset: 0x00042444
		private void Out(DebugViewWriter.Flow before, string s, DebugViewWriter.Flow after)
		{
			switch (this.GetFlow(before))
			{
			case DebugViewWriter.Flow.Space:
				this.Write(" ");
				break;
			case DebugViewWriter.Flow.NewLine:
				this.WriteLine();
				this.Write(new string(' ', this.Depth));
				break;
			}
			this.Write(s);
			this._flow = after;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000442A1 File Offset: 0x000424A1
		private void WriteLine()
		{
			this._out.WriteLine();
			this._column = 0;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000442B5 File Offset: 0x000424B5
		private void Write(string s)
		{
			this._out.Write(s);
			this._column += s.Length;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000442D8 File Offset: 0x000424D8
		private DebugViewWriter.Flow GetFlow(DebugViewWriter.Flow flow)
		{
			DebugViewWriter.Flow val = this.CheckBreak(this._flow);
			flow = this.CheckBreak(flow);
			return (DebugViewWriter.Flow)Math.Max((int)val, (int)flow);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00044302 File Offset: 0x00042502
		private DebugViewWriter.Flow CheckBreak(DebugViewWriter.Flow flow)
		{
			if ((flow & DebugViewWriter.Flow.Break) != DebugViewWriter.Flow.None)
			{
				if (this._column > 120 + this.Depth)
				{
					flow = DebugViewWriter.Flow.NewLine;
				}
				else
				{
					flow &= ~DebugViewWriter.Flow.Break;
				}
			}
			return flow;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00044330 File Offset: 0x00042530
		private static string FormatBinder(CallSiteBinder binder)
		{
			ConvertBinder convertBinder;
			if ((convertBinder = (binder as ConvertBinder)) != null)
			{
				return "Convert " + convertBinder.Type.ToString();
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
				return "UnaryOperation " + unaryOperationBinder.Operation.ToString();
			}
			BinaryOperationBinder binaryOperationBinder;
			if ((binaryOperationBinder = (binder as BinaryOperationBinder)) != null)
			{
				return "BinaryOperation " + binaryOperationBinder.Operation.ToString();
			}
			return binder.ToString();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004446F File Offset: 0x0004266F
		private void VisitExpressions<T>(char open, IList<T> expressions) where T : Expression
		{
			this.VisitExpressions<T>(open, ',', expressions);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004447B File Offset: 0x0004267B
		private void VisitExpressions<T>(char open, char separator, IList<T> expressions) where T : Expression
		{
			this.VisitExpressions<T>(open, separator, expressions, delegate(T e)
			{
				this.Visit(e);
			});
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00044492 File Offset: 0x00042692
		private void VisitDeclarations(IList<ParameterExpression> expressions)
		{
			this.VisitExpressions<ParameterExpression>('(', ',', expressions, delegate(ParameterExpression variable)
			{
				this.Out(variable.Type.ToString());
				if (variable.IsByRef)
				{
					this.Out("&");
				}
				this.Out(" ");
				this.VisitParameter(variable);
			});
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000444AC File Offset: 0x000426AC
		private void VisitExpressions<T>(char open, char separator, IList<T> expressions, Action<T> visit)
		{
			this.Out(open.ToString());
			if (expressions != null)
			{
				this.Indent();
				bool flag = true;
				foreach (T obj in expressions)
				{
					if (flag)
					{
						if (open == '{' || expressions.Count > 1)
						{
							this.NewLine();
						}
						flag = false;
					}
					else
					{
						this.Out(separator.ToString(), DebugViewWriter.Flow.NewLine);
					}
					visit(obj);
				}
				this.Dedent();
			}
			char c;
			if (open <= '<')
			{
				if (open == '(')
				{
					c = ')';
					goto IL_AA;
				}
				if (open == '<')
				{
					c = '>';
					goto IL_AA;
				}
			}
			else
			{
				if (open == '[')
				{
					c = ']';
					goto IL_AA;
				}
				if (open == '{')
				{
					c = '}';
					goto IL_AA;
				}
			}
			throw ContractUtils.Unreachable;
			IL_AA:
			if (open == '{')
			{
				this.NewLine();
			}
			this.Out(c.ToString(), DebugViewWriter.Flow.Break);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00044590 File Offset: 0x00042790
		protected internal override Expression VisitDynamic(DynamicExpression node)
		{
			this.Out(".Dynamic", DebugViewWriter.Flow.Space);
			this.Out(DebugViewWriter.FormatBinder(node.Binder));
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000445C0 File Offset: 0x000427C0
		protected internal override Expression VisitBinary(BinaryExpression node)
		{
			if (node.NodeType == ExpressionType.ArrayIndex)
			{
				this.ParenthesizedVisit(node, node.Left);
				this.Out("[");
				this.Visit(node.Right);
				this.Out("]");
			}
			else
			{
				bool flag = DebugViewWriter.NeedsParentheses(node, node.Left);
				bool flag2 = DebugViewWriter.NeedsParentheses(node, node.Right);
				bool flag3 = false;
				DebugViewWriter.Flow before = DebugViewWriter.Flow.Space;
				ExpressionType nodeType = node.NodeType;
				string text;
				switch (nodeType)
				{
				case ExpressionType.Add:
					text = "+";
					goto IL_304;
				case ExpressionType.AddChecked:
					text = "+";
					flag3 = true;
					goto IL_304;
				case ExpressionType.And:
					text = "&";
					goto IL_304;
				case ExpressionType.AndAlso:
					text = "&&";
					before = (DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
					goto IL_304;
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
					text = "??";
					goto IL_304;
				case ExpressionType.Divide:
					text = "/";
					goto IL_304;
				case ExpressionType.Equal:
					text = "==";
					goto IL_304;
				case ExpressionType.ExclusiveOr:
					text = "^";
					goto IL_304;
				case ExpressionType.GreaterThan:
					text = ">";
					goto IL_304;
				case ExpressionType.GreaterThanOrEqual:
					text = ">=";
					goto IL_304;
				case ExpressionType.LeftShift:
					text = "<<";
					goto IL_304;
				case ExpressionType.LessThan:
					text = "<";
					goto IL_304;
				case ExpressionType.LessThanOrEqual:
					text = "<=";
					goto IL_304;
				case ExpressionType.Modulo:
					text = "%";
					goto IL_304;
				case ExpressionType.Multiply:
					text = "*";
					goto IL_304;
				case ExpressionType.MultiplyChecked:
					text = "*";
					flag3 = true;
					goto IL_304;
				case ExpressionType.NotEqual:
					text = "!=";
					goto IL_304;
				case ExpressionType.Or:
					text = "|";
					goto IL_304;
				case ExpressionType.OrElse:
					text = "||";
					before = (DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
					goto IL_304;
				case ExpressionType.Power:
					text = "**";
					goto IL_304;
				case ExpressionType.RightShift:
					text = ">>";
					goto IL_304;
				case ExpressionType.Subtract:
					text = "-";
					goto IL_304;
				case ExpressionType.SubtractChecked:
					text = "-";
					flag3 = true;
					goto IL_304;
				case ExpressionType.Assign:
					text = "=";
					goto IL_304;
				default:
					switch (nodeType)
					{
					case ExpressionType.AddAssign:
						text = "+=";
						goto IL_304;
					case ExpressionType.AndAssign:
						text = "&=";
						goto IL_304;
					case ExpressionType.DivideAssign:
						text = "/=";
						goto IL_304;
					case ExpressionType.ExclusiveOrAssign:
						text = "^=";
						goto IL_304;
					case ExpressionType.LeftShiftAssign:
						text = "<<=";
						goto IL_304;
					case ExpressionType.ModuloAssign:
						text = "%=";
						goto IL_304;
					case ExpressionType.MultiplyAssign:
						text = "*=";
						goto IL_304;
					case ExpressionType.OrAssign:
						text = "|=";
						goto IL_304;
					case ExpressionType.PowerAssign:
						text = "**=";
						goto IL_304;
					case ExpressionType.RightShiftAssign:
						text = ">>=";
						goto IL_304;
					case ExpressionType.SubtractAssign:
						text = "-=";
						goto IL_304;
					case ExpressionType.AddAssignChecked:
						text = "+=";
						flag3 = true;
						goto IL_304;
					case ExpressionType.MultiplyAssignChecked:
						text = "*=";
						flag3 = true;
						goto IL_304;
					case ExpressionType.SubtractAssignChecked:
						text = "-=";
						flag3 = true;
						goto IL_304;
					}
					break;
				}
				throw new InvalidOperationException();
				IL_304:
				if (flag)
				{
					this.Out("(", DebugViewWriter.Flow.None);
				}
				this.Visit(node.Left);
				if (flag)
				{
					this.Out(DebugViewWriter.Flow.None, ")", DebugViewWriter.Flow.Break);
				}
				if (flag3)
				{
					text = string.Format(CultureInfo.CurrentCulture, "#{0}", new object[]
					{
						text
					});
				}
				this.Out(before, text, DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
				if (flag2)
				{
					this.Out("(", DebugViewWriter.Flow.None);
				}
				this.Visit(node.Right);
				if (flag2)
				{
					this.Out(DebugViewWriter.Flow.None, ")", DebugViewWriter.Flow.Break);
				}
			}
			return node;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00044960 File Offset: 0x00042B60
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Out("$");
			if (string.IsNullOrEmpty(node.Name))
			{
				this.Out("var" + this.GetParamId(node).ToString());
			}
			else
			{
				this.Out(DebugViewWriter.GetDisplayName(node.Name));
			}
			return node;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000449B8 File Offset: 0x00042BB8
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, "{0} {1}<{2}>", new object[]
			{
				".Lambda",
				this.GetLambdaName(node),
				node.Type.ToString()
			}));
			if (this._lambdas == null)
			{
				this._lambdas = new Queue<LambdaExpression>();
			}
			if (!this._lambdas.Contains(node))
			{
				this._lambdas.Enqueue(node);
			}
			return node;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00044A30 File Offset: 0x00042C30
		private static bool IsSimpleExpression(Expression node)
		{
			BinaryExpression binaryExpression = node as BinaryExpression;
			return binaryExpression != null && !(binaryExpression.Left is BinaryExpression) && !(binaryExpression.Right is BinaryExpression);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00044A6C File Offset: 0x00042C6C
		protected internal override Expression VisitConditional(ConditionalExpression node)
		{
			if (DebugViewWriter.IsSimpleExpression(node.Test))
			{
				this.Out(".If (");
				this.Visit(node.Test);
				this.Out(") {", DebugViewWriter.Flow.NewLine);
			}
			else
			{
				this.Out(".If (", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Test);
				this.Dedent();
				this.Out(DebugViewWriter.Flow.NewLine, ") {", DebugViewWriter.Flow.NewLine);
			}
			this.Indent();
			this.Visit(node.IfTrue);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "} .Else {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.IfFalse);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			return node;
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00044B2C File Offset: 0x00042D2C
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			object value = node.Value;
			if (value == null)
			{
				this.Out("null");
			}
			else if (value is string && node.Type == typeof(string))
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "\"{0}\"", new object[]
				{
					value
				}));
			}
			else if (value is char && node.Type == typeof(char))
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "'{0}'", new object[]
				{
					value
				}));
			}
			else if ((value is int && node.Type == typeof(int)) || (value is bool && node.Type == typeof(bool)))
			{
				this.Out(value.ToString());
			}
			else
			{
				string constantValueSuffix = DebugViewWriter.GetConstantValueSuffix(node.Type);
				if (constantValueSuffix != null)
				{
					this.Out(value.ToString());
					this.Out(constantValueSuffix);
				}
				else
				{
					this.Out(string.Format(CultureInfo.CurrentCulture, ".Constant<{0}>({1})", new object[]
					{
						node.Type.ToString(),
						value
					}));
				}
			}
			return node;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00044C78 File Offset: 0x00042E78
		private static string GetConstantValueSuffix(Type type)
		{
			if (type == typeof(uint))
			{
				return "U";
			}
			if (type == typeof(long))
			{
				return "L";
			}
			if (type == typeof(ulong))
			{
				return "UL";
			}
			if (type == typeof(double))
			{
				return "D";
			}
			if (type == typeof(float))
			{
				return "F";
			}
			if (type == typeof(decimal))
			{
				return "M";
			}
			return null;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00044D16 File Offset: 0x00042F16
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			this.Out(".RuntimeVariables");
			this.VisitExpressions<ParameterExpression>('(', node.Variables);
			return node;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00044D34 File Offset: 0x00042F34
		private void OutMember(Expression node, Expression instance, MemberInfo member)
		{
			if (instance != null)
			{
				this.ParenthesizedVisit(node, instance);
				this.Out("." + member.Name);
				return;
			}
			this.Out(member.DeclaringType.ToString() + "." + member.Name);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00044D84 File Offset: 0x00042F84
		protected internal override Expression VisitMember(MemberExpression node)
		{
			this.OutMember(node, node.Expression, node.Member);
			return node;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00044D9A File Offset: 0x00042F9A
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			this.Out(".Invoke ");
			this.ParenthesizedVisit(node, node.Expression);
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x00044DC4 File Offset: 0x00042FC4
		private static bool NeedsParentheses(Expression parent, Expression child)
		{
			if (child == null)
			{
				return false;
			}
			ExpressionType nodeType = parent.NodeType;
			if (nodeType <= ExpressionType.Increment)
			{
				if (nodeType != ExpressionType.Decrement && nodeType != ExpressionType.Increment)
				{
					goto IL_2B;
				}
			}
			else if (nodeType != ExpressionType.Unbox && nodeType - ExpressionType.IsTrue > 1)
			{
				goto IL_2B;
			}
			return true;
			IL_2B:
			int operatorPrecedence = DebugViewWriter.GetOperatorPrecedence(child);
			int operatorPrecedence2 = DebugViewWriter.GetOperatorPrecedence(parent);
			if (operatorPrecedence == operatorPrecedence2)
			{
				ExpressionType nodeType2 = parent.NodeType;
				if (nodeType2 <= ExpressionType.ExclusiveOr)
				{
					if (nodeType2 <= ExpressionType.AndAlso)
					{
						if (nodeType2 <= ExpressionType.AddChecked)
						{
							return false;
						}
						if (nodeType2 - ExpressionType.And > 1)
						{
							return true;
						}
					}
					else
					{
						if (nodeType2 == ExpressionType.Divide)
						{
							goto IL_98;
						}
						if (nodeType2 != ExpressionType.ExclusiveOr)
						{
							return true;
						}
					}
				}
				else if (nodeType2 <= ExpressionType.MultiplyChecked)
				{
					if (nodeType2 == ExpressionType.Modulo)
					{
						goto IL_98;
					}
					if (nodeType2 - ExpressionType.Multiply > 1)
					{
						return true;
					}
					return false;
				}
				else if (nodeType2 - ExpressionType.Or > 1)
				{
					if (nodeType2 - ExpressionType.Subtract > 1)
					{
						return true;
					}
					goto IL_98;
				}
				return false;
				IL_98:
				BinaryExpression binaryExpression = parent as BinaryExpression;
				return child == binaryExpression.Right;
			}
			return (child != null && child.NodeType == ExpressionType.Constant && (parent.NodeType == ExpressionType.Negate || parent.NodeType == ExpressionType.NegateChecked)) || operatorPrecedence < operatorPrecedence2;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00044EA4 File Offset: 0x000430A4
		private static int GetOperatorPrecedence(Expression node)
		{
			switch (node.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return 10;
			case ExpressionType.And:
				return 6;
			case ExpressionType.AndAlso:
				return 3;
			case ExpressionType.Coalesce:
			case ExpressionType.Assign:
			case ExpressionType.AddAssign:
			case ExpressionType.AndAssign:
			case ExpressionType.DivideAssign:
			case ExpressionType.ExclusiveOrAssign:
			case ExpressionType.LeftShiftAssign:
			case ExpressionType.ModuloAssign:
			case ExpressionType.MultiplyAssign:
			case ExpressionType.OrAssign:
			case ExpressionType.PowerAssign:
			case ExpressionType.RightShiftAssign:
			case ExpressionType.SubtractAssign:
			case ExpressionType.AddAssignChecked:
			case ExpressionType.MultiplyAssignChecked:
			case ExpressionType.SubtractAssignChecked:
				return 1;
			case ExpressionType.Constant:
			case ExpressionType.Parameter:
				return 15;
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.Decrement:
			case ExpressionType.Increment:
			case ExpressionType.Throw:
			case ExpressionType.Unbox:
			case ExpressionType.PreIncrementAssign:
			case ExpressionType.PreDecrementAssign:
			case ExpressionType.OnesComplement:
			case ExpressionType.IsTrue:
			case ExpressionType.IsFalse:
				return 12;
			case ExpressionType.Divide:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				return 11;
			case ExpressionType.Equal:
			case ExpressionType.NotEqual:
				return 7;
			case ExpressionType.ExclusiveOr:
				return 5;
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.TypeAs:
			case ExpressionType.TypeIs:
			case ExpressionType.TypeEqual:
				return 8;
			case ExpressionType.LeftShift:
			case ExpressionType.RightShift:
				return 9;
			case ExpressionType.Or:
				return 4;
			case ExpressionType.OrElse:
				return 2;
			case ExpressionType.Power:
				return 13;
			}
			return 14;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00045038 File Offset: 0x00043238
		private void ParenthesizedVisit(Expression parent, Expression nodeToVisit)
		{
			if (DebugViewWriter.NeedsParentheses(parent, nodeToVisit))
			{
				this.Out("(");
				this.Visit(nodeToVisit);
				this.Out(")");
				return;
			}
			this.Visit(nodeToVisit);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004506C File Offset: 0x0004326C
		protected internal override Expression VisitMethodCall(MethodCallExpression node)
		{
			this.Out(".Call ");
			if (node.Object != null)
			{
				this.ParenthesizedVisit(node, node.Object);
			}
			else if (node.Method.DeclaringType != null)
			{
				this.Out(node.Method.DeclaringType.ToString());
			}
			else
			{
				this.Out("<UnknownType>");
			}
			this.Out(".");
			this.Out(node.Method.Name);
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000450FC File Offset: 0x000432FC
		protected internal override Expression VisitNewArray(NewArrayExpression node)
		{
			if (node.NodeType == ExpressionType.NewArrayBounds)
			{
				this.Out(".NewArray " + node.Type.GetElementType().ToString());
				this.VisitExpressions<Expression>('[', node.Expressions);
			}
			else
			{
				this.Out(".NewArray " + node.Type.ToString(), DebugViewWriter.Flow.Space);
				this.VisitExpressions<Expression>('{', node.Expressions);
			}
			return node;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004516E File Offset: 0x0004336E
		protected internal override Expression VisitNew(NewExpression node)
		{
			this.Out(".New " + node.Type.ToString());
			this.VisitExpressions<Expression>('(', node.Arguments);
			return node;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004519A File Offset: 0x0004339A
		protected override ElementInit VisitElementInit(ElementInit node)
		{
			if (node.Arguments.Count == 1)
			{
				this.Visit(node.Arguments[0]);
			}
			else
			{
				this.VisitExpressions<Expression>('{', node.Arguments);
			}
			return node;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000451CE File Offset: 0x000433CE
		protected internal override Expression VisitListInit(ListInitExpression node)
		{
			this.Visit(node.NewExpression);
			this.VisitExpressions<ElementInit>('{', ',', node.Initializers, delegate(ElementInit e)
			{
				this.VisitElementInit(e);
			});
			return node;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x000451FA File Offset: 0x000433FA
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Out(assignment.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.Visit(assignment.Expression);
			return assignment;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00045228 File Offset: 0x00043428
		protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.VisitExpressions<ElementInit>('{', ',', binding.Initializers, delegate(ElementInit e)
			{
				this.VisitElementInit(e);
			});
			return binding;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00045265 File Offset: 0x00043465
		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.Out(binding.Member.Name);
			this.Out(DebugViewWriter.Flow.Space, "=", DebugViewWriter.Flow.Space);
			this.VisitExpressions<MemberBinding>('{', ',', binding.Bindings, delegate(MemberBinding e)
			{
				this.VisitMemberBinding(e);
			});
			return binding;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000452A2 File Offset: 0x000434A2
		protected internal override Expression VisitMemberInit(MemberInitExpression node)
		{
			this.Visit(node.NewExpression);
			this.VisitExpressions<MemberBinding>('{', ',', node.Bindings, delegate(MemberBinding e)
			{
				this.VisitMemberBinding(e);
			});
			return node;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000452D0 File Offset: 0x000434D0
		protected internal override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			this.ParenthesizedVisit(node, node.Expression);
			ExpressionType nodeType = node.NodeType;
			if (nodeType != ExpressionType.TypeIs)
			{
				if (nodeType == ExpressionType.TypeEqual)
				{
					this.Out(DebugViewWriter.Flow.Space, ".TypeEqual", DebugViewWriter.Flow.Space);
				}
			}
			else
			{
				this.Out(DebugViewWriter.Flow.Space, ".Is", DebugViewWriter.Flow.Space);
			}
			this.Out(node.TypeOperand.ToString());
			return node;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004532C File Offset: 0x0004352C
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Quote)
			{
				if (nodeType <= ExpressionType.Convert)
				{
					if (nodeType != ExpressionType.ArrayLength)
					{
						if (nodeType == ExpressionType.Convert)
						{
							this.Out("(" + node.Type.ToString() + ")");
						}
					}
				}
				else if (nodeType != ExpressionType.ConvertChecked)
				{
					switch (nodeType)
					{
					case ExpressionType.Negate:
						this.Out("-");
						break;
					case ExpressionType.UnaryPlus:
						this.Out("+");
						break;
					case ExpressionType.NegateChecked:
						this.Out("#-");
						break;
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						this.Out((node.Type == typeof(bool)) ? "!" : "~");
						break;
					default:
						if (nodeType == ExpressionType.Quote)
						{
							this.Out("'");
						}
						break;
					}
				}
				else
				{
					this.Out("#(" + node.Type.ToString() + ")");
				}
			}
			else if (nodeType <= ExpressionType.Increment)
			{
				if (nodeType != ExpressionType.TypeAs)
				{
					if (nodeType != ExpressionType.Decrement)
					{
						if (nodeType == ExpressionType.Increment)
						{
							this.Out(".Increment");
						}
					}
					else
					{
						this.Out(".Decrement");
					}
				}
			}
			else if (nodeType != ExpressionType.Throw)
			{
				if (nodeType != ExpressionType.Unbox)
				{
					switch (nodeType)
					{
					case ExpressionType.PreIncrementAssign:
						this.Out("++");
						break;
					case ExpressionType.PreDecrementAssign:
						this.Out("--");
						break;
					case ExpressionType.OnesComplement:
						this.Out("~");
						break;
					case ExpressionType.IsTrue:
						this.Out(".IsTrue");
						break;
					case ExpressionType.IsFalse:
						this.Out(".IsFalse");
						break;
					}
				}
				else
				{
					this.Out(".Unbox");
				}
			}
			else if (node.Operand == null)
			{
				this.Out(".Rethrow");
			}
			else
			{
				this.Out(".Throw", DebugViewWriter.Flow.Space);
			}
			this.ParenthesizedVisit(node, node.Operand);
			ExpressionType nodeType2 = node.NodeType;
			if (nodeType2 <= ExpressionType.TypeAs)
			{
				if (nodeType2 != ExpressionType.ArrayLength)
				{
					if (nodeType2 == ExpressionType.TypeAs)
					{
						this.Out(DebugViewWriter.Flow.Space, ".As", DebugViewWriter.Flow.Space | DebugViewWriter.Flow.Break);
						this.Out(node.Type.ToString());
					}
				}
				else
				{
					this.Out(".Length");
				}
			}
			else if (nodeType2 != ExpressionType.PostIncrementAssign)
			{
				if (nodeType2 == ExpressionType.PostDecrementAssign)
				{
					this.Out("--");
				}
			}
			else
			{
				this.Out("++");
			}
			return node;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000455B4 File Offset: 0x000437B4
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			this.Out(".Block");
			if (node.Type != node.GetExpression(node.ExpressionCount - 1).Type)
			{
				this.Out(string.Format(CultureInfo.CurrentCulture, "<{0}>", new object[]
				{
					node.Type.ToString()
				}));
			}
			this.VisitDeclarations(node.Variables);
			this.Out(" ");
			this.VisitExpressions<Expression>('{', ';', node.Expressions);
			return node;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004563D File Offset: 0x0004383D
		protected internal override Expression VisitDefault(DefaultExpression node)
		{
			this.Out(".Default(" + node.Type.ToString() + ")");
			return node;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00045660 File Offset: 0x00043860
		protected internal override Expression VisitLabel(LabelExpression node)
		{
			this.Out(".Label", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.DefaultValue);
			this.Dedent();
			this.NewLine();
			this.DumpLabel(node.Target);
			return node;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004569C File Offset: 0x0004389C
		protected internal override Expression VisitGoto(GotoExpression node)
		{
			this.Out("." + node.Kind.ToString(), DebugViewWriter.Flow.Space);
			this.Out(this.GetLabelTargetName(node.Target), DebugViewWriter.Flow.Space);
			this.Out("{", DebugViewWriter.Flow.Space);
			this.Visit(node.Value);
			this.Out(DebugViewWriter.Flow.Space, "}");
			return node;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00045708 File Offset: 0x00043908
		protected internal override Expression VisitLoop(LoopExpression node)
		{
			this.Out(".Loop", DebugViewWriter.Flow.Space);
			if (node.ContinueLabel != null)
			{
				this.DumpLabel(node.ContinueLabel);
			}
			this.Out(" {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			if (node.BreakLabel != null)
			{
				this.Out("", DebugViewWriter.Flow.NewLine);
				this.DumpLabel(node.BreakLabel);
			}
			return node;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00045788 File Offset: 0x00043988
		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			foreach (Expression node2 in node.TestValues)
			{
				this.Out(".Case (");
				this.Visit(node2);
				this.Out("):", DebugViewWriter.Flow.NewLine);
			}
			this.Indent();
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			this.Dedent();
			this.NewLine();
			return node;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004581C File Offset: 0x00043A1C
		protected internal override Expression VisitSwitch(SwitchExpression node)
		{
			this.Out(".Switch ");
			this.Out("(");
			this.Visit(node.SwitchValue);
			this.Out(") {", DebugViewWriter.Flow.NewLine);
			ExpressionVisitor.Visit<SwitchCase>(node.Cases, new Func<SwitchCase, SwitchCase>(this.VisitSwitchCase));
			if (node.DefaultBody != null)
			{
				this.Out(".Default:", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Indent();
				this.Visit(node.DefaultBody);
				this.Dedent();
				this.Dedent();
				this.NewLine();
			}
			this.Out("}");
			return node;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x000458BC File Offset: 0x00043ABC
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			this.Out(DebugViewWriter.Flow.NewLine, "} .Catch (" + node.Test.ToString());
			if (node.Variable != null)
			{
				this.Out(DebugViewWriter.Flow.Space, "");
				this.VisitParameter(node.Variable);
			}
			if (node.Filter != null)
			{
				this.Out(") .If (", DebugViewWriter.Flow.Break);
				this.Visit(node.Filter);
			}
			this.Out(") {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			return node;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00045954 File Offset: 0x00043B54
		protected internal override Expression VisitTry(TryExpression node)
		{
			this.Out(".Try {", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(node.Body);
			this.Dedent();
			ExpressionVisitor.Visit<CatchBlock>(node.Handlers, new Func<CatchBlock, CatchBlock>(this.VisitCatchBlock));
			if (node.Finally != null)
			{
				this.Out(DebugViewWriter.Flow.NewLine, "} .Finally {", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Finally);
				this.Dedent();
			}
			else if (node.Fault != null)
			{
				this.Out(DebugViewWriter.Flow.NewLine, "} .Fault {", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Fault);
				this.Dedent();
			}
			this.Out(DebugViewWriter.Flow.NewLine, "}");
			return node;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00045A0C File Offset: 0x00043C0C
		protected internal override Expression VisitIndex(IndexExpression node)
		{
			if (node.Indexer != null)
			{
				this.OutMember(node, node.Object, node.Indexer);
			}
			else
			{
				this.ParenthesizedVisit(node, node.Object);
			}
			this.VisitExpressions<Expression>('[', node.Arguments);
			return node;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00045A58 File Offset: 0x00043C58
		protected internal override Expression VisitExtension(Expression node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".Extension<{0}>", new object[]
			{
				node.GetType().ToString()
			}));
			if (node.CanReduce)
			{
				this.Out(DebugViewWriter.Flow.Space, "{", DebugViewWriter.Flow.NewLine);
				this.Indent();
				this.Visit(node.Reduce());
				this.Dedent();
				this.Out(DebugViewWriter.Flow.NewLine, "}");
			}
			return node;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00045ACC File Offset: 0x00043CCC
		protected internal override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".DebugInfo({0}: {1}, {2} - {3}, {4})", new object[]
			{
				node.Document.FileName,
				node.StartLine,
				node.StartColumn,
				node.EndLine,
				node.EndColumn
			}));
			return node;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00045B3B File Offset: 0x00043D3B
		private void DumpLabel(LabelTarget target)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".LabelTarget {0}:", new object[]
			{
				this.GetLabelTargetName(target)
			}));
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00045B62 File Offset: 0x00043D62
		private string GetLabelTargetName(LabelTarget target)
		{
			if (string.IsNullOrEmpty(target.Name))
			{
				return string.Format(CultureInfo.CurrentCulture, "#Label{0}", new object[]
				{
					this.GetLabelTargetId(target)
				});
			}
			return DebugViewWriter.GetDisplayName(target.Name);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00045BA4 File Offset: 0x00043DA4
		private void WriteLambda(LambdaExpression lambda)
		{
			this.Out(string.Format(CultureInfo.CurrentCulture, ".Lambda {0}<{1}>", new object[]
			{
				this.GetLambdaName(lambda),
				lambda.Type.ToString()
			}));
			this.VisitDeclarations(lambda.Parameters);
			this.Out(DebugViewWriter.Flow.Space, "{", DebugViewWriter.Flow.NewLine);
			this.Indent();
			this.Visit(lambda.Body);
			this.Dedent();
			this.Out(DebugViewWriter.Flow.NewLine, "}");
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00045C24 File Offset: 0x00043E24
		private string GetLambdaName(LambdaExpression lambda)
		{
			if (string.IsNullOrEmpty(lambda.Name))
			{
				return "#Lambda" + this.GetLambdaId(lambda).ToString();
			}
			return DebugViewWriter.GetDisplayName(lambda.Name);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00045C64 File Offset: 0x00043E64
		private static bool ContainsWhiteSpace(string name)
		{
			foreach (char c in name)
			{
				if (char.IsWhiteSpace(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00045C97 File Offset: 0x00043E97
		private static string QuoteName(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, "'{0}'", new object[]
			{
				name
			});
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00045CB2 File Offset: 0x00043EB2
		private static string GetDisplayName(string name)
		{
			if (DebugViewWriter.ContainsWhiteSpace(name))
			{
				return DebugViewWriter.QuoteName(name);
			}
			return name;
		}

		// Token: 0x04000986 RID: 2438
		private const int Tab = 4;

		// Token: 0x04000987 RID: 2439
		private const int MaxColumn = 120;

		// Token: 0x04000988 RID: 2440
		private TextWriter _out;

		// Token: 0x04000989 RID: 2441
		private int _column;

		// Token: 0x0400098A RID: 2442
		private Stack<int> _stack = new Stack<int>();

		// Token: 0x0400098B RID: 2443
		private int _delta;

		// Token: 0x0400098C RID: 2444
		private DebugViewWriter.Flow _flow;

		// Token: 0x0400098D RID: 2445
		private Queue<LambdaExpression> _lambdas;

		// Token: 0x0400098E RID: 2446
		private Dictionary<LambdaExpression, int> _lambdaIds;

		// Token: 0x0400098F RID: 2447
		private Dictionary<ParameterExpression, int> _paramIds;

		// Token: 0x04000990 RID: 2448
		private Dictionary<LabelTarget, int> _labelIds;

		// Token: 0x02000442 RID: 1090
		[Flags]
		private enum Flow
		{
			// Token: 0x040012B4 RID: 4788
			None = 0,
			// Token: 0x040012B5 RID: 4789
			Space = 1,
			// Token: 0x040012B6 RID: 4790
			NewLine = 2,
			// Token: 0x040012B7 RID: 4791
			Break = 32768
		}
	}
}
