using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x0200012B RID: 299
	internal class ExpressionPrinter : TreePrinter
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00031FFC File Offset: 0x000301FC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbDeleteCommandTree")]
		internal string Print(DbDeleteCommandTree tree)
		{
			TreeNode treeNode;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2;
			if (tree.Predicate != null)
			{
				treeNode2 = this._visitor.VisitExpression("Predicate", tree.Predicate);
			}
			else
			{
				treeNode2 = new TreeNode("Predicate", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbDeleteCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2
			}));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00032098 File Offset: 0x00030298
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ResultType")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbFunctionCommandTree")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EdmFunction")]
		internal string Print(DbFunctionCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("EdmFunction", new TreeNode[0]);
			if (tree.EdmFunction != null)
			{
				treeNode.Children.Add(this._visitor.VisitFunction(tree.EdmFunction, null));
			}
			TreeNode treeNode2 = new TreeNode("ResultType", new TreeNode[0]);
			if (tree.ResultType != null)
			{
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, tree.ResultType);
			}
			return this.Print(new TreeNode("DbFunctionCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2
			}));
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003212C File Offset: 0x0003032C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbInsertCommandTree")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetClauses")]
		internal string Print(DbInsertCommandTree tree)
		{
			TreeNode treeNode = null;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2 = new TreeNode("SetClauses", new TreeNode[0]);
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				if (dbModificationClause != null)
				{
					treeNode2.Children.Add(dbModificationClause.Print(this._visitor));
				}
			}
			TreeNode treeNode3;
			if (tree.Returning != null)
			{
				treeNode3 = new TreeNode("Returning", new TreeNode[]
				{
					this._visitor.VisitExpression(tree.Returning)
				});
			}
			else
			{
				treeNode3 = new TreeNode("Returning", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbInsertCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2,
				treeNode3
			}));
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00032250 File Offset: 0x00030450
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbUpdateCommandTree")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetClauses")]
		internal string Print(DbUpdateCommandTree tree)
		{
			TreeNode treeNode = null;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2 = new TreeNode("SetClauses", new TreeNode[0]);
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				if (dbModificationClause != null)
				{
					treeNode2.Children.Add(dbModificationClause.Print(this._visitor));
				}
			}
			TreeNode treeNode3;
			if (tree.Predicate != null)
			{
				treeNode3 = new TreeNode("Predicate", new TreeNode[]
				{
					this._visitor.VisitExpression(tree.Predicate)
				});
			}
			else
			{
				treeNode3 = new TreeNode("Predicate", new TreeNode[0]);
			}
			TreeNode treeNode4;
			if (tree.Returning != null)
			{
				treeNode4 = new TreeNode("Returning", new TreeNode[]
				{
					this._visitor.VisitExpression(tree.Returning)
				});
			}
			else
			{
				treeNode4 = new TreeNode("Returning", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbUpdateCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2,
				treeNode3,
				treeNode4
			}));
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000323C0 File Offset: 0x000305C0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbQueryCommandTree")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		internal string Print(DbQueryCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("Query", new TreeNode[0]);
			if (tree.Query != null)
			{
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, tree.Query.ResultType);
				treeNode.Children.Add(this._visitor.VisitExpression(tree.Query));
			}
			return this.Print(new TreeNode("DbQueryCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode
			}));
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00032438 File Offset: 0x00030638
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		private static TreeNode CreateParametersNode(DbCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("Parameters", new TreeNode[0]);
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in tree.Parameters)
			{
				TreeNode treeNode2 = new TreeNode(keyValuePair.Key, new TreeNode[0]);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, keyValuePair.Value);
				treeNode.Children.Add(treeNode2);
			}
			return treeNode;
		}

		// Token: 0x0400029C RID: 668
		private readonly ExpressionPrinter.PrinterVisitor _visitor = new ExpressionPrinter.PrinterVisitor();

		// Token: 0x0200012C RID: 300
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private class PrinterVisitor : DbExpressionVisitor<TreeNode>
		{
			// Token: 0x060009D2 RID: 2514 RVA: 0x000324D0 File Offset: 0x000306D0
			private static Dictionary<DbExpressionKind, string> InitializeOpMap()
			{
				Dictionary<DbExpressionKind, string> dictionary = new Dictionary<DbExpressionKind, string>(12);
				dictionary[DbExpressionKind.Divide] = "/";
				dictionary[DbExpressionKind.Modulo] = "%";
				dictionary[DbExpressionKind.Multiply] = "*";
				dictionary[DbExpressionKind.Plus] = "+";
				dictionary[DbExpressionKind.Minus] = "-";
				dictionary[DbExpressionKind.UnaryMinus] = "-";
				dictionary[DbExpressionKind.Equals] = "=";
				dictionary[DbExpressionKind.LessThan] = "<";
				dictionary[DbExpressionKind.LessThanOrEquals] = "<=";
				dictionary[DbExpressionKind.GreaterThan] = ">";
				dictionary[DbExpressionKind.GreaterThanOrEquals] = ">=";
				dictionary[DbExpressionKind.NotEquals] = "<>";
				return dictionary;
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x00032582 File Offset: 0x00030782
			internal TreeNode VisitExpression(DbExpression expr)
			{
				return expr.Accept<TreeNode>(this);
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x0003258C File Offset: 0x0003078C
			internal TreeNode VisitExpression(string name, DbExpression expr)
			{
				return new TreeNode(name, new TreeNode[]
				{
					expr.Accept<TreeNode>(this)
				});
			}

			// Token: 0x060009D5 RID: 2517 RVA: 0x000325B1 File Offset: 0x000307B1
			internal TreeNode VisitBinding(string propName, DbExpressionBinding binding)
			{
				return this.VisitWithLabel(propName, binding.VariableName, binding.Expression);
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x000325E4 File Offset: 0x000307E4
			internal TreeNode VisitFunction(EdmFunction func, IList<DbExpression> args)
			{
				TreeNode treeNode = new TreeNode();
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, func);
				ExpressionPrinter.PrinterVisitor.AppendParameters(treeNode, from fp in func.Parameters
				select new KeyValuePair<string, TypeUsage>(fp.Name, fp.TypeUsage));
				if (args != null)
				{
					this.AppendArguments(treeNode, (from fp in func.Parameters
					select fp.Name).ToArray<string>(), args);
				}
				return treeNode;
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0003266A File Offset: 0x0003086A
			private static TreeNode NodeFromExpression(DbExpression expr)
			{
				return new TreeNode(Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind), new TreeNode[0]);
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x00032694 File Offset: 0x00030894
			private static void AppendParameters(TreeNode node, IEnumerable<KeyValuePair<string, TypeUsage>> paramInfos)
			{
				node.Text.Append("(");
				int num = 0;
				foreach (KeyValuePair<string, TypeUsage> keyValuePair in paramInfos)
				{
					if (num > 0)
					{
						node.Text.Append(", ");
					}
					ExpressionPrinter.PrinterVisitor.AppendType(node, keyValuePair.Value);
					node.Text.Append(" ");
					node.Text.Append(keyValuePair.Key);
					num++;
				}
				node.Text.Append(")");
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x00032744 File Offset: 0x00030944
			internal static void AppendTypeSpecifier(TreeNode node, TypeUsage type)
			{
				node.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendType(node, type);
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x0003275E File Offset: 0x0003095E
			internal static void AppendType(TreeNode node, TypeUsage type)
			{
				ExpressionPrinter.PrinterVisitor.BuildTypeName(node.Text, type);
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x0003276C File Offset: 0x0003096C
			private static void BuildTypeName(StringBuilder text, TypeUsage type)
			{
				RowType rowType = type.EdmType as RowType;
				CollectionType collectionType = type.EdmType as CollectionType;
				RefType refType = type.EdmType as RefType;
				if (TypeSemantics.IsPrimitiveType(type))
				{
					text.Append(type);
					return;
				}
				if (collectionType != null)
				{
					text.Append("Collection{");
					ExpressionPrinter.PrinterVisitor.BuildTypeName(text, collectionType.TypeUsage);
					text.Append("}");
					return;
				}
				if (refType != null)
				{
					text.Append("Ref<");
					ExpressionPrinter.PrinterVisitor.AppendFullName(text, refType.ElementType);
					text.Append(">");
					return;
				}
				if (rowType != null)
				{
					text.Append("Record[");
					int num = 0;
					foreach (EdmProperty edmProperty in rowType.Properties)
					{
						text.Append("'");
						text.Append(edmProperty.Name);
						text.Append("'");
						text.Append("=");
						ExpressionPrinter.PrinterVisitor.BuildTypeName(text, edmProperty.TypeUsage);
						num++;
						if (num < rowType.Properties.Count)
						{
							text.Append(", ");
						}
					}
					text.Append("]");
					return;
				}
				if (!string.IsNullOrEmpty(type.EdmType.NamespaceName))
				{
					text.Append(type.EdmType.NamespaceName);
					text.Append(".");
				}
				text.Append(type.EdmType.Name);
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x00032900 File Offset: 0x00030B00
			private static void AppendFullName(StringBuilder text, EdmType type)
			{
				if (BuiltInTypeKind.RowType != type.BuiltInTypeKind && !string.IsNullOrEmpty(type.NamespaceName))
				{
					text.Append(type.NamespaceName);
					text.Append(".");
				}
				text.Append(type.Name);
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x00032940 File Offset: 0x00030B40
			private List<TreeNode> VisitParams(IList<string> paramInfo, IList<DbExpression> args)
			{
				List<TreeNode> list = new List<TreeNode>();
				for (int i = 0; i < paramInfo.Count; i++)
				{
					list.Add(new TreeNode(paramInfo[i], new TreeNode[0])
					{
						Children = 
						{
							this.VisitExpression(args[i])
						}
					});
				}
				return list;
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x00032997 File Offset: 0x00030B97
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Collections.Generic.List<System.Data.Entity.Core.Common.Utils.TreeNode>)")]
			private void AppendArguments(TreeNode node, IList<string> paramNames, IList<DbExpression> args)
			{
				if (paramNames.Count > 0)
				{
					node.Children.Add(new TreeNode("Arguments", this.VisitParams(paramNames, args)));
				}
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x000329C0 File Offset: 0x00030BC0
			private TreeNode VisitWithLabel(string label, string name, DbExpression def)
			{
				TreeNode treeNode = new TreeNode(label, new TreeNode[0]);
				treeNode.Text.Append(" : '");
				treeNode.Text.Append(name);
				treeNode.Text.Append("'");
				treeNode.Children.Add(this.VisitExpression(def));
				return treeNode;
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x00032A1C File Offset: 0x00030C1C
			private TreeNode VisitBindingList(string propName, IList<DbExpressionBinding> bindings)
			{
				List<TreeNode> list = new List<TreeNode>();
				for (int i = 0; i < bindings.Count; i++)
				{
					list.Add(this.VisitBinding(StringUtil.FormatIndex(propName, i), bindings[i]));
				}
				return new TreeNode(propName, list);
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x00032A64 File Offset: 0x00030C64
			private TreeNode VisitGroupBinding(DbGroupExpressionBinding groupBinding)
			{
				TreeNode item = this.VisitExpression(groupBinding.Expression);
				TreeNode treeNode = new TreeNode();
				treeNode.Children.Add(item);
				treeNode.Text.AppendFormat(CultureInfo.InvariantCulture, "Input : '{0}', '{1}'", new object[]
				{
					groupBinding.VariableName,
					groupBinding.GroupVariableName
				});
				return treeNode;
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x00032AC4 File Offset: 0x00030CC4
			private TreeNode Visit(string name, params DbExpression[] exprs)
			{
				TreeNode treeNode = new TreeNode(name, new TreeNode[0]);
				foreach (DbExpression expr in exprs)
				{
					treeNode.Children.Add(this.VisitExpression(expr));
				}
				return treeNode;
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x00032B08 File Offset: 0x00030D08
			private TreeNode VisitInfix(DbExpression left, string name, DbExpression right)
			{
				if (this._infix)
				{
					return new TreeNode("", new TreeNode[0])
					{
						Children = 
						{
							this.VisitExpression(left),
							new TreeNode(name, new TreeNode[0]),
							this.VisitExpression(right)
						}
					};
				}
				return this.Visit(name, new DbExpression[]
				{
					left,
					right
				});
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x00032B82 File Offset: 0x00030D82
			private TreeNode VisitUnary(DbUnaryExpression expr)
			{
				return this.VisitUnary(expr, false);
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x00032B8C File Offset: 0x00030D8C
			private TreeNode VisitUnary(DbUnaryExpression expr, bool appendType)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(expr);
				if (appendType)
				{
					ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, expr.ResultType);
				}
				treeNode.Children.Add(this.VisitExpression(expr.Argument));
				return treeNode;
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x00032BC8 File Offset: 0x00030DC8
			private TreeNode VisitBinary(DbBinaryExpression expr)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(expr);
				treeNode.Children.Add(this.VisitExpression(expr.Left));
				treeNode.Children.Add(this.VisitExpression(expr.Right));
				return treeNode;
			}

			// Token: 0x060009E7 RID: 2535 RVA: 0x00032C0B File Offset: 0x00030E0B
			public override TreeNode Visit(DbExpression e)
			{
				Check.NotNull<DbExpression>(e, "e");
				throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
			}

			// Token: 0x060009E8 RID: 2536 RVA: 0x00032C30 File Offset: 0x00030E30
			public override TreeNode Visit(DbConstantExpression e)
			{
				Check.NotNull<DbConstantExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				string text = e.Value as string;
				if (text != null)
				{
					text = text.Replace("\r\n", "\\r\\n");
					int num = text.Length;
					if (this._maxStringLength > 0)
					{
						num = Math.Min(text.Length, this._maxStringLength);
					}
					treeNode.Text.Append("'");
					treeNode.Text.Append(text, 0, num);
					if (text.Length > num)
					{
						treeNode.Text.Append("...");
					}
					treeNode.Text.Append("'");
				}
				else
				{
					treeNode.Text.Append(e.Value);
				}
				return treeNode;
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x00032CF4 File Offset: 0x00030EF4
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbNullExpression e)
			{
				Check.NotNull<DbNullExpression>(e, "e");
				return new TreeNode("null", new TreeNode[0]);
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x00032D14 File Offset: 0x00030F14
			public override TreeNode Visit(DbVariableReferenceExpression e)
			{
				Check.NotNull<DbVariableReferenceExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("Var({0})", e.VariableName);
				return treeNode;
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x00032D4C File Offset: 0x00030F4C
			public override TreeNode Visit(DbParameterReferenceExpression e)
			{
				Check.NotNull<DbParameterReferenceExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("@{0}", e.ParameterName);
				return treeNode;
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x00032D84 File Offset: 0x00030F84
			public override TreeNode Visit(DbFunctionExpression e)
			{
				Check.NotNull<DbFunctionExpression>(e, "e");
				return this.VisitFunction(e.Function, e.Arguments);
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x00032DCC File Offset: 0x00030FCC
			public override TreeNode Visit(DbLambdaExpression expression)
			{
				Check.NotNull<DbLambdaExpression>(expression, "expression");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.Append("Lambda");
				ExpressionPrinter.PrinterVisitor.AppendParameters(treeNode, from v in expression.Lambda.Variables
				select new KeyValuePair<string, TypeUsage>(v.VariableName, v.ResultType));
				this.AppendArguments(treeNode, (from v in expression.Lambda.Variables
				select v.VariableName).ToArray<string>(), expression.Arguments);
				treeNode.Children.Add(this.Visit("Body", new DbExpression[]
				{
					expression.Lambda.Body
				}));
				return treeNode;
			}

			// Token: 0x060009EE RID: 2542 RVA: 0x00032E9C File Offset: 0x0003109C
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbPropertyExpression e)
			{
				Check.NotNull<DbPropertyExpression>(e, "e");
				TreeNode treeNode = null;
				if (e.Instance != null)
				{
					treeNode = this.VisitExpression(e.Instance);
					if (e.Instance.ExpressionKind == DbExpressionKind.VariableReference || (e.Instance.ExpressionKind == DbExpressionKind.Property && treeNode.Children.Count == 0))
					{
						treeNode.Text.Append(".");
						treeNode.Text.Append(e.Property.Name);
						return treeNode;
					}
				}
				TreeNode treeNode2 = new TreeNode(".", new TreeNode[0]);
				EdmProperty edmProperty = e.Property as EdmProperty;
				if (edmProperty != null && !(edmProperty.DeclaringType is RowType))
				{
					ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode2.Text, edmProperty.DeclaringType);
					treeNode2.Text.Append(".");
				}
				treeNode2.Text.Append(e.Property.Name);
				if (treeNode != null)
				{
					treeNode2.Children.Add(new TreeNode("Instance", new TreeNode[]
					{
						treeNode
					}));
				}
				return treeNode2;
			}

			// Token: 0x060009EF RID: 2543 RVA: 0x00032FAC File Offset: 0x000311AC
			public override TreeNode Visit(DbComparisonExpression e)
			{
				Check.NotNull<DbComparisonExpression>(e, "e");
				return this.VisitInfix(e.Left, ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Right);
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x00032FDC File Offset: 0x000311DC
			public override TreeNode Visit(DbLikeExpression e)
			{
				Check.NotNull<DbLikeExpression>(e, "e");
				return this.Visit("Like", new DbExpression[]
				{
					e.Argument,
					e.Pattern,
					e.Escape
				});
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x00033024 File Offset: 0x00031224
			public override TreeNode Visit(DbLimitExpression e)
			{
				Check.NotNull<DbLimitExpression>(e, "e");
				return this.Visit(e.WithTies ? "LimitWithTies" : "Limit", new DbExpression[]
				{
					e.Argument,
					e.Limit
				});
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x00033071 File Offset: 0x00031271
			public override TreeNode Visit(DbIsNullExpression e)
			{
				Check.NotNull<DbIsNullExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x060009F3 RID: 2547 RVA: 0x00033088 File Offset: 0x00031288
			public override TreeNode Visit(DbArithmeticExpression e)
			{
				Check.NotNull<DbArithmeticExpression>(e, "e");
				if (DbExpressionKind.UnaryMinus == e.ExpressionKind)
				{
					return this.Visit(ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], new DbExpression[]
					{
						e.Arguments[0]
					});
				}
				return this.VisitInfix(e.Arguments[0], ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Arguments[1]);
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x00033107 File Offset: 0x00031307
			public override TreeNode Visit(DbAndExpression e)
			{
				Check.NotNull<DbAndExpression>(e, "e");
				return this.VisitInfix(e.Left, "And", e.Right);
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x0003312C File Offset: 0x0003132C
			public override TreeNode Visit(DbOrExpression e)
			{
				Check.NotNull<DbOrExpression>(e, "e");
				return this.VisitInfix(e.Left, "Or", e.Right);
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x00033154 File Offset: 0x00031354
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbInExpression e)
			{
				Check.NotNull<DbInExpression>(e, "e");
				TreeNode treeNode;
				if (this._infix)
				{
					treeNode = new TreeNode(string.Empty, new TreeNode[0]);
					treeNode.Children.Add(this.VisitExpression(e.Item));
					treeNode.Children.Add(new TreeNode("In", new TreeNode[0]));
				}
				else
				{
					treeNode = new TreeNode("In", new TreeNode[0]);
					treeNode.Children.Add(this.VisitExpression(e.Item));
				}
				foreach (DbExpression expr in e.List)
				{
					treeNode.Children.Add(this.VisitExpression(expr));
				}
				return treeNode;
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x00033230 File Offset: 0x00031430
			public override TreeNode Visit(DbNotExpression e)
			{
				Check.NotNull<DbNotExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x00033245 File Offset: 0x00031445
			public override TreeNode Visit(DbDistinctExpression e)
			{
				Check.NotNull<DbDistinctExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x0003325A File Offset: 0x0003145A
			public override TreeNode Visit(DbElementExpression e)
			{
				Check.NotNull<DbElementExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x00033270 File Offset: 0x00031470
			public override TreeNode Visit(DbIsEmptyExpression e)
			{
				Check.NotNull<DbIsEmptyExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00033285 File Offset: 0x00031485
			public override TreeNode Visit(DbUnionAllExpression e)
			{
				Check.NotNull<DbUnionAllExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x0003329A File Offset: 0x0003149A
			public override TreeNode Visit(DbIntersectExpression e)
			{
				Check.NotNull<DbIntersectExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x000332AF File Offset: 0x000314AF
			public override TreeNode Visit(DbExceptExpression e)
			{
				Check.NotNull<DbExceptExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x000332C4 File Offset: 0x000314C4
			private TreeNode VisitCastOrTreat(string op, DbUnaryExpression e)
			{
				TreeNode treeNode = this.VisitExpression(e.Argument);
				TreeNode treeNode2;
				if (treeNode.Children.Count == 0)
				{
					treeNode.Text.Insert(0, op);
					treeNode.Text.Insert(op.Length, '(');
					treeNode.Text.Append(" As ");
					ExpressionPrinter.PrinterVisitor.AppendType(treeNode, e.ResultType);
					treeNode.Text.Append(")");
					treeNode2 = treeNode;
				}
				else
				{
					treeNode2 = new TreeNode(op, new TreeNode[0]);
					ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, e.ResultType);
					treeNode2.Children.Add(treeNode);
				}
				return treeNode2;
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00033367 File Offset: 0x00031567
			public override TreeNode Visit(DbTreatExpression e)
			{
				Check.NotNull<DbTreatExpression>(e, "e");
				return this.VisitCastOrTreat("Treat", e);
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00033381 File Offset: 0x00031581
			public override TreeNode Visit(DbCastExpression e)
			{
				Check.NotNull<DbCastExpression>(e, "e");
				return this.VisitCastOrTreat("Cast", e);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x0003339C File Offset: 0x0003159C
			public override TreeNode Visit(DbIsOfExpression e)
			{
				Check.NotNull<DbIsOfExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				if (DbExpressionKind.IsOfOnly == e.ExpressionKind)
				{
					treeNode.Text.Append("IsOfOnly");
				}
				else
				{
					treeNode.Text.Append("IsOf");
				}
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.OfType);
				treeNode.Children.Add(this.VisitExpression(e.Argument));
				return treeNode;
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x00033410 File Offset: 0x00031610
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OfTypeOnly")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OfType")]
			public override TreeNode Visit(DbOfTypeExpression e)
			{
				Check.NotNull<DbOfTypeExpression>(e, "e");
				TreeNode treeNode = new TreeNode((e.ExpressionKind == DbExpressionKind.OfTypeOnly) ? "OfTypeOnly" : "OfType", new TreeNode[0]);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.OfType);
				treeNode.Children.Add(this.VisitExpression(e.Argument));
				return treeNode;
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00033470 File Offset: 0x00031670
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbCaseExpression e)
			{
				Check.NotNull<DbCaseExpression>(e, "e");
				TreeNode treeNode = new TreeNode("Case", new TreeNode[0]);
				for (int i = 0; i < e.When.Count; i++)
				{
					treeNode.Children.Add(this.Visit("When", new DbExpression[]
					{
						e.When[i]
					}));
					treeNode.Children.Add(this.Visit("Then", new DbExpression[]
					{
						e.Then[i]
					}));
				}
				treeNode.Children.Add(this.Visit("Else", new DbExpression[]
				{
					e.Else
				}));
				return treeNode;
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x00033538 File Offset: 0x00031738
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelatedEntityReferences")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbNewInstanceExpression e)
			{
				Check.NotNull<DbNewInstanceExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.ResultType);
				if (BuiltInTypeKind.CollectionType == e.ResultType.EdmType.BuiltInTypeKind)
				{
					using (IEnumerator<DbExpression> enumerator = e.Arguments.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DbExpression expr = enumerator.Current;
							treeNode.Children.Add(this.VisitExpression(expr));
						}
						return treeNode;
					}
				}
				string label = (BuiltInTypeKind.RowType == e.ResultType.EdmType.BuiltInTypeKind) ? "Column" : "Property";
				IList<EdmProperty> properties = TypeHelpers.GetProperties(e.ResultType);
				for (int i = 0; i < properties.Count; i++)
				{
					treeNode.Children.Add(this.VisitWithLabel(label, properties[i].Name, e.Arguments[i]));
				}
				if (BuiltInTypeKind.EntityType == e.ResultType.EdmType.BuiltInTypeKind && e.HasRelatedEntityReferences)
				{
					TreeNode treeNode2 = new TreeNode("RelatedEntityReferences", new TreeNode[0]);
					foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
					{
						TreeNode treeNode3 = ExpressionPrinter.PrinterVisitor.CreateNavigationNode(dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
						treeNode3.Children.Add(ExpressionPrinter.PrinterVisitor.CreateRelationshipNode((RelationshipType)dbRelatedEntityRef.SourceEnd.DeclaringType));
						treeNode3.Children.Add(this.VisitExpression(dbRelatedEntityRef.TargetEntityReference));
						treeNode2.Children.Add(treeNode3);
					}
					treeNode.Children.Add(treeNode2);
				}
				return treeNode;
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00033714 File Offset: 0x00031914
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EntitySet")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbRefExpression e)
			{
				Check.NotNull<DbRefExpression>(e, "e");
				TreeNode treeNode = new TreeNode("Ref", new TreeNode[0]);
				treeNode.Text.Append("<");
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, TypeHelpers.GetEdmType<RefType>(e.ResultType).ElementType);
				treeNode.Text.Append(">");
				TreeNode treeNode2 = new TreeNode("EntitySet : ", new TreeNode[0]);
				treeNode2.Text.Append(e.EntitySet.EntityContainer.Name);
				treeNode2.Text.Append(".");
				treeNode2.Text.Append(e.EntitySet.Name);
				treeNode.Children.Add(treeNode2);
				treeNode.Children.Add(this.Visit("Keys", new DbExpression[]
				{
					e.Argument
				}));
				return treeNode;
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x00033804 File Offset: 0x00031A04
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			private static TreeNode CreateRelationshipNode(RelationshipType relType)
			{
				TreeNode treeNode = new TreeNode("Relationship", new TreeNode[0]);
				treeNode.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, relType);
				return treeNode;
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00033840 File Offset: 0x00031A40
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			private static TreeNode CreateNavigationNode(RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text.Append("Navigation : ");
				treeNode.Text.Append(fromEnd.Name);
				treeNode.Text.Append(" -> ");
				treeNode.Text.Append(toEnd.Name);
				return treeNode;
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x0003389C File Offset: 0x00031A9C
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			public override TreeNode Visit(DbRelationshipNavigationExpression e)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(ExpressionPrinter.PrinterVisitor.CreateRelationshipNode(e.Relationship));
				treeNode.Children.Add(ExpressionPrinter.PrinterVisitor.CreateNavigationNode(e.NavigateFrom, e.NavigateTo));
				treeNode.Children.Add(this.Visit("Source", new DbExpression[]
				{
					e.NavigationSource
				}));
				return treeNode;
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00033916 File Offset: 0x00031B16
			public override TreeNode Visit(DbDerefExpression e)
			{
				Check.NotNull<DbDerefExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x0003392B File Offset: 0x00031B2B
			public override TreeNode Visit(DbRefKeyExpression e)
			{
				Check.NotNull<DbRefKeyExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x00033941 File Offset: 0x00031B41
			public override TreeNode Visit(DbEntityRefExpression e)
			{
				Check.NotNull<DbEntityRefExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x00033958 File Offset: 0x00031B58
			public override TreeNode Visit(DbScanExpression e)
			{
				Check.NotNull<DbScanExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Text.Append(" : ");
				treeNode.Text.Append(e.Target.EntityContainer.Name);
				treeNode.Text.Append(".");
				treeNode.Text.Append(e.Target.Name);
				return treeNode;
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x000339D0 File Offset: 0x00031BD0
			public override TreeNode Visit(DbFilterExpression e)
			{
				Check.NotNull<DbFilterExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[]
				{
					e.Predicate
				}));
				return treeNode;
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x00033A34 File Offset: 0x00031C34
			public override TreeNode Visit(DbProjectExpression e)
			{
				Check.NotNull<DbProjectExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Projection", new DbExpression[]
				{
					e.Projection
				}));
				return treeNode;
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x00033A98 File Offset: 0x00031C98
			public override TreeNode Visit(DbCrossJoinExpression e)
			{
				Check.NotNull<DbCrossJoinExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBindingList("Inputs", e.Inputs));
				return treeNode;
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00033AD8 File Offset: 0x00031CD8
			public override TreeNode Visit(DbJoinExpression e)
			{
				Check.NotNull<DbJoinExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Left", e.Left));
				treeNode.Children.Add(this.VisitBinding("Right", e.Right));
				treeNode.Children.Add(this.Visit("JoinCondition", new DbExpression[]
				{
					e.JoinCondition
				}));
				return treeNode;
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x00033B58 File Offset: 0x00031D58
			public override TreeNode Visit(DbApplyExpression e)
			{
				Check.NotNull<DbApplyExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitBinding("Apply", e.Apply));
				return treeNode;
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x00033BB4 File Offset: 0x00031DB4
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Collections.Generic.List<System.Data.Entity.Core.Common.Utils.TreeNode>)")]
			public override TreeNode Visit(DbGroupByExpression e)
			{
				Check.NotNull<DbGroupByExpression>(e, "e");
				List<TreeNode> list = new List<TreeNode>();
				List<TreeNode> list2 = new List<TreeNode>();
				RowType edmType = TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage);
				int num = 0;
				for (int i = 0; i < e.Keys.Count; i++)
				{
					list.Add(this.VisitWithLabel("Key", edmType.Properties[i].Name, e.Keys[num]));
					num++;
				}
				int num2 = 0;
				for (int j = e.Keys.Count; j < edmType.Properties.Count; j++)
				{
					TreeNode treeNode = new TreeNode("Aggregate : '", new TreeNode[0]);
					treeNode.Text.Append(edmType.Properties[j].Name);
					treeNode.Text.Append("'");
					DbFunctionAggregate dbFunctionAggregate = e.Aggregates[num2] as DbFunctionAggregate;
					if (dbFunctionAggregate != null)
					{
						TreeNode treeNode2 = this.VisitFunction(dbFunctionAggregate.Function, dbFunctionAggregate.Arguments);
						if (dbFunctionAggregate.Distinct)
						{
							treeNode2 = new TreeNode("Distinct", new TreeNode[]
							{
								treeNode2
							});
						}
						treeNode.Children.Add(treeNode2);
					}
					else
					{
						DbGroupAggregate dbGroupAggregate = e.Aggregates[num2] as DbGroupAggregate;
						treeNode.Children.Add(this.Visit("GroupAggregate", new DbExpression[]
						{
							dbGroupAggregate.Arguments[0]
						}));
					}
					list2.Add(treeNode);
					num2++;
				}
				TreeNode treeNode3 = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode3.Children.Add(this.VisitGroupBinding(e.Input));
				if (list.Count > 0)
				{
					treeNode3.Children.Add(new TreeNode("Keys", list));
				}
				if (list2.Count > 0)
				{
					treeNode3.Children.Add(new TreeNode("Aggregates", list2));
				}
				return treeNode3;
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x00033DC8 File Offset: 0x00031FC8
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortOrder")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
			private TreeNode VisitSortOrder(IList<DbSortClause> sortOrder)
			{
				TreeNode treeNode = new TreeNode("SortOrder", new TreeNode[0]);
				foreach (DbSortClause dbSortClause in sortOrder)
				{
					TreeNode treeNode2 = this.Visit(dbSortClause.Ascending ? "Asc" : "Desc", new DbExpression[]
					{
						dbSortClause.Expression
					});
					if (!string.IsNullOrEmpty(dbSortClause.Collation))
					{
						treeNode2.Text.Append(" : ");
						treeNode2.Text.Append(dbSortClause.Collation);
					}
					treeNode.Children.Add(treeNode2);
				}
				return treeNode;
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x00033E88 File Offset: 0x00032088
			public override TreeNode Visit(DbSkipExpression e)
			{
				Check.NotNull<DbSkipExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				treeNode.Children.Add(this.Visit("Count", new DbExpression[]
				{
					e.Count
				}));
				return treeNode;
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00033F04 File Offset: 0x00032104
			public override TreeNode Visit(DbSortExpression e)
			{
				Check.NotNull<DbSortExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				return treeNode;
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x00033F58 File Offset: 0x00032158
			public override TreeNode Visit(DbQuantifierExpression e)
			{
				Check.NotNull<DbQuantifierExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[]
				{
					e.Predicate
				}));
				return treeNode;
			}

			// Token: 0x0400029D RID: 669
			private static readonly Dictionary<DbExpressionKind, string> _opMap = ExpressionPrinter.PrinterVisitor.InitializeOpMap();

			// Token: 0x0400029E RID: 670
			private int _maxStringLength = 80;

			// Token: 0x0400029F RID: 671
			private bool _infix = true;
		}
	}
}
