using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x0200017B RID: 379
	internal class SourceInfo
	{
		// Token: 0x06001927 RID: 6439 RVA: 0x000705FF File Offset: 0x0006E7FF
		public SourceInfo(string source, string arg, MemberInfo memberInfo, Type type, CodeGenerator ilg)
		{
			this.Source = source;
			this.Arg = (arg ?? source);
			this.MemberInfo = memberInfo;
			this.Type = type;
			this.ILG = ilg;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00070634 File Offset: 0x0006E834
		public SourceInfo CastTo(TypeDesc td)
		{
			return new SourceInfo(string.Concat(new string[]
			{
				"((",
				td.CSharpName,
				")",
				this.Source,
				")"
			}), this.Arg, this.MemberInfo, td.Type, this.ILG);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00070693 File Offset: 0x0006E893
		public void LoadAddress(Type elementType)
		{
			this.InternalLoad(elementType, true);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0007069D File Offset: 0x0006E89D
		public void Load(Type elementType)
		{
			this.InternalLoad(elementType, false);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000706A8 File Offset: 0x0006E8A8
		private void InternalLoad(Type elementType, bool asAddress = false)
		{
			Match match = SourceInfo.regex.Match(this.Arg);
			if (match.Success)
			{
				object variable = this.ILG.GetVariable(match.Groups["a"].Value);
				Type variableType = this.ILG.GetVariableType(variable);
				object variable2 = this.ILG.GetVariable(match.Groups["ia"].Value);
				if (variableType.IsArray)
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					Type elementType2 = variableType.GetElementType();
					if (CodeGenerator.IsNullableGenericType(elementType2))
					{
						this.ILG.Ldelema(elementType2);
						this.ConvertNullableValue(elementType2, elementType);
						return;
					}
					if (elementType2.IsValueType)
					{
						this.ILG.Ldelema(elementType2);
						if (!asAddress)
						{
							this.ILG.Ldobj(elementType2);
						}
					}
					else
					{
						this.ILG.Ldelem(elementType2);
					}
					if (elementType != null)
					{
						this.ILG.ConvertValue(elementType2, elementType);
						return;
					}
				}
				else
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					MethodInfo methodInfo = variableType.GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(int)
					}, null);
					if (methodInfo == null && typeof(IList).IsAssignableFrom(variableType))
					{
						methodInfo = SourceInfo.iListGetItemMethod.Value;
					}
					this.ILG.Call(methodInfo);
					Type returnType = methodInfo.ReturnType;
					if (CodeGenerator.IsNullableGenericType(returnType))
					{
						LocalBuilder tempLocal = this.ILG.GetTempLocal(returnType);
						this.ILG.Stloc(tempLocal);
						this.ILG.Ldloca(tempLocal);
						this.ConvertNullableValue(returnType, elementType);
						return;
					}
					if (elementType != null && !returnType.IsAssignableFrom(elementType) && !elementType.IsAssignableFrom(returnType))
					{
						throw new CodeGeneratorConversionException(returnType, elementType, asAddress, "IsNotAssignableFrom");
					}
					this.Convert(returnType, elementType, asAddress);
					return;
				}
			}
			else
			{
				if (this.Source == "null")
				{
					this.ILG.Load(null);
					return;
				}
				Type type;
				if (this.Arg.StartsWith("o.@", StringComparison.Ordinal) || this.MemberInfo != null)
				{
					object variable3 = this.ILG.GetVariable(this.Arg.StartsWith("o.@", StringComparison.Ordinal) ? "o" : this.Arg);
					type = this.ILG.GetVariableType(variable3);
					if (type.IsValueType)
					{
						this.ILG.LoadAddress(variable3);
					}
					else
					{
						this.ILG.Load(variable3);
					}
				}
				else
				{
					object variable3 = this.ILG.GetVariable(this.Arg);
					type = this.ILG.GetVariableType(variable3);
					if (CodeGenerator.IsNullableGenericType(type) && type.GetGenericArguments()[0] == elementType)
					{
						this.ILG.LoadAddress(variable3);
						this.ConvertNullableValue(type, elementType);
					}
					else if (asAddress)
					{
						this.ILG.LoadAddress(variable3);
					}
					else
					{
						this.ILG.Load(variable3);
					}
				}
				if (this.MemberInfo != null)
				{
					Type type2 = (this.MemberInfo is FieldInfo) ? ((FieldInfo)this.MemberInfo).FieldType : ((PropertyInfo)this.MemberInfo).PropertyType;
					if (CodeGenerator.IsNullableGenericType(type2))
					{
						this.ILG.LoadMemberAddress(this.MemberInfo);
						this.ConvertNullableValue(type2, elementType);
						return;
					}
					this.ILG.LoadMember(this.MemberInfo);
					this.Convert(type2, elementType, asAddress);
					return;
				}
				else
				{
					match = SourceInfo.regex2.Match(this.Source);
					if (match.Success)
					{
						if (asAddress)
						{
							this.ILG.ConvertAddress(type, this.Type);
						}
						else
						{
							this.ILG.ConvertValue(type, this.Type);
						}
						type = this.Type;
					}
					this.Convert(type, elementType, asAddress);
				}
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00070A9B File Offset: 0x0006EC9B
		private void Convert(Type sourceType, Type targetType, bool asAddress)
		{
			if (targetType != null)
			{
				if (asAddress)
				{
					this.ILG.ConvertAddress(sourceType, targetType);
					return;
				}
				this.ILG.ConvertValue(sourceType, targetType);
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00070AC4 File Offset: 0x0006ECC4
		private void ConvertNullableValue(Type nullableType, Type targetType)
		{
			if (targetType != nullableType)
			{
				MethodInfo method = nullableType.GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ILG.Call(method);
				if (targetType != null)
				{
					this.ILG.ConvertValue(method.ReturnType, targetType);
				}
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00070B19 File Offset: 0x0006ED19
		public static implicit operator string(SourceInfo source)
		{
			return source.Source;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00070B21 File Offset: 0x0006ED21
		public static bool operator !=(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return !a.Equals(b);
			}
			return b != null;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00070B35 File Offset: 0x0006ED35
		public static bool operator ==(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return a.Equals(b);
			}
			return b == null;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00070B48 File Offset: 0x0006ED48
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.Source == null;
			}
			SourceInfo sourceInfo = obj as SourceInfo;
			return sourceInfo != null && this.Source == sourceInfo.Source;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00070B85 File Offset: 0x0006ED85
		public override int GetHashCode()
		{
			if (this.Source != null)
			{
				return this.Source.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000B65 RID: 2917
		private static Regex regex = new Regex("([(][(](?<t>[^)]+)[)])?(?<a>[^[]+)[[](?<ia>.+)[]][)]?");

		// Token: 0x04000B66 RID: 2918
		private static Regex regex2 = new Regex("[(][(](?<cast>[^)]+)[)](?<arg>[^)]+)[)]");

		// Token: 0x04000B67 RID: 2919
		private static readonly Lazy<MethodInfo> iListGetItemMethod = new Lazy<MethodInfo>(() => typeof(IList).GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
		{
			typeof(int)
		}, null));

		// Token: 0x04000B68 RID: 2920
		public string Source;

		// Token: 0x04000B69 RID: 2921
		public readonly string Arg;

		// Token: 0x04000B6A RID: 2922
		public readonly MemberInfo MemberInfo;

		// Token: 0x04000B6B RID: 2923
		public readonly Type Type;

		// Token: 0x04000B6C RID: 2924
		public readonly CodeGenerator ILG;
	}
}
