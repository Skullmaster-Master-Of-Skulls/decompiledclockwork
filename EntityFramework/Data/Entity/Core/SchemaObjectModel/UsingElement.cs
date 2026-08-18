using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037A RID: 890
	internal class UsingElement : SchemaElement
	{
		// Token: 0x06002016 RID: 8214 RVA: 0x000981EB File Offset: 0x000963EB
		internal UsingElement(Schema parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000981F5 File Offset: 0x000963F5
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x000981FD File Offset: 0x000963FD
		public virtual string Alias { get; private set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x00098206 File Offset: 0x00096406
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x0009820E File Offset: 0x0009640E
		public virtual string NamespaceName { get; private set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x00098217 File Offset: 0x00096417
		public override string FQName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009821A File Offset: 0x0009641A
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0009823B File Offset: 0x0009643B
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

		// Token: 0x0600201E RID: 8222 RVA: 0x00098278 File Offset: 0x00096478
		private void HandleNamespaceAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this.NamespaceName);
			if (returnValue.Succeeded)
			{
				this.NamespaceName = returnValue.Value;
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000982A7 File Offset: 0x000964A7
		private void HandleAliasAttribute(XmlReader reader)
		{
			this.Alias = base.HandleUndottedNameAttribute(reader, this.Alias);
		}
	}
}
