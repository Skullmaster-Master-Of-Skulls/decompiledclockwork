using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000137 RID: 311
	[SecuritySafeCritical]
	[__DynamicallyInvokable]
	public class ComAwareEventInfo : EventInfo
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002478F File Offset: 0x0002298F
		[__DynamicallyInvokable]
		public ComAwareEventInfo(Type type, string eventName)
		{
			this._innerEventInfo = type.GetEvent(eventName);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000247A4 File Offset: 0x000229A4
		[__DynamicallyInvokable]
		public override void AddEventHandler(object target, Delegate handler)
		{
			if (Marshal.IsComObject(target))
			{
				Guid iid;
				int dispid;
				ComAwareEventInfo.GetDataForComInvocation(this._innerEventInfo, out iid, out dispid);
				ComEventsHelper.Combine(target, iid, dispid, handler);
				return;
			}
			this._innerEventInfo.AddEventHandler(target, handler);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000247E0 File Offset: 0x000229E0
		[__DynamicallyInvokable]
		public override void RemoveEventHandler(object target, Delegate handler)
		{
			if (Marshal.IsComObject(target))
			{
				Guid iid;
				int dispid;
				ComAwareEventInfo.GetDataForComInvocation(this._innerEventInfo, out iid, out dispid);
				ComEventsHelper.Remove(target, iid, dispid, handler);
				return;
			}
			this._innerEventInfo.RemoveEventHandler(target, handler);
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002481C File Offset: 0x00022A1C
		[__DynamicallyInvokable]
		public override EventAttributes Attributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this._innerEventInfo.Attributes;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00024829 File Offset: 0x00022A29
		[__DynamicallyInvokable]
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			return this._innerEventInfo.GetAddMethod(nonPublic);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00024837 File Offset: 0x00022A37
		[__DynamicallyInvokable]
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			return this._innerEventInfo.GetRaiseMethod(nonPublic);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00024845 File Offset: 0x00022A45
		[__DynamicallyInvokable]
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			return this._innerEventInfo.GetRemoveMethod(nonPublic);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00024853 File Offset: 0x00022A53
		[__DynamicallyInvokable]
		public override Type DeclaringType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._innerEventInfo.DeclaringType;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00024860 File Offset: 0x00022A60
		[__DynamicallyInvokable]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._innerEventInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002486F File Offset: 0x00022A6F
		[__DynamicallyInvokable]
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._innerEventInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002487D File Offset: 0x00022A7D
		[__DynamicallyInvokable]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._innerEventInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002488C File Offset: 0x00022A8C
		[__DynamicallyInvokable]
		public override string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._innerEventInfo.Name;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00024899 File Offset: 0x00022A99
		[__DynamicallyInvokable]
		public override Type ReflectedType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._innerEventInfo.ReflectedType;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000248A8 File Offset: 0x00022AA8
		private static void GetDataForComInvocation(EventInfo eventInfo, out Guid sourceIid, out int dispid)
		{
			object[] customAttributes = eventInfo.DeclaringType.GetCustomAttributes(typeof(ComEventInterfaceAttribute), false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				throw new InvalidOperationException("event invocation for COM objects requires interface to be attributed with ComSourceInterfaceGuidAttribute");
			}
			if (customAttributes.Length > 1)
			{
				throw new AmbiguousMatchException("more than one ComSourceInterfaceGuidAttribute found");
			}
			Type sourceInterface = ((ComEventInterfaceAttribute)customAttributes[0]).SourceInterface;
			Guid guid = sourceInterface.GUID;
			MethodInfo method = sourceInterface.GetMethod(eventInfo.Name);
			Attribute customAttribute = Attribute.GetCustomAttribute(method, typeof(DispIdAttribute));
			if (customAttribute == null)
			{
				throw new InvalidOperationException("event invocation for COM objects requires event to be attributed with DispIdAttribute");
			}
			sourceIid = guid;
			dispid = ((DispIdAttribute)customAttribute).Value;
		}

		// Token: 0x0400075D RID: 1885
		private EventInfo _innerEventInfo;
	}
}
