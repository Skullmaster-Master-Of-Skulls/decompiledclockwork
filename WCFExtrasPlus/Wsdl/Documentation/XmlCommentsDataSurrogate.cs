using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000016 RID: 22
	internal class XmlCommentsDataSurrogate : IDataContractSurrogate
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00004418 File Offset: 0x00002618
		public XmlCommentsDataSurrogate(IDataContractSurrogate prevSurrogate)
		{
			this.prevSurrogate = prevSurrogate;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004427 File Offset: 0x00002627
		public XmlCommentsDataSurrogate(IDataContractSurrogate prevSurrogate, XmlCommentFormat format) : this(prevSurrogate)
		{
			this.format = format;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004438 File Offset: 0x00002638
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
					string formattedComment2 = XmlCommentsUtils.GetFormattedComment(xmlDocument, dataContractType, this.format);
					if (formattedComment2 != null)
					{
						return new Annotation(formattedComment2);
					}
				}
			}
			return null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004548 File Offset: 0x00002748
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

		// Token: 0x0600007B RID: 123 RVA: 0x0000457D File Offset: 0x0000277D
		Type IDataContractSurrogate.GetDataContractType(Type type)
		{
			if (this.prevSurrogate != null)
			{
				return this.prevSurrogate.GetDataContractType(type);
			}
			return type;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004595 File Offset: 0x00002795
		object IDataContractSurrogate.GetDeserializedObject(object obj, Type targetType)
		{
			if (this.prevSurrogate != null)
			{
				return this.prevSurrogate.GetDeserializedObject(obj, targetType);
			}
			return obj;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000045AE File Offset: 0x000027AE
		void IDataContractSurrogate.GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
			if (this.prevSurrogate != null)
			{
				this.prevSurrogate.GetKnownCustomDataTypes(customDataTypes);
			}
			customDataTypes.Add(typeof(Annotation));
			customDataTypes.Add(typeof(EnumAnnotation));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000045E4 File Offset: 0x000027E4
		object IDataContractSurrogate.GetObjectToSerialize(object obj, Type targetType)
		{
			if (this.prevSurrogate != null)
			{
				return this.prevSurrogate.GetObjectToSerialize(obj, targetType);
			}
			return obj;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000045FD File Offset: 0x000027FD
		Type IDataContractSurrogate.GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			if (this.prevSurrogate != null)
			{
				return this.prevSurrogate.GetReferencedTypeOnImport(typeName, typeNamespace, customData);
			}
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004617 File Offset: 0x00002817
		CodeTypeDeclaration IDataContractSurrogate.ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			if (this.prevSurrogate != null)
			{
				return this.prevSurrogate.ProcessImportedType(typeDeclaration, compileUnit);
			}
			return typeDeclaration;
		}

		// Token: 0x0400001D RID: 29
		private IDataContractSurrogate prevSurrogate;

		// Token: 0x0400001E RID: 30
		private XmlCommentFormat format;
	}
}
