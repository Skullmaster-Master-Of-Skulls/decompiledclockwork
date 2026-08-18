using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200057A RID: 1402
	internal class EntityProxyFactory
	{
		// Token: 0x060036AC RID: 13996 RVA: 0x00103A1C File Offset: 0x00101C1C
		private static ModuleBuilder GetDynamicModule(EntityType ospaceEntityType)
		{
			Assembly assembly = ospaceEntityType.ClrType.Assembly();
			ModuleBuilder moduleBuilder;
			if (!EntityProxyFactory._moduleBuilders.TryGetValue(assembly, out moduleBuilder))
			{
				AssemblyName assemblyName = new AssemblyName(string.Format(CultureInfo.InvariantCulture, "EntityFrameworkDynamicProxies-{0}", new object[]
				{
					assembly.FullName
				}));
				assemblyName.Version = new Version(1, 0, 0, 0);
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, EntityProxyFactory.s_ProxyAssemblyBuilderAccess);
				if (EntityProxyFactory.s_ProxyAssemblyBuilderAccess == AssemblyBuilderAccess.RunAndSave)
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("EntityProxyModule", "EntityProxyModule.dll");
				}
				else
				{
					moduleBuilder = assemblyBuilder.DefineDynamicModule("EntityProxyModule");
				}
				EntityProxyFactory._moduleBuilders.Add(assembly, moduleBuilder);
			}
			return moduleBuilder;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x00103AC1 File Offset: 0x00101CC1
		private static void DiscardDynamicModule(EntityType ospaceEntityType)
		{
			EntityProxyFactory._moduleBuilders.Remove(ospaceEntityType.ClrType.Assembly());
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x00103ADC File Offset: 0x00101CDC
		internal static bool TryGetProxyType(Type clrType, string entityTypeName, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			bool result;
			try
			{
				result = EntityProxyFactory._proxyNameMap.TryGetValue(new Tuple<Type, string>(clrType, entityTypeName), out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x00103B24 File Offset: 0x00101D24
		internal static bool TryGetProxyType(Type proxyType, out EntityProxyTypeInfo proxyTypeInfo)
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			bool result;
			try
			{
				result = EntityProxyFactory._proxyTypeMap.TryGetValue(proxyType, out proxyTypeInfo);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x00103B68 File Offset: 0x00101D68
		internal static bool TryGetProxyWrapper(object instance, out IEntityWrapper wrapper)
		{
			wrapper = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.IsProxyType(instance.GetType()) && EntityProxyFactory.TryGetProxyType(instance.GetType(), out entityProxyTypeInfo))
			{
				wrapper = entityProxyTypeInfo.GetEntityWrapper(instance);
			}
			return wrapper != null;
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x00103BA8 File Offset: 0x00101DA8
		internal static EntityProxyTypeInfo GetProxyType(ClrEntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			EntityProxyTypeInfo entityProxyTypeInfo = null;
			if (EntityProxyFactory.TryGetProxyType(ospaceEntityType.ClrType, ospaceEntityType.CSpaceTypeName, out entityProxyTypeInfo))
			{
				if (entityProxyTypeInfo != null)
				{
					entityProxyTypeInfo.ValidateType(ospaceEntityType);
				}
				return entityProxyTypeInfo;
			}
			EntityProxyFactory._typeMapLock.EnterUpgradeableReadLock();
			EntityProxyTypeInfo result;
			try
			{
				result = EntityProxyFactory.TryCreateProxyType(ospaceEntityType, workspace);
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x00103C08 File Offset: 0x00101E08
		internal static bool TryGetAssociationTypeFromProxyInfo(IEntityWrapper wrappedEntity, string relationshipName, out AssociationType associationType)
		{
			associationType = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			return EntityProxyFactory.TryGetProxyType(wrappedEntity.Entity.GetType(), out entityProxyTypeInfo) && entityProxyTypeInfo != null && entityProxyTypeInfo.TryGetNavigationPropertyAssociationType(relationshipName, out associationType);
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x00103C3C File Offset: 0x00101E3C
		internal static IEnumerable<AssociationType> TryGetAllAssociationTypesFromProxyInfo(IEntityWrapper wrappedEntity)
		{
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (!EntityProxyFactory.TryGetProxyType(wrappedEntity.Entity.GetType(), out entityProxyTypeInfo))
			{
				return null;
			}
			return entityProxyTypeInfo.GetAllAssociationTypes();
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x00103C68 File Offset: 0x00101E68
		internal static void TryCreateProxyTypes(IEnumerable<EntityType> ospaceEntityTypes, MetadataWorkspace workspace)
		{
			EntityProxyFactory._typeMapLock.EnterUpgradeableReadLock();
			try
			{
				foreach (EntityType ospaceEntityType in ospaceEntityTypes)
				{
					EntityProxyFactory.TryCreateProxyType(ospaceEntityType, workspace);
				}
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x00103CD4 File Offset: 0x00101ED4
		private static EntityProxyTypeInfo TryCreateProxyType(EntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			ClrEntityType clrEntityType = (ClrEntityType)ospaceEntityType;
			Tuple<Type, string> key = new Tuple<Type, string>(clrEntityType.ClrType, clrEntityType.HashedDescription);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (!EntityProxyFactory._proxyNameMap.TryGetValue(key, out entityProxyTypeInfo) && EntityProxyFactory.CanProxyType(ospaceEntityType))
			{
				try
				{
					ModuleBuilder dynamicModule = EntityProxyFactory.GetDynamicModule(ospaceEntityType);
					entityProxyTypeInfo = EntityProxyFactory.BuildType(dynamicModule, clrEntityType, workspace);
					EntityProxyFactory._typeMapLock.EnterWriteLock();
					try
					{
						EntityProxyFactory._proxyNameMap[key] = entityProxyTypeInfo;
						if (entityProxyTypeInfo != null)
						{
							EntityProxyFactory._proxyTypeMap[entityProxyTypeInfo.ProxyType] = entityProxyTypeInfo;
						}
					}
					finally
					{
						EntityProxyFactory._typeMapLock.ExitWriteLock();
					}
				}
				catch
				{
					EntityProxyFactory.DiscardDynamicModule(ospaceEntityType);
					throw;
				}
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x00103D80 File Offset: 0x00101F80
		internal static bool IsProxyType(Type type)
		{
			return type != null && EntityProxyFactory._proxyRuntimeAssemblies.Contains(type.Assembly());
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x00103DB0 File Offset: 0x00101FB0
		internal static IEnumerable<Type> GetKnownProxyTypes()
		{
			EntityProxyFactory._typeMapLock.EnterReadLock();
			IEnumerable<Type> result;
			try
			{
				IEnumerable<Type> source = from info in EntityProxyFactory._proxyNameMap.Values
				where info != null
				select info.ProxyType;
				result = source.ToArray<Type>();
			}
			finally
			{
				EntityProxyFactory._typeMapLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x00103E80 File Offset: 0x00102080
		public virtual Func<object, object> CreateBaseGetter(Type declaringType, PropertyInfo propertyInfo)
		{
			ParameterExpression parameterExpression;
			Func<object, object> nonProxyGetter = Expression.Lambda<Func<object, object>>(Expression.Property(Expression.Convert(parameterExpression, declaringType), propertyInfo), new ParameterExpression[]
			{
				parameterExpression
			}).Compile();
			string propertyName = propertyInfo.Name;
			return delegate(object entity)
			{
				Type type = entity.GetType();
				object result;
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TryGetBasePropertyValue(type, propertyName, entity, out result))
				{
					return result;
				}
				return nonProxyGetter(entity);
			};
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x00103EEC File Offset: 0x001020EC
		private static bool TryGetBasePropertyValue(Type proxyType, string propertyName, object entity, out object value)
		{
			value = null;
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseGetter(propertyName))
			{
				value = entityProxyTypeInfo.BaseGetter(entity, propertyName);
				return true;
			}
			return false;
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x00103F68 File Offset: 0x00102168
		public virtual Action<object, object> CreateBaseSetter(Type declaringType, PropertyInfo propertyInfo)
		{
			Action<object, object> nonProxySetter = DelegateFactory.CreateNavigationPropertySetter(declaringType, propertyInfo);
			string propertyName = propertyInfo.Name;
			return delegate(object entity, object value)
			{
				Type type = entity.GetType();
				if (EntityProxyFactory.IsProxyType(type) && EntityProxyFactory.TrySetBasePropertyValue(type, propertyName, entity, value))
				{
					return;
				}
				nonProxySetter(entity, value);
			};
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x00103FA0 File Offset: 0x001021A0
		private static bool TrySetBasePropertyValue(Type proxyType, string propertyName, object entity, object value)
		{
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(proxyType, out entityProxyTypeInfo) && entityProxyTypeInfo.ContainsBaseSetter(propertyName))
			{
				entityProxyTypeInfo.BaseSetter(entity, propertyName, value);
				return true;
			}
			return false;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x00103FD4 File Offset: 0x001021D4
		private static EntityProxyTypeInfo BuildType(ModuleBuilder moduleBuilder, ClrEntityType ospaceEntityType, MetadataWorkspace workspace)
		{
			EntityProxyFactory.ProxyTypeBuilder proxyTypeBuilder = new EntityProxyFactory.ProxyTypeBuilder(ospaceEntityType);
			Type type = proxyTypeBuilder.CreateType(moduleBuilder);
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (type != null)
			{
				Assembly assembly = type.Assembly();
				if (!EntityProxyFactory._proxyRuntimeAssemblies.Contains(assembly))
				{
					EntityProxyFactory._proxyRuntimeAssemblies.Add(assembly);
					EntityProxyFactory.AddAssemblyToResolveList(assembly);
				}
				entityProxyTypeInfo = new EntityProxyTypeInfo(type, ospaceEntityType, proxyTypeBuilder.CreateInitalizeCollectionMethod(type), proxyTypeBuilder.BaseGetters, proxyTypeBuilder.BaseSetters, workspace);
				foreach (EdmMember member in proxyTypeBuilder.LazyLoadMembers)
				{
					EntityProxyFactory.InterceptMember(member, type, entityProxyTypeInfo);
				}
				EntityProxyFactory.SetResetFKSetterFlagDelegate(type, entityProxyTypeInfo);
				EntityProxyFactory.SetCompareByteArraysDelegate(type);
			}
			else
			{
				entityProxyTypeInfo = null;
			}
			return entityProxyTypeInfo;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x001040C4 File Offset: 0x001022C4
		private static void AddAssemblyToResolveList(Assembly assembly)
		{
			try
			{
				AppDomain.CurrentDomain.AssemblyResolve += delegate(object _, ResolveEventArgs args)
				{
					if (!(args.Name == assembly.FullName))
					{
						return null;
					}
					return assembly;
				};
			}
			catch (MethodAccessException)
			{
			}
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x00104110 File Offset: 0x00102310
		private static void InterceptMember(EdmMember member, Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			PropertyInfo topProperty = proxyType.GetTopProperty(member.Name);
			FieldInfo field = proxyType.GetField(LazyLoadImplementor.GetInterceptorFieldName(member.Name), BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			Delegate interceptorDelegate = EntityProxyFactory.GetInterceptorDelegateMethod.MakeGenericMethod(new Type[]
			{
				proxyType,
				topProperty.PropertyType
			}).Invoke(null, new object[]
			{
				member,
				proxyTypeInfo.EntityWrapperDelegate
			}) as Delegate;
			EntityProxyFactory.AssignInterceptionDelegate(interceptorDelegate, field);
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x00104189 File Offset: 0x00102389
		private static void AssignInterceptionDelegate(Delegate interceptorDelegate, FieldInfo interceptorField)
		{
			interceptorField.SetValue(null, interceptorDelegate);
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x00104194 File Offset: 0x00102394
		private static void SetResetFKSetterFlagDelegate(Type proxyType, EntityProxyTypeInfo proxyTypeInfo)
		{
			FieldInfo field = proxyType.GetField("_resetFKSetterFlag", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			Action<object> resetFKSetterFlagDelegate = EntityProxyFactory.GetResetFKSetterFlagDelegate(proxyTypeInfo.EntityWrapperDelegate);
			EntityProxyFactory.AssignInterceptionDelegate(resetFKSetterFlagDelegate, field);
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x001041E0 File Offset: 0x001023E0
		private static Action<object> GetResetFKSetterFlagDelegate(Func<object, object> getEntityWrapperDelegate)
		{
			return delegate(object proxy)
			{
				EntityProxyFactory.ResetFKSetterFlag(getEntityWrapperDelegate(proxy));
			};
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x00104208 File Offset: 0x00102408
		private static void ResetFKSetterFlag(object wrappedEntityAsObject)
		{
			IEntityWrapper entityWrapper = (IEntityWrapper)wrappedEntityAsObject;
			if (entityWrapper != null && entityWrapper.Context != null)
			{
				entityWrapper.Context.ObjectStateManager.EntityInvokingFKSetter = null;
			}
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x00104238 File Offset: 0x00102438
		private static void SetCompareByteArraysDelegate(Type proxyType)
		{
			FieldInfo field = proxyType.GetField("_compareByteArrays", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
			EntityProxyFactory.AssignInterceptionDelegate(new Func<object, object, bool>(ByValueEqualityComparer.Default.Equals), field);
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x0010426C File Offset: 0x0010246C
		private static bool CanProxyType(EntityType ospaceEntityType)
		{
			Type clrType = ospaceEntityType.ClrType;
			if (!clrType.IsPublic() || clrType.IsSealed() || typeof(IEntityWithRelationships).IsAssignableFrom(clrType) || ospaceEntityType.Abstract)
			{
				return false;
			}
			ConstructorInfo declaredConstructor = clrType.GetDeclaredConstructor(new Type[0]);
			return declaredConstructor != null && ((declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public || (declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family || (declaredConstructor.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x001042E8 File Offset: 0x001024E8
		private static bool CanProxyMethod(MethodInfo method)
		{
			bool result = false;
			if (method != null)
			{
				MethodAttributes methodAttributes = method.Attributes & MethodAttributes.MemberAccessMask;
				result = (method.IsVirtual && !method.IsFinal && (methodAttributes == MethodAttributes.Public || methodAttributes == MethodAttributes.Family || methodAttributes == MethodAttributes.FamORAssem));
			}
			return result;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x0010432D File Offset: 0x0010252D
		internal static bool CanProxyGetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.Getter());
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x0010433A File Offset: 0x0010253A
		internal static bool CanProxySetter(PropertyInfo clrProperty)
		{
			return EntityProxyFactory.CanProxyMethod(clrProperty.Setter());
		}

		// Token: 0x040014EB RID: 5355
		internal const string ResetFKSetterFlagFieldName = "_resetFKSetterFlag";

		// Token: 0x040014EC RID: 5356
		internal const string CompareByteArraysFieldName = "_compareByteArrays";

		// Token: 0x040014ED RID: 5357
		private static AssemblyBuilderAccess s_ProxyAssemblyBuilderAccess = AssemblyBuilderAccess.Run;

		// Token: 0x040014EE RID: 5358
		private static readonly Dictionary<Tuple<Type, string>, EntityProxyTypeInfo> _proxyNameMap = new Dictionary<Tuple<Type, string>, EntityProxyTypeInfo>();

		// Token: 0x040014EF RID: 5359
		private static readonly Dictionary<Type, EntityProxyTypeInfo> _proxyTypeMap = new Dictionary<Type, EntityProxyTypeInfo>();

		// Token: 0x040014F0 RID: 5360
		private static readonly Dictionary<Assembly, ModuleBuilder> _moduleBuilders = new Dictionary<Assembly, ModuleBuilder>();

		// Token: 0x040014F1 RID: 5361
		private static readonly ReaderWriterLockSlim _typeMapLock = new ReaderWriterLockSlim();

		// Token: 0x040014F2 RID: 5362
		private static readonly HashSet<Assembly> _proxyRuntimeAssemblies = new HashSet<Assembly>();

		// Token: 0x040014F3 RID: 5363
		internal static readonly MethodInfo GetInterceptorDelegateMethod = typeof(LazyLoadBehavior).GetOnlyDeclaredMethod("GetInterceptorDelegate");

		// Token: 0x0200057B RID: 1403
		internal class ProxyTypeBuilder
		{
			// Token: 0x060036CC RID: 14028 RVA: 0x001043B0 File Offset: 0x001025B0
			public ProxyTypeBuilder(ClrEntityType ospaceEntityType)
			{
				this._ospaceEntityType = ospaceEntityType;
				this._baseImplementor = new BaseProxyImplementor();
				this._ipocoImplementor = new IPocoImplementor(ospaceEntityType);
				this._lazyLoadImplementor = new LazyLoadImplementor(ospaceEntityType);
				this._dataContractImplementor = new DataContractImplementor(ospaceEntityType);
				this._iserializableImplementor = new SerializableImplementor(ospaceEntityType);
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x060036CD RID: 14029 RVA: 0x00104411 File Offset: 0x00102611
			public Type BaseType
			{
				get
				{
					return this._ospaceEntityType.ClrType;
				}
			}

			// Token: 0x060036CE RID: 14030 RVA: 0x0010441E File Offset: 0x0010261E
			public DynamicMethod CreateInitalizeCollectionMethod(Type proxyType)
			{
				return this._ipocoImplementor.CreateInitalizeCollectionMethod(proxyType);
			}

			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x060036CF RID: 14031 RVA: 0x0010442C File Offset: 0x0010262C
			public List<PropertyInfo> BaseGetters
			{
				get
				{
					return this._baseImplementor.BaseGetters;
				}
			}

			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x060036D0 RID: 14032 RVA: 0x00104439 File Offset: 0x00102639
			public List<PropertyInfo> BaseSetters
			{
				get
				{
					return this._baseImplementor.BaseSetters;
				}
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x060036D1 RID: 14033 RVA: 0x00104446 File Offset: 0x00102646
			public IEnumerable<EdmMember> LazyLoadMembers
			{
				get
				{
					return this._lazyLoadImplementor.Members;
				}
			}

			// Token: 0x060036D2 RID: 14034 RVA: 0x00104454 File Offset: 0x00102654
			public Type CreateType(ModuleBuilder moduleBuilder)
			{
				this._moduleBuilder = moduleBuilder;
				bool flag = false;
				if (this._iserializableImplementor.TypeIsSuitable)
				{
					foreach (EdmMember edmMember in this._ospaceEntityType.Members)
					{
						if (this._ipocoImplementor.CanProxyMember(edmMember) || this._lazyLoadImplementor.CanProxyMember(edmMember))
						{
							PropertyInfo topProperty = this.BaseType.GetTopProperty(edmMember.Name);
							PropertyBuilder propertyBuilder = this.TypeBuilder.DefineProperty(edmMember.Name, PropertyAttributes.None, topProperty.PropertyType, Type.EmptyTypes);
							if (!this._ipocoImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								EntityProxyFactory.ProxyTypeBuilder.EmitBaseSetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							if (!this._lazyLoadImplementor.EmitMember(this.TypeBuilder, edmMember, propertyBuilder, topProperty, this._baseImplementor))
							{
								EntityProxyFactory.ProxyTypeBuilder.EmitBaseGetter(this.TypeBuilder, propertyBuilder, topProperty);
							}
							flag = true;
						}
					}
					if (this._typeBuilder != null)
					{
						this._baseImplementor.Implement(this.TypeBuilder);
						this._iserializableImplementor.Implement(this.TypeBuilder, this._serializedFields);
					}
				}
				if (!flag)
				{
					return null;
				}
				return this.TypeBuilder.CreateType();
			}

			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x060036D3 RID: 14035 RVA: 0x001045B0 File Offset: 0x001027B0
			private TypeBuilder TypeBuilder
			{
				get
				{
					if (this._typeBuilder == null)
					{
						TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
						if ((this.BaseType.Attributes() & TypeAttributes.Serializable) == TypeAttributes.Serializable)
						{
							typeAttributes |= TypeAttributes.Serializable;
						}
						string text = (this.BaseType.Name.Length <= 20) ? this.BaseType.Name : this.BaseType.Name.Substring(0, 20);
						string name = string.Format(CultureInfo.InvariantCulture, "System.Data.Entity.DynamicProxies.{0}_{1}", new object[]
						{
							text,
							this._ospaceEntityType.HashedDescription
						});
						this._typeBuilder = this._moduleBuilder.DefineType(name, typeAttributes, this.BaseType, this._ipocoImplementor.Interfaces);
						this._typeBuilder.DefineDefaultConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
						Action<FieldBuilder, bool> registerField = new Action<FieldBuilder, bool>(this.RegisterInstanceField);
						this._ipocoImplementor.Implement(this._typeBuilder, registerField);
						this._lazyLoadImplementor.Implement(this._typeBuilder, registerField);
						if (!this._iserializableImplementor.TypeImplementsISerializable)
						{
							this._dataContractImplementor.Implement(this._typeBuilder);
						}
					}
					return this._typeBuilder;
				}
			}

			// Token: 0x060036D4 RID: 14036 RVA: 0x001046E4 File Offset: 0x001028E4
			private static void EmitBaseGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxyGetter(baseProperty))
				{
					MethodInfo methodInfo = baseProperty.Getter();
					MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("get_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), baseProperty.PropertyType, Type.EmptyTypes);
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Call, methodInfo);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
			}

			// Token: 0x060036D5 RID: 14037 RVA: 0x00104764 File Offset: 0x00102964
			private static void EmitBaseSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, PropertyInfo baseProperty)
			{
				if (EntityProxyFactory.CanProxySetter(baseProperty))
				{
					MethodInfo methodInfo = baseProperty.Setter();
					MethodAttributes methodAttributes = methodInfo.Attributes & MethodAttributes.MemberAccessMask;
					MethodBuilder methodBuilder = typeBuilder.DefineMethod("set_" + baseProperty.Name, methodAttributes | (MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName), null, new Type[]
					{
						baseProperty.PropertyType
					});
					ILGenerator ilgenerator = methodBuilder.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Call, methodInfo);
					ilgenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetSetMethod(methodBuilder);
				}
			}

			// Token: 0x060036D6 RID: 14038 RVA: 0x001047F7 File Offset: 0x001029F7
			private void RegisterInstanceField(FieldBuilder field, bool serializable)
			{
				if (serializable)
				{
					this._serializedFields.Add(field);
					return;
				}
				EntityProxyFactory.ProxyTypeBuilder.MarkAsNotSerializable(field);
			}

			// Token: 0x060036D7 RID: 14039 RVA: 0x00104810 File Offset: 0x00102A10
			[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
			private static ConstructorInfo TryGetScriptIgnoreAttributeConstructor()
			{
				try
				{
					if (AspProxy.IsSystemWebLoaded())
					{
						Assembly assembly = Assembly.Load("System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
						Type type = assembly.GetType("System.Web.Script.Serialization.ScriptIgnoreAttribute");
						if (type != null)
						{
							return type.GetDeclaredConstructor(new Type[0]);
						}
					}
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x060036D8 RID: 14040 RVA: 0x0010486C File Offset: 0x00102A6C
			public static void MarkAsNotSerializable(FieldBuilder field)
			{
				object[] constructorArgs = new object[0];
				field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._nonSerializedAttributeConstructor, constructorArgs));
				if (field.IsPublic)
				{
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._ignoreDataMemberAttributeConstructor, constructorArgs));
					field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._xmlIgnoreAttributeConstructor, constructorArgs));
					if (EntityProxyFactory.ProxyTypeBuilder._scriptIgnoreAttributeConstructor.Value != null)
					{
						field.SetCustomAttribute(new CustomAttributeBuilder(EntityProxyFactory.ProxyTypeBuilder._scriptIgnoreAttributeConstructor.Value, constructorArgs));
					}
				}
			}

			// Token: 0x040014F6 RID: 5366
			private TypeBuilder _typeBuilder;

			// Token: 0x040014F7 RID: 5367
			private readonly BaseProxyImplementor _baseImplementor;

			// Token: 0x040014F8 RID: 5368
			private readonly IPocoImplementor _ipocoImplementor;

			// Token: 0x040014F9 RID: 5369
			private readonly LazyLoadImplementor _lazyLoadImplementor;

			// Token: 0x040014FA RID: 5370
			private readonly DataContractImplementor _dataContractImplementor;

			// Token: 0x040014FB RID: 5371
			private readonly SerializableImplementor _iserializableImplementor;

			// Token: 0x040014FC RID: 5372
			private readonly ClrEntityType _ospaceEntityType;

			// Token: 0x040014FD RID: 5373
			private ModuleBuilder _moduleBuilder;

			// Token: 0x040014FE RID: 5374
			private readonly List<FieldBuilder> _serializedFields = new List<FieldBuilder>(3);

			// Token: 0x040014FF RID: 5375
			private static readonly ConstructorInfo _nonSerializedAttributeConstructor = typeof(NonSerializedAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04001500 RID: 5376
			private static readonly ConstructorInfo _ignoreDataMemberAttributeConstructor = typeof(IgnoreDataMemberAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04001501 RID: 5377
			private static readonly ConstructorInfo _xmlIgnoreAttributeConstructor = typeof(XmlIgnoreAttribute).GetDeclaredConstructor(new Type[0]);

			// Token: 0x04001502 RID: 5378
			private static readonly Lazy<ConstructorInfo> _scriptIgnoreAttributeConstructor = new Lazy<ConstructorInfo>(new Func<ConstructorInfo>(EntityProxyFactory.ProxyTypeBuilder.TryGetScriptIgnoreAttributeConstructor));
		}
	}
}
