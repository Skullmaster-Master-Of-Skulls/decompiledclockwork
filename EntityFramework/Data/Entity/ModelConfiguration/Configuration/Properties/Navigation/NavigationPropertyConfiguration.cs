using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x020007C9 RID: 1993
	internal class NavigationPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06005A71 RID: 23153 RVA: 0x00185D0F File Offset: 0x00183F0F
		internal NavigationPropertyConfiguration(PropertyInfo navigationProperty)
		{
			this._navigationProperty = navigationProperty;
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x00185D20 File Offset: 0x00183F20
		private NavigationPropertyConfiguration(NavigationPropertyConfiguration source)
		{
			this._navigationProperty = source._navigationProperty;
			this._endKind = source._endKind;
			this._inverseNavigationProperty = source._inverseNavigationProperty;
			this._inverseEndKind = source._inverseEndKind;
			this._constraint = ((source._constraint == null) ? null : source._constraint.Clone());
			this._associationMappingConfiguration = ((source._associationMappingConfiguration == null) ? null : source._associationMappingConfiguration.Clone());
			this.DeleteAction = source.DeleteAction;
			this.IsNavigationPropertyDeclaringTypePrincipal = source.IsNavigationPropertyDeclaringTypePrincipal;
			this._modificationStoredProceduresConfiguration = ((source._modificationStoredProceduresConfiguration == null) ? null : source._modificationStoredProceduresConfiguration.Clone());
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x00185DCF File Offset: 0x00183FCF
		internal virtual NavigationPropertyConfiguration Clone()
		{
			return new NavigationPropertyConfiguration(this);
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005A74 RID: 23156 RVA: 0x00185DD7 File Offset: 0x00183FD7
		// (set) Token: 0x06005A75 RID: 23157 RVA: 0x00185DDF File Offset: 0x00183FDF
		public OperationAction? DeleteAction { get; set; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005A76 RID: 23158 RVA: 0x00185DE8 File Offset: 0x00183FE8
		internal PropertyInfo NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x00185DF0 File Offset: 0x00183FF0
		// (set) Token: 0x06005A78 RID: 23160 RVA: 0x00185DF8 File Offset: 0x00183FF8
		public RelationshipMultiplicity? RelationshipMultiplicity
		{
			get
			{
				return this._endKind;
			}
			set
			{
				Check.NotNull<RelationshipMultiplicity>(value, "value");
				this._endKind = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x00185E0D File Offset: 0x0018400D
		// (set) Token: 0x06005A7A RID: 23162 RVA: 0x00185E15 File Offset: 0x00184015
		internal PropertyInfo InverseNavigationProperty
		{
			get
			{
				return this._inverseNavigationProperty;
			}
			set
			{
				if (value == this._navigationProperty)
				{
					throw Error.NavigationInverseItself(value.Name, value.ReflectedType);
				}
				this._inverseNavigationProperty = value;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x00185E3E File Offset: 0x0018403E
		// (set) Token: 0x06005A7C RID: 23164 RVA: 0x00185E46 File Offset: 0x00184046
		internal RelationshipMultiplicity? InverseEndKind
		{
			get
			{
				return this._inverseEndKind;
			}
			set
			{
				this._inverseEndKind = value;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x00185E4F File Offset: 0x0018404F
		// (set) Token: 0x06005A7E RID: 23166 RVA: 0x00185E57 File Offset: 0x00184057
		public ConstraintConfiguration Constraint
		{
			get
			{
				return this._constraint;
			}
			set
			{
				Check.NotNull<ConstraintConfiguration>(value, "value");
				this._constraint = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x00185E6C File Offset: 0x0018406C
		// (set) Token: 0x06005A80 RID: 23168 RVA: 0x00185E74 File Offset: 0x00184074
		internal bool? IsNavigationPropertyDeclaringTypePrincipal { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005A81 RID: 23169 RVA: 0x00185E7D File Offset: 0x0018407D
		// (set) Token: 0x06005A82 RID: 23170 RVA: 0x00185E85 File Offset: 0x00184085
		internal AssociationMappingConfiguration AssociationMappingConfiguration
		{
			get
			{
				return this._associationMappingConfiguration;
			}
			set
			{
				this._associationMappingConfiguration = value;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x00185E8E File Offset: 0x0018408E
		// (set) Token: 0x06005A84 RID: 23172 RVA: 0x00185E96 File Offset: 0x00184096
		internal ModificationStoredProceduresConfiguration ModificationStoredProceduresConfiguration
		{
			get
			{
				return this._modificationStoredProceduresConfiguration;
			}
			set
			{
				this._modificationStoredProceduresConfiguration = value;
			}
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x00185EA0 File Offset: 0x001840A0
		internal void Configure(NavigationProperty navigationProperty, EdmModel model, EntityTypeConfiguration entityTypeConfiguration)
		{
			navigationProperty.SetConfiguration(this);
			AssociationType association = navigationProperty.Association;
			NavigationPropertyConfiguration navigationPropertyConfiguration = association.GetConfiguration() as NavigationPropertyConfiguration;
			if (navigationPropertyConfiguration == null)
			{
				association.SetConfiguration(this);
			}
			else
			{
				this.EnsureConsistency(navigationPropertyConfiguration);
			}
			this.ConfigureInverse(association, model);
			this.ConfigureEndKinds(association, navigationPropertyConfiguration);
			this.ConfigureDependentBehavior(association, model, entityTypeConfiguration);
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x00185EF4 File Offset: 0x001840F4
		internal void Configure(AssociationSetMapping associationSetMapping, DbDatabaseMapping databaseMapping, DbProviderManifest providerManifest)
		{
			if (this.AssociationMappingConfiguration != null)
			{
				associationSetMapping.SetConfiguration(this);
				this.AssociationMappingConfiguration.Configure(associationSetMapping, databaseMapping.Database, this._navigationProperty);
			}
			if (this._modificationStoredProceduresConfiguration != null)
			{
				if (associationSetMapping.ModificationFunctionMapping == null)
				{
					new ModificationFunctionMappingGenerator(providerManifest).Generate(associationSetMapping, databaseMapping);
				}
				this._modificationStoredProceduresConfiguration.Configure(associationSetMapping.ModificationFunctionMapping, providerManifest);
			}
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x00185F58 File Offset: 0x00184158
		private void ConfigureInverse(AssociationType associationType, EdmModel model)
		{
			if (this._inverseNavigationProperty == null)
			{
				return;
			}
			NavigationProperty navigationProperty = model.GetNavigationProperty(this._inverseNavigationProperty);
			if (navigationProperty != null && navigationProperty.Association != associationType)
			{
				associationType.SourceEnd.RelationshipMultiplicity = navigationProperty.Association.TargetEnd.RelationshipMultiplicity;
				if (associationType.Constraint == null && this._constraint == null && navigationProperty.Association.Constraint != null)
				{
					associationType.Constraint = navigationProperty.Association.Constraint;
					associationType.Constraint.FromRole = associationType.SourceEnd;
					associationType.Constraint.ToRole = associationType.TargetEnd;
				}
				model.RemoveAssociationType(navigationProperty.Association);
				navigationProperty.RelationshipType = associationType;
				navigationProperty.FromEndMember = associationType.TargetEnd;
				navigationProperty.ToEndMember = associationType.SourceEnd;
			}
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x0018602C File Offset: 0x0018422C
		private void ConfigureEndKinds(AssociationType associationType, NavigationPropertyConfiguration configuration)
		{
			AssociationEndMember associationEndMember = associationType.SourceEnd;
			AssociationEndMember associationEndMember2 = associationType.TargetEnd;
			if (configuration != null && configuration.InverseNavigationProperty != null)
			{
				associationEndMember = associationType.TargetEnd;
				associationEndMember2 = associationType.SourceEnd;
			}
			if (this._inverseEndKind != null)
			{
				associationEndMember.RelationshipMultiplicity = this._inverseEndKind.Value;
			}
			if (this._endKind != null)
			{
				associationEndMember2.RelationshipMultiplicity = this._endKind.Value;
			}
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x001860A4 File Offset: 0x001842A4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void EnsureConsistency(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			if (this.RelationshipMultiplicity != null)
			{
				if (navigationPropertyConfiguration.InverseEndKind == null)
				{
					navigationPropertyConfiguration.InverseEndKind = this.RelationshipMultiplicity;
				}
				else if (navigationPropertyConfiguration.InverseEndKind != this.RelationshipMultiplicity)
				{
					throw Error.ConflictingMultiplicities(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.InverseEndKind != null)
			{
				if (navigationPropertyConfiguration.RelationshipMultiplicity == null)
				{
					navigationPropertyConfiguration.RelationshipMultiplicity = this.InverseEndKind;
				}
				else if (navigationPropertyConfiguration.RelationshipMultiplicity != this.InverseEndKind)
				{
					if (this.InverseNavigationProperty == null)
					{
						throw Error.ConflictingMultiplicities(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
					}
					throw Error.ConflictingMultiplicities(this.InverseNavigationProperty.Name, this.InverseNavigationProperty.ReflectedType);
				}
			}
			if (this.DeleteAction != null)
			{
				if (navigationPropertyConfiguration.DeleteAction == null)
				{
					navigationPropertyConfiguration.DeleteAction = this.DeleteAction;
				}
				else if (navigationPropertyConfiguration.DeleteAction != this.DeleteAction)
				{
					throw Error.ConflictingCascadeDeleteOperation(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.Constraint != null)
			{
				if (navigationPropertyConfiguration.Constraint == null)
				{
					navigationPropertyConfiguration.Constraint = this.Constraint;
				}
				else if (!object.Equals(navigationPropertyConfiguration.Constraint, this.Constraint))
				{
					throw Error.ConflictingConstraint(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.IsNavigationPropertyDeclaringTypePrincipal != null)
			{
				if (navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal == null)
				{
					navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = !this.IsNavigationPropertyDeclaringTypePrincipal;
				}
				else if (navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal == this.IsNavigationPropertyDeclaringTypePrincipal)
				{
					throw Error.ConflictingConstraint(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.AssociationMappingConfiguration != null)
			{
				if (navigationPropertyConfiguration.AssociationMappingConfiguration == null)
				{
					navigationPropertyConfiguration.AssociationMappingConfiguration = this.AssociationMappingConfiguration;
				}
				else if (!object.Equals(navigationPropertyConfiguration.AssociationMappingConfiguration, this.AssociationMappingConfiguration))
				{
					throw Error.ConflictingMapping(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
			if (this.ModificationStoredProceduresConfiguration != null)
			{
				if (navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
				{
					navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = this.ModificationStoredProceduresConfiguration;
					return;
				}
				if (!navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.IsCompatibleWith(this.ModificationStoredProceduresConfiguration))
				{
					throw Error.ConflictingFunctionsMapping(this.NavigationProperty.Name, this.NavigationProperty.ReflectedType);
				}
			}
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0018644C File Offset: 0x0018464C
		private void ConfigureDependentBehavior(AssociationType associationType, EdmModel model, EntityTypeConfiguration entityTypeConfiguration)
		{
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			if (!associationType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2))
			{
				if (this.IsNavigationPropertyDeclaringTypePrincipal != null)
				{
					associationType.MarkPrincipalConfigured();
					NavigationProperty navigationProperty = model.EntityTypes.SelectMany((EntityType et) => et.DeclaredNavigationProperties).Single((NavigationProperty np) => np.RelationshipType.Equals(associationType) && np.GetClrPropertyInfo().IsSameAs(this.NavigationProperty));
					associationEndMember = (this.IsNavigationPropertyDeclaringTypePrincipal.Value ? associationType.GetOtherEnd(navigationProperty.ResultEnd) : navigationProperty.ResultEnd);
					associationEndMember2 = associationType.GetOtherEnd(associationEndMember);
					if (associationType.SourceEnd != associationEndMember)
					{
						associationType.SourceEnd = associationEndMember;
						associationType.TargetEnd = associationEndMember2;
						AssociationSet associationSet = model.Containers.SelectMany((EntityContainer ct) => ct.AssociationSets).Single((AssociationSet aset) => aset.ElementType == associationType);
						EntitySet sourceSet = associationSet.SourceSet;
						associationSet.SourceSet = associationSet.TargetSet;
						associationSet.TargetSet = sourceSet;
					}
				}
				if (associationEndMember == null)
				{
					associationEndMember2 = associationType.TargetEnd;
				}
			}
			this.ConfigureConstraint(associationType, associationEndMember2, entityTypeConfiguration);
			this.ConfigureDeleteAction(associationType.GetOtherEnd(associationEndMember2));
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x001865E4 File Offset: 0x001847E4
		private void ConfigureConstraint(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			if (this._constraint != null)
			{
				this._constraint.Configure(associationType, dependentEnd, entityTypeConfiguration);
				ReferentialConstraint constraint = associationType.Constraint;
				if (constraint != null && constraint.ToProperties.SequenceEqual(constraint.ToRole.GetEntityType().KeyProperties) && this._inverseEndKind == null && associationType.SourceEnd.IsMany())
				{
					associationType.SourceEnd.RelationshipMultiplicity = System.Data.Entity.Core.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne;
					associationType.TargetEnd.RelationshipMultiplicity = System.Data.Entity.Core.Metadata.Edm.RelationshipMultiplicity.One;
				}
			}
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x00186660 File Offset: 0x00184860
		private void ConfigureDeleteAction(AssociationEndMember principalEnd)
		{
			if (this.DeleteAction != null)
			{
				principalEnd.DeleteBehavior = this.DeleteAction.Value;
			}
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x00186691 File Offset: 0x00184891
		internal void Reset()
		{
			this._endKind = null;
			this._inverseNavigationProperty = null;
			this._inverseEndKind = null;
			this._constraint = null;
			this._associationMappingConfiguration = null;
		}

		// Token: 0x04002414 RID: 9236
		private readonly PropertyInfo _navigationProperty;

		// Token: 0x04002415 RID: 9237
		private RelationshipMultiplicity? _endKind;

		// Token: 0x04002416 RID: 9238
		private PropertyInfo _inverseNavigationProperty;

		// Token: 0x04002417 RID: 9239
		private RelationshipMultiplicity? _inverseEndKind;

		// Token: 0x04002418 RID: 9240
		private ConstraintConfiguration _constraint;

		// Token: 0x04002419 RID: 9241
		private AssociationMappingConfiguration _associationMappingConfiguration;

		// Token: 0x0400241A RID: 9242
		private ModificationStoredProceduresConfiguration _modificationStoredProceduresConfiguration;
	}
}
