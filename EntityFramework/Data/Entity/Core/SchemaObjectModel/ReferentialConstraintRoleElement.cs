using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037D RID: 893
	internal sealed class ReferentialConstraintRoleElement : SchemaElement
	{
		// Token: 0x06002033 RID: 8243 RVA: 0x00098A74 File Offset: 0x00096C74
		public ReferentialConstraintRoleElement(ReferentialConstraint parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x00098A7E File Offset: 0x00096C7E
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x00098A99 File Offset: 0x00096C99
		public IRelationshipEnd End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00098AA1 File Offset: 0x00096CA1
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

		// Token: 0x06002037 RID: 8247 RVA: 0x00098AC6 File Offset: 0x00096CC6
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00098AE0 File Offset: 0x00096CE0
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.RoleProperties.Add(propertyRefElement);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00098B0C File Offset: 0x00096D0C
		private void HandleRoleAttribute(XmlReader reader)
		{
			string name;
			Utils.GetString(base.Schema, reader, out name);
			this.Name = name;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00098B30 File Offset: 0x00096D30
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

		// Token: 0x0600203B RID: 8251 RVA: 0x00098B8C File Offset: 0x00096D8C
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

		// Token: 0x04000B74 RID: 2932
		private List<PropertyRefElement> _roleProperties;

		// Token: 0x04000B75 RID: 2933
		private IRelationshipEnd _end;
	}
}
