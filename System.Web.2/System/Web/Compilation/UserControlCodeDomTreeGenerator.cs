using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000869 RID: 2153
	internal class UserControlCodeDomTreeGenerator : TemplateControlCodeDomTreeGenerator
	{
		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x00165B9B File Offset: 0x00163D9B
		private UserControlParser Parser
		{
			get
			{
				return this._ucParser;
			}
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x00165BA3 File Offset: 0x00163DA3
		internal UserControlCodeDomTreeGenerator(UserControlParser ucParser) : base(ucParser)
		{
			this._ucParser = ucParser;
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x00165BB4 File Offset: 0x00163DB4
		protected override void GenerateClassAttributes()
		{
			base.GenerateClassAttributes();
			if (this._sourceDataClass != null && this.Parser.OutputCacheParameters != null)
			{
				OutputCacheParameters outputCacheParameters = this.Parser.OutputCacheParameters;
				if (outputCacheParameters.Duration > 0)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.UI.PartialCachingAttribute");
					CodeAttributeArgument value = new CodeAttributeArgument(new CodePrimitiveExpression(outputCacheParameters.Duration));
					codeAttributeDeclaration.Arguments.Add(value);
					value = new CodeAttributeArgument(new CodePrimitiveExpression(outputCacheParameters.VaryByParam));
					codeAttributeDeclaration.Arguments.Add(value);
					value = new CodeAttributeArgument(new CodePrimitiveExpression(outputCacheParameters.VaryByControl));
					codeAttributeDeclaration.Arguments.Add(value);
					value = new CodeAttributeArgument(new CodePrimitiveExpression(outputCacheParameters.VaryByCustom));
					codeAttributeDeclaration.Arguments.Add(value);
					value = new CodeAttributeArgument(new CodePrimitiveExpression(outputCacheParameters.SqlDependency));
					codeAttributeDeclaration.Arguments.Add(value);
					value = new CodeAttributeArgument(new CodePrimitiveExpression(this.Parser.FSharedPartialCaching));
					codeAttributeDeclaration.Arguments.Add(value);
					if (MultiTargetingUtil.IsTargetFramework40OrAbove)
					{
						value = new CodeAttributeArgument("ProviderName", new CodePrimitiveExpression(this.Parser.Provider));
						codeAttributeDeclaration.Arguments.Add(value);
					}
					this._sourceDataClass.CustomAttributes.Add(codeAttributeDeclaration);
				}
			}
		}

		// Token: 0x0400343F RID: 13375
		protected UserControlParser _ucParser;
	}
}
