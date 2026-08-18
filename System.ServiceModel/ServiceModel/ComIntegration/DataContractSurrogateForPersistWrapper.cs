using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023C RID: 572
	internal class DataContractSurrogateForPersistWrapper : IDataContractSurrogate
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x0003E6A0 File Offset: 0x0003C8A0
		public DataContractSurrogateForPersistWrapper(Guid[] allowedClasses)
		{
			this.allowedClasses = allowedClasses;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
		private bool IsAllowedClass(Guid clsid)
		{
			foreach (Guid b in this.allowedClasses)
			{
				if (clsid == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003E6E6 File Offset: 0x0003C8E6
		public Type GetDataContractType(Type type)
		{
			if (type.IsInterface)
			{
				return typeof(PersistStreamTypeWrapper);
			}
			return type;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003E6FC File Offset: 0x0003C8FC
		public object GetObjectToSerialize(object obj, Type targetType)
		{
			if (!(targetType == typeof(object)) && !targetType.IsInterface)
			{
				return obj;
			}
			IPersistStream persistStream = obj as IPersistStream;
			if (persistStream != null)
			{
				PersistStreamTypeWrapper persistStreamTypeWrapper = new PersistStreamTypeWrapper();
				persistStream.GetClassID(out persistStreamTypeWrapper.clsid);
				persistStreamTypeWrapper.dataStream = PersistHelper.PersistIPersistStreamToByteArray(persistStream);
				return persistStreamTypeWrapper;
			}
			if (targetType.IsInterface)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TargetObjectDoesNotSupportIPersistStream")));
			}
			return obj;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003E774 File Offset: 0x0003C974
		public object GetDeserializedObject(object obj, Type targetType)
		{
			if (targetType == typeof(object) || targetType.IsInterface)
			{
				PersistStreamTypeWrapper persistStreamTypeWrapper = obj as PersistStreamTypeWrapper;
				if (persistStreamTypeWrapper != null)
				{
					if (this.IsAllowedClass(persistStreamTypeWrapper.clsid))
					{
						return PersistHelper.ActivateAndLoadFromByteStream(persistStreamTypeWrapper.clsid, persistStreamTypeWrapper.dataStream);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NotAllowedPersistableCLSID", new object[]
					{
						persistStreamTypeWrapper.clsid.ToString("B")
					})));
				}
				else if (targetType.IsInterface)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TargetTypeIsAnIntefaceButCorrespoindingTypeIsNotPersistStreamTypeWrapper")));
				}
			}
			return obj;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003E81B File Offset: 0x0003CA1B
		public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
		{
			return null;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003E81E File Offset: 0x0003CA1E
		public object GetCustomDataToExport(Type clrType, Type dataContractType)
		{
			return null;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003E821 File Offset: 0x0003CA21
		public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
			customDataTypes.Add(typeof(PersistStreamTypeWrapper));
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003E833 File Offset: 0x0003CA33
		public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			return null;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003E836 File Offset: 0x0003CA36
		public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			return null;
		}

		// Token: 0x04001896 RID: 6294
		private Guid[] allowedClasses;
	}
}
