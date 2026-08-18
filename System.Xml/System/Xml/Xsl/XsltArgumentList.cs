using System;
using System.Collections;

namespace System.Xml.Xsl
{
	// Token: 0x02000175 RID: 373
	public class XsltArgumentList
	{
		// Token: 0x060013E8 RID: 5096 RVA: 0x00055FA7 File Offset: 0x00054FA7
		public object GetParam(string name, string namespaceUri)
		{
			return this.parameters[new XmlQualifiedName(name, namespaceUri)];
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00055FBB File Offset: 0x00054FBB
		public object GetExtensionObject(string namespaceUri)
		{
			return this.extensions[namespaceUri];
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00055FCC File Offset: 0x00054FCC
		public void AddParam(string name, string namespaceUri, object parameter)
		{
			XsltArgumentList.CheckArgumentNull(name, "name");
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(parameter, "parameter");
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, namespaceUri);
			xmlQualifiedName.Verify();
			this.parameters.Add(xmlQualifiedName, parameter);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00056015 File Offset: 0x00055015
		public void AddExtensionObject(string namespaceUri, object extension)
		{
			XsltArgumentList.CheckArgumentNull(namespaceUri, "namespaceUri");
			XsltArgumentList.CheckArgumentNull(extension, "extension");
			this.extensions.Add(namespaceUri, extension);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0005603C File Offset: 0x0005503C
		public object RemoveParam(string name, string namespaceUri)
		{
			XmlQualifiedName key = new XmlQualifiedName(name, namespaceUri);
			object result = this.parameters[key];
			this.parameters.Remove(key);
			return result;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0005606C File Offset: 0x0005506C
		public object RemoveExtensionObject(string namespaceUri)
		{
			object result = this.extensions[namespaceUri];
			this.extensions.Remove(namespaceUri);
			return result;
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060013EE RID: 5102 RVA: 0x00056093 File Offset: 0x00055093
		// (remove) Token: 0x060013EF RID: 5103 RVA: 0x000560AC File Offset: 0x000550AC
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

		// Token: 0x060013F0 RID: 5104 RVA: 0x000560C5 File Offset: 0x000550C5
		public void Clear()
		{
			this.parameters.Clear();
			this.extensions.Clear();
			this.xsltMessageEncountered = null;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000560E4 File Offset: 0x000550E4
		private static void CheckArgumentNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x04000C3B RID: 3131
		private Hashtable parameters = new Hashtable();

		// Token: 0x04000C3C RID: 3132
		private Hashtable extensions = new Hashtable();

		// Token: 0x04000C3D RID: 3133
		internal XsltMessageEncounteredEventHandler xsltMessageEncountered;
	}
}
