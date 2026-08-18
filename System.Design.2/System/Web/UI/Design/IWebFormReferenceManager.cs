using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200005D RID: 93
	[Obsolete("The recommended alternative is System.Web.UI.Design.WebFormsReferenceManager. The WebFormsReferenceManager contains additional functionality and allows for more extensibility. To get the WebFormsReferenceManager use the RootDesigner.ReferenceManager property from your ControlDesigner. http://go.microsoft.com/fwlink/?linkid=14202")]
	public interface IWebFormReferenceManager
	{
		// Token: 0x060002DC RID: 732
		Type GetObjectType(string tagPrefix, string typeName);

		// Token: 0x060002DD RID: 733
		string GetTagPrefix(Type objectType);

		// Token: 0x060002DE RID: 734
		string GetRegisterDirectives();
	}
}
