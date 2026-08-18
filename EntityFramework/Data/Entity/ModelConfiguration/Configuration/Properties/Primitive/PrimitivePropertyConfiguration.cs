using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x020007D7 RID: 2007
	internal class PrimitivePropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06005B2B RID: 23339 RVA: 0x00187AC4 File Offset: 0x00185CC4
		public PrimitivePropertyConfiguration()
		{
			this.OverridableConfigurationParts = (OverridableConfigurationParts.OverridableInCSpace | OverridableConfigurationParts.OverridableInSSpace);
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x00187AE0 File Offset: 0x00185CE0
		protected PrimitivePropertyConfiguration(PrimitivePropertyConfiguration source)
		{
			Check.NotNull<PrimitivePropertyConfiguration>(source, "source");
			this.TypeConfiguration = source.TypeConfiguration;
			this.IsNullable = source.IsNullable;
			this.ConcurrencyMode = source.ConcurrencyMode;
			this.DatabaseGeneratedOption = source.DatabaseGeneratedOption;
			this.ColumnType = source.ColumnType;
			this.ColumnName = source.ColumnName;
			this.ParameterName = source.ParameterName;
			this.ColumnOrder = source.ColumnOrder;
			this.OverridableConfigurationParts = source.OverridableConfigurationParts;
			foreach (KeyValuePair<string, object> item in source._annotations)
			{
				this._annotations.Add(item);
			}
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x00187BBC File Offset: 0x00185DBC
		internal virtual PrimitivePropertyConfiguration Clone()
		{
			return new PrimitivePropertyConfiguration(this);
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00187BC4 File Offset: 0x00185DC4
		// (set) Token: 0x06005B2F RID: 23343 RVA: 0x00187BCC File Offset: 0x00185DCC
		public bool? IsNullable { get; set; }

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x00187BD5 File Offset: 0x00185DD5
		// (set) Token: 0x06005B31 RID: 23345 RVA: 0x00187BDD File Offset: 0x00185DDD
		public ConcurrencyMode? ConcurrencyMode { get; set; }

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x00187BE6 File Offset: 0x00185DE6
		// (set) Token: 0x06005B33 RID: 23347 RVA: 0x00187BEE File Offset: 0x00185DEE
		public DatabaseGeneratedOption? DatabaseGeneratedOption { get; set; }

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x00187BF7 File Offset: 0x00185DF7
		// (set) Token: 0x06005B35 RID: 23349 RVA: 0x00187BFF File Offset: 0x00185DFF
		public string ColumnType { get; set; }

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005B36 RID: 23350 RVA: 0x00187C08 File Offset: 0x00185E08
		// (set) Token: 0x06005B37 RID: 23351 RVA: 0x00187C10 File Offset: 0x00185E10
		public string ColumnName { get; set; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005B38 RID: 23352 RVA: 0x00187C19 File Offset: 0x00185E19
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00187C21 File Offset: 0x00185E21
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x00187C44 File Offset: 0x00185E44
		// (set) Token: 0x06005B3B RID: 23355 RVA: 0x00187C4C File Offset: 0x00185E4C
		public string ParameterName { get; set; }

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x00187C55 File Offset: 0x00185E55
		// (set) Token: 0x06005B3D RID: 23357 RVA: 0x00187C5D File Offset: 0x00185E5D
		public int? ColumnOrder { get; set; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x00187C66 File Offset: 0x00185E66
		// (set) Token: 0x06005B3F RID: 23359 RVA: 0x00187C6E File Offset: 0x00185E6E
		internal OverridableConfigurationParts OverridableConfigurationParts { get; set; }

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x00187C77 File Offset: 0x00185E77
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x00187C7F File Offset: 0x00185E7F
		internal StructuralTypeConfiguration TypeConfiguration { get; set; }

		// Token: 0x06005B42 RID: 23362 RVA: 0x00187CDC File Offset: 0x00185EDC
		internal virtual void Configure(EdmProperty property)
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration = this.Clone();
			PrimitivePropertyConfiguration primitivePropertyConfiguration2 = primitivePropertyConfiguration.MergeWithExistingConfiguration(property, delegate(string errorMessage)
			{
				PropertyInfo clrPropertyInfo = property.GetClrPropertyInfo();
				string p = (clrPropertyInfo == null) ? string.Empty : ObjectContextTypeCache.GetObjectType(clrPropertyInfo.DeclaringType).FullNameWithNesting();
				return Error.ConflictingPropertyConfiguration(property.Name, p, errorMessage);
			}, true, false);
			primitivePropertyConfiguration2.ConfigureProperty(property);
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x00187D24 File Offset: 0x00185F24
		private PrimitivePropertyConfiguration MergeWithExistingConfiguration(EdmProperty property, Func<string, Exception> getConflictException, bool inCSpace, bool fillFromExistingConfiguration)
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration = property.GetConfiguration() as PrimitivePropertyConfiguration;
			if (primitivePropertyConfiguration == null)
			{
				return this;
			}
			OverridableConfigurationParts overridableConfigurationParts = inCSpace ? OverridableConfigurationParts.OverridableInCSpace : OverridableConfigurationParts.OverridableInSSpace;
			if (primitivePropertyConfiguration.OverridableConfigurationParts.HasFlag(overridableConfigurationParts) || fillFromExistingConfiguration)
			{
				return primitivePropertyConfiguration.OverrideFrom(this, inCSpace);
			}
			string arg;
			if (this.OverridableConfigurationParts.HasFlag(overridableConfigurationParts) || primitivePropertyConfiguration.IsCompatible(this, inCSpace, out arg))
			{
				return this.OverrideFrom(primitivePropertyConfiguration, inCSpace);
			}
			throw getConflictException(arg);
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x00187DA3 File Offset: 0x00185FA3
		private PrimitivePropertyConfiguration OverrideFrom(PrimitivePropertyConfiguration overridingConfiguration, bool inCSpace)
		{
			if (overridingConfiguration.GetType().IsAssignableFrom(base.GetType()))
			{
				this.MakeCompatibleWith(overridingConfiguration, inCSpace);
				this.FillFrom(overridingConfiguration, inCSpace);
				return this;
			}
			overridingConfiguration.FillFrom(this, inCSpace);
			return overridingConfiguration;
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x00187DD4 File Offset: 0x00185FD4
		protected virtual void ConfigureProperty(EdmProperty property)
		{
			if (this.IsNullable != null)
			{
				property.Nullable = this.IsNullable.Value;
			}
			if (this.ConcurrencyMode != null)
			{
				property.ConcurrencyMode = this.ConcurrencyMode.Value;
			}
			if (this.DatabaseGeneratedOption != null)
			{
				property.SetStoreGeneratedPattern((StoreGeneratedPattern)this.DatabaseGeneratedOption.Value);
				if (this.DatabaseGeneratedOption.Value == System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
				{
					property.Nullable = false;
				}
			}
			property.SetConfiguration(this);
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x00187EA8 File Offset: 0x001860A8
		internal void Configure(IEnumerable<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false, bool fillFromExistingConfiguration = false)
		{
			propertyMappings.Each(delegate(Tuple<ColumnMappingBuilder, EntityType> pm)
			{
				this.Configure(pm.Item1.ColumnProperty, pm.Item2, providerManifest, allowOverride, fillFromExistingConfiguration);
			});
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x00187EEA File Offset: 0x001860EA
		internal void ConfigureFunctionParameters(IEnumerable<FunctionParameter> parameters)
		{
			parameters.Each(new Action<FunctionParameter>(this.ConfigureParameterName));
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x001880BC File Offset: 0x001862BC
		private void ConfigureParameterName(FunctionParameter parameter)
		{
			if (string.IsNullOrWhiteSpace(this.ParameterName) || string.Equals(this.ParameterName, parameter.Name, StringComparison.Ordinal))
			{
				return;
			}
			parameter.Name = this.ParameterName;
			IEnumerable<FunctionParameter> ts = from p in parameter.DeclaringFunction.Parameters
			let configuration = p.GetConfiguration() as PrimitivePropertyConfiguration
			where p != parameter && string.Equals(this.ParameterName, p.Name, StringComparison.Ordinal) && (configuration == null || configuration.ParameterName == null)
			select p;
			List<FunctionParameter> renamedParameters = new List<FunctionParameter>
			{
				parameter
			};
			ts.Each(delegate(FunctionParameter c)
			{
				c.Name = renamedParameters.UniquifyName(this.ParameterName);
				renamedParameters.Add(c);
			});
			parameter.SetConfiguration(this);
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x001881D8 File Offset: 0x001863D8
		internal void Configure(EdmProperty column, EntityType table, DbProviderManifest providerManifest, bool allowOverride = false, bool fillFromExistingConfiguration = false)
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration = this.Clone();
			if (allowOverride)
			{
				primitivePropertyConfiguration.OverridableConfigurationParts |= OverridableConfigurationParts.OverridableInSSpace;
			}
			PrimitivePropertyConfiguration primitivePropertyConfiguration2 = primitivePropertyConfiguration.MergeWithExistingConfiguration(column, (string errorMessage) => Error.ConflictingColumnConfiguration(column.Name, table.Name, errorMessage), false, fillFromExistingConfiguration);
			primitivePropertyConfiguration2.ConfigureColumn(column, table, providerManifest);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x00188278 File Offset: 0x00186478
		protected virtual void ConfigureColumn(EdmProperty column, EntityType table, DbProviderManifest providerManifest)
		{
			this.ConfigureColumnName(column, table);
			this.ConfigureAnnotations(column);
			if (!string.IsNullOrWhiteSpace(this.ColumnType))
			{
				column.PrimitiveType = providerManifest.GetStoreTypeFromName(this.ColumnType);
			}
			if (this.ColumnOrder != null)
			{
				column.SetOrder(this.ColumnOrder.Value);
			}
			PrimitiveType primitiveType = providerManifest.GetStoreTypes().SingleOrDefault((PrimitiveType t) => t.Name.Equals(column.TypeName, StringComparison.OrdinalIgnoreCase));
			if (primitiveType != null)
			{
				primitiveType.FacetDescriptions.Each(delegate(FacetDescription f)
				{
					this.Configure(column, f);
				});
			}
			column.SetConfiguration(this);
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x00188504 File Offset: 0x00186704
		private void ConfigureColumnName(EdmProperty column, EntityType table)
		{
			if (string.IsNullOrWhiteSpace(this.ColumnName) || string.Equals(this.ColumnName, column.Name, StringComparison.Ordinal))
			{
				return;
			}
			column.Name = this.ColumnName;
			IEnumerable<EdmProperty> ts = from c in table.Properties
			let configuration = c.GetConfiguration() as PrimitivePropertyConfiguration
			where c != column && string.Equals(this.ColumnName, c.GetPreferredName(), StringComparison.Ordinal) && (configuration == null || configuration.ColumnName == null)
			select c;
			List<EdmProperty> renamedColumns = new List<EdmProperty>
			{
				column
			};
			ts.Each(delegate(EdmProperty c)
			{
				c.Name = renamedColumns.UniquifyName(this.ColumnName);
				renamedColumns.Add(c);
			});
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x001885E4 File Offset: 0x001867E4
		private void ConfigureAnnotations(EdmProperty column)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this._annotations)
			{
				column.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x00188648 File Offset: 0x00186848
		internal virtual void Configure(EdmProperty column, FacetDescription facetDescription)
		{
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x0018864C File Offset: 0x0018684C
		internal virtual void CopyFrom(PrimitivePropertyConfiguration other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return;
			}
			this.ColumnName = other.ColumnName;
			this.ParameterName = other.ParameterName;
			this.ColumnOrder = other.ColumnOrder;
			this.ColumnType = other.ColumnType;
			this.ConcurrencyMode = other.ConcurrencyMode;
			this.DatabaseGeneratedOption = other.DatabaseGeneratedOption;
			this.IsNullable = other.IsNullable;
			this.OverridableConfigurationParts = other.OverridableConfigurationParts;
			this._annotations.Clear();
			foreach (KeyValuePair<string, object> keyValuePair in other._annotations)
			{
				this._annotations[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x00188720 File Offset: 0x00186920
		internal virtual void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			if (object.ReferenceEquals(this, other))
			{
				return;
			}
			if (inCSpace)
			{
				if (this.ConcurrencyMode == null)
				{
					this.ConcurrencyMode = other.ConcurrencyMode;
				}
				if (this.DatabaseGeneratedOption == null)
				{
					this.DatabaseGeneratedOption = other.DatabaseGeneratedOption;
				}
				if (this.IsNullable == null)
				{
					this.IsNullable = other.IsNullable;
				}
				if (!other.OverridableConfigurationParts.HasFlag(OverridableConfigurationParts.OverridableInCSpace))
				{
					this.OverridableConfigurationParts &= ~OverridableConfigurationParts.OverridableInCSpace;
					return;
				}
			}
			else
			{
				if (this.ColumnName == null)
				{
					this.ColumnName = other.ColumnName;
				}
				if (this.ParameterName == null)
				{
					this.ParameterName = other.ParameterName;
				}
				if (this.ColumnOrder == null)
				{
					this.ColumnOrder = other.ColumnOrder;
				}
				if (this.ColumnType == null)
				{
					this.ColumnType = other.ColumnType;
				}
				foreach (KeyValuePair<string, object> keyValuePair in other._annotations)
				{
					if (this._annotations.ContainsKey(keyValuePair.Key))
					{
						IMergeableAnnotation mergeableAnnotation = this._annotations[keyValuePair.Key] as IMergeableAnnotation;
						if (mergeableAnnotation != null)
						{
							this._annotations[keyValuePair.Key] = mergeableAnnotation.MergeWith(keyValuePair.Value);
						}
					}
					else
					{
						this._annotations[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				if (!other.OverridableConfigurationParts.HasFlag(OverridableConfigurationParts.OverridableInSSpace))
				{
					this.OverridableConfigurationParts &= ~OverridableConfigurationParts.OverridableInSSpace;
				}
			}
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x001888E4 File Offset: 0x00186AE4
		internal virtual void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			if (object.ReferenceEquals(this, other))
			{
				return;
			}
			if (inCSpace)
			{
				if (other.ConcurrencyMode != null)
				{
					this.ConcurrencyMode = null;
				}
				if (other.DatabaseGeneratedOption != null)
				{
					this.DatabaseGeneratedOption = null;
				}
				if (other.IsNullable != null)
				{
					this.IsNullable = null;
					return;
				}
			}
			else
			{
				if (other.ColumnName != null)
				{
					this.ColumnName = null;
				}
				if (other.ParameterName != null)
				{
					this.ParameterName = null;
				}
				if (other.ColumnOrder != null)
				{
					this.ColumnOrder = null;
				}
				if (other.ColumnType != null)
				{
					this.ColumnType = null;
				}
				foreach (string key in other._annotations.Keys)
				{
					if (this._annotations.ContainsKey(key))
					{
						IMergeableAnnotation mergeableAnnotation = this._annotations[key] as IMergeableAnnotation;
						if (mergeableAnnotation == null || !mergeableAnnotation.IsCompatibleWith(other._annotations[key]))
						{
							this._annotations.Remove(key);
						}
					}
				}
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x00188A40 File Offset: 0x00186C40
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
		internal virtual bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			errorMessage = string.Empty;
			if (other == null || object.ReferenceEquals(this, other))
			{
				return true;
			}
			bool flag = !inCSpace || this.IsCompatible<bool, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.IsNullable, other, ref errorMessage);
			bool flag2 = !inCSpace || this.IsCompatible<ConcurrencyMode, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ConcurrencyMode, other, ref errorMessage);
			bool flag3 = !inCSpace || this.IsCompatible<DatabaseGeneratedOption, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.DatabaseGeneratedOption, other, ref errorMessage);
			bool flag4 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnName, other, ref errorMessage);
			bool flag5 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ParameterName, other, ref errorMessage);
			bool flag6 = inCSpace || this.IsCompatible<int, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnOrder, other, ref errorMessage);
			bool flag7 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnType, other, ref errorMessage);
			bool flag8 = inCSpace || this.AnnotationsAreCompatible(other, ref errorMessage);
			return flag && flag2 && flag3 && flag4 && flag5 && flag6 && flag7 && flag8;
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00188CC4 File Offset: 0x00186EC4
		private bool AnnotationsAreCompatible(PrimitivePropertyConfiguration other, ref string errorMessage)
		{
			bool result = true;
			foreach (KeyValuePair<string, object> keyValuePair in this.Annotations)
			{
				if (other.Annotations.ContainsKey(keyValuePair.Key))
				{
					object value = keyValuePair.Value;
					object obj = other.Annotations[keyValuePair.Key];
					IMergeableAnnotation mergeableAnnotation = value as IMergeableAnnotation;
					if (mergeableAnnotation != null)
					{
						CompatibilityResult compatibilityResult = mergeableAnnotation.IsCompatibleWith(obj);
						if (!compatibilityResult)
						{
							result = false;
							errorMessage = errorMessage + Environment.NewLine + "\t" + compatibilityResult.ErrorMessage;
						}
					}
					else if (!object.Equals(value, obj))
					{
						result = false;
						errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingAnnotationValue(keyValuePair.Key, value.ToString(), obj.ToString());
					}
				}
			}
			return result;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x00188DC0 File Offset: 0x00186FC0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		protected bool IsCompatible<TProperty, TConfiguration>(Expression<Func<TConfiguration, TProperty?>> propertyExpression, TConfiguration other, ref string errorMessage) where TProperty : struct where TConfiguration : PrimitivePropertyConfiguration
		{
			Check.NotNull<Expression<Func<TConfiguration, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotNull<TConfiguration>(other, "other");
			PropertyInfo propertyInfo = propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			TProperty? tproperty = (TProperty?)propertyInfo.GetValue(this, null);
			TProperty? tproperty2 = (TProperty?)propertyInfo.GetValue(other, null);
			if (PrimitivePropertyConfiguration.IsCompatible<TProperty>(tproperty, tproperty2))
			{
				return true;
			}
			errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingConfigurationValue(propertyInfo.Name, tproperty, propertyInfo.Name, tproperty2);
			return false;
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x00188E50 File Offset: 0x00187050
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "2#")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected bool IsCompatible<TConfiguration>(Expression<Func<TConfiguration, string>> propertyExpression, TConfiguration other, ref string errorMessage) where TConfiguration : PrimitivePropertyConfiguration
		{
			Check.NotNull<Expression<Func<TConfiguration, string>>>(propertyExpression, "propertyExpression");
			Check.NotNull<TConfiguration>(other, "other");
			PropertyInfo propertyInfo = propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			string text = (string)propertyInfo.GetValue(this, null);
			string text2 = (string)propertyInfo.GetValue(other, null);
			if (PrimitivePropertyConfiguration.IsCompatible(text, text2))
			{
				return true;
			}
			errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingConfigurationValue(propertyInfo.Name, text, propertyInfo.Name, text2);
			return false;
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x00188ED4 File Offset: 0x001870D4
		protected static bool IsCompatible<T>(T? thisConfiguration, T? other) where T : struct
		{
			return thisConfiguration == null || other == null || object.Equals(thisConfiguration.Value, other.Value);
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00188F09 File Offset: 0x00187109
		protected static bool IsCompatible(string thisConfiguration, string other)
		{
			return thisConfiguration == null || other == null || object.Equals(thisConfiguration, other);
		}

		// Token: 0x04002437 RID: 9271
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}
