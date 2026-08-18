using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web
{
	// Token: 0x02000F60 RID: 3936
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ClientControlMethodAttribute : Attribute
	{
		// Token: 0x06009609 RID: 38409 RVA: 0x002186BE File Offset: 0x002168BE
		public ClientControlMethodAttribute() : this(true)
		{
		}

		// Token: 0x0600960A RID: 38410 RVA: 0x002186C7 File Offset: 0x002168C7
		public ClientControlMethodAttribute(bool isScriptMethod)
		{
			this._isScriptMethod = isScriptMethod;
		}

		// Token: 0x17002F66 RID: 12134
		// (get) Token: 0x0600960B RID: 38411 RVA: 0x002186D6 File Offset: 0x002168D6
		public bool IsScriptMethod
		{
			get
			{
				return this._isScriptMethod;
			}
		}

		// Token: 0x0600960C RID: 38412 RVA: 0x002186E0 File Offset: 0x002168E0
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ClientControlMethodAttribute clientControlMethodAttribute = obj as ClientControlMethodAttribute;
			return clientControlMethodAttribute != null && clientControlMethodAttribute._isScriptMethod == this._isScriptMethod;
		}

		// Token: 0x0600960D RID: 38413 RVA: 0x00218714 File Offset: 0x00216914
		public override int GetHashCode()
		{
			return this._isScriptMethod.GetHashCode();
		}

		// Token: 0x0600960E RID: 38414 RVA: 0x0021872F File Offset: 0x0021692F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ClientControlMethodAttribute.Default);
		}

		// Token: 0x04002AE3 RID: 10979
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Exposing this for user convenience")]
		private static readonly ClientControlMethodAttribute Yes = new ClientControlMethodAttribute(true);

		// Token: 0x04002AE4 RID: 10980
		private static readonly ClientControlMethodAttribute No = new ClientControlMethodAttribute(false);

		// Token: 0x04002AE5 RID: 10981
		private static readonly ClientControlMethodAttribute Default = ClientControlMethodAttribute.No;

		// Token: 0x04002AE6 RID: 10982
		private readonly bool _isScriptMethod;
	}
}
