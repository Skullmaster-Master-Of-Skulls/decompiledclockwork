using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001A9 RID: 425
	internal sealed class EdmProviderManifestFunctionBuilder
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x0006B83C File Offset: 0x00069A3C
		internal EdmProviderManifestFunctionBuilder(ReadOnlyCollection<PrimitiveType> edmPrimitiveTypes)
		{
			TypeUsage[] array = new TypeUsage[edmPrimitiveTypes.Count];
			foreach (PrimitiveType primitiveType in edmPrimitiveTypes)
			{
				array[(int)primitiveType.PrimitiveTypeKind] = TypeUsage.Create(primitiveType);
			}
			this.primitiveTypes = array;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0006B8B0 File Offset: 0x00069AB0
		internal ReadOnlyCollection<EdmFunction> ToFunctionCollection()
		{
			return this.functions.AsReadOnly();
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006B8C0 File Offset: 0x00069AC0
		internal void ForAllTypes(Action<PrimitiveTypeKind> forEachType)
		{
			for (int i = 0; i < 31; i++)
			{
				forEachType((PrimitiveTypeKind)i);
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006B8E4 File Offset: 0x00069AE4
		internal void ForAllBasePrimitiveTypes(Action<PrimitiveTypeKind> forEachType)
		{
			for (int i = 0; i < 31; i++)
			{
				PrimitiveTypeKind primitiveTypeKind = (PrimitiveTypeKind)i;
				if (!Helper.IsStrongSpatialTypeKind(primitiveTypeKind))
				{
					forEachType(primitiveTypeKind);
				}
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006B910 File Offset: 0x00069B10
		internal void ForTypes(IEnumerable<PrimitiveTypeKind> typeKinds, Action<PrimitiveTypeKind> forEachType)
		{
			foreach (PrimitiveTypeKind obj in typeKinds)
			{
				forEachType(obj);
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0006B958 File Offset: 0x00069B58
		internal void AddAggregate(string aggregateFunctionName, PrimitiveTypeKind collectionArgumentElementTypeKind)
		{
			this.AddAggregate(collectionArgumentElementTypeKind, aggregateFunctionName, collectionArgumentElementTypeKind);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0006B964 File Offset: 0x00069B64
		internal void AddAggregate(PrimitiveTypeKind returnTypeKind, string aggregateFunctionName, PrimitiveTypeKind collectionArgumentElementTypeKind)
		{
			FunctionParameter functionParameter = this.CreateReturnParameter(returnTypeKind);
			FunctionParameter functionParameter2 = this.CreateAggregateParameter(collectionArgumentElementTypeKind);
			EdmFunction edmFunction = new EdmFunction(aggregateFunctionName, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsAggregate = new bool?(true),
				IsBuiltIn = new bool?(true),
				ReturnParameters = new FunctionParameter[]
				{
					functionParameter
				},
				Parameters = new FunctionParameter[]
				{
					functionParameter2
				},
				IsFromProviderManifest = new bool?(true)
			});
			edmFunction.SetReadOnly();
			this.functions.Add(edmFunction);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0006B9F2 File Offset: 0x00069BF2
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[0]);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0006BA02 File Offset: 0x00069C02
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argumentTypeKind, string argumentName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argumentName, argumentTypeKind)
			});
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006BA21 File Offset: 0x00069C21
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind)
			});
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0006BA50 File Offset: 0x00069C50
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind)
			});
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0006BA90 File Offset: 0x00069C90
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name, PrimitiveTypeKind argument4TypeKind, string argument4Name, PrimitiveTypeKind argument5TypeKind, string argument5Name, PrimitiveTypeKind argument6TypeKind, string argument6Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument4Name, argument4TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument5Name, argument5TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument6Name, argument6TypeKind)
			});
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0006BB0C File Offset: 0x00069D0C
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name, PrimitiveTypeKind argument4TypeKind, string argument4Name, PrimitiveTypeKind argument5TypeKind, string argument5Name, PrimitiveTypeKind argument6TypeKind, string argument6Name, PrimitiveTypeKind argument7TypeKind, string argument7Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument4Name, argument4TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument5Name, argument5TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument6Name, argument6TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument7Name, argument7TypeKind)
			});
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0006BB98 File Offset: 0x00069D98
		private void AddFunction(PrimitiveTypeKind returnType, string functionName, KeyValuePair<string, PrimitiveTypeKind>[] parameterDefinitions)
		{
			FunctionParameter functionParameter = this.CreateReturnParameter(returnType);
			FunctionParameter[] parameters = (from paramDef in parameterDefinitions
			select this.CreateParameter(paramDef.Value, paramDef.Key)).ToArray<FunctionParameter>();
			EdmFunction edmFunction = new EdmFunction(functionName, "Edm", DataSpace.CSpace, new EdmFunctionPayload
			{
				IsBuiltIn = new bool?(true),
				ReturnParameters = new FunctionParameter[]
				{
					functionParameter
				},
				Parameters = parameters,
				IsFromProviderManifest = new bool?(true)
			});
			edmFunction.SetReadOnly();
			this.functions.Add(edmFunction);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0006BC20 File Offset: 0x00069E20
		private FunctionParameter CreateParameter(PrimitiveTypeKind primitiveParameterType, string parameterName)
		{
			return new FunctionParameter(parameterName, this.primitiveTypes[(int)primitiveParameterType], ParameterMode.In);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0006BC31 File Offset: 0x00069E31
		private FunctionParameter CreateAggregateParameter(PrimitiveTypeKind collectionParameterTypeElementTypeKind)
		{
			return new FunctionParameter("collection", TypeUsage.Create(this.primitiveTypes[(int)collectionParameterTypeElementTypeKind].EdmType.GetCollectionType()), ParameterMode.In);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0006BC55 File Offset: 0x00069E55
		private FunctionParameter CreateReturnParameter(PrimitiveTypeKind primitiveReturnType)
		{
			return new FunctionParameter("ReturnType", this.primitiveTypes[(int)primitiveReturnType], ParameterMode.ReturnValue);
		}

		// Token: 0x04000CDC RID: 3292
		private readonly List<EdmFunction> functions = new List<EdmFunction>();

		// Token: 0x04000CDD RID: 3293
		private readonly TypeUsage[] primitiveTypes;
	}
}
