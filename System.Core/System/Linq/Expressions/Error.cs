using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200020E RID: 526
	internal static class Error
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x0003B74F File Offset: 0x0003994F
		internal static Exception ArgCntMustBeGreaterThanNameCnt()
		{
			return new ArgumentException(Strings.ArgCntMustBeGreaterThanNameCnt);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003B75B File Offset: 0x0003995B
		internal static Exception ReducibleMustOverrideReduce()
		{
			return new ArgumentException(Strings.ReducibleMustOverrideReduce);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003B767 File Offset: 0x00039967
		internal static Exception MustReduceToDifferent()
		{
			return new ArgumentException(Strings.MustReduceToDifferent);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003B773 File Offset: 0x00039973
		internal static Exception ReducedNotCompatible()
		{
			return new ArgumentException(Strings.ReducedNotCompatible);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003B77F File Offset: 0x0003997F
		internal static Exception SetterHasNoParams()
		{
			return new ArgumentException(Strings.SetterHasNoParams);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003B78B File Offset: 0x0003998B
		internal static Exception PropertyCannotHaveRefType()
		{
			return new ArgumentException(Strings.PropertyCannotHaveRefType);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003B797 File Offset: 0x00039997
		internal static Exception IndexesOfSetGetMustMatch()
		{
			return new ArgumentException(Strings.IndexesOfSetGetMustMatch);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0003B7A3 File Offset: 0x000399A3
		internal static Exception AccessorsCannotHaveVarArgs()
		{
			return new ArgumentException(Strings.AccessorsCannotHaveVarArgs);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003B7AF File Offset: 0x000399AF
		internal static Exception AccessorsCannotHaveByRefArgs()
		{
			return new ArgumentException(Strings.AccessorsCannotHaveByRefArgs);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003B7BB File Offset: 0x000399BB
		internal static Exception BoundsCannotBeLessThanOne()
		{
			return new ArgumentException(Strings.BoundsCannotBeLessThanOne);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003B7C7 File Offset: 0x000399C7
		internal static Exception TypeMustNotBeByRef()
		{
			return new ArgumentException(Strings.TypeMustNotBeByRef);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003B7D3 File Offset: 0x000399D3
		internal static Exception TypeDoesNotHaveConstructorForTheSignature()
		{
			return new ArgumentException(Strings.TypeDoesNotHaveConstructorForTheSignature);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003B7DF File Offset: 0x000399DF
		internal static Exception CountCannotBeNegative()
		{
			return new ArgumentException(Strings.CountCannotBeNegative);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003B7EB File Offset: 0x000399EB
		internal static Exception ArrayTypeMustBeArray()
		{
			return new ArgumentException(Strings.ArrayTypeMustBeArray);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003B7F7 File Offset: 0x000399F7
		internal static Exception SetterMustBeVoid()
		{
			return new ArgumentException(Strings.SetterMustBeVoid);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003B803 File Offset: 0x00039A03
		internal static Exception PropertyTyepMustMatchSetter()
		{
			return new ArgumentException(Strings.PropertyTyepMustMatchSetter);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003B80F File Offset: 0x00039A0F
		internal static Exception BothAccessorsMustBeStatic()
		{
			return new ArgumentException(Strings.BothAccessorsMustBeStatic);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003B81B File Offset: 0x00039A1B
		internal static Exception OnlyStaticMethodsHaveNullInstance()
		{
			return new ArgumentException(Strings.OnlyStaticMethodsHaveNullInstance);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003B827 File Offset: 0x00039A27
		internal static Exception PropertyTypeCannotBeVoid()
		{
			return new ArgumentException(Strings.PropertyTypeCannotBeVoid);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003B833 File Offset: 0x00039A33
		internal static Exception InvalidUnboxType()
		{
			return new ArgumentException(Strings.InvalidUnboxType);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003B83F File Offset: 0x00039A3F
		internal static Exception ArgumentMustNotHaveValueType()
		{
			return new ArgumentException(Strings.ArgumentMustNotHaveValueType);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003B84B File Offset: 0x00039A4B
		internal static Exception MustBeReducible()
		{
			return new ArgumentException(Strings.MustBeReducible);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003B857 File Offset: 0x00039A57
		internal static Exception DefaultBodyMustBeSupplied()
		{
			return new ArgumentException(Strings.DefaultBodyMustBeSupplied);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003B863 File Offset: 0x00039A63
		internal static Exception MethodBuilderDoesNotHaveTypeBuilder()
		{
			return new ArgumentException(Strings.MethodBuilderDoesNotHaveTypeBuilder);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003B86F File Offset: 0x00039A6F
		internal static Exception TypeMustBeDerivedFromSystemDelegate()
		{
			return new ArgumentException(Strings.TypeMustBeDerivedFromSystemDelegate);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003B87B File Offset: 0x00039A7B
		internal static Exception ArgumentTypeCannotBeVoid()
		{
			return new ArgumentException(Strings.ArgumentTypeCannotBeVoid);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003B887 File Offset: 0x00039A87
		internal static Exception LabelMustBeVoidOrHaveExpression()
		{
			return new ArgumentException(Strings.LabelMustBeVoidOrHaveExpression);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003B893 File Offset: 0x00039A93
		internal static Exception LabelTypeMustBeVoid()
		{
			return new ArgumentException(Strings.LabelTypeMustBeVoid);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003B89F File Offset: 0x00039A9F
		internal static Exception QuotedExpressionMustBeLambda()
		{
			return new ArgumentException(Strings.QuotedExpressionMustBeLambda);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003B8AB File Offset: 0x00039AAB
		internal static Exception VariableMustNotBeByRef(object p0, object p1)
		{
			return new ArgumentException(Strings.VariableMustNotBeByRef(p0, p1));
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003B8B9 File Offset: 0x00039AB9
		internal static Exception DuplicateVariable(object p0)
		{
			return new ArgumentException(Strings.DuplicateVariable(p0));
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003B8C6 File Offset: 0x00039AC6
		internal static Exception StartEndMustBeOrdered()
		{
			return new ArgumentException(Strings.StartEndMustBeOrdered);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0003B8D2 File Offset: 0x00039AD2
		internal static Exception FaultCannotHaveCatchOrFinally()
		{
			return new ArgumentException(Strings.FaultCannotHaveCatchOrFinally);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003B8DE File Offset: 0x00039ADE
		internal static Exception TryMustHaveCatchFinallyOrFault()
		{
			return new ArgumentException(Strings.TryMustHaveCatchFinallyOrFault);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003B8EA File Offset: 0x00039AEA
		internal static Exception BodyOfCatchMustHaveSameTypeAsBodyOfTry()
		{
			return new ArgumentException(Strings.BodyOfCatchMustHaveSameTypeAsBodyOfTry);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003B8F6 File Offset: 0x00039AF6
		internal static Exception ExtensionNodeMustOverrideProperty(object p0)
		{
			return new InvalidOperationException(Strings.ExtensionNodeMustOverrideProperty(p0));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003B903 File Offset: 0x00039B03
		internal static Exception UserDefinedOperatorMustBeStatic(object p0)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustBeStatic(p0));
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003B910 File Offset: 0x00039B10
		internal static Exception UserDefinedOperatorMustNotBeVoid(object p0)
		{
			return new ArgumentException(Strings.UserDefinedOperatorMustNotBeVoid(p0));
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003B91D File Offset: 0x00039B1D
		internal static Exception CoercionOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CoercionOperatorNotDefined(p0, p1));
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003B92B File Offset: 0x00039B2B
		internal static Exception DynamicBinderResultNotAssignable(object p0, object p1, object p2)
		{
			return new InvalidCastException(Strings.DynamicBinderResultNotAssignable(p0, p1, p2));
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003B93A File Offset: 0x00039B3A
		internal static Exception DynamicObjectResultNotAssignable(object p0, object p1, object p2, object p3)
		{
			return new InvalidCastException(Strings.DynamicObjectResultNotAssignable(p0, p1, p2, p3));
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003B94A File Offset: 0x00039B4A
		internal static Exception DynamicBindingNeedsRestrictions(object p0, object p1)
		{
			return new InvalidOperationException(Strings.DynamicBindingNeedsRestrictions(p0, p1));
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003B958 File Offset: 0x00039B58
		internal static Exception BinderNotCompatibleWithCallSite(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinderNotCompatibleWithCallSite(p0, p1, p2));
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003B967 File Offset: 0x00039B67
		internal static Exception UnaryOperatorNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.UnaryOperatorNotDefined(p0, p1));
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003B975 File Offset: 0x00039B75
		internal static Exception BinaryOperatorNotDefined(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.BinaryOperatorNotDefined(p0, p1, p2));
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003B984 File Offset: 0x00039B84
		internal static Exception ReferenceEqualityNotDefined(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ReferenceEqualityNotDefined(p0, p1));
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003B992 File Offset: 0x00039B92
		internal static Exception OperandTypesDoNotMatchParameters(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OperandTypesDoNotMatchParameters(p0, p1));
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		internal static Exception OverloadOperatorTypeDoesNotMatchConversionType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.OverloadOperatorTypeDoesNotMatchConversionType(p0, p1));
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003B9AE File Offset: 0x00039BAE
		internal static Exception ConversionIsNotSupportedForArithmeticTypes()
		{
			return new InvalidOperationException(Strings.ConversionIsNotSupportedForArithmeticTypes);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0003B9BA File Offset: 0x00039BBA
		internal static Exception ArgumentMustBeArray()
		{
			return new ArgumentException(Strings.ArgumentMustBeArray);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0003B9C6 File Offset: 0x00039BC6
		internal static Exception ArgumentMustBeBoolean()
		{
			return new ArgumentException(Strings.ArgumentMustBeBoolean);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003B9D2 File Offset: 0x00039BD2
		internal static Exception EqualityMustReturnBoolean(object p0)
		{
			return new ArgumentException(Strings.EqualityMustReturnBoolean(p0));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003B9DF File Offset: 0x00039BDF
		internal static Exception ArgumentMustBeFieldInfoOrPropertInfo()
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertInfo);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003B9EB File Offset: 0x00039BEB
		internal static Exception ArgumentMustBeFieldInfoOrPropertInfoOrMethod()
		{
			return new ArgumentException(Strings.ArgumentMustBeFieldInfoOrPropertInfoOrMethod);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003B9F7 File Offset: 0x00039BF7
		internal static Exception ArgumentMustBeInstanceMember()
		{
			return new ArgumentException(Strings.ArgumentMustBeInstanceMember);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0003BA03 File Offset: 0x00039C03
		internal static Exception ArgumentMustBeInteger()
		{
			return new ArgumentException(Strings.ArgumentMustBeInteger);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003BA0F File Offset: 0x00039C0F
		internal static Exception ArgumentMustBeArrayIndexType()
		{
			return new ArgumentException(Strings.ArgumentMustBeArrayIndexType);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0003BA1B File Offset: 0x00039C1B
		internal static Exception ArgumentMustBeSingleDimensionalArrayType()
		{
			return new ArgumentException(Strings.ArgumentMustBeSingleDimensionalArrayType);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003BA27 File Offset: 0x00039C27
		internal static Exception ArgumentTypesMustMatch()
		{
			return new ArgumentException(Strings.ArgumentTypesMustMatch);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003BA33 File Offset: 0x00039C33
		internal static Exception CannotAutoInitializeValueTypeElementThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeElementThroughProperty(p0));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003BA40 File Offset: 0x00039C40
		internal static Exception CannotAutoInitializeValueTypeMemberThroughProperty(object p0)
		{
			return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeMemberThroughProperty(p0));
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003BA4D File Offset: 0x00039C4D
		internal static Exception IncorrectTypeForTypeAs(object p0)
		{
			return new ArgumentException(Strings.IncorrectTypeForTypeAs(p0));
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003BA5A File Offset: 0x00039C5A
		internal static Exception CoalesceUsedOnNonNullType()
		{
			return new InvalidOperationException(Strings.CoalesceUsedOnNonNullType);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003BA66 File Offset: 0x00039C66
		internal static Exception ExpressionTypeCannotInitializeArrayType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.ExpressionTypeCannotInitializeArrayType(p0, p1));
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003BA74 File Offset: 0x00039C74
		internal static Exception ExpressionTypeDoesNotMatchConstructorParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchConstructorParameter(p0, p1));
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003BA82 File Offset: 0x00039C82
		internal static Exception ArgumentTypeDoesNotMatchMember(object p0, object p1)
		{
			return new ArgumentException(Strings.ArgumentTypeDoesNotMatchMember(p0, p1));
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003BA90 File Offset: 0x00039C90
		internal static Exception ArgumentMemberNotDeclOnType(object p0, object p1)
		{
			return new ArgumentException(Strings.ArgumentMemberNotDeclOnType(p0, p1));
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003BA9E File Offset: 0x00039C9E
		internal static Exception ExpressionTypeDoesNotMatchMethodParameter(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchMethodParameter(p0, p1, p2));
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003BAAD File Offset: 0x00039CAD
		internal static Exception ExpressionTypeDoesNotMatchParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchParameter(p0, p1));
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0003BABB File Offset: 0x00039CBB
		internal static Exception ExpressionTypeDoesNotMatchReturn(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchReturn(p0, p1));
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003BAC9 File Offset: 0x00039CC9
		internal static Exception ExpressionTypeDoesNotMatchAssignment(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchAssignment(p0, p1));
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0003BAD7 File Offset: 0x00039CD7
		internal static Exception ExpressionTypeDoesNotMatchLabel(object p0, object p1)
		{
			return new ArgumentException(Strings.ExpressionTypeDoesNotMatchLabel(p0, p1));
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0003BAE5 File Offset: 0x00039CE5
		internal static Exception ExpressionTypeNotInvocable(object p0)
		{
			return new ArgumentException(Strings.ExpressionTypeNotInvocable(p0));
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003BAF2 File Offset: 0x00039CF2
		internal static Exception FieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.FieldNotDefinedForType(p0, p1));
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003BB00 File Offset: 0x00039D00
		internal static Exception InstanceFieldNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstanceFieldNotDefinedForType(p0, p1));
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003BB0E File Offset: 0x00039D0E
		internal static Exception FieldInfoNotDefinedForType(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.FieldInfoNotDefinedForType(p0, p1, p2));
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0003BB1D File Offset: 0x00039D1D
		internal static Exception IncorrectNumberOfIndexes()
		{
			return new ArgumentException(Strings.IncorrectNumberOfIndexes);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003BB29 File Offset: 0x00039D29
		internal static Exception IncorrectNumberOfLambdaArguments()
		{
			return new InvalidOperationException(Strings.IncorrectNumberOfLambdaArguments);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003BB35 File Offset: 0x00039D35
		internal static Exception IncorrectNumberOfLambdaDeclarationParameters()
		{
			return new ArgumentException(Strings.IncorrectNumberOfLambdaDeclarationParameters);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0003BB41 File Offset: 0x00039D41
		internal static Exception IncorrectNumberOfMethodCallArguments(object p0)
		{
			return new ArgumentException(Strings.IncorrectNumberOfMethodCallArguments(p0));
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003BB4E File Offset: 0x00039D4E
		internal static Exception IncorrectNumberOfConstructorArguments()
		{
			return new ArgumentException(Strings.IncorrectNumberOfConstructorArguments);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0003BB5A File Offset: 0x00039D5A
		internal static Exception IncorrectNumberOfMembersForGivenConstructor()
		{
			return new ArgumentException(Strings.IncorrectNumberOfMembersForGivenConstructor);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003BB66 File Offset: 0x00039D66
		internal static Exception IncorrectNumberOfArgumentsForMembers()
		{
			return new ArgumentException(Strings.IncorrectNumberOfArgumentsForMembers);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003BB72 File Offset: 0x00039D72
		internal static Exception LambdaTypeMustBeDerivedFromSystemDelegate()
		{
			return new ArgumentException(Strings.LambdaTypeMustBeDerivedFromSystemDelegate);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003BB7E File Offset: 0x00039D7E
		internal static Exception MemberNotFieldOrProperty(object p0)
		{
			return new ArgumentException(Strings.MemberNotFieldOrProperty(p0));
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003BB8B File Offset: 0x00039D8B
		internal static Exception MethodContainsGenericParameters(object p0)
		{
			return new ArgumentException(Strings.MethodContainsGenericParameters(p0));
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003BB98 File Offset: 0x00039D98
		internal static Exception MethodIsGeneric(object p0)
		{
			return new ArgumentException(Strings.MethodIsGeneric(p0));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003BBA5 File Offset: 0x00039DA5
		internal static Exception MethodNotPropertyAccessor(object p0, object p1)
		{
			return new ArgumentException(Strings.MethodNotPropertyAccessor(p0, p1));
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003BBB3 File Offset: 0x00039DB3
		internal static Exception PropertyDoesNotHaveGetter(object p0)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveGetter(p0));
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		internal static Exception PropertyDoesNotHaveSetter(object p0)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveSetter(p0));
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003BBCD File Offset: 0x00039DCD
		internal static Exception PropertyDoesNotHaveAccessor(object p0)
		{
			return new ArgumentException(Strings.PropertyDoesNotHaveAccessor(p0));
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003BBDA File Offset: 0x00039DDA
		internal static Exception NotAMemberOfType(object p0, object p1)
		{
			return new ArgumentException(Strings.NotAMemberOfType(p0, p1));
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		internal static Exception OperatorNotImplementedForType(object p0, object p1)
		{
			return new NotImplementedException(Strings.OperatorNotImplementedForType(p0, p1));
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003BBF6 File Offset: 0x00039DF6
		internal static Exception ParameterExpressionNotValidAsDelegate(object p0, object p1)
		{
			return new ArgumentException(Strings.ParameterExpressionNotValidAsDelegate(p0, p1));
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003BC04 File Offset: 0x00039E04
		internal static Exception PropertyNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.PropertyNotDefinedForType(p0, p1));
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003BC12 File Offset: 0x00039E12
		internal static Exception InstancePropertyNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstancePropertyNotDefinedForType(p0, p1));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003BC20 File Offset: 0x00039E20
		internal static Exception InstancePropertyWithoutParameterNotDefinedForType(object p0, object p1)
		{
			return new ArgumentException(Strings.InstancePropertyWithoutParameterNotDefinedForType(p0, p1));
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003BC2E File Offset: 0x00039E2E
		internal static Exception InstancePropertyWithSpecifiedParametersNotDefinedForType(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.InstancePropertyWithSpecifiedParametersNotDefinedForType(p0, p1, p2));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003BC3D File Offset: 0x00039E3D
		internal static Exception InstanceAndMethodTypeMismatch(object p0, object p1, object p2)
		{
			return new ArgumentException(Strings.InstanceAndMethodTypeMismatch(p0, p1, p2));
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003BC4C File Offset: 0x00039E4C
		internal static Exception TypeContainsGenericParameters(object p0)
		{
			return new ArgumentException(Strings.TypeContainsGenericParameters(p0));
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003BC59 File Offset: 0x00039E59
		internal static Exception TypeIsGeneric(object p0)
		{
			return new ArgumentException(Strings.TypeIsGeneric(p0));
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003BC66 File Offset: 0x00039E66
		internal static Exception TypeMissingDefaultConstructor(object p0)
		{
			return new ArgumentException(Strings.TypeMissingDefaultConstructor(p0));
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003BC73 File Offset: 0x00039E73
		internal static Exception ListInitializerWithZeroMembers()
		{
			return new ArgumentException(Strings.ListInitializerWithZeroMembers);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003BC7F File Offset: 0x00039E7F
		internal static Exception ElementInitializerMethodNotAdd()
		{
			return new ArgumentException(Strings.ElementInitializerMethodNotAdd);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003BC8B File Offset: 0x00039E8B
		internal static Exception ElementInitializerMethodNoRefOutParam(object p0, object p1)
		{
			return new ArgumentException(Strings.ElementInitializerMethodNoRefOutParam(p0, p1));
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003BC99 File Offset: 0x00039E99
		internal static Exception ElementInitializerMethodWithZeroArgs()
		{
			return new ArgumentException(Strings.ElementInitializerMethodWithZeroArgs);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003BCA5 File Offset: 0x00039EA5
		internal static Exception ElementInitializerMethodStatic()
		{
			return new ArgumentException(Strings.ElementInitializerMethodStatic);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003BCB1 File Offset: 0x00039EB1
		internal static Exception TypeNotIEnumerable(object p0)
		{
			return new ArgumentException(Strings.TypeNotIEnumerable(p0));
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003BCBE File Offset: 0x00039EBE
		internal static Exception TypeParameterIsNotDelegate(object p0)
		{
			return new InvalidOperationException(Strings.TypeParameterIsNotDelegate(p0));
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003BCCB File Offset: 0x00039ECB
		internal static Exception UnexpectedCoalesceOperator()
		{
			return new InvalidOperationException(Strings.UnexpectedCoalesceOperator);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003BCD7 File Offset: 0x00039ED7
		internal static Exception InvalidCast(object p0, object p1)
		{
			return new InvalidOperationException(Strings.InvalidCast(p0, p1));
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003BCE5 File Offset: 0x00039EE5
		internal static Exception UnhandledBinary(object p0)
		{
			return new ArgumentException(Strings.UnhandledBinary(p0));
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003BCF2 File Offset: 0x00039EF2
		internal static Exception UnhandledBinding()
		{
			return new ArgumentException(Strings.UnhandledBinding);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003BCFE File Offset: 0x00039EFE
		internal static Exception UnhandledBindingType(object p0)
		{
			return new ArgumentException(Strings.UnhandledBindingType(p0));
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003BD0B File Offset: 0x00039F0B
		internal static Exception UnhandledConvert(object p0)
		{
			return new ArgumentException(Strings.UnhandledConvert(p0));
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003BD18 File Offset: 0x00039F18
		internal static Exception UnhandledExpressionType(object p0)
		{
			return new ArgumentException(Strings.UnhandledExpressionType(p0));
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003BD25 File Offset: 0x00039F25
		internal static Exception UnhandledUnary(object p0)
		{
			return new ArgumentException(Strings.UnhandledUnary(p0));
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003BD32 File Offset: 0x00039F32
		internal static Exception UnknownBindingType()
		{
			return new ArgumentException(Strings.UnknownBindingType);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003BD3E File Offset: 0x00039F3E
		internal static Exception UserDefinedOpMustHaveConsistentTypes(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveConsistentTypes(p0, p1));
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003BD4C File Offset: 0x00039F4C
		internal static Exception UserDefinedOpMustHaveValidReturnType(object p0, object p1)
		{
			return new ArgumentException(Strings.UserDefinedOpMustHaveValidReturnType(p0, p1));
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003BD5A File Offset: 0x00039F5A
		internal static Exception LogicalOperatorMustHaveBooleanOperators(object p0, object p1)
		{
			return new ArgumentException(Strings.LogicalOperatorMustHaveBooleanOperators(p0, p1));
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003BD68 File Offset: 0x00039F68
		internal static Exception MethodDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003BD76 File Offset: 0x00039F76
		internal static Exception MethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003BD84 File Offset: 0x00039F84
		internal static Exception GenericMethodWithArgsDoesNotExistOnType(object p0, object p1)
		{
			return new InvalidOperationException(Strings.GenericMethodWithArgsDoesNotExistOnType(p0, p1));
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003BD92 File Offset: 0x00039F92
		internal static Exception MethodWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MethodWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		internal static Exception PropertyWithMoreThanOneMatch(object p0, object p1)
		{
			return new InvalidOperationException(Strings.PropertyWithMoreThanOneMatch(p0, p1));
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003BDAE File Offset: 0x00039FAE
		internal static Exception IncorrectNumberOfTypeArgsForFunc()
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForFunc);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003BDBA File Offset: 0x00039FBA
		internal static Exception IncorrectNumberOfTypeArgsForAction()
		{
			return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForAction);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003BDC6 File Offset: 0x00039FC6
		internal static Exception ArgumentCannotBeOfTypeVoid()
		{
			return new ArgumentException(Strings.ArgumentCannotBeOfTypeVoid);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003BDD2 File Offset: 0x00039FD2
		internal static Exception AmbiguousMatchInExpandoObject(object p0)
		{
			return new AmbiguousMatchException(Strings.AmbiguousMatchInExpandoObject(p0));
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003BDDF File Offset: 0x00039FDF
		internal static Exception SameKeyExistsInExpando(object p0)
		{
			return new ArgumentException(Strings.SameKeyExistsInExpando(p0));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003BDEC File Offset: 0x00039FEC
		internal static Exception KeyDoesNotExistInExpando(object p0)
		{
			return new KeyNotFoundException(Strings.KeyDoesNotExistInExpando(p0));
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003BDF9 File Offset: 0x00039FF9
		internal static Exception NoOrInvalidRuleProduced()
		{
			return new InvalidOperationException(Strings.NoOrInvalidRuleProduced);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003BE05 File Offset: 0x0003A005
		internal static Exception FirstArgumentMustBeCallSite()
		{
			return new ArgumentException(Strings.FirstArgumentMustBeCallSite);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003BE11 File Offset: 0x0003A011
		internal static Exception BindingCannotBeNull()
		{
			return new InvalidOperationException(Strings.BindingCannotBeNull);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003BE1D File Offset: 0x0003A01D
		internal static Exception InvalidOperation(object p0)
		{
			return new ArgumentException(Strings.InvalidOperation(p0));
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003BE2A File Offset: 0x0003A02A
		internal static Exception OutOfRange(object p0, object p1)
		{
			return new ArgumentOutOfRangeException(Strings.OutOfRange(p0, p1));
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003BE38 File Offset: 0x0003A038
		internal static Exception QueueEmpty()
		{
			return new InvalidOperationException(Strings.QueueEmpty);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003BE44 File Offset: 0x0003A044
		internal static Exception LabelTargetAlreadyDefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetAlreadyDefined(p0));
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003BE51 File Offset: 0x0003A051
		internal static Exception LabelTargetUndefined(object p0)
		{
			return new InvalidOperationException(Strings.LabelTargetUndefined(p0));
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003BE5E File Offset: 0x0003A05E
		internal static Exception ControlCannotLeaveFinally()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFinally);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003BE6A File Offset: 0x0003A06A
		internal static Exception ControlCannotLeaveFilterTest()
		{
			return new InvalidOperationException(Strings.ControlCannotLeaveFilterTest);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003BE76 File Offset: 0x0003A076
		internal static Exception AmbiguousJump(object p0)
		{
			return new InvalidOperationException(Strings.AmbiguousJump(p0));
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003BE83 File Offset: 0x0003A083
		internal static Exception ControlCannotEnterTry()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterTry);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003BE8F File Offset: 0x0003A08F
		internal static Exception ControlCannotEnterExpression()
		{
			return new InvalidOperationException(Strings.ControlCannotEnterExpression);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003BE9B File Offset: 0x0003A09B
		internal static Exception NonLocalJumpWithValue(object p0)
		{
			return new InvalidOperationException(Strings.NonLocalJumpWithValue(p0));
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
		internal static Exception ExtensionNotReduced()
		{
			return new InvalidOperationException(Strings.ExtensionNotReduced);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
		internal static Exception CannotCompileConstant(object p0)
		{
			return new InvalidOperationException(Strings.CannotCompileConstant(p0));
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		internal static Exception CannotCompileDynamic()
		{
			return new NotSupportedException(Strings.CannotCompileDynamic);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003BECD File Offset: 0x0003A0CD
		internal static Exception InvalidLvalue(object p0)
		{
			return new InvalidOperationException(Strings.InvalidLvalue(p0));
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003BEDA File Offset: 0x0003A0DA
		internal static Exception InvalidMemberType(object p0)
		{
			return new InvalidOperationException(Strings.InvalidMemberType(p0));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003BEE7 File Offset: 0x0003A0E7
		internal static Exception UnknownLiftType(object p0)
		{
			return new InvalidOperationException(Strings.UnknownLiftType(p0));
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		internal static Exception InvalidOutputDir()
		{
			return new ArgumentException(Strings.InvalidOutputDir);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003BF00 File Offset: 0x0003A100
		internal static Exception InvalidAsmNameOrExtension()
		{
			return new ArgumentException(Strings.InvalidAsmNameOrExtension);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003BF0C File Offset: 0x0003A10C
		internal static Exception CollectionReadOnly()
		{
			return new NotSupportedException(Strings.CollectionReadOnly);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003BF18 File Offset: 0x0003A118
		internal static Exception IllegalNewGenericParams(object p0)
		{
			return new ArgumentException(Strings.IllegalNewGenericParams(p0));
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003BF25 File Offset: 0x0003A125
		internal static Exception UndefinedVariable(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.UndefinedVariable(p0, p1, p2));
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0003BF34 File Offset: 0x0003A134
		internal static Exception CannotCloseOverByRef(object p0, object p1)
		{
			return new InvalidOperationException(Strings.CannotCloseOverByRef(p0, p1));
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003BF42 File Offset: 0x0003A142
		internal static Exception UnexpectedVarArgsCall(object p0)
		{
			return new InvalidOperationException(Strings.UnexpectedVarArgsCall(p0));
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003BF4F File Offset: 0x0003A14F
		internal static Exception RethrowRequiresCatch()
		{
			return new InvalidOperationException(Strings.RethrowRequiresCatch);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003BF5B File Offset: 0x0003A15B
		internal static Exception TryNotAllowedInFilter()
		{
			return new InvalidOperationException(Strings.TryNotAllowedInFilter);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003BF67 File Offset: 0x0003A167
		internal static Exception MustRewriteToSameNode(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteToSameNode(p0, p1, p2));
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0003BF76 File Offset: 0x0003A176
		internal static Exception MustRewriteChildToSameType(object p0, object p1, object p2)
		{
			return new InvalidOperationException(Strings.MustRewriteChildToSameType(p0, p1, p2));
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0003BF85 File Offset: 0x0003A185
		internal static Exception MustRewriteWithoutMethod(object p0, object p1)
		{
			return new InvalidOperationException(Strings.MustRewriteWithoutMethod(p0, p1));
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0003BF93 File Offset: 0x0003A193
		internal static Exception TryNotSupportedForMethodsWithRefArgs(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForMethodsWithRefArgs(p0));
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		internal static Exception TryNotSupportedForValueTypeInstances(object p0)
		{
			return new NotSupportedException(Strings.TryNotSupportedForValueTypeInstances(p0));
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0003BFAD File Offset: 0x0003A1AD
		internal static Exception CollectionModifiedWhileEnumerating()
		{
			return new InvalidOperationException(Strings.CollectionModifiedWhileEnumerating);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003BFB9 File Offset: 0x0003A1B9
		internal static Exception EnumerationIsDone()
		{
			return new InvalidOperationException(Strings.EnumerationIsDone);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003BFC5 File Offset: 0x0003A1C5
		internal static Exception HomogenousAppDomainRequired()
		{
			return new InvalidOperationException(Strings.HomogenousAppDomainRequired);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0003BFD1 File Offset: 0x0003A1D1
		internal static Exception TestValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.TestValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003BFDF File Offset: 0x0003A1DF
		internal static Exception SwitchValueTypeDoesNotMatchComparisonMethodParameter(object p0, object p1)
		{
			return new ArgumentException(Strings.SwitchValueTypeDoesNotMatchComparisonMethodParameter(p0, p1));
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003BFED File Offset: 0x0003A1ED
		internal static Exception InvalidMetaObjectCreated(object p0)
		{
			return new InvalidOperationException(Strings.InvalidMetaObjectCreated(p0));
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003BFFA File Offset: 0x0003A1FA
		internal static Exception PdbGeneratorNeedsExpressionCompiler()
		{
			return new NotSupportedException(Strings.PdbGeneratorNeedsExpressionCompiler);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003C006 File Offset: 0x0003A206
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003C00E File Offset: 0x0003A20E
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003C016 File Offset: 0x0003A216
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003C01D File Offset: 0x0003A21D
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
