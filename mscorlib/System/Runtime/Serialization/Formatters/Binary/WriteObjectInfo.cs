using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007F8 RID: 2040
	internal sealed class WriteObjectInfo
	{
		// Token: 0x06004829 RID: 18473 RVA: 0x000F9CDF File Offset: 0x000F8CDF
		internal WriteObjectInfo()
		{
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x000F9CE7 File Offset: 0x000F8CE7
		internal void ObjectEnd()
		{
			WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x000F9CF8 File Offset: 0x000F8CF8
		private void InternalInit()
		{
			this.obj = null;
			this.objectType = null;
			this.isSi = false;
			this.isNamed = false;
			this.isTyped = false;
			this.isArray = false;
			this.si = null;
			this.cache = null;
			this.memberData = null;
			this.objectId = 0L;
			this.assemId = 0L;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x000F9D54 File Offset: 0x000F8D54
		internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(obj, surrogateSelector, context, serObjectInfoInit, converter, objectWriter);
			return objectInfo;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x000F9D78 File Offset: 0x000F8D78
		internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter)
		{
			this.context = context;
			this.obj = obj;
			this.serObjectInfoInit = serObjectInfoInit;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				this.objectType = Converter.typeofMarshalByRefObject;
			}
			else
			{
				this.objectType = obj.GetType();
			}
			if (this.objectType.IsArray)
			{
				this.isArray = true;
				this.InitNoMembers();
				return;
			}
			objectWriter.ObjectManager.RegisterObject(obj);
			ISurrogateSelector surrogateSelector2;
			if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out surrogateSelector2)) != null)
			{
				this.si = new SerializationInfo(this.objectType, converter);
				if (!this.objectType.IsPrimitive)
				{
					this.serializationSurrogate.GetObjectData(obj, this.si, context);
				}
				this.InitSiWrite();
				return;
			}
			if (!(obj is ISerializable))
			{
				this.InitMemberInfo();
				return;
			}
			if (!this.objectType.IsSerializable)
			{
				throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_NonSerType"), new object[]
				{
					this.objectType.FullName,
					this.objectType.Assembly.FullName
				}));
			}
			this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
			((ISerializable)obj).GetObjectData(this.si, context);
			this.InitSiWrite();
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x000F9ED0 File Offset: 0x000F8ED0
		[Conditional("SER_LOGGING")]
		private void DumpMemberInfo()
		{
			for (int i = 0; i < this.cache.memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x000F9EF8 File Offset: 0x000F8EF8
		internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(objectType, surrogateSelector, context, serObjectInfoInit, converter);
			return objectInfo;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x000F9F1C File Offset: 0x000F8F1C
		internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter)
		{
			this.objectType = objectType;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.si = new SerializationInfo(objectType, converter);
				this.cache = new SerObjectInfoCache();
				this.cache.fullTypeName = this.si.FullTypeName;
				this.cache.assemblyString = this.si.AssemblyName;
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
				this.cache = new SerObjectInfoCache();
				this.cache.fullTypeName = this.si.FullTypeName;
				this.cache.assemblyString = this.si.AssemblyName;
				this.isSi = true;
			}
			if (!this.isSi)
			{
				this.InitMemberInfo();
			}
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x000FA030 File Offset: 0x000F9030
		private void InitSiWrite()
		{
			this.isSi = true;
			SerializationInfoEnumerator enumerator = this.si.GetEnumerator();
			int memberCount = this.si.MemberCount;
			int num = memberCount;
			this.cache = new SerObjectInfoCache();
			this.cache.memberNames = new string[num];
			this.cache.memberTypes = new Type[num];
			this.memberData = new object[num];
			this.cache.fullTypeName = this.si.FullTypeName;
			this.cache.assemblyString = this.si.AssemblyName;
			enumerator = this.si.GetEnumerator();
			int num2 = 0;
			while (enumerator.MoveNext())
			{
				this.cache.memberNames[num2] = enumerator.Name;
				this.cache.memberTypes[num2] = enumerator.ObjectType;
				this.memberData[num2] = enumerator.Value;
				num2++;
			}
			this.isNamed = true;
			this.isTyped = false;
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x000FA128 File Offset: 0x000F9128
		private void InitNoMembers()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache();
				this.cache.fullTypeName = this.objectType.FullName;
				this.cache.assemblyString = this.objectType.Assembly.FullName;
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x000FA1B8 File Offset: 0x000F91B8
		private void InitMemberInfo()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache();
				this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
				int num = this.cache.memberInfos.Length;
				this.cache.memberNames = new string[num];
				this.cache.memberTypes = new Type[num];
				for (int i = 0; i < num; i++)
				{
					this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
				this.cache.fullTypeName = this.objectType.FullName;
				this.cache.assemblyString = this.objectType.Assembly.FullName;
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
			if (this.obj != null)
			{
				this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x000FA312 File Offset: 0x000F9312
		internal string GetTypeFullName()
		{
			return this.cache.fullTypeName;
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x000FA31F File Offset: 0x000F931F
		internal string GetAssemblyString()
		{
			return this.cache.assemblyString;
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x000FA32C File Offset: 0x000F932C
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type result;
			if (objMember is FieldInfo)
			{
				result = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_SerMemberInfo"), new object[]
					{
						objMember.GetType()
					}));
				}
				result = ((PropertyInfo)objMember).PropertyType;
			}
			return result;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x000FA394 File Offset: 0x000F9394
		internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
		{
			outMemberNames = this.cache.memberNames;
			outMemberTypes = this.cache.memberTypes;
			outMemberData = this.memberData;
			if (this.isSi && !this.isNamed)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableMemberInfo"));
			}
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x000FA3E4 File Offset: 0x000F93E4
		private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			WriteObjectInfo writeObjectInfo;
			if (!serObjectInfoInit.oiPool.IsEmpty())
			{
				writeObjectInfo = (WriteObjectInfo)serObjectInfoInit.oiPool.Pop();
				writeObjectInfo.InternalInit();
			}
			else
			{
				writeObjectInfo = new WriteObjectInfo();
				writeObjectInfo.objectInfoId = serObjectInfoInit.objectInfoIdCount++;
			}
			return writeObjectInfo;
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x000FA437 File Offset: 0x000F9437
		private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
		{
			serObjectInfoInit.oiPool.Push(objectInfo);
		}

		// Token: 0x04002519 RID: 9497
		private static SecurityPermission serializationPermission = new SecurityPermission(SecurityPermissionFlag.SerializationFormatter);

		// Token: 0x0400251A RID: 9498
		internal int objectInfoId;

		// Token: 0x0400251B RID: 9499
		internal object obj;

		// Token: 0x0400251C RID: 9500
		internal Type objectType;

		// Token: 0x0400251D RID: 9501
		internal bool isSi;

		// Token: 0x0400251E RID: 9502
		internal bool isNamed;

		// Token: 0x0400251F RID: 9503
		internal bool isTyped;

		// Token: 0x04002520 RID: 9504
		internal bool isArray;

		// Token: 0x04002521 RID: 9505
		internal SerializationInfo si;

		// Token: 0x04002522 RID: 9506
		internal SerObjectInfoCache cache;

		// Token: 0x04002523 RID: 9507
		internal object[] memberData;

		// Token: 0x04002524 RID: 9508
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x04002525 RID: 9509
		internal StreamingContext context;

		// Token: 0x04002526 RID: 9510
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04002527 RID: 9511
		internal long objectId;

		// Token: 0x04002528 RID: 9512
		internal long assemId;
	}
}
