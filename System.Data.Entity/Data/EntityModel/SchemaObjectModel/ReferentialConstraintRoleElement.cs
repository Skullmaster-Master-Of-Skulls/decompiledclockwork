using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000301 RID: 769
	internal sealed class ReferentialConstraintRoleElement : SchemaElement
	{
		// Token: 0x06002D8A RID: 11658 RVA: 0x000A9632 File Offset: 0x000A7832
		public ReferentialConstraintRoleElement(ReferentialConstraint parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002D8B RID: 11659 RVA: 0x000ACA86 File Offset: 0x000AAC86
		public IList<PropertyRefElement> RoleProperties
		{
			get
			{
				if (this._roleProperties == null)
				{
					this._roleProperties = new List<PropertyRefElement>();
				}
				return this._roleProperties;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000ACAA1 File Offset: 0x000AACA1
		public IRelationshipEnd End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000ACAA9 File Offset: 0x000AACA9
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "PropertyRef"))
			{
				this.HandlePropertyRefElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000ACACE File Offset: 0x000AACCE
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000ACAE8 File Offset: 0x000AACE8
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.RoleProperties.Add(propertyRefElement);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000ACB14 File Offset: 0x000AAD14
		private void HandleRoleAttribute(XmlReader reader)
		{
			string name;
			Utils.GetString(base.Schema, reader, out name);
			this.Name = name;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000ACB38 File Offset: 0x000AAD38
		internal override void ResolveTopLevelNames()
		{
			IRelationship relationship = (IRelationship)base.ParentElement.ParentElement;
			if (!relationship.TryGetEnd(this.Name, out this._end))
			{
				base.AddError(ErrorCode.InvalidRoleInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidEndRoleInRelationshipConstraint(this.Name, relationship.Name));
				return;
			}
			SchemaEntityType type = this._end.Type;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000ACB94 File Offset: 0x000AAD94
		internal override void Validate()
		{
			base.Validate();
			foreach (PropertyRefElement propertyRefElement in this._roleProperties)
			{
				if (!propertyRefElement.ResolveNames(this._end.Type))
				{
					base.AddError(ErrorCode.InvalidPropertyInRelationshipConstraint, EdmSchemaErrorSeverity.Error, Strings.InvalidPropertyInRelationshipConstraint(propertyRefElement.Name, this.Name));
				}
			}
		}

		// Token: 0x040013E5 RID: 5093
		private List<PropertyRefElement> _roleProperties;

		// Token: 0x040013E6 RID: 5094
		private IRelationshipEnd _end;
	}
}
