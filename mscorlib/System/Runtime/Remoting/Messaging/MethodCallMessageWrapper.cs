using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000726 RID: 1830
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x0600419F RID: 16799 RVA: 0x000DF8CE File Offset: 0x000DE8CE
		public MethodCallMessageWrapper(IMethodCallMessage msg) : base(msg)
		{
			this._msg = msg;
			this._args = this._msg.Args;
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x000DF8EF File Offset: 0x000DE8EF
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x000DF8FC File Offset: 0x000DE8FC
		public virtual string Uri
		{
			get
			{
				return this._msg.Uri;
			}
			set
			{
				this._msg.Properties[Message.UriKey] = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x000DF914 File Offset: 0x000DE914
		public virtual string MethodName
		{
			get
			{
				return this._msg.MethodName;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000DF921 File Offset: 0x000DE921
		public virtual string TypeName
		{
			get
			{
				return this._msg.TypeName;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x000DF92E File Offset: 0x000DE92E
		public virtual object MethodSignature
		{
			get
			{
				return this._msg.MethodSignature;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000DF93B File Offset: 0x000DE93B
		public virtual LogicalCallContext LogicalCallContext
		{
			get
			{
				return this._msg.LogicalCallContext;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x000DF948 File Offset: 0x000DE948
		public virtual MethodBase MethodBase
		{
			get
			{
				return this._msg.MethodBase;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x000DF955 File Offset: 0x000DE955
		public virtual int ArgCount
		{
			get
			{
				if (this._args != null)
				{
					return this._args.Length;
				}
				return 0;
			}
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000DF969 File Offset: 0x000DE969
		public virtual string GetArgName(int index)
		{
			return this._msg.GetArgName(index);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000DF977 File Offset: 0x000DE977
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x000DF981 File Offset: 0x000DE981
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x000DF989 File Offset: 0x000DE989
		public virtual object[] Args
		{
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x000DF992 File Offset: 0x000DE992
		public virtual bool HasVarArgs
		{
			get
			{
				return this._msg.HasVarArgs;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000DF99F File Offset: 0x000DE99F
		public virtual int InArgCount
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000DF9C1 File Offset: 0x000DE9C1
		public virtual object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000DF9E4 File Offset: 0x000DE9E4
		public virtual string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000DFA07 File Offset: 0x000DEA07
		public virtual object[] InArgs
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x000DFA29 File Offset: 0x000DEA29
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.MCMWrapperDictionary(this, this._msg.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x040020F7 RID: 8439
		private IMethodCallMessage _msg;

		// Token: 0x040020F8 RID: 8440
		private IDictionary _properties;

		// Token: 0x040020F9 RID: 8441
		private ArgMapper _argMapper;

		// Token: 0x040020FA RID: 8442
		private object[] _args;

		// Token: 0x02000727 RID: 1831
		private class MCMWrapperDictionary : Hashtable
		{
			// Token: 0x060041B2 RID: 16818 RVA: 0x000DFA50 File Offset: 0x000DEA50
			public MCMWrapperDictionary(IMethodCallMessage msg, IDictionary idict)
			{
				this._mcmsg = msg;
				this._idict = idict;
			}

			// Token: 0x17000B80 RID: 2944
			public override object this[object key]
			{
				get
				{
					string text = key as string;
					string a;
					if (text != null && (a = text) != null)
					{
						if (a == "__Uri")
						{
							return this._mcmsg.Uri;
						}
						if (a == "__MethodName")
						{
							return this._mcmsg.MethodName;
						}
						if (a == "__MethodSignature")
						{
							return this._mcmsg.MethodSignature;
						}
						if (a == "__TypeName")
						{
							return this._mcmsg.TypeName;
						}
						if (a == "__Args")
						{
							return this._mcmsg.Args;
						}
					}
					return this._idict[key];
				}
				set
				{
					string text = key as string;
					if (text != null)
					{
						string a;
						if ((a = text) != null && (a == "__MethodName" || a == "__MethodSignature" || a == "__TypeName" || a == "__Args"))
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
						}
						this._idict[key] = value;
					}
				}
			}

			// Token: 0x040020FB RID: 8443
			private IMethodCallMessage _mcmsg;

			// Token: 0x040020FC RID: 8444
			private IDictionary _idict;
		}
	}
}
