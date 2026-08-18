using System;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002FF RID: 767
	internal class UsingElement : SchemaElement
	{
		// Token: 0x06002D74 RID: 11636 RVA: 0x000A9632 File Offset: 0x000A7832
		internal UsingElement(Schema parentElement) : base(parentElement)
		{
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000AC330 File Offset: 0x000AA530
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x000AC338 File Offset: 0x000AA538
		public virtual string Alias
		{
			get
			{
				return this._alias;
			}
			private set
			{
				this._alias = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000AC341 File Offset: 0x000AA541
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x000AC349 File Offset: 0x000AA549
		public virtual string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
			private set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00006174 File Offset: 0x00004374
		public override string FQName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000A9C93 File Offset: 0x000A7E93
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

		// Token: 0x06002D7B RID: 11643 RVA: 0x000AC352 File Offset: 0x000AA552
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Namespace"))
			{
				this.HandleNamespaceAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Alias"))
			{
				this.HandleAliasAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000AC38C File Offset: 0x000AA58C
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.NamespaceName, null);
			if (returnValue.Succeeded)
			{
				this.NamespaceName = returnValue.Value;
			}
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000AC3BC File Offset: 0x000AA5BC
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}

		// Token: 0x040013E0 RID: 5088
		private string _alias;

		// Token: 0x040013E1 RID: 5089
		private string _namespaceName;
	}
}
