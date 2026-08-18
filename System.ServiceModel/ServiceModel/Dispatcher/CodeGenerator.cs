using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A5 RID: 1445
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal class CodeGenerator
	{
		// Token: 0x0600382D RID: 14381 RVA: 0x000D7FBC File Offset: 0x000D61BC
		internal CodeGenerator()
		{
			SourceSwitch codeGenerationSwitch = OperationInvokerTrace.CodeGenerationSwitch;
			if ((codeGenerationSwitch.Level & SourceLevels.Verbose) == SourceLevels.Verbose)
			{
				this.codeGenTrace = CodeGenerator.CodeGenTrace.Tron;
				return;
			}
			if ((codeGenerationSwitch.Level & SourceLevels.Information) == SourceLevels.Information)
			{
				this.codeGenTrace = CodeGenerator.CodeGenTrace.Save;
				return;
			}
			this.codeGenTrace = CodeGenerator.CodeGenTrace.None;
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600382E RID: 14382 RVA: 0x000D800D File Offset: 0x000D620D
		private static MethodInfo GetTypeFromHandle
		{
			get
			{
				if (CodeGenerator.getTypeFromHandle == null)
				{
					CodeGenerator.getTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle");
				}
				return CodeGenerator.getTypeFromHandle;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x0600382F RID: 14383 RVA: 0x000D803C File Offset: 0x000D623C
		private static MethodInfo StringConcat2
		{
			get
			{
				if (CodeGenerator.stringConcat2 == null)
				{
					CodeGenerator.stringConcat2 = typeof(string).GetMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string)
					});
				}
				return CodeGenerator.stringConcat2;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003830 RID: 14384 RVA: 0x000D8094 File Offset: 0x000D6294
		private static MethodInfo ObjectToString
		{
			get
			{
				if (CodeGenerator.objectToString == null)
				{
					CodeGenerator.objectToString = typeof(object).GetMethod("ToString", new Type[0]);
				}
				return CodeGenerator.objectToString;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003831 RID: 14385 RVA: 0x000D80C7 File Offset: 0x000D62C7
		private static MethodInfo BoxPointer
		{
			get
			{
				if (CodeGenerator.boxPointer == null)
				{
					CodeGenerator.boxPointer = typeof(Pointer).GetMethod("Box");
				}
				return CodeGenerator.boxPointer;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000D80F4 File Offset: 0x000D62F4
		private static MethodInfo UnboxPointer
		{
			get
			{
				if (CodeGenerator.unboxPointer == null)
				{
					CodeGenerator.unboxPointer = typeof(Pointer).GetMethod("Unbox");
				}
				return CodeGenerator.unboxPointer;
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000D8124 File Offset: 0x000D6324
		internal void BeginMethod(string methodName, Type delegateType, bool allowPrivateMemberAccess)
		{
			MethodInfo method = delegateType.GetMethod("Invoke");
			ParameterInfo[] parameters = method.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			this.BeginMethod(method.ReturnType, methodName, array, allowPrivateMemberAccess);
			this.delegateType = delegateType;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000D817C File Offset: 0x000D637C
		private void BeginMethod(Type returnType, string methodName, Type[] argTypes, bool allowPrivateMemberAccess)
		{
			this.dynamicMethod = new DynamicMethod(methodName, returnType, argTypes, CodeGenerator.SerializationModule, allowPrivateMemberAccess);
			this.ilGen = this.dynamicMethod.GetILGenerator();
			this.methodEndLabel = this.ilGen.DefineLabel();
			this.blockStack = new Stack();
			this.argList = new ArrayList();
			for (int i = 0; i < argTypes.Length; i++)
			{
				this.argList.Add(new ArgBuilder(i, argTypes[i]));
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceLabel("Begin method " + methodName + " {");
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000D8218 File Offset: 0x000D6418
		internal Delegate EndMethod()
		{
			this.MarkLabel(this.methodEndLabel);
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceLabel("} End method");
			}
			this.Ret();
			Delegate result = this.dynamicMethod.CreateDelegate(this.delegateType);
			this.dynamicMethod = null;
			this.delegateType = null;
			this.ilGen = null;
			this.blockStack = null;
			this.argList = null;
			return result;
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003836 RID: 14390 RVA: 0x000D8282 File Offset: 0x000D6482
		internal MethodInfo CurrentMethod
		{
			get
			{
				return this.dynamicMethod;
			}
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000D828A File Offset: 0x000D648A
		internal ArgBuilder GetArg(int index)
		{
			return (ArgBuilder)this.argList[index];
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000D829D File Offset: 0x000D649D
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

		// Token: 0x06003839 RID: 14393 RVA: 0x000D82CD File Offset: 0x000D64CD
		internal LocalBuilder DeclareLocal(Type type, string name)
		{
			return this.DeclareLocal(type, name, false);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000D82D8 File Offset: 0x000D64D8
		internal LocalBuilder DeclareLocal(Type type, string name, bool isPinned)
		{
			LocalBuilder localBuilder = this.ilGen.DeclareLocal(type, isPinned);
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.LocalNames[localBuilder] = name;
				this.EmitSourceComment("Declare local '" + name + "' of type " + ((type != null) ? type.ToString() : null));
			}
			return localBuilder;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000D832C File Offset: 0x000D652C
		internal void If()
		{
			this.InternalIf(false);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000D8335 File Offset: 0x000D6535
		internal void IfNot()
		{
			this.InternalIf(true);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000D8340 File Offset: 0x000D6540
		internal void Else()
		{
			IfState ifState = this.PopIfState();
			this.Br(ifState.EndIf);
			this.MarkLabel(ifState.ElseBegin);
			ifState.ElseBegin = ifState.EndIf;
			this.blockStack.Push(ifState);
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000D8384 File Offset: 0x000D6584
		internal void EndIf()
		{
			IfState ifState = this.PopIfState();
			if (!ifState.ElseBegin.Equals(ifState.EndIf))
			{
				this.MarkLabel(ifState.ElseBegin);
			}
			this.MarkLabel(ifState.EndIf);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000D83C8 File Offset: 0x000D65C8
		internal void Call(MethodInfo methodInfo)
		{
			if (methodInfo.IsVirtual)
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					this.EmitSourceInstruction("Callvirt " + methodInfo.ToString() + " on type " + methodInfo.DeclaringType.ToString());
				}
				this.ilGen.Emit(OpCodes.Callvirt, methodInfo);
				return;
			}
			if (methodInfo.IsStatic)
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					this.EmitSourceInstruction("Static Call " + methodInfo.ToString() + " on type " + methodInfo.DeclaringType.ToString());
				}
				this.ilGen.Emit(OpCodes.Call, methodInfo);
				return;
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Call " + methodInfo.ToString() + " on type " + methodInfo.DeclaringType.ToString());
			}
			this.ilGen.Emit(OpCodes.Call, methodInfo);
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000D84A4 File Offset: 0x000D66A4
		internal void New(ConstructorInfo constructor)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Newobj " + constructor.ToString() + " on type " + constructor.DeclaringType.ToString());
			}
			this.ilGen.Emit(OpCodes.Newobj, constructor);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000D84F0 File Offset: 0x000D66F0
		internal void InitObj(Type valueType)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Initobj " + ((valueType != null) ? valueType.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Initobj, valueType);
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000D8528 File Offset: 0x000D6728
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

		// Token: 0x06003843 RID: 14403 RVA: 0x000D8570 File Offset: 0x000D6770
		internal void StoreArrayElement(object obj, object arrayIndex, object value)
		{
			Type elementType = this.GetVariableType(obj).GetElementType();
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

		// Token: 0x06003844 RID: 14404 RVA: 0x000D85D3 File Offset: 0x000D67D3
		private static bool IsStruct(Type objType)
		{
			return objType.IsValueType && !objType.IsPrimitive;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000D85E8 File Offset: 0x000D67E8
		internal void Load(object obj)
		{
			if (obj == null)
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					this.EmitSourceInstruction("Ldnull");
				}
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

		// Token: 0x06003846 RID: 14406 RVA: 0x000D8650 File Offset: 0x000D6850
		internal void Store(object var)
		{
			if (var is ArgBuilder)
			{
				this.Starg((ArgBuilder)var);
				return;
			}
			if (var is LocalBuilder)
			{
				this.Stloc((LocalBuilder)var);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenCanOnlyStoreIntoArgOrLocGot0", new object[]
			{
				var.GetType().FullName
			})));
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000D86B4 File Offset: 0x000D68B4
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

		// Token: 0x06003848 RID: 14408 RVA: 0x000D86E7 File Offset: 0x000D68E7
		internal void ConvertAddress(Type source, Type target)
		{
			this.InternalConvert(source, target, true);
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x000D86F2 File Offset: 0x000D68F2
		internal void ConvertValue(Type source, Type target)
		{
			this.InternalConvert(source, target, false);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000D86FD File Offset: 0x000D68FD
		internal void Castclass(Type target)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Castclass " + ((target != null) ? target.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Castclass, target);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000D8735 File Offset: 0x000D6935
		internal void Box(Type type)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Box " + ((type != null) ? type.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Box, type);
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000D876D File Offset: 0x000D696D
		internal void Unbox(Type type)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Unbox " + ((type != null) ? type.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000D87A8 File Offset: 0x000D69A8
		internal void Ldobj(Type type)
		{
			OpCode ldindOpCode = this.GetLdindOpCode(Type.GetTypeCode(type));
			if (!ldindOpCode.Equals(OpCodes.Nop))
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					this.EmitSourceInstruction(ldindOpCode.ToString());
				}
				this.ilGen.Emit(ldindOpCode);
				return;
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldobj " + ((type != null) ? type.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Ldobj, type);
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x000D882E File Offset: 0x000D6A2E
		internal void Stobj(Type type)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Stobj " + ((type != null) ? type.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Stobj, type);
		}

		// Token: 0x0600384F RID: 14415 RVA: 0x000D8866 File Offset: 0x000D6A66
		internal void Ldtoken(Type t)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldtoken " + ((t != null) ? t.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Ldtoken, t);
		}

		// Token: 0x06003850 RID: 14416 RVA: 0x000D88A0 File Offset: 0x000D6AA0
		internal void Ldc(object o)
		{
			Type type = o.GetType();
			if (o is Type)
			{
				this.Ldtoken((Type)o);
				this.Call(CodeGenerator.GetTypeFromHandle);
				return;
			}
			if (type.IsEnum)
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					string str = "Ldc ";
					Type type2 = o.GetType();
					this.EmitSourceComment(str + ((type2 != null) ? type2.ToString() : null) + "." + ((o != null) ? o.ToString() : null));
				}
				this.Ldc(((IConvertible)o).ToType(Enum.GetUnderlyingType(type), null));
				return;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.Ldc((bool)o);
				return;
			case TypeCode.Char:
				this.Ldc((int)((char)o));
				return;
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
			case TypeCode.String:
				this.Ldstr((string)o);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenUnknownConstantType", new object[]
			{
				type.FullName
			})));
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000D8A0C File Offset: 0x000D6C0C
		internal void Ldc(bool boolVar)
		{
			if (boolVar)
			{
				if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
				{
					this.EmitSourceInstruction("Ldc.i4 1");
				}
				this.ilGen.Emit(OpCodes.Ldc_I4_1);
				return;
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldc.i4 0");
			}
			this.ilGen.Emit(OpCodes.Ldc_I4_0);
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000D8A64 File Offset: 0x000D6C64
		internal void Ldc(int intVar)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldc.i4 " + intVar.ToString());
			}
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

		// Token: 0x06003853 RID: 14419 RVA: 0x000D8B80 File Offset: 0x000D6D80
		internal void Ldstr(string strVar)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldstr " + strVar);
			}
			this.ilGen.Emit(OpCodes.Ldstr, strVar);
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000D8BAC File Offset: 0x000D6DAC
		internal void LdlocAddress(LocalBuilder localBuilder)
		{
			if (localBuilder.LocalType.IsValueType)
			{
				this.Ldloca(localBuilder);
				return;
			}
			this.Ldloc(localBuilder);
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x000D8BCC File Offset: 0x000D6DCC
		internal void Ldloc(LocalBuilder localBuilder)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				string str = "Ldloc ";
				object obj = this.LocalNames[localBuilder];
				this.EmitSourceInstruction(str + ((obj != null) ? obj.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Ldloc, localBuilder);
			this.EmitStackTop(localBuilder.LocalType);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x000D8C28 File Offset: 0x000D6E28
		internal void Stloc(LocalBuilder local)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				string str = "Stloc ";
				object obj = this.LocalNames[local];
				this.EmitSourceInstruction(str + ((obj != null) ? obj.ToString() : null));
			}
			this.EmitStackTop(local.LocalType);
			this.ilGen.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x000D8C84 File Offset: 0x000D6E84
		internal void Ldloc(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldloc " + slot.ToString());
			}
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Ldloc_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Ldloc_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Ldloc_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Ldloc_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Ldloc_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Ldloc, slot);
				return;
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x000D8D38 File Offset: 0x000D6F38
		internal void Stloc(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Stloc " + slot.ToString());
			}
			switch (slot)
			{
			case 0:
				this.ilGen.Emit(OpCodes.Stloc_0);
				return;
			case 1:
				this.ilGen.Emit(OpCodes.Stloc_1);
				return;
			case 2:
				this.ilGen.Emit(OpCodes.Stloc_2);
				return;
			case 3:
				this.ilGen.Emit(OpCodes.Stloc_3);
				return;
			default:
				if (slot <= 255)
				{
					this.ilGen.Emit(OpCodes.Stloc_S, slot);
					return;
				}
				this.ilGen.Emit(OpCodes.Stloc, slot);
				return;
			}
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x000D8DEC File Offset: 0x000D6FEC
		internal void Ldloca(LocalBuilder localBuilder)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				string str = "Ldloca ";
				object obj = this.LocalNames[localBuilder];
				this.EmitSourceInstruction(str + ((obj != null) ? obj.ToString() : null));
			}
			this.ilGen.Emit(OpCodes.Ldloca, localBuilder);
			this.EmitStackTop(localBuilder.LocalType);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x000D8E48 File Offset: 0x000D7048
		internal void Ldloca(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldloca " + slot.ToString());
			}
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldloca_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldloca, slot);
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x000D8E9F File Offset: 0x000D709F
		internal void LdargAddress(ArgBuilder argBuilder)
		{
			if (argBuilder.ArgType.IsValueType)
			{
				this.Ldarga(argBuilder);
				return;
			}
			this.Ldarg(argBuilder);
		}

		// Token: 0x0600385C RID: 14428 RVA: 0x000D8EBD File Offset: 0x000D70BD
		internal void Ldarg(ArgBuilder arg)
		{
			this.Ldarg(arg.Index);
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000D8ECB File Offset: 0x000D70CB
		internal void Starg(ArgBuilder arg)
		{
			this.Starg(arg.Index);
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000D8EDC File Offset: 0x000D70DC
		internal void Ldarg(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldarg " + slot.ToString());
			}
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

		// Token: 0x0600385F RID: 14431 RVA: 0x000D8F90 File Offset: 0x000D7190
		internal void Starg(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Starg " + slot.ToString());
			}
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Starg_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Starg, slot);
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000D8FE7 File Offset: 0x000D71E7
		internal void Ldarga(ArgBuilder argBuilder)
		{
			this.Ldarga(argBuilder.Index);
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000D8FF8 File Offset: 0x000D71F8
		internal void Ldarga(int slot)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ldarga " + slot.ToString());
			}
			if (slot <= 255)
			{
				this.ilGen.Emit(OpCodes.Ldarga_S, slot);
				return;
			}
			this.ilGen.Emit(OpCodes.Ldarga, slot);
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000D9050 File Offset: 0x000D7250
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
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenArrayTypeIsNotSupported", new object[]
				{
					arrayElementType.FullName
				})));
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction(ldelemOpCode.ToString());
			}
			this.ilGen.Emit(ldelemOpCode);
			this.EmitStackTop(arrayElementType);
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000D90E4 File Offset: 0x000D72E4
		internal void Ldelema(Type arrayElementType)
		{
			OpCode ldelema = OpCodes.Ldelema;
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction(ldelema.ToString());
			}
			this.ilGen.Emit(ldelema, arrayElementType);
			this.EmitStackTop(arrayElementType);
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000D9128 File Offset: 0x000D7328
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
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenArrayTypeIsNotSupported", new object[]
				{
					arrayElementType.FullName
				})));
			}
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction(stelemOpCode.ToString());
			}
			this.EmitStackTop(arrayElementType);
			this.ilGen.Emit(stelemOpCode);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000D91BC File Offset: 0x000D73BC
		internal Label DefineLabel()
		{
			return this.ilGen.DefineLabel();
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000D91CC File Offset: 0x000D73CC
		internal void MarkLabel(Label label)
		{
			this.ilGen.MarkLabel(label);
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceLabel(label.GetHashCode().ToString() + ":");
			}
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000D9212 File Offset: 0x000D7412
		internal void Ret()
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Ret");
			}
			this.ilGen.Emit(OpCodes.Ret);
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000D9238 File Offset: 0x000D7438
		internal void Br(Label label)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Br " + label.GetHashCode().ToString());
			}
			this.ilGen.Emit(OpCodes.Br, label);
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x000D9284 File Offset: 0x000D7484
		internal void Brfalse(Label label)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Brfalse " + label.GetHashCode().ToString());
			}
			this.ilGen.Emit(OpCodes.Brfalse, label);
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x000D92D0 File Offset: 0x000D74D0
		internal void Brtrue(Label label)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Brtrue " + label.GetHashCode().ToString());
			}
			this.ilGen.Emit(OpCodes.Brtrue, label);
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000D931B File Offset: 0x000D751B
		internal void Pop()
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Pop");
			}
			this.ilGen.Emit(OpCodes.Pop);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000D9340 File Offset: 0x000D7540
		internal void Dup()
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				this.EmitSourceInstruction("Dup");
			}
			this.ilGen.Emit(OpCodes.Dup);
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000D9368 File Offset: 0x000D7568
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

		// Token: 0x0600386E RID: 14446 RVA: 0x000D93BC File Offset: 0x000D75BC
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
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenNoConversionPossibleTo", new object[]
						{
							target.FullName
						})));
					}
					if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
					{
						this.EmitSourceInstruction(convOpCode.ToString());
					}
					this.ilGen.Emit(convOpCode);
					return;
				}
				else
				{
					if (!source.IsAssignableFrom(target))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenIsNotAssignableFrom", new object[]
						{
							target.FullName,
							source.FullName
						})));
					}
					this.Unbox(target);
					if (!isAddress)
					{
						this.Ldobj(target);
						return;
					}
				}
			}
			else
			{
				if (target.IsPointer)
				{
					this.Call(CodeGenerator.UnboxPointer);
					return;
				}
				if (source.IsPointer)
				{
					this.Load(source);
					this.Call(CodeGenerator.BoxPointer);
					return;
				}
				if (target.IsAssignableFrom(source))
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
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenIsNotAssignableFrom", new object[]
					{
						target.FullName,
						source.FullName
					})));
				}
			}
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000D9550 File Offset: 0x000D7750
		private IfState PopIfState()
		{
			object obj = this.blockStack.Pop();
			IfState ifState = obj as IfState;
			if (ifState == null)
			{
				this.ThrowMismatchException(obj);
			}
			return ifState;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000D957B File Offset: 0x000D777B
		private void ThrowMismatchException(object expected)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCodeGenExpectingEnd", new object[]
			{
				expected.ToString()
			})));
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000D95A5 File Offset: 0x000D77A5
		private Hashtable LocalNames
		{
			get
			{
				if (this.localNames == null)
				{
					this.localNames = new Hashtable();
				}
				return this.localNames;
			}
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000D95C0 File Offset: 0x000D77C0
		private OpCode GetConvOpCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Boolean:
				return OpCodes.Conv_I1;
			case TypeCode.Char:
				return OpCodes.Conv_I2;
			case TypeCode.SByte:
				return OpCodes.Conv_I1;
			case TypeCode.Byte:
				return OpCodes.Conv_U1;
			case TypeCode.Int16:
				return OpCodes.Conv_I2;
			case TypeCode.UInt16:
				return OpCodes.Conv_U2;
			case TypeCode.Int32:
				return OpCodes.Conv_I4;
			case TypeCode.UInt32:
				return OpCodes.Conv_U4;
			case TypeCode.Int64:
				return OpCodes.Conv_I8;
			case TypeCode.UInt64:
				return OpCodes.Conv_I8;
			case TypeCode.Single:
				return OpCodes.Conv_R4;
			case TypeCode.Double:
				return OpCodes.Conv_R8;
			default:
				return OpCodes.Nop;
			}
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000D9654 File Offset: 0x000D7854
		private OpCode GetLdindOpCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Boolean:
				return OpCodes.Ldind_I1;
			case TypeCode.Char:
				return OpCodes.Ldind_I2;
			case TypeCode.SByte:
				return OpCodes.Ldind_I1;
			case TypeCode.Byte:
				return OpCodes.Ldind_U1;
			case TypeCode.Int16:
				return OpCodes.Ldind_I2;
			case TypeCode.UInt16:
				return OpCodes.Ldind_U2;
			case TypeCode.Int32:
				return OpCodes.Ldind_I4;
			case TypeCode.UInt32:
				return OpCodes.Ldind_U4;
			case TypeCode.Int64:
				return OpCodes.Ldind_I8;
			case TypeCode.UInt64:
				return OpCodes.Ldind_I8;
			case TypeCode.Single:
				return OpCodes.Ldind_R4;
			case TypeCode.Double:
				return OpCodes.Ldind_R8;
			case TypeCode.String:
				return OpCodes.Ldind_Ref;
			}
			return OpCodes.Nop;
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000D9700 File Offset: 0x000D7900
		private OpCode GetLdelemOpCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Object:
				return OpCodes.Ldelem_Ref;
			case TypeCode.Boolean:
				return OpCodes.Ldelem_I1;
			case TypeCode.Char:
				return OpCodes.Ldelem_I2;
			case TypeCode.SByte:
				return OpCodes.Ldelem_I1;
			case TypeCode.Byte:
				return OpCodes.Ldelem_U1;
			case TypeCode.Int16:
				return OpCodes.Ldelem_I2;
			case TypeCode.UInt16:
				return OpCodes.Ldelem_U2;
			case TypeCode.Int32:
				return OpCodes.Ldelem_I4;
			case TypeCode.UInt32:
				return OpCodes.Ldelem_U4;
			case TypeCode.Int64:
				return OpCodes.Ldelem_I8;
			case TypeCode.UInt64:
				return OpCodes.Ldelem_I8;
			case TypeCode.Single:
				return OpCodes.Ldelem_R4;
			case TypeCode.Double:
				return OpCodes.Ldelem_R8;
			case TypeCode.String:
				return OpCodes.Ldelem_Ref;
			}
			return OpCodes.Nop;
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000D97B8 File Offset: 0x000D79B8
		private OpCode GetStelemOpCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Object:
				return OpCodes.Stelem_Ref;
			case TypeCode.Boolean:
				return OpCodes.Stelem_I1;
			case TypeCode.Char:
				return OpCodes.Stelem_I2;
			case TypeCode.SByte:
				return OpCodes.Stelem_I1;
			case TypeCode.Byte:
				return OpCodes.Stelem_I1;
			case TypeCode.Int16:
				return OpCodes.Stelem_I2;
			case TypeCode.UInt16:
				return OpCodes.Stelem_I2;
			case TypeCode.Int32:
				return OpCodes.Stelem_I4;
			case TypeCode.UInt32:
				return OpCodes.Stelem_I4;
			case TypeCode.Int64:
				return OpCodes.Stelem_I8;
			case TypeCode.UInt64:
				return OpCodes.Stelem_I8;
			case TypeCode.Single:
				return OpCodes.Stelem_R4;
			case TypeCode.Double:
				return OpCodes.Stelem_R8;
			case TypeCode.String:
				return OpCodes.Stelem_Ref;
			}
			return OpCodes.Nop;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000D9870 File Offset: 0x000D7A70
		internal void EmitSourceInstruction(string line)
		{
			this.EmitSourceLine("    " + line);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000D9883 File Offset: 0x000D7A83
		internal void EmitSourceLabel(string line)
		{
			this.EmitSourceLine(line);
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000D988C File Offset: 0x000D7A8C
		internal void EmitSourceComment(string comment)
		{
			this.EmitSourceInstruction("// " + comment);
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000D98A0 File Offset: 0x000D7AA0
		internal void EmitSourceLine(string line)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.None)
			{
				int num = this.lineNo;
				this.lineNo = num + 1;
				OperationInvokerTrace.WriteInstruction(num, line);
			}
			if (this.ilGen != null && this.codeGenTrace == CodeGenerator.CodeGenTrace.Tron)
			{
				this.ilGen.Emit(OpCodes.Ldstr, string.Format(CultureInfo.InvariantCulture, "{0:00000}: {1}", new object[]
				{
					this.lineNo - 1,
					line
				}));
				this.ilGen.Emit(OpCodes.Call, OperationInvokerTrace.TraceInstructionMethod);
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000D992C File Offset: 0x000D7B2C
		internal void EmitStackTop(Type stackTopType)
		{
			if (this.codeGenTrace != CodeGenerator.CodeGenTrace.Tron)
			{
				return;
			}
			this.codeGenTrace = CodeGenerator.CodeGenTrace.None;
			this.Dup();
			this.ToString(stackTopType);
			LocalBuilder localBuilder = this.DeclareLocal(typeof(string), "topValue");
			this.Store(localBuilder);
			this.Load("//value = ");
			this.Load(localBuilder);
			this.Concat2();
			this.Call(OperationInvokerTrace.TraceInstructionMethod);
			this.codeGenTrace = CodeGenerator.CodeGenTrace.Tron;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000D99A0 File Offset: 0x000D7BA0
		internal void ToString(Type type)
		{
			if (type.IsValueType)
			{
				this.Box(type);
				this.Call(CodeGenerator.ObjectToString);
				return;
			}
			this.Dup();
			this.IfNot();
			this.Pop();
			this.Load("<null>");
			this.Else();
			this.Call(CodeGenerator.ObjectToString);
			this.EndIf();
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000D99FC File Offset: 0x000D7BFC
		internal void Concat2()
		{
			this.Call(CodeGenerator.StringConcat2);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000D9A0C File Offset: 0x000D7C0C
		internal void LoadZeroValueIntoLocal(Type type, LocalBuilder local)
		{
			if (type.IsValueType)
			{
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					this.ilGen.Emit(OpCodes.Ldc_I4_0);
					this.Store(local);
					return;
				case TypeCode.Int64:
				case TypeCode.UInt64:
					this.ilGen.Emit(OpCodes.Ldc_I4_0);
					this.ilGen.Emit(OpCodes.Conv_I8);
					this.Store(local);
					return;
				case TypeCode.Single:
					this.ilGen.Emit(OpCodes.Ldc_R4, 0f);
					this.Store(local);
					return;
				case TypeCode.Double:
					this.ilGen.Emit(OpCodes.Ldc_R8, 0.0);
					this.Store(local);
					return;
				}
				this.LoadAddress(local);
				this.InitObj(type);
				return;
			}
			this.Load(null);
			this.Store(local);
		}

		// Token: 0x04002983 RID: 10627
		private static MethodInfo getTypeFromHandle;

		// Token: 0x04002984 RID: 10628
		private static MethodInfo stringConcat2;

		// Token: 0x04002985 RID: 10629
		private static MethodInfo objectToString;

		// Token: 0x04002986 RID: 10630
		private static MethodInfo boxPointer;

		// Token: 0x04002987 RID: 10631
		private static MethodInfo unboxPointer;

		// Token: 0x04002988 RID: 10632
		private static Module SerializationModule = typeof(CodeGenerator).Module;

		// Token: 0x04002989 RID: 10633
		private DynamicMethod dynamicMethod;

		// Token: 0x0400298A RID: 10634
		private Type delegateType;

		// Token: 0x0400298B RID: 10635
		private ILGenerator ilGen;

		// Token: 0x0400298C RID: 10636
		private ArrayList argList;

		// Token: 0x0400298D RID: 10637
		private Stack blockStack;

		// Token: 0x0400298E RID: 10638
		private Label methodEndLabel;

		// Token: 0x0400298F RID: 10639
		private Hashtable localNames;

		// Token: 0x04002990 RID: 10640
		private int lineNo = 1;

		// Token: 0x04002991 RID: 10641
		private CodeGenerator.CodeGenTrace codeGenTrace;

		// Token: 0x02000CA7 RID: 3239
		private enum CodeGenTrace
		{
			// Token: 0x04004506 RID: 17670
			None,
			// Token: 0x04004507 RID: 17671
			Save,
			// Token: 0x04004508 RID: 17672
			Tron
		}
	}
}
