using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200000A RID: 10
	public class BindingsVisitor : IVisitor
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002974 File Offset: 0x00000B74
		private BindingsVisitor()
		{
			this.m_bindings = new List<BindingIdentifier>();
			this.m_lookups = new List<Lookup>();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002994 File Offset: 0x00000B94
		public static IList<BindingIdentifier> Bindings(AstNode node)
		{
			BindingsVisitor bindingsVisitor = new BindingsVisitor();
			if (node != null)
			{
				node.Accept(bindingsVisitor);
			}
			return bindingsVisitor.m_bindings;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static IList<Lookup> References(AstNode node)
		{
			BindingsVisitor bindingsVisitor = new BindingsVisitor();
			if (node != null)
			{
				node.Accept(bindingsVisitor);
			}
			return bindingsVisitor.m_lookups;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000029FD File Offset: 0x00000BFD
		public void Visit(ArrayLiteral node)
		{
			node.IfNotNull(delegate(ArrayLiteral n)
			{
				n.Elements.ForEach(delegate(AstNode e)
				{
					e.Accept(this);
				});
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002A33 File Offset: 0x00000C33
		public void Visit(AstNodeList node)
		{
			node.IfNotNull(delegate(AstNodeList n)
			{
				n.Children.ForEach(delegate(AstNode i)
				{
					i.Accept(this);
				});
			});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002A55 File Offset: 0x00000C55
		public void Visit(BindingIdentifier node)
		{
			node.IfNotNull(delegate(BindingIdentifier n)
			{
				this.m_bindings.Add(n);
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002A69 File Offset: 0x00000C69
		public void Visit(ClassNode node)
		{
			if (node != null && node.Binding != null)
			{
				node.Binding.Accept(this);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002A82 File Offset: 0x00000C82
		public void Visit(ConstantWrapper node)
		{
			if (node != null && node.Value != Missing.Value)
			{
				BindingsVisitor.ReportError(node);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void Visit(ConstStatement node)
		{
			node.IfNotNull(delegate(ConstStatement n)
			{
				n.Children.ForEach(delegate(AstNode v)
				{
					v.Accept(this);
				});
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public void Visit(CustomNode node)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public void Visit(ExportNode node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node)
				{
					astNode.Accept(this);
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002B20 File Offset: 0x00000D20
		public void Visit(FunctionObject node)
		{
			if (node != null && node.Binding != null)
			{
				node.Binding.Accept(this);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002B5B File Offset: 0x00000D5B
		public void Visit(InitializerNode node)
		{
			node.IfNotNull(delegate(InitializerNode n)
			{
				n.Binding.IfNotNull(delegate(AstNode v)
				{
					v.Accept(this);
				});
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002B70 File Offset: 0x00000D70
		public void Visit(ImportExportSpecifier node)
		{
			if (node != null)
			{
				BindingIdentifier bindingIdentifier = node.LocalIdentifier as BindingIdentifier;
				if (bindingIdentifier != null)
				{
					this.m_bindings.Add(bindingIdentifier);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002B9C File Offset: 0x00000D9C
		public void Visit(ImportNode node)
		{
			if (node != null)
			{
				foreach (AstNode astNode in node)
				{
					astNode.Accept(this);
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002C0A File Offset: 0x00000E0A
		public void Visit(LexicalDeclaration node)
		{
			node.IfNotNull(delegate(LexicalDeclaration n)
			{
				n.Children.ForEach(delegate(AstNode v)
				{
					v.Accept(this);
				});
			});
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002C2C File Offset: 0x00000E2C
		public void Visit(Lookup node)
		{
			node.IfNotNull(delegate(Lookup n)
			{
				this.m_lookups.Add(n);
			});
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002C40 File Offset: 0x00000E40
		public void Visit(ModuleDeclaration node)
		{
			if (node != null && node.Binding != null)
			{
				this.m_bindings.Add(node.Binding);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002C80 File Offset: 0x00000E80
		public void Visit(ObjectLiteral node)
		{
			node.IfNotNull(delegate(ObjectLiteral n)
			{
				n.Properties.ForEach(delegate(AstNode p)
				{
					p.Accept(this);
				});
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public void Visit(ObjectLiteralProperty node)
		{
			node.IfNotNull(delegate(ObjectLiteralProperty n)
			{
				n.Value.IfNotNull(delegate(AstNode v)
				{
					v.Accept(this);
				});
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002CEC File Offset: 0x00000EEC
		public void Visit(ParameterDeclaration node)
		{
			node.IfNotNull(delegate(ParameterDeclaration n)
			{
				n.Binding.IfNotNull(delegate(AstNode b)
				{
					b.Accept(this);
				});
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002D22 File Offset: 0x00000F22
		public void Visit(Var node)
		{
			node.IfNotNull(delegate(Var n)
			{
				n.Children.ForEach(delegate(AstNode v)
				{
					v.Accept(this);
				});
			});
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002D58 File Offset: 0x00000F58
		public void Visit(VariableDeclaration node)
		{
			node.IfNotNull(delegate(VariableDeclaration n)
			{
				n.Binding.IfNotNull(delegate(AstNode b)
				{
					b.Accept(this);
				});
			});
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private static void ReportError(AstNode node)
		{
			node.IfNotNull(delegate(AstNode n)
			{
				n.Context.IfNotNull(delegate(Context c)
				{
					c.HandleError(JSError.BadBindingSyntax, true);
				});
			});
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002DC9 File Offset: 0x00000FC9
		public void Visit(AspNetBlockNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public void Visit(BinaryOperator node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public void Visit(Block node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public void Visit(Break node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public void Visit(CallNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public void Visit(ComprehensionNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public void Visit(ComprehensionForClause node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002E01 File Offset: 0x00001001
		public void Visit(ComprehensionIfClause node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002E09 File Offset: 0x00001009
		public void Visit(ConditionalCompilationComment node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002E11 File Offset: 0x00001011
		public void Visit(ConditionalCompilationElse node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002E19 File Offset: 0x00001019
		public void Visit(ConditionalCompilationElseIf node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002E21 File Offset: 0x00001021
		public void Visit(ConditionalCompilationEnd node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002E29 File Offset: 0x00001029
		public void Visit(ConditionalCompilationIf node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002E31 File Offset: 0x00001031
		public void Visit(ConditionalCompilationOn node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002E39 File Offset: 0x00001039
		public void Visit(ConditionalCompilationSet node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002E41 File Offset: 0x00001041
		public void Visit(Conditional node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002E49 File Offset: 0x00001049
		public void Visit(ConstantWrapperPP node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002E51 File Offset: 0x00001051
		public void Visit(ContinueNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002E59 File Offset: 0x00001059
		public void Visit(DebuggerNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002E61 File Offset: 0x00001061
		public void Visit(DirectivePrologue node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002E69 File Offset: 0x00001069
		public void Visit(DoWhile node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002E71 File Offset: 0x00001071
		public void Visit(EmptyStatement node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002E79 File Offset: 0x00001079
		public void Visit(ForIn node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002E81 File Offset: 0x00001081
		public void Visit(ForNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002E89 File Offset: 0x00001089
		public void Visit(GetterSetter node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002E91 File Offset: 0x00001091
		public void Visit(GroupingOperator node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002E99 File Offset: 0x00001099
		public void Visit(IfNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002EA1 File Offset: 0x000010A1
		public void Visit(ImportantComment node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002EA9 File Offset: 0x000010A9
		public void Visit(LabeledStatement node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002EB1 File Offset: 0x000010B1
		public void Visit(Member node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002EB9 File Offset: 0x000010B9
		public void Visit(ObjectLiteralField node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002EC1 File Offset: 0x000010C1
		public void Visit(RegExpLiteral node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002EC9 File Offset: 0x000010C9
		public void Visit(ReturnNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002ED1 File Offset: 0x000010D1
		public void Visit(Switch node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002ED9 File Offset: 0x000010D9
		public void Visit(SwitchCase node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002EE1 File Offset: 0x000010E1
		public void Visit(TemplateLiteral node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002EE9 File Offset: 0x000010E9
		public void Visit(TemplateLiteralExpression node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002EF1 File Offset: 0x000010F1
		public void Visit(ThisLiteral node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002EF9 File Offset: 0x000010F9
		public void Visit(ThrowNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002F01 File Offset: 0x00001101
		public void Visit(TryNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002F09 File Offset: 0x00001109
		public void Visit(UnaryOperator node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002F11 File Offset: 0x00001111
		public void Visit(WhileNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002F19 File Offset: 0x00001119
		public void Visit(WithNode node)
		{
			BindingsVisitor.ReportError(node);
		}

		// Token: 0x04000017 RID: 23
		private IList<BindingIdentifier> m_bindings;

		// Token: 0x04000018 RID: 24
		private IList<Lookup> m_lookups;
	}
}
