using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

// Token: 0x0200000A RID: 10
internal class ClientBuildManagerTypeDescriptionProviderBridge : MarshalByRefObject
{
	// Token: 0x06000032 RID: 50 RVA: 0x0000297E File Offset: 0x00000B7E
	internal ClientBuildManagerTypeDescriptionProviderBridge(TypeDescriptionProvider typeDescriptionProvider)
	{
		this._targetFrameworkProvider = typeDescriptionProvider;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000298D File Offset: 0x00000B8D
	public override object InitializeLifetimeService()
	{
		return null;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002990 File Offset: 0x00000B90
	private Type GetReflectionType(Type type)
	{
		if (type == null)
		{
			return null;
		}
		return this._targetFrameworkProvider.GetReflectionType(type);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000029AC File Offset: 0x00000BAC
	private Type[] GetReflectionTypes(Type[] types)
	{
		if (types == null)
		{
			return null;
		}
		IEnumerable<Type> source = from t in types
		select this.GetReflectionType(t);
		return source.ToArray<Type>();
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000029D8 File Offset: 0x00000BD8
	internal bool HasProperty(Type type, string name, BindingFlags bindingAttr, Type returnType, Type[] types)
	{
		if (this._targetFrameworkProvider == null)
		{
			PropertyInfo property = type.GetProperty(name, bindingAttr, null, returnType, types, null);
			return property != null;
		}
		Type reflectionType = this.GetReflectionType(type);
		Type[] reflectionTypes = this.GetReflectionTypes(types);
		PropertyInfo property2 = reflectionType.GetProperty(name, bindingAttr, null, returnType, reflectionTypes, null);
		return property2 != null;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002A2C File Offset: 0x00000C2C
	internal bool HasField(Type type, string name, BindingFlags bindingAttr)
	{
		if (this._targetFrameworkProvider == null)
		{
			FieldInfo field = type.GetField(name, bindingAttr);
			return field != null;
		}
		Type reflectionType = this._targetFrameworkProvider.GetReflectionType(type);
		FieldInfo field2 = reflectionType.GetField(name, bindingAttr);
		return field2 != null;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002A70 File Offset: 0x00000C70
	internal bool HasEvent(Type type, string name)
	{
		if (this._targetFrameworkProvider == null)
		{
			EventInfo @event = type.GetEvent(name);
			return @event != null;
		}
		Type reflectionType = this._targetFrameworkProvider.GetReflectionType(type);
		EventInfo event2 = reflectionType.GetEvent(name);
		return event2 != null;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002AB4 File Offset: 0x00000CB4
	private string[] GetMemberNames(MemberInfo[] members)
	{
		IEnumerable<string> source = from m in members
		select m.Name;
		return source.ToArray<string>();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002AF0 File Offset: 0x00000CF0
	internal bool HasMethod(Type type, string name, BindingFlags bindingAttr)
	{
		Type type2 = type;
		if (this._targetFrameworkProvider != null)
		{
			type2 = this.GetReflectionType(type);
		}
		MethodInfo method = type2.GetMethod(name, bindingAttr);
		return method != null;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002B20 File Offset: 0x00000D20
	internal string[] GetFilteredProperties(Type type, BindingFlags bindingFlags)
	{
		PropertyInfo[] properties = type.GetProperties(bindingFlags);
		if (this._targetFrameworkProvider == null)
		{
			MemberInfo[] members = properties;
			return this.GetMemberNames(members);
		}
		Type reflectionType = this._targetFrameworkProvider.GetReflectionType(type);
		PropertyInfo[] properties2 = reflectionType.GetProperties(bindingFlags);
		IEnumerable<string> reflectionPropertyNames = from p in properties2
		select p.Name;
		return (from p in properties
		where reflectionPropertyNames.Contains(p.Name)
		select p.Name).ToArray<string>();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002BCC File Offset: 0x00000DCC
	internal string[] GetFilteredEvents(Type type, BindingFlags bindingFlags)
	{
		EventInfo[] events = type.GetEvents(bindingFlags);
		if (this._targetFrameworkProvider == null)
		{
			MemberInfo[] members = events;
			return this.GetMemberNames(members);
		}
		Type reflectionType = this._targetFrameworkProvider.GetReflectionType(type);
		EventInfo[] events2 = reflectionType.GetEvents(bindingFlags);
		IEnumerable<string> reflectionEventNames = from e in events2
		select e.Name;
		return (from e in events
		where reflectionEventNames.Contains(e.Name)
		select e.Name).ToArray<string>();
	}

	// Token: 0x04000012 RID: 18
	private TypeDescriptionProvider _targetFrameworkProvider;
}
