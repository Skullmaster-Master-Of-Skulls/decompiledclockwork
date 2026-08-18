using System;
using System.Collections;
using System.Runtime.Remoting.Proxies;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200072A RID: 1834
	internal class MessageSmuggler
	{
		// Token: 0x060041CF RID: 16847 RVA: 0x000DFEAE File Offset: 0x000DEEAE
		private static bool CanSmuggleObjectDirectly(object obj)
		{
			return obj is string || obj.GetType() == typeof(void) || obj.GetType().IsPrimitive;
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000DFEDC File Offset: 0x000DEEDC
		protected static object[] FixupArgs(object[] args, ref ArrayList argsToSerialize)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.FixupArg(args[i], ref argsToSerialize);
			}
			return array;
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000DFF10 File Offset: 0x000DEF10
		protected static object FixupArg(object arg, ref ArrayList argsToSerialize)
		{
			if (arg == null)
			{
				return null;
			}
			MarshalByRefObject marshalByRefObject = arg as MarshalByRefObject;
			int count;
			if (marshalByRefObject != null)
			{
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject) || RemotingServices.GetRealProxy(marshalByRefObject) is RemotingProxy)
				{
					ObjRef objRef = RemotingServices.MarshalInternal(marshalByRefObject, null, null);
					if (objRef.CanSmuggle())
					{
						if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
						{
							ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(marshalByRefObject);
							serverIdentity.SetHandle();
							objRef.SetServerIdentity(serverIdentity.GetHandle());
							objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
						}
						ObjRef objRef2 = objRef.CreateSmuggleableCopy();
						objRef2.SetMarshaledObject();
						return new SmuggledObjRef(objRef2);
					}
				}
				if (argsToSerialize == null)
				{
					argsToSerialize = new ArrayList();
				}
				count = argsToSerialize.Count;
				argsToSerialize.Add(arg);
				return new MessageSmuggler.SerializedArg(count);
			}
			if (MessageSmuggler.CanSmuggleObjectDirectly(arg))
			{
				return arg;
			}
			Array array = arg as Array;
			if (array != null)
			{
				Type elementType = array.GetType().GetElementType();
				if (elementType.IsPrimitive || elementType == typeof(string))
				{
					return array.Clone();
				}
			}
			if (argsToSerialize == null)
			{
				argsToSerialize = new ArrayList();
			}
			count = argsToSerialize.Count;
			argsToSerialize.Add(arg);
			return new MessageSmuggler.SerializedArg(count);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000E002C File Offset: 0x000DF02C
		protected static object[] UndoFixupArgs(object[] args, ArrayList deserializedArgs)
		{
			object[] array = new object[args.Length];
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				array[i] = MessageSmuggler.UndoFixupArg(args[i], deserializedArgs);
			}
			return array;
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000E0060 File Offset: 0x000DF060
		protected static object UndoFixupArg(object arg, ArrayList deserializedArgs)
		{
			SmuggledObjRef smuggledObjRef = arg as SmuggledObjRef;
			if (smuggledObjRef != null)
			{
				return smuggledObjRef.ObjRef.GetRealObjectHelper();
			}
			MessageSmuggler.SerializedArg serializedArg = arg as MessageSmuggler.SerializedArg;
			if (serializedArg != null)
			{
				return deserializedArgs[serializedArg.Index];
			}
			return arg;
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000E009C File Offset: 0x000DF09C
		protected static int StoreUserPropertiesForMethodMessage(IMethodMessage msg, ref ArrayList argsToSerialize)
		{
			IDictionary properties = msg.Properties;
			MessageDictionary messageDictionary = properties as MessageDictionary;
			if (messageDictionary == null)
			{
				int num = 0;
				foreach (object obj in properties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry);
					num++;
				}
				return num;
			}
			if (messageDictionary.HasUserData())
			{
				int num2 = 0;
				foreach (object obj2 in messageDictionary.InternalDictionary)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (argsToSerialize == null)
					{
						argsToSerialize = new ArrayList();
					}
					argsToSerialize.Add(dictionaryEntry2);
					num2++;
				}
				return num2;
			}
			return 0;
		}

		// Token: 0x0200072B RID: 1835
		protected class SerializedArg
		{
			// Token: 0x060041D6 RID: 16854 RVA: 0x000E01A4 File Offset: 0x000DF1A4
			public SerializedArg(int index)
			{
				this._index = index;
			}

			// Token: 0x17000B90 RID: 2960
			// (get) Token: 0x060041D7 RID: 16855 RVA: 0x000E01B3 File Offset: 0x000DF1B3
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x04002105 RID: 8453
			private int _index;
		}
	}
}
