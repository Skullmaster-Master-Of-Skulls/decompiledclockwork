using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002FA RID: 762
	internal sealed class OnOperation : SchemaElement
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x000ABB05 File Offset: 0x000A9D05
		public OnOperation(RelationshipEnd parentElement, Operation operation) : base(parentElement)
		{
			this.Operation = operation;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x000ABB15 File Offset: 0x000A9D15
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x000ABB1D File Offset: 0x000A9D1D
		public Operation Operation
		{
			get
			{
				return this._Operation;
			}
			private set
			{
				this._Operation = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000ABB26 File Offset: 0x000A9D26
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000ABB2E File Offset: 0x000A9D2E
		public Action Action
		{
			get
			{
				return this._Action;
			}
			private set
			{
				this._Action = value;
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000A9C93 File Offset: 0x000A7E93
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000ABB37 File Offset: 0x000A9D37
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Action"))
			{
				this.HandleActionAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000ABB5C File Offset: 0x000A9D5C
		private void HandleActionAttribute(XmlReader reader)
		{
			RelationshipKind relationshipKind = this.ParentElement.ParentElement.RelationshipKind;
			string a = reader.Value.Trim();
			if (a == "None")
			{
				this.Action = Action.None;
				return;
			}
			if (!(a == "Cascade"))
			{
				base.AddError(ErrorCode.InvalidAction, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidAction(reader.Value, this.ParentElement.FQName));
				return;
			}
			this.Action = Action.Cascade;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000ABBD2 File Offset: 0x000A9DD2
		private new RelationshipEnd ParentElement
		{
			get
			{
				return (RelationshipEnd)base.ParentElement;
			}
		}

		// Token: 0x040013D8 RID: 5080
		private Operation _Operation;

		// Token: 0x040013D9 RID: 5081
		private Action _Action;
	}
}
