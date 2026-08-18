using System;

namespace System.Security
{
	// Token: 0x02000612 RID: 1554
	[Serializable]
	internal sealed class SecurityDocumentElement : ISecurityElementFactory
	{
		// Token: 0x06003824 RID: 14372 RVA: 0x000BC2C2 File Offset: 0x000BB2C2
		internal SecurityDocumentElement(SecurityDocument document, int position)
		{
			this.m_document = document;
			this.m_position = position;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000BC2D8 File Offset: 0x000BB2D8
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this.m_document.GetElement(this.m_position, true);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000BC2EC File Offset: 0x000BB2EC
		object ISecurityElementFactory.Copy()
		{
			return new SecurityDocumentElement(this.m_document, this.m_position);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000BC2FF File Offset: 0x000BB2FF
		string ISecurityElementFactory.GetTag()
		{
			return this.m_document.GetTagForElement(this.m_position);
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000BC312 File Offset: 0x000BB312
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.m_document.GetAttributeForElement(this.m_position, attributeName);
		}

		// Token: 0x04001D17 RID: 7447
		private int m_position;

		// Token: 0x04001D18 RID: 7448
		private SecurityDocument m_document;
	}
}
