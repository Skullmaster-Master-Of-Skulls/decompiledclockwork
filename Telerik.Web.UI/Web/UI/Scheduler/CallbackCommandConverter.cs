using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F89 RID: 3977
	internal class CallbackCommandConverter : JavaScriptConverter
	{
		// Token: 0x0600985F RID: 39007 RVA: 0x002211F4 File Offset: 0x0021F3F4
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			ICallbackCommandFactory callbackCommandFactory;
			if (CallbackCommandConverter.CommandFactories.TryGetValue((string)dictionary["Command"], out callbackCommandFactory))
			{
				return callbackCommandFactory.FromDictionary(dictionary, serializer);
			}
			return null;
		}

		// Token: 0x06009860 RID: 39008 RVA: 0x00221229 File Offset: 0x0021F429
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17003032 RID: 12338
		// (get) Token: 0x06009861 RID: 39009 RVA: 0x00221230 File Offset: 0x0021F430
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ICallbackCommand)
				};
			}
		}

		// Token: 0x04002B81 RID: 11137
		private static readonly Dictionary<string, ICallbackCommandFactory> CommandFactories = new Dictionary<string, ICallbackCommandFactory>
		{
			{
				"DismissReminder",
				new CallbackCommandFactory<DismissReminderCommand>()
			},
			{
				"SnoozeReminder",
				new CallbackCommandFactory<SnoozeReminderCommand>()
			},
			{
				"GetSlotAppointments",
				new CallbackCommandFactory<GetSlotAppointmentsCommand>()
			}
		};
	}
}
