using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;

namespace System.Data.Design
{
	// Token: 0x0200021C RID: 540
	internal sealed class DataComponentGenerator
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x00070D61 File Offset: 0x0006EF61
		internal DataComponentGenerator(TypedDataSourceCodeGenerator codeGenerator)
		{
			this.dataSourceGenerator = codeGenerator;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00070D70 File Offset: 0x0006EF70
		internal CodeTypeDeclaration GenerateDataComponent(DesignTable designTable, bool isFunctionsComponent, bool generateHierarchicalUpdate)
		{
			string generatorDataComponentClassName = designTable.GeneratorDataComponentClassName;
			CodeTypeDeclaration codeTypeDeclaration = CodeGenHelper.Class(generatorDataComponentClassName, true, designTable.DataAccessorModifier);
			codeTypeDeclaration.BaseTypes.Add(CodeGenHelper.GlobalType(designTable.BaseClass));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerCategoryAttribute", CodeGenHelper.Str("code")));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.ToolboxItem", CodeGenHelper.Primitive(true)));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DataObjectAttribute", CodeGenHelper.Primitive(true)));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl("System.ComponentModel.DesignerAttribute", CodeGenHelper.Str(DataComponentGenerator.adapterDesigner + ", Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")));
			codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.AttributeDecl(typeof(HelpKeywordAttribute).FullName, CodeGenHelper.Str("vs.data.TableAdapter")));
			if (designTable.WebServiceAttribute)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.Web.Services.WebService");
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", CodeGenHelper.Str(designTable.WebServiceNamespace)));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Description", CodeGenHelper.Str(designTable.WebServiceDescription)));
				codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			}
			codeTypeDeclaration.Comments.Add(CodeGenHelper.Comment("Represents the connection and commands used to retrieve and save data.", true));
			DataComponentMethodGenerator dataComponentMethodGenerator = new DataComponentMethodGenerator(this.dataSourceGenerator, designTable, generateHierarchicalUpdate);
			dataComponentMethodGenerator.AddMethods(codeTypeDeclaration, isFunctionsComponent);
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			QueryHandler queryHandler = new QueryHandler(this.dataSourceGenerator, designTable);
			if (isFunctionsComponent)
			{
				queryHandler.AddFunctionsToDataComponent(codeTypeDeclaration, true);
			}
			else
			{
				queryHandler.AddQueriesToDataComponent(codeTypeDeclaration);
			}
			return codeTypeDeclaration;
		}

		// Token: 0x04000AB2 RID: 2738
		private TypedDataSourceCodeGenerator dataSourceGenerator;

		// Token: 0x04000AB3 RID: 2739
		private static string adapterDesigner = "Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner";
	}
}
