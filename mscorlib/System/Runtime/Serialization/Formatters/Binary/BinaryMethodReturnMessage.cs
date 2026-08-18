using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007FD RID: 2045
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x0600485E RID: 18526 RVA: 0x000FADF4 File Offset: 0x000F9DF4
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
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

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x000FAE4F File Offset: 0x000F9E4F
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x000FAE57 File Offset: 0x000F9E57
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x000FAE5F File Offset: 0x000F9E5F
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x000FAE67 File Offset: 0x000F9E67
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x000FAE6F File Offset: 0x000F9E6F
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x000FAE80 File Offset: 0x000F9E80
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x0400254C RID: 9548
		private object[] _outargs;

		// Token: 0x0400254D RID: 9549
		private Exception _exception;

		// Token: 0x0400254E RID: 9550
		private object _returnValue;

		// Token: 0x0400254F RID: 9551
		private object[] _args;

		// Token: 0x04002550 RID: 9552
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002551 RID: 9553
		private object[] _properties;
	}
}
