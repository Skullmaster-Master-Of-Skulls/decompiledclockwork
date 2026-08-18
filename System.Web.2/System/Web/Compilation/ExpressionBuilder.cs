using System;
using System.CodeDom;
using System.ComponentModel.Design;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.Design;

namespace System.Web.Compilation
{
	// Token: 0x0200083A RID: 2106
	public abstract class ExpressionBuilder
	{
		// Token: 0x06006481 RID: 25729 RVA: 0x0016069C File Offset: 0x0015E89C
		internal virtual void BuildExpression(BoundPropertyEntry bpe, ControlBuilder controlBuilder, CodeExpression controlReference, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, ref bool hasTempObject)
		{
			CodeExpression codeExpression = this.GetCodeExpression(bpe, bpe.ParsedExpressionData, new ExpressionBuilderContext(controlBuilder.VirtualPath));
			CodeDomUtility.CreatePropertySetStatements(methodStatements, statements, controlReference, bpe.Name, bpe.Type, codeExpression, linePragma);
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001606DB File Offset: 0x0015E8DB
		internal static ExpressionBuilder GetExpressionBuilder(string expressionPrefix, VirtualPath virtualPath)
		{
			return ExpressionBuilder.GetExpressionBuilder(expressionPrefix, virtualPath, null);
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x001606E8 File Offset: 0x0015E8E8
		internal static ExpressionBuilder GetExpressionBuilder(string expressionPrefix, VirtualPath virtualPath, IDesignerHost host)
		{
			if (expressionPrefix.Length == 0)
			{
				if (ExpressionBuilder.dataBindingExpressionBuilder == null)
				{
					ExpressionBuilder.dataBindingExpressionBuilder = new DataBindingExpressionBuilder();
				}
				return ExpressionBuilder.dataBindingExpressionBuilder;
			}
			CompilationSection compilationSection = null;
			if (host != null)
			{
				IWebApplication webApplication = (IWebApplication)host.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					compilationSection = (webApplication.OpenWebConfiguration(true).GetSection("system.web/compilation") as CompilationSection);
				}
			}
			if (compilationSection == null)
			{
				compilationSection = MTConfigUtil.GetCompilationConfig(virtualPath);
			}
			ExpressionBuilder expressionBuilder = compilationSection.ExpressionBuilders[expressionPrefix];
			if (expressionBuilder == null)
			{
				throw new HttpParseException(SR.GetString("InvalidExpressionPrefix", new object[]
				{
					expressionPrefix
				}));
			}
			Type type = null;
			if (host != null)
			{
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)host.GetService(typeof(ITypeResolutionService));
				if (typeResolutionService != null)
				{
					type = typeResolutionService.GetType(expressionBuilder.Type);
				}
			}
			if (type == null)
			{
				type = expressionBuilder.TypeInternal;
			}
			if (!typeof(ExpressionBuilder).IsAssignableFrom(type))
			{
				throw new HttpParseException(SR.GetString("ExpressionBuilder_InvalidType", new object[]
				{
					type.FullName
				}));
			}
			return (ExpressionBuilder)HttpRuntime.FastCreatePublicInstance(type);
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool SupportsEvaluate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			return null;
		}

		// Token: 0x06006486 RID: 25734
		public abstract CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context);

		// Token: 0x06006487 RID: 25735 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return null;
		}

		// Token: 0x040033E3 RID: 13283
		private static ExpressionBuilder dataBindingExpressionBuilder;
	}
}
