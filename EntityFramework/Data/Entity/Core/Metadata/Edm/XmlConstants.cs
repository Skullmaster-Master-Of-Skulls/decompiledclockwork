using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000529 RID: 1321
	internal static class XmlConstants
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x000EF1C8 File Offset: 0x000ED3C8
		public static string GetCsdlNamespace(double edmVersion)
		{
			if (object.Equals(edmVersion, 1.0))
			{
				return "http://schemas.microsoft.com/ado/2006/04/edm";
			}
			if (object.Equals(edmVersion, 1.1))
			{
				return "http://schemas.microsoft.com/ado/2007/05/edm";
			}
			if (object.Equals(edmVersion, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2008/09/edm";
			}
			return "http://schemas.microsoft.com/ado/2009/11/edm";
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000EF240 File Offset: 0x000ED440
		public static string GetSsdlNamespace(double edmVersion)
		{
			if (object.Equals(edmVersion, 1.0))
			{
				return "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";
			}
			if (object.Equals(edmVersion, 2.0))
			{
				return "http://schemas.microsoft.com/ado/2009/02/edm/ssdl";
			}
			return "http://schemas.microsoft.com/ado/2009/11/edm/ssdl";
		}

		// Token: 0x040012CD RID: 4813
		internal const string CustomAnnotationNamespace = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation";

		// Token: 0x040012CE RID: 4814
		internal const string CustomAnnotationPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:";

		// Token: 0x040012CF RID: 4815
		internal const string ClrTypeAnnotation = "ClrType";

		// Token: 0x040012D0 RID: 4816
		internal const string ClrTypeAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:ClrType";

		// Token: 0x040012D1 RID: 4817
		internal const string UseClrTypesAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes";

		// Token: 0x040012D2 RID: 4818
		internal const string IndexAnnotationWithPrefix = "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index";

		// Token: 0x040012D3 RID: 4819
		internal const string ModelNamespace_1 = "http://schemas.microsoft.com/ado/2006/04/edm";

		// Token: 0x040012D4 RID: 4820
		internal const string ModelNamespace_1_1 = "http://schemas.microsoft.com/ado/2007/05/edm";

		// Token: 0x040012D5 RID: 4821
		internal const string ModelNamespace_2 = "http://schemas.microsoft.com/ado/2008/09/edm";

		// Token: 0x040012D6 RID: 4822
		internal const string ModelNamespace_3 = "http://schemas.microsoft.com/ado/2009/11/edm";

		// Token: 0x040012D7 RID: 4823
		internal const string ProviderManifestNamespace = "http://schemas.microsoft.com/ado/2006/04/edm/providermanifest";

		// Token: 0x040012D8 RID: 4824
		internal const string TargetNamespace_1 = "http://schemas.microsoft.com/ado/2006/04/edm/ssdl";

		// Token: 0x040012D9 RID: 4825
		internal const string TargetNamespace_2 = "http://schemas.microsoft.com/ado/2009/02/edm/ssdl";

		// Token: 0x040012DA RID: 4826
		internal const string TargetNamespace_3 = "http://schemas.microsoft.com/ado/2009/11/edm/ssdl";

		// Token: 0x040012DB RID: 4827
		internal const string CodeGenerationSchemaNamespace = "http://schemas.microsoft.com/ado/2006/04/codegeneration";

		// Token: 0x040012DC RID: 4828
		internal const string EntityStoreSchemaGeneratorNamespace = "http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator";

		// Token: 0x040012DD RID: 4829
		internal const string AnnotationNamespace = "http://schemas.microsoft.com/ado/2009/02/edm/annotation";

		// Token: 0x040012DE RID: 4830
		internal const string StoreGeneratedPatternAnnotation = "http://schemas.microsoft.com/ado/2009/02/edm/annotation:StoreGeneratedPattern";

		// Token: 0x040012DF RID: 4831
		internal const string Alias = "Alias";

		// Token: 0x040012E0 RID: 4832
		internal const string Self = "Self";

		// Token: 0x040012E1 RID: 4833
		internal const string Provider = "Provider";

		// Token: 0x040012E2 RID: 4834
		internal const string ProviderManifestToken = "ProviderManifestToken";

		// Token: 0x040012E3 RID: 4835
		internal const string CSSpaceSchemaExtension = ".msl";

		// Token: 0x040012E4 RID: 4836
		internal const string CSpaceSchemaExtension = ".csdl";

		// Token: 0x040012E5 RID: 4837
		internal const string SSpaceSchemaExtension = ".ssdl";

		// Token: 0x040012E6 RID: 4838
		internal const double UndefinedVersion = 0.0;

		// Token: 0x040012E7 RID: 4839
		internal const double EdmVersionForV1 = 1.0;

		// Token: 0x040012E8 RID: 4840
		internal const double EdmVersionForV1_1 = 1.1;

		// Token: 0x040012E9 RID: 4841
		internal const double EdmVersionForV2 = 2.0;

		// Token: 0x040012EA RID: 4842
		internal const double EdmVersionForV3 = 3.0;

		// Token: 0x040012EB RID: 4843
		internal const double SchemaVersionLatest = 3.0;

		// Token: 0x040012EC RID: 4844
		internal const double StoreVersionForV1 = 1.0;

		// Token: 0x040012ED RID: 4845
		internal const double StoreVersionForV2 = 2.0;

		// Token: 0x040012EE RID: 4846
		internal const double StoreVersionForV3 = 3.0;

		// Token: 0x040012EF RID: 4847
		internal const string Association = "Association";

		// Token: 0x040012F0 RID: 4848
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x040012F1 RID: 4849
		internal const string ComplexType = "ComplexType";

		// Token: 0x040012F2 RID: 4850
		internal const string DefiningQuery = "DefiningQuery";

		// Token: 0x040012F3 RID: 4851
		internal const string DefiningExpression = "DefiningExpression";

		// Token: 0x040012F4 RID: 4852
		internal const string Documentation = "Documentation";

		// Token: 0x040012F5 RID: 4853
		internal const string DependentRole = "Dependent";

		// Token: 0x040012F6 RID: 4854
		internal const string End = "End";

		// Token: 0x040012F7 RID: 4855
		internal const string EntityType = "EntityType";

		// Token: 0x040012F8 RID: 4856
		internal const string EntityContainer = "EntityContainer";

		// Token: 0x040012F9 RID: 4857
		internal const string FunctionImport = "FunctionImport";

		// Token: 0x040012FA RID: 4858
		internal const string Key = "Key";

		// Token: 0x040012FB RID: 4859
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x040012FC RID: 4860
		internal const string OnDelete = "OnDelete";

		// Token: 0x040012FD RID: 4861
		internal const string PrincipalRole = "Principal";

		// Token: 0x040012FE RID: 4862
		internal const string Property = "Property";

		// Token: 0x040012FF RID: 4863
		internal const string PropertyRef = "PropertyRef";

		// Token: 0x04001300 RID: 4864
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04001301 RID: 4865
		internal const string Role = "Role";

		// Token: 0x04001302 RID: 4866
		internal const string Schema = "Schema";

		// Token: 0x04001303 RID: 4867
		internal const string Summary = "Summary";

		// Token: 0x04001304 RID: 4868
		internal const string LongDescription = "LongDescription";

		// Token: 0x04001305 RID: 4869
		internal const string SampleValue = "SampleValue";

		// Token: 0x04001306 RID: 4870
		internal const string EnumType = "EnumType";

		// Token: 0x04001307 RID: 4871
		internal const string Member = "Member";

		// Token: 0x04001308 RID: 4872
		internal const string ValueTerm = "ValueTerm";

		// Token: 0x04001309 RID: 4873
		internal const string Annotations = "Annotations";

		// Token: 0x0400130A RID: 4874
		internal const string ValueAnnotation = "ValueAnnotation";

		// Token: 0x0400130B RID: 4875
		internal const string TypeAnnotation = "TypeAnnotation";

		// Token: 0x0400130C RID: 4876
		internal const string Using = "Using";

		// Token: 0x0400130D RID: 4877
		internal const string TypeAccess = "TypeAccess";

		// Token: 0x0400130E RID: 4878
		internal const string MethodAccess = "MethodAccess";

		// Token: 0x0400130F RID: 4879
		internal const string SetterAccess = "SetterAccess";

		// Token: 0x04001310 RID: 4880
		internal const string GetterAccess = "GetterAccess";

		// Token: 0x04001311 RID: 4881
		internal const string Abstract = "Abstract";

		// Token: 0x04001312 RID: 4882
		internal const string OpenType = "OpenType";

		// Token: 0x04001313 RID: 4883
		internal const string Action = "Action";

		// Token: 0x04001314 RID: 4884
		internal const string BaseType = "BaseType";

		// Token: 0x04001315 RID: 4885
		internal const string EntitySet = "EntitySet";

		// Token: 0x04001316 RID: 4886
		internal const string EntitySetPath = "EntitySetPath";

		// Token: 0x04001317 RID: 4887
		internal const string Extends = "Extends";

		// Token: 0x04001318 RID: 4888
		internal const string FromRole = "FromRole";

		// Token: 0x04001319 RID: 4889
		internal const string Multiplicity = "Multiplicity";

		// Token: 0x0400131A RID: 4890
		internal const string Name = "Name";

		// Token: 0x0400131B RID: 4891
		internal const string Namespace = "Namespace";

		// Token: 0x0400131C RID: 4892
		internal const string Table = "Table";

		// Token: 0x0400131D RID: 4893
		internal const string ToRole = "ToRole";

		// Token: 0x0400131E RID: 4894
		internal const string Relationship = "Relationship";

		// Token: 0x0400131F RID: 4895
		internal const string ElementType = "ElementType";

		// Token: 0x04001320 RID: 4896
		internal const string StoreGeneratedPattern = "StoreGeneratedPattern";

		// Token: 0x04001321 RID: 4897
		internal const string IsFlags = "IsFlags";

		// Token: 0x04001322 RID: 4898
		internal const string IsBindable = "IsBindable";

		// Token: 0x04001323 RID: 4899
		internal const string IsSideEffecting = "IsSideEffecting";

		// Token: 0x04001324 RID: 4900
		internal const string UnderlyingType = "UnderlyingType";

		// Token: 0x04001325 RID: 4901
		internal const string Value = "Value";

		// Token: 0x04001326 RID: 4902
		internal const string ContainsTarget = "ContainsTarget";

		// Token: 0x04001327 RID: 4903
		internal const string Max = "Max";

		// Token: 0x04001328 RID: 4904
		internal const string None = "None";

		// Token: 0x04001329 RID: 4905
		internal const string Identity = "Identity";

		// Token: 0x0400132A RID: 4906
		internal const string Computed = "Computed";

		// Token: 0x0400132B RID: 4907
		internal const string Fixed = "Fixed";

		// Token: 0x0400132C RID: 4908
		internal const string CollectionKind_None = "None";

		// Token: 0x0400132D RID: 4909
		internal const string CollectionKind_List = "List";

		// Token: 0x0400132E RID: 4910
		internal const string CollectionKind_Bag = "Bag";

		// Token: 0x0400132F RID: 4911
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x04001330 RID: 4912
		internal const string In = "In";

		// Token: 0x04001331 RID: 4913
		internal const string Out = "Out";

		// Token: 0x04001332 RID: 4914
		internal const string InOut = "InOut";

		// Token: 0x04001333 RID: 4915
		internal const string Variable = "Variable";

		// Token: 0x04001334 RID: 4916
		internal const string True = "true";

		// Token: 0x04001335 RID: 4917
		internal const string False = "false";

		// Token: 0x04001336 RID: 4918
		internal const string Function = "Function";

		// Token: 0x04001337 RID: 4919
		internal const string ReturnType = "ReturnType";

		// Token: 0x04001338 RID: 4920
		internal const string Parameter = "Parameter";

		// Token: 0x04001339 RID: 4921
		internal const string Mode = "Mode";

		// Token: 0x0400133A RID: 4922
		internal const string StoreFunctionName = "StoreFunctionName";

		// Token: 0x0400133B RID: 4923
		internal const string ProviderManifestElement = "ProviderManifest";

		// Token: 0x0400133C RID: 4924
		internal const string TypesElement = "Types";

		// Token: 0x0400133D RID: 4925
		internal const string FunctionsElement = "Functions";

		// Token: 0x0400133E RID: 4926
		internal const string TypeElement = "Type";

		// Token: 0x0400133F RID: 4927
		internal const string FunctionElement = "Function";

		// Token: 0x04001340 RID: 4928
		internal const string ScaleElement = "Scale";

		// Token: 0x04001341 RID: 4929
		internal const string PrecisionElement = "Precision";

		// Token: 0x04001342 RID: 4930
		internal const string MaxLengthElement = "MaxLength";

		// Token: 0x04001343 RID: 4931
		internal const string FacetDescriptionsElement = "FacetDescriptions";

		// Token: 0x04001344 RID: 4932
		internal const string UnicodeElement = "Unicode";

		// Token: 0x04001345 RID: 4933
		internal const string FixedLengthElement = "FixedLength";

		// Token: 0x04001346 RID: 4934
		internal const string ReturnTypeElement = "ReturnType";

		// Token: 0x04001347 RID: 4935
		internal const string SridElement = "SRID";

		// Token: 0x04001348 RID: 4936
		internal const string IsStrictElement = "IsStrict";

		// Token: 0x04001349 RID: 4937
		internal const string TypeAttribute = "Type";

		// Token: 0x0400134A RID: 4938
		internal const string MinimumAttribute = "Minimum";

		// Token: 0x0400134B RID: 4939
		internal const string MaximumAttribute = "Maximum";

		// Token: 0x0400134C RID: 4940
		internal const string NamespaceAttribute = "Namespace";

		// Token: 0x0400134D RID: 4941
		internal const string DefaultValueAttribute = "DefaultValue";

		// Token: 0x0400134E RID: 4942
		internal const string ConstantAttribute = "Constant";

		// Token: 0x0400134F RID: 4943
		internal const string DestinationTypeAttribute = "DestinationType";

		// Token: 0x04001350 RID: 4944
		internal const string PrimitiveTypeKindAttribute = "PrimitiveTypeKind";

		// Token: 0x04001351 RID: 4945
		internal const string AggregateAttribute = "Aggregate";

		// Token: 0x04001352 RID: 4946
		internal const string BuiltInAttribute = "BuiltIn";

		// Token: 0x04001353 RID: 4947
		internal const string NameAttribute = "Name";

		// Token: 0x04001354 RID: 4948
		internal const string IgnoreFacetsAttribute = "IgnoreFacets";

		// Token: 0x04001355 RID: 4949
		internal const string NiladicFunction = "NiladicFunction";

		// Token: 0x04001356 RID: 4950
		internal const string IsComposable = "IsComposable";

		// Token: 0x04001357 RID: 4951
		internal const string CommandText = "CommandText";

		// Token: 0x04001358 RID: 4952
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x04001359 RID: 4953
		internal const string CollectionType = "CollectionType";

		// Token: 0x0400135A RID: 4954
		internal const string ReferenceType = "ReferenceType";

		// Token: 0x0400135B RID: 4955
		internal const string RowType = "RowType";

		// Token: 0x0400135C RID: 4956
		internal const string TypeRef = "TypeRef";

		// Token: 0x0400135D RID: 4957
		internal const string UseStrongSpatialTypes = "UseStrongSpatialTypes";

		// Token: 0x0400135E RID: 4958
		internal const string XmlCommentStartString = "<!--";

		// Token: 0x0400135F RID: 4959
		internal const string XmlCommentEndString = "-->";
	}
}
