using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI;

namespace System.Web.Caching
{
	// Token: 0x02000896 RID: 2198
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class SubstitutionResponseElement : ResponseElement
	{
		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06006722 RID: 26402 RVA: 0x0016C24B File Offset: 0x0016A44B
		public HttpResponseSubstitutionCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		// Token: 0x06006723 RID: 26403 RVA: 0x001697FE File Offset: 0x001679FE
		private SubstitutionResponseElement()
		{
		}

		// Token: 0x06006724 RID: 26404 RVA: 0x0016C253 File Offset: 0x0016A453
		public SubstitutionResponseElement(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._callback = callback;
		}

		// Token: 0x06006725 RID: 26405 RVA: 0x0016C270 File Offset: 0x0016A470
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			this._targetTypeName = Util.GetAssemblyQualifiedTypeName(this._callback.Method.ReflectedType);
			this._methodName = this._callback.Method.Name;
		}

		// Token: 0x06006726 RID: 26406 RVA: 0x0016C2A4 File Offset: 0x0016A4A4
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			Type type = BuildManager.GetType(this._targetTypeName, true, false);
			this._callback = (HttpResponseSubstitutionCallback)Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), type, this._methodName);
		}

		// Token: 0x04003544 RID: 13636
		[NonSerialized]
		private HttpResponseSubstitutionCallback _callback;

		// Token: 0x04003545 RID: 13637
		private string _targetTypeName;

		// Token: 0x04003546 RID: 13638
		private string _methodName;
	}
}
