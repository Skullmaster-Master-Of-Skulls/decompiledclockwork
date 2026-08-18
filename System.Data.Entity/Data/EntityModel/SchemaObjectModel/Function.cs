using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F0 RID: 752
	internal class Function : SchemaType
	{
		// Token: 0x06002CE3 RID: 11491 RVA: 0x000AA8BC File Offset: 0x000A8ABC
		internal static void RemoveTypeModifier(ref string type, out TypeModifier typeModifier, out bool isRefType)
		{
			isRefType = false;
			typeModifier = TypeModifier.None;
			Match match = Function.s_typeParser.Match(type);
			if (!match.Success)
			{
				return;
			}
			type = match.Groups["typeName"].Value;
			string value = match.Groups["modifier"].Value;
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

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000AA934 File Offset: 0x000A8B34
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

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000AA972 File Offset: 0x000A8B72
		public Function(Schema parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000AA982 File Offset: 0x000A8B82
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000AA98A File Offset: 0x000A8B8A
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

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000AA993 File Offset: 0x000A8B93
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000AA99B File Offset: 0x000A8B9B
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000AA9A4 File Offset: 0x000A8BA4
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000AA9AC File Offset: 0x000A8BAC
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000AA9B5 File Offset: 0x000A8BB5
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000AA9BD File Offset: 0x000A8BBD
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

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000AA9C6 File Offset: 0x000A8BC6
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

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000AA9DD File Offset: 0x000A8BDD
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000AA9E5 File Offset: 0x000A8BE5
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

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000AA9EE File Offset: 0x000A8BEE
		// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x000AA9F6 File Offset: 0x000A8BF6
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

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000AA9FF File Offset: 0x000A8BFF
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

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000AAA21 File Offset: 0x000A8C21
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000AAA38 File Offset: 0x000A8C38
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

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000AAA53 File Offset: 0x000A8C53
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x000AAA5B File Offset: 0x000A8C5B
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

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000AAA64 File Offset: 0x000A8C64
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x000AAB24 File Offset: 0x000A8D24
		public bool IsReturnAttributeReftype
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x000173E2 File Offset: 0x000155E2
		public virtual bool IsFunctionImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x000AAB2C File Offset: 0x000A8D2C
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000AAB34 File Offset: 0x000A8D34
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
					base.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					base.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000AABD0 File Offset: 0x000A8DD0
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

		// Token: 0x06002CFE RID: 11518 RVA: 0x000AAC9C File Offset: 0x000A8E9C
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

		// Token: 0x06002CFF RID: 11519 RVA: 0x000AAD58 File Offset: 0x000A8F58
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

		// Token: 0x06002D00 RID: 11520 RVA: 0x000AAFC0 File Offset: 0x000A91C0
		internal override void ResolveSecondLevelNames()
		{
			foreach (Parameter parameter in this._parameters)
			{
				parameter.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000AB00C File Offset: 0x000A920C
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000AB014 File Offset: 0x000A9214
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
				AddErrorKind addErrorKind = clone.Parameters.TryAdd((Parameter)parameter.Clone(clone));
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000AB104 File Offset: 0x000A9304
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x000AB10C File Offset: 0x000A930C
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

		// Token: 0x06002D05 RID: 11525 RVA: 0x000AB115 File Offset: 0x000A9315
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000AB124 File Offset: 0x000A9324
		private void HandleAggregateAttribute(XmlReader reader)
		{
			bool isAggregate = false;
			base.HandleBoolAttribute(reader, ref isAggregate);
			this.IsAggregate = isAggregate;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000AB144 File Offset: 0x000A9344
		private void HandleBuiltInAttribute(XmlReader reader)
		{
			bool isBuiltIn = false;
			base.HandleBoolAttribute(reader, ref isBuiltIn);
			this.IsBuiltIn = isBuiltIn;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000AB164 File Offset: 0x000A9364
		private void HandleStoreFunctionNameAttribute(XmlReader reader)
		{
			string text = reader.Value.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				this.StoreFunctionName = text;
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000AB194 File Offset: 0x000A9394
		private void HandleNiladicFunctionAttribute(XmlReader reader)
		{
			bool isNiladicFunction = false;
			base.HandleBoolAttribute(reader, ref isNiladicFunction);
			this.IsNiladicFunction = isNiladicFunction;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000AB1B4 File Offset: 0x000A93B4
		private void HandleIsComposableAttribute(XmlReader reader)
		{
			bool isComposable = true;
			base.HandleBoolAttribute(reader, ref isComposable);
			this.IsComposable = isComposable;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000AB1D4 File Offset: 0x000A93D4
		private void HandleCommandTextFunctionElment(XmlReader reader)
		{
			FunctionCommandText functionCommandText = new FunctionCommandText(this);
			functionCommandText.Parse(reader);
			this._commandText = functionCommandText;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000AB1F8 File Offset: 0x000A93F8
		protected virtual void HandleReturnTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier != TypeModifier.None && typeModifier == TypeModifier.Array)
			{
				this.CollectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this.UnresolvedReturnType = text;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000AB24C File Offset: 0x000A944C
		protected void HandleParameterElement(XmlReader reader)
		{
			Parameter parameter = new Parameter(this);
			parameter.Parse(reader);
			this.Parameters.Add(parameter, true, new Func<object, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000AB280 File Offset: 0x000A9480
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

		// Token: 0x06002D0F RID: 11535 RVA: 0x000AB2BC File Offset: 0x000A94BC
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
				if (text == "ExactMatchOnly")
				{
					this.ParameterTypeSemantics = ParameterTypeSemantics.ExactMatchOnly;
					return;
				}
				if (text == "AllowImplicitPromotion")
				{
					this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitPromotion;
					return;
				}
				if (text == "AllowImplicitConversion")
				{
					this.ParameterTypeSemantics = ParameterTypeSemantics.AllowImplicitConversion;
					return;
				}
				base.AddError(ErrorCode.InvalidValueForParameterTypeSemantics, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidValueForParameterTypeSemanticsAttribute(text));
			}
		}

		// Token: 0x040013BC RID: 5052
		private bool _isAggregate;

		// Token: 0x040013BD RID: 5053
		private bool _isBuiltIn;

		// Token: 0x040013BE RID: 5054
		private bool _isNiladicFunction;

		// Token: 0x040013BF RID: 5055
		protected bool _isComposable = true;

		// Token: 0x040013C0 RID: 5056
		protected FunctionCommandText _commandText;

		// Token: 0x040013C1 RID: 5057
		private string _storeFunctionName;

		// Token: 0x040013C2 RID: 5058
		protected SchemaType _type;

		// Token: 0x040013C3 RID: 5059
		private string _unresolvedType;

		// Token: 0x040013C4 RID: 5060
		protected bool _isRefType;

		// Token: 0x040013C5 RID: 5061
		protected SchemaElementLookUpTable<Parameter> _parameters;

		// Token: 0x040013C6 RID: 5062
		protected List<ReturnType> _returnTypeList;

		// Token: 0x040013C7 RID: 5063
		private CollectionKind _returnTypeCollectionKind;

		// Token: 0x040013C8 RID: 5064
		private ParameterTypeSemantics _parameterTypeSemantics;

		// Token: 0x040013C9 RID: 5065
		private string _schema;

		// Token: 0x040013CA RID: 5066
		private string _functionStrongName;

		// Token: 0x040013CB RID: 5067
		private static Regex s_typeParser = new Regex("^(?<modifier>((Collection)|(Ref)))\\s*\\(\\s*(?<typeName>\\S*)\\s*\\)$", RegexOptions.Compiled);
	}
}
