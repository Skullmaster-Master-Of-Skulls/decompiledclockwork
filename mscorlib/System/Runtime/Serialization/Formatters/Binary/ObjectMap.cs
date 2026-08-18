using System;
using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E9 RID: 2025
	internal sealed class ObjectMap
	{
		// Token: 0x06004798 RID: 18328 RVA: 0x000F5500 File Offset: 0x000F4500
		internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			this.objectName = objectName;
			this.objectType = objectType;
			this.memberNames = memberNames;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
			this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
			this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
			this.typeInformationA = new object[this.memberTypes.Length];
			for (int i = 0; i < this.memberTypes.Length; i++)
			{
				object obj = null;
				BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[i], out obj);
				this.binaryTypeEnumA[i] = parserBinaryTypeInfo;
				this.typeInformationA[i] = obj;
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x000F55C8 File Offset: 0x000F45C8
		internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			this.objectName = objectName;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			if (assemblyInfo == null)
			{
				throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_Assembly"), new object[]
				{
					objectName
				}));
			}
			this.objectType = objectReader.GetType(assemblyInfo, objectName);
			this.memberTypes = new Type[memberNames.Length];
			for (int i = 0; i < memberNames.Length; i++)
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE;
				string text;
				Type type;
				bool flag;
				BinaryConverter.TypeFromInfo(binaryTypeEnumA[i], typeInformationA[i], objectReader, (BinaryAssemblyInfo)assemIdToAssemblyTable[memberAssemIds[i]], out internalPrimitiveTypeE, out text, out type, out flag);
				this.memberTypes[i] = type;
			}
			this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, null);
			if (!this.objectInfo.isSi)
			{
				this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x000F56DC File Offset: 0x000F46DC
		internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isInitObjectInfo)
			{
				this.isInitObjectInfo = false;
				this.objectInfo.InitDataStore(ref si, ref memberData);
				return this.objectInfo;
			}
			this.objectInfo.PrepareForReuse();
			this.objectInfo.InitDataStore(ref si, ref memberData);
			return this.objectInfo;
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000F572A File Offset: 0x000F472A
		internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000F573C File Offset: 0x000F473C
		internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
		}

		// Token: 0x0400243A RID: 9274
		internal string objectName;

		// Token: 0x0400243B RID: 9275
		internal Type objectType;

		// Token: 0x0400243C RID: 9276
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400243D RID: 9277
		internal object[] typeInformationA;

		// Token: 0x0400243E RID: 9278
		internal Type[] memberTypes;

		// Token: 0x0400243F RID: 9279
		internal string[] memberNames;

		// Token: 0x04002440 RID: 9280
		internal ReadObjectInfo objectInfo;

		// Token: 0x04002441 RID: 9281
		internal bool isInitObjectInfo = true;

		// Token: 0x04002442 RID: 9282
		internal ObjectReader objectReader;

		// Token: 0x04002443 RID: 9283
		internal int objectId;

		// Token: 0x04002444 RID: 9284
		internal BinaryAssemblyInfo assemblyInfo;
	}
}
