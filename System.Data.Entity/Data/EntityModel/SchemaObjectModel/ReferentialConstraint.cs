using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000300 RID: 768
	internal sealed class ReferentialConstraint : SchemaElement
	{
		// Token: 0x06002D7E RID: 11646 RVA: 0x000A9632 File Offset: 0x000A7832
		public ReferentialConstraint(Relationship relationship) : base(relationship)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000AC3D4 File Offset: 0x000AA5D4
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
				RelationshipMultiplicity? multiplicity = end.Multiplicity;
				RelationshipMultiplicity relationshipMultiplicity3 = RelationshipMultiplicity.Many;
				if (multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityFromRoleUpperBoundMustBeOne(this._principalRole.Name, this.ParentElement.Name));
				}
				else
				{
					if (flag2)
					{
						multiplicity = end.Multiplicity;
						relationshipMultiplicity3 = RelationshipMultiplicity.One;
						if (multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null)
						{
							string message = Strings.InvalidMultiplicityFromRoleToPropertyNullableV1(this._principalRole.Name, this.ParentElement.Name);
							base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, message);
							goto IL_28E;
						}
					}
					if ((flag9 && !flag2) || (!flag9 && !flag3))
					{
						multiplicity = end.Multiplicity;
						relationshipMultiplicity3 = RelationshipMultiplicity.One;
						if (!(multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null))
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
					}
				}
				IL_28E:
				multiplicity = end2.Multiplicity;
				relationshipMultiplicity3 = RelationshipMultiplicity.One;
				if ((multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null) && base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
				{
					base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleLowerBoundMustBeZero(this._dependentRole.Name, this.ParentElement.Name));
				}
				if (!flag4 && !this.ParentElement.IsForeignKey && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidToPropertyInRelationshipConstraint(this.DependentRole.Name, end2.Type.FQName, this.ParentElement.FQName));
				}
				if (flag)
				{
					multiplicity = end2.Multiplicity;
					relationshipMultiplicity3 = RelationshipMultiplicity.Many;
					if (multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null)
					{
						base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeOne(end2.Name, this.ParentElement.Name));
					}
				}
				else
				{
					multiplicity = end2.Multiplicity;
					relationshipMultiplicity3 = RelationshipMultiplicity.Many;
					if (!(multiplicity.GetValueOrDefault() == relationshipMultiplicity3 & multiplicity != null))
					{
						base.AddError(ErrorCode.InvalidMultiplicityInRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidMultiplicityToRoleUpperBoundMustBeMany(end2.Name, this.ParentElement.Name));
					}
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
						base.AddError(ErrorCode.TypeMismatchRelationshipConstaint, EdmSchemaErrorSeverity.Error, Strings.TypeMismatchRelationshipConstaint(this._dependentRole.RoleProperties[i].Name, this._dependentRole.End.Type.Identity, this._principalRole.RoleProperties[i].Name, this._principalRole.End.Type.Identity, this.ParentElement.Name));
					}
				}
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000AC894 File Offset: 0x000AAA94
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

		// Token: 0x06002D81 RID: 11649 RVA: 0x000AC908 File Offset: 0x000AAB08
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

		// Token: 0x06002D82 RID: 11650 RVA: 0x000173E2 File Offset: 0x000155E2
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000AC9E1 File Offset: 0x000AABE1
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

		// Token: 0x06002D84 RID: 11652 RVA: 0x000ACA1D File Offset: 0x000AAC1D
		internal void HandleReferentialConstraintPrincipalRoleElement(XmlReader reader)
		{
			this._principalRole = new ReferentialConstraintRoleElement(this);
			this._principalRole.Parse(reader);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000ACA37 File Offset: 0x000AAC37
		internal void HandleReferentialConstraintDependentRoleElement(XmlReader reader)
		{
			this._dependentRole = new ReferentialConstraintRoleElement(this);
			this._dependentRole.Parse(reader);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000ACA51 File Offset: 0x000AAC51
		internal override void ResolveTopLevelNames()
		{
			this._dependentRole.ResolveTopLevelNames();
			this._principalRole.ResolveTopLevelNames();
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000ACA69 File Offset: 0x000AAC69
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x000ACA76 File Offset: 0x000AAC76
		internal ReferentialConstraintRoleElement PrincipalRole
		{
			get
			{
				return this._principalRole;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000ACA7E File Offset: 0x000AAC7E
		internal ReferentialConstraintRoleElement DependentRole
		{
			get
			{
				return this._dependentRole;
			}
		}

		// Token: 0x040013E2 RID: 5090
		private const char KEY_DELIMITER = ' ';

		// Token: 0x040013E3 RID: 5091
		private ReferentialConstraintRoleElement _principalRole;

		// Token: 0x040013E4 RID: 5092
		private ReferentialConstraintRoleElement _dependentRole;
	}
}
