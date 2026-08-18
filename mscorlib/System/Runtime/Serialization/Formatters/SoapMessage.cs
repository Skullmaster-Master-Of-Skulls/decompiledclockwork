using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007BB RID: 1979
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x000F087E File Offset: 0x000EF87E
		// (set) Token: 0x06004681 RID: 18049 RVA: 0x000F0886 File Offset: 0x000EF886
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000F088F File Offset: 0x000EF88F
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000F0897 File Offset: 0x000EF897
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x000F08A0 File Offset: 0x000EF8A0
		// (set) Token: 0x06004685 RID: 18053 RVA: 0x000F08A8 File Offset: 0x000EF8A8
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x000F08B1 File Offset: 0x000EF8B1
		// (set) Token: 0x06004687 RID: 18055 RVA: 0x000F08B9 File Offset: 0x000EF8B9
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x000F08C2 File Offset: 0x000EF8C2
		// (set) Token: 0x06004689 RID: 18057 RVA: 0x000F08CA File Offset: 0x000EF8CA
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x000F08D3 File Offset: 0x000EF8D3
		// (set) Token: 0x0600468B RID: 18059 RVA: 0x000F08DB File Offset: 0x000EF8DB
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x0400230F RID: 8975
		internal string[] paramNames;

		// Token: 0x04002310 RID: 8976
		internal object[] paramValues;

		// Token: 0x04002311 RID: 8977
		internal Type[] paramTypes;

		// Token: 0x04002312 RID: 8978
		internal string methodName;

		// Token: 0x04002313 RID: 8979
		internal string xmlNameSpace;

		// Token: 0x04002314 RID: 8980
		internal Header[] headers;
	}
}
