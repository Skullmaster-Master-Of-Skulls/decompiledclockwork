using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x020004F9 RID: 1273
	internal sealed class EdmProviderManifestFunctionBuilder
	{
		// Token: 0x06002F5D RID: 12125 RVA: 0x000E3334 File Offset: 0x000E1534
		internal EdmProviderManifestFunctionBuilder(ReadOnlyCollection<PrimitiveType> edmPrimitiveTypes)
		{
			TypeUsage[] array = new TypeUsage[edmPrimitiveTypes.Count];
			foreach (PrimitiveType primitiveType in edmPrimitiveTypes)
			{
				array[(int)primitiveType.PrimitiveTypeKind] = TypeUsage.Create(primitiveType);
			}
			this.primitiveTypes = array;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000E33A8 File Offset: 0x000E15A8
		internal ReadOnlyCollection<EdmFunction> ToFunctionCollection()
		{
			return new ReadOnlyCollection<EdmFunction>(this.functions);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000E33B8 File Offset: 0x000E15B8
		internal static void ForAllBasePrimitiveTypes(Action<PrimitiveTypeKind> forEachType)
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

		// Token: 0x06002F60 RID: 12128 RVA: 0x000E33E4 File Offset: 0x000E15E4
		internal static void ForTypes(IEnumerable<PrimitiveTypeKind> typeKinds, Action<PrimitiveTypeKind> forEachType)
		{
			foreach (PrimitiveTypeKind obj in typeKinds)
			{
				forEachType(obj);
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000E342C File Offset: 0x000E162C
		internal void AddAggregate(string aggregateFunctionName, PrimitiveTypeKind collectionArgumentElementTypeKind)
		{
			this.AddAggregate(collectionArgumentElementTypeKind, aggregateFunctionName, collectionArgumentElementTypeKind);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000E3438 File Offset: 0x000E1638
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

		// Token: 0x06002F63 RID: 12131 RVA: 0x000E34C9 File Offset: 0x000E16C9
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[0]);
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000E34DC File Offset: 0x000E16DC
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argumentTypeKind, string argumentName)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argumentName, argumentTypeKind)
			});
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000E3510 File Offset: 0x000E1710
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind)
			});
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000E3558 File Offset: 0x000E1758
		internal void AddFunction(PrimitiveTypeKind returnType, string functionName, PrimitiveTypeKind argument1TypeKind, string argument1Name, PrimitiveTypeKind argument2TypeKind, string argument2Name, PrimitiveTypeKind argument3TypeKind, string argument3Name)
		{
			this.AddFunction(returnType, functionName, new KeyValuePair<string, PrimitiveTypeKind>[]
			{
				new KeyValuePair<string, PrimitiveTypeKind>(argument1Name, argument1TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument2Name, argument2TypeKind),
				new KeyValuePair<string, PrimitiveTypeKind>(argument3Name, argument3TypeKind)
			});
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000E35B4 File Offset: 0x000E17B4
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

		// Token: 0x06002F68 RID: 12136 RVA: 0x000E3650 File Offset: 0x000E1850
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

		// Token: 0x06002F69 RID: 12137 RVA: 0x000E3718 File Offset: 0x000E1918
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

		// Token: 0x06002F6A RID: 12138 RVA: 0x000E379F File Offset: 0x000E199F
		private FunctionParameter CreateParameter(PrimitiveTypeKind primitiveParameterType, string parameterName)
		{
			return new FunctionParameter(parameterName, this.primitiveTypes[(int)primitiveParameterType], ParameterMode.In);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000E37B0 File Offset: 0x000E19B0
		private FunctionParameter CreateAggregateParameter(PrimitiveTypeKind collectionParameterTypeElementTypeKind)
		{
			return new FunctionParameter("collection", TypeUsage.Create(this.primitiveTypes[(int)collectionParameterTypeElementTypeKind].EdmType.GetCollectionType()), ParameterMode.In);
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000E37D4 File Offset: 0x000E19D4
		private FunctionParameter CreateReturnParameter(PrimitiveTypeKind primitiveReturnType)
		{
			return new FunctionParameter("ReturnType", this.primitiveTypes[(int)primitiveReturnType], ParameterMode.ReturnValue);
		}

		// Token: 0x0400121F RID: 4639
		private readonly List<EdmFunction> functions = new List<EdmFunction>();

		// Token: 0x04001220 RID: 4640
		private readonly TypeUsage[] primitiveTypes;
	}
}
