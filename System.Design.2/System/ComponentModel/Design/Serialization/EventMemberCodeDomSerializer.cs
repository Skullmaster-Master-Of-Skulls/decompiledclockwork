using System;
using System.CodeDom;
using System.Design;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E0 RID: 480
	internal sealed class EventMemberCodeDomSerializer : MemberCodeDomSerializer
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00067B0C File Offset: 0x00065D0C
		internal static EventMemberCodeDomSerializer Default
		{
			get
			{
				if (EventMemberCodeDomSerializer._default == null)
				{
					EventMemberCodeDomSerializer._default = new EventMemberCodeDomSerializer();
				}
				return EventMemberCodeDomSerializer._default;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00067B24 File Offset: 0x00065D24
		public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
		{
			EventDescriptor eventDescriptor = descriptor as EventDescriptor;
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			try
			{
				IEventBindingService eventBindingService = (IEventBindingService)manager.GetService(typeof(IEventBindingService));
				if (eventBindingService != null)
				{
					PropertyDescriptor eventProperty = eventBindingService.GetEventProperty(eventDescriptor);
					string text = (string)eventProperty.GetValue(value);
					if (text != null)
					{
						CodeExpression codeExpression = base.SerializeToExpression(manager, value);
						if (codeExpression != null)
						{
							CodeTypeReference delegateType = new CodeTypeReference(eventDescriptor.EventType);
							CodeDelegateCreateExpression listener = new CodeDelegateCreateExpression(delegateType, EventMemberCodeDomSerializer._thisRef, text);
							CodeEventReferenceExpression eventRef = new CodeEventReferenceExpression(codeExpression, eventDescriptor.Name);
							CodeAttachEventStatement codeAttachEventStatement = new CodeAttachEventStatement(eventRef, listener);
							codeAttachEventStatement.UserData[typeof(Delegate)] = eventDescriptor.EventType;
							statements.Add(codeAttachEventStatement);
						}
					}
				}
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				manager.ReportError(SR.GetString("SerializerPropertyGenFailed", new object[]
				{
					eventDescriptor.Name,
					innerException.Message
				}));
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			return true;
		}

		// Token: 0x040009F2 RID: 2546
		private static CodeThisReferenceExpression _thisRef = new CodeThisReferenceExpression();

		// Token: 0x040009F3 RID: 2547
		private static EventMemberCodeDomSerializer _default;
	}
}
