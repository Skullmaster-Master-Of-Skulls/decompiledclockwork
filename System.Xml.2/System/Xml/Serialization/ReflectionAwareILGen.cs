using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020001B6 RID: 438
	internal class ReflectionAwareILGen
	{
		// Token: 0x06001E5D RID: 7773 RVA: 0x000A71BD File Offset: 0x000A53BD
		internal ReflectionAwareILGen()
		{
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000A71C8 File Offset: 0x000A53C8
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				TypeDesc typeDesc = scope.GetTypeDesc(type);
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000A7224 File Offset: 0x000A5424
		internal void ILGenForEnumLongValue(CodeGenerator ilg, string variable)
		{
			ArgBuilder arg = ilg.GetArg(variable);
			ilg.Ldarg(arg);
			ilg.ConvertValue(arg.ArgType, typeof(long));
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000A7256 File Offset: 0x000A5456
		internal string GetStringForTypeof(string typeFullName)
		{
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000A7268 File Offset: 0x000A5468
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			return obj + ".@" + memberName;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000A7276 File Offset: 0x000A5476
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return this.GetSourceForMember(obj, member, member.MemberInfo, typeDesc, ilg);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000A7289 File Offset: 0x000A5489
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, MemberInfo memberInfo, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return new SourceInfo(this.GetStringForMember(obj, member.Name, typeDesc), obj, memberInfo, member.TypeDesc.Type, ilg);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000A72AE File Offset: 0x000A54AE
		internal void ILGenForEnumMember(CodeGenerator ilg, Type type, string memberName)
		{
			ilg.Ldc(Enum.Parse(type, memberName, false));
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000A72BE File Offset: 0x000A54BE
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			return arrayName + "[" + subscript + "]";
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000A72D1 File Offset: 0x000A54D1
		internal string GetStringForMethod(string obj, string typeFullName, string memberName)
		{
			return obj + "." + memberName + "(";
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000A72E4 File Offset: 0x000A54E4
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, bool ctorInaccessible, bool cast)
		{
			if (ctorInaccessible)
			{
				this.ILGenForCreateInstance(ilg, type, cast ? type : null, ctorInaccessible);
				return;
			}
			ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			if (constructor != null)
			{
				ilg.New(constructor);
				return;
			}
			LocalBuilder tempLocal = ilg.GetTempLocal(type);
			ilg.Ldloca(tempLocal);
			ilg.InitObj(type);
			ilg.Ldloc(tempLocal);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000A7348 File Offset: 0x000A5548
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, Type cast, bool nonPublic)
		{
			if (type == typeof(DBNull))
			{
				FieldInfo field = typeof(DBNull).GetField("Value", CodeGenerator.StaticBindingFlags);
				ilg.LoadMember(field);
				return;
			}
			if (type.FullName == "System.Xml.Linq.XElement")
			{
				Type type2 = type.Assembly.GetType("System.Xml.Linq.XName");
				if (type2 != null)
				{
					MethodInfo method = type2.GetMethod("op_Implicit", CodeGenerator.StaticBindingFlags, null, new Type[]
					{
						typeof(string)
					}, null);
					ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						type2
					}, null);
					if (method != null && constructor != null)
					{
						ilg.Ldstr("default");
						ilg.Call(method);
						ilg.New(constructor);
						return;
					}
				}
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			MethodInfo method2 = typeof(Activator).GetMethod("CreateInstance", CodeGenerator.StaticBindingFlags, null, new Type[]
			{
				typeof(Type),
				typeof(BindingFlags),
				typeof(Binder),
				typeof(object[]),
				typeof(CultureInfo)
			}, null);
			ilg.Ldc(type);
			ilg.Load((int)bindingFlags);
			ilg.Load(null);
			ilg.NewArray(typeof(object), 0);
			ilg.Load(null);
			ilg.Call(method2);
			if (cast != null)
			{
				ilg.ConvertValue(method2.ReturnType, cast);
			}
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000A74F0 File Offset: 0x000A56F0
		internal void WriteLocalDecl(string variableName, SourceInfo initValue)
		{
			Type type = initValue.Type;
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue.Source != null)
			{
				if (initValue == "null")
				{
					initValue.ILG.Load(null);
				}
				else if (initValue.Arg.StartsWith("o.@", StringComparison.Ordinal))
				{
					initValue.ILG.LoadMember(initValue.ILG.GetLocal("o"), initValue.MemberInfo);
				}
				else if (initValue.Source.EndsWith("]", StringComparison.Ordinal))
				{
					initValue.Load(initValue.Type);
				}
				else if (initValue.Source == "fixup.Source" || initValue.Source == "e.Current")
				{
					string[] array = initValue.Source.Split(new char[]
					{
						'.'
					});
					object variable = initValue.ILG.GetVariable(array[0]);
					PropertyInfo property = initValue.ILG.GetVariableType(variable).GetProperty(array[1]);
					initValue.ILG.LoadMember(variable, property);
					initValue.ILG.ConvertValue(property.PropertyType, localBuilder.LocalType);
				}
				else
				{
					object variable2 = initValue.ILG.GetVariable(initValue.Arg);
					initValue.ILG.Load(variable2);
					initValue.ILG.ConvertValue(initValue.ILG.GetVariableType(variable2), localBuilder.LocalType);
				}
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000A7674 File Offset: 0x000A5874
		internal void WriteCreateInstance(string source, bool ctorInaccessible, Type type, CodeGenerator ilg)
		{
			LocalBuilder local = ilg.DeclareOrGetLocal(type, source);
			this.ILGenForCreateInstance(ilg, type, ctorInaccessible, ctorInaccessible);
			ilg.Stloc(local);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000A769E File Offset: 0x000A589E
		internal void WriteInstanceOf(SourceInfo source, Type type, CodeGenerator ilg)
		{
			source.Load(typeof(object));
			ilg.IsInst(type);
			ilg.Load(null);
			ilg.Cne();
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000A76C4 File Offset: 0x000A58C4
		internal void WriteArrayLocalDecl(string typeName, string variableName, SourceInfo initValue, TypeDesc arrayTypeDesc)
		{
			Type type = (typeName == arrayTypeDesc.CSharpName) ? arrayTypeDesc.Type : arrayTypeDesc.Type.MakeArrayType();
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue != null)
			{
				initValue.Load(localBuilder.LocalType);
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000A7725 File Offset: 0x000A5925
		internal void WriteTypeCompare(string variable, Type type, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(type);
			ilg.Ceq();
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000A7745 File Offset: 0x000A5945
		internal void WriteArrayTypeCompare(string variable, Type arrayType, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(arrayType);
			ilg.Ceq();
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000A7768 File Offset: 0x000A5968
		internal static string GetQuotedCSharpString(IndentedWriter notUsed, string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("@\"");
			stringBuilder.Append(ReflectionAwareILGen.GetCSharpString(value));
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000A77AC File Offset: 0x000A59AC
		internal static string GetCSharpString(string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						stringBuilder.Append("\\r");
					}
					else if (c == '\n')
					{
						stringBuilder.Append("\\n");
					}
					else if (c == '\t')
					{
						stringBuilder.Append("\\t");
					}
					else
					{
						byte b = (byte)c;
						stringBuilder.Append("\\x");
						stringBuilder.Append("0123456789ABCDEF"[b >> 4]);
						stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000CD7 RID: 3287
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04000CD8 RID: 3288
		private const string arrayMemberKey = "0";
	}
}
