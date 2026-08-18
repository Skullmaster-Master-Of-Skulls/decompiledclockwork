using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D8 RID: 472
	public sealed class EdmFunction : EdmType
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x0006FAE0 File Offset: 0x0006DCE0
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload) : base(name, namespaceName, dataSpace)
		{
			this._schemaName = payload.Schema;
			this._fullName = base.NamespaceName + "." + base.Name;
			FunctionParameter[] returnParameters = payload.ReturnParameters;
			this._returnParameters = new ReadOnlyMetadataCollection<FunctionParameter>((from returnParameter in returnParameters
			select SafeLink<EdmFunction>.BindChild<FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, returnParameter)).ToList<FunctionParameter>());
			if (payload.IsAggregate != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.Aggregate, payload.IsAggregate.Value);
			}
			if (payload.IsBuiltIn != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.BuiltIn, payload.IsBuiltIn.Value);
			}
			if (payload.IsNiladic != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.NiladicFunction, payload.IsNiladic.Value);
			}
			if (payload.IsComposable != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsComposable, payload.IsComposable.Value);
			}
			if (payload.IsFromProviderManifest != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsFromProviderManifest, payload.IsFromProviderManifest.Value);
			}
			if (payload.IsCachedStoreFunction != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsCachedStoreFunction, payload.IsCachedStoreFunction.Value);
			}
			if (payload.IsFunctionImport != null)
			{
				EdmFunction.SetFunctionAttribute(ref this._functionAttributes, EdmFunction.FunctionAttributes.IsFunctionImport, payload.IsFunctionImport.Value);
			}
			if (payload.ParameterTypeSemantics != null)
			{
				this._parameterTypeSemantics = payload.ParameterTypeSemantics.Value;
			}
			if (payload.StoreFunctionName != null)
			{
				this._storeFunctionNameAttribute = payload.StoreFunctionName;
			}
			if (payload.EntitySets != null)
			{
				this._entitySets = new ReadOnlyMetadataCollection<EntitySet>(payload.EntitySets);
			}
			else
			{
				List<EntitySet> list = new List<EntitySet>();
				if (this._returnParameters.Count != 0)
				{
					list.Add(null);
				}
				this._entitySets = new ReadOnlyMetadataCollection<EntitySet>(list);
			}
			if (payload.CommandText != null)
			{
				this._commandTextAttribute = payload.CommandText;
			}
			if (payload.Parameters != null)
			{
				FunctionParameter[] parameters = payload.Parameters;
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i] == null)
					{
						throw EntityUtil.CollectionParameterElementIsNull("parameters");
					}
				}
				this._parameters = new SafeLinkCollection<EdmFunction, FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, new MetadataCollection<FunctionParameter>(payload.Parameters));
				return;
			}
			this._parameters = new ReadOnlyMetadataCollection<FunctionParameter>(new MetadataCollection<FunctionParameter>());
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0006FD4D File Offset: 0x0006DF4D
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmFunction;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0006FD51 File Offset: 0x0006DF51
		public override string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0006FD59 File Offset: 0x0006DF59
		public ReadOnlyMetadataCollection<FunctionParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0006FD61 File Offset: 0x0006DF61
		internal bool HasUserDefinedBody
		{
			get
			{
				return this.IsModelDefinedFunction && !string.IsNullOrEmpty(this.CommandTextAttribute);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0006FD7B File Offset: 0x0006DF7B
		[MetadataProperty(BuiltInTypeKind.EntitySet, false)]
		internal EntitySet EntitySet
		{
			get
			{
				if (this._entitySets.Count == 0)
				{
					return null;
				}
				return this._entitySets[0];
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0006FD98 File Offset: 0x0006DF98
		[MetadataProperty(BuiltInTypeKind.EntitySet, true)]
		internal ReadOnlyMetadataCollection<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0006FDA0 File Offset: 0x0006DFA0
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, false)]
		public FunctionParameter ReturnParameter
		{
			get
			{
				return this._returnParameters.FirstOrDefault<FunctionParameter>();
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0006FDAD File Offset: 0x0006DFAD
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, true)]
		public ReadOnlyMetadataCollection<FunctionParameter> ReturnParameters
		{
			get
			{
				return this._returnParameters;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x0006FDB5 File Offset: 0x0006DFB5
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string StoreFunctionNameAttribute
		{
			get
			{
				return this._storeFunctionNameAttribute;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0006FDBD File Offset: 0x0006DFBD
		[MetadataProperty(typeof(ParameterTypeSemantics), false)]
		internal ParameterTypeSemantics ParameterTypeSemanticsAttribute
		{
			get
			{
				return this._parameterTypeSemantics;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0006FDC5 File Offset: 0x0006DFC5
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool AggregateAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.Aggregate);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0006FDCE File Offset: 0x0006DFCE
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool BuiltInAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.BuiltIn);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x0006FDD7 File Offset: 0x0006DFD7
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool IsFromProviderManifest
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFromProviderManifest);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0006FDE1 File Offset: 0x0006DFE1
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		internal bool NiladicFunctionAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.NiladicFunction);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0006FDEA File Offset: 0x0006DFEA
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsComposableAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsComposable);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0006FDF3 File Offset: 0x0006DFF3
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string CommandTextAttribute
		{
			get
			{
				return this._commandTextAttribute;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0006FDFB File Offset: 0x0006DFFB
		internal bool IsCachedStoreFunction
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsCachedStoreFunction);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0006FE05 File Offset: 0x0006E005
		internal bool IsModelDefinedFunction
		{
			get
			{
				return base.DataSpace == DataSpace.CSpace && !this.IsCachedStoreFunction && !this.IsFromProviderManifest && !this.IsFunctionImport;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0006FE2B File Offset: 0x0006E02B
		internal bool IsFunctionImport
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFunctionImport);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0006FE35 File Offset: 0x0006E035
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		internal string Schema
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0006FE40 File Offset: 0x0006E040
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Parameters.Source.SetReadOnly();
				foreach (FunctionParameter functionParameter in this.ReturnParameters)
				{
					functionParameter.SetReadOnly();
				}
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (base.CacheIdentity != null)
			{
				builder.Append(base.CacheIdentity);
				return;
			}
			EdmFunction.BuildIdentity<FunctionParameter>(builder, this.FullName, this.Parameters, (FunctionParameter param) => param.TypeUsage, (FunctionParameter param) => param.Mode);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0006FF28 File Offset: 0x0006E128
		internal static string BuildIdentity(string functionName, IEnumerable<TypeUsage> functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmFunction.BuildIdentity<TypeUsage>(stringBuilder, functionName, functionParameters, (TypeUsage param) => param, (TypeUsage param) => ParameterMode.In);
			return stringBuilder.ToString();
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0006FF88 File Offset: 0x0006E188
		internal static void BuildIdentity<TParameterMetadata>(StringBuilder builder, string functionName, IEnumerable<TParameterMetadata> functionParameters, Func<TParameterMetadata, TypeUsage> getParameterTypeUsage, Func<TParameterMetadata, ParameterMode> getParameterMode)
		{
			builder.Append(functionName);
			builder.Append('(');
			bool flag = true;
			foreach (TParameterMetadata arg in functionParameters)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(",");
				}
				builder.Append(Helper.ToString(getParameterMode(arg)));
				builder.Append(' ');
				getParameterTypeUsage(arg).BuildIdentity(builder);
			}
			builder.Append(')');
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00070024 File Offset: 0x0006E224
		private bool GetFunctionAttribute(EdmFunction.FunctionAttributes attribute)
		{
			return attribute == (attribute & this._functionAttributes);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00070031 File Offset: 0x0006E231
		private static void SetFunctionAttribute(ref EdmFunction.FunctionAttributes field, EdmFunction.FunctionAttributes attribute, bool isSet)
		{
			if (isSet)
			{
				field |= attribute;
				return;
			}
			field ^= (field & attribute);
		}

		// Token: 0x04000E1C RID: 3612
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _returnParameters;

		// Token: 0x04000E1D RID: 3613
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _parameters;

		// Token: 0x04000E1E RID: 3614
		private readonly EdmFunction.FunctionAttributes _functionAttributes = EdmFunction.FunctionAttributes.IsComposable;

		// Token: 0x04000E1F RID: 3615
		private readonly string _storeFunctionNameAttribute;

		// Token: 0x04000E20 RID: 3616
		private readonly ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x04000E21 RID: 3617
		private readonly string _commandTextAttribute;

		// Token: 0x04000E22 RID: 3618
		private readonly string _schemaName;

		// Token: 0x04000E23 RID: 3619
		private readonly ReadOnlyMetadataCollection<EntitySet> _entitySets;

		// Token: 0x04000E24 RID: 3620
		private readonly string _fullName;

		// Token: 0x02000517 RID: 1303
		[Flags]
		private enum FunctionAttributes : byte
		{
			// Token: 0x04001B1E RID: 6942
			None = 0,
			// Token: 0x04001B1F RID: 6943
			Aggregate = 1,
			// Token: 0x04001B20 RID: 6944
			BuiltIn = 2,
			// Token: 0x04001B21 RID: 6945
			NiladicFunction = 4,
			// Token: 0x04001B22 RID: 6946
			IsComposable = 8,
			// Token: 0x04001B23 RID: 6947
			IsFromProviderManifest = 16,
			// Token: 0x04001B24 RID: 6948
			IsCachedStoreFunction = 32,
			// Token: 0x04001B25 RID: 6949
			IsFunctionImport = 64,
			// Token: 0x04001B26 RID: 6950
			Default = 8
		}
	}
}
