using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000711 RID: 1809
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06004074 RID: 16500 RVA: 0x000DB994 File Offset: 0x000DA994
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._ret = ret;
			this._outArgs = outArgs;
			this._outArgsCount = outArgsCount;
			if (callCtx != null)
			{
				this._callContext = callCtx;
			}
			else
			{
				this._callContext = CallContext.GetLogicalCallContext();
			}
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000DBA24 File Offset: 0x000DAA24
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._e = (ReturnMessage.IsCustomErrorEnabled() ? new RemotingException(Environment.GetResourceString("Remoting_InternalError")) : e);
			this._callContext = CallContext.GetLogicalCallContext();
			if (mcm != null)
			{
				this._URI = mcm.Uri;
				this._methodName = mcm.MethodName;
				this._methodSignature = null;
				this._typeName = mcm.TypeName;
				this._hasVarArgs = mcm.HasVarArgs;
				this._methodBase = mcm.MethodBase;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000DBAA7 File Offset: 0x000DAAA7
		// (set) Token: 0x06004077 RID: 16503 RVA: 0x000DBAAF File Offset: 0x000DAAAF
		public string Uri
		{
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x000DBAB8 File Offset: 0x000DAAB8
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x000DBAC0 File Offset: 0x000DAAC0
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000DBAC8 File Offset: 0x000DAAC8
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
				{
					this._methodSignature = Message.GenerateMethodSignature(this._methodBase);
				}
				return this._methodSignature;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x000DBAF1 File Offset: 0x000DAAF1
		public MethodBase MethodBase
		{
			get
			{
				return this._methodBase;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000DBAF9 File Offset: 0x000DAAF9
		public bool HasVarArgs
		{
			get
			{
				return this._hasVarArgs;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x000DBB01 File Offset: 0x000DAB01
		public int ArgCount
		{
			get
			{
				if (this._outArgs == null)
				{
					return this._outArgsCount;
				}
				return this._outArgs.Length;
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000DBB1C File Offset: 0x000DAB1C
		public object GetArg(int argNum)
		{
			if (this._outArgs == null)
			{
				if (argNum < 0 || argNum >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return null;
			}
			else
			{
				if (argNum < 0 || argNum >= this._outArgs.Length)
				{
					throw new ArgumentOutOfRangeException("argNum");
				}
				return this._outArgs[argNum];
			}
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000DBB70 File Offset: 0x000DAB70
		public string GetArgName(int index)
		{
			if (this._outArgs == null)
			{
				if (index < 0 || index >= this._outArgsCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
			}
			else if (index < 0 || index >= this._outArgs.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._methodBase != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._methodBase);
				return reflectionCachedData.Parameters[index].Name;
			}
			return "__param" + index;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000DBBE9 File Offset: 0x000DABE9
		public object[] Args
		{
			get
			{
				if (this._outArgs == null)
				{
					return new object[this._outArgsCount];
				}
				return this._outArgs;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x000DBC05 File Offset: 0x000DAC05
		public int OutArgCount
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000DBC27 File Offset: 0x000DAC27
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000DBC4A File Offset: 0x000DAC4A
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000DBC6D File Offset: 0x000DAC6D
		public object[] OutArgs
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x000DBC8F File Offset: 0x000DAC8F
		public Exception Exception
		{
			get
			{
				return this._e;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000DBC97 File Offset: 0x000DAC97
		public virtual object ReturnValue
		{
			get
			{
				return this._ret;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000DBC9F File Offset: 0x000DAC9F
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MRMDictionary(this, null);
				}
				return (MRMDictionary)this._properties;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000DBCC1 File Offset: 0x000DACC1
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000DBCC9 File Offset: 0x000DACC9
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000DBCE4 File Offset: 0x000DACE4
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = ctx;
			return callContext;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000DBD00 File Offset: 0x000DAD00
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000DBD10 File Offset: 0x000DAD10
		internal static bool IsCustomErrorEnabled()
		{
			object data = CallContext.GetData("__CustomErrorsEnabled");
			return data != null && (bool)data;
		}

		// Token: 0x0400208C RID: 8332
		internal object _ret;

		// Token: 0x0400208D RID: 8333
		internal object _properties;

		// Token: 0x0400208E RID: 8334
		internal string _URI;

		// Token: 0x0400208F RID: 8335
		internal Exception _e;

		// Token: 0x04002090 RID: 8336
		internal object[] _outArgs;

		// Token: 0x04002091 RID: 8337
		internal int _outArgsCount;

		// Token: 0x04002092 RID: 8338
		internal string _methodName;

		// Token: 0x04002093 RID: 8339
		internal string _typeName;

		// Token: 0x04002094 RID: 8340
		internal Type[] _methodSignature;

		// Token: 0x04002095 RID: 8341
		internal bool _hasVarArgs;

		// Token: 0x04002096 RID: 8342
		internal LogicalCallContext _callContext;

		// Token: 0x04002097 RID: 8343
		internal ArgMapper _argMapper;

		// Token: 0x04002098 RID: 8344
		internal MethodBase _methodBase;
	}
}
