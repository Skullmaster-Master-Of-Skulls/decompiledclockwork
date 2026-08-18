using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037E RID: 894
	internal sealed class Relationship : SchemaType, IRelationship
	{
		// Token: 0x0600203C RID: 8252 RVA: 0x00098C0C File Offset: 0x00096E0C
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x00098C67 File Offset: 0x00096E67
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

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x00098C82 File Offset: 0x00096E82
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

		// Token: 0x0600203F RID: 8255 RVA: 0x00098C9D File Offset: 0x00096E9D
		public bool TryGetEnd(string roleName, out IRelationshipEnd end)
		{
			return this._ends.TryGetEnd(roleName, out end);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x00098CAC File Offset: 0x00096EAC
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x00098CB4 File Offset: 0x00096EB4
		public RelationshipKind RelationshipKind { get; private set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x00098CBD File Offset: 0x00096EBD
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00098CC8 File Offset: 0x00096EC8
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

		// Token: 0x06002044 RID: 8260 RVA: 0x00098DBC File Offset: 0x00096FBC
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

		// Token: 0x06002045 RID: 8261 RVA: 0x00098E50 File Offset: 0x00097050
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

		// Token: 0x06002046 RID: 8262 RVA: 0x00098E8C File Offset: 0x0009708C
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

		// Token: 0x06002047 RID: 8263 RVA: 0x00098ED8 File Offset: 0x000970D8
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

		// Token: 0x04000B76 RID: 2934
		private RelationshipEndCollection _ends;

		// Token: 0x04000B77 RID: 2935
		private List<ReferentialConstraint> _constraints;

		// Token: 0x04000B78 RID: 2936
		private bool _isForeignKey;
	}
}
