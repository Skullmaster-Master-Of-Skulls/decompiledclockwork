using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Linq.Expressions
{
	// Token: 0x02000275 RID: 629
	internal sealed class SR
	{
		// Token: 0x06001671 RID: 5745 RVA: 0x0004A194 File Offset: 0x00048394
		internal SR()
		{
			this.resources = new ResourceManager("System.Linq.Expressions", base.GetType().Assembly);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0004A1B8 File Offset: 0x000483B8
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004A1E4 File Offset: 0x000483E4
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0004A1E7 File Offset: 0x000483E7
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0004A1F4 File Offset: 0x000483F4
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0004A274 File Offset: 0x00048474
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0004A29D File Offset: 0x0004849D
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0004A2A8 File Offset: 0x000484A8
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x04000A6E RID: 2670
		internal const string MethodPreconditionViolated = "MethodPreconditionViolated";

		// Token: 0x04000A6F RID: 2671
		internal const string InvalidArgumentValue = "InvalidArgumentValue";

		// Token: 0x04000A70 RID: 2672
		internal const string NonEmptyCollectionRequired = "NonEmptyCollectionRequired";

		// Token: 0x04000A71 RID: 2673
		internal const string ArgCntMustBeGreaterThanNameCnt = "ArgCntMustBeGreaterThanNameCnt";

		// Token: 0x04000A72 RID: 2674
		internal const string ReducibleMustOverrideReduce = "ReducibleMustOverrideReduce";

		// Token: 0x04000A73 RID: 2675
		internal const string MustReduceToDifferent = "MustReduceToDifferent";

		// Token: 0x04000A74 RID: 2676
		internal const string ReducedNotCompatible = "ReducedNotCompatible";

		// Token: 0x04000A75 RID: 2677
		internal const string SetterHasNoParams = "SetterHasNoParams";

		// Token: 0x04000A76 RID: 2678
		internal const string PropertyCannotHaveRefType = "PropertyCannotHaveRefType";

		// Token: 0x04000A77 RID: 2679
		internal const string IndexesOfSetGetMustMatch = "IndexesOfSetGetMustMatch";

		// Token: 0x04000A78 RID: 2680
		internal const string AccessorsCannotHaveVarArgs = "AccessorsCannotHaveVarArgs";

		// Token: 0x04000A79 RID: 2681
		internal const string AccessorsCannotHaveByRefArgs = "AccessorsCannotHaveByRefArgs";

		// Token: 0x04000A7A RID: 2682
		internal const string BoundsCannotBeLessThanOne = "BoundsCannotBeLessThanOne";

		// Token: 0x04000A7B RID: 2683
		internal const string TypeMustNotBeByRef = "TypeMustNotBeByRef";

		// Token: 0x04000A7C RID: 2684
		internal const string TypeDoesNotHaveConstructorForTheSignature = "TypeDoesNotHaveConstructorForTheSignature";

		// Token: 0x04000A7D RID: 2685
		internal const string CountCannotBeNegative = "CountCannotBeNegative";

		// Token: 0x04000A7E RID: 2686
		internal const string ArrayTypeMustBeArray = "ArrayTypeMustBeArray";

		// Token: 0x04000A7F RID: 2687
		internal const string SetterMustBeVoid = "SetterMustBeVoid";

		// Token: 0x04000A80 RID: 2688
		internal const string PropertyTyepMustMatchSetter = "PropertyTyepMustMatchSetter";

		// Token: 0x04000A81 RID: 2689
		internal const string BothAccessorsMustBeStatic = "BothAccessorsMustBeStatic";

		// Token: 0x04000A82 RID: 2690
		internal const string OnlyStaticFieldsHaveNullInstance = "OnlyStaticFieldsHaveNullInstance";

		// Token: 0x04000A83 RID: 2691
		internal const string OnlyStaticPropertiesHaveNullInstance = "OnlyStaticPropertiesHaveNullInstance";

		// Token: 0x04000A84 RID: 2692
		internal const string OnlyStaticMethodsHaveNullInstance = "OnlyStaticMethodsHaveNullInstance";

		// Token: 0x04000A85 RID: 2693
		internal const string PropertyTypeCannotBeVoid = "PropertyTypeCannotBeVoid";

		// Token: 0x04000A86 RID: 2694
		internal const string InvalidUnboxType = "InvalidUnboxType";

		// Token: 0x04000A87 RID: 2695
		internal const string ExpressionMustBeReadable = "ExpressionMustBeReadable";

		// Token: 0x04000A88 RID: 2696
		internal const string ExpressionMustBeWriteable = "ExpressionMustBeWriteable";

		// Token: 0x04000A89 RID: 2697
		internal const string ArgumentMustNotHaveValueType = "ArgumentMustNotHaveValueType";

		// Token: 0x04000A8A RID: 2698
		internal const string MustBeReducible = "MustBeReducible";

		// Token: 0x04000A8B RID: 2699
		internal const string AllTestValuesMustHaveSameType = "AllTestValuesMustHaveSameType";

		// Token: 0x04000A8C RID: 2700
		internal const string AllCaseBodiesMustHaveSameType = "AllCaseBodiesMustHaveSameType";

		// Token: 0x04000A8D RID: 2701
		internal const string DefaultBodyMustBeSupplied = "DefaultBodyMustBeSupplied";

		// Token: 0x04000A8E RID: 2702
		internal const string MethodBuilderDoesNotHaveTypeBuilder = "MethodBuilderDoesNotHaveTypeBuilder";

		// Token: 0x04000A8F RID: 2703
		internal const string TypeMustBeDerivedFromSystemDelegate = "TypeMustBeDerivedFromSystemDelegate";

		// Token: 0x04000A90 RID: 2704
		internal const string ArgumentTypeCannotBeVoid = "ArgumentTypeCannotBeVoid";

		// Token: 0x04000A91 RID: 2705
		internal const string LabelMustBeVoidOrHaveExpression = "LabelMustBeVoidOrHaveExpression";

		// Token: 0x04000A92 RID: 2706
		internal const string LabelTypeMustBeVoid = "LabelTypeMustBeVoid";

		// Token: 0x04000A93 RID: 2707
		internal const string QuotedExpressionMustBeLambda = "QuotedExpressionMustBeLambda";

		// Token: 0x04000A94 RID: 2708
		internal const string VariableMustNotBeByRef = "VariableMustNotBeByRef";

		// Token: 0x04000A95 RID: 2709
		internal const string DuplicateVariable = "DuplicateVariable";

		// Token: 0x04000A96 RID: 2710
		internal const string StartEndMustBeOrdered = "StartEndMustBeOrdered";

		// Token: 0x04000A97 RID: 2711
		internal const string FaultCannotHaveCatchOrFinally = "FaultCannotHaveCatchOrFinally";

		// Token: 0x04000A98 RID: 2712
		internal const string TryMustHaveCatchFinallyOrFault = "TryMustHaveCatchFinallyOrFault";

		// Token: 0x04000A99 RID: 2713
		internal const string BodyOfCatchMustHaveSameTypeAsBodyOfTry = "BodyOfCatchMustHaveSameTypeAsBodyOfTry";

		// Token: 0x04000A9A RID: 2714
		internal const string ExtensionNodeMustOverrideProperty = "ExtensionNodeMustOverrideProperty";

		// Token: 0x04000A9B RID: 2715
		internal const string UserDefinedOperatorMustBeStatic = "UserDefinedOperatorMustBeStatic";

		// Token: 0x04000A9C RID: 2716
		internal const string UserDefinedOperatorMustNotBeVoid = "UserDefinedOperatorMustNotBeVoid";

		// Token: 0x04000A9D RID: 2717
		internal const string CoercionOperatorNotDefined = "CoercionOperatorNotDefined";

		// Token: 0x04000A9E RID: 2718
		internal const string DynamicBinderResultNotAssignable = "DynamicBinderResultNotAssignable";

		// Token: 0x04000A9F RID: 2719
		internal const string DynamicObjectResultNotAssignable = "DynamicObjectResultNotAssignable";

		// Token: 0x04000AA0 RID: 2720
		internal const string DynamicBindingNeedsRestrictions = "DynamicBindingNeedsRestrictions";

		// Token: 0x04000AA1 RID: 2721
		internal const string BinderNotCompatibleWithCallSite = "BinderNotCompatibleWithCallSite";

		// Token: 0x04000AA2 RID: 2722
		internal const string UnaryOperatorNotDefined = "UnaryOperatorNotDefined";

		// Token: 0x04000AA3 RID: 2723
		internal const string BinaryOperatorNotDefined = "BinaryOperatorNotDefined";

		// Token: 0x04000AA4 RID: 2724
		internal const string ReferenceEqualityNotDefined = "ReferenceEqualityNotDefined";

		// Token: 0x04000AA5 RID: 2725
		internal const string OperandTypesDoNotMatchParameters = "OperandTypesDoNotMatchParameters";

		// Token: 0x04000AA6 RID: 2726
		internal const string OverloadOperatorTypeDoesNotMatchConversionType = "OverloadOperatorTypeDoesNotMatchConversionType";

		// Token: 0x04000AA7 RID: 2727
		internal const string ConversionIsNotSupportedForArithmeticTypes = "ConversionIsNotSupportedForArithmeticTypes";

		// Token: 0x04000AA8 RID: 2728
		internal const string ArgumentMustBeArray = "ArgumentMustBeArray";

		// Token: 0x04000AA9 RID: 2729
		internal const string ArgumentMustBeBoolean = "ArgumentMustBeBoolean";

		// Token: 0x04000AAA RID: 2730
		internal const string EqualityMustReturnBoolean = "EqualityMustReturnBoolean";

		// Token: 0x04000AAB RID: 2731
		internal const string ArgumentMustBeFieldInfoOrPropertInfo = "ArgumentMustBeFieldInfoOrPropertInfo";

		// Token: 0x04000AAC RID: 2732
		internal const string ArgumentMustBeFieldInfoOrPropertInfoOrMethod = "ArgumentMustBeFieldInfoOrPropertInfoOrMethod";

		// Token: 0x04000AAD RID: 2733
		internal const string ArgumentMustBeInstanceMember = "ArgumentMustBeInstanceMember";

		// Token: 0x04000AAE RID: 2734
		internal const string ArgumentMustBeInteger = "ArgumentMustBeInteger";

		// Token: 0x04000AAF RID: 2735
		internal const string ArgumentMustBeArrayIndexType = "ArgumentMustBeArrayIndexType";

		// Token: 0x04000AB0 RID: 2736
		internal const string ArgumentMustBeSingleDimensionalArrayType = "ArgumentMustBeSingleDimensionalArrayType";

		// Token: 0x04000AB1 RID: 2737
		internal const string ArgumentTypesMustMatch = "ArgumentTypesMustMatch";

		// Token: 0x04000AB2 RID: 2738
		internal const string CannotAutoInitializeValueTypeElementThroughProperty = "CannotAutoInitializeValueTypeElementThroughProperty";

		// Token: 0x04000AB3 RID: 2739
		internal const string CannotAutoInitializeValueTypeMemberThroughProperty = "CannotAutoInitializeValueTypeMemberThroughProperty";

		// Token: 0x04000AB4 RID: 2740
		internal const string IncorrectTypeForTypeAs = "IncorrectTypeForTypeAs";

		// Token: 0x04000AB5 RID: 2741
		internal const string CoalesceUsedOnNonNullType = "CoalesceUsedOnNonNullType";

		// Token: 0x04000AB6 RID: 2742
		internal const string ExpressionTypeCannotInitializeArrayType = "ExpressionTypeCannotInitializeArrayType";

		// Token: 0x04000AB7 RID: 2743
		internal const string ExpressionTypeDoesNotMatchConstructorParameter = "ExpressionTypeDoesNotMatchConstructorParameter";

		// Token: 0x04000AB8 RID: 2744
		internal const string ArgumentTypeDoesNotMatchMember = "ArgumentTypeDoesNotMatchMember";

		// Token: 0x04000AB9 RID: 2745
		internal const string ArgumentMemberNotDeclOnType = "ArgumentMemberNotDeclOnType";

		// Token: 0x04000ABA RID: 2746
		internal const string ExpressionTypeDoesNotMatchMethodParameter = "ExpressionTypeDoesNotMatchMethodParameter";

		// Token: 0x04000ABB RID: 2747
		internal const string ExpressionTypeDoesNotMatchParameter = "ExpressionTypeDoesNotMatchParameter";

		// Token: 0x04000ABC RID: 2748
		internal const string ExpressionTypeDoesNotMatchReturn = "ExpressionTypeDoesNotMatchReturn";

		// Token: 0x04000ABD RID: 2749
		internal const string ExpressionTypeDoesNotMatchAssignment = "ExpressionTypeDoesNotMatchAssignment";

		// Token: 0x04000ABE RID: 2750
		internal const string ExpressionTypeDoesNotMatchLabel = "ExpressionTypeDoesNotMatchLabel";

		// Token: 0x04000ABF RID: 2751
		internal const string ExpressionTypeNotInvocable = "ExpressionTypeNotInvocable";

		// Token: 0x04000AC0 RID: 2752
		internal const string FieldNotDefinedForType = "FieldNotDefinedForType";

		// Token: 0x04000AC1 RID: 2753
		internal const string InstanceFieldNotDefinedForType = "InstanceFieldNotDefinedForType";

		// Token: 0x04000AC2 RID: 2754
		internal const string FieldInfoNotDefinedForType = "FieldInfoNotDefinedForType";

		// Token: 0x04000AC3 RID: 2755
		internal const string IncorrectNumberOfIndexes = "IncorrectNumberOfIndexes";

		// Token: 0x04000AC4 RID: 2756
		internal const string IncorrectNumberOfLambdaArguments = "IncorrectNumberOfLambdaArguments";

		// Token: 0x04000AC5 RID: 2757
		internal const string IncorrectNumberOfLambdaDeclarationParameters = "IncorrectNumberOfLambdaDeclarationParameters";

		// Token: 0x04000AC6 RID: 2758
		internal const string IncorrectNumberOfMethodCallArguments = "IncorrectNumberOfMethodCallArguments";

		// Token: 0x04000AC7 RID: 2759
		internal const string IncorrectNumberOfConstructorArguments = "IncorrectNumberOfConstructorArguments";

		// Token: 0x04000AC8 RID: 2760
		internal const string IncorrectNumberOfMembersForGivenConstructor = "IncorrectNumberOfMembersForGivenConstructor";

		// Token: 0x04000AC9 RID: 2761
		internal const string IncorrectNumberOfArgumentsForMembers = "IncorrectNumberOfArgumentsForMembers";

		// Token: 0x04000ACA RID: 2762
		internal const string LambdaTypeMustBeDerivedFromSystemDelegate = "LambdaTypeMustBeDerivedFromSystemDelegate";

		// Token: 0x04000ACB RID: 2763
		internal const string MemberNotFieldOrProperty = "MemberNotFieldOrProperty";

		// Token: 0x04000ACC RID: 2764
		internal const string MethodContainsGenericParameters = "MethodContainsGenericParameters";

		// Token: 0x04000ACD RID: 2765
		internal const string MethodIsGeneric = "MethodIsGeneric";

		// Token: 0x04000ACE RID: 2766
		internal const string MethodNotPropertyAccessor = "MethodNotPropertyAccessor";

		// Token: 0x04000ACF RID: 2767
		internal const string PropertyDoesNotHaveGetter = "PropertyDoesNotHaveGetter";

		// Token: 0x04000AD0 RID: 2768
		internal const string PropertyDoesNotHaveSetter = "PropertyDoesNotHaveSetter";

		// Token: 0x04000AD1 RID: 2769
		internal const string PropertyDoesNotHaveAccessor = "PropertyDoesNotHaveAccessor";

		// Token: 0x04000AD2 RID: 2770
		internal const string NotAMemberOfType = "NotAMemberOfType";

		// Token: 0x04000AD3 RID: 2771
		internal const string OperatorNotImplementedForType = "OperatorNotImplementedForType";

		// Token: 0x04000AD4 RID: 2772
		internal const string ParameterExpressionNotValidAsDelegate = "ParameterExpressionNotValidAsDelegate";

		// Token: 0x04000AD5 RID: 2773
		internal const string PropertyNotDefinedForType = "PropertyNotDefinedForType";

		// Token: 0x04000AD6 RID: 2774
		internal const string InstancePropertyNotDefinedForType = "InstancePropertyNotDefinedForType";

		// Token: 0x04000AD7 RID: 2775
		internal const string InstancePropertyWithoutParameterNotDefinedForType = "InstancePropertyWithoutParameterNotDefinedForType";

		// Token: 0x04000AD8 RID: 2776
		internal const string InstancePropertyWithSpecifiedParametersNotDefinedForType = "InstancePropertyWithSpecifiedParametersNotDefinedForType";

		// Token: 0x04000AD9 RID: 2777
		internal const string InstanceAndMethodTypeMismatch = "InstanceAndMethodTypeMismatch";

		// Token: 0x04000ADA RID: 2778
		internal const string TypeContainsGenericParameters = "TypeContainsGenericParameters";

		// Token: 0x04000ADB RID: 2779
		internal const string TypeIsGeneric = "TypeIsGeneric";

		// Token: 0x04000ADC RID: 2780
		internal const string TypeMissingDefaultConstructor = "TypeMissingDefaultConstructor";

		// Token: 0x04000ADD RID: 2781
		internal const string ListInitializerWithZeroMembers = "ListInitializerWithZeroMembers";

		// Token: 0x04000ADE RID: 2782
		internal const string ElementInitializerMethodNotAdd = "ElementInitializerMethodNotAdd";

		// Token: 0x04000ADF RID: 2783
		internal const string ElementInitializerMethodNoRefOutParam = "ElementInitializerMethodNoRefOutParam";

		// Token: 0x04000AE0 RID: 2784
		internal const string ElementInitializerMethodWithZeroArgs = "ElementInitializerMethodWithZeroArgs";

		// Token: 0x04000AE1 RID: 2785
		internal const string ElementInitializerMethodStatic = "ElementInitializerMethodStatic";

		// Token: 0x04000AE2 RID: 2786
		internal const string TypeNotIEnumerable = "TypeNotIEnumerable";

		// Token: 0x04000AE3 RID: 2787
		internal const string TypeParameterIsNotDelegate = "TypeParameterIsNotDelegate";

		// Token: 0x04000AE4 RID: 2788
		internal const string UnexpectedCoalesceOperator = "UnexpectedCoalesceOperator";

		// Token: 0x04000AE5 RID: 2789
		internal const string InvalidCast = "InvalidCast";

		// Token: 0x04000AE6 RID: 2790
		internal const string UnhandledBinary = "UnhandledBinary";

		// Token: 0x04000AE7 RID: 2791
		internal const string UnhandledBinding = "UnhandledBinding";

		// Token: 0x04000AE8 RID: 2792
		internal const string UnhandledBindingType = "UnhandledBindingType";

		// Token: 0x04000AE9 RID: 2793
		internal const string UnhandledConvert = "UnhandledConvert";

		// Token: 0x04000AEA RID: 2794
		internal const string UnhandledExpressionType = "UnhandledExpressionType";

		// Token: 0x04000AEB RID: 2795
		internal const string UnhandledUnary = "UnhandledUnary";

		// Token: 0x04000AEC RID: 2796
		internal const string UnknownBindingType = "UnknownBindingType";

		// Token: 0x04000AED RID: 2797
		internal const string UserDefinedOpMustHaveConsistentTypes = "UserDefinedOpMustHaveConsistentTypes";

		// Token: 0x04000AEE RID: 2798
		internal const string UserDefinedOpMustHaveValidReturnType = "UserDefinedOpMustHaveValidReturnType";

		// Token: 0x04000AEF RID: 2799
		internal const string LogicalOperatorMustHaveBooleanOperators = "LogicalOperatorMustHaveBooleanOperators";

		// Token: 0x04000AF0 RID: 2800
		internal const string MethodDoesNotExistOnType = "MethodDoesNotExistOnType";

		// Token: 0x04000AF1 RID: 2801
		internal const string MethodWithArgsDoesNotExistOnType = "MethodWithArgsDoesNotExistOnType";

		// Token: 0x04000AF2 RID: 2802
		internal const string GenericMethodWithArgsDoesNotExistOnType = "GenericMethodWithArgsDoesNotExistOnType";

		// Token: 0x04000AF3 RID: 2803
		internal const string MethodWithMoreThanOneMatch = "MethodWithMoreThanOneMatch";

		// Token: 0x04000AF4 RID: 2804
		internal const string PropertyWithMoreThanOneMatch = "PropertyWithMoreThanOneMatch";

		// Token: 0x04000AF5 RID: 2805
		internal const string IncorrectNumberOfTypeArgsForFunc = "IncorrectNumberOfTypeArgsForFunc";

		// Token: 0x04000AF6 RID: 2806
		internal const string IncorrectNumberOfTypeArgsForAction = "IncorrectNumberOfTypeArgsForAction";

		// Token: 0x04000AF7 RID: 2807
		internal const string ArgumentCannotBeOfTypeVoid = "ArgumentCannotBeOfTypeVoid";

		// Token: 0x04000AF8 RID: 2808
		internal const string AmbiguousMatchInExpandoObject = "AmbiguousMatchInExpandoObject";

		// Token: 0x04000AF9 RID: 2809
		internal const string SameKeyExistsInExpando = "SameKeyExistsInExpando";

		// Token: 0x04000AFA RID: 2810
		internal const string KeyDoesNotExistInExpando = "KeyDoesNotExistInExpando";

		// Token: 0x04000AFB RID: 2811
		internal const string NoOrInvalidRuleProduced = "NoOrInvalidRuleProduced";

		// Token: 0x04000AFC RID: 2812
		internal const string FirstArgumentMustBeCallSite = "FirstArgumentMustBeCallSite";

		// Token: 0x04000AFD RID: 2813
		internal const string BindingCannotBeNull = "BindingCannotBeNull";

		// Token: 0x04000AFE RID: 2814
		internal const string InvalidOperation = "InvalidOperation";

		// Token: 0x04000AFF RID: 2815
		internal const string OutOfRange = "OutOfRange";

		// Token: 0x04000B00 RID: 2816
		internal const string QueueEmpty = "QueueEmpty";

		// Token: 0x04000B01 RID: 2817
		internal const string LabelTargetAlreadyDefined = "LabelTargetAlreadyDefined";

		// Token: 0x04000B02 RID: 2818
		internal const string LabelTargetUndefined = "LabelTargetUndefined";

		// Token: 0x04000B03 RID: 2819
		internal const string ControlCannotLeaveFinally = "ControlCannotLeaveFinally";

		// Token: 0x04000B04 RID: 2820
		internal const string ControlCannotLeaveFilterTest = "ControlCannotLeaveFilterTest";

		// Token: 0x04000B05 RID: 2821
		internal const string AmbiguousJump = "AmbiguousJump";

		// Token: 0x04000B06 RID: 2822
		internal const string ControlCannotEnterTry = "ControlCannotEnterTry";

		// Token: 0x04000B07 RID: 2823
		internal const string ControlCannotEnterExpression = "ControlCannotEnterExpression";

		// Token: 0x04000B08 RID: 2824
		internal const string NonLocalJumpWithValue = "NonLocalJumpWithValue";

		// Token: 0x04000B09 RID: 2825
		internal const string ExtensionNotReduced = "ExtensionNotReduced";

		// Token: 0x04000B0A RID: 2826
		internal const string CannotCompileConstant = "CannotCompileConstant";

		// Token: 0x04000B0B RID: 2827
		internal const string CannotCompileDynamic = "CannotCompileDynamic";

		// Token: 0x04000B0C RID: 2828
		internal const string InvalidLvalue = "InvalidLvalue";

		// Token: 0x04000B0D RID: 2829
		internal const string InvalidMemberType = "InvalidMemberType";

		// Token: 0x04000B0E RID: 2830
		internal const string UnknownLiftType = "UnknownLiftType";

		// Token: 0x04000B0F RID: 2831
		internal const string InvalidOutputDir = "InvalidOutputDir";

		// Token: 0x04000B10 RID: 2832
		internal const string InvalidAsmNameOrExtension = "InvalidAsmNameOrExtension";

		// Token: 0x04000B11 RID: 2833
		internal const string CollectionReadOnly = "CollectionReadOnly";

		// Token: 0x04000B12 RID: 2834
		internal const string IllegalNewGenericParams = "IllegalNewGenericParams";

		// Token: 0x04000B13 RID: 2835
		internal const string UndefinedVariable = "UndefinedVariable";

		// Token: 0x04000B14 RID: 2836
		internal const string CannotCloseOverByRef = "CannotCloseOverByRef";

		// Token: 0x04000B15 RID: 2837
		internal const string UnexpectedVarArgsCall = "UnexpectedVarArgsCall";

		// Token: 0x04000B16 RID: 2838
		internal const string RethrowRequiresCatch = "RethrowRequiresCatch";

		// Token: 0x04000B17 RID: 2839
		internal const string TryNotAllowedInFilter = "TryNotAllowedInFilter";

		// Token: 0x04000B18 RID: 2840
		internal const string MustRewriteToSameNode = "MustRewriteToSameNode";

		// Token: 0x04000B19 RID: 2841
		internal const string MustRewriteChildToSameType = "MustRewriteChildToSameType";

		// Token: 0x04000B1A RID: 2842
		internal const string MustRewriteWithoutMethod = "MustRewriteWithoutMethod";

		// Token: 0x04000B1B RID: 2843
		internal const string InvalidNullValue = "InvalidNullValue";

		// Token: 0x04000B1C RID: 2844
		internal const string InvalidObjectType = "InvalidObjectType";

		// Token: 0x04000B1D RID: 2845
		internal const string TryNotSupportedForMethodsWithRefArgs = "TryNotSupportedForMethodsWithRefArgs";

		// Token: 0x04000B1E RID: 2846
		internal const string TryNotSupportedForValueTypeInstances = "TryNotSupportedForValueTypeInstances";

		// Token: 0x04000B1F RID: 2847
		internal const string CollectionModifiedWhileEnumerating = "CollectionModifiedWhileEnumerating";

		// Token: 0x04000B20 RID: 2848
		internal const string EnumerationIsDone = "EnumerationIsDone";

		// Token: 0x04000B21 RID: 2849
		internal const string HomogenousAppDomainRequired = "HomogenousAppDomainRequired";

		// Token: 0x04000B22 RID: 2850
		internal const string TestValueTypeDoesNotMatchComparisonMethodParameter = "TestValueTypeDoesNotMatchComparisonMethodParameter";

		// Token: 0x04000B23 RID: 2851
		internal const string SwitchValueTypeDoesNotMatchComparisonMethodParameter = "SwitchValueTypeDoesNotMatchComparisonMethodParameter";

		// Token: 0x04000B24 RID: 2852
		internal const string InvalidMetaObjectCreated = "InvalidMetaObjectCreated";

		// Token: 0x04000B25 RID: 2853
		internal const string PdbGeneratorNeedsExpressionCompiler = "PdbGeneratorNeedsExpressionCompiler";

		// Token: 0x04000B26 RID: 2854
		private static SR loader;

		// Token: 0x04000B27 RID: 2855
		private ResourceManager resources;
	}
}
