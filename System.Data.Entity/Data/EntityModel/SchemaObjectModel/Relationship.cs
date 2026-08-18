using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000302 RID: 770
	internal sealed class Relationship : SchemaType, IRelationship
	{
		// Token: 0x06002D93 RID: 11667 RVA: 0x000ACC14 File Offset: 0x000AAE14
		public Relationship(Schema parent, RelationshipKind kind) : base(parent)
		{
			this.RelationshipKind = kind;
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this._isForeignKey = false;
				base.OtherContent.Add(base.Schema.SchemaSource);
				return;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._isForeignKey = true;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000ACC6F File Offset: 0x000AAE6F
		public IList<IRelationshipEnd> Ends
		{
			get
			{
				if (this._ends == null)
				{
					this._ends = new RelationshipEndCollection();
				}
				return this._ends;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000ACC8A File Offset: 0x000AAE8A
		public IList<ReferentialConstraint> Constraints
		{
			get
			{
				if (this._constraints == null)
				{
					this._constraints = new List<ReferentialConstraint>();
				}
				return this._constraints;
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000ACCA5 File Offset: 0x000AAEA5
		public bool TryGetEnd(string roleName, out IRelationshipEnd end)
		{
			return this._ends.TryGetEnd(roleName, out end);
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000ACCB4 File Offset: 0x000AAEB4
		// (set) Token: 0x06002D98 RID: 11672 RVA: 0x000ACCBC File Offset: 0x000AAEBC
		public RelationshipKind RelationshipKind
		{
			get
			{
				return this._relationshipKind;
			}
			private set
			{
				this._relationshipKind = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000ACCC5 File Offset: 0x000AAEC5
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000ACCD0 File Offset: 0x000AAED0
		internal override void Validate()
		{
			base.Validate();
			bool flag = false;
			foreach (IRelationshipEnd relationshipEnd in this.Ends)
			{
				RelationshipEnd relationshipEnd2 = (RelationshipEnd)relationshipEnd;
				relationshipEnd2.Validate();
				if (this.RelationshipKind == RelationshipKind.Association && relationshipEnd2.Operations.Count > 0)
				{
					if (flag)
					{
						relationshipEnd2.AddError(ErrorCode.InvalidOperation, EdmSchemaErrorSeverity.Error, Strings.InvalidOperationMultipleEndsInAssociation);
					}
					flag = true;
				}
			}
			if (this.Constraints.Count == 0)
			{
				if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
				{
					base.AddError(ErrorCode.MissingConstraintOnRelationshipType, EdmSchemaErrorSeverity.Error, Strings.MissingConstraintOnRelationshipType(this.FQName));
					return;
				}
			}
			else
			{
				foreach (ReferentialConstraint referentialConstraint in this.Constraints)
				{
					referentialConstraint.Validate();
				}
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000ACDC0 File Offset: 0x000AAFC0
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			foreach (IRelationshipEnd relationshipEnd in this.Ends)
			{
				RelationshipEnd relationshipEnd2 = (RelationshipEnd)relationshipEnd;
				relationshipEnd2.ResolveTopLevelNames();
			}
			foreach (ReferentialConstraint referentialConstraint in this.Constraints)
			{
				referentialConstraint.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000ACE54 File Offset: 0x000AB054
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "End"))
			{
				this.HandleEndElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReferentialConstraint"))
			{
				this.HandleConstraintElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000ACE90 File Offset: 0x000AB090
		private void HandleEndElement(XmlReader reader)
		{
			RelationshipEnd relationshipEnd = new RelationshipEnd(this);
			relationshipEnd.Parse(reader);
			if (this.Ends.Count == 2)
			{
				base.AddError(ErrorCode.InvalidAssociation, EdmSchemaErrorSeverity.Error, Strings.TooManyAssociationEnds(this.FQName));
				return;
			}
			this.Ends.Add(relationshipEnd);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000ACEDC File Offset: 0x000AB0DC
		private void HandleConstraintElement(XmlReader reader)
		{
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(this);
			referentialConstraint.Parse(reader);
			this.Constraints.Add(referentialConstraint);
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel && base.Schema.SchemaVersion >= 2.0)
			{
				this._isForeignKey = true;
			}
		}

		// Token: 0x040013E7 RID: 5095
		private RelationshipKind _relationshipKind;

		// Token: 0x040013E8 RID: 5096
		private RelationshipEndCollection _ends;

		// Token: 0x040013E9 RID: 5097
		private List<ReferentialConstraint> _constraints;

		// Token: 0x040013EA RID: 5098
		private bool _isForeignKey;
	}
}
