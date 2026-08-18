using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DA RID: 1242
	public class EdmFunction : EdmType
	{
		// Token: 0x06002DAD RID: 11693 RVA: 0x000DC6DB File Offset: 0x000DA8DB
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace) : this(name, namespaceName, dataSpace, new EdmFunctionPayload())
		{
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000DC6FC File Offset: 0x000DA8FC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal EdmFunction(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload) : base(name, namespaceName, dataSpace)
		{
			this._schemaName = payload.Schema;
			IList<FunctionParameter> list = payload.ReturnParameters ?? ((IList<FunctionParameter>)new FunctionParameter[0]);
			foreach (FunctionParameter functionParameter in list)
			{
				if (functionParameter == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("ReturnParameters"));
				}
				if (functionParameter.Mode != ParameterMode.ReturnValue)
				{
					throw new ArgumentException(Strings.NonReturnParameterInReturnParameterCollection);
				}
			}
			this._returnParameters = new ReadOnlyMetadataCollection<FunctionParameter>((from returnParameter in list
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
				if (payload.EntitySets.Count != list.Count)
				{
					throw new ArgumentException(Strings.NumberOfEntitySetsDoesNotMatchNumberOfReturnParameters);
				}
				this._entitySets = new ReadOnlyCollection<EntitySet>(payload.EntitySets);
			}
			else
			{
				if (this._returnParameters.Count > 1)
				{
					throw new ArgumentException(Strings.NullEntitySetsForFunctionReturningMultipleResultSets);
				}
				this._entitySets = new ReadOnlyCollection<EntitySet>((from p in this._returnParameters
				select null).ToList<EntitySet>());
			}
			if (payload.CommandText != null)
			{
				this._commandTextAttribute = payload.CommandText;
			}
			if (payload.Parameters != null)
			{
				foreach (FunctionParameter functionParameter2 in payload.Parameters)
				{
					if (functionParameter2 == null)
					{
						throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("parameters"));
					}
					if (functionParameter2.Mode == ParameterMode.ReturnValue)
					{
						throw new ArgumentException(Strings.ReturnParameterInInputParameterCollection);
					}
				}
				this._parameters = new SafeLinkCollection<EdmFunction, FunctionParameter>(this, FunctionParameter.DeclaringFunctionLinker, new MetadataCollection<FunctionParameter>(payload.Parameters));
				return;
			}
			this._parameters = new ReadOnlyMetadataCollection<FunctionParameter>(new MetadataCollection<FunctionParameter>());
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000DCA88 File Offset: 0x000DAC88
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmFunction;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000DCA8C File Offset: 0x000DAC8C
		public override string FullName
		{
			get
			{
				return this.NamespaceName + "." + this.Name;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000DCAA4 File Offset: 0x000DACA4
		public ReadOnlyMetadataCollection<FunctionParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000DCAAC File Offset: 0x000DACAC
		public void AddParameter(FunctionParameter functionParameter)
		{
			Check.NotNull<FunctionParameter>(functionParameter, "functionParameter");
			Util.ThrowIfReadOnly(this);
			if (functionParameter.Mode == ParameterMode.ReturnValue)
			{
				throw new ArgumentException(Strings.ReturnParameterInInputParameterCollection);
			}
			this._parameters.Source.Add(functionParameter);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000DCAE5 File Offset: 0x000DACE5
		internal bool HasUserDefinedBody
		{
			get
			{
				return this.IsModelDefinedFunction && !string.IsNullOrEmpty(this.CommandTextAttribute);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000DCAFF File Offset: 0x000DACFF
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

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000DCB1C File Offset: 0x000DAD1C
		[MetadataProperty(BuiltInTypeKind.EntitySet, true)]
		internal ReadOnlyCollection<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000DCB24 File Offset: 0x000DAD24
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, false)]
		public FunctionParameter ReturnParameter
		{
			get
			{
				return this._returnParameters.FirstOrDefault<FunctionParameter>();
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000DCB31 File Offset: 0x000DAD31
		[MetadataProperty(BuiltInTypeKind.FunctionParameter, true)]
		public ReadOnlyMetadataCollection<FunctionParameter> ReturnParameters
		{
			get
			{
				return this._returnParameters;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000DCB39 File Offset: 0x000DAD39
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x000DCB41 File Offset: 0x000DAD41
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string StoreFunctionNameAttribute
		{
			get
			{
				return this._storeFunctionNameAttribute;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._storeFunctionNameAttribute = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000DCB5C File Offset: 0x000DAD5C
		internal string FunctionName
		{
			get
			{
				return this.StoreFunctionNameAttribute ?? this.Name;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000DCB6E File Offset: 0x000DAD6E
		[MetadataProperty(typeof(ParameterTypeSemantics), false)]
		public ParameterTypeSemantics ParameterTypeSemanticsAttribute
		{
			get
			{
				return this._parameterTypeSemantics;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000DCB76 File Offset: 0x000DAD76
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool AggregateAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.Aggregate);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000DCB7F File Offset: 0x000DAD7F
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public virtual bool BuiltInAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.BuiltIn);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000DCB88 File Offset: 0x000DAD88
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsFromProviderManifest
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFromProviderManifest);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000DCB92 File Offset: 0x000DAD92
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool NiladicFunctionAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.NiladicFunction);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000DCB9B File Offset: 0x000DAD9B
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Composable")]
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsComposableAttribute
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsComposable);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x000DCBA4 File Offset: 0x000DADA4
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string CommandTextAttribute
		{
			get
			{
				return this._commandTextAttribute;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000DCBAC File Offset: 0x000DADAC
		internal bool IsCachedStoreFunction
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsCachedStoreFunction);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000DCBB6 File Offset: 0x000DADB6
		internal bool IsModelDefinedFunction
		{
			get
			{
				return this.DataSpace == DataSpace.CSpace && !this.IsCachedStoreFunction && !this.IsFromProviderManifest && !this.IsFunctionImport;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000DCBDC File Offset: 0x000DADDC
		internal bool IsFunctionImport
		{
			get
			{
				return this.GetFunctionAttribute(EdmFunction.FunctionAttributes.IsFunctionImport);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000DCBE6 File Offset: 0x000DADE6
		// (set) Token: 0x06002DC6 RID: 11718 RVA: 0x000DCBEE File Offset: 0x000DADEE
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Schema
		{
			get
			{
				return this._schemaName;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._schemaName = value;
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000DCC0C File Offset: 0x000DAE0C
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

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000DCC90 File Offset: 0x000DAE90
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (base.CacheIdentity != null)
			{
				builder.Append(base.CacheIdentity);
				return;
			}
			EdmFunction.BuildIdentity<FunctionParameter>(builder, this.FullName, this.Parameters, (FunctionParameter param) => param.TypeUsage, (FunctionParameter param) => param.Mode);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000DCD08 File Offset: 0x000DAF08
		internal static string BuildIdentity(string functionName, IEnumerable<TypeUsage> functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmFunction.BuildIdentity<TypeUsage>(stringBuilder, functionName, functionParameters, (TypeUsage param) => param, (TypeUsage param) => ParameterMode.In);
			return stringBuilder.ToString();
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000DCD64 File Offset: 0x000DAF64
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

		// Token: 0x06002DCB RID: 11723 RVA: 0x000DCE00 File Offset: 0x000DB000
		private bool GetFunctionAttribute(EdmFunction.FunctionAttributes attribute)
		{
			return attribute == (attribute & this._functionAttributes);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000DCE0E File Offset: 0x000DB00E
		private static void SetFunctionAttribute(ref EdmFunction.FunctionAttributes field, EdmFunction.FunctionAttributes attribute, bool isSet)
		{
			if (isSet)
			{
				field |= attribute;
				return;
			}
			field ^= (field & attribute);
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000DCE28 File Offset: 0x000DB028
		public static EdmFunction Create(string name, string namespaceName, DataSpace dataSpace, EdmFunctionPayload payload, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			EdmFunction edmFunction = new EdmFunction(name, namespaceName, dataSpace, payload);
			if (metadataProperties != null)
			{
				edmFunction.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			edmFunction.SetReadOnly();
			return edmFunction;
		}

		// Token: 0x0400117E RID: 4478
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _returnParameters;

		// Token: 0x0400117F RID: 4479
		private readonly ReadOnlyMetadataCollection<FunctionParameter> _parameters;

		// Token: 0x04001180 RID: 4480
		private readonly EdmFunction.FunctionAttributes _functionAttributes = EdmFunction.FunctionAttributes.IsComposable;

		// Token: 0x04001181 RID: 4481
		private string _storeFunctionNameAttribute;

		// Token: 0x04001182 RID: 4482
		private readonly ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x04001183 RID: 4483
		private readonly string _commandTextAttribute;

		// Token: 0x04001184 RID: 4484
		private string _schemaName;

		// Token: 0x04001185 RID: 4485
		private readonly ReadOnlyCollection<EntitySet> _entitySets;

		// Token: 0x020004DB RID: 1243
		[Flags]
		private enum FunctionAttributes : byte
		{
			// Token: 0x0400118C RID: 4492
			Aggregate = 1,
			// Token: 0x0400118D RID: 4493
			BuiltIn = 2,
			// Token: 0x0400118E RID: 4494
			NiladicFunction = 4,
			// Token: 0x0400118F RID: 4495
			IsComposable = 8,
			// Token: 0x04001190 RID: 4496
			IsFromProviderManifest = 16,
			// Token: 0x04001191 RID: 4497
			IsCachedStoreFunction = 32,
			// Token: 0x04001192 RID: 4498
			IsFunctionImport = 64,
			// Token: 0x04001193 RID: 4499
			Default = 8
		}
	}
}
