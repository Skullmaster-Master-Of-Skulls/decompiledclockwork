using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace WCFExtras.Utils
{
	// Token: 0x0200001D RID: 29
	internal static class CodeDomUtils
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00004D98 File Offset: 0x00002F98
		public static Dictionary<string, CodeTypeMember> EnumerareCodeMembers(CodeCompileUnit unit)
		{
			Dictionary<string, CodeTypeMember> dictionary = new Dictionary<string, CodeTypeMember>();
			foreach (object obj in unit.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				foreach (object obj2 in codeNamespace.Types)
				{
					CodeTypeDeclaration member = (CodeTypeDeclaration)obj2;
					CodeDomUtils.EnumerareCodeMembers(member, null, dictionary);
				}
			}
			return dictionary;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004E70 File Offset: 0x00003070
		private static void EnumerareCodeMembers(CodeTypeMember member, CodeDomUtils.QualifiedName parentName, Dictionary<string, CodeTypeMember> members)
		{
			CodeDomUtils.QualifiedName uniqueName = CodeDomUtils.GetUniqueName(member, parentName);
			members[uniqueName.ToString()] = member;
			CodeTypeDeclaration codeTypeDeclaration = member as CodeTypeDeclaration;
			if (codeTypeDeclaration != null)
			{
				foreach (object obj in codeTypeDeclaration.Members)
				{
					CodeTypeMember member2 = (CodeTypeMember)obj;
					CodeDomUtils.EnumerareCodeMembers(member2, uniqueName, members);
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004F08 File Offset: 0x00003108
		private static CodeDomUtils.QualifiedName GetUniqueName(CodeTypeMember member, CodeDomUtils.QualifiedName parentName)
		{
			CodeDomUtils.QualifiedName result;
			if (member is CodeTypeDeclaration)
			{
				result = new CodeDomUtils.QualifiedName(CodeDomUtils.GetUniqueName((CodeTypeDeclaration)member), null);
			}
			else
			{
				result = new CodeDomUtils.QualifiedName(CodeDomUtils.GetUniqueName(member), parentName);
			}
			return result;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004F4C File Offset: 0x0000314C
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
					CodeAttributeArgument codeAttributeArgument = codeAttributeDeclaration.Arguments.Find("Value");
					if (codeAttributeArgument != null)
					{
						name = codeAttributeArgument.GetStringValue();
					}
				}
			}
			return new CodeDomUtils.QualifiedName(null, name);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000500C File Offset: 0x0000320C
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000050DC File Offset: 0x000032DC
		public static CodeAttributeDeclaration Find(this CodeAttributeDeclarationCollection attributes, params string[] attributeTypes)
		{
			return Enumerable.FirstOrDefault<CodeAttributeDeclaration>(attributes.Cast<CodeAttributeDeclaration>(), (CodeAttributeDeclaration attribute) => attributeTypes.Contains(attribute.AttributeType.BaseType));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005140 File Offset: 0x00003340
		public static CodeAttributeArgument Find(this CodeAttributeArgumentCollection attributes, string argName)
		{
			return Enumerable.FirstOrDefault<CodeAttributeArgument>(attributes.Cast<CodeAttributeArgument>(), (CodeAttributeArgument arg) => arg.Name == argName);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005178 File Offset: 0x00003378
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

		// Token: 0x060000BA RID: 186 RVA: 0x000051C0 File Offset: 0x000033C0
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

		// Token: 0x060000BB RID: 187 RVA: 0x00005208 File Offset: 0x00003408
		public static string GetStringValue(this CodeAttributeArgument argument)
		{
			return (argument.Value as CodePrimitiveExpression).Value.ToString();
		}

		// Token: 0x0200001E RID: 30
		private class QualifiedName
		{
			// Token: 0x060000BC RID: 188 RVA: 0x0000522F File Offset: 0x0000342F
			public QualifiedName(string Namespace, string Name)
			{
				this.Namespace = Namespace;
				this.Name = Name;
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00005248 File Offset: 0x00003448
			public QualifiedName(CodeDomUtils.QualifiedName name, CodeDomUtils.QualifiedName parent)
			{
				if (parent != null)
				{
					this.Namespace = (name.Namespace ?? parent.Namespace);
					this.Name = parent.Name + "." + name.Name;
				}
				else
				{
					this.Namespace = name.Namespace;
					this.Name = name.Name;
				}
			}

			// Token: 0x060000BE RID: 190 RVA: 0x000052B8 File Offset: 0x000034B8
			public override string ToString()
			{
				return this.Namespace + ":" + this.Name;
			}

			// Token: 0x0400002C RID: 44
			public string Namespace;

			// Token: 0x0400002D RID: 45
			public string Name;
		}
	}
}
