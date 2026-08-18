using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x02000130 RID: 304
	internal class CodeGenerator
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x00061E48 File Offset: 0x00060048
		internal static bool IsValidLanguageIndependentIdentifier(string ident)
		{
			return CodeGenerator.IsValidLanguageIndependentIdentifier(ident);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00061E50 File Offset: 0x00060050
		internal static void ValidateIdentifiers(CodeObject e)
		{
			CodeGenerator.ValidateIdentifiers(e);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00061E58 File Offset: 0x00060058
		internal CodeGenerator(TypeBuilder typeBuilder)
		{
			this.typeBuilder = typeBuilder;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00061E8B File Offset: 0x0006008B
		internal static bool IsNullableGenericType(Type type)
		{
			return type.Name == "Nullable`1";
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00061E9D File Offset: 0x0006009D
		internal static void AssertHasInterface(Type type, Type iType)
		{
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00061EA0 File Offset: 0x000600A0
		internal void BeginMethod(Type returnType, string methodName, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = this.typeBuilder.DefineMethod(methodName, methodAttributes, returnType, argTypes);
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00061EEF File Offset: 0x000600EF
		internal void BeginMethod(Type returnType, MethodBuilderInfo methodBuilderInfo, Type[] argTypes, string[] argNames, MethodAttributes methodAttributes)
		{
			this.methodBuilder = methodBuilderInfo.MethodBuilder;
			this.ilGen = this.methodBuilder.GetILGenerator();
			this.InitILGeneration(argTypes, argNames, (this.methodBuilder.Attributes & MethodAttributes.Static) == MethodAttributes.Static);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00061F2C File Offset: 0x0006012C
		private void InitILGeneration(Type[] argTypes, string[] argNames, bool isStatic)
		{
			this.methodEndLabel = this.ilGen.DefineLabel();
			this.retLabel = this.ilGen.DefineLabel();
			this.blockStack = new Stack();
			this.whileStack = new Stack();
			this.currentScope = new LocalScope();
			this.freeLocals = new Dictionary<Tuple<Type, string>, Queue<LocalBuilder>>();
			this.argList = new Dictionary<string, ArgBuilder>();
			if (!isStatic)
			{
				this.argList.Add("this", new ArgBuilder("this", 0, this.typeBuilder.BaseType));
			}
			for (int i = 0; i < argTypes.Length; i++)
			{
				ArgBuilder argBuilder = new ArgBuilder(argNames[i], this.argList.Count, argTypes[i]);
				this.argList.Add(argBuilder.Name, argBuilder);
				this.methodBuilder.DefineParameter(argBuilder.Index, ParameterAttributes.None, argBuilder.Name);
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0006200C File Offset: 0x0006020C
		internal MethodBuilder EndMethod()
		{
			this.MarkLabel(this.methodEndLabel);
			this.Ret();
			MethodBuilder result = this.methodBuilder;
			this.methodBuilder = null;
			this.ilGen = null;
			this.freeLocals = null;
			this.blockStack = null;
			this.whileStack = null;
			this.argList = null;
			this.currentScope = null;
			this.retLocal = null;
			return result;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0006206D File Offset: 0x0006026D
		internal MethodBuilder MethodBuilder
		{
			get
			{
				return this.methodBuilder;
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00062075 File Offset: 0x00060275
		internal static Exception NotSupported(string msg)
		{
			return new NotSupportedException(msg);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0006207D File Offset: 0x0006027D
		internal ArgBuilder GetArg(string name)
		{
			return this.argList[name];
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0006208B File Offset: 0x0006028B
		internal LocalBuilder GetLocal(string name)
		{
			return this.currentScope[name];
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00062099 File Offset: 0x00060299
		internal LocalBuilder ReturnLocal
		{
			get
			{
				if (this.retLocal == null)
				{
					this.retLocal = this.DeclareLocal(this.methodBuilder.ReturnType, "_ret");
				}
				return this.retLocal;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x000620C5 File Offset: 0x000602C5
		internal Label ReturnLabel
		{
			get
			{
				return this.retLabel;
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000620D0 File Offset: 0x000602D0
		internal LocalBuilder GetTempLocal(Type type)
		{
			LocalBuilder localBuilder;
			if (!this.TmpLocals.TryGetValue(type, out localBuilder))
			{
				localBuilder = this.DeclareLocal(type, "_tmp" + this.TmpLocals.Count.ToString());
				this.TmpLocals.Add(type, localBuilder);
			}
			return localBuilder;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00062120 File Offset: 0x00060320
		internal Type GetVariableType(object var)
		{
			if (var is ArgBuilder)
			{
				return ((ArgBuilder)var).ArgType;
			}
			if (var is LocalBuilder)
			{
				return ((LocalBuilder)var).LocalType;
			}
			return var.GetType();
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00062150 File Offset: 0x00060350
		internal object GetVariable(string name)
		{
			object result;
			if (this.TryGetVariable(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0006216C File Offset: 0x0006036C
		internal bool TryGetVariable(string name, out object variable)
		{
			LocalBuilder localBuilder;
			if (this.currentScope != null && this.currentScope.TryGetValue(name, out localBuilder))
			{
				variable = localBuilder;
				return true;
			}
			ArgBuilder argBuilder;
			if (this.argList != null && this.argList.TryGetValue(name, out argBuilder))
			{
				variable = argBuilder;
				return true;
			}
			int num;
			if (int.TryParse(name, out num))
			{
				variable = num;
				return true;
			}
			variable = null;
			return false;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000621CC File Offset: 0x000603CC
		internal void EnterScope()
		{
			LocalScope localScope = new LocalScope(this.currentScope);
			this.currentScope = localScope;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000621EC File Offset: 0x000603EC
		internal void ExitScope()
		{
			this.currentScope.AddToFreeLocals(this.freeLocals);
			this.currentScope = this.currentScope.parent;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00062210 File Offset: 0x00060410
		private bool TryDequeueLocal(Type type, string name, out LocalBuilder local)
		{
			Tuple<Type, string> key = new Tuple<Type, string>(type, name);
			Queue<LocalBuilder> queue;
			if (this.freeLocals.TryGetValue(key, out queue))
			{
				local = queue.Dequeue();
				if (queue.Count == 0)
				{
					this.freeLocals.Remove(key);
				}
				return true;
			}
			local = null;
			return false;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00062258 File Offset: 0x00060458
		internal LocalBuilder DeclareLocal(Type type, string name)
		{
			LocalBuilder localBuilder;
			if (!this.TryDequeueLocal(type, name, out localBuilder))
			{
				localBuilder = this.ilGen.DeclareLocal(type, false);
				if (DiagnosticsSwitches.KeepTempFiles.Enabled)
				{
					localBuilder.SetLocalSymInfo(name);
				}
			}
			this.currentScope[name] = localBuilder;
			return localBuilder;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000622A0 File Offset: 0x000604A0
		internal LocalBuilder DeclareOrGetLocal(Type type, string name)
		{
			LocalBuilder result;
			if (!this.currentScope.TryGetValue(name, out result))
			{
				result = this.DeclareLocal(type, name);
			}
			return result;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000622C8 File Offset: 0x000604C8
		internal object For(LocalBuilder local, object start, object end)
		{
			ForState forState = new ForState(local, this.DefineLabel(), this.DefineLabel(), end);
			if (forState.Index != null)
			{
				this.Load(start);
				this.Stloc(forState.Index);
				this.Br(forState.TestLabel);
			}
			this.MarkLabel(forState.BeginLabel);
			this.blockStack.Push(forState);
			return forState;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0006232C File Offset: 0x0006052C
		internal void EndFor()
		{
			object obj = this.blockStack.Pop();
			ForState forState = obj as ForState;
			if (forState.Index != null)
			{
				this.Ldloc(forState.Index);
				this.Ldc(1);
				this.Add();
				this.Stloc(forState.Index);
				this.MarkLabel(forState.TestLabel);
				this.Ldloc(forState.Index);
				this.Load(forState.End);
				Type variableType = this.GetVariableType(forState.End);
				if (variableType.IsArray)
				{
					this.Ldlen();
				}
				else
				{
					MethodInfo method = typeof(ICollection).GetMethod("get_Count", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
					this.Call(method);
				}
				this.Blt(forState.BeginLabel);
				return;
			}
			this.Br(forState.BeginLabel);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000623FE File Offset: 0x000605FE
		internal void If()
		{
			this.InternalIf(false);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00062407 File Offset: 0x00060607
		internal void IfNot()
		{
			this.InternalIf(true);
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00062410 File Offset: 0x00060610
		private OpCode GetBranchCode(Cmp cmp)
		{
			return CodeGenerator.BranchCodes[(int)cmp];
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00062420 File Offset: 0x00060620
		internal void If(Cmp cmpOp)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0006246F File Offset: 0x0006066F
		internal void If(object value1, Cmp cmpOp, object value2)
		{
			this.Load(value1);
			this.Load(value2);
			this.If(cmpOp);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00062488 File Offset: 0x00060688
		internal void Else()
		{
			IfState ifState = this.PopIfState();
			this.Br(ifState.EndIf);
			this.MarkLabel(ifState.ElseBegin);
			ifState.ElseBegin = ifState.EndIf;
			this.blockStack.Push(ifState);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000624CC File Offset: 0x000606CC
		internal void EndIf()
		{
			IfState ifState = this.PopIfState();
			if (!ifState.ElseBegin.Equals(ifState.EndIf))
			{
				this.MarkLabel(ifState.ElseBegin);
			}
			this.MarkLabel(ifState.EndIf);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0006250E File Offset: 0x0006070E
		internal void BeginExceptionBlock()
		{
			this.leaveLabels.Push(this.DefineLabel());
			this.ilGen.BeginExceptionBlock();
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00062532 File Offset: 0x00060732
		internal void BeginCatchBlock(Type exception)
		{
			this.ilGen.BeginCatchBlock(exception);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00062540 File Offset: 0x00060740
		internal void EndExceptionBlock()
		{
			this.ilGen.EndExceptionBlock();
			this.ilGen.MarkLabel((Label)this.leaveLabels.Pop());
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00062568 File Offset: 0x00060768
		internal void Leave()
		{
			this.ilGen.Emit(OpCodes.Leave, (Label)this.leaveLabels.Peek());
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0006258A File Offset: 0x0006078A
		internal void Call(MethodInfo methodInfo)
		{
			if (methodInfo.IsVirtual && !methodInfo.DeclaringType.IsValueType)
			{
				this.ilGen.Emit(OpCodes.Callvirt, methodInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Call, methodInfo);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000625C4 File Offset: 0x000607C4
		internal void Call(ConstructorInfo ctor)
		{
			this.ilGen.Emit(OpCodes.Call, ctor);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000625D7 File Offset: 0x000607D7
		internal void New(ConstructorInfo constructorInfo)
		{
			this.ilGen.Emit(OpCodes.Newobj, constructorInfo);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000625EA File Offset: 0x000607EA
		internal void InitObj(Type valueType)
		{
			this.ilGen.Emit(OpCodes.Initobj, valueType);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000625FD File Offset: 0x000607FD
		internal void NewArray(Type elementType, object len)
		{
			this.Load(len);
			this.ilGen.Emit(OpCodes.Newarr, elementType);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00062618 File Offset: 0x00060818
		internal void LoadArrayElement(object obj, object arrayIndex)
		{
			Type elementType = this.GetVariableType(obj).GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
				this.Ldobj(elementType);
				return;
			}
			this.Ldelem(elementType);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00062660 File Offset: 0x00060860
		internal void StoreArrayElement(object obj, object arrayIndex, object value)
		{
			Type variableType = this.GetVariableType(obj);
			if (variableType == typeof(Array))
			{
				this.Load(obj);
				this.Call(typeof(Array).GetMethod("SetValue", new Type[]
				{
					typeof(object),
					typeof(int)
				}));
				return;
			}
			Type elementType = variableType.GetElementType();
			this.Load(obj);
			this.Load(arrayIndex);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Ldelema(elementType);
			}
			this.Load(value);
			this.ConvertValue(this.GetVariableType(value), elementType);
			if (CodeGenerator.IsStruct(elementType))
			{
				this.Stobj(elementType);
				return;
			}
			this.Stelem(elementType);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00062719 File Offset: 0x00060919
		private static bool IsStruct(Type objType)
		{
			return objType.IsValueType && !objType.IsPrimitive;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0006272E File Offset: 0x0006092E
		internal Type LoadMember(object obj, MemberInfo memberInfo)
		{
			if (this.GetVariableType(obj).IsValueType)
			{
				this.LoadAddress(obj);
			}
			else
			{
				this.Load(obj);
			}
			return this.LoadMember(memberInfo);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00062758 File Offset: 0x00060958
		private static MethodInfo GetPropertyMethodFromBaseType(PropertyInfo propertyInfo, bool isGetter)
		{
			Type baseType = propertyInfo.DeclaringType.BaseType;
			string name = propertyInfo.Name;
			MethodInfo methodInfo = null;
			while (baseType != null)
			{
				PropertyInfo property = baseType.GetProperty(name);
				if (property != null)
				{
					if (isGetter)
					{
						methodInfo = property.GetGetMethod(true);
					}
					else
					{
						methodInfo = property.GetSetMethod(true);
					}
					if (methodInfo != null)
					{
						break;
					}
				}
				baseType = baseType.BaseType;
			}
			return methodInfo;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000627BC File Offset: 0x000609BC
		internal Type LoadMember(MemberInfo memberInfo)
		{
			Type result;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				result = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsfld, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldfld, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				result = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
				}
			}
			return result;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00062848 File Offset: 0x00060A48
		internal Type LoadMemberAddress(MemberInfo memberInfo)
		{
			Type type;
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				type = fieldInfo.FieldType;
				if (fieldInfo.IsStatic)
				{
					this.ilGen.Emit(OpCodes.Ldsflda, fieldInfo);
				}
				else
				{
					this.ilGen.Emit(OpCodes.Ldflda, fieldInfo);
				}
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				type = propertyInfo.PropertyType;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, true);
					}
					this.Call(methodInfo);
					LocalBuilder tempLocal = this.GetTempLocal(type);
					this.Stloc(tempLocal);
					this.Ldloca(tempLocal);
				}
			}
			return type;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000628F0 File Offset: 0x00060AF0
		internal void StoreMember(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType != MemberTypes.Field)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				if (propertyInfo != null)
				{
					MethodInfo methodInfo = propertyInfo.GetSetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = CodeGenerator.GetPropertyMethodFromBaseType(propertyInfo, false);
					}
					this.Call(methodInfo);
				}
				return;
			}
			FieldInfo fieldInfo = (FieldInfo)memberInfo;
			if (fieldInfo.IsStatic)
			{
				this.ilGen.Emit(OpCodes.Stsfld, fieldInfo);
				return;
			}
			this.ilGen.Emit(OpCodes.Stfld, fieldInfo);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0006296C File Offset: 0x00060B6C
		internal void Load(object obj)
		{
			if (obj == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			if (obj is ArgBuilder)
			{
				this.Ldarg((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.Ldloc((LocalBuilder)obj);
				return;
			}
			this.Ldc(obj);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000629BE File Offset: 0x00060BBE
		internal void LoadAddress(object obj)
		{
			if (obj is ArgBuilder)
			{
				this.LdargAddress((ArgBuilder)obj);
				return;
			}
			if (obj is LocalBuilder)
			{
				this.LdlocAddress((LocalBuilder)obj);
				return;
			}
			this.Load(obj);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000629F1 File Offset: 0x00060BF1
		internal void ConvertAddress(Type source, Type target)
		{
			this.InternalConvert(source, target, true);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000629FC File Offset: 0x00060BFC
		internal void ConvertValue(Type source, Type target)
		{
			this.InternalConvert(source, target, false);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00062A07 File Offset: 0x00060C07
		internal void Castclass(Type target)
		{
			this.ilGen.Emit(OpCodes.Castclass, target);
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00062A1A File Offset: 0x00060C1A
		internal void Box(Type type)
		{
			this.ilGen.Emit(OpCodes.Box, type);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00062A2D File Offset: 0x00060C2D
		internal void Unbox(Type type)
		{
			this.ilGen.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00062A40 File Offset: 0x00060C40
		private OpCode GetLdindOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdindOpCodes[(int)typeCode];
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00062A50 File Offset: 0x00060C50
		internal void Ldobj(Type type)
		{
			OpCode ldindOpCode = this.GetLdindOpCode(Type.GetTypeCode(type));
			if (!ldindOpCode.Equals(OpCodes.Nop))
			{
				this.ilGen.Emit(ldindOpCode);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00062A96 File Offset: 0x00060C96
		internal void Stobj(Type type)
		{
			this.ilGen.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00062AA9 File Offset: 0x00060CA9
		internal void Ceq()
		{
			this.ilGen.Emit(OpCodes.Ceq);
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00062ABB File Offset: 0x00060CBB
		internal void Clt()
		{
			this.ilGen.Emit(OpCodes.Clt);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00062ACD File Offset: 0x00060CCD
		internal void Cne()
		{
			this.Ceq();
			this.Ldc(0);
			this.Ceq();
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00062AE2 File Offset: 0x00060CE2
		internal void Ble(Label label)
		{
			this.ilGen.Emit(OpCodes.Ble, label);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00062AF5 File Offset: 0x00060CF5
		internal void Throw()
		{
			this.ilGen.Emit(OpCodes.Throw);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00062B07 File Offset: 0x00060D07
		internal void Ldtoken(Type t)
		{
			this.ilGen.Emit(OpCodes.Ldtoken, t);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00062B1C File Offset: 0x00060D1C
		internal void Ldc(object o)
		{
			Type type = o.GetType();
			if (o is Type)
			{
				this.Ldtoken((Type)o);
				this.Call(typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(RuntimeTypeHandle)
				}, null));
				return;
			}
			if (type.IsEnum)
			{
				this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(type), null));
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.Ldc((bool)o);
				return;
			case TypeCode.Char:
				throw new NotSupportedException("Char is not a valid schema primitive and should be treated as int in DataContract");
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				this.Ldc(((IConvertible)o).ToInt32(CultureInfo.InvariantCulture));
				return;
			case TypeCode.Int32:
				this.Ldc((int)o);
				return;
			case TypeCode.UInt32:
				this.Ldc((int)((uint)o));
				return;
			case TypeCode.Int64:
				this.Ldc((long)o);
				return;
			case TypeCode.UInt64:
				this.Ldc((long)((ulong)o));
				return;
			case TypeCode.Single:
				this.Ldc((float)o);
				return;
			case TypeCode.Double:
				this.Ldc((double)o);
				return;
			case TypeCode.Decimal:
			{
				ConstructorInfo constructor = typeof(decimal).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(bool),
					typeof(byte)
				}, null);
				int[] bits = decimal.GetBits((decimal)o);
				this.Ldc(bits[0]);
				this.Ldc(bits[1]);
				this.Ldc(bits[2]);
				this.Ldc(((long)bits[3] & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue));
				this.Ldc((int)((byte)(bits[3] >> 16 & 255)));
				this.New(constructor);
				return;
			}
			case TypeCode.DateTime:
			{
				ConstructorInfo constructor2 = typeof(DateTime).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(long)
				}, null);
				this.Ldc(((DateTime)o).Ticks);
				this.New(constructor2);
				return;
			}
			case TypeCode.String:
				this.Ldstr((string)o);
				return;
			}
			if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				ConstructorInfo constructor3 = typeof(TimeSpan).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(long)
				}, null);
				this.Ldc(((TimeSpan)o).Ticks);
				this.New(constructor3);
				return;
			}
			throw new NotSupportedException("UnknownConstantType");
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00062DED File Offset: 0x00060FED
		internal void Ldc(bool boolVar)
		{
			if (boolVar)
			{
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00062E14 File Offset: 0x00061014
		internal void Ldc(int intVar)
		{
			switch (intVar)
			{
			case -1:
				this.ilGen.Emit(OpCodes.Ldc_I4_M1);
				return;
			case 0:
				this.ilGen.Emit(OpCodes.Ldc_I4_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldc_I4_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldc_I4_3);
				return;
			case 4:
				this.ilGen.Emit(OpCodes.Ldc_I4_4);
				return;
			case 5:
				this.ilGen.Emit(OpCodes.Ldc_I4_5);
				return;
			case 6:
				this.ilGen.Emit(OpCodes.Ldc_I4_6);
				return;
			case 7:
				this.ilGen.Emit(OpCodes.Ldc_I4_7);
				return;
			case 8:
				this.ilGen.Emit(OpCodes.Ldc_I4_8);
				return;
			default:
				this.ilGen.Emit(OpCodes.Ldc_I4, intVar);
				return;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00062F11 File Offset: 0x00061111
		internal void Ldc(long l)
		{
			this.ilGen.Emit(OpCodes.Ldc_I8, l);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00062F24 File Offset: 0x00061124
		internal void Ldc(float f)
		{
			this.ilGen.Emit(OpCodes.Ldc_R4, f);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00062F37 File Offset: 0x00061137
		internal void Ldc(double d)
		{
			this.ilGen.Emit(OpCodes.Ldc_R8, d);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00062F4A File Offset: 0x0006114A
		internal void Ldstr(string strVar)
		{
			if (strVar == null)
			{
				this.ilGen.Emit(OpCodes.Ldnull);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldstr, strVar);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00062F71 File Offset: 0x00061171
		internal void LdlocAddress(LocalBuilder localBuilder)
		{
			if (localBuilder.LocalType.IsValueType)
			{
				this.Ldloca(localBuilder);
				return;
			}
			this.Ldloc(localBuilder);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00062F8F File Offset: 0x0006118F
		internal void Ldloc(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloc, localBuilder);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00062FA4 File Offset: 0x000611A4
		internal void Ldloc(string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00062FC8 File Offset: 0x000611C8
		internal void Stloc(Type type, string name)
		{
			LocalBuilder local = null;
			if (!this.currentScope.TryGetValue(name, out local))
			{
				local = this.DeclareLocal(type, name);
			}
			this.Stloc(local);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00062FF7 File Offset: 0x000611F7
		internal void Stloc(LocalBuilder local)
		{
			this.ilGen.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0006300C File Offset: 0x0006120C
		internal void Ldloc(Type type, string name)
		{
			LocalBuilder localBuilder = this.currentScope[name];
			this.Ldloc(localBuilder);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0006302D File Offset: 0x0006122D
		internal void Ldloca(LocalBuilder localBuilder)
		{
			this.ilGen.Emit(OpCodes.Ldloca, localBuilder);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00063040 File Offset: 0x00061240
		internal void LdargAddress(ArgBuilder argBuilder)
		{
			if (argBuilder.ArgType.IsValueType)
			{
				this.Ldarga(argBuilder);
				return;
			}
			this.Ldarg(argBuilder);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0006305E File Offset: 0x0006125E
		internal void Ldarg(string arg)
		{
			this.Ldarg(this.GetArg(arg));
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0006306D File Offset: 0x0006126D
		internal void Ldarg(ArgBuilder arg)
		{
			this.Ldarg(arg.Index);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0006307C File Offset: 0x0006127C
		internal void Ldarg(int slot)
		{
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Ldarg_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldarg_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldarg_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldarg_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Ldarg_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Ldarg, slot);
				return;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00063110 File Offset: 0x00061310
		internal void Ldarga(ArgBuilder argBuilder)
		{
			this.Ldarga(argBuilder.Index);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0006311E File Offset: 0x0006131E
		internal void Ldarga(int slot)
		{
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldarga_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldarga, slot);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0006314B File Offset: 0x0006134B
		internal void Ldlen()
		{
			this.ilGen.Emit(OpCodes.Ldlen);
			this.ilGen.Emit(OpCodes.Conv_I4);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0006316D File Offset: 0x0006136D
		private OpCode GetLdelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.LdelemOpCodes[(int)typeCode];
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0006317C File Offset: 0x0006137C
		internal void Ldelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Ldelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode ldelemOpCode = this.GetLdelemOpCode(Type.GetTypeCode(arrayElementType));
			if (ldelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(ldelemOpCode);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000631D0 File Offset: 0x000613D0
		internal void Ldelema(Type arrayElementType)
		{
			OpCode ldelema = OpCodes.Ldelema;
			this.ilGen.Emit(ldelema, arrayElementType);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000631F0 File Offset: 0x000613F0
		private OpCode GetStelemOpCode(TypeCode typeCode)
		{
			return CodeGenerator.StelemOpCodes[(int)typeCode];
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00063200 File Offset: 0x00061400
		internal void Stelem(Type arrayElementType)
		{
			if (arrayElementType.IsEnum)
			{
				this.Stelem(Enum.GetUnderlyingType(arrayElementType));
				return;
			}
			OpCode stelemOpCode = this.GetStelemOpCode(Type.GetTypeCode(arrayElementType));
			if (stelemOpCode.Equals(OpCodes.Nop))
			{
				throw new InvalidOperationException("ArrayTypeIsNotSupported");
			}
			this.ilGen.Emit(stelemOpCode);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x00063254 File Offset: 0x00061454
		internal Label DefineLabel()
		{
			return this.ilGen.DefineLabel();
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00063261 File Offset: 0x00061461
		internal void MarkLabel(Label label)
		{
			this.ilGen.MarkLabel(label);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0006326F File Offset: 0x0006146F
		internal void Nop()
		{
			this.ilGen.Emit(OpCodes.Nop);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00063281 File Offset: 0x00061481
		internal void Add()
		{
			this.ilGen.Emit(OpCodes.Add);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00063293 File Offset: 0x00061493
		internal void Ret()
		{
			this.ilGen.Emit(OpCodes.Ret);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000632A5 File Offset: 0x000614A5
		internal void Br(Label label)
		{
			this.ilGen.Emit(OpCodes.Br, label);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000632B8 File Offset: 0x000614B8
		internal void Br_S(Label label)
		{
			this.ilGen.Emit(OpCodes.Br_S, label);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000632CB File Offset: 0x000614CB
		internal void Blt(Label label)
		{
			this.ilGen.Emit(OpCodes.Blt, label);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000632DE File Offset: 0x000614DE
		internal void Brfalse(Label label)
		{
			this.ilGen.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000632F1 File Offset: 0x000614F1
		internal void Brtrue(Label label)
		{
			this.ilGen.Emit(OpCodes.Brtrue, label);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00063304 File Offset: 0x00061504
		internal void Pop()
		{
			this.ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00063316 File Offset: 0x00061516
		internal void Dup()
		{
			this.ilGen.Emit(OpCodes.Dup);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00063328 File Offset: 0x00061528
		internal void Ldftn(MethodInfo methodInfo)
		{
			this.ilGen.Emit(OpCodes.Ldftn, methodInfo);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0006333C File Offset: 0x0006153C
		private void InternalIf(bool negate)
		{
			IfState ifState = new IfState();
			ifState.EndIf = this.DefineLabel();
			ifState.ElseBegin = this.DefineLabel();
			if (negate)
			{
				this.Brtrue(ifState.ElseBegin);
			}
			else
			{
				this.Brfalse(ifState.ElseBegin);
			}
			this.blockStack.Push(ifState);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00063390 File Offset: 0x00061590
		private OpCode GetConvOpCode(TypeCode typeCode)
		{
			return CodeGenerator.ConvOpCodes[(int)typeCode];
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000633A0 File Offset: 0x000615A0
		private void InternalConvert(Type source, Type target, bool isAddress)
		{
			if (target == source)
			{
				return;
			}
			if (target.IsValueType)
			{
				if (source.IsValueType)
				{
					OpCode convOpCode = this.GetConvOpCode(Type.GetTypeCode(target));
					if (convOpCode.Equals(OpCodes.Nop))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "NoConversionPossibleTo");
					}
					this.ilGen.Emit(convOpCode);
					return;
				}
				else
				{
					if (!source.IsAssignableFrom(target))
					{
						throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
					}
					this.Unbox(target);
					if (!isAddress)
					{
						this.Ldobj(target);
						return;
					}
				}
			}
			else if (target.IsAssignableFrom(source))
			{
				if (source.IsValueType)
				{
					if (isAddress)
					{
						this.Ldobj(source);
					}
					this.Box(source);
					return;
				}
			}
			else
			{
				if (source.IsAssignableFrom(target))
				{
					this.Castclass(target);
					return;
				}
				if (target.IsInterface || source.IsInterface)
				{
					this.Castclass(target);
					return;
				}
				throw new CodeGeneratorConversionException(source, target, isAddress, "IsNotAssignableFrom");
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00063480 File Offset: 0x00061680
		private IfState PopIfState()
		{
			object obj = this.blockStack.Pop();
			return obj as IfState;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000634A4 File Offset: 0x000616A4
		internal static AssemblyBuilder CreateAssemblyBuilder(AppDomain appDomain, string name)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name;
			assemblyName.Version = new Version(1, 0, 0, 0);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave, CodeGenerator.TempFilesLocation);
			}
			return appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x000634F0 File Offset: 0x000616F0
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00063544 File Offset: 0x00061744
		internal static string TempFilesLocation
		{
			get
			{
				if (CodeGenerator.tempFilesLocation == null)
				{
					object section = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath);
					string text = null;
					if (section != null)
					{
						XmlSerializerSection xmlSerializerSection = section as XmlSerializerSection;
						if (xmlSerializerSection != null)
						{
							text = xmlSerializerSection.TempFilesLocation;
						}
					}
					if (text != null)
					{
						CodeGenerator.tempFilesLocation = text.Trim();
					}
					else
					{
						CodeGenerator.tempFilesLocation = Path.GetTempPath();
					}
				}
				return CodeGenerator.tempFilesLocation;
			}
			set
			{
				CodeGenerator.tempFilesLocation = value;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0006354C File Offset: 0x0006174C
		internal static ModuleBuilder CreateModuleBuilder(AssemblyBuilder assemblyBuilder, string name)
		{
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				return assemblyBuilder.DefineDynamicModule(name, name + ".dll", true);
			}
			return assemblyBuilder.DefineDynamicModule(name);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00063575 File Offset: 0x00061775
		internal static TypeBuilder CreateTypeBuilder(ModuleBuilder moduleBuilder, string name, TypeAttributes attributes, Type parent, Type[] interfaces)
		{
			return moduleBuilder.DefineType("Microsoft.Xml.Serialization.GeneratedAssembly." + name, attributes, parent, interfaces);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0006358C File Offset: 0x0006178C
		internal void InitElseIf()
		{
			this.elseIfState = (IfState)this.blockStack.Pop();
			this.initElseIfStack = this.blockStack.Count;
			this.Br(this.elseIfState.EndIf);
			this.MarkLabel(this.elseIfState.ElseBegin);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000635E2 File Offset: 0x000617E2
		internal void InitIf()
		{
			this.initIfStack = this.blockStack.Count;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000635F8 File Offset: 0x000617F8
		internal void AndIf(Cmp cmpOp)
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If(cmpOp);
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.ilGen.Emit(this.GetBranchCode(cmpOp), this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.ilGen.Emit(this.GetBranchCode(cmpOp), ifState.ElseBegin);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000636AC File Offset: 0x000618AC
		internal void AndIf()
		{
			if (this.initIfStack == this.blockStack.Count)
			{
				this.initIfStack = -1;
				this.If();
				return;
			}
			if (this.initElseIfStack == this.blockStack.Count)
			{
				this.initElseIfStack = -1;
				this.elseIfState.ElseBegin = this.DefineLabel();
				this.Brfalse(this.elseIfState.ElseBegin);
				this.blockStack.Push(this.elseIfState);
				return;
			}
			IfState ifState = (IfState)this.blockStack.Peek();
			this.Brfalse(ifState.ElseBegin);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00063745 File Offset: 0x00061945
		internal void IsInst(Type type)
		{
			this.ilGen.Emit(OpCodes.Isinst, type);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00063758 File Offset: 0x00061958
		internal void Beq(Label label)
		{
			this.ilGen.Emit(OpCodes.Beq, label);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0006376B File Offset: 0x0006196B
		internal void Bne(Label label)
		{
			this.ilGen.Emit(OpCodes.Bne_Un, label);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0006377E File Offset: 0x0006197E
		internal void GotoMethodEnd()
		{
			this.Br(this.methodEndLabel);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0006378C File Offset: 0x0006198C
		internal void WhileBegin()
		{
			CodeGenerator.WhileState whileState = new CodeGenerator.WhileState(this);
			this.Br(whileState.CondLabel);
			this.MarkLabel(whileState.StartLabel);
			this.whileStack.Push(whileState);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000637C4 File Offset: 0x000619C4
		internal void WhileEnd()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Pop();
			this.MarkLabel(whileState.EndLabel);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000637F0 File Offset: 0x000619F0
		internal void WhileBreak()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.EndLabel);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0006381C File Offset: 0x00061A1C
		internal void WhileContinue()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Br(whileState.CondLabel);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00063848 File Offset: 0x00061A48
		internal void WhileBeginCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Nop();
			this.MarkLabel(whileState.CondLabel);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00063878 File Offset: 0x00061A78
		internal void WhileEndCondition()
		{
			CodeGenerator.WhileState whileState = (CodeGenerator.WhileState)this.whileStack.Peek();
			this.Brtrue(whileState.StartLabel);
		}

		// Token: 0x04000A61 RID: 2657
		internal static BindingFlags InstancePublicBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04000A62 RID: 2658
		internal static BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000A63 RID: 2659
		internal static BindingFlags StaticBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000A64 RID: 2660
		internal static MethodAttributes PublicMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig;

		// Token: 0x04000A65 RID: 2661
		internal static MethodAttributes PublicOverrideMethodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x04000A66 RID: 2662
		internal static MethodAttributes ProtectedOverrideMethodAttributes = MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig;

		// Token: 0x04000A67 RID: 2663
		internal static MethodAttributes PrivateMethodAttributes = MethodAttributes.Private | MethodAttributes.HideBySig;

		// Token: 0x04000A68 RID: 2664
		internal static Type[] EmptyTypeArray = new Type[0];

		// Token: 0x04000A69 RID: 2665
		internal static string[] EmptyStringArray = new string[0];

		// Token: 0x04000A6A RID: 2666
		private TypeBuilder typeBuilder;

		// Token: 0x04000A6B RID: 2667
		private MethodBuilder methodBuilder;

		// Token: 0x04000A6C RID: 2668
		private ILGenerator ilGen;

		// Token: 0x04000A6D RID: 2669
		private Dictionary<string, ArgBuilder> argList;

		// Token: 0x04000A6E RID: 2670
		private LocalScope currentScope;

		// Token: 0x04000A6F RID: 2671
		private Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals;

		// Token: 0x04000A70 RID: 2672
		private Stack blockStack;

		// Token: 0x04000A71 RID: 2673
		private Label methodEndLabel;

		// Token: 0x04000A72 RID: 2674
		internal LocalBuilder retLocal;

		// Token: 0x04000A73 RID: 2675
		internal Label retLabel;

		// Token: 0x04000A74 RID: 2676
		private Dictionary<Type, LocalBuilder> TmpLocals = new Dictionary<Type, LocalBuilder>();

		// Token: 0x04000A75 RID: 2677
		private static OpCode[] BranchCodes = new OpCode[]
		{
			OpCodes.Bge,
			OpCodes.Bne_Un,
			OpCodes.Bgt,
			OpCodes.Ble,
			OpCodes.Beq,
			OpCodes.Blt
		};

		// Token: 0x04000A76 RID: 2678
		private Stack leaveLabels = new Stack();

		// Token: 0x04000A77 RID: 2679
		private static OpCode[] LdindOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_I1,
			OpCodes.Ldind_U1,
			OpCodes.Ldind_I2,
			OpCodes.Ldind_U2,
			OpCodes.Ldind_I4,
			OpCodes.Ldind_U4,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_I8,
			OpCodes.Ldind_R4,
			OpCodes.Ldind_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldind_Ref
		};

		// Token: 0x04000A78 RID: 2680
		private static OpCode[] LdelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_Ref,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_I1,
			OpCodes.Ldelem_U1,
			OpCodes.Ldelem_I2,
			OpCodes.Ldelem_U2,
			OpCodes.Ldelem_I4,
			OpCodes.Ldelem_U4,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_I8,
			OpCodes.Ldelem_R4,
			OpCodes.Ldelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Ldelem_Ref
		};

		// Token: 0x04000A79 RID: 2681
		private static OpCode[] StelemOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_Ref,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I1,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I2,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I4,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_I8,
			OpCodes.Stelem_R4,
			OpCodes.Stelem_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Stelem_Ref
		};

		// Token: 0x04000A7A RID: 2682
		private static OpCode[] ConvOpCodes = new OpCode[]
		{
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Conv_I1,
			OpCodes.Conv_I2,
			OpCodes.Conv_I1,
			OpCodes.Conv_U1,
			OpCodes.Conv_I2,
			OpCodes.Conv_U2,
			OpCodes.Conv_I4,
			OpCodes.Conv_U4,
			OpCodes.Conv_I8,
			OpCodes.Conv_U8,
			OpCodes.Conv_R4,
			OpCodes.Conv_R8,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop,
			OpCodes.Nop
		};

		// Token: 0x04000A7B RID: 2683
		private static string tempFilesLocation = null;

		// Token: 0x04000A7C RID: 2684
		private int initElseIfStack = -1;

		// Token: 0x04000A7D RID: 2685
		private IfState elseIfState;

		// Token: 0x04000A7E RID: 2686
		private int initIfStack = -1;

		// Token: 0x04000A7F RID: 2687
		private Stack whileStack;

		// Token: 0x02000477 RID: 1143
		internal class WhileState
		{
			// Token: 0x060030C1 RID: 12481 RVA: 0x0011D5C2 File Offset: 0x0011B7C2
			public WhileState(CodeGenerator ilg)
			{
				this.StartLabel = ilg.DefineLabel();
				this.CondLabel = ilg.DefineLabel();
				this.EndLabel = ilg.DefineLabel();
			}

			// Token: 0x04001DC5 RID: 7621
			public Label StartLabel;

			// Token: 0x04001DC6 RID: 7622
			public Label CondLabel;

			// Token: 0x04001DC7 RID: 7623
			public Label EndLabel;
		}
	}
}
