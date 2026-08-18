using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000283 RID: 643
	internal class HtmlToClrEventProxy : IReflect
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x000BCE78 File Offset: 0x000BB078
		public HtmlToClrEventProxy(object sender, string eventName, EventHandler eventHandler)
		{
			this.eventHandler = eventHandler;
			this.eventName = eventName;
			Type typeFromHandle = typeof(HtmlToClrEventProxy);
			this.typeIReflectImplementation = typeFromHandle;
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000BCEAB File Offset: 0x000BB0AB
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000BCEB3 File Offset: 0x000BB0B3
		[DispId(0)]
		public void OnHtmlEvent()
		{
			this.InvokeClrEvent();
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000BCEBB File Offset: 0x000BB0BB
		private void InvokeClrEvent()
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this.sender, EventArgs.Empty);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000BCEDB File Offset: 0x000BB0DB
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return this.typeIReflectImplementation.UnderlyingSystemType;
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000BCEE8 File Offset: 0x000BB0E8
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetField(name, bindingAttr);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000BCEF7 File Offset: 0x000BB0F7
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetFields(bindingAttr);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000BCF05 File Offset: 0x000BB105
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMember(name, bindingAttr);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000BCF14 File Offset: 0x000BB114
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMembers(bindingAttr);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000BCF22 File Offset: 0x000BB122
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMethod(name, bindingAttr);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000BCF31 File Offset: 0x000BB131
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeIReflectImplementation.GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000BCF45 File Offset: 0x000BB145
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetMethods(bindingAttr);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000BCF53 File Offset: 0x000BB153
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetProperties(bindingAttr);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000BCF61 File Offset: 0x000BB161
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return this.typeIReflectImplementation.GetProperty(name, bindingAttr);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000BCF70 File Offset: 0x000BB170
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeIReflectImplementation.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000BCF88 File Offset: 0x000BB188
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if (name == "[DISPID=0]")
			{
				this.OnHtmlEvent();
				return null;
			}
			return this.typeIReflectImplementation.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x040010DB RID: 4315
		private EventHandler eventHandler;

		// Token: 0x040010DC RID: 4316
		private IReflect typeIReflectImplementation;

		// Token: 0x040010DD RID: 4317
		private object sender;

		// Token: 0x040010DE RID: 4318
		private string eventName;
	}
}
