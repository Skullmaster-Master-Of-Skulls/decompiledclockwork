using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007FC RID: 2044
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x06004855 RID: 18517 RVA: 0x000FAD08 File Offset: 0x000F9D08
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x000FAD76 File Offset: 0x000F9D76
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x000FAD7E File Offset: 0x000F9D7E
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000FAD86 File Offset: 0x000F9D86
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x000FAD8E File Offset: 0x000F9D8E
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x000FAD96 File Offset: 0x000F9D96
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x000FAD9E File Offset: 0x000F9D9E
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x000FADA6 File Offset: 0x000F9DA6
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x000FADB4 File Offset: 0x000F9DB4
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002544 RID: 9540
		private object[] _inargs;

		// Token: 0x04002545 RID: 9541
		private string _methodName;

		// Token: 0x04002546 RID: 9542
		private string _typeName;

		// Token: 0x04002547 RID: 9543
		private object _methodSignature;

		// Token: 0x04002548 RID: 9544
		private Type[] _instArgs;

		// Token: 0x04002549 RID: 9545
		private object[] _args;

		// Token: 0x0400254A RID: 9546
		private LogicalCallContext _logicalCallContext;

		// Token: 0x0400254B RID: 9547
		private object[] _properties;
	}
}
