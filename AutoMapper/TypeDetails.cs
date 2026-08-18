using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x0200003C RID: 60
	[DebuggerDisplay("{Type}")]
	public class TypeDetails
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00005F10 File Offset: 0x00004110
		public TypeDetails(Type type) : this(type, (PropertyInfo _) => true, (FieldInfo _) => true, new MethodInfo[0])
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005F68 File Offset: 0x00004168
		public TypeDetails(Type type, Func<PropertyInfo, bool> shouldMapProperty, Func<FieldInfo, bool> shouldMapField) : this(type, shouldMapProperty, shouldMapField, new MethodInfo[0])
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00005F7C File Offset: 0x0000417C
		public TypeDetails(Type type, Func<PropertyInfo, bool> shouldMapProperty, Func<FieldInfo, bool> shouldMapField, IEnumerable<MethodInfo> sourceExtensionMethodSearch)
		{
			this.Type = type;
			Func<MemberInfo, bool> membersToMap = this.MembersToMap(shouldMapProperty, shouldMapField);
			IEnumerable<MemberInfo> allPublicReadableMembers = this.GetAllPublicReadableMembers(membersToMap);
			IEnumerable<MemberInfo> allPublicWritableMembers = this.GetAllPublicWritableMembers(membersToMap);
			this.PublicReadAccessors = TypeDetails.BuildPublicReadAccessors(allPublicReadableMembers);
			this.PublicWriteAccessors = TypeDetails.BuildPublicAccessors(allPublicWritableMembers);
			this.PublicNoArgMethods = this.BuildPublicNoArgMethods();
			this.Constructors = (from ci in type.GetDeclaredConstructors()
			where !ci.IsStatic
			select ci).ToArray<ConstructorInfo>();
			this.PublicNoArgExtensionMethods = this.BuildPublicNoArgExtensionMethods(sourceExtensionMethodSearch);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006016 File Offset: 0x00004216
		private Func<MemberInfo, bool> MembersToMap(Func<PropertyInfo, bool> shouldMapProperty, Func<FieldInfo, bool> shouldMapField)
		{
			return delegate(MemberInfo m)
			{
				PropertyInfo propertyInfo = m as PropertyInfo;
				if (propertyInfo != null)
				{
					return !propertyInfo.IsStatic() && shouldMapProperty(propertyInfo);
				}
				FieldInfo fieldInfo = (FieldInfo)m;
				return !fieldInfo.IsStatic && shouldMapField(fieldInfo);
			};
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00006036 File Offset: 0x00004236
		public Type Type { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000603E File Offset: 0x0000423E
		public IEnumerable<ConstructorInfo> Constructors { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00006046 File Offset: 0x00004246
		public IEnumerable<MemberInfo> PublicReadAccessors { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000604E File Offset: 0x0000424E
		public IEnumerable<MemberInfo> PublicWriteAccessors { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00006056 File Offset: 0x00004256
		public IEnumerable<MethodInfo> PublicNoArgMethods { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000605E File Offset: 0x0000425E
		public IEnumerable<MethodInfo> PublicNoArgExtensionMethods { get; }

		// Token: 0x06000282 RID: 642 RVA: 0x00006068 File Offset: 0x00004268
		private IList<MethodInfo> BuildPublicNoArgExtensionMethods(IEnumerable<MethodInfo> sourceExtensionMethodSearch)
		{
			MethodInfo[] source = sourceExtensionMethodSearch.ToArray<MethodInfo>();
			List<MethodInfo> list = (from method in source
			where method.GetParameters()[0].ParameterType == this.Type
			select method).ToList<MethodInfo>();
			List<Type> genericInterfaces = (from t in this.Type.GetTypeInfo().ImplementedInterfaces
			where t.IsGenericType()
			select t).ToList<Type>();
			if (this.Type.IsInterface() && this.Type.IsGenericType())
			{
				genericInterfaces.Add(this.Type);
			}
			list.AddRange(from method in source
			where method.IsGenericMethodDefinition
			let parameterType = method.GetParameters()[0].ParameterType
			let interfaceMatch = (from t in genericInterfaces
			where t.GetGenericParameters().Length == parameterType.GetTypeInfo().GenericTypeArguments.Length
			select t).FirstOrDefault((Type t) => method.MakeGenericMethod(t.GetTypeInfo().GenericTypeArguments).GetParameters()[0].ParameterType.GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
			where interfaceMatch != null
			select method.MakeGenericMethod(interfaceMatch.GetTypeInfo().GenericTypeArguments));
			return list;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000061AC File Offset: 0x000043AC
		private static MemberInfo[] BuildPublicReadAccessors(IEnumerable<MemberInfo> allMembers)
		{
			return (from x in allMembers.OfType<PropertyInfo>()
			group x by x.Name into x
			select x.First<PropertyInfo>()).OfType<MemberInfo>().Concat(from x in allMembers
			where x is FieldInfo
			select x).ToArray<MemberInfo>();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000623C File Offset: 0x0000443C
		private static MemberInfo[] BuildPublicAccessors(IEnumerable<MemberInfo> allMembers)
		{
			return (from pi in (from x in allMembers.OfType<PropertyInfo>()
			group x by x.Name).Select(delegate(IGrouping<string, PropertyInfo> x)
			{
				if (!x.Any((PropertyInfo y) => y.CanWrite && y.CanRead))
				{
					return x.First<PropertyInfo>();
				}
				return x.First((PropertyInfo y) => y.CanWrite && y.CanRead);
			})
			where pi.CanWrite || pi.PropertyType.IsListOrDictionaryType()
			select pi).OfType<MemberInfo>().Concat(from x in allMembers
			where x is FieldInfo
			select x).ToArray<MemberInfo>();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000062EF File Offset: 0x000044EF
		private IEnumerable<MemberInfo> GetAllPublicReadableMembers(Func<MemberInfo, bool> membersToMap)
		{
			return this.GetAllPublicMembers(new Func<PropertyInfo, bool>(TypeDetails.PropertyReadable), new Func<FieldInfo, bool>(this.FieldReadable), membersToMap);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006310 File Offset: 0x00004510
		private IEnumerable<MemberInfo> GetAllPublicWritableMembers(Func<MemberInfo, bool> membersToMap)
		{
			return this.GetAllPublicMembers(new Func<PropertyInfo, bool>(TypeDetails.PropertyWritable), new Func<FieldInfo, bool>(this.FieldWritable), membersToMap);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006331 File Offset: 0x00004531
		private static bool PropertyReadable(PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00006339 File Offset: 0x00004539
		private bool FieldReadable(FieldInfo fieldInfo)
		{
			return true;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000633C File Offset: 0x0000453C
		private static bool PropertyWritable(PropertyInfo propertyInfo)
		{
			bool flag = typeof(string) != propertyInfo.PropertyType && typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propertyInfo.PropertyType.GetTypeInfo());
			return propertyInfo.CanWrite || flag;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000638B File Offset: 0x0000458B
		private bool FieldWritable(FieldInfo fieldInfo)
		{
			return !fieldInfo.IsInitOnly;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006398 File Offset: 0x00004598
		private IEnumerable<MemberInfo> GetAllPublicMembers(Func<PropertyInfo, bool> propertyAvailableFor, Func<FieldInfo, bool> fieldAvailableFor, Func<MemberInfo, bool> memberAvailableFor)
		{
			List<Type> list = new List<Type>();
			Type type = this.Type;
			while (type != null)
			{
				list.Add(type);
				type = type.BaseType();
			}
			if (this.Type.IsInterface())
			{
				list.AddRange(this.Type.GetTypeInfo().ImplementedInterfaces);
			}
			Func<MemberInfo, bool> <>9__3;
			return (from x in list
			where x != null
			select x).SelectMany(delegate(Type x)
			{
				IEnumerable<MemberInfo> source = from mi in x.GetDeclaredMembers()
				where mi.DeclaringType != null && mi.DeclaringType == x
				select mi;
				Func<MemberInfo, bool> predicate;
				if ((predicate = <>9__3) == null)
				{
					predicate = (<>9__3 = ((MemberInfo m) => (m is FieldInfo && fieldAvailableFor((FieldInfo)m)) || (m is PropertyInfo && propertyAvailableFor((PropertyInfo)m) && !((PropertyInfo)m).GetIndexParameters().Any<ParameterInfo>())));
				}
				return source.Where(predicate).Where(memberAvailableFor);
			});
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00006440 File Offset: 0x00004640
		private MethodInfo[] BuildPublicNoArgMethods()
		{
			return (from mi in this.Type.GetAllMethods()
			where mi.IsPublic && !mi.IsStatic && mi.DeclaringType != typeof(object)
			select mi into m
			where m.ReturnType != typeof(void) && m.GetParameters().Length == 0
			select m).ToArray<MethodInfo>();
		}
	}
}
