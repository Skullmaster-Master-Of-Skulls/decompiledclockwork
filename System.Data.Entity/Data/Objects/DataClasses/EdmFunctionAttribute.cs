using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200019A RID: 410
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class EdmFunctionAttribute : Attribute
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x000667CE File Offset: 0x000649CE
		public EdmFunctionAttribute(string namespaceName, string functionName)
		{
			this._namespaceName = namespaceName;
			this._functionName = functionName;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x000667E4 File Offset: 0x000649E4
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000667EC File Offset: 0x000649EC
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x04000BD7 RID: 3031
		private readonly string _namespaceName;

		// Token: 0x04000BD8 RID: 3032
		private readonly string _functionName;
	}
}
