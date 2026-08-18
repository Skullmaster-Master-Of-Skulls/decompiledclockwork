using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007AB RID: 1963
	public class ConventionsConfiguration
	{
		// Token: 0x06005890 RID: 22672 RVA: 0x0017C53C File Offset: 0x0017A73C
		internal ConventionsConfiguration() : this(V2ConventionSet.Conventions)
		{
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x0017C54C File Offset: 0x0017A74C
		internal ConventionsConfiguration(ConventionSet conventionSet)
		{
			this._configurationConventions = new List<IConvention>();
			this._conceptualModelConventions = new List<IConvention>();
			this._conceptualToStoreMappingConventions = new List<IConvention>();
			this._storeModelConventions = new List<IConvention>();
			base..ctor();
			this._configurationConventions.AddRange(conventionSet.ConfigurationConventions);
			this._conceptualModelConventions.AddRange(conventionSet.ConceptualModelConventions);
			this._conceptualToStoreMappingConventions.AddRange(conventionSet.ConceptualToStoreMappingConventions);
			this._storeModelConventions.AddRange(conventionSet.StoreModelConventions);
			this._initialConventionSet = conventionSet;
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0017C5D8 File Offset: 0x0017A7D8
		private ConventionsConfiguration(ConventionsConfiguration source)
		{
			this._configurationConventions = new List<IConvention>();
			this._conceptualModelConventions = new List<IConvention>();
			this._conceptualToStoreMappingConventions = new List<IConvention>();
			this._storeModelConventions = new List<IConvention>();
			base..ctor();
			this._configurationConventions.AddRange(source._configurationConventions);
			this._conceptualModelConventions.AddRange(source._conceptualModelConventions);
			this._conceptualToStoreMappingConventions.AddRange(source._conceptualToStoreMappingConventions);
			this._storeModelConventions.AddRange(source._storeModelConventions);
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005893 RID: 22675 RVA: 0x0017C65B File Offset: 0x0017A85B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		internal IEnumerable<IConvention> ConfigurationConventions
		{
			get
			{
				return this._configurationConventions;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005894 RID: 22676 RVA: 0x0017C663 File Offset: 0x0017A863
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		internal IEnumerable<IConvention> ConceptualModelConventions
		{
			get
			{
				return this._conceptualModelConventions;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0017C66B File Offset: 0x0017A86B
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		internal IEnumerable<IConvention> ConceptualToStoreMappingConventions
		{
			get
			{
				return this._conceptualToStoreMappingConventions;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x0017C673 File Offset: 0x0017A873
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		internal IEnumerable<IConvention> StoreModelConventions
		{
			get
			{
				return this._storeModelConventions;
			}
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x0017C67B File Offset: 0x0017A87B
		internal virtual ConventionsConfiguration Clone()
		{
			return new ConventionsConfiguration(this);
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x0017C6AC File Offset: 0x0017A8AC
		public void AddFromAssembly(Assembly assembly)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			IOrderedEnumerable<Type> types = from type in assembly.GetAccessibleTypes()
			orderby type.Name
			select type;
			new ConventionsTypeFinder().AddConventions(types, delegate(IConvention convention)
			{
				this.Add(new IConvention[]
				{
					convention
				});
			});
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0017C718 File Offset: 0x0017A918
		public void Add(params IConvention[] conventions)
		{
			Check.NotNull<IConvention[]>(conventions, "conventions");
			foreach (IConvention convention in conventions)
			{
				bool flag = true;
				if (ConventionsTypeFilter.IsConfigurationConvention(convention.GetType()))
				{
					flag = false;
					int num = this._configurationConventions.FindIndex((IConvention initialConvention) => this._initialConventionSet.ConfigurationConventions.Contains(initialConvention));
					num = ((num == -1) ? this._configurationConventions.Count : num);
					this._configurationConventions.Insert(num, convention);
				}
				if (ConventionsTypeFilter.IsConceptualModelConvention(convention.GetType()))
				{
					flag = false;
					this._conceptualModelConventions.Add(convention);
				}
				if (ConventionsTypeFilter.IsStoreModelConvention(convention.GetType()))
				{
					flag = false;
					this._storeModelConventions.Add(convention);
				}
				if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(convention.GetType()))
				{
					flag = false;
					this._conceptualToStoreMappingConventions.Add(convention);
				}
				if (flag)
				{
					throw new InvalidOperationException(Strings.ConventionsConfiguration_InvalidConventionType(convention.GetType()));
				}
			}
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x0017C808 File Offset: 0x0017AA08
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public void Add<TConvention>() where TConvention : IConvention, new()
		{
			this.Add(new IConvention[]
			{
				(default(TConvention) == null) ? Activator.CreateInstance<TConvention>() : default(TConvention)
			});
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0017C84C File Offset: 0x0017AA4C
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public void AddAfter<TExistingConvention>(IConvention newConvention) where TExistingConvention : IConvention
		{
			Check.NotNull<IConvention>(newConvention, "newConvention");
			bool flag = true;
			if (ConventionsTypeFilter.IsConfigurationConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConfigurationConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._configurationConventions);
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._conceptualModelConventions);
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsStoreModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._storeModelConventions);
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._conceptualToStoreMappingConventions);
			}
			if (flag)
			{
				throw new InvalidOperationException(Strings.ConventionsConfiguration_ConventionTypeMissmatch(newConvention.GetType(), typeof(TExistingConvention)));
			}
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x0017C964 File Offset: 0x0017AB64
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public void AddBefore<TExistingConvention>(IConvention newConvention) where TExistingConvention : IConvention
		{
			Check.NotNull<IConvention>(newConvention, "newConvention");
			bool flag = true;
			if (ConventionsTypeFilter.IsConfigurationConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConfigurationConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._configurationConventions);
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._conceptualModelConventions);
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsStoreModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._storeModelConventions);
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._conceptualToStoreMappingConventions);
			}
			if (flag)
			{
				throw new InvalidOperationException(Strings.ConventionsConfiguration_ConventionTypeMissmatch(newConvention.GetType(), typeof(TExistingConvention)));
			}
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x0017CA7C File Offset: 0x0017AC7C
		private static void Insert(Type existingConventionType, int offset, IConvention newConvention, IList<IConvention> conventions)
		{
			int num = ConventionsConfiguration.IndexOf(existingConventionType, conventions);
			if (num < 0)
			{
				throw Error.ConventionNotFound(newConvention.GetType(), existingConventionType);
			}
			conventions.Insert(num + offset, newConvention);
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0017CAAC File Offset: 0x0017ACAC
		private static int IndexOf(Type existingConventionType, IList<IConvention> conventions)
		{
			int num = 0;
			foreach (IConvention convention in conventions)
			{
				if (convention.GetType() == existingConventionType)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x0017CB08 File Offset: 0x0017AD08
		public void Remove(params IConvention[] conventions)
		{
			Check.NotNull<IConvention[]>(conventions, "conventions");
			Check.NotNull<IConvention[]>(conventions, "conventions");
			foreach (IConvention convention in conventions)
			{
				if (ConventionsTypeFilter.IsConfigurationConvention(convention.GetType()))
				{
					this._configurationConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsConceptualModelConvention(convention.GetType()))
				{
					this._conceptualModelConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsStoreModelConvention(convention.GetType()))
				{
					this._storeModelConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(convention.GetType()))
				{
					this._conceptualToStoreMappingConventions.Remove(convention);
				}
			}
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x0017CC08 File Offset: 0x0017AE08
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public void Remove<TConvention>() where TConvention : IConvention
		{
			Predicate<IConvention> predicate = null;
			Predicate<IConvention> predicate2 = null;
			Predicate<IConvention> predicate3 = null;
			Predicate<IConvention> predicate4 = null;
			if (ConventionsTypeFilter.IsConfigurationConvention(typeof(TConvention)))
			{
				List<IConvention> configurationConventions = this._configurationConventions;
				if (predicate == null)
				{
					predicate = ((IConvention c) => c.GetType() == typeof(TConvention));
				}
				configurationConventions.RemoveAll(predicate);
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(typeof(TConvention)))
			{
				List<IConvention> conceptualModelConventions = this._conceptualModelConventions;
				if (predicate2 == null)
				{
					predicate2 = ((IConvention c) => c.GetType() == typeof(TConvention));
				}
				conceptualModelConventions.RemoveAll(predicate2);
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(typeof(TConvention)))
			{
				List<IConvention> storeModelConventions = this._storeModelConventions;
				if (predicate3 == null)
				{
					predicate3 = ((IConvention c) => c.GetType() == typeof(TConvention));
				}
				storeModelConventions.RemoveAll(predicate3);
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TConvention)))
			{
				List<IConvention> conceptualToStoreMappingConventions = this._conceptualToStoreMappingConventions;
				if (predicate4 == null)
				{
					predicate4 = ((IConvention c) => c.GetType() == typeof(TConvention));
				}
				conceptualToStoreMappingConventions.RemoveAll(predicate4);
			}
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x0017CCD8 File Offset: 0x0017AED8
		internal void ApplyConceptualModel(DbModel model)
		{
			foreach (IConvention convention in this._conceptualModelConventions)
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.CSpace).Dispatch();
			}
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x0017CD34 File Offset: 0x0017AF34
		internal void ApplyStoreModel(DbModel model)
		{
			foreach (IConvention convention in this._storeModelConventions)
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.SSpace).Dispatch();
			}
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x0017CD9C File Offset: 0x0017AF9C
		internal void ApplyPluralizingTableNameConvention(DbModel model)
		{
			foreach (IConvention convention in from c in this._storeModelConventions
			where c is PluralizingTableNameConvention
			select c)
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.SSpace).Dispatch();
			}
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x0017CE14 File Offset: 0x0017B014
		internal void ApplyMapping(DbDatabaseMapping databaseMapping)
		{
			foreach (IConvention convention in this._conceptualToStoreMappingConventions)
			{
				IDbMappingConvention dbMappingConvention = convention as IDbMappingConvention;
				if (dbMappingConvention != null)
				{
					dbMappingConvention.Apply(databaseMapping);
				}
			}
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x0017CE74 File Offset: 0x0017B074
		internal virtual void ApplyModelConfiguration(ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention configurationConvention = convention as IConfigurationConvention;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyModelConfiguration(modelConfiguration);
				}
			}
		}

		// Token: 0x060058A6 RID: 22694 RVA: 0x0017CEC8 File Offset: 0x0017B0C8
		internal virtual void ApplyModelConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<Type> configurationConvention = convention as IConfigurationConvention<Type>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(type, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyModelConfiguration(type, modelConfiguration);
				}
			}
		}

		// Token: 0x060058A7 RID: 22695 RVA: 0x0017CF20 File Offset: 0x0017B120
		internal virtual void ApplyTypeConfiguration<TStructuralTypeConfiguration>(Type type, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<Type, TStructuralTypeConfiguration> configurationConvention = convention as IConfigurationConvention<Type, TStructuralTypeConfiguration>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(type, structuralTypeConfiguration, modelConfiguration);
				}
				IConfigurationConvention<Type, StructuralTypeConfiguration> configurationConvention2 = convention as IConfigurationConvention<Type, StructuralTypeConfiguration>;
				if (configurationConvention2 != null)
				{
					configurationConvention2.Apply(type, structuralTypeConfiguration, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyTypeConfiguration<TStructuralTypeConfiguration>(type, structuralTypeConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x060058A8 RID: 22696 RVA: 0x0017CF90 File Offset: 0x0017B190
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<PropertyInfo> configurationConvention = convention as IConfigurationConvention<PropertyInfo>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(propertyInfo, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyConfiguration(propertyInfo, modelConfiguration);
				}
			}
		}

		// Token: 0x060058A9 RID: 22697 RVA: 0x0017CFE8 File Offset: 0x0017B1E8
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			Type propertyConfigurationType = StructuralTypeConfiguration.GetPropertyConfigurationType(propertyInfo.PropertyType);
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				new ConventionsConfiguration.PropertyConfigurationConventionDispatcher(convention, propertyConfigurationType, propertyInfo, propertyConfiguration, modelConfiguration).Dispatch();
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyConfiguration(propertyInfo, propertyConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x060058AA RID: 22698 RVA: 0x0017D048 File Offset: 0x0017B248
		internal virtual void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<PropertyInfo, TStructuralTypeConfiguration> configurationConvention = convention as IConfigurationConvention<PropertyInfo, TStructuralTypeConfiguration>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
				IConfigurationConvention<PropertyInfo, StructuralTypeConfiguration> configurationConvention2 = convention as IConfigurationConvention<PropertyInfo, StructuralTypeConfiguration>;
				if (configurationConvention2 != null)
				{
					configurationConvention2.Apply(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x060058AB RID: 22699 RVA: 0x0017D0B6 File Offset: 0x0017B2B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0017D0BE File Offset: 0x0017B2BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0017D0C7 File Offset: 0x0017B2C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060058AE RID: 22702 RVA: 0x0017D0CF File Offset: 0x0017B2CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002383 RID: 9091
		private readonly List<IConvention> _configurationConventions;

		// Token: 0x04002384 RID: 9092
		private readonly List<IConvention> _conceptualModelConventions;

		// Token: 0x04002385 RID: 9093
		private readonly List<IConvention> _conceptualToStoreMappingConventions;

		// Token: 0x04002386 RID: 9094
		private readonly List<IConvention> _storeModelConventions;

		// Token: 0x04002387 RID: 9095
		private readonly ConventionSet _initialConventionSet;

		// Token: 0x020007AC RID: 1964
		private class ModelConventionDispatcher : EdmModelVisitor
		{
			// Token: 0x060058B7 RID: 22711 RVA: 0x0017D0D7 File Offset: 0x0017B2D7
			public ModelConventionDispatcher(IConvention convention, DbModel model, DataSpace dataSpace)
			{
				Check.NotNull<IConvention>(convention, "convention");
				Check.NotNull<DbModel>(model, "model");
				this._convention = convention;
				this._model = model;
				this._dataSpace = dataSpace;
			}

			// Token: 0x060058B8 RID: 22712 RVA: 0x0017D10C File Offset: 0x0017B30C
			public void Dispatch()
			{
				this.VisitEdmModel((this._dataSpace == DataSpace.CSpace) ? this._model.ConceptualModel : this._model.StoreModel);
			}

			// Token: 0x060058B9 RID: 22713 RVA: 0x0017D138 File Offset: 0x0017B338
			private void Dispatch<T>(T item) where T : MetadataItem
			{
				if (this._dataSpace == DataSpace.CSpace)
				{
					IConceptualModelConvention<T> conceptualModelConvention = this._convention as IConceptualModelConvention<T>;
					if (conceptualModelConvention != null)
					{
						conceptualModelConvention.Apply(item, this._model);
						return;
					}
				}
				else
				{
					IStoreModelConvention<T> storeModelConvention = this._convention as IStoreModelConvention<T>;
					if (storeModelConvention != null)
					{
						storeModelConvention.Apply(item, this._model);
					}
				}
			}

			// Token: 0x060058BA RID: 22714 RVA: 0x0017D187 File Offset: 0x0017B387
			protected internal override void VisitEdmModel(EdmModel item)
			{
				this.Dispatch<EdmModel>(item);
				base.VisitEdmModel(item);
			}

			// Token: 0x060058BB RID: 22715 RVA: 0x0017D197 File Offset: 0x0017B397
			protected override void VisitEdmNavigationProperty(NavigationProperty item)
			{
				this.Dispatch<NavigationProperty>(item);
				base.VisitEdmNavigationProperty(item);
			}

			// Token: 0x060058BC RID: 22716 RVA: 0x0017D1A7 File Offset: 0x0017B3A7
			protected override void VisitEdmAssociationConstraint(ReferentialConstraint item)
			{
				this.Dispatch<ReferentialConstraint>(item);
				if (item != null)
				{
					this.VisitMetadataItem(item);
				}
			}

			// Token: 0x060058BD RID: 22717 RVA: 0x0017D1BA File Offset: 0x0017B3BA
			protected override void VisitEdmAssociationEnd(RelationshipEndMember item)
			{
				this.Dispatch<RelationshipEndMember>(item);
				base.VisitEdmAssociationEnd(item);
			}

			// Token: 0x060058BE RID: 22718 RVA: 0x0017D1CA File Offset: 0x0017B3CA
			protected internal override void VisitEdmProperty(EdmProperty item)
			{
				this.Dispatch<EdmProperty>(item);
				base.VisitEdmProperty(item);
			}

			// Token: 0x060058BF RID: 22719 RVA: 0x0017D1DA File Offset: 0x0017B3DA
			protected internal override void VisitMetadataItem(MetadataItem item)
			{
				this.Dispatch<MetadataItem>(item);
				base.VisitMetadataItem(item);
			}

			// Token: 0x060058C0 RID: 22720 RVA: 0x0017D1EA File Offset: 0x0017B3EA
			protected override void VisitEdmEntityContainer(EntityContainer item)
			{
				this.Dispatch<EntityContainer>(item);
				base.VisitEdmEntityContainer(item);
			}

			// Token: 0x060058C1 RID: 22721 RVA: 0x0017D1FA File Offset: 0x0017B3FA
			protected internal override void VisitEdmEntitySet(EntitySet item)
			{
				this.Dispatch<EntitySet>(item);
				base.VisitEdmEntitySet(item);
			}

			// Token: 0x060058C2 RID: 22722 RVA: 0x0017D20A File Offset: 0x0017B40A
			protected override void VisitEdmAssociationSet(AssociationSet item)
			{
				this.Dispatch<AssociationSet>(item);
				base.VisitEdmAssociationSet(item);
			}

			// Token: 0x060058C3 RID: 22723 RVA: 0x0017D21A File Offset: 0x0017B41A
			protected override void VisitEdmAssociationSetEnd(EntitySet item)
			{
				this.Dispatch<EntitySet>(item);
				base.VisitEdmAssociationSetEnd(item);
			}

			// Token: 0x060058C4 RID: 22724 RVA: 0x0017D22A File Offset: 0x0017B42A
			protected override void VisitComplexType(ComplexType item)
			{
				this.Dispatch<ComplexType>(item);
				base.VisitComplexType(item);
			}

			// Token: 0x060058C5 RID: 22725 RVA: 0x0017D23A File Offset: 0x0017B43A
			protected internal override void VisitEdmEntityType(EntityType item)
			{
				this.Dispatch<EntityType>(item);
				this.VisitMetadataItem(item);
				if (item != null)
				{
					this.VisitDeclaredProperties(item, item.DeclaredProperties);
					this.VisitDeclaredNavigationProperties(item, item.DeclaredNavigationProperties);
				}
			}

			// Token: 0x060058C6 RID: 22726 RVA: 0x0017D267 File Offset: 0x0017B467
			protected internal override void VisitEdmAssociationType(AssociationType item)
			{
				this.Dispatch<AssociationType>(item);
				base.VisitEdmAssociationType(item);
			}

			// Token: 0x0400238A RID: 9098
			private readonly IConvention _convention;

			// Token: 0x0400238B RID: 9099
			private readonly DbModel _model;

			// Token: 0x0400238C RID: 9100
			private readonly DataSpace _dataSpace;
		}

		// Token: 0x020007AD RID: 1965
		private class PropertyConfigurationConventionDispatcher
		{
			// Token: 0x060058C7 RID: 22727 RVA: 0x0017D278 File Offset: 0x0017B478
			public PropertyConfigurationConventionDispatcher(IConvention convention, Type propertyConfigurationType, PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
			{
				Check.NotNull<IConvention>(convention, "convention");
				Check.NotNull<Type>(propertyConfigurationType, "propertyConfigurationType");
				Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
				Check.NotNull<Func<PropertyConfiguration>>(propertyConfiguration, "propertyConfiguration");
				this._convention = convention;
				this._propertyConfigurationType = propertyConfigurationType;
				this._propertyInfo = propertyInfo;
				this._propertyConfiguration = propertyConfiguration;
				this._modelConfiguration = modelConfiguration;
			}

			// Token: 0x060058C8 RID: 22728 RVA: 0x0017D2E1 File Offset: 0x0017B4E1
			public void Dispatch()
			{
				this.Dispatch<PropertyConfiguration>();
				this.Dispatch<PrimitivePropertyConfiguration>();
				this.Dispatch<LengthPropertyConfiguration>();
				this.Dispatch<DateTimePropertyConfiguration>();
				this.Dispatch<DecimalPropertyConfiguration>();
				this.Dispatch<StringPropertyConfiguration>();
				this.Dispatch<BinaryPropertyConfiguration>();
				this.Dispatch<NavigationPropertyConfiguration>();
			}

			// Token: 0x060058C9 RID: 22729 RVA: 0x0017D328 File Offset: 0x0017B528
			private void Dispatch<TPropertyConfiguration>() where TPropertyConfiguration : PropertyConfiguration
			{
				IConfigurationConvention<PropertyInfo, TPropertyConfiguration> configurationConvention = this._convention as IConfigurationConvention<PropertyInfo, TPropertyConfiguration>;
				if (configurationConvention != null && typeof(TPropertyConfiguration).IsAssignableFrom(this._propertyConfigurationType))
				{
					configurationConvention.Apply(this._propertyInfo, () => (TPropertyConfiguration)((object)this._propertyConfiguration()), this._modelConfiguration);
				}
			}

			// Token: 0x0400238D RID: 9101
			private readonly IConvention _convention;

			// Token: 0x0400238E RID: 9102
			private readonly Type _propertyConfigurationType;

			// Token: 0x0400238F RID: 9103
			private readonly PropertyInfo _propertyInfo;

			// Token: 0x04002390 RID: 9104
			private readonly Func<PropertyConfiguration> _propertyConfiguration;

			// Token: 0x04002391 RID: 9105
			private readonly ModelConfiguration _modelConfiguration;
		}
	}
}
