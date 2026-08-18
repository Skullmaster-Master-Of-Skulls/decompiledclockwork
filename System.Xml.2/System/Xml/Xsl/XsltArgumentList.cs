using System;
using System.Collections;

namespace System.Xml.Xsl
{
	// Token: 0x020002D8 RID: 728
	public class XsltArgumentList
	{
		// Token: 0x06002BB6 RID: 11190 RVA: 0x000E7F27 File Offset: 0x000E6127
		public object GetParam(string name, string namespaceUri)
		{
			return this.parameters[new XmlQualifiedName(name, namespaceUri)];
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000E7F3B File Offset: 0x000E613B
		public object GetExtensionObject(string namespaceUri)
		{
			return this.extensions[namespaceUri];
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000E7F4C File Offset: 0x000E614C
		public void AddParam(string name, string namespaceUri, object parameter)
		{
			XsltArgumentList.CheckArgumentNull(name, "name");
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(parameter, "parameter");
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, namespaceUri);
			xmlQualifiedName.Verify();
			this.parameters.Add(xmlQualifiedName, parameter);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000E7F95 File Offset: 0x000E6195
		public void AddExtensionObject(string namespaceUri, object extension)
		{
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(extension, "extension");
			this.extensions.Add(namespaceUri, extension);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000E7FBC File Offset: 0x000E61BC
		public object RemoveParam(string name, string namespaceUri)
		{
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			object result = this.parameters[key];
			this.parameters.Remove(key);
			return result;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000E7FEC File Offset: 0x000E61EC
		public object RemoveExtensionObject(string namespaceUri)
		{
			object result = this.extensions[namespaceUri];
			this.extensions.Remove(namespaceUri);
			return result;
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06002BBC RID: 11196 RVA: 0x000E8013 File Offset: 0x000E6213
		// (remove) Token: 0x06002BBD RID: 11197 RVA: 0x000E802C File Offset: 0x000E622C
		public event XsltMessageEncounteredEventHandler XsltMessageEncountered
		{
			add
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Combine(this.xsltMessageEncountered, value);
			}
			remove
			{
				this.xsltMessageEncountered = (XsltMessageEncounteredEventHandler)Delegate.Remove(this.xsltMessageEncountered, value);
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000E8045 File Offset: 0x000E6245
		public void Clear()
		{
			this.parameters.Clear();
			this.extensions.Clear();
			this.xsltMessageEncountered = null;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000E8064 File Offset: 0x000E6264
		private static void CheckArgumentNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0400132C RID: 4908
		private Hashtable parameters = new Hashtable();

		// Token: 0x0400132D RID: 4909
		private Hashtable extensions = new Hashtable();

		// Token: 0x0400132E RID: 4910
		internal XsltMessageEncounteredEventHandler xsltMessageEncountered;
	}
}
