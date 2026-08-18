using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000009 RID: 9
	public interface IVisitor
	{
		// Token: 0x06000055 RID: 85
		void Visit(ArrayLiteral node);

		// Token: 0x06000056 RID: 86
		void Visit(AspNetBlockNode node);

		// Token: 0x06000057 RID: 87
		void Visit(AstNodeList node);

		// Token: 0x06000058 RID: 88
		void Visit(BinaryOperator node);

		// Token: 0x06000059 RID: 89
		void Visit(BindingIdentifier node);

		// Token: 0x0600005A RID: 90
		void Visit(Block node);

		// Token: 0x0600005B RID: 91
		void Visit(Break node);

		// Token: 0x0600005C RID: 92
		void Visit(CallNode node);

		// Token: 0x0600005D RID: 93
		void Visit(ClassNode node);

		// Token: 0x0600005E RID: 94
		void Visit(ComprehensionNode node);

		// Token: 0x0600005F RID: 95
		void Visit(ComprehensionForClause node);

		// Token: 0x06000060 RID: 96
		void Visit(ComprehensionIfClause node);

		// Token: 0x06000061 RID: 97
		void Visit(ConditionalCompilationComment node);

		// Token: 0x06000062 RID: 98
		void Visit(ConditionalCompilationElse node);

		// Token: 0x06000063 RID: 99
		void Visit(ConditionalCompilationElseIf node);

		// Token: 0x06000064 RID: 100
		void Visit(ConditionalCompilationEnd node);

		// Token: 0x06000065 RID: 101
		void Visit(ConditionalCompilationIf node);

		// Token: 0x06000066 RID: 102
		void Visit(ConditionalCompilationOn node);

		// Token: 0x06000067 RID: 103
		void Visit(ConditionalCompilationSet node);

		// Token: 0x06000068 RID: 104
		void Visit(Conditional node);

		// Token: 0x06000069 RID: 105
		void Visit(ConstantWrapper node);

		// Token: 0x0600006A RID: 106
		void Visit(ConstantWrapperPP node);

		// Token: 0x0600006B RID: 107
		void Visit(ConstStatement node);

		// Token: 0x0600006C RID: 108
		void Visit(ContinueNode node);

		// Token: 0x0600006D RID: 109
		void Visit(CustomNode node);

		// Token: 0x0600006E RID: 110
		void Visit(DebuggerNode node);

		// Token: 0x0600006F RID: 111
		void Visit(DirectivePrologue node);

		// Token: 0x06000070 RID: 112
		void Visit(DoWhile node);

		// Token: 0x06000071 RID: 113
		void Visit(EmptyStatement node);

		// Token: 0x06000072 RID: 114
		void Visit(ExportNode node);

		// Token: 0x06000073 RID: 115
		void Visit(ForIn node);

		// Token: 0x06000074 RID: 116
		void Visit(ForNode node);

		// Token: 0x06000075 RID: 117
		void Visit(FunctionObject node);

		// Token: 0x06000076 RID: 118
		void Visit(GetterSetter node);

		// Token: 0x06000077 RID: 119
		void Visit(GroupingOperator node);

		// Token: 0x06000078 RID: 120
		void Visit(IfNode node);

		// Token: 0x06000079 RID: 121
		void Visit(ImportantComment node);

		// Token: 0x0600007A RID: 122
		void Visit(ImportExportSpecifier node);

		// Token: 0x0600007B RID: 123
		void Visit(ImportNode node);

		// Token: 0x0600007C RID: 124
		void Visit(InitializerNode node);

		// Token: 0x0600007D RID: 125
		void Visit(LabeledStatement node);

		// Token: 0x0600007E RID: 126
		void Visit(LexicalDeclaration node);

		// Token: 0x0600007F RID: 127
		void Visit(Lookup node);

		// Token: 0x06000080 RID: 128
		void Visit(Member node);

		// Token: 0x06000081 RID: 129
		void Visit(ModuleDeclaration node);

		// Token: 0x06000082 RID: 130
		void Visit(ObjectLiteral node);

		// Token: 0x06000083 RID: 131
		void Visit(ObjectLiteralField node);

		// Token: 0x06000084 RID: 132
		void Visit(ObjectLiteralProperty node);

		// Token: 0x06000085 RID: 133
		void Visit(ParameterDeclaration node);

		// Token: 0x06000086 RID: 134
		void Visit(RegExpLiteral node);

		// Token: 0x06000087 RID: 135
		void Visit(ReturnNode node);

		// Token: 0x06000088 RID: 136
		void Visit(Switch node);

		// Token: 0x06000089 RID: 137
		void Visit(SwitchCase node);

		// Token: 0x0600008A RID: 138
		void Visit(TemplateLiteral node);

		// Token: 0x0600008B RID: 139
		void Visit(TemplateLiteralExpression node);

		// Token: 0x0600008C RID: 140
		void Visit(ThisLiteral node);

		// Token: 0x0600008D RID: 141
		void Visit(ThrowNode node);

		// Token: 0x0600008E RID: 142
		void Visit(TryNode node);

		// Token: 0x0600008F RID: 143
		void Visit(Var node);

		// Token: 0x06000090 RID: 144
		void Visit(VariableDeclaration node);

		// Token: 0x06000091 RID: 145
		void Visit(UnaryOperator node);

		// Token: 0x06000092 RID: 146
		void Visit(WhileNode node);

		// Token: 0x06000093 RID: 147
		void Visit(WithNode node);
	}
}
