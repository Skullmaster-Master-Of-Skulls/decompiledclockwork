using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using WCFExtras.Utils;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x02000020 RID: 32
	internal class XmlCommentsDataSurrogate : IDataContractSurrogate
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005EA0 File Offset: 0x000040A0
		public XmlCommentsDataSurrogate(IDataContractSurrogate prevSurrogate)
		{
			this.prevSurrogate = prevSurrogate;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005EB2 File Offset: 0x000040B2
		public XmlCommentsDataSurrogate(IDataContractSurrogate prevSurrogate, XmlCommentFormat format) : this(prevSurrogate)
		{
			this.format = format;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005EC8 File Offset: 0x000040C8
		object IDataContractSurrogate.GetCustomDataToExport(Type clrType, Type dataContractType)
		{
			if (dataContractType.IsGenericType && dataContractType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				dataContractType = dataContractType.GetGenericArguments()[0];
			}
			XmlDocument xmlDocument = XmlCommentsUtils.LoadXmlComments(dataContractType);
			if (xmlDocument != null)
			{
				if (dataContractType.IsEnum)
				{
					EnumAnnotation enumAnnotation = new EnumAnnotation();
					string formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, dataContractType, this.format);
					if (formattedComment != null)
					{
						enumAnnotation.EnumText = formattedComment;
					}
					Dictionary<string, MemberInfo> enumMembers = ReflectionUtils.GetEnumMembers(dataContractType);
					foreach (KeyValuePair<string, MemberInfo> keyValuePair in enumMembers)
					{
						formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, keyValuePair.Value, this.format);
						if (formattedComment != null)
						{
							enumAnnotation.Members.Add(keyValuePair.Key, formattedComment);
						}
					}
					if (enumAnnotation.EnumText != null || enumAnnotation.Members.Count > 0)
					{
						return enumAnnotation;
					}
				}
				else
				{
					string formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, dataContractType, this.format);
					if (formattedComment != null)
					{
						return new Annotation(formattedComment);
					}
				}
			}
			return null;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000602C File Offset: 0x0000422C
		object IDataContractSurrogate.GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
		{
			XmlDocument xmlDocument = XmlCommentsUtils.LoadXmlComments(memberInfo.DeclaringType);
			if (xmlDocument != null)
			{
				string formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, memberInfo, this.format);
				if (formattedComment != null)
				{
					return new Annotation(formattedComment);
				}
			}
			return null;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006078 File Offset: 0x00004278
		Type IDataContractSurrogate.GetDataContractType(Type type)
		{
			Type result;
			if (this.prevSurrogate != null)
			{
				result = this.prevSurrogate.GetDataContractType(type);
			}
			else
			{
				result = type;
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000060A8 File Offset: 0x000042A8
		object IDataContractSurrogate.GetDeserializedObject(object obj, Type targetType)
		{
			object result;
			if (this.prevSurrogate != null)
			{
				result = this.prevSurrogate.GetDeserializedObject(obj, targetType);
			}
			else
			{
				result = obj;
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000060D8 File Offset: 0x000042D8
		void IDataContractSurrogate.GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
			if (this.prevSurrogate != null)
			{
				this.prevSurrogate.GetKnownCustomDataTypes(customDataTypes);
			}
			customDataTypes.Add(typeof(Annotation));
			customDataTypes.Add(typeof(EnumAnnotation));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006124 File Offset: 0x00004324
		object IDataContractSurrogate.GetObjectToSerialize(object obj, Type targetType)
		{
			object result;
			if (this.prevSurrogate != null)
			{
				result = this.prevSurrogate.GetObjectToSerialize(obj, targetType);
			}
			else
			{
				result = obj;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006154 File Offset: 0x00004354
		Type IDataContractSurrogate.GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			Type result;
			if (this.prevSurrogate != null)
			{
				result = this.prevSurrogate.GetReferencedTypeOnImport(typeName, typeNamespace, customData);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006188 File Offset: 0x00004388
		CodeTypeDeclaration IDataContractSurrogate.ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			CodeTypeDeclaration result;
			if (this.prevSurrogate != null)
			{
				result = this.prevSurrogate.ProcessImportedType(typeDeclaration, compileUnit);
			}
			else
			{
				result = typeDeclaration;
			}
			return result;
		}

		// Token: 0x0400002E RID: 46
		private IDataContractSurrogate prevSurrogate;

		// Token: 0x0400002F RID: 47
		private XmlCommentFormat format;
	}
}
