using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B5 RID: 181
	public class MatchPropertiesVisitor : IVisitor
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x00037600 File Offset: 0x00035800
		public bool Match(AstNode node, string identifiers)
		{
			this.m_isMatch = false;
			if (node != null && !string.IsNullOrEmpty(identifiers))
			{
				string[] array = identifiers.Split(new char[]
				{
					'.'
				});
				bool flag = true;
				foreach (string name in array)
				{
					if (!JSScanner.IsValidIdentifier(name))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this.m_parts = array;
					this.m_index = array.Length - 1;
					node.Accept(this);
				}
			}
			return this.m_isMatch;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00037680 File Offset: 0x00035880
		public void Visit(CallNode node)
		{
			if (node != null && this.m_index > 0 && node.InBrackets && node.Arguments != null && node.Arguments.Count == 1)
			{
				ConstantWrapper constantWrapper = node.Arguments[0] as ConstantWrapper;
				if (constantWrapper != null && constantWrapper.PrimitiveType == PrimitiveType.String && string.CompareOrdinal(constantWrapper.Value.ToString(), this.m_parts[this.m_index--]) == 0)
				{
					node.Function.Accept(this);
				}
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003770C File Offset: 0x0003590C
		public void Visit(Member node)
		{
			if (node != null && this.m_index > 0 && string.CompareOrdinal(node.Name, this.m_parts[this.m_index--]) == 0)
			{
				node.Root.Accept(this);
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00037758 File Offset: 0x00035958
		public void Visit(Lookup node)
		{
			if (node != null && this.m_index == 0 && string.CompareOrdinal(node.Name, this.m_parts[0]) == 0 && (node.VariableField == null || node.VariableField.FieldType == FieldType.UndefinedGlobal || node.VariableField.FieldType == FieldType.Global))
			{
				this.m_isMatch = true;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000377B1 File Offset: 0x000359B1
		public virtual void Visit(GroupingOperator node)
		{
			if (node != null && node.Operand != null)
			{
				node.Operand.Accept(this);
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000377CA File Offset: 0x000359CA
		public void Visit(ArrayLiteral node)
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000377CC File Offset: 0x000359CC
		public void Visit(AspNetBlockNode node)
		{
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000377CE File Offset: 0x000359CE
		public void Visit(AstNodeList node)
		{
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000377D0 File Offset: 0x000359D0
		public void Visit(BinaryOperator node)
		{
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000377D2 File Offset: 0x000359D2
		public void Visit(BindingIdentifier node)
		{
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000377D4 File Offset: 0x000359D4
		public void Visit(Block node)
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000377D6 File Offset: 0x000359D6
		public void Visit(Break node)
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000377D8 File Offset: 0x000359D8
		public void Visit(ClassNode node)
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000377DA File Offset: 0x000359DA
		public void Visit(ComprehensionNode node)
		{
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000377DC File Offset: 0x000359DC
		public void Visit(ComprehensionForClause node)
		{
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000377DE File Offset: 0x000359DE
		public void Visit(ComprehensionIfClause node)
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000377E0 File Offset: 0x000359E0
		public void Visit(ConditionalCompilationComment node)
		{
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000377E2 File Offset: 0x000359E2
		public void Visit(ConditionalCompilationElse node)
		{
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000377E4 File Offset: 0x000359E4
		public void Visit(ConditionalCompilationElseIf node)
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000377E6 File Offset: 0x000359E6
		public void Visit(ConditionalCompilationEnd node)
		{
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000377E8 File Offset: 0x000359E8
		public void Visit(ConditionalCompilationIf node)
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000377EA File Offset: 0x000359EA
		public void Visit(ConditionalCompilationOn node)
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000377EC File Offset: 0x000359EC
		public void Visit(ConditionalCompilationSet node)
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000377EE File Offset: 0x000359EE
		public void Visit(Conditional node)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000377F0 File Offset: 0x000359F0
		public void Visit(ConstantWrapper node)
		{
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000377F2 File Offset: 0x000359F2
		public void Visit(ConstantWrapperPP node)
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000377F4 File Offset: 0x000359F4
		public void Visit(ConstStatement node)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000377F6 File Offset: 0x000359F6
		public void Visit(ContinueNode node)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000377F8 File Offset: 0x000359F8
		public void Visit(CustomNode node)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000377FA File Offset: 0x000359FA
		public void Visit(DebuggerNode node)
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000377FC File Offset: 0x000359FC
		public void Visit(DirectivePrologue node)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000377FE File Offset: 0x000359FE
		public void Visit(DoWhile node)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00037800 File Offset: 0x00035A00
		public void Visit(EmptyStatement node)
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00037802 File Offset: 0x00035A02
		public void Visit(ExportNode node)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00037804 File Offset: 0x00035A04
		public void Visit(ForIn node)
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00037806 File Offset: 0x00035A06
		public void Visit(ForNode node)
		{
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00037808 File Offset: 0x00035A08
		public void Visit(FunctionObject node)
		{
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003780A File Offset: 0x00035A0A
		public void Visit(GetterSetter node)
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0003780C File Offset: 0x00035A0C
		public void Visit(IfNode node)
		{
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003780E File Offset: 0x00035A0E
		public void Visit(ImportantComment node)
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00037810 File Offset: 0x00035A10
		public void Visit(ImportExportSpecifier node)
		{
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00037812 File Offset: 0x00035A12
		public void Visit(ImportNode node)
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00037814 File Offset: 0x00035A14
		public void Visit(InitializerNode node)
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00037816 File Offset: 0x00035A16
		public void Visit(LabeledStatement node)
		{
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00037818 File Offset: 0x00035A18
		public void Visit(LexicalDeclaration node)
		{
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003781A File Offset: 0x00035A1A
		public void Visit(ModuleDeclaration node)
		{
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003781C File Offset: 0x00035A1C
		public void Visit(ObjectLiteral node)
		{
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003781E File Offset: 0x00035A1E
		public void Visit(ObjectLiteralField node)
		{
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00037820 File Offset: 0x00035A20
		public void Visit(ObjectLiteralProperty node)
		{
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00037822 File Offset: 0x00035A22
		public void Visit(ParameterDeclaration node)
		{
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00037824 File Offset: 0x00035A24
		public void Visit(RegExpLiteral node)
		{
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00037826 File Offset: 0x00035A26
		public void Visit(ReturnNode node)
		{
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00037828 File Offset: 0x00035A28
		public void Visit(Switch node)
		{
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0003782A File Offset: 0x00035A2A
		public void Visit(SwitchCase node)
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003782C File Offset: 0x00035A2C
		public void Visit(TemplateLiteral node)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003782E File Offset: 0x00035A2E
		public void Visit(TemplateLiteralExpression node)
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00037830 File Offset: 0x00035A30
		public void Visit(ThisLiteral node)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00037832 File Offset: 0x00035A32
		public void Visit(ThrowNode node)
		{
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00037834 File Offset: 0x00035A34
		public void Visit(TryNode node)
		{
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00037836 File Offset: 0x00035A36
		public void Visit(UnaryOperator node)
		{
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00037838 File Offset: 0x00035A38
		public void Visit(Var node)
		{
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003783A File Offset: 0x00035A3A
		public void Visit(VariableDeclaration node)
		{
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003783C File Offset: 0x00035A3C
		public void Visit(WhileNode node)
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003783E File Offset: 0x00035A3E
		public void Visit(WithNode node)
		{
		}

		// Token: 0x040004CF RID: 1231
		private string[] m_parts;

		// Token: 0x040004D0 RID: 1232
		private bool m_isMatch;

		// Token: 0x040004D1 RID: 1233
		private int m_index;
	}
}
