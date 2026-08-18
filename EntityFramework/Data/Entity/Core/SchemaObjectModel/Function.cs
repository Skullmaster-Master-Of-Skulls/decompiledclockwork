using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000367 RID: 871
	internal class Function : SchemaType
	{
		// Token: 0x06001F21 RID: 7969 RVA: 0x00094920 File Offset: 0x00092B20
		internal static void RemoveTypeModifier(ref string type, out TypeModifier typeModifier, out bool isRefType)
		{
			isRefType = false;
			typeModifier = TypeModifier.None;
			Match match = Function._typeParser.Match(type);
			if (match.Success)
			{
				type = match.Groups["typeName"].Value;
				string value;
				if ((value = match.Groups["modifier"].Value) != null)
				{
					if (value == "Collection")
					{
						typeModifier = TypeModifier.Array;
						return;
					}
					if (!(value == "Ref"))
					{
						return;
					}
					isRefType = true;
				}
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0009499C File Offset: 0x00092B9C
		internal static string GetTypeNameForErrorMessage(SchemaType type, CollectionKind colKind, bool isRef)
		{
			string text = type.FQName;
			if (isRef)
			{
				text = "Ref(" + text + ")";
			}
			if (colKind == CollectionKind.Bag)
			{
				text = "Collection(" + text + ")";
			}
			return text;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000949DC File Offset: 0x00092BDC
		public Function(Schema parentElement) : base(parentElement)
		{
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000949EC File Offset: 0x00092BEC
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000949F4 File Offset: 0x00092BF4
		public bool IsAggregate
		{
			get
			{
				return this._isAggregate;
			}
			internal set
			{
				this._isAggregate = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000949FD File Offset: 0x00092BFD
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x00094A05 File Offset: 0x00092C05
		public bool IsBuiltIn
		{
			get
			{
				return this._isBuiltIn;
			}
			internal set
			{
				this._isBuiltIn = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00094A0E File Offset: 0x00092C0E
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x00094A16 File Offset: 0x00092C16
		public bool IsNiladicFunction
		{
			get
			{
				return this._isNiladicFunction;
			}
			internal set
			{
				this._isNiladicFunction = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00094A1F File Offset: 0x00092C1F
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x00094A27 File Offset: 0x00092C27
		public bool IsComposable
		{
			get
			{
				return this._isComposable;
			}
			internal set
			{
				this._isComposable = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00094A30 File Offset: 0x00092C30
		public string CommandText
		{
			get
			{
				if (this._commandText != null)
				{
					return this._commandText.CommandText;
				}
				return null;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00094A47 File Offset: 0x00092C47
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x00094A4F File Offset: 0x00092C4F
		public ParameterTypeSemantics ParameterTypeSemantics
		{
			get
			{
				return this._parameterTypeSemantics;
			}
			internal set
			{
				this._parameterTypeSemantics = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00094A58 File Offset: 0x00092C58
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x00094A60 File Offset: 0x00092C60
		public string StoreFunctionName
		{
			get
			{
				return this._storeFunctionName;
			}
			internal set
			{
				this._storeFunctionName = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00094A69 File Offset: 0x00092C69
		public virtual SchemaType Type
		{
			get
			{
				if (this._returnTypeList != null)
				{
					return this._returnTypeList[0].Type;
				}
				return this._type;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00094A8B File Offset: 0x00092C8B
		public IList<ReturnType> ReturnTypeList
		{
			get
			{
				if (this._returnTypeList == null)
				{
					return null;
				}
				return new ReadOnlyCollection<ReturnType>(this._returnTypeList);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00094AA2 File Offset: 0x00092CA2
		public SchemaElementLookUpTable<Parameter> Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new SchemaElementLookUpTable<Parameter>();
				}
				return this._parameters;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00094ABD File Offset: 0x00092CBD
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x00094AC5 File Offset: 0x00092CC5
		public CollectionKind CollectionKind
		{
			get
			{
				return this._returnTypeCollectionKind;
			}
			internal set
			{
				this._returnTypeCollectionKind = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x00094AD0 File Offset: 0x00092CD0
		public override string Identity
		{
			get
			{
				if (string.IsNullOrEmpty(this._functionStrongName))
				{
					string fqname = this.FQName;
					StringBuilder stringBuilder = new StringBuilder(fqname);
					bool flag = true;
					stringBuilder.Append('(');
					foreach (Parameter parameter in this.Parameters)
					{
						if (!flag)
						{
							stringBuilder.Append(',');
						}
						else
						{
							flag = false;
						}
						stringBuilder.Append(Helper.ToString(parameter.ParameterDirection));
						stringBuilder.Append(' ');
						parameter.WriteIdentity(stringBuilder);
					}
					stringBuilder.Append(')');
					this._functionStrongName = stringBuilder.ToString();
				}
				return this._functionStrongName;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x00094B94 File Offset: 0x00092D94
		public bool IsReturnAttributeReftype
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00094B9C File Offset: 0x00092D9C
		public virtual bool IsFunctionImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00094B9F File Offset: 0x00092D9F
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00094BA8 File Offset: 0x00092DA8
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "CommandText"))
			{
				this.HandleCommandTextFunctionElment(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Parameter"))
			{
				this.HandleParameterElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReturnType"))
			{
				this.HandleReturnTypeElement(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00094C44 File Offset: 0x00092E44
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ReturnType"))
			{
				this.HandleReturnTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Aggregate"))
			{
				this.HandleAggregateAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "BuiltIn"))
			{
				this.HandleBuiltInAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "StoreFunctionName"))
			{
				this.HandleStoreFunctionNameAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "NiladicFunction"))
			{
				this.HandleNiladicFunctionAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "IsComposable"))
			{
				this.HandleIsComposableAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ParameterTypeSemantics"))
			{
				this.HandleParameterTypeSemanticsAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Schema"))
			{
				this.HandleDbSchemaAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00094D10 File Offset: 0x00092F10
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._unresolvedType != null)
			{
				base.Schema.ResolveTypeName(this, this.UnresolvedReturnType, out this._type);
			}
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					returnType.ResolveTopLevelNames();
				}
			}
			foreach (Parameter parameter in this.Parameters)
			{
				parameter.ResolveTopLevelNames();
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00094DCC File Offset: 0x00092FCC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal override void Validate()
		{
			base.Validate();
			if (this._type != null && this._returnTypeList != null)
			{
				base.AddError(ErrorCode.ReturnTypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.TypeDeclaredAsAttributeAndElement);
			}
			if (this._returnTypeList == null && this.Type == null)
			{
				if (this.IsComposable)
				{
					base.AddError(ErrorCode.ComposableFunctionOrFunctionImportWithoutReturnType, EdmSchemaErrorSeverity.Error, Strings.ComposableFunctionOrFunctionImportMustDeclareReturnType);
				}
			}
			else if (!this.IsComposable && !this.IsFunctionImport)
			{
				base.AddError(ErrorCode.NonComposableFunctionWithReturnType, EdmSchemaErrorSeverity.Error, Strings.NonComposableFunctionMustNotDeclareReturnType);
			}
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				if (this.IsAggregate)
				{
					if (this.Parameters.Count != 1)
					{
						base.AddError(ErrorCode.InvalidNumberOfParametersForAggregateFunction, EdmSchemaErrorSeverity.Error, this, Strings.InvalidNumberOfParametersForAggregateFunction(this.FQName));
					}
					else if (this.Parameters.GetElementAt(0).CollectionKind == CollectionKind.None)
					{
						Parameter elementAt = this.Parameters.GetElementAt(0);
						base.AddError(ErrorCode.InvalidParameterTypeForAggregateFunction, EdmSchemaErrorSeverity.Error, this, Strings.InvalidParameterTypeForAggregateFunction(elementAt.Name, this.FQName));
					}
				}
				if (!this.IsComposable && (this.IsAggregate || this.IsNiladicFunction || this.IsBuiltIn))
				{
					base.AddError(ErrorCode.NonComposableFunctionAttributesNotValid, EdmSchemaErrorSeverity.Error, Strings.NonComposableFunctionHasDisallowedAttribute);
				}
				if (this.CommandText != null)
				{
					if (this.IsComposable)
					{
						base.AddError(ErrorCode.ComposableFunctionWithCommandText, EdmSchemaErrorSeverity.Error, Strings.CommandTextFunctionsNotComposable);
					}
					if (this.StoreFunctionName != null)
					{
						base.AddError(ErrorCode.FunctionDeclaresCommandTextAndStoreFunctionName, EdmSchemaErrorSeverity.Error, Strings.CommandTextFunctionsCannotDeclareStoreFunctionName);
					}
				}
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel && this._type != null && (!(this._type is ScalarType) || this._returnTypeCollectionKind != CollectionKind.None))
			{
				base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(Function.GetTypeNameForErrorMessage(this._type, this._returnTypeCollectionKind, this._isRefType), this.FQName));
			}
			if (this._returnTypeList != null)
			{
				foreach (ReturnType returnType in this._returnTypeList)
				{
					returnType.Validate();
				}
			}
			if (this._parameters != null)
			{
				foreach (Parameter parameter in this._parameters)
				{
					parameter.Validate();
				}
			}
			if (this._commandText != null)
			{
				this._commandText.Validate();
			}
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00095034 File Offset: 0x00093234
		internal override void ResolveSecondLevelNames()
		{
			foreach (Parameter parameter in this._parameters)
			{
				parameter.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00095080 File Offset: 0x00093280
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00095088 File Offset: 0x00093288
		protected void CloneSetFunctionFields(Function clone)
		{
			clone._isAggregate = this._isAggregate;
			clone._isBuiltIn = this._isBuiltIn;
			clone._isNiladicFunction = this._isNiladicFunction;
			clone._isComposable = this._isComposable;
			clone._commandText = this._commandText;
			clone._storeFunctionName = this._storeFunctionName;
			clone._type = this._type;
			clone._returnTypeList = this._returnTypeList;
			clone._returnTypeCollectionKind = this._returnTypeCollectionKind;
			clone._parameterTypeSemantics = this._parameterTypeSemantics;
			clone._schema = this._schema;
			clone.Name = this.Name;
			foreach (Parameter parameter in this.Parameters)
			{
				clone.Parameters.TryAdd((Parameter)parameter.Clone(clone));
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00095178 File Offset: 0x00093378
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x00095180 File Offset: 0x00093380
		internal string UnresolvedReturnType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00095189 File Offset: 0x00093389
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00095198 File Offset: 0x00093398
		private void HandleAggregateAttribute(XmlReader reader)
		{
			bool isAggregate = false;
			base.HandleBoolAttribute(reader, ref isAggregate);
			this.IsAggregate = isAggregate;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000951B8 File Offset: 0x000933B8
		private void HandleBuiltInAttribute(XmlReader reader)
		{
			bool isBuiltIn = false;
			base.HandleBoolAttribute(reader, ref isBuiltIn);
			this.IsBuiltIn = isBuiltIn;
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000951D8 File Offset: 0x000933D8
		private void HandleStoreFunctionNameAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				this.StoreFunctionName = text;
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00095204 File Offset: 0x00093404
		private void HandleNiladicFunctionAttribute(XmlReader reader)
		{
			bool isNiladicFunction = false;
			base.HandleBoolAttribute(reader, ref isNiladicFunction);
			this.IsNiladicFunction = isNiladicFunction;
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00095224 File Offset: 0x00093424
		private void HandleIsComposableAttribute(XmlReader reader)
		{
			bool isComposable = true;
			base.HandleBoolAttribute(reader, ref isComposable);
			this.IsComposable = isComposable;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00095244 File Offset: 0x00093444
		private void HandleCommandTextFunctionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00095268 File Offset: 0x00093468
		protected virtual void HandleReturnTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			switch (typeModifier)
			{
			case TypeModifier.Array:
				this.CollectionKind = CollectionKind.Bag;
				break;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this.UnresolvedReturnType = text;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000952C8 File Offset: 0x000934C8
		protected void HandleParameterElement(XmlReader reader)
		{
			Parameter parameter = new Parameter(this);
			parameter.Parse(reader);
			this.Parameters.Add(parameter, true, new Func<object, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x000952FC File Offset: 0x000934FC
		protected void HandleReturnTypeElement(XmlReader reader)
		{
			ReturnType returnType = new ReturnType(this);
			returnType.Parse(reader);
			if (this._returnTypeList == null)
			{
				this._returnTypeList = new List<ReturnType>();
			}
			this._returnTypeList.Add(returnType);
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00095338 File Offset: 0x00093538
		private void HandleParameterTypeSemanticsAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				string a;
				if ((a = text) != null)
				{
					if (a == "ExactMatchOnly")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.ExactMatchOnly;
						return;
					}
					if (a == "AllowImplicitPromotion")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitPromotion;
						return;
					}
					if (a == "AllowImplicitConversion")
					{
						this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitConversion;
						return;
					}
				}
				base.AddError(ErrorCode.InvalidValueForParameterTypeSemantics, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidValueForParameterTypeSemanticsAttribute(text));
			}
		}

		// Token: 0x04000B2A RID: 2858
		private bool _isAggregate;

		// Token: 0x04000B2B RID: 2859
		private bool _isBuiltIn;

		// Token: 0x04000B2C RID: 2860
		private bool _isNiladicFunction;

		// Token: 0x04000B2D RID: 2861
		protected bool _isComposable = true;

		// Token: 0x04000B2E RID: 2862
		protected FunctionCommandText _commandText;

		// Token: 0x04000B2F RID: 2863
		private string _storeFunctionName;

		// Token: 0x04000B30 RID: 2864
		protected SchemaType _type;

		// Token: 0x04000B31 RID: 2865
		private string _unresolvedType;

		// Token: 0x04000B32 RID: 2866
		protected bool _isRefType;

		// Token: 0x04000B33 RID: 2867
		protected SchemaElementLookUpTable<Parameter> _parameters;

		// Token: 0x04000B34 RID: 2868
		protected List<ReturnType> _returnTypeList;

		// Token: 0x04000B35 RID: 2869
		private CollectionKind _returnTypeCollectionKind;

		// Token: 0x04000B36 RID: 2870
		private ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x04000B37 RID: 2871
		private string _schema;

		// Token: 0x04000B38 RID: 2872
		private string _functionStrongName;

		// Token: 0x04000B39 RID: 2873
		private static readonly Regex _typeParser = new Regex("^(?<modifier>((Collection)|(Ref)))\\s*\\(\\s*(?<typeName>\\S*)\\s*\\)$", RegexOptions.Compiled);
	}
}
