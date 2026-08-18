using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037C RID: 892
	internal sealed class ReferentialConstraint : SchemaElement
	{
		// Token: 0x06002027 RID: 8231 RVA: 0x000983BB File Offset: 0x000965BB
		public ReferentialConstraint(Relationship relationship) : base(relationship, null)
		{
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000983C8 File Offset: 0x000965C8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal override void Validate()
		{
			base.Validate();
			this._principalRole.Validate();
			this._dependentRole.Validate();
			if (ReferentialConstraint.ReadyForFurtherValidation(this._principalRole) && ReferentialConstraint.ReadyForFurtherValidation(this._dependentRole))
			{
				IRelationshipEnd end = this._principalRole.End;
				IRelationshipEnd end2 = this._dependentRole.End;
				if (this._principalRole.Name == this._dependentRole.Name)
				{
					base.AddError(ErrorCode.SameRoleReferredInReferentialConstraint, EdmSchemaErrorSeverity.Error, Strings.SameRoleReferredInReferentialConstraint(this.ParentElement.Name));
				}
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				ReferentialConstraint.IsKeyProperty(this._dependentRole, end2.Type, out flag, out flag2, out flag3, out flag4);
				bool flag5;
				bool flag6;
				bool flag7;
				bool flag8;
				ReferentialConstraint.IsKeyProperty(this._principalRole, end.Type, out flag5, out flag6, out flag7, out flag8);
				if (!flag5)
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidFromPropertyInRelationshipConstraint(this.PrincipalRole.Name, end.Type.FQName, this.ParentElement.FQName));
					return;
				}
				bool flag9 = base.Schema.SchemaVersion <= 1.1;
				RelationshipMultiplicity relationshipMultiplicity = (flag9 ? flag6 : flag7) ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.One;
				RelationshipMultiplicity relationshipMultiplicity2 = (flag9 ? flag2 : flag3) ? RelationshipMultiplicity.ZeroOrOne : RelationshipMultiplicity.Many;
				end.Multiplicity = new RelationshipMultiplicity?(end.Multiplicity ?? relationshipMultiplicity);
				end2.Multiplicity = new RelationshipMultiplicity?(end2.Multiplicity ?? relationshipMultiplicity2);
				if (end.Multiplicity == RelationshipMultiplicity.Many)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityFromRoleUpperBoundMustBeOne(this._principalRole.Name, this.ParentElement.Name));
				}
				else if (flag2 && end.Multiplicity == RelationshipMultiplicity.One)
				{
					string message = Strings.InvalidMultiplicityFromRoleToPropertyNullableV1(this._principalRole.Name, this.ParentElement.Name);
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, message);
				}
				else if (((flag9 && !flag2) || (!flag9 && !flag3)) && end.Multiplicity != RelationshipMultiplicity.One)
				{
					string message2;
					if (flag9)
					{
						message2 = Strings.InvalidMultiplicityFromRoleToPropertyNonNullableV1(this._principalRole.Name, this.ParentElement.Name);
					}
					else
					{
						message2 = Strings.InvalidMultiplicityFromRoleToPropertyNonNullableV2(this._principalRole.Name, this.ParentElement.Name);
					}
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, message2);
				}
				if (end2.Multiplicity == RelationshipMultiplicity.One && base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleLowerBoundMustBeZero(this._dependentRole.Name, this.ParentElement.Name));
				}
				if (!flag4 && !this.ParentElement.IsForeignKey && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidToPropertyInRelationshipConstraint(this.DependentRole.Name, end2.Type.FQName, this.ParentElement.FQName));
				}
				if (flag)
				{
					if (end2.Multiplicity == RelationshipMultiplicity.Many)
					{
						base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeOne(end2.Name, this.ParentElement.Name));
					}
				}
				else if (end2.Multiplicity != RelationshipMultiplicity.Many)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeMany(end2.Name, this.ParentElement.Name));
				}
				if (this._dependentRole.RoleProperties.Count != this._principalRole.RoleProperties.Count)
				{
					base.AddError(ErrorCode.MismatchNumberOfPropertiesInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.MismatchNumberOfPropertiesinRelationshipConstraint);
					return;
				}
				for (int i = 0; i < this._dependentRole.RoleProperties.Count; i++)
				{
					if (this._dependentRole.RoleProperties[i].Property.Type != this._principalRole.RoleProperties[i].Property.Type)
					{
						base.AddError(ErrorCode.TypeMismatchRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.TypeMismatchRelationshipConstraint(this._dependentRole.RoleProperties[i].Name, this._dependentRole.End.Type.Identity, this._principalRole.RoleProperties[i].Name, this._principalRole.End.Type.Identity, this.ParentElement.Name));
					}
				}
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00098880 File Offset: 0x00096A80
		private static bool ReadyForFurtherValidation(ReferentialConstraintRoleElement role)
		{
			if (role == null)
			{
				return false;
			}
			if (role.End == null)
			{
				return false;
			}
			if (role.RoleProperties.Count == 0)
			{
				return false;
			}
			foreach (PropertyRefElement propertyRefElement in role.RoleProperties)
			{
				if (propertyRefElement.Property == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000988F4 File Offset: 0x00096AF4
		private static void IsKeyProperty(ReferentialConstraintRoleElement roleElement, SchemaEntityType itemType, out bool isKeyProperty, out bool areAllPropertiesNullable, out bool isAnyPropertyNullable, out bool isSubsetOfKeyProperties)
		{
			isKeyProperty = true;
			areAllPropertiesNullable = true;
			isAnyPropertyNullable = false;
			isSubsetOfKeyProperties = true;
			if (itemType.KeyProperties.Count != roleElement.RoleProperties.Count)
			{
				isKeyProperty = false;
			}
			for (int i = 0; i < roleElement.RoleProperties.Count; i++)
			{
				if (isSubsetOfKeyProperties)
				{
					bool flag = false;
					for (int j = 0; j < itemType.KeyProperties.Count; j++)
					{
						if (itemType.KeyProperties[j].Property == roleElement.RoleProperties[i].Property)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						isKeyProperty = false;
						isSubsetOfKeyProperties = false;
					}
				}
				areAllPropertiesNullable &= roleElement.RoleProperties[i].Property.Nullable;
				isAnyPropertyNullable |= roleElement.RoleProperties[i].Property.Nullable;
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000989CC File Offset: 0x00096BCC
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000989CF File Offset: 0x00096BCF
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Principal"))
			{
				this.HandleReferentialConstraintPrincipalRoleElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "Dependent"))
			{
				this.HandleReferentialConstraintDependentRoleElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00098A0B File Offset: 0x00096C0B
		internal void HandleReferentialConstraintPrincipalRoleElement(XmlReader reader)
		{
			this._principalRole = new ReferentialConstraintRoleElement(this);
			this._principalRole.Parse(reader);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00098A25 File Offset: 0x00096C25
		internal void HandleReferentialConstraintDependentRoleElement(XmlReader reader)
		{
			this._dependentRole = new ReferentialConstraintRoleElement(this);
			this._dependentRole.Parse(reader);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00098A3F File Offset: 0x00096C3F
		internal override void ResolveTopLevelNames()
		{
			this._dependentRole.ResolveTopLevelNames();
			this._principalRole.ResolveTopLevelNames();
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x00098A57 File Offset: 0x00096C57
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00098A64 File Offset: 0x00096C64
		internal ReferentialConstraintRoleElement PrincipalRole
		{
			get
			{
				return this._principalRole;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x00098A6C File Offset: 0x00096C6C
		internal ReferentialConstraintRoleElement DependentRole
		{
			get
			{
				return this._dependentRole;
			}
		}

		// Token: 0x04000B71 RID: 2929
		private const char KEY_DELIMITER = ' ';

		// Token: 0x04000B72 RID: 2930
		private ReferentialConstraintRoleElement _principalRole;

		// Token: 0x04000B73 RID: 2931
		private ReferentialConstraintRoleElement _dependentRole;
	}
}
