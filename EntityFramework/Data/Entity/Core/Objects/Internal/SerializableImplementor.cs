using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Security;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000598 RID: 1432
	internal sealed class SerializableImplementor
	{
		// Token: 0x060037F2 RID: 14322 RVA: 0x001098FC File Offset: 0x00107AFC
		internal SerializableImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			this._baseImplementsISerializable = (this._baseClrType.IsSerializable() && typeof(ISerializable).IsAssignableFrom(this._baseClrType));
			if (this._baseImplementsISerializable)
			{
				this._getObjectDataMethod = this._baseClrType.GetInterfaceMap(typeof(ISerializable)).TargetMethods[0];
				bool flag = this._getObjectDataMethod.IsVirtual && !this._getObjectDataMethod.IsFinal && this._getObjectDataMethod.IsPublic;
				if (flag)
				{
					this._serializationConstructor = this._baseClrType.GetDeclaredConstructor((ConstructorInfo c) => c.IsPublic || c.IsFamily || c.IsFamilyOrAssembly, new Type[][]
					{
						new Type[]
						{
							typeof(SerializationInfo),
							typeof(StreamingContext)
						},
						new Type[]
						{
							typeof(SerializationInfo),
							typeof(object)
						},
						new Type[]
						{
							typeof(object),
							typeof(StreamingContext)
						},
						new Type[]
						{
							typeof(object),
							typeof(object)
						}
					});
					this._canOverride = (this._serializationConstructor != null);
				}
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x00109A8D File Offset: 0x00107C8D
		internal bool TypeIsSuitable
		{
			get
			{
				return !this._baseImplementsISerializable || this._canOverride;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x00109A9F File Offset: 0x00107C9F
		internal bool TypeImplementsISerializable
		{
			get
			{
				return this._baseImplementsISerializable;
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x00109AA8 File Offset: 0x00107CA8
		internal void Implement(TypeBuilder typeBuilder, IEnumerable<FieldBuilder> serializedFields)
		{
			if (this._baseImplementsISerializable && this._canOverride)
			{
				Type[] parameterTypes = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				MethodBuilder methodBuilder = typeBuilder.DefineMethod(this._getObjectDataMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, null, parameterTypes);
				methodBuilder.SetCustomAttribute(new CustomAttributeBuilder(typeof(SecurityCriticalAttribute).GetDeclaredConstructor(new Type[0]), new object[0]));
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				foreach (FieldBuilder fieldBuilder in serializedFields)
				{
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Ldstr, fieldBuilder.Name);
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
					ilgenerator.Emit(OpCodes.Ldtoken, fieldBuilder.FieldType);
					ilgenerator.Emit(OpCodes.Call, SerializableImplementor.GetTypeFromHandleMethod);
					ilgenerator.Emit(OpCodes.Callvirt, SerializableImplementor.AddValueMethod);
				}
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldarg_2);
				ilgenerator.Emit(OpCodes.Call, this._getObjectDataMethod);
				ilgenerator.Emit(OpCodes.Ret);
				MethodAttributes methodAttributes = MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
				methodAttributes |= (this._serializationConstructor.IsPublic ? MethodAttributes.Public : MethodAttributes.Private);
				ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(methodAttributes, CallingConventions.Standard | CallingConventions.HasThis, parameterTypes);
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
					ilgenerator2.Emit(OpCodes.Call, SerializableImplementor.GetTypeFromHandleMethod);
					ilgenerator2.Emit(OpCodes.Callvirt, SerializableImplementor.GetValueMethod);
					ilgenerator2.Emit(OpCodes.Castclass, fieldBuilder2.FieldType);
					ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder2);
				}
				ilgenerator2.Emit(OpCodes.Ret);
			}
		}

		// Token: 0x0400157F RID: 5503
		private readonly Type _baseClrType;

		// Token: 0x04001580 RID: 5504
		private readonly bool _baseImplementsISerializable;

		// Token: 0x04001581 RID: 5505
		private readonly bool _canOverride;

		// Token: 0x04001582 RID: 5506
		private readonly MethodInfo _getObjectDataMethod;

		// Token: 0x04001583 RID: 5507
		private readonly ConstructorInfo _serializationConstructor;

		// Token: 0x04001584 RID: 5508
		internal static readonly MethodInfo GetTypeFromHandleMethod = typeof(Type).GetDeclaredMethod("GetTypeFromHandle", new Type[]
		{
			typeof(RuntimeTypeHandle)
		});

		// Token: 0x04001585 RID: 5509
		internal static readonly MethodInfo AddValueMethod = typeof(SerializationInfo).GetDeclaredMethod("AddValue", new Type[]
		{
			typeof(string),
			typeof(object),
			typeof(Type)
		});

		// Token: 0x04001586 RID: 5510
		internal static readonly MethodInfo GetValueMethod = typeof(SerializationInfo).GetDeclaredMethod("GetValue", new Type[]
		{
			typeof(string),
			typeof(Type)
		});
	}
}
