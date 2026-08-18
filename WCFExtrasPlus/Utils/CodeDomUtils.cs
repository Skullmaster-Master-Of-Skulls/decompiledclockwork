using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace WCFExtrasPlus.Utils
{
	// Token: 0x0200000D RID: 13
	internal static class CodeDomUtils
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002B64 File Offset: 0x00000D64
		public static Dictionary<string, CodeTypeMember> EnumerateCodeMembers(CodeCompileUnit unit)
		{
			Dictionary<string, CodeTypeMember> dictionary = new Dictionary<string, CodeTypeMember>();
			foreach (object obj in unit.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				foreach (object obj2 in codeNamespace.Types)
				{
					CodeTypeDeclaration member = (CodeTypeDeclaration)obj2;
					CodeDomUtils.EnumerateCodeMembers(member, null, dictionary);
				}
			}
			return dictionary;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C10 File Offset: 0x00000E10
		private static void EnumerateCodeMembers(CodeTypeMember member, CodeDomUtils.QualifiedName parentName, Dictionary<string, CodeTypeMember> members)
		{
			CodeDomUtils.QualifiedName uniqueName = CodeDomUtils.GetUniqueName(member, parentName);
			members[uniqueName.ToString()] = member;
			CodeTypeDeclaration codeTypeDeclaration = member as CodeTypeDeclaration;
			if (codeTypeDeclaration != null)
			{
				foreach (object obj in codeTypeDeclaration.Members)
				{
					CodeTypeMember member2 = (CodeTypeMember)obj;
					CodeDomUtils.EnumerateCodeMembers(member2, uniqueName, members);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C8C File Offset: 0x00000E8C
		private static CodeDomUtils.QualifiedName GetUniqueName(CodeTypeMember member, CodeDomUtils.QualifiedName parentName)
		{
			if (member is CodeTypeDeclaration)
			{
				return new CodeDomUtils.QualifiedName(CodeDomUtils.GetUniqueName((CodeTypeDeclaration)member), null);
			}
			return new CodeDomUtils.QualifiedName(CodeDomUtils.GetUniqueName(member), parentName);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private static CodeDomUtils.QualifiedName GetUniqueName(CodeTypeMember codeMember)
		{
			string name = codeMember.Name;
			CodeAttributeDeclaration codeAttributeDeclaration = codeMember.CustomAttributes.Find(new string[]
			{
				"System.Runtime.Serialization.DataMemberAttribute"
			});
			if (codeAttributeDeclaration != null)
			{
				CodeAttributeArgument codeAttributeArgument = codeAttributeDeclaration.Arguments.Find("Name");
				if (codeAttributeArgument != null)
				{
					name = codeAttributeArgument.GetStringValue();
				}
			}
			else
			{
				codeAttributeDeclaration = codeMember.CustomAttributes.Find(new string[]
				{
					"System.Runtime.Serialization.EnumMemberAttribute"
				});
				if (codeAttributeDeclaration != null)
				{
					CodeAttributeArgument codeAttributeArgument2 = codeAttributeDeclaration.Arguments.Find("Value");
					if (codeAttributeArgument2 != null)
					{
						name = codeAttributeArgument2.GetStringValue();
					}
				}
			}
			return new CodeDomUtils.QualifiedName(null, name);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D4C File Offset: 0x00000F4C
		private static CodeDomUtils.QualifiedName GetUniqueName(CodeTypeDeclaration codeType)
		{
			string name = codeType.Name;
			string @namespace = null;
			CodeAttributeDeclaration codeAttributeDeclaration = codeType.CustomAttributes.Find(new string[]
			{
				"System.Runtime.Serialization.DataContractAttribute",
				"System.Runtime.Serialization.CollectionDataContractAttribute"
			});
			if (codeAttributeDeclaration != null)
			{
				CodeAttributeArgument codeAttributeArgument = codeAttributeDeclaration.Arguments.Find("Name");
				if (codeAttributeArgument != null)
				{
					name = codeAttributeArgument.GetStringValue();
				}
				CodeAttributeArgument codeAttributeArgument2 = codeAttributeDeclaration.Arguments.Find("Namespace");
				if (codeAttributeArgument2 != null)
				{
					@namespace = codeAttributeArgument2.GetStringValue();
				}
			}
			return new CodeDomUtils.QualifiedName(@namespace, name);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public static CodeAttributeDeclaration Find(this CodeAttributeDeclarationCollection attributes, params string[] attributeTypes)
		{
			return attributes.Cast<CodeAttributeDeclaration>().FirstOrDefault((CodeAttributeDeclaration attribute) => attributeTypes.Contains(attribute.AttributeType.BaseType));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E3C File Offset: 0x0000103C
		public static CodeAttributeArgument Find(this CodeAttributeArgumentCollection attributes, string argName)
		{
			return attributes.Cast<CodeAttributeArgument>().FirstOrDefault((CodeAttributeArgument arg) => arg.Name == argName);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E70 File Offset: 0x00001070
		public static int IndexOf(this CodeTypeMemberCollection members, string memberName)
		{
			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].Name == memberName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static int IndexOf(this CodeTypeReferenceCollection references, string typeName)
		{
			for (int i = 0; i < references.Count; i++)
			{
				if (references[i].BaseType == typeName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EDD File Offset: 0x000010DD
		public static string GetStringValue(this CodeAttributeArgument argument)
		{
			return (argument.Value as CodePrimitiveExpression).Value.ToString();
		}

		// Token: 0x0200000E RID: 14
		private class QualifiedName
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00002EF4 File Offset: 0x000010F4
			public QualifiedName(string Namespace, string Name)
			{
				this.Namespace = Namespace;
				this.Name = Name;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002F0C File Offset: 0x0000110C
			public QualifiedName(CodeDomUtils.QualifiedName name, CodeDomUtils.QualifiedName parent)
			{
				if (parent != null)
				{
					this.Namespace = (name.Namespace ?? parent.Namespace);
					this.Name = parent.Name + "." + name.Name;
					return;
				}
				this.Namespace = name.Namespace;
				this.Name = name.Name;
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00002F6D File Offset: 0x0000116D
			public override string ToString()
			{
				return this.Namespace + ":" + this.Name;
			}

			// Token: 0x04000012 RID: 18
			public string Namespace;

			// Token: 0x04000013 RID: 19
			public string Name;
		}
	}
}
