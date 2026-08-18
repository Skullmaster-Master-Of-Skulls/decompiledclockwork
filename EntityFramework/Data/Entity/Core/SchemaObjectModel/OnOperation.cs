using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000374 RID: 884
	internal sealed class OnOperation : SchemaElement
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x000965D5 File Offset: 0x000947D5
		public OnOperation(RelationshipEnd parentElement, Operation operation) : base(parentElement, null)
		{
			this.Operation = operation;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000965E6 File Offset: 0x000947E6
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x000965EE File Offset: 0x000947EE
		public Operation Operation { get; private set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x000965F7 File Offset: 0x000947F7
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x000965FF File Offset: 0x000947FF
		public Action Action { get; private set; }

		// Token: 0x06001FB6 RID: 8118 RVA: 0x00096608 File Offset: 0x00094808
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00096629 File Offset: 0x00094829
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

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00096650 File Offset: 0x00094850
		private void HandleActionAttribute(XmlReader reader)
		{
			RelationshipKind relationshipKind = this.ParentElement.ParentElement.RelationshipKind;
			string a;
			if ((a = reader.Value.Trim()) != null)
			{
				if (a == "None")
				{
					this.Action = Action.None;
					return;
				}
				if (a == "Cascade")
				{
					this.Action = Action.Cascade;
					return;
				}
			}
			base.AddError(ErrorCode.InvalidAction, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidAction(reader.Value, this.ParentElement.FQName));
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x000966C9 File Offset: 0x000948C9
		private new RelationshipEnd ParentElement
		{
			get
			{
				return (RelationshipEnd)base.ParentElement;
			}
		}
	}
}
