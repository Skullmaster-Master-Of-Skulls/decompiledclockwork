using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200072D RID: 1837
	internal class SmuggledMethodCallMessage : MessageSmuggler
	{
		// Token: 0x060041DA RID: 16858 RVA: 0x000E01D4 File Offset: 0x000DF1D4
		internal static SmuggledMethodCallMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new SmuggledMethodCallMessage(methodCallMessage);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000E01F3 File Offset: 0x000DF1F3
		private SmuggledMethodCallMessage()
		{
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000E01FC File Offset: 0x000DF1FC
		private SmuggledMethodCallMessage(IMethodCallMessage mcm)
		{
			this._uri = mcm.Uri;
			this._methodName = mcm.MethodName;
			this._typeName = mcm.TypeName;
			ArrayList arrayList = null;
			IInternalMessage internalMessage = mcm as IInternalMessage;
			if (internalMessage == null || internalMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mcm, ref arrayList);
			}
			if (mcm.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = mcm.MethodBase.GetGenericArguments();
				if (genericArguments != null && genericArguments.Length > 0)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					this._instantiation = new MessageSmuggler.SerializedArg(arrayList.Count);
					arrayList.Add(genericArguments);
				}
			}
			if (RemotingServices.IsMethodOverloaded(mcm))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._methodSignature = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(mcm.MethodSignature);
			}
			LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._callContext = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(logicalCallContext);
			}
			else
			{
				this._callContext = logicalCallContext.RemotingData.LogicalCallID;
			}
			this._args = MessageSmuggler.FixupArgs(mcm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000E0344 File Offset: 0x000DF344
		internal ArrayList FixupForNewAppDomain()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				result = CrossAppDomainSerializer.DeserializeMessageParts(new MemoryStream(this._serializedArgs));
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x000E0374 File Offset: 0x000DF374
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x000E037C File Offset: 0x000DF37C
		internal string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x000E0384 File Offset: 0x000DF384
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000E038C File Offset: 0x000DF38C
		internal Type[] GetInstantiation(ArrayList deserializedArgs)
		{
			if (this._instantiation != null)
			{
				return (Type[])deserializedArgs[this._instantiation.Index];
			}
			return null;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000E03AE File Offset: 0x000DF3AE
		internal object[] GetMethodSignature(ArrayList deserializedArgs)
		{
			if (this._methodSignature != null)
			{
				return (object[])deserializedArgs[this._methodSignature.Index];
			}
			return null;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000E03D0 File Offset: 0x000DF3D0
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000E03E0 File Offset: 0x000DF3E0
		internal LogicalCallContext GetCallContext(ArrayList deserializedArgs)
		{
			if (this._callContext == null)
			{
				return null;
			}
			if (this._callContext is string)
			{
				return new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = (string)this._callContext
					}
				};
			}
			return (LogicalCallContext)deserializedArgs[((MessageSmuggler.SerializedArg)this._callContext).Index];
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060041E5 RID: 16869 RVA: 0x000E043D File Offset: 0x000DF43D
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000E0448 File Offset: 0x000DF448
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002107 RID: 8455
		private string _uri;

		// Token: 0x04002108 RID: 8456
		private string _methodName;

		// Token: 0x04002109 RID: 8457
		private string _typeName;

		// Token: 0x0400210A RID: 8458
		private object[] _args;

		// Token: 0x0400210B RID: 8459
		private byte[] _serializedArgs;

		// Token: 0x0400210C RID: 8460
		private MessageSmuggler.SerializedArg _methodSignature;

		// Token: 0x0400210D RID: 8461
		private MessageSmuggler.SerializedArg _instantiation;

		// Token: 0x0400210E RID: 8462
		private object _callContext;

		// Token: 0x0400210F RID: 8463
		private int _propertyCount;
	}
}
