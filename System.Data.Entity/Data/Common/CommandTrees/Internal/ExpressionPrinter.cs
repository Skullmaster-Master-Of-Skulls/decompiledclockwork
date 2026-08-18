using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000437 RID: 1079
	internal class ExpressionPrinter : TreePrinter
	{
		// Token: 0x06003A02 RID: 14850 RVA: 0x000DD372 File Offset: 0x000DB572
		internal ExpressionPrinter()
		{
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000DD385 File Offset: 0x000DB585
		internal string Print(DbExpression expr)
		{
			return this.Print(this._visitor.VisitExpression(expr));
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000DD39C File Offset: 0x000DB59C
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

		// Token: 0x06003A05 RID: 14853 RVA: 0x000DD434 File Offset: 0x000DB634
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

		// Token: 0x06003A06 RID: 14854 RVA: 0x000DD4C4 File Offset: 0x000DB6C4
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

		// Token: 0x06003A07 RID: 14855 RVA: 0x000DD5DC File Offset: 0x000DB7DC
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

		// Token: 0x06003A08 RID: 14856 RVA: 0x000DD738 File Offset: 0x000DB938
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

		// Token: 0x06003A09 RID: 14857 RVA: 0x000DD7B0 File Offset: 0x000DB9B0
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

		// Token: 0x04001869 RID: 6249
		private ExpressionPrinter.PrinterVisitor _visitor = new ExpressionPrinter.PrinterVisitor();

		// Token: 0x020006CB RID: 1739
		private class PrinterVisitor : DbExpressionVisitor<TreeNode>
		{
			// Token: 0x0600460B RID: 17931 RVA: 0x000FABA0 File Offset: 0x000F8DA0
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

			// Token: 0x0600460C RID: 17932 RVA: 0x000FAC52 File Offset: 0x000F8E52
			internal TreeNode VisitExpression(DbExpression expr)
			{
				return expr.Accept<TreeNode>(this);
			}

			// Token: 0x0600460D RID: 17933 RVA: 0x000FAC5B File Offset: 0x000F8E5B
			internal TreeNode VisitExpression(string name, DbExpression expr)
			{
				return new TreeNode(name, new TreeNode[]
				{
					expr.Accept<TreeNode>(this)
				});
			}

			// Token: 0x0600460E RID: 17934 RVA: 0x000FAC73 File Offset: 0x000F8E73
			internal TreeNode VisitBinding(string propName, DbExpressionBinding binding)
			{
				return this.VisitWithLabel(propName, binding.VariableName, binding.Expression);
			}

			// Token: 0x0600460F RID: 17935 RVA: 0x000FAC88 File Offset: 0x000F8E88
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

			// Token: 0x06004610 RID: 17936 RVA: 0x000FAD12 File Offset: 0x000F8F12
			private static TreeNode NodeFromExpression(DbExpression expr)
			{
				return new TreeNode(Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind), new TreeNode[0]);
			}

			// Token: 0x06004611 RID: 17937 RVA: 0x000FAD3C File Offset: 0x000F8F3C
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

			// Token: 0x06004612 RID: 17938 RVA: 0x000FADEC File Offset: 0x000F8FEC
			internal static void AppendTypeSpecifier(TreeNode node, TypeUsage type)
			{
				node.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendType(node, type);
			}

			// Token: 0x06004613 RID: 17939 RVA: 0x000FAE06 File Offset: 0x000F9006
			internal static void AppendType(TreeNode node, TypeUsage type)
			{
				ExpressionPrinter.PrinterVisitor.BuildTypeName(node.Text, type);
			}

			// Token: 0x06004614 RID: 17940 RVA: 0x000FAE14 File Offset: 0x000F9014
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

			// Token: 0x06004615 RID: 17941 RVA: 0x000FAFA8 File Offset: 0x000F91A8
			private static void AppendFullName(StringBuilder text, EdmType type)
			{
				if (BuiltInTypeKind.RowType != type.BuiltInTypeKind && !string.IsNullOrEmpty(type.NamespaceName))
				{
					text.Append(type.NamespaceName);
					text.Append(".");
				}
				text.Append(type.Name);
			}

			// Token: 0x06004616 RID: 17942 RVA: 0x000FAFE8 File Offset: 0x000F91E8
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

			// Token: 0x06004617 RID: 17943 RVA: 0x000FB03F File Offset: 0x000F923F
			private void AppendArguments(TreeNode node, IList<string> paramNames, IList<DbExpression> args)
			{
				if (paramNames.Count > 0)
				{
					node.Children.Add(new TreeNode("Arguments", this.VisitParams(paramNames, args)));
				}
			}

			// Token: 0x06004618 RID: 17944 RVA: 0x000FB068 File Offset: 0x000F9268
			private TreeNode VisitWithLabel(string label, string name, DbExpression def)
			{
				TreeNode treeNode = new TreeNode(label, new TreeNode[0]);
				treeNode.Text.Append(" : '");
				treeNode.Text.Append(name);
				treeNode.Text.Append("'");
				treeNode.Children.Add(this.VisitExpression(def));
				return treeNode;
			}

			// Token: 0x06004619 RID: 17945 RVA: 0x000FB0C4 File Offset: 0x000F92C4
			private TreeNode VisitBindingList(string propName, IList<DbExpressionBinding> bindings)
			{
				List<TreeNode> list = new List<TreeNode>();
				for (int i = 0; i < bindings.Count; i++)
				{
					list.Add(this.VisitBinding(StringUtil.FormatIndex(propName, i), bindings[i]));
				}
				return new TreeNode(propName, list);
			}

			// Token: 0x0600461A RID: 17946 RVA: 0x000FB10C File Offset: 0x000F930C
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

			// Token: 0x0600461B RID: 17947 RVA: 0x000FB168 File Offset: 0x000F9368
			private TreeNode Visit(string name, params DbExpression[] exprs)
			{
				TreeNode treeNode = new TreeNode(name, new TreeNode[0]);
				foreach (DbExpression expr in exprs)
				{
					treeNode.Children.Add(this.VisitExpression(expr));
				}
				return treeNode;
			}

			// Token: 0x0600461C RID: 17948 RVA: 0x000FB1AC File Offset: 0x000F93AC
			private TreeNode VisitInfix(DbExpression root, DbExpression left, string name, DbExpression right)
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

			// Token: 0x0600461D RID: 17949 RVA: 0x000FB226 File Offset: 0x000F9426
			private TreeNode VisitUnary(DbUnaryExpression expr)
			{
				return this.VisitUnary(expr, false);
			}

			// Token: 0x0600461E RID: 17950 RVA: 0x000FB230 File Offset: 0x000F9430
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

			// Token: 0x0600461F RID: 17951 RVA: 0x000FB26C File Offset: 0x000F946C
			private TreeNode VisitBinary(DbBinaryExpression expr)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(expr);
				treeNode.Children.Add(this.VisitExpression(expr.Left));
				treeNode.Children.Add(this.VisitExpression(expr.Right));
				return treeNode;
			}

			// Token: 0x06004620 RID: 17952 RVA: 0x00017364 File Offset: 0x00015564
			public override TreeNode Visit(DbExpression e)
			{
				throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
			}

			// Token: 0x06004621 RID: 17953 RVA: 0x000FB2B0 File Offset: 0x000F94B0
			public override TreeNode Visit(DbConstantExpression e)
			{
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
					treeNode.Text.Append(e.Value.ToString());
				}
				return treeNode;
			}

			// Token: 0x06004622 RID: 17954 RVA: 0x000FB36D File Offset: 0x000F956D
			public override TreeNode Visit(DbNullExpression e)
			{
				return new TreeNode("null", new TreeNode[0]);
			}

			// Token: 0x06004623 RID: 17955 RVA: 0x000FB380 File Offset: 0x000F9580
			public override TreeNode Visit(DbVariableReferenceExpression e)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("Var({0})", e.VariableName);
				return treeNode;
			}

			// Token: 0x06004624 RID: 17956 RVA: 0x000FB3AC File Offset: 0x000F95AC
			public override TreeNode Visit(DbParameterReferenceExpression e)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("@{0}", e.ParameterName);
				return treeNode;
			}

			// Token: 0x06004625 RID: 17957 RVA: 0x000FB3D8 File Offset: 0x000F95D8
			public override TreeNode Visit(DbFunctionExpression e)
			{
				return this.VisitFunction(e.Function, e.Arguments);
			}

			// Token: 0x06004626 RID: 17958 RVA: 0x000FB3FC File Offset: 0x000F95FC
			public override TreeNode Visit(DbLambdaExpression expression)
			{
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

			// Token: 0x06004627 RID: 17959 RVA: 0x000FB4C4 File Offset: 0x000F96C4
			public override TreeNode Visit(DbPropertyExpression e)
			{
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

			// Token: 0x06004628 RID: 17960 RVA: 0x000FB5C6 File Offset: 0x000F97C6
			public override TreeNode Visit(DbComparisonExpression e)
			{
				return this.VisitInfix(e, e.Left, ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Right);
			}

			// Token: 0x06004629 RID: 17961 RVA: 0x000FB5EB File Offset: 0x000F97EB
			public override TreeNode Visit(DbLikeExpression e)
			{
				return this.Visit("Like", new DbExpression[]
				{
					e.Argument,
					e.Pattern,
					e.Escape
				});
			}

			// Token: 0x0600462A RID: 17962 RVA: 0x000FB619 File Offset: 0x000F9819
			public override TreeNode Visit(DbLimitExpression e)
			{
				return this.Visit(e.WithTies ? "LimitWithTies" : "Limit", new DbExpression[]
				{
					e.Argument,
					e.Limit
				});
			}

			// Token: 0x0600462B RID: 17963 RVA: 0x000FB64D File Offset: 0x000F984D
			public override TreeNode Visit(DbIsNullExpression e)
			{
				return this.VisitUnary(e);
			}

			// Token: 0x0600462C RID: 17964 RVA: 0x000FB658 File Offset: 0x000F9858
			public override TreeNode Visit(DbArithmeticExpression e)
			{
				if (DbExpressionKind.UnaryMinus == e.ExpressionKind)
				{
					return this.Visit(ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], new DbExpression[]
					{
						e.Arguments[0]
					});
				}
				return this.VisitInfix(e, e.Arguments[0], ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Arguments[1]);
			}

			// Token: 0x0600462D RID: 17965 RVA: 0x000FB6CA File Offset: 0x000F98CA
			public override TreeNode Visit(DbAndExpression e)
			{
				return this.VisitInfix(e, e.Left, "And", e.Right);
			}

			// Token: 0x0600462E RID: 17966 RVA: 0x000FB6E4 File Offset: 0x000F98E4
			public override TreeNode Visit(DbOrExpression e)
			{
				return this.VisitInfix(e, e.Left, "Or", e.Right);
			}

			// Token: 0x0600462F RID: 17967 RVA: 0x000FB64D File Offset: 0x000F984D
			public override TreeNode Visit(DbNotExpression e)
			{
				return this.VisitUnary(e);
			}

			// Token: 0x06004630 RID: 17968 RVA: 0x000FB64D File Offset: 0x000F984D
			public override TreeNode Visit(DbDistinctExpression e)
			{
				return this.VisitUnary(e);
			}

			// Token: 0x06004631 RID: 17969 RVA: 0x000FB6FE File Offset: 0x000F98FE
			public override TreeNode Visit(DbElementExpression e)
			{
				return this.VisitUnary(e, true);
			}

			// Token: 0x06004632 RID: 17970 RVA: 0x000FB64D File Offset: 0x000F984D
			public override TreeNode Visit(DbIsEmptyExpression e)
			{
				return this.VisitUnary(e);
			}

			// Token: 0x06004633 RID: 17971 RVA: 0x000FB708 File Offset: 0x000F9908
			public override TreeNode Visit(DbUnionAllExpression e)
			{
				return this.VisitBinary(e);
			}

			// Token: 0x06004634 RID: 17972 RVA: 0x000FB708 File Offset: 0x000F9908
			public override TreeNode Visit(DbIntersectExpression e)
			{
				return this.VisitBinary(e);
			}

			// Token: 0x06004635 RID: 17973 RVA: 0x000FB708 File Offset: 0x000F9908
			public override TreeNode Visit(DbExceptExpression e)
			{
				return this.VisitBinary(e);
			}

			// Token: 0x06004636 RID: 17974 RVA: 0x000FB714 File Offset: 0x000F9914
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

			// Token: 0x06004637 RID: 17975 RVA: 0x000FB7B7 File Offset: 0x000F99B7
			public override TreeNode Visit(DbTreatExpression e)
			{
				return this.VisitCastOrTreat("Treat", e);
			}

			// Token: 0x06004638 RID: 17976 RVA: 0x000FB7C5 File Offset: 0x000F99C5
			public override TreeNode Visit(DbCastExpression e)
			{
				return this.VisitCastOrTreat("Cast", e);
			}

			// Token: 0x06004639 RID: 17977 RVA: 0x000FB7D4 File Offset: 0x000F99D4
			public override TreeNode Visit(DbIsOfExpression e)
			{
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

			// Token: 0x0600463A RID: 17978 RVA: 0x000FB83C File Offset: 0x000F9A3C
			public override TreeNode Visit(DbOfTypeExpression e)
			{
				TreeNode treeNode = new TreeNode((e.ExpressionKind == DbExpressionKind.OfTypeOnly) ? "OfTypeOnly" : "OfType", new TreeNode[0]);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.OfType);
				treeNode.Children.Add(this.VisitExpression(e.Argument));
				return treeNode;
			}

			// Token: 0x0600463B RID: 17979 RVA: 0x000FB890 File Offset: 0x000F9A90
			public override TreeNode Visit(DbCaseExpression e)
			{
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

			// Token: 0x0600463C RID: 17980 RVA: 0x000FB940 File Offset: 0x000F9B40
			public override TreeNode Visit(DbNewInstanceExpression e)
			{
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
						TreeNode treeNode3 = this.CreateNavigationNode(dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
						treeNode3.Children.Add(this.CreateRelationshipNode((RelationshipType)dbRelatedEntityRef.SourceEnd.DeclaringType));
						treeNode3.Children.Add(this.VisitExpression(dbRelatedEntityRef.TargetEntityReference));
						treeNode2.Children.Add(treeNode3);
					}
					treeNode.Children.Add(treeNode2);
				}
				return treeNode;
			}

			// Token: 0x0600463D RID: 17981 RVA: 0x000FBB10 File Offset: 0x000F9D10
			public override TreeNode Visit(DbRefExpression e)
			{
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

			// Token: 0x0600463E RID: 17982 RVA: 0x000FBBF4 File Offset: 0x000F9DF4
			private TreeNode CreateRelationshipNode(RelationshipType relType)
			{
				TreeNode treeNode = new TreeNode("Relationship", new TreeNode[0]);
				treeNode.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, relType);
				return treeNode;
			}

			// Token: 0x0600463F RID: 17983 RVA: 0x000FBC30 File Offset: 0x000F9E30
			private TreeNode CreateNavigationNode(RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text.Append("Navigation : ");
				treeNode.Text.Append(fromEnd.Name);
				treeNode.Text.Append(" -> ");
				treeNode.Text.Append(toEnd.Name);
				return treeNode;
			}

			// Token: 0x06004640 RID: 17984 RVA: 0x000FBC8C File Offset: 0x000F9E8C
			public override TreeNode Visit(DbRelationshipNavigationExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.CreateRelationshipNode(e.Relationship));
				treeNode.Children.Add(this.CreateNavigationNode(e.NavigateFrom, e.NavigateTo));
				treeNode.Children.Add(this.Visit("Source", new DbExpression[]
				{
					e.NavigationSource
				}));
				return treeNode;
			}

			// Token: 0x06004641 RID: 17985 RVA: 0x000FB64D File Offset: 0x000F984D
			public override TreeNode Visit(DbDerefExpression e)
			{
				return this.VisitUnary(e);
			}

			// Token: 0x06004642 RID: 17986 RVA: 0x000FB6FE File Offset: 0x000F98FE
			public override TreeNode Visit(DbRefKeyExpression e)
			{
				return this.VisitUnary(e, true);
			}

			// Token: 0x06004643 RID: 17987 RVA: 0x000FB6FE File Offset: 0x000F98FE
			public override TreeNode Visit(DbEntityRefExpression e)
			{
				return this.VisitUnary(e, true);
			}

			// Token: 0x06004644 RID: 17988 RVA: 0x000FBCFC File Offset: 0x000F9EFC
			public override TreeNode Visit(DbScanExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Text.Append(" : ");
				treeNode.Text.Append(e.Target.EntityContainer.Name);
				treeNode.Text.Append(".");
				treeNode.Text.Append(e.Target.Name);
				return treeNode;
			}

			// Token: 0x06004645 RID: 17989 RVA: 0x000FBD68 File Offset: 0x000F9F68
			public override TreeNode Visit(DbFilterExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[]
				{
					e.Predicate
				}));
				return treeNode;
			}

			// Token: 0x06004646 RID: 17990 RVA: 0x000FBDC0 File Offset: 0x000F9FC0
			public override TreeNode Visit(DbProjectExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Projection", new DbExpression[]
				{
					e.Projection
				}));
				return treeNode;
			}

			// Token: 0x06004647 RID: 17991 RVA: 0x000FBE18 File Offset: 0x000FA018
			public override TreeNode Visit(DbCrossJoinExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBindingList("Inputs", e.Inputs));
				return treeNode;
			}

			// Token: 0x06004648 RID: 17992 RVA: 0x000FBE4C File Offset: 0x000FA04C
			public override TreeNode Visit(DbJoinExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Left", e.Left));
				treeNode.Children.Add(this.VisitBinding("Right", e.Right));
				treeNode.Children.Add(this.Visit("JoinCondition", new DbExpression[]
				{
					e.JoinCondition
				}));
				return treeNode;
			}

			// Token: 0x06004649 RID: 17993 RVA: 0x000FBEC0 File Offset: 0x000FA0C0
			public override TreeNode Visit(DbApplyExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitBinding("Apply", e.Apply));
				return treeNode;
			}

			// Token: 0x0600464A RID: 17994 RVA: 0x000FBF10 File Offset: 0x000FA110
			public override TreeNode Visit(DbGroupByExpression e)
			{
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

			// Token: 0x0600464B RID: 17995 RVA: 0x000FC110 File Offset: 0x000FA310
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

			// Token: 0x0600464C RID: 17996 RVA: 0x000FC1CC File Offset: 0x000FA3CC
			public override TreeNode Visit(DbSkipExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				treeNode.Children.Add(this.Visit("Count", new DbExpression[]
				{
					e.Count
				}));
				return treeNode;
			}

			// Token: 0x0600464D RID: 17997 RVA: 0x000FC23C File Offset: 0x000FA43C
			public override TreeNode Visit(DbSortExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				return treeNode;
			}

			// Token: 0x0600464E RID: 17998 RVA: 0x000FC284 File Offset: 0x000FA484
			public override TreeNode Visit(DbQuantifierExpression e)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[]
				{
					e.Predicate
				}));
				return treeNode;
			}

			// Token: 0x04002084 RID: 8324
			private static Dictionary<DbExpressionKind, string> _opMap = ExpressionPrinter.PrinterVisitor.InitializeOpMap();

			// Token: 0x04002085 RID: 8325
			private int _maxStringLength = 80;

			// Token: 0x04002086 RID: 8326
			private bool _infix = true;
		}
	}
}
