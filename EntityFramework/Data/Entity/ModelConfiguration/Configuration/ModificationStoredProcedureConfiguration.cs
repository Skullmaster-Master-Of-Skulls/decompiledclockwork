using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002BE RID: 702
	internal class ModificationStoredProcedureConfiguration
	{
		// Token: 0x060018C7 RID: 6343 RVA: 0x0007AEC9 File Offset: 0x000790C9
		public ModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0007AF34 File Offset: 0x00079134
		private ModificationStoredProcedureConfiguration(ModificationStoredProcedureConfiguration source)
		{
			this._name = source._name;
			this._schema = source._schema;
			this._rowsAffectedParameter = source._rowsAffectedParameter;
			source._parameterNames.Each(delegate(KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> c)
			{
				this._parameterNames.Add(c.Key, Tuple.Create<string, string>(c.Value.Item1, c.Value.Item2));
			});
			source._resultBindings.Each(delegate(KeyValuePair<PropertyInfo, string> r)
			{
				this._resultBindings.Add(r.Key, r.Value);
			});
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007AFBD File Offset: 0x000791BD
		public virtual ModificationStoredProcedureConfiguration Clone()
		{
			return new ModificationStoredProcedureConfiguration(this);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0007AFC8 File Offset: 0x000791C8
		public void HasName(string name)
		{
			DatabaseName databaseName = DatabaseName.Parse(name);
			this._name = databaseName.Name;
			this._schema = databaseName.Schema;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0007AFF4 File Offset: 0x000791F4
		public void HasName(string name, string schema)
		{
			this._name = name;
			this._schema = schema;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0007B004 File Offset: 0x00079204
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0007B00C File Offset: 0x0007920C
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0007B014 File Offset: 0x00079214
		public void RowsAffectedParameter(string name)
		{
			this._rowsAffectedParameter = name;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0007B01D File Offset: 0x0007921D
		public string RowsAffectedParameterName
		{
			get
			{
				return this._rowsAffectedParameter;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0007B025 File Offset: 0x00079225
		public IEnumerable<Tuple<string, string>> ParameterNames
		{
			get
			{
				return this._parameterNames.Values;
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0007B032 File Offset: 0x00079232
		public void ClearParameterNames()
		{
			this._parameterNames.Clear();
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0007B03F File Offset: 0x0007923F
		public Dictionary<PropertyInfo, string> ResultBindings
		{
			get
			{
				return this._resultBindings;
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0007B047 File Offset: 0x00079247
		public void Parameter(PropertyPath propertyPath, string parameterName, string originalValueParameterName = null, bool rightKey = false)
		{
			this._parameterNames[new ModificationStoredProcedureConfiguration.ParameterKey(propertyPath, rightKey)] = Tuple.Create<string, string>(parameterName, originalValueParameterName);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0007B063 File Offset: 0x00079263
		public void Result(PropertyPath propertyPath, string columnName)
		{
			this._resultBindings[propertyPath.Single<PropertyInfo>()] = columnName;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0007B077 File Offset: 0x00079277
		public virtual void Configure(ModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			this._configuredParameters = new List<FunctionParameter>();
			this.ConfigureName(modificationStoredProcedureMapping);
			this.ConfigureSchema(modificationStoredProcedureMapping);
			this.ConfigureRowsAffectedParameter(modificationStoredProcedureMapping, providerManifest);
			this.ConfigureParameters(modificationStoredProcedureMapping);
			this.ConfigureResultBindings(modificationStoredProcedureMapping);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0007B0A8 File Offset: 0x000792A8
		private void ConfigureName(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			if (!string.IsNullOrWhiteSpace(this._name))
			{
				modificationStoredProcedureMapping.Function.StoreFunctionNameAttribute = this._name;
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0007B0C8 File Offset: 0x000792C8
		private void ConfigureSchema(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			if (!string.IsNullOrWhiteSpace(this._schema))
			{
				modificationStoredProcedureMapping.Function.Schema = this._schema;
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0007B0E8 File Offset: 0x000792E8
		private void ConfigureRowsAffectedParameter(ModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (!string.IsNullOrWhiteSpace(this._rowsAffectedParameter))
			{
				if (modificationStoredProcedureMapping.RowsAffectedParameter == null)
				{
					FunctionParameter functionParameter = new FunctionParameter("_RowsAffected_", providerManifest.GetStoreType(TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32))), ParameterMode.Out);
					modificationStoredProcedureMapping.Function.AddParameter(functionParameter);
					modificationStoredProcedureMapping.RowsAffectedParameter = functionParameter;
				}
				modificationStoredProcedureMapping.RowsAffectedParameter.Name = this._rowsAffectedParameter;
				this._configuredParameters.Add(modificationStoredProcedureMapping.RowsAffectedParameter);
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0007B2FC File Offset: 0x000794FC
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void ConfigureParameters(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			foreach (KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> keyValuePair in this._parameterNames)
			{
				PropertyPath propertyPath = keyValuePair.Key.PropertyPath;
				string item = keyValuePair.Value.Item1;
				string item2 = keyValuePair.Value.Item2;
				List<ModificationFunctionParameterBinding> list = modificationStoredProcedureMapping.ParameterBindings.Where(delegate(ModificationFunctionParameterBinding pb)
				{
					if (pb.MemberPath.AssociationSetEnd == null || pb.MemberPath.AssociationSetEnd.ParentAssociationSet.ElementType.IsManyToMany())
					{
						if (propertyPath.Equals(new PropertyPath(from m in pb.MemberPath.Members.OfType<EdmProperty>()
						select m.GetClrPropertyInfo())))
						{
							return true;
						}
					}
					if (propertyPath.Count == 2 && pb.MemberPath.AssociationSetEnd != null && pb.MemberPath.Members.First<EdmMember>().GetClrPropertyInfo().IsSameAs(propertyPath.Last<PropertyInfo>()))
					{
						return (from ae in pb.MemberPath.AssociationSetEnd.ParentAssociationSet.AssociationSetEnds
						select ae.CorrespondingAssociationEndMember.GetClrPropertyInfo() into pi
						where pi != null
						select pi).Any((PropertyInfo pi) => pi.IsSameAs(propertyPath.First<PropertyInfo>()));
					}
					return false;
				}).ToList<ModificationFunctionParameterBinding>();
				if (list.Count == 1)
				{
					ModificationFunctionParameterBinding modificationFunctionParameterBinding = list.Single<ModificationFunctionParameterBinding>();
					if (!string.IsNullOrWhiteSpace(item2) && modificationFunctionParameterBinding.IsCurrent)
					{
						throw Error.ModificationFunctionParameterNotFoundOriginal(propertyPath, modificationStoredProcedureMapping.Function.FunctionName);
					}
					modificationFunctionParameterBinding.Parameter.Name = item;
					this._configuredParameters.Add(modificationFunctionParameterBinding.Parameter);
				}
				else
				{
					if (list.Count == 2)
					{
						if ((from pb in list
						select pb.IsCurrent).Distinct<bool>().Count<bool>() != 1)
						{
							goto IL_12B;
						}
						if (!list.All((ModificationFunctionParameterBinding pb) => pb.MemberPath.AssociationSetEnd != null))
						{
							goto IL_12B;
						}
						ModificationFunctionParameterBinding modificationFunctionParameterBinding2 = (!keyValuePair.Key.IsRightKey) ? list.First<ModificationFunctionParameterBinding>() : list.Last<ModificationFunctionParameterBinding>();
						IL_16C:
						ModificationFunctionParameterBinding modificationFunctionParameterBinding3 = modificationFunctionParameterBinding2;
						modificationFunctionParameterBinding3.Parameter.Name = item;
						this._configuredParameters.Add(modificationFunctionParameterBinding3.Parameter);
						if (!string.IsNullOrWhiteSpace(item2))
						{
							modificationFunctionParameterBinding3 = list.Single((ModificationFunctionParameterBinding pb) => !pb.IsCurrent);
							modificationFunctionParameterBinding3.Parameter.Name = item2;
							this._configuredParameters.Add(modificationFunctionParameterBinding3.Parameter);
							continue;
						}
						continue;
						IL_12B:
						modificationFunctionParameterBinding2 = list.Single((ModificationFunctionParameterBinding pb) => pb.IsCurrent);
						goto IL_16C;
					}
					throw Error.ModificationFunctionParameterNotFound(propertyPath, modificationStoredProcedureMapping.Function.FunctionName);
				}
			}
			IEnumerable<FunctionParameter> enumerable = modificationStoredProcedureMapping.Function.Parameters.Except(this._configuredParameters);
			foreach (FunctionParameter functionParameter in enumerable)
			{
				functionParameter.Name = modificationStoredProcedureMapping.Function.Parameters.Except(new FunctionParameter[]
				{
					functionParameter
				}).UniquifyName(functionParameter.Name);
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0007B5E4 File Offset: 0x000797E4
		private void ConfigureResultBindings(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			foreach (KeyValuePair<PropertyInfo, string> keyValuePair in this._resultBindings)
			{
				PropertyInfo propertyInfo = keyValuePair.Key;
				string value = keyValuePair.Value;
				ModificationFunctionResultBinding modificationFunctionResultBinding = (modificationStoredProcedureMapping.ResultBindings ?? Enumerable.Empty<ModificationFunctionResultBinding>()).SingleOrDefault((ModificationFunctionResultBinding rb) => propertyInfo.IsSameAs(rb.Property.GetClrPropertyInfo()));
				if (modificationFunctionResultBinding == null)
				{
					throw Error.ResultBindingNotFound(propertyInfo.Name, modificationStoredProcedureMapping.Function.FunctionName);
				}
				modificationFunctionResultBinding.ColumnName = value;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0007B6C4 File Offset: 0x000798C4
		public bool IsCompatibleWith(ModificationStoredProcedureConfiguration other)
		{
			if (this._name != null && other._name != null && !string.Equals(this._name, other._name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (this._schema != null && other._schema != null && !string.Equals(this._schema, other._schema, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return !(from kv1 in this._parameterNames
			join kv2 in other._parameterNames on kv1.Key equals kv2.Key
			select !object.Equals(kv1.Value, kv2.Value)).Any((bool j) => j);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0007B800 File Offset: 0x00079A00
		public void Merge(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration, bool allowOverride)
		{
			if (allowOverride || string.IsNullOrWhiteSpace(this._name))
			{
				this._name = (modificationStoredProcedureConfiguration.Name ?? this._name);
			}
			if (allowOverride || string.IsNullOrWhiteSpace(this._schema))
			{
				this._schema = (modificationStoredProcedureConfiguration.Schema ?? this._schema);
			}
			if (allowOverride || string.IsNullOrWhiteSpace(this._rowsAffectedParameter))
			{
				this._rowsAffectedParameter = (modificationStoredProcedureConfiguration.RowsAffectedParameterName ?? this._rowsAffectedParameter);
			}
			foreach (KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> keyValuePair in from parameterName in modificationStoredProcedureConfiguration._parameterNames
			where allowOverride || !this._parameterNames.ContainsKey(parameterName.Key)
			select parameterName)
			{
				this._parameterNames[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<PropertyInfo, string> keyValuePair2 in from resultBinding in modificationStoredProcedureConfiguration.ResultBindings
			where allowOverride || !this._resultBindings.ContainsKey(resultBinding.Key)
			select resultBinding)
			{
				this._resultBindings[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x04000881 RID: 2177
		private readonly Dictionary<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> _parameterNames = new Dictionary<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>>();

		// Token: 0x04000882 RID: 2178
		private readonly Dictionary<PropertyInfo, string> _resultBindings = new Dictionary<PropertyInfo, string>();

		// Token: 0x04000883 RID: 2179
		private string _name;

		// Token: 0x04000884 RID: 2180
		private string _schema;

		// Token: 0x04000885 RID: 2181
		private string _rowsAffectedParameter;

		// Token: 0x04000886 RID: 2182
		private List<FunctionParameter> _configuredParameters;

		// Token: 0x020002BF RID: 703
		private sealed class ParameterKey
		{
			// Token: 0x060018E7 RID: 6375 RVA: 0x0007B96C File Offset: 0x00079B6C
			public ParameterKey(PropertyPath propertyPath, bool rightKey)
			{
				this._propertyPath = propertyPath;
				this._rightKey = rightKey;
			}

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0007B982 File Offset: 0x00079B82
			public PropertyPath PropertyPath
			{
				get
				{
					return this._propertyPath;
				}
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0007B98A File Offset: 0x00079B8A
			public bool IsRightKey
			{
				get
				{
					return this._rightKey;
				}
			}

			// Token: 0x060018EA RID: 6378 RVA: 0x0007B994 File Offset: 0x00079B94
			public override bool Equals(object obj)
			{
				if (object.ReferenceEquals(null, obj))
				{
					return false;
				}
				if (object.ReferenceEquals(this, obj))
				{
					return true;
				}
				ModificationStoredProcedureConfiguration.ParameterKey parameterKey = (ModificationStoredProcedureConfiguration.ParameterKey)obj;
				return this._propertyPath.Equals(parameterKey._propertyPath) && this._rightKey.Equals(parameterKey._rightKey);
			}

			// Token: 0x060018EB RID: 6379 RVA: 0x0007B9E8 File Offset: 0x00079BE8
			public override int GetHashCode()
			{
				return this._propertyPath.GetHashCode() * 397 ^ this._rightKey.GetHashCode();
			}

			// Token: 0x0400088F RID: 2191
			private readonly PropertyPath _propertyPath;

			// Token: 0x04000890 RID: 2192
			private readonly bool _rightKey;
		}
	}
}
