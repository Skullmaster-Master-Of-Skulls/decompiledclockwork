using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000175 RID: 373
	internal sealed class ISerializableImplementor
	{
		// Token: 0x06001B44 RID: 6980 RVA: 0x0005E4D4 File Offset: 0x0005C6D4
		internal ISerializableImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			this._baseImplementsISerializable = (this._baseClrType.IsSerializable && typeof(ISerializable).IsAssignableFrom(this._baseClrType));
			if (this._baseImplementsISerializable)
			{
				InterfaceMapping interfaceMap = this._baseClrType.GetInterfaceMap(typeof(ISerializable));
				this._getObjectDataMethod = interfaceMap.TargetMethods[0];
				bool flag = this._getObjectDataMethod.IsVirtual && !this._getObjectDataMethod.IsFinal && this._getObjectDataMethod.IsPublic;
				if (flag)
				{
					this._serializationConstructor = this._baseClrType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(SerializationInfo),
						typeof(StreamingContext)
					}, null);
					this._canOverride = (this._serializationConstructor != null && (this._serializationConstructor.IsPublic || this._serializationConstructor.IsFamily || this._serializationConstructor.IsFamilyOrAssembly));
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0005E5ED File Offset: 0x0005C7ED
		internal bool TypeIsSuitable
		{
			get
			{
				return !this._baseImplementsISerializable || this._canOverride;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0005E5FF File Offset: 0x0005C7FF
		internal bool TypeImplementsISerializable
		{
			get
			{
				return this._baseImplementsISerializable;
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0005E608 File Offset: 0x0005C808
		internal void Implement(TypeBuilder typeBuilder, IEnumerable<FieldBuilder> serializedFields)
		{
			if (this._baseImplementsISerializable && this._canOverride)
			{
				PermissionSet permissionSet = new PermissionSet(null);
				permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
				Type[] parameterTypes = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				MethodInfo method = typeof(Type).GetMethod("GetTypeFromHandle", new Type[]
				{
					typeof(RuntimeTypeHandle)
				});
				MethodInfo method2 = typeof(SerializationInfo).GetMethod("AddValue", new Type[]
				{
					typeof(string),
					typeof(object),
					typeof(Type)
				});
				MethodInfo method3 = typeof(SerializationInfo).GetMethod("GetValue", new Type[]
				{
					typeof(string),
					typeof(Type)
				});
				MethodBuilder methodBuilder = typeBuilder.DefineMethod(this._getObjectDataMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, null, parameterTypes);
				methodBuilder.AddDeclarativeSecurity(SecurityAction.Demand, permissionSet);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				foreach (FieldBuilder fieldBuilder in serializedFields)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldstr, fieldBuilder.Name);
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
					ilgenerator.Emit(OpCodes.Ldtoken, fieldBuilder.FieldType);
					ilgenerator.Emit(OpCodes.Call, method);
					ilgenerator.Emit(OpCodes.Callvirt, method2);
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Call, this._getObjectDataMethod);
				ilgenerator.Emit(OpCodes.Ret);
				MethodAttributes methodAttributes = MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
				methodAttributes |= (this._serializationConstructor.IsPublic ? MethodAttributes.Public : MethodAttributes.Private);
				ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(methodAttributes, CallingConventions.Standard | CallingConventions.HasThis, parameterTypes);
				constructorBuilder.AddDeclarativeSecurity(SecurityAction.Demand, permissionSet);
				ILGenerator ilgenerator2 = constructorBuilder.GetILGenerator();
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Ldarg_1);
				ilgenerator2.Emit(OpCodes.Ldarg_2);
				ilgenerator2.Emit(OpCodes.Call, this._serializationConstructor);
				foreach (FieldBuilder fieldBuilder2 in serializedFields)
				{
					ilgenerator2.Emit(OpCodes.Ldarg_0);
					ilgenerator2.Emit(OpCodes.Ldarg_1);
					ilgenerator2.Emit(OpCodes.Ldstr, fieldBuilder2.Name);
					ilgenerator2.Emit(OpCodes.Ldtoken, fieldBuilder2.FieldType);
					ilgenerator2.Emit(OpCodes.Call, method);
					ilgenerator2.Emit(OpCodes.Callvirt, method3);
					ilgenerator2.Emit(OpCodes.Castclass, fieldBuilder2.FieldType);
					ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder2);
				}
				ilgenerator2.Emit(OpCodes.Ret);
			}
		}

		// Token: 0x04000B65 RID: 2917
		private readonly Type _baseClrType;

		// Token: 0x04000B66 RID: 2918
		private readonly bool _baseImplementsISerializable;

		// Token: 0x04000B67 RID: 2919
		private readonly bool _canOverride;

		// Token: 0x04000B68 RID: 2920
		private readonly MethodInfo _getObjectDataMethod;

		// Token: 0x04000B69 RID: 2921
		private readonly ConstructorInfo _serializationConstructor;
	}
}
