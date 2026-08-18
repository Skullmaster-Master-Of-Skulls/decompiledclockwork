using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x020001B6 RID: 438
	internal class ConventionNavigationPropertyConfiguration
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003F8D4 File Offset: 0x0003DAD4
		internal ConventionNavigationPropertyConfiguration(NavigationPropertyConfiguration configuration, ModelConfiguration modelConfiguration)
		{
			this._configuration = configuration;
			this._modelConfiguration = modelConfiguration;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003F8EA File Offset: 0x0003DAEA
		public virtual PropertyInfo ClrPropertyInfo
		{
			get
			{
				if (this._configuration == null)
				{
					return null;
				}
				return this._configuration.NavigationProperty;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0003F901 File Offset: 0x0003DB01
		internal NavigationPropertyConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003F909 File Offset: 0x0003DB09
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public virtual void HasConstraint<T>() where T : ConstraintConfiguration
		{
			this.HasConstraintInternal<T>(null);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003F912 File Offset: 0x0003DB12
		public virtual void HasConstraint<T>(Action<T> constraintConfigurationAction) where T : ConstraintConfiguration
		{
			Check.NotNull<Action<T>>(constraintConfigurationAction, "constraintConfigurationAction");
			this.HasConstraintInternal<T>(constraintConfigurationAction);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003F928 File Offset: 0x0003DB28
		private void HasConstraintInternal<T>(Action<T> constraintConfigurationAction) where T : ConstraintConfiguration
		{
			if (this._configuration != null && !this.HasConfiguredConstraint())
			{
				Type typeFromHandle = typeof(T);
				if (this._configuration.Constraint == null)
				{
					if (typeFromHandle == typeof(IndependentConstraintConfiguration))
					{
						this._configuration.Constraint = IndependentConstraintConfiguration.Instance;
					}
					else
					{
						this._configuration.Constraint = (ConstraintConfiguration)Activator.CreateInstance(typeFromHandle);
					}
				}
				else if (this._configuration.Constraint.GetType() != typeFromHandle)
				{
					return;
				}
				if (constraintConfigurationAction != null)
				{
					constraintConfigurationAction((T)((object)this._configuration.Constraint));
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003F9D4 File Offset: 0x0003DBD4
		private bool HasConfiguredConstraint()
		{
			if (this._configuration != null && this._configuration.Constraint != null && this._configuration.Constraint.IsFullySpecified)
			{
				return true;
			}
			if (this._configuration != null && this._configuration.InverseNavigationProperty != null)
			{
				Type targetType = this._configuration.NavigationProperty.PropertyType.GetTargetType();
				if (this._modelConfiguration.Entities.Contains(targetType))
				{
					EntityTypeConfiguration entityTypeConfiguration = this._modelConfiguration.Entity(targetType);
					if (entityTypeConfiguration.IsNavigationPropertyConfigured(this._configuration.InverseNavigationProperty))
					{
						return entityTypeConfiguration.Navigation(this._configuration.InverseNavigationProperty).Constraint != null;
					}
				}
			}
			return false;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		public virtual ConventionNavigationPropertyConfiguration HasInverseNavigationProperty(Func<PropertyInfo, PropertyInfo> inverseNavigationPropertyGetter)
		{
			Check.NotNull<Func<PropertyInfo, PropertyInfo>>(inverseNavigationPropertyGetter, "inverseNavigationPropertyGetter");
			if (this._configuration != null && this._configuration.InverseNavigationProperty == null)
			{
				PropertyInfo propertyInfo = inverseNavigationPropertyGetter(this.ClrPropertyInfo);
				Check.NotNull<PropertyInfo>(propertyInfo, "inverseNavigationProperty");
				if (!propertyInfo.IsValidEdmNavigationProperty())
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidNavigationProperty(propertyInfo.Name));
				}
				if (!propertyInfo.DeclaringType.IsAssignableFrom(this._configuration.NavigationProperty.PropertyType.GetTargetType()))
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_MismatchedInverseNavigationProperty(this._configuration.NavigationProperty.PropertyType.GetTargetType().FullName, this._configuration.NavigationProperty.Name, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
				}
				if (!this._configuration.NavigationProperty.DeclaringType.IsAssignableFrom(propertyInfo.PropertyType.GetTargetType()))
				{
					throw new InvalidOperationException(Strings.LightweightEntityConfiguration_InvalidInverseNavigationProperty(this._configuration.NavigationProperty.DeclaringType.FullName, this._configuration.NavigationProperty.Name, propertyInfo.PropertyType.GetTargetType().FullName, propertyInfo.Name));
				}
				if (this._configuration.InverseEndKind != null)
				{
					ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(this._configuration.InverseEndKind.Value, propertyInfo);
				}
				this._modelConfiguration.Entity(this._configuration.NavigationProperty.PropertyType.GetTargetType()).Navigation(propertyInfo);
				this._configuration.InverseNavigationProperty = propertyInfo;
			}
			return this;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003FC2C File Offset: 0x0003DE2C
		public virtual ConventionNavigationPropertyConfiguration HasInverseEndMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (this._configuration != null && this._configuration.InverseEndKind == null)
			{
				if (this._configuration.InverseNavigationProperty != null)
				{
					ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(multiplicity, this._configuration.InverseNavigationProperty);
				}
				this._configuration.InverseEndKind = new RelationshipMultiplicity?(multiplicity);
			}
			return this;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003FC8C File Offset: 0x0003DE8C
		public virtual ConventionNavigationPropertyConfiguration IsDeclaringTypePrincipal(bool isPrincipal)
		{
			if (this._configuration != null && this._configuration.IsNavigationPropertyDeclaringTypePrincipal == null)
			{
				this._configuration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(isPrincipal);
			}
			return this;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003FCC8 File Offset: 0x0003DEC8
		public virtual ConventionNavigationPropertyConfiguration HasDeleteAction(OperationAction deleteAction)
		{
			if (this._configuration != null && this._configuration.DeleteAction == null)
			{
				this._configuration.DeleteAction = new OperationAction?(deleteAction);
			}
			return this;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003FD04 File Offset: 0x0003DF04
		public virtual ConventionNavigationPropertyConfiguration HasRelationshipMultiplicity(RelationshipMultiplicity multiplicity)
		{
			if (this._configuration != null && this._configuration.RelationshipMultiplicity == null)
			{
				ConventionNavigationPropertyConfiguration.VerifyMultiplicityCompatibility(multiplicity, this._configuration.NavigationProperty);
				this._configuration.RelationshipMultiplicity = new RelationshipMultiplicity?(multiplicity);
			}
			return this;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003FD54 File Offset: 0x0003DF54
		private static void VerifyMultiplicityCompatibility(RelationshipMultiplicity multiplicity, PropertyInfo propertyInfo)
		{
			bool flag;
			switch (multiplicity)
			{
			case RelationshipMultiplicity.ZeroOrOne:
			case RelationshipMultiplicity.One:
				flag = !propertyInfo.PropertyType.IsCollection();
				break;
			case RelationshipMultiplicity.Many:
				flag = propertyInfo.PropertyType.IsCollection();
				break;
			default:
				throw new InvalidOperationException(Strings.LightweightNavigationPropertyConfiguration_InvalidMultiplicity(multiplicity));
			}
			if (!flag)
			{
				throw new InvalidOperationException(Strings.LightweightNavigationPropertyConfiguration_IncompatibleMultiplicity(RelationshipMultiplicityConverter.MultiplicityToString(multiplicity), propertyInfo.DeclaringType.Name + "." + propertyInfo.Name, propertyInfo.PropertyType));
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003FDDE File Offset: 0x0003DFDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003FDE6 File Offset: 0x0003DFE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003FDEF File Offset: 0x0003DFEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003FDF7 File Offset: 0x0003DFF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003F8 RID: 1016
		private readonly NavigationPropertyConfiguration _configuration;

		// Token: 0x040003F9 RID: 1017
		private readonly ModelConfiguration _modelConfiguration;
	}
}
